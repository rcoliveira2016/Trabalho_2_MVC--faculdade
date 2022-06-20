using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Trabalho_2_MVC.Dominio.Infra.Ioc;
using Trabalho_2_MVC.Dominio.Interfaces.Data;

namespace Trabalho_2_MVC.Dominio.Entidades
{
    public class Cliente: Entidade
    {
        [Required()]
        public string Nome { get; set; }
        [Required()]
        public string CPF { get; set; }
        [Display(Name = "Data de nascimento")]
        [Required()]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",ApplyFormatInEditMode =true)]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
        [Display(Name = "Endereço")]
        [Required()]
        public string Endereco { get; set; }
        [Required()]
        public string Telefone { get; set; }


        public virtual ICollection<OrdemServico> OrdensServicos { get; set; }

        public override string DescricaoCombo => Nome;

        public override bool ValidarDados(out List<string> mensagens)
        {
            if (!base.ValidarDados(out mensagens))
            {
                return false;
            }

            ValidarCampo(mensagens, CPF, "CPF");

            ValidarCampo(mensagens, Nome, "Nome");

            ValidarCampo(mensagens, Endereco, "Endereço");

            ValidarCampo(mensagens, Telefone, "Telefone");

            ValidarCampo(mensagens, DataNascimento, "Data de nascimaneto");

            return !mensagens.Any();
        }

        public override bool ValidarExclusao(out string mensagemErro)
        {
            var servivoRepository = InMemory.GetService<IOrdensServicosRepositorio>();

            if (servivoRepository.PossuiCliente(Id))
            {
                mensagemErro = "Existe Ordens de serviço com esse cliente";
                return false;
            }

            mensagemErro = null;
            return true;
        }
    }
}