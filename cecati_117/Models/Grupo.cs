using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace cecati_117.Models
{
    [Table("Grupo")] //para que EF no pluralize la tabla
    public class Grupo
    {
        [Key]
        public Guid Grupo_id { get; set; }

        [DisplayName("Nombre de grupo:")]
        public string Nombre_grupo { get; set; }

        [DisplayName("Turno:")]
        public string Turno { get; set; }

        [DisplayName("Ciclo escolar:")]
        public string Ciclo_escolar { get; set; }

        [DisplayName("Horario:")]
        public string Horario { get; set; }

        public string Especialidad { get; set; }

        public virtual ICollection<Persona> Personas { get; set; }

    }
}