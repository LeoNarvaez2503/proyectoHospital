window.onload = function () {
    listarPacientes();
}
async function listarPacientes() {
    pintar({
        url: "Pacientes/ListarPacientes",
        cabeceras: ["ID Paciente", "Nombre", "Apellido", "Fecha de nacimiento", "Telefono", "email", "direccion"],
        propiedades: ["id", "nombre", "apellido", "fechaNacimiento", "telefono", "email", "direccion"],
        editar: true,
        eliminar: true,
        propiedadId: "id"
    });
}

async function guardar() {
    let form = document.getElementById("frmOperaciones");
    let frm = new FormData(form);
    confirmacion(undefined, undefined, function (resp) {
        fetchPost("Pacientes/GuardarPaciente", "json", frm, function (res) {
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
        fetchGet("Pacientes/RecuperarPaciente/?id=" + id, "json", function (data) {
            setN("id", data.id);
            setN("nombre", data.nombre);
            setN("apellido", data.apellido);
            setN("fechaNacimiento", data.fechaNacimiento);
            setN("telefono", data.telefono);
            setN("email", data.email);
            setN("direccion", data.direccion);

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

        fetchPost("Pacientes/EliminarPaciente", "json", frm, function (data) {
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
    listarPacientes();
}