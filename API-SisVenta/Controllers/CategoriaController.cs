using API_SisVenta.Dtos;
using API_SisVenta.Repositories;
using API_SisVenta.CasosDeUso;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_SisVenta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly DBVENTAbakContext _context;
        private readonly IActualizaCategoriaCasoDeUso _actualizaCategoria;

        public CategoriaController(DBVENTAbakContext context,
                                   IActualizaCategoriaCasoDeUso actualizaCategoria)
        {
            _context = context;
            _actualizaCategoria = actualizaCategoria;
        }

        // GET: api/categoria
        [HttpGet]
        public IActionResult TraeCategorias()
        {
            var result = _context.Categoria
                                .Select(c => c.ToDto())
                                .ToList();

            return Ok(result);
        }

        // GET: api/categoria/5
        [HttpGet("{id}")]
        public async Task<IActionResult> TraeCategoria(int id)
        {
            var entity = await _context.Categoria
                                      .FirstOrDefaultAsync(c => c.idCategoria == id);

            if (entity == null)
                return NotFound();

            return Ok(entity.ToDto());
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> CreaCategoria(CreaCategoriaDto dto)
        {
            var entity = new CategoriaEntity
            {
                descripcion = dto.descripcion,
                esActivo = dto.esActivo,
                fechaRegistro = DateTime.Now
            };

            await _context.Categoria.AddAsync(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(TraeCategoria),
                     new { id = entity.idCategoria },
                     entity.ToDto());
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EliminarCategoria(int id)
        {
            var result = await _context.Delete(id);

            if (!result)
                return NotFound();

            return Ok(new { mensaje = "Categoría eliminada correctamente" });
        }


        // PUT
        [HttpPut]
        public async Task<IActionResult> ActualizaCategoria(CategoriaDto dto)
        {
            var result = await _actualizaCategoria.Execute(dto);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}

