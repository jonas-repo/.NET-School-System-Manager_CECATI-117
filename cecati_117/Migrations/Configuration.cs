namespace cecati_117.Migrations
{
    using cecati_117.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<cecati_117.Models.ApplicationDbContext>
    {
        //**https://stackoverflow.com/questions/21537558/multiple-db-contexts-in-the-same-db-and-application-in-ef-6-and-code-first-migra //
        //***https://stackoverflow.com/questions/13469881/how-do-i-enable-ef-migrations-for-multiple-contexts-to-separate-databases ///

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\ApplicationDbContext";
            ContextKey = "cecati_117.Models.ApplicationDbContext";
        }

        protected override void Seed(cecati_117.Models.ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }


            if (!context.Roles.Any(r => r.Name == "Maestro"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Maestro" };

                manager.Create(role);
            }


            if (!context.Roles.Any(r => r.Name == "Alumno"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Alumno" };

                manager.Create(role);
            }


            if (!context.Users.Any(u => u.UserName == "cecati117Manager@gmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "cecati117Manager@gmail.com", Email = "cecati117Manager@gmail.com" };

                manager.Create(user, "EducationCECATI.8989");
                manager.AddToRole(user.Id, "Admin");
            }
        }
    }
}
