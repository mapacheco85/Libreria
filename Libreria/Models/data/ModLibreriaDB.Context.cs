﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Libreria.Models.data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ModLibreriaDB : DbContext
    {
        public ModLibreriaDB()
            : base("name=ModLibreriaDB")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Carrito> Carrito { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Clasificacion> Clasificacion { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Descuento> Descuento { get; set; }
        public virtual DbSet<Detalle> Detalle { get; set; }
        public virtual DbSet<DetReserva> DetReserva { get; set; }
        public virtual DbSet<Empresa> Empresa { get; set; }
        public virtual DbSet<Factura> Factura { get; set; }
        public virtual DbSet<Nivel> Nivel { get; set; }
        public virtual DbSet<Pedido> Pedido { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Proveedor> Proveedor { get; set; }
        public virtual DbSet<Reserva> Reserva { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<RolUsuario> RolUsuario { get; set; }
        public virtual DbSet<Stock> Stock { get; set; }
        public virtual DbSet<Sucursal> Sucursal { get; set; }
        public virtual DbSet<Sujeto> Sujeto { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
    
        public virtual ObjectResult<Pro_Reportes_Result> Pro_Reportes(Nullable<System.DateTime> fecha1, Nullable<System.DateTime> fecha2, string tipoReportes)
        {
            var fecha1Parameter = fecha1.HasValue ?
                new ObjectParameter("Fecha1", fecha1) :
                new ObjectParameter("Fecha1", typeof(System.DateTime));
    
            var fecha2Parameter = fecha2.HasValue ?
                new ObjectParameter("Fecha2", fecha2) :
                new ObjectParameter("Fecha2", typeof(System.DateTime));
    
            var tipoReportesParameter = tipoReportes != null ?
                new ObjectParameter("TipoReportes", tipoReportes) :
                new ObjectParameter("TipoReportes", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Pro_Reportes_Result>("Pro_Reportes", fecha1Parameter, fecha2Parameter, tipoReportesParameter);
        }
    
        public virtual ObjectResult<Pro_Reportes_Caja_Result> Pro_Reportes_Caja(Nullable<System.DateTime> fecha1, Nullable<System.DateTime> fecha2, string tipoReportes)
        {
            var fecha1Parameter = fecha1.HasValue ?
                new ObjectParameter("Fecha1", fecha1) :
                new ObjectParameter("Fecha1", typeof(System.DateTime));
    
            var fecha2Parameter = fecha2.HasValue ?
                new ObjectParameter("Fecha2", fecha2) :
                new ObjectParameter("Fecha2", typeof(System.DateTime));
    
            var tipoReportesParameter = tipoReportes != null ?
                new ObjectParameter("TipoReportes", tipoReportes) :
                new ObjectParameter("TipoReportes", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Pro_Reportes_Caja_Result>("Pro_Reportes_Caja", fecha1Parameter, fecha2Parameter, tipoReportesParameter);
        }
    
        public virtual ObjectResult<Pro_Reportes_Clientes_Result> Pro_Reportes_Clientes(Nullable<System.DateTime> fecha1, Nullable<System.DateTime> fecha2, string tipoReportes)
        {
            var fecha1Parameter = fecha1.HasValue ?
                new ObjectParameter("Fecha1", fecha1) :
                new ObjectParameter("Fecha1", typeof(System.DateTime));
    
            var fecha2Parameter = fecha2.HasValue ?
                new ObjectParameter("Fecha2", fecha2) :
                new ObjectParameter("Fecha2", typeof(System.DateTime));
    
            var tipoReportesParameter = tipoReportes != null ?
                new ObjectParameter("TipoReportes", tipoReportes) :
                new ObjectParameter("TipoReportes", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Pro_Reportes_Clientes_Result>("Pro_Reportes_Clientes", fecha1Parameter, fecha2Parameter, tipoReportesParameter);
        }
    
        public virtual ObjectResult<Pro_Reportes_Productos_Result> Pro_Reportes_Productos(Nullable<System.DateTime> fecha1, Nullable<System.DateTime> fecha2, string tipoReportes)
        {
            var fecha1Parameter = fecha1.HasValue ?
                new ObjectParameter("Fecha1", fecha1) :
                new ObjectParameter("Fecha1", typeof(System.DateTime));
    
            var fecha2Parameter = fecha2.HasValue ?
                new ObjectParameter("Fecha2", fecha2) :
                new ObjectParameter("Fecha2", typeof(System.DateTime));
    
            var tipoReportesParameter = tipoReportes != null ?
                new ObjectParameter("TipoReportes", tipoReportes) :
                new ObjectParameter("TipoReportes", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Pro_Reportes_Productos_Result>("Pro_Reportes_Productos", fecha1Parameter, fecha2Parameter, tipoReportesParameter);
        }
    
        public virtual ObjectResult<Pro_Reportes_Promociones_Result> Pro_Reportes_Promociones(Nullable<System.DateTime> fecha1, Nullable<System.DateTime> fecha2, string tipoReportes)
        {
            var fecha1Parameter = fecha1.HasValue ?
                new ObjectParameter("Fecha1", fecha1) :
                new ObjectParameter("Fecha1", typeof(System.DateTime));
    
            var fecha2Parameter = fecha2.HasValue ?
                new ObjectParameter("Fecha2", fecha2) :
                new ObjectParameter("Fecha2", typeof(System.DateTime));
    
            var tipoReportesParameter = tipoReportes != null ?
                new ObjectParameter("TipoReportes", tipoReportes) :
                new ObjectParameter("TipoReportes", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Pro_Reportes_Promociones_Result>("Pro_Reportes_Promociones", fecha1Parameter, fecha2Parameter, tipoReportesParameter);
        }
    
        public virtual ObjectResult<Pro_Reportes_Proveedor_Result> Pro_Reportes_Proveedor(Nullable<System.DateTime> fecha1, Nullable<System.DateTime> fecha2, string tipoReportes)
        {
            var fecha1Parameter = fecha1.HasValue ?
                new ObjectParameter("Fecha1", fecha1) :
                new ObjectParameter("Fecha1", typeof(System.DateTime));
    
            var fecha2Parameter = fecha2.HasValue ?
                new ObjectParameter("Fecha2", fecha2) :
                new ObjectParameter("Fecha2", typeof(System.DateTime));
    
            var tipoReportesParameter = tipoReportes != null ?
                new ObjectParameter("TipoReportes", tipoReportes) :
                new ObjectParameter("TipoReportes", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Pro_Reportes_Proveedor_Result>("Pro_Reportes_Proveedor", fecha1Parameter, fecha2Parameter, tipoReportesParameter);
        }
    
        public virtual ObjectResult<Pro_Reportes_Reservas_Result> Pro_Reportes_Reservas(Nullable<System.DateTime> fecha1, Nullable<System.DateTime> fecha2, string tipoReportes)
        {
            var fecha1Parameter = fecha1.HasValue ?
                new ObjectParameter("Fecha1", fecha1) :
                new ObjectParameter("Fecha1", typeof(System.DateTime));
    
            var fecha2Parameter = fecha2.HasValue ?
                new ObjectParameter("Fecha2", fecha2) :
                new ObjectParameter("Fecha2", typeof(System.DateTime));
    
            var tipoReportesParameter = tipoReportes != null ?
                new ObjectParameter("TipoReportes", tipoReportes) :
                new ObjectParameter("TipoReportes", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Pro_Reportes_Reservas_Result>("Pro_Reportes_Reservas", fecha1Parameter, fecha2Parameter, tipoReportesParameter);
        }
    
        public virtual ObjectResult<Pro_Reportes_Usuarios_Result> Pro_Reportes_Usuarios(Nullable<System.DateTime> fecha1, Nullable<System.DateTime> fecha2, string tipoReportes)
        {
            var fecha1Parameter = fecha1.HasValue ?
                new ObjectParameter("Fecha1", fecha1) :
                new ObjectParameter("Fecha1", typeof(System.DateTime));
    
            var fecha2Parameter = fecha2.HasValue ?
                new ObjectParameter("Fecha2", fecha2) :
                new ObjectParameter("Fecha2", typeof(System.DateTime));
    
            var tipoReportesParameter = tipoReportes != null ?
                new ObjectParameter("TipoReportes", tipoReportes) :
                new ObjectParameter("TipoReportes", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Pro_Reportes_Usuarios_Result>("Pro_Reportes_Usuarios", fecha1Parameter, fecha2Parameter, tipoReportesParameter);
        }
    
        public virtual int PROActualizarStock(Nullable<int> producto, Nullable<int> cantidad)
        {
            var productoParameter = producto.HasValue ?
                new ObjectParameter("producto", producto) :
                new ObjectParameter("producto", typeof(int));
    
            var cantidadParameter = cantidad.HasValue ?
                new ObjectParameter("cantidad", cantidad) :
                new ObjectParameter("cantidad", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("PROActualizarStock", productoParameter, cantidadParameter);
        }
    
        public virtual ObjectResult<PROAutenticar_Result> PROAutenticar(string login, string pwd, Nullable<short> idSucursal)
        {
            var loginParameter = login != null ?
                new ObjectParameter("Login", login) :
                new ObjectParameter("Login", typeof(string));
    
            var pwdParameter = pwd != null ?
                new ObjectParameter("Pwd", pwd) :
                new ObjectParameter("Pwd", typeof(string));
    
            var idSucursalParameter = idSucursal.HasValue ?
                new ObjectParameter("IdSucursal", idSucursal) :
                new ObjectParameter("IdSucursal", typeof(short));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PROAutenticar_Result>("PROAutenticar", loginParameter, pwdParameter, idSucursalParameter);
        }
    
        public virtual ObjectResult<PROListarProductos_Result> PROListarProductos()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PROListarProductos_Result>("PROListarProductos");
        }
    
        public virtual ObjectResult<PROListarPromociones_Result> PROListarPromociones()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PROListarPromociones_Result>("PROListarPromociones");
        }
    
        public virtual ObjectResult<PROListarUsuarios_Result> PROListarUsuarios()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PROListarUsuarios_Result>("PROListarUsuarios");
        }
    
        public virtual ObjectResult<Nullable<int>> PROObtenerNroFactura(string nroAutorizacion)
        {
            var nroAutorizacionParameter = nroAutorizacion != null ?
                new ObjectParameter("NroAutorizacion", nroAutorizacion) :
                new ObjectParameter("NroAutorizacion", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("PROObtenerNroFactura", nroAutorizacionParameter);
        }
    
        public virtual ObjectResult<PROObtenerProductoStock_Result> PROObtenerProductoStock(Nullable<short> idProducto)
        {
            var idProductoParameter = idProducto.HasValue ?
                new ObjectParameter("IdProducto", idProducto) :
                new ObjectParameter("IdProducto", typeof(short));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PROObtenerProductoStock_Result>("PROObtenerProductoStock", idProductoParameter);
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    }
}
