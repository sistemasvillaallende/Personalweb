using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ConsultaEmpleadoD
    {
        //REVISAR DIRECTORES POR SECRETARIA
        public static List<Entities.LstEmpleados> GetDirectoresBySecretarias
            (int idSecretaria, int ejercicio, int idFicha)
        {
            using (SqlConnection conn = DALBase.GetConnection("Siimva"))
            {
                try
                {
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                        @"SELECT DISTINCT e.legajo, rtrim(ltrim(e.nombre)) as nombre, 
                        convert(varchar(10), e.fecha_ingreso, 103) as fecha_ingreso,
                        convert(varchar(10), e.fecha_nacimiento, 103) as fecha_nacimiento,
                        e.cod_categoria, F.des_categoria, e.tarea, tl.des_tipo_liq,
                        ba.nom_banco, e.nro_caja_ahorro, e.nro_cbu,
                        e.nro_documento, e.nro_cta_sb, e.nro_cta_gastos,
                        rtrim(ltrim(B.descripcion)) as Secretaria, rtrim(ltrim(C.descripcion)) as Direccion,
                        ltrim(rtrim(o.nombre_oficina)) as Oficina,
                        e.celular, e.telefonos, e.email, passTemp, 
                        ISNULL(z.NOMBRE, 'Sin Evaluar') AS NOMBRE_ESTADO, z.ID 
                        AS IdEstadoEvaluacion, A.Id_direccion
                        FROM DIRECCION_X_SECRETARIA A
                        INNER JOIN SECRETARIA B ON A.Id_secretaria=B.Id_secretaria 
                        AND B.activa=1 AND B.ejercicio=@ejercicio
                        INNER JOIN DIRECCION C ON A.Id_direccion=C.Id_direccion AND 
                        C.activa=1 AND C.ejercicio=@ejercicio 
                        INNER JOIN EMPLEADOS E ON E.id_direccion=A.Id_direccion AND E.activo=1
                        AND A.Id_secretaria=B.Id_secretaria 
                        LEFT join oficinas o on
                        o.codigo_oficina = e.id_oficina
                        INNER JOIN CATEGORIAS F ON F.cod_categoria=E.cod_categoria
                        LEFT join TIPOS_LIQUIDACION tl on
                        tl.cod_tipo_liq = e.cod_tipo_liq
                        LEFT join BANCOS ba on
                        ba.cod_banco = e.cod_banco
                        FULL JOIN FICHAS_RELEVAMIENTOS X ON
                        X.CUIT = e.legajo AND X.ID_FICHA=@idFicha
                        FULL JOIN FICHAS_ESTADOS_EVALUACION z ON
                        X.ID_ESTADO = Z.ID
                        WHERE A.Id_secretaria=@Id_secretaria AND 
                        A.ejercicio=@ejercicio AND A.activo=1
                        ORDER BY e.legajo";

                    cmd.Parameters.AddWithValue("@Id_secretaria", idSecretaria);
                    cmd.Parameters.AddWithValue("@ejercicio", ejercicio);
                    cmd.Parameters.AddWithValue("@idFicha", idFicha);

                    cmd.Connection.Open();

                    List<Entities.LstEmpleados> lst = new List<Entities.LstEmpleados>();
                    Entities.LstEmpleados oEmp;

                    try
                    {
                        SqlDataReader dr = cmd.ExecuteReader();


                        if (dr.HasRows)
                        {
                            int legajo = dr.GetOrdinal("legajo");
                            int nombre = dr.GetOrdinal("nombre");
                            int fecha_ingreso = dr.GetOrdinal("fecha_ingreso");
                            int fecha_nac = dr.GetOrdinal("fecha_nacimiento");
                            int cod_categoria = dr.GetOrdinal("cod_categoria");
                            int des_categoria = dr.GetOrdinal("des_categoria");
                            int tarea = dr.GetOrdinal("tarea");
                            int des_tipo_liq = dr.GetOrdinal("des_tipo_liq");
                            int nom_banco = dr.GetOrdinal("nom_banco");
                            int nro_caja_ahorro = dr.GetOrdinal("nro_caja_ahorro");
                            int nro_cbu = dr.GetOrdinal("nro_cbu");
                            int nro_documento = dr.GetOrdinal("nro_documento");
                            int nro_cta_sb = dr.GetOrdinal("nro_cta_sb");

                            int nro_cta_gastos = dr.GetOrdinal("nro_cta_gastos");
                            int secretaria = dr.GetOrdinal("secretaria");
                            int direccion = dr.GetOrdinal("direccion");
                            int oficina = dr.GetOrdinal("oficina");

                            int celular = dr.GetOrdinal("celular");
                            int telefonos = dr.GetOrdinal("telefonos");
                            int email = dr.GetOrdinal("email");
                            int passTemp = dr.GetOrdinal("passTemp");
                            int estadoEvaluacion = dr.GetOrdinal("NOMBRE_ESTADO");
                            int IdEstadoEvaluacion = dr.GetOrdinal("IdEstadoEvaluacion");
                            int id_direccion = dr.GetOrdinal("id_direccion");

                            while (dr.Read())
                            {
                                oEmp = new Entities.LstEmpleados();

                                if (!dr.IsDBNull(legajo)) oEmp.legajo = dr.GetInt32(legajo);
                                if (!dr.IsDBNull(nombre)) oEmp.nombre = dr.GetString(nombre);
                                if (!dr.IsDBNull(fecha_ingreso)) oEmp.fecha_ingreso = dr.GetString(fecha_ingreso);
                                if (!dr.IsDBNull(nro_documento)) oEmp.nro_documento = dr.GetString(nro_documento);
                                if (!dr.IsDBNull(fecha_nac)) oEmp.fecha_nacimiento = dr.GetString(fecha_nac);
                                if (!dr.IsDBNull(cod_categoria)) oEmp.cod_categoria = dr.GetInt32(cod_categoria);
                                if (!dr.IsDBNull(des_categoria)) oEmp.des_categoria = dr.GetString(des_categoria);
                                if (!dr.IsDBNull(tarea)) oEmp.tarea = dr.GetString(tarea);
                                if (!dr.IsDBNull(des_tipo_liq)) oEmp.des_tipo_liq = dr.GetString(des_tipo_liq);
                                if (!dr.IsDBNull(nom_banco)) oEmp.nom_banco = dr.GetString(nom_banco);
                                if (!dr.IsDBNull(nro_caja_ahorro)) oEmp.nro_caja_ahorro = dr.GetString(nro_caja_ahorro);
                                if (!dr.IsDBNull(nro_cbu)) oEmp.nro_cbu = dr.GetString(nro_cbu);
                                if (!dr.IsDBNull(nro_documento)) oEmp.nro_documento = dr.GetString(nro_documento);
                                if (!dr.IsDBNull(nro_cta_sb)) oEmp.nro_cta_sb = dr.GetString(nro_cta_sb);
                                if (!dr.IsDBNull(nro_cta_gastos)) oEmp.nro_cta_gastos = dr.GetString(nro_cta_gastos);
                                if (!dr.IsDBNull(secretaria)) oEmp.secrectaria = dr.GetString(secretaria);
                                if (!dr.IsDBNull(direccion)) oEmp.direccion = dr.GetString(direccion);
                                if (!dr.IsDBNull(oficina)) oEmp.oficina = dr.GetString(oficina);
                                if (!dr.IsDBNull(celular)) oEmp.celular = dr.GetString(celular);
                                if (!dr.IsDBNull(telefonos)) oEmp.telefonos = dr.GetString(telefonos);
                                if (!dr.IsDBNull(email)) oEmp.email = dr.GetString(email);
                                if (!dr.IsDBNull(passTemp)) oEmp.passTemp = dr.GetString(passTemp);
                                if (!dr.IsDBNull(estadoEvaluacion)) oEmp.estadoEvaluacion = dr.GetString(estadoEvaluacion);
                                if (!dr.IsDBNull(IdEstadoEvaluacion)) oEmp.idEstadoEvaluacion = dr.GetInt32(IdEstadoEvaluacion);
                                if (!dr.IsDBNull(id_direccion)) oEmp.id_direccion = dr.GetInt32(id_direccion);

                                lst.Add(oEmp);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error in query!" + e.ToString());
                        throw e;
                    }
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }


        }
        //REVISAR DIRECTORES POR SECRETARIA
        public static List<Entities.LstEmpleados> GetEmpleadosDirectosBySecretarias
            (int idSecretaria, int ejercicio)
        {
            using (SqlConnection conn = DALBase.GetConnection("Siimva"))
            {
                try
                {
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                        @"SELECT DISTINCT e.legajo, rtrim(ltrim(e.nombre)) as nombre, 
                        convert(varchar(10), e.fecha_ingreso, 103) as fecha_ingreso,
                        convert(varchar(10), e.fecha_nacimiento, 103) as fecha_nacimiento,
                        e.cod_categoria, F.des_categoria, e.tarea, tl.des_tipo_liq,
                        ba.nom_banco, e.nro_caja_ahorro, e.nro_cbu,
                        e.nro_documento, e.nro_cta_sb, e.nro_cta_gastos,
                        rtrim(ltrim(SEC.descripcion)) as Secretaria, rtrim(ltrim(dir.descripcion)) as Direccion,
                        ltrim(rtrim(o.nombre_oficina)) as Oficina,
                        e.celular, e.telefonos, e.email, passTemp, 
                        ISNULL(z.NOMBRE, 'Sin Evaluar') AS NOMBRE_ESTADO, z.ID 
                        AS IdEstadoEvaluacion
                        FROM EMPLEADOS E 
                        INNER JOIN PROGRAMAS_PUBLICOS A ON A.Id_programa=E.id_programa
                        INNER JOIN SECRETARIA SEC ON E.id_secretaria=SEC.Id_secretaria
                        AND SEC.ejercicio=@ejercicio AND SEC.activa=1
                        INNER JOIN DIRECCION dir ON dir.Id_direccion=E.id_direccion
                        LEFT join oficinas o on
                        o.codigo_oficina = e.id_oficina
                        INNER JOIN CATEGORIAS F ON F.cod_categoria=E.cod_categoria
                        LEFT join TIPOS_LIQUIDACION tl on
                        tl.cod_tipo_liq = e.cod_tipo_liq
                        LEFT join BANCOS ba on
                        ba.cod_banco = e.cod_banco
                        FULL JOIN FICHAS_RELEVAMIENTOS X ON
                        X.CUIT = e.legajo
                        FULL JOIN FICHAS_ESTADOS_EVALUACION z ON
                        X.ID_ESTADO = Z.ID
                        WHERE E.id_direccion IN(
	                        SELECT DISTINCT C.Id_direccion
	                        FROM DIRECCION_X_SECRETARIA A
	                        INNER JOIN SECRETARIA B ON A.Id_secretaria=B.Id_secretaria 
	                        AND B.activa=1 AND B.ejercicio=@ejercicio
	                        INNER JOIN DIRECCION C ON A.Id_direccion=C.Id_direccion AND 
	                        C.activa=1 AND C.ejercicio=@ejercicio AND B.cod_ceco=C.cod_subceco
	                        WHERE A.Id_secretaria=@Id_secretaria AND A.ejercicio=@ejercicio AND A.activo=1)
                        AND CONVERT(int, SEC.cod_ceco) <> E.legajo AND e.activo=1
                        ORDER BY nombre";

                    cmd.Parameters.AddWithValue("@ejercicio", ejercicio);
                    cmd.Parameters.AddWithValue("@Id_secretaria", idSecretaria);
                    cmd.Connection.Open();

                    List<Entities.LstEmpleados> lst = new List<Entities.LstEmpleados>();
                    Entities.LstEmpleados oEmp;

                    try
                    {
                        SqlDataReader dr = cmd.ExecuteReader();


                        if (dr.HasRows)
                        {
                            int legajo = dr.GetOrdinal("legajo");
                            int nombre = dr.GetOrdinal("nombre");
                            int fecha_ingreso = dr.GetOrdinal("fecha_ingreso");
                            int fecha_nac = dr.GetOrdinal("fecha_nacimiento");
                            int cod_categoria = dr.GetOrdinal("cod_categoria");
                            int des_categoria = dr.GetOrdinal("des_categoria");
                            int tarea = dr.GetOrdinal("tarea");
                            int des_tipo_liq = dr.GetOrdinal("des_tipo_liq");
                            int nom_banco = dr.GetOrdinal("nom_banco");
                            int nro_caja_ahorro = dr.GetOrdinal("nro_caja_ahorro");
                            int nro_cbu = dr.GetOrdinal("nro_cbu");
                            int nro_documento = dr.GetOrdinal("nro_documento");
                            int nro_cta_sb = dr.GetOrdinal("nro_cta_sb");

                            int nro_cta_gastos = dr.GetOrdinal("nro_cta_gastos");
                            int secretaria = dr.GetOrdinal("secretaria");
                            int direccion = dr.GetOrdinal("direccion");
                            int oficina = dr.GetOrdinal("oficina");

                            int celular = dr.GetOrdinal("celular");
                            int telefonos = dr.GetOrdinal("telefonos");
                            int email = dr.GetOrdinal("email");
                            int passTemp = dr.GetOrdinal("passTemp");
                            int estadoEvaluacion = dr.GetOrdinal("NOMBRE_ESTADO");
                            int IdEstadoEvaluacion = dr.GetOrdinal("IdEstadoEvaluacion");
                            while (dr.Read())
                            {
                                oEmp = new Entities.LstEmpleados();

                                if (!dr.IsDBNull(legajo)) oEmp.legajo = dr.GetInt32(legajo);
                                if (!dr.IsDBNull(nombre)) oEmp.nombre = dr.GetString(nombre);
                                if (!dr.IsDBNull(fecha_ingreso)) oEmp.fecha_ingreso = dr.GetString(fecha_ingreso);
                                if (!dr.IsDBNull(nro_documento)) oEmp.nro_documento = dr.GetString(nro_documento);
                                if (!dr.IsDBNull(fecha_nac)) oEmp.fecha_nacimiento = dr.GetString(fecha_nac);
                                if (!dr.IsDBNull(cod_categoria)) oEmp.cod_categoria = dr.GetInt32(cod_categoria);
                                if (!dr.IsDBNull(des_categoria)) oEmp.des_categoria = dr.GetString(des_categoria);
                                if (!dr.IsDBNull(tarea)) oEmp.tarea = dr.GetString(tarea);
                                if (!dr.IsDBNull(des_tipo_liq)) oEmp.des_tipo_liq = dr.GetString(des_tipo_liq);
                                if (!dr.IsDBNull(nom_banco)) oEmp.nom_banco = dr.GetString(nom_banco);
                                if (!dr.IsDBNull(nro_caja_ahorro)) oEmp.nro_caja_ahorro = dr.GetString(nro_caja_ahorro);
                                if (!dr.IsDBNull(nro_cbu)) oEmp.nro_cbu = dr.GetString(nro_cbu);
                                if (!dr.IsDBNull(nro_documento)) oEmp.nro_documento = dr.GetString(nro_documento);
                                if (!dr.IsDBNull(nro_cta_sb)) oEmp.nro_cta_sb = dr.GetString(nro_cta_sb);
                                if (!dr.IsDBNull(nro_cta_gastos)) oEmp.nro_cta_gastos = dr.GetString(nro_cta_gastos);
                                if (!dr.IsDBNull(secretaria)) oEmp.secrectaria = dr.GetString(secretaria);
                                if (!dr.IsDBNull(direccion)) oEmp.direccion = dr.GetString(direccion);
                                if (!dr.IsDBNull(oficina)) oEmp.oficina = dr.GetString(oficina);
                                if (!dr.IsDBNull(celular)) oEmp.celular = dr.GetString(celular);
                                if (!dr.IsDBNull(telefonos)) oEmp.telefonos = dr.GetString(telefonos);
                                if (!dr.IsDBNull(email)) oEmp.email = dr.GetString(email);
                                if (!dr.IsDBNull(passTemp)) oEmp.passTemp = dr.GetString(passTemp);
                                if (!dr.IsDBNull(estadoEvaluacion)) oEmp.estadoEvaluacion = dr.GetString(estadoEvaluacion);
                                if (!dr.IsDBNull(IdEstadoEvaluacion)) oEmp.idEstadoEvaluacion = dr.GetInt32(IdEstadoEvaluacion);

                                lst.Add(oEmp);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error in query!" + e.ToString());
                        throw e;
                    }
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }


        }

        public static List<Entities.LstEmpleados> GetEmpleadosByEvaluadorDirectos
    (int leg, int ejercicio)
        {
            using (SqlConnection conn = DALBase.GetConnection("Siimva"))
            {
                try
                {
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                    @"SELECT DISTINCT e.legajo, evaluadores, rtrim(ltrim(e.nombre)) as nombre, 
                        convert(varchar(10), e.fecha_ingreso, 103) as fecha_ingreso,
                        convert(varchar(10), e.fecha_nacimiento, 103) as fecha_nacimiento,
                        e.cod_categoria, F.des_categoria, e.tarea, tl.des_tipo_liq,
                        ba.nom_banco, e.nro_caja_ahorro, e.nro_cbu,
                        e.nro_documento, e.nro_cta_sb, e.nro_cta_gastos,
                        rtrim(ltrim(SEC.descripcion)) as Secretaria, rtrim(ltrim(dir.descripcion)) as Direccion,
                        ltrim(rtrim(o.nombre_oficina)) as Oficina,
                        e.celular, e.telefonos, e.email, passTemp, 
                        ISNULL(z.NOMBRE, 'Sin Evaluar') AS NOMBRE_ESTADO, z.ID 
                        AS IdEstadoEvaluacion
                    FROM EMPLEADOS E 
                        FULL JOIN PROGRAMAS_PUBLICOS A ON A.Id_programa=E.id_programa
                        FULL JOIN SECRETARIA SEC ON E.id_secretaria=SEC.Id_secretaria
                        AND SEC.ejercicio=@ejercicio AND SEC.activa=1
                        FULL JOIN DIRECCION dir ON dir.Id_direccion=E.id_direccion
                        FULL join oficinas o on
                        o.codigo_oficina = e.id_oficina
                        FULL JOIN CATEGORIAS F ON F.cod_categoria=E.cod_categoria
                        LEFT join TIPOS_LIQUIDACION tl on
                        tl.cod_tipo_liq = e.cod_tipo_liq
                        LEFT join BANCOS ba on
                        ba.cod_banco = e.cod_banco
                        FULL JOIN FICHAS_RELEVAMIENTOS X ON
                        X.CUIT = e.legajo
                        FULL JOIN FICHAS_ESTADOS_EVALUACION z ON
                        X.ID_ESTADO = Z.ID
                    WHERE E.id_direccion IN(
                        SELECT DISTINCT C.Id_direccion
                        FROM DIRECCION_X_SECRETARIA A
                        INNER JOIN SECRETARIA B ON A.Id_secretaria=B.Id_secretaria 
                        AND B.activa=1 AND B.ejercicio=@ejercicio
                        INNER JOIN DIRECCION C ON A.Id_direccion=C.Id_direccion AND 
                        C.activa=1 AND C.ejercicio=@ejercicio 
                        WHERE A.ejercicio=@ejercicio AND A.activo=1)
                        AND e.activo=1 AND EXISTS (
                        SELECT 1
                        FROM OPENJSON(Evaluadores, '$.evaluadores') AS jsonData
                        WHERE jsonData.value = CAST(@leg AS NVARCHAR(10)))
                        AND legajo NOT IN(SELECT DISTINCT Evaluadores.value AS Evaluador
                        FROM empleados
                        CROSS APPLY OPENJSON(Evaluadores, '$.evaluadores') AS Evaluadores)
                        ORDER BY nombre";

                    cmd.Parameters.AddWithValue("@ejercicio", ejercicio);
                    cmd.Parameters.AddWithValue("@leg", leg);
                    cmd.Connection.Open();

                    List<Entities.LstEmpleados> lst = new List<Entities.LstEmpleados>();
                    Entities.LstEmpleados oEmp;

                    try
                    {
                        SqlDataReader dr = cmd.ExecuteReader();


                        if (dr.HasRows)
                        {
                            int legajo = dr.GetOrdinal("legajo");
                            int nombre = dr.GetOrdinal("nombre");
                            int fecha_ingreso = dr.GetOrdinal("fecha_ingreso");
                            int fecha_nac = dr.GetOrdinal("fecha_nacimiento");
                            int cod_categoria = dr.GetOrdinal("cod_categoria");
                            int des_categoria = dr.GetOrdinal("des_categoria");
                            int tarea = dr.GetOrdinal("tarea");
                            int des_tipo_liq = dr.GetOrdinal("des_tipo_liq");
                            int nom_banco = dr.GetOrdinal("nom_banco");
                            int nro_caja_ahorro = dr.GetOrdinal("nro_caja_ahorro");
                            int nro_cbu = dr.GetOrdinal("nro_cbu");
                            int nro_documento = dr.GetOrdinal("nro_documento");
                            int nro_cta_sb = dr.GetOrdinal("nro_cta_sb");

                            int nro_cta_gastos = dr.GetOrdinal("nro_cta_gastos");
                            int secretaria = dr.GetOrdinal("secretaria");
                            int direccion = dr.GetOrdinal("direccion");
                            int oficina = dr.GetOrdinal("oficina");

                            int celular = dr.GetOrdinal("celular");
                            int telefonos = dr.GetOrdinal("telefonos");
                            int email = dr.GetOrdinal("email");
                            int passTemp = dr.GetOrdinal("passTemp");
                            int estadoEvaluacion = dr.GetOrdinal("NOMBRE_ESTADO");
                            int IdEstadoEvaluacion = dr.GetOrdinal("IdEstadoEvaluacion");
                            while (dr.Read())
                            {
                                oEmp = new Entities.LstEmpleados();

                                if (!dr.IsDBNull(legajo)) oEmp.legajo = dr.GetInt32(legajo);
                                if (!dr.IsDBNull(nombre)) oEmp.nombre = dr.GetString(nombre);
                                if (!dr.IsDBNull(fecha_ingreso)) oEmp.fecha_ingreso = dr.GetString(fecha_ingreso);
                                if (!dr.IsDBNull(nro_documento)) oEmp.nro_documento = dr.GetString(nro_documento);
                                if (!dr.IsDBNull(fecha_nac)) oEmp.fecha_nacimiento = dr.GetString(fecha_nac);
                                if (!dr.IsDBNull(cod_categoria)) oEmp.cod_categoria = dr.GetInt32(cod_categoria);
                                if (!dr.IsDBNull(des_categoria)) oEmp.des_categoria = dr.GetString(des_categoria);
                                if (!dr.IsDBNull(tarea)) oEmp.tarea = dr.GetString(tarea);
                                if (!dr.IsDBNull(des_tipo_liq)) oEmp.des_tipo_liq = dr.GetString(des_tipo_liq);
                                if (!dr.IsDBNull(nom_banco)) oEmp.nom_banco = dr.GetString(nom_banco);
                                if (!dr.IsDBNull(nro_caja_ahorro)) oEmp.nro_caja_ahorro = dr.GetString(nro_caja_ahorro);
                                if (!dr.IsDBNull(nro_cbu)) oEmp.nro_cbu = dr.GetString(nro_cbu);
                                if (!dr.IsDBNull(nro_documento)) oEmp.nro_documento = dr.GetString(nro_documento);
                                if (!dr.IsDBNull(nro_cta_sb)) oEmp.nro_cta_sb = dr.GetString(nro_cta_sb);
                                if (!dr.IsDBNull(nro_cta_gastos)) oEmp.nro_cta_gastos = dr.GetString(nro_cta_gastos);
                                if (!dr.IsDBNull(secretaria)) oEmp.secrectaria = dr.GetString(secretaria);
                                if (!dr.IsDBNull(direccion)) oEmp.direccion = dr.GetString(direccion);
                                if (!dr.IsDBNull(oficina)) oEmp.oficina = dr.GetString(oficina);
                                if (!dr.IsDBNull(celular)) oEmp.celular = dr.GetString(celular);
                                if (!dr.IsDBNull(telefonos)) oEmp.telefonos = dr.GetString(telefonos);
                                if (!dr.IsDBNull(email)) oEmp.email = dr.GetString(email);
                                if (!dr.IsDBNull(passTemp)) oEmp.passTemp = dr.GetString(passTemp);
                                if (!dr.IsDBNull(estadoEvaluacion)) oEmp.estadoEvaluacion = dr.GetString(estadoEvaluacion);
                                if (!dr.IsDBNull(IdEstadoEvaluacion)) oEmp.idEstadoEvaluacion = dr.GetInt32(IdEstadoEvaluacion);

                                lst.Add(oEmp);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error in query!" + e.ToString());
                        throw e;
                    }
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public static List<Entities.LstEmpleados> GetEmpleadosByEvaluadorEvaluadores
   (int leg, int ejercicio)
        {
            using (SqlConnection conn = DALBase.GetConnection("Siimva"))
            {
                try
                {
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                        @"SELECT DISTINCT e.legajo, evaluadores, rtrim(ltrim(e.nombre)) as nombre, 
	                        convert(varchar(10), e.fecha_ingreso, 103) as fecha_ingreso,
	                        convert(varchar(10), e.fecha_nacimiento, 103) as fecha_nacimiento,
	                        e.cod_categoria, F.des_categoria, e.tarea, tl.des_tipo_liq,
	                        ba.nom_banco, e.nro_caja_ahorro, e.nro_cbu,
	                        e.nro_documento, e.nro_cta_sb, e.nro_cta_gastos,
	                        rtrim(ltrim(SEC.descripcion)) as Secretaria, rtrim(ltrim(dir.descripcion)) as Direccion,
	                        ltrim(rtrim(o.nombre_oficina)) as Oficina,
	                        e.celular, e.telefonos, e.email, passTemp, 
	                        ISNULL(z.NOMBRE, 'Sin Evaluar') AS NOMBRE_ESTADO, z.ID 
	                        AS IdEstadoEvaluacion
                        FROM EMPLEADOS E 
	                        INNER JOIN PROGRAMAS_PUBLICOS A ON A.Id_programa=E.id_programa
	                        INNER JOIN SECRETARIA SEC ON E.id_secretaria=SEC.Id_secretaria
	                        AND SEC.ejercicio=@ejercicio AND SEC.activa=1
	                        INNER JOIN DIRECCION dir ON dir.Id_direccion=E.id_direccion
	                        LEFT join oficinas o on
	                        o.codigo_oficina = e.id_oficina
	                        INNER JOIN CATEGORIAS F ON F.cod_categoria=E.cod_categoria
	                        LEFT join TIPOS_LIQUIDACION tl on
	                        tl.cod_tipo_liq = e.cod_tipo_liq
	                        LEFT join BANCOS ba on
	                        ba.cod_banco = e.cod_banco
	                        FULL JOIN FICHAS_RELEVAMIENTOS X ON
	                        X.CUIT = e.legajo
	                        FULL JOIN FICHAS_ESTADOS_EVALUACION z ON
	                        X.ID_ESTADO = Z.ID
                        WHERE E.id_direccion IN(
	                        SELECT DISTINCT C.Id_direccion
	                        FROM DIRECCION_X_SECRETARIA A
	                        INNER JOIN SECRETARIA B ON A.Id_secretaria=B.Id_secretaria 
	                        AND B.activa=1 AND B.ejercicio=@ejercicio
	                        INNER JOIN DIRECCION C ON A.Id_direccion=C.Id_direccion AND 
	                        C.activa=1 AND C.ejercicio=@ejercicio 
	                        WHERE A.ejercicio=@ejercicio AND A.activo=1)
	                        AND e.activo=1 AND EXISTS (
                            SELECT 1
                            FROM OPENJSON(Evaluadores, '$.evaluadores') AS jsonData
                            WHERE jsonData.value = CAST(@leg AS NVARCHAR(10)))
	                        AND legajo IN (SELECT DISTINCT Evaluadores.value AS Evaluador
                        FROM empleados
                        CROSS APPLY OPENJSON(Evaluadores, '$.evaluadores') AS Evaluadores)
                        ORDER BY nombre";

                    cmd.Parameters.AddWithValue("@ejercicio", ejercicio);
                    cmd.Parameters.AddWithValue("@leg", leg);
                    cmd.Connection.Open();

                    List<Entities.LstEmpleados> lst = new List<Entities.LstEmpleados>();
                    Entities.LstEmpleados oEmp;

                    try
                    {
                        SqlDataReader dr = cmd.ExecuteReader();


                        if (dr.HasRows)
                        {
                            int legajo = dr.GetOrdinal("legajo");
                            int nombre = dr.GetOrdinal("nombre");
                            int fecha_ingreso = dr.GetOrdinal("fecha_ingreso");
                            int fecha_nac = dr.GetOrdinal("fecha_nacimiento");
                            int cod_categoria = dr.GetOrdinal("cod_categoria");
                            int des_categoria = dr.GetOrdinal("des_categoria");
                            int tarea = dr.GetOrdinal("tarea");
                            int des_tipo_liq = dr.GetOrdinal("des_tipo_liq");
                            int nom_banco = dr.GetOrdinal("nom_banco");
                            int nro_caja_ahorro = dr.GetOrdinal("nro_caja_ahorro");
                            int nro_cbu = dr.GetOrdinal("nro_cbu");
                            int nro_documento = dr.GetOrdinal("nro_documento");
                            int nro_cta_sb = dr.GetOrdinal("nro_cta_sb");

                            int nro_cta_gastos = dr.GetOrdinal("nro_cta_gastos");
                            int secretaria = dr.GetOrdinal("secretaria");
                            int direccion = dr.GetOrdinal("direccion");
                            int oficina = dr.GetOrdinal("oficina");

                            int celular = dr.GetOrdinal("celular");
                            int telefonos = dr.GetOrdinal("telefonos");
                            int email = dr.GetOrdinal("email");
                            int passTemp = dr.GetOrdinal("passTemp");
                            int estadoEvaluacion = dr.GetOrdinal("NOMBRE_ESTADO");
                            int IdEstadoEvaluacion = dr.GetOrdinal("IdEstadoEvaluacion");
                            while (dr.Read())
                            {
                                oEmp = new Entities.LstEmpleados();

                                if (!dr.IsDBNull(legajo)) oEmp.legajo = dr.GetInt32(legajo);
                                if (!dr.IsDBNull(nombre)) oEmp.nombre = dr.GetString(nombre);
                                if (!dr.IsDBNull(fecha_ingreso)) oEmp.fecha_ingreso = dr.GetString(fecha_ingreso);
                                if (!dr.IsDBNull(nro_documento)) oEmp.nro_documento = dr.GetString(nro_documento);
                                if (!dr.IsDBNull(fecha_nac)) oEmp.fecha_nacimiento = dr.GetString(fecha_nac);
                                if (!dr.IsDBNull(cod_categoria)) oEmp.cod_categoria = dr.GetInt32(cod_categoria);
                                if (!dr.IsDBNull(des_categoria)) oEmp.des_categoria = dr.GetString(des_categoria);
                                if (!dr.IsDBNull(tarea)) oEmp.tarea = dr.GetString(tarea);
                                if (!dr.IsDBNull(des_tipo_liq)) oEmp.des_tipo_liq = dr.GetString(des_tipo_liq);
                                if (!dr.IsDBNull(nom_banco)) oEmp.nom_banco = dr.GetString(nom_banco);
                                if (!dr.IsDBNull(nro_caja_ahorro)) oEmp.nro_caja_ahorro = dr.GetString(nro_caja_ahorro);
                                if (!dr.IsDBNull(nro_cbu)) oEmp.nro_cbu = dr.GetString(nro_cbu);
                                if (!dr.IsDBNull(nro_documento)) oEmp.nro_documento = dr.GetString(nro_documento);
                                if (!dr.IsDBNull(nro_cta_sb)) oEmp.nro_cta_sb = dr.GetString(nro_cta_sb);
                                if (!dr.IsDBNull(nro_cta_gastos)) oEmp.nro_cta_gastos = dr.GetString(nro_cta_gastos);
                                if (!dr.IsDBNull(secretaria)) oEmp.secrectaria = dr.GetString(secretaria);
                                if (!dr.IsDBNull(direccion)) oEmp.direccion = dr.GetString(direccion);
                                if (!dr.IsDBNull(oficina)) oEmp.oficina = dr.GetString(oficina);
                                if (!dr.IsDBNull(celular)) oEmp.celular = dr.GetString(celular);
                                if (!dr.IsDBNull(telefonos)) oEmp.telefonos = dr.GetString(telefonos);
                                if (!dr.IsDBNull(email)) oEmp.email = dr.GetString(email);
                                if (!dr.IsDBNull(passTemp)) oEmp.passTemp = dr.GetString(passTemp);
                                if (!dr.IsDBNull(estadoEvaluacion)) oEmp.estadoEvaluacion = dr.GetString(estadoEvaluacion);
                                if (!dr.IsDBNull(IdEstadoEvaluacion)) oEmp.idEstadoEvaluacion = dr.GetInt32(IdEstadoEvaluacion);

                                lst.Add(oEmp);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error in query!" + e.ToString());
                        throw e;
                    }
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public static List<Entities.LstEmpleados> GetEmpleadosByDireccion(
            int idDireccion, int idFicha)
        {
            using (SqlConnection conn = DALBase.GetConnection("Siimva"))
            {
                try
                {
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"SELECT
                        e.legajo, rtrim(ltrim(e.nombre)) as nombre, 
                        convert(varchar(10), e.fecha_ingreso, 103) as fecha_ingreso,
                        convert(varchar(10), e.fecha_nacimiento, 103) as fecha_nacimiento,
                        e.cod_categoria, c.des_categoria, e.tarea, tl.des_tipo_liq,
                        b.nom_banco, e.nro_caja_ahorro, e.nro_cbu,
                        e.nro_documento, e.nro_cta_sb, e.nro_cta_gastos,
                        rtrim(ltrim(s.descripcion)) as Secretaria, rtrim(ltrim(d1.descripcion)) as Direccion,
                        ltrim(rtrim(o.nombre_oficina)) as Oficina,
                        e.celular, e.telefonos, e.email, passTemp, 
                        ISNULL(z.NOMBRE, 'Sin Evaluar') AS NOMBRE_ESTADO, z.ID 
                        AS IdEstadoEvaluacion
                        FROM EMPLEADOS e
                        LEFT join TIPOS_LIQUIDACION tl on
                        tl.cod_tipo_liq = e.cod_tipo_liq
                        LEFT join BANCOS b on
                        b.cod_banco = e.cod_banco
                        LEFT join CATEGORIAS c on
                        e.cod_categoria = c.cod_categoria
                        LEFT join secretaria s on
                        s.id_secretaria = e.id_secretaria
                        LEFT join direccion d1 on
                        d1.id_direccion = e.id_direccion
                        LEFT join oficinas o on
                        o.codigo_oficina = e.id_oficina
                        FULL JOIN FICHAS_RELEVAMIENTOS X ON
                        X.CUIT = e.legajo AND X.ID_FICHA=@ID_FICHA
                        FULL JOIN FICHAS_ESTADOS_EVALUACION z ON
                        X.ID_ESTADO = Z.ID
                        WHERE e.fecha_baja is null AND
                        e.id_direccion=@id_direccion AND legajo <> 615 AND e.activo=1
                        ORDER BY e.legajo";

                    cmd.Parameters.AddWithValue("@id_direccion", idDireccion);
                    cmd.Parameters.AddWithValue("@ID_FICHA", idFicha);
                    cmd.Connection.Open();

                    List<Entities.LstEmpleados> lst = new List<Entities.LstEmpleados>();
                    Entities.LstEmpleados oEmp;

                    try
                    {
                        SqlDataReader dr = cmd.ExecuteReader();


                        if (dr.HasRows)
                        {
                            int legajo = dr.GetOrdinal("legajo");
                            int nombre = dr.GetOrdinal("nombre");
                            int fecha_ingreso = dr.GetOrdinal("fecha_ingreso");
                            int fecha_nac = dr.GetOrdinal("fecha_nacimiento");
                            int cod_categoria = dr.GetOrdinal("cod_categoria");
                            int des_categoria = dr.GetOrdinal("des_categoria");
                            int tarea = dr.GetOrdinal("tarea");
                            int des_tipo_liq = dr.GetOrdinal("des_tipo_liq");
                            int nom_banco = dr.GetOrdinal("nom_banco");
                            int nro_caja_ahorro = dr.GetOrdinal("nro_caja_ahorro");
                            int nro_cbu = dr.GetOrdinal("nro_cbu");
                            int nro_documento = dr.GetOrdinal("nro_documento");
                            int nro_cta_sb = dr.GetOrdinal("nro_cta_sb");

                            int nro_cta_gastos = dr.GetOrdinal("nro_cta_gastos");
                            int secretaria = dr.GetOrdinal("secretaria");
                            int direccion = dr.GetOrdinal("direccion");
                            int oficina = dr.GetOrdinal("oficina");

                            int celular = dr.GetOrdinal("celular");
                            int telefonos = dr.GetOrdinal("telefonos");
                            int email = dr.GetOrdinal("email");
                            int passTemp = dr.GetOrdinal("passTemp");
                            int estadoEvaluacion = dr.GetOrdinal("NOMBRE_ESTADO");
                            int IdEstadoEvaluacion = dr.GetOrdinal("IdEstadoEvaluacion");
                            while (dr.Read())
                            {
                                oEmp = new Entities.LstEmpleados();

                                if (!dr.IsDBNull(legajo)) oEmp.legajo = dr.GetInt32(legajo);
                                if (!dr.IsDBNull(nombre)) oEmp.nombre = dr.GetString(nombre);
                                if (!dr.IsDBNull(fecha_ingreso)) oEmp.fecha_ingreso = dr.GetString(fecha_ingreso);
                                if (!dr.IsDBNull(nro_documento)) oEmp.nro_documento = dr.GetString(nro_documento);
                                if (!dr.IsDBNull(fecha_nac)) oEmp.fecha_nacimiento = dr.GetString(fecha_nac);
                                if (!dr.IsDBNull(cod_categoria)) oEmp.cod_categoria = dr.GetInt32(cod_categoria);
                                if (!dr.IsDBNull(des_categoria)) oEmp.des_categoria = dr.GetString(des_categoria);
                                if (!dr.IsDBNull(tarea)) oEmp.tarea = dr.GetString(tarea);
                                if (!dr.IsDBNull(des_tipo_liq)) oEmp.des_tipo_liq = dr.GetString(des_tipo_liq);
                                if (!dr.IsDBNull(nom_banco)) oEmp.nom_banco = dr.GetString(nom_banco);
                                if (!dr.IsDBNull(nro_caja_ahorro)) oEmp.nro_caja_ahorro = dr.GetString(nro_caja_ahorro);
                                if (!dr.IsDBNull(nro_cbu)) oEmp.nro_cbu = dr.GetString(nro_cbu);
                                if (!dr.IsDBNull(nro_documento)) oEmp.nro_documento = dr.GetString(nro_documento);
                                if (!dr.IsDBNull(nro_cta_sb)) oEmp.nro_cta_sb = dr.GetString(nro_cta_sb);
                                if (!dr.IsDBNull(nro_cta_gastos)) oEmp.nro_cta_gastos = dr.GetString(nro_cta_gastos);
                                if (!dr.IsDBNull(secretaria)) oEmp.secrectaria = dr.GetString(secretaria);
                                if (!dr.IsDBNull(direccion)) oEmp.direccion = dr.GetString(direccion);
                                if (!dr.IsDBNull(oficina)) oEmp.oficina = dr.GetString(oficina);
                                if (!dr.IsDBNull(celular)) oEmp.celular = dr.GetString(celular);
                                if (!dr.IsDBNull(telefonos)) oEmp.telefonos = dr.GetString(telefonos);
                                if (!dr.IsDBNull(email)) oEmp.email = dr.GetString(email);
                                if (!dr.IsDBNull(passTemp)) oEmp.passTemp = dr.GetString(passTemp);
                                if (!dr.IsDBNull(estadoEvaluacion)) oEmp.estadoEvaluacion = dr.GetString(estadoEvaluacion);
                                if (!dr.IsDBNull(IdEstadoEvaluacion)) oEmp.idEstadoEvaluacion = dr.GetInt32(IdEstadoEvaluacion);

                                lst.Add(oEmp);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error in query!" + e.ToString());
                        throw e;
                    }
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }


        }
        public static List<Entities.LstEmpleados> GetEmpleadosEvaluados()
        {
            using (SqlConnection conn = DALBase.GetConnection("Siimva"))
            {
                try
                {
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = @"getEmpleadosEvaluar";
                    cmd.Connection.Open();

                    List<Entities.LstEmpleados> lst = new List<Entities.LstEmpleados>();
                    Entities.LstEmpleados oEmp;

                    try
                    {
                        SqlDataReader dr = cmd.ExecuteReader();


                        if (dr.HasRows)
                        {
                            int legajo = dr.GetOrdinal("legajo");
                            int nombre = dr.GetOrdinal("nombre");
                            int fecha_ingreso = dr.GetOrdinal("fecha_ingreso");
                            int fecha_nac = dr.GetOrdinal("fecha_nacimiento");
                            int cod_categoria = dr.GetOrdinal("cod_categoria");
                            int des_categoria = dr.GetOrdinal("des_categoria");
                            int tarea = dr.GetOrdinal("tarea");
                            int des_tipo_liq = dr.GetOrdinal("des_tipo_liq");
                            int nom_banco = dr.GetOrdinal("nom_banco");
                            int nro_caja_ahorro = dr.GetOrdinal("nro_caja_ahorro");
                            int nro_cbu = dr.GetOrdinal("nro_cbu");
                            int nro_documento = dr.GetOrdinal("nro_documento");
                            int nro_cta_sb = dr.GetOrdinal("nro_cta_sb");

                            int nro_cta_gastos = dr.GetOrdinal("nro_cta_gastos");
                            int secretaria = dr.GetOrdinal("secretaria");
                            int direccion = dr.GetOrdinal("direccion");
                            int oficina = dr.GetOrdinal("oficina");

                            int celular = dr.GetOrdinal("celular");
                            int telefonos = dr.GetOrdinal("telefonos");
                            int email = dr.GetOrdinal("email");
                            int passTemp = dr.GetOrdinal("passTemp");
                            int estadoEvaluacion = dr.GetOrdinal("NOMBRE_ESTADO");
                            int IdEstadoEvaluacion = dr.GetOrdinal("IdEstadoEvaluacion");
                            while (dr.Read())
                            {
                                oEmp = new Entities.LstEmpleados();

                                if (!dr.IsDBNull(legajo)) oEmp.legajo = dr.GetInt32(legajo);
                                if (!dr.IsDBNull(nombre)) oEmp.nombre = dr.GetString(nombre);
                                if (!dr.IsDBNull(fecha_ingreso)) oEmp.fecha_ingreso = dr.GetString(fecha_ingreso);
                                if (!dr.IsDBNull(nro_documento)) oEmp.nro_documento = dr.GetString(nro_documento);
                                if (!dr.IsDBNull(fecha_nac)) oEmp.fecha_nacimiento = dr.GetString(fecha_nac);
                                if (!dr.IsDBNull(cod_categoria)) oEmp.cod_categoria = dr.GetInt32(cod_categoria);
                                if (!dr.IsDBNull(des_categoria)) oEmp.des_categoria = dr.GetString(des_categoria);
                                if (!dr.IsDBNull(tarea)) oEmp.tarea = dr.GetString(tarea);
                                if (!dr.IsDBNull(des_tipo_liq)) oEmp.des_tipo_liq = dr.GetString(des_tipo_liq);
                                if (!dr.IsDBNull(nom_banco)) oEmp.nom_banco = dr.GetString(nom_banco);
                                if (!dr.IsDBNull(nro_caja_ahorro)) oEmp.nro_caja_ahorro = dr.GetString(nro_caja_ahorro);
                                if (!dr.IsDBNull(nro_cbu)) oEmp.nro_cbu = dr.GetString(nro_cbu);
                                if (!dr.IsDBNull(nro_documento)) oEmp.nro_documento = dr.GetString(nro_documento);
                                if (!dr.IsDBNull(nro_cta_sb)) oEmp.nro_cta_sb = dr.GetString(nro_cta_sb);
                                if (!dr.IsDBNull(nro_cta_gastos)) oEmp.nro_cta_gastos = dr.GetString(nro_cta_gastos);
                                if (!dr.IsDBNull(secretaria)) oEmp.secrectaria = dr.GetString(secretaria);
                                if (!dr.IsDBNull(direccion)) oEmp.direccion = dr.GetString(direccion);
                                if (!dr.IsDBNull(oficina)) oEmp.oficina = dr.GetString(oficina);
                                if (!dr.IsDBNull(celular)) oEmp.celular = dr.GetString(celular);
                                if (!dr.IsDBNull(telefonos)) oEmp.telefonos = dr.GetString(telefonos);
                                if (!dr.IsDBNull(email)) oEmp.email = dr.GetString(email);
                                if (!dr.IsDBNull(passTemp)) oEmp.passTemp = dr.GetString(passTemp);
                                if (!dr.IsDBNull(estadoEvaluacion)) oEmp.estadoEvaluacion = dr.GetString(estadoEvaluacion);
                                if (!dr.IsDBNull(IdEstadoEvaluacion)) oEmp.idEstadoEvaluacion = dr.GetInt32(IdEstadoEvaluacion);

                                lst.Add(oEmp);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error in query!" + e.ToString());
                        throw e;
                    }
                    return lst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }


        }
        public static List<Entities.LstEmpleados> GetEmpleados()
        {
            StringBuilder strSQL = new StringBuilder();
            StringBuilder strCondicion = new StringBuilder();
            {

                strSQL.AppendLine("SELECT");
                strSQL.AppendLine("e.legajo, rtrim(ltrim(e.nombre)) as nombre, ");
                strSQL.AppendLine("convert(varchar(10), e.fecha_ingreso, 103) as fecha_ingreso,");
                strSQL.AppendLine("convert(varchar(10), e.fecha_nacimiento, 103) as fecha_nacimiento,");
                strSQL.AppendLine("e.cod_categoria, c.des_categoria, e.tarea, tl.des_tipo_liq,");
                strSQL.AppendLine("b.nom_banco, e.nro_caja_ahorro, e.nro_cbu,");
                strSQL.AppendLine("e.nro_documento, e.nro_cta_sb, e.nro_cta_gastos,");
                strSQL.AppendLine("rtrim(ltrim(s.descripcion)) as Secretaria, rtrim(ltrim(d1.descripcion)) as Direccion,");
                strSQL.AppendLine("ltrim(rtrim(o.nombre_oficina)) as Oficina,");
                strSQL.AppendLine("e.celular, e.telefonos, e.email, passTemp, ISNULL(z.NOMBRE, 'SIN EVALUAR') AS NOMBRE_ESTADO");
                /**/
                strSQL.AppendLine("FROM EMPLEADOS e");
                strSQL.AppendLine("LEFT join TIPOS_LIQUIDACION tl on");
                strSQL.AppendLine("tl.cod_tipo_liq = e.cod_tipo_liq");
                strSQL.AppendLine("LEFT join BANCOS b on");
                strSQL.AppendLine("b.cod_banco = e.cod_banco");
                strSQL.AppendLine("LEFT join CATEGORIAS c on");
                strSQL.AppendLine("e.cod_categoria = c.cod_categoria");
                strSQL.AppendLine("LEFT join secretaria s on");
                strSQL.AppendLine("s.id_secretaria = e.id_secretaria");
                strSQL.AppendLine("LEFT join direccion d1 on");
                strSQL.AppendLine("d1.id_direccion = e.id_direccion");
                strSQL.AppendLine("LEFT join oficinas o on");
                strSQL.AppendLine("o.codigo_oficina = e.id_oficina");
                strSQL.AppendLine("FULL JOIN FICHAS_RELEVAMIENTOS X ON");
                strSQL.AppendLine("X.CUIT = e.legajo");
                strSQL.AppendLine("FULL JOIN FICHAS_ESTADOS_EVALUACION z");
                strSQL.AppendLine("ON X.ID_ESTADO = Z.ID");
                strSQL.AppendLine("WHERE e.fecha_baja is null");
                strSQL.AppendLine("ORDER BY e.legajo");

                using (SqlConnection conn = DALBase.GetConnection("Siimva"))
                {
                    try
                    {
                        SqlCommand cmd = conn.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = strSQL.ToString();
                        cmd.Connection.Open();
                        return getLstEmpleado(cmd);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

            }
        }

        public static List<Entities.LstEmpleados> GetByLegajo(string legajo)
        {
            StringBuilder strSQL = new StringBuilder();
            StringBuilder strCondicion = new StringBuilder();
            {
                #region "Consulta"
                strSQL.AppendLine("SELECT");
                strSQL.AppendLine("e.legajo, rtrim(ltrim(e.nombre)) as nombre, ");
                strSQL.AppendLine("convert(varchar(10), e.fecha_ingreso, 103) as fecha_ingreso,");
                strSQL.AppendLine("convert(varchar(10), e.fecha_nacimiento, 103) as fecha_nacimiento,");
                strSQL.AppendLine("e.cod_categoria, c.des_categoria, e.tarea, tl.des_tipo_liq,");
                strSQL.AppendLine("b.nom_banco, e.nro_caja_ahorro, e.nro_cbu,");
                strSQL.AppendLine("e.nro_documento, e.nro_cta_sb, e.nro_cta_gastos,");
                strSQL.AppendLine("rtrim(ltrim(s.descripcion)) as Secretaria, rtrim(ltrim(d1.descripcion)) as Direccion,");
                strSQL.AppendLine("ltrim(rtrim(o.nombre_oficina)) as Oficina,");
                strSQL.AppendLine("e.celular, e.telefonos, e.email, passTemp, ISNULL(z.NOMBRE, 'SIN EVALUAR') AS NOMBRE_ESTADO");

                strSQL.AppendLine("FROM EMPLEADOS e");
                strSQL.AppendLine("LEFT join TIPOS_LIQUIDACION tl on");
                strSQL.AppendLine("tl.cod_tipo_liq = e.cod_tipo_liq");
                strSQL.AppendLine("LEFT join BANCOS b on");
                strSQL.AppendLine("b.cod_banco = e.cod_banco");
                strSQL.AppendLine("LEFT join CATEGORIAS c on");
                strSQL.AppendLine("e.cod_categoria = c.cod_categoria");
                strSQL.AppendLine("LEFT join secretaria s on");
                strSQL.AppendLine("s.id_secretaria = e.id_secretaria");
                strSQL.AppendLine("LEFT join direccion d1 on");
                strSQL.AppendLine("d1.id_direccion = e.id_direccion");
                strSQL.AppendLine("LEFT join oficinas o on");
                strSQL.AppendLine("o.codigo_oficina = e.id_oficina");
                strSQL.AppendLine("FULL JOIN FICHAS_RELEVAMIENTOS X ON");
                strSQL.AppendLine("X.CUIT = e.legajo");
                strSQL.AppendLine("FULL JOIN FICHAS_ESTADOS_EVALUACION z");
                strSQL.AppendLine("ON X.ID_ESTADO = Z.ID");
                if (!string.IsNullOrEmpty(legajo.ToString()))
                {
                    strCondicion.AppendLine(strCondicion.ToString());
                    //strCondicion.AppendLine("WHERE e.fecha_baja is null");
                    strCondicion.AppendLine("WHERE ");
                    strCondicion.AppendLine(" e.legajo=@legajo");
                }
                else
                {
                    strSQL.AppendLine("WHERE e.fecha_baja is null");
                }
                strSQL.AppendLine(strCondicion.ToString());
                strSQL.AppendLine("ORDER BY e.legajo");

                #endregion

                using (SqlConnection conn = DALBase.GetConnection("Siimva"))
                {
                    try
                    {
                        SqlCommand cmd = conn.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = strSQL.ToString();
                        cmd.Parameters.AddWithValue("@legajo", legajo);
                        cmd.Connection.Open();
                        return getLstEmpleado(cmd);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

            }
        }

        public static List<Entities.LstEmpleados> GetByNombre(string nombre)
        {
            StringBuilder strSQL = new StringBuilder();
            StringBuilder strCondicion = new StringBuilder();
            {
                #region "Consulta"
                strSQL.AppendLine("SELECT");
                strSQL.AppendLine("e.legajo, rtrim(ltrim(e.nombre)) as nombre, ");
                strSQL.AppendLine("convert(varchar(10), e.fecha_ingreso, 103) as fecha_ingreso,");
                strSQL.AppendLine("convert(varchar(10), e.fecha_nacimiento, 103) as fecha_nacimiento,");
                strSQL.AppendLine("e.cod_categoria, c.des_categoria, e.tarea, tl.des_tipo_liq,");
                strSQL.AppendLine("b.nom_banco, e.nro_caja_ahorro, e.nro_cbu,");
                strSQL.AppendLine("e.nro_documento, e.nro_cta_sb, e.nro_cta_gastos,");
                strSQL.AppendLine("rtrim(ltrim(s.descripcion)) as Secretaria, rtrim(ltrim(d1.descripcion)) as Direccion,");
                strSQL.AppendLine("ltrim(rtrim(o.nombre_oficina)) as Oficina,");
                strSQL.AppendLine("e.celular, e.telefonos, e.email, passTemp, ISNULL(z.NOMBRE, 'SIN EVALUAR') AS NOMBRE_ESTADO");

                strSQL.AppendLine("FROM EMPLEADOS e");
                strSQL.AppendLine("LEFT join TIPOS_LIQUIDACION tl on");
                strSQL.AppendLine("tl.cod_tipo_liq = e.cod_tipo_liq");
                strSQL.AppendLine("LEFT join BANCOS b on");
                strSQL.AppendLine("b.cod_banco = e.cod_banco");
                strSQL.AppendLine("LEFT join CATEGORIAS c on");
                strSQL.AppendLine("e.cod_categoria = c.cod_categoria");
                strSQL.AppendLine("LEFT join secretaria s on");
                strSQL.AppendLine("s.id_secretaria = e.id_secretaria");
                strSQL.AppendLine("LEFT join direccion d1 on");
                strSQL.AppendLine("d1.id_direccion = e.id_direccion");
                strSQL.AppendLine("LEFT join oficinas o on");
                strSQL.AppendLine("o.codigo_oficina = e.id_oficina");
                strSQL.AppendLine("FULL JOIN FICHAS_RELEVAMIENTOS X ON");
                strSQL.AppendLine("X.CUIT = e.legajo");
                strSQL.AppendLine("FULL JOIN FICHAS_ESTADOS_EVALUACION z");
                strSQL.AppendLine("ON X.ID_ESTADO = Z.ID");
                if (!string.IsNullOrEmpty(nombre.ToString()))
                {
                    strCondicion.AppendLine(strCondicion.ToString());
                    //strCondicion.AppendLine("WHERE e.fecha_baja is null");
                    strCondicion.AppendLine("WHERE ");
                    strCondicion.AppendLine(" e.nombre like '%' + @nombre + '%' ");
                }
                else
                {
                    strSQL.AppendLine("WHERE e.fecha_baja is null");
                }

                strSQL.AppendLine(strCondicion.ToString());
                strSQL.AppendLine("ORDER BY e.legajo");

                #endregion

                using (SqlConnection conn = DALBase.GetConnection("Siimva"))
                {
                    try
                    {
                        SqlCommand cmd = conn.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = strSQL.ToString();
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Connection.Open();
                        return getLstEmpleado(cmd);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

            }
        }
        private static List<Entities.LstEmpleados> getLstEmpleado(SqlCommand cmd)
        {

            List<Entities.LstEmpleados> lst = new List<Entities.LstEmpleados>();
            Entities.LstEmpleados oEmp;

            try
            {
                SqlDataReader dr = cmd.ExecuteReader();


                if (dr.HasRows)
                {
                    int legajo = dr.GetOrdinal("legajo");
                    int nombre = dr.GetOrdinal("nombre");
                    int fecha_ingreso = dr.GetOrdinal("fecha_ingreso");
                    int fecha_nac = dr.GetOrdinal("fecha_nacimiento");
                    int cod_categoria = dr.GetOrdinal("cod_categoria");
                    int des_categoria = dr.GetOrdinal("des_categoria");
                    int tarea = dr.GetOrdinal("tarea");
                    int des_tipo_liq = dr.GetOrdinal("des_tipo_liq");
                    int nom_banco = dr.GetOrdinal("nom_banco");
                    int nro_caja_ahorro = dr.GetOrdinal("nro_caja_ahorro");
                    int nro_cbu = dr.GetOrdinal("nro_cbu");
                    int nro_documento = dr.GetOrdinal("nro_documento");
                    int nro_cta_sb = dr.GetOrdinal("nro_cta_sb");

                    int nro_cta_gastos = dr.GetOrdinal("nro_cta_gastos");
                    int secretaria = dr.GetOrdinal("secretaria");
                    int direccion = dr.GetOrdinal("direccion");
                    int oficina = dr.GetOrdinal("oficina");

                    int celular = dr.GetOrdinal("celular");
                    int telefonos = dr.GetOrdinal("telefonos");
                    int email = dr.GetOrdinal("email");
                    int passTemp = dr.GetOrdinal("passTemp");
                    int estadoEvaluacion = dr.GetOrdinal("NOMBRE_ESTADO");
                    while (dr.Read())
                    {
                        oEmp = new Entities.LstEmpleados();

                        if (!dr.IsDBNull(legajo)) oEmp.legajo = dr.GetInt32(legajo);
                        if (!dr.IsDBNull(nombre)) oEmp.nombre = dr.GetString(nombre);
                        if (!dr.IsDBNull(fecha_ingreso)) oEmp.fecha_ingreso = dr.GetString(fecha_ingreso);
                        if (!dr.IsDBNull(nro_documento)) oEmp.nro_documento = dr.GetString(nro_documento);
                        if (!dr.IsDBNull(fecha_nac)) oEmp.fecha_nacimiento = dr.GetString(fecha_nac);
                        if (!dr.IsDBNull(cod_categoria)) oEmp.cod_categoria = dr.GetInt32(cod_categoria);
                        if (!dr.IsDBNull(des_categoria)) oEmp.des_categoria = dr.GetString(des_categoria);
                        if (!dr.IsDBNull(tarea)) oEmp.tarea = dr.GetString(tarea);
                        if (!dr.IsDBNull(des_tipo_liq)) oEmp.des_tipo_liq = dr.GetString(des_tipo_liq);
                        if (!dr.IsDBNull(nom_banco)) oEmp.nom_banco = dr.GetString(nom_banco);
                        if (!dr.IsDBNull(nro_caja_ahorro)) oEmp.nro_caja_ahorro = dr.GetString(nro_caja_ahorro);
                        if (!dr.IsDBNull(nro_cbu)) oEmp.nro_cbu = dr.GetString(nro_cbu);
                        if (!dr.IsDBNull(nro_documento)) oEmp.nro_documento = dr.GetString(nro_documento);
                        if (!dr.IsDBNull(nro_cta_sb)) oEmp.nro_cta_sb = dr.GetString(nro_cta_sb);
                        if (!dr.IsDBNull(nro_cta_gastos)) oEmp.nro_cta_gastos = dr.GetString(nro_cta_gastos);
                        if (!dr.IsDBNull(secretaria)) oEmp.secrectaria = dr.GetString(secretaria);
                        if (!dr.IsDBNull(direccion)) oEmp.direccion = dr.GetString(direccion);
                        if (!dr.IsDBNull(oficina)) oEmp.oficina = dr.GetString(oficina);
                        if (!dr.IsDBNull(celular)) oEmp.celular = dr.GetString(celular);
                        if (!dr.IsDBNull(telefonos)) oEmp.telefonos = dr.GetString(telefonos);
                        if (!dr.IsDBNull(email)) oEmp.email = dr.GetString(email);
                        if (!dr.IsDBNull(passTemp)) oEmp.passTemp = dr.GetString(passTemp);
                        if (!dr.IsDBNull(estadoEvaluacion)) oEmp.estadoEvaluacion = dr.GetString(estadoEvaluacion);

                        lst.Add(oEmp);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            return lst;
        }


        public static DataSet ListOficinas(int id_secretaria, int id_direccion)
        {

            string strSQL = "";
            DataSet ds;
            SqlDataAdapter adapter;


            strSQL = "select a.id_oficina as codigo_oficina, o.nombre_oficina ";
            strSQL += " from DIRECCION_X_SECRETARIA a ";
            strSQL += " join OFICINAS o on ";
            strSQL += " o.codigo_oficina=a.id_oficina ";
            strSQL += " where a.activo=1 ";
            strSQL += " and a.id_secretaria=" + id_secretaria;
            strSQL += " and a.id_direccion=" + id_direccion;
            strSQL += " Order By o.nombre_oficina";

            using (SqlConnection conn = DALBase.GetConnection("Siimva"))
            {
                try
                {
                    ds = new DataSet();

                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Connection.Open();


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

        public static DataSet ListOficinas(int id_oficina)
        {
            string strSQL = "";
            DataSet ds;
            SqlDataAdapter adapter;
            strSQL = " select * from oficinas";
            if (id_oficina > 0)
                strSQL += "where codigo_oficina=" + id_oficina;
            strSQL += " order by nombre_oficina ";


            using (SqlConnection conn = DALBase.GetConnection("Siimva"))
            {
                try
                {
                    ds = new DataSet();

                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Connection.Open();


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

        public static DataSet ListProgramas(int id_secretaria, int id_direccion)
        {

            string strSQL = "";
            DataSet ds;
            SqlDataAdapter adapter;


            strSQL = "select a.id_programa, p.programa ";
            strSQL += " from DIRECCION_X_SECRETARIA a ";
            strSQL += " join Programas_publicos p on ";
            strSQL += " p.id_programa=a.id_programa ";
            strSQL += " where a.activo=1 ";
            strSQL += " and a.id_secretaria=" + id_secretaria;
            strSQL += " and a.id_direccion=" + id_direccion;
            strSQL += " Order By a.id_programa ";

            using (SqlConnection conn = DALBase.GetConnection("Siimva"))
            {
                try
                {
                    ds = new DataSet();

                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Connection.Open();


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

        public static DataSet ListProgramas(int id_programa)
        {
            string strSQL = "";
            DataSet ds;
            SqlDataAdapter adapter;
            strSQL = " select id_programa, programa ";
            strSQL += " from programas_publicos";
            strSQL += " where activo=1";
            if (id_programa > 0)
                strSQL += " AND id_programa=" + id_programa;
            strSQL += " order by id_programa ";


            using (SqlConnection conn = DALBase.GetConnection("Siimva"))
            {
                try
                {
                    ds = new DataSet();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Connection.Open();
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
        public static DataSet ListSecretarias(int id_secretaria)
        {
            string strSQL = "";
            strSQL += "select id_secretaria, descripcion ";
            strSQL += " from Secretaria";
            strSQL += " where activa=1";
            if (id_secretaria > 0)
                strSQL += " and id_secretaria=" + id_secretaria;
            strSQL += " ORDER By descripcion ";

            DataSet ds;
            SqlDataAdapter adapter;


            using (SqlConnection conn = DALBase.GetConnection("Siimva"))
            {
                try
                {
                    ds = new DataSet();

                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Connection.Open();


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

        public static DataSet ListDirecciones(int id_secretaria)
        {

            string strSQL = "";

            strSQL = "SELECT distinct ";
            strSQL += "dxs.id_direccion, ";
            strSQL += "d.Descripcion AS direccion, ";
            strSQL += "dxs.nro_cta ";
            strSQL += "FROM DIRECCION_X_SECRETARIA dxs ";
            strSQL += "JOIN DIRECCION d ON ";
            strSQL += "d.Id_direccion = dxs.Id_direccion ";
            //strSQl = strSQl & "JOIN ejercicios e ON "
            //strSQl = strSQl & "e.activo=1 and "
            //strSQl = strSQl & "e.ejercicio=dxs.ejercicio "
            strSQL += "WHERE dxs.activo=1 ";

            if (id_secretaria > 0)
                strSQL += " AND dxs.id_secretaria=" + id_secretaria;
            strSQL += " ORDER By d.descripcion";

            DataSet ds;
            SqlDataAdapter adapter;

            using (SqlConnection conn = DALBase.GetConnection("Siimva"))
            {
                try
                {
                    ds = new DataSet();

                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Connection = conn;
                    cmd.Connection.Open();


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

        public static DataSet ListCargos(int cod_cargo)
        {

            string strSQL = "";
            DataSet ds;
            SqlDataAdapter adapter;


            strSQL = "select cod_cargo, cast(cod_cargo as varchar(3)) +' - ' + desc_cargo as desc_cargo ";
            strSQL += " from cargos ";
            if (cod_cargo > 0)
                strSQL += " where cod_cargo=" + cod_cargo;
            //strSQL += " order by desc_cargo";


            using (SqlConnection conn = DALBase.GetConnection("Siimva"))
            {
                try
                {
                    ds = new DataSet();

                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Connection.Open();


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


        public static DataSet ListCargosCuenta(string nro_cuenta)
        {

            string strSQL = "";
            DataSet ds;
            SqlDataAdapter adapter;


            strSQL = "select nro_cta , nro_cta  +' - ' + desc_cargo as desc_cargo ";
            strSQL += " from cargos ";
            if (nro_cuenta.Length > 0)
                strSQL += " where nro_cta=" + nro_cuenta;
            //strSQL += " order by desc_cargo";

            using (SqlConnection conn = DALBase.GetConnection("Siimva"))
            {
                try
                {
                    ds = new DataSet();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Connection.Open();
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

        public static DataSet ListCargosCuenta(int id_cargo)
        {

            string strSQL = "";
            DataSet ds;
            SqlDataAdapter adapter;

            strSQL = "select nro_cta , nro_cta  +' - ' + desc_cargo as desc_cargo ";
            strSQL += " from cargos ";
            if (id_cargo > 0)
                strSQL += " where cod_cargo=" + id_cargo;
            //strSQL += " order by desc_cargo";

            using (SqlConnection conn = DALBase.GetConnection("Siimva"))
            {
                try
                {
                    ds = new DataSet();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Connection.Open();
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

        public static string GetNro_cta_sb(int cod_cargo)
        {
            string strSQL = "";
            string string_cuenta = "";

            strSQL = "select nro_cta ";
            strSQL += " from cargos ";
            if (cod_cargo > 0)
                strSQL += " where cod_cargo=" + cod_cargo;
            using (SqlConnection conn = DALBase.GetConnection("Siimva"))
            {
                try
                {
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    int cuenta = dr.GetOrdinal("nro_cta");
                    if (dr.Read())
                    {
                        string_cuenta = dr.GetString(cuenta);
                    };
                    dr.Close();
                    return string_cuenta;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static DataSet ListSecciones(int cod_seccion)
        {

            string strSQL = "";
            DataSet ds;
            SqlDataAdapter adapter;


            strSQL = "select cod_seccion, des_seccion  ";
            strSQL += " from Secciones ";
            if (cod_seccion > 0)
                strSQL += " where cod_seccion=" + cod_seccion;
            strSQL += " order by des_seccion";


            using (SqlConnection conn = DALBase.GetConnection("Siimva"))
            {
                try
                {
                    ds = new DataSet();

                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Connection.Open();


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



        public static DataSet ListSemestres(int cod_semestre)
        {

            string strSQL = "";
            DataSet ds;
            SqlDataAdapter adapter;


            strSQL = "select *  ";
            strSQL += " from Semestres ";
            if (cod_semestre > 0)
                strSQL += " where cod_semestre=" + cod_semestre;
            strSQL += " order by 1";


            using (SqlConnection conn = DALBase.GetConnection("Siimva"))
            {
                try
                {
                    ds = new DataSet();

                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Connection.Open();


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

        public static DataSet ListCategoria(int cod_categoria)
        {

            string strSQL = "";
            DataSet ds;
            SqlDataAdapter adapter;


            strSQL = "select cod_categoria, + '$' + cast(sueldo_basico as varchar(14)) +' - ' + cast(cod_categoria as varchar(2)) +' - '+ SUBSTRING(des_categoria, 0, 30) as des_categoria";
            strSQL += " from categorias";
            if (cod_categoria > 0)
                strSQL += " where cod_categoria=" + cod_categoria;



            using (SqlConnection conn = DALBase.GetConnection("Siimva"))
            {
                try
                {
                    ds = new DataSet();

                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Connection.Open();


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

        public static DataSet ListClasificacion_personal(int cod_clasif_per)
        {

            string strSQL = "";
            DataSet ds;
            SqlDataAdapter adapter;


            strSQL = "select cod_clasif_per, des_clasif_per  ";
            strSQL += " from CLASIFICACIONES_PERSONAL ";
            if (cod_clasif_per > 0)
                strSQL += " where cod_clasif_per=" + cod_clasif_per;


            using (SqlConnection conn = DALBase.GetConnection("Siimva"))
            {
                try
                {
                    ds = new DataSet();

                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Connection.Open();


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

        public static DataSet LisTiposDocumento(int cod_tipo_documento)
        {

            string strSQL = "";
            DataSet ds;
            SqlDataAdapter adapter;



            strSQL = " select cod_tipo_documento, des_tipo_documento  ";
            strSQL += " from TIPOS_DOCUMENTOS";
            if (cod_tipo_documento > 0)
                strSQL += " where cod_tipo_documento=" + cod_tipo_documento;


            using (SqlConnection conn = DALBase.GetConnection("Siimva"))
            {
                try
                {
                    ds = new DataSet();

                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Connection.Open();
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

        public static DataSet ListTiposLiquidacion(int cod_tipo_liq)
        {

            string strSQL = "";
            DataSet ds;
            SqlDataAdapter adapter;



            strSQL = " select cod_tipo_liq, des_tipo_liq  ";
            strSQL += " from TIPOS_LIQUIDACION";
            if (cod_tipo_liq > 0)
                strSQL += " where cod_tipo_liq=" + cod_tipo_liq;


            using (SqlConnection conn = DALBase.GetConnection("Siimva"))
            {
                try
                {
                    ds = new DataSet();

                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Connection.Open();
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

        public static DataSet ListNroLiquidacion(int anio, int cod_tipo_liq)
        {

            string strSQL = "";
            DataSet ds;
            SqlDataAdapter adapter;
            strSQL = " select nro_liquidacion, des_liquidacion ";
            strSQL += " from LIQUIDACIONES";
            if (cod_tipo_liq > 0)
            {
                strSQL += " where cod_tipo_liq=" + cod_tipo_liq;
                strSQL += " and anio=" + anio;
            }
            strSQL += " Order by nro_liquidacion";

            using (SqlConnection conn = DALBase.GetConnection("Siimva"))
            {
                try
                {
                    ds = new DataSet();

                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Connection.Open();
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

        public static DataSet PeriodosLiquidados(int anio, int cod_tipo_liq)
        {

            string strSQL = string.Empty;
            string strWhere = string.Empty;
            DataSet ds;
            SqlDataAdapter adapter;
            strSQL = @" SELECT nro_liquidacion, des_liquidacion
                        FROM LIQUIDACIONES";
            if (cod_tipo_liq > 0)
            {
                strWhere = @" WHERE prueba=0
                              AND cod_tipo_liq = " + cod_tipo_liq +
                             "AND anio = " + anio;
            };

            if (strWhere.Length > 0)
                strSQL += strWhere + " ORDER BY nro_liquidacion";
            else
                strSQL += " WHERE prueba=0 ORDER BY nro_liquidacion";
            using (SqlConnection conn = DALBase.GetConnection("Siimva"))
            {
                try
                {
                    ds = new DataSet();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Connection.Open();
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

        public static DataSet ListRegimen(int cod_regimen_empleado)
        {
            string strSQL = "";
            DataSet ds;
            SqlDataAdapter adapter;
            strSQL = "select cod_regimen_empleado, descripcion ";
            strSQL += " from EMPLEADOS_REGIMEN";
            if (cod_regimen_empleado > 0)
                strSQL += " where cod_regimen_empleado=" + cod_regimen_empleado;


            using (SqlConnection conn = DALBase.GetConnection("Siimva"))
            {
                try
                {
                    ds = new DataSet();

                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Connection.Open();
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

        public static DataSet ListEscalaAumentos(int cod_escala_aumento)
        {
            string strSQL = "";
            DataSet ds;
            SqlDataAdapter adapter;
            strSQL = "select cod_escala_aumento, descripcion ";
            strSQL += " from ESCALA_AUMENTOS";
            if (cod_escala_aumento > 0)
                strSQL += " where cod_escala_aumento=" + cod_escala_aumento;
            using (SqlConnection conn = DALBase.GetConnection("Siimva"))
            {
                try
                {
                    ds = new DataSet();

                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Connection.Open();


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

        public static DataSet ListBancos(int cod_banco)
        {
            string strSQL = "";
            DataSet ds;
            SqlDataAdapter adapter;

            strSQL = " select * from bancos ";
            strSQL += " where activo=1";
            if (cod_banco > 0)
                strSQL += " AND cod_banco=" + cod_banco;


            using (SqlConnection conn = DALBase.GetConnection("Siimva"))
            {
                try
                {
                    ds = new DataSet();

                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Connection.Open();


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

        public static DataSet ListTipos_Cuenta(int cod_tipo_cuenta)
        {
            string strSQL = "";
            DataSet ds;
            SqlDataAdapter adapter;

            strSQL = " select * from tipos_cuentas ";
            if (cod_tipo_cuenta > 0)
                strSQL += " where cod_tipo_cuenta=" + cod_tipo_cuenta;


            using (SqlConnection conn = DALBase.GetConnection("Siimva"))
            {
                try
                {
                    ds = new DataSet();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Connection.Open();
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

        public static DataSet ListSexos()
        {
            string strSQL = "";
            DataSet ds;
            SqlDataAdapter adapter;
            strSQL = " select * from sexos ";
            using (SqlConnection conn = DALBase.GetConnection("Siimva"))
            {
                try
                {
                    ds = new DataSet();

                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Connection.Open();


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

        public static DataSet ListEstado_Civil()
        {
            string strSQL = "";
            DataSet ds;
            SqlDataAdapter adapter;

            strSQL = " select * from ESTADOS_CIVILES ";


            using (SqlConnection conn = DALBase.GetConnection("Siimva"))
            {
                try
                {
                    ds = new DataSet();

                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Connection.Open();


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

        public static DataSet ListPlan_ctas_egreso(string nro_cta)
        {
            string strSQL = "";
            DataSet ds;
            SqlDataAdapter adapter;

            strSQL = " select * from Plan_ctas_egreso ";
            if (nro_cta.Length > 0)
                strSQL += " where nro_cta=" + nro_cta;

            using (SqlConnection conn = DALBase.GetConnection("Siimva"))
            {
                try
                {
                    ds = new DataSet();

                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Connection.Open();


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

        public static DataSet ListRevista(int id)
        {
            string strSQL = "";
            DataSet ds;
            SqlDataAdapter adapter;
            strSQL = @"SELECT * 
                       FROM SITUACION_REVISTA_LEGAJO ";
            if (id > 0)
                strSQL += " where id_revista=" + id;
            using (SqlConnection conn = DALBase.GetConnection("Siimva"))
            {
                try
                {
                    ds = new DataSet();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Connection.Open();
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
