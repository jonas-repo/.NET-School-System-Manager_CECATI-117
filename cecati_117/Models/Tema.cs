using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace cecati_117.Models
{
    [Table("Tema")] //para que EF no pluralize la tabla
    public class Tema
    {
        [Key]
        public Guid Tema_id { get; set; }

        [DisplayName("Curso")]
        public string Tema_nombre { get; set; }

        [Required]
        public virtual ListaEspecialidades Especialidad { get; set; }

        public virtual ICollection<Subtema> Subtemas { get; set; }

    }
}