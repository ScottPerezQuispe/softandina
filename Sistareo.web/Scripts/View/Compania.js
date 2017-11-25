$(document).ready(function () {



    var IdCampania = $('#IdCampania').val();
    if (IdCampania != 0) {
        fn_ObtenerPorIdCampania(IdCampania);
    }
    $("#btnGrabar").click(function () {
        fn_RegistrarRetoque();
    });

});

//Permite Registrar Retoque
function fn_RegistrarRetoque() {
    //Variables
    var IdCampania = parseInt($("#IdCampania").val());
    var Nombre = $("#txtNombre").val();
    var Descripcion = $("#txtDescripcion").val();


    //VAlidaciones
    if ($.trim(Nombre) == 0) {
        parent.fn_util_MuestraMensaje("Alerta", "Ingrese nombre", "W");
        return false;
    }
    if ($.trim(Descripcion) == 0) {
        parent.fn_util_MuestraMensaje("Alerta", "Ingrese descripción", "W");
        return false;
    }
  


    parent.$("#btnGrabarModal").button('reset');

    var url = $("#Url_RegistrarCampania").val();
    $.ajax({
        type: 'POST',
        url: url,
        //async: true,
        data: { "IdCampania": IdCampania, "Nombre": Nombre, "Descripcion": Descripcion },
        datatype: 'JSON',
        //contentType: "application/json; charset=utf-8",
        beforeSend: function (xhr) {
            parent.fn_util_bloquearPantalla();
        },
        success: function (Result) {
            //Muestra Listado
            setTimeout(function () {
                if (Result.iTipoResultado) {

                    parent.fn_util_desbloquearPantalla();
                    parent.fn_util_MuestraMensaje("", Result.vMensaje, "OK");

                    parent.fn_ListarCampania();

                    //Cierra Modal
                    parent.$('#mdlNuevo').modal('hide');

                } else {
                    parent.fn_util_MuestraMensaje("Error", Result.vMensaje, "E");
                    parent.fn_util_desbloquearPantalla();
                }
            }, 1000);
        }
    });

}


function fn_ObtenerPorIdCampania(IdCampania) {

    var url = $("#Url_ObtenerCampania").val();
    $.ajax({
        type: 'POST',
        url: url,
        async: true,
        data: { "IdCampania": IdCampania },
        datatype: 'JSON',
        success: function (Result) {
            if (Result.iTipoResultado == 1) {
                $("#txtNombre").val(Result.Campania.Nombre);
                $("#txtDescripcion").val(Result.Campania.Descripcion);

            } else {
                parent.fn_util_MuestraMensaje("ERROR.", Result.vMensaje, "E");
            }
        }
    });

}