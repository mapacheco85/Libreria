//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Carrito
    {
        public decimal IdCarrito { get; set; }
        public string SessionID { get; set; }
        public short IdProducto { get; set; }
        public short Cantidad { get; set; }
        public decimal CostoUnidad { get; set; }
        public decimal CostoTotal { get; set; }
        public System.DateTime FechaREG { get; set; }
        public bool Transaccion { get; set; }
        public bool Activo { get; set; }
    
        public virtual Producto Producto { get; set; }
    }
}
