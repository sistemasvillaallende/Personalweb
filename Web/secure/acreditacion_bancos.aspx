<%@ Page Title="" Language="C#" MasterPageFile="~/MP/MasterNew.Master" AutoEventWireup="true"
    CodeBehind="acreditacion_bancos.aspx.cs" Inherits="web.secure.acreditacion_bancos" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="uPanelCliente" UpdateMode="Conditional" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="lbtnGenerar" />
        </Triggers>
        <ContentTemplate>
            <div class="container-fluid shadow p-3 mb-5 bg-white rounded"
                style="background-color: white; padding-left: 2px !important; padding-top: 0px !important; padding-right: 2px !important; padding-bottom: 0 !important;">
                <div class="row">
                    <div class="col-md-12">
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
                <div class="row">
                    <div class="col-md-12">
                            <div class="nav nav-pills">
                                <%--style="padding-top: 15px;"--%>
                                <ul class="nav nav-tabs">
                                    <li class="active">
                                        <a href="#tab_movimientos_caja" data-toggle="tab" aria-expanded="true">
                                            Generar Archivo para Bancos</a></li>
                                </ul>
                                <div class="tab-content" style="padding-top: 0px; border: solid lightgray 0.4px; background-color: white;">
                                    <div class="tab-pane active" style="min-height: 300px;" id="tab_movimientos_caja">
                                        <div class="box">
                                            <br />
                                            <div class="modal-body" id="activity" style="min-height: 160px; padding-top: 40px;">
                                                <div class="row">
                                                    <div class="form-group col-md-4">
                                                        <label>Año</label>
                                                        <asp:TextBox ID="txtAnio" TextMode="Number" CssClass="form-control"
                                                            placeholder="Ingrese Año de Liq" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rv1" runat="server" ValidationGroup="cliente"
                                                            Text="Ingrese el Año" ForeColor="Red" Display="Dynamic"
                                                            ErrorMessage="Ingrese el Año" ControlToValidate="txtAnio">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label>Tipo Liquidacion</label>
                                                        <asp:DropDownList ID="ddlTipo_liq" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                            AutoPostBack="true"
                                                            OnSelectedIndexChanged="ddlTipo_liq_SelectedIndexChanged">
                                                            <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rv2" runat="server" ValidationGroup="cliente"
                                                            Text="Seleccione Tipo Liq" ForeColor="Red" InitialValue="0" Display="Dynamic"
                                                            ErrorMessage="Seleccione Tipo Liq" ControlToValidate="ddlTipo_liq">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label>Mes Liquidacion</label>
                                                        <asp:DropDownList ID="ddlNro_liq" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlNro_liq_SelectedIndexChanged">
                                                            <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-md-6">
                                                        <label>Descripcion Liq (Formato Mes Año)</label>
                                                        <asp:TextBox ID="txtDes_liquidacion" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="cliente"
                                                            Text="Ingrese la Descripcion de la Liq." ForeColor="Red" Display="Dynamic"
                                                            ErrorMessage="Ingrese la Descripcion de la Liq" ControlToValidate="txtDes_liquidacion">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-6">
                                                        <label>Periodo(Formato AAAAMM)</label>
                                                        <asp:TextBox ID="txtPeriodo" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rv4" runat="server" ValidationGroup="cliente"
                                                            Text="Ingrese Periodo" ForeColor="Red" Display="Dynamic"
                                                            ErrorMessage="Ingrese Periodo" ControlToValidate="txtPeriodo">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-md-4">
                                                        <label>Listar por Banco</label>
                                                        <asp:DropDownList ID="ddlOpcionBanco" CssClass="form-control" runat="server">
                                                            <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                                            <asp:ListItem Value="1">Por Nº liq</asp:ListItem>
                                                            <asp:ListItem Value="2">Por Periodo</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="cliente"
                                                            Text="Seleccione Banco" ForeColor="Red" Display="Dynamic" InitialValue="0"
                                                            ErrorMessage="Seleccione Banco" ControlToValidate="ddlOpcionBanco">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label>Nombre Archivo</label>
                                                        <asp:TextBox ID="txtArchivo" CssClass="form-control" runat="server" placeholder="Ingrese Nombre del Archivo"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="cliente"
                                                            Text="Ingrese Nombre del Archivo" ForeColor="Red" Display="Dynamic"
                                                            ErrorMessage="Ingrese Nombre del Archivo" ControlToValidate="txtArchivo">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label>Porcentaje</label>
                                                        <asp:TextBox ID="txtPorcentaje" TextMode="Number" CssClass="form-control" runat="server" placeholder="Ingrese Porcentaje"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="cliente"
                                                            Text="Ingrese Porcentaje" ForeColor="Red" Display="Dynamic"
                                                            ErrorMessage="Ingrese Porcentaje" ControlToValidate="txtPorcentaje">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>

                                                <div class="modal-footer">
                                                    <asp:ValidationSummary ID="cliente" runat="server" ForeColor="Red" />
                                                    <div class="btn-group pull-right">
                                                        <asp:LinkButton ID="lbtnGenerar" CssClass="btn btn-default" runat="server" OnClick="lbtnGenerar_Click"
                                                            ValidationGroup="cliente">
                                                                <i class="fa fa-file"></i> Generar Archivo
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnImprimir" CssClass="btn btn-default" runat="server" OnClick="lbtnImprimir_Click"
                                                            ValidationGroup="cliente">
                                                                <i class="fa fa-file"></i> Imprime Reporte
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnSalir" CssClass="btn btn-default" runat="server" OnClick="btnSalir_Click">
                                            <i class="fa fa-sign-out"></i> Salir
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%-- /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// --%>
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
                                            <h2 style="color: white">Reportes Acreditacion Sueldos</h2>
                                        </div>
                                    </div>
                                    <div runat="server" id="divReporte">
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
