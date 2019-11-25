using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Libreria.Seguridad
{
  public class UsuarioMembership:MembershipUser
  {
    public int IdSujeto { get; set; }
    public string Usuario { get; set; }
    public string Login { get; set; }
    public short IdSucursal { get; set; }
    public short IdRol { get; set; }

    public UsuarioMembership(SujetoUsuario us) //Usuario es tabla del modelo (LibreriaDB)
    {
      IdSujeto = us.IdSujeto;
      Usuario = us.Usuario;
      Login = us.Login;
      IdSucursal = us.IdSucursal;
      IdRol = us.IdRol;
    }
  }

  public class SujetoUsuario
  {
    public int IdSujeto { get; set; }
    public string Usuario { get; set; }
    public string Login { get; set; }
    public short IdSucursal { get; set; }
    public short IdRol { get; set; }
  }


}