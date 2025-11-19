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
    public class ClienteController : Controller
    {
        private readonly DBVENTAbakContext _DBventabakcontext;
        private readonly IActualizaCasoDeUso _actualizaCasoDeUso;

        public ClienteController(DBVENTAbakContext DBventabakcontext, IActualizaCasoDeUso actualizaCasoDeUso)
        {
            _DBventabakcontext = DBventabakcontext;
            _actualizaCasoDeUso = actualizaCasoDeUso;
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ClienteDto>))]
        [HttpGet]
        public IActionResult TraeClientes()
        {
            var result = _DBventabakcontext.Cliente.Select(c =>c.ToDto()).ToList();
            return new OkObjectResult(result);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClienteDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> TraeCliente(int id)
        {
            ClienteEntity result = await _DBventabakcontext.Get(id);
            return new OkObjectResult(result.ToDto());

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> EliminaCliente(int id)
        {
            var result = await _DBventabakcontext.Delete(id);
            return new OkObjectResult(result);
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ClienteDto))]
        public async Task<IActionResult> CreaCliente(CreaClienteDto clientes)

        {
            ClienteEntity result = await _DBventabakcontext.Add(clientes);
            return new CreatedResult($"http://localhost:5181/api/clientes/{result.idCliente}", null);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ClienteDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActualizaCliente(ClienteDto clientes)
        {
            ClienteDto? result = await _actualizaCasoDeUso.Execute(clientes);
            if (result == null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }
    }
}


