// 1) Traer datos del cliente
function unCliente() {
    const clienteId = document.getElementById("ClienteId").value;
    if (!clienteId) return;

    fetch(`/api/ClientesApi/${clienteId}`)
        .then(res => res.json())
        .then(cli => {
            document.getElementById("Correo").value = cli.email || "";
            //document.getElementById("Cedula_RUC").value = cli.cedula_RUC || "";
            document.getElementById("Telefono").value = cli.telefono || "";
            //document.getElementById("Direccion").value = cli.direccion || "";
        })
        .catch(() => alert("No se pudo obtener el cliente"));
}

// 2) Listar eventos en el modal
function Lista_Eventos() {
    fetch(`/api/EventosApi`)
        .then(res => res.json())
        .then(eventos => {
            let html = "";
            eventos.forEach(ev => {
                html += `
                  <tr>
                    <td>${ev.nombre}</td>
                    <td>${ev.fecha}</td>
                    <td>${ev.ubicacion}</td>
                    <td>
                      <button type="button"
                              class="btn btn-success"
                              onclick="cargarEvento(${ev.eventoId}, '${ev.nombre}', '${ev.fecha}', '${ev.ubicacion}')">+</button>
                    </td>
                  </tr>`;
            });
            document.getElementById("lista_eventos").innerHTML = html;
        })
        .catch(() => alert("No se pudo cargar la lista de eventos"));
}

// 3) Cargar evento a la tabla principal
function cargarEvento(id, nombre, fecha, ubicacion) {
    const tbody = document.querySelector("#eventosTable tbody");
    tbody.innerHTML = ""; // si quieres solo un evento
    const fila = `
      <tr data-id="${id}">
        <td>${nombre}</td>
        <td>${fecha}</td>
        <td>${ubicacion}</td>
        <td><button type="button" class="btn btn-danger" onclick="this.closest('tr').remove()">X</button></td>
      </tr>`;
    tbody.innerHTML = fila;
}

// 4) Crear la reservación
function crear_reservacion() {
    const clienteId = parseInt(document.getElementById("ClienteId").value);
    const fechaReserva = document.getElementById("FechaReserva").value;

    const fila = document.querySelector("#eventosTable tbody tr[data-id]");
    if (!fila) { alert("Seleccione un evento"); return; }
    const eventoId = parseInt(fila.dataset.id);

    const reservacion = { clienteId, eventoId, fechaReserva };

    fetch("/api/ReservacionesApi", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(reservacion)
    })
        .then(r => {
            if (r.ok) {
                alert("Reservación guardada");
                window.location.href = "/Reservaciones/Index";
            } else {
                alert("Error al guardar");
            }
        })
        .catch(() => alert("Error en la petición"));
}
