using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trabalho_2_MVC.Dominio.Entidades;
using Trabalho_2_MVC.Dominio.Infra.Ioc;
using Trabalho_2_MVC.Dominio.Interfaces.Data;
using Trabalho_2_MVC.ViewModels;

namespace Trabalho_2_MVC
{
    public static class _AplicacaoWeb
    {

        const string nomeUsuario = "***";
        const string chaveUsuarioLogadoSessao = "UsuarioLogado";
        const string chaveValorLoginUsuario = "LoginUsuario";

        public static UsuarioViewModel UsuarioLogado { get => ObterUsuarioLogado(); }

        private static UsuarioViewModel ObterUsuarioLogado()
        {
            var usuario = HttpContext.Current.Session[chaveUsuarioLogadoSessao] as UsuarioViewModel;

            if (usuario != null)
            {
                usuario.EstaLogado = true;
                return usuario;
            }

            var cookie = HttpContext.Current.Response.Cookies.Get(chaveUsuarioLogadoSessao);

            if (cookie != null && cookie.HasKeys)
            {
                usuario = BuscarUsuarioLogin(cookie.Value);
                if (usuario != null)
                {
                    HttpContext.Current.Session[chaveUsuarioLogadoSessao] = usuario;
                    return usuario;
                };
            }

            return new UsuarioViewModel { EstaLogado = false };
        }


        public static void LogarUsuario(Usuario usuario)
        {
            var cookie = new HttpCookie(chaveUsuarioLogadoSessao)
            {
                Expires = DateTime.Now.AddDays(3)
            };

            cookie.Value = usuario.Login;

            HttpContext.Current.Response.Cookies.Add(cookie);

            HttpContext.Current.Session.Add(chaveUsuarioLogadoSessao, new UsuarioViewModel
            {
                Login = usuario.Login,
                EstaLogado = true,
                Id = usuario.Id,
                Nome = usuario.NomeCompleto
            });
        }

        public static void LogOffUsuario()
        {
            HttpContext.Current.Session.Remove(chaveUsuarioLogadoSessao);
            HttpContext.Current.Response.Cookies.Remove(chaveUsuarioLogadoSessao);
        }


        private static UsuarioViewModel BuscarUsuarioLogin(string login)
        {
            var usuarioRepositorio = InMemory.GetService<IUsuariosRepositorio>();

            var usuario =  usuarioRepositorio.BuscarPorLogin(nomeUsuario);
            return new UsuarioViewModel
            {
                Login = login,
                EstaLogado = true,
                Id = usuario.Id,
                Nome = usuario.NomeCompleto
            };
        }
    }
}