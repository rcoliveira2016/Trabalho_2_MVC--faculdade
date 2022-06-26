using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabalho_2_MVC.Dominio.Entidades
{
    public class Pagamento: Entidade
    {        
        public virtual OrdemServico OrdemServico { get; set; }
        public virtual FormaPagamento FormaPagamento { get; set; }
        public decimal ValorTotal { get; set; }

        public static Pagamento Criar() => new Pagamento() { FormaPagamento = new FormaPagamento() };

        public override bool ValidarDados(out List<string> mensagens)
        {
            if (!base.ValidarDados(out mensagens))
            {
                return false;
            }

            ValidarCampo(mensagens, ValorTotal, "Valor Total");

            FormaPagamento.ValidarDados(out var mensagensFormas);

            mensagens.AddRange(mensagensFormas);
            return !mensagens.Any();
        }
    }
}