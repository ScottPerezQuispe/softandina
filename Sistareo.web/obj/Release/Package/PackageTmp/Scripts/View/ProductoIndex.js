$(document).ready(function () {


    //******************************************
    //* Clear iFRAME
    //******************************************
    $("#btnCancelarModal").click(function () {
        $("#ifrModal").contents().find("body").html('');

    });


    //****************************
    //btnNuevo
    //****************************
    $("#btnNuevo").click(function () {

        fn_abreNuevo(0);

    });

    //****************************
    //btnEditar
    //****************************
    $("#btnEditar").click(function () {
        fn_abreNuevo(0);
    });


    //Listar Usuario
    fn_ListarProducto()


    //**************************************
    // btnGrabarModal
    //**************************************
    $("#btnGrabarModal").click(function () {

        var objBotonGrabar = $('#ifrModal').contents().find('#btnGrabar');
        objBotonGrabar.trigger("click");

    });


    //Buscar Fecha por Operador
    $("#btnBuscar").click(function () {
        fn_ListarProducto();
    });
});


function fn_ListarProducto() {


    var url = $("#Url_ListarProducto").val();
    //Filtro
    var Nombres = $("#txtProducto").val();


    //Bloquear Pantalla
    fn_util_bloquearPantalla();

    //Grabado (Ajax)


    $.ajax({
        type: 'POST',
        url: url,
        async: true,
        data: { "Nombres": Nombres },
        datatype: 'JSON',

        success: function (result) {

            if (result.iTipoResultado == 1) {
                //Muestra Listado
                setTimeout(function () {
                    var htmlListaUsuario = "";
                    var ListaProducto = result.ListaProducto;



                    $.each(ListaProducto, function (index, items) {
                        htmlListaUsuario = htmlListaUsuario + '<tr id="' + items.IdProducto + '" style="cursor: pointer;">' +
                            '<td>' + items.CodigoProducto + '</td>' +
                            '<td>' + items.DescripcionProducto + '</td>' +
                           '<td style="text-align: center;"> <a id="btnEditar_' + items.IdProducto + '"   class="glyphicon glyphicon-pencil" onclick="javascript:fn_abreNuevo(\'' + items.IdProducto + '\')"></a>' +
                        '<a id="btnEliminarUsuario_' + items.IdProducto + '"  class="glyphicon glyphicon-remove" onclick="javascript:fn_EliminarProducto(\'' + items.IdProducto + '\')" </a></td>' +
                        '</tr>';
                    });
                    ;
                    $("#tbContenedor").html(htmlListaUsuario);

                    if (ListaProducto.length == 0) {
                        $("#tbContenedor").html("<tr id='0'><td colspan='5' style='text-align:center;'>No existen Registros.</td></tr>");
                    }
                }, 500);


            } else {
                fn_util_MuestraMensaje("ERROR.", result.vError, "E");
            }

            //Desbloquear Pantalla
            setTimeout(function () { fn_util_desbloquearPantalla(); }, 1000);
        },
        error: function (data) {

            //Desbloquear Pantalla
            parent.fn_util_desbloquearPantalla();
            parent.fn_util_MuestraMensaje("ERROR.", "Ocurrió un error", "E");
        }
    });

}

//***********************************
//fn_abreNuevo
//***********************************
function fn_abreNuevo(IdProducto) {




    $("#ifrModal").attr("src", "/Configuracion/NuevoProducto?IdProducto=" + IdProducto);
    $('#btnGrabarModal').show();
    $("#mdlNuevoDialog").css("width", "600px");
    $('#btnCancelarModal').html("Cancelar");
    $("#ifrModal").css("height", "300px");
    $("#ifrModal").css("width", "550px");

    //Mostrar Modal
    $('#mdlNuevo').modal('show');
}


//**************************************
//*Boton Eliminar Producto
//**************************************

function fn_EliminarProducto(IdProducto) {
    //Confirmación
    window.parent.$.confirm({
        title: "CONFIRMACIÓN",
        text: "¿Está seguro que desea eliminar? ",
        confirm: function (button) {

            //Grabado (Ajax)
            parent.fn_util_bloquearPantalla();


            var url = $("#Url_EliminarProducto").val();
            $.ajax({
                type: 'POST',
                url: url,
                async: true,
                data: { "IdProducto": IdProducto },
                datatype: 'JSON',

                success: function (Result) {

                    setTimeout(function () {
                        if (Result.iTipoResultado) {

                            parent.fn_util_desbloquearPantalla();
                            parent.fn_util_MuestraMensaje("", Result.vMensaje, "OK");
                            //Muestra Listado
                            fn_ListarProducto();



                        } else {
                            parent.fn_util_MuestraMensaje("Error", Result.vMensaje, "E");
                            parent.fn_util_desbloquearPantalla();
                        }
                    }, 1000);
                }
            });

        },
        cancel: function (button) { },
        confirmButton: "Aceptar",
        cancelButton: "Cancelar"
    })
}