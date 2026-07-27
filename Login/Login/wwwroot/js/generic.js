function get(valor) {
    return document.getElementById(valor).value;
}
function set(idControl, valor) {
    document.getElementById(idControl).value = valor;
}

function setN(namecontrol, valor) {
    let element = document.getElementsByName(namecontrol)[0];
    if (element) {
        element.value = valor;
    } else {
        console.error(`Elemento con name='${namecontrol}' no encontrado.`);
    }
}
function getN(namecontrol) {
    return document.getElementsByName(namecontrol)[0].value;
}
function limpiarDatos(idFormulario) {
    let elementos = document.querySelectorAll("#" + idFormulario + " [name]");
    console.log(elementos);
    for (let i = 0; i < elementos.length; i++) {
        if (elementos[i].name !== "__RequestVerificationToken") {
            elementos[i].value = "";
        }
    }
}
async function fetchGet(url, tipoRespuesta, callback) {
    try {
        let urlCompleta = window.location.protocol + "//" + window.location.host + "/" + url;
        let res = await fetch(urlCompleta);
        if (!res.ok) {
            let errorText = await res.text();
            console.error("Error en respuesta GET:", errorText);
            alert("Error en el servidor (" + res.status + "): " + res.statusText);
            return;
        }
        if (tipoRespuesta == "json")
            res = await res.json();
        else if (tipoRespuesta == "text")
            res = await res.text();
        else if (tipoRespuesta == "none")
            res = null;

        callback(res);
    } catch (e) {
        console.error("Error en fetchGet:", e);
        alert("Ocurrio un problema en GET: " + e);
    }
}

async function cargarForaneas(tabla, id) {
    fetchGet("Generic/obtenerClaves/?tabla=" + tabla, "json", function (data) {
        let select = document.getElementById(id);
        if (!select || !data) return;
        select.innerHTML = "";
        for (let clave of data) {
            let foranea = document.createElement("option");
            foranea.value = clave.id;
            foranea.textContent = clave.descripcion;
            select.appendChild(foranea);
        }
    });
}
async function fetchPost(url, tipoRespuesta, frm, callback) {
    try {
        let urlCompleta = window.location.protocol + "//" + window.location.host + "/" + url;
        let res = await fetch(urlCompleta, {
            method: "POST",
            body: frm
        });
        if (!res.ok) {
            let errorText = await res.text();
            console.error("Error en respuesta POST:", errorText);
            alert("Error en el servidor (" + res.status + "): " + res.statusText);
            return;
        }
        if (tipoRespuesta == "json")
            res = await res.json();
        else if (tipoRespuesta == "text")
            res = await res.text();
        else if (tipoRespuesta == "none")
            res = null;
        callback(res);

    } catch (e) {
        alert("Ocurrio un problema en POST: " + e);
    }
}

let objConfiguracionGlobal;

function pintar(objConfiguracion) {
    objConfiguracionGlobal = objConfiguracion;
    if (objConfiguracionGlobal.divContenedor == undefined) {
        objConfiguracionGlobal.divContenedor = "divContenedor"
    }
    if (objConfiguracionGlobal.editar == undefined) {
        objConfiguracionGlobal.editar = false
    }
    if (objConfiguracionGlobal.eliminar == undefined) {
        objConfiguracionGlobal.eliminar = false
    }
    if (objConfiguracionGlobal.propiedadId == undefined) {
        objConfiguracionGlobal.propiedadId = ""
    }
    fetchGet(objConfiguracion.url, "json", function (res) {
        generarTabla(res);
    })
}

let tablaGrid = null;
function generarTabla(res, id = "divTabla") {

    if (!res || res.length === 0) {
        return "<p>No se encontraron resultados.</p>";
    }

    let cabeceras = objConfiguracionGlobal.cabeceras;
    let propiedades = objConfiguracionGlobal.propiedades;
    let datos = [];

    if ((objConfiguracionGlobal.editar == true || objConfiguracionGlobal.eliminar == true)) {
        cabeceras.push("Operaciones");
    }

    for (let i = 0; i < res.length; i++) {
        let fila = [];
        for (let j = 0; j < propiedades.length; j++) {
            fila.push(res[i][propiedades[j]]);
        }

        let contenido = "";
        if ((objConfiguracionGlobal.editar == true || objConfiguracionGlobal.eliminar == true)) {
            let propiedadId = objConfiguracionGlobal.propiedadId;
            if (objConfiguracionGlobal.editar == true) {
                contenido +=
                    `<i onclick="Editar(${res[i][propiedadId]})" data-bs-toggle="modal" data-bs-target="#operacionesModal" class="btn btn-primary me-2">
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16">
                            <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" />
                            <path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5z" />
                        </svg>
                        </i>`;
            }
            if (objConfiguracionGlobal.eliminar == true) {
                contenido +=
                    `<i onclick="Eliminar(${res[i][propiedadId]})" class="btn btn-danger">
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-x-lg" viewBox="0 0 16 16">
                            <path d="M2.146 2.854a.5.5 0 1 1 .708-.708L8 7.293l5.146-5.147a.5.5 0 0 1 .708.708L8.707 8l5.147 5.146a.5.5 0 0 1-.708.708L8 8.707l-5.146 5.147a.5.5 0 0 1-.708-.708L7.293 8z" />
                        </svg>
                        </i>`;
            }

        }

        let botones = document.createElement("div");
        botones.innerHTML = contenido;

        fila.push(botones);
        datos.push(fila);
    }

    if (tablaGrid) {
        tablaGrid.updateConfig({ data: datos }).forceRender();
    }
    else {
        tablaGrid = new gridjs.Grid({
            columns: cabeceras,
            data: datos,
            pagination: {
                limit: 5,
                summary: true,
            },
            search: true,
            sort: true,
            language: {
                'search': {
                    'placeholder': 'Buscar...'
                },
                'pagination': {
                    'next': 'Siguiente',
                    'previous': 'Anterior',
                    'showing': 'Mostrando',
                    'results': 'resultados'
                }
            },
        }).render(document.getElementById(id));
    }
}


function Exito() {
    Swal.fire({
        position: "top-end",
        icon: "success",
        title: "Se aplicaron los cambios",
        showConfirmButton: false,
        timer: 700
    });
}

function ErrorA(mensaje = "Debes rellenar todos los campos") {
    Swal.fire({
        icon: "error",
        title: "Oops...",
        text: mensaje
    });
}
function confirmacion(titulo = "Confirmacion", texto = "¿Desea guardar los cambios?", callback) {
    Swal.fire({
        title: titulo,
        text: texto,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si"
    }).then((result) => {
        if (result.isConfirmed) {
            callback();
        }
    });
}

function validacion() {
    Swal.fire({
        icon: "warning",
        title: "Campos vacíos",
        text: "Todos los campos son obligatorios. Por favor, complétalos."
    });
}
