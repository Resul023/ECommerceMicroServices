using Ocelot.Middleware;
using Ocelot.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using Gateway.DelegateHandlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication()
                .AddJwtBearer("GatewayAuthenticationScheme", options =>
{
    options.Authority = builder.Configuration["IdentityServerURL"];
    options.Audience = "resource_gateway";
    options.RequireHttpsMetadata = false;
});


builder.Configuration
    .AddJsonFile($"configuration.{builder.Environment.EnvironmentName.ToLower()}.json")
    .AddEnvironmentVariables();
builder.Services.AddHttpClient<TokenExhangeDelegateHandler>();
builder.Services.AddOcelot(builder.Configuration).AddDelegatingHandler<TokenExhangeDelegateHandler>();

var app = builder.Build();

await app.UseOcelot();

app.MapGet("/", () => "Hello World!");

app.Run();
