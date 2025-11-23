csharp API-SisVenta/Dtos/ObjetivoImpDto.cs
using System;

namespace API_SisVenta.Dtos
{
    public class ObjetivoImpDto
    {
        public int idObjetivoImp { get; set; }
        public string? c_ObjetivoImp_SAT { get; set; }
        public string? descripcion { get; set; }
        public DateTime? fechaRegistro { get; set; }
    }
}
