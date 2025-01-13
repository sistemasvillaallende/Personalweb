// Decompiled with JetBrains decompiler
// Type: DAL.Fichas.Resultado_evaluacion
// Assembly: DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 91BCCD41-7BB9-464A-8DA6-7AF7AD7D1B8D
// Assembly location: D:\Muni\Dev\rrhh\bin\DAL.dll

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAL.Fichas
{
    public class Resultado_evaluacion : DALBase
    {
        public int LEGAGO { get; set; }

        public string NOMBRE { get; set; }

        public string CONTRATACION { get; set; }

        public string CLASIFICACION { get; set; }

        public string SECRETARIA { get; set; }

        public string DIRECCION { get; set; }

        public string OFICINA { get; set; }

        public string PROGRAMA { get; set; }

        public string EVALUADOR { get; set; }

        public Decimal RESULTADO { get; set; }

        public int ID_FICHA { get; set; }

        public Resultado_evaluacion()
        {
            this.LEGAGO = 0;
            this.NOMBRE = string.Empty;
            this.CONTRATACION = string.Empty;
            this.CLASIFICACION = string.Empty;
            this.SECRETARIA = string.Empty;
            this.DIRECCION = string.Empty;
            this.OFICINA = string.Empty;
            this.PROGRAMA = string.Empty;
            this.EVALUADOR = string.Empty;
            this.RESULTADO = 0M;
            this.ID_FICHA = 0;
        }

        private static List<Resultado_evaluacion> mapeo(SqlDataReader dr)
        {
            List<Resultado_evaluacion> resultadoEvaluacionList = new List<Resultado_evaluacion>();
            if (dr.HasRows)
            {
                int ordinal1 = dr.GetOrdinal("LEGAGO");
                int ordinal2 = dr.GetOrdinal("NOMBRE");
                int ordinal3 = dr.GetOrdinal("CONTRATACION");
                int ordinal4 = dr.GetOrdinal("CLASIFICACION");
                int ordinal5 = dr.GetOrdinal("SECRETARIA");
                int ordinal6 = dr.GetOrdinal("DIRECCION");
                int ordinal7 = dr.GetOrdinal("OFICINA");
                int ordinal8 = dr.GetOrdinal("PROGRAMA");
                int ordinal9 = dr.GetOrdinal("EVALUADOR");
                int ordinal10 = dr.GetOrdinal("RESULTADO");
                int ordinal11 = dr.GetOrdinal("ID");
                while (dr.Read())
                {
                    Resultado_evaluacion resultadoEvaluacion = new Resultado_evaluacion();
                    if (!dr.IsDBNull(ordinal1))
                        resultadoEvaluacion.LEGAGO = dr.GetInt32(ordinal1);
                    if (!dr.IsDBNull(ordinal2))
                        resultadoEvaluacion.NOMBRE = dr.GetString(ordinal2);
                    if (!dr.IsDBNull(ordinal3))
                        resultadoEvaluacion.CONTRATACION = dr.GetString(ordinal3);
                    if (!dr.IsDBNull(ordinal4))
                        resultadoEvaluacion.CLASIFICACION = dr.GetString(ordinal4);
                    if (!dr.IsDBNull(ordinal5))
                        resultadoEvaluacion.SECRETARIA = dr.GetString(ordinal5);
                    if (!dr.IsDBNull(ordinal6))
                        resultadoEvaluacion.DIRECCION = dr.GetString(ordinal6);
                    if (!dr.IsDBNull(ordinal7))
                        resultadoEvaluacion.OFICINA = dr.GetString(ordinal7);
                    if (!dr.IsDBNull(ordinal8))
                        resultadoEvaluacion.PROGRAMA = dr.GetString(ordinal8);
                    if (!dr.IsDBNull(ordinal9))
                        resultadoEvaluacion.EVALUADOR = dr.GetString(ordinal9);
                    if (!dr.IsDBNull(ordinal10))
                        resultadoEvaluacion.RESULTADO = (Decimal)dr.GetInt32(ordinal10);
                    if (!dr.IsDBNull(ordinal11))
                        resultadoEvaluacion.ID_FICHA = dr.GetInt32(ordinal11);
                    resultadoEvaluacionList.Add(resultadoEvaluacion);
                }
            }
            return resultadoEvaluacionList;
        }

        public static List<Resultado_evaluacion> read(int idFicha)
        {
            try
            {
                List<Resultado_evaluacion> resultadoEvaluacionList = new List<Resultado_evaluacion>();
                using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText =
                    @"SELECT 
                        e.legajo AS LEGAGO, 
                        rtrim(ltrim(e.nombre)) as NOMBRE,
                        tl.des_tipo_liq AS CONTRATACION, 
                        J.des_clasif_per AS CLASIFICACION,
                        rtrim(ltrim(s.descripcion)) as SECRETARIA,
                        rtrim(ltrim(d1.descripcion)) as DIRECCION,
                        ltrim(rtrim(o.nombre_oficina)) as OFICINA,
                        ltrim(rtrim(p.Programa)) as PROGRAMA,
                        --ISNULL(z.NOMBRE, 'Sin Evaluar') AS ESTADO,
                        Y.NOMBRE_COMPLETO AS EVALUADOR,
                        AVG(I.PUNTUACION) AS RESULTADO,
                        X.ID
                    FROM EMPLEADOS e
                        LEFT join TIPOS_LIQUIDACION tl on tl.cod_tipo_liq = e.cod_tipo_liq
                        LEFT join BANCOS b on b.cod_banco = e.cod_banco
                        LEFT join CATEGORIAS c on e.cod_categoria = c.cod_categoria
                        LEFT join secretaria s on s.id_secretaria = e.id_secretaria
                        LEFT join direccion d1 on d1.id_direccion = e.id_direccion
                        LEFT join oficinas o on o.codigo_oficina = e.id_oficina
                        LEFT join PROGRAMAS_PUBLICOS p on p.Id_programa = e.id_programa
                        FULL JOIN FICHAS_RELEVAMIENTOS X ON X.CUIT = e.legajo AND X.ID_FICHA=@ID_FICHA
                        FULL JOIN FICHAS_ESTADOS_EVALUACION z ON X.ID_ESTADO = Z.ID
                        FULL JOIN USUARIOS_V2 Y ON X.USUARIO_RELEVA=Y.NOMBRE
                        FULL JOIN CLASIFICACIONES_PERSONAL J ON E.cod_clasif_per=J.cod_clasif_per
                        FULL JOIN FICHAS_RELEVAMIENTOS_PERSONAS H ON H.ID_RELEVAMIENTO = X.ID
                        FULL JOIN FICHAS_RESPUESTAS I ON H.ID_RESPUESTA=I.ID AND I.PUNTUACION <> 0
                    WHERE e.fecha_baja is null AND e.legajo <> 615 AND e.activo=1
                    GROUP BY 
                        e.legajo, 
                        (ltrim(e.nombre)),
                        tl.des_tipo_liq,
                        J.des_clasif_per,
                        rtrim(ltrim(s.descripcion)),
                        rtrim(ltrim(d1.descripcion)),
                        ltrim(rtrim(o.nombre_oficina)),
                        ltrim(rtrim(p.Programa)),
                        --ISNULL(z.NOMBRE, 'Sin Evaluar') AS ESTADO, 
                        Y.NOMBRE_COMPLETO,
                        X.ID
                    ORDER BY rtrim(ltrim(e.nombre)) DESC";

                    command.Parameters.AddWithValue("@ID_FICHA", idFicha);
                    command.Connection.Open();
                    return Resultado_evaluacion.mapeo(command.ExecuteReader());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Resultado_evaluacion> getBySecretaria(int id_secretaria)
        {
            try
            {
                List<Resultado_evaluacion> resultadoEvaluacionList = new List<Resultado_evaluacion>();
                using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "\r\n                    SELECT\r\n\t                    e.legajo AS LEGAGO, rtrim(ltrim(e.nombre)) as NOMBRE, \r\n\t                    tl.des_tipo_liq AS CONTRATACION,\r\n\t                    J.des_clasif_per AS CLASIFICACION,\r\n\t                    rtrim(ltrim(s.descripcion)) as SECRETARIA, \r\n\t                    rtrim(ltrim(d1.descripcion)) as DIRECCION,\r\n\t                    ltrim(rtrim(o.nombre_oficina)) as OFICINA,\r\n\t                    ltrim(rtrim(p.Programa)) as PROGRAMA,\r\n\t                    --ISNULL(z.NOMBRE, 'Sin Evaluar') AS ESTADO, \r\n\t                    Y.NOMBRE_COMPLETO AS EVALUADOR,\r\n\t                    AVG(I.PUNTUACION) AS RESULTADO,\r\n\t                    X.ID\r\n\t                    FROM EMPLEADOS e\r\n\t                    LEFT join TIPOS_LIQUIDACION tl on tl.cod_tipo_liq = e.cod_tipo_liq\r\n\t                    LEFT join BANCOS b on b.cod_banco = e.cod_banco\r\n\t                    LEFT join CATEGORIAS c on e.cod_categoria = c.cod_categoria\r\n\t                    LEFT join secretaria s on s.id_secretaria = e.id_secretaria\r\n\t                    LEFT join direccion d1 on d1.id_direccion = e.id_direccion\r\n\t                    LEFT join oficinas o on\r\n\t                    o.codigo_oficina = e.id_oficina\r\n\t                    LEFT join PROGRAMAS_PUBLICOS p on\r\n\t                    p.Id_programa = e.id_programa\r\n\t                    FULL JOIN FICHAS_RELEVAMIENTOS X ON X.CUIT = e.legajo\r\n\t                    FULL JOIN FICHAS_ESTADOS_EVALUACION z ON X.ID_ESTADO = Z.ID\r\n\t                    INNER JOIN USUARIOS_V2 Y ON X.USUARIO_RELEVA=Y.NOMBRE\r\n\t                    INNER JOIN CLASIFICACIONES_PERSONAL J ON E.cod_clasif_per=J.cod_clasif_per\r\n\t\t\t\t\t\tINNER JOIN FICHAS_RELEVAMIENTOS_PERSONAS H ON H.ID_RELEVAMIENTO = X.ID\r\n\t\t\t\t\t\tINNER JOIN FICHAS_RESPUESTAS I ON H.ID_RESPUESTA=I.ID AND I.PUNTUACION <> 0\r\n                        WHERE e.fecha_baja is null AND e.legajo <> 615 AND e.activo=1 AND e.id_secretaria=@id_secretaria\r\n\t\t\t\t\tGROUP BY \t                    \r\n\t\t\t\t\t\te.legajo, \r\n\t\t\t\t\t\trtrim(ltrim(e.nombre)), \r\n\t                    tl.des_tipo_liq,\r\n\t                    J.des_clasif_per,\r\n\t                    rtrim(ltrim(s.descripcion)), \r\n\t                    rtrim(ltrim(d1.descripcion)),\r\n\t                    ltrim(rtrim(o.nombre_oficina)),\r\n\t                    ltrim(rtrim(p.Programa)),\r\n\t                    --ISNULL(z.NOMBRE, 'Sin Evaluar') AS ESTADO, \r\n\t                    Y.NOMBRE_COMPLETO,\r\n\t\t\t\t\t\tX.ID \r\n                    ORDER BY rtrim(ltrim(e.nombre)) DESC";
                    command.Parameters.AddWithValue("@id_secretaria", (object)id_secretaria);
                    command.Connection.Open();
                    return Resultado_evaluacion.mapeo(command.ExecuteReader());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Resultado_evaluacion> getByDireccion(int id_direccion)
        {
            try
            {
                List<Resultado_evaluacion> resultadoEvaluacionList = new List<Resultado_evaluacion>();
                using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "\r\n                    SELECT\r\n\t                    e.legajo AS LEGAGO, rtrim(ltrim(e.nombre)) as NOMBRE, \r\n\t                    tl.des_tipo_liq AS CONTRATACION,\r\n\t                    J.des_clasif_per AS CLASIFICACION,\r\n\t                    rtrim(ltrim(s.descripcion)) as SECRETARIA, \r\n\t                    rtrim(ltrim(d1.descripcion)) as DIRECCION,\r\n\t                    ltrim(rtrim(o.nombre_oficina)) as OFICINA,\r\n\t                    ltrim(rtrim(p.Programa)) as PROGRAMA,\r\n\t                    --ISNULL(z.NOMBRE, 'Sin Evaluar') AS ESTADO, \r\n\t                    Y.NOMBRE_COMPLETO AS EVALUADOR,\r\n\t                    AVG(I.PUNTUACION) AS RESULTADO,\r\n\t                    X.ID\r\n\t                    FROM EMPLEADOS e\r\n\t                    LEFT join TIPOS_LIQUIDACION tl on tl.cod_tipo_liq = e.cod_tipo_liq\r\n\t                    LEFT join BANCOS b on b.cod_banco = e.cod_banco\r\n\t                    LEFT join CATEGORIAS c on e.cod_categoria = c.cod_categoria\r\n\t                    LEFT join secretaria s on s.id_secretaria = e.id_secretaria\r\n\t                    LEFT join direccion d1 on d1.id_direccion = e.id_direccion\r\n\t                    LEFT join oficinas o on\r\n\t                    o.codigo_oficina = e.id_oficina\r\n\t                    LEFT join PROGRAMAS_PUBLICOS p on\r\n\t                    p.Id_programa = e.id_programa\r\n\t                    FULL JOIN FICHAS_RELEVAMIENTOS X ON X.CUIT = e.legajo\r\n\t                    FULL JOIN FICHAS_ESTADOS_EVALUACION z ON X.ID_ESTADO = Z.ID\r\n\t                    INNER JOIN USUARIOS_V2 Y ON X.USUARIO_RELEVA=Y.NOMBRE\r\n\t                    INNER JOIN CLASIFICACIONES_PERSONAL J ON E.cod_clasif_per=J.cod_clasif_per\r\n\t\t\t\t\t\tINNER JOIN FICHAS_RELEVAMIENTOS_PERSONAS H ON H.ID_RELEVAMIENTO = X.ID\r\n\t\t\t\t\t\tINNER JOIN FICHAS_RESPUESTAS I ON H.ID_RESPUESTA=I.ID AND I.PUNTUACION <> 0\r\n                        WHERE e.fecha_baja is null AND e.legajo <> 615 AND e.activo=1 AND e.id_direccion=@id_direccion\r\n\t\t\t\t\tGROUP BY \t                    \r\n\t\t\t\t\t\te.legajo, \r\n\t\t\t\t\t\trtrim(ltrim(e.nombre)), \r\n\t                    tl.des_tipo_liq,\r\n\t                    J.des_clasif_per,\r\n\t                    rtrim(ltrim(s.descripcion)), \r\n\t                    rtrim(ltrim(d1.descripcion)),\r\n\t                    ltrim(rtrim(o.nombre_oficina)),\r\n\t                    ltrim(rtrim(p.Programa)),\r\n\t                    --ISNULL(z.NOMBRE, 'Sin Evaluar') AS ESTADO, \r\n\t                    Y.NOMBRE_COMPLETO,\r\n\t\t\t\t\t\tX.ID \r\n                    ORDER BY rtrim(ltrim(e.nombre)) DESC";
                    command.Parameters.AddWithValue("@id_direccion", (object)id_direccion);
                    command.Connection.Open();
                    return Resultado_evaluacion.mapeo(command.ExecuteReader());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Resultado_evaluacion getByPk()
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("SELECT *FROM Resultado_evaluacion WHERE");
                Resultado_evaluacion byPk = (Resultado_evaluacion)null;
                using (SqlConnection connection = DALBase.GetConnection("SIIMVA"))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = stringBuilder.ToString();
                    command.Connection.Open();
                    List<Resultado_evaluacion> resultadoEvaluacionList = Resultado_evaluacion.mapeo(command.ExecuteReader());
                    if (resultadoEvaluacionList.Count != 0)
                        byPk = resultadoEvaluacionList[0];
                }
                return byPk;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
