using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Consultas;
using Dominio;

namespace CatalogoWEB
{
    public partial class Administracion : System.Web.UI.Page
    {
        ConsultasCatalogo consultasCatalogo = new ConsultasCatalogo();
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Seguridad.Admin(Session["Usuario"]) == false)
            //{
            //    Session.Add("Error", "No tienes permisos para ingresar aqui.");
            //    Response.Redirect("Error.aspx", false);
            //}

            FiltroAvanzado = chBoxFiltroAvanzado.Checked;
            ddlCriterio.Enabled = true;

            if (!IsPostBack)
            {
                dgvArticulos.DataSource = consultasCatalogo.ListarArticulo();
                dgvArticulos.DataBind();

                ddlFiltrar_SelectedIndexChanged(null, EventArgs.Empty);
            }
        }

        protected void dgvArticulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvArticulos.PageIndex = e.NewPageIndex;
                dgvArticulos.DataSource = consultasCatalogo.ListarArticulo();
                dgvArticulos.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
                Response.Redirect("Error.aspx", false);
            }
        }

        public bool FiltroAvanzado { get; set; }
        protected void chBoxFiltroAvanzado_CheckedChanged(object sender, EventArgs e)
        {
            FiltroAvanzado = chBoxFiltroAvanzado.Checked;
        }

        protected void btnAgregarArticulo_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarArticulo.aspx", false);
        }
        protected void dgvArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string id = dgvArticulos.SelectedRow.Cells[0].Text;
                Response.Redirect("AgregarArticulo.aspx?Id=" + id, false);
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnBorrarFiltro_Click(object sender, EventArgs e)
        {
            dgvArticulos.DataSource = Session["ListaEnSession"];
            dgvArticulos.DataBind();
            txtBoxFiltrar.Text = "";
        }

        protected void btnLimpiarFiltroAvanzado_Click(object sender, EventArgs e)
        {
            dgvArticulos.DataSource = Session["ListaEnSession"];
            dgvArticulos.DataBind();
            ddlFiltrar.ClearSelection();
            ddlCriterio.ClearSelection();
            ddlMarca.ClearSelection();
            ddlCategoria.ClearSelection();
            txtBoxFiltrarAvanzado.Text = "";
        }

        protected void txtBoxFiltrar_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> articulos = (List<Articulo>)Session["ListaEnSession"];
            List<Articulo> listaFiltrada = articulos.FindAll(x => x.Nombre.ToUpper().Contains(txtBoxFiltrar.Text.ToUpper()));
            dgvArticulos.DataSource = listaFiltrada;
            dgvArticulos.DataBind();
        }

        protected void ddlFiltrar_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCriterio.Items.Clear();
            ddlMarca.Items.Clear();
            ddlCategoria.Items.Clear();
            if (ddlFiltrar.SelectedItem.ToString() == "Código")
            {
                ddlCriterio.Items.Add("=");
            }
            else if (ddlFiltrar.SelectedItem.ToString() == "Precio")
            {
                ddlCriterio.Items.Add(">");
                ddlCriterio.Items.Add("=");
                ddlCriterio.Items.Add("<");
            }
            else if (ddlFiltrar.SelectedItem.ToString() == "Nombre")
            {
                ddlCriterio.Items.Add("Contiene");
            }
            if (ddlFiltrar.SelectedItem.ToString() == "Marca")
            {
                List<Marca> listaMarcas = consultasCatalogo.ListarMarcas();
                foreach (Marca item in listaMarcas)
                {
                    ListItem opcion = new ListItem(item.Descripcion, item.Id.ToString());
                    ddlMarca.Items.Add(opcion);
                }

                ddlCriterio.Enabled = false;
                ddlMarca.Visible = true;
                ddlCategoria.Visible = false;
                txtBoxFiltrarAvanzado.Visible = false;

                lblDdlMarca.Visible = true;
                lblDdlCategoria.Visible = false;
                lblTxtBoxFiltrarAvanzado.Visible = false;
            }
            else if (ddlFiltrar.SelectedItem.ToString() == "Categoría")
            {
                List<Categoria> listaCategorias = consultasCatalogo.ListarCategorias();
                foreach (Categoria item in listaCategorias)
                {
                    ListItem opcion = new ListItem(item.Descripcion, item.Id.ToString());
                    ddlCategoria.Items.Add(opcion);
                }

                ddlCriterio.Enabled = false;
                ddlCategoria.Visible = true;
                ddlMarca.Visible = false;
                txtBoxFiltrarAvanzado.Visible = false;

                lblDdlCategoria.Visible = true;
                lblDdlMarca.Visible = false;
                lblTxtBoxFiltrarAvanzado.Visible = false;
            }
            else
            {
                ddlMarca.Visible = false;
                ddlCategoria.Visible = false;
                txtBoxFiltrarAvanzado.Visible = true;

                lblTxtBoxFiltrarAvanzado.Visible = true;
                lblDdlMarca.Visible = false;
                lblDdlCategoria.Visible = false;
            }
        }
        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                string campo = ddlFiltrar.SelectedItem.ToString();
                string criterio = ddlCriterio.SelectedItem != null ? ddlCriterio.SelectedItem.ToString() : "";
                string filtro = "";

                if (campo == "Marca")
                {
                    filtro = ddlMarca.SelectedValue;
                }
                else if (campo == "Categoría")
                {
                    filtro = ddlCategoria.SelectedValue;
                }
                else
                {
                    filtro = txtBoxFiltrarAvanzado.Text;
                }

                if (string.IsNullOrEmpty(filtro))
                {
                    return;
                }

                dgvArticulos.DataSource = consultasCatalogo.FiltrarAvanzado(campo, criterio, filtro);
                dgvArticulos.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
                Response.Redirect("Error.aspx", false);
            }
        }


    }
}