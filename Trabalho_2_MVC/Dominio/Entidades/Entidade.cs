using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabalho_2_MVC.Dominio.Entidades
{
    public abstract class Entidade
    {
        public long Id { get; set; }
        public virtual string DescricaoCombo => string.Empty;
        public bool RegistroNovo => Id <= 0;


        #region Validacoes
        public virtual bool ValidarExclusao(out string mensagemErro)
        {
            mensagemErro = null;
            return true;
        }
        public virtual bool ValidarDados(out List<string> mensagens)
        {
            mensagens = new List<string>();
            return true;
        }
        public void ValidarCampo(List<string> mensagens, string campo, string nomeCampo)
        {
            if (string.IsNullOrEmpty(campo))
                mensagens.Add(string.Format(MensagemCampoVazio, nomeCampo));
        }

        public void ValidarCampo(List<string> mensagens, long campo, string nomeCampo)
        {
            if (campo == 0)
                mensagens.Add(string.Format(MensagemCampoVazio, nomeCampo));
        }

        public void ValidarCampo(List<string> mensagens, double campo, string nomeCampo)
        {
            if (campo == default(double))
                mensagens.Add(string.Format(MensagemCampoVazio, nomeCampo));
        }

        public void ValidarCampo(List<string> mensagens, DateTime campo, string nomeCampo)
        {
            if (campo == default(DateTime))
                mensagens.Add(string.Format(MensagemCampoVazio, nomeCampo));
        }

        public void ValidarCampo<T>(List<string> mensagens, T campo, string nomeCampo) where T : class
        {
            if (campo == null)
                mensagens.Add(string.Format(MensagemCampoVazio, nomeCampo));
        }

        public string MensagemCampoVazio => "Campo '{0}' está vazio"; 
        #endregion
    }
}