using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cecati_117.Models
{
    public class Galeria
    {
        [Key]
        public Guid Galeria_ID { get; set; }

        [DisplayName("Titulo de la galeria")]
        public string Galeria_titulo { get; set; }

        public virtual ICollection<Fotos_galeria> Fotos_Galeria { get; set; }
    }
}