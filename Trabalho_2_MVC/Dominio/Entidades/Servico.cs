using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Trabalho_2_MVC.Dominio.Entidades
{
    public class Servico:Entidade
    {
        [Required()]
        public string Nome { get; set; }
        [Display(Name = "Descrição")]
        [Required()]
        public string Descricao { get; set; }
        [Display(Name = "Valor unitário")]
        [Required()]
        public double ValorUnitario { get; set; }
        public virtual ICollection<OrdemServico> OrdensServicos { get; set; }


        public override string DescricaoCombo => Nome;

        public override bool ValidarDados(out List<string> mensagens)
        {
            if (!base.ValidarDados(out mensagens))
            {
                return false;
            }

            ValidarCampo(mensagens, Nome, "Nome");

            ValidarCampo(mensagens, Descricao, "Descrição");

            ValidarCampo(mensagens, ValorUnitario, "Valor Unitario");

            return !mensagens.Any();
        }
    }
}