$(document).ready(function () {
    debugger;
    var IdProducto = $('#IdProducto').val();
    if (IdProducto != 0) {
        fn_ObtenerPorIdProducto(IdProducto);
    }
    $("#btnGrabar").click(function () {
        fn_RegistrarProducto();
    });

});

//Permite Registrar Retoque
function fn_RegistrarProducto() {
    //Variables
    var IdProducto = parseInt($("#IdProducto").val());
    var CodigoProducto = $("#txtCodigoProducto").val();
    var CodigoBarra = $("#txtCodigoBarra").val();
    var DescripcionProducto = $("#txtDescripcion").val();
    

    //VAlidaciones
    //if ($.trim(Nombre) == 0) {
    //    parent.fn_util_MuestraMensaje("Alerta", "Ingrese nombre", "W");
    //    return false;
    //}
    //if ($.trim(Descripcion) == 0) {
    //    parent.fn_util_MuestraMensaje("Alerta", "Ingrese descripción", "W");
    //    return false;
    //}



    parent.$("#btnGrabarModal").button('reset');

    var url = $("#Url_RegistrarProducto").val();
    $.ajax({
        type: 'POST',
        url: url,
        //async: true,
        data: { "IdProducto": IdProducto, "CodigoProducto": CodigoProducto, "CodigoBarra": CodigoBarra, "DescripcionProducto": DescripcionProducto },
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

                    parent.fn_ListarProducto();

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


function fn_ObtenerPorIdProducto(IdProducto) {

    var url = $("#Url_ObtenerProductoPorId").val();
    $.ajax({
        type: 'POST',
        url: url,
        async: true,
        data: { "IdProducto": IdProducto },
        datatype: 'JSON',
        success: function (Result) {
            if (Result.iTipoResultado == 1) {
                $("#txtCodigoProducto").val(Result.Producto.CodigoProducto);
                $("#txtCodigoBarra").val(Result.Producto.CodigoBarra);
                $("#txtDescripcion").val(Result.Producto.DescripcionProducto);
   

            } else {
                parent.fn_util_MuestraMensaje("ERROR.", Result.vMensaje, "E");
            }
        }
    });

}