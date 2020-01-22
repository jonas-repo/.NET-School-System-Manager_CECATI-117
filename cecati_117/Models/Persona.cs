using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace cecati_117.Models
{
    [Table("Persona")] //para que EF no pluralize la tabla
    public class Persona
    {
        [Key]
        public Guid Persona_id { get; set; }

        [DisplayName("Nombre:")]
        public string Nombre { get; set; }

        [DisplayName("Apellido Paterno:")]
        public string Apellido_paterno { get; set; }

        [DisplayName("Apellido Materno:")]
        public string Apellido_materno { get; set; }

        [DisplayName("Foto de perfil:")]
        public string Foto_perfil { get; set; }

        [DisplayName("Telefono:")]
        public int Telefono { get; set; }

        [DisplayName("Email:")]
        public string Email { get; set; }

        [DisplayName("Sexo:")]
        public bool Genero { get; set; }

        [DisplayName("Numero de control:")]
        public string Numero_control { get; set; }


        public string Id_usuario { get; set; }
        public string Rol { get; set; }
        public virtual ICollection<Grupo> Grupos { get; set; }
        public virtual ICollection<PersonaEspecialidad> Especialidades { get; set; }
    }
}