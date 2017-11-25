$(document).ready(function () {
    


    var vFechaActual = $("#hhFechaApertura").val();
    $('#hhFechaApertura').val(vFechaActual);
    $('#hhFechaApertura').mask("99/99/9999");
    $.mask.definitions['H'] = "[0-2]";
    $.mask.definitions['h'] = "[0-9]";
    $.mask.definitions['M'] = "[0-5]";
    $.mask.definitions['m'] = "[0-9]";

    //Hora Inicio
    $("#txtHoraInicio").mask("Hh:Mm", {
        completed: function () {
            var currentMask = $(this).mask();
            if (isNaN(parseInt(currentMask))) {
                $(this).val("");
            } else if (parseInt(currentMask) > 2359) {
                $(this).val("23:59");
            };
        }
    });
    //Hora Fin
    $("#txtHoraFin").mask("Hh:Mm", {
        completed: function () {
            var currentMask = $(this).mask();
            if (isNaN(parseInt(currentMask))) {
                $(this).val("");
            } else if (parseInt(currentMask) > 2359) {
                $(this).val("23:59");
            };
        }
    });

    $("#btnGrabar").click(function () {

        fn_RegistrarRetoqueProducto();

    });

    $("#btnCancelar").click(function () {
        debugger;
        var IdRetoque = $("#hhIdRetoque").val();
       parent.$("#IdRetoque").val(IdRetoque);
       //parent.fn_ListarPorIdRetoque();
       parent.$('#btnGrabarModal').show();
       parent.$("#mdlNuevoDialog").css("width", "1200px");
       parent.$('#btnCancelarModal').html("Cancelar");
       parent.$("#ifrModal").css("height", "500px");
       parent.$("#ifrModal").css("width", "1100px");

       parent.$("#ifrModal").attr("src", "/Proceso/RetoqueProducto?IdRetoque=" + IdRetoque);
        //Mostrar Modal
       parent.$('#mdlNuevo').modal('show');
       parent.$('#mdlRPDetalle').modal('hide');
    });




    $("#btnNuevo").click(function () {

        fn_Limpiar();

    });

    fn_ListarPorIdRetoque();
    $("#btnNuevo").click(function () {

        fn_Limpiar();

    });
});

function fn_RegistrarRetoqueProducto() {
    //Variables

    var IdRetoqueProductoDetalle = parseInt($("#IdRetoqueProductoDetalle").val());
    var IdRetoqueProducto = parseInt($("#IdRetoqueProducto").val());
    var FechaApertura = $('#hhFechaApertura').val();
    var Descripcion = $("#txtDescripcion").val();

    var HoraInicio = $("#txtHoraInicio").val();
    var HoraFin = $("#txtHoraFin").val();

    parent.$("#btnGrabarModal").button('reset');


    //VAlidaciones
    if ($.trim(Descripcion) == "") {
        parent.fn_util_MuestraMensaje("Alerta", "Ingrese Descripción", "W");
        return false;
    }

    if ($.trim(HoraInicio) == "") {
        parent.fn_util_MuestraMensaje("Alerta", "Ingrese Hora Inicio", "W");
        return false;
    }
    if ($.trim(HoraFin) == "") {
        parent.fn_util_MuestraMensaje("Alerta", "Ingrese Hora Fin", "W");
        return false;
    }
    if ($.trim(HoraInicio) > $.trim(HoraFin)) {
        parent.fn_util_MuestraMensaje("Alerta", "Hora Fin, no puede ser menor al de hora de Inicio, por favor de revisar la hora !", "W");
        return false;
    }
    

    var url = $("#Url_RegistrarRetoqueProductoDetalle").val();
    $.ajax({
        type: 'POST',
        url: url,
        async: true,
        data: {
            "IdRetoqueProductoDetalle": IdRetoqueProductoDetalle, "IdRetoqueProducto": IdRetoqueProducto, "FechaApertura": FechaApertura,
            "Descripcion": Descripcion, "HoraInicio": HoraInicio, "HoraFin": HoraFin
        },
        datatype: 'JSON',
        //contentType: "application/json; charset=utf-8",
        beforeSend: function (xhr) {
            parent.fn_util_bloquearPantalla();
        },
        success: function (Result) {
            //Muestra Listado
            setTimeout(function () {
                if (Result.iTipoResultado) {

                   fn_Limpiar();
                    parent.fn_util_MuestraMensaje("", Result.vMensaje, "OK");

                    fn_ListarPorIdRetoque();
                    parent.fn_util_desbloquearPantalla();
                    //Cierra Modal
                    //parent.$('#mdlNuevo').modal('hide');

                } else {
                    parent.fn_util_MuestraMensaje("Error", Result.vMensaje, "E");
                    parent.fn_util_desbloquearPantalla();
                }
            }, 1000);
        }
    });
}


//Lista Operario

