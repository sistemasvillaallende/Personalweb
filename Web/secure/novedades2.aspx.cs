using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Drawing;
using System.Net;
using System.Data.OleDb;
using System.Data;
using ClosedXML.Excel;

namespace web.secure
{
    public partial class novedades2 : System.Web.UI.Page
    {

        string operacion = "";
        List<Entities.Conceptos> lstConceptos = new List<Conceptos>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
                Response.Redirect("../index.aspx");


            if (!Page.IsPostBack)
            {
                CargarCombos();
                txtAnio.Text = DateTime.Now.Year.ToString();
                Session.Add("Detalle", lstConceptos);
                Session.Add("Total", 0);
                Session.Add("opcion", 0);
                Session.Add("index", 0);
                txtAnio.Focus();

            }
            string var = Request.Params["__EVENTARGUMENT"];
            if (var == "Error")
                divError.Visible = false;
            if (var == "Confirma")
                divConfirma.Visible = false;
            //if (var == "Alerta")
            //    divMSJDetalleLegajos.Visible = false;
        }

        protected void CargarCombos()
        {
            txtTipo_liq.DataTextField = "des_tipo_liq";
            txtTipo_liq.DataValueField = "cod_tipo_liq";
            txtTipo_liq.DataSource = BLL.ConsultaEmpleadoB.ListTiposLiquidacion(0);
            txtTipo_liq.DataBind();
        }
        protected void txtTipo_liq_SelectedIndexChanged(object sender, EventArgs e)
        {
            int anio = Convert.ToInt32(txtAnio.Text);
            int cod_tipo_liq = Convert.ToInt32(txtTipo_liq.SelectedValue);
            txtNro_liq.Items.Clear();
            txtNro_liq.DataTextField = "des_liquidacion";
            txtNro_liq.DataValueField = "nro_liquidacion";
            txtNro_liq.DataSource = BLL.ConsultaEmpleadoB.ListNroLiquidacion(anio, cod_tipo_liq);
            txtNro_liq.DataBind();
            txtNro_liq.Focus();
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("cargar_conceptos.aspx");
        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            List<Entities.Conceptos> lst;
            lst = leerGrillaConceptos();
            int op = 0;

            if (Session["opcion"] != null)
                op = (Convert.ToInt32(Session["opcion"]) == 0 ? 0 : 1);
            try
            {

                if (txtAnio.Text.Length == 0 || Convert.ToInt32(txtTipo_liq.SelectedValue) == 0 || Convert.ToInt32(txtNro_liq.SelectedValue) == 0)
                {
                    divError.Visible = true;
                    txtError.InnerHtml = "Problemas con el Alta, Ingrese nuevamente el Año, Tipo de liquidacion y el Mes de la Liquidacion!!!";
                    PanelInfomacion.Update();
                    return;
                }

                if (lst.Count == 0)
                {
                    divError.Visible = true;
                    txtError.InnerHtml = "Debe agregar al menos un Item/s al detalle!!!";
                    PanelInfomacion.Update();
                    return;
                }

                if (op == 0)
                {

                    BLL.ParxDetLiqxEmpB.InsertParCptoLiqxEmp(Convert.ToInt32(txtAnio.Text),
                      Convert.ToInt32(txtTipo_liq.SelectedValue), Convert.ToInt32(txtNro_liq.Text), lst, Convert.ToString(Session));
                    divConfirma.Visible = true;
                    msjConfirmar.InnerHtml = "Los datos han sido ingresada de forma correcta!!!";
                    PanelInfomacion.Update();
                }
                //else
                //{
                //    BLL.ParxDetLiqxEmpB.UpdateParxDetLiqxEmp(Convert.ToInt32(txtAnio.Text),
                //   Convert.ToInt32(txtTipo_liq.SelectedValue), Convert.ToInt32(txtNro_liq.Text),
                //   Convert.ToInt32(txtCod_concepto.Text), lstDetalle);

                //    divInformacion.Visible = true;
                //    msjInformacion.InnerHtml = "Los datos han sido ingresada de forma correcta!!!";
                //    PanelInfomacion.Update();

                //    btnAgregarConceptos.Visible = true;
                //    btnConfirma.Visible = false;
                //}
                //FillDetalle();
                //txtOP.InnerText = oOrden.nroOrden.ToString();
            }
            catch
            {
                divError.Visible = true;
                txtError.InnerHtml = "Problemas con el Alta de los Novedades, Revise la Grilla, si se Cargo varias veces el mismo Legajo!!!";
                PanelInfomacion.Update();
            }
        }

