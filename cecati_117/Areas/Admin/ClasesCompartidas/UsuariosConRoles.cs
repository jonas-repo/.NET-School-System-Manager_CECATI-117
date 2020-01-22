using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cecati_117.Areas.Admin.ClasesCompartidas
{
    public class UsuariosConRoles
    {
        public string UserId { get; set; }

        [DisplayName("Nombre de usuario")]
        public string Username { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Rol de usuario")]
        public string Role { get; set; }
    }
}