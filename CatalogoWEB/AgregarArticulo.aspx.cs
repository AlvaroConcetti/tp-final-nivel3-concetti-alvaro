using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.WebSockets;
using Consultas;
using Dominio;
using static System.Net.WebRequestMethods;

namespace CatalogoWEB
{
    public partial class AgregarArticulo : System.Web.UI.Page
    {
        ConsultasArticulo consultasArticulo = new ConsultasArticulo();
        ConsultasCatalogo consultasCatalogo = new ConsultasCatalogo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Seguridad.Admin(Session["Usuario"]) == false)
            {
                Session.Add("Error", "No tienes permisos para ingresar aqui.");
                Response.Redirect("Error.aspx", false);
            }

            txtBoxId.Enabled = false;
            if (!IsPostBack)
            {
                try
                {
                    ddlMarca.DataSource = consultasCatalogo.ListarMarcas();
                    ddlMarca.DataValueField = "Id";
                    ddlMarca.DataTextField = "Descripcion";
                    ddlMarca.DataBind();

                    ddlCategoria.DataSource = consultasCatalogo.ListarCategorias();
                    ddlCategoria.DataValueField = "Id";
                    ddlCategoria.DataTextField = "Descripcion";
                    ddlCategoria.DataBind();
                }
                catch (Exception ex)
                {
                    Session.Add("Error", ex);
                    Response.Redirect("Error.aspx", false);
                }

                if (Request.QueryString["Id"] != null)
                {
                    btnAceptar.Text = "Editar";
                    btnEliminar.Visible = true;

                    int idArticulo = int.Parse(Request.QueryString["id"]);
                    List<Articulo> articulos = consultasCatalogo.ListarArticulo();
                    Articulo articuloSeleccionado = articulos.Find(x => x.Id == idArticulo);

                    txtBoxId.Text = articuloSeleccionado.Id.ToString();
                    txtBoxPrecio.Text = articuloSeleccionado.Precio.ToString();
                    txtBoxNombre.Text = articuloSeleccionado.Nombre;
                    txtBoxDescripcion.Text = articuloSeleccionado.Descripcion;
                    ddlMarca.SelectedValue = articuloSeleccionado.Marca.Id.ToString();
                    ddlCategoria.SelectedValue = articuloSeleccionado.Categoria.Id.ToString();
                    txtBoxCodigo.Text = articuloSeleccionado.Codigo;
                    if (!string.IsNullOrWhiteSpace(articuloSeleccionado.UrlImagen))
                    {
                        imgArticulo.ImageUrl = articuloSeleccionado.UrlImagen;
                    }
                    else
                    {
                        imgArticulo.ImageUrl = "https://developers.elementor.com/docs/assets/img/elementor-placeholder-image.png";
                    }
                }

            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                    Page.Validate();
                    if (!Page.IsValid)
                        return;

                Articulo articuloNuevo = new Articulo();

                try
                {
                    articuloNuevo.Precio = decimal.Parse(txtBoxPrecio.Text);
                }
                catch (Exception)
                {
                    Session.Add("Error", "El precio tiene un formato inválido.");
                    Response.Redirect("Error.aspx", false);
                    return;
                }

                articuloNuevo.Nombre = txtBoxNombre.Text;
                articuloNuevo.Descripcion = txtBoxDescripcion.Text;
                articuloNuevo.Marca = new Marca();
                articuloNuevo.Marca.Id = int.Parse(ddlMarca.SelectedValue);
                articuloNuevo.Categoria = new Categoria();
                articuloNuevo.Categoria.Id = int.Parse(ddlCategoria.SelectedValue);
                articuloNuevo.Codigo = txtBoxCodigo.Text;

                if (txtImagen.PostedFile != null && txtImagen.PostedFile.ContentLength > 0)
                {
                    string ruta = Server.MapPath("./Images/");
                    string nombreArchivo = "articulo-" + txtBoxCodigo.Text + ".jpg";
                    txtImagen.PostedFile.SaveAs(ruta + nombreArchivo);
                    articuloNuevo.UrlImagen = "Images/" + nombreArchivo;
                }
                else
                {
                    articuloNuevo.UrlImagen = string.Empty;
                }

                if (Request.QueryString["Id"] != null)
                {
                    articuloNuevo.Id = int.Parse(Request.QueryString["Id"]);
                    consultasArticulo.ModificarArticulo(articuloNuevo);
                }
                else
                {
                    consultasArticulo.AgregarArticulo(articuloNuevo);
                }

                Response.Redirect("Administracion.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
                Response.Redirect("Error.aspx", false);
            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Administracion.aspx", false);
        }

        //protected void btnEliminar_Click(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        int idArticulo = int.Parse(Request.QueryString["id"]);
        //        consultasArticulo.EliminarArticulo(idArticulo);
        //        Response.Redirect("Administracion.aspx", false);
        //    }
        //    catch (Exception ex)
        //    {
        //        Session.Add("Error", ex);
        //        Response.Redirect("Error.aspx", false);
        //    }
        //}

        // Lo hice con claude code para poder borrar las imagenes de la carpeta Images.
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int idArticulo = int.Parse(Request.QueryString["id"]);
                List<Articulo> lista = consultasCatalogo.ListarArticulo();
                Articulo articuloABorrar = lista.Find(x => x.Id == idArticulo);

                if (articuloABorrar != null && !string.IsNullOrEmpty(articuloABorrar.UrlImagen))
                {
                    string rutaFisica = Server.MapPath("~/" + articuloABorrar.UrlImagen);

                    if (System.IO.File.Exists(rutaFisica))
                    {
                        System.IO.File.Delete(rutaFisica);
                    }
                }

                consultasArticulo.EliminarArticulo(idArticulo);
                Response.Redirect("Administracion.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}