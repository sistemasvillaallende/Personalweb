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
        public static List<Entities.LstEmpleados> GetEmpleados2()
        {
            return DAL.ConsultaEmpleadoD.GetEmpleados2();
        }
        public static List<Entities.LstEmpleados> GetEmpleadosByDireccion(int idDireccion)
        {
            return DAL.ConsultaEmpleadoD.GetEmpleadosByDireccion(idDireccion, 6);
        }
        public static List<Entities.LstEmpleados> GetByLegajo(string legajo)
        {
            return DAL.ConsultaEmpleadoD.GetByLegajo(legajo);
        }

        public static List<Entities.LstEmpleados> GetByNombre(string nombre)
        {
            return DAL.ConsultaEmpleadoD.GetByNombre(nombre);
        }

        public static List<Entities.LstEmpleados> GetEmpleadosByCategoria(int cod_categoria)
        {

            return DAL.ConsultaEmpleadoD.GetEmpleadosByCategoria(cod_categoria);
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

        public static DataSet ListDirecciones(int id_direccion)
        {
            return DAL.ConsultaEmpleadoD.ListDirecciones(id_direccion);

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

        public static DataSet ListCategoriaProfesional()
        {
            return DAL.ConsultaEmpleadoD.ListCategoriaProfesional();
        }

        public static DataSet ListTareas()
        {
            return DAL.ConsultaEmpleadoD.ListTareas();
        }




    }
}
