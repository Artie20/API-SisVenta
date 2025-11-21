using API_SisVenta.Dtos;
using API_SisVenta.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API_SisVenta.CasosDeUso
{
    public interface IActualizaCategoriaCasoDeUso
    {
        Task<CategoriaDto?> Execute(CategoriaDto categoria);
    }

    public class ActualizaCategoriaCasoDeUso : IActualizaCategoriaCasoDeUso
    {
        private readonly DBVENTAbakContext _context;

        public ActualizaCategoriaCasoDeUso(DBVENTAbakContext context)
        {
            _context = context;
        }

        public async Task<CategoriaDto?> Execute(CategoriaDto dto)
        {
            var entity = await _context.Categoria
                                      .FirstOrDefaultAsync(c => c.idCategoria == dto.idCategoria);

            if (entity == null)
                return null;

            entity.descripcion = dto.descripcion;
            entity.esActivo = dto.esActivo;

            await _context.SaveChangesAsync();

            return entity.ToDto();
        }
    }
}

