using Autofac;
using Autofac.Extensions.DependencyInjection;
using BookShop.Core.DIModule;
using BookShop.Core.Security.Authorization;
using BookShop.ModelsLayer.DataAccessLayer.DbContexts.BookShopDbContexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Preparing Configs.
var connectionString = builder.Configuration.GetConnectionString("BookShopDB")
    ?? throw new InvalidOperationException("Connection string 'BookShopDB' not found.");

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();


// Adding Logger Service.
builder.Host.UseSerilog(logger);

// Injecting Dependency.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    var assembly = typeof(BookShopDIModule).Assembly;

    builder.RegisterAssemblyModules(assembly);

    
});


// Setting Authentication.
builder.Services
    .AddAuthentication(authenticationOptions =>
    {
        authenticationOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        authenticationOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(jwtOptions =>
    {
        var secretKey = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);

        jwtOptions.SaveToken = true;
        jwtOptions.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(secretKey)
        };
    });


// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<BookShopDbContext>(opt => opt.UseSqlServer(connectionString));

builder.Services.AddExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.ExceptionHandler += async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        // using static System.Net.Mime.MediaTypeNames;
        context.Response.ContentType = Text.Plain;

        await context.Response.WriteAsync("An exception was thrown.");

        var exceptionHandlerPathFeature =
            context.Features.Get<IExceptionHandlerPathFeature>();

        if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
        {
            await context.Response.WriteAsync(" The file was not found.");
        }

        if (exceptionHandlerPathFeature?.Path == "/")
        {
            await context.Response.WriteAsync(" Page: Home.");
        }
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var securityScheme = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        },
        Description = "JWT Token!",
        In = ParameterLocation.Header, 
        Name = "Authorization", 
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    };
    options.AddSecurityDefinition("Bearer", securityScheme);

    // Security Requirement
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        { securityScheme, Array.Empty<string>() }
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler();
    //app.UseHsts();
}


app.UseHttpsRedirection();

app.UseRouting();

app.UseStaticFiles();


app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

