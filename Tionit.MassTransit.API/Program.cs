using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Tionit.MassTransit.API.Entities;

var builder = WebApplication.CreateBuilder(args);

var connectionstring = builder.Configuration.GetConnectionString("DatabasConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionstring));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowSpecificOrigins",
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:3000")
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                      });
                    });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tionit.MassTransit.API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger(c => c.RouteTemplate = "/swagger/{documentName}/swagger.json");

app.UseSwaggerUI(c =>
{
    c.RoutePrefix = "swagger";
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "HomeAnimals v1");
    c.InjectStylesheet("/swagger-ui/custom.css");

});

app.UseHttpsRedirection();
app.MapControllers();
app.UseCors();
app.UseCors("MyAllowSpecificOrigins");

app.UseAuthorization();
app.UseRouting();
 

app.Run();
