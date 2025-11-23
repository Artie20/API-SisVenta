using API_SisVenta.CasosDeUso;
using API_SisVenta.Dtos;
using API_SisVenta.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API_SisVenta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly ICrearProductoCasoDeUso _crear;
        private readonly DBVENTAbakContext _context;

        public ProductoController(ICrearProductoCasoDeUso crear, DBVENTAbakContext context)
        {
            _crear = crear;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<ProductoDto>> Post([FromBody] CreaProductoDto dto)
        {
            var result = await _crear.Execute(dto);
            return CreatedAtAction(nameof(Get), new { id = result.idProducto }, result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductoDto>> Get(int id)
        {
            var entity = await _context.GetProducto(id);
            if (entity == null) return NotFound();

            var dto = new ProductoDto
            {
                idProducto = entity.idProducto,
                codigoBarra = entity.codigoBarra,
                marca = entity.marca,
                descripcion = entity.descripcion,
                idCategoria = entity.idCategoria,
                stock = entity.stock,
                urlImagen = entity.urlImagen,
                nombreImagen = entity.nombreImagen,
                precio = entity.precio,
                esActivo = entity.esActivo,
                fechaRegistro = entity.fechaRegistro,
                unidadMedida = entity.unidadMedida,
                unidadMedidaSat = entity.unidadMedidaSat,
                claveProductoSat = entity.claveProductoSat,
                objetoImpuesto = entity.objetoImpuesto,
                factorImpuesto = entity.factorImpuesto,
                impuesto = entity.impuesto,
                valorImpuesto = entity.valorImpuesto,
                tipoImpuesto = entity.tipoImpuesto,
                descuento = entity.descuento
            };

            return Ok(dto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductoDto dto)
        {
            if (id != dto.idProducto) return BadRequest();

            var entity = await _context.GetProducto(id);
            if (entity == null) return NotFound();

            entity.codigoBarra = dto.codigoBarra;
            entity.marca = dto.marca;
            entity.descripcion = dto.descripcion;
            entity.idCategoria = dto.idCategoria;
            entity.stock = dto.stock;
            entity.urlImagen = dto.urlImagen;
            entity.nombreImagen = dto.nombreImagen;
            entity.precio = dto.precio;
            entity.esActivo = dto.esActivo;
            entity.fechaRegistro = dto.fechaRegistro;
            entity.unidadMedida = dto.unidadMedida;
            entity.unidadMedidaSat = dto.unidadMedidaSat;
            entity.claveProductoSat = dto.claveProductoSat;
            entity.objetoImpuesto = dto.objetoImpuesto;
            entity.factorImpuesto = dto.factorImpuesto;
            entity.impuesto = dto.impuesto;
            entity.valorImpuesto = dto.valorImpuesto;
            entity.tipoImpuesto = dto.tipoImpuesto;
            entity.descuento = dto.descuento;

            await _context.ActualizarProducto(entity);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _context.DeleteProducto(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
