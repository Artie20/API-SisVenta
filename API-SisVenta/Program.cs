using API_Cliente.CasosDeUso;
using API_SisVenta.CasosDeUso;
using API_SisVenta.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(routing => routing.LowercaseUrls = true);

// DbContext
builder.Services.AddDbContext<DBVENTAbakContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql"))
);

// Casos de uso
builder.Services.AddScoped<IActualizaCasoDeUso, ActualizaCasoDeUso>();
builder.Services.AddScoped<IActualizaUsuarioCasoDeUso, ActualizaUsuarioCasoDeUso>();
builder.Services.AddScoped<IActualizaNegocioCasoDeUso, ActualizaNegocioCasoDeUso>();
builder.Services.AddScoped<IActualizaCategoriaCasoDeUso, ActualizaCategoriaCasoDeUso>();
/*builder.Services.AddScoped<IActualizaRolCasoDeUso, ActualizaRolCasoDeUso>();*/

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
