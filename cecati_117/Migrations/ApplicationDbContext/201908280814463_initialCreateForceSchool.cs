namespace cecati_117.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialCreateForceSchool : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BolsaDeTrabajoes",
                c => new
                    {
                        BolsaDeTrabajo_ID = c.Guid(nullable: false),
                        BolsaDeTrabajo_Titulo = c.String(),
                        BolsaDeTrabajo_Fecha = c.DateTime(nullable: false),
                        BolsaDeTrabajo_Descripción = c.String(),
                    })
                .PrimaryKey(t => t.BolsaDeTrabajo_ID);
            
            CreateTable(
                "dbo.Calificacion",
                c => new
                    {
                        Calificacion_id = c.Guid(nullable: false),
                        Calificacion_total = c.String(),
                        Alumno_Persona_id = c.Guid(nullable: false),
                        Subtema_Subtema_id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Calificacion_id)
                .ForeignKey("dbo.Persona", t => t.Alumno_Persona_id, cascadeDelete: true)
                .ForeignKey("dbo.Subtema", t => t.Subtema_Subtema_id, cascadeDelete: true)
                .Index(t => t.Alumno_Persona_id)
                .Index(t => t.Subtema_Subtema_id);
            
            CreateTable(
                "dbo.Persona",
                c => new
                    {
                        Persona_id = c.Guid(nullable: false),
                        Nombre = c.String(),
                        Apellido_paterno = c.String(),
                        Apellido_materno = c.String(),
                        Foto_perfil = c.String(),
                        Telefono = c.Int(nullable: false),
                        Email = c.String(),
                        Genero = c.Boolean(nullable: false),
                        Numero_control = c.String(),
                        Id_usuario = c.String(),
                        Rol = c.String(),
                    })
                .PrimaryKey(t => t.Persona_id);
            
            CreateTable(
                "dbo.PersonaEspecialidad",
                c => new
                    {
                        PersonaEspecialidad_id = c.Guid(nullable: false),
                        Especialidad_ListaEspecialidades_ID = c.Guid(nullable: false),
                        Persona_Persona_id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.PersonaEspecialidad_id)
                .ForeignKey("dbo.ListaEspecialidades", t => t.Especialidad_ListaEspecialidades_ID, cascadeDelete: true)
                .ForeignKey("dbo.Persona", t => t.Persona_Persona_id, cascadeDelete: true)
                .Index(t => t.Especialidad_ListaEspecialidades_ID)
                .Index(t => t.Persona_Persona_id);
            
            CreateTable(
                "dbo.ListaEspecialidades",
                c => new
                    {
                        ListaEspecialidades_ID = c.Guid(nullable: false),
                        ListaEspecialidades_especialidad = c.String(),
                    })
                .PrimaryKey(t => t.ListaEspecialidades_ID);
            
            CreateTable(
                "dbo.EspecialidadDetalles",
                c => new
                    {
                        EspecialidadDetalles_ID = c.Guid(nullable: false),
                        EspecialidadDetalles_titulo = c.String(),
                        EspecialidadDetalles_descripcion = c.String(),
                        EspecialidadDetalles_ListaAprendizaje = c.String(),
                        EspecialidadDetalles_Imagen = c.String(),
                        EspecialidadDetalles_Mostrar = c.Boolean(nullable: false),
                        Id_Especialidad_ListaEspecialidades_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.EspecialidadDetalles_ID)
                .ForeignKey("dbo.ListaEspecialidades", t => t.Id_Especialidad_ListaEspecialidades_ID)
                .Index(t => t.Id_Especialidad_ListaEspecialidades_ID);
            
            CreateTable(
                "dbo.Tema",
                c => new
                    {
                        Tema_id = c.Guid(nullable: false),
                        Tema_nombre = c.String(),
                        Especialidad_ListaEspecialidades_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Tema_id)
                .ForeignKey("dbo.ListaEspecialidades", t => t.Especialidad_ListaEspecialidades_ID, cascadeDelete: true)
                .Index(t => t.Especialidad_ListaEspecialidades_ID);
            
            CreateTable(
                "dbo.Subtema",
                c => new
                    {
                        Subtema_id = c.Guid(nullable: false),
                        Subtema_nombre = c.String(),
                        Tema_Tema_id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Subtema_id)
                .ForeignKey("dbo.Tema", t => t.Tema_Tema_id, cascadeDelete: true)
                .Index(t => t.Tema_Tema_id);
            
            CreateTable(
                "dbo.Grupo",
                c => new
                    {
                        Grupo_id = c.Guid(nullable: false),
                        Nombre_grupo = c.String(),
                        Turno = c.String(),
                        Ciclo_escolar = c.String(),
                        Horario = c.String(),
                        Especialidad = c.String(),
                    })
                .PrimaryKey(t => t.Grupo_id);
            
            CreateTable(
                "dbo.Fotos_galeria",
                c => new
                    {
                        Fotos_galeriaID = c.Guid(nullable: false),
                        Fotos_galeria_titulo = c.String(),
                        Fotos_galeria_autor = c.String(),
                        Fotos_galeria_imagenURL = c.String(),
                        Fotos_galeria_fecha = c.DateTime(nullable: false),
                        Id_Galeria_Galeria_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.Fotos_galeriaID)
                .ForeignKey("dbo.Galerias", t => t.Id_Galeria_Galeria_ID)
                .Index(t => t.Id_Galeria_Galeria_ID);
            
            CreateTable(
                "dbo.Galerias",
                c => new
                    {
                        Galeria_ID = c.Guid(nullable: false),
                        Galeria_titulo = c.String(),
                    })
                .PrimaryKey(t => t.Galeria_ID);
            
            CreateTable(
                "dbo.InicioCarrusels",
                c => new
                    {
                        InicioCarrusel_ID = c.Guid(nullable: false),
                        InicioCarrusel_Titulo = c.String(),
                        InicioCarrusel_ImagenURL = c.String(),
                        InicioCarrusel_MiniImagenUrl = c.String(),
                        InicioCarrusel_ListaAprendizaje = c.String(),
                        InicioCarrusel_Fecha = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.InicioCarrusel_ID);
            
            CreateTable(
                "dbo.Noticias",
                c => new
                    {
                        Noticias_ID = c.Guid(nullable: false),
                        Noticias_Titulo = c.String(),
                        Noticias_ImagenURL = c.String(),
                        Noticias_Descripcion = c.String(),
                        Noticias_Fecha = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Noticias_ID);
            
            CreateTable(
                "dbo.Presentacions",
                c => new
                    {
                        Presentacion_ID = c.Guid(nullable: false),
                        Presentacion_titulo = c.String(),
                        Presentacion_descripcion = c.String(),
                        Presentacion_numero = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Presentacion_ID);
            
            CreateTable(
                "dbo.ProximosCursos",
                c => new
                    {
                        ProximosCursos_ID = c.Guid(nullable: false),
                        ProximosCursos_especialidad = c.String(),
                        ProximosCursos_ImagenURL = c.String(),
                        ProximosCursos_fecha = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProximosCursos_ID);
            
            CreateTable(
                "dbo.RequisitosInscripcions",
                c => new
                    {
                        RequisitosInscripcion_ID = c.Guid(nullable: false),
                        RequisitosInscripcion_titulo = c.String(),
                        RequisitosInscripcion_icono = c.String(),
                        RequisitosInscripcion_descripcion = c.String(),
                    })
                .PrimaryKey(t => t.RequisitosInscripcion_ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Servicios",
                c => new
                    {
                        Servicios_ID = c.Guid(nullable: false),
                        Servicios_titulo = c.String(),
                        Servicios_descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Servicios_ID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.GrupoPersonas",
                c => new
                    {
                        Grupo_Grupo_id = c.Guid(nullable: false),
                        Persona_Persona_id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Grupo_Grupo_id, t.Persona_Persona_id })
                .ForeignKey("dbo.Grupo", t => t.Grupo_Grupo_id, cascadeDelete: true)
                .ForeignKey("dbo.Persona", t => t.Persona_Persona_id, cascadeDelete: true)
                .Index(t => t.Grupo_Grupo_id)
                .Index(t => t.Persona_Persona_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Fotos_galeria", "Id_Galeria_Galeria_ID", "dbo.Galerias");
            DropForeignKey("dbo.Calificacion", "Subtema_Subtema_id", "dbo.Subtema");
            DropForeignKey("dbo.Calificacion", "Alumno_Persona_id", "dbo.Persona");
            DropForeignKey("dbo.GrupoPersonas", "Persona_Persona_id", "dbo.Persona");
            DropForeignKey("dbo.GrupoPersonas", "Grupo_Grupo_id", "dbo.Grupo");
            DropForeignKey("dbo.PersonaEspecialidad", "Persona_Persona_id", "dbo.Persona");
            DropForeignKey("dbo.PersonaEspecialidad", "Especialidad_ListaEspecialidades_ID", "dbo.ListaEspecialidades");
            DropForeignKey("dbo.Subtema", "Tema_Tema_id", "dbo.Tema");
            DropForeignKey("dbo.Tema", "Especialidad_ListaEspecialidades_ID", "dbo.ListaEspecialidades");
            DropForeignKey("dbo.EspecialidadDetalles", "Id_Especialidad_ListaEspecialidades_ID", "dbo.ListaEspecialidades");
            DropIndex("dbo.GrupoPersonas", new[] { "Persona_Persona_id" });
            DropIndex("dbo.GrupoPersonas", new[] { "Grupo_Grupo_id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Fotos_galeria", new[] { "Id_Galeria_Galeria_ID" });
            DropIndex("dbo.Subtema", new[] { "Tema_Tema_id" });
            DropIndex("dbo.Tema", new[] { "Especialidad_ListaEspecialidades_ID" });
            DropIndex("dbo.EspecialidadDetalles", new[] { "Id_Especialidad_ListaEspecialidades_ID" });
            DropIndex("dbo.PersonaEspecialidad", new[] { "Persona_Persona_id" });
            DropIndex("dbo.PersonaEspecialidad", new[] { "Especialidad_ListaEspecialidades_ID" });
            DropIndex("dbo.Calificacion", new[] { "Subtema_Subtema_id" });
            DropIndex("dbo.Calificacion", new[] { "Alumno_Persona_id" });
            DropTable("dbo.GrupoPersonas");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Servicios");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RequisitosInscripcions");
            DropTable("dbo.ProximosCursos");
            DropTable("dbo.Presentacions");
            DropTable("dbo.Noticias");
            DropTable("dbo.InicioCarrusels");
            DropTable("dbo.Galerias");
            DropTable("dbo.Fotos_galeria");
            DropTable("dbo.Grupo");
            DropTable("dbo.Subtema");
            DropTable("dbo.Tema");
            DropTable("dbo.EspecialidadDetalles");
            DropTable("dbo.ListaEspecialidades");
            DropTable("dbo.PersonaEspecialidad");
            DropTable("dbo.Persona");
            DropTable("dbo.Calificacion");
            DropTable("dbo.BolsaDeTrabajoes");
        }
    }
}
