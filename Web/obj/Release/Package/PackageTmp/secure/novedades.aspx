<%@ Page Title="" Language="C#" MasterPageFile="~/MP/Bootstrap.Master" AutoEventWireup="true" CodeBehind="novedades.aspx.cs" Inherits="web.secure.novedades"
    UICulture="es" Culture="es-MX" %>

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

    <%-- <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.19/css/jquery.dataTables.css" />--%>

    <div class="row" style="margin-top: 100px;">
        <div class="col-md-10 col-md-offset-1">
            <div class="row" style="margin-top: 1px; padding-top: 1px">
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
        </div>
        <div class="col-md-10 col-md-offset-1">
            <div class="panel panel-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">&nbsp;&nbsp;Carga de Novedades de Empleados</h3>
                    <p>&nbsp;</p>
                </div>
                <%-- <asp:UpdatePanel ID="UpdatePanelDatos" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>--%>
                <div class="row">
                    <div class="col-md-10 col-md-offset-1">
                        <div class="box-body">
                            <div class="form-group">
                                <div class="box-body" style="padding-left: 20px; padding-right: 20px;">
                                    <div class="row">
                                        <div class="form-group col-md-4">
                                            <label>Año : </label>
                                            <asp:TextBox ID="txtAnio" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAnio"
                                                Text="*" ForeColor="Red" Display="Dynamic"
                                                ErrorMessage="Debe Ingresar Año" ValidationGroup="Validation1">*</asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="El valor debe ser de Tipo Numerico"
                                                Text="*" ForeColor="Red" Display="Dynamic"
                                                Operator="DataTypeCheck" Type="Integer" ControlToValidate="txtAnio"
                                                ValidationGroup="Validation1">*</asp:CompareValidator>
                                        </div>

                                        <div class="form-group col-md-4">
                                            <label>Tipo Liquidacion : </label>
                                            <asp:DropDownList ID="txtTipo_liq" runat="server" CssClass="form-control"
                                                AppendDataBoundItems="True" OnSelectedIndexChanged="txtTipo_liq_SelectedIndexChanged"
                                                AutoPostBack="True">
                                                <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTipo_liq"
                                                Text="*" ForeColor="Red" Display="Dynamic" InitialValue="0"
                                                ErrorMessage="Debe Ingresar TipoLiquidacion" ValidationGroup="Validation1">*</asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtTipo_liq"
                                                Text="*" ForeColor="Red" Display="Dynamic"
                                                ErrorMessage="El valor debe ser Numerico" Operator="DataTypeCheck" Type="Integer" ValidationGroup="Validation1">*</asp:CompareValidator>
                                        </div>

                                        <div class="form-group col-md-4">
                                            <label>Nro Liquidacion : </label>
                                            <asp:DropDownList ID="txtNro_liq" runat="server" CssClass="form-control"
                                                AppendDataBoundItems="True"
                                                OnSelectedIndexChanged="txtNro_liq_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                ControlToValidate="txtNro_liq" ErrorMessage="Debe Ingresar Nro Liquidacion"
                                                Text="*" ForeColor="Red" Display="Dynamic" InitialValue="0"
                                                ValidationGroup="Validation1">*</asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator3" runat="server"
                                                ControlToValidate="txtNro_liq" ErrorMessage="El valor debe ser Numerico"
                                                Text="*" ForeColor="Red" Display="Dynamic"
                                                Operator="DataTypeCheck" Type="Integer" ValidationGroup="Validation1">*</asp:CompareValidator>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <label>Cod Concepto:</label>
                                            <div class="input-group">
                                                <asp:TextBox ID="txtCod_concepto" CssClass="form-control" runat="server"
                                                    OnTextChanged="txtCod_concepto_TextChanged"
                                                    AutoPostBack="true"></asp:TextBox>
                                                <%--<span class="input-group-btn">
                                                    <br />
                                                </span>--%>
                                                <span class="input-group-btn">
                                                    <button class="btn btn-info" type="button" id="btnBuscar" runat="server"
                                                        onserverclick="btnBuscar_ServerClick">
                                                        <span class="fa fa-search"></span>Buscar</button>
                                                </span>
                                            </div>
                                        </div>

                                        <div class="form-group col-md-6">
                                            <label>Concepto</label>
                                            <asp:TextBox ID="txtConcepto" CssClass="form-control" runat="server" placeholder="Seleccione Concepto">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <asp:ValidationSummary ID="Validation1" ForeColor="red" runat="server" />
                        </div>
                        <div class="box-footer" style="text-align: right;">
                            <div class="form-group">
                                <div class="col-md-4">
                                    <%-- <div class="input-group">
                                    <input type="text" class="form-control" placeholder="Buscar por nombre" id="q" onkeyup="load(1);" />
                                    <span class="input-group-btn">
                                        <button class="btn btn-default" type="button" onclick="load(1);"><i class="fa fa-search"></i></button>
                                    </span>
                                </div>--%>
                                </div>
                                <div class="col-md-8">
                                    <div class="btn-group pull-right">
                                        <asp:LinkButton ID="lbtnCargar_legajos" CssClass="btn btn-app btn-bitbucket" runat="server" OnClick="btnCargar_Legajos_ServerClick">
                                            <i class="fa fa-plus"></i> Cargar Legajos
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnEliminaTodos" CssClass="btn btn-app bg-green-active" runat="server" OnClick="lbtnEliminaTodos_Click"
                                            OnClientClick="return confirm('Desea Eliminar los Legajos');">
                                            <i class="fa fa-eraser"></i> Eliminar Todos Legajos
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnSalir" CssClass="btn btn-app bg-orange margin" runat="server" OnClick="lbtnSalir_Click">
                                            <i class="fa fa-sign-out"></i> Salir
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="text-align: left;">
                            <div class="col-md-12">
                                <hr />
                                <h4 class="modal-title">Vista de Conceptos</h4>
                            </div>
                        </div>
                        <div class="row">
                            <!-- ////////////////////////////// GRILLA DETALLE /////////////////////////////////////// -->
                            <%--<div class="col-md-10 col-md-offset-1">--%>
                            <asp:UpdatePanel ID="PanelDetalle" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="lbtnExporCtaCte" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="auto-style1" style="margin-top: 10px;">
                                        <asp:GridView ID="gvDetalle" runat="server" AutoGenerateColumns="False" Width="100%"
                                            CssClass="table"
                                            AllowPaging="True"
                                            PageSize="10"
                                            EmptyDataText="No hay detalle agregado!!!"
                                            OnPageIndexChanging="gvDetalle_PageIndexChanging"
                                            OnRowCommand="gvDetalle_RowCommand" GridLines="Horizontal"
                                            OnRowCreated="gvDetalle_RowCreated" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
                                            BorderWidth="1px" CellPadding="4" ForeColor="Black"
                                            DataKeyNames="legajo,cod_concepto_liq,nro_parametro">
                                            <Columns>
                                                <asp:BoundField HeaderText="Legajo" DataField="legajo">
                                                    <ControlStyle Width="400px" />
                                                    <%--<HeaderStyle HorizontalAlign="Left" BackColor="#d9edf7" />--%>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                                    <%--<HeaderStyle BackColor="#d9edf7" />--%>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="cod_concepto_liq" HeaderText="Codigo" />
                                                <asp:BoundField DataField="concepto" HeaderText="Concepto" />
                                                <asp:BoundField DataField="nro_parametro" HeaderText="Nro Parametro">
                                                    <%--<HeaderStyle BackColor="#d9edf7" />--%>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="valor_parametro" HeaderText="Valor/Monto"
                                                    DataFormatString="{0:C}">
                                                    <%--<HeaderStyle BackColor="#d9edf7" />--%>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Accion">
                                                    <%--<HeaderStyle BackColor="#d9edf7" />--%>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbDelete" runat="server" CommandName="deleterow"
                                                            ImageUrl="~/App_Themes/Tema1/Images/delete.gif"
                                                            OnClientClick="return confirm('¿Está seguro de eliminar este registro?');"
                                                            CausesValidation="False" />
                                                        <asp:ImageButton ID="imgbEdit" runat="server" CommandName="editrow"
                                                            ImageUrl="~/App_Themes/Tema1/Images/editar.gif"
                                                            CausesValidation="False" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                            <HeaderStyle HorizontalAlign="Left" BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                            <%--<PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />--%>
                                            <PagerStyle CssClass="gridview"></PagerStyle>
                                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                            <SortedDescendingHeaderStyle BackColor="#242121" />
                                        </asp:GridView>
                                    </div>
                                    <div class="row" style="text-align: right;">
                                        <div class="col-xs-9">
                                            <h4 class="modal-title"><strong>
                                                <asp:Label ID="lblTituloInforme" Font-Bold="true" Font-Size="18" ForeColor="GrayText" runat="server" Text="Totales">
                                                </asp:Label>
                                            </strong></h4>
                                        </div>
                                        <div class="col-xs-3" style="padding-right: 30px; text-align: right;">
                                            <asp:Label ID="Label4" Font-Bold="true" Font-Size="12" ForeColor="GrayText"
                                                runat="server" Text="Total:  ">
                                            </asp:Label>
                                            <asp:Label ID="lblTotal" Font-Bold="true" Font-Size="12"
                                                ForeColor="GrayText" runat="server" Text=""></asp:Label>
                                            <br />
                                            <asp:Label ID="Label5" Font-Bold="true" Font-Size="12" ForeColor="GrayText"
                                                runat="server" Text="Cant.Mov.:  ">
                                            </asp:Label><asp:Label ID="lblCantReg" Font-Bold="true" Font-Size="12"
                                                ForeColor="GrayText" runat="server" Text=""></asp:Label>
                                            <br />
                                        </div>
                                    </div>
                                    <!-- ////////////////////////////////// BOTONES ////////////////////////////////////////////// -->
                                    <div class="box-footer clearfix" style="text-align: right;">
                                        <div class="btn-group pull-right">
                                            <asp:LinkButton ID="lbtnConfirma" CssClass="btn btn-app btn-primary" runat="server" OnClick="btnConfirma_Click">
                                                <i class="fa fa-archive"></i> Confirma
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lbtnExporCtaCte" CssClass="btn btn-app bg-green-active" runat="server" OnClick="btnExporCtaCte_Click">
                                                 <i class="fa fa-expand"></i> Excel
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lbtnAgregarConceptos" CssClass="btn btn-app btn-link" runat="server" OnClick="btnExporCtaCte_Click">
                                                 <i class="fa fa-plus"></i> Excel
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lbtnCargar_Legajos_2" CssClass="btn btn-app btn-bitbucket" runat="server" OnClick="btnCargar_Legajos_ServerClick">
                                                 <i class="fa fa-plus"></i> Cargar Legajos
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lbtnSalir2" CssClass="btn btn-app bg-orange margin" runat="server" OnClick="lbtnSalir_Click">
                                                 <i class="fa fa-sign-out"></i> Salir
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <%--</div>--%>
                        </div>
                    </div>
                </div>
            </div>
            <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
        </div>
    </div>
    <!-- ///////////////////////////////////////////////////////////////////////////////////// -->
    <!-- ////////////////////////////// GRILLA BUSQUEDA CONCEPTO////////////////////////////// -->
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:Button ID="Button1" runat="server" Text="Button" Style="visibility: hidden;" />
    <ajaxToolkit:ModalPopupExtender ID="popUpConcepto" runat="server"
        PopupControlID="Div1"
        TargetControlID="Button1"
        BackgroundCssClass="backgroundColor">
    </ajaxToolkit:ModalPopupExtender>
    <div style="padding: 10px; width: 60%; background-color: White; box-shadow: 0px 0px 10px #000;" id="Div1" runat="server">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-header">
                    <button type="button"
                        runat="server"
                        id="btnCloseModalConcepto"
                        onserverclick="btnCloseModalConcepto_ServerClick"
                        class="close" data-dismiss="modal"
                        aria-label="Close">
                        <span aria-hidden="true">×</span></button>
                    <h4 class="modal-title">
                        <asp:Label ID="Label1" runat="server" Text="Buscar x Concepto"></asp:Label>
                    </h4>
                </div>

                <div class="input-group">
                    <input type="text" class="form-control" id="txtInput" runat="server" />
                    <span class="input-group-btn">
                        <button class="btn btn-facebook" type="button" id="cmdBuscar" runat="server" onserverclick="cmdBuscar_ServerClick"
                            causesvalidation="False">
                            <span class="fa fa-search"></span>Buscar</button>
                    </span>
                </div>
                <br />
                <div style="overflow: scroll; height: 150px;">
                    <asp:GridView ID="grdConceptos" runat="server" AutoGenerateColumns="False"
                        CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="table"
                        OnRowCommand="grdConceptos_RowCommand" OnRowCreated="grdConceptos_RowCreated" formnovalidate="" DataKeyNames="cod_concepto, des_concepto_liq">
                        <AlternatingRowStyle BackColor="White" />
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
                        <EditRowStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#E3EAEB" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F8FAFA" />
                        <SortedAscendingHeaderStyle BackColor="#246B61" />
                        <SortedDescendingCellStyle BackColor="#D4DFE1" />
                        <SortedDescendingHeaderStyle BackColor="#15524A" />
                    </asp:GridView>
                </div>
                <div class="box-footer clearfix" style="text-align: right;">
                    <asp:Button ID="cmdCancelar" runat="server" Text="Cancelar" class="btn btn-primary"
                        OnClick="cmdCancelar_Click" CausesValidation="False" />
                </div>


            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- ///////////////////////////////////////////////////////////////////////////////////// -->
    <!-- ////////////////////////////// POPUP DETALLE LEGAJOS //////////////////////////////// -->
    <asp:HiddenField ID="HiddenField3" runat="server" />
    <asp:Button ID="Button2" runat="server" Text="Button" Style="visibility: hidden;" />
    <ajaxToolkit:ModalPopupExtender ID="popUpDetalleLegajos" runat="server"
        PopupControlID="modalAddItems"
        BehaviorID="popUpDetalleLegajos"
        TargetControlID="Button2"
        OkControlID="btnAceptar"
        BackgroundCssClass="backgroundColor">
    </ajaxToolkit:ModalPopupExtender>
    <div style="padding: 10px; width: 50%; background-color: White; box-shadow: 0px 0px 10px #000;"
        id="modalAddItems"
        runat="server"
        class="panel panel-info">
        <div class="panel-body">
            <asp:UpdatePanel ID="UpdatePanelLegajo" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-header">
                        <button type="button"
                            runat="server"
                            id="btnCloseModal"
                            onserverclick="btnCloseModal_ServerClick"
                            class="close" data-dismiss="modal"
                            aria-label="Close">
                            <span aria-hidden="true">×</span></button>
                        <h4 class="modal-title">
                            <asp:Label ID="lblTituloFormModal" runat="server" Text="Agregar Legajos"></asp:Label>
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

                    <div class="form-group">
                        <label>Legajo</label>
                        <br />
                        <asp:TextBox ID="txtLegajo" runat="server" Width="50%" autocomplete="false"
                            placeholder="Ingrese Legajo" AutoPostBack="True" CssClass="form-control"
                            OnTextChanged="txtLegajo_TextChanged"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator6" runat="server" ValidationGroup="Validation2"
                            ControlToValidate="txtLegajo" ErrorMessage="Debe Ingresar Legajo" Type="Integer"
                            Text="*" ForeColor="Red" Display="Dynamic"
                            Operator="DataTypeCheck">*</asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                            ControlToValidate="txtLegajo" ErrorMessage="Debe ingresar Legajo" SetFocusOnError="True"
                            Text="*" ForeColor="Red" Display="Dynamic"
                            ValidationGroup="Validation2">*</asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label>Nombre</label>
                        <br />
                        <asp:TextBox ID="txtNombre" runat="server" autocomplete="false" Width="90%"
                            placeholder="Nombre" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Valor o Monto</label>
                        <br />
                        <asp:TextBox ID="txtValor" runat="server" Width="90%" autocomplete="false" CssClass="form-control"
                            placeholder="Valor / Monto"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator5" runat="server" ValidationGroup="Validation2"
                            ControlToValidate="txtValor" ErrorMessage="Debe Ingresar Valor" Type="Double"
                            Text="*" ForeColor="Red" Display="Dynamic"
                            Operator="DataTypeCheck">*</asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                            ControlToValidate="txtValor" ErrorMessage="Debe ingresa Valor"
                            Text="*" ForeColor="Red" Display="Dynamic"
                            SetFocusOnError="True" ValidationGroup="Validation2">*</asp:RequiredFieldValidator>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-12">
                            <label>Observacion</label>
                            <asp:TextBox ID="txtObs" runat="server" TextMode="MultiLine" CssClass="form-control"
                                placeholder="Texto"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                ControlToValidate="txtObs" ErrorMessage="Debe ingresa Observaciones" Display="Dynamic"
                                ValidationGroup="Validation2">*</asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="modal-footer">
                        <asp:ValidationSummary ID="Validation2" runat="server" ForeColor="Red" />
                        <br />
                        <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">--%>
                        <%--<ContentTemplate>--%>
                        <div class="modal-footer">
                            <asp:Button ID="btnAceptar" runat="server"
                                CssClass="btn btn-primary" Text="Aceptar"
                                OnClick="btnAceptar_ServerClick" ValidationGroup="Validation2" />
                            <asp:Button ID="btnCancelarModal" runat="server"
                                CssClass="btn btn-default" Text="Cancelar"
                                OnClick="btnCancelar_ServerClick" />

                        </div>
                        <%--</ContentTemplate>--%>
                        <%--</asp:UpdatePanel>--%>
                    </div>
                    </fieldset>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <!-- ///////////////////////////////////////////////////////////////////////////////////// -->

</asp:Content>
