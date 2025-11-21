using API_SisVenta.Dtos;
using API_SisVenta.Repositories;
using API_SisVenta.CasosDeUso;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_SisVenta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NegocioController : Controller
    {
        private readonly DBVENTAbakContext _DBventabakcontext;
        private readonly IActualizaNegocioCasoDeUso _actualizaNegocioCasoDeUso;

        public NegocioController(
            DBVENTAbakContext context,
            IActualizaNegocioCasoDeUso actualizaNegocioCasoDeUso)
        {
            _DBventabakcontext = context;
            _actualizaNegocioCasoDeUso = actualizaNegocioCasoDeUso;
        }

        // GET api/negocio
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<NegocioDto>))]
        public IActionResult TraeNegocios()
        {
            var result = _DBventabakcontext.Negocio
                            .Select(n => n.ToDto())
                            .ToList();

            return Ok(result);
        }

        // GET api/negocio/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NegocioDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> TraeNegocio(int id)
        {
            var entity = await _DBventabakcontext.Negocio
                              .FirstOrDefaultAsync(n => n.idNegocio == id);

            if (entity == null)
                return NotFound();

            return Ok(entity.ToDto());
        }

        // PUT api/negocio
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NegocioDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActualizaNegocio(NegocioDto negocio)
        {
            var result = await _actualizaNegocioCasoDeUso.Execute(negocio);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}

