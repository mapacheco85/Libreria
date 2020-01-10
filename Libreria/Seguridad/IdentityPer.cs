using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace Libreria.Seguridad
{
    public class IdentityPer : IIdentity
    {
        public string Name
        {
            get { return Login; }
        }

        public string AuthenticationType
        {
            get { return Identity.AuthenticationType; }
        }

        public bool IsAuthenticated
        {
            get { return Identity.IsAuthenticated; }
        }

        public int IdSujeto { get; set; }
        public string Usuario { get; set; }
        public string Login { get; set; }
        public short IdSucursal { get; set; }
        public short IdRol { get; set; }

        public IIdentity Identity { get; set; }

        public IdentityPer(IIdentity identity)
        {
            this.Identity = identity;
            var us = Membership.GetUser(identity.Name) as UsuarioMembership;

            IdSujeto = us.IdSujeto;
            Usuario = us.Usuario;
            Login = us.Login;
            IdSucursal = us.IdSucursal;
            IdRol = us.IdRol;
        }
    }
}