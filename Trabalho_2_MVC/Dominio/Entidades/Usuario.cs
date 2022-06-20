using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Trabalho_2_MVC.Dominio.Infra.Ioc;
using Trabalho_2_MVC.Dominio.Interfaces.Data;
using Trabalho_2_MVC.Dominio.Interfaces.GerenciadorAcessos;

namespace Trabalho_2_MVC.Dominio.Entidades
{
    public class Usuario:Entidade
    {
        [Display(Name = "Nome completo")]
        [Required()]
        public string NomeCompleto { get; set; }
        [Required()]
        public string Login { get; set; }
        [Required()]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        public virtual ICollection<OrdemServico> OrdensServicos { get; set; }

        public override string DescricaoCombo => NomeCompleto;

        public override bool ValidarDados(out List<string> mensagens)
        {
            if (!base.ValidarDados(out mensagens))
            {
                return false;
            }

            ValidarCampo(mensagens, NomeCompleto, "Nome completo");

            ValidarCampo(mensagens, Login, "Login");

            ValidarCampo(mensagens, Senha, "Senha");

            LoginJaExiste(mensagens);

            return !mensagens.Any();
        }

        private void LoginJaExiste(List<string> mensagens)
        {
            var repositorio = InMemory.GetService<IUsuariosRepositorio>();

            if (repositorio.ExisteLogin(Login))
                mensagens.Add("Já existe usuário com esse login");
        }

        public override bool ValidarExclusao(out string mensagemErro)
        {
            var servivoRepository = InMemory.GetService<IOrdensServicosRepositorio>();

            if (servivoRepository.PossuiUsuario(Id))
            {
                mensagemErro = "Existe Ordens de serviço com esse Usuario";
                return false;
            }
            var gerenciadorAcesso = InMemory.GetService<IGerenciadorAcesso>();

            if (gerenciadorAcesso.UsuarioLogado.Id == Id)
            {
                mensagemErro = "Não é possivel excluir um usuario logado";
                return false;
            }

            mensagemErro = null;
            return true;
        }
    }
}