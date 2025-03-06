using GestionDeTareas.Core.Application;
using GestionDeTareas.Core.Application.Hub;
using GestionDeTareas.Infrastructure.Identity.Models;
using GestionDeTareas.Infrastructure.Identity.Seeds;
using GestionDeTareas.Infrastructure.Persistence;
using GestionDeTareas.Presentation.Api.Extensions;
using Microsoft.AspNetCore.Identity;
using GestionDeTareas.Infrastructure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DI
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddIdentityLayer(builder.Configuration);

//ExceptionHandler
builder.Services.AddException();

var app = builder.Build();
app.UseExceptionHandler(_ => { });
//Configuracion para crear los roles por default y son los que configure
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var userManager = services.GetRequiredService<UserManager<User>>();
        var rolManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await DefaultRoles.SeedAsync(userManager, rolManager);
        await DefaultAdmin.SeedAsync(userManager, rolManager);

    }
    catch (Exception ex)
    {
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<NotificationHub>("SendNotification");

app.Run();
