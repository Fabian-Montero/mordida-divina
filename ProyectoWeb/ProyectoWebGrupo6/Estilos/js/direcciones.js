
$(document).ready(function () {

    $.ajax({
        dataType: "json",
        url: "https://ubicaciones.paginasweb.cr/provincias.json",
        data: {},
        success: function (data) {
            var html = "<option selected='selected' value=''>Seleccione una provincia</option>";
            for (key in data) {
                html += "<option value='" + key + "'>" + data[key] + "</option>";   
            }
            
            $('#provincias').html(html);
            
        }
    });

    $("#provincias").change(function () {
        cambiarValorProvincia($("#provincias option:selected").text());

        console.log($("#provincias option:selected").text());
        console.log($("#provincias").val());
        cargarCantones($("#provincias").val());
        limpiarDistritos();
    });
    $("#cantones").change(function () {
        cambiarValorCanton($("#cantones option:selected").text());

        console.log($("#cantones option:selected").text());
        console.log($("#cantones").val());
        cargarDistritos($("#cantones").val(), $("#provincias").val());

    });

    $("#distritos").change(function () {
        cambiarValorDistrito($("#distritos option:selected").text());
        console.log($("#distritos option:selected").text());
        console.log($("#distritos").val());

    });



});
    function cambiarValorProvincia(val)
    {
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
function cargarCantones(val) {
    console.log("Va a buscar los cantones del la provincia con el id " + val);
    $.ajax({
        dataType: "json",
        url: "https://ubicaciones.paginasweb.cr/provincia/" + val + "/cantones.json",
        data: {},
        success: function (data) {
            var html = "<option selected='selected' value=''>Seleccione un cantón</option>";
            
            for (key in data) {
                html += "<option value='" + key + "'>" + data[key] + "</option>";
            }
            
            $('#cantones').html(html);
            
        }
    });
}

    function cargarDistritos(cantonId, provinciaId) {
        console.log("Va a buscar los distritos del canton con el id " + cantonId + " y la provincia " + provinciaId);
        $.ajax({
            dataType: "json",
            url: "https://ubicaciones.paginasweb.cr/provincia/" + provinciaId + "/canton/" + cantonId + "/distritos.json",
            data: {},
            success: function (data) {
                var html = "<option selected='selected' value=''>Seleccione un distrito</option>";
                for (key in data) {
                    html += "<option value='" + key + "'>" + data[key] + "</option>";
                }

                $('#distritos').html(html);
            }
        });

    }
    
