using API_Cliente.CasosDeUso;
using API_SisVenta.Dtos;
using API_SisVenta.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static API_SisVenta.Repositories.DBVENTAbakContext;

namespace API_SisVenta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly DBVENTAbakContext _context;
        private readonly IActualizaUsuarioCasoDeUso _actualizaUsuarioCasoDeUso;

        public UsuarioController(DBVENTAbakContext context, IActualizaUsuarioCasoDeUso actualizaUsuarioCasoDeUso)
        {
            _context = context;
            _actualizaUsuarioCasoDeUso = actualizaUsuarioCasoDeUso;
        }

        // GET: api/usuario
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UsuarioDto>))]
        public IActionResult TraeUsuarios()
        {
            var result = _context.Usuario
                                 .Include(u => u.Rol)
                                 .Select(u => u.ToDto())
                                 .ToList();

            return new OkObjectResult(result);
        }

        // GET: api/usuario/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsuarioDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> TraeUsuario(int id)
        {
            var result = await _context.Usuario
                                       .Include(u => u.Rol)
                                       .FirstOrDefaultAsync(u => u.IdUsuario == id);

            if (result == null)
                return new NotFoundResult();

            return new OkObjectResult(result.ToDto());
        }

        // POST: api/usuario
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UsuarioDto))]
        public async Task<IActionResult> CreaUsuario(CreaUsuarioDto dto)
        {
            UsuarioEntity entity = await _context.AddUsuario(dto);

            return new CreatedResult($"http://localhost:5181/api/usuario/{entity.IdUsuario}", null);
        }

        // DELETE: api/usuario/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> EliminaUsuario(int id)
        {
            bool result = await _context.DeleteUsuario(id);
            return new OkObjectResult(result);
        }

        // PUT: api/usuario
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsuarioDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActualizaUsuario(UsuarioDto usuario)
        {
            UsuarioDto? result = await _actualizaUsuarioCasoDeUso.Execute(usuario);

            if (result == null)
                return new NotFoundResult();

            return new OkObjectResult(result);
        }
    }
}

