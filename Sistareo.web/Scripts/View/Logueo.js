$(document).ready(function () {
    var session = $("#hddSession").val();
    
    var sMensaje = $("#hddMensaje").val();

    
    if (session == 1) {
        fn_util_MuestraMensaje("ALERTA:", "Su sesión a caducado, vuelva a ingresar al sistema.", "W");
    }

    if ($.trim(sMensaje).length != 0) {
       fn_util_MuestraMensaje("ALERTA:", sMensaje, "W");
    }

    
    //$("#txtIngresar").click(function () {
        
    //    var Usuario = $.trim($("#txtUsuario").val());
    //    var pwd = $.trim($("#txtpwd").val());
    

    //    if (Usuario.length < 3) {
    //        fn_util_MuestraMensaje("ALERTA:", "3 caracteres como mínimo para el codigo de usuario.", "W");
    //        return false;
    //    }

    //    if (pwd.length < 3) {
    //        fn_util_MuestraMensaje("ALERTA:", "3 caracteres como mínimo para la contraseña.", "W");
    //        return false;
    //    }

    //});

   

});


function fn_util_MuestraMensaje(pTitulo, pMensaje, pTipo) {
    if (pTipo == "E") {
        $("#dvAlertaERROR_titulo").html(pTitulo);
        $("#dvAlertaERROR_msg").html(pMensaje);
        $("#dvAlertaERROR").show();
        $("#dvAlertaERROR").delay(6000).fadeOut(300);
    }
    if (pTipo == "W") {
        $("#dvAlertaWARN_titulo").html(pTitulo);
        $("#dvAlertaWARN_msg").html(pMensaje);
        $("#dvAlertaWARN").show();
        $("#dvAlertaWARN").delay(6000).fadeOut(300);
    }
    if (pTipo == "OK") {
        $("#dvAlertaOK_titulo").html(pTitulo);
        $("#dvAlertaOK_msg").html(pMensaje);
        $("#dvAlertaOK").show();
        $("#dvAlertaOK").delay(6000).fadeOut(300);
    }
}