using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cecati_117.Models
{
    public class Presentacion
    {
        [Key]
        public Guid Presentacion_ID { get; set; }

		[DisplayName("Titulo")]
		public string Presentacion_titulo { get; set; }

		[DisplayName("Descripción")]
		public string Presentacion_descripcion { get; set; }

        [DisplayName("Orden")]
        public int Presentacion_numero{ get; set; }
    }
}