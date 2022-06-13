using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabalho_2_MVC.ViewModels
{
    public class LoginViewModel
    {
        public string Senha { get; set; }

        public string Login { get; set; }
    }

    public class CadastroUsuarioViewModel
    {
        public string NomeComplemento { get; set; }
        public string Senha { get; set; }

        public string Login { get; set; }

    }
}