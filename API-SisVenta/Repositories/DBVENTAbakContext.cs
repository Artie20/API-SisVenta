using API_SisVenta.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API_SisVenta.Repositories
{
    public class DBVENTAbakContext : DbContext
    {
        public DBVENTAbakContext(DbContextOptions<DBVENTAbakContext> options) : base(options) { }

        public DbSet<ClienteEntity> Cliente { get; set; }
        public DbSet<UsuarioEntity> Usuario { get; set; }
        public DbSet<RolEntity> Rol { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClienteEntity>()
                .HasKey(c => c.idCliente);

            modelBuilder.Entity<UsuarioEntity>()
                .HasKey(u => u.IdUsuario);

            modelBuilder.Entity<RolEntity>()
                .HasKey(r => r.IdRol);

            modelBuilder.Entity<UsuarioEntity>()
                .HasOne(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.IdRol);

            base.OnModelCreating(modelBuilder);
        }

        // Métodos Cliente
        public async Task<ClienteEntity?> Get(int id) =>
            await Cliente.FirstOrDefaultAsync(x => x.idCliente == id);

        public async Task<bool> Delete(int id)
        {
            var entity = await Get(id);
            if (entity == null) return false;

            Cliente.Remove(entity);
            await SaveChangesAsync();
            return true;
        }

        public async Task<ClienteEntity> Add(CreaClienteDto dto)
        {
            var entity = new ClienteEntity
            {
                nombre = dto.nombre,
                correo = dto.correo,
                rfc = dto.rfc,
                domicilioFiscalReceptor = dto.domicilioFiscalReceptor,
                regimenFiscalReceptor = dto.regimenFiscalReceptor,
                esActivo = dto.esActivo,
                fechaRegistro = dto.fechaRegistro
            };

            await Cliente.AddAsync(entity);
            await SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Actualizar(ClienteEntity entity)
        {
            Cliente.Update(entity);
            await SaveChangesAsync();
            return true;
        }

        // Métodos Usuario
        public async Task<UsuarioEntity?> GetUsuario(int id) =>
            await Usuario.FirstOrDefaultAsync(x => x.IdUsuario == id);

        public async Task<UsuarioEntity> AddUsuario(CrearUsuarioDto dto)
        {
            var entity = new UsuarioEntity
            {
                Nombre = dto.nombre,
                Correo = dto.correo,
                Telefono = dto.telefono,
                IdRol = dto.idRol,
                Clave = dto.clave,
                EsActivo = true,
                FechaRegistro = DateTime.Now
            };

            await Usuario.AddAsync(entity);
            await SaveChangesAsync();
            return entity;
        }

        public async Task<bool> ActualizarUsuario(UsuarioEntity entity)
        {
            Usuario.Update(entity);
            await SaveChangesAsync();
            return true;
        }
    }

    // ENTIDADES ----------------------------------------

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

        public ClienteDto ToDto() => new ClienteDto
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

    public class RolEntity
    {
        public int IdRol { get; set; }
        public string Descripcion { get; set; }
        public bool? EsActivo { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public ICollection<UsuarioEntity> Usuarios { get; set; }

        public RolDto ToDto()
        {
            return new RolDto
            {
                IdRol = this.IdRol,
                Descripcion = this.Descripcion,
                EsActivo = this.EsActivo
            };
        }
    }

    public class UsuarioEntity
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public int? IdRol { get; set; }
        public string UrlFoto { get; set; }
        public string NombreFoto { get; set; }
        public string Clave { get; set; }
        public bool? EsActivo { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public RolEntity Rol { get; set; }

    }


}

