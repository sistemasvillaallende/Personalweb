using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace BLL
{
    public class LiquidacionesB
    {
        public static Entities.Liquidacion getByPk(int anio, int cod_tipo_liq, int nro_liquidacion)
        {
            SqlConnection cn = DAL.DALBase.GetConnection("SIIMVA");
            cn.Open();
            try
            {
                return DAL.LiquidacionesD.getByPk(anio, cod_tipo_liq, nro_liquidacion, cn);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { cn.Close(); }
        }

        public static List<Entities.Liquidacion> getLstLiq()
        {
            SqlConnection cn = DAL.DALBase.GetConnection("SIIMVA");
            cn.Open();
            try
            {
                return DAL.LiquidacionesD.getLstLiq(cn);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { cn.Close(); }
        }

        public static void insert(Entities.Liquidacion obj)
        {
            SqlConnection cn = DAL.DALBase.GetConnection("SIIMVA");
            SqlTransaction trx = null;
            try
            {
                cn.Open();
                trx = cn.BeginTransaction();
                DAL.LiquidacionesD.insert(obj, cn, trx);
                trx.Commit();
            }
            catch (Exception ex)
            {
                trx.Rollback();
                throw ex;
            }
            finally
            { cn.Close(); }
        }

        public static void update(Entities.Liquidacion oLiq)
        {
            SqlConnection cn = DAL.DALBase.GetConnection("SIIMVA");
            SqlTransaction trx = null;

            try
            {
                cn.Open();
                trx = cn.BeginTransaction();
                DAL.LiquidacionesD.update(oLiq, cn, trx);
                trx.Commit();
            }
            catch (Exception ex)
            {
                trx.Rollback();
                throw ex;
            }
            finally
            { cn.Close(); }

        }

        public static void Liquidar(int anio, int cod_tipo_liq, int nro_liquidacion, Liquidacion oLiq,
            bool salarioFam)
        {
            SqlConnection cn = DAL.DALBase.GetConnection("SIIMVA");
            SqlTransaction trx = null;
            try
            {
                if (salarioFam)
                {
                    BLL.Concepto_Liq_x_EmpB.CalculoSalarioFamiliar(oLiq.usuario);
                    BLL.Concepto_Liq_x_EmpB.CalculoSalarioHijoDiscapcitado(oLiq.usuario);
                }
                cn.Open();
                trx = cn.BeginTransaction();                
                DAL.LiquidacionesD.Liquidar(anio, cod_tipo_liq, nro_liquidacion, oLiq, cn, trx);
                trx.Commit();
            }
            catch (Exception ex)
            {
                trx.Rollback();
                throw ex;
            }
            finally
            { cn.Close(); }

        }

        //public static bool ExisteDetalleLiquidacion(int anio, int cod_tipo_liq, int nro_liquidacion)
        //{
        //    SqlConnection cn = DAL.DALBase.GetConnection("SIIMVA");
        //    bool si = false;
        //    try
        //    {
        //        cn.Open();
        //        si = DAL.LiquidacionesD.ExisteDetalleLiquidacion(anio, cod_tipo_liq, nro_liquidacion, cn);                
        //        return si;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    { cn.Close(); }
        //}

        public static void Aguinaldo(int anio, int cod_tipo_liq, int nro_liquidacion, int cod_semestre, Liquidacion oLiq)
        {
            SqlConnection cn = DAL.DALBase.GetConnection("SIIMVA");
            SqlTransaction trx = null;

            try
            {
                cn.Open();
                trx = cn.BeginTransaction();
                DAL.LiquidacionesD.Aguinaldo(anio, cod_tipo_liq, nro_liquidacion, cod_semestre, oLiq, cn, trx);
                trx.Commit();
            }
            catch (Exception ex)
            {
                trx.Rollback();
                throw ex;
            }
            finally
            { cn.Close(); }

        }

        public static void Publicar_liquidacion(int anio, int cod_tipo_liq, int nro_liquidacion, string usuario, string operacion, bool publica)
        {
            SqlConnection cn = DAL.DALBase.GetConnection("SIIMVA");
            try
            {
                cn.Open();
                DAL.LiquidacionesD.Publicar_liquidacion(anio, cod_tipo_liq, nro_liquidacion, usuario, operacion, publica, cn);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { cn.Close(); }
        }

        public static void Cerrar_liquidacion(int anio, int cod_tipo_liq, int nro_liquidacion, string usuario_cierre, string operacion, bool cerrada, string fecha_cierre)

        {
            SqlConnection cn = DAL.DALBase.GetConnection("SIIMVA");
            try
            {
                cn.Open();
                DAL.LiquidacionesD.Cerrar_liquidacion(anio, cod_tipo_liq, nro_liquidacion, usuario_cierre, operacion, cerrada, fecha_cierre, cn);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { cn.Close(); }

        }

    }
}
