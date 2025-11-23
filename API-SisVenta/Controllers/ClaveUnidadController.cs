using API_SisVenta.Dtos;
using API_SisVenta.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API_SisVenta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClaveUnidadController : ControllerBase
    {
        private readonly DBVENTAbakContext _context;

        public ClaveUnidadController(DBVENTAbakContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<ClaveUnidadDto>> Post([FromBody] ClaveUnidadDto dto)
        {
            var entity = new ClaveUnidadEntity
            {
                c_ClaveUnidad_SAT = dto.c_ClaveUnidad_SAT,
                descripcion = dto.descripcion,
                fechaRegistro = dto.fechaRegistro ?? DateTime.Now
            };

            var saved = await _context.AddClaveUnidad(entity);

            var result = new ClaveUnidadDto
            {
                idClaveUnidad_SAT = saved.idClaveUnidad_SAT,
                c_ClaveUnidad_SAT = saved.c_ClaveUnidad_SAT,
                descripcion = saved.descripcion,
                fechaRegistro = saved.fechaRegistro
            };

            return CreatedAtAction(nameof(Get), new { id = result.idClaveUnidad_SAT }, result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ClaveUnidadDto>> Get(int id)
        {
            var entity = await _context.GetClaveUnidad(id);
            if (entity == null) return NotFound();

            var dto = new ClaveUnidadDto
            {
                idClaveUnidad_SAT = entity.idClaveUnidad_SAT,
                c_ClaveUnidad_SAT = entity.c_ClaveUnidad_SAT,
                descripcion = entity.descripcion,
                fechaRegistro = entity.fechaRegistro
            };

            return Ok(dto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] ClaveUnidadDto dto)
        {
            if (id != dto.idClaveUnidad_SAT) return BadRequest();

            var entity = await _context.GetClaveUnidad(id);
            if (entity == null) return NotFound();

            entity.c_ClaveUnidad_SAT = dto.c_ClaveUnidad_SAT;
            entity.descripcion = dto.descripcion;
            entity.fechaRegistro = dto.fechaRegistro;

            await _context.ActualizarClaveUnidad(entity);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _context.DeleteClaveUnidad(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
