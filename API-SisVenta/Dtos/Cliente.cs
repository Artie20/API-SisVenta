using System.ComponentModel.DataAnnotations;

namespace API_SisVenta.Dtos
{
    public class ClienteDto
    {
        public int idCliente { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string rfc { get; set; }
        public string domicilioFiscalReceptor { get; set; }
        public string regimenFiscalReceptor { get; set; }
        public bool? esActivo { get; set; }
        public DateTime? fechaRegistro { get; set; }

    }

    public class CreaClienteDto
    {
        [Required(ErrorMessage = "El nombre debe estar especificado.")]
        public string nombre { get; set; }
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "El correo no es correcto")]
        public string correo { get; set; }
        public string rfc { get; set; }
        public string domicilioFiscalReceptor { get; set; }
        public string regimenFiscalReceptor { get; set; }
        public bool? esActivo { get; set; }
        public DateTime? fechaRegistro { get; set; }

    }
}
