using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cecati_117.Models
{
    public class Calificados
    {
        public Persona Persona { get; set; }
        public Subtema Subtema { get; set; }
        public Calificacion Calificacion { get; set; }
    }
}