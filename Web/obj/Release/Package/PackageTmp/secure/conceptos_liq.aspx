<%@ Page Title="" Language="C#" MasterPageFile="~/MP/Bootstrap.Master" AutoEventWireup="true" CodeBehind="conceptos_liq.aspx.cs" 
    Inherits="web.secure.conceptos_liq" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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



    <!--<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.19/css/jquery.dataTables.css" />-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="uPanelCliente" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="row" style="margin-top: 25px; padding-top: 25px">
                <div class="col-md-12 col-md-offset-0">
                    <div class="col-md-12">

                        <div class="row">
                            <asp:UpdatePanel ID="PanelInfomacion" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="lbtnListado_concepto" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="alert alert-success alert-dismissable" runat="server" id="divConfirma"
                                        visible="false" role="alert">
                                        <button type="button" class="close" data-dismiss="alert" onclick="__doPostBack('<%=PanelInfomacion.ClientID%>', 'Confirma');">
                                            <span aria-hidden="true">×</span></button>
                                        <%--</button>--%>
                                        <h4>Aviso Importante!</h4>
                                        <p id="msjConfirmar" runat="server">
                                        </p>
                                    </div>

                                    <div class="alert alert-warning alert-dismissible" runat="server" id="div1"
                                        visible="false" role="alert">
                                        <button type="button" class="close" data-dismiss="alert" onclick="__doPostBack('<%=PanelInfomacion.ClientID%>', 'Alerta');">
                                            <span aria-hidden="true">×</span></button>
                                        <%-- </button>--%>
                                        <h4>Error!</h4>
                                        <p id="P1" runat="server">
                                        </p>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="alert alert-danger alert-dismissible fade in" role="alert" id="divError"
                                    runat="server" visible="false">
                                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                        <span aria-hidden="true">×</span></button>
                                    <h4>Error!</h4>
                                    <p id="txtError" runat="server">
                                    </p>
                                </div>
                            </div>
                        </div>

                        <div class="box-header with-border">
                            <h3 class="box-title">Conceptos Liquidación</h3>
                        </div>
                        <div class="row">
                            <%--<div class="box-body" style="margin-top: 10px;">--%>
                            <div class="form-group">
                                <div class="col-md-6">
                                    <div class="input-group">
                                        <input type="text" class="form-control" id="txtInput" runat="server" placeholder="Buscar por Concepto" />
                                        <span class="input-group-btn">
                                            <button class="btn btn-info" type="button" id="btnBuscar" runat="server"
                                                onserverclick="btnBuscar_ServerClick">
                                                <span class="fa fa-search"></span>Buscar</button>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="btn-group pull-right">
                                        <asp:LinkButton ID="lbtnNuevo_concepto" CssClass="btn btn-default" runat="server" OnClick="lbtnNuevo_concepto_Click">
                                            <i class="fa fa-plus"></i> Nuevo Concepto.
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnListado_concepto" CssClass="btn btn-default" runat="server" OnClick="lbtnListado_concepto_Click">
                                            <i class="fa fa-list"></i> Listado Ctas x Conceptos.
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnSalir" CssClass="btn btn-default" runat="server" OnClick="lbtnSalir_Click">
                                            <i class="fa fa-sign-out"></i> Salir
                                        </asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                            <%--</div>--%>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <%--<div class="box">--%>
                                <div class="auto-style1" style="margin-top: 20px; overflow: scroll;">
                                    <%--<div class="box-body">--%>
                                    <asp:GridView ID="gvConceptos"
                                        CssClass="table"
                                        runat="server"
                                        OnRowDataBound="gvConceptos_RowDataBound"
                                        OnRowCommand="gvConceptos_RowCommand"
                                        CellPadding="4"
                                        AutoGenerateColumns="False"
                                        ForeColor="#333333"
                                        GridLines="None" DataKeyNames="cod_concepto_liq"
                                        AllowPaging="true"
                                        PageSize="8"
                                        OnPageIndexChanging="gvConceptos_PageIndexChanging">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Codigo" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <p>
                                                        <asp:Label ID="lblCod_concepto_liq" runat="server" Text=""></asp:Label>
                                                    </p>

                                                </ItemTemplate>
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Fecha Alta" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <p>
                                                        <asp:Label ID="lblFecha_alta" runat="server" Text=""></asp:Label>
                                                    </p>

                                                </ItemTemplate>
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Descripcion" ItemStyle-Width="40%">
                                                <ItemTemplate>
                                                    <p>
                                                        <strong>
                                                            <asp:Label ID="lblDescripcion" runat="server" Text=""></asp:Label>
                                                        </strong>
                                                    </p>

                                                </ItemTemplate>
                                                <ItemStyle Width="40%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Tipo Concepto" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <p>
                                                        <strong>
                                                            <asp:Label ID="lblTipo_concepto" runat="server" Text=""></asp:Label>
                                                        </strong>
                                                    </p>
                                                </ItemTemplate>
                                                <ItemStyle Width="20%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Suma" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <p>
                                                        <asp:CheckBox ID="chkSuma" runat="server" />
                                                    </p>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sujeto a Desc" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <p>
                                                        <asp:CheckBox ID="chkSujeto_a_desc" runat="server" />
                                                    </p>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sac" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <p>
                                                        <asp:CheckBox ID="chkSac" runat="server" />
                                                    </p>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Aporte" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <p>
                                                        <asp:CheckBox ID="chkAporte" runat="server" />
                                                    </p>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Remunerativo" ItemStyle-Width="15">
                                                <ItemTemplate>
                                                    <p>
                                                        <asp:CheckBox ID="chkRemunerativo" runat="server" />
                                                    </p>
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <div class="btn-group pull-right">
                                                        <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-expanded="false">Acciones <span class="fa fa-caret-down"></span></button>
                                                        <ul class="dropdown-menu">

                                                            <li>
                                                                <asp:LinkButton
                                                                    ID="lbtnEditar"
                                                                    CommandName="editar"
                                                                    CommandArgument="<%# Container.DataItemIndex %>"
                                                                    runat="server">
                                                                    <i class="fa fa-edit"></i>&nbsp Editar
                                                                </asp:LinkButton>
                                                            </li>

                                                            <li>
                                                                <asp:LinkButton
                                                                    ID="lbtnValores"
                                                                    CommandName="valores"
                                                                    CommandArgument="<%# Container.DataItemIndex %>"
                                                                    runat="server">
                                                                    <i class="fa fa-money"></i>&nbsp Valores
                                                                </asp:LinkButton>
                                                            </li>

                                                            <li>
                                                                <asp:LinkButton
                                                                    ID="lbtnCuentas"
                                                                    CommandName="cuentas"
                                                                    CommandArgument="<%# Container.DataItemIndex %>"
                                                                    runat="server">
                                                                    <i class="fa fa-fax"></i>&nbsp Cuentas
                                                                </asp:LinkButton>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#999999"></EditRowStyle>
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></FooterStyle>
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                        <%--<PagerStyle HorizontalAlign="Center" BackColor="#284775" ForeColor="White"></PagerStyle>--%>
                                        <PagerStyle CssClass="gridview"></PagerStyle>
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                                        <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                                        <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                                        <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                                    </asp:GridView>
                                </div>
                                <%--</div>--%>
                            </div>
                            <!-- /.col -->
                        </div>
                        <!-- /.row -->
                    </div>
                </div>
            </div>

            <asp:HiddenField ID="hID" runat="server" />
            <asp:HiddenField ID="hId_concepto_liq" runat="server" />
            <asp:Button ID="Button1" runat="server" Text="Button" Style="visibility: hidden;" />
            <ajaxToolkit:ModalPopupExtender runat="server"
                DynamicServicePath=""
                BackgroundCssClass="modalBackground"
                PopupControlID="modalConcepto"
                BehaviorID="modalConceptoExtender"
                TargetControlID="Button1"
                ID="modalConceptoExtender">
            </ajaxToolkit:ModalPopupExtender>
            <div class="modal-dialog" id="modalConcepto" runat="server">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button"
                            runat="server"
                            id="btnCloseModal"
                            onserverclick="btnCloseModal_ServerClick"
                            class="close" data-dismiss="modal"
                            aria-label="Close">
                            <span aria-hidden="true">×</span></button>
                        <h4 class="modal-title">
                            <asp:Label ID="lblTituloFormModal" runat="server" Text="Label"></asp:Label>
                        </h4>
                    </div>
                    <div class="modal-body">
                        <div class="nav-tabs-custom">
                            <ul class="nav nav-tabs">
                                <li class="active"><a href="#activity" data-toggle="tab">Datos</a></li>
                            </ul>
                            <div class="tab-content">
                                <div class="active tab-pane" id="activity1" style="min-height: 320px;">
                                    <div class="row">
                                        <div class="form-group col-md-4">
                                            <label>Codigo</label>
                                            <asp:TextBox ID="txtCod_concepto_liq" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="concepto"
                                                Text="*" ForeColor="Red" Display="Dynamic"
                                                ErrorMessage="Ingrese Codigo de Concepto" ControlToValidate="txtCod_concepto_liq">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-8">
                                            <label>Concepto</label>
                                            <asp:TextBox ID="txtConcepto" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="concepto"
                                                Text="*" ForeColor="Red" Display="Dynamic"
                                                ErrorMessage="Ingrese Concepto" ControlToValidate="txtConcepto">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-12">
                                            <label>Tipo Concepto</label>
                                            <asp:DropDownList ID="ddlTipo_concepto" CssClass="form-control" AppendDataBoundItems="true" runat="server">
                                                <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rv1" runat="server" ValidationGroup="concepto"
                                                Text="*" ForeColor="Red" Display="Dynamic"
                                                ErrorMessage="Ingrese Tipo de Concepto" ControlToValidate="ddlTipo_concepto">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-4">
                                            <label>Concepto Suma</label>
                                            <asp:CheckBox ID="chkSuma_" CssClass="form-control" runat="server" />
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>Concepto Sujeto a Desc.</label>
                                            <asp:CheckBox ID="chkSujeto_a_desc_" CssClass="form-control" runat="server" />
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>Concepto Aporte</label>
                                            <asp:CheckBox ID="chkAporte_" CssClass="form-control" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-4">
                                            <label>Concepto Sac</label>
                                            <asp:CheckBox ID="chkSac_" CssClass="form-control" runat="server" />
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>Concepto Remunerativo</label>
                                            <asp:CheckBox ID="chkRemunerativo_" CssClass="form-control" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <!-- /.tab-pane -->
                            </div>
                            <!-- /.tab-content -->
                        </div>
                        <!-- /.nav-tabs-custom -->
                    </div>

                    <div class="modal-footer">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ValidationGroup="concepto" />
                        <asp:Button ID="btnCancelarModal" runat="server"
                            CssClass="btn btn-default" Text="Cancelar"
                            OnClick="btnCancelarModal_Click" />
                        <asp:Button ID="btnCrearConcepto" runat="server"
                            ValidationGroup="concepto"
                            CssClass="btn btn-primary" Text="Aceptar"
                            OnClick="btnCrearConcepto_Click" />
                    </div>

                </div>

            </div>
            <asp:Button ID="Button2" runat="server" Text="Button" Style="visibility: hidden;" />
            <ajaxToolkit:ModalPopupExtender runat="server"
                DynamicServicePath=""
                BackgroundCssClass="modalBackground"
                PopupControlID="modalCuenta"
                BehaviorID="CuentaPopupExtender"
                TargetControlID="Button2"
                ID="CuentaPopupExtender">
            </ajaxToolkit:ModalPopupExtender>
            <div class="modal-dialog" id="modalCuenta" runat="server">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button"
                            runat="server"
                            id="btnCloseModalCuenta"
                            onserverclick="btnCloseModalCuenta_ServerClick"
                            class="close" data-dismiss="modal"
                            aria-label="Close">
                            <span aria-hidden="true">×</span></button>
                        <h4 class="modal-title">Cuenta por Concepto</h4>
                    </div>
                    <div class="modal-body">
                        <div class="nav-tabs-custom">
                            <ul class="nav nav-tabs">
                                <li class="active"><a href="#activity" data-toggle="tab">Datos</a></li>
                            </ul>
                            <div class="tab-content">
                                <div class="active tab-pane" id="activity2" style="min-height: 320px;">
                                    <div class="row">
                                        <div class="form-group col-md-4">
                                            <label>Cod Concepto</label>
                                            <asp:TextBox ID="txtCod_concepto_1" CssClass="form-control" runat="server"
                                                ReadOnly="true">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="cuenta"
                                                Text="*" ForeColor="Red" Display="Dynamic"
                                                ErrorMessage="Ingrese Cod Concepto" ControlToValidate="txtCod_concepto_1">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-8">
                                            <label>Concepto</label>
                                            <asp:TextBox ID="txtConcepto_1" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="cuenta"
                                                Text="*" ForeColor="Red" Display="Dynamic"
                                                ErrorMessage="Ingrese Concepto" ControlToValidate="txtConcepto_1">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-12">
                                            <label>Clasif Personal</label>
                                            <asp:DropDownList ID="ddlClasifPersonal" CssClass="form-control" AppendDataBoundItems="true" runat="server"
                                                OnSelectedIndexChanged="ddlClasifPersonal_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="cuenta"
                                                Text="*" ForeColor="Red" Display="Dynamic"
                                                ErrorMessage="Ingrese Clasificacion del Personal" ControlToValidate="ddlClasifPersonal">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-12">
                                            <label>Tipo Liquidación</label>
                                            <asp:DropDownList ID="ddlTipoLiq" CssClass="form-control" AppendDataBoundItems="true" runat="server"
                                                OnSelectedIndexChanged="ddlTipoLiq_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="cuenta"
                                                Text="*" ForeColor="Red" Display="Dynamic"
                                                ErrorMessage="Ingrese Tipo Liquidación" ControlToValidate="ddlTipoLiq">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="form-group col-md-12">
                                            <label>Nro Cta</label>
                                            <asp:DropDownList ID="ddlNro_cuenta" CssClass="form-control" AppendDataBoundItems="true" runat="server"
                                                OnSelectedIndexChanged="ddlNro_cuenta_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="cuenta"
                                                Text="*" ForeColor="Red" Display="Dynamic"
                                                ErrorMessage="Ingrese Nro de Cuenta" ControlToValidate="ddlNro_cuenta">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-8">
                                            <label>Cuenta</label>
                                            <asp:TextBox ID="txtNro_Cuenta" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="cuenta"
                                                Text="*" ForeColor="Red" Display="Dynamic"
                                                ErrorMessage="Ingrese Cuenta" ControlToValidate="txtNro_Cuenta">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <!-- /.tab-pane -->
                            </div>
                            <!-- /.tab-content -->
                        </div>
                        <!-- /.nav-tabs-custom -->
                    </div>
                    <div class="modal-footer">
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ForeColor="Red" ValidationGroup="cuenta" />
                        <asp:Button ID="btnCancela_cuenta" runat="server"
                            CssClass="btn btn-primary" Text="Cancelar"
                            OnClick="btnCancela_cuenta_Click" />
                        <asp:Button ID="btnAceptar_cuenta" runat="server"
                            ValidationGroup="cuenta"
                            CssClass="btn btn-primary" Text="Aceptar"
                            OnClick="btnAceptar_cuenta_Click" />

                    </div>

                    <div class="modal-footer">
                        <asp:Button ID="cmdNvacuenta" runat="server"
                            CssClass="btn btn-default" Text="Nuevo"
                            OnClick="cmdNvacuenta_Click" />
                        <asp:Button ID="cmdModcuenta" runat="server"
                            CssClass="btn btn-default" Text="Modifica"
                            OnClick="cmdModcuenta_Click" />
                        <asp:Button ID="cmdDelcta" runat="server"
                            CssClass="btn btn-default" Text="Elimina"
                            OnClick="cmdDelcta_Click" />
                    </div>
                </div>
            </div>
            <%-- /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// --%>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="visibility: hidden;" />
            <ajaxToolkit:ModalPopupExtender runat="server"
                DynamicServicePath=""
                BackgroundCssClass="modalBackground"
                PopupControlID="modalValores"
                BehaviorID="ValoresPopupExtender"
                TargetControlID="Button3"
                ID="ValoresPopupExtender">
            </ajaxToolkit:ModalPopupExtender>
            <div class="modal-dialog" id="modalValores" runat="server">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button"
                            runat="server"
                            id="btnCloseModalValor"
                            onserverclick="btnCloseModalValor_ServerClick"
                            class="close" data-dismiss="modal"
                            aria-label="Close">
                            <span aria-hidden="true">×</span></button>
                        <h4 class="modal-title">Valores por Concepto</h4>
                    </div>
                    <div class="modal-body">
                        <div class="nav-tabs-custom">
                            <ul class="nav nav-tabs">
                                <li class="active"><a href="#activity" data-toggle="tab">Datos</a></li>
                            </ul>
                            <div class="tab-content">
                                <div class="active tab-pane" id="activity3" style="min-height: 320px;">
                                    <div class="row">
                                        <div class="form-group col-md-4">
                                            <label>Cod Concepto</label>
                                            <asp:TextBox ID="txtCod_concepto_2" CssClass="form-control" runat="server"
                                                ReadOnly="true">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="valores"
                                                Text="*" ForeColor="Red" Display="Dynamic"
                                                ErrorMessage="Ingrese Cod Concepto" ControlToValidate="txtCod_concepto_2">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-8">
                                            <label>Concepto</label>
                                            <asp:TextBox ID="txtConcepto_2" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="valores"
                                                Text="*" ForeColor="Red" Display="Dynamic"
                                                ErrorMessage="Ingrese Concepto" ControlToValidate="txtConcepto_2">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-12">
                                            <label>Nro Valor</label>
                                            <asp:DropDownList ID="ddlValores" CssClass="form-control" AppendDataBoundItems="true" runat="server"
                                                OnSelectedIndexChanged="ddlValores_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="cuenta"
                                                Text="*" ForeColor="Red" Display="Dynamic"
                                                ErrorMessage="Seleccionar Valor" ControlToValidate="ddlValores">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <label>Valor</label>
                                            <asp:TextBox ID="txtValor" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="valores"
                                                Text="*" ForeColor="Red" Display="Dynamic"
                                                ErrorMessage="Ingrese Valor" ControlToValidate="txtValor">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- /.tab-pane -->
                        </div>
                        <!-- /.tab-content -->


                        <div class="modal-footer">
                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ForeColor="Red" ValidationGroup="valores" />
                            <asp:Button ID="btnCancela_valor" runat="server"
                                CssClass="btn btn-primary" Text="Cancelar"
                                OnClick="btnCancela_valor_Click" />
                            <asp:Button ID="btnAceptar_valor" runat="server"
                                ValidationGroup="valores"
                                CssClass="btn btn-primary" Text="Aceptar"
                                OnClick="btnAceptar_valor_Click" />

                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="cmdNva_valor" runat="server"
                                CssClass="btn btn-default" Text="Nuevo"
                                OnClick="cmdNva_valor_Click" />
                            <asp:Button ID="cmdMod_valor" runat="server"
                                CssClass="btn btn-default" Text="Modifica"
                                OnClick="cmdMod_valor_Click" />
                            <asp:Button ID="cmdDel_valor" runat="server"
                                CssClass="btn btn-default" Text="Elimina"
                                OnClick="cmdDel_valor_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <%--<script src="../App_Themes/jQuery-2.1.4.min.js"></script>
    <script src="../App_Themes/jquery.dataTables.js"></script>
    <script>
        $.noConflict();
        jQuery(document).ready(function ($) {
            $('#' + '<%=gvConceptos.ClientID %>').DataTable(
                //{
                //    "language": {
                //        "url": "//cdn.datatables.net/plug-ins/1.10.16/i18n/Spanish.json"
                //    },
                //    dom: 'Bfrtip',
                //    buttons: [
                //        'copy', 'csv', 'excel', 'pdf', 'print'
                //    ]
                //}
            );

            $('#modalAdd').on('shown.bs.modal', function () {
                $('input:visible:enabled:first', this).focus();
            });
        });
                // Code that uses other library's $ can follow here.
    </script>--%>
            <!-- /.content -->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
