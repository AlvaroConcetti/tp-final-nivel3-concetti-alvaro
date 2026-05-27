using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Consultas
{
    public class ConsultasCatalogo
    {
        AccesoDatos datos = new AccesoDatos();
        public List<Articulo> ListarArticulo()
        {
            List<Articulo> lista = new List<Articulo>();

            try
            {
                datos.setearProcedimiento("ListarArticulosSP");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["ImagenUrl"];

                    aux.Precio = (decimal)datos.Lector["Precio"];

                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];

                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    lista.Add(aux);
                }

                return lista;
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

        public List<Articulo> FiltrarArticulo(string busqueda)
        {
            List<Articulo> articulos = new List<Articulo>();

            try
            {
                datos.setearProcedimiento("FiltrarArticulosSP");
                datos.setearParametro("@busqueda", busqueda);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["ImagenUrl"];

                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];

                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    articulos.Add(aux);
                }
                return articulos;
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

        public List<Articulo> FiltrarAvanzado(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.ImagenUrl, A.Precio, M.Id AS IdMarca, M.Descripcion AS Marca, C.Id AS IdCategoria, C.Descripcion AS Categoria FROM ARTICULOS A INNER JOIN MARCAS M ON A.IdMarca = M.Id INNER JOIN CATEGORIAS C ON A.IdCategoria = C.Id WHERE ";

                switch (campo)
                {
                    case "Código":
                        consulta += "A.Codigo = '" + filtro + "'";
                        break;

                    case "Precio":
                        if (criterio == ">")
                            consulta += "A.Precio > " + filtro;
                        else if (criterio == "<")
                            consulta += "A.Precio < " + filtro;
                        else
                            consulta += "A.Precio = " + filtro;
                        break;

                    case "Marca":
                        consulta += "A.IdMarca = " + filtro;
                        break;

                    case "Categoría":
                        consulta += "A.IdCategoria = " + filtro;
                        break;
                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["ImagenUrl"];

                    aux.Precio = (decimal)datos.Lector["Precio"];

                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];

                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    lista.Add(aux);
                }

                return lista;
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
        public List<Marca> ListarMarcas()
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT Id, Descripcion FROM MARCAS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca aux = new Marca();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        public List<Categoria> ListarCategorias()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT Id, Descripcion FROM CATEGORIAS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        public List<Articulo> ListarFavoritos(int idUser)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("ListarFavoritosSP");
                datos.setearParametro("@IdUser", idUser);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["ImagenUrl"];

                    aux.Precio = (decimal)datos.Lector["Precio"];

                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];

                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    lista.Add(aux);
                }

                return lista;
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

        public void EliminarFavorito(int idUser, int idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("EliminarFavoritoSP");

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
