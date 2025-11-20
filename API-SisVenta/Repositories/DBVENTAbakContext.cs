using API_SisVenta.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                .HasKey(u => u.idUsuario);

            modelBuilder.Entity<RolEntity>()
                .HasKey(r => r.idRol);

            modelBuilder.Entity<UsuarioEntity>()
                .HasOne(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.idRol);

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
            await Usuario.FirstOrDefaultAsync(x => x.idUsuario == id);

        public async Task<UsuarioEntity> AddUsuario(CrearUsuarioDto dto)
        {
            var entity = new UsuarioEntity
            {
                nombre = dto.nombre,
                correo = dto.correo,
                telefono = dto.telefono,
                idRol = dto.idRol,
                clave = dto.clave,
                esActivo = true,
                fechaRegistro = DateTime.Now
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

        //Metodos Rol
        // MÉTODOS ROL ----------------------------------------

        public async Task<RolEntity?> GetRol(int id) =>
            await Rol.FirstOrDefaultAsync(x => x.idRol == id);

        public async Task<RolEntity> AddRol(CreaRolDto dto)
        {
            var entity = new RolEntity
            {
                descripcion = dto.descripcion,
                esActivo = dto.esActivo,
                fechaRegistro = DateTime.Now
            };

            await Rol.AddAsync(entity);
            await SaveChangesAsync();

            return entity;
        }

        public async Task<RolDto?> UpdateRol(RolDto dto)
        {
            var entity = await Rol.FirstOrDefaultAsync(x => x.idRol == dto.idRol);

            if (entity == null)
                return null;

            entity.descripcion = dto.descripcion;
            entity.esActivo = dto.esActivo;

            Rol.Update(entity);
            await SaveChangesAsync();

            return entity.ToDto();
        }

        public async Task<bool> DeleteRol(int id)
        {
            var entity = await Rol.FirstOrDefaultAsync(x => x.idRol == id);

            if (entity == null)
                return false;

            Rol.Remove(entity);
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
        public int idRol { get; set; }
        public string descripcion { get; set; }
        public bool? esActivo { get; set; }
        public DateTime? fechaRegistro { get; set; }

        public ICollection<UsuarioEntity> Usuarios { get; set; }

        public RolDto ToDto()
        {
            return new RolDto
            {
                idRol = this.idRol,
                descripcion = this.descripcion,
                esActivo = this.esActivo,
            };
        }
    }

    public class UsuarioEntity
    {
        public int idUsuario { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public int? idRol { get; set; }
        public string urlFoto { get; set; }
        public string nombreFoto { get; set; }
        public string clave { get; set; }
        public bool? esActivo { get; set; }
        public DateTime? fechaRegistro { get; set; }

        public RolEntity Rol { get; set; }

        public UsuarioDto ToDto()
        {
            return new UsuarioDto
            {
                idUsuario = this.idUsuario,
                nombre = this.nombre,
                correo = this.correo,
                telefono = this.telefono,
                idRol = this.idRol
            };
        }
    }

}
