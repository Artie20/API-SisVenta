namespace API_SisVenta.Dtos
{
    public class UsuarioDto
    {
        public int idUsuario { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public int? idRol { get; set; }
        public string urlFoto { get; set; }
        public string nombreFoto { get; set; }
        public bool esActivo { get; set; }
        public DateTime fechaRegistro { get; set; }
    }

    public class CrearUsuarioDto
    {
        public string nombre { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public int? idRol { get; set; }
        public string clave { get; set; }
    }

    public class ActualizarUsuarioDto
    {
        public int idUsuario { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public int? idRol { get; set; }
        public bool esActivo { get; set; }
    }
}

