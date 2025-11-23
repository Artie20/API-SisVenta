using API_SisVenta.Dtos;
using API_SisVenta.Repositories;

namespace API_SisVenta.CasosDeUso
{
    public interface ICrearProductoCasoDeUso
    {
        Task<ProductoDto> Execute(CreaProductoDto dto);
    }

    public class CrearProductoCasoDeUso : ICrearProductoCasoDeUso
    {
        private readonly DBVENTAbakContext _context;

        public CrearProductoCasoDeUso(DBVENTAbakContext context)
        {
            _context = context;
        }

        public async Task<ProductoDto> Execute(CreaProductoDto dto)
        {
            var entity = new ProductoEntity
            {
                codigoBarra = dto.codigoBarra,
                marca = dto.marca,
                descripcion = dto.descripcion,
                idCategoria = dto.idCategoria,
                stock = dto.stock,
                urlImagen = dto.urlImagen,
                nombreImagen = dto.nombreImagen,
                precio = dto.precio,
                esActivo = dto.esActivo,
                fechaRegistro = dto.fechaRegistro ?? DateTime.Now,
                unidadMedida = dto.unidadMedida,
                unidadMedidaSat = dto.unidadMedidaSat,
                claveProductoSat = dto.claveProductoSat,
                objetoImpuesto = dto.objetoImpuesto,
                factorImpuesto = dto.factorImpuesto,
                impuesto = dto.impuesto,
                valorImpuesto = dto.valorImpuesto,
                tipoImpuesto = dto.tipoImpuesto,
                descuento = dto.descuento
            };

            var saved = await _context.AddProducto(entity);

            return new ProductoDto
            {
                idProducto = saved.idProducto,
                codigoBarra = saved.codigoBarra,
                marca = saved.marca,
                descripcion = saved.descripcion,
                idCategoria = saved.idCategoria,
                stock = saved.stock,
                urlImagen = saved.urlImagen,
                nombreImagen = saved.nombreImagen,
                precio = saved.precio,
                esActivo = saved.esActivo,
                fechaRegistro = saved.fechaRegistro,
                unidadMedida = saved.unidadMedida,
                unidadMedidaSat = saved.unidadMedidaSat,
                claveProductoSat = saved.claveProductoSat,
                objetoImpuesto = saved.objetoImpuesto,
                factorImpuesto = saved.factorImpuesto,
                impuesto = saved.impuesto,
                valorImpuesto = saved.valorImpuesto,
                tipoImpuesto = saved.tipoImpuesto,
                descuento = saved.descuento
            };
        }
    }
}
