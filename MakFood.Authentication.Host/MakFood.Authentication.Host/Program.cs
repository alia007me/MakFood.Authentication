using MakFood.Authentication.DI;
using MakFood.Authentication.Infraustraucture.Context;
using MakFood.Authentication.Infraustraucture.Substructure.Utils.LocalAccess;
using MakFood.KGB.Auditing;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using MakFood.Authentication.Application.Service.Consumers;
using System;
using StackExchange.Redis;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AuthRegistration(builder.Configuration);
builder.Services.Configure<LocalAccessOptions>(builder.Configuration.GetSection("LocalAccess"));



builder.Services.AddDbContext<AuthDbContext>((sp, options) =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<UserRegisteredConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("user-auth-registration-queue", e =>
        {
            e.ConfigureConsumer<UserRegisteredConsumer>(context);
            e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
        });
    });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();