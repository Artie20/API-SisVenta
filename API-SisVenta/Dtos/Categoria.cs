namespace API_SisVenta.Dtos
{
    public class CategoriaDto
    {
        public int idCategoria { get; set; }
        public string? descripcion { get; set; }
        public bool? esActivo { get; set; }
        public DateTime? fechaRegistro { get; set; }
    }


    public class CreaCategoriaDto
    {
        public string descripcion { get; set; }
        public bool? esActivo { get; set; }
    }
}

