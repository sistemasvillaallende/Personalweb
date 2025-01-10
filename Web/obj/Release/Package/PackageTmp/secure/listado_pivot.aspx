<%@ Page Title="" Language="C#" MasterPageFile="~/MP/Bootstrap.Master" AutoEventWireup="true" CodeBehind="listado_pivot.aspx.cs" Inherits="web.secure.listado_pivot" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            position: relative;
            min-height: 1px;
            float: left;
            width: 100%;
            left: 0px;
            top: 0px;
            padding-left: 25px;
            padding-right: 25px;
        }
    </style>

    <link href="../App_Themes/dist/css/AdminLTE.min.css" rel="stylesheet" />
    <script type="text/javascript" src="https://code.jquery.com/jquery-3.5.1.js"></script>
    <link rel="stylesheet" type="text/css" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.19/css/jquery.dataTables.css" />
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" />
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="tab-pane" id="tabInforme" style="padding-top: 50px;">
        <div class="panel-body">
            <div class="container-fluid" style="border: solid lightgray 0.4px; background-color: white;">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header">
                                <div class="widget-user-image">
                                    <span class="glyphicon glyphicon-file" style="float: left; font-size: 40px;"></span>
                                </div>
                                <!-- /.widget-user-image -->
                                <h3 class="widget-user-username"><strong>INFORME LIQUIDACION</strong></h3>
                                <%-- <a href="#" onclick="printDiv('tabInforme')" class="btn btn-default pull-right">
                                    <span class="fa fa-print" style="font-size: 30px;"></span>
                                </a>--%>
                            </div>
                            <div class="box-body">
                                <div class="box">
                                    <%--<br />--%>
                                    <section class="content-header">
                                        <div class="row">
                                            <div class="col-md-8 col-md-offset-2">
                                                <div class="row">
                                                    <div class="col-md-4">
                                                       <%-- <label>
                                                            Año : 
                                                        </label>--%>
                                                        <asp:TextBox ID="txtAnio" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                            Display="Dynamic"
                                                            ControlToValidate="txtAnio"
                                                            ErrorMessage="Debe Ingresar Año">*</asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="col-md-12">
                                                            &nbsp
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="col-md-12">
                                                            &nbsp
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <%--<label>
                                                            Tipo Liq : 
                                                        </label>--%>
                                                        <asp:DropDownList ID="cmbTipo_liq" runat="server" CssClass="form-control"
                                                            AppendDataBoundItems="True" OnSelectedIndexChanged="cmbTipo_liq_SelectedIndexChanged"
                                                            AutoPostBack="True">
                                                            <asp:ListItem Selected="True" Value="0">Seleccione Tipo Liquidacion</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                            Display="Dynamic"
                                                            ControlToValidate="cmbTipo_liq" ErrorMessage="Debe Ingresar TipoLiquidacion"
                                                            ValidationGroup="Validation1">*</asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="cmbTipo_liq"
                                                            Display="Dynamic"
                                                            ErrorMessage="El valor debe ser Numerico" Operator="DataTypeCheck" Type="Integer"
                                                            ValidationGroup="Validation1">*</asp:CompareValidator>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:DropDownList ID="cmbNro_liq" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                                            <asp:ListItem Selected="True" Value="0">Seleccione Liquidacion</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-6" style="text-align: left;">
                                                        <asp:LinkButton ID="lbtnConsultar" CssClass="btn btn-app bg-green-active" runat="server" OnClick="lbtnConsultar_Click">
                                                            <i class="fa fa-search"></i>&nbsp;Consultar
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnSalir" CssClass="btn btn-app bg-orange margin" runat="server" OnClick="cmdSalir_Click" CausesValidation="False">
                                                            <i class="fa fa-sign-out"></i>&nbsp;Salir
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    <%--</section>
                                    <section class="content">--%>
                                        <%--<div id="movimientos_caja"></div>--%>
                                        <div class="outer_div">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <hr style="border-top: 1px solid #a2a2a2;" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="auto-style1">
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
                                            </div>
                                        </div>
                                    </section>
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
                    "lengthMenu": [[4, 25, 50, -1], [4, 25, 50, "Todos"]],
                    "iDisplayLength": 4,
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
</asp:Content>


