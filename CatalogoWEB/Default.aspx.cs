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


        // Esto lo hizo claude code
        private List<int> _idsFavoritos = new List<int>();
        // Esto lo hizo claude code
        protected bool EsFavorito(int articuloId)
        {
            return _idsFavoritos.Contains(articuloId);
        }
        // Esto lo hizo claude code
        private void CargarFavoritos()
        {
            if (Seguridad.SesionActiva(Session["Usuario"]))
            {
                Usuario u = (Usuario)Session["Usuario"];
                _idsFavoritos = catalogo.ListarFavoritos(u.Id)
                                        .Select(a => a.Id)
                                        .ToList();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CargarFavoritos();

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
                Articulo seleccionado = lista.Find(x => x.Id == articuloId);
                Session.Add("ArticuloSeleccionado", seleccionado);
                Session.Add("Origen", "Default.aspx");
                Response.Redirect("Detalle.aspx", false);
            }
            else
            {
                repRepetidor.DataSource = catalogo.ListarArticulo();
                repRepetidor.DataBind();
            }
        }

        protected void repRepetidor_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "AddFavorito")
            {
                if (!Seguridad.SesionActiva(Session["Usuario"]))
                {
                    Session.Add("Error", "Debes estar logueado para añadir un articulo a Favoritos.");
                    Response.Redirect("Error.aspx");
                }

                try
                {
                    Usuario usuarioLogueado = (Usuario)Session["Usuario"];
                    int idUsuario = usuarioLogueado.Id;
                    int idArticuloFavorito = int.Parse(e.CommandArgument.ToString());

                    ConsultasArticulo consultasArticulo = new ConsultasArticulo();

                    // Toggle: si ya es favorito lo saca, si no lo agrega
                    if (_idsFavoritos.Contains(idArticuloFavorito))
                        catalogo.EliminarFavorito(idUsuario, idArticuloFavorito);
                    else
                        consultasArticulo.AgregarArticuloFavorito(idUsuario, idArticuloFavorito);

                    // Recargar favoritos y rebindear para actualizar el corazón
                    CargarFavoritos();
                    List<Articulo> lista = (List<Articulo>)Session["ListaEnSession"];
                    repRepetidor.DataSource = lista ?? catalogo.ListarArticulo();
                    repRepetidor.DataBind();
                }
                catch (Exception ex)
                {
                    Session.Add("Error", ex);
                    Response.Redirect("Error.aspx", false);
                }
            }
        }
    }
}
