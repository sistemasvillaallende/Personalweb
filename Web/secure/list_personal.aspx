<%@ Page Title="" Language="C#" MasterPageFile="~/MP/Bootstrap.Master" AutoEventWireup="true" CodeBehind="list_personal.aspx.cs"
    Inherits="web.secure.list_personal" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <div class="col-md-10 col-md-offset-1">
            <div class="box-header">
            </div>
            <div class="box-body" style="margin-top: 80px; overflow-x: hidden;">
                <h4>Listados Varios</h4>
                <div class="form-group">
                    <div class="col-md-12">
                        <div class="box">
                            <div class="box-header">
                                <h4 runat="server" id="lblVecino"></h4>
                                <hr style="border-top: 1px solid #d2d6de;" />
                            </div>
                            <div class="box-body">
                                <ul class="list-group">
                                    <li class="list-group-item active"><a runat="server" id="toNominaPersonal" onserverclick="toNominaPersonal_ServerClick">Nomina Personal</a></li>
                                    <li class="list-group-item"><a runat="server" id="toPersonalExcel" onserverclick="toPersonalExcel_ServerClick">Nomina de Personal Activos(Excel Completo)</a></li>
                                    <li class="list-group-item"><a runat="server" id="toPersonalExcelTodos" onserverclick="toPersonalExcelTodos_ServerClick">Nomina de Personal Todos(Excel Completo)</a></li>
                                    <li class="list-group-item"><a runat="server" id="toPersonalxSeccion" onserverclick="toPersonalxSeccion_ServerClick">Nomina Personal por Seccion</a></li>
                                    <li class="list-group-item"><a runat="server" id="toPersonalxSeccionExcel" onserverclick="toPersonalxSeccionExcel_ServerClick">Nomina Personal por Seccion(Excel) </a></li>
                                    <li class="list-group-item"><a runat="server" id="ToCuentaporConceptos" onserverclick="ToCuentaporConceptos_ServerClick">Cuentas Por Concepto</a></li>
                                    <li class="list-group-item"><a runat="server" id="toCuentaSueldoyGtos" onserverclick="toCuentaSueldoyGtos_ServerClick">Cuenta, Sueldo Basico. y Gtos Rep.</a></li>
                                    <%--<li class="list-group-item"><a runat="server" id="toFamiliares" onserverclick="toSettings_ServerClick">Familiares a Cargo</a></li>
                                    <li class="list-group-item"><a runat="server" id="toEMpleadosDatosCompletos" onserverclick="toSettings_ServerClick">Empleado y Nro. Caja de Ahorro</a></li>--%>
                                    <li class="list-group-item"><a runat="server" id="toNominaArt" onserverclick="toNominaArt_ServerClick">Nomina ART (Excel)</a></li>
                                </ul>
                            </div>
                        </div>

                        <div class="box-footer">
                            <div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" style="text-align: right;">
                        <a href="listempleados.aspx" class="btn btn-warning">Volver</a>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div id="ContentPlaceHolder1_ValidationSummary1" style="color: Red; display: none;">
                </div>
            </div>
        </div>
    </div>
    <asp:Button ID="Button2" runat="server" Text="Button" Style="visibility: hidden;" />
    <ajaxToolkit:ModalPopupExtender runat="server"
        BackgroundCssClass="modalBackground"
        PopupControlID="modalReporte"
        BehaviorID="popUpListado"
        TargetControlID="Button2"
        ID="popUpListado">
    </ajaxToolkit:ModalPopupExtender>
    <div class="row" id="modalReporte" style="background-color: White; width: 70%; padding: 15px; border-radius: 12px; padding-top: 0px;">
        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="modal-header" style="background-color: #3587B2;">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true" runat="server"
                            id="btnCloseListado" onserverclick="btnCloseListado_ServerClick">
                            ×</button>
                        <h2 style="color: white">Reportes</h2>
                    </div>
                </div>
                <div runat="server" id="divReporte">
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>


