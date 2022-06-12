using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using Trabalho_2_MVC.Dominio.Data;
using Trabalho_2_MVC.Dominio.Entidades;

namespace Trabalho_2_MVC.Dominio.Infra.Extensoes
{
    public static class ExtensoesPadrao
    {


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