namespace API_SisVenta.Dtos
{
    public class ProductoDto
    {
        public int idProducto { get; set; }
        public string codigoBarra { get; set; }
        public string marca { get; set; }
        public string descripcion { get; set; }
        public int? idCategoria { get; set; }
        public int stock { get; set; }
        public string urlImagen { get; set; }
        public string nombreImagen { get; set; }
        public decimal precio { get; set; }
        public bool? esActivo { get; set; }
        public DateTime? fechaRegistro { get; set; }
        public string unidadMedida { get; set; }
        public string unidadMedidaSat { get; set; }
        public string claveProductoSat { get; set; }
        public string objetoImpuesto { get; set; }
        public string factorImpuesto { get; set; }
        public string impuesto { get; set; }
        public decimal? valorImpuesto { get; set; }
        public string tipoImpuesto { get; set; }
        public decimal? descuento { get; set; }
    }

    public class CreaProductoDto
    {
        public string codigoBarra { get; set; }
        public string marca { get; set; }
        public string descripcion { get; set; }
        public int? idCategoria { get; set; }
        public int stock { get; set; }
        public string urlImagen { get; set; }
        public string nombreImagen { get; set; }
        public decimal precio { get; set; }
        public bool? esActivo { get; set; }
        public DateTime? fechaRegistro { get; set; }
        public string unidadMedida { get; set; }
        public string unidadMedidaSat { get; set; }
        public string claveProductoSat { get; set; }
        public string objetoImpuesto { get; set; }
        public string factorImpuesto { get; set; }
        public string impuesto { get; set; }
        public decimal? valorImpuesto { get; set; }
        public string tipoImpuesto { get; set; }
        public decimal? descuento { get; set; }
    }
}
