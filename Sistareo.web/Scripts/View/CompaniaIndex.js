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
    fn_ListarCampania()


    //**************************************
    // btnGrabarModal
    //**************************************
    $("#btnGrabarModal").click(function () {

        var objBotonGrabar = $('#ifrModal').contents().find('#btnGrabar');
        objBotonGrabar.trigger("click");

    });


    //Buscar Fecha por Operador
    $("#btnBuscar").click(function () {
        fn_ListarCampania();
    });
});


function fn_ListarCampania() {


    var url = $("#Url_ListarCapania").val();
    //Filtro
    var Nombres = $("#txtCampania").val();
   

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
                    var ListaCampania = result.ListaCampania;



                    $.each(ListaCampania, function (index, items) {
                        htmlListaUsuario = htmlListaUsuario + '<tr id="' + items.IdCampania + '" style="cursor: pointer;">' +
                            '<td>' + items.Nombre + '</td>' +
                            '<td>' + items.Descripcion + '</td>' +
                           '<td style="text-align: center;"> <a id="btnEditar_' + items.IdCampania + '"   class="glyphicon glyphicon-pencil" onclick="javascript:fn_abreNuevo(\'' + items.IdCampania + '\')"></a>' +
                        '<a id="btnEliminarUsuario_' + items.IdCampania + '"  class="glyphicon glyphicon-remove" onclick="javascript:fn_EliminarCampania(\'' + items.IdCampania + '\')" </a></td>' +
                        '</tr>';
                    });
                    ;
                    $("#tbContenedor").html(htmlListaUsuario);

                    if (ListaCampania.length == 0) {
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
function fn_abreNuevo(IdCampania) {



    if (IdCampania == 0) {
        $("#btnEliminar").hide();

    } else {
        $("#btnEliminar").show();
    }

    $("#ifrModal").attr("src", "/Configuracion/NuevoCampania?IdCampania=" + IdCampania);
    $('#btnGrabarModal').show();
    $("#mdlNuevoDialog").css("width", "600px");
    $('#btnCancelarModal').html("Cancelar");
    $("#ifrModal").css("height", "300px");
    $("#ifrModal").css("width", "550px");

    //Mostrar Modal
    $('#mdlNuevo').modal('show');
}


//**************************************
//*Boton Eliminar Campaña
//**************************************

function fn_EliminarCampania(IdCampania) {
    //Confirmación
    window.parent.$.confirm({
        title: "CONFIRMACIÓN",
        text: "¿Está seguro que desea eliminar? ",
        confirm: function (button) {

            //Grabado (Ajax)
            parent.fn_util_bloquearPantalla();


            var url = $("#Url_EliminarCampania").val();
            $.ajax({
                type: 'POST',
                url: url,
                async: true,
                data: { "IdCampania": IdCampania },
                datatype: 'JSON',

                success: function (Result) {

                    setTimeout(function () {
                        if (Result.iTipoResultado) {

                            parent.fn_util_desbloquearPantalla();
                            parent.fn_util_MuestraMensaje("", Result.vMensaje, "OK");
                            //Muestra Listado
                            fn_ListarCampania();



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