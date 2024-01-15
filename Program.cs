using Line.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Line.Data;
using Microsoft.EntityFrameworkCore.Proxies;
using Line.Repositories.Interfaces;
using Line.Repositories.Implementations;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// ---- the code below was added by farshad:
builder.Services.AddControllersWithViews()
            // Maintain property names during serialization. See:
            .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling =Newtonsoft.Json.ReferenceLoopHandling.Ignore)
            .AddNewtonsoftJson(options =>
            options.SerializerSettings.ContractResolver = new DefaultContractResolver());

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//adding cors policy
builder.Services.AddCors(c =>
{
    c.AddPolicy("Allow origin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// this command leads to use customer in case of IcustomerRepository

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();



var connectionString = builder.Configuration.GetConnectionString("Line");
builder.Services.AddDbContext<LineDbContext>(options =>
    options.UseSqlServer(connectionString).UseLazyLoadingProxies());

using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<LineDbContext>();
    dbContext.Database.EnsureCreated();
}
builder.Services.AddDbContext<LineDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("Line")));


builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
builder.Services.AddScoped(typeof(ICurrencyRepository), typeof(CurrencyRepository));
builder.Services.AddScoped(typeof(IDocumentRepository), typeof(DocumentRepository));
builder.Services.AddScoped(typeof(IAdminRepository), typeof(AdminRepository));
builder.Services.AddScoped(typeof(ISellBuyPriceRepository), typeof(SellBuyPriceRepository));

//builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
var app = builder.Build();



//Enable CORS
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
// The code below was added by me:

//app.UseStaticFiles(
//    new StaticFileOptions
//    {
//        FileProvider = new PhysicalFileProvider(
//        Path.Combine(Directory.GetCurrentDirectory(), "Photos")),
//        RequestPath = "/Photos"
//    }
//);

app.Run();


