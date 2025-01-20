using System;
using System.Globalization;
using System.Threading;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Configuration;

namespace Web.Library
{
    /// <summary>
    /// Descripción breve de Utiles.
    /// </summary>
    public class Util
    {
        // fecha utilizada para los campos de hora
        public const string DEFAULT_DATE = "30/12/1899";
        // mensaje por defecto para los campos vacios
        public const string DEFAULT_REQUIRED_FIELD_ERROR = "Por favor rellene el campo $";
        // mensaje por defecto para los campos con formato incorrecto
        public const string DEFAULT_CUSTOM_ERROR = "El formato del campo $ es ";


        public Util()
        {
            //
            // TODO: agregar aquí la lógica del constructor
            //
        }


        public static string Version()
        {
            string VERSION_SISTEMA = ConfigurationManager.AppSettings["Version"];
            return VERSION_SISTEMA;
        }
        // ** SetCulture establece la cultura del esplorator web del cliente      **
        public static void SetCulture(string culture)
        {
            CultureInfo info = new CultureInfo(culture, true);
            //return info.ToString();
            Thread.CurrentThread.CurrentCulture = info;
            //CultureInfo info = new CultureInfo(HttpContext.Current.Request.UserLanguages[0], true);
            //return Thread.CurrentThread.CurrentCulture.Name;
        }


        // ** GetFormat devuelve el formato de las fechas                  **
        // ** segun el formato establecido (ej: dd/MM/yyyy)                **
        public static string GetDateFormat(string sCulture)
        {
            CultureInfo info = new CultureInfo(formatCulture(sCulture), true);
            return info.DateTimeFormat.ShortDatePattern;
        }

        private static string formatCulture(string sCulture)
        {
            if (sCulture.Equals("es"))
            {
                return "es-AR";
            }
            if (sCulture.Equals("fr"))
            {
                return "fr-FR";
            }
            if (sCulture.Equals("en"))
            {
                return "en-US";
            }
            return "es-AR";
        }


        // ** GetNumberFormat devuelve el formato de los numeros           **
        // ** segun el pais establecido (ej: ?#.###,00 )                   **
        public static string GetNumberFormat(string sCulture)
        {
            CultureInfo info = new CultureInfo(formatCulture(sCulture), true);
            string s = "?#";  //?#
            s += info.NumberFormat.NumberGroupSeparator; //?#.
            for (int i = 0; i < info.NumberFormat.NumberGroupSizes[0]; i++)
                s += "#";
            // ?#.###
            s += info.NumberFormat.NumberDecimalSeparator; //?#.###,
            for (int i = 0; i < info.NumberFormat.NumberDecimalDigits; i++)
            {
                s += "0";
            }
            // ?#.###,00

            return s;

        }


        // ** GetAdaptedNumberFormat devuelve el formato de un             **
        // ** numero inferior a 1000 (ej ?#,00)                            **
        // ** sFormat: cadena conteniendo el formato generico de un numero **
        public static string GetAdaptedNumberFormat(string sFormat)
        {
            // tomamos el principio del formato hasta la primera almohadilla
            string sBegin = sFormat.Substring(0, 2);
            // tomamos el final del formato a pratir el separador de coma 
            string sEnd = sFormat.Substring(sFormat.IndexOf("0") - 1);
            return sBegin + sEnd; // formato de tipo "?#,##"
        }


        // ** StringToDate convierte una cadena de texto en fecha con un  **
        // ** formato establecido                                         **
        // ** sDate:     cadena a convertir                               **
        // ** bStandard: true para convertir una fecha, false para        **
        // **            convertir una hora                               **
        public static DateTime StringToDate(string sDate, bool bStandard, string sCulture)
        {
            // declaracion de variables helpdeskes
            CultureInfo format = new CultureInfo(formatCulture(sCulture), true);
            DateTime dtDate;

            if (bStandard)
            {
                // conversion de la fecha con el formato establecido
                dtDate = DateTime.Parse(sDate, format, System.Globalization.DateTimeStyles.AllowWhiteSpaces);
            }
            else
            {
                // conversion de la fecha por defecto al formato establecido
                DateTime dtDefaultDate = DateTime.Parse(DEFAULT_DATE, new CultureInfo("es-AR", true), System.Globalization.DateTimeStyles.AllowWhiteSpaces);
                // conversion de la hora con la fecha por defecto y el formato establecido
                dtDate = DateTime.Parse(dtDefaultDate.ToString("d", format) + " " + sDate, format, System.Globalization.DateTimeStyles.AllowWhiteSpaces);
            }
            return dtDate;
        }

