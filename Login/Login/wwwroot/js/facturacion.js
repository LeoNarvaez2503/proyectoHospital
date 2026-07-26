window.onload = function () {
    listarFacturacion();
}
async function listarFacturacion() {
    pintar({
        url: "Facturacion/ListarFacturaciones",
        cabeceras: ["ID Facturación", "Paciente", "Monto", "Método Pago", "Fecha Pago"],
        propiedades: ["id", "nombrePaciente", "monto", "metodoPago", "fechaPago"],
        editar: true,
        eliminar: true,
        propiedadId: "id"
    });
}

async function guardar() {
    let form = document.getElementById("frmOperaciones");
    let frm = new FormData(form);
    confirmacion(undefined, undefined, function (resp) {
        fetchPost("Facturacion/GuardarFacturacion", "json", frm, function (res) {
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
        fetchGet("Facturacion/RecuperarFacturacion/?id=" + id, "json", function (data) {
            setN("id", data.id);
            cargarForaneas("Pacientes", "pacienteId");
            setN("pacienteId", data.pacienteId);
            setN("monto", data.monto);
            setN("metodoPago", data.metodoPago);
            setN("fechaPago", data.fechaPago);
            
        });
    }
    else {
        limpiarForm();
        cargarForaneas("Pacientes", "pacienteId");
        setN("id", id);
    }

}

function Eliminar(id) {
    fetchGet("Facturacion/EliminarFacturacion/?id=" + id, "json", function (data) {
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
    listarFacturacion();
}