<%@ Page Title="" Language="C#" MasterPageFile="~/MP/Bootstrap.Master" AutoEventWireup="true" CodeBehind="novedades2.aspx.cs" Inherits="web.secure.novedades2"
    UICulture="es" Culture="es-MX" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .Image {
            max_width: 100%;
            height: auto;
        }
    </style>
    <link href="../App_Themes/dist/css/AdminLTE.min.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.19/css/jquery.dataTables.css" />
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" />
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row" style="margin-top: 100px;">
        <div class="col-md-10 col-md-offset-1">
            <div class="row" style="margin-top: 1px; padding-top: 1px">
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
                            <button type="button" class="close" data-dismiss="alert" onclick="__doPostBack('<%=PanelInfomacion.ClientID%>', 'Error');">
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
        <div class="col-md-10 col-md-offset-1">
            <!-- general form elements -->
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">Carga de Conceptos de Liquidación</h3>
                </div>
                <div class="row">
                    <div class="col-md-10 col-md-offset-1">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Año</label>
                                        <asp:TextBox ID="txtAnio" TextMode="Number" CssClass="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rv1" runat="server" ErrorMessage="*"
                                            ValidationGroup="sueldo" Text="Ingrese el año" Display="Dynamic"
                                            ControlToValidate="txtAnio" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        &nbsp;
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
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Nro Liquidacion : </label>
                                        <asp:DropDownList ID="txtNro_liq" runat="server" CssClass="form-control"
                                            AppendDataBoundItems="True">
                                            <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="box-footer" style="text-align: right;">
                    <a class="btn btn-app btn-bitbucket"
                        onclick="abrirmodalConceptos();">
                        <i class="fa fa-file-excel-o"></i>Cargar Excel
                    </a>
                    <a href="#" class="btn btn-app bg-green-active"
                        onclick="abrirmodalVerformato()">
                        <i class="fa fa-file-photo-o"></i>&nbsp;Formato Excel
                    </a>
                    <asp:LinkButton ID="cmdSalir" runat="server" OnClick="cmdSalir_Click" Text=""
                        CssClass="btn btn-app bg-orange margin">
                        <i class="fa fa-sign-out"></i>Salir</asp:LinkButton>
                    <div class="row" style="text-align: left;">
                        <div class="col-md-12">
                            <hr />
                            <h4 class="modal-title">Vista de Conceptos</h4>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-8 col-md-offset-2">
                            <div class="form-group">
                                <%--<div class="box-body">--%>
                                <asp:GridView ID="gvConceptos" CssClass="table"
                                    AutoGenerateColumns="false"
                                    OnRowCommand="gvConceptos_RowCommand"
                                    OnRowDeleting="gvConceptos_RowDeleting"
                                    DataKeyNames="legajo, codigo, importe, nro_parametro"
                                    EmptyDataText="No hay resultados..."
                                    runat="server" CellPadding="4"
                                    ForeColor="#333333"
                                    GridLines="None">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                                    <Columns>
                                        <asp:BoundField DataField="legajo" HeaderText="Legajo">
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="codigo" HeaderText="Codigo">
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="importe" HeaderText="Importe" DataFormatString="{0:c}">
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nro_parametro" HeaderText="Nro" DataFormatString="{0:d}">
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        </asp:BoundField>
                                        <%--<asp:TemplateField HeaderText="Accion">
                                            <ItemTemplate>
                                                <asp:LinkButton
                                                    ID="lbtnElimiar"
                                                    CommandName="eliminar"
                                                    CommandArgument="<%# Container.DataItemIndex %>"
                                                    OnClientClick="return confirm('¿Está seguro de Eliminar este registro?');"
                                                    runat="server">
                                                    <i class="fa fa-close "></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Accion">
                                            <ItemTemplate>
                                                <asp:LinkButton
                                                    ID="lbtnDelete"
                                                    OnClientClick="return confirm('¿Está seguro de Eliminar este registro?');"
                                                    CommandName="delete"
                                                    CommandArgument="<%# Container.DataItemIndex %>"
                                                    runat="server">
                                                    <i class="fa fa-times"></i>&nbsp Eliminar
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                    </Columns>
                                    <EditRowStyle BackColor="#999999"></EditRowStyle>
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></FooterStyle>
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                    <PagerStyle HorizontalAlign="Center" BackColor="#284775" ForeColor="White"></PagerStyle>
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                                    <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                                    <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                                    <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                                </asp:GridView>
                                <%--</div>
                            <div class="box-footer"></div>--%>
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
    </div>
    <div class="modal fade in" id="modalConceptos">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span></button>
                    <h4 class="modal-title">Subir Archivos de Conceptos</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label>Subir archivo</label>
                        <asp:FileUpload ID="fUploadConceptos" CssClass="form-control" runat="server" />
                    </div>
                </div>
                <div class="modal-footer">
                    <button data-dismiss="modal" class="btn btn-default">Cancelar</button>
                    <asp:Button ID="btnConceptos_x_legajos" CssClass="btn btn-primary"
                        OnClientClick="this.disabled=true;this.value = 'Procesando...'" UseSubmitBehavior="false"
                        OnClick="btnConceptos_x_legajos_Click" runat="server" Text="Aceptar" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade in" id="modalVerformato">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span></button>
                    <h4 class="modal-title">Ejemplo Archivos de Conceptos</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <div id="Image">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/conceptos.jpg" Style="width: 500px; height: 550px;" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button data-dismiss="modal" class="btn btn-primary">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

    <script src="../App_Themes/bower_components/jquery/dist/jquery.min.js"></script>
    <script src="../App_Themes/bower_components/bootstrap/js/modal.js"></script>
    <script src="../App_Themes/bower_components/datatables.net/js/jquery.dataTables.js"></script>


    <script src="https://cdn.datatables.net/buttons/1.6.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.6.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.6.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.6.1/js/buttons.print.min.js"></script>
    <script>
        $.noConflict();
        jQuery(document).ready(function ($) {
            $('#' + '<%=gvConceptos.ClientID %>').DataTable(
                {
                    initComplete: function () {
                        $(this.api().table().container()).find('input').parent().wrap('<form>').parent().attr('autocomplete', 'off');
                    },
                    "language": {
                        "url": "//cdn.datatables.net/plug-ins/1.10.16/i18n/Spanish.json"
                    },
                    dom: 'Bfrtip',
                    "lengthMenu": [[8, 25, 50, -1], [8, 25, 50, "Todos"]],
                    "iDisplayLength": 10,
                    buttons: [
                        'excel', 'print'
                    ],
                    "order": [[0, "asc"]]
                    //buttons: ['excel', 'print'],
                    //"order": [[0, "desc"]]
                }
            );
        });

        function abrirmodalConceptos() {
            $('#modalConceptos').modal('show');
        }

        function abrirmodalVerformato() {
            $('#modalVerformato').modal('show');
        }
        //function abrirModalRapiPago() {
        //    $('#modalRapiPago').modal('show');
        //}


        //function abrirModalRapiPago() {
        //    $('#modalRapiPago').modal('show');
        //}
        //function abrirModalMacro() {
        //    $('#modalMacro').modal('show');
        //}
        //function abrirModalBanelco() {
        //    $('#modalBanelco').modal('show');
        //}
        //function asignarServicio(ID, DESCRIPCION, SUMA, MONTO) {
        //    $("#divListado").hide('slow');
        //    $("#divDatos").show('slow');
        //    $("#ContentPlaceHolder1_hIdServicio").val(ID);
        //    $("#ContentPlaceHolder1_txtConcepto").val(DESCRIPCION);
        //    $("#ContentPlaceHolder1_txtCostoUnit").val(MONTO);
        //    $("#ContentPlaceHolder1_txtCantConcept").val('1');
        //    $("#ContentPlaceHolder1_txtTot").val(MONTO);

        //    var f = new Date();
        //    var dia = parseInt(f.getDate());
        //    var mes = parseInt(f.getMonth() + 1);
        //    var anio = f.getFullYear();

        //    var d = dia;
        //    var m = mes;

        //    if (dia < 10) {
        //        d = '0' + dia;
        //    }
        //    if (mes < 10) {
        //        m = '0' + mes;
        //    }

        //    $('#ContentPlaceHolder1_txtFecha').val(anio + '-' + m + '-' + d);
        //    $("#ContentPlaceHolder1_txtObsConcepto").focus();
        //}
        //function calSubTotal() {
        //    var preUni = parseFloat($("#ContentPlaceHolder1_txtCostoUnit").val());
        //    var cant = parseFloat($("#ContentPlaceHolder1_txtCantConcept").val());

        //    $("#ContentPlaceHolder1_txtTot").val(cant * preUni);
        //}
    </script>
</asp:Content>
