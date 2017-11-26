$(document).ready(function () {


    var vFechaActual = $("#FechaApertura").val();
    $('#txtFechaApertura').val(vFechaActual);
  
    var IdRetoque = $('#IdRetoque').val();
    if (IdRetoque!=0) {
        fn_ObtenerPorIdRetoque(IdRetoque);
    }
    $("#btnGrabar").click(function () {
        fn_RegistrarRetoque();
    });


    $('#FechaIni').datetimepicker({
        locale: 'es',
        format: 'DD/MM/YYYY'
        //defaultDate: n
    });

});

//Permite Registrar Retoque
function fn_RegistrarRetoque() {
    //Variables
    var IdRetoque = parseInt($("#IdRetoque").val());
    var IdOperario = $("#cboOperario").val();
    var IdCampania = $("#cboCampania").val();
    var Jefatura = $("#txtJefatura").val();
    var Coordinador = $("#txtCoordinador").val();
    var FechaApertura = $("#txtFechaApertura").val();
    
    //VAlidaciones
    if ($.trim(IdOperario)==0) {
        parent.fn_util_MuestraMensaje("Alerta", "Ingrese un Operario", "W");
        return false;
    }
    if ($.trim(IdCampania) == 0) {
        parent.fn_util_MuestraMensaje("Alerta", "Ingrese campaña", "W");
        return false;
    }
    if ($.trim(Jefatura) == "") {
        parent.fn_util_MuestraMensaje("Alerta", "Ingrese Jefatura", "W");
        return false;
    }
    if ($.trim(Coordinador) == "") {
        parent.fn_util_MuestraMensaje("Alerta", "Ingrese Coordinador", "W");
        return false;
    }


    parent.$("#btnGrabarModal").button('reset');

    var url = $("#Url_RegistrarRetoque").val();
    $.ajax({
        type: 'POST',
        url: url,
        //async: true,
        data: { "IdRetoque": IdRetoque, "IdOperario": IdOperario, "IdCampania": IdCampania, "Jefatura": Jefatura, "Coordinador": Coordinador, "FechaApertura": FechaApertura },
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
                    
                        parent.fn_ListarFechaPorOperario();

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

function fn_EliminarRetoque() {
    //Variables
    var IdRetoque = parseInt($("#IdRetoque").val());
    var url = $("#Url_EliminarRetoque").val();
    $.ajax({
        type: 'POST',
        url: url,
        async: true,
        data: { "IdRetoque": IdRetoque},
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

                    parent.fn_ListarFechaPorOperario();

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
function fn_ObtenerPorIdRetoque(IdRetoque) {

    var url = $("#Url_ObtenerPorIdRetoque").val();
    $.ajax({
        type: 'POST',
        url: url,
        async: true,
        data: { "IdRetoque": IdRetoque},
        datatype: 'JSON',
        //contentType: "application/json; charset=utf-8",
        //beforeSend: function (xhr) {

        //},
        success: function (Result) {
            if (Result.iTipoResultado == 1) {
                $("#cboOperario").val(Result.Retoque.IdOperario);
                $("#cboCampania").val(Result.Retoque.IdCampania);
                
                $("#txtJefatura").val(Result.Retoque.Jefatura);
                $("#txtCoordinador").val(Result.Retoque.Coordinador);
                $("#txtFechaApertura").val(Result.Retoque.vFechaApertura);

            } else {
                parent.fn_util_MuestraMensaje("ERROR.", Result.vMensaje, "E");
            }
        }
    });

}


