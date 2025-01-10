<%@ Page Title="" Language="C#" MasterPageFile="~/MP/Bootstrap.Master" AutoEventWireup="true" CodeBehind="listempleados.aspx.cs" Inherits="Web.secure.listempleados"
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
                -moz-box-shadow: 1px 1px 1px #111;
                -webkit-box-shadow: 1px 1px 1px #111;
                box-shadow: 1px 1px 1px #111;
                border-radius: 50%;
                padding: 5px 10px 5px 10px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" id="44446">
        function cmdPrintConstancia(url) {
            var url1 = url PopUp(url1, "_blank", "900", "800");
        }


        function cmdPrintCaratula(url) {
            var url1 = url;
            PopUp(url1, "_blank", "900", "800");
            this ._source = null;
            this ._popup = null;
        }

        function SelectAllCheckboxes(spanChk) {
            // Added as ASPX uses SPAN for checkbox var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;
            for (i = 0; i < elm.length; i++) if (elm[i].type == "checkbox" && elm[i].id != theBox.id){
              if (elm[i].checked != xState) elm[i].click();
            }
        }

        function AttachListener() {
            var elements = document.getElementsByTagName("INPUT");
            for (i = 0; i < elements.length; i++){
              if (IsCheckBox(elements[i]) && IsMatch(elements[i].id)) {
                AddEvent(elements[i], 'click', CheckChild);
              }
            }
        }
    </script>
    <div class="tab-pane" id="tabInforme" style="padding-top: 50px;">
        <div class="panel-body">
            <div class="container-fluid" style="border: solid lightgray 0.4px; background-color: white;">
                <div class="row" style="margin-top: 10px;">
                    <div class="col-md-12 col-md-offset-0">
                        <div class="box-body">
                            <div class="row">
                                <div class="box-header with-border">
                                    <div class="col-xs-12">
                                        <h3 class="box-title">Nomina Empleados</h3>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group" style="padding-bottom: 20px;">
                                    <div class="col-md-3">
                                        Busqueda Por
                                    <asp:DropDownList ID="ddFindBy" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                        <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                    </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        Descripción:
                                    <div class="input-group">
                                        <input type="text" class="form-control" id="txtInput" runat="server" />
                                        <span class="input-group-btn">
                                            <button class="btn btn-info" type="button" id="btnBuscar" runat="server"
                                                onserverclick="btnBuscar_Click">
                                                <span class="fa fa-search"></span>Buscar</button>
                                        </span>
                                    </div>
                                    </div>
                                    <div class="col-md-6">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- ////////////////////////////// GRILLA DETALLE /////////////////////////////////////// -->
                        <div class="auto-style1" style="margin-top: 20px; overflow: scroll;">
                            <asp:GridView ID="grdList"
                                runat="server"
                                AutoGenerateColumns="False"
                                EmptyDataText="No hay resultados..."
                                Width="100%"
                                CssClass=""
                                CellPadding="4" ForeColor="Black"
                                OnRowDataBound="grdList_RowDataBound"
                                DataKeyNames="legajo"
                                BackColor="White"
                                BorderColor="#CCCCCC"
                                BorderStyle="None"
                                GridLines="Horizontal"
                                AllowPaging="True"
                                OnPageIndexChanging="grdList_PageIndexChanging"
                                PageSize="10"
                                AlternatingRowStyle-CssClass="alt">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <input type="checkbox" id="chkAll" name="chkAll" onclick="javascript: SelectAllCheckboxes(this)" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Legajo">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlDetalles" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"legajo").ToString()%>'
                                                NavigateUrl='<%# "empleado.aspx?legajo=" +                                 
                                         Server.UrlEncode(DataBinder.Eval(Container.DataItem,"legajo").ToString())+"&op=modifica"%>'>
                                            </asp:HyperLink>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Left" Width="5%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                    <asp:BoundField DataField="fecha_ingreso" HeaderText="Fec Ingreso" />
                                    <asp:BoundField DataField="fecha_nacimiento" HeaderText="Fec Nac" />
                                    <asp:TemplateField HeaderText="Cod Cate">
                                        <ItemTemplate>
                                            <div>
                                                <p><strong><%#Eval("cod_categoria")%></strong></p>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Categoria">
                                        <ItemTemplate>
                                            <div>
                                                <p><small><%#Eval("des_categoria")%></small></p>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="des_tipo_liq" HeaderText="Tipo Liq" />
                                    <asp:BoundField DataField="nom_banco" HeaderText="Banco" />
                                    <asp:BoundField DataField="nro_caja_ahorro" HeaderText="Nº Caja Ahorro" />
                                    <asp:BoundField DataField="nro_cbu" HeaderText="Nº Cbu" />
                                    <asp:BoundField DataField="nro_documento" HeaderText="Nº Doc." Visible="False" />
                                    <asp:BoundField DataField="nro_cta_sb" HeaderText="Nº Cta Sb" Visible="False" />
                                    <asp:BoundField DataField="nro_cta_gastos" HeaderText="Nº Cta Gastos" Visible="False" />
                                    <asp:BoundField DataField="secretaria" HeaderText="Secretaria" Visible="False" />
                                    <asp:BoundField DataField="direccion" HeaderText="Direccion" Visible="False" />
                                    <asp:BoundField DataField="oficina" HeaderText="Oficina" Visible="False" />
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" />
                                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                <PagerStyle CssClass="gridview"></PagerStyle>
                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                <SortedDescendingHeaderStyle BackColor="#242121" />
                            </asp:GridView>
                        </div>

                        <div class="box-footer" style="text-align: right;">
                            <div class="row">
                                <div class="col-md-12">
                                    &nbsp;
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:LinkButton ID="lbtnNuevo" CssClass="btn btn-app btn-facebook" runat="server" OnClick="cmdNuevo_Click">
                                                        <i class="fa fa-user"></i>Nuevo
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lbtnRecibos" CssClass="btn btn-app bg-green-active" runat="server" OnClick="cmdRecibos_Click">
                                                        <i class="fa fa-files-o"></i>Recibos
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lbtnReportes" CssClass="btn btn-app bg-light-blue" runat="server" OnClick="btnReportes_Click">
                                                        <i class="fa fa-print"></i>Reportes
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lbtnSalir" CssClass="btn btn-app bg-orange" runat="server" OnClick="cmdSalir_Click">
                                                        <i class="fa fa-sign-out"></i>Salir
                                    </asp:LinkButton>

                                </div>
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


