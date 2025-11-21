using API_SisVenta.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API_SisVenta.Repositories
{
    public class DBVENTAbakContext : DbContext
    {
        public DBVENTAbakContext(DbContextOptions<DBVENTAbakContext> options) : base(options) { }
        public DbSet<ClienteEntity> Cliente { get; set; }
        public DbSet<UsuarioEntity> Usuario { get; set; }
        public DbSet<RolEntity> Rol { get; set; }
        public DbSet<NegocioEntity> Negocio { get; set; }
        public DbSet<CategoriaEntity> Categoria { get; set; }
        public DbSet<VentaEntity> Venta { get; set; }




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

            modelBuilder.Entity<NegocioEntity>()
                .HasKey(n => n.idNegocio);

            modelBuilder.Entity<VentaEntity>()
                .HasKey(v => v.IdVenta);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CategoriaEntity>(entity =>
            {
                entity.ToTable("Categoria");

                entity.HasKey(e => e.idCategoria);

                entity.Property(e => e.idCategoria)
                      .HasColumnName("idCategoria")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.descripcion)
                      .HasColumnName("descripcion")
                      .HasMaxLength(50);

                entity.Property(e => e.esActivo)
                      .HasColumnName("esActivo");

                entity.Property(e => e.fechaRegistro)
                      .HasColumnName("fechaRegistro")
                      .HasColumnType("datetime");
            });

                
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

        public async Task<NegocioEntity?> GetNegocio(int id) =>
        await Negocio.FirstOrDefaultAsync(x => x.idNegocio == id);

        public async Task<bool> ActualizarNegocio(NegocioEntity entity)
        {
            Negocio.Update(entity);
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

    public class NegocioEntity
    {
        public int idNegocio { get; set; }
        public string urlLogo { get; set; }
        public string nombreLogo { get; set; }
        public string numeroDocumento { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public decimal porcentajeImpuesto { get; set; }
        public string simboloMoneda { get; set; }

        public NegocioDto ToDto()
        {
            return new NegocioDto
            {
                idNegocio = idNegocio,
                urlLogo = urlLogo,
                nombreLogo = nombreLogo,
                numeroDocumento = numeroDocumento,
                nombre = nombre,
                correo = correo,
                direccion = direccion,
                telefono = telefono,
                porcentajeImpuesto = porcentajeImpuesto,
                simboloMoneda = simboloMoneda
            };
        }
    }

    public class CategoriaEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idCategoria { get; set; }
        public string? descripcion { get; set; }
        public bool? esActivo { get; set; }
        public DateTime? fechaRegistro { get; set; }

        public CategoriaDto ToDto()
        {
            return new CategoriaDto
            {
                idCategoria = idCategoria,
                descripcion = descripcion,
                esActivo = esActivo,
                fechaRegistro = fechaRegistro
            };
        }
    }

    public class VentaEntity
    {
        public int IdVenta { get; set; }
        public string NumeroVenta { get; set; }
        public int? IdTipoDocumentoVenta { get; set; }
        public int? IdUsuario { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? ImpuestoTotal { get; set; }
        public decimal? Total { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public int? IdCliente { get; set; }
        public decimal? Descuento { get; set; }
    }


}