function fn_ListarPorIdRetoque() {
    ////Bloquear Pantalla
    //parent.fn_util_bloquearPantalla();

    var IdRetoqueProducto = $("#IdRetoqueProducto").val();

    var url = $("#Url_ListarPorIdProductoDetalle").val();
    var vRow = "";
    $.ajax({
        type: 'POST',
        url: url,
        async: true,
        data: { "IdRetoqueProducto": IdRetoqueProducto },
        datatype: 'JSON',
        //contentType: "application/json; charset=utf-8",
        //beforeSend: function (xhr) {

        //},
        success: function (Result) {

            if (Result.iTipoResultado == 1) {
                //Limpia Explorador
                $("#tblRetoqueProducto > tbody").html("");
                $.each(Result.ListaRetoqueProductoDetalle, function (i, item) {

                    vRow += '<tr>';
                  
                    vRow += '<td style="text-align:left;"> ' + item.DescripcionRetoqueProductoDetalle + ' </td>';
                    vRow += '<td> ' + item.HoraInicioRetoqueProductoDetalle + '  </td>';
                    vRow += '<td>' + item.HoraFinRetoqueProductoDetalla + '  </td>';
                    vRow += '<td>' + item.TotalRetoqueProductoDetalle + '  </td>';
                    vRow += '<td style="text-align: center;"> <a id="btnEditar_' + item.IdRetoqueProductoDetalle + '"   class="glyphicon glyphicon-pencil" onclick="javascript:fn_EditarRetoqueProducto(\'' + item.IdRetoqueProductoDetalle + '\')"></a>';
                   
                    vRow += '  <a id="btnEliminarRetoqueProducto_' + item.IdRetoqueProductoDetalle + '"  class="glyphicon glyphicon-remove" onclick="javascript:fn_EliminarRetoqueProducto(\'' + item.IdRetoqueProductoDetalle + '\')" </a></td>';
                    vRow += '</tr>';
                    //$('#tblRetoqueProducto > tbody:last-child').append(vRow);
                });
                if (Result.ListaRetoqueProductoDetalle == 0) {
                    vRow += '<tr><td colspan="5" style="text-align:center;">No se ha encontrado datos</td></tr>';

                } else {
                    vRow += '<tr><td colspan="3" style="text-align:right;">Total de Horas</td>';
                    vRow += '<td>' + Result.TotalHoras + '  </td></tr>';
                }
                //Llena la tabla
                $('#tblRetoqueProducto > tbody:last-child').html(vRow);

                //Desbloquear Pantalla
                parent.fn_util_desbloquearPantalla();
            } else {
                parent.fn_util_MuestraMensaje("ERROR.", "Ocurrió un error", "E");
            }
        },
        error: function (data) {
            alert(data)
            //Desbloquear Pantalla
            parent.fn_util_desbloquearPantalla();
            parent.fn_util_MuestraMensaje("ERROR.", "Ocurrió un error", "E");
        }
    });
}


function fn_EditarRetoqueProducto(IdRetoqueProductoDetalle) {

    var url = $("#Url_EditarRetoqueProductoDetalle").val();
    $.ajax({
        type: 'POST',
        url: url,
        async: true,
        data: { "IdRetoqueProductoDetalle": IdRetoqueProductoDetalle },
        datatype: 'JSON',
        //contentType: "application/json; charset=utf-8",
        //beforeSend: function (xhr) {

        //},
        success: function (Result) {
            if (Result.iTipoResultado == 1) {
                $("#IdRetoqueProductoDetalle").val(Result.RetoqueProductoDetalle.IdRetoqueProductoDetalle);
              
                $("#txtDescripcion").val(Result.RetoqueProductoDetalle.DescripcionRetoqueProductoDetalle);
                $("#txtHoraInicio").val(Result.RetoqueProductoDetalle.HoraInicioRetoqueProductoDetalle);
                $("#txtHoraFin").val(Result.RetoqueProductoDetalle.HoraFinRetoqueProductoDetalla);

            } else {
                parent.fn_util_MuestraMensaje("ERROR.", Result.vMensaje, "E");
            }
        }
    });
}

function fn_EliminarRetoqueProducto(IdRetoqueProductoDetalle) {
    //Confirmación
    window.parent.$.confirm({
        title: "CONFIRMACIÓN",
        text: "¿Está seguro que desea eliminar? ",
        confirm: function (button) {
    //Variables

    var url = $("#Url_EliminarRetoqueProductoDetalle").val();
    $.ajax({
        type: 'POST',
        url: url,
        async: true,
        data: { "IdRetoqueProductoDetalle": IdRetoqueProductoDetalle },
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
                    fn_ListarPorIdRetoque();
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

function fn_Limpiar() {
    $("#IdRetoqueProductoDetalle").val(0);
   
    $("#txtDescripcion").val("");
    $("#txtHoraInicio").val("");
    $("#txtHoraFin").val("");

}