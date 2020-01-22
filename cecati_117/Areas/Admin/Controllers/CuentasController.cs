using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using cecati_117.Models;
using System.Net;
using Microsoft.AspNet.Identity.EntityFramework;
using cecati_117.Areas.Admin.ClasesCompartidas;
using System.Collections.Generic;
using System.Data.Entity;

namespace cecati_117.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CuentasController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        private FuncionesUtilesController funcionesUtiles = new FuncionesUtilesController();

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        #region Constructores necesarios para acceder a cuentas de usuario
        public CuentasController()
        {
        }

        public CuentasController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        #endregion

        #region Pagina con lista de usuarios
        public ActionResult Index()
        {
            //Mas información de este codigo aqui https://www.c-sharpcorner.com/article/list-of-users-with-roles-in-mvc-asp-net-identity/ //
            //Aqui le decimos que obtenga el usuario actual y la cuenta de usuario por default para que no pueda ser eliminada mientras se ejecuta la lista
            ApplicationUser usuarioLogeado = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            var usersWithRoles = (from user in context.Users
                                  select new
                                  {
                                      UserId = user.Id,
                                      Username = user.FullName,
                                      Email = user.Email,
                                      RoleNames = (from userRole in user.Roles
                                                   join role in context.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new UsuariosConRoles()

                                  {
                                      UserId = p.UserId,
                                      Username = p.Username,
                                      Email = p.Email,
                                      Role = string.Join(",", p.RoleNames)
                                  });
            
            var usuarios = usersWithRoles.Where(x => x.Email != "cecati117.2018@gmail.com" && x.Email != usuarioLogeado.Email).ToList();
            return View(usuarios);
        }
        #endregion    

        #region Pagina para registro de usuarios
        public ActionResult Registro()
        {
            ViewBag.Roles = context.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
            ViewBag.Especialidad = context.ListaEspecialidades.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registro(RegisterViewModel model, FormCollection form, Guid[] especialidades_id)
        {
           
            if (ModelState.IsValid)
            {
                
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FullName = model.FullName};
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    string rolname = form["RoleName"];
                    result = UserManager.AddToRole(user.Id, rolname); //registro de usuario al rol https://www.codeproject.com/Questions/808785/Adding-user-to-role-upon-registration-mvc //

                    if (!rolname.Equals("Admin"))
                    {
                        Persona persona = new Persona
                        {
                            Persona_id = Guid.NewGuid(),
                            Email = user.Email = user.Email,
                            Nombre = user.FullName,                           
                            Id_usuario = user.Id,
                            Rol = rolname,
                            Genero = true
                        };
                        
                        foreach (var id_guid in especialidades_id)
                        {
                            PersonaEspecialidad personaEspecialidad = new PersonaEspecialidad
                            {
                                PersonaEspecialidad_id = Guid.NewGuid(),
                                Especialidad = context.ListaEspecialidades.Find(id_guid),
                                Persona = persona
                            };
                            context.PersonaEspecialidad.Add(personaEspecialidad);
                            persona.Especialidades.Add(personaEspecialidad);
                        }
                        context.Persona.Add(persona);
                        

                    }
                    //registro de informacion a persona                                     
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                AddErrors(result);
                ViewBag.Roles = context.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
                ViewBag.Especialidad = context.ListaEspecialidades.ToList();
                return View(model);
            }
            ViewBag.Roles = context.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
            ViewBag.Especialidad = context.ListaEspecialidades.ToList();
            // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
            return View(model);
        }
        #endregion

        #region pagina para editar usuarios
        public ActionResult EditarRegistro(string id_usuario)
        {
            var Usuario = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(id_usuario);
            ViewBag.Usuario = Usuario;
            var rolid = "";           
            rolid = Usuario.Roles.SingleOrDefault().RoleId;
            var rol_user = context.Roles.SingleOrDefault(r => r.Id == rolid).Id;

            ViewBag.Roles = context.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name, Selected = r.Id == rol_user}).ToList();           
            ViewBag.Especialidad = context.ListaEspecialidades.ToList();
            ViewBag.Persona = context.Persona.Where(x => x.Id_usuario.Equals(id_usuario)).FirstOrDefault();


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditarRegistro(RegisterViewModel model, FormCollection form, Guid[] especialidades_id, string user_id)
        {
            bool password_hasText = true;
            if (String.IsNullOrEmpty(model.Password))
            {
                ModelState.Remove("Password");
                ModelState.Remove("ConfirmPassword");
                password_hasText = false;
            }
            


            if (ModelState.IsValid)
            {
                var userToEdit = await UserManager.FindByIdAsync(user_id);
                if(userToEdit != null)
                {
                    userToEdit.UserName = model.Email;
                    userToEdit.Email = model.Email;
                    userToEdit.FullName = model.FullName;
                    var result = await UserManager.UpdateAsync(userToEdit);

                    if (result.Succeeded)
                    {

                        string role = form["RoleName"];
                        var oldRoleId = userToEdit.Roles.SingleOrDefault().RoleId;
                        var oldRoleName = context.Roles.SingleOrDefault(r => r.Id == oldRoleId).Name;

                        if (oldRoleName != role)
                        {
                            UserManager.RemoveFromRole(userToEdit.Id, oldRoleName);
                            UserManager.AddToRole(userToEdit.Id, role);
                           // context.Entry(userToEdit).State = EntityState.Modified;
                        }
                        if(password_hasText)
                        {
                            UserManager.RemovePassword(userToEdit.Id);
                            UserManager.AddPassword(userToEdit.Id, model.Password);                                  
                        }

                        //registro de informacion a persona                  
                        if (!role.Equals("Admin"))
                        {
                            var persona = context.Persona.SingleOrDefault(x => x.Id_usuario == userToEdit.Id);
                            persona.Nombre = userToEdit.FullName;
                            persona.Rol = role;
                            persona.Email = userToEdit.Email;
                            List<PersonaEspecialidad> personaEspecialidad_existentes = persona.Especialidades.ToList();
                            foreach(var especialidad in personaEspecialidad_existentes)
                            {
                                 
                                if(!especialidades_id.Any(x => x.Equals(especialidad.PersonaEspecialidad_id)))
                                {
                                    persona.Especialidades.Remove(especialidad);
                                    context.PersonaEspecialidad.Remove(especialidad);
                                }
                                
                            }
                            foreach (var id_guid in especialidades_id)
                            {
                                PersonaEspecialidad personaEspecialidad = new PersonaEspecialidad
                                {
                                    PersonaEspecialidad_id = Guid.NewGuid(),
                                    Especialidad = context.ListaEspecialidades.Find(id_guid),
                                    Persona = persona
                                };
                                context.PersonaEspecialidad.Add(personaEspecialidad);
                                persona.Especialidades.Add(personaEspecialidad);
                            }
                            context.Entry(persona).State = EntityState.Modified;
                            

                        }

                        //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);  lo comenté porque sino la cuenta que acabas de crear se logea y quita a la otra cuenta

                        // Para obtener más información sobre cómo habilitar la confirmación de cuentas y el restablecimiento de contraseña, visite https://go.microsoft.com/fwlink/?LinkID=320771
                        // Enviar correo electrónico con este vínculo
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // await UserManager.SendEmailAsync(user.Id, "Confirmar cuenta", "Para confirmar la cuenta, haga clic <a href=\"" + callbackUrl + "\">aquí</a>");
                        context.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddErrors(result);
                        var Usuari = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(user_id);
                        ViewBag.Usuario = Usuari;
                        var rolidd = "";
                        foreach (var rol in Usuari.Roles)
                        {
                            rolidd = rol.RoleId;
                        }
                        var rol_userr = context.Roles.Find(rolidd);

                        ViewBag.Roles = context.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name, Selected = r.Id == rol_userr.Id }).ToList();
                        ViewBag.Especialidad = context.ListaEspecialidades.ToList();
                        ViewBag.Persona = context.Persona.Where(x => x.Id_usuario.Equals(user_id)).FirstOrDefault();
                        return View(model);
                    }
                }


                
               
            }           
            var Usuario = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(user_id);
            ViewBag.Usuario = Usuario;
            var rolid = "";
            rolid = Usuario.Roles.SingleOrDefault().RoleId;
            var rol_user = context.Roles.SingleOrDefault(r => r.Id == rolid).Id;

            ViewBag.Roles = context.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name, Selected = r.Id == rol_user }).ToList();
            ViewBag.Especialidad = context.ListaEspecialidades.ToList();
            ViewBag.Persona = context.Persona.Where(x => x.Id_usuario.Equals(user_id)).FirstOrDefault();
            // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
            return View(model);
        }
        #endregion pagina para editar usuarios




        #region Rutina para borrar un usuario seleccionado de la lista
        public ActionResult BorrarUsuario(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = context.Users.Find(id);
            if ( user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var userId = user.Id;

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            userManager.Delete(user);
            var persona = context.Persona.Where(x => x.Id_usuario == userId).FirstOrDefault();

            if(persona != null)
            {
                funcionesUtiles.QuitarImagen_Servidor(persona.Foto_perfil, this.Server);
                List<PersonaEspecialidad> personaEspecialidad = persona.Especialidades.ToList();
                foreach (var especialidad in personaEspecialidad)
                {
                    context.PersonaEspecialidad.Remove(especialidad);
                }


                context.Persona.Remove(persona);
                context.SaveChanges();
            }
            



            return RedirectToAction("Index");
        } 
        #endregion

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

    }
}