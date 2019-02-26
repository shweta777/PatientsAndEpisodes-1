using Castle.Windsor;
using RestApi.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RestApi
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        private readonly IWindsorContainer container;

        public WebApiApplication()
        {
            container = new WindsorContainer().Install(new ApplicationDependencyContainer());
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //UnityConfig.RegisterComponents();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new WindsorCompositionRoot(container));
           

        }
    }
}
