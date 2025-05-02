using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class MakeWarehousesIDFromNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transactions_warehouses_warehousesIDFrom",
                table: "transactions");

            migrationBuilder.AlterColumn<int>(
                name: "warehousesIDFrom",
                table: "transactions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_warehouses_warehousesIDFrom",
                table: "transactions",
                column: "warehousesIDFrom",
                principalTable: "warehouses",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transactions_warehouses_warehousesIDFrom",
                table: "transactions");

            migrationBuilder.AlterColumn<int>(
                name: "warehousesIDFrom",
                table: "transactions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_warehouses_warehousesIDFrom",
                table: "transactions",
                column: "warehousesIDFrom",
                principalTable: "warehouses",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
