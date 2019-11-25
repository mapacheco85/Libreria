using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Threading.Tasks;

namespace Libreria.Models
{
    public class CReports
    {
public static List<string> ListadoReportes()
        {
            
            List<string> lista = new List<string>();
            lista.Add("Elija una opción");
            lista.Add("Usuarios");
            lista.Add("Proveedor");
            lista.Add("Productos");
            lista.Add("Clientes");
            lista.Add("Reservas");
            lista.Add("Promociones");
            lista.Add("Ventas");
            return lista;
        }
    }
}