
//------------------------------------------------------CREADOR DE ENCUESTAS--------------------------

async function buscarEncuestas() {
    // Mostrar la sección que estaba oculta
    document.getElementById('seccionAuditoria').classList.remove('d-none');

    // Obtener valores de inputs (los filtros que tienes definidos)
    const nombreDireccion = document.getElementById('lugares').value.trim();
    const nombreFacultad = document.getElementById('facultad').value.trim();
    const nombreDepartamento = document.getElementById('departamento').value.trim();

    if (!nombreDepartamento || !nombreDireccion) {
        alert('Debe ingresar Departamento y Dirección para buscar.');
        return;
    }

    // Construir parámetros para la query string
    const params = new URLSearchParams();
    params.append('nombreDepartamento', nombreDepartamento);
    params.append('nombreDireccion', nombreDireccion);
    if (nombreFacultad) params.append('nombreFacultad', nombreFacultad);

    const url = `/Index?handler=BuscarEncuestas&${params.toString()}`;

    try {
        const response = await fetch(url);
        if (!response.ok) throw new Error('Error en la consulta');
        const datos = await response.json();

        // Mostrar tabla y renderizar filas
        const tabla = document.getElementById('tabla-auditorias');
        const tbody = document.getElementById('tbody-auditorias');

        tabla.style.display = 'table';  // Mostrar tabla
        tbody.innerHTML = '';            // Limpiar tabla previa

        if (!datos || datos.length === 0) {
            document.getElementById('mensaje-auditorias').textContent = 'No se encontraron encuestas.';
            tabla.style.display = 'none';  // Ocultar tabla si no hay datos
            return;
        }

        document.getElementById('mensaje-auditorias').textContent = '';

        // Asumo que datos es un array o un objeto (adaptar según backend)
        const lista = Array.isArray(datos) ? datos : [datos];

        lista.forEach(encuesta => {
            const tr = document.createElement('tr');
            tr.innerHTML = `
                <td>${encuesta.idEncuesta}</td>
                <td>${new Date(encuesta.fechaEjecucion).toLocaleDateString()}</td>
                <td>${encuesta.descripcion}</td>
            `;
            tbody.appendChild(tr);
        });

    } catch (error) {
        console.error(error);
        document.getElementById('mensaje-auditorias').textContent = 'Error al cargar encuestas.';
    }
}



//--------------------------------------------------------INDEX--------------------------
async function buscarAuditorias() {
    // Obtener valores de inputs y limpiar espacios
    const nombreDireccion = document.getElementById('lugares').value.trim();
    const nombreFacultad = document.getElementById('facultad').value.trim();
    const nombreDepartamento = document.getElementById('departamento').value.trim();

    // Validar campos obligatorios: departamento y direccion (facultad opcional)
    if (!nombreDepartamento || !nombreDireccion) {
        alert('Debe ingresar Departamento y Dirección para buscar.');
        return;
    }


    // Construir URL con query params
    const params = new URLSearchParams();
    params.append('nombreDepartamento', nombreDepartamento);
    params.append('nombreDireccion', nombreDireccion);
    if (nombreFacultad) params.append('nombreFacultad', nombreFacultad);

    const url = `/Index?handler=Resumen&${params.toString()}`;

    try {
        const response = await fetch(url);
        if (!response.ok) throw new Error('Error en la consulta');
        const json = await response.json();
            console.log(json);

            renderizarResultados(json);
        } catch (error) {
            console.error(error);
            document.getElementById('contenedor-auditorias').innerHTML = `<div class="alert alert-danger">Error al cargar auditorías.</div>`;
        }
    }

    function renderizarResultados(datos) {
        const contenedor = document.getElementById('contenedor-auditorias');
        contenedor.innerHTML = ''; 

        let lista = []; 

        if (Array.isArray(datos)) {
            lista = datos; 
            console.log("Datos es un array. Longitud de lista:", lista.length); // DEBUG
        } else if (datos && typeof datos === 'object') {
            lista = [datos]; 
            console.log("Datos es un objeto. Longitud de lista:", lista.length); // DEBUG
        } else {
            console.log("Datos no es ni array ni objeto válido."); // DEBUG
        }

        if (lista.length === 0) {
            contenedor.innerHTML = `<p>No se encontraron encuestas con esos filtros.</p>`;
            console.log("Lista está vacía. Mostrando mensaje de no resultados."); // DEBUG
            return;
        }

        console.log("Lista tiene elementos. Procediendo a renderizar la tabla."); // DEBUG


        // Crear tabla simple
        const table = document.createElement('table');
        table.classList.add('table', 'table-striped', 'table-bordered');

        // Encabezado
        table.innerHTML = `
        <thead class="table-dark">
          <tr>
            <th>Id Encuesta</th>
            <th>Fecha Auditoría</th>
            <th>Persona Auditada</th>
            <th>Auditor</th>
            <th>Descripción Encuesta</th>
            <th>Cantidad Preguntas</th>
            <th>Cantidad Ítems</th>
            <th>Detalle Encuesta</th>
          </tr>
        </thead>
      `;

        // Cuerpo
        const tbody = document.createElement('tbody');

        lista.forEach(dato => {
            const tr = document.createElement('tr');

            tr.innerHTML = `
          <td>${dato.idEncuesta}</td>
          <td>${new Date(dato.fechaAuditoria).toLocaleString()}</td>
          <td>${dato.personaAuditada || ''}</td>
          <td>${dato.auditor || ''}</td>
          <td>${dato.descripcionEncuesta || ''}</td>
          <td>${dato.cantidadPreguntas}</td>
          <td>${dato.cantidadItems}</td>
        <td>
            <button class="btn btn-succes btn-detalle" data-id="${dato.idEncuesta}">
              Detalle Encuesta
            </button>
        </td>
          `;

            tbody.appendChild(tr);
        });

        table.appendChild(tbody);
        contenedor.appendChild(table);
    }


