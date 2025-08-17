
// PAra seleccionar boton guardar o eliminar del CRUD
const btnGuardar = document.querySelector('input[value="Guardar"]') || document.querySelector('input[value="Eliminar"]')
let formResult = document.querySelector('.form-result') // PAra mmostrar resultados de la accion

if (btnGuardar) 
btnGuardar.addEventListener('click', (e) => {
    e.preventDefault()

    let frm = document.forms['form-cliente']
    let formData = new FormData(frm)
    let object = {};
    let mth='get'
    let url =`/api/ClientesApi/`
    let rt = frm.action.split('/')

    
    if (rt.includes('Nuevo')) {
        mth = 'post'
        url=`/api/ClientesApi/Nuevo`
    }
    if (rt.includes('Editar')) {
        let clienteid = document.querySelector('#Id')
        mth = 'put'
        url = `/api/ClientesApi/${clienteid.value}`
        console.log(url)
    }
    if (rt.includes('Eliminar')) {
        let clienteid = document.querySelector('#Id')
        mth = 'delete'
        url = `/api/ClientesApi/${clienteid.value}`
    }

    
    formData.forEach((value, key) => object[key] = value);
    
    if (document.querySelector('#isDelete'))
        object['isDelete'] = document.querySelector('#isDelete').checked;
   
    if (formResult) formResult.innerHTML = ''; else formResult = frm;
    
   
    fetch(url, {
        method: mth,
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(object)
    }).then(uncliente => {
        if (!uncliente.ok) {
            throw new Error("Error al obtener el cliente");
        }
        return uncliente.text();
    }).then(datos => {
        
        if (datos) {
            
            if (datos.errors && Object.keys(datos.errors).length > 0) {
                formResult.className = `form-result error`
                formResult.innerHTML = `<span style="color:red">${datos.title}:</span>`

                for (const pair in datos.errors) {
                    formResult.insertAdjacentHTML('beforeend', `<div  style="border-bottom:1px dotted red"><strong>${pair}</strong>:<span>${datos.errors[pair].toString()}</span></div>`)
                }

                throw { ...datos };
            }
          
            alert('Registro afectado exitosamente')
            e.target.disabled = true

        }
       
    }).catch(error => {
        console.log(error)        
        alert("Ocurrió un error:", error)
    }
    )

}, false)
