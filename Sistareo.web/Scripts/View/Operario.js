$(document).ready(function () {
    ObtenerOperador();

    ObtenerCampania();
});

//Permite obtener la lista de los operadores s
function ObtenerOperador() {
    //Bloquear Pantalla
    parent.fn_util_bloquearPantalla();

    var url = $("#Url_ListarTodoOperador").val();
    var Cuenta = $('#cboOperario');

    $.ajax({
        type: "GET",
        url: url,
        async: true,
        dataType: "json",
        cache: false,
        success: function (Result) {

            //setTimeout(function () {
            if (Result.iTipoResultado == 1) {
                var lstLista = Result.ListaOperador
                $.each(lstLista, function (index, item) {
                    $("#cboOperario").append("<option value=" + this.IdOperario + ">" + this.NombreCompleto + "</option>");
                });
            } else {
                parent.fn_util_MuestraMensaje("Error", Result.Mensaje, "E");
                parent.fn_util_desbloquearPantalla();
            }
            //}, 1000);
        },
        beforeSend: function (xhr) {
            parent.fn_util_bloquearPantalla();
        },
        complete: function () {
            parent.fn_util_desbloquearPantalla();
        }
    });
}


//Permite obtener la lista de los operadores s
function ObtenerCampania() {
    //Bloquear Pantalla
    parent.fn_util_bloquearPantalla();

    var url = $("#Url_ListarTodoCampania").val();
    var Cuenta = $('#cboCampania');

    $.ajax({
        type: "GET",
        url: url,
        async: true,
        dataType: "json",
        cache: false,
        success: function (Result) {

            //setTimeout(function () {
            if (Result.iTipoResultado == 1) {
                var lstLista = Result.ListaCampania
                $.each(lstLista, function (index, item) {
                    $("#cboCampania").append("<option value=" + this.IdCampania + ">" + this.Nombre + "</option>");
                });
            } else {
                parent.fn_util_MuestraMensaje("Error", Result.Mensaje, "E");
                parent.fn_util_desbloquearPantalla();
            }
            //}, 1000);
        },
        beforeSend: function (xhr) {
            parent.fn_util_bloquearPantalla();
        },
        complete: function () {
            parent.fn_util_desbloquearPantalla();
        }
    });
}


