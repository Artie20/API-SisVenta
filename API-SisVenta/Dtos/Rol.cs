namespace API_SisVenta.Dtos
{
    public class RolDto
    {
        public int IdRol { get; set; }
        public string? Descripcion { get; set; }
        public bool? EsActivo { get; set; }
    }

    public class CreaRolDto
    {
        public string? Descripcion { get; set; }
        public bool? EsActivo { get; set; }
    }


}
