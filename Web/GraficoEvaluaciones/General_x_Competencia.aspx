<%@ Page Title="" Language="C#" MasterPageFile="~/MP/MasterNew.Master" AutoEventWireup="true" CodeBehind="General_x_Competencia.aspx.cs" Inherits="web.GraficoEvaluaciones.General_x_Competencia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Resultado General por Competencia</title>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-annotation"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
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
    <script src="utiles.js?v=1"></script>
    <asp:HiddenField ID="hIdFicha" runat="server" />
    <asp:HiddenField ID="hNombreSecretaria" runat="server" />
    <div class="container">
        <div class="row">
            <div class="col-md-6" style="align-content: center;">
                <h3 style="font-size: 20px !important; font-weight: 500 !important;">Resultado Evaluación de desempeño 2024</h3>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <asp:DropDownList ID="DDLSecretarias"
                        CssClass="form-control" runat="server">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row competencia" style="margin-bottom: 20px;">
            <div class="col-md-6">
                <canvas id="graficoGeneral"></canvas>
            </div>
            <div class="col-md-6" style="max-height: 400px !important;" id="resultadoSerie">
            </div>
        </div>
    </div>

    <div class="container">
        <div style="margin-bottom: 20px;" id="graficos-container">
        </div>
    </div>


    <script>
        $(document).ready(function () {
            // Obtener los parámetros de la URL actual
            let params = new URLSearchParams(window.location.search);
            let idf = params.get("idFicha"); // "Juan"
            $.ajax({
                type: "POST",
                url: "General_x_Competencia.aspx/ObtenerDatos", // <-- SIN idFicha en la URL
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ idFicha: idf }), // <-- Se envía correctamente en el body
                success: function (response) {
                    let datos = JSON.parse(response.d);
                    generarGraficos(datos);
                    createGraficoGeneral(datos)
                },
                error: function (error) {
                    console.log("Error al obtener datos", error);
                }
            });
            $.ajax({
                type: "POST",
                url: "WebForm1.aspx/ObtenerDatos", // <-- SIN idFicha en la URL
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ idFicha: idf }), // <-- Se envía correctamente en el body
                success: function (response) {
                    let datos = JSON.parse(response.d);
                    createInformeGeneral(datos);
                },
                error: function (error) {
                    console.log("Error al obtener datos", error);
                }
            });
        });
        function generarGraficos(datos) {
            let container = document.getElementById("graficos-container");

            datos.forEach((item, index) => {
                let divR = document.createElement("div");
                divR.classList = "row competencia";

                let divCmd6_1 = document.createElement("div");
                divCmd6_1.className = "col-md-6";
                let divCmd6_2 = document.createElement("div");
                divCmd6_2.className = "col-md-6";

                let canvas = document.createElement("canvas");
                canvas.id = "chart" + index;
                canvas.style.marginBottom = 20;
                canvas.style.height = 220;
                //container.appendChild(canvas);
                divCmd6_1.appendChild(canvas);
                divR.appendChild(divCmd6_1);
                let ctx = canvas.getContext("2d");

                // Calcular la Media, Mediana y Moda
                let valores = item.Respuestas;
                let media = calcularMedia2([1, 2, 3, 4], valores).toFixed(2);
                let mediana = calcularMediana2([1, 2, 3, 4], valores).toFixed(2);
                let moda = calcularModa2([1, 2, 3, 4], valores).toFixed(2); // Devuelve {nombre: "Alcanza Plenamente", valor: 220}
                let etiquetas = ["No Cubre", "Sólido", "Alcanza Plenamente", "Supera Expectativas"]
                new Chart(ctx, {
                    type: "bar",
                    data: {
                        labels: ['1', '2', '3', '4'],
                        datasets: [{
                            label: "Frecuencias",
                            data: valores,
                            backgroundColor: ["#FF9999", "#99CCFF", "#99FF99", "#FFDD99"],
                            borderColor: ["#FF9999", "#99CCFF", "#99FF99", "#FFDD99"],
                            borderWidth: 1
                        }]
                    },
                    options: {
                        responsive: true,
                        scales: {
                            x: {
                                type: 'linear',  // Permite valores continuos en el eje X
                                ticks: {
                                    stepSize: 1,  // Controla los intervalos del eje X
                                }
                            },
                            y: { beginAtZero: true }
                        },
                        plugins: {
                            annotation: {
                                annotations: {
                                    lineMedia: {
                                        type: 'line',
                                        xMin: media,
                                        xMax: media,
                                        borderColor: 'blue',
                                        borderWidth: 2,
                                        label: {
                                            content: 'Media',
                                            enabled: true,
                                            position: 'end'
                                        }
                                    },
                                    lineMediana: {
                                        type: 'line',
                                        xMin: mediana,
                                        xMax: mediana,
                                        borderColor: 'green',
                                        borderWidth: 2,
                                        label: {
                                            content: 'Mediana',
                                            enabled: true,
                                            position: 'end'
                                        }
                                    },
                                    /*lineModa: {
                                        type: 'line',
                                        xMin: moda, // Ahora usa el valor correcto
                                        xMax: moda,
                                        borderColor: 'red',
                                        borderWidth: 2,
                                        label: {
                                            content: 'Moda',
                                            enabled: true,
                                            position: 'end'
                                        }
                                    }*/
                                }
                            }
                        }
                    }
                });


                // Calcular porcentaje de logro basado en el puntaje ideal
                let puntajeIdeal = (item.Respuestas[0] + item.Respuestas[1] + item.Respuestas[2] + item.Respuestas[3]) * 4;
                let puntajeObtenido = (item.Respuestas[0] * 1) + (item.Respuestas[1] * 2) + (item.Respuestas[2] * 3) + (item.Respuestas[3] * 4);
                let porcentajeLogro = (puntajeObtenido / puntajeIdeal) * 100;

                // Agregar el porcentaje al título
                let h3 = document.createElement("h3");
                h3.innerHTML = `${item.pregunta} <span style=\"float: right;\">${porcentajeLogro.toFixed(2)}%</span>`; // Se muestra el porcentaje al lado del título
                h3.className = "h3";
                divCmd6_2.appendChild(h3);


                let spanMedia = document.createElement("span");
                spanMedia.innerHTML = media;
                spanMedia.style.color = "black";
                let p = document.createElement("p");
                p.id = "lblMedia" + index;
                p.style.color = "blue";
                p.style.fontSize = 18;
                p.style.fontWeight = 500;
                p.style.marginBottom = 0;
                p.innerHTML =
                    "Media:  <span style=\"color:black; margin-right:20px;\">" + media +
                    "</span>" +
                    "<span style=\"color: green; font-weight:500; margin-right 10px;\">" +
                    "Mediana:  <span style=\"color:black;\ margin-right:20px;\">" + mediana + "</span>" +
                    "<span style=\"color: red; font-weight:500; margin-left 10px;\">" +
                    "Moda:  <span style=\"color:black;  margin-right:20px;\">" + moda + "</span>";
                divCmd6_2.appendChild(p);


                let resultado = document.createElement("p");
                resultado.style.color = "black";
                resultado.style.fontSize = 16;
                resultado.style.fontWeight = 400;

                // Calcular diferencia porcentual
                let diferencia = ((media - mediana) / mediana) * 100;

                // Determinar el tipo de asimetría
                let tipoAsimetria = "";
                if (Math.abs(diferencia) <= 2) {
                    tipoAsimetria = "Distribución Simétrica";
                } else if (diferencia > 0 && diferencia <= 10) {
                    tipoAsimetria = "Distribución Asimétrica Positiva Leve";
                } else if (diferencia > 10 && diferencia <= 25) {
                    tipoAsimetria = "Distribución Asimétrica Positiva Moderada";
                } else if (diferencia > 25) {
                    tipoAsimetria = "Distribución Asimétrica Positiva - Sesgada a la derecha";
                } else {
                    tipoAsimetria = "Distribución Asimétrica Negativa - Sesgada a la izquierda";
                }
                resultado.innerHTML = tipoAsimetria;//analizarDistribucion(media, mediana);
                divCmd6_2.appendChild(resultado);
                divCmd6_2.append(createTabla(item.Respuestas));
                divR.appendChild(divCmd6_2);

                container.appendChild(divR);
            });

        }

        // Función para calcular la mediana
        function calcularMediana(arr) {
            let sorted = arr.slice().sort((a, b) => a - b);
            let mid = Math.floor(sorted.length / 2);
            return sorted.length % 2 !== 0 ? sorted[mid] : (sorted[mid - 1] + sorted[mid]) / 2;
        }

        // Función para calcular la moda correctamente (obtiene la respuesta con más frecuencia)
        function calcularModa(respuestas) {
            let maxOcurrencias = Math.max(...respuestas);
            let index = respuestas.indexOf(maxOcurrencias);
            let categorias = ["No Cubre", "Sólido", "Alcanza Plenamente", "Supera Expectativas"];
            return { nombre: categorias[index], valor: maxOcurrencias };
        }



        function analizarDistribucion(media, mediana) {

            // Calcular porcentaje de diferencia
            let porcentajeDiferencia = ((media - mediana) / mediana) * 100;
            // Determinar el tipo de asimetría
            let tipoAsimetria = "";

            if (porcentajeDiferencia <= 5) {
                tipoAsimetria =
                    "<ul style=\"list-style: none; padding-left: 0; margin-top: 10px;\">" +
                    "<li style=\"padding-bottom: 5px; padding-top: 10px;\"> " +
                    "<b>Distribución:</b> Simétrica.</li> " +
                    "<li style=\"padding-bottom: 5px; padding-top: 10px;\">" +
                    "<b>Caracteristicas:</b> Media ≈ Mediana ≈ Moda " +
                    "<li style=\"padding-bottom: 5px; padding-top: 10px;\">" +
                    "<b>Interpretacion:</b> La media la mediana y la moda son cercanas lo que indica que el " +
                    "valor promedio representa de manera correcta el resultado.</li></ul>";


            }
            if (porcentajeDiferencia > 5 && porcentajeDiferencia <= 10) {
                tipoAsimetria =
                    "<ul style=\"list-style: none; padding-left: 0; margin-top: 10px;\">" +
                    "<li style=\"padding-bottom: 5px; padding-top: 10px;\"><span style=\"font-weight: 600; color:black;\"> " +
                    "Distribución Asimétrica Positiva Leve</span></li> " +
                    "<li>Media > Mediana</li>" +
                    "<li>La mayoría de los datos están en el rango bajo-medio, " +
                    "pero hay algunos valores extremos altos que elevan el promedio.</li> " +
                    "</ul>";
            }
            if (porcentajeDiferencia > 10 && porcentajeDiferencia <= 25) {
                tipoAsimetria =
                    "<ul style=\"list-style: none; padding-left: 0; margin-top: 10px;\">" +
                    "<li style=\"padding-bottom: 5px; padding-top: 10px;\"><span style=\"font-weight: 600; color:black;\"> " +
                    "Distribución Asimétrica Positiva Moderada</span></li> " +
                    "<li>Media > Mediana</li>" +
                    "<li>Hay más respuestas en los valores intermedios, pero un número " +
                    "importante de respuestas altas empuja la media hacia arriba.</li > " +
                    "</ul>";
            }
            if (porcentajeDiferencia > 25) {
                tipoAsimetria =
                    "<ul style=\"list-style: none; padding-left: 0; margin-top: 10px;\">" +
                    "<li style=\"padding-bottom: 5px; padding-top: 10px;\"><span style=\"font-weight: 600; color:black;\"> " +
                    "Distribución Asimétrica Positiva Sesgada a la derecha</span></li> " +
                    "<li>Media > Mediana</li>" +
                    "<li>La mayoría de las respuestas están en los valores bajos o " +
                    "intermedios, pero hay algunos valores extremadamente altos que elevan " +
                    "la media significativamente.</li> " +
                    "</ul>";
            }

            if (porcentajeDiferencia < -5) {
                tipoAsimetria =
                    "<ul style=\"list-style: none; padding-left: 0; margin-top: 10px;\">" +
                    "<li style=\"padding-bottom: 5px; padding-top: 10px;\"> " +
                    "<b>Distribución:</b> Asimétrica Negativa Sesgada a la izquierda</li> " +
                    "<li style=\"padding-bottom: 5px; padding-top: 10px;\">" +
                    "<b>Caracteristicas:</b> La Moda es mayor a la Mediana y a su vez esta es mayor " +
                    "a la Media. Estas distribuciones tienen colas más largas en el lado izquierdo (" +
                    " La mayor parte de los resultados son menores a la media ) " +
                    "<li style=\"padding-bottom: 5px; padding-top: 10px;\">" +
                    "<b>Interpretacion:</b> Existe un grupo relativamente pequeno de valores " +
                    "altos que elevan el promedio</li></ul>";
            }

            return tipoAsimetria;
        }

        // Función para calcular la Mediana
        function calcularMediana(arr) {
            let sorted = arr.slice().sort((a, b) => a - b);
            let mid = Math.floor(sorted.length / 2);
            return sorted.length % 2 !== 0 ? sorted[mid] : (sorted[mid - 1] + sorted[mid]) / 2;
        }

        // Datos de ejemplo (debes reemplazar esto con los datos obtenidos del backend)
        let datosEjemplo = [
            { Pregunta: "Orientación a Resultados", Respuestas: [13, 104, 220, 83] },
            { Pregunta: "Orientación a la Calidad", Respuestas: [10, 104, 224, 82] },
            { Pregunta: "Orientación al Vecino", Respuestas: [3, 93, 198, 99] },
            { Pregunta: "Trabajo en Equipo", Respuestas: [8, 77, 236, 103] },
            { Pregunta: "Liderazgo", Respuestas: [2, 19, 41, 13] }
        ];

        function createTabla(resp) {
            // Crear la tabla
            let table = document.createElement("table");
            table.style.width = "100%";
            table.style.borderCollapse = "collapse";
            table.style.margin = "0";

            // Crear el encabezado de la tabla (thead)
            let thead = document.createElement("thead");
            let trHead = document.createElement("tr");

            let th1 = document.createElement("th");
            th1.textContent = "Respuestas";
            th1.style.border = "1px solid black";
            th1.style.padding = "0px !important";
            th1.style.paddingLeft = "5px !important";
            th1.style.backgroundColor = "#f0f0f0";
            th1.style.fontSize = 14;
            let th2 = document.createElement("th");
            th2.textContent = "Ocurrencias";
            th2.style.border = "1px solid black";
            th2.style.padding = "10px";
            th2.style.backgroundColor = "#f0f0f0";
            th2.style.paddingLeft = "5px !important";
            th2.style.backgroundColor = "#f0f0f0";
            th2.style.fontSize = 14;
            trHead.appendChild(th1);
            trHead.appendChild(th2);
            thead.appendChild(trHead);
            table.appendChild(thead);

            // Crear el cuerpo de la tabla (tbody)
            let tbody = document.createElement("tbody");

            let respuestas = ["No Cubre", "Sólido", "Alcanza Plenamente", "Supera Expectativas"];
            var i = 0;
            respuestas.forEach(respuesta => {
                let tr = document.createElement("tr");

                let td1 = document.createElement("td");
                td1.textContent = respuesta;
                td1.style.border = "1px solid black";
                td1.style.padding = "5px";
                td1.style.paddingLeft = "10px";
                td1.style.fontSize = 14;
                let td2 = document.createElement("td");
                td2.textContent = resp[i];
                td2.style.border = "1px solid black";
                td2.style.padding = "5px";
                td2.style.textAlign = "center";
                td1.style.paddingLeft = "10px";
                td2.style.fontSize = 14;
                tr.appendChild(td1);
                tr.appendChild(td2);
                tbody.appendChild(tr);
                i = i + 1;
            });

            table.appendChild(tbody);
            return table;
        }
        function calcularModa4(arr) {
            const frecuencias = {};
            arr.forEach(num => frecuencias[num] = (frecuencias[num] || 0) + 1);
            let maxFreq = Math.max(...Object.values(frecuencias));
            return parseFloat(Object.keys(frecuencias).find(key => frecuencias[key] === maxFreq));
        }
        function createInformeGeneral(datos) {
            // Calcular valores
            const media = calcularMedia(datos);
            const mediana = calcularMediana(datos);
            const moda = calcularModa4(datos);

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

        }

        function createGraficoGeneral(apiResponse) {

            // Obtener etiquetas de competencias
            const etiquetas = apiResponse.map(item => item.pregunta);

            // Obtener los valores de cada nivel (1, 2, 3 y 4)
            const nivel1 = apiResponse.map(item => item.Respuestas[0]); // Respuestas con valor 1
            const nivel2 = apiResponse.map(item => item.Respuestas[1]); // Respuestas con valor 2
            const nivel3 = apiResponse.map(item => item.Respuestas[2]); // Respuestas con valor 3
            const nivel4 = apiResponse.map(item => item.Respuestas[3]); // Respuestas con valor 4

            const indices = Array.from({ length: apiResponse.length }, (_, i) => i + 1);



            // Crear gráfico con Chart.js
            const ctx = document.getElementById('graficoGeneral').getContext('2d');
            new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: etiquetas, // Competencias
                    datasets: [
                        {
                            label: "No Cubre (1)",
                            data: nivel1,
                            backgroundColor: "rgba(255, 99, 132, 0.6)", // Rojo
                            borderColor: "rgba(255, 99, 132, 1)",
                            borderWidth: 1
                        },
                        {
                            label: "Sólido (2)",
                            data: nivel2,
                            backgroundColor: "rgba(54, 162, 235, 0.6)", // Azul
                            borderColor: "rgba(54, 162, 235, 1)",
                            borderWidth: 1
                        },
                        {
                            label: "Alcanza Plenamente (3)",
                            data: nivel3,
                            backgroundColor: "rgba(75, 192, 192, 0.6)", // Verde
                            borderColor: "rgba(75, 192, 192, 1)",
                            borderWidth: 1
                        },
                        {
                            label: "Supera Expectativas (4)",
                            data: nivel4,
                            backgroundColor: "rgba(255, 206, 86, 0.6)", // Amarillo
                            borderColor: "rgba(255, 206, 86, 1)",
                            borderWidth: 1
                        }
                    ]
                },
                options: {
                    responsive: true,
                    scales: {
                        x: {
                            title: {
                                display: true,
                                text: 'Competencias'
                            }
                        },
                        y: {
                            title: {
                                display: true,
                                text: 'Frecuencia de Evaluaciones'
                            }
                        }
                    }
                }
            });
        }
    </script>
</asp:Content>