// SECCION - INDEX -------------------------------DETALLE DE LA ENCUESTA

document.addEventListener('click', async function (e) {
    if (e.target && e.target.classList.contains('btn-detalle')) {
        const idEncuesta = e.target.getAttribute('data-id');
        if (!idEncuesta) return;

        try {
            const response = await fetch(`/Index?handler=DetalleEncuesta&idEncuesta=${idEncuesta}`);
            if (!response.ok) throw new Error('Error al cargar detalle de encuesta');

            const detalle = await response.json();
            console.log(detalle); // DEBUG: Verificar estructura de detalle
            renderizarDetalleEncuesta(detalle);
        } catch (error) {
            console.error(error);
            alert('No se pudo cargar el detalle de la encuesta');
        }
    }
});

function renderizarDetalleEncuesta(datos) {
    const contenedor = document.getElementById('contenedor-detalle');
    contenedor.innerHTML = '';  // Limpiar contenido previo

    if (!datos || datos.length === 0) {
        contenedor.innerHTML = '<p>No se encontraron detalles para esta encuesta.</p>';
        return;
    }

    const table = document.createElement('table');
    table.classList.add('table', 'table-sm', 'table-bordered');

    table.innerHTML = `
    <thead class="table-secondary">
      <tr>
        <th>Pregunta</th>
        <th>Título</th>
        <th>Código</th>
        <th>Descripción</th>
        <th>% Cumplimiento</th>
        <th>Comentario</th>
      </tr>
    </thead>
  `;

    const tbody = document.createElement('tbody');

    datos.forEach(item => {
        const tr = document.createElement('tr');
        tr.innerHTML = `
      <td>${item.pregunta}</td>
      <td>${item.titulo}</td>
      <td>${item.codigo}</td>
      <td>${item.descripcion}</td>
      <td>${item.porcentajeCumplimiento ?? ''}</td>
      <td>${item.comentario ?? ''}</td>
    `;
        tbody.appendChild(tr);
    });

    table.appendChild(tbody);
    contenedor.appendChild(table);
}


//SECCION - INDEX --------------------------------FIN DETALLE DE LA ENCUESTA--------------------------------

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
        const res = await fetch(`/CreadorEncuesta?handler=DetalleEncuesta&id=${id}`);
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

//--------------------AGREGAR NUEVA ENCUESTA--------------------
function alternarSeccion(crearNueva) {
    const divNueva = document.getElementById('nueva-seccion');
    const divExistente = document.getElementById('seccion-existente');
    if (crearNueva) {
        divNueva.classList.remove('d-none');
        divExistente.classList.add('d-none');
    } else {
        divNueva.classList.add('d-none');
        divExistente.classList.remove('d-none');
    }
}

