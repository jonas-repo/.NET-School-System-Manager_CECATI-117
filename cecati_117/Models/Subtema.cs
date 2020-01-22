using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace cecati_117.Models
{
    [Table("Subtema")]
    public class Subtema
    {
        [Key]
        public Guid Subtema_id { get; set; }

        [DisplayName("Tema nombre")]
        public string Subtema_nombre { get; set; }
       
        [Required]
        public virtual Tema Tema { get; set; }


        public virtual ICollection<Calificacion> Calificaciones { get; set; }
    }
}