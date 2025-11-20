using API_SisVenta.Dtos;
using API_SisVenta.Repositories;

namespace API_Cliente.CasosDeUso
{
    public interface IActualizaUsuarioCasoDeUso
    {
        Task<UsuarioDto?> Execute(UsuarioDto usuario);
    }

    public class ActualizaUsuarioCasoDeUso : IActualizaUsuarioCasoDeUso
    {
        private readonly DBVENTAbakContext _DBventabakContext;

        public ActualizaUsuarioCasoDeUso(DBVENTAbakContext DBventabakContext)
        {
            _DBventabakContext = DBventabakContext;
        }

        public async Task<UsuarioDto?> Execute(UsuarioDto dto)
        {
            var entity = await _DBventabakContext.GetUsuario(dto.idUsuario);

            if (entity == null)
                return null;

            entity.nombre = dto.nombre;
            entity.correo = dto.correo;
            entity.telefono = dto.telefono;
            entity.idRol = dto.idRol;
            entity.esActivo = dto.esActivo;

            await _DBventabakContext.ActualizarUsuario(entity);

            return entity.ToDto();
        }
    }
}



