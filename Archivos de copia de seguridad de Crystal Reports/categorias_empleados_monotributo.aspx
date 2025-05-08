<%@ Page Language="C#" MasterPageFile="~/MP/MasterNew.Master" AutoEventWireup="true"
    CodeBehind="categorias_empleados_monotributo.aspx.cs" Inherits="web.secure.categorias_empleados_monotributo" %>
    <%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
        <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
            <link href="../App_Themes/dist/css/AdminLTE.min.css" rel="stylesheet" />
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
                                                <div class="alert alert-success alert-dismissable" runat="server"
                                                    id="divConfirma" visible="false" role="alert">
                                                    <button type="button" class="close" data-dismiss="alert"
                                                        onclick="__doPostBack('<%=PanelInfomacion.ClientID%>', 'Confirma');">
                                                        <span aria-hidden="true">×</span></button>
                                                    </button>
                                                    <h4>Aviso Importante!</h4>
                                                    <p id="msjConfirmar" runat="server">
                                                    </p>
                                                </div>

                                                <div class="alert alert-warning alert-dismissible" runat="server"
                                                    id="divError" visible="false" role="alert">
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
                                        <%--< /div>--%>
                                            <%--< /section>--%>
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
                                                            <h3 class="box-title">Categorias Empleados Monotributo</h3>
                                                        </div>
                                                        <br />
                                                    </div>
                                                    <!-- /.box-header -->
                                                    <div class="box-body" style="margin-top: 20px;">
                                                        <div class="row">
                                                            <%--<div class="col-md-10 col-md-offset-1">--%>
                                                                <hr style="border-top: 2px solid #9c9c9c;" />
                                                                <%--< /div>--%>
                                                        </div>
                                                        <div class="row">
                                                            <div class="form-group">
                                                                <div class="col-xs-6">

                                                                    <div
                                                                        class="formulario-busqueda d-flex justify-content-between">

                                                                        <input type="text" class="input-control col-10"
                                                                            id="txtInput" runat="server"
                                                                            placeholder="Buscar por Categoria" />


                                                                        <button class="btn-control busqueda w-100"
                                                                            type="button" id="btnBuscar" runat="server"
                                                                            onserverclick="btnBuscar_ServerClick">
                                                                            <span
                                                                                class="fa fa-search"></span>Buscar</button>

                                                                    </div>
                                                                </div>
                                                                <div class="col-xs-6">
                                                                    <div class="btn-group pull-right" id="divActualiza"
                                                                        runat="server">
                                                                        <asp:LinkButton ID="lbtnHistorial"
                                                                            CssClass="btn-control aceptar"
                                                                            runat="server"
                                                                            OnClick="lbtnHistorial_Click">
                                                                            <i class="fa fa-history"></i>&nbsp;Ver
                                                                            Historial
                                                                        </asp:LinkButton>
                                                                        <asp:LinkButton ID="lbtnActualizar_valores"
                                                                            CssClass="btn-control secondary"
                                                                            runat="server"
                                                                            OnClick="lbtnActualizar_valores_Click">
                                                                            <i class="fa fa-money"></i>&nbsp;Actualizar
                                                                            Valores
                                                                        </asp:LinkButton>
                                                                        <asp:LinkButton ID="lbtnNuevo"
                                                                            CssClass="btn-control primario"
                                                                            runat="server" OnClick="lbtnNuevo_Click">
                                                                            <i class="fa fa-plus"></i>&nbsp;Nueva
                                                                            Categoria
                                                                        </asp:LinkButton>
                                                                        <asp:LinkButton ID="LinkExportar"
                                                                            CssClass="btn-control excel" runat="server"
                                                                            OnClick="LinkExportar_Click">
                                                                            <i class="fa fa-download"></i>&nbsp;Exportar
                                                                            Excel
                                                                        </asp:LinkButton>
                                                                        <asp:LinkButton ID="lbtnSalir"
                                                                            CssClass="btn-control volver" runat="server"
                                                                            OnClick="lbtnSalir_Click">
                                                                            <i class="fa fa-sign-out"></i>&nbsp;Salir
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" style="text-align: right"
                                                                id="divAcepta" visible="false" runat="server">
                                                                <asp:Button ID="btnCancelarValores"
                                                                    CssClass="btn-control cancelar" runat="server"
                                                                    Text="Cancelar"
                                                                    OnClick="btnCancelarValores_Click" />
                                                                <asp:Button ID="btnAceptarValores" runat="server"
                                                                    CssClass="btn-control acepetar" Text="Aceptar"
                                                                    OnClick="btnAceptarValores_Click" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin-top: 20px;">
                                                        <%--<div class="col-md-10 col-md-offset-1" id="divLista"
                                                            runat="server">--%>
                                                            <div class="auto-style1" style="margin-top: 20px;">
                                                                <asp:GridView ID="gvCategoriasMono"
                                                                    CssClass="table table-bordered" runat="server"
                                                                    OnRowDataBound="gvCategoriasMono_RowDataBound"
                                                                    OnRowCommand="gvCategoriasMono_RowCommand"
                                                                    CellPadding="4" AutoGenerateColumns="False"
                                                                    ForeColor="#333333" GridLines="None"
                                                                    DataKeyNames="id_profesional_monotributo"
                                                                    AllowPaging="True"
                                                                    OnPageIndexChanging="gvCategoriasMono_PageIndexChanging"
                                                                    PageSize="8">
                                                                    <AlternatingRowStyle BackColor="White"
                                                                        ForeColor="#284775"></AlternatingRowStyle>
                                                                    <Columns>
                                                                        <asp:TemplateField
                                                                            HeaderText="Id categoria monotributo"
                                                                            ItemStyle-Width="10%">
                                                                            <ItemTemplate>
                                                                                <p>
                                                                                    <asp:Label ID="lblIdCateMono"
                                                                                        runat="server" Text="">
                                                                                    </asp:Label>
                                                                                </p>

                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="10%" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Fecha Alta"
                                                                            ItemStyle-Width="20%">
                                                                            <ItemTemplate>
                                                                                <p>
                                                                                    <asp:Label ID="lblFecha_alta"
                                                                                        runat="server" Text="">
                                                                                    </asp:Label>
                                                                                </p>

                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="20%" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField
                                                                            HeaderText="Descripcion Categoria"
                                                                            ItemStyle-Width="40%">
                                                                            <ItemTemplate>
                                                                                <p>
                                                                                    <asp:Label ID="lblCategoria"
                                                                                        runat="server" Text="">
                                                                                    </asp:Label>
                                                                                </p>

                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="40%" />
                                                                        </asp:TemplateField>

                                                                        <%--<asp:TemplateField HeaderText="Monto"
                                                                            ItemStyle-Width="30%">
                                                                            <ItemTemplate>
                                                                                <p>
                                                                                    <asp:Label ID="lblMonto"
                                                                                        runat="server" Text="">
                                                                                    </asp:Label>
                                                                                </p>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="30%" />
                                                                            </asp:TemplateField>--%>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtMonto"
                                                                                        CssClass="form-control"
                                                                                        Text='<%#Eval("monto")%>'
                                                                                        Enabled="false" runat="server">
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <div class="btn-group pull-right">
                                                                                        <button type="button"
                                                                                            class="btn btn-secondary dropdown-toggle"
                                                                                            data-toggle="dropdown"
                                                                                            aria-expanded="false">Acciones</button>
                                                                                        <ul class="dropdown-menu">

                                                                                            <li>
                                                                                                <asp:LinkButton
                                                                                                    ID="lbtnEditar"
                                                                                                    CommandName="editar"
                                                                                                    CommandArgument="<%# Container.DataItemIndex %>"
                                                                                                    runat="server"
                                                                                                    class="dropdown-item">
                                                                                                    <i
                                                                                                        class="fa fa-edit"></i>&nbsp
                                                                                                    Editar
                                                                                                </asp:LinkButton>
                                                                                            </li>

                                                                                            <li>
                                                                                                <asp:LinkButton
                                                                                                    ID="lbtnEliminar"
                                                                                                    CommandName="eliminar"
                                                                                                    CommandArgument="<%# Container.DataItemIndex %>"
                                                                                                    runat="server"
                                                                                                    class="dropdown-item">
                                                                                                    <i
                                                                                                        class="fa fa-edit"></i>&nbsp
                                                                                                    Borrar
                                                                                                </asp:LinkButton>
                                                                                            </li>
                                                                                        </ul>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                    </Columns>
                                                                    <EditRowStyle BackColor="#999999"></EditRowStyle>
                                                                    <PagerStyle CssClass="gridview-category">
                                                                    </PagerStyle>
                                                                    <SortedAscendingCellStyle BackColor="#E9E7E2">
                                                                    </SortedAscendingCellStyle>
                                                                    <SortedAscendingHeaderStyle BackColor="#506C8C">
                                                                    </SortedAscendingHeaderStyle>
                                                                    <SortedDescendingCellStyle BackColor="#FFFDF8">
                                                                    </SortedDescendingCellStyle>
                                                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE">
                                                                    </SortedDescendingHeaderStyle>
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
                                    <%--< /section>--%>
                                        <asp:HiddenField ID="hID" runat="server" />
                                        <asp:Button ID="Button1" runat="server" Text="Button"
                                            Style="visibility: hidden;" />
                                        <ajaxToolkit:ModalPopupExtender runat="server"
                                            BackgroundCssClass="modalBackground" PopupControlID="modalDatosCategorias"
                                            BehaviorID="modalPopupExtender" TargetControlID="Button1"
                                            ID="modalPopupExtender">
                                        </ajaxToolkit:ModalPopupExtender>

                                        <div id="modalDatosCategorias" runat="server">
                                            <div class="modal-windows">
                                                <div class="modal-header">

                                                    <h4 class="modal-title">
                                                        <asp:Label ID="lblTituloFormModal" runat="server" Text="Label">
                                                        </asp:Label>
                                                    </h4>

                                                    <button type="button" runat="server" id="btnCloseModal"
                                                        onserverclick="btnCloseModal_ServerClick" class="close"
                                                        data-dismiss="modal" aria-label="Close">
                                                        <span aria-hidden="true">×</span></button>

                                                </div>

                                                <div class="modal-body" id="activity">
                                                    <div class="row">
                                                        <div class="form-group col-md-4">
                                                            <label>id</label>
                                                            <asp:TextBox ID="txtCodigo" CssClass="input-control"
                                                                placeholder="Ingrese Codigo" runat="server">
                                                            </asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rv1" runat="server"
                                                                ValidationGroup="cliente" Text="*" ForeColor="Red"
                                                                Display="Dynamic" ErrorMessage="Ingrese Codigo"
                                                                ControlToValidate="txtCodigo">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-md-4">
                                                        </div>
                                                        <div class="form-group col-md-4">
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group col-md-6">
                                                            <label>Categoria</label>
                                                            <asp:TextBox ID="txtCategoria" CssClass="input-control"
                                                                runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                                                runat="server" ValidationGroup="cliente" Text="*"
                                                                ForeColor="Red" Display="Dynamic"
                                                                ErrorMessage="Ingrese la Descripcion de la Categoria"
                                                                ControlToValidate="txtCategoria">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            <label>Monto</label>
                                                            <asp:TextBox ID="txtMonto" CssClass="input-control"
                                                                runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rv4" runat="server"
                                                                ValidationGroup="cliente" Text="*" ForeColor="Red"
                                                                Display="Dynamic" ErrorMessage="Ingrese Sueldo Basico"
                                                                ControlToValidate="txtMonto">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="modal-footer">
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                                        ForeColor="Red" ValidationGroup="cliente" />
                                                    <asp:Button ID="btnCancelar" runat="server"
                                                        CssClass="btn-control cancelar" Text="Cancelar"
                                                        OnClick="btnCancelar_Click" />
                                                    <asp:Button ID="btnAceptar" runat="server" ValidationGroup="cliente"
                                                        CssClass="btn-control aceptar" Text="Aceptar"
                                                        OnClick="btnAceptar_Click" />

                                                </div>

                                                <!-- /.tab-pane -->

                                                <!-- /.tab-pane -->

                                                <!-- /.tab-pane -->
                                            </div>
                                        </div>
                                        <%-- ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                            --%>
                                            <asp:Button ID="Button2" runat="server" Text="Button"
                                                Style="visibility: hidden;" />
                                            <ajaxToolkit:ModalPopupExtender runat="server"
                                                BackgroundCssClass="modalBackground"
                                                PopupControlID="modalActualizarMontos"
                                                BehaviorID="popupActualizarMontos" TargetControlID="Button2"
                                                ID="popupActualizarMontos">
                                            </ajaxToolkit:ModalPopupExtender>
                                            <div style="padding: 10px; width: 70%; background-color: White; box-shadow: 0px 0px 10px #000;"
                                                id="modalActualizarMontos" runat="server">
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server"
                                                    UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <h3>Actualizar Sueldo Basico</h3>
                                                        <%--<asp:Label ID="Label9" runat="server" Text="Asunto:"
                                                            Width="100px">
                                                            </asp:Label>
                                                            <asp:TextBox ID="txtFindAsunto" runat="server">
                                                            </asp:TextBox>

                                                            &nbsp;
                                                            <asp:ImageButton ID="imgbFindOficina" runat="server"
                                                                ImageUrl="~/App_Themes/Tema1/Images/search.png"
                                                                OnClick="imgbFindOficina_Click"
                                                                CausesValidation="False" />
                                                            <br />--%>
                                                            <div class="input-group">
                                                                <input type="text" class="form-control" id="Text1"
                                                                    runat="server" />
                                                                <span class="input-group-btn">
                                                                    <button class="btn btn-info" type="button"
                                                                        id="cmdBuscar2" runat="server"
                                                                        onserverclick="cmdBuscar2_ServerClick"
                                                                        causesvalidation="false">
                                                                        <span class="fa fa-search"></span>Buscar
                                                                    </button>
                                                                </span>
                                                            </div>
                                                            <br />
                                                            <div style="overflow: scroll; height: 150px;">
                                                                <asp:GridView ID="gvCategoriasMono2" runat="server"
                                                                    AutoGenerateColumns="False" CellPadding="4"
                                                                    ForeColor="#333333" GridLines="None" CssClass="grid"
                                                                    OnRowCommand="gvCategoriasMono2_RowCommand"
                                                                    OnRowCreated="gvCategoriasMono2_RowCreated"
                                                                    formnovalidate=""
                                                                    DataKeyNames="id_profesional_monotributo, monto"
                                                                    AllowPaging="True" PageSize="8"
                                                                    OnPageIndexChanging="gvCategoriasMono2_PageIndexChanging">
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                    <Columns>
                                                                        <asp:BoundField
                                                                            DataField="id_profesional_monotributo"
                                                                            HeaderText="Codigo" />
                                                                        <asp:BoundField DataField="categoria"
                                                                            HeaderText="Descripcion" />
                                                                        <asp:BoundField DataField="monto"
                                                                            HeaderText="Basico" />

                                                                        <asp:TemplateField HeaderText="Seleccionar">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="imgbSeleccionar"
                                                                                    runat="server"
                                                                                    CommandName="selected"
                                                                                    ImageUrl="~/App_Themes/Tema1/Images/masGrilla.gif"
                                                                                    CausesValidation="False" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EditRowStyle BackColor="#7C6F57" />
                                                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True"
                                                                        ForeColor="White" />
                                                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True"
                                                                        ForeColor="White" />
                                                                    <%--<PagerStyle BackColor="#666666"
                                                                        ForeColor="White"
                                                                        HorizontalAlign="Center" />--%>
                                                                    <PagerStyle CssClass="gridview"></PagerStyle>
                                                                    <RowStyle BackColor="#E3EAEB" />
                                                                    <SelectedRowStyle BackColor="#C5BBAF"
                                                                        Font-Bold="True" ForeColor="#333333" />
                                                                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                                                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                                                                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                                                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                                                                </asp:GridView>
                                                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                                            </div>
                                                            <div class="modal-footer">
                                                                <asp:LinkButton ID="lbtnCancelarBuscador"
                                                                    CssClass="btn btn-default" runat="server"
                                                                    CausesValidation="False"
                                                                    OnClick="lbtnCancelarBuscador_Click">
                                                                    <i class="fa fa-times" aria-hidden="true"></i>
                                                                    Cancelar
                                                                </asp:LinkButton>
                                                                <asp:LinkButton ID="lbtnSalirBuscador"
                                                                    CssClass="btn btn-default" runat="server"
                                                                    CausesValidation="False"
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