using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CatalogoWEB
{
    public partial class Detalle : System.Web.UI.Page
    {
        public Articulo ArticuloSeleccionado { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ArticuloSeleccionado = (Articulo)Session["ArticuloSeleccionado"];
            if ((string)Session["Origen"] == "Favoritos.aspx")
            {
                btnDetalleVolver.Text = "← Volver a favoritos";
            }
            else
            {
                btnDetalleVolver.Text = "← Volver al catálogo";
            }
        }

        protected void btnDetalleVolver_Click(object sender, EventArgs e)
        {
            if ((string)Session["Origen"] == "Favoritos.aspx")
            {
                Response.Redirect("Favoritos.aspx");
            }
            else
            {
                Response.Redirect("Detalle.aspx");
            }
        }
    }
}
