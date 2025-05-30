﻿global using InventoryManagementSystem.CQRS.Products.Commands;
global using InventoryManagementSystem.Data;
global using InventoryManagementSystem.DTO.Products;
global using MediatR;
global using Microsoft.AspNetCore.Mvc;

global using InventoryManagementSystem.DTO.Account;
global using InventoryManagementSystem.Models;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.IdentityModel.Tokens;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Text;

global using AutoMapper;
global using InventoryManagementSystem.DTO;
global using InventoryManagementSystem.Enums;
global using InventoryManagementSystem.Services;

global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore;

global using AutoMapper.QueryableExtensions;

global using System.Linq.Expressions;

global using Microsoft.AspNetCore.Authentication.JwtBearer;

global using InventoryManagementSystem.CQRS.InventoryProduct.Commands;
global using InventoryManagementSystem.DTO.ProductInventoryDto;


global using InventoryManagementSystem.CQRS.Orchestrators;
global using Microsoft.AspNetCore.Authorization;
global using System.ComponentModel.DataAnnotations.Schema;
global using InventoryManagementSystem.CQRS.Transactions.Command;
global using InventoryManagementSystem.DTO.TrasactionsDTO;

global using InventoryManagementSystem.CQRS.Notifications.Commands;

global using Hangfire;

global using InventoryManagementSystem.Jobs;
global using InventoryManagementSystem.Middlewares;

