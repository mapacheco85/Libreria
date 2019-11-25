using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Models
{
    class CCodigoControl
    {
        public string GetCodigoControl(string NroAutorizacion, string NroFactura, string Nit, string Fecha, string Monto, string Llave)
        {
            string Codigo = string.Empty;

            for (int i = 1; i <= 2; i++)
            {
                NroFactura += NroVerhoeff(NroFactura).ToString();
                Nit += NroVerhoeff(Nit);
                Fecha += NroVerhoeff(Fecha);
                Monto += NroVerhoeff(Monto);
            }

            string DigitosVerhoeff = (long.Parse(NroFactura) + long.Parse(Nit) + long.Parse(Fecha) + long.Parse(Monto)).ToString();

            for (int i = 1; i <= 5; i++)
            {
                DigitosVerhoeff += NroVerhoeff(DigitosVerhoeff);
            }

            DigitosVerhoeff = DigitosVerhoeff.Substring(DigitosVerhoeff.Length - 5);

            string Tmp = Llave;
            //Paso 2
            NroAutorizacion += Tmp.Substring(0, (int.Parse(DigitosVerhoeff[0].ToString()) + 1));
            Tmp = Tmp.Substring((int.Parse(DigitosVerhoeff[0].ToString()) + 1));
            NroFactura += Tmp.Substring(0, (int.Parse(DigitosVerhoeff[1].ToString()) + 1));
            Tmp = Tmp.Substring((int.Parse(DigitosVerhoeff[1].ToString()) + 1));
            Nit += Tmp.Substring(0, (int.Parse(DigitosVerhoeff[2].ToString()) + 1));
            Tmp = Tmp.Substring((int.Parse(DigitosVerhoeff[2].ToString()) + 1));
            Fecha += Tmp.Substring(0, (int.Parse(DigitosVerhoeff[3].ToString()) + 1));
            Tmp = Tmp.Substring((int.Parse(DigitosVerhoeff[3].ToString()) + 1));
            Monto += Tmp.Substring(0, (int.Parse(DigitosVerhoeff[4].ToString()) + 1));
            Tmp = Tmp.Substring((int.Parse(DigitosVerhoeff[4].ToString()) + 1));
            //Paso 3
            string Concatena = NroAutorizacion + NroFactura + Nit + Fecha + Monto;
            string LlaveCifrado = Llave + DigitosVerhoeff;

            Tmp = AllegeDRC4(Concatena, LlaveCifrado);
            //Paso 4
            //Sumatoria de todos los ASCII
            int TotalAscii = 0;
            foreach (char x in Tmp)
            {
                TotalAscii += (int)x;
            }

            int[] SumParcial = new int[] { 0, 0, 0, 0, 0 };

            for (int i = 0; i < 5; i++)
            {
                for (int j = i; j < Tmp.Length; j += 5)
                {
                    SumParcial[i] += (int)Tmp[j];
                }
            }
            //Paso 5
            int Sumatoria = 0;
            for (int i = 0; i < 5; i++)
            {
                Sumatoria += ((TotalAscii * SumParcial[i]) / (int.Parse(DigitosVerhoeff[i].ToString()) + 1));
            }
            //Paso 6
            Codigo = AllegeDRC4(Base64(Sumatoria), Llave + DigitosVerhoeff);
            Tmp = Codigo;
            Codigo = string.Empty;
            for (int i = 0; i < Tmp.Length; i += 2)
            {
                Codigo += "-" + Tmp.Substring(i, 2);
            }

            return Codigo.Substring(1);
        }//Fin GetCodigoControl

        public int NroVerhoeff(string Cifra)
        {
            int[,] Mul = new int[,] { {0,1,2,3,4,5,6,7,8,9},
                                {1,2,3,4,0,6,7,8,9,5},
                                {2,3,4,0,1,7,8,9,5,6},
                                {3,4,0,1,2,8,9,5,6,7},
                                {4,0,1,2,3,9,5,6,7,8},
                                {5,9,8,7,6,0,4,3,2,1},
                                {6,5,9,8,7,1,0,4,3,2},
                                {7,6,5,9,8,2,1,0,4,3},
                                {8,7,6,5,9,3,2,1,0,4},
                                {9,8,7,6,5,4,3,2,1,0}
                              };
            int[,] Per = new int[,] { {0,1,2,3,4,5,6,7,8,9},
                                {1,5,7,6,2,8,3,0,9,4},
                                {5,8,0,3,7,9,6,1,4,2},
                                {8,9,1,6,0,4,3,5,2,7},
                                {9,4,5,3,1,2,6,8,7,0},
                                {4,2,8,6,5,7,3,9,0,1},
                                {2,7,9,3,8,0,6,4,1,5},
                                {7,0,4,6,9,1,3,2,5,8}
                              };
            int[] Inv = new int[] { 0, 4, 3, 2, 1, 5, 6, 7, 8, 9 };
            int Check = 0;
            string NroInvertido = new string(Cifra.Reverse().ToArray());

            //Inicio.
            for (int i = 0; i <= (NroInvertido.Length - 1); i++)
            {
                Check = Mul[Check, Per[((i + 1) % 8), int.Parse(NroInvertido[i].ToString())]];
            }
            return Inv[Check];
        }//Fin NroVerhoeff

        public string AllegeDRC4(string Mensaje, string Key)
        {
            int[] State = new int[256];
            int x = 0, y = 0, Index1 = 0, Index2 = 0, Nmen;
            string MensajeCifrado = string.Empty;

            for (int i = 0; i <= 255; i++)
            {
                State[i] = i;
            }

            for (int i = 0; i <= 255; i++)
            {
                Index2 = ((int)(Key[Index1]) + State[i] + Index2) % 256;
                //Intercambio de valores
                var Aux = State[i];
                State[i] = State[Index2];
                State[Index2] = Aux;

                Index1 = (Index1 + 1) % Key.Length;
            }

            for (int i = 0; i <= (Mensaje.Length - 1); i++)
            {
                x = (x + 1) % 256;
                y = (State[x] + y) % 256;
                //Intercambio de valores
                var Aux = State[x];
                State[x] = State[y];
                State[y] = Aux;

                Nmen = Mensaje[i] ^ State[(State[x] + State[y]) % 256];

                MensajeCifrado = MensajeCifrado + ((Nmen.ToString("X").Length == 1) ? "0" + Nmen.ToString("X") : Nmen.ToString("X"));
            }

            return MensajeCifrado;
        }// Fin AllegeDRC4

        public string Base64(int Numero)
        {
            char[] Diccionario = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                                        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
                                        'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
                                        'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd',
                                        'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
                                        'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x',
                                        'y', 'z', '+', '/'};
            int Cociente = 1, Resto;
            string Palabra = string.Empty;

            while (Cociente > 0)
            {
                Cociente = Numero / 64;
                Resto = Numero % 64;
                Palabra = Diccionario[Resto] + Palabra;
                Numero = Cociente;
            }
            return Palabra;
        }//Fin Base64
    }
}
