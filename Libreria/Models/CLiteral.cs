using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Libreria.Models
{
    class CLiteral
    {
    }
    public class Num2Letras
    {
        private string[] Unidades = new string[] { "", "UN ", "DOS ", "TRES ", "CUATRO ", "CINCO ", "SEIS ", "SIETE ", "OCHO ", "NUEVE " };
        private string[] Decenas = new string[] {"DIEZ ","ONCE ","DOCE ","TRECE ","CATORCE ","QUINCE ","DIECISEIS ","DIECISIETE ","DIECIOCHO ","DIECINUEVE ",
                                             "VEINTE ","TREINTA ","CUARENTA ","CINCUENTA ","SESENTA ","SETENTA ","OCHENTA ","NOVENTA " };
        private string[] Centenas = new string[] {"","CIENTO ","DOCIENTOS ","TRECIENTOS ","CUATROCIENTOS ","QUINIENTOS ","SEISCIENTOS ","SETECIENTOS ","OCHOCIENTOS ",
                                             "NOVECIENTOS "};
        private Regex r;

        public string Convertir(string Numero, bool Mayusculas = true)
        {
            string Literal = string.Empty;
            string ParteDecimal = string.Empty;

            Numero = Numero.Replace(".", ",");

            if (Numero.IndexOf(",") == -1)
            {
                Numero = Numero + ",00";
            }

            //se valida el formato de entrada
            r = new Regex(@"\d{1,9},\d{1,2}");
            MatchCollection MC = r.Matches(Numero);

            if (MC.Count > 0)
            {
                string[] Num = Numero.Split(',');

                ParteDecimal = Num[1] + "/100 BOLIVIANOS";
                //Se convierte el numero a literal.
                if (int.Parse(Num[0]) == 0)
                {
                    Literal = "CERO";
                }
                else if (int.Parse(Num[0]) > 999999)
                {
                    Literal = GetMillones(Num[0]);
                }
                else if (int.Parse(Num[0]) > 999)
                {
                    Literal = GetMiles(Num[0]);
                }
                else if (int.Parse(Num[0]) > 99)
                {
                    Literal = GetCentenas(Num[0]);
                }
                else if (int.Parse(Num[0]) > 9)
                {
                    Literal = GetDecenas(Num[0]);
                }
                else
                {
                    Literal = GetUnidades(Num[0]);
                }

                //Devuelve el resultado en Mayusculas o minusculas.
                if (Mayusculas)
                {
                    return (Literal + ParteDecimal).ToUpper();
                }
                else
                {
                    return (Literal + ParteDecimal).ToLower();
                }

            }
            else
                return Literal = null;
        }//Fin Convertir.

        //Funciones que converierten Numeros a literales
        private string GetUnidades(string Numero)
        {
            string Num = Numero.Substring(Numero.Length - 1);
            return Unidades[int.Parse(Num)];
        }

        private string GetDecenas(string Numero)
        {
            int N = int.Parse(Numero);
            if (N < 10)
            {
                return GetUnidades(Numero);
            }
            else if (N > 19)
            {
                string U = GetUnidades(Numero);
                if (U.Equals(""))
                {
                    return Decenas[int.Parse(Numero.Substring(0, 1)) + 8];
                }
                else
                {
                    return Decenas[int.Parse(Numero.Substring(0, 1)) + 8] + " Y " + U;
                }

            }
            else
            {
                return Decenas[N - 10];
            }

        }//Fin GetDecenas.

        private string GetCentenas(string Numero)
        {
            if (int.Parse(Numero) > 99)
            {
                if (int.Parse(Numero) == 100)
                {
                    return "CIEN ";
                }
                else
                {
                    return Centenas[int.Parse(Numero.Substring(0, 1))] + GetDecenas(Numero.Substring(1));
                }
            }
            else
                return GetDecenas(int.Parse(Numero).ToString());

        }//Fin GetCentenas.

        private string GetMiles(string Numero)
        {
            string C = Numero.Substring(Numero.Length - 3);
            string M = Numero.Substring(0, Numero.Length - 3);
            string N = string.Empty;

            if (int.Parse(M) > 0)
            {
                N = GetCentenas(M);
                return N + "MIL " + GetCentenas(C);
            }
            else
                return "" + GetCentenas(C);

        }//Fin GetMiles

        private string GetMillones(string Numero)
        {
            string Miles = Numero.Substring(Numero.Length - 6);
            string Millon = Numero.Substring(0, Numero.Length - 6);
            string N = string.Empty;

            if (Millon.Length > 1)
            {
                N = GetCentenas(Millon) + "MILLONES ";
            }
            else
            {
                N = GetUnidades(Millon) + "MILLON ";
            }
            return N + GetMiles(Miles);

        }//Fin GetMillones.

    }//Fin class Num2Letras
}
