using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cecati_117.Models
{
    public class ProximosCursos
    {
        [Key]
        public Guid ProximosCursos_ID { get; set; }

        [DisplayName("Titulo")]
        public string ProximosCursos_especialidad { get; set; }
        [DisplayName("Imagen del curso")]
        public string ProximosCursos_ImagenURL { get; set; }
        public DateTime ProximosCursos_fecha{ get; set; }


    }
}