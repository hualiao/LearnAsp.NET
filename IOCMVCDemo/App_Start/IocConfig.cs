using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IOCMVCDemo.Repository;
using Autofac;
using Autofac.Integration.Mvc;

namespace IOCMVCDemo.App_Start
{
    public class IocConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<GoldMedalWinnersRepository>()
                .As<IGoldMedalWinnersRepository>()
                .InstancePerHttpRequest();

            builder.RegisterType<GoldMedalWinnersContext>()
                .As<IGoldMedalWinnersContext>()
                .InstancePerHttpRequest();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }


    }
}