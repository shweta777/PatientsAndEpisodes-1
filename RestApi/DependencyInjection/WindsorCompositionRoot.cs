using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Castle.Windsor;

namespace RestApi.Windsor
{
    /// <summary> Windsor Composition Root </summary>
    public class WindsorCompositionRoot : IHttpControllerActivator
    {
        private readonly IWindsorContainer container;

        /// <summary> Constructor </summary>
        /// <param name="container"> </param>
        public WindsorCompositionRoot(IWindsorContainer container)
        {
            this.container = container;
        }

        /// <summary> Create IHttpController. </summary>
        /// <param name="request"> </param>
        /// <param name="controllerDescriptor"> </param>
        /// <param name="controllerType"> </param>
        /// <returns> </returns>
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            IHttpController controller = (IHttpController)container.Resolve(controllerType);

            request.RegisterForDispose(new Release(() => container.Release(controller)));

            return controller;
        }

        /// <summary> The RegisterForDispose method parameter is an IDisposable instance, and not a Release method, so you must wrap the call to the Release method in an IDisposable implementation. </summary>
        /// <remarks> http://blog.ploeh.dk/2012/10/03/DependencyInjectioninASP.NETWebAPIwithCastleWindsor/ </remarks>
        private class Release : IDisposable
        {
            private readonly Action release;

            public Release(Action release)
            {
                this.release = release;
            }

            public void Dispose()
            {
                release();
            }
        }
    }
}
