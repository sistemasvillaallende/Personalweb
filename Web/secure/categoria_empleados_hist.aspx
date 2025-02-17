<%@ Page Language="C#" MasterPageFile="~/MP/MasterNew.Master"  AutoEventWireup="true" CodeBehind="categoria_empleados_hist.aspx.cs" Inherits="web.secure.categoria_empleados_hist" %>

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
                <Triggers>
                    <asp:PostBackTrigger ControlID="LinkExportar" />
                </Triggers>
                <ContentTemplate>
                    <div class="row" style="margin-top: 30px; padding-top: 30px">
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
                
                    <%--<section class="content">--%>

                    <div class="outer_div">

                        <div class="row">
                            <div class="col-md-12">
                                <div class="box">
                                    <div class="box-header with-border">
                                        <div class="col-md-12">
                                            <h3 class="box-title">Historial categoria empleados </h3>
                                        </div>
                                        <br />
                                    </div>
                                 
                                    <div class="row" style="margin-top: 20px;">
                                        <%--<div class="col-md-10 col-md-offset-1" id="divLista" runat="server">--%>
                                        <div class="auto-style1" style="margin-top: 20px;">
                                            <asp:GridView ID="gvCategoriasHist"
                                                CssClass="table"
                                                runat="server"
                                                OnRowDataBound="gvCategoriasHist_RowDataBound"
                                                CellPadding="4"
                                                AutoGenerateColumns="False"
                                                ForeColor="#333333"
                                                GridLines="None" DataKeyNames="cod_categoria" AllowPaging="True"
                                                OnPageIndexChanging="gvCategoriasHist_PageIndexChanging" PageSize="10">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="cod. Categoria " ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <p>
                                                                <asp:Label ID="lblCategoria" runat="server" Text=""></asp:Label>
                                                            </p>

                                                        </ItemTemplate>
                                                        <ItemStyle Width="50%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Categoria " ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <p>
                                                                <asp:Label ID="lblDesCategoria" runat="server" Text=""></asp:Label>
                                                            </p>

                                                        </ItemTemplate>
                                                        <ItemStyle Width="50%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Numero movimiento" ItemStyle-Width="50%">
                                                        <ItemTemplate>
                                                            <p>
                                                                <asp:Label ID="lblMovimiento" runat="server" Text=""></asp:Label>
                                                            </p>

                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Fecha Movimiento" ItemStyle-Width="20%">
                                                        <ItemTemplate>
                                                            <p>
                                                                <asp:Label ID="lblFechaMovimiento" runat="server" Text=""></asp:Label>
                                                            </p>

                                                        </ItemTemplate>
                                                        <ItemStyle Width="20%" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Sueldo" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <p>
                                                        <asp:Label ID="lblSueldo" runat="server" Text=""></asp:Label>
                                                    </p>
                                                </ItemTemplate>
                                                <ItemStyle Width="20%" />
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
                                        <div class="box-footer clearfix">
                                        </div>
                                    </div>
                                </div>
                            </div>


                        </div>
                            </div>
                        </div>

                            
                </ContentTemplate>
        </div>
    </div>
</asp:Content> 


<!-- <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

                    <div class="auto-style1" style="margin-top: -30px;">
                        <asp:GridView ID="gvCategoriasHist"
                            runat="server"
                            AutoGenerateColumns="False"
                            EmptyDataText="No hay resultados..."
                            Width="100%"
                            CssClass="table"
                            CellPadding="4" ForeColor="Black"
                            OnRowDataBound="gvCategoriaHist_RowDataBound"
                            OnPageIndexChanging="gvCategoriasHist_PageIndexChanging"
                            DataKeyNames="cod_categoria"
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
                                <asp:TemplateField HeaderText="Cod_categoria" Visible="False">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlDetalles" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"cod_categoria").ToString()%>'
                                            NavigateUrl='<%# "empleado.aspx?legajo=" +                                 
                                         Server.UrlEncode(DataBinder.Eval(Container.DataItem,"cod_categoria").ToString())+"&op=modifica"%>'>
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Left" Width="5%" />
                                <asp:TemplateField HeaderText="Cod. Categoria">
                                    <ItemTemplate>
                                        <p style="font-size: 14px; margin-top: 0; margin-bottom: 5px;">
                                            <strong>Cod. categoria</strong>
                                        </p>
                                        <p style="color: var(--bs-gray); font-size: 14px; margin-bottom: 5px;">
                                            Legajo: <%#Eval("cod_categoria")%>
                                        </p>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Descripcion Categoria">
                                    <ItemTemplate>                                       
                                        <p style="color: var(--bs-gray); font-size: 14px; margin-bottom: 5px;">
                                            <%#Eval("des_categoria")%>
                                        </p>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <p style="font-size: 14px; margin-top: 0; margin-bottom: 5px;">
                                            <strong><%#Eval("item")%></strong>
                                        </p>                                  
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sueldo">
                                    <ItemTemplate>
                                        <p style="font-size: 14px; margin-top: 0; margin-bottom: 5px;">
                                            <strong><%#Eval("sueldo_basico")%></strong>
                                        </p>
                                       
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


 -->
