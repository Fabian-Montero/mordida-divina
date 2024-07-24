
$(document).ready(function () {
    var provinciaSeleccionada = document.getElementById('Provincia').value;
    var cantonSeleccionado = document.getElementById('Canton').value;
    var distritoSeleccionado = document.getElementById('Distrito').value;
    

    $.ajax({
        dataType: "json",
        url: "https://ubicaciones.paginasweb.cr/provincias.json",
        data: {},
        success: function (data) {
            var html = "";
            for (key in data) {
                if (data[key] === provinciaSeleccionada) {
                    html += "<option value='" + key + "' selected>" + data[key] + "</option>";
                    cargarCantones(key, cantonSeleccionado, distritoSeleccionado);
                } else {
                    html += "<option value='" + key + "'>" + data[key] + "</option>";
                }
            }

            $('#provincias').html(html);

        }
    });

    $("#provincias").change(function () {
        cambiarValorProvincia($("#provincias option:selected").text());
        cargarCantones($("#provincias").val());
        limpiarDistritos();
    });
    $("#cantones").change(function () {
        cambiarValorCanton($("#cantones option:selected").text());
        cargarDistritos($("#cantones").val(), $("#provincias").val());
    });

    $("#distritos").change(function () {
        cambiarValorDistrito($("#distritos option:selected").text());
    });

});
function cambiarValorProvincia(val) {
    $("#Provincia").val(val);
}
function cambiarValorCanton(val) {
    $("#Canton").val(val);
}
function cambiarValorDistrito(val) {
    $("#Distrito").val(val);
}
function limpiarDistritos() {

    var predeterminada = '<option value="">Seleccione una provincia y cantón</option>';
    $("#distritos").find("option").remove();
    $("#distritos").append(predeterminada);
}
function cargarCantones(provinciaId, cantonSeleccionado, distritoSeleccionado) {
    console.log("Va a buscar los cantones del la provincia con el id " + provinciaId);
    $.ajax({
        dataType: "json",
        url: "https://ubicaciones.paginasweb.cr/provincia/" + provinciaId + "/cantones.json",
        data: {},
        success: function (data) {
            var html = "<option selected='selected' value=''>Seleccione un cantón</option>";
            
            for (key in data) {
                //html += "<option value='" + key + "'>" + data[key] + "</option>";
                if (data[key] === cantonSeleccionado) {
                    html += "<option value='" + key + "' selected>" + data[key] + "</option>";
                    cargarDistritos(key, provinciaId, distritoSeleccionado);
                } else {
                    html += "<option value='" + key + "'>" + data[key] + "</option>";
                }
            }

            $('#cantones').html(html);

        }
    });
}

function cargarDistritos(cantonId,provinciaId,distritoSeleccionado) {
    
    console.log("Va a buscar los distritos del canton con el id " + cantonId + " y la provincia " + provinciaId);
    $.ajax({
        dataType: "json",
        url: "https://ubicaciones.paginasweb.cr/provincia/" + provinciaId + "/canton/" + cantonId + "/distritos.json",
        data: {},
        success: function (data) {
            var html = "<option selected='selected' value=''>Seleccione un distrito</option>";
            
            for (key in data) {
                //html += "<option value='" + key + "'>" + data[key] + "</option>";
                if (data[key] === distritoSeleccionado) {
                    html += "<option value='" + key + "' selected>" + data[key] + "</option>";
                } else {
                    html += "<option value='" + key + "'>" + data[key] + "</option>";
                }
            }

            $('#distritos').html(html);
        }
    });

}

