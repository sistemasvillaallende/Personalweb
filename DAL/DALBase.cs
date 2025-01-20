using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace DAL
{
  public class DALBase
  {
    public DALBase()
    {

    }

    public static SqlConnection GetConnection(string strDB)
    {
      string connectionString;
      SqlConnection objCon;

      //connectionString = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
      connectionString = ConfigurationManager.ConnectionStrings[strDB].ConnectionString;
      objCon = new SqlConnection(connectionString);

      return objCon;
    }


    public static string MakeInsertSQL(string SQLStr, string FieldName, object Value)
    {
      string retStr = "";
      string sep = "";
      string fields = "";
      string values = "";

      if (SQLStr.Length == 0)
      {
        fields = "()";
        values = "()";
        sep = "";
      }
      else
      {
        int ef = SQLStr.IndexOf(")");
        int sv = SQLStr.LastIndexOf("(");

        fields = SQLStr.Substring(0, ef + 1);
        values = SQLStr.Substring(sv);
        sep = ",";
      }
      string strValue = GetStrValue(Value);

      fields = fields.Replace(")", sep + FieldName + ")");
      values = values.Replace(")", sep + strValue + ")");
      retStr = fields + " values " + values;

      return retStr;

    }



    public static string MakeUpdateSQL(string SQLStr, string FieldName, object Value)
    {
      string retStr = "";
      string sep = "";

      if (SQLStr.Length == 0)
      {
        retStr = " set ";
        sep = "";
      }
      else
      {
        retStr = SQLStr;
        sep = ",";
      }
      string strValue = GetStrValue(Value);

      retStr = retStr + sep + " " + FieldName + "=" + strValue;
      return retStr;
    }


    public static string GetStrValue(object Value)
    {
      string strValue = "";
      if (Value == null)
      {
        strValue = "null";
      }
      else
      {
        switch (Value.GetType().ToString())
        {
          case "System.String":

            if (string.IsNullOrEmpty(Value.ToString()))
              strValue = "null";
            else
              strValue = "'" + Value.ToString().Replace("'", "\'").Replace(",", ".") + "'";
            break;

          case "System.Boolean":
            strValue = ((System.Convert.ToBoolean(Value)) ? System.Convert.ToString(1) : System.Convert.ToString(0));
            break;

          case "System.Int16":
            strValue = System.Convert.ToInt16(Value).ToString();
            break;
          case "System.Int32":
            strValue = System.Convert.ToInt32(Value).ToString();
            break;
          case "System.Int64":
            strValue = System.Convert.ToInt64(Value).ToString();
            break;

          case "System.Decimal":
            //strValue = System.Convert.ToDouble(Value).ToString();
            strValue = "'" + Value.ToString().Replace(",", ".") + "'";
            break;

          case "System.DateTime":
            strValue = "'" + System.DateTime.Parse(Value.ToString()).ToString("dd/MM/yyyy hh:mm:ss") + "'";
            //strValue = "'" + System.DateTime.Parse(Value.ToString()).ToString("yyyy-MM-dd hh:mm:ss") + "'";
            break;
          case "System.Single":
            strValue = "'" + Value.ToString().Replace(",", ".") + "'";
            //strValue = "'" + System.DateTime.Parse(Value.ToString()).ToString("yyyy-MM-dd hh:mm:ss") + "'";
            break;
            //case "System.Nullable":
            //  strValue = null;
            //  //strValue = "'" + System.DateTime.Parse(Value.ToString()).ToString("yyyy-MM-dd hh:mm:ss") + "'";
            //  break;

        }
      }
      return strValue;
    }

    //public static DataSet GetDataSet(string strSQL, string tableName, int lngMaxRegistros)
    //{

    //  int p = 0;
    //  if (lngMaxRegistros > 0)
    //  {
    //    strSQL = strSQL.Trim();
    //    ///Busco el Primer espacio en la cadena SQL.
    //    p = strSQL.IndexOf(" ", 0);
    //    ///Inserto la clausula TOP
    //    strSQL = strSQL.Insert(p - 1, " TOP " + lngMaxRegistros);
    //  }

    //  SqlConnection cn = DALBase.GetConnection();
    //  SqlCommand cmd = null;
    //  cmd = new SqlCommand();
    //  cmd.Connection = cn;
    //  cmd.CommandType = CommandType.Text;
    //  cmd.CommandText = strSQL;
    //  SqlDataAdapter adapter = null;
    //  adapter.SelectCommand = cmd;

    //  DataSet ds = new DataSet();

    //  try
    //  {
    //    adapter.Fill(ds, tableName);
    //    return ds;
    //  }
    //  catch (SqlException e)
    //  {
    //    throw e;
    //  }
    //  finally
    //  {
    //    cn.Close();
    //    cmd = null;
    //  }
    //}


    public static DataSet Pagination(string tableName, string strSQL, int Page, int RowsPerPage, string OrderBy, string Order)
    {
      //o.TotalRows = this.RowCount(strSQL);
      if (OrderBy.Trim().Length > 0)
      {
        strSQL += " Order by " + OrderBy + " " + Order;
      }
      SqlConnection cn = null;
      cn = DALBase.GetConnection("Siimva");
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

    public static DataSet Pagination(string strSQL, int Page, int RowsPerPage, string OrderBy, string Order)
    {
      //o.TotalRows = this.RowCount(strSQL);
      if (OrderBy.Trim().Length > 0)
      {
        strSQL += " Order by " + OrderBy + " " + Order;
      }
      SqlConnection cn = null;
      cn = DALBase.GetConnection("Siimva");
      SqlCommand cmd = null;
      cmd = new SqlCommand();
      cmd.Connection = cn;
      cmd.CommandType = CommandType.Text;
      cmd.CommandText = strSQL;

      SqlDataAdapter adapter = new SqlDataAdapter();

      //cmd.Connection.Open();
      adapter.SelectCommand = cmd;



      DataSet ds = new DataSet();

      try
      {
        adapter.Fill(ds, Page, RowsPerPage, "Expediente");



        return ds;
      }
      catch (SqlException e)
      {
        throw e;
      }
      finally { cn.Close(); cmd = null; adapter = null; }
    }


    private static long RowCount(string strSQL)
    {

      string sqlCount = "";
      long count = 0;


      SqlConnection cn = null;
      SqlCommand cmd = null;
      cn = DALBase.GetConnection("Siimva");

      cmd = new SqlCommand();
      cmd.Connection = cn;
      cmd.CommandType = CommandType.Text;

      SqlDataReader reader = null;

      cmd.Connection.Open();

      if (strSQL.ToLower().IndexOf("group", 0) > 0)
      {
        sqlCount = "select count(*) as TotalRows " + strSQL.Substring(strSQL.ToLower().LastIndexOf("from"));

        cmd.CommandText = sqlCount; ;
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


    public static long NewID(string tableName, string campo)
    {
      long id = 0;

      StringBuilder strSQL = new StringBuilder();

      strSQL.AppendLine("SELECT MAX(nro_cedulon) As Mayor");
      strSQL.AppendLine("FROM " + tableName);

      SqlCommand cmd = new SqlCommand();
      SqlConnection cn = null;

      cmd.Parameters.Add(new SqlParameter("@campo", campo));
      //cmd.Parameters.Add(new SqlParameter("@table", tableName));

      try
      {
        cn = DALBase.GetConnection("Siimva");

        cmd.Connection = cn;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = strSQL.ToString();
        cmd.Connection.Open();

        SqlDataReader dr = cmd.ExecuteReader();

        while (dr.Read())
          id = dr.GetInt32(0) + 1;

        return id;
      }
      catch (Exception ex)
      {
        System.Console.WriteLine("Error, no se pudo obtener el prox. código, " + ex.Message);
        throw ex;
        /*EventLog.WriteEntry("netLibraty - nvoCodigo ", ex.Message);*/
      }
      finally { cn.Close(); }
    }


    public static int Antiguedad(DateTime desde, DateTime hasta)
    {

      int anios = 0;
      int mes = 0;
      int aux_anios = 0;
      int antiguedad = 0;
      
      aux_anios = hasta.Year - desde.Year;
      mes = desde.Month;
      if (mes <= 6)
        anios = aux_anios;
      else
        anios = aux_anios - 1;

      if (anios < 0)
        antiguedad = 0;
      else
        antiguedad = anios;

      return antiguedad;

    }
  }
}

