<%@ Page Title="" Language="C#" MasterPageFile="~/MP/MasterNew.Master" AutoEventWireup="true"
    CodeBehind="listempleados.aspx.cs" Inherits="Web.secure.listempleados"
    UICulture="es" Culture="es-MX" %>

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

        #ContentPlaceHolder1_grdList_filter {
            margin-bottom: 15px;
            float: left;
            position: relative;
            width: 100%;
            padding-bottom: 15px;
            text-align: left;
        }

        thead {
            display: none;
        }

        table {
            border: none;
        }
    </style>
    <style type="text/css">
        .table tr th {
            border-color: var(--border-color);
            background-color: var(--border-color);
            color: var(--color-800);
            text-transform: uppercase;
            font-size: 12px;
            height: 40px;
            vertical-align: middle;
        }
    </style>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.13.7/css/jquery.dataTables.css" />




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="https://cdn.datatables.net/1.13.7/js/jquery.dataTables.js"></script>

    <script type="text/javascript" id="44446">
        function cmdPrintConstancia(url) {
            var url1 = url;
            PopUp(url1, "_blank", "900", "800");
        }


        function cmdPrintCaratula(url) {
            var url1 = url;
            PopUp(url1, "_blank", "900", "800");
            this._source = null;
            this._popup = null;
        }

        function SelectAllCheckboxes(spanChk) {
            // Added as ASPX uses SPAN for checkbox var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;
            for (i = 0; i < elm.length; i++) if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
                if (elm[i].checked != xState) elm[i].click();
            }
        }

        function AttachListener() {
            var elements = document.getElementsByTagName("INPUT");
            for (i = 0; i < elements.length; i++) {
                if (IsCheckBox(elements[i]) && IsMatch(elements[i].id)) {
                    AddEvent(elements[i], 'click', CheckChild);
                }
            }
        }

    </script>
    <script>
        $(document).ready(function () {
            $('#<%=grdList.ClientID %>').dataTable({
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/1.10.16/i18n/Spanish.json"
                },
                order: false,
                pageLength: 5,
                dom: 'Bfrtip',
                buttons: [
                    'copy', 'csv', 'excel', 'pdf', 'print'
                ]
            });
        });
    </script>
    <div class="tab-pane" id="tabInforme">
        <div class="panel-body">
            <div class="container-fluid  p-3 mb-5 bg-white rounded"
                style="background-color: white; padding-left: 25px !important; padding-top: 5px !important;">
                <div class="row" style="margin-top: 10px;">
                    <div class="col-md-12 col-md-offset-0">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-8" style="text-align: right;">
                                    <asp:LinkButton ID="lbtnNuevo" CssClass="btn btn-outline-primary" runat="server" OnClick="cmdNuevo_Click">
                                                        <i class="fa fa-user"></i>&nbsp; Nuevo
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lbtnRecibos" CssClass="btn btn-outline-primary" runat="server" OnClick="cmdRecibos_Click">
                                                        <i class="fa fa-files-o"></i>&nbsp; Recibos
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lbtnReportes" CssClass="btn btn-outline-primary" runat="server" OnClick="btnReportes_Click">
                                                        <i class="fa fa-print"></i>&nbsp; Reportes
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lbtnSalir" CssClass="btn btn-outline-primary" runat="server" OnClick="cmdSalir_Click">
                                                        <i class="fa fa-sign-out"></i>&nbsp; Salir
                                    </asp:LinkButton>

                                </div>
                            </div>
                            <div class="row" style="display: none;">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label style="font-size: 16px; color: gray; margin-bottom: 5px;">
                                            Busqueda Por</label>
                                        <asp:DropDownList ID="ddFindBy" runat="server"
                                            Style="border-color: var(--bs-gray-400)"
                                            CssClass="form-control" AppendDataBoundItems="True">
                                            <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3" style="padding-top: 29px;">
                                    <div class="input-group">
                                        <input type="text" class="form-control"
                                            style="border-color: var(--bs-gray-400)"
                                            id="txtInput" runat="server" />
                                        <span class="input-group-btn">
                                            <button class="btn btn-info" type="button"
                                                id="btnBuscar" runat="server"
                                                style="border-bottom-left-radius: 0; border-top-left-radius: 0; height: 38px; color: white;"
                                                onserverclick="btnBuscar_Click">
                                                <span class="fa fa-search"></span>&nbsp;Buscar</button>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- ////////////////////////////// GRILLA DETALLE /////////////////////////////////////// -->
                    <div class="auto-style1" style="margin-top: -30px;">
                        <asp:GridView ID="grdList"
                            runat="server"
                            AutoGenerateColumns="False"
                            EmptyDataText="No hay resultados..."
                            Width="100%"
                            CssClass="table"
                            CellPadding="4" ForeColor="Black"
                            OnRowDataBound="grdList_RowDataBound"
                            DataKeyNames="legajo"
                            GridLines="Horizontal"
                            Font-Names="Ubuntu, sans-serif;"
                            AlternatingRowStyle-CssClass="alt">
                            <Columns>
                                <asp:TemplateField Visible="False">
                                    <HeaderTemplate>
                                        <input type="checkbox" id="chkAll" name="chkAll" onclick="javascript: SelectAllCheckboxes(this)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Legajo" Visible="False">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlDetalles" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"legajo").ToString()%>'
                                            NavigateUrl='<%# "empleado.aspx?legajo=" +                                 
                                         Server.UrlEncode(DataBinder.Eval(Container.DataItem,"legajo").ToString())+"&op=modifica"%>'>
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Left" Width="5%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <img src="../img/usuario.png" class="rounded-circle avatar" alt="">
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Empleado">
                                    <ItemTemplate>
                                        <p style="font-size: 14px; margin-top: 0; margin-bottom: 5px;">
                                            <strong><%#Eval("nombre")%></strong>
                                        </p>
                                        <p style="color: var(--bs-gray); font-size: 14px; margin-bottom: 5px;">
                                            Legajo: <%#Eval("legajo")%>
                                        </p>
                                        <p style="color: var(--bs-gray); font-size: 14px; margin-bottom: 0px;">
                                            Categoria: <%#Eval("cod_categoria")%>
                                        </p>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contacto" Visible="false">
                                    <ItemTemplate>
                                        <p style="font-size: 12px; margin-top: 0; margin-bottom: 5px;">
                                            <strong><%#Eval("telefonos")%></strong>
                                        </p>
                                        <p style="color: var(--bs-gray); font-size: 14px; margin-bottom: 5px;">
                                            <%#Eval("celular")%>
                                        </p>
                                        <p style="color: var(--bs-gray); font-size: 14px; margin-bottom: 0px;">
                                            <%#Eval("email")%>
                                        </p>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contratación">
                                    <ItemTemplate>
                                        <p style="font-size: 14px; margin-top: 0; margin-bottom: 5px;">
                                            <strong><%#Eval("des_tipo_liq")%></strong>
                                        </p>
                                        <p style="color: var(--bs-gray); font-size: 14px; margin-bottom: 5px;">
                                            Fecha de ingreso 
                                        </p>
                                        <p style="color: var(--bs-gray); font-size: 14px; margin-bottom: 5px;">
                                            <%#Eval("fecha_ingreso")%>
                                        </p>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <p style="font-size: 14px; margin-top: 0; margin-bottom: 5px;">
                                            <strong><%#Eval("secrectaria")%></strong>
                                        </p>
                                        <p style="color: var(--bs-gray); font-size: 14px; margin-bottom: 5px;">
                                            <%#Eval("direccion")%>
                                        </p>
                                        <p style="color: var(--bs-gray); font-size: 14px; margin-bottom: 0px;">
                                            <%#Eval("oficina")%>
                                        </p>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Datos Bancarios">
                                    <ItemTemplate>
                                        <p style="font-size: 14px; margin-top: 0; margin-bottom: 5px;">
                                            <strong><%#Eval("nom_banco")%></strong>
                                        </p>
                                        <p style="color: var(--bs-gray); font-size: 14px; margin-bottom: 5px;">
                                            Caja de ahorro: <%#Eval("nro_caja_ahorro")%>
                                        </p>
                                        <p style="color: var(--bs-gray); font-size: 14px; margin-bottom: 5px;">
                                            CBU: <%#Eval("nro_cbu")%>
                                        </p>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Datos Bancarios">
                                    <ItemTemplate>
                                        <div class="dropdown">
                                            <div class="btn-group dropleft">
                                                <button type="button" class="btn btn-secondary" data-toggle="dropdown" aria-expanded="false">
                                                    ...
                                                </button>
                                                <div class="dropdown-menu">
                                                    <a class="dropdown-item"
                                                        href="empleado.aspx?legajo=<%#Eval("legajo")%>&op=modifica">Recibos</a>
                                                    <a class="dropdown-item" href="#">Editar</a>
                                                    <a class="dropdown-item" href="#">Ver</a>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="nro_documento" HeaderText="Nº Doc." Visible="False" />
                                <asp:BoundField DataField="nro_cta_sb" HeaderText="Nº Cta Sb" Visible="False" />
                                <asp:BoundField DataField="nro_cta_gastos" HeaderText="Nº Cta Gastos" Visible="False" />
                                <asp:BoundField DataField="secretaria" HeaderText="Secretaria" Visible="False" />
                                <asp:BoundField DataField="direccion" HeaderText="Direccion" Visible="False" />
                                <asp:BoundField DataField="oficina" HeaderText="Oficina" Visible="False" />
                            </Columns>
                        </asp:GridView>
                    </div>

                    <div class="box-footer" style="text-align: right;">
                        <div class="row">
                            <div class="col-md-12">
                                &nbsp;
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:Button ID="Button1" runat="server" Text="Button" Style="visibility: hidden;" />
    <ajaxToolkit:ModalPopupExtender runat="server"
        BehaviorID="modalMSJ"
        TargetControlID="Button1"
        ID="modalMSJ"
        PopupControlID="modMSJ"
        BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <div id="modMSJ">
        <asp:UpdatePanel ID="uPanelMSj" runat="server">
            <ContentTemplate>
                <div class="modal-dialog">
                    <div class="alert alert-danger fade in" runat="server" id="divAlerta"
                        visible="false" role="alert">
                        <button type="button" class="close" runat="server" data-dismiss="alert" onclick="__doPostBack('<%=uPanelMSj.ClientID%>', 'AlertaMSJ');">
                            <span arial-hidden="true">&times;</span> <span class="sr-only">Cerrar</span>
                        </button>
                        <strong>Mensaje! </strong>
                        <br />
                        <p id="msj" runat="server">
                        </p>
                        <br />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>


