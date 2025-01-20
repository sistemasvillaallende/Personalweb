<%@ Page Language="C#" AutoEventWireup="true" 
    CodeBehind="~/Autoridades/DashboardSecretaria.aspx.cs" Inherits="web.Autoridades.DashboardSecretaria" 
    MasterPageFile="~/Autoridades/MPSec.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../App_Themes/NewTheme/dist/apexcharts.css?v=1" rel="stylesheet" />
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hIdSecretaria" runat="server" />
    <script src="https://cdn.jsdelivr.net/npm/apexcharts"></script>

    <div class="row g-2 clearfix row-deck" style="margin-top: 50px;">
        <div class="col-md-12" style="display: block">
            <h3 style="font-size: 24px; color: var(--bs-gray-600);"
                id="lblSecretaria" runat="server"></h3>
            <hr style="border-top: solid 1px; margin-top: 5px; margin-bottom: 25px;" />
        </div>
    </div>

    <div class="row g-2 clearfix row-deck">
        <div class="col-xl-3 col-lg-3 col-md-3" style="display: block">
            <div class="card top_counter">
                <div class="list-group list-group-custom list-group-flush">
                    <h3 style="padding-left: 20px; padding-top: 20px; font-size: 20px; color: var(--primary-color); font-weight: 600;">Incidencias del dia</h3>
                    <hr style="margin-bottom: 0; margin-top: 5px; border-top: 3px solid lightgray; opacity: 1; margin-left: 20px; margin-right: 20px;" />
                    <div class="list-group-item d-flex align-items-center py-3"
                        style="padding-top: 5px !important; padding-bottom: 5px !important;">
                        <div class="icon text-center me-3"><i class="fa fa-plane"></i></div>
                        <div class="content">
                            <div>Personal de Licencia</div>
                            <h5 id="lblLicencia" runat="server" class="mb-0"></h5>
                        </div>
                    </div>
                    <div class="list-group-item d-flex align-items-center py-3">
                        <div class="icon text-center me-3"><i class="fa fa-coffee"></i></div>
                        <div class="content">
                            <div>Razones Particulares</div>
                            <h5 id="lblRazones" runat="server" class="mb-0">425</h5>
                        </div>
                    </div>
                    <div class="list-group-item d-flex align-items-center py-3">
                        <div class="icon text-center me-3"><i class="fa fa-ambulance"></i></div>
                        <div class="content">
                            <div>Ausentes con aviso</div>
                            <h5 id="lblAusentesAviso" runat="server" class="mb-0"></h5>
                        </div>
                    </div>
                    <div class="list-group-item d-flex align-items-center py-3">
                        <div class="icon text-center me-3"><i class="fa fa-question-circle"></i></div>
                        <div class="content">
                            <div>Ausentes sin procesar</div>
                            <h5 id="lblAusentesSinAviso" runat="server" class="mb-0"></h5>
                        </div>
                    </div>
                    <div class="list-group-item d-flex align-items-center py-3">
                        <div class="icon text-center me-3"><i class="fa fa-birthday-cake"></i></div>
                        <div class="content">
                            <div>
                                Cumpleaños
                            </div>
                            <h5 id="lblCumpleaños" runat="server" class="mb-0"></h5>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xl-3 col-lg-3 col-md-3">
            <div class="card">
                <div class="card-header border-0" style="padding-bottom: 0;">
                    <h5 style="padding-left: 5px; font-size: 20px; color: var(--primary-color); font-weight: 600;">Estructura de personal
                    </h5>
                    <hr style="margin-top: 10px; border-top: 3px solid lightgray; opacity: 1; margin-left: 5px; margin-right: 5px;" />
                </div>
                <div class="card-body" style="padding-top: 0;">
                    <div id="chart"></div>
                    <script>
                        function obtenerCookie(nombre) {
                            var name = nombre + "=";
                            var decodedCookie = decodeURIComponent(document.cookie);
                            var cookies = decodedCookie.split(';');
                            for (var i = 0; i < cookies.length; i++) {
                                var cookie = cookies[i].trim();
                                if (cookie.indexOf(name) == 0) {
                                    return cookie.substring(name.length, cookie.length);
                                }
                            }
                            return "";
                        }
                    </script>
                    <script>
                        $(document).ready(function () {
                            var valorCookie = $("#ContentPlaceHolder1_hIdSecretaria").val();
                            $.ajax({
                                type: "POST",
                                url: "DashboardSecretaria.aspx/ActualizarEstructuraPersonal",
                                data: '{idSecretaria: "' + valorCookie + '" }',
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
                                height: 380,
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
                                        width: 360
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
                                url: "DashboardSecretaria.aspx/EstadoEvaluador",
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
                                height: 380,
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
                                        width: 320
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
                            var valorCookie = $("#ContentPlaceHolder1_hIdSecretaria").val();
                            $.ajax({
                                type: "POST",
                                url: "DashboardSecretaria.aspx/ResultadoEvaluacion",
                                data: '{idSecretaria: "' + valorCookie + '" }',
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
    </div>
    <div class="row g-2 clearfix row-deck" style="margin-top: 20px;">
        <div class="col-xl-12 col-lg-12 col-md-12" style="display: block">
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
                            var valorCookie = $("#ContentPlaceHolder1_hIdSecretaria").val();
                            $.ajax({
                                type: "POST",
                                data: '{idSecretaria: "' + valorCookie + '" }',
                                url: "DashboardSecretaria.aspx/sueldosPlanta",
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (response) {
                                    chartSueldosPlanta.updateSeries([{
                                        name: 'Sales',
                                        data: response.d
                                    }]);
                                    chartSueldosPlanta.addPointAnnotation({
                                        x: 'Media',
                                        y: response.d[1],
                                        label: {
                                            text: 'Media: $' + response.d[1]
                                        },
                                    });
                                    chartSueldosPlanta.addPointAnnotation({
                                        x: 'Mediana',
                                        y: response.d[2],
                                        label: {
                                            text: 'Mediana: $' + response.d[2]
                                        },
                                    });
                                    chartSueldosPlanta.addPointAnnotation({
                                        x: 'Moda',
                                        y: response.d[3],
                                        label: {
                                            text: 'Moda: $' + response.d[3]
                                        },
                                    });
                                    chartSueldosPlanta.addPointAnnotation({
                                        x: 'Sueldo minimo',
                                        y: response.d[0],
                                        label: {
                                            text: 'Sueldo minimo: $' + response.d[0]
                                        },
                                    });
                                    chartSueldosPlanta.addPointAnnotation({
                                        x: 'Sueldo maximo',
                                        y: response.d[4],
                                        label: {
                                            text: 'Sueldo maximo: $' + response.d[4]
                                        },
                                    });
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
                                height: 350,
                                type: 'line',
                                toolbar: { show: false },
                            },
                            labels: ['Sueldo minimo', 'Media', 'Mediana', 'Moda', 'Sueldo maximo'],
                            dataLabels: {
                                enabled: false,
                                show: false
                            },
                            series: [],
                            noData: {
                                text: 'Loading...'
                            },
                            legend: {
                                show: false,
                                fontSize: '15px',
                                fontWeight: 400,
                                horizontalAlign: 'left',
                                offsetY: 0,
                                position: 'bottom'
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
</asp:Content>
