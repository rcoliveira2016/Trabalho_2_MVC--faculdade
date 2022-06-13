using System;
using System.Web;
using Trabalho_2_MVC.Dominio.Entidades;
using Trabalho_2_MVC.Dominio.Infra.GerenciadorAcessos.Models;
using Trabalho_2_MVC.Dominio.Interfaces.Data;
using Trabalho_2_MVC.Dominio.Interfaces.GerenciadorAcessos;

namespace Trabalho_2_MVC.Dominio.Infra.GerenciadorAcessos
{
    public class GerenciadorAcesso : IGerenciadorAcesso
    {
        private readonly IUsuariosRepositorio usuariosRepositorio;
        public GerenciadorAcesso(IUsuariosRepositorio usuariosRepositorio)
        {
            this.usuariosRepositorio = usuariosRepositorio;
        }

        const string nomeUsuario = "***";
        const string chaveUsuarioLogadoSessao = "UsuarioLogado";
        const string chaveValorLoginUsuario = "LoginUsuario";

        public UsuarioModel UsuarioLogado { get => ObterUsuarioLogado(); }

        private UsuarioModel ObterUsuarioLogado()
        {
            var usuario = HttpContext.Current.Session[chaveUsuarioLogadoSessao] as UsuarioModel;

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

            return new UsuarioModel { EstaLogado = false };
        }


        public void LogarUsuario(Usuario usuario)
        {
            var cookie = new HttpCookie(chaveUsuarioLogadoSessao)
            {
                Expires = DateTime.Now.AddDays(3)
            };

            cookie.Value = usuario.Login;

            HttpContext.Current.Response.Cookies.Add(cookie);

            HttpContext.Current.Session.Add(chaveUsuarioLogadoSessao, new UsuarioModel
            {
                Login = usuario.Login,
                EstaLogado = true,
                Id = usuario.Id,
                Nome = usuario.NomeCompleto
            });
        }

        public void LogOffUsuario()
        {
            HttpContext.Current.Session.Remove(chaveUsuarioLogadoSessao);
            HttpContext.Current.Response.Cookies.Remove(chaveUsuarioLogadoSessao);
        }


        private UsuarioModel BuscarUsuarioLogin(string login)
        {

            var usuario = usuariosRepositorio.BuscarPorLogin(nomeUsuario);
            return new UsuarioModel
            {
                Login = login,
                EstaLogado = true,
                Id = usuario.Id,
                Nome = usuario.NomeCompleto
            };
        }
    }
}