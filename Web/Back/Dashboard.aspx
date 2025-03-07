<%@ Page Title="" Language="C#" MasterPageFile="~/MP/MasterNew.Master" AutoEventWireup="true"
    CodeBehind="Dashboard.aspx.cs" Inherits="web.secure.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../App_Themes/NewTheme/dist/apexcharts.css?v=1" rel="stylesheet" />
    <style>
        .apexcharts-legend {
            max-height:150px !important;
        }
        .table tr td {
            border-color: var(--border-color);
            background-color: transparent;
            padding-top: 5px;
            padding-bottom: 5px;
            font-size: 14px;
            color: var(--bs-gray-600);
        }

        .modal {
            top: 100 !important;
        }

        .apexcharts-canvas text {
            fill: gray !important;
            font-size: 15px;
        }

        #divEval .apexcharts-canvas text {
            fill: white !important;
            font-size: 13px;
        }
    </style>
    <style>
        .apexcharts-legend.apexcharts-align-right .apexcharts-legend-series, .apexcharts-legend.apexcharts-align-left .apexcharts-legend-series {
            /* display: block; */
            width: 100% !important;
        }

        .apexcharts-legend-text {
            position: relative;
            font-size: 14px !important;
            line-height: 1.5;
        }

        .apexcharts-text tspan {
            font-family: inherit;
            font-size: 13px !important;
        }
    </style>

    <link rel="stylesheet" href="https://cdn.datatables.net/1.13.7/css/jquery.dataTables.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/apexcharts"></script>
    <div class="row g-2 clearfix row-deck" style="padding:25px;">
        <div class="col-xl-3 col-lg-3 col-md-3" style="display: block">
            <div class="card top_counter">
                <div class="list-group list-group-custom list-group-flush">
                    <h5 style="padding-left: 20px; font-size: 18px; color: var(--primary-color); font-weight: 600; padding-top: 15px; padding-bottom: 0px; margin-bottom: 2px;">Incidencias del día
                    </h5>
                    <hr style="margin-bottom: 0; margin-top: 5px; border-top: 3px solid lightgray; opacity: 1; margin-left: 20px; margin-right: 20px;" />
                    <div class="list-group-item d-flex align-items-center py-3"
                        style="padding: 5px !important;">
                        <div class="icon text-center me-3"><i class="fa fa-plane"></i></div>
                        <div class="content">
                            <button type="button" style="background-color: transparent; border: none; display: block; text-align: left"
                                data-toggle="modal" data-target="#ModalLic">
                                <div style="font-size: 16px;">Personal de Licencia</div>
                                <h5 style="font-size: 16px;" id="lblLicencia" runat="server" class="mb-0"></h5>
                            </button>

                        </div>
                    </div>
                    <div class="list-group-item d-flex align-items-center py-3"
                        style="padding: 5px !important;">
                        <div class="icon text-center me-3"><i class="fa fa-coffee"></i></div>
                        <div class="content">
                            <button type="button" style="background-color: transparent; border: none; display: block; text-align: left"
                                data-toggle="modal" data-target="#ModalRazones">
                                <div style="font-size: 16px;">Razones Particulares</div>
                                <h5 style="font-size: 16px;" id="lblRazones" runat="server" class="mb-0"></h5>
                            </button>
                        </div>
                    </div>
                    <div class="list-group-item d-flex align-items-center py-3"
                        style="padding: 5px !important;">
                        <div class="icon text-center me-3"><i class="fa fa-ambulance"></i></div>
                        <div class="content">
                            <button type="button" style="background-color: transparent; border: none; display: block; text-align: left"
                                data-toggle="modal" data-target="#ModalCon">
                                <div style="font-size: 16px;">Ausentes con aviso</div>
                                <h5 style="font-size: 16px;" id="lblAusentesAviso" runat="server" class="mb-0"></h5>
                            </button>

                        </div>
                    </div>
                    <div class="list-group-item d-flex align-items-center py-3"
                        style="padding: 5px !important;">
                        <div class="icon text-center me-3"><i class="fa fa-question-circle"></i></div>
                        <div class="content">
                            <button type="button" style="background-color: transparent; border: none; display: block; text-align: left"
                                data-toggle="modal" data-target="#ModalSin">
                                <div style="font-size: 16px;">Ausentes sin procesar</div>
                                <h5 style="font-size: 16px;" id="lblAusentesSinAviso" runat="server" class="mb-0"></h5>
                            </button>
                        </div>
                    </div>
                    <div class="list-group-item d-flex align-items-center py-3"
                        style="padding: 5px !important;">
                        <div class="icon text-center me-3"><i class="fa fa-birthday-cake"></i></div>
                        <div class="content">
                            <button type="button" style="background-color: transparent; border: none; display: block; text-align: left"
                                data-toggle="modal" data-target="#ModalCumple">
                                <div style="font-size: 16px;">Cumpleaños</div>
                                <hr style="border: solid 2px lightgrey"/>
                                <h5 style="font-size: 16px;" id="lblCumpleaños" runat="server" class="mb-0"></h5>
                            </button>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-xl-3 col-lg-3 col-md-3">
            <div class="card">
                <div class="card-header border-0" style="padding-bottom: 0;">
                    <h5 style="padding-left: 5px; font-size: 20px; color: var(--primary-color); font-weight: 600;">Dotación de personal
                    </h5>
                    <hr style="margin-top: 10px; border-top: 3px solid lightgray; opacity: 1; margin-left: 5px; margin-right: 5px;" />
                </div>
                <div class="card-body" style="padding-top: 0;">
                    <div id="chart"></div>
                    <script>
                        $(document).ready(function () {
                            $.ajax({
                                type: "POST",
                                url: "Dashboard.aspx/ActualizarEstructuraPersonal",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (response) {
                                    chart.updateSeries(response.d);
                                },
                                error: function (error) {
                                    console.log(error);
                                }
                            });

                        });

                    </script>
                    <script>
                        var options = {
                            chart: {
                                width: 260,
                                type: 'donut',
                            },
                            colors: ['#2eacb3', '#FED049', '#2D6187', '#89BEB3', '#FF9A76', '#D7263D', '#1B998B', '#2E294E', '#F46036', '#E2C044'],
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
                                        size: '60%',
                                        background: 'transparent',
                                        labels: {
                                            show: true,
                                            name: {
                                                show: false,
                                                fontSize: '22px',
                                                fontFamily: 'NORMAL, Arial, sans-serif',
                                                fontWeight: 600,
                                                color: undefined,
                                                offsetY: -10,
                                                formatter: function (val) {
                                                    return val
                                                }
                                            },
                                            value: {
                                                show: true,
                                                fontSize: '12px',
                                                fontFamily: 'NORMAL, Arial, sans-serif',
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
                                                fontSize: '18px',
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
                                height: 260,
                                type: 'donut',
                            },
                            labels: ['Monotributo', 'Personal Contratado', 'Personal Permanente', 'Personal Planta Politica', 'Personal Transitorio'],
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
                            fill: {
                                type: 'gradient',
                            },
                            legend: {
                                show: true,
                                fontSize: '15px',
                                fontWeight: 400,
                                horizontalAlign: 'left',
                                offsetY: 0,
                                position: 'bottom'
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
                            responsive: [{
                                breakpoint: 480,
                                options: {
                                    chart: {
                                        width: 260
                                    },
                                    legend: {
                                        position: 'bottom'
                                    }
                                }
                            }]
                        }

                        var chart = new ApexCharts(
                            document.querySelector("#chart"),
                            options
                        );

                        chart.render();
                    </script>
                </div>
            </div>
        </div>
        <!--
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
                            $.ajax({
                                type: "POST",
                                url: "Dashboard.aspx/EstadoEvaluador",
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
                            colors: ['#2eacb3', '#FED049', '#2D6187', '#89BEB3', '#FF9A76', '#D7263D', '#1B998B', '#2E294E', '#F46036', '#E2C044'],
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
                                                offsetY: 16
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
                                height: 240,
                                type: 'donut',
                            },
                            //labels: ['Sin notificar', 'Notificadas', 'Aceptadas', 'Rechazadas', 'Sin realizar'],
                            //labels: ['Realizadas', 'Sin Realizar'],
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
                                },
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
                                show: false,
                                fontSize: '15px',
                                fontWeight: 400,
                                horizontalAlign: 'left',
                                offsetY: 0,
                                position: 'bottom'
                            }
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
        <div class="col-xl-3 col-lg-3 col-md-3">
            <div class="card">
                <div class="card-header border-0" style="padding-bottom: 0;">
                    <h5 style="padding-left: 5px; font-size: 20px; color: var(--primary-color); font-weight: 600;">Resultado Evaluacion
                    </h5>
                    <hr style="margin-top: 10px; border-top: 3px solid lightgray; opacity: 1; margin-left: 5px; margin-right: 5px;" />
                </div>
                <div class="card-body" style="padding: 0;" id="divEval">
                    <div id="chartRv"></div>
                    <script>
                        $(document).ready(function () {
                            $.ajax({
                                type: "POST",
                                url: "Dashboard.aspx/ResultadoEvaluacion",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (response) {
                                    var options = {

                                        series: [{
                                            data: response.d.lstValores
                                        }],
                                        chart: {
                                            toolbar: {
                                                show: false
                                            },
                                            type: 'bar',
                                            height: 340
                                        },
                                        plotOptions: {
                                            bar: {
                                                barHeight: '65%',
                                                distributed: true,
                                                horizontal: true,
                                                dataLabels: {
                                                    position: 'bottom'
                                                },
                                            }
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
                                        dataLabels: {
                                            enabled: true,
                                            textAnchor: 'start',
                                            style: {
                                                fontSize: '14px',
                                                fontFamily: 'Helvetica, Arial, sans-serif',
                                                fontWeight: '400',
                                                foreColor: '#ffffff'
                                            },
                                            formatter: function (val, opt) {
                                                return opt.w.globals.labels[opt.dataPointIndex] + ":  " + val + "%"
                                            },
                                            offsetX: 0,
                                            background: {
                                                enabled: false,
                                                foreColor: '#ffffff',
                                                padding: 2,
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
                                        stroke: {
                                            width: 1,
                                            colors: ['#eee']
                                        },
                                        xaxis: {
                                            categories: response.d.lstSeries,
                                            labels: {
                                                show: false
                                            }
                                        },
                                        yaxis: {
                                            labels: {
                                                show: false
                                            }
                                        },
                                        tooltip: {
                                            theme: 'dark',
                                            x: {
                                                show: false
                                            },
                                            y: {
                                                title: {
                                                    formatter: function () {
                                                        return ''
                                                    }
                                                }
                                            }
                                        }
                                    };

                                    var chartRv = new ApexCharts(document.querySelector("#chartRv"), options);
                                    chartRv.render();
                                },
                                error: function (error) {
                                    console.log(error);
                                }
                            });

                        });
                    </script>
                </div>
            </div>
        </div>
        -->
        <div class="col-xl-6 col-lg-6 col-md-6" style="display: block">
            <div class="card">
                <div class="card-header border-0" style="padding-bottom: 0;">
                    <h5 style="padding-left: 5px; font-size: 20px; color: var(--primary-color); font-weight: 600;">Distribución salarial, contratos y planta
                    </h5>
                    <hr style="margin-top: 10px; border-top: 3px solid lightgray; opacity: 1; margin-left: 5px; margin-right: 5px;" />
                </div>
                <div class="card-body">
                    <div id="chartSueldosPlanta"></div>
                    <script>
                        $(document).ready(function () {
                            $.ajax({
                                type: "POST",
                                url: "Dashboard.aspx/sueldosPlanta",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (response) {
                                    chartSueldosPlanta.updateSeries([{
                                        name: response.d[0][0],
                                        data: response.d[0][1]
                                    },
                                    {
                                        name: response.d[1][0],
                                        data: response.d[1][1]
                                    },
                                    {
                                        name: response.d[2][0],
                                        data: response.d[2][1]
                                    }])

                                    const formatoMoneda = new Intl.NumberFormat('es-AR', { style: 'currency', currency: 'ARS' });

                                },
                                error: function (error) {
                                    console.log(error);
                                }
                            });
                            $.ajax({
                                type: "POST",
                                url: "Dashboard.aspx/sueldosPlanta",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (response) {
                                    chartSueldosPlanta.updateSeries([{
                                        name: response.d[0][0],
                                        data: response.d[0][1]
                                    },
                                    {
                                        name: response.d[1][0],
                                        data: response.d[1][1]
                                    },
                                    {
                                        name: response.d[2][0],
                                        data: response.d[2][1]
                                    }])

                                    const formatoMoneda = new Intl.NumberFormat('es-AR', { style: 'currency', currency: 'ARS' });

                                },
                                error: function (error) {
                                    console.log(error);
                                }
                            });
                        });
                    </script>
                    <script>
                        var options = {
                            series: [],
                            chart: {
                                height: 350,
                                type: 'line',
                                toolbar: { show: false },
                            },
                            labels: ['Sueldo minimo', 'Media', 'Mediana', 'Moda', 'Sueldo maximo'],
                            theme: {
                                mode: 'light',
                                palette: 'palette7',
                                monochrome: {
                                    enabled: false,
                                    color: '#fff',
                                    shadeTo: 'dark',
                                    shadeIntensity: 1
                                },
                            },
                            dataLabels: {
                                enabled: false,
                                show: true,
                            },
                            noData: {
                                text: 'Loading...'
                            },
                            legend: {
                                show: true,
                                fontSize: '15px',
                                fontWeight: 400,
                                horizontalAlign: 'center',
                                offsetY: 0,

                            },

                        }

                        var chartSueldosPlanta = new ApexCharts(
                            document.querySelector("#chartSueldosPlanta"),
                            options
                        );

                        chartSueldosPlanta.render();

                    </script>
                </div>
            </div>
        </div>
    </div>
    <div class="row g-2 clearfix row-deck" style="margin-top: 20px;">
    </div>

    <!-- Button trigger modal -->


    <!-- Modal -->
    <div class="modal fade" id="ModalLic" tabindex="-1" aria-labelledby="ModalLicLabel" aria-hidden="true">
        <div class="modal-dialog" style="max-width: 80%;">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalLicLabel">Personal de Licencia</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:GridView ID="gvLicencias" CssClass="table" runat="server" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="LEG_LEGAJO" HeaderText="Legajo" />
                            <asp:BoundField DataField="LEG_APYNOM" HeaderText="Nombre" />
                            <asp:BoundField DataField="RES_FECHA" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />

                            <asp:BoundField DataField="CON_DESCRIP" HeaderText="Descripción" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Salir</button>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal -->
    <div class="modal fade" id="ModalRazones" tabindex="-1" aria-labelledby="ModalRazonesLabel" aria-hidden="true">
        <div class="modal-dialog" style="max-width: 80%;">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalRazonesLabel">Ausentes por razones particulares</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:GridView ID="gvRazones" CssClass="table" runat="server" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="LEG_LEGAJO" HeaderText="Legajo" />
                            <asp:BoundField DataField="LEG_APYNOM" HeaderText="Nombre" />
                            <asp:BoundField DataField="RES_FECHA" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="CON_DESCRIP" HeaderText="Descripción" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Salir</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="ModalCon" tabindex="-1" aria-labelledby="ModalConLabel" aria-hidden="true">
        <div class="modal-dialog" style="max-width: 80%;">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalConLabel">Ausentes con aviso</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:GridView ID="gvCon" CssClass="table" runat="server" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="LEG_LEGAJO" HeaderText="Legajo" />
                            <asp:BoundField DataField="LEG_APYNOM" HeaderText="Nombre" />
                            <asp:BoundField DataField="RES_FECHA" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="CON_DESCRIP" HeaderText="Descripción" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Salir</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="ModalSin" tabindex="-1" aria-labelledby="ModalSinlLabel" aria-hidden="true">
        <div class="modal-dialog" style="max-width: 80%;">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalSinLabel">Ausentes sin procesar</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:GridView ID="gvSin" CssClass="table" runat="server" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="LEG_LEGAJO" HeaderText="Legajo" />
                            <asp:BoundField DataField="LEG_APYNOM" HeaderText="Nombre" />
                            <asp:BoundField DataField="RES_FECHA" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="CON_DESCRIP" HeaderText="Descripción" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Salir</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="ModalCumple" tabindex="-1" aria-labelledby="ModalCumpleLabel"
        aria-hidden="true">
        <div class="modal-dialog" style="max-width: 80%;">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalCumpleLabel">Ausentes con aviso</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:GridView ID="gvCumple" CssClass="table" runat="server" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="legajo" HeaderText="Legajo" />
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="OFICINA" HeaderText="Oficina" />
                            <asp:BoundField DataField="PROGRAMA" HeaderText="Programa" />
                            <asp:BoundField DataField="DIRECCION" HeaderText="Dirección" />
                            <asp:BoundField DataField="SECRETARIA" HeaderText="Secretaría" />
                            <asp:BoundField DataField="telefonos" HeaderText="Telefonos" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Salir</button>
                </div>
            </div>
        </div>
    </div>

    <script src="https://cdn.datatables.net/1.13.7/js/jquery.dataTables.js"></script>
    <script>

        $(document).ready(function () {
            $('#<%=gvLicencias.ClientID %>').dataTable({
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

            $('#<%=gvCon.ClientID %>').dataTable({
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

            $('#<%=
        gvSin.ClientID %>').dataTable({
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
</asp:Content>
