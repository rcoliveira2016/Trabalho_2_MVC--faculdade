using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Trabalho_2_MVC.Dominio.Data;
using Trabalho_2_MVC.Dominio.Interfaces.Data;

namespace Trabalho_2_MVC.Dominio.Infra.Ioc
{
    public static class RegisterService
    {
        private static readonly Container Container = new Container();
        public static void Registar()
        {
            Container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            Container.Register<IClientesRepositorio, ClienteRepositorio>(Lifestyle.Scoped);
            Container.Register<IServicosRepositorio, ServicoRepositorio>(Lifestyle.Scoped);
            Container.Register<IUsuariosRepositorio, UsuarioRepositorio>(Lifestyle.Scoped);
            Container.Register<IOrdensServicosRepositorio, OrdemServicoRepositorio>(Lifestyle.Scoped);
            Container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            InMemory.ContainerAccessor = ()=> Container;

            Container.Verify();

            DependencyResolver.SetResolver(
                new SimpleInjectorDependencyResolver(Container));
        }
    }
}