using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Utils
{
    public class Utils
    {
        public static string ArmoCBarra(string cuit, int codComprobante, int ptoVta, Int64 cae, DateTime fecVenc)
        {
            Char pad;
            pad = Convert.ToChar("0");
            string strCadena;
            strCadena = cuit + codComprobante.ToString().PadLeft(3, Convert.ToChar("0")) +
                ptoVta.ToString().PadLeft(3, Convert.ToChar("0")) + cae +
                fecVenc.Year + fecVenc.Month.ToString().PadLeft(2, Convert.ToChar("0")) +
                fecVenc.Day.ToString().PadLeft(2, Convert.ToChar("0"));

            int _digito_ver = Calcula_digito_verificador(strCadena);
            strCadena = strCadena + _digito_ver.ToString();
            return strCadena;
        }


        public static int Calcula_digito_verificador(string CodS)
        {
            int nCant;
            int nSuma, nFloat;
            int[] V1;
            string V2;
            int j;
            int starindex;

            V2 = "1357935793579357935793579357935793579357935793579";

            nCant = CodS.Length;

            V1 = new int[nCant];

            for (j = 0; j < nCant; j++)
            {
                V1[j] = 0;
            }
            Console.Write(j);
            // 1ª etapa
            for (int i = 0; i < nCant; i++)
            {
                V1[i] = Convert.ToInt32(CodS[i].ToString()) * Convert.ToInt32(V2[i].ToString());
            }
            nSuma = 0;
            // 2ª etapa
            for (int i = 0; i < nCant; i++)
            {
                nSuma = nSuma + V1[i];
            }
            nFloat = nSuma / 2;
            starindex = nFloat.ToString().Length - 1;
            return Convert.ToInt32(nFloat.ToString().Substring(starindex, 1));
        }

        public static string EncodeTo64(string toEncode)
        {

            byte[] toEncodeAsBytes

                  = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);

            string returnValue

                  = System.Convert.ToBase64String(toEncodeAsBytes);

            return returnValue;

        }
        public static string DecodeFrom64(string encodedData)

        {

            byte[] encodedDataAsBytes

                = System.Convert.FromBase64String(encodedData);

            string returnValue =

               System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);

            return returnValue;

        }
    }
}