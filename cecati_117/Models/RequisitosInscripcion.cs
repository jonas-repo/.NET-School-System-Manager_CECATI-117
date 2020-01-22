using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cecati_117.Models
{
    public class RequisitosInscripcion
    {
        [Key]
        public Guid RequisitosInscripcion_ID { get; set; }

        [DisplayName("Titulo")]
        public string RequisitosInscripcion_titulo { get; set; }

        [DisplayName("Icono")]
        public string RequisitosInscripcion_icono { get; set; }

        [DisplayName("Descripción")]
        public string RequisitosInscripcion_descripcion { get; set; }
    }
}