
var unCliente = () => {
    var clienteid = document.getElementById("ClientesModelId").value;
    //fetch("/api/ProductosApi/"+ clienteid)

    /*fetch(`/api/ClientesApi/${clienteid}`)
        .then(
            uncliente => {
                if (!uncliente.ok) {
                    alert("Error al obtener el cliente")
                }
                return uncliente.json();
            })
        .then(datos => {
            console.log(datos)
            document.getElementById("Correo").value = datos.email;
            document.getElementById("Cedula_RUC").value = datos.cedula_RUC;
            document.getElementById("Telefono").value = datos.telefono;
            document.getElementById("Direccion").value = datos.direccion;
        }).catch(
            error => {
                alert("Ocucion un error:", error)
            }
        )*/

    $.get(`/api/ClientesApi/${clienteid}`, (uncliente) => {
        document.getElementById("Correo").value = uncliente.email;
        document.getElementById("Cedula_RUC").value = uncliente.cedula_RUC;
        document.getElementById("Telefono").value = uncliente.telefono;
        document.getElementById("Direccion").value = uncliente.direccion;
    })
}

var Lista_Productos = () => {
    $.get(`/api/ProductosApi`,async (listaproductos) => {
        html = "";
        $.each(listaproductos, (index, producto) => {
            html += `<tr>
                <td> ${producto.nombre} </td>
                <td> ${producto.precio} </td>
                <td> <input type='number' min="1" value="0" id="qty_${producto.id}"/> </td>
                <td> <button type="button"
                data-id="${producto.id}"
                data-nombre="${producto.nombre}"
                data-precio="${producto.precio}"
                onclick="cargarproducto(this)"
                class="btn-success">+</button> </td>
            `;
        })
        await $("#Lista_prodcutos").html(html)
    })
}

var cargarproducto = (producto) => {
    const id = producto.dataset.id
    const nombre = producto.dataset.nombre
    const precio = parseFloat(producto.dataset.precio)
    const cantidad = document.getElementById(`qty_${id}`).value


    //const tabla = document.getElementById("productosTable")
   // const tbody = tabla.querySelector('tbody')
   // const btnAdd = document.getElementById("btnAgregarFila")
    //const subTotal = document.getElementById("Sub_total")





}