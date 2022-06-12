using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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

            return !mensagens.Any();
        }
    }
}