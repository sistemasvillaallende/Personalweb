<%@ Page Title="" Language="C#" MasterPageFile="~/MP/Bootstrap.Master" AutoEventWireup="true" CodeBehind="aportes_jubilatorios.aspx.cs" Inherits="web.secure.aportes_jubilatorios" %>

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

        .auto-style3 {
            left: 0px;
            top: 10px;
        }
    </style>
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.19/css/jquery.dataTables.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row" style="padding-top: 60px;">
        <div class="col-md-10 col-md-offset-1">
            <div class="row">
                <div class="panel panel-primary">
                    <div class="panel-heading" style="height: 40px;">
                        <h4>Aportes Jubilatorios</h4>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <div class="col-md-8 col-md-offset-2" style="padding-top: 40px;">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label>
                                            Año : 
                                        </label>
                                        <asp:TextBox ID="txtAnio" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAnio" ErrorMessage="Debe Ingresar Año">*</asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="El valor debe ser de Tipo Numerico" Operator="DataTypeCheck" Type="Integer" ControlToValidate="txtAnio">*</asp:CompareValidator>
                                    </div>
                                    <div class="col-md-4">
                                        <label>
                                            Tipo Liq : 
                                        </label>

                                        <asp:DropDownList ID="cmbTipo_liq" runat="server" CssClass="form-control" AppendDataBoundItems="True" OnSelectedIndexChanged="cmbTipo_liq_SelectedIndexChanged"
                                            AutoPostBack="True">
                                            <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cmbTipo_liq" ErrorMessage="Debe Ingresar TipoLiquidacion" ValidationGroup="Validation1">*</asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="cmbTipo_liq" ErrorMessage="El valor debe ser Numerico" Operator="DataTypeCheck" Type="Integer" ValidationGroup="Validation1">*</asp:CompareValidator>
                                    </div>
                                    <div class="col-md-4">
                                        <label>
                                            Nro Liq : 
                                        </label>
                                        <asp:DropDownList ID="cmbNro_liq" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                            <asp:ListItem Selected="True" Value="0">Seleccionar</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate="cmbNro_liq" ErrorMessage="Debe Ingresar Nro Liquidacion" ValidationGroup="Validation1">*</asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator3" runat="server"
                                            ControlToValidate="cmbNro_liq" ErrorMessage="El valor debe ser Numerico"
                                            Operator="DataTypeCheck" Type="Integer" ValidationGroup="Validation1">*</asp:CompareValidator>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:RadioButtonList ID="rbOpcion" runat="server" RepeatDirection="Horizontal"
                                            ToolTip="Opciones del Reporte" CssClass="form-control" AutoPostBack="true">
                                            <asp:ListItem Enabled="true" Value="0">Aportes Jubilatorios</asp:ListItem>
                                            <asp:ListItem Enabled="true" Value="1">Aportes Jubilatorios x Sec.</asp:ListItem>
                                            <asp:ListItem Enabled="true" Value="2">Aportes Tarea Insalubre</asp:ListItem>
                                        </asp:RadioButtonList>
                                        &nbsp&nbsp
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-8 col-md-offset-2" style="text-align: right;">
                            <asp:Button ID="cmdConsulta" runat="server" CssClass="btn btn-facebook" Text="Consultar" OnClick="cmdConsulta_Click" />
                            <asp:Button ID="btnExportar" runat="server" CssClass="btn btn-success" OnClick="btnExportar_Click" Text="Exportar a Excel" />
                            <asp:Button ID="cmdSalir" runat="server" CssClass="btn btn-warning" Text="Salir" OnClick="cmdSalir_Click" CausesValidation="False" />
                        </div>
                        <div class="form-group">
                            <asp:ValidationSummary ID="ValidationSummary1" ForeColor="red" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="auto-style1">
                <div class="row" style="padding-top: 20px; overflow: scroll;">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table-condensed" CellPadding="4" ForeColor="Black" GridLines="Horizontal" BackColor="White"
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
</asp:Content>
