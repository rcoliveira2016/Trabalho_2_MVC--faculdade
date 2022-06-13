﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabalho_2_MVC.Dominio.Infra.GerenciadorAcessos.Models
{
    public class UsuarioModel
    {
        public long Id { get; set; }

        public string Nome { get; set; }

        public string Login { get; set; }

        public bool EstaLogado { get; set; }
    }
}