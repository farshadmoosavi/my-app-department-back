using accounting.Repositories;
using accounting.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using accounting.Data;
using Microsoft.EntityFrameworkCore.Proxies;



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
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

builder.Services.AddDbContext<AccountingDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("HesabanAppCon")));

var connectionString = builder.Configuration.GetConnectionString("HesabanAppCon");
builder.Services.AddDbContext<AccountingDbContext>(options =>
    options.UseSqlServer(connectionString).UseLazyLoadingProxies());

using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AccountingDbContext>();
    dbContext.Database.EnsureCreated();
}

var app = builder.Build();



//Enable CORS
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action}/{id?}",
        defaults: new { controller = "Home", action = "Index" }
    );

    endpoints.MapControllerRoute(
        name: "customer",
        pattern: "api/customer/{id?}",
        defaults: new { controller = "Customer" }
    );
});

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

app.UseStaticFiles(
    new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Photos")),
        RequestPath = "/Photos"
    }
);

app.Run();


