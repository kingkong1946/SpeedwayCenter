using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Ninject;
using Ninject.Web.Common;
using SpeedwayCenter.ORM;
using SpeedwayCenter.ORM.Repository;

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
            _kernel.Bind(typeof(IQueryRepository<>)).To(typeof(QueryRepository<>));
            _kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>));
            _kernel.Bind<IDatabaseContext>().To<SpeedwayCenterContext>().InSingletonScope();
            _kernel.Bind<IAuthenticationProvider>().To<FormsAuthenticationProvider>();
            _kernel.Bind<MembershipProvider>().To<CustomMembershipProvider>();
            _kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope().WithConstructorArgument("list", new List<object>());
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