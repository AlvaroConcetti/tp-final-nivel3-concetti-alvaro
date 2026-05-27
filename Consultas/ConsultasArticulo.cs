using Consultas;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultas
{
    public class ConsultasArticulo
    {
        public int AgregarArticulo(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("AgregarArticuloSP");

                datos.setearParametro("@codigo", nuevo.Codigo);
                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@descripcion", nuevo.Descripcion);
                datos.setearParametro("@idMarca", nuevo.Marca.Id);
                datos.setearParametro("@idCategoria", nuevo.Categoria.Id);

                if (!string.IsNullOrEmpty(nuevo.UrlImagen))
                    datos.setearParametro("@imagenUrl", nuevo.UrlImagen);
                else
                    datos.setearParametro("@imagenUrl", DBNull.Value);

                datos.setearParametro("@precio", nuevo.Precio);

                return datos.ejecutarAccionScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void EliminarArticulo(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("EliminarArticuloSP");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void ModificarArticulo(Articulo articuloModificado)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("ModificarArticuloSP");
                datos.setearParametro("@id", articuloModificado.Id);
                datos.setearParametro("@codigo", articuloModificado.Codigo);
                datos.setearParametro("@nombre", articuloModificado.Nombre);
                datos.setearParametro("@descripcion", articuloModificado.Descripcion);
                datos.setearParametro("@idMarca", articuloModificado.Marca.Id);
                datos.setearParametro("@idCategoria", articuloModificado.Categoria.Id);

                if (!string.IsNullOrEmpty(articuloModificado.UrlImagen))
                    datos.setearParametro("@imagenUrl", articuloModificado.UrlImagen);
                else
                    datos.setearParametro("@imagenUrl", DBNull.Value);

                datos.setearParametro("@precio", articuloModificado.Precio);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void AgregarArticuloFavorito(int idUser, int idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("AgregarFavoritoSP");

                datos.setearParametro("@IdUser", idUser);
                datos.setearParametro("@IdArticulo", idArticulo);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
