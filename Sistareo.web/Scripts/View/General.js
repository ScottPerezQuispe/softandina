
$(document).ready(function () {
    fn_ListarRoles();
    fn_ListarTipoUsuario();
});


//Permite obtener la lista de los operadores
function fn_ListarRoles() {
    var url = $("#Url_ListarTodoRol").val();


    $.ajax({
        type: "GET",
        url: url,
        //async: true,
        dataType: "json",
        cache: false,
        success: function (Result) {
            if (Result.iTipoResultado == 1) {
                var ListaRol = Result.ListaRol
                $.each(ListaRol, function (index, item) {

                    $("#cboRoles").append("<option value=" + this.IdRol + ">" + this.Nombre + "</option>");
                });
            }
        },

        error: function () {
            parent.fn_util_MuestraMensaje("ERROR.", "No se listar. Por favor, inténtelo mas tarde.", "E");
            parent.fn_util_desbloquearPantalla();
        }
    });
}


//Permite obtener la lista de los operadores
function fn_ListarTipoUsuario() {
    var url = $("#Url_ListarTodoTipoUsuario").val();


    $.ajax({
        type: "GET",
        url: url,
        //async: true,
        dataType: "json",
        cache: false,
        success: function (Result) {
            if (Result.iTipoResultado == 1) {
                var ListaTipoUsuario = Result.ListaTipoUsuario
                $.each(ListaTipoUsuario, function (index, item) {

                    $("#cboTipoUsuario").append("<option value=" + this.IdTipo + ">" + this.Nombre + "</option>");
                });
            }
        },

        error: function () {
            parent.fn_util_MuestraMensaje("ERROR.", "No se listar. Por favor, inténtelo mas tarde.", "E");
            parent.fn_util_desbloquearPantalla();
        }
    });
}