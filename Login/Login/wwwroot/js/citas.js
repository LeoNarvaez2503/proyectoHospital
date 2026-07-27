window.onload = function () {
    listarCitas();
}

async function listarCitas() {
    pintar({
        url: "Citas/ListarCitas",
        cabeceras: ["ID Cita", "Paciente", "Medico", "Fecha", "Estado"],
        propiedades: ["idCita", "pacienteNombre", "medicoNombre", "fecha", "estado"],
        editar: true,
        eliminar: true,
        propiedadId: "idCita"
    });
}

async function guardar() {
    let form = document.getElementById("frmOperaciones");
    let frm = new FormData(form);
    confirmacion(undefined, undefined, function (resp) {
        fetchPost("Citas/guardarCita", "json", frm, function (res) {
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
        fetchGet("Citas/recuperarCitas/?id=" + id, "json", function (data) {
            setN("idCita", data.idCita);
            cargarForaneas("Pacientes", "idPaciente");
            setN("idPaciente", data.idPaciente);
            cargarForaneas("Medicos", "idMedico");
            setN("idMedico", data.idMedico);
            setN("fecha", data.fecha);
            setN("estado", data.estado);
        });
    }
    else {
        limpiarForm();
        cargarForaneas("Pacientes", "idPaciente");
        cargarForaneas("Medicos", "idMedico");
        setN("idCita", id);
    }

}

function Eliminar(id) {
    fetchGet("Citas/EliminarCita/?id=" + id, "json", function (data) {
        confirmacion(undefined, "¿Seguro desea eliminar?", function (resp) {
            limpiarForm();
            Exito();
        });
    });
}

function limpiarForm() {
    limpiarDatos("frmOperaciones");
    listarCitas();
}
