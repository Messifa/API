using Microsoft.EntityFrameworkCore;
using DocAppointApi.Datas;
using DocAppointApi.Repositories;
using DocAppointApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DbContextRed>(Options => Options.UseNpgsql(
    builder.Configuration.GetConnectionString("BasesConnection")
    ));
builder.Services.AddScoped<UserService, UserService>();
builder.Services.AddScoped<AdminService, AdminService>();
builder.Services.AddScoped<RDVService, RDVService>();
builder.Services.AddScoped<RDVRepository, RDVRepository>();

var app = builder.Build();

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
