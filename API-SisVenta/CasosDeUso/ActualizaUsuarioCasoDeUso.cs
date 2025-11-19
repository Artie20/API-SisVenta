using API_SisVenta.Dtos;
using API_SisVenta.Repositories;

namespace API_Cliente.CasosDeUso
{
    public interface IActualizaUsuarioCasoDeUso
    {
        Task<UsuarioDto?> Execute(ActualizarUsuarioDto dto);
    }

    public class ActualizaUsuarioCasoDeUso : IActualizaUsuarioCasoDeUso
    {
        private readonly DBVENTAbakContext _context;

        public ActualizaUsuarioCasoDeUso(DBVENTAbakContext context)
        {
            _context = context;
        }

        public async Task<UsuarioDto?> Execute(ActualizarUsuarioDto dto)
        {
            var entity = await _context.GetUsuario(dto.idUsuario);

            if (entity == null)
                return null;

            entity.nombre = dto.nombre;
            entity.correo = dto.correo;
            entity.telefono = dto.telefono;
            entity.idRol = dto.idRol;
            entity.esActivo = dto.esActivo;

            await _context.ActualizarUsuario(entity);

            return entity.ToDto();
        }
    }
}

