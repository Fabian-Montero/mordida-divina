
function ValidarNumeros(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode >= 48 && charCode <= 57) {
        return true;
    }
    return false;
}

function AgregarCarrito(id) {
    let cantProducto = $("#prd-" + id).val();

    if (cantProducto <= 0) {
        MostrarAlerta("info", "Debe ingresar una cantidad válida")
        return;
    }

    $.ajax({
        url: '/Carrito/AgregarCarrito',
        type: "POST",
        data: {
            "idProducto": id,
            "cantProducto": cantProducto
        },
        success: function (data) {
            if (data === "OK") {
                alert("Correcto")
            } else {
                alert(data);
            }
        }
    });
}

function MostrarAlerta(icono, mensaje) {
    Swal.fire({
        position: "center",
        icon: icono,
        title: mensaje,
        showConfirmButton: false,
        timer: 2500
    });
}