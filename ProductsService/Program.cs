using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using ProductsService.Data;
using ProductsService.Auth;
using ProductsService.Repositories;
using ProductsService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProductDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllers();

builder.Services.AddAuthentication("ApiKey")
    .AddScheme<ApiKeyOptions, ApiKeyAuthHandler>("ApiKey", options =>
    {
        options.ClaimsIssuer = builder.Configuration["ApiKey:Issuer"];
        options.ApiKey = builder.Configuration["ApiKey:Key"]!;
    });
builder.Services.AddAuthorization();

var app = builder.Build();


app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

app.Run();
public partial class Program { }