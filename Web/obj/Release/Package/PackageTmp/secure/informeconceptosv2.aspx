﻿
<%@ Page Title="" Language="C#" MasterPageFile="~/MP/Bootstrap.Master" AutoEventWireup="true" CodeBehind="informeconceptosv2.aspx.cs"
    Inherits="web.secure.informeconceptosv2" %>

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
    <%--<link href="../App_Themes/stacktable.css?v=1.0" rel="stylesheet" />--%>
    <style>
        .headerDerecha {
            text-align: right;
        }

        .checkbox .btn, .checkbox-inline .btn {
            padding-left: 3em;
            min-width: 8em;
        }

        .checkbox label, .checkbox-inline label {
            text-align: left;
            padding-left: 0.5em;
        }

        .checkbox input[type="checkbox"] {
            float: none;
            margin-left: -80px;
        }

        td {
            display: table-cell;
            vertical-align: middle !important;
        }

        @media (max-width: 800px) {
            .ocultar {
                display: none;
            }

            .HiddenCol {
                display: none;
            }
        }
    </style>

    <style>
        @page {
            size: A4;
            margin: 80px;
        }

        @media print {
            html, body {
                width: 210mm;
                height: 297mm;
            }
        }
    </style>

    <style type="text/css">
        .encabezado {
            border-style: solid;
            border-width: 1px;
            text-align: center;
        }

        td, th {
            padding-left: 10px;
        }
    </style>

    <script type="text/javascript" src="https://code.jquery.com/jquery-3.5.1.js"></script>
    <link rel="stylesheet" type="text/css" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.19/css/jquery.dataTables.css" />
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" />
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="tab-pane" id="tabInforme" style="padding-top: 50px;">
        <div class="panel-body">
            <div class="container" style="border: solid lightgray 0.4px; background-color: white;">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header">
                                <%--<h3 class="box-title">Productos</h3>--%>
                                <div class="widget-user-image">
                                    <span class="glyphicon glyphicon-file" style="float: left; font-size: 40px;"></span>
                                </div>
                                <!-- /.widget-user-image -->
                                <h3 class="widget-user-username"><strong>INFORME de CONCEPTO de Liquidación</strong></h3>
                                <a href="#" onclick="printDiv('tabInforme')" class="btn btn-default pull-right">
                                    <span class="fa fa-print" style="font-size: 30px;"></span>
                                </a>
                            </div>
                            <div class="box-body">
                                <div class="col-md-12">
                                    <%--<div id="divFiltros">
                                        <asp:UpdatePanel ID="uPanelFiltros" UpdateMode="Conditional" runat="server">--%>
                                    <%--<Triggers>
                                                <asp:PostBackTrigger ControlID="btnExportExcel" />
                                            </Triggers>--%>
                                    <%--<ContentTemplate>--%>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <h3 style="color: #367fa9;">Filtros</h3>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <label>Año Liquidacion</label>
                                            <asp:DropDownList ID="ddlPeriodos" CssClass="form-control" runat="server">
                                                <asp:ListItem Text="Sin filtro" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Año a partir de" Value="1"></asp:ListItem>
                                                <%--<asp:ListItem Text="Año hasta" Value="2" Enabled="false"></asp:ListItem>--%>
                                                <asp:ListItem Text="Año entre" Value="3"></asp:ListItem>
                                            </asp:DropDownList>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:TextBox ID="txtDesde" Enabled="false" CssClass="form-control" runat="server" Placeholder="Año Desde"></asp:TextBox>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:TextBox ID="txtHasta" Enabled="false" CssClass="form-control" runat="server" Placeholder="Año Hasta"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <label>Tipo Liquidacion</label>
                                            <asp:DropDownList ID="cmbTipo_liq" runat="server" CssClass="form-control"
                                                AppendDataBoundItems="True">
                                                <asp:ListItem Selected="True" Value="0">Seleccione Tipo Liquidacion</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                Display="Dynamic"
                                                ControlToValidate="cmbTipo_liq" ErrorMessage="Debe Ingresar TipoLiquidacion"
                                                ValidationGroup="consulta">*</asp:RequiredFieldValidator>
                                            <br />
                                            <div class="row">
                                                <%--<div class="col-md-12">
                                                                <asp:DropDownList ID="cmbNro_liq" runat="server" CssClass="form-control"
                                                                    AppendDataBoundItems="True">
                                                                    <asp:ListItem Selected="True" Value="0">Seleccione Liquidacion</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <label>Conceptos Liquidacion</label>
                                            <asp:TextBox ID="txtCod_concepto" runat="server" CssClass="form-control"
                                                TextMode="Number" placeholder="Cod Concepto" OnTextChanged="txtCod_concepto_TextChanged"
                                                AutoPostBack="true">
                                            </asp:TextBox>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:DropDownList ID="ddConcepto" runat="server" CssClass="form-control"
                                                        AppendDataBoundItems="True" OnSelectedIndexChanged="ddConcepto_SelectedIndexChanged"
                                                        AutoPostBack="true">
                                                        <asp:ListItem Selected="True" Value="0">Seleccione Concepto</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <label>Legajo/Empleado</label>
                                            <asp:TextBox ID="txtLegajo" runat="server" CssClass="form-control"
                                                TextMode="Number" placeholder="Legajo" OnTextChanged="txtLegajo_TextChanged"
                                                AutoPostBack="true">
                                            </asp:TextBox>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:DropDownList ID="ddLegajos" runat="server" CssClass="form-control"
                                                        AppendDataBoundItems="True" OnSelectedIndexChanged="ddLegajos_SelectedIndexChanged"
                                                        AutoPostBack="True">
                                                        <asp:ListItem Selected="True" Value="0">Seleccione Legajo</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12" style="text-align: right;">
                                            <div class="btn-group" style="padding-bottom: 20px;">
                                                <button type="button" class="btn btn-info" id="btnClearFiltros"
                                                    runat="server" onserverclick="btnClearFiltros_ServerClick">
                                                    <span class="glyphicon glyphicon-erase"></span>&nbsp;Limpiar Filtros
                                                </button>
                                                <asp:Button ID="cmdConsulta" runat="server" Text="Consultar" class="btn btn-success"
                                                    ValidationGroup="consulta" OnClick="cmdConsulta_Click" />
                                                <%--<asp:Button ID="cmdExportar" runat="server" CssClass="btn btn-success"
                                                            OnClick="cmdExportar_Click" Text="Exportar a Excel" />--%>
                                                <asp:Button ID="cmdSalir" runat="server" CssClass="btn btn-success"
                                                    OnClick="cmdSalir_Click" Text="Salir" />
                                                <%-- <button type="button" runat="server" id="btnExportExcel" onserverclick="btnExportExcel_ServerClick"
                                                                class="btn btn-success" data-toggle="modal" data-target="#page-change-name">
                                                                <span class="glyphicon glyphicon-save"></span>&nbsp; Exportar a Excel
                                                            </button>--%>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- </ContentTemplate>
                                        </asp:UpdatePanel>--%>
                                </div>
                            </div>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ValidationGroup="consulta" />
                            <%--<section class="content">--%>
                            <%--<div id="movimientos_caja"></div>--%>
                            <%--<div class="outer_div">--%>
                            <div class="row">
                                <div class="col-md-12">
                                    <h3 style="color: dimgray;"><strong>
                                        <asp:Label ID="lblTituloInforme" Font-Bold="true" Font-Size="18" ForeColor="GrayText" runat="server" Text="">
                                        </asp:Label>
                                    </strong></h3>
                                </div>
                            </div>
                            <%--<div class="row">
                                        <div class="col-md-12">
                                            <hr style="border-top: 1px solid #a2a2a2;" />
                                        </div>
                                    </div>--%>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row" style="padding-top: 10px; overflow: scroll;">
                                        <asp:GridView ID="GridView1" runat="server" CssClass="display compact" CellPadding="4"
                                            ForeColor="Black" GridLines="Horizontal" BackColor="White"
                                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" Width="100%">
                                            <FooterStyle BackColor="#CCCC99" ForeColor="Black"></FooterStyle>
                                            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                            <PagerStyle HorizontalAlign="Right" BackColor="White" ForeColor="Black"></PagerStyle>
                                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
                                            <SortedAscendingCellStyle BackColor="#F7F7F7"></SortedAscendingCellStyle>
                                            <SortedAscendingHeaderStyle BackColor="#4B4B4B"></SortedAscendingHeaderStyle>
                                            <SortedDescendingCellStyle BackColor="#E5E5E5"></SortedDescendingCellStyle>
                                            <SortedDescendingHeaderStyle BackColor="#242121"></SortedDescendingHeaderStyle>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                            <%-- </div>--%>
                            <%-- </section>--%>
                        </div>
                        <div>
                            <br />
                        </div>
                    </div>
                </div>
                <%--</div>--%>
            </div>
        </div>
    </div>

    <script src="../App_Themes/jQuery-2.1.4.min.js"></script>
    <script src="../App_Themes/jquery.dataTables.js"></script>
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
            $('#' + '<%=GridView1.ClientID %>').DataTable(
                {
                    initComplete: function () {
                        $(this.api().table().container()).find('input').parent().wrap('<form>').parent().attr('autocomplete', 'off');
                    },
                    "language": {
                        "url": "//cdn.datatables.net/plug-ins/1.10.16/i18n/Spanish.json"

                    },
                    dom: 'Bfrtip',
                    "lengthMenu": [[8, 25, 50, -1], [8, 25, 50, "Todos"]],
                    "iDisplayLength": 8,
                    buttons: [
                        'excel', 'print'
                    ]
                }
            );

            //$('#modalAdd').on('shown.bs.modal', function () {
            //    $('input:visible:enabled:first', this).focus();
            //});
        });
        // Code that uses other library's $ can follow here.
    </script>
    <script>
        $(document).ready(function (e) {
            $("#ContentPlaceHolder1_ddlPeriodos").change(function () {
                cambioPeriodo();
            });
            function cambioPeriodo() {
                var selectedVal = $('#ContentPlaceHolder1_ddlPeriodos option:selected').attr('value');
                if (selectedVal == 1) {
                    $("#ContentPlaceHolder1_txtHasta").attr("disabled", true);
                    $("#ContentPlaceHolder1_txtDesde").removeAttr("disabled");
                    //$("#ContentPlaceHolder1_cmbNro_liq").prop("disabled",true);
                    $("#ContentPlaceHolder1_txtDesde").focus();
                }
                if (selectedVal == 2) {
                    $("#ContentPlaceHolder1_txtDesde").attr("disabled", true);
                    $("#ContentPlaceHolder1_txtHasta").removeAttr("disabled");
                    $("#ContentPlaceHolder1_txtHasta").focus();
                }
                if (selectedVal == 3) {
                    $("#ContentPlaceHolder1_txtDesde").removeAttr("disabled");
                    $("#ContentPlaceHolder1_txtHasta").removeAttr("disabled");
                    $("#ContentPlaceHolder1_txtDesde").focus();
                }
                if (selectedVal == 0) {
                    $("#ContentPlaceHolder1_txtDesde").attr("disabled", true);
                    $("#ContentPlaceHolder1_txtHasta").attr("disabled", true);
                }
                $("#ContentPlaceHolder1_txtHasta").val('');
                $("#ContentPlaceHolder1_txtDesde").val('');
            }

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {
                $("#ContentPlaceHolder1_ddlPeriodos").change(function () {
                    cambioPeriodo();
                });
                function cambioPeriodo() {
                    var selectedVal = $('#ContentPlaceHolder1_ddlPeriodos option:selected').attr('value');
                    if (selectedVal == 1) {
                        $("#ContentPlaceHolder1_txtHasta").attr("disabled", true);
                        $("#ContentPlaceHolder1_txtDesde").removeAttr("disabled");
                    }
                    if (selectedVal == 2) {
                        $("#ContentPlaceHolder1_txtDesde").attr("disabled", true);
                        $("#ContentPlaceHolder1_txtHasta").removeAttr("disabled");
                    }
                    if (selectedVal == 3) {
                        $("#ContentPlaceHolder1_txtDesde").removeAttr("disabled");
                        $("#ContentPlaceHolder1_txtHasta").removeAttr("disabled");
                        alert('2 vuelta,3')
                    }
                    if (selectedVal == 0) {
                        $("#ContentPlaceHolder1_txtDesde").attr("disabled", false);
                        $("#ContentPlaceHolder1_txtHasta").attr("disabled", false);
                    }
                    $("#ContentPlaceHolder1_txtHasta").val('');
                    $("#ContentPlaceHolder1_txtDesde").val('');
                }
            });
        });
    </script>
</asp:Content>



