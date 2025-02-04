<%@ Page Language="C#" MasterPageFile="~/MP/MasterNew.Master"  AutoEventWireup="true" CodeBehind="Concepto_Liq_x_Emp_Mov.aspx.cs" Inherits="web.secure.Concepto_Liq_x_Emp_Mov" %>

    <%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

        <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
            <link href="../App_Themes/dist/css/AdminLTE.min.css" rel="stylesheet" />
        </asp:Content>

        <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

            <div class="row" style="margin-top: 35px; padding-top: 25px">
                <div class="col-md-8 col-md-offset-2">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel-heading" style="height: 40px;">
                                <h4>Empleado</h4>
                            </div>
                            <div class="row">
                                <asp:UpdatePanel ID="PanelInfomacion" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="alert alert-warning alert-success" runat="server"
                                            id="divInformacion" visible="false" role="alert">
                                            <button type="button" class="close" data-dismiss="alert"
                                                onclick="__doPostBack('<%=PanelInfomacion.ClientID%>', 'Informacion');">
                                                <span arial-hidden="true">&times;</span> <span
                                                    class="sr-only">Cerrar</span>
                                            </button>
                                            <strong>Aviso Importante! </strong>
                                            <p id="msjInformacion" runat="server">
                                            </p>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                                <asp:UpdatePanel ID="PanelError" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="alert alert-warning alert-danger" runat="server" id="divError"
                                            visible="false" role="alert">
                                            <button type="button" class="close" data-dismiss="alert"
                                                onclick="__doPostBack('<%=PanelError.ClientID%>', 'Error');">
                                                <span arial-hidden="true">&times;</span> <span
                                                    class="sr-only">Cerrar</span>
                                            </button>
                                            <strong>Aviso Importante! </strong>
                                            <p id="msjError" runat="server">
                                            </p>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="panel panel-primary">
                                <asp:UpdatePanel ID="UpdatePanelDatos" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>

                                        <div class="panel-body">
                                            <div class="form-group">
                                                <div class="col-md-8  col-md-offset-1">
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <label>
                                                                Legajo :
                                                            </label>
                                                            <asp:TextBox ID="txtLegajo" runat="server"
                                                                CssClass="input-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                                                runat="server" ControlToValidate="txtLegajo"
                                                                ErrorMessage="Debe Ingresar Legajo"
                                                                ValidationGroup="Validation1">*
                                                            </asp:RequiredFieldValidator>
                                                            <asp:CompareValidator ID="CompareValidator2" runat="server"
                                                                ErrorMessage="El valor debe ser de Tipo Numerico"
                                                                Operator="DataTypeCheck" Type="Integer"
                                                                ControlToValidate="txtLegajo"
                                                                ValidationGroup="Validation1">*</asp:CompareValidator>
                                                        </div>

                                                        <div class="col-md-6">
                                                            <label>
                                                                Nombre :
                                                            </label>
                                                            <asp:TextBox ID="txtNombre" runat="server"
                                                                CssClass="input-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                                                runat="server" ControlToValidate="txtNombre"
                                                                ErrorMessage="Debe Ingresar Nombre"
                                                                ValidationGroup="Validation1">*
                                                            </asp:RequiredFieldValidator>
                                                            <asp:CompareValidator ID="CompareValidator1" runat="server"
                                                                ErrorMessage="El valor debe ser de Tipo Numerico"
                                                                Operator="DataTypeCheck" Type="Integer"
                                                                ControlToValidate="txtNombre"
                                                                ValidationGroup="Validation1">*</asp:CompareValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <asp:ValidationSummary ID="Validation1" ForeColor="red"
                                                    runat="server" />
                                            </div>
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- ///////////////////////////////////////////////////////////////////////////////////////// -->
                <!-- ////////////////////////////// DETALLE ////////////////////////////////////////////////// -->
                <div class="row">
                    <div class="col-md-12 col-md-offset-2">
                        <div class="panel-heading" style="height: 40px;">
                            <h4>Conceptos del Empleado Movimientos</h4>
                        </div>
                        <div class="row">

                            <div class="col-md-12">
                            
                                <!-- ///////////////////////////////////////////////////////////////////////////////////// -->
                                <!-- ////////////////////////////// GRILLA DETALLE /////////////////////////////////////// -->
                                <div class="col-md-12">
                                    <asp:UpdatePanel ID="PanelDetalle" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="lbtnExporCtaCte" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <div class="table-informacion">
                                                <asp:GridView ID="gvDetalle" runat="server" AutoGenerateColumns="False"
                                                    Width="100%" CssClass="table table-hover table-bordered"
                                                    EmptyDataText="No hay detalle agregado!!!" GridLines="Horizontal"
                                                    OnRowCommand="gvDetalle_RowCommand"
                                                    OnRowCreated="gvDetalle_RowCreated" BackColor="White"
                                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                                    CellPadding="4" ForeColor="Black"
                                                    DataKeyNames="legajo,cod_concepto_liq">
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Codigo"
                                                            DataField="cod_concepto_liq">
                                                            <ControlStyle Width="400px" />
                                    
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="des_concepto_liq"
                                                            HeaderText="Concepto">
                                         
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="valor_concepto_liq"
                                                            HeaderText="Valor Concepto">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="fecha_vto"
                                                            HeaderText="Fecha Vencimiento">
                                                 
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                    <HeaderStyle HorizontalAlign="Left" BackColor="#333333"
                                                        Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="White" ForeColor="Black"
                                                        HorizontalAlign="Right" />
                                                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True"
                                                        ForeColor="White" />
                                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                    <SortedDescendingHeaderStyle BackColor="#242121" />
                                                </asp:GridView>
                                            </div>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="btn-group pull-right">
                                    <asp:LinkButton ID="lbtnSalir" CssClass="btn-control volver" runat="server"
                                        OnClick="lbtnSalir_Click">
                                        <i class="fa fa-sign-out"></i> Volver
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

               
                <!-- ///////////////////////////////////////////////////////////////////////////////////// -->


            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:Button ID="Button6" runat="server" Text="Button" Style="visibility: hidden;" />
            <ajaxToolkit:ModalPopupExtender runat="server" BackgroundCssClass="modalBackground"
                PopupControlID="modalAuditoria" BehaviorID="popUpAuditoria" TargetControlID="Button6"
                ID="popUpAuditoria">
            </ajaxToolkit:ModalPopupExtender>
            <div class="modal-dialog" runat="server" style="background-color: white; padding: 20px;"
                id="modalAuditoria">
                <div class="">
                    <div class="modal-header">
                        <button type="button" runat="server" id="btnCloseModalAuditoria"
                            onserverclick="btnCloseModalAuditoria_ServerClick" class="close" data-dismiss="modal"
                            aria-label="Close">
                            <span aria-hidden="true">×</span></button>
                        <h4 class="panel-heading">Auditoria del Sistema</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <div class="modal-body">
                                        <asp:TextBox runat="server" Rows="6" TextMode="MultiLine"
                                            CssClass="form-control" autocomplete="false"
                                            placeholder="Ingrese el motivo de la Alta/Modificación/Eliminación"
                                            ID="txtObservAuditoria"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="row">
                            <asp:LinkButton OnClick="btnAceptarAuditoria_Click" ID="btnAceptarAuditoria"
                                CssClass="btn btn-primary" runat="server">
                                Aceptar
                                <br />
                                <span class="fa fa-save" style="font-size: 22px;"></span>
                            </asp:LinkButton>
                            <asp:LinkButton OnClick="btnCancelarAuditoria_Click" ID="btnCancelarAuditoria"
                                CssClass="btn btn-warning" CausesValidation="False" runat="server">
                                Salir&nbsp;&nbsp;&nbsp;&nbsp;
                                <br />
                                <span class="fa fa-sign-out" style="font-size: 22px;"></span>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>


            <script type="text/javascript">
                function printDiv(nombreDiv) {
                    var contenido = document.getElementById(nombreDiv).innerHTML;
                    var contenidoOriginal = document.body.innerHTML;

                    document.body.innerHTML = contenido;

                    window.print();

                    document.body.innerHTML = contenidoOriginal;
                }
            </script>

            <script type="text/javascript"
                src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
            <script type="text/javascript" src="../js/MaxLength.min.js"></script>
            <script type="text/javascript">
                $(function () {
                    //Specifying the Character Count control explicitly
                    $("[id*=txtObservaciones.ClientID]").MaxLength(
                        {
                            MaxLength: 300,
                            CharacterCountControl: $('#counter')
                        });
                    //Disable Character Count
                    //$("[id*=TextBox3]").MaxLength(
                    //{
                    //    MaxLength: 20,
                    //    DisplayCharacterCount: false
                    //});
                });
            </script>

            <%-- <script type="text/javascript">
                document.addEventListener("DOMContentLoaded", function () {
                var textBox = document.getElementById("<%= txtObservaciones.ClientID %>");
                    textBox.addEventListener("input", function () {
                    if (this.value.length > 100) {
                    this.value = this.value.slice(0, 100);
                    }
                    });
                    });
                    </script>--%>
        </asp:Content>