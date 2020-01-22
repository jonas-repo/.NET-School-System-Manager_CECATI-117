using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cecati_117.Models
{
    public class InicioCarrusel_ListaDesplegada 
    {
        [Key]
        public Guid InicioCarrusel_ID { get; set; }

        [DisplayName("Título")]
        public string InicioCarrusel_Titulo { get; set; }

        [DisplayName("Imagen")]
        public string InicioCarrusel_ImagenURL { get; set; }

        [DisplayName("Mini imagen a mostrar")]
        public string InicioCarrusel_MiniImagenUrl { get; set; }

        public string[] InicioCarrusel_ListaDeAprendizajes { get; set; }

        public DateTime InicioCarrusel_Fecha { get; set; }
    }
}