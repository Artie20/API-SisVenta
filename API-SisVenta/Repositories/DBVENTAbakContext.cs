using API_SisVenta.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API_SisVenta.Repositories
{
    public class DBVENTAbakContext : DbContext
    {
        public DBVENTAbakContext(DbContextOptions<DBVENTAbakContext> options) : base(options)
        {
        }
        public DbSet<ClienteEntity> Cliente { get; set; } //Para que ya acceda a los campos de datos.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClienteEntity>()
                .HasKey(c => c.idCliente);

            base.OnModelCreating(modelBuilder);
        }
        public async Task<ClienteEntity?> Get(int id)
        {
            return await Cliente.FirstOrDefaultAsync(x => x.idCliente == id);
        }

        public async Task<bool> Delete(int id)
        {
            ClienteEntity entity = await Get(id);
            Cliente.Remove(entity);
            SaveChanges();
            return true;
        }
        public async Task<ClienteEntity> Add(CreaClienteDto ClienteDto) //Se hace público y asíncrono Trae a Clientes de Entity y se agrega Add en CrearClientesDto clientesDto
        {
            ClienteEntity entity = new ClienteEntity()
            {
                
                nombre = ClienteDto.nombre,
                correo = ClienteDto.correo,
                rfc = ClienteDto.rfc,
                domicilioFiscalReceptor = ClienteDto.domicilioFiscalReceptor,
                regimenFiscalReceptor = ClienteDto.regimenFiscalReceptor,
                esActivo = ClienteDto.esActivo,
                fechaRegistro = ClienteDto.fechaRegistro
            };
            EntityEntry<ClienteEntity> response = await Cliente.AddAsync(entity);
            await SaveChangesAsync();
            return await Get(response.Entity.idCliente);
        }
    }

    public class ClienteEntity
    {
        public int idCliente { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string rfc { get; set; }
        public string domicilioFiscalReceptor { get; set; }
        public string regimenFiscalReceptor { get; set; }
        public bool? esActivo { get; set; }
        public DateTime? fechaRegistro { get; set; }

        public ClienteDto ToDto()
        {
            return new ClienteDto
            {
                idCliente = idCliente,
                nombre = nombre,
                correo = correo,
                rfc = rfc,
                domicilioFiscalReceptor = domicilioFiscalReceptor,
                regimenFiscalReceptor = regimenFiscalReceptor,
                esActivo = esActivo,
                fechaRegistro = fechaRegistro
            };
        }
    }
}
