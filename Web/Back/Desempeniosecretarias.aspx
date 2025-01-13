<%@ Page Title="" Language="C#" MasterPageFile="~/MP/MP_Desempenio.Master"   AutoEventWireup="true"
    CodeBehind="Desempeniosecretarias.aspx.cs" Inherits="web.secure.Desempeniosecretarias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        thead, tbody, tfoot, tr, td, th {
            border-color: inherit;
            border-style: solid;
            border-width: 0;
            font-size: 14px;
            vertical-align: middle;
        }

        .table tr td {
            border-color: var(--border-color);
            color: var(--color-500);
            background-color: transparent;
            padding-top: 5px !important;
            padding-bottom: 5px !important;
            vertical-align: middle;
        }
    </style>
    <link href="../../App_Themes/NewTheme/dist/apexcharts.css?v=1" rel="stylesheet" />
    <style>
        .table tr td {
            border-color: var(--border-color);
            background-color: transparent;
            padding-top: 5px;
            padding-bottom: 5px;
            font-size: 14px;
            color: var(--bs-gray-600);
        }

        .apexcharts-canvas text {
            fill: gray !important;
            font-size: 16px;
        }
    </style>
    <style>
        .apexcharts-legend-text {
            position: relative;
            font-size: 14px;
            line-height: 1.5;
        }

        .apexcharts-text tspan {
            font-family: inherit;
            font-size: 14px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hIdSecretaria" runat="server" />
    <asp:HiddenField ID="HAnio" runat="server" />
    <script src="https://cdn.jsdelivr.net/npm/apexcharts"></script>
    <div class="row g-2 clearfix row-deck" style="padding-top: 25px;">
        <div class="row">
            <div class="col-md-9">
                <h3 style="padding-left: 5px; font-size: 20px; color: var(--primary-color); font-weight: 600;"
                    id="lblSecretaria" runat="server"></h3>
                <hr style="border-top: solid 1px; margin-top: 5px; margin-bottom: 25px;" />
            </div>
            <div class="col-md-3" style="text-align: right;">
                <a class="btn btn-outline-primary" id="btnSalir" runat="server"
                    onserverclick="btnSalir_ServerClick" href="#">
                    <span class="fa fa-sign-out"></span>
                    Volver
                </a>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-8">
            <h5 style="padding-left: 5px; font-size: 20px; color: var(--primary-color); font-weight: 600;">Resultado evaluaciones de desempeño ->
            </h5>
        </div>
        <div class="col-4">
            <asp:DropDownList ID="DDLEvaluaciones"
                CssClass="form-control"
                AutoPostBack="true"

                runat="server">
            </asp:DropDownList>
        </div>
    </div>
    <div class="row" id="divSecretarias" runat="server">
        <div class="col-md-12" style="text-align: center;">
            <div class="card text-center">
                <asp:GridView
                    AutoGenerateColumns="false"
                    CssClass="table"
                    ID="gvSecretarias"
                    DataKeyNames="id_secretaria, COD_USUARIO, legajo"
                    OnRowDataBound="gvSecretarias_RowDataBound"
                    OnRowCommand="gvSecretarias_RowCommand"
                    runat="server">
                    <Columns>
                        <asp:BoundField HeaderText="Secretaría" DataField="SECRETARIA" />
                        <asp:BoundField HeaderText="Secretario" DataField="SECRETARIO" />
                        <asp:BoundField HeaderText="Diracciones a cargo" DataField="DIRECCIONES" />
                        <asp:BoundField HeaderText="Cant. Personal" DataField="PERSONAL" />
                        <asp:TemplateField HeaderText="Permiso Evaluar">
                            <ItemTemplate>
                                <p id="pPermiso" runat="server"></p>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div class="dropdown">
                                    <div class="btn-group dropleft">
                                        <button type="button" class="btn btn-secondary" data-toggle="dropdown" aria-expanded="false">
                                            ...
                                        </button>
                                        <div class="dropdown-menu">
                                            <a class="dropdown-item"
                                                href="Desempeniosecretarias.aspx?id_secretaria=<%#Eval("id_secretaria")%>&anio=2024">Ver desempeño Secretaría</a>
                                            <a class="dropdown-item"
                                                href="DesempenioDirecciones.aspx?id_secretaria=<%#Eval("id_secretaria")%>">Ver Direcciones
                                            </a>
                                            <asp:LinkButton
                                                class="dropdown-item"
                                                CommandArgument="<%#Container.DataItemIndex%>"
                                                CommandName="asignarpermiso"
                                                ID="btnAsignarPermiso"
                                                runat="server">
                                                Asignar permiso
                                            </asp:LinkButton>
                                            <asp:LinkButton
                                                class="dropdown-item"
                                                CommandArgument="<%#Container.DataItemIndex%>"
                                                CommandName="quitarpermiso"
                                                ID="btnQuitarPermiso"
                                                runat="server">
                                                Quitar permiso
                                            </asp:LinkButton>
                                            <asp:LinkButton
                                                class="dropdown-item"
                                                CommandArgument="<%#Container.DataItemIndex%>"
                                                CommandName="cambiarevaluador"
                                                ID="btnCambiarEvaluador"
                                                runat="server">
                                                Cambiar evaluador
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </div>
        </div>
    </div>
    <div class="row" id="divDetalleSecretaria" runat="server">
        <div class="col-xl-3 col-lg-3 col-md-3">
            <div class="card">
                <div class="card-header border-0" style="padding-bottom: 0;">
                    <h5 style="padding-left: 5px; font-size: 20px; color: var(--primary-color); font-weight: 600;">Estado Evaluador
                    </h5>
                    <hr style="margin-top: 10px; border-top: 3px solid lightgray; opacity: 1; margin-left: 5px; margin-right: 5px;" />
                </div>
                <div class="card-body" style="padding-top: 0;">
                    <div id="chartEv"></div>
                    <script>
                        $(document).ready(function () {
                            var valorCookie = $("#ContentPlaceHolder1_hIdSecretaria").val();
                            $.ajax({
                                type: "POST",
                                url: "../Autoridades/Secretarias/DashboardSecretaria.aspx/EstadoEvaluador",
                                data: '{idSecretaria: "' + valorCookie + '" }',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (response) {
                                    chartEv.updateSeries(response.d);
                                },
                                error: function (error) {
                                    console.log(error);
                                }
                            });

                        });

                    </script>
                    <script>
                        var options = {
                            plotOptions: {
                                pie: {
                                    startAngle: 0,
                                    endAngle: 360,
                                    expandOnClick: true,
                                    offsetX: 0,
                                    offsetY: 0,
                                    customScale: 1,
                                    dataLabels: {
                                        offset: 0,
                                        minAngleToShowLabel: 10
                                    },
                                    donut: {
                                        size: '75%',
                                        background: 'transparent',
                                        labels: {
                                            show: true,
                                            name: {
                                                show: true,
                                                fontSize: '22px',
                                                fontFamily: 'Helvetica, Arial, sans-serif',
                                                fontWeight: 600,
                                                color: undefined,
                                                offsetY: -10,
                                                formatter: function (val) {
                                                    return val
                                                }
                                            },
                                            value: {
                                                show: true,
                                                fontSize: '16px',
                                                fontFamily: 'Helvetica, Arial, sans-serif',
                                                fontWeight: 400,
                                                color: undefined,
                                                offsetY: 16,
                                                formatter: function (val) {
                                                    return val
                                                }
                                            },
                                            total: {
                                                show: true,
                                                showAlways: false,
                                                label: 'Total',
                                                fontSize: '22px',
                                                fontFamily: 'Helvetica, Arial, sans-serif',
                                                fontWeight: 600,
                                                color: '#373d3f',
                                                formatter: function (w) {
                                                    return w.globals.seriesTotals.reduce((a, b) => {
                                                        return a + b
                                                    }, 0)
                                                }
                                            }
                                        }
                                    },
                                }
                            },
                            chart: {
                                height: 300,
                                type: 'donut',
                            },
                            labels: ['Sin notificar', 'Notificadas', 'Aceptadas', 'Rechazadas', 'Sin realizar'],
                            dataLabels: {
                                enabled: false,
                                textAnchor: 'middle',
                                style: {
                                    fontSize: '18px',
                                    fontFamily: 'Helvetica, Arial, sans-serif',
                                    fontWeight: '400'
                                },
                                background: {
                                    enabled: true,
                                    foreColor: '#ffffff',
                                    padding: 8,
                                    borderRadius: 2,
                                    borderWidth: 0,
                                    borderColor: '#ffffff',
                                    opacity: 1,
                                    dropShadow: {
                                        enabled: false,
                                    }
                                },
                                dropShadow: {
                                    enabled: false,
                                }
                            },
                            title: {
                                text: '',
                            },
                            noData: {
                                text: 'Loading...'
                            },
                            theme: {
                                mode: 'light',
                                palette: 'palette7',
                                monochrome: {
                                    enabled: false,
                                    color: '#255aee',
                                    shadeTo: 'light',
                                    shadeIntensity: 0.65
                                },
                            },
                            legend: {
                                show: true,
                                fontSize: '15px',
                                fontWeight: 400,
                                horizontalAlign: 'left',
                                offsetY: 0,
                                position: 'bottom'
                            },
                            responsive: [{
                                breakpoint: 480,
                                options: {
                                    chart: {
                                        width: 280
                                    },
                                    legend: {
                                        position: 'bottom'
                                    }
                                }
                            }]
                        }

                        var chartEv = new ApexCharts(
                            document.querySelector("#chartEv"),
                            options
                        );

                        chartEv.render();
                    </script>
                </div>
            </div>
        </div>
        <div class="col-xl-9 col-lg-9 col-md-9">
            <div class="tab-pane" id="tabInforme">
                <asp:HiddenField ID="hIdEvaluacion" runat="server" />
                <div class="panel-body">
                    <div class="container-fluid shadow p-3 mb-5 bg-white rounded"
                        style="background-color: white; padding-left: 25px !important; padding-top: 5px !important;">
                        <div class="row" style="margin-top: 10px;">
                            <div class="col-md-12 col-md-offset-0" style="z-index: 500; margin-bottom: 0px;">
                                <div class="box-body">
                                    <div class="row">
                                        <div class="box-header with-border">
                                            <div class="row">
                                                <div class="col-md-6" style="padding-top: 10px;">
                                                    <h3 style="font-size: 24px; color: var(--bs-gray-600);"
                                                        id="lblSecretariaConsultada" runat="server"></h3>
                                                </div>
                                                <div class="col-md-6" style="padding-top: 10px; text-align: right;">
                                                    <span id="lblFichaActiva" runat="server"
                                                        style="display: none; padding: 12px; font-size: 16px; font-weight: 600; margin-right: 20px;"
                                                        class="badge badge-pill badge-info">Evaluación de desempeño 2023</span>
                                                    <div class="dropdown">
                                                        <button class="btn btn-secondary dropdown-toggle" type="button" data-toggle="dropdown" aria-expanded="false">
                                                            Evaluación a revisar
                                                        </button>
                                                        <div class="dropdown-menu">
                                                            <a class="dropdown-item" href="#">Action</a>
                                                            <a class="dropdown-item" href="#">Another action</a>
                                                            <a class="dropdown-item" href="#">Something else here</a>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- ////////////////////////////// GRILLA DETALLE /////////////////////////////////////// -->
                            <div class="auto-style1">
                                <nav>
                                    <div class="nav nav-tabs" id="nav-tab" role="tablist">
                                        <button class="nav-link active" id="nav-home-tab" data-toggle="tab"
                                            data-target="#nav-home" type="button" role="tab"
                                            aria-controls="nav-home" aria-selected="true">
                                            Directores</button>
                                        <button class="nav-link" id="nav-profile-tab" data-toggle="tab"
                                            data-target="#nav-profile" type="button" role="tab"
                                            aria-controls="nav-profile" aria-selected="false">
                                            Personal a cargo directo</button>
                                    </div>
                                </nav>
                                <div class="tab-content" id="nav-tabContent">
                                    <div class="tab-pane fade show active" id="nav-home" role="tabpanel"
                                        aria-labelledby="nav-home-tab" style="padding: 20px; padding-top: 40px;">
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
                                                <asp:TemplateField HeaderText="Empleado">
                                                    <ItemTemplate>
                                                        <p style="font-size: 14px; margin-top: 0; margin-bottom: 5px;">
                                                            <strong><%#Eval("nombre")%></strong>
                                                        </p>
                                                        <p style="color: var(--bs-gray); font-size: 14px; margin-bottom: 5px;">
                                                            Legajo: <%#Eval("legajo")%> - Categoria: <%#Eval("cod_categoria")%>
                                                        </p>
                                                        <p style="color: var(--bs-gray); font-size: 14px; margin-bottom: 0px;">
                                                            <div style="padding-top: 2px; padding-bottom: 2px; font-size: 14px;"
                                                                id="lblEstadoEvaluacion" runat="server"
                                                                role="alert">
                                                            </div>
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
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <a class="btn btn-outline-primary" id="btnEnviarEvaluacion" runat="server"
                                                            href="#">
                                                            <span class="fa fa-graduation-cap"></span>
                                                            Evaluar
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="tab-pane fade" id="nav-profile" role="tabpanel"
                                        aria-labelledby="nav-profile-tab">
                                        <asp:GridView ID="gvDirectos"
                                            runat="server"
                                            AutoGenerateColumns="False"
                                            EmptyDataText="No hay resultados..."
                                            Width="100%"
                                            CssClass="table"
                                            CellPadding="4" ForeColor="Black"
                                            OnRowDataBound="gvDirectos_RowDataBound"
                                            DataKeyNames="legajo"
                                            GridLines="Horizontal"
                                            Font-Names="Ubuntu, sans-serif;"
                                            AlternatingRowStyle-CssClass="alt">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Empleado">
                                                    <ItemTemplate>
                                                        <p style="font-size: 14px; margin-top: 0; margin-bottom: 5px;">
                                                            <strong><%#Eval("nombre")%></strong>
                                                        </p>
                                                        <p style="color: var(--bs-gray); font-size: 14px; margin-bottom: 5px;">
                                                            Legajo: <%#Eval("legajo")%> - Categoria: <%#Eval("cod_categoria")%>
                                                        </p>
                                                        <p style="color: var(--bs-gray); font-size: 14px; margin-bottom: 0px;">
                                                            <div style="padding-top: 2px; padding-bottom: 2px; font-size: 14px;"
                                                                id="lblEstadoEvaluacion" runat="server"
                                                                role="alert">
                                                            </div>
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
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <a class="btn btn-outline-primary" id="btnEnviarEvaluacion" runat="server"
                                                            href="#">
                                                            <span class="fa fa-graduation-cap"></span>
                                                            Evaluar
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
