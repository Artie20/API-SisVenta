csharp API-SisVenta/Controllers/ObjetivoImpController.cs
using API_SisVenta.Dtos;
using API_SisVenta.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API_SisVenta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ObjetivoImpController : ControllerBase
    {
        private readonly DBVENTAbakContext _context;

        public ObjetivoImpController(DBVENTAbakContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<ObjetivoImpDto>> Post([FromBody] ObjetivoImpDto dto)
        {
            var entity = new ObjetivoImpEntity
            {
                c_ObjetivoImp_SAT = dto.c_ObjetivoImp_SAT,
                descripcion = dto.descripcion,
                fechaRegistro = dto.fechaRegistro ?? DateTime.Now
            };

            var saved = await _context.AddObjetivoImp(entity);

            var result = new ObjetivoImpDto
            {
                idObjetivoImp = saved.idObjetivoImp,
                c_ObjetivoImp_SAT = saved.c_ObjetivoImp_SAT,
                descripcion = saved.descripcion,
                fechaRegistro = saved.fechaRegistro
            };

            return CreatedAtAction(nameof(Get), new { id = result.idObjetivoImp }, result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ObjetivoImpDto>> Get(int id)
        {
            var entity = await _context.GetObjetivoImp(id);
            if (entity == null) return NotFound();

            var dto = new ObjetivoImpDto
            {
                idObjetivoImp = entity.idObjetivoImp,
                c_ObjetivoImp_SAT = entity.c_ObjetivoImp_SAT,
                descripcion = entity.descripcion,
                fechaRegistro = entity.fechaRegistro
            };

            return Ok(dto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] ObjetivoImpDto dto)
        {
            if (id != dto.idObjetivoImp) return BadRequest();

            var entity = await _context.GetObjetivoImp(id);
            if (entity == null) return NotFound();

            entity.c_ObjetivoImp_SAT = dto.c_ObjetivoImp_SAT;
            entity.descripcion = dto.descripcion;
            entity.fechaRegistro = dto.fechaRegistro;

            await _context.ActualizarObjetivoImp(entity);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _context.DeleteObjetivoImp(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
