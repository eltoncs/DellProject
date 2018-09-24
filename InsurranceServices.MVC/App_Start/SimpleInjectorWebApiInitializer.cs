using System.Web.Http;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using WebActivatorEx;
using InsuranceServices.Infra.CrossCutting.KickOff;

[assembly: PostApplicationStartMethod(typeof(InsuranceServices.Service.WebAPI.SimpleInjectorWebApiInitializer), "Initialize")]

namespace InsuranceServices.Service.WebAPI
{
    public class SimpleInjectorWebApiInitializer
    {

        /// <summary>Initialize the container and register it as Web API Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            InitializeContainer(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

        }

        private static void InitializeContainer(Container container)
        {
            SimpleInjectorMapping.Register(container);
        }

    }
}