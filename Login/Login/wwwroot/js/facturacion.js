window.onload = function () {
    listarFacturacion();
}
async function listarFacturacion() {
    pintar({
        url: "Facturacion/ListarFacturaciones",
        cabeceras: ["ID Facturacion", "Paciente", "Monto", "Metodo Pago", "Fecha Pago"],
        propiedades: ["id", "pacienteNombre", "monto", "metodoPago", "fechaPago"],
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
    confirmacion(undefined, "¿Seguro desea eliminar?", function (resp) {
        let tokenElement = document.getElementsByName("__RequestVerificationToken")[0];
        let token = tokenElement ? tokenElement.value : "";
        let frm = new FormData();
        frm.append("id", id);
        frm.append("__RequestVerificationToken", token);

        fetchPost("Facturacion/EliminarFacturacion", "json", frm, function (data) {
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
