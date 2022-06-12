using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Trabalho_2_MVC.Dominio.Entidades;

namespace Trabalho_2_MVC.ViewModels
{
    public class OrdemServicoViewModel
    {
        public bool RegistroNovo => Id < 1;
        public long Id { get; set; }
        
        [Display(Name = "Cliente")]
        [Required()]
        public long IdCliente { get; set; }
        [Required()]
        [Display(Name = "Usuário")]
        public long IdUsuario { get; set; }
        [Required()]
        [Display(Name = "Serviço")]
        public long IdServico { get; set; }
        [Required()]
        public int Unitario { get; set; }        
        [Required()]
        [Display(Name = "Tipo de Pagamento")]
        public eTipoFormaPagamento TipoPagamento { get; set; }
        [Display(Name = "Código Pix")]
        public string CodigoPix { get; set; }
        [Display(Name = "Código Barra")]
        public string CodigoBarra { get; set; }
        [Display(Name = "Número Cartão")]
        public string NumeroCartão { get; set; }
        [Display(Name = "Código Segurança")]
        public string CodigoSegurança { get; set; }
    }
}