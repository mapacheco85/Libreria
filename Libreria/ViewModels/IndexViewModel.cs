using Libreria.Models;
using Libreria.Models.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libreria.ViewModels
{
    public class IndexViewModel: BaseModelo
    {
        public List<Usuario> Usuarios { get; set; }
        public List<Proveedor> Proveedores { get; set; }
        public List<Producto> Productos { get; set; }
        public List<Stock> Stocks { get; set; }
        public List<Categoria> Categorias { get; set; }
        public List<Factura> Facturas { get; set; }
    }
}