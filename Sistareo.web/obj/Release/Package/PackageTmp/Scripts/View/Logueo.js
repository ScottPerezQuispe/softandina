$(document).ready(function () {
    var session = $("#hddSession").val();
    
    var sMensaje = $("#hddMensaje").val();

    
    if (session == 1) {
        parent.fn_util_MuestraMensaje("ALERTA:", "Su sesión a caducado, vuelva a ingresar al sistema.", "W");
    }

    if ($.trim(sMensaje).length != 0) {
        parent.fn_util_MuestraMensaje("ALERTA:", sMensaje, "W");
    }

    
    $("#txtIngresar").click(function () {
        
        var Usuario = $.trim($("#txtUsuario").val());
        var pwd = $.trim($("#txtpwd").val());
    

        if (Usuario.length < 3) {
            parent.fn_util_MuestraMensaje("ALERTA:", "3 caracteres como mínimo para el codigo de usuario.", "W");
            return false;
        }

        if (pwd.length < 3) {
            parent.fn_util_MuestraMensaje("ALERTA:", "3 caracteres como mínimo para la contraseña.", "W");
            return false;
        }

    });

   

});