using IdentityServer.API;
using IdentityServer.API.Data;
using IdentityServer.API.Model;
using IdentityServer.API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalApiAuthentication();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var defaultConString = builder.Configuration.GetConnectionString("default");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(defaultConString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
    options.EmitStaticAudienceClaim = true;
}).AddInMemoryIdentityResources(Config.IdentityResources)
.AddInMemoryApiResources(Config.ApiResources)
.AddInMemoryApiScopes(Config.ApiScopes)
.AddInMemoryClients(Config.Clients)
.AddAspNetIdentity<ApplicationUser>()
.AddDeveloperSigningCredential()
.AddResourceOwnerValidator<IdentityResourceOwnerPasswordValidator>()
.AddExtensionGrantValidator<TokenExchangeExtensionGrantValidator>();





var app = builder.Build();

try
{
    using (var scope = app.Services.CreateScope())
    {
        var serviceProvider = scope.ServiceProvider;
        var applicationDbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
        applicationDbContext.Database.Migrate();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        if (!userManager.Users.Any())
        {
            userManager.CreateAsync(new ApplicationUser { UserName = "hikmet123asd", Email = "h-ikmet1@outlook.com", City = "Dubai" }, "Hikmet123!").Wait();
        }
    }
}
catch (Exception ex)
{
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseIdentityServer();
app.MapControllers();

app.Run();
