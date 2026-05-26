using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CatalogoWEB
{
    public static class Validacion
    {
        public static bool ValidarVacio(object control)
        {
            if (control is TextBox txtBox)
            {
                if (string.IsNullOrWhiteSpace(txtBox.Text))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public static bool ValidarMaxLength(string texto, int maximo)
        {
            if (string.IsNullOrEmpty(texto)) return false;
            return texto.Length > maximo;
        }
    }
}