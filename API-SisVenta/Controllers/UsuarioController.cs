using API_Cliente.CasosDeUso;
using API_SisVenta.Dtos;
using API_SisVenta.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_SisVenta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly DBVENTAbakContext _DBventabakcontext;
        private readonly IActualizaUsuarioCasoDeUso _actualizaUsuarioCasoDeUso;

        public UsuarioController(DBVENTAbakContext DBventabakcontext, IActualizaUsuarioCasoDeUso actualizaUsuarioCasoDeUso)
        {
            _DBventabakcontext = DBventabakcontext;
            _actualizaUsuarioCasoDeUso = actualizaUsuarioCasoDeUso;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UsuarioDto>))]
        public IActionResult TraeUsuarios()
        {
            var result = _DBventabakcontext.Usuario.Include(u => u.Rol)
                                    .Select(u => u.ToDto())
                                    .ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsuarioDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> TraeUsuario(int id)
        {
            var entity = await _DBventabakcontext.Usuario
                                  .Include(u => u.Rol)
                                  .FirstOrDefaultAsync(u => u.idUsuario == id);

            if (entity == null)
                return NotFound();

            return Ok(entity.ToDto());
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UsuarioDto))]
        public async Task<IActionResult> CreaUsuario(CrearUsuarioDto usuario)
        {
            UsuarioEntity result = await _DBventabakcontext.AddUsuario(usuario);
            return Created($"http://localhost:5181/api/usuario/{result.idUsuario}", null);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> EliminaUsuario(int id)
        {
            var result = await _DBventabakcontext.Delete(id);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsuarioDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActualizaUsuario(UsuarioDto usuario)
        {
            var result = await _actualizaUsuarioCasoDeUso.Execute(usuario);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}


