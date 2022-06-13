using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Reflection;
using System.Web.Mvc;
using Trabalho_2_MVC.Dominio.Context;
using Trabalho_2_MVC.Dominio.Data;
using Trabalho_2_MVC.Dominio.Infra.GerenciadorAcessos;
using Trabalho_2_MVC.Dominio.Interfaces.Context;
using Trabalho_2_MVC.Dominio.Interfaces.Data;
using Trabalho_2_MVC.Dominio.Interfaces.GerenciadorAcessos;

namespace Trabalho_2_MVC.Dominio.Infra.Ioc
{
    public static class RegisterService
    {
        private static readonly Container Container = new Container();
        public static void Registar()
        {
            Container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            Container.Register<IDbContext, BancoDadosContexto>(Lifestyle.Scoped);
            Container.Register<IClientesRepositorio, ClienteRepositorio>(Lifestyle.Scoped);
            Container.Register<IServicosRepositorio, ServicoRepositorio>(Lifestyle.Scoped);
            Container.Register<IUsuariosRepositorio, UsuarioRepositorio>(Lifestyle.Scoped);
            Container.Register<IOrdensServicosRepositorio, OrdemServicoRepositorio>(Lifestyle.Scoped);
            Container.Register<IGerenciadorAcesso, GerenciadorAcesso>(Lifestyle.Scoped);
            Container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            InMemory.ContainerAccessor = () => Container;

            Container.Verify();

            DependencyResolver.SetResolver(
                new SimpleInjectorDependencyResolver(Container));
        }
    }
}