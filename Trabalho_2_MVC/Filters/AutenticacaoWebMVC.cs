using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Trabalho_2_MVC.Dominio.Infra.Ioc;
using Trabalho_2_MVC.Dominio.Interfaces.GerenciadorAcessos;

namespace Trabalho_2_MVC.Filters
{
    public class AutenticacaoWebMVC : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var gerenciadorAcesso = InMemory.GetService<IGerenciadorAcesso>();

            if (
                !filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), false) &&
                !gerenciadorAcesso.UsuarioLogado.EstaLogado)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "Login"
                }));
            }
            filterContext.Controller.ViewBag.NomeUsuario = gerenciadorAcesso.UsuarioLogado.Nome;
            filterContext.Controller.ViewBag.IdUsuario = gerenciadorAcesso.UsuarioLogado.Id;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
        }
    }
}