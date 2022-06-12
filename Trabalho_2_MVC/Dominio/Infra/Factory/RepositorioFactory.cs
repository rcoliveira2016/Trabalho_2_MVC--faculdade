using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trabalho_2_MVC.Dominio.Data;
using Trabalho_2_MVC.Dominio.Interfaces.Data;

namespace Trabalho_2_MVC.Dominio.Infra.Factory
{
    public static class RepositorioFactory
    {
        public static IClientesRepositorio CriarClientes() => new ClienteRepositorio();
        public static IServicosRepositorio CriarServicos() => new ServicoRepositorio();
        public static IUsuariosRepositorio CriarUsuarios() => new UsuarioRepositorio();
        public static IOrdensServicosRepositorio CriarOrdensServicos() => new OrdemServicoRepositorio();
    }
}