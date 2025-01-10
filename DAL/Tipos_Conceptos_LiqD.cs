using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
  public class Tipos_Conceptos_LiqD:DALBase
  {
    public static DataSet ListTipo_Concep(int id)
    {
      DataSet ds;
      SqlDataAdapter adapter;

      using (SqlConnection conn = GetConnection("Siimva"))
      {
        try
        {
          ds = new DataSet();

          SqlCommand cmd = conn.CreateCommand();
          cmd.CommandType = CommandType.Text;
          if (id > 0)
          {
            cmd.CommandText = "SELECT cod_tipo_concepto, des_tipo_concepto FROM TIPOS_CONCEPTOS_LIQ " +
              " WHERE cod_tipo_concepto=@id";
            cmd.Parameters.AddWithValue("@id", id);
          }
          else
            cmd.CommandText = "SELECT * FROM TIPOS_CONCEPTOS_LIQ";
          cmd.Connection.Open();
          //
          adapter = new SqlDataAdapter(cmd);
          adapter.SelectCommand = cmd;
          adapter.Fill(ds);

          return ds;
        }
        catch (Exception ex)
        {
          throw ex;
        }
      }
    }
  }
}
