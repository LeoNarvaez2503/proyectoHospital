window.onload = function () {
    listarMedicos();
}
async function listarMedicos() {
    pintar({
        url: "Medicos/ListarMedicos",
        cabeceras: ["ID Medico", "Nombre", "Apellido", "Especialidad", "Telefono", "email"],
        propiedades: ["id", "nombre", "apellido", "especialidadNombre", "telefono", "email"],
        editar: true,
        eliminar: true,
        propiedadId: "id"
    });
}

async function guardar() {
    let form = document.getElementById("frmOperaciones");
    let frm = new FormData(form);
    confirmacion(undefined, undefined, function (resp) {
        fetchPost("Medicos/GuardarMedico", "json", frm, function (res) {
            if (res == -1) {
                ErrorA("Oops... debes rellenar todos los campos");
                return;
            }
            limpiarForm();
            Exito("Registro guardado exitosamente");
        });

    });
}

function Editar(id) {
    if (id != 0) {
        fetchGet("Medicos/RecuperarMedico/?id=" + id, "json", function (data) {
            setN("id", data.id);
            setN("nombre", data.nombre);
            setN("apellido", data.apellido);
            cargarForaneas("Especialidades", "especialidadId");
            setN("especialidadId", data.especialidadId);
            setN("telefono", data.telefono);
            setN("email", data.email);

        });
    }
    else {
        limpiarForm();
        cargarForaneas("Especialidades", "especialidadId");
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

        fetchPost("Medicos/EliminarMedico", "json", frm, function (data) {
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
    listarMedicos();
}
