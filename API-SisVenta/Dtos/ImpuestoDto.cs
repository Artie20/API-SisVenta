csharp API-SisVenta/Dtos/ImpuestoDto.cs
using System;

namespace API_SisVenta.Dtos
{
    public class ImpuestoDto
    {
        public int idImpuesto { get; set; }
        public string? c_Impuesto_SAT { get; set; }
        public string? descripcion { get; set; }
        public DateTime? fechaRegistro { get; set; }
    }
}
