using System.Collections.Generic;
using System.Web.Mvc;

namespace Trabalho_2_MVC.Controllers.Common
{
    public abstract class CommonController : Controller
    {
        private List<string> MensagensErros { 
            get
            {
                if (ViewBag.MensagensErros == null)
                    ViewBag.MensagensErros = new List<string>();

                return ViewBag.MensagensErros;
            }
            set { ViewBag.MensagensErros = value; }
        }

        protected List<string> MensagensSucessos
        {
            get {
                if (ViewBag.MensagensSucessos == null)
                    ViewBag.MensagensSucessos = new List<string>();

                return ViewBag.MensagensSucessos; 
            }
            set { ViewBag.MensagensSucessos = value; }
        }

        protected void AdicionarErro(string texto)
        {
            MensagensErros.Add(texto);
        }

        protected void AdicionarSucesso(string texto)
        {
            MensagensSucessos.Add(texto);
        }
    }
}