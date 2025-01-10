using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ConsultaEmpleadoB
    {


        //ConsultaExpedienteD objExp;
        DataSet dsDatos;


        //struct ListerProps
        //{
        //  public int TotalRows;
        //  //public decimal  TotalDeuda;
        //}
        //ListerProps o;

        public ConsultaEmpleadoB()
        {
            //objExp = new ConsultaExpedienteD();
            dsDatos = new DataSet();
            //o = new ListerProps();
        }

        //public int TotalRows
        //{
        //  get { return o.TotalRows; }
        //}


        public static List<Entities.LstEmpleados> GetEmpleados()
        {
            return DAL.ConsultaEmpleadoD.GetEmpleados();
        }

        public static List<Entities.LstEmpleados> GetByLegajo(string legajo)
        {
            return DAL.ConsultaEmpleadoD.GetByLegajo(legajo);
        }

        public static List<Entities.LstEmpleados> GetByNombre(string nombre)
        {
            return DAL.ConsultaEmpleadoD.GetByNombre(nombre);
        }
        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>


        public static DataSet ListOficinas(int id_oficina)
        {
            //o.TotalRows = objExp.TotalRows;
            return DAL.ConsultaEmpleadoD.ListOficinas(id_oficina);
        }

        public static DataSet ListProgramas(int id_programa)
        {
            //o.TotalRows = objExp.TotalRows;
            return DAL.ConsultaEmpleadoD.ListProgramas(id_programa);
        }

        public static DataSet ListProgramas(int id_secretaria, int id_direccion)
        {
            return DAL.ConsultaEmpleadoD.ListProgramas(id_secretaria, id_direccion);
        }

        public static DataSet ListOficinas(int id_secretaria, int id_direccion)
        {
            return DAL.ConsultaEmpleadoD.ListOficinas(id_secretaria, id_direccion);
        }


        public static DataSet ListSecretarias(int id_secretaria)
        {
            return DAL.ConsultaEmpleadoD.ListSecretarias(id_secretaria);
        }

        public static DataSet LisCargos(int cod_cargo)
        {
            return DAL.ConsultaEmpleadoD.ListCargos(cod_cargo);
        }

        public static DataSet ListCargosCuenta(string nro_cta)
        {
            return DAL.ConsultaEmpleadoD.ListCargosCuenta(nro_cta);
        }

        public static DataSet ListCargosCuenta(int id_cargo)
        {
            return DAL.ConsultaEmpleadoD.ListCargosCuenta(id_cargo);
        }

        public static DataSet ListSecciones(int cod_seccion)
        {
            return DAL.ConsultaEmpleadoD.ListSecciones(cod_seccion);
        }

        public static DataSet ListSemestres(int cod_semestre)
        {
            return DAL.ConsultaEmpleadoD.ListSemestres(cod_semestre);
        }

        public static DataSet ListDirecciones(int id_secretaria)
        {
            return DAL.ConsultaEmpleadoD.ListDirecciones(id_secretaria);

        }

        public static DataSet ListCategoria(int cod_categoria)
        {
            return DAL.ConsultaEmpleadoD.ListCategoria(cod_categoria);
        }


        public static DataSet ListClasificacion_personal(int cod_clasif_per)
        {
            return DAL.ConsultaEmpleadoD.ListClasificacion_personal(cod_clasif_per);
        }


        public static DataSet LisTiposDocumento(int cod_tipo_documento)
        {
            return DAL.ConsultaEmpleadoD.LisTiposDocumento(cod_tipo_documento);
        }


        public static DataSet ListTiposLiquidacion(int cod_tipo_liq)
        {
            return DAL.ConsultaEmpleadoD.ListTiposLiquidacion(cod_tipo_liq);
        }

        public static DataSet ListNroLiquidacion(int anio, int cod_tipo_liq)
        {
            return DAL.ConsultaEmpleadoD.ListNroLiquidacion(anio, cod_tipo_liq);
        }

        public static DataSet PeriodosLiquidados(int anio, int cod_tipo_liq)
        {
            return DAL.ConsultaEmpleadoD.PeriodosLiquidados(anio, cod_tipo_liq);
        }

        public static DataSet ListRegimen(int cod_regimen_empleado)
        {
            return DAL.ConsultaEmpleadoD.ListRegimen(cod_regimen_empleado);
        }


        public static DataSet ListEscalaAumentos(int cod_escala_aumento)
        {
            return DAL.ConsultaEmpleadoD.ListEscalaAumentos(cod_escala_aumento);
        }


        public static DataSet ListBancos(int cod_banco)
        {
            return DAL.ConsultaEmpleadoD.ListBancos(cod_banco);
        }

        public static DataSet ListTipos_Cuenta(int cod_tipo_cuenta)
        {
            return DAL.ConsultaEmpleadoD.ListTipos_Cuenta(cod_tipo_cuenta);
        }


        public static DataSet ListSexos()
        {
            return DAL.ConsultaEmpleadoD.ListSexos();
        }

        public static DataSet ListEstado_Civil()
        {
            return DAL.ConsultaEmpleadoD.ListEstado_Civil();
        }

        public static DataSet ListPlan_ctas_egreso(string nro_cta)
        {
            return DAL.ConsultaEmpleadoD.ListPlan_ctas_egreso(nro_cta);
        }

        public static DataSet ListRevista(int id)
        {
            return DAL.ConsultaEmpleadoD.ListRevista(id);
        }

        //public DataSet ListAsuntos()
        //{
        //  dsDatos = objExp.ListAsuntos();
        //  o.TotalRows = objExp.TotalRows;
        //  return dsDatos;
        //}

        //public DataSet ListAsuntos(int cod_tipo_tramite)
        //{
        //  dsDatos = objExp.ListAsuntos(cod_tipo_tramite);
        //  o.TotalRows = objExp.TotalRows;
        //  return dsDatos;
        //}

        //public DataSet ListAsuntos(int cod_tipo_tramite, string filtro, int Page, int RowsPerPage,
        //  string OrderBy, string Order)
        //{
        //  dsDatos = objExp.ListAsuntos(cod_tipo_tramite, filtro, Page, RowsPerPage, OrderBy, Order);
        //  o.TotalRows = objExp.TotalRows;
        //  return dsDatos;
        //}

        //public string GetDescripcionAsunto(int cod_asunto)
        //{
        //  string descripcion = "";
        //  descripcion = objExp.GetDescripcionAsunto(cod_asunto);
        //  o.TotalRows = objExp.TotalRows;
        //  return descripcion;
        //}


        //public string ListMovimientos_Expediente(int anio, long nro, int Page, int RowsPerPage)
        //{
        //  string strXML = "";
        //  strXML = objExp.ListMovimientos_Expediente(anio, nro, Page, RowsPerPage);
        //  o.TotalRows = objExp.TotalRows;
        //  return strXML;
        //}


        //public string ListMovimientos_ExpedienteByUsuario(int Page, int RowsPerPage,
        //  int id_oficina_usuario, string strFechas, string strFindBy,
        //  string strInput, string OrderBy, string Order, bool verTodo)
        //{
        //  string strXML = "";
        //  strXML = objExp.ListMovimientos_ExpedienteByUsuario(Page, RowsPerPage,
        //    id_oficina_usuario, strFechas, strFindBy, strInput, OrderBy, Order, verTodo);
        //  o.TotalRows = objExp.TotalRows;
        //  return strXML;
        //}


        //public string ListAgregados_Expediente(int anio, long nro, int Page, int RowsPerPage)
        //{
        //  string strXML = "";
        //  strXML = objExp.ListAgregados_Expediente(anio, nro, Page, RowsPerPage);
        //  o.TotalRows = objExp.TotalRows;
        //  return strXML;
        //}



        //public string OficinasList(int Page, int RowsPerPage)
        //{
        //  string strXML = "";
        //  strXML = objExp.OficinasList(Page, RowsPerPage);
        //  o.TotalRows = objExp.TotalRows;
        //  return strXML;
        //}

        //public static object GetByfecha_ingreso(string fecha_ingreso, int id_oficina_origen, bool verTodo)
        //{
        //  throw new NotImplementedException();
        //}

        //public DataSet ListRelacion_Estados_Expediente(int cod_estado_expediente)
        //{
        //  dsDatos = objExp.ListRelacion_Estados_Expediente(cod_estado_expediente);
        //  o.TotalRows = objExp.TotalRows;
        //  return dsDatos;
        //}


        //public DataSet ListEstados()
        //{
        //  dsDatos = objExp.ListEstados();
        //  o.TotalRows = objExp.TotalRows;
        //  return dsDatos;
        //}

        //public DataSet ListAgegados()
        //{
        //  dsDatos = objExp.ListAgegados();
        //  o.TotalRows = objExp.TotalRows;
        //  return dsDatos;
        //}

        //public DataSet ListTiposDni()
        //{
        //  dsDatos = objExp.ListTiposDni();
        //  o.TotalRows = objExp.TotalRows;
        //  return dsDatos;
        //}


        //public string ConsultaExpedienteByParametros(string Nombre,
        //  string Anio, string Nro_expediente, string id_oficina_actual,
        //  string id_estado, string id_tipo_tramite, string id_asunto, string strFechas,
        //  long Page, long RowsPerPage, out bool hasregistro)
        //{
        //  string strXML = "";
        //  strXML = objExp.ConsultaExpedienteByParametros(Nombre, Anio, Nro_expediente,
        //    id_oficina_actual, id_estado, id_tipo_tramite, id_asunto, strFechas,
        //    Page, RowsPerPage, out hasregistro);
        //  o.TotalRows = objExp.TotalRows;
        //  return strXML;
        //}



        //public string ListMovimientos_ExpedienteByParametros(string Nombre, string Anio, string Nro_expediente,
        //  int Page, int RowsPerPage, out bool hasregistro)
        //{
        //  string strXML = "";
        //  strXML = objExp.ListMovimientos_ExpedienteByParametros(Nombre, Anio, Nro_expediente,
        //    Page, RowsPerPage, out hasregistro);
        //  o.TotalRows = objExp.TotalRows;
        //  return strXML;
        //}


        //public string ConsultaExpedienteByParametros(string Nombre,
        //string Anio, string Nro_expediente, int RowsPerPage)
        //{
        //  string strXML = "";
        //  strXML = objConsulta.ConsultaExpedienteByParametros(Nombre, Anio, Nro_expediente,
        //    Page, RowsPerPage, out hasregistro);
        //  o.TotalRows = objConsulta.TotalRows;
        //  return strXML;
        //}



    }
}
