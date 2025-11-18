using API_SisVenta.Dtos;
using API_SisVenta.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace API_Cliente.CasosDeUso
{
    public interface IActualizaCasoDeUso
    {
        Task<ClienteDto?> Execute(ClienteDto clientes);
    }
    public class ActualizaCasoDeUso : IActualizaCasoDeUso
    {
        private readonly DBVENTAbakContext _DBventabakContext;
        public ActualizaCasoDeUso(DBVENTAbakContext DBventabakContext)
        {
            _DBventabakContext = DBventabakContext;
        }
        public async Task<ClienteDto?> Execute(ClienteDto clientes)
        {
            var entity = await _DBventabakContext.Get(clientes.idCliente);
            if (entity == null)
            {
                return null;
            }
            entity.nombre = clientes.nombre;
            entity.correo = clientes.correo;
            entity.rfc = clientes.rfc;
            entity.domicilioFiscalReceptor = clientes.domicilioFiscalReceptor;
            entity.regimenFiscalReceptor = clientes.regimenFiscalReceptor;
            entity.esActivo = clientes.esActivo;
            entity.fechaRegistro = clientes.fechaRegistro;
            await _DBventabakContext.Actualizar(entity);
            return entity.ToDto();
        }
    }
}
