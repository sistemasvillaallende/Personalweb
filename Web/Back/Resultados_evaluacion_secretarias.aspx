<%@ Page Title="" Language="C#" MasterPageFile="~/MP/MP_Desempenio.Master" AutoEventWireup="true" CodeBehind="Resultados_evaluacion_secretarias.aspx.cs" Inherits="web.secure.Resultados_evaluacion_secretarias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css"
        href="https://cdn.datatables.net/1.10.24/css/jquery.dataTables.min.css" />
    <style>
        table.dataTable tbody tr {
            background-color: #fff;
            font-size: 13px;
            text-wrap: balance;
        }

        .buttons-excel {
            background-color: var(--bs-teal);
            border-color: var(--bs-teal);
            color: white;
            padding: 8px;
            width: 120px;
            border: none;
            border-radius: 10px;
            margin-bottom: 10px;
        }

        .dt-buttons {
            display: inline;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/apexcharts"></script>
    <div class="row g-2 clearfix row-deck">
        <div class="col-xl-12 col-lg-12 col-md-12" style="display: block">
            <div class="card">
                <div class="card-header border-0" style="padding-bottom: 0;">
                    <div class="row">
                        <div class="col-8">
                            <h5 style="padding-left: 5px; font-size: 20px; color: var(--primary-color); font-weight: 600;">Resultado evaluaciones de desempeño ->
                            </h5>
                        </div>
                        <div class="col-4">
                            <asp:DropDownList ID="DDLEvaluaciones"
                                CssClass="form-control"
                                AutoPostBack="true"
                                OnSelectedIndexChanged="DDLEvaluaciones_SelectedIndexChanged"
                                runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <hr style="margin-top: 10px; border-top: 3px solid lightgray; opacity: 1; margin-left: 5px; margin-right: 5px;" />
                        </div>
                    </div>

                </div>
                <div class="card-body" style="padding-top: 0;">
                    <asp:GridView ID="gvResultados"
                        CssClass="table"
                        OnRowDataBound="gvResultados_RowDataBound"
                        AutoGenerateColumns="false"
                        runat="server">
                        <Columns>
                            <asp:BoundField HeaderText="Leg." DataField="LEGAGO" />
                            <asp:BoundField HeaderText="Empleado" DataField="NOMBRE" />
                            <asp:BoundField HeaderText="Contratación" DataField="CLASIFICACION" />
                            <asp:BoundField HeaderText="Secretaría" DataField="SECRETARIA" />
                            <asp:BoundField HeaderText="Dirección" DataField="DIRECCION" />
                            <asp:BoundField HeaderText="Programa" DataField="PROGRAMA" />
                            <asp:BoundField HeaderText="Evaluador" DataField="EVALUADOR" />
                            <asp:BoundField HeaderText="Resultado" DataField="RESULTADO" />
                            <asp:TemplateField HeaderText="LINK">
                                <ItemTemplate>
                                    <div id="divLink" runat="server">
                                        <a href="Personas_fichas.aspx?idFicha=<%#Eval("ID_FICHA")%>&legajo=<%#Eval("LEGAGO")%>">
                                            <span class="fa fa-search-plus"></span>
                                        </a>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

    <script src="https://code.jquery.com/jquery-3.5.1.js"></script>
    <script src="https://cdn.datatables.net/1.10.24/js/jquery.dataTables.min.js"></script>
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
            $('#' + '<%=gvResultados.ClientID %>').DataTable(
                {
                    "language": {
                        "url": "//cdn.datatables.net/plug-ins/1.10.16/i18n/Spanish.json"
                    },
                    dom: 'Bfrtip',
                    buttons: [{
                        extend: "excel",
                        title: ""
                    }]
                }
            );
        });
    </script>

</asp:Content>
