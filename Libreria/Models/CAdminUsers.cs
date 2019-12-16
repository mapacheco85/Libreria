using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Libreria.Models.data;

namespace Libreria.Models
{
    public class CAdminUsers
    {
        public short ObtenerRol(string Name)
        {
            using (var DB = new LibreriaDB())
            {
                var Us = DB.Usuario.Include("RolUsuario").SingleOrDefault(P => P.Login == Name);
                if (Us != null)
                {
                    return (short)Us.RolUsuario.FirstOrDefault(P => P.Activo == true).IdRol;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}