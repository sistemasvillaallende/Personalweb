<%@ Page Title="" Language="C#" MasterPageFile="~/MP/MasterNew.Master" 
    AutoEventWireup="true" CodeBehind="EstadoEvaluacion.aspx.cs" Inherits="web.secure.EstadoEvaluacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .apexcharts-text apexcharts-pie-label {
            font-size: 48px !important;
        }
    </style>

    <link href="../App_Themes/NewTheme/dist/apexcharts.css?v=1" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">   
    <script src="../App_Themes/jQuery-2.1.4.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/apexcharts"></script>
    <div class="row">
        <div class="col-md-4">
            <div class="card mb-3">
                <div class="card-body">
                    <div id="chart"></div>
                    <script>
                        $(document).ready(function () {
                            $.ajax({
                                type: "POST",
                                url: "EstadoEvaluacion.aspx/ActualizarEstructuraPersonal",  
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
                                        size: '65%',
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
                                height: 250,
                                type: 'donut',
                            },
                            dataLabels: {
                                enabled: false
                            },
                            title: {
                                text: 'Estado Evaluador',
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
                                show: false
                            },
                            responsive: [{
                                breakpoint: 480,
                                options: {
                                    chart: {
                                        width: 200
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
        <div class="col-md-4">
        </div>
        <div class="col-md-4">
        </div>
    </div>

</asp:Content>
