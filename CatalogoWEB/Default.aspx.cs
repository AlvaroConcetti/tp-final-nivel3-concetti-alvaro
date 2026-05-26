using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Consultas;
using System.Web.DynamicData;

namespace CatalogoWEB
{
    public partial class Default : System.Web.UI.Page
    {
        ConsultasCatalogo catalogo = new ConsultasCatalogo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Articulo> listaOriginal = catalogo.ListarArticulo();
                Session.Add("ListaEnSession", listaOriginal);
                repRepetidor.DataSource = listaOriginal;
                repRepetidor.DataBind();
            }
        }

        protected void btnBuscarFiltro_Click(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada = catalogo.FiltrarArticulo(txtBoxFiltro.Text);
            repRepetidor.DataSource = listaFiltrada;
            repRepetidor.DataBind();
        }

        protected void btnBorrarFiltro_Click(object sender, EventArgs e)
        {
            txtBoxFiltro.Text = "";
            repRepetidor.DataSource = catalogo.ListarArticulo();
            repRepetidor.DataBind();
        }

        protected void btnDetalle_Click(object sender, EventArgs e)
        {
            int articuloId = int.Parse(((Button)sender).CommandArgument);

            List<Articulo> lista = (List<Articulo>)Session["ListaEnSession"];

            if (lista != null)
            {
                // 4. Buscamos el bicho en la lista que recuperamos
                Articulo seleccionado = lista.Find(x => x.Id == articuloId);

                // 5. Lo guardamos para la siguiente página
                Session.Add("ArticuloSeleccionado", seleccionado);

                // 6. Redigimos a la pantalla de detalle
                Response.Redirect("Detalle.aspx", false);
            }
            else
            {
                repRepetidor.DataSource = catalogo.ListarArticulo();
                repRepetidor.DataBind();
            }
        }

    }
}
