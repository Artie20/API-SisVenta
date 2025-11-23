csharp API-SisVenta/Dtos/RegimenFiscalDto.cs
using System;

namespace API_SisVenta.Dtos
{
    public class RegimenFiscalDto
    {
        public int idRegimenFiscal { get; set; }
        public string? c_RegimenFiscal { get; set; }
        public string? descripcion { get; set; }
        public DateTime? fechaRegistro { get; set; }
    }
}
