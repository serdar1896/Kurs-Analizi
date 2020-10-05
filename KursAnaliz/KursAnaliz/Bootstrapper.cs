using Autofac;
using Autofac.Integration.Mvc;
using KursAnaliz.Business.Repository;
using KursAnaliz.Data.Model;
using System.Web.Mvc;

namespace KursAnaliz
{
    public static class Bootstrapper
    {
        public static void RunConfig()
        {
            BuildAutoFac();
        }
        // Dependency injection
        private static void BuildAutoFac()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<KurslarRepository>();
            builder.RegisterType<KursiyerlerRepository>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}