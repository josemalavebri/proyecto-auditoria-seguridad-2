// Función para buscar auditorías según filtros y cargar resultados
async function buscarAuditorias() {
    if (!validarElementos()) return;

    const filtros = obtenerFiltros();
    if (filtrosVacios(filtros)) {
        limpiarResultados();
        return;
    }

    const url = construirUrl(filtros);

    const datos = await obtenerDatos(url);
    if (!datos) {
        mostrarError('Error al cargar datos.');
        ocultarTabla();
        ocultarBotonCrear();
        return;
    }

    if (datos.mensaje) {
        mostrarMensaje(datos.mensaje);
        limpiarTabla();
        ocultarTabla();
        ocultarBotonCrear();
        return;
    }

    renderizarResultados(datos);
}

// Validación de elementos del DOM
function validarElementos() {
    return ['lugares', 'facultad', 'departamento', 'tabla-auditorias', 'tbody-auditorias', 'mensaje-auditorias', 'btn-crear-auditoria']
        .every(id => document.getElementById(id));
}

// Obtener valores de filtros del formulario
function obtenerFiltros() {
    return {
        nombreDireccion: document.getElementById('lugares').value.trim(),
        nombreFacultad: document.getElementById('facultad').value.trim(),
        nombreDepartamento: document.getElementById('departamento').value.trim(),
    };
}

// Verificar si todos los filtros están vacíos
function filtrosVacios(filtros) {
    return Object.values(filtros).every(valor => !valor);
}

// Limpiar resultados y ocultar tabla, mensajes y botón
function limpiarResultados() {
    limpiarTabla();
    ocultarTabla();
    mostrarMensaje('');
    ocultarBotonCrear();
}

function limpiarTabla() {
    document.getElementById('tbody-auditorias').innerHTML = '';
}

function ocultarTabla() {
    document.getElementById('tabla-auditorias').style.display = 'none';
}

function mostrarMensaje(texto) {
    document.getElementById('mensaje-auditorias').textContent = texto;
}

function ocultarBotonCrear() {
    document.getElementById('btn-crear-auditoria').classList.add('d-none');
}

function mostrarBotonCrear() {
    document.getElementById('btn-crear-auditoria').classList.remove('d-none');
}

// Construir URL con parámetros para la petición
function construirUrl(filtros) {
    Object.keys(filtros).forEach(k => !filtros[k] && delete filtros[k]);
    const query = new URLSearchParams(filtros).toString();
    return `/Index?handler=UltimaEjecucionFiltrada${query ? '&' + query : ''}`;
}

// Petición fetch y obtención de datos JSON
async function obtenerDatos(url) {
    try {
        const res = await fetch(url);
        if (!res.ok) return null;
        return await res.json();
    } catch {
        return null;
    }
}

// Renderizar tabla con los datos recibidos y mostrar botón si hay datos
function renderizarResultados(datos) {
    limpiarTabla();
    mostrarMensaje('');

    if (!datos || !datos.fechaEjecucion) {
        mostrarMensaje('No se encontraron resultados.');
        ocultarTabla();
        ocultarBotonCrear();
        return;
    }

    const tbody = document.getElementById('tbody-auditorias');
    const fila = document.createElement('tr');
    fila.dataset.idEncuesta = datos.idEncuesta;

    // Celda ID Encuesta (primera columna)
    const idCelda = document.createElement('td');
    idCelda.textContent = datos.idEncuesta || 'N/A';
    fila.appendChild(idCelda);

    // Celda Fecha Ejecución (segunda columna)
    const fechaCelda = document.createElement('td');
    fechaCelda.textContent = new Date(datos.fechaEjecucion).toLocaleString();
    fila.appendChild(fechaCelda);

    // Celda Descripción (tercera columna)
    const descCelda = document.createElement('td');
    descCelda.textContent = datos.descripcion || 'Sin descripción';
    fila.appendChild(descCelda);

    tbody.appendChild(fila);

    const tabla = document.getElementById('tabla-auditorias');
    if (tabla) tabla.style.display = '';

    mostrarBotonCrear();
}


//---------------------------------------

// Función genérica para autocompletar inputs (valida existencia)
function autocompletar(inputId, sugerenciasId, handler) {
    const input = document.getElementById(inputId);
    const sugerenciasDiv = document.getElementById(sugerenciasId);

    if (!input || !sugerenciasDiv) {
        return;
    }

    input.addEventListener('input', async () => {
        const valor = input.value.trim();
        if (!valor) {
            sugerenciasDiv.innerHTML = '';
            return;
        }

        try {
            const res = await fetch(`/Index?handler=${handler}&termino=${encodeURIComponent(valor)}`);
            const datos = await res.json();

            sugerenciasDiv.innerHTML = '';
            datos.forEach(item => {
                const btn = document.createElement('button');
                btn.type = 'button';
                btn.className = 'list-group-item list-group-item-action';
                btn.textContent = item;
                btn.onclick = () => {
                    input.value = item;
                    sugerenciasDiv.innerHTML = '';
                };
                sugerenciasDiv.appendChild(btn);
            });
        } catch (e) {
            console.error(`Error al buscar sugerencias de ${handler}`, e);
        }
    });
}

// Inicialización segura de autocompletados solo si existen los inputs
['departamento', 'facultad', 'lugares'].forEach(id => {
    const sugerenciasId = id === 'lugares' ? 'sugerencias' : `sugerencias-${id}`;
    const handlerMap = {
        departamento: 'SugerenciasDepartamento',
        facultad: 'SugerenciasFacultad',
        lugares: 'SugerenciasDireccion'
    };
    if (document.getElementById(id)) {
        autocompletar(id, sugerenciasId, handlerMap[id]);
    }
});


// Función para mostrar detalle de encuesta, renderizado simple sin tabla
async function detallarEncuesta(id) {
    try {
        const res = await fetch(`/Index?handler=DetalleEncuesta&id=${id}`);
        const detalle = await res.json();
        const contenedor = document.getElementById('contenedor-detalle');

        if (!contenedor) {
            return;
        }

        if (!detalle) {
            contenedor.textContent = 'No hay detalles disponibles.';
            return;
        }

        contenedor.innerHTML = `
            <div><strong>Id Respuesta:</strong> ${detalle.idRespuesta}</div>
            <div><strong>Pregunta:</strong> ${detalle.pregunta}</div>
            <div><strong>Items Asociados:</strong> ${detalle.itemsAsociados}</div>
            <div><strong>Marcado:</strong> ${detalle.marcado ? 'Sí' : 'No'}</div>
        `;
    } catch (error) {
        console.error('Error al obtener detalle de encuesta', error);
    }
}


