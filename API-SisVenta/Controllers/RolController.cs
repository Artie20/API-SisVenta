using API_SisVenta.Dtos;
using API_SisVenta.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API_SisVenta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolController : Controller
    {
        private readonly DBVENTAbakContext _context;

        public RolController(DBVENTAbakContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RolDto>))]
        public IActionResult TraeRoles()
        {
            var result = _context.Rol

                                 .Select(r => r.ToDto())
                                 .ToList();

            return new OkObjectResult(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RolDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> TraeRol(int id)
        {
            var entity = await _context.Rol.FindAsync(id);

            if (entity == null)
                return new NotFoundResult();

            return new OkObjectResult(entity.ToDto());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RolDto))]
        public async Task<IActionResult> CreaRol(CreaRolDto dto)
        {
            var entity = await _context.AddRol(dto);
            return new CreatedResult($"http://localhost:5181/api/rol/{entity.IdRol}", null);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RolDto))]
        public async Task<IActionResult> ActualizaRol(RolDto dto)
        {
            var entity = await _context.UpdateRol(dto);
            return new OkObjectResult(entity);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> EliminaRol(int id)
        {
            bool result = await _context.DeleteRol(id);
            return new OkObjectResult(result);
        }
    }
}

