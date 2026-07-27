window.onload = function () {
    listarTratamientos();
}
async function listarTratamientos() {
    pintar({
        url: "Tratamientos/ListarTratamientos",
        cabeceras: ["ID Tratamiento", "Paciente", "Descripcion", "fecha", "costo"],
        propiedades: ["id", "pacienteNombre", "descripcion", "fecha", "costo"],
        editar: true,
        eliminar: true,
        propiedadId: "id"
    });
}

async function guardar() {
    let form = document.getElementById("frmOperaciones");
    let frm = new FormData(form);
    confirmacion(undefined, "¿Seguro desea actualizar?", function (resp) {
        fetchPost("Tratamientos/GuardarTratamiento", "json", frm, function (res) {
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
        fetchGet("Tratamientos/RecuperarTratamiento/?id=" + id, "json", function (data) {
            setN("id", data.id);
            cargarForaneas("Pacientes", "pacienteId");
            setN("pacienteId", data.pacienteId);
            setN("descripcion", data.descripcion);
            setN("fecha", data.fecha);
            setN("costo", data.costo);

        });
    }
    else {
        limpiarForm();
        cargarForaneas("Pacientes", "pacienteId");
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

        fetchPost("Tratamientos/EliminarTratamiento", "json", frm, function (data) {
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
    listarTratamientos();
}
