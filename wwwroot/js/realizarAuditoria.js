//---------------------REALIZAR AUDITORIA--------------------
window.addEventListener('DOMContentLoaded', async () => {

    const contenedor = document.getElementById('contenedor-auditorias-detalle');
    if (!contenedor) {
        return;
    }
    contenedor.innerHTML = '<div class="text-center py-5"><span class="spinner-border text-primary" role="status"></span><span class="ms-2">Cargando datos...</span></div>';

    try {
        const url = `/RealizarAuditoria?handler=ResumenProgramadas`;

        const response = await fetch(url);
        if (!response.ok) {
            throw new Error(`Error en la consulta: ${response.status}`);
        }

        const datos = await response.json();

        if (!Array.isArray(datos) || datos.length === 0) {
            contenedor.innerHTML = `<div class="alert alert-warning" role="alert">No se encontraron registros.</div>`;
            return;
        }

        const table = document.createElement('table');
        table.classList.add('table', 'table-striped', 'table-bordered', 'table-hover');

        // Modificamos el encabezado para añadir una columna de "Acciones"
        table.innerHTML = `
            <thead class="table-dark">
                <tr>
                    <th>Dirección</th>
                    <th>Departamento</th>
                    <th>Facultad</th>
                    <th>ID Encuesta</th>
                    <th>Fecha Programada</th>
                    <th>Descripción</th>
                    <th>Acciones</th> </tr>
            </thead>
        `;

        const tbody = document.createElement('tbody');

        datos.forEach(item => {
            const tr = document.createElement('tr');
            tr.innerHTML = `
                <td>${item.direccion || ''}</td>
                <td>${item.departamento || ''}</td>
                <td>${item.facultad || ''}</td>
                <td>${item.idEncuesta || ''}</td>
                <td>${new Date(item.fechaProgramada).toLocaleString()}</td>
                <td>${item.descripcion || ''}</td>
                <td>
                    <button class="btn btn-sm btn-primary" onclick="realizarAuditoria(${item.idEncuesta})">
                        Realizar Auditoría
                    </button>
                </td>
            `;
            tbody.appendChild(tr);
        });

        table.appendChild(tbody);
        contenedor.innerHTML = '';
        contenedor.appendChild(table);
        console.log('Tabla renderizada con éxito.');

    } catch (error) {
        contenedor.innerHTML = `<div class="alert alert-danger">Error al cargar auditorías.</div>`;
    }
});

// Función de ejemplo para manejar el botón de "Realizar Auditoría"
function realizarAuditoria(idEncuesta) {
    console.log(`Se ha hecho clic en el botón de la encuesta con ID: ${idEncuesta}`);
    // Aquí puedes redirigir a otra página, abrir un modal o ejecutar otra lógica.
    // Por ejemplo: window.location.href = `/RealizarAuditoria?id=${idEncuesta}`;
}
//---------------------FIN REALIZAR AUDITORIA--------------------