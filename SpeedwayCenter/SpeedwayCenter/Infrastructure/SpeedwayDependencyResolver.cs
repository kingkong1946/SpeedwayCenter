using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Ninject.Web.Common;
using SpeedwayCenter.Models.Entity_Framework;
using SpeedwayCenter.Models.Repository;

namespace SpeedwayCenter.Infrastructure
{
    public class SpeedwayDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public SpeedwayDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
            AddBindings();
        }

        private void AddBindings()
        {
            _kernel
                .Bind<IQueryRepository<Rider>>()
                .To<QueryRepository<SpeedwayContext, Rider>>()
                .WithConstructorArgument("context", new SpeedwayContext());

            _kernel
                .Bind<IQueryRepository<Team>>()
                .To<QueryRepository<SpeedwayContext, Team>>()
                .WithConstructorArgument("context", new SpeedwayContext());

            _kernel
                .Bind<IQueryRepository<Meeting>>()
                .To<QueryRepository<SpeedwayContext, Meeting>>()
                .WithConstructorArgument("context", new SpeedwayContext());

            _kernel
                .Bind<IQueryRepository<Score>>()
                .To<QueryRepository<SpeedwayContext, Score>>()
                .WithConstructorArgument("context", new SpeedwayContext());
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