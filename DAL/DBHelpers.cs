using System;
using System.Data;
//using Microsoft.Practices.EnterpriseLibrary.Data;
//using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;


namespace DAL
{
    /// Libreria y Funciones para Base de Datos
    /// Autor: Ing Manuel A. Andrés.
    /// Implementa EnterpriseLibrary
    /// 

    public class DBHelper
    {

        struct DBHelperProps
        {
            public int TotalRows;
        }


        DBHelperProps o;


        public DBHelper()
        {
            o = new DBHelperProps();
            o.TotalRows = 0;
        }

        ~DBHelper()
        {
            //oDal = null;
        }

        public int TotalRows
        {

            get { return o.TotalRows; }

        }

        public static SqlConnection GetConnection()
        {
            string connectionString;
            SqlConnection objCon;

            connectionString = ConfigurationManager.ConnectionStrings["Siimva"].ConnectionString;
            objCon = new SqlConnection(connectionString);

            return objCon;
        }


        public string GetXML(string RootNodeName, string NodeName, string strCom)
        {
            return this.GetXMLRs(RootNodeName, NodeName, strCom);
        }


        private string GetXMLRs(string NodeName, string SubNodeName, string strSQL)
        {

            XMLHelper oLibXML = new XMLHelper();
            string GetXML = "";

            o.TotalRows = this.RowCount(strSQL);

            SqlConnection cn = GetConnection();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSQL;
            cmd.Connection.Open();
            SqlDataReader reader = null;

            try
            {

                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                    GetXML = oLibXML.RsToXMLStr(reader, NodeName, SubNodeName);
                else
                    GetXML = "<" + NodeName.Trim() + "/>";

                reader.Close();
                return GetXML;
            }
            finally
            {
                cn.Close();
                oLibXML = null;
            }
        }


        //private string GetStrValue(object Value)
        //{
        //  string strValue = "";
        //  if (Value == null)
        //  {
        //    strValue = "null";
        //  }
        //  else
        //  {
        //    switch (Value.GetType().ToString())
        //    {
        //      case "System.String":

        //        if (string.IsNullOrEmpty(Value.ToString()))
        //          strValue = "null";
        //        else
        //          strValue = "'" + Value.ToString().Replace("'", "\'").Replace(",", ".") + "'";
        //        break;

        //      case "System.Boolean":
        //        strValue = ((System.Convert.ToBoolean(Value)) ? System.Convert.ToString(1) : System.Convert.ToString(0));
        //        break;

        //      case "System.Int16":
        //        strValue = System.Convert.ToInt16(Value).ToString();
        //        break;
        //      case "System.Int32":
        //        strValue = System.Convert.ToInt32(Value).ToString();
        //        break;
        //      case "System.Int64":
        //        strValue = System.Convert.ToInt64(Value).ToString();
        //        break;

        //      case "System.Decimal":
        //        //strValue = System.Convert.ToDouble(Value).ToString();
        //        strValue = "'" + Value.ToString().Replace(",", ".") + "'";
        //        break;

        //      case "System.DateTime":
        //        strValue = "'" + System.DateTime.Parse(Value.ToString()).ToString("dd/MM/yyyy hh:mm:ss") + "'";
        //        //strValue = "'" + System.DateTime.Parse(Value.ToString()).ToString("yyyy-MM-dd hh:mm:ss") + "'";
        //        break;
        //      case "System.Single":
        //        strValue = "'" + Value.ToString().Replace(",", ".") + "'";
        //        //strValue = "'" + System.DateTime.Parse(Value.ToString()).ToString("yyyy-MM-dd hh:mm:ss") + "'";
        //        break;
        //      //case "System.Nullable":
        //      //  strValue = null;
        //      //  //strValue = "'" + System.DateTime.Parse(Value.ToString()).ToString("yyyy-MM-dd hh:mm:ss") + "'";
        //      //  break;

        //    }
        //  }
        //  return strValue;
        //}


        private int RowCount(string strSQL)
        {

            string sqlCount = "";
            int count = 0;


            SqlConnection cn = null;
            SqlCommand cmd = null;
            cn = GetConnection();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;

            SqlDataReader reader = null;

            if (strSQL.ToLower().IndexOf("group", 0) > 0)
            {
                sqlCount = "select count(*) as TotalRows " + strSQL.Substring(strSQL.ToLower().LastIndexOf("from"));

                cmd.CommandText = sqlCount;
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    count = reader.GetInt32(0);
                }
                reader.Close();
            }
            else
            {
                if (strSQL.ToLower().IndexOf("order", 0) > 0)
                {
                    int i1 = strSQL.ToLower().IndexOf("select");
                    int i2 = strSQL.ToLower().LastIndexOf("order");


                    strSQL = strSQL.Substring(i1, i2 - 1);
                    sqlCount = "select count(*) as TotalRows " + strSQL.Substring(strSQL.ToLower().LastIndexOf("from"));
                    cmd.CommandText = sqlCount;
                    cmd.Connection.Open();
                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        count = reader.GetInt32(0);
                    }
                    reader.Close();
                }

