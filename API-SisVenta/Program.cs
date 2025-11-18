using API_SisVenta.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(routing => routing.LowercaseUrls = true);

// 👉 CONFIGURAR DbContext CON SQL SERVER Y CADENA DE CONEXIÓN
builder.Services.AddDbContext<DBVENTAbakContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