        // ** DateToString convierte una fecha en cadena de texto         **
        // ** a partir del formato establecido                            **
        // ** dtDate:    fecha a convertir                                **
        // ** bStandard: true si es una conversion de fecha, false si es  **
        // **            una conversion de hora                           **
        public static string DateToString(DateTime dtDate, bool bStandard, string sCulture)
        {
            CultureInfo cultureInfo = new CultureInfo(formatCulture(sCulture), true);
            // si tratamos una fecha
            if (bStandard)
            {
                // pasamos la separacion '/' a la informacion de pais y idioma
                //cultureInfo.DateTimeFormat.DateSeparator = "/";
                // conversion de la fecha con el formato establecido
                return dtDate.ToString(cultureInfo.DateTimeFormat.ShortDatePattern, cultureInfo);
            }
            // si tratemos una hora
            else
            {
                // conversion de la hora con el formato establecido
                return dtDate.ToString("HH:mm", cultureInfo);
            }

        }


        // ** StringToDouble convierte una cadena de texto en un decimal  **
        // ** a partir de un formato establecido                          **
        // ** sDouble: texto a convertir                                  **
        public static Double StringToDouble(string sDouble, string sCulture)
        {
            // declaracion de variables helpdeskes
            Double dValue = 0;
            // conversion del texto con el formato establecido
            CultureInfo format = new CultureInfo(formatCulture(sCulture), true);
            dValue = Double.Parse(sDouble, format);
            return dValue;
        }


        // ** Conversion de un decimal a una cadena de texto segun el     **
        // ** formato establecido                                         **
        // ** dValue: decimal a convertir                                 **
        public static string DoubleToString(double dValue, string sCulture)
        {
            // declaracion de variables helpdeskes
            CultureInfo format = new CultureInfo(formatCulture(sCulture), true);
            Double d = dValue;
            // conversion con el formato establecido
            return d.ToString(format);
        }


        public static string Left(string param, int length)
        {
            //we start at 0 since we want to get the characters starting from the
            //left and with the specified lenght and assign it to a variable
            string result = param.Substring(0, length);
            //return the result of the operation
            return result;
        }
        public static string Right(string param, int length)
        {
            //start at the index based on the lenght of the sting minus
            //the specified lenght and assign it a variable
            string result = param.Substring(param.Length - length, length);
            //return the result of the operation
            return result;
        }

        public static string Mid(string param, int startIndex, int length)
        {
            //start at the specified index in the string ang get N number of
            //characters depending on the lenght and assign it to a variable
            string result = param.Substring(startIndex, length);
            //return the result of the operation
            return result;
        }

        public static string Mid(string param, int startIndex)
        {
            //start at the specified index and return all characters after it
            //and assign it to a variable
            string result = param.Substring(startIndex);
            //return the result of the operation
            return result;
        }

        public static int Asc(string s)
        {
            return Encoding.ASCII.GetBytes(s)[0];
        }

        public static char Chr(int c)
        {
            return Convert.ToChar(c);
        }


        public static string ConvertToChar(string Input)
        {
            string cadena;
            string par;
            string cpar;
            string Numero;
            int ccar = 0;
            Char pad1, pad2;
            double mod = 0;
            double result = 0;

            pad1 = Chr(0);
            pad2 = Chr(255);

            cpar = "";

            Numero = Input;
            ccar = Numero.Length;

            mod = Numero.Length % 2;
            result = ccar / 2;
            if (mod != 0)
            {
                Numero = "0" + Numero;
            }
            ccar = Numero.Length;
            for (int i = 1; i <= ccar / 2; i++)
            {
                par = Left(Numero, 2);
                if (int.Parse(par) < 90)
                {
                    cpar = cpar + Chr(int.Parse(par) + 33).ToString();
                }
                else
                {
                    cpar = cpar + Chr(int.Parse(par) + 71).ToString();
                }
                Numero = Right(Numero, Numero.Length - 2);

            }
            cadena = Chr(171) + cpar + Chr(172);
            return cadena;
        }



