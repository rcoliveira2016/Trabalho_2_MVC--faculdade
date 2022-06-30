using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Trabalho_2_MVC.App_Start;
using Trabalho_2_MVC.Dominio.Entidades;
using Trabalho_2_MVC.Dominio.Infra.Ioc;
using Trabalho_2_MVC.Dominio.Interfaces.Data;

namespace Trabalho_2_MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());

            CriarInjecaoDepedencia();
        }


        private void CriarInjecaoDepedencia()
        {
            RegisterService.Registar();
        }
    }
}
