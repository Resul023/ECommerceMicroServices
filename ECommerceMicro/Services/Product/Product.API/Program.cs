using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Product.API.Services;
using Product.API.Settings;
using Prometheus;
using Serilog;
using Serilog.Formatting.Compact;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var logger = Log.Logger = new LoggerConfiguration()
                                .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
                                .Enrich.FromLogContext()
                                .WriteTo.File(@"D:\productLog.txt")
                                .WriteTo.Console(new RenderedCompactJsonFormatter())
                                .CreateLogger();

builder.Logging.AddSerilog(logger);
builder.Logging.ClearProviders();
builder.Host.UseSerilog(logger);

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddSingleton<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerURL"];
    options.Audience = "resource_product";
    options.RequireHttpsMetadata = false;

});

builder.Services.AddMassTransit(x =>
{
    // Default Port : 5672
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQUrl"], "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });
    });
});
builder.Services.AddMassTransitHostedService();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

    var categoryService = serviceProvider.GetRequiredService<ICategoryService>();

    if (!categoryService.GetAllAsync().Result.Data.Any())
    {
        categoryService.CreateAsync(new CategoryDto { Name = "Category1" }).Wait();
        categoryService.CreateAsync(new CategoryDto { Name = "Category2" }).Wait();
    }
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthentication();
app.UseAuthorization();

//app.UseSerilogRequestLogging();

app.UseMetricServer();
app.UseHttpMetrics();
app.MapControllers();

app.Run();