// Filtrar ítems por grupo sin desmarcar checkboxes
document.querySelectorAll('.item-titulo').forEach(titulo => {
    titulo.addEventListener('click', () => {
        const grupoSeleccionado = titulo.getAttribute('data-group');
        document.querySelectorAll('.item-group').forEach(grupo => {
            if (grupo.getAttribute('data-group') === grupoSeleccionado) {
                grupo.classList.remove('d-none');
            } else {
                grupo.classList.add('d-none');
                // No desmarcar checkboxes para preservar selección
                // grupo.querySelectorAll('input[type=checkbox]').forEach(chk => chk.checked = false);
            }
        });
        actualizarItemsAgregados();
    });
});

const itemsAgregadosUL = document.getElementById('items-agregados');
const form = document.getElementById('form-pregunta-completa');
const accordion = document.getElementById('accordionPreguntas');
let preguntaCount = 0;

function actualizarItemsAgregados() {
    // Se toman todos los checkboxes marcados en todo el formulario, no solo visibles
    const checkedItems = Array.from(document.querySelectorAll('input[name="items"]:checked'));
    itemsAgregadosUL.innerHTML = '';
    if (checkedItems.length === 0) {
        const li = document.createElement('li');
        li.className = 'list-group-item text-muted';
        li.textContent = 'No hay ítems seleccionados.';
        itemsAgregadosUL.appendChild(li);
        return;
    }
    checkedItems.forEach(chk => {
        const li = document.createElement('li');
        li.className = 'list-group-item';
        li.textContent = chk.nextElementSibling.textContent;
        itemsAgregadosUL.appendChild(li);
    });
}

// Escuchar cambios en todos los checkboxes, sin importar si están visibles o no
document.querySelectorAll('input[name="items"]').forEach(chk => {
    chk.addEventListener('change', actualizarItemsAgregados);
});

actualizarItemsAgregados();

form.addEventListener('submit', e => {
    e.preventDefault();

    const preguntaText = document.getElementById('pregunta').value.trim();
    if (!preguntaText) return alert('Debe ingresar la pregunta.');

    let seccion;
    if (document.getElementById('toggleNuevaSeccion').checked) {
        seccion = document.getElementById('nombreSeccion').value.trim();
        if (!seccion) return alert('Debe ingresar el nombre de la nueva sección.');
    } else {
        const sel = document.getElementById('seccion');
        seccion = sel.options[sel.selectedIndex].text;
        if (!sel.value) return alert('Debe seleccionar una sección existente.');
    }

    const checkedItems = Array.from(document.querySelectorAll('input[name="items"]:checked'))
        .map(chk => chk.value);
    if (checkedItems.length === 0) return alert('Debe seleccionar al menos un ítem.');

    preguntaCount++;
    const collapseId = 'collapsePregunta' + preguntaCount;
    const headingId = 'headingPregunta' + preguntaCount;

    const itemsHtml = checkedItems.map(item => `<li>${item}</li>`).join('');

    const accordionItem = document.createElement('div');
    accordionItem.className = 'accordion-item';
    accordionItem.innerHTML = `
    <h2 class="accordion-header" id="${headingId}">
      <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#${collapseId}" aria-expanded="false" aria-controls="${collapseId}">
        ${preguntaText} <small class="text-muted ms-3">[${seccion}]</small>
      </button>
    </h2>
    <div id="${collapseId}" class="accordion-collapse collapse" aria-labelledby="${headingId}">
      <div class="accordion-body">
        <strong>Ítems asociados:</strong>
        <ul>${itemsHtml}</ul>
      </div>
    </div>
  `;

    accordion.appendChild(accordionItem);

    form.reset();
    alternarSeccion(false);
    // Mostrar todas las categorías al limpiar formulario
    document.querySelectorAll('.item-group').forEach(g => g.classList.remove('d-none'));
    actualizarItemsAgregados();
});

//--------------------FIN AGREGAR NUEVA ENCUESTA--------------------


//---------------------REALIZAR AUDITORIA--------------------



//---------------------FIN REALIZAR AUDITORIA--------------------