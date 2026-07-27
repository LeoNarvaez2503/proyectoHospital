window.onload = function () {
    listarEspecialidades();
}
async function listarEspecialidades() {
    pintar({
        url: "Especialidades/ListarEspecialidades",
        cabeceras: ["ID Especialidad", "Nombre"],
        propiedades: ["id", "nombre"],
        editar: true,
        eliminar: true,
        propiedadId: "id"
    });
}

async function guardar() {
    let form = document.getElementById("frmOperaciones");
    let frm = new FormData(form);
    confirmacion(undefined, undefined, function (resp) {
        fetchPost("Especialidades/GuardarEspecialidad", "json", frm, function (res) {
            if (res == -1) {
                ErrorA();
                return;
            }
            limpiarForm();
            Exito();
        });

    });
}

function Editar(id) {
    if (id != 0) {
        fetchGet("Especialidades/RecuperarEspecialidad/?id=" + id, "json", function (data) {
            setN("id", data.id);
            setN("nombre", data.nombre);
        });
    }
    else {
        limpiarForm();
        setN("id", id);
    }

}

function Eliminar(id) {
    confirmacion(undefined, "¿Seguro desea eliminar?", function (resp) {
        let tokenElement = document.getElementsByName("__RequestVerificationToken")[0];
        let token = tokenElement ? tokenElement.value : "";
        let frm = new FormData();
        frm.append("id", id);
        frm.append("__RequestVerificationToken", token);

        fetchPost("Especialidades/EliminarEspecialidad", "json", frm, function (data) {
            if (data == -1) {
                ErrorA("No se puede eliminar, por dependencia con otras tablas");
                return;
            }
            limpiarForm();
            Exito();
        });
    });
}

function limpiarForm() {
    limpiarDatos("frmOperaciones");
    listarEspecialidades();
}