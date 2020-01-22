using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cecati_117.Models
{
    public class Servicios
    {
        [Key]
        public Guid Servicios_ID { get; set; }
		[DisplayName("Titulo")]
		public string Servicios_titulo { get; set; }
		[DisplayName("Descripción")]
		public string Servicios_descripcion { get; set; }

    }
}