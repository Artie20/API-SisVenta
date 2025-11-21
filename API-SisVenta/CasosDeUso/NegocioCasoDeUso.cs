using API_SisVenta.Dtos;
using API_SisVenta.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API_SisVenta.CasosDeUso
{
   public interface IActualizaNegocioCasoDeUso
   {
       Task<NegocioDto?> Execute(NegocioDto negocio);
   }
    public class ActualizaNegocioCasoDeUso : IActualizaNegocioCasoDeUso
    {
        private readonly DBVENTAbakContext _DBventabakContext;
       public ActualizaNegocioCasoDeUso(DBVENTAbakContext DBventabakContext)
       {
           _DBventabakContext = DBventabakContext;
       }
        public async Task<NegocioDto?> Execute(NegocioDto negocio)
        {
            var entity = await _DBventabakContext.GetNegocio(negocio.idNegocio);

            if (entity == null)
                return null;

            entity.urlLogo = negocio.urlLogo;
            entity.nombreLogo = negocio.nombreLogo;
            entity.numeroDocumento = negocio.numeroDocumento;
            entity.nombre = negocio.nombre;
            entity.correo = negocio.correo;
            entity.direccion = negocio.direccion;
            entity.telefono = negocio.telefono; // CORREGIDO
            entity.porcentajeImpuesto = negocio.porcentajeImpuesto;
            entity.simboloMoneda = negocio.simboloMoneda;

            await _DBventabakContext.ActualizarNegocio(entity);

            return entity.ToDto();
        }

    }
}
