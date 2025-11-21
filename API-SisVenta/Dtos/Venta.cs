namespace API_SisVenta.Dtos
{
    public class VentaDto
    {
        public int idVenta { get; set; }
        public string numeroVenta { get; set; }
        public int? idTipoDocumentoVenta { get; set; }
        public int? idUsuario { get; set; }
        public decimal? subTotal { get; set; }
        public decimal? impuestoTotal { get; set; }
        public decimal? total { get; set; }
        public DateTime? fechaRegistro { get; set; }
        public int? idCliente { get; set; }
        public decimal? descuento { get; set; }
    }
}

