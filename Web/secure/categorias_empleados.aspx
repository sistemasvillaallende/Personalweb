<%@ Page Title="" Language="C#" MasterPageFile="~/MP/MasterNew.Master" AutoEventWireup="true"
    CodeBehind="categorias_empleados.aspx.cs" Inherits="web.secure.categorias_empleados" %>

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
    </style>

    <style type="text/css">
        .gridview {
            background-color: #fff;
            height: 60px;
            padding: 2px;
            margin: 15px;
        }

            .gridview a {
                background-color: #cbcbcb;
                padding-top: 5px;
                padding-left: 10px;
                padding-bottom: 5px;
                padding-right: 10px;
                border-radius: 12%;
            }

                .gridview a:hover {
                    background-color: #a3a3a3;
                    color: #fff;
                    text-decoration: none;
                }

            .gridview span {
                background-color: #24a2ae;
                border-radius: 12%;
                padding-top: 5px;
                padding-left: 10px;
                padding-bottom: 5px;
                padding-right: 10px;
                color: #fff;
            }
            .desplegable li{
                padding: 10px;
                width: 100%;                                
            }
            .desplegable a {
                text-decoration: none;
                color: #333;
                transition: color 0.3s;
            }
            .desplegable li:hover {
                background-color: rgba(212, 212, 212, 0.424);
            }
            .desplegable a:hover {
                color: white;
                width: 100%;  
            }
            
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="p-4">
        <div class="row">
            <asp:UpdatePanel ID="uPanelCliente" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="LinkExportar" />
                </Triggers>
                <ContentTemplate>
                    <div class="row">
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
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="box" style="border-top: none;">
                                <h3>Categorias Empleados</h3>
                                <div class="box-body" style="margin-top: 5px;">

                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-xs-6">
                                                <div
                                                    class="formulario-busqueda d-flex justify-content-between">
                                                    <input type="text" class="input-control col-10"
                                                        id="txtInput" runat="server"
                                                        placeholder="Buscar por Categoria" />

                                                    <button class="btn-control busqueda"
                                                        type="button" id="btnBuscar" runat="server"
                                                        onserverclick="btnBuscar_ServerClick">
                                                        <span
                                                            class="fa fa-search"></span></button>
                                                </div>
                                            </div>
                                            <div class="col-xs-6">
                                                <div class="btn-group pull-right" id="divActualiza" runat="server">
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
                                                CssClass="btn-control aceptar" Text="Aceptar"
                                                OnClick="btnAceptarValores_Click" />
                                        </div>
                                    </div>
                                </div>

                                <div class="row" style="margin-top: 5px;">
                                    <%--<div class="col-md-10 col-md-offset-1" id="divLista"
                                                            runat="server">--%>
                                    <div style="margin-top: 5px;">
                                        <asp:GridView ID="gvCategorias"
                                            CssClass="table table-bordered" runat="server"
                                            OnRowDataBound="gvCategorias_RowDataBound"
                                            OnRowCommand="gvCategorias_RowCommand"
                                            CellPadding="4" AutoGenerateColumns="False"
                                            ForeColor="#333333" GridLines="None"
                                            DataKeyNames="cod_categoria" AllowPaging="True"
                                            OnPageIndexChanging="gvCategorias_PageIndexChanging"
                                            PageSize="8">
                                            <AlternatingRowStyle BackColor="White"
                                                ForeColor="#284775"></AlternatingRowStyle>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Cod Categoria"
                                                    ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <p>
                                                            <asp:Label ID="lblCodigo"
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
                                                <asp:TemplateField HeaderText="Cantidad Empleados"
                                                    ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <p>
                                                            <asp:Label ID="lblCantidad"
                                                                runat="server" Text="">
                                                            </asp:Label>
                                                        </p>

                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField
                                                    HeaderText="Descripcion Categoria"
                                                    ItemStyle-Width="38%">
                                                    <ItemTemplate>
                                                        <p>
                                                            <asp:Label ID="lblDes_categoria"
                                                                runat="server" Text="">
                                                            </asp:Label>
                                                        </p>

                                                    </ItemTemplate>
                                                    <ItemStyle Width="38%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtSueldo_basico"
                                                            CssClass="form-control"
                                                            Text='<%#Eval("sueldo_basico")%>'
                                                            Enabled="false" runat="server">
                                                        </asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <div class="btn-group dropleft">

                                                            <button type="button" 
                                                                class="btn btn-secondary" 
                                                                data-toggle="dropdown" 
                                                                aria-expanded="false">
                                                                    <i class="fa fa-bars"></i>
                                                            </button>

                                                            <ul class="dropdown-menu desplegable">
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
                                                                        CommandName="eliminar"
                                                                        CommandArgument="<%# Container.DataItemIndex %>"
                                                                        runat="server"
                                                                        class="dropdown-item">
                                                                        <i
                                                                            class="fa fa-edit"></i>&nbsp
                                                                        Borrar
                                                                    </asp:LinkButton>
                                                                </li>
                                                                <li>
                                                                    <asp:LinkButton
                                                                        ID="lbtaDetalles"
                                                                        CommandName="detalles"
                                                                        CommandArgument="<%# Container.DataItemIndex %>"
                                                                        runat="server"
                                                                        class="dropdown-item">
                                                                        <i
                                                                            class="fa fa-edit"></i>&nbsp
                                                                        Detalles
                                                                    </asp:LinkButton>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle BackColor="#999999"></EditRowStyle>
                                            <PagerStyle CssClass="gridview"></PagerStyle>
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

                                        <label>Codigo</label>
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
                                        <label>Descripcion Categoria</label>
                                        <asp:TextBox ID="txtDes_categoria" CssClass="input-control"
                                            runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                            runat="server" ValidationGroup="cliente" Text="*"
                                            ForeColor="Red" Display="Dynamic"
                                            ErrorMessage="Ingrese la Descripcion de la Categoria"
                                            ControlToValidate="txtDes_categoria">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Sueldo Basico</label>
                                        <asp:TextBox ID="txtSueldo_basico" CssClass="input-control"
                                            runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rv4" runat="server"
                                            ValidationGroup="cliente" Text="*" ForeColor="Red"
                                            Display="Dynamic" ErrorMessage="Ingrese Sueldo Basico"
                                            ControlToValidate="txtSueldo_basico">
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
                                                            Width="100px"></asp:Label>
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
                                        <button 
                                            class="btn btn-facebook" type="button"
                                            id="cmdBuscar2" runat="server"
                                            onserverclick="cmdBuscar2_ServerClick"
                                            causesvalidation="false">
                                            <span class="fa fa-search"></span>Buscar
                                        </button>
                                    </span>
                                </div>
                                <br />
                                <div style="overflow: scroll; height: 150px;">
                                    <asp:GridView ID="gvCategorias2" runat="server"
                                        AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" GridLines="None" CssClass="grid"
                                        OnRowCommand="gvCategorias2_RowCommand"
                                        OnRowCreated="gvCategorias2_RowCreated"
                                        formnovalidate=""
                                        DataKeyNames="cod_categoria, sueldo_basico"
                                        AllowPaging="True" PageSize="8"
                                        OnPageIndexChanging="gvCategorias2_PageIndexChanging">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="cod_categoria"
                                                HeaderText="Codigo" />
                                            <asp:BoundField DataField="des_categoria"
                                                HeaderText="Descripcion" />
                                            <asp:BoundField DataField="sueldo_basico"
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
