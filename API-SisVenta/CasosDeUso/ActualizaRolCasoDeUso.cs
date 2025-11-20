using API_SisVenta.Dtos;
using API_SisVenta.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API_SisVenta.CasosDeUso
{
    public interface IActualizaRolCasoDeUso
    {
        Task<RolDto?> Execute(RolDto rol);
    }

    public class ActualizaRolCasoDeUso : IActualizaRolCasoDeUso
    {
        private readonly DBVENTAbakContext _db;

        public ActualizaRolCasoDeUso(DBVENTAbakContext db)
        {
            _db = db;
        }

        public async Task<RolDto?> Execute(RolDto rol)
        {
            var entity = await _db.Rol.FirstOrDefaultAsync(x => x.idRol == rol.idRol);
            if (entity == null)
            {
                return null;
            }

            entity.descripcion = rol.descripcion;
            entity.esActivo = rol.esActivo;

            await _db.SaveChangesAsync();

            return entity.ToDto();
        }
    }

}
