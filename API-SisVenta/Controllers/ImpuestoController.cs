using API_SisVenta.Dtos;
using API_SisVenta.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API_SisVenta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImpuestoController : ControllerBase
    {
        private readonly DBVENTAbakContext _context;

        public ImpuestoController(DBVENTAbakContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<ImpuestoDto>> Post([FromBody] ImpuestoDto dto)
        {
            var entity = new ImpuestoEntity
            {
                c_Impuesto_SAT = dto.c_Impuesto_SAT,
                descripcion = dto.descripcion,
                fechaRegistro = dto.fechaRegistro ?? DateTime.Now
            };

            var saved = await _context.AddImpuesto(entity);

            var result = new ImpuestoDto
            {
                idImpuesto = saved.idImpuesto,
                c_Impuesto_SAT = saved.c_Impuesto_SAT,
                descripcion = saved.descripcion,
                fechaRegistro = saved.fechaRegistro
            };

            return CreatedAtAction(nameof(Get), new { id = result.idImpuesto }, result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ImpuestoDto>> Get(int id)
        {
            var entity = await _context.GetImpuesto(id);
            if (entity == null) return NotFound();

            var dto = new ImpuestoDto
            {
                idImpuesto = entity.idImpuesto,
                c_Impuesto_SAT = entity.c_Impuesto_SAT,
                descripcion = entity.descripcion,
                fechaRegistro = entity.fechaRegistro
            };

            return Ok(dto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] ImpuestoDto dto)
        {
            if (id != dto.idImpuesto) return BadRequest();

            var entity = await _context.GetImpuesto(id);
            if (entity == null) return NotFound();

            entity.c_Impuesto_SAT = dto.c_Impuesto_SAT;
            entity.descripcion = dto.descripcion;
            entity.fechaRegistro = dto.fechaRegistro;

            await _context.ActualizarImpuesto(entity);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _context.DeleteImpuesto(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
