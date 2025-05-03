using Hangfire;
using InventoryManagementSystem.Jobs;
using InventoryManagementSystem.Middlewares;

namespace InventoryManagementSystem
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddAutoMapper(typeof(Program).Assembly);
            builder.Services.AddScoped<TransactionMiddleware>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //AddHangfire 
            builder.Services.AddHangfire(config =>
            config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddHangfireServer();

            //connectionString
            builder.Services.AddDbContext<InventoryContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            //for authentication
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<InventoryContext>()
                .AddDefaultTokenProviders();

            //setting authentication middleware check using JWT Token
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidIssuer = builder.Configuration["JWT:Iss"],
                        ValidateAudience = false,
                        ValidAudience = builder.Configuration["JWT:Aud"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
                    };
                });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var app = builder.Build();

            MapperService.Mapper = app.Services.GetService<IMapper>();

            app.UseMiddleware<TransactionMiddleware>();
            app.UseHangfireDashboard("/dashboard");

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                string[] roles = { "Admin", "User" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            RecurringJob.AddOrUpdate<DailyNotification>(
            "low-stock-check-job",
             job => job.DailyCheckLowStockAsync(),
             Cron.Daily(12)
            );

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
