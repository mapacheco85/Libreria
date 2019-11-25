using System;
using System.Security.Cryptography;
using System.Text;

namespace Libreria.Utilitarios
{
    public class Utils
    {
        //Funcion para encrptar datos en el registro de Ususario (Password)
        public static string GetSha1(string Texto)
        {
            var sha = SHA1.Create();
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] datos;
            StringBuilder builder = new StringBuilder();

            datos = sha.ComputeHash(encoding.GetBytes(Texto));
            for (int i = 0; i < datos.Length; i++)
            {
                builder.AppendFormat("{0:x2}", datos[i]);
            }

            return builder.ToString();
        }

        public static byte[] GetKey(string txt)
        {
            return new PasswordDeriveBytes(txt, null).GetBytes(32);
        }

        public static string Cifrar(string Contenido, string Clave)
        {
            var encoding = new UTF32Encoding();
            var cripto = new RijndaelManaged();

            byte[] cifrado, retorno, key = GetKey(Clave);

            cripto.Key = key;
            cripto.GenerateIV();
            byte[] aEncriptar = encoding.GetBytes(Contenido);

            cifrado = cripto.CreateEncryptor().TransformFinalBlock(aEncriptar, 0, aEncriptar.Length);

            retorno = new byte[cripto.IV.Length + cifrado.Length];

            cripto.IV.CopyTo(retorno, 0);
            cifrado.CopyTo(retorno, cripto.IV.Length);

            return Convert.ToBase64String(retorno);
        }

        public static string Descifrar(byte[] contenido, string clave)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            var cripto = new RijndaelManaged();
            var ivTemp = new byte[cripto.IV.Length];
            var key = GetKey(clave);
            var cifrado = new byte[contenido.Length - ivTemp.Length];

            cripto.Key = key;

            Array.Copy(contenido, ivTemp, ivTemp.Length);
            Array.Copy(contenido, ivTemp.Length, cifrado, 0, cifrado.Length);

            cripto.IV = ivTemp;
            var descifrado = cripto.CreateDecryptor().TransformFinalBlock(cifrado, 0, cifrado.Length);

            return encoding.GetString(descifrado);
        }



    }
}