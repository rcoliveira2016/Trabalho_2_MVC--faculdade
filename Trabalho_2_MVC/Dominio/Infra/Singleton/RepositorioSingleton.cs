using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trabalho_2_MVC.Dominio.Data;
using Trabalho_2_MVC.Dominio.Entidades;

namespace Trabalho_2_MVC.Dominio.Infra
{
    public static class RepositorioSingleton
    {
        public static readonly ClienteRepositorio ClienteRepositorio = new ClienteRepositorio();
        public static readonly FormaPagamentoRepositorio FormaPagamentoRepositorio = new FormaPagamentoRepositorio();
        public static readonly OrdemServicoRepositorio OrdemServicoRepositorio = new OrdemServicoRepositorio();
        public static readonly PagamentoRepositorio PagamentoRepositorio = new PagamentoRepositorio();
        public static readonly ServicoRepositorio ServicoRepositorio = new ServicoRepositorio();
        public static readonly UsuarioRepositorio UsuarioRepositorio = new UsuarioRepositorio();
    }
}