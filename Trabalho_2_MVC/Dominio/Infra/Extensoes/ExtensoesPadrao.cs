using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Web.UI.WebControls;
using Trabalho_2_MVC.Dominio.Data;
using Trabalho_2_MVC.Dominio.Entidades;

namespace Trabalho_2_MVC.Dominio.Infra.Extensoes
{
    public static class ExtensoesPadrao
    {

        public static void RedirecionarPaginaCadastro(this GridView grdDados, GridViewEditEventArgs e, HttpResponse Response, string pastaPagina)
        {
            var row = grdDados.Rows[e.NewEditIndex];
            var id = Convert.ToInt64(row.Cells[1].Text);
            Response.Redirect($"~/Paginas/{pastaPagina}/Cadastro?Id={id}", true);
        }
        public static void CarregarDadosGrid<T>(this GridView grdDados, RepositorioBase<T> repositorio) where T : Entidade
        {
            grdDados.DataSource = repositorio.ListaTodos();
            grdDados.DataBind();
        }
        public static void CarregarDadosGrid(this GridView grdDados, object dados)
        {
            grdDados.DataSource = grdDados;
            grdDados.DataBind();
        }
        public static void CarregarItens<T>(this DropDownList combo, IEnumerable<T> list, Func<T, ListItem> map)
        {
            combo.Items.Clear();
            combo.Items.Add(new ListItem("", ""));
            foreach (var item in list)
            {
                combo.Items.Add(map(item));
            }
        }
        public static void CarregarItens<T>(this DropDownList combo, IEnumerable<T> list) where T : Entidade
        {
            combo.CarregarItens(list, x => new ListItem(x.DescricaoCombo, x.Id.ToString()));
        }
        public static bool TentarObterLong(this DropDownList combo, out long valor)
        {
            return long.TryParse(combo.SelectedItem.Value, out valor);
        }
        public static bool TentarObterLong(this TextBox txt, out long valor)
        {
            return long.TryParse(txt.Text, out valor);
        }
        public static bool TentarObterInt(this TextBox txt, out int valor)
        {
            return int.TryParse(txt.Text, out valor);
        }
        public static bool TentarObterDouble(this TextBox txt, out double valor)
        {
            return double.TryParse(txt.Text, out valor);
        }
        public static DateTime ObterDataOuPadrao(this TextBox txt)
        {
            if (DateTime.TryParse(txt.Text, out var valor))
                return valor;
            return default(DateTime);
        }

        public static string GetEnumDescription(this Enum value)
        {
            System.Reflection.FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}