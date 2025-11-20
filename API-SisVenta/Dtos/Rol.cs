namespace API_SisVenta.Dtos
{
    public class RolDto
    {
        public int idRol { get; set; }
        public string? descripcion { get; set; }
        public bool? esActivo { get; set; }
        public DateTime fechaRegistro { get; set; }

    }

    public class CreaRolDto
    {
        public string? descripcion { get; set; }
        public bool? esActivo { get; set; }
    }


}

