using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using SpeedwayCenter.Models.Entity_Framework;
using SpeedwayCenter.Models.Repository;

namespace SpeedwayCenter.Infrastructure
{
    public class SpeedwayDependencyResolver : IDependencyResolver
    {
        private IKernel _kernel;

        public SpeedwayDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
            AddBindings();
        }

        private void AddBindings()
        {
            _kernel.Bind<IRepository<Rider>>().To<RiderRepository<Speedway>>();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
    }
}