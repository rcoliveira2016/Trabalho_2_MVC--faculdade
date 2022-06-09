using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
    }
}