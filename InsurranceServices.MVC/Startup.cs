using InsuranceServices.Application.Services;
using InsuranceServices.Domain.Entities;
using InsuranceServices.Domain.Services;
using InsuranceServices.Infra.Data.Context;
using InsuranceServices.Infra.Data.Repository;
using InsuranceServices.Infra.Data.UnitOfWork;
using InsurranceServices.MVC.App_Start;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System.Threading.Tasks;

[assembly: OwinStartupAttribute(typeof(InsurranceServices.MVC.Startup))]
namespace InsurranceServices.MVC
{
    public partial class Startup
    {
        
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
            InitializeDb();
        }

        private void createRolesandUsers()
        {
            IdentityContext context = new IdentityContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            IdentityRole role;
            ApplicationUser user;
            string userPWD;
            IdentityResult chkUser;

            if (!roleManager.RoleExists("Administrators"))
            {
                role = new IdentityRole();
                role.Name = "Administrators";
                roleManager.Create(role);

                user = new ApplicationUser();
                user.UserName = "adm@acme.com";
                user.Email = "adm@acme.com";
                userPWD = "default@123";
                chkUser = UserManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Administrators");
                }
            }

            if (!roleManager.RoleExists("Contributors"))
            {
                role = new IdentityRole();
                role.Name = "Contributors";
                roleManager.Create(role);

                user = new ApplicationUser();
                user.UserName = "ctb@acme.com";
                user.Email = "ctb@acme.com";
                userPWD = "default@123";
                chkUser = UserManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Contributors");
                }
            }            
        }

        private void InitializeDb()
        {
            var dbContext = new ISContext();
            var repo = new InsuranceTypeRepository(dbContext);
            var domainService = new InsuranceTypeService(repo);
            var appService = new InsuranceTypeAppService(domainService, new UnitOfWork(dbContext));
            var service = new ServiceStartUp(appService);
            service.SaveInsuranceTypes();//could not use async for this method, due to signature problems
            
        }
    }
}
