using BarbeariaApi.Data;
using BarbeariaApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("BarbeariaConnection");

builder.Services.AddDbContext<BarbeariaContext>(opts =>
    opts.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    )
);

// Add services to the container.
builder.Services.AddScoped<AgendamentoService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
