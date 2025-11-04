
using MakFood.Authentication.DI;
using MakFood.Authentication.Infraustraucture.Substructure.Utils.LocalAccess;
using MakFood.FBI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen();
builder.Services.AuthRegistration(builder.Configuration);

builder.Services.Configure<LocalAccessOptions>(builder.Configuration.GetSection("LocalAccess"));





var app = builder.Build();
app.UseJwsValidation();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())    
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