                else
                {
                    sqlCount = "SELECT COUNT(*) as TotalRows " + strSQL.Substring(strSQL.ToLower().LastIndexOf("from"));

                    cmd.CommandText = sqlCount;
                    cmd.Connection.Open();
                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        count = reader.GetInt32(0);
                    }

                    reader.Close();
                }
            }
            cn.Close();
            cmd = null;
            return count;
        }


        public DataSet Pagination(string tableName, string strSQL, int Page, int RowsPerPage, string OrderBy, string Order)
        {
            //o.TotalRows = this.RowCount(strSQL);
            if (OrderBy.Trim().Length > 0)
            {
                strSQL += " Order by " + OrderBy + " " + Order;
            }
            SqlConnection cn = null;
            cn = GetConnection();
            SqlCommand cmd = null;
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;

            SqlDataAdapter adapter = null;
            adapter.SelectCommand = cmd;

            DataSet ds = new DataSet();

            try
            {
                adapter.Fill(ds, Page, RowsPerPage, tableName);
                return ds;
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally { cn.Close(); cmd = null; }
        }


        public DataSet Pagination(string strSQL, int Page, int RowsPerPage, string OrderBy, string Order)
        {
            //o.TotalRows = this.RowCount(strSQL);
            if (OrderBy.Trim().Length > 0)
            {
                strSQL += " Order by " + OrderBy + " " + Order;
            }
            SqlConnection cn = null;
            cn = GetConnection();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSQL;
            SqlDataAdapter adapter = null;
            adapter.SelectCommand = cmd;

            DataSet ds = new DataSet();

            try
            {
                adapter.Fill(ds, Page, RowsPerPage, "Result");
                return ds;
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally { cn.Close(); cmd = null; }
        }
        
        public DataSet GetDataSet(string strSQL, string tableName, int lngMaxRegistros)
        {
            //o.TotalRows = this.RowCount(strSQL);
            int p = 0;
            if (lngMaxRegistros > 0)
            {
                strSQL = strSQL.Trim();
                ///Busco el Primer espacio en la cadena SQL.
                p = strSQL.IndexOf(" ", 0);
                ///Inserto la clausula TOP
                strSQL = strSQL.Insert(p - 1, " TOP " + lngMaxRegistros);
            }

            SqlConnection cn = GetConnection();
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSQL;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //adapter.SelectCommand = cmd;

            DataSet ds = new DataSet();

            try
            {
                adapter.Fill(ds);
                return ds;
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                cn.Close();
                cmd = null;
            }
        }

        //public static int CalcularEdad(DateTime fechaNacimiento)
        //{
        //    // Obtiene la fecha actual:
        //    DateTime fechaActual = DateTime.Today;

        //    // Comprueba que la se haya introducido una fecha válida; si 
        //    // la fecha de nacimiento es mayor a la fecha actual se muestra mensaje 
        //    // de advertencia:
        //    if (fechaNacimiento > fechaActual)
        //    {
        //        Console.WriteLine("La fecha de nacimiento es mayor que la actual.");
        //        return -1;
        //    }
        //    else
        //    {
        //        int edad = fechaActual.Year - fechaNacimiento.Year;

        //        // Comprueba que el mes de la fecha de nacimiento es mayor 
        //        // que el mes de la fecha actual:
        //        if (fechaNacimiento.Month > fechaActual.Month)
        //        {
        //            --edad;
        //        }

        //        return edad;
        //    }
        //}

        //public static int CalcularEdad(DateTime fechaNacimiento)
        //{
        //    try
        //    {
        //        //Obtengo la diferencia en años.
        //        int edad = DateTime.Now.Year - fechaNacimiento.Year;

        //        //Obtengo la fecha de cumpleaños de este año.
        //        DateTime nacimientoAhora = fechaNacimiento.AddYears(edad);

        //        //Le resto un año si la fecha actual es anterior 
        //        //al día de nacimiento.
        //        if (DateTime.Now.CompareTo(nacimientoAhora) < 0)
        //        {
        //            edad--;
        //        }

        //        return edad;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public static int CalcularEdad(DateTime fechaNacimiento)
        {
            // Obtiene la fecha actual:
            DateTime fechaActual = DateTime.Today;

            // Comprueba que la se haya introducido una fecha válida; si 
            // la fecha de nacimiento es mayor a la fecha actual se muestra mensaje 
            // de advertencia:
            if (fechaNacimiento > fechaActual)
            {
                Console.WriteLine("La fecha de nacimiento es mayor que la actual.");
                return -1;
            }
            else
            {
                int edad = fechaActual.Year - fechaNacimiento.Year;

                // Comprueba que el mes de la fecha de nacimiento es mayor 
                // que el mes de la fecha actual:
                if (fechaNacimiento.Month > fechaActual.Month)
                {
                    --edad;
                }

                return edad;
            }
        }

    }
}