        protected string uploadFile(FileUpload fU, string entidad)
        {
            string ret = "nodisponible.png";
            try
            {
                string path = Server.MapPath(entidad + "/");
                if (fU.HasFile)
                {
                    try
                    {
                        string nombreImagen = fU.FileName;
                        fU.PostedFile.SaveAs(path + nombreImagen);
                        ret = nombreImagen;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private void GenerateExcelData(string path, string archivo)
        //{
        //    try
        //    {
        //        //string read = System.IO.Path.GetFullPath(Server.MapPath("~/empdetail.xlsx"));
        //        //string nombrearchivo = System.IO.Path.GetFullPath(Server.MapPath(path));
        //        string nombrearchivo = Server.MapPath("~/archivos/" + archivo);
        //        string conexion;

        //        if (Path.GetExtension(path) == ".xls")
        //        {
        //            conexion = string.Format("Provider = Microsoft.ACE.OLEDB.4.0; Data Source = {0}; Extended Properties = 'Excel 8.0;'", nombrearchivo);
        //        }
        //        else if (Path.GetExtension(path) == ".xlsx")
        //        {
        //            conexion = string.Format("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = {0}; Extended Properties = 'Excel 12.0;'", nombrearchivo);
        //        }
        //        else
        //        {
        //            conexion = string.Empty;
        //        }

        //        if (conexion.Length > 0)
        //        {
        //            DataTable dtExcelSchema;

        //            OleDbConnection cn = new OleDbConnection(conexion);
        //            cn.Open();
        //            OleDbCommand cmd = new OleDbCommand();
        //            OleDbDataAdapter adapter = new OleDbDataAdapter();
        //            dtExcelSchema = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        //            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();


        //            DataSet dset = new DataSet();
        //            cmd.Connection = cn;
        //            cmd.CommandType = CommandType.Text;
        //            cmd.CommandText = "SELECT distinct([*]) FROM [" + sheetName + "]";
        //            adapter = new OleDbDataAdapter(cmd);
        //            adapter.Fill(dset, "Conceptos");
        //            gvConceptos.DataSource = dset.Tables["Concepto"].DefaultView;
        //            gvConceptos.DataBind();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //lbl1.Text = ex.ToString();
        //    }
        //    finally
        //    {
        //        //x.Close();
        //    }
        //}


        protected void ImportExcel()
        {
            //Use Library Open the Excel file using ClosedXML.
            //string archivo = uploadFile(fUploadConceptos, "archivos");
            ////string nombrearchivo = System.IO.Path.GetFullPath(Server.MapPath(archivo));
            //string path = Server.MapPath("~/archivos/" + nombrearchivo);
            //using (XLWorkbook workBook = new XLWorkbook(fUploadConceptos.PostedFile.InputStream))
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            string nombreArchivo = uploadFile(fUploadConceptos, "archivos");
            string path = Server.MapPath("archivos/" + nombreArchivo);
            using (XLWorkbook workBook = new XLWorkbook(path))
            {
                //Read the first Sheet from Excel file.
                IXLWorksheet workSheet = workBook.Worksheet(1);

                //Create a new DataTable.
                DataTable dt = new DataTable();

                //Loop through the Worksheet rows.
                bool firstRow = true;
                foreach (IXLRow row in workSheet.Rows())
                {
                    //Use the first row to add columns to DataTable.
                    if (firstRow)
                    {
                        foreach (IXLCell cell in row.Cells())
                        {
                            dt.Columns.Add(cell.Value.ToString());
                        }
                        //Agrego a esta columna que es para agregarle botones para acciones
                        //dt.Columns.Add("Accion");
                        firstRow = false;
                    }
                    else
                    {
                        //Add rows to DataTable.
                        dt.Rows.Add();
                        int i = 0;
                        foreach (IXLCell cell in row.Cells())
                        {
                            if (cell.Value.ToString().Length > 0)
                            {
                                dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                                i++;
                            }
                            //else
                            //break;
                        }
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    List<Entities.Conceptos> lst = new List<Conceptos>();
                    lst = pasarALstConcepto2(dt);
                    Session["Detalle"] = lst;
                    gvConceptos.DataSource = lst;
                    gvConceptos.DataBind();
                    if (gvConceptos.Rows.Count > 0)
                    {
                        gvConceptos.UseAccessibleHeader = true;
                        gvConceptos.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }
            }
        }

        //private List<Entities.Conceptos> pasarALstConcepto(DataTable dt)
        //{
        //    List<Entities.Conceptos> lstConcepto = new List<Entities.Conceptos>();
        //    lstConcepto = (from DataRow dr in dt.Rows
        //                   select new Entities.Conceptos()
        //                   {
        //                       legajo = Convert.ToInt32(dr["legajo"].ToString().Trim()),
        //                       codigo = Convert.ToInt32(dr["codigo"]),
        //                       importe = Convert.ToDecimal(dr["importe"]),
        //                       nro_parametro = Convert.ToInt32(dr["nro_parametro"])
        //                   }).ToList();
        //    lstConcepto = lstConcepto.OrderBy(o => o.legajo).OrderBy(o => o.codigo).ToList();
        //    return lstConcepto;
        //}

        private List<Entities.Conceptos> pasarALstConcepto2(DataTable dt)
        {
            List<Entities.Conceptos> lstConcepto = new List<Entities.Conceptos>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //if (i == 72)
                //    continue;
                Entities.Conceptos lst = new Entities.Conceptos();
                if (dt.Rows[i]["legajo"] != DBNull.Value)
                {
                    lst.legajo = Convert.ToInt32(dt.Rows[i]["legajo"]);
                    lst.codigo = Convert.ToInt32(dt.Rows[i]["codigo"]);
                    lst.importe = Convert.ToDecimal(dt.Rows[i]["importe"]);
                    lst.nro_parametro = Convert.ToInt32(dt.Rows[i]["nro_parametro"]);
                    lstConcepto.Add(lst);
                }
            }
            return lstConcepto;
        }

        private List<Entities.Conceptos> leerGrillaConceptos()
        {
            //decimal tot = 0;
            List<Entities.Conceptos> lst = new List<Entities.Conceptos>();
            for (int i = 0; i < gvConceptos.Rows.Count; i++)
            {
                GridViewRow row = gvConceptos.Rows[i];
                Entities.Conceptos obj = new Entities.Conceptos();
                obj.legajo = Convert.ToInt32(gvConceptos.DataKeys[i].Values["legajo"]);
                obj.codigo = Convert.ToInt32(gvConceptos.DataKeys[i].Values["codigo"]);
                obj.importe = Convert.ToDecimal(gvConceptos.DataKeys[i].Values["importe"].ToString());
                obj.nro_parametro = Convert.ToInt32(gvConceptos.DataKeys[i].Values["nro_parametro"]);
                //obj.anio = Convert.ToInt32(txtAnio.Text);
                //obj.cod_tipo_liq = Convert.ToInt32(txtTipo_liq.SelectedValue);
                //obj.nro_liquidacion = Convert.ToInt32(txtNro_liq.SelectedValue);
                lst.Add(obj);
            }
            //txtTot.Text = tot.ToString();
            return lst;
        }

        protected void gvConceptos_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnConceptos_x_legajos_Click(object sender, EventArgs e)
        {
            try
            {
                ImportExcel();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void cmdSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("cargar_conceptos.aspx");
        }

        protected void cmdCerrar_Click(object sender, EventArgs e)
        {

        }


        private void fillGrillaConceptos(List<Entities.Conceptos> lst)
        {
            gvConceptos.DataSource = null;
            gvConceptos.DataSource = lst;
            gvConceptos.DataBind();
            if (gvConceptos.Rows.Count > 0)
            {
                gvConceptos.UseAccessibleHeader = true;
                gvConceptos.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void gvConceptos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Page")
                    return;
                if (e.CommandName == "delete")
                {
                    //int index = Convert.ToInt32(e.CommandArgument);
                    //List<Entities.Conceptos> lst = (List<Entities.Conceptos>)Session["Detalle"];
                    //lst.RemoveAt(index);
                    //Session["Detalle"] = lst;
                    //fillGrillaConceptos(lst);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvConceptos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvConceptos.PageIndex = e.NewPageIndex;
        }

        protected void gvConceptos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            int index = Convert.ToInt32(e.RowIndex);
            List<Entities.Conceptos> lst = (List<Entities.Conceptos>)Session["Detalle"];
            lst.RemoveAt(index);
            Session["Detalle"] = lst;
            fillGrillaConceptos(lst);

        }
    }
}




//protected void ImportExcel()
//{
//    //Use Library Open the Excel file using ClosedXML.
//    //string archivo = uploadFile(fUploadConceptos, "archivos");
//    ////string nombrearchivo = System.IO.Path.GetFullPath(Server.MapPath(archivo));
//    //string path = Server.MapPath("~/archivos/" + nombrearchivo);
//    //using (XLWorkbook workBook = new XLWorkbook(fUploadConceptos.PostedFile.InputStream))
//    string nombreArchivo = uploadFile(fUploadConceptos, "archivos");
//    string path = Server.MapPath("archivos/" + nombreArchivo);
//    List<Entities.Conceptos> lst = new List<Entities.Conceptos>();
//    Entities.Conceptos eConcepto;//= new Entities.Liquidacion();
//    using (XLWorkbook workBook = new XLWorkbook(path))
//    {
//        //Read the first Sheet from Excel file.
//        IXLWorksheet workSheet = workBook.Worksheet(1);

//        //Create a new DataTable.
//        DataTable dt = new DataTable();

//        //Loop through the Worksheet rows.
//        bool firstRow = true;
//        foreach (IXLRow row in workSheet.Rows())
//        {
//            //Use the first row to add columns to DataTable.
//            if (firstRow)
//            {
//                foreach (IXLCell cell in row.Cells())
//                {
//                    dt.Columns.Add(cell.Value.ToString());

//                }
//                firstRow = false;
//            }
//            else
//            {
//                //Add rows to DataTable.
//                dt.Rows.Add();
//                int i = 0;
//                foreach (IXLCell cell in row.Cells())
//                {
//                    dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
//                    i++;
//                }
//            }
//        }
//        gvConceptos.DataSource = dt;
//        gvConceptos.DataBind();
//    }
//}