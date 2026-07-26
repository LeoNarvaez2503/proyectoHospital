window.onload = function () {
    listarTratamientos();
}
async function listarTratamientos() {
    pintar({
        url: "Tratamientos/ListarTratamientos",
        cabeceras: ["ID Tratamiento", "Paciente", "Descripción", "Fecha", "Costo"],
        propiedades: ["id", "nombrePaciente", "descripcion", "fecha", "costo"],
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
        setN("id", id);
    }

}

function Eliminar(id) {
    fetchGet("Tratamientos/EliminarTratamiento/?id=" + id, "json", function (data) {
        confirmacion(undefined, "¿Seguro desea eliminar?", function (resp) {
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