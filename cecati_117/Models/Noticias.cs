using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cecati_117.Models
{
    public class Noticias
    {
        [Key]
        public Guid Noticias_ID { get; set; }

        [DisplayName("Título:")]
        public string Noticias_Titulo { get; set; }

        [DisplayName("Imagen:")]
        public string Noticias_ImagenURL { get; set; }

        [DisplayName("Descripción:")]
        public string Noticias_Descripcion { get; set; }

        public DateTime Noticias_Fecha { get; set; }
    }
}