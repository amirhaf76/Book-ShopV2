using Autofac;
using Autofac.Extensions.DependencyInjection;
using BookShop.Core.DIModule;
using BookShop.Models.DataBaseLayer.DbContexts.BookShopDbContexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Settings.Configuration;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("BookShopDB")
    ?? throw new InvalidOperationException("Connection string 'BookShopDB' not found.");


var options = new ConfigurationReaderOptions { SectionName = "CustomSection" };

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

// Auth
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    var assembly = typeof(BookShopDIModule).Assembly;

    builder.RegisterAssemblyModules(assembly);
});


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

//builder.Services.AddScoped<IExceptionCaseService, DefaultExceptionCaseService>();
//builder.Services.AddScoped<IHashingPasswordStrategy, HashingPassowordStrategy>();
//builder.Services.AddScoped<IPasswordHasher<UserAccount>, BookShopPasswordHasher<UserAccount>>();
//builder.Services.AddScoped<IBookShopDbContext, BookShopDbContext>();
//builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
//builder.Services.AddScoped<IBookRepository, BookRepository>();
//builder.Services.AddScoped<IUserAccountRepository, UserAccountRepository>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.Logger.LogInformation("{test}", "test");

//Log.CloseAndFlush();

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


//app.UseHsts();

//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("Hello Worlds.");
//    // Do work that can write to the Response.
//    await next.Invoke();
//    // Do logging or other work that doesn't write to the Response.
//});

////app.Map("/yarn", async context =>
////{
////    await context.Response.WriteAsync("Mapped.yarn");
////});
////app.Map("/", async context =>
////{
////    await context.Response.WriteAsync("Mapped.");
////});


//app.Run(async context =>
//{
//    await context.Response.WriteAsync("\nHello from 3nd delegate.");
//});

//app.Run();