        public static string ArmoCBarra(string nro_cliente, string nro_comprobante,
          decimal importe_1vto, string fecha_1vto, decimal importe_2vto, string fecha_2vto,
          string tipo_cedulon)
        {

            string strCadena = "";
            string _codigo_empresa = "";
            string _nro_cliente = "";
            string _nro_comprobante = "";
            string _importe_1vto = "";
            string _fecha_1vto = "";
            string _recargo = "";
            string _fecha_2vto = "";
            string _tipo_cedulon = "";
            int _digito_ver;
            decimal diferencia;
            int dias;
            Char pad;
            pad = Convert.ToChar("0");

            strCadena = "";
            diferencia = 0;
            dias = 0;
            _codigo_empresa = "0549";
            _nro_cliente = nro_cliente.PadLeft(9, pad);
            _nro_comprobante = nro_comprobante.PadLeft(12, pad);
            _importe_1vto = String.Format("{0:N}", importe_1vto).Replace(".", "").Replace(",", "").PadLeft(8, pad);
            _fecha_1vto = Convert.ToDateTime(fecha_1vto).ToShortDateString().Replace("/", "");
            //String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(fecha_1vto).ToShortDateString()).Replace("/", "");
            //averiguo el recargo si lo hay
            if (importe_2vto > 0)
                diferencia = importe_2vto - importe_1vto;
            _recargo = string.Format("{0:N}", diferencia).Replace(".", "").Replace(",", "").PadLeft(4, pad);
            //averiguo la dif en dias de la fecha del 2vto si lo hay
            if (fecha_2vto.Length > 1)
                dias = Date.GetDaysBetween(fecha_2vto, fecha_1vto);
            _fecha_2vto = dias.ToString().PadLeft(2, pad);
            _tipo_cedulon = tipo_cedulon;
            strCadena = _codigo_empresa + _nro_cliente + _nro_comprobante + _importe_1vto + _fecha_1vto + _recargo + _fecha_2vto + _tipo_cedulon;
            _digito_ver = Calcula_digito_verificador(strCadena);
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



        public static int Licencia(out string mensaje)
        {
            bool error = false;
            float intDays = 0;
            string fechalicencia = "01/06/2021";//"01/08/2014";
            int num_mensaje = 0;
            mensaje = string.Empty;
            
            intDays = Library.Date.GetDaysBetween(fechalicencia, DateTime.Today.ToShortDateString());
            if (intDays >= 0 && intDays <= 30)
            {
                num_mensaje = 1;
                mensaje = "Le quedan " + intDays.ToString() +
                  " dia/s, actualize su Licencia, Comuniquese con el Vendedor del Producto...";
            }
            //else
            //{
            //    mensaje = "La Licencia de este Producto ha Caducado, Por favor solicite una Nueva Licencia...";
            //    num_mensaje = 2;
            //}
            if (DateTime.Today >= Convert.ToDateTime(fechalicencia))
            {
                error = true;
            }
            if (error)
            {
                mensaje = "La Licencia de este Producto ha Caducado, Por favor solicite una Nueva Licencia...";
                num_mensaje = 2;
            }
            return num_mensaje;
        }
    }




    public class Date
    {
        int year;
        int month;
        int day;

        public int Year
        {
            get { return year; }
            private set
            {
                if (value > 3000)
                {
                    Console.WriteLine("Year too late, using '3000' instead");
                    year = 3000;
                }
                else if (value < 1900)
                {
                    Console.WriteLine("Year too early, using '1900' instead");
                    year = 1900;
                }
                else
                {
                    year = value;
                }
            }
        }

        public int Month
        {
            get { return month; }
            private set
            {
                if (value > 12)
                {
                    Console.WriteLine("Month too big, using 12 instead");
                    month = 12;
                }
                else if (value < 1)
                {
                    Console.WriteLine("Month too small, using '1' instead");
                    month = 1;
                }
                else
                {
                    month = value;
                }
            }
        }

        public int Day
        {
            get { return day; }
            private set
            {
                int extraDay = 0;
                if (month == 2 && IsLeapYear(year)) extraDay = 1;
                int maxDays = daysInMonth[month - 1] + extraDay;
                if (value > maxDays)
                {
                    Console.WriteLine("Day too big, using '{0}' instead", maxDays);
                    day = maxDays;
                }
                else if (value < 1)
                {
                    Console.WriteLine("Day too small, using '1' instead");
                    day = 1;
                }
                else
                {
                    day = value;
                }
            }
        }


        public int DaysIntoYear
        {
            get
            {
                if (IsLeapYear(year))
                    return daysIntoLeapYear[month - 1] + day;
                else
                    return daysIntoYear[month - 1] + day;
            }
        }


        public Date(int year, int month, int day)
        {
            this.Year = year;
            this.Month = month;
            this.Day = day;

        }

        static int[] daysInMonth = new int[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        static int[] daysIntoYear = new int[12] { 0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334 };
        static int[] daysIntoLeapYear = new int[12] { 0, 31, 60, 91, 121, 152, 182, 213, 244, 274, 305, 335 };


        public static bool IsLeapYear(int year)
        {
            return (year % 4 == 0) && (year % 100 != 0 || year % 400 == 0);
        }

        private static int GetLeapYearsSince1900(int year)
        {
            int leapYears = (year - 1900) / 4;
            int centuries = (year - 1900) / 100;
            int fourCenturies = (year - 1600) / 400;
            int thisYear = 0;
            if (IsLeapYear(year)) thisYear = 1;
            return leapYears - centuries + fourCenturies - thisYear;
        }

        private static int GetDaysSince1900(Date dt)
        {
            return (dt.Year - 1900) * 365 + GetLeapYearsSince1900(dt.Year) + dt.DaysIntoYear;
        }

        public static int GetDaysBetween(Date dt1, Date dt2)
        {
            int days1 = GetDaysSince1900(dt1);
            int days2 = GetDaysSince1900(dt2);
            return Math.Abs(days1 - days2);
        }

        public static int GetDaysBetween(string date1, string date2)
        {

            DateTime d1, d2;

            DateTime.TryParse(date1, out d1);
            Date dt1 = new Date(d1.Year, d1.Month, d1.Day);

            DateTime.TryParse(date2, out d2);
            Date dt2 = new Date(d2.Year, d2.Month, d2.Day);

            int days1 = GetDaysSince1900(dt1);
            int days2 = GetDaysSince1900(dt2);
            return Math.Abs(days1 - days2);
        }

    }

}
