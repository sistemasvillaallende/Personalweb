<%@ Page Title="" Language="C#" MasterPageFile="~/MP/MasterNew.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="web.GraficoEvaluaciones.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Media, Mediana y Moda - Chart.js</title>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-annotation"></script>

    <style>
        .h3 {
            font-size: 18px !important;
            font-weight: 700 !important;
            margin-bottom: 2px;
        }

        .h5 {
            font-size: 14px !important;
            font-weight: 300 !important;
            border-bottom: solid 2px lightgray;
            padding-bottom: 10px;
        }

        .competencia {
            padding-top: 25px;
            border: navajowhite;
            margin-bottom: 25px;
            margin-top: 15px;
            -webkit-box-shadow: 1px 2px 14px -2px rgba(165, 165, 171, 1);
            -moz-box-shadow: 1px 2px 14px -2px rgba(165, 165, 171, 1);
            box-shadow: 1px 2px 14px -2px rgba(165, 165, 171, 1);
            border-radius: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="utiles.js?v=5"></script>
    <div class="container">
        <div class="row competencia">
            <div class=" border-0" style="padding-bottom: 0;">
                <div class="row">
                    <div class="col-8">
                         <h3 style="font-size: 20px !important; font-weight: 500 !important;">Resultado Evaluaciones de desempeño</h3>
                    </div>
                    <div class="col-4">
                        <asp:DropDownList ID="DDLEvaluaciones"
                            CssClass="form-control"                         
                            runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row" style="margin-top: 25px;">
                    <div class="col-3">
                        <div class="form-group">
                            <label>Secretaría</label>
                            <asp:DropDownList ID="DDLSecretarias"
                                CssClass="form-control"
                                runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-3">
                        <div class="form-group">
                            <label>Dirección</label>
                            <asp:DropDownList ID="DDLDirecciones"
                                CssClass="form-control"
                                runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-3">
                        <div class="form-group">
                            <label>Oficina</label>
                            <asp:DropDownList ID="DDLOficinas"
                                CssClass="form-control"
                                runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-3">
                        <div class="form-group">
                            <label>Programa</label>
                            <asp:DropDownList ID="DDLProgramas"
                                CssClass="form-control"
                                runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row competencia" style="padding-top: 45px; margin-top: 25px;">
            <div class="col-md-6">
                <canvas id="graficoResultados"></canvas>
            </div>
            <div class="col-md-6" style="width: 350px !important; height: 380px !important; justify-items: center;">
                <div id="resultadoSerie"></div>
            </div>
        </div>
        <div class="row competencia" style="display: none;">
            <div class="col-md-6" style="margin-top: -60px; justify-items: center;">
                <canvas id="chartTorta" width="650" height="400"></canvas>
                <div id="resultadoTorta"></div>
            </div>
            <div class="col-md-6">
            </div>
        </div>
    </div>



    <script>

        let chartInstance = null; // Variable global para almacenar la instancia del gráfico
        // Función para calcular la media
        function calcularMedia2(valores, frecuencias) {
            let sumaProductos = 0;
            let totalFrecuencia = 0;

            for (let i = 0; i < valores.length; i++) {
                sumaProductos += valores[i] * frecuencias[i];
                totalFrecuencia += frecuencias[i];
            }

            return sumaProductos / totalFrecuencia;
        }

        function generarGraficos(datos) {
            const indices = Array.from({ length: datos.length }, (_, i) => i + 1);
            // Calcular valores
            const media = calcularMedia(datos);
            const mediana = calcularMediana(datos);
            const moda = calcularModa(datos);

            const varianza = calcularVarianza(datos);

            let divCmd6_2 = document.getElementById("resultadoSerie");
            divCmd6_2.replaceChildren();
            divCmd6_2.innerHTML = "";

            // Agregar el porcentaje al título
            let h3 = document.createElement("h3");
            h3.innerHTML = "Promedio General: <span style=\"float: right\">" + parseFloat(media).toFixed(2) + "%</span>"; // Se muestra el porcentaje al lado del título
            h3.className = "h3";
            let h5 = document.createElement("h5");
            h5.innerHTML = "Sobre un total de: " + datos.length + " evaluaciones"; // Se muestra el porcentaje al lado del título
            h5.className = "h5";

            divCmd6_2.appendChild(h3);
            divCmd6_2.appendChild(h5);
            let spanMedia = document.createElement("span");
            spanMedia.innerHTML = media;
            spanMedia.style.color = "black";
            let p = document.createElement("p");
            p.id = "lblMedia";
            p.style.color = "blue";
            p.style.fontSize = 18;
            p.style.fontWeight = 500;
            p.style.marginBottom = 0;
            p.innerHTML =
                "Media:  <span style=\"color:black; margin-right:20px;\">" + parseFloat(media).toFixed(2) +
                "</span>" +
                "<span style=\"color: green; font-weight:500; margin-right 10px;\">" +
                "Mediana:  <span style=\"color:black;\ margin-right:20px;\">" + parseFloat(mediana).toFixed(2) + "</span>" +
                "<span style=\"color: red; font-weight:500; margin-left 10px;\">" +
                "Moda:  <span style=\"color:black;  margin-right:20px;\">" + moda.toFixed(2) + "</span>";
            divCmd6_2.appendChild(p);
            let p2 = document.createElement("p");
            p2.innerHTML = analizarDistribucion(media, mediana);
            divCmd6_2.appendChild(p2);
            // Configurar el gráfico con Chart.js
            let p3 = document.createElement("p");
            p3.innerHTML =
                "<b>Varianza:</b> <span style=\"color:black; margin-right:20px;\">" + calcularVarianza(datos).toFixed(2) +
                "</span>" +
                "<b>Desviacion Estandard:</b> <span style=\"color:black; margin-right:20px;\">" + calcularDesviacionEstandar(datos).toFixed(2) +
                "</span>"
            divCmd6_2.appendChild(p3);


            var de = calcularDesviacionEstandar(datos).toFixed(2);
            ymin = Number(media) - Number(de);
            ymax = Number(media) + Number(de);


            const ctx = document.getElementById('graficoResultados').getContext('2d');

            // Si ya existe un gráfico, destrúyelo antes de crear uno nuevo
            if (chartInstance) {
                chartInstance.destroy();
            }

            chartInstance = new Chart(ctx, {
                type: 'line',
                data: {
                    labels: indices, // Números correlativos (1, 2, 3, ..., 424)
                    datasets: [{
                        label: 'Resultados',
                        data: datos,
                        borderColor: 'blue',
                        borderWidth: 1,
                        pointStyle: 'false',
                        pointRadius: 0,
                        fill: false
                    }]
                },
                options: {
                    responsive: true,
                    plugins: {
                        annotation: {
                            annotations: {
                                media: {
                                    type: 'line',
                                    yMin: media,
                                    yMax: media,
                                    borderColor: 'blue',
                                    borderWidth: 1,
                                    label: {
                                        content: `Media (${media.toFixed(2)})`,
                                        enabled: true,
                                        position: 'top'
                                    }
                                },
                                mediana: {
                                    type: 'line',
                                    yMin: mediana,
                                    yMax: mediana,
                                    borderColor: 'green',
                                    borderWidth: 1,
                                    label: {
                                        content: `Mediana (${mediana.toFixed(2)})`,
                                        enabled: true,
                                        position: 'top'
                                    }
                                },
                                moda: {
                                    type: 'line',
                                    yMin: moda,
                                    yMax: moda,
                                    borderColor: 'red',
                                    borderWidth: 0,
                                    label: {
                                        content: `Moda (${moda.toFixed(2)})`,
                                        enabled: true,
                                        position: 'top'
                                    }
                                },
                                shadedRegion: { // Área sombreada entre Y = 60 y Y = 80
                                    type: "box",
                                    yMin: ymin,
                                    yMax: ymax,
                                    backgroundColor: "rgba(0, 255, 0, 0.2)", // Color verde transparente
                                    borderWidth: 0
                                }
                            }
                        }
                    },
                    scales: {
                        x: {
                            title: {
                                display: true,
                                text: 'Participantes'
                            }
                        },
                        y: {
                            title: {
                                display: true,
                                text: 'Porcentaje de Evaluación'
                            }
                        }
                    }
                }
            });
        }
        function generarGraficoTorta(datosParticipantes) {
            let ctx = document.getElementById("chartTorta").getContext("2d");

            let bajo60 = 0, medio60_80 = 0, alto80 = 0;

            datosParticipantes.forEach(puntaje => {
                let porcentaje = puntaje;

                if (porcentaje < 60) {
                    bajo60++;
                } else if (porcentaje <= 80) {
                    medio60_80++;
                } else {
                    alto80++;
                }
            });

            new Chart(ctx, {
                type: 'pie',
                data: {
                    labels: ["Calif. Menores al 60%",
                        "Calif. Entre 60% y 80%",
                        "Calif. Mayores al 80%"],
                    datasets: [{
                        data: [bajo60, medio60_80, alto80],
                        backgroundColor: ["#e11d48", "#059669", "#f6a823"],

                    }]
                },
                options: {
                    responsive: true, // Mantiene la adaptabilidad
                    maintainAspectRatio: false, // Evita que el gráfico se escale con el canvas
                    layout: {
                        padding: {
                            right: 140 // Agrega más espacio para la leyenda
                        }
                    },
                    plugins: {
                        legend: {
                            labels: {
                                generateLabels: function (chart) {
                                    return chart.data.labels.map((label, index) => ({
                                        text: label.length > 50 ? label.substring(0, 50) + '...' : label, // Limita a 10 caracteres y agrega "..."
                                        fillStyle: chart.data.datasets[0].backgroundColor[index], // Color de la caja de la leyenda
                                        strokeStyle: chart.data.datasets[0].borderColor ? chart.data.datasets[0].borderColor[index] : 'transparent',
                                        hidden: chart.getDatasetMeta(0).data[index].hidden,
                                        index: index
                                    }));
                                },
                                font: {
                                    size: 14 // Tamaño de la fuente
                                },
                                color: 'black' // Color del texto de la leyenda
                            },
                            position: "right"
                        }
                    }
                }
            });
        }

        // **Datos de prueba (cada número representa el puntaje final de un participante en la escala 1-4)**
        let datosParticipantesEjemplo = [2.5, 3.2, 3.8, 2.1, 1.9, 3.5, 2.8, 4.0, 3.1, 2.0, 3.7];

        // Calcular Varianza y Desviación Estándar
        function calcularVarianza(datos) {
            let n = datos.length;
            let media = datos.reduce((acc, val) => acc + val, 0) / n;
            return datos.reduce((acc, val) => acc + Math.pow(val - media, 2), 0) / n;
        }

        function calcularDesviacionEstandar(datos) {
            return Math.sqrt(calcularVarianza(datos));
        }


            $(document).ready(function () {
                // Función para obtener el ID de ficha actual
                function obtenerIdFicha() {
                    let result = parseInt($('#<%= DDLEvaluaciones.ClientID %>').val());
                    return result;
             }

                
                $("#ContentPlaceHolder1_DDLEvaluaciones").change(function () {
                    // Obtener el nuevo valor cada vez que cambia
                    let idf = obtenerIdFicha();

                    console.log("idf cambiado a:", idf);

                    if (idf) {
                        // Limpiar todos los dropdowns
                        limpiarDropdown("#ContentPlaceHolder1_DDLSecretarias", "Seleccione Secretaría");
                        limpiarDropdown("#ContentPlaceHolder1_DDLDirecciones", "Seleccione Dirección");
                        limpiarDropdown("#ContentPlaceHolder1_DDLOficinas", "Seleccione Oficina");

                        // Cargar datos iniciales
                        cargarDatos();
                        cargarSecretarias(idf);
                    } else {
                        // Si no hay evaluación seleccionada, limpiar gráficos
                        limpiarGraficos();
                    }
                });

                // Event handlers para los dropdowns
                $("#ContentPlaceHolder1_DDLSecretarias").change(function () {
                    let secretaria = $(this).val();
                    let idf = obtenerIdFicha(); // Obtener ID actual

                    // Limpiar dropdowns dependientes
                    limpiarDropdown("#ContentPlaceHolder1_DDLDirecciones", "Seleccione Dirección");
                    limpiarDropdown("#ContentPlaceHolder1_DDLOficinas", "Seleccione Oficina");

                    if (secretaria) {
                        cargarDirecciones(idf, secretaria);
                    }

                    cargarDatos(); // Actualizar gráficos
                });

                $("#ContentPlaceHolder1_DDLDirecciones").change(function () {
                    let direccion = $(this).val();
                    let secretaria = $("#ContentPlaceHolder1_DDLSecretarias").val();
                    let idf = obtenerIdFicha(); // Obtener ID actual

                    // Limpiar dropdown de oficinas
                    limpiarDropdown("#ContentPlaceHolder1_DDLOficinas", "Seleccione Oficina");

                    if (direccion && secretaria) {
                        cargarOficinas(idf, secretaria, direccion);
                    }

                    cargarDatos(); // Actualizar gráficos
                });

                $("#ContentPlaceHolder1_DDLOficinas").change(function () {
                    cargarDatos(); // Actualizar gráficos
                });

                function cargarSecretarias(idFicha) {
                    $.ajax({
                        type: "POST",
                        url: "WebForm1.aspx/ObtenerSecretarias",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify({ idFicha: idFicha }),
                        success: function (response) {
                            let opciones = JSON.parse(response.d);
                            let select = $("#ContentPlaceHolder1_DDLSecretarias");
                            select.empty();
                            select.append('<option value="">Todas las Secretarias</option>');
                            opciones.forEach(opcion => {
                                select.append(`<option value="${opcion}">${opcion}</option>`);
                            });
                        },
                        error: function (xhr, status, error) {
                            console.error("Error al obtener secretarías:", error);
                        }
                    });
                }

                function cargarDirecciones(idFicha, secretaria) {
                    $.ajax({
                        type: "POST",
                        url: "WebForm1.aspx/ObtenerDirecciones",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify({ idFicha: idFicha, secretaria: secretaria }),
                        success: function (response) {
                            let opciones = JSON.parse(response.d);
                            let select = $("#ContentPlaceHolder1_DDLDirecciones");
                            select.empty();
                            select.append('<option value="">Todas las Direcciones</option>');
                            opciones.forEach(opcion => {
                                select.append(`<option value="${opcion}">${opcion}</option>`);
                            });
                            select.prop('disabled', false);
                        },
                        error: function (xhr, status, error) {
                            console.error("Error al obtener direcciones:", error);
                        }
                    });
                }

                function cargarOficinas(idFicha, secretaria, direccion) {
                    $.ajax({
                        type: "POST",
                        url: "WebForm1.aspx/ObtenerOficinas",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify({
                            idFicha: idFicha,
                            secretaria: secretaria,
                            direccion: direccion
                        }),
                        success: function (response) {
                            let opciones = JSON.parse(response.d);
                            let select = $("#ContentPlaceHolder1_DDLOficinas");
                            select.empty();
                            select.append('<option value="">Todas las Oficinas</option>');
                            opciones.forEach(opcion => {
                                select.append(`<option value="${opcion}">${opcion}</option>`);
                            });
                            select.prop('disabled', false);
                        },
                        error: function (xhr, status, error) {
                            console.error("Error al obtener oficinas:", error);
                        }
                    });
                }

                function limpiarDropdown(selector, textoDefault) {
                    let select = $(selector);
                    select.empty();
                    select.append(`<option value="">${textoDefault}</option>`);
                    if (selector !== "#ContentPlaceHolder1_DDLSecretarias") {
                        select.prop('disabled', true);
                    }
                }

                function cargarDatos() {
                    let idf = obtenerIdFicha();

                    let secretaria = $("#ContentPlaceHolder1_DDLSecretarias").val() || null;
                    let direccion = $("#ContentPlaceHolder1_DDLDirecciones").val() || null;
                    let oficina = $("#ContentPlaceHolder1_DDLOficinas").val() || null;

                    console.log("Cargando datos con idf:", idf);

                    $.ajax({
                        type: "POST",
                        url: "WebForm1.aspx/ObtenerDatosFiltrados",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify({
                            idFicha: idf,
                            secretaria: secretaria,
                            direccion: direccion,
                            oficina: oficina
                        }),
                        success: function (response) {
                            let datos = JSON.parse(response.d);
                            console.log("Datos filtrados para idf", idf, ":", datos);
                            generarGraficos(datos);
                        },
                        error: function (error) {
                            console.log("Error al obtener datos", error);
                        }
                    });
                }

                // Inicializar al cargar la página
                let idfInicial = obtenerIdFicha();
                if (idfInicial) {
                    cargarSecretarias(idfInicial);
                    cargarDatos();
                }
            });
    </script>
</asp:Content>
