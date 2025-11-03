
using MakFood.Authentication.DI;
using MakFood.Authentication.Infraustraucture.Context;
using MakFood.Authentication.Infraustraucture.Repositories.EF.Repository;
using MakFood.Authentication.Infraustraucture.Substructure.Utils.LocalAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen();
builder.Services.AuthRegistration(builder.Configuration);
builder.Services.Configure<LocalAccessOptions>(builder.Configuration.GetSection("LocalAccess"));


var app = builder.Build();
if (app.Environment.IsDevelopment())    
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetService<AuthDbContext>();
    if (dbContext != null)
    {
        try
        {
            dbContext.Database.Migrate();

            dbContext.SeedSuperAdmin();

            dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
 
        }
    }
    }
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
