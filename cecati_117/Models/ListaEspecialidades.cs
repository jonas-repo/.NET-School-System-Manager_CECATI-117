using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cecati_117.Models
{
    public class ListaEspecialidades
    {
        [Key]
        public Guid ListaEspecialidades_ID { get; set; }


        [DisplayName("Especialidad")]
        public string ListaEspecialidades_especialidad { get; set; }

        #region Enlaces a otras tablas
        public virtual ICollection<EspecialidadDetalles> EspecialidadDetalles { get; set; } //pagina con detalles de la especialidad para mostrar
        public virtual ICollection<Tema> Temas { get; set; }
        #endregion
    }
}