<%@ Page Title="" Language="C#" MasterPageFile="~/MP/MasterNew.Master" AutoEventWireup="true" CodeBehind="sijcor.aspx.cs" Inherits="web.secure.sijcor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="uPanelCliente" UpdateMode="Conditional" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="lbtnGenerar" />
        </Triggers>
        <ContentTemplate>
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <div class="row" style="margin-top: 40px; padding-top: 40px">
                            <asp:UpdatePanel ID="PanelInfomacion" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="lbtnExcel" />
                                </Triggers>
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
                        <div class="col-md-10 col-md-offset-1">
                            <div class="nav nav-pills">
                                <%--style="padding-top: 15px;"--%>
                                <ul class="nav nav-tabs">
                                    <li class="active"><a href="#tab_movimientos_caja" data-toggle="tab" aria-expanded="true">Generar Archivo Sijcor</a></li>
                                </ul>
                                <div class="tab-content" style="padding-top: 0px; border: solid lightgray 0.4px; background-color: white;">
                                    <div class="tab-pane active" style="min-height: 300px;" id="tab_movimientos_caja">
                                        <div class="box">
                                            <br />
                                            <div class="modal-body" id="activity" style="min-height: 160px; padding-top: 40px;">
                                                <div class="row">
                                                    <div class="form-group col-md-4">
                                                        <label>Año</label>
                                                        <asp:TextBox ID="txtAnio" CssClass="form-control"
                                                            placeholder="Ingrese Año de Liq" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rv1" runat="server" ValidationGroup="cliente"
                                                            Text="*" ForeColor="Red" Display="Dynamic"
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
                                                            Text="*" ForeColor="Red" InitialValue="0" Display="Dynamic"
                                                            ErrorMessage="Ingrese Tipo Liq" ControlToValidate="ddlTipo_liq">
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
                                                            Text="*" ForeColor="Red" Display="Dynamic"
                                                            ErrorMessage="Ingrese la Descripcion de la Liq" ControlToValidate="txtDes_liquidacion">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-6">
                                                        <label>Periodo(Formato AAAAMM)</label>
                                                        <asp:TextBox ID="txtPeriodo" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rv4" runat="server" ValidationGroup="cliente"
                                                            Text="*" ForeColor="Red" Display="Dynamic"
                                                            ErrorMessage="Ingrese Periodo" ControlToValidate="txtPeriodo">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-md-3">
                                                        <label>Incluir Aguinaldo en Sueldo?</label>
                                                        <asp:CheckBox ID="chkConAquinaldo" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label>Listar por</label>
                                                        <asp:DropDownList ID="ddlOpcionArchivo" CssClass="form-control" runat="server">
                                                            <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                                            <asp:ListItem Value="1">Por Nº liq</asp:ListItem>
                                                            <asp:ListItem Value="2">Por Periodo</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="cliente"
                                                            Text="*" ForeColor="Red" Display="Dynamic" InitialValue="0"
                                                            ErrorMessage="Seleccione Opcion" ControlToValidate="ddlOpcionArchivo">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-5">
                                                        <label>Nombre Archivo</label>
                                                        <asp:TextBox ID="txtArchivo" CssClass="form-control" runat="server" placeholder="Ingrese Nombre del Archivo"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="cliente"
                                                            Text="*" ForeColor="Red" Display="Dynamic"
                                                            ErrorMessage="Ingrese Nombre del Archivo" ControlToValidate="txtArchivo">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>

                                                <div class="modal-footer">
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
                                                    <div class="btn-group pull-right">
                                                        <asp:LinkButton ID="lbtnGenerar" CssClass="btn btn-app btn-bitbucket" runat="server" OnClick="lbtnGenerar_Click"
                                                            ValidationGroup="cliente">
                                                                <i class="fa fa-file"></i> Generar Archivo
                                                        </asp:LinkButton>

                                                        <asp:LinkButton ID="lbtnExcel" CssClass="btn btn-app bg-green-active" runat="server" OnClick="lbtnExcel_Click"
                                                            ValidationGroup="cliente">
                                                                <i class="fa fa-plus"></i> Generar Excel
                                                        </asp:LinkButton>

                                                        <%-- <asp:LinkButton ID="lbtnGuardar" CssClass="btn btn-default" runat="server" OnClick="lbtnGuardar_Click"
                                                            ValidationGroup="cliente" Visible="false">
                                            <i class="fa fa-file"></i> Descargar Archivo
                                                        </asp:LinkButton>--%>
                                                        <asp:LinkButton ID="lbtnSalir" CssClass="btn btn-app bg-orange margin" runat="server" OnClick="btnSalir_Click">
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
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
