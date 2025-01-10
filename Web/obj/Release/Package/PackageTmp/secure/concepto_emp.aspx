<%@ Page Title="" Language="C#" MasterPageFile="~/MP/Bootstrap.Master" AutoEventWireup="true" CodeBehind="concepto_emp.aspx.cs" Inherits="web.secure.concepto_emp" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../App_Themes/dist/css/AdminLTE.min.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            position: relative;
            min-height: 1px;
            float: left;
            width: 100%;
            left: 0px;
            top: 0px;
            padding-left: 15px;
            padding-right: 15px;
        }

        .auto-style2 {
            display: block;
            width: 100%;
            height: 34px;
            padding: 6px 12px;
            font-size: 14px;
            line-height: 1.42857143;
            color: #555;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            -webkit-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
            left: 0px;
            top: 1px;
        }
    </style>
    <style type="text/css">
        .gridview {
            background-color: #fff;
            height: 60px;
            padding: 2px;
            margin: 4% auto;
        }

            .gridview a {
                margin: 5px;
                border-radius: 50%;
                background-color: #444;
                padding: 5px 10px 5px 10px;
                color: #fff !important;
                text-decoration: none;
                -o-box-shadow: 1px 1px 1px #111;
                -moz-box-shadow: 1px 1px 1px #111;
                -webkit-box-shadow: 1px 1px 1px #111;
                box-shadow: 1px 1px 1px #111;
            }

                .gridview a:hover {
                    background-color: #1e8d12;
                    color: #fff;
                }

            .gridview span {
                background-color: #ae2676;
                color: #fff;
                /*-o-box-shadow: 1px 1px 1px #111;*/
                -moz-box-shadow: 1px 1px 1px #111;
                -webkit-box-shadow: 1px 1px 1px #111;
                box-shadow: 1px 1px 1px #111;
                border-radius: 50%;
                padding: 5px 10px 5px 10px;
            }
    </style>
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
                                <div class="alert alert-warning alert-success" runat="server" id="divInformacion"
                                    visible="false" role="alert">
                                    <button type="button" class="close" data-dismiss="alert" onclick="__doPostBack('<%=PanelInfomacion.ClientID%>', 'Informacion');">
                                        <span arial-hidden="true">&times;</span> <span class="sr-only">Cerrar</span>
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
                                    <button type="button" class="close" data-dismiss="alert" onclick="__doPostBack('<%=PanelError.ClientID%>', 'Error');">
                                        <span arial-hidden="true">&times;</span> <span class="sr-only">Cerrar</span>
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
                                                    <asp:TextBox ID="txtLegajo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtLegajo" ErrorMessage="Debe Ingresar Legajo" ValidationGroup="Validation1">*</asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="El valor debe ser de Tipo Numerico" Operator="DataTypeCheck" Type="Integer" ControlToValidate="txtLegajo" ValidationGroup="Validation1">*</asp:CompareValidator>
                                                </div>

                                                <div class="col-md-6">
                                                    <label>
                                                        Nombre : 
                                                    </label>
                                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNombre" ErrorMessage="Debe Ingresar Nombre" ValidationGroup="Validation1">*</asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="El valor debe ser de Tipo Numerico" Operator="DataTypeCheck" Type="Integer" ControlToValidate="txtNombre" ValidationGroup="Validation1">*</asp:CompareValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:ValidationSummary ID="Validation1" ForeColor="red" runat="server" />
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
            <div class="col-md-8 col-md-offset-2">
                <div class="panel-heading" style="height: 40px;">
                    <h4>Conceptos del Empleado</h4>
                </div>
                <div class="row">

                    <div class="col-md-12">
                        <div class="col-md-12" style="text-align: right;">
                            <asp:LinkButton ID="lnkAgrega_conceptos" CssClass="btn btn-app btn-bitbucket" runat="server" OnClick="lnkAgrega_conceptos_Click">
                        <i class="fa fa-money"></i> Agregar Concepto
                            </asp:LinkButton>
                        </div>
                        <br />
                        <br />
                        <!-- ///////////////////////////////////////////////////////////////////////////////////// -->
                        <!-- ////////////////////////////// GRILLA DETALLE /////////////////////////////////////// -->
                        <div class="col-md-12">
                            <asp:UpdatePanel ID="PanelDetalle" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="lbtnExporCtaCte" />
                                </Triggers>
                                <ContentTemplate>
                                    <div style="overflow: scroll; height: 300px;">
                                        <asp:GridView ID="gvDetalle" runat="server" AutoGenerateColumns="False" Width="100%"
                                            CssClass="table"
                                            EmptyDataText="No hay detalle agregado!!!"
                                            GridLines="Horizontal" OnRowCommand="gvDetalle_RowCommand" OnRowCreated="gvDetalle_RowCreated" BackColor="White" BorderColor="#CCCCCC"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" DataKeyNames="legajo,cod_concepto_liq">
                                            <Columns>
                                                <asp:BoundField HeaderText="Codigo" DataField="cod_concepto_liq">
                                                    <ControlStyle Width="400px" />
                                                    <%--<HeaderStyle HorizontalAlign="Left" BackColor="#d9edf7" />--%>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="des_concepto_liq" HeaderText="Concepto">
                                                    <%--<HeaderStyle BackColor="#d9edf7" />--%>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="valor_concepto_liq" HeaderText="Valor Concepto">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="fecha_vto" HeaderText="Fecha Vencimiento">
                                                    <%--<HeaderStyle BackColor="#D9EDF7" />--%>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Accion">
                                                    <%--<HeaderStyle BackColor="#d9edf7" />--%>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbDelete" runat="server" CommandName="deleterow"
                                                            ImageUrl="~/App_Themes/Tema1/Images/delete.gif"
                                                            OnClientClick="return confirm('¿Está seguro de eliminar este registro?');"
                                                            CommandArgument=' <%# Container.DataItemIndex %> '
                                                            CausesValidation="False" />
                                                        <asp:ImageButton ID="imgbEdit" runat="server" CommandName="editrow"
                                                            ImageUrl="~/App_Themes/Tema1/Images/editar.gif"
                                                            CausesValidation="False" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                            <HeaderStyle HorizontalAlign="Left" BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                            <SortedDescendingHeaderStyle BackColor="#242121" />
                                        </asp:GridView>
                                    </div>
                                    <br />
                                    <label id="lblCantidad" runat="server" class="form-control" style="text-align: right; background-color: #d9edf7;">
                                        Cantidad: 0</label>
                                    <%--<label id="lblTotal" runat="server" class="form-control" style="text-align: right; background-color: #d9edf7;">
                                Total: $ 0.00</label>--%>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <!-- ////////////////////////////////// BOTONES ////////////////////////////////////////////// -->
                        <div class="col-md-12" style="text-align: right;">
                            <%--<div class="btn-group pull-righ" style="padding-bottom: 20px;">
                                <asp:Button ID="btnConfirma" runat="server" CssClass="btn btn-app btn-primary" Text="Confirmar" OnClick="btnConfirma_Click" />
                                <asp:Button ID="btnExporCtaCte" CssClass="btn btn-app btn-link" runat="server" Text="Exportar a Excel" OnClick="btnExporCtaCte_Click" />
                                <asp:Button ID="btnSalir" runat="server" CssClass="btn btn-app bg-orange margin" Text="Salir" CausesValidation="False" OnClick="btnSalir_Click" />
                            </div>--%>

                            <div class="btn-group pull-right">
                                <asp:LinkButton ID="lbtnConfirma" CssClass="btn btn-app btn-bitbucket" runat="server" OnClick="lbtnConfirma_Click"
                                    OnClientClick="return confirm('Desea Confirmar los Cambios...');">
                                            <i class="fa fa-play-circle"></i> Confirma
                                </asp:LinkButton>
                                <asp:LinkButton ID="lbtnExporCtaCte" CssClass="btn btn-app bg-green-active" runat="server" OnClick="lbtnExporCtaCte_Click">
                                            <i class="fa fa-plus"></i> Excel
                                </asp:LinkButton>
                                <asp:LinkButton ID="lbtnSalir" CssClass="btn btn-app bg-orange margin" runat="server" OnClick="lbtnSalir_Click">
                                            <i class="fa fa-sign-out"></i> Salir
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- ///////////////////////////////////////////////////////////////////////////////////// -->
            <!-- ////////////////////////////// GRILLA BUSQUEDA CONCEPTO////////////////////////////// -->
            <asp:Button ID="Button1" runat="server" Text="Button" Style="visibility: hidden;" />
            <ajaxToolkit:ModalPopupExtender runat="server"
                BackgroundCssClass="backgroundColor"
                PopupControlID="divBusqueda"
                BehaviorID="modalPopupBusqueda"
                TargetControlID="Button1"
                ID="modalPopupBusqueda">
            </ajaxToolkit:ModalPopupExtender>
            <div style="padding: 10px; width: 60%; background-color: White; box-shadow: 0px 0px 10px #000;" id="divBusqueda" runat="server">

                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <fieldset class="register">
                            <legend>Buscar x Concepto</legend>

                            <div class="input-group">
                                <input type="text" class="form-control" id="txtInput" runat="server" />
                                <span class="input-group-btn">
                                    <button class="btn btn-facebook" type="button" id="cmdBuscar" runat="server"
                                        causesvalidation="False">
                                        <span class="fa fa-search"></span>Buscar</button>
                                </span>
                            </div>
                            <br />
                            <div style="overflow: scroll; height: 150px;">
                                <asp:GridView ID="grdConceptos" runat="server" AutoGenerateColumns="False"
                                    CellPadding="4" ForeColor="Black" GridLines="Horizontal"
                                    formnovalidate="" DataKeyNames="cod_concepto,des_concepto_liq" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
                                    BorderWidth="1px" Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="cod_concepto" HeaderText="Codigo" />
                                        <asp:BoundField DataField="des_concepto_liq" HeaderText="Concepto" />
                                        <asp:BoundField DataField="des_tipo_concepto" HeaderText="Tipo Concepto" />
                                        <asp:TemplateField HeaderText="Seleccionar">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbSeleccionar" runat="server" CommandName="selected"
                                                    ImageUrl="~/App_Themes/Tema1/Images/masGrilla.gif" CausesValidation="False" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                    <SortedDescendingHeaderStyle BackColor="#242121" />
                                </asp:GridView>
                            </div>
                            <asp:Button ID="cmdCancelar" runat="server" Text="Cancelar" class="btn btn-primary"
                                CausesValidation="False" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <!-- ///////////////////////////////////////////////////////////////////////////////////// -->
            <!-- ////////////////////////////// POPUP DETALLE LEGAJOS //////////////////////////////// -->
            <asp:HiddenField ID="HiddenField3" runat="server" />
            <asp:Button ID="Button2" runat="server" Text="Button" Style="visibility: hidden;" />
            <ajaxToolkit:ModalPopupExtender runat="server"
                BackgroundCssClass="modalBackground"
                PopupControlID="divModalDetalle"
                BehaviorID="modalPopupDetalle"
                TargetControlID="Button2"
                OkControlID="btnAceptar"
                ID="modalPopupDetalle">
            </ajaxToolkit:ModalPopupExtender>
            <div style="padding: 10px; width: 40%; background-color: White; box-shadow: 0px 0px 10px #000;"
                id="divModalDetalle"
                runat="server"
                class="panel panel-info">
                <%--<div class="panel-heading">
                <h4>Agregar Items</h4>
            </div>--%>
                <div class="panel-body">
                    <asp:UpdatePanel ID="UpdatePanelConcepto" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-header col-md-12">
                                <button type="button"
                                    runat="server"
                                    id="btnCloseModal"
                                    onserverclick="btnCloseModal_ServerClick"
                                    class="close" data-dismiss="modal"
                                    aria-label="Close">
                                    <span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">
                                    <asp:Label ID="lblTituloFormModal" runat="server" Text="Agregar Conceptos"></asp:Label>
                                </h4>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-12">
                                    <div class="alert alert-success alert-dismissible" runat="server" id="divMSJDetalleLegajos"
                                        visible="false" role="alert">
                                        <button type="button" class="close" data-dismiss="alert" onclick="__doPostBack('<%=PanelInfomacion.ClientID%>', 'Alerta');">
                                            <span aria-hidden="true">×</span></button>
                                        </button>
                                <h4>Aviso!</h4>
                                        <p id="msjDetalleLegajo" runat="server">
                                        </p>
                                    </div>

                                </div>
                            </div>
                            <div class="modal-body" id="m1" style="min-height: 320px;">
                                <div class="row">
                                    <div class="form-group col-md-4">
                                        <label>Cod Concepto</label>
                                        <asp:TextBox ID="txtCod_concepto_liq" runat="server" autocomplete="false"
                                            placeholder="Ingrese Codigo" AutoPostBack="True" CssClass="form-control" OnTextChanged="txtCod_concepto_liq_TextChanged"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator6" runat="server" ValidationGroup="cliente"
                                            ControlToValidate="txtCod_concepto_liq" ErrorMessage="Debe Ingresar Legajo" Type="Integer"
                                            Operator="DataTypeCheck" Display="Dynamic">*</asp:CompareValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                            ControlToValidate="txtCod_concepto_liq" ErrorMessage="Debe ingresar Codigo"
                                            Display="Dynamic" ValidationGroup="cliente">*</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-8">
                                        <label>Concepto</label>
                                        <asp:TextBox ID="txtConcepto" runat="server" autocomplete="false"
                                            placeholder="Concepto" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="cliente"
                                            Display="Dynamic" ControlToValidate="txtConcepto" ErrorMessage="Debe Ingresar Concepto">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-md-4">
                                        <label>Valor/Monto</label>
                                        <asp:TextBox ID="txtValor" runat="server" autocomplete="false" CssClass="form-control"
                                            placeholder="Valor / Monto"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator5" runat="server" ValidationGroup="cliente"
                                            ControlToValidate="txtValor" ErrorMessage="Debe Ingresar Valor" Type="Double"
                                            Display="Dynamic" Operator="DataTypeCheck">*</asp:CompareValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                            ControlToValidate="txtValor" ErrorMessage="Debe ingresa Valor" Display="Dynamic"
                                            ValidationGroup="cliente">*</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>Fecha Vto</label>
                                        <asp:TextBox ID="txtFecha_vto" runat="server" autocomplete="false"
                                            placeholder="Fecha Vto." CssClass="form-control"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator4" runat="server" ValidationGroup="cliente" ControlToValidate="txtFecha_vto"
                                            ErrorMessage="Debe Ingresar Fecha_vto" Type="Date" Operator="DataTypeCheck">*</asp:CompareValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtFecha_vto"
                                            ErrorMessage="Debe ingresar Fecha_vto" Display="Dynamic" ValidationGroup="cliente">*</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-4">
                                        &nbsp;
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Motivo/Obs:</label>
                                            <asp:TextBox ID="txtObs" runat="server" TextMode="MultiLine" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Motivo por el cual se realiza este trámite"
                                                ControlToValidate="txtObs" ForeColor="#FF3300" Display="Dynamic" ValidationGroup="Validation2">*</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer col-md-12">
                                    <div class="form-group">
                                        <asp:ValidationSummary ID="Validation2" runat="server" ForeColor="Red" ValidationGroup="cliente" />
                                    </div>
                                    <div class="form-group">
                                        <button type="button" class="btn btn-primary"
                                            runat="server" id="btnAceptar" validationgroup="cliente"
                                            onserverclick="btnAceptar_ServerClick">
                                            Aceptar</button>
                                        <button type="button" class="btn btn-default"
                                            runat="server" id="btnCancelar"
                                            onserverclick="btnCancelar_ServerClick">
                                            Cancelar</button>
                                    </div>
                                </div>
                            </div>
                            <br />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <!-- ///////////////////////////////////////////////////////////////////////////////////// -->
        </div>
    </div>


    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:Button ID="Button6" runat="server" Text="Button" Style="visibility: hidden;" />
    <ajaxToolkit:ModalPopupExtender runat="server"
        BackgroundCssClass="modalBackground"
        PopupControlID="modalAuditoria"
        BehaviorID="popUpAuditoria"
        TargetControlID="Button6"
        ID="popUpAuditoria">
    </ajaxToolkit:ModalPopupExtender>
    <div class="modal-dialog" runat="server" style="background-color: white; padding: 20px;"
        id="modalAuditoria">
        <div class="">
            <div class="modal-header">
                <button type="button"
                    runat="server"
                    id="btnCloseModalAuditoria"
                    onserverclick="btnCloseModalAuditoria_ServerClick"
                    class="close" data-dismiss="modal"
                    aria-label="Close">
                    <span aria-hidden="true">×</span></button>
                <h4 class="panel-heading">Auditoria del Sistema</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <div class="modal-body">
                                <asp:TextBox runat="server" Rows="6"
                                    TextMode="MultiLine"
                                    CssClass="form-control"
                                    autocomplete="false"
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
                        CssClass="btn btn-primary"
                        runat="server">
                        Aceptar
                        <br />
                        <span class="fa fa-save" style="font-size: 22px;"></span>
                    </asp:LinkButton>
                    <asp:LinkButton OnClick="btnCancelarAuditoria_Click" ID="btnCancelarAuditoria"
                        CssClass="btn btn-warning"
                        CausesValidation="False"
                        runat="server">
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

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
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
