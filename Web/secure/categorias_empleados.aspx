<%@ Page Title="" Language="C#" MasterPageFile="~/MP/MasterNew.Master" AutoEventWireup="true" CodeBehind="categorias_empleados.aspx.cs" Inherits="web.secure.categorias_empleados" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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
    <div class="container">
        <div class="row">
            <asp:UpdatePanel ID="uPanelCliente" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="LinkExportar" />
                </Triggers>
                <ContentTemplate>
                    <div class="row" style="margin-top: 30px; padding-top: 30px">
                        <!-- Content Header (Page header) -->
                        <%--<section class="content-header">--%>
                        <%--<div class="row" style="margin-top: 40px; padding-top: 40px">--%>
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
                        <%--</div>--%>
                        <%--</section>--%>
                    </div>
                    <!-- Main content -->
                    <%--<section class="content">--%>

                    <div class="outer_div">

                        <div class="row">
                            <div class="col-md-12">
                                <div class="box">
                                    <%--style="margin-top: 10px;"--%>
                                    <div class="box-header with-border">
                                        <div class="col-md-12">
                                            <h3 class="box-title">Categorias Empleados</h3>
                                        </div>
                                        <br />
                                    </div>
                                    <!-- /.box-header -->
                                    <div class="box-body" style="margin-top: 20px;">
                                        <div class="row">
                                            <%--<div class="col-md-10 col-md-offset-1">--%>
                                            <hr style="border-top: 2px solid #9c9c9c;" />
                                            <%--</div>--%>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <div class="col-xs-6">
                                                    <div class="input-group">
                                                        <input type="text" class="form-control" id="txtInput" runat="server" placeholder="Buscar por Categoria" />
                                                        <span class="input-group-btn">
                                                            <button class="btn btn-info" type="button" id="btnBuscar" runat="server"
                                                                onserverclick="btnBuscar_ServerClick">
                                                                <span class="fa fa-search"></span>Buscar</button>
                                                        </span>
                                                    </div>
                                                </div>
                                                <div class="col-xs-6">
                                                    <div class="btn-group pull-right" id="divActualiza" runat="server">
                                                        <asp:LinkButton ID="lbtnActualizar_valores" CssClass="btn btn-app btn-default" runat="server" OnClick="lbtnActualizar_valores_Click">
                                                        <i class="fa fa-money"></i>&nbsp;Actualizar Valores
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnNuevo" CssClass="btn btn-app" runat="server" OnClick="lbtnNuevo_Click">
                                                        <i class="fa fa-plus"></i>&nbsp;Nueva Categoria
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="LinkExportar" CssClass="btn btn-app" runat="server" OnClick="LinkExportar_Click">
                                                        <i class="fa fa-download"></i>&nbsp;Exportar Excel
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnSalir" CssClass="btn btn-app bg-orange" runat="server" OnClick="lbtnSalir_Click">
                                                        <i class="fa fa-sign-out"></i>&nbsp;Salir
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group" style="text-align: right" id="divAcepta" visible="false" runat="server">
                                                <asp:Button ID="btnCancelarValores" CssClass="btn btn-warning"
                                                    runat="server" Text="Cancelar" OnClick="btnCancelarValores_Click" />
                                                <asp:Button ID="btnAceptarValores" runat="server" CssClass="btn btn-primary"
                                                    Text="Aceptar" OnClick="btnAceptarValores_Click" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 20px;">
                                        <%--<div class="col-md-10 col-md-offset-1" id="divLista" runat="server">--%>
                                        <div class="auto-style1" style="margin-top: 20px;">
                                            <asp:GridView ID="gvCategorias"
                                                CssClass="table"
                                                runat="server"
                                                OnRowDataBound="gvCategorias_RowDataBound"
                                                OnRowCommand="gvCategorias_RowCommand"
                                                CellPadding="4"
                                                AutoGenerateColumns="False"
                                                ForeColor="#333333"
                                                GridLines="None" DataKeyNames="cod_categoria" AllowPaging="True"
                                                OnPageIndexChanging="gvCategorias_PageIndexChanging" PageSize="8">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Cod Categoria" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <p>
                                                                <asp:Label ID="lblCodigo" runat="server" Text=""></asp:Label>
                                                            </p>

                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Fecha Alta" ItemStyle-Width="20%">
                                                        <ItemTemplate>
                                                            <p>
                                                                <asp:Label ID="lblFecha_alta" runat="server" Text=""></asp:Label>
                                                            </p>

                                                        </ItemTemplate>
                                                        <ItemStyle Width="20%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Descripcion Categoria" ItemStyle-Width="50%">
                                                        <ItemTemplate>
                                                            <p>
                                                                <asp:Label ID="lblDes_categoria" runat="server" Text=""></asp:Label>
                                                            </p>

                                                        </ItemTemplate>
                                                        <ItemStyle Width="50%" />
                                                    </asp:TemplateField>

                                                    <%--<asp:TemplateField HeaderText="Sueldo Basico" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <p>
                                                        <asp:Label ID="lblSueldo_basico" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </ItemTemplate>
                                                <ItemStyle Width="20%" />
                                            </asp:TemplateField>--%>

                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtSueldo_basico"
                                                                CssClass="form-control"
                                                                Text='<%#Eval("sueldo_basico")%>'
                                                                Enabled="false"
                                                                runat="server"></asp:TextBox>
                                                        </ItemTemplate>
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
                                                                            CommandName="eliminar"
                                                                            CommandArgument="<%# Container.DataItemIndex %>"
                                                                            runat="server">
                                                                    <i class="fa fa-edit"></i>&nbsp Borrar
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

                                        <!-- /.box-body -->
                                        <div class="box-footer clearfix">
                                        </div>
                                    </div>
                                    <!-- /.box -->
                                </div>
                                <!-- /.col -->
                            </div>
                            <!-- /.row -->


                        </div>
                        <%--</section>--%>
                        <asp:HiddenField ID="hID" runat="server" />
                        <asp:Button ID="Button1" runat="server" Text="Button" Style="visibility: hidden;" />
                        <ajaxToolkit:ModalPopupExtender runat="server"
                            BackgroundCssClass="modalBackground"
                            PopupControlID="modalDatosCategorias"
                            BehaviorID="modalPopupExtender"
                            TargetControlID="Button1"
                            ID="modalPopupExtender">
                        </ajaxToolkit:ModalPopupExtender>
                        <div class="modal-dialog" id="modalDatosCategorias" runat="server" style="background-color: white; padding: 20px;">
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

                                <div class="modal-body" id="activity" style="min-height: 320px;">
                                    <div class="row">
                                        <div class="form-group col-md-4">
                                            <label>Codigo</label>
                                            <asp:TextBox ID="txtCodigo" CssClass="form-control"
                                                placeholder="Ingrese Codigo" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rv1" runat="server" ValidationGroup="cliente"
                                                Text="*" ForeColor="Red" Display="Dynamic"
                                                ErrorMessage="Ingrese Codigo" ControlToValidate="txtCodigo">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-4">
                                        </div>
                                        <div class="form-group col-md-4">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <label>Descripcion Categoria</label>
                                            <asp:TextBox ID="txtDes_categoria" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="cliente"
                                                Text="*" ForeColor="Red" Display="Dynamic"
                                                ErrorMessage="Ingrese la Descripcion de la Categoria" ControlToValidate="txtDes_categoria">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label>Sueldo Basico</label>
                                            <asp:TextBox ID="txtSueldo_basico" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rv4" runat="server" ValidationGroup="cliente"
                                                Text="*" ForeColor="Red" Display="Dynamic"
                                                ErrorMessage="Ingrese Sueldo Basico" ControlToValidate="txtSueldo_basico">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="modal-footer">
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ValidationGroup="cliente" />
                                    <asp:Button ID="btnCancelar" runat="server"
                                        CssClass="btn btn-default" Text="Cancelar"
                                        OnClick="btnCancelar_Click" />
                                    <asp:Button ID="btnAceptar" runat="server"
                                        ValidationGroup="cliente"
                                        CssClass="btn btn-primary" Text="Aceptar"
                                        OnClick="btnAceptar_Click" />

                                </div>

                                <!-- /.tab-pane -->

                                <!-- /.tab-pane -->

                                <!-- /.tab-pane -->
                            </div>
                        </div>
                        <%-- /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// --%>
                        <asp:Button ID="Button2" runat="server" Text="Button" Style="visibility: hidden;" />
                        <ajaxToolkit:ModalPopupExtender runat="server"
                            BackgroundCssClass="modalBackground"
                            PopupControlID="modalActualizarMontos"
                            BehaviorID="popupActualizarMontos"
                            TargetControlID="Button2"
                            ID="popupActualizarMontos">
                        </ajaxToolkit:ModalPopupExtender>
                        <div style="padding: 10px; width: 70%; background-color: White; box-shadow: 0px 0px 10px #000;" id="modalActualizarMontos" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <h3>Actualizar Sueldo Basico</h3>
                                    <%--<asp:Label ID="Label9" runat="server" Text="Asunto:" Width="100px"></asp:Label>
                            <asp:TextBox ID="txtFindAsunto" runat="server"></asp:TextBox>

                            &nbsp;<asp:ImageButton ID="imgbFindOficina" runat="server"
                                ImageUrl="~/App_Themes/Tema1/Images/search.png"
                                OnClick="imgbFindOficina_Click" CausesValidation="False" />
                            <br />--%>
                                    <div class="input-group">
                                        <input type="text" class="form-control" id="Text1" runat="server" />
                                        <span class="input-group-btn">
                                            <button class="btn btn-info" type="button" id="cmdBuscar2" runat="server" onserverclick="cmdBuscar2_ServerClick"
                                                causesvalidation="false">
                                                <span class="fa fa-search"></span>Buscar
                                            </button>
                                        </span>
                                    </div>
                                    <br />
                                    <div style="overflow: scroll; height: 150px;">
                                        <asp:GridView ID="gvCategorias2" runat="server"
                                            AutoGenerateColumns="False"
                                            CellPadding="4" ForeColor="#333333"
                                            GridLines="None" CssClass="grid"
                                            OnRowCommand="gvCategorias2_RowCommand" OnRowCreated="gvCategorias2_RowCreated"
                                            formnovalidate=""
                                            DataKeyNames="cod_categoria, sueldo_basico"
                                            AllowPaging="True"
                                            PageSize="8"
                                            OnPageIndexChanging="gvCategorias2_PageIndexChanging">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="cod_categoria" HeaderText="Codigo" />
                                                <asp:BoundField DataField="des_categoria" HeaderText="Descripcion" />
                                                <asp:BoundField DataField="sueldo_basico" HeaderText="Basico" />

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
                                            <%--<PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />--%>
                                            <PagerStyle CssClass="gridview"></PagerStyle>
                                            <RowStyle BackColor="#E3EAEB" />
                                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                            <SortedAscendingHeaderStyle BackColor="#246B61" />
                                            <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                            <SortedDescendingHeaderStyle BackColor="#15524A" />
                                        </asp:GridView>
                                        <asp:HiddenField ID="HiddenField2" runat="server" />
                                    </div>
                                    <div class="modal-footer">
                                        <asp:LinkButton ID="lbtnCancelarBuscador" CssClass="btn btn-default" runat="server" CausesValidation="False"
                                            OnClick="lbtnCancelarBuscador_Click">
                                            <i class="fa fa-times" aria-hidden="true"></i> Cancelar
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnSalirBuscador" CssClass="btn btn-default" runat="server" CausesValidation="False"
                                            OnClick="lbtnSalirBuscador_Click">
                                            <i class="fa fa-sign-out"></i> Salir
                                        </asp:LinkButton>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
