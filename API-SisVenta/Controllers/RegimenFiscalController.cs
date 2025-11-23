csharp API-SisVenta/Controllers/RegimenFiscalController.cs
using API_SisVenta.Dtos;
using API_SisVenta.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API_SisVenta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegimenFiscalController : ControllerBase
    {
        private readonly DBVENTAbakContext _context;

        public RegimenFiscalController(DBVENTAbakContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<RegimenFiscalDto>> Post([FromBody] RegimenFiscalDto dto)
        {
            var entity = new RegimenFiscalEntity
            {
                c_RegimenFiscal = dto.c_RegimenFiscal,
                descripcion = dto.descripcion,
                fechaRegistro = dto.fechaRegistro ?? DateTime.Now
            };

            var saved = await _context.AddRegimenFiscal(entity);

            var result = new RegimenFiscalDto
            {
                idRegimenFiscal = saved.idRegimenFiscal,
                c_RegimenFiscal = saved.c_RegimenFiscal,
                descripcion = saved.descripcion,
                fechaRegistro = saved.fechaRegistro
            };

            return CreatedAtAction(nameof(Get), new { id = result.idRegimenFiscal }, result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<RegimenFiscalDto>> Get(int id)
        {
            var entity = await _context.GetRegimenFiscal(id);
            if (entity == null) return NotFound();

            var dto = new RegimenFiscalDto
            {
                idRegimenFiscal = entity.idRegimenFiscal,
                c_RegimenFiscal = entity.c_RegimenFiscal,
                descripcion = entity.descripcion,
                fechaRegistro = entity.fechaRegistro
            };

            return Ok(dto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] RegimenFiscalDto dto)
        {
            if (id != dto.idRegimenFiscal) return BadRequest();

            var entity = await _context.GetRegimenFiscal(id);
            if (entity == null) return NotFound();

            entity.c_RegimenFiscal = dto.c_RegimenFiscal;
            entity.descripcion = dto.descripcion;
            entity.fechaRegistro = dto.fechaRegistro;

            await _context.ActualizarRegimenFiscal(entity);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _context.DeleteRegimenFiscal(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
