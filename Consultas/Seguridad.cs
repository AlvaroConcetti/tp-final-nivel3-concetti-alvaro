using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Consultas
{
    public static class Seguridad
    {
        public static bool SesionActiva(Object user)
        {
            try
            {
                Usuario usuario = user != null ? (Usuario)user : null;
                if (usuario != null && usuario.Id != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool Admin(Object user)
        {
            try
            {
                Usuario usuario = user != null ? (Usuario)user : null;
                return usuario != null ? usuario.Admin : false; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
