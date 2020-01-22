using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cecati_117.Models
{
    public class EspecialidadDetalles
    {
        [Key]
        public Guid EspecialidadDetalles_ID { get; set; }

        [DisplayName("Título")]
        public string EspecialidadDetalles_titulo { get; set; }

        [DisplayName("Descripción")]
        public string EspecialidadDetalles_descripcion { get; set; }

        [DisplayName("Lista de lo que aprenderás")]
        public string EspecialidadDetalles_ListaAprendizaje { get; set; }

        [DisplayName("Imagen de la página")]
        public string EspecialidadDetalles_Imagen { get; set; }

        [DisplayName("Mostrar página")]
        public bool EspecialidadDetalles_Mostrar { get; set; }

        #region enlace a otras tablas, clave foranea
        public virtual ListaEspecialidades Id_Especialidad { get; set; } //pide que le asignes id de tipo ListaEspecialidades
        #endregion
    }
}