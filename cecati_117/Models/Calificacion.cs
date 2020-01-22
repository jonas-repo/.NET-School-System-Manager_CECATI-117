using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace cecati_117.Models
{
    [Table("Calificacion")] //para que EF no pluralize la tabla
    public class Calificacion
    {
        [Key]
        public Guid Calificacion_id { get; set; }

        public string Calificacion_total { get; set; }

        [Required]
        public virtual Subtema Subtema { get; set; }

        [Required]
        public virtual Persona Alumno { get; set; }

    }
}