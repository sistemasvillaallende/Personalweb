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

    if (porcentajeDiferencia < 0) {
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

// Función para calcular la media
function calcularMedia(arr) {
    return arr.reduce((a, b) => a + b, 0) / arr.length;
}

// Función para calcular la mediana
function calcularMediana(arr) {
    const sorted = [...arr].sort((a, b) => a - b);
    const mid = Math.floor(sorted.length / 2);
    return sorted.length % 2 !== 0 ? sorted[mid] : (sorted[mid - 1] + sorted[mid]) / 2;
}

// Función para calcular la moda
function calcularModa(arr) {
    const frecuencias = {};
    arr.forEach(num => frecuencias[num] = (frecuencias[num] || 0) + 1);
    let maxFreq = Math.max(...Object.values(frecuencias));
    return parseFloat(Object.keys(frecuencias).find(key => frecuencias[key] === maxFreq));
}


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

// Función para calcular la mediana
function calcularMediana2(valores, frecuencias) {
    let totalFrecuencia = frecuencias.reduce((a, b) => a + b, 0);
    let mitad = Math.floor(totalFrecuencia / 2);
    let acumulado = 0;

    for (let i = 0; i < valores.length; i++) {
        acumulado += frecuencias[i];
        if (acumulado >= mitad) {
            return valores[i]; // Retorna el valor donde se encuentra la mediana
        }
    }
}

// Función para calcular la moda
function calcularModa2(valores, frecuencias) {
    let maxFrecuencia = Math.max(...frecuencias);
    let indice = frecuencias.indexOf(maxFrecuencia);
    return valores[indice];
}


