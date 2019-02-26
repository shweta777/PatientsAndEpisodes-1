using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using RestApi.Interfaces;
using RestApi.Models;
using RestApi.Services;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace RestApi.Windsor
{
    /// <summary> Dependency Conventions </summary>
    public class ApplicationDependencyContainer : IWindsorInstaller
    {
        /// <summary> </summary>
        /// <param name="container"> </param>
        /// <param name="store"> </param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes
                .FromThisAssembly()
                .BasedOn<IHttpController>()
                .ConfigureFor<ApiController>(c => c.PropertiesRequire(pi => false))
                .LifestyleTransient());

            container.Register(Component.For<IEpisodesService>().ImplementedBy<EpisodesService>().LifestyleSingleton());
            container.Register(Component.For<IPatientsService>().ImplementedBy<PatientsService>().LifestyleSingleton());
            container.Register(Component.For<IDatabaseContext>().ImplementedBy<PatientContext>().LifestyleSingleton());
    
        
        }
    }
}
