using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace cecati_117.Models
{
    // Para agregar datos de perfil del usuario, agregue más propiedades a su clase ApplicationUser. Visite https://go.microsoft.com/fwlink/?LinkID=317594 para obtener más información.
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; internal set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("Contexto_BaseDatos", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<Noticias> Noticias { get; set; }
        public virtual DbSet<InicioCarrusel> InicioCarrusel { get; set; }
        public virtual DbSet<RequisitosInscripcion> RequisitosInscripcion { get; set; }
        public virtual DbSet<ProximosCursos> ProximosCursos { get; set; }
        public virtual DbSet<Servicios> Servicios { get; set; }
        public virtual DbSet<Presentacion> Presentacion { get; set; }
        public virtual DbSet<ListaEspecialidades> ListaEspecialidades { get; set; }
        public virtual DbSet<EspecialidadDetalles> EspecialidadDetalles { get; set; }
        public virtual DbSet<Galeria> Galerias { get; set; }
        public virtual DbSet<Fotos_galeria> Fotos_Galerias { get; set; }
        public virtual DbSet<BolsaDeTrabajo> BolsaDeTrabajo { get; set; }
        /// <summary>
        /// modelos del control escolar
        /// </summary>
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Calificacion> Calificacion { get; set; }
        public virtual DbSet<Grupo> Grupo { get; set; }
        public virtual DbSet<Tema> Tema { get; set; }
        public virtual DbSet<Subtema> Subtema { get; set; }
        public virtual DbSet<PersonaEspecialidad> PersonaEspecialidad { get; set; }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //}

    }
}