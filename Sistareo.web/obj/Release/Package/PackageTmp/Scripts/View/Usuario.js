
$(document).ready(function () {

    var IdUsuario = $("#hhdIdUsuario").val();


    //Carga Roles
    fn_ListarRoles();

    if (IdUsuario != 0) {
        fn_ObtenerUsuario(IdUsuario)
    }

    $("#txtDNI").fn_util_validarNumeros();


 

    $("#btnGrabar").click(function () {
        fn_RegistrarUsuario();
    });

});



//Permite obtener la lista de los operadores
function fn_ListarRoles() {
    var url = $("#Url_ListarTodoRol").val();

    debugger;
    $.ajax({
        type: "GET",
        url: url,
        async: true,
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


function fn_RegistrarUsuario() {


    var IdUsuario = parseInt($("#hhdIdUsuario").val());
    var Nombres = $("#txtNombres").val();
    var ApellidoPaterno = $("#txtApePaterno").val();
    var ApellidoMaterno = $('#txtApeMaterno').val();
    var IdRol = $('#cboRoles').val();
    var DNI = $("#txtDNI").val();
    var NombreUsuario = $('#txtUsuario').val();
    var Clave = $('#txtClave').val();


    if ($.trim(Nombres) == "") {
        parent.fn_util_MuestraMensaje("Alerta", "Ingrese un Nombre", "W");
        return false;
    }
    if ($.trim(ApellidoPaterno) == "") {
        parent.fn_util_MuestraMensaje("Alerta", "Ingrese Apellido Paterno", "W");
        return false;
    }
    if ($.trim(ApellidoMaterno) == "") {
        parent.fn_util_MuestraMensaje("Alerta", "Ingrese Apellido Materno", "W");
        return false;
    }
    if ($.trim(IdRol) == 0) {
        parent.fn_util_MuestraMensaje("Alerta", "Seleccione un Rol", "W");
        return false;
    }
    if ($.trim(DNI) == "") {
        parent.fn_util_MuestraMensaje("Alerta", "Ingrese DNI", "W");
        return false;
    }
    if ($.trim(NombreUsuario) == "") {
        parent.fn_util_MuestraMensaje("Alerta", "Ingrese usuario", "W");
        return false;
    }
    if ($.trim(Clave) == "") {
        parent.fn_util_MuestraMensaje("Alerta", "Ingrese constraseña", "W");
        return false;
    }



    var Usuario = $("#hhdIdUsuario").val();
    //Bloquear Pantalla
    if (Usuario == 0) {
        parent.fn_util_bloquearPantalla("Modificando usuario");
    } else {
        parent.fn_util_bloquearPantalla("Registrando usuario");
    }

  

    var url = $("#Url_RegistrarUsuario").val();
    $.ajax({
        type: 'POST',
        url: url,
        //async: true,
        data: {
            "IdUsuario": IdUsuario, "Nombres": Nombres, "ApellidoPaterno": ApellidoPaterno, "ApellidoMaterno": ApellidoMaterno, "DNI": DNI
            , "IdRol": IdRol, "NombreUsuario": NombreUsuario, "Clave": Clave
        },
        datatype: 'JSON',

        success: function (result) {

            setTimeout(function () {

            if (result.iTipoResultado == 1) {
                parent.fn_util_MuestraMensaje("Exito", "Se registró correctamente al usuario", "OK");
                parent.fn_ListarUsuarios();
                parent.fn_util_desbloquearPantalla();

                //Cierra Modal
                parent.$('#mdlNuevo').modal('hide');
            }
            else if (result.iTipoResultado == 2) {
                parent.fn_util_MuestraMensaje("ERROR.", "Ocurrio un problema al intentar registrar al usuario", "E");
                parent.fn_util_desbloquearPantalla();
            }
            else if (result.iTipoResultado == 3) {
                parent.fn_util_MuestraMensaje("Exito", "Se modificó correctamente al usuario.", "OK");
                parent.fn_ListarUsuarios();
                parent.fn_util_desbloquearPantalla();

                //Cierra Modal
                parent.$('#mdlNuevo').modal('hide');
            }

            }, 1000);
        },
        error: function (data) {
            fn_util_MuestraMensaje("ERROR.", "No se pudo realizar la acción. Por favor, inténtelo mas tarde.", "E");
            parent.fn_util_desbloquearPantalla();
        }
    });







}



function fn_ObtenerUsuario(IdUsuario) {

    //Bloquear Pantalla
    parent.fn_util_bloquearPantalla();


    //Grabado (Ajax)
    var url = $("#Url_ObtenerUsuario").val();
    $.ajax({
        type: 'POST',
        url: url,
        async: true,
        data: { "IdUsuario": IdUsuario },
        datatype: 'JSON',

        success: function (Result) {
            if (Result.iTipoResultado == 1) {
                var Usuario = Result.Usuario;

      
                 $("#txtNombres").val(Usuario.Nombres);
                 $("#txtApePaterno").val(Usuario.ApellidoPaterno);
                 $('#txtApeMaterno').val(Usuario.ApellidoMaterno);
                 $('#cboRoles').val(Usuario.IdRol);
                 $("#txtDNI").val(Usuario.DNI);
                 $('#txtUsuario').val(Usuario.NombreUsuario);
                 $('#txtClave').val(Usuario.Clave);

                //Desbloquear Pantalla
                parent.fn_util_desbloquearPantalla();

            } else {
                parent.fn_util_MuestraMensaje("ERROR.", Result.vError, "E");

                //Desbloquear Pantalla
                parent.fn_util_desbloquearPantalla();
            }
        },
        error: function () {
            fn_util_MuestraMensaje("ERROR.", "No se pudo Obtener el detalle del usuario. Por favor, inténtelo mas tarde.", "E");
            parent.fn_util_desbloquearPantalla();
        }
    });


}