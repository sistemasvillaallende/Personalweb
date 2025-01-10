<%@ Page Title="" Language="C#" MasterPageFile="~/MP/Bootstrap.Master" AutoEventWireup="true" CodeBehind="liquidaciones.aspx.cs"
    Inherits="web.secure.liquidaciones" %>

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
                /*-o-box-shadow: 1px 1px 1px #111;*/
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
    <asp:UpdatePanel ID="uPanelCliente" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="row" style="margin-top: 10px; padding-top: 10px">
                <div class="col-md-12 col-md-offset-0">
                    <div class="col-md-12">
                        <div class="row" style="margin-top: 20px; padding-top: 20px">
                            <asp:UpdatePanel ID="PanelInfomacion" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="alert alert-success alert-dismissable" runat="server" id="divConfirma"
                                        visible="false" role="alert">
                                        <button type="button" class="close" data-dismiss="alert"
                                            onclick="__doPostBack('<%=PanelInfomacion.ClientID%>', 'Confirma');">
                                            <span aria-hidden="true">×</span></button>
                                        </button>
                                <h4>Aviso Importante!</h4>
                                        <p id="msjConfirmar" runat="server">
                                        </p>
                                    </div>

                                    <div class="alert alert-warning alert-dismissible" runat="server" id="divError"
                                        visible="false" role="alert">
                                        <button type="button" class="close" data-dismiss="alert"
                                            onclick="__doPostBack('<%=PanelInfomacion.ClientID%>', 'Alerta');">
                                            <span aria-hidden="true">×</span></button>
                                        </button>
                                <h4>Error!</h4>
                                        <p id="txtError" runat="server">
                                        </p>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="outer_div">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="box" style="margin-top: 10px;">
                                        <div class="box-header with-border">
                                            <div class="col-md-12">
                                                <h3 class="box-title">Liquidaciones</h3>
                                            </div>
                                            <br />
                                        </div>
                                        <!-- /.box-header -->
                                        <div class="box-body" style="margin-top: 20px;">
                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-xs-6">
                                                        <div class="input-group">
                                                            <input type="text" class="form-control" placeholder="Buscar por nombre"
                                                                id="q" onkeyup="load(1);">
                                                            <span class="input-group-btn">
                                                                <button class="btn btn-default" type="button" onclick="load(1);"><i class="fa fa-search"></i></button>
                                                            </span>
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-6">
                                                        <div class="btn-group pull-right" id="divActualiza" runat="server">
                                                            <asp:LinkButton ID="lbtnPublicar_liq" CssClass="btn btn-default" runat="server" OnClick="lbtnPublicar_liq_Click">
                                                            <i class="fa fa-check"></i>&nbsp;Publicar Liquidación
                                                            </asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnNuevo" CssClass="btn btn-default" runat="server" OnClick="lbtnNuevo_Click">
                                                            <i class="fa fa-plus"></i> Nueva Liq.
                                                            </asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnSalir" CssClass="btn btn-default" runat="server" OnClick="lbtnSalir_Click">
                                                            <i class="fa fa-sign-out"></i> Salir
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group" style="text-align: center;" id="divAcepta" visible="false" runat="server">
                                                    <asp:Button ID="btnCancelarPublicar" CssClass="btn btn-warning"
                                                        runat="server" Text="Cancelar" OnClick="btnCancelarPublicar_Click" />
                                                    <asp:Button ID="btnAceptarPublicar" runat="server" CssClass="btn btn-primary"
                                                        Text="Aceptar" OnClick="btnAceptarPublicar_Click" />

                                                </div>
                                            </div>
                                        </div>

                                        <div class="auto-style1" style="margin-top: 20px;">
                                            <asp:GridView ID="gvLiquidaciones"
                                                CssClass="table"
                                                runat="server"
                                                OnRowDataBound="gvLiquidaciones_RowDataBound"
                                                OnRowCommand="gvLiquidaciones_RowCommand"
                                                CellPadding="4"
                                                AutoGenerateColumns="False"
                                                ForeColor="#333333"
                                                GridLines="None" DataKeyNames="anio,cod_tipo_liq,nro_liquidacion" AllowPaging="True"
                                                OnPageIndexChanging="gvLiquidaciones_PageIndexChanging" PageSize="8">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Año" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <p>
                                                                <asp:Label ID="lblAnio" runat="server" Text=""></asp:Label>
                                                            </p>

                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Tipo Liq" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <p>
                                                                <asp:Label ID="lblTipo_liq" runat="server" Text=""></asp:Label>
                                                            </p>

                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Nro Liq" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <p>
                                                                <asp:Label ID="lblNro_liquidacion" runat="server" Text=""></asp:Label>
                                                            </p>

                                                        </ItemTemplate>
                                                        <ItemStyle Width="5%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Descripcion" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <p>
                                                                <asp:Label ID="lblDes_liquidacion" runat="server" Text=""></asp:Label>
                                                            </p>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Aguinaldo" ItemStyle-Width="7%">
                                                        <ItemTemplate>
                                                            <p>
                                                                <asp:CheckBox ID="chkAguinaldo" runat="server" />
                                                            </p>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="7%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Periodo" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <p>
                                                                <asp:Label ID="lblPeriodo" runat="server" Text=""></asp:Label>
                                                            </p>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Semestre" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <p>
                                                                <asp:Label ID="lblSemestre" runat="server" Text=""></asp:Label>
                                                            </p>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Fecha Pago" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <p>
                                                                <asp:Label ID="lblFecha_pago" runat="server" Text=""></asp:Label>
                                                            </p>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Publicar Liq">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkPublicar"
                                                                CssClass="form-control"
                                                                Enabled="true"
                                                                runat="server"></asp:CheckBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Publicada">
                                                        <ItemTemplate>
                                                            <p><i class="fa fa-check-square"></i>&nbsp;<%# (Convert.ToInt16(Eval("publica"))) == 1 ? "Si" : "No" %></p>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Cierre Liq">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkCerrada"
                                                                CssClass="form-control"
                                                                Enabled="true"
                                                                runat="server"></asp:CheckBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Cerrada">
                                                        <ItemTemplate>
                                                            <p><i class="fa fa-check-square"></i>&nbsp;<%# (Convert.ToInt16(Eval("cerrada"))) == 1 ? "Si" : "No" %></p>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <div class="btn-group pull-right">
                                                                <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown"
                                                                    arial-expanded="false">
                                                                    Acciones <span class="fa fa-caret-down"></span>
                                                                </button>
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
                                                                            ID="lbtnLiquidar"
                                                                            CommandName="liquidar"
                                                                            CommandArgument="<%# Container.DataItemIndex %>"
                                                                            runat="server">
                                                                    <i class="fa fa-edit"></i>&nbsp Liquidar Sueldo
                                                                        </asp:LinkButton>
                                                                    </li>
                                                                    <li>
                                                                        <asp:LinkButton
                                                                            ID="lbtnTraspado"
                                                                            CommandName="traspaso"
                                                                            CommandArgument="<%# Container.DataItemIndex %>"
                                                                            runat="server">
                                                                    <i class="fa fa-download"></i>&nbsp Traspaso Conceptos
                                                                        </asp:LinkButton>
                                                                    </li>
                                                                    <li>
                                                                        <asp:LinkButton
                                                                            ID="lbtnPuntualidad"
                                                                            CommandName="asistencia"
                                                                            CommandArgument="<%# Container.DataItemIndex %>"
                                                                            runat="server">
                                                                    <i class="fa fa-check-square-o"></i>&nbsp Dias Trabajados / Puntualidad / Asistencia
                                                                        </asp:LinkButton>
                                                                    </li>
                                                                    <%--<li>
                                                                        <asp:LinkButton
                                                                            ID="lbtnSalario"
                                                                            CommandName="salariofam"
                                                                            CommandArgument="<%# Container.DataItemIndex %>"
                                                                            OnClientClick='return confirm("Esta por Actualizar el Salario Familiar, desdea continuar?");'
                                                                            runat="server">
                                                                    <i class="fa fa-check-square-o"></i>&nbsp Actualizar Salario Familiar
                                                                        </asp:LinkButton>
                                                                    </li>--%>
                                                                    <li>
                                                                        <asp:LinkButton
                                                                            ID="lbtnBorrar"
                                                                            CommandName="eliminar"
                                                                            CommandArgument="<%# Container.DataItemIndex %>"
                                                                            OnClientClick='return confirm("Esta por Eliminar la Caratula de Liquidacion");'
                                                                            runat="server">
                                                                    <i class="fa fa-trash-o"></i>&nbsp Borrar Caratula Liquidación
                                                                        </asp:LinkButton>
                                                                    </li>
                                                                    </li>
                                                                    <li>
                                                                        <asp:LinkButton
                                                                            ID="lnkPublicar"
                                                                            CommandName="publicar"
                                                                            CommandArgument="<%# Container.DataItemIndex %>"
                                                                            runat="server">
                                                                    <i class="fa fa-tablet"></i>&nbsp Publicar Liquidación
                                                                        </asp:LinkButton>
                                                                    </li>
                                                                    <li>
                                                                        <a href="#" title="cerrarliquidacion" onclick="abrirModalCerrarLiquidacion('<%#Eval("anio")%>','<%#Eval("cod_tipo_liq")%>','<%#Eval("nro_liquidacion")%>')">
                                                                            <span class="fa fa-check"></span>&nbsp Cerrar Liquidación
                                                                        </a>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--abrirModalCerraliquidacion(anio, cod_tipo_liq, nro_liquidacion) {--%>
                                                    <%-- <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <div class="btn-group pull-right">
                                                                <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-expanded="false">Acciones <span class="fa fa-caret-down"></span></button>
                                                                <ul class="dropdown-menu">
                                                                </ul>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
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
                                        <!-- /.box-body -->
                                        <div class="box-footer clearfix">
                                        </div>
                                    </div>
                                    <!-- /.box -->
                                </div>
                                <!-- /.col -->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hID" runat="server" />
            <asp:Button ID="Button1" runat="server" Text="Button" Style="visibility: hidden;" />
            <ajaxToolkit:ModalPopupExtender runat="server"
                BackgroundCssClass="modalBackground"
                PopupControlID="modalDatosLiquidacion"
                BehaviorID="lbtnNuevo_ModalPopupExtender"
                TargetControlID="Button1"
                ID="lbtnNuevo_ModalPopupExtender">
            </ajaxToolkit:ModalPopupExtender>
            <div class="modal-dialog" id="modalDatosLiquidacion" runat="server" style="background-color: white; padding: 20px;">
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
                    <div class="modal-body" id="activity" style="min-height: 320px; scrollbar">
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
                                    OnSelectedIndexChanged="ddlTipo_liq_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rv2" runat="server" ValidationGroup="cliente"
                                    Text="*" ForeColor="Red" InitialValue="0" Display="Dynamic"
                                    ErrorMessage="Ingrese Tipo Liq" ControlToValidate="ddlTipo_liq">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4">
                                <label>Nro Liquidacion</label>
                                <asp:DropDownList ID="ddlNro_liq" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                    <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                </asp:DropDownList>
                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    Text="*" ForeColor="Red" Display="Dynamic"
                                    ErrorMessage="Ingrese Nro Liq" ControlToValidate="ddlNro_liq">
                                </asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-md-4">
                                <label>Descripcion Liq(Mes Año)</label>
                                <asp:TextBox ID="txtDes_liquidacion" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="cliente"
                                    Text="*" ForeColor="Red" Display="Dynamic"
                                    ErrorMessage="Ingrese la Descripcion de la Liq" ControlToValidate="txtDes_liquidacion">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4">
                                <label>Periodo(AAAAMM)</label>
                                <asp:TextBox ID="txtPeriodo" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rv4" runat="server" ValidationGroup="cliente"
                                    Text="*" ForeColor="Red" Display="Dynamic"
                                    ErrorMessage="Ingrese Periodo" ControlToValidate="txtPeriodo">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4">
                                <label class="control-label" for="inputSuccess">Act.Salario Fam?</label>
                                <asp:CheckBox ID="chkSalarioFam" runat="server" CssClass="form-control"></asp:CheckBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-md-4">
                                <label>Semestre</label>
                                <asp:DropDownList ID="ddLSemestre" CssClass="form-control" runat="server">
                                    <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="cliente"
                                    Text="*" ForeColor="Red" Display="Dynamic" InitialValue="0"
                                    ErrorMessage="Ingrese Semestre" ControlToValidate="ddLSemestre">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4">
                                <label>Aguinaldo</label>
                                <asp:CheckBox ID="chkAguinaldo" CssClass="form-control" runat="server" />
                            </div>
                            <div class="form-group col-md-4">
                                <label>Liq de Prueba?</label>
                                <asp:CheckBox ID="chkPrueba" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:CheckBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-md-6">
                                <label>Per.Ult.Deposito(AAAAMM)</label>
                                <asp:TextBox ID="txtPer_ult_dep" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="cliente"
                                    Text="*" ForeColor="Red" Display="Dynamic"
                                    ErrorMessage="Ingrese Periodo" ControlToValidate="txtPer_ult_dep">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4">
                                <label>Fec.Ult.Deposito</label>
                                <asp:TextBox ID="txtFecha_ult_deposito" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="cliente"
                                    Text="*" ForeColor="Red" Display="Dynamic"
                                    ErrorMessage="Ingrese Fecha Ult Deposito" ControlToValidate="txtFecha_ult_deposito">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-2">
                                <label>Publicar</label>
                                <asp:CheckBox ID="chkPublicar" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:CheckBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-md-3">
                                <label>Fec.Liquidación</label>
                                <asp:TextBox ID="txtFecha_liquidacion" CssClass="form-control"
                                    placeholder="Ingrese Fecha Liquidacion" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="cliente"
                                    Text="*" ForeColor="Red" Display="Dynamic"
                                    ErrorMessage="Ingrese Fecha Liquidacion" ControlToValidate="txtFecha_liquidacion">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-6">
                                <label>Banco Ult.Deposito</label>
                                <asp:DropDownList ID="ddlBanco" CssClass="form-control" runat="server">
                                    <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group col-md-3">
                                <label>Fecha Pago</label>
                                <asp:TextBox ID="txtFecha_pago" CssClass="form-control"
                                    placeholder="Ingrese Fecha Pago" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="cliente"
                                    Text="*" ForeColor="Red" Display="Dynamic"
                                    ErrorMessage="Ingrese Fecha Pago" ControlToValidate="txtFecha_pago">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ValidationGroup="cliente" />
                        <asp:Button ID="btnCancelarModal" runat="server"
                            CssClass="btn btn-default" Text="Cancelar"
                            OnClick="btnCancelarModal_Click" />
                        <asp:Button ID="btnCrearLiq" runat="server"
                            ValidationGroup="cliente"
                            CssClass="btn btn-primary" Text="Aceptar"
                            OnClick="btnCrearLiq_Click" />
                    </div>
                    <!-- /.tab-pane -->
                    <!-- /.tab-pane -->
                    <!-- /.tab-pane -->
                </div>
            </div>
            <%-- /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// --%>
            <asp:HiddenField ID="hID2" runat="server" />
            <asp:HiddenField ID="hAño" runat="server" />
            <asp:HiddenField ID="hCod_tipo_liq" runat="server" />
            <asp:HiddenField ID="hDes_tipo_liq" runat="server" />
            <asp:HiddenField ID="hNro_liq" runat="server" />
            <asp:HiddenField ID="hDes_liquidacion" runat="server" />
            <asp:Button ID="Button2" runat="server" Text="Button" Style="visibility: hidden;" />
            <ajaxToolkit:ModalPopupExtender runat="server"
                BackgroundCssClass="modalBackground"
                PopupControlID="modalDatosTraspaso"
                BehaviorID="lbtnmodalTraspasoExtender"
                TargetControlID="Button2"
                ID="lbtnmodalTraspasoExtender">
            </ajaxToolkit:ModalPopupExtender>
            <div class="modal-dialog" id="modalDatosTraspaso" runat="server" style="background-color: white; padding: 20px;">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button"
                            runat="server"
                            id="btnCloseTraspaso"
                            onserverclick="btnCloseTraspaso_ServerClick"
                            class="close" data-dismiss="modal"
                            aria-label="Close">
                            <span aria-hidden="true">×</span></button>
                        <h4 class="modal-title">
                            <asp:Label ID="lblTraspaso" runat="server" Text="Label"></asp:Label>
                        </h4>
                    </div>
                    <div class="modal-body" id="activity_traspaso" style="min-height: 320px;">
                        <div class="row">
                            <div class="form-group col-md-12">
                                <div class="alert alert-success alert-dismissible" runat="server" id="divMsjTraspaso"
                                    visible="false" role="alert">
                                    <button type="button" class="close" data-dismiss="alert" onclick="__doPostBack('<%=PanelInfomacion.ClientID%>', 'Alerta');">
                                        <span aria-hidden="true">×</span></button>
                                    </button>
                                <h4>Aviso!</h4>
                                    <p id="msjTraspaso" runat="server">
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-md-12">
                                <label>Liquidacion Actual</label>
                                <asp:TextBox ID="txtLiquidacion" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <%--<div class="row">
                            <br />
                        </div>--%>
                        <div class="row">
                            <div class="form-group col-md-4">
                                <label>Año</label>
                                <asp:TextBox ID="txtAnio_1" CssClass="form-control"
                                    placeholder="Ingrese Año de Liq" runat="server" AutoPostBack="True" OnTextChanged="txtAnio_1_TextChanged"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="traspaso"
                                    Text="*" ForeColor="Red" Display="Dynamic"
                                    ErrorMessage="Ingrese el Año" ControlToValidate="txtAnio_1">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4">
                                <label>Tipo Liquidacion</label>
                                <asp:DropDownList ID="ddlTipo_liq_1" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                    OnSelectedIndexChanged="ddlTipo_liq_1_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="traspaso"
                                    Text="*" ForeColor="Red" InitialValue="0" Display="Dynamic"
                                    ErrorMessage="Ingrese Tipo Liq" ControlToValidate="ddlTipo_liq_1">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4">
                                <label>Nro Liquidacion</label>
                                <asp:DropDownList ID="ddlNro_liq_1" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                    <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="traspaso"
                                    Text="*" ForeColor="Red" Display="Dynamic"
                                    ErrorMessage="Ingrese Nro Liq" ControlToValidate="ddlNro_liq_1">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-md-6">
                                <label>Cod Concepto:</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtCod_concepto" CssClass="form-control" runat="server"
                                        OnTextChanged="txtCod_concepto_TextChanged"
                                        AutoPostBack="true"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <button class="btn btn-facebook" type="button" id="btnBuscarConcepto" runat="server"
                                            onserverclick="btnBuscarConcepto_ServerClick" causesvalidation="False">
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
                    <div class="modal-footer">
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ForeColor="Red" ValidationGroup="traspaso" />
                        <asp:Button ID="btnCancelaTraspaso" runat="server"
                            CssClass="btn btn-default" Text="Cancelar"
                            OnClick="btnCancelaTraspaso_Click" />
                        <asp:Button ID="btnAceptaTraspaso" runat="server"
                            ValidationGroup="traspaso"
                            CssClass="btn btn-primary" Text="Aceptar"
                            OnClick="btnAceptaTraspaso_Click" />
                    </div>
                    <!-- /.tab-pane -->
                    <!-- /.tab-pane -->
                    <!-- /.tab-pane -->
                </div>
            </div>
            <%-- /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// --%>
            <asp:Button ID="Button7" runat="server" Text="Button" Style="visibility: hidden;" />
            <ajaxToolkit:ModalPopupExtender runat="server"
                BackgroundCssClass="modalBackground"
                PopupControlID="modalAsistenciaPuntualidad"
                BehaviorID="lbtnmodalAsistenciaExtender"
                TargetControlID="Button7"
                ID="lbtnmodalAsistenciaExtender">
            </ajaxToolkit:ModalPopupExtender>
            <div class="modal-dialog" id="modalAsistenciaPuntualidad" runat="server" style="background-color: white; padding: 20px;">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button"
                            runat="server"
                            id="btnCloseAsistenciaPuntualidad"
                            onserverclick="btnCloseAsistenciaPuntualidad_ServerClick"
                            class="close" data-dismiss="modal"
                            aria-label="Close">
                            <span aria-hidden="true">×</span></button>
                        <h4 class="modal-title">
                            <asp:Label ID="lblTitulo_asistencia" runat="server" Text="Label"></asp:Label>
                        </h4>
                    </div>
                    <div class="modal-body" id="activity_asistencia" style="min-height: 320px;">
                        <div class="row">
                            <div class="form-group col-md-12">
                                <div class="alert alert-success alert-dismissible" runat="server" id="divMsjAsistencia"
                                    visible="false" role="alert">
                                    <button type="button" class="close" data-dismiss="alert" onclick="__doPostBack('<%=PanelInfomacion.ClientID%>', 'Alerta');">
                                        <span aria-hidden="true">×</span></button>
                                    </button>
                                <h4>Aviso!</h4>
                                    <p id="msjAsistencia" runat="server">
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="modal-body" id="activity_datos" style="min-height: 320px;">
                            <div class="row">
                                <div class="form-group col-md-4">
                                    <label>Año</label>
                                    <asp:TextBox ID="txtAnio_2" CssClass="form-control"
                                        placeholder="Ingrese Año de Liq" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>Tipo Liquidacion</label>
                                    <asp:DropDownList ID="ddlTipo_liq_2" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlTipo_liq_2_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>Nro Liquidacion</label>
                                    <asp:DropDownList ID="ddlNro_liq_2" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                        <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-6">
                                    <label>Descripcion Liq (Formato Mes Año)</label>
                                    <asp:TextBox ID="txtDes_liquidacion_2" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-6">
                                    <label>Periodo(Formato AAAAMM)</label>
                                    <asp:TextBox ID="txtPeriodo_2" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-6">
                                    <label>Asistencia</label>
                                    <div class="input-group">
                                        <asp:CheckBox ID="chkAsistencia" CssClass="form-control" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group col-md-6">
                                    <label>Puntualidad</label>
                                    <div class="input-group">
                                        <asp:CheckBox ID="chkPuntualidad" CssClass="form-control" runat="server" />
                                    </div>
                                </div>
                                <%--<div class="form-group col-md-4">
                                    &nbsp;
                                </div>--%>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-6">
                                    <label>Dias Trabajados(30)</label>
                                    <div class="input-group">
                                        <asp:CheckBox ID="chkDias" CssClass="form-control" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group col-md-6">
                                    <label>Dias Aguinaldo(180)</label>
                                    <div class="input-group">
                                        <asp:CheckBox ID="chkDiasAguinaldo" CssClass="form-control" runat="server" />
                                    </div>
                                </div>
                                <%--<div class="form-group col-md-4">
                                    &nbsp;
                                </div>--%>
                            </div>
                        </div>
                    </div>

                    <div class="modal-footer">
                        <%--<asp:ValidationSummary ID="ValidationSummary3" runat="server" ForeColor="Red" ValidationGroup="asistencia" />--%>
                        <asp:Button ID="btnCancelaAsistencia" runat="server"
                            CssClass="btn btn-default" Text="Cancelar"
                            OnClick="btnCancelaAsistencia_Click" />
                        <asp:Button ID="btnConfirmaAsistencia" runat="server"
                            CssClass="btn btn-primary" Text="Aceptar"
                            OnClick="btnConfirmaAsistencia_Click" />

                    </div>
                </div>
            </div>
            <%-- /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// --%>
            <asp:Button ID="Button3" runat="server" Text="Button" Style="visibility: hidden;" />
            <ajaxToolkit:ModalPopupExtender runat="server"
                BackgroundCssClass="modalBackground"
                PopupControlID="divModalPublicar"
                BehaviorID="lbtnmodalPublicarExtender"
                TargetControlID="Button3"
                ID="lbtnmodalPublicarExtender">
            </ajaxToolkit:ModalPopupExtender>
            <div class="modal-dialog" id="divModalPublicar" runat="server" style="background-color: white; padding: 20px;">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button"
                            runat="server"
                            id="btnClosePublicar"
                            onserverclick="btnClosePublicar_ServerClick"
                            class="close" data-dismiss="modal"
                            aria-label="Close">
                            <span aria-hidden="true">×</span></button>
                        <h4 class="modal-title">
                            <asp:Label ID="lblTitulo_publicar" runat="server" Text="Label"></asp:Label>
                        </h4>
                    </div>
                    <div class="modal-body" id="activity_publicar_liq" style="min-height: 320px;">
                        <div class="row">
                            <div class="form-group col-md-12">
                                <div class="alert alert-success alert-dismissible" runat="server" id="divMsjPublicar"
                                    visible="false" role="alert">
                                    <button type="button" class="close" data-dismiss="alert" onclick="__doPostBack('<%=PanelInfomacion.ClientID%>', 'Alerta');">
                                        <span aria-hidden="true">×</span></button>
                                    </button>
                                <h4>Aviso!</h4>
                                    <p id="msjPublicar" runat="server">
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="modal-body" id="activity_publicar_liquidacion" style="min-height: 320px;">
                            <div class="row">
                                <div class="form-group col-md-4">
                                    <label>Año</label>
                                    <asp:TextBox ID="txtAnio_3" CssClass="form-control"
                                        placeholder="Ingrese Año de Liq" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="asistencia"
                                        Text="*" ForeColor="Red" Display="Dynamic"
                                        ErrorMessage="Ingrese el Año" ControlToValidate="txtAnio_3">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>Tipo Liquidacion</label>
                                    <asp:DropDownList ID="ddlTipo_liq_3" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlTipo_liq_3_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ValidationGroup="asistencia"
                                        Text="*" ForeColor="Red" InitialValue="0" Display="Dynamic"
                                        ErrorMessage="Ingrese Tipo Liq" ControlToValidate="ddlTipo_liq_3">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>Nro Liquidacion</label>
                                    <asp:DropDownList ID="ddlNro_liq_3" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                        <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ValidationGroup="asistencia"
                                        Text="*" ForeColor="Red" Display="Dynamic"
                                        ErrorMessage="Ingrese Nro Liq" ControlToValidate="ddlNro_liq_3">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-6">
                                    <label>Descripcion Liq (Formato Mes Año)</label>
                                    <asp:TextBox ID="txtDes_liquidacion_3" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ValidationGroup="asistencia"
                                        Text="*" ForeColor="Red" Display="Dynamic"
                                        ErrorMessage="Ingrese la Descripcion de la Liq" ControlToValidate="txtDes_liquidacion_3">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-6">
                                    <label>Periodo(Formato AAAAMM)</label>
                                    <asp:TextBox ID="txtPeriodo_3" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ValidationGroup="asistencia"
                                        Text="*" ForeColor="Red" Display="Dynamic"
                                        ErrorMessage="Ingrese Periodo" ControlToValidate="txtPeriodo_3">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-6">
                                    <label>Publicar Liquidación</label>
                                    <div class="input-group">
                                        <asp:CheckBox ID="chkPublicar_liquidacion" CssClass="form-control" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-6">
                                    <label></label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:ValidationSummary ID="ValidationSummary4" runat="server" ForeColor="Red" ValidationGroup="publicar" />
                        <asp:Button ID="btnCancelaPublicar" runat="server"
                            CssClass="btn btn-default" Text="Cancelar"
                            OnClick="btnCancelaPublicar_Click" />
                        <asp:Button ID="btnConfirmaPublicar" runat="server"
                            ValidationGroup="publicar"
                            CssClass="btn btn-primary" Text="Aceptar"
                            OnClick="btnConfirmaPublicar_Click" />

                    </div>
                </div>
            </div>
            <asp:HiddenField ID="HFAnio1" runat="server" />
            <asp:HiddenField ID="HFCod_tipo_liq1" runat="server" />
            <asp:HiddenField ID="HFNro_liquidacion1" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <!-- /.content -->
    <div class="modal fade in" id="modalCerrarliquidacion">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span></button>
                    <h4 class="modal-title">Cerrar Liquidación</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label>Fecha Cierre Liq</label>
                        <asp:TextBox ID="txtFecha_cierre"
                            CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11"
                            ForeColor="Red"
                            ControlToValidate="txtFecha_cierre"
                            ValidationGroup="NCF"
                            runat="server" ErrorMessage="Ingrese la Fecha">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label>Cerrar Liq.</label>
                        <asp:CheckBox ID="chkCerrada" CssClass="form-control" runat="server" />
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Close</button>
                    <asp:Button ID="btnCierrarLiq"
                        OnClick="btnCierrarLiq_Click"
                        CssClass="btn btn-primary"
                        ValidationGroup="NCF"
                        runat="server" Text="Aceptar" />

                </div>
            </div>
            <!-- /.modal-content -->
        </div>
    </div>
    <!-- /.modal-dialog -->
    <script>              
        function abrirModalCerrarLiquidacion(anio, cod_tipo_liq, nro_liquidacion) {
            var f = new Date();
            $('#modalCerrarliquidacion').modal('show');
            //PERIODO
            $("#ContentPlaceHolder1_chkCerrada").prop("checked", true);
            $("#ContentPlaceHolder1_txtFecha_cierre").val(format(f));
            $("#ContentPlaceHolder1_HFAnio1").val(anio);
            $("#ContentPlaceHolder1_HFCod_tipo_liq1").val(cod_tipo_liq);
            $("#ContentPlaceHolder1_HFNro_liquidacion1").val(nro_liquidacion);
            //txtFecha_cierre
        }
        function format(inputDate) {
            let date, month, year;
            date = inputDate.getDate();
            month = inputDate.getMonth() + 1;
            year = inputDate.getFullYear();
            date = date
                .toString()
                .padStart(2, '0');

            month = month
                .toString()
                .padStart(2, '0');
            return `${date}/${month}/${year}`;
        }
    </script>
</asp:Content>
