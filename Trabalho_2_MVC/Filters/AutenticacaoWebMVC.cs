using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Trabalho_2_MVC.Filters
{
    public class AutenticacaoWebMVC : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (
                !filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), false) &&
                !AplicacaoWeb.UsuarioLogado.EstaLogado)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "Login"
                }));
            }
            filterContext.Controller.ViewBag.NomeUsuario = AplicacaoWeb.UsuarioLogado.Nome;
            filterContext.Controller.ViewBag.IdUsuario = AplicacaoWeb.UsuarioLogado.Id;
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