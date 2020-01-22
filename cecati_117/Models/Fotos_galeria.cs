using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cecati_117.Models
{
    public class Fotos_galeria
    {
        [Key]
        public Guid Fotos_galeriaID { get; set; }

        [DisplayName("Titulo de las fotos")]
        public string Fotos_galeria_titulo { get; set; }

        [DisplayName("Autor de las fotos")]
        public string Fotos_galeria_autor { get; set; }

        public string Fotos_galeria_imagenURL { get; set; }
        public DateTime Fotos_galeria_fecha { get; set; }
        
        public virtual Galeria Id_Galeria { get; set; }
    }
}