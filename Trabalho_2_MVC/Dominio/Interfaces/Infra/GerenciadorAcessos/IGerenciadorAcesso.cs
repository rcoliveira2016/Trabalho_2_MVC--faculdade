using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabalho_2_MVC.Dominio.Entidades;
using Trabalho_2_MVC.Dominio.Infra.GerenciadorAcessos.Models;

namespace Trabalho_2_MVC.Dominio.Interfaces.GerenciadorAcessos
{
    public interface IGerenciadorAcesso
    {
        UsuarioModel UsuarioLogado { get; }
        void LogarUsuario(Usuario usuario);
        void LogOffUsuario();
    }
}
