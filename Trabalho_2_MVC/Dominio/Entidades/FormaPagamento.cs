using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabalho_2_MVC.Dominio.Entidades
{
    public class FormaPagamento: Entidade
    {
        public eTipoFormaPagamento Tipo { get; set; }
        public string CodigoPix { get; set; }
        public string CodigoBarra { get; set; }
        public string NumeroCartão { get; set; }
        public string CodigoSegurança { get; set; }
        public virtual Pagamento Pagamento { get; set; }

        public override bool ValidarDados(out List<string> mensagens)
        {
            if (!base.ValidarDados(out mensagens))
            {
                return false;
            }


            switch (Tipo)
            {
                case eTipoFormaPagamento.Cartao:
                    ValidarCampo(mensagens, NumeroCartão, "Numero cartão");
                    ValidarCampo(mensagens, CodigoSegurança, "Codigo segurança");
                    break;
                case eTipoFormaPagamento.Boleto:
                    ValidarCampo(mensagens, CodigoBarra, "Codigo barra");
                    break;
                case eTipoFormaPagamento.Pix:
                    ValidarCampo(mensagens, CodigoPix, "Codigo Pix");
                    break;
                default:
                    mensagens.Add(string.Format(MensagemCampoVazio, "Tipo Pagamento"));
                    break;
            }


            return !mensagens.Any();
        }
    }

    public enum eTipoFormaPagamento
    {
        Cartao=1,
        Boleto=2,
        Pix=3,
    }
}