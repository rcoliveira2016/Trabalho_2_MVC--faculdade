
using System;

namespace Trabalho_2_MVC.Dominio.Infra.Ioc
{
    public static class InMemory
    {
        public static Func<IServiceProvider> ContainerAccessor { get; set; }
        private static IServiceProvider Container => ContainerAccessor?.Invoke();

        public static TService GetService<TService>()
        {
            return (TService)Container.GetService(typeof(TService));
        }
    }
}