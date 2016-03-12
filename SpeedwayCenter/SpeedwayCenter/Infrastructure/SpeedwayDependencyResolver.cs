using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Ninject.Web.Common;
using SpeedwayCenter.Models.FluentApi;
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
                .To<QueryRepository<SpeedwayCenterContext, Rider>>()
                .WithConstructorArgument("context", new SpeedwayCenterContext());

            //_kernel
            //    .Bind<IQueryRepository<Team>>()
            //    .To<QueryRepository<SpeedwayCenterContext, Team>>()
            //    .WithConstructorArgument("context", new SpeedwayCenterContext());

            //_kernel
            //    .Bind<IQueryRepository<Meeting>>()
            //    .To<QueryRepository<SpeedwayCenterContext, Meeting>>()
            //    .WithConstructorArgument("context", new SpeedwayCenterContext());

            //_kernel
            //    .Bind<IQueryRepository<Score>>()
            //    .To<QueryRepository<SpeedwayCenterContext, Score>>()
            //    .WithConstructorArgument("context", new SpeedwayCenterContext());
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