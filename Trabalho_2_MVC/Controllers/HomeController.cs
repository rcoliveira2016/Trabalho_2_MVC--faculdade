using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trabalho_2_MVC.Dominio.Entidades;
using Trabalho_2_MVC.Dominio.Interfaces.Data;
using Trabalho_2_MVC.Dominio.Interfaces.GerenciadorAcessos;
using Trabalho_2_MVC.ViewModels;

namespace Trabalho_2_MVC.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IUsuariosRepositorio usuariosRepositorio;
        private readonly IGerenciadorAcesso gerenciadorAcesso;

        public HomeController(IUsuariosRepositorio usuariosRepositorio, IGerenciadorAcesso gerenciadorAcesso)
        {
            this.usuariosRepositorio = usuariosRepositorio;
            this.gerenciadorAcesso = gerenciadorAcesso;
        }
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LoginUsuario(LoginViewModel input)
        {
            var usuario = usuariosRepositorio.BuscarPorLogin(input.Login);
            if (usuario != null && usuario.Senha.Equals(input.Senha) && usuario.Login.Equals(input.Login))
            {
                gerenciadorAcesso.LogarUsuario(usuario);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Erros = "Usuario ou senha Inválido!";
                return View("Login");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult CadastrarUsuario(Usuario usuario)
        {
            ValidarStateModel(usuario);
            if (ModelState.IsValid)
            {
                usuariosRepositorio.Adiciona(usuario);

                return LoginUsuario(new LoginViewModel { Login = usuario.Login, Senha = usuario.Senha});
            }

            //ViewBag.Erros = $"Erro no cadastro: {ModelState.MensagensErrosStateModel()}";
            return View("Login");
        }

        [HttpGet]
        public ActionResult LogOff()
        {

            gerenciadorAcesso.LogOffUsuario();
            return RedirectToAction("Login");
        }
    }
}