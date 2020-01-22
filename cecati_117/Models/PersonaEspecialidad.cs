using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace cecati_117.Models
{
    [Table("PersonaEspecialidad")] //para que EF no pluralize la tabla
    public class PersonaEspecialidad
    {
        [Key]
        public Guid PersonaEspecialidad_id { get; set; }

        [Required]
        public virtual ListaEspecialidades Especialidad { get; set; }

        [Required]
        public virtual Persona Persona { get; set; }

    }
}