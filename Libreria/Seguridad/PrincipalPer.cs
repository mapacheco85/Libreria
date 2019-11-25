using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Libreria.Seguridad
{
  public class PrincipalPer:IPrincipal
  {
    public bool IsInRol(string Role)
    {
      throw new NotImplementedException();
    }

    bool IPrincipal.IsInRole(string role)
    {
      throw new NotImplementedException();
    }

    public IIdentity Identity { get;private set; }

    public IdentityPer MiIdentidadPersonalizada
    {
      get { return (IdentityPer)Identity; }
      set { Identity = value; }
    }

    public PrincipalPer(IdentityPer identity)
    {
      Identity = identity;
    }
  }
}