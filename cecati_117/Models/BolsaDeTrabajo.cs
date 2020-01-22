using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cecati_117.Models
{
    public class BolsaDeTrabajo
    {
        [Key]
        public Guid BolsaDeTrabajo_ID { get; set; }

        [DisplayName("Título")]
        public string BolsaDeTrabajo_Titulo { get; set; }

        public DateTime BolsaDeTrabajo_Fecha { get; set; } 

        [DisplayName("Descripción")]
        public string BolsaDeTrabajo_Descripción { get; set; } 
    }
}