global using MealCenter.API.Registrars;
global using MealCenter.API.Extensions;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Mvc.ApiExplorer;
global using Microsoft.Extensions.Options;
global using Microsoft.OpenApi.Models;
global using Swashbuckle.AspNetCore.SwaggerGen;
global using MealCenter.API.Options;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Versioning;
global using MealCenter.Core.Communication.Mediator;
global using MealCenter.Core.Messages.CommonMessages.Notifications;
global using MediatR;
global using Microsoft.AspNetCore.Mvc.ModelBinding;
global using MealCenter.Registration.Infrastructure.Context;
global using Microsoft.EntityFrameworkCore;
global using MealCenter.Identity.Infrastructure.Context;
global using Microsoft.AspNetCore.Identity;
global using System.ComponentModel.DataAnnotations;
global using Microsoft.AspNetCore.Mvc.Filters;
global using MealCenter.API.Contracts.Identity.Requests;
global using MealCenter.API.Filters;
global using MealCenter.Identity.Application.Options;
global using Microsoft.IdentityModel.Tokens;
global using System.Text;
global using MealCenter.Identity.Application.Commands;
global using MealCenter.Identity.Application.Queries;
global using MealCenter.Identity.Application.Services;
global using MealCenter.Registration.Domain.Clients;
global using MealCenter.Registration.Infrastructure.Repository.Clients;
global using MealCenter.Registration.Application.Interfaces;
global using MealCenter.Registration.Application.Services;
global using MealCenter.Registration.Application.AutoMapper;
global using MealCenter.Registration.Application.Contracts.Clients;
global using MealCenter.Registration.Domain.Posts;
global using MealCenter.Registration.Infrastructure.Repository.Posts;
global using System.Security.Claims;
global using MealCenter.Registration.Application.Contracts.Posts;
global using MealCenter.Registration.Application.Contracts.Restaurants;
global using MealCenter.Registration.Domain.Restaurants;
global using MealCenter.Registration.Infrastructure.Repository.Restaurants;
global using MealCenter.API.Contracts.Common;
global using System.Net;
global using System.Text.Json;
global using Microsoft.AspNetCore.Diagnostics;
global using MealCenter.Core.DomainObjects;
global using MealCenter.Catalog.Application.Interfaces;
global using MealCenter.Catalog.Application.ViewModels;
global using MealCenter.Catalog.Infrastructure.Context;
global using MealCenter.Catalog.Application.Services;
global using MealCenter.Catalog.Domain;
global using MealCenter.Catalog.Infrastructure.Repository;
global using MealCenter.Orders.Application.Queries;
global using MealCenter.Orders.Application.Queries.ViewModels;
global using MealCenter.Orders.Application.Commands;






