
$(document).ready(function () {

    var vFechaActual = $("#vFechaActual").val();
    $('#txtFechaApertura').val(vFechaActual);

 
    
    $('#FechaIni').datetimepicker({
        locale: 'es',
        format: 'DD/MM/YYYY'
        //defaultDate: n
    });

    $("#btnNuevo").click(function () {
        fn_Nuevo(0);
    });

    //**************************************
    // btnGrabarModal
    //**************************************
    $("#btnGrabarModal").click(function () {

        var objBotonGrabar = $('#ifrModal').contents().find('#btnGrabar');
        objBotonGrabar.trigger("click");
        $(this).button('loading');
        $(this).button('reset');


    });
    //**************************************
    // btnGrabarDetalleModal
    //**************************************
    $("#btnGrabarDetalleModal").click(function () {

        var objBotonGrabar = $('#ifrModalProduco').contents().find('#btnGrabar');
        objBotonGrabar.trigger("click");
        $(this).button('loading');
        $(this).button('reset');


    });

    //**************************************
    // btnCancelarDetalleModal Detalle
    //**************************************
    $("#btnCancelarDetalleModal").click(function () {

        var objBotonCancelar = $('#ifrModalProduco').contents().find('#btnCancelar');
        objBotonCancelar.trigger("click");
        $(this).button('loading');
        $(this).button('reset');


    });


    //Buscar Fecha por Operador
    $("#btnBuscar").click(function () {
        fn_ListarFechaPorOperario();
    });
    fn_ObtenerOperador();
    fn_ListarFechaPorOperario();
  
    //**************************************
    // btnGrabarModal
    //**************************************
    $("#btnEliminar").click(function () {

        var objBotonGrabar = $('#ifrModal').contents().find('#btnEliminar');
        objBotonGrabar.trigger("click");
        $(this).button('loading');
        $(this).button('reset');


    });
});
//***********************************
//fn_abreNuevo
//***********************************
function fn_Nuevo(IdRetoque) {

   

    if (IdRetoque==0) {
        $("#btnEliminar").hide();
      
   } else {
        $("#btnEliminar").show();
    }

    $("#ifrModal").attr("src", "/Proceso/Retoque?IdRetoque=" + IdRetoque);
    $('#btnGrabarModal').show();
    $("#mdlNuevoDialog").css("width", "600px");
    $('#btnCancelarModal').html("Cancelar");
    $("#ifrModal").css("height", "300px");
    $("#ifrModal").css("width", "550px");
    
    //Mostrar Modal
    $('#mdlNuevo').modal('show');
}

//Lista Operario

function fn_ListarFechaPorOperario() {
    //Bloquear Pantalla
    parent.fn_util_bloquearPantalla();

    var IdOperador = parseInt( $("#cboOperario").val());
    var FechaApertura = $("#txtFechaApertura").val();
    var url = $("#Url_ListarFechaPorOperario").val();

    $.ajax({
        type: 'POST',
        url: url,
        async: true,
        data: { "FechaApertura": FechaApertura, "IdOperador": IdOperador },
        datatype: 'JSON',
        //contentType: "application/json; charset=utf-8",
        //beforeSend: function (xhr) {

        //},
        success: function (Result) {

            if (Result.iTipoResultado == 1) {
                //Limpia 
                $("#tblExplorador > tbody").html("");
                $.each(Result.ListaRetoque, function (i, item) {
                    
                    var vRow = "";
                    vRow += '<tr>';
                    vRow += '<td > ' + item.NombreCampania + ' </td>';
                    vRow += '<td > ' + item.NombreCompleto + ' </td>';
                    vRow += '<td> ' + item.vFechaApertura + ' </td>';
                    vRow += '<td> ' + item.Jefatura + '  </td>';
                    vRow += '<td>' + item.Coordinador + '  </td>';
                    vRow += '<td style="text-align: center;"> <a id="btnEditar_' + item.IdRetoque + '"   class="glyphicon glyphicon-pencil" onclick="javascript:fn_Nuevo(\'' + item.IdRetoque + '\')"></a>';
                    vRow += '<a id="btnAgregar_' + item.IdRetoque + '"  class="glyphicon glyphicon-plus" onclick="javascript:fn_AgregarProducto(\'' + item.IdRetoque + '\')" </a>';
                    vRow += '  <a id="btnEliminarRetoqueProducto_' + item.IdRetoqueProducto + '"  class="glyphicon glyphicon-remove" onclick="javascript:fn_EliminarRetoqueProducto(\'' + item.IdRetoque + '\')" </a></td>';
                    vRow += '</tr>';
                    $('#tblExplorador > tbody:last-child').append(vRow);
                });

                //Desbloquear Pantalla
                parent.fn_util_desbloquearPantalla();
            } else {
                parent.fn_util_MuestraMensaje("ERROR.", "Ocurrió un error", "E");
            }
        },
        error: function (data) {
            alert("Ocurrió un error")
            //Desbloquear Pantalla
            //Parent.fn_util_desbloquearPantalla();
            //parent.fn_util_MuestraMensaje("ERROR.", "Ocurrió un error", "E");
        }
    });
}

//Permite obtener la lista de los operadores
function fn_ObtenerOperador() {
    var url = $("#Url_ListarTodoOperador").val();
    var Cuenta = $('#cboOperario');

    $.ajax({
        type: "GET",
        url: url,
        async: true,
        dataType: "json",
        cache: false,
        success: function (Result) {
            if (Result.iTipoResultado == 1) {
                var lstLista = Result.ListaOperador
                $.each(lstLista, function (index, item) {
                   
                    $("#cboOperario").append("<option value=" + this.IdOperario + ">" + this.NombreCompleto + "</option>");
                });

            
            }
        }//,
        //beforeSend: function (xhr) {
        //    parent.fn_util_bloquearPantalla();
        //},
        //complete: function () {
        //    fn_util_desbloquearPantalla();
        //}
    });
}

function fn_AgregarProducto(IdRetoque) {

    $('#btnGrabarModal').show();
    $("#mdlNuevoDialog").css("width", "1200px");
    $('#btnCancelarModal').html("Cancelar");
    $("#ifrModal").css("height", "500px");
    $("#ifrModal").css("width", "1100px");
    
    $("#ifrModal").attr("src", "/Proceso/RetoqueProducto?IdRetoque=" + IdRetoque );
    //Mostrar Modal
    $('#mdlNuevo').modal('show');
}


//**************************************
//*Boton Eliminar Carpeta
//**************************************

function fn_EliminarRetoqueProducto(IdRetoque) {
    //Confirmación
    window.parent.$.confirm({
        title: "CONFIRMACIÓN",
        text: "¿Está seguro que desea eliminar? ",
        confirm: function (button) {

            //Grabado (Ajax)
            parent.fn_util_bloquearPantalla();


            var url = $("#Url_EliminarRetoque").val();
            $.ajax({
                type: 'POST',
                url: url,
                async: true,
                data: { "IdRetoque": IdRetoque },
                datatype: 'JSON',

                success: function (Result) {

                    setTimeout(function () {
                        if (Result.iTipoResultado) {

                            parent.fn_util_desbloquearPantalla();
                            parent.fn_util_MuestraMensaje("", Result.vMensaje, "OK");
                            //Muestra Listado
                            fn_ListarFechaPorOperario();

            

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
