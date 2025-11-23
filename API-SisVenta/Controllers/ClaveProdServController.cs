using API_SisVenta.Dtos;
using API_SisVenta.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace API_SisVenta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClaveProdServController : ControllerBase
    {
        private readonly DBVENTAbakContext _context;

        public ClaveProdServController(DBVENTAbakContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<ClaveProdServDto>> Post([FromBody] ClaveProdServDto dto)
        {
            var entity = new ClaveProdServEntity
            {
                c_ClaveProdServ_SAT = dto.c_ClaveProdServ_SAT,
                descripcion = dto.descripcion,
                fechaRegistro = dto.fechaRegistro ?? DateTime.Now
            };

            var saved = await _context.AddClaveProdServ(entity);

            var result = new ClaveProdServDto
            {
                idClaveProdServ_SAT = saved.idClaveProdServ_SAT,
                c_ClaveProdServ_SAT = saved.c_ClaveProdServ_SAT,
                descripcion = saved.descripcion,
                fechaRegistro = saved.fechaRegistro
            };

            return CreatedAtAction(nameof(Get), new { id = result.idClaveProdServ_SAT }, result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ClaveProdServDto>> Get(int id)
        {
            var entity = await _context.GetClaveProdServ(id);
            if (entity == null) return NotFound();

            var dto = new ClaveProdServDto
            {
                idClaveProdServ_SAT = entity.idClaveProdServ_SAT,
                c_ClaveProdServ_SAT = entity.c_ClaveProdServ_SAT,
                descripcion = entity.descripcion,
                fechaRegistro = entity.fechaRegistro
            };

            return Ok(dto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] ClaveProdServDto dto)
        {
            if (id != dto.idClaveProdServ_SAT) return BadRequest();

            var entity = await _context.GetClaveProdServ(id);
            if (entity == null) return NotFound();

            entity.c_ClaveProdServ_SAT = dto.c_ClaveProdServ_SAT;
            entity.descripcion = dto.descripcion;
            entity.fechaRegistro = dto.fechaRegistro;

            await _context.ActualizarClaveProdServ(entity);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _context.DeleteClaveProdServ(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
