<%@ Page Title="" Language="C#" MasterPageFile="~/MP/Bootstrap.Master" AutoEventWireup="true" CodeBehind="reporteLiquidacion.aspx.cs" Inherits="web.secure.reporteLiquidacion" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../App_Themes/dist/css/AdminLTE.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row" style="margin-top: 100px;">
        <div class="col-md-10 col-md-offset-1">
            <div class="row" style="margin-top: 40px; padding-top: 40px">
                <asp:UpdatePanel ID="PanelInfomacion" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="alert alert-success alert-dismissable" runat="server" id="divConfirma"
                            visible="false" role="alert">
                            <button type="button" class="close" data-dismiss="alert" onclick="__doPostBack('<%=PanelInfomacion.ClientID%>', 'Confirma');">
                                <span aria-hidden="true">×</span></button>
                            </button>
                                <h4>Aviso Importante!</h4>
                            <p id="msjConfirmar" runat="server">
                            </p>
                        </div>

                        <div class="alert alert-warning alert-dismissible" runat="server" id="divError"
                            visible="false" role="alert">
                            <button type="button" class="close" data-dismiss="alert" onclick="__doPostBack('<%=PanelInfomacion.ClientID%>', 'Alerta');">
                                <span aria-hidden="true">×</span></button>
                            </button>
                                <h4>Error!</h4>
                            <p id="txtError" runat="server">
                            </p>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="col-md-6 col-md-offset-3">
            <!-- general form elements -->
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">Reportes de Liquidación</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Año</label>
                                <asp:TextBox ID="txtAnio" TextMode="Number" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rv1" runat="server" ErrorMessage="*"
                                    ValidationGroup="sueldo" Text="Ingrese el año" Display="Dynamic"
                                    ControlToValidate="txtAnio" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Tipo Liquidacion : </label>
                                <asp:DropDownList ID="txtTipo_liq" runat="server" CssClass="form-control" AppendDataBoundItems="True" 
                                    OnSelectedIndexChanged="txtTipo_liq_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTipo_liq"
                                    Text="Debe Ingresar TipoLiquidacion" ForeColor="Red" Display="Dynamic" InitialValue="0"
                                    ErrorMessage="Debe Ingresar Tipo Liquidacion" ValidationGroup="sueldo"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Nro Liquidacion : </label>
                                <asp:DropDownList ID="txtNro_liq" runat="server" CssClass="form-control"
                                    AppendDataBoundItems="True"
                                    OnSelectedIndexChanged="txtNro_liq_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNro_liq"
                                    Text="Debe Ingresar Número de Liquidacion" ForeColor="Red" Display="Dynamic" InitialValue="0"
                                    ErrorMessage="Debe Ingresar Número de Liquidacion" ValidationGroup="sueldo"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <%--<div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Fecha Liquidación</label>
                                <asp:TextBox ID="txtFechaLiq" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                    ValidationGroup="sueldo" Text="Ingrese fecha de liquidación" Display="Dynamic"
                                    ControlToValidate="txtFechaLiq" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Fecha Pago</label>
                                <asp:TextBox ID="txtFechaPago" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                    ValidationGroup="sueldo" Text="Ingrese fecha de pago" Display="Dynamic"
                                    ControlToValidate="txtFechaPago" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>--%>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Seleccionar Reporte</label>
                                <asp:DropDownList ID="ddlReportes" runat="server" CssClass="form-control" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlReportes_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlReportes"
                                    Text="Debe Seleccionar Reportes" ForeColor="Red" Display="Dynamic" InitialValue="0"
                                    ErrorMessage="Debe Seleccionar Reportes" ValidationGroup="sueldo"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="box-footer" style="text-align: right;">
                    <asp:LinkButton ID="btnCancelar" OnClick="btnCancelar_Click" CssClass="btn btn-warning" runat="server">
                        <span class="fa fa-sign-out"></span>&nbsp; Cancelar</asp:LinkButton>
                    <asp:LinkButton ID="btnAceptar" Style="padding: 9px;" OnClick="btnAceptar_Click"
                        CssClass="btn btn-primary" runat="server" ValidationGroup="sueldo">
                        <span class="fa fa-check">&nbsp; Aceptar</span>
                    </asp:LinkButton>
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
                        <h2 style="color: white">Reportes Liq Sueldos</h2>
                    </div>
                </div>
                <div runat="server" id="divReporte">
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>
