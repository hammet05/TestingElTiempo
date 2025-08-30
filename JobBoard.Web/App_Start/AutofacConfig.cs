using AutoMapper;
using JobBoard.Data.Repository;
using JobBoard.Data.UnitOfWork;
using System.Reflection;
using System.Web.Compilation;
using Autofac;
using Autofac.Integration.Mvc;
using JobBoard.Data;
using System;
using System.Linq;

namespace JobBoard.Web.App_Start
{
    public static class AutofacConfig
    {
        public static IContainer RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            var webAsm = Assembly.GetExecutingAssembly();

            builder.RegisterControllers(webAsm).PropertiesAutowired();
            builder.RegisterFilterProvider();

            builder.RegisterType<AppDbContext>().InstancePerRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerRequest();

            var jobBoardAssemblies = BuildManager.GetReferencedAssemblies()
                .Cast<Assembly>()
                .Where(a => !a.IsDynamic && a.GetName().Name.StartsWith("JobBoard.", System.StringComparison.OrdinalIgnoreCase))
                .ToArray();

            builder.RegisterAssemblyTypes(jobBoardAssemblies)
                   .Where(t => t.Name.EndsWith("Service"))
                   .AsImplementedInterfaces()
                   .InstancePerRequest()
                   .PropertiesAutowired();

            builder.Register(ctx => new MapperConfiguration(cfg => cfg.AddMaps(jobBoardAssemblies)))
                   .AsSelf()
                   .SingleInstance();

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper())
                   .As<IMapper>()
                   .InstancePerRequest();

            return builder.Build();
        }
    }
}