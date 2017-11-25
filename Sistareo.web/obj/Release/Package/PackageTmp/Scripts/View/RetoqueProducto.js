$(document).ready(function () {

    var vFechaActual = $("#FechaApertura").val();
    $('#txtFechaApertura').val(vFechaActual);
    $('#txtFechaApertura').mask("99/99/9999");
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

    $("#btnNuevo").click(function () {

        fn_Limpiar();

    });
    
    fn_ListarPorIdRetoque();

});

function fn_RegistrarRetoqueProducto() {
    //Variables
  
    var IdRetoqueProducto =parseInt( $("#IdRetoqueProducto").val());
    var IdRetoque = $("#IdRetoque").val();
    var IdProducto = $("#cboProducto").val();
    var FechaApertura =$('#txtFechaApertura').val();
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





    var url = $("#Url_RegistrarRetoqueProducto").val();
    $.ajax({
        type: 'POST',
        url: url,
        //async: true,
        data: {
            "IdRetoqueProducto": IdRetoqueProducto, "IdRetoque": IdRetoque,
            "IdProducto": IdProducto, "Descripcion": Descripcion, "FechaApertura":FechaApertura,
            "HoraInicio": HoraInicio, "HoraFin": HoraFin
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
                    //if (Result.iResultado==false) {
                        
                    //    parent.fn_util_MuestraMensaje("Alerta", Result.vMensaje, "W");
                    //} else {

                    //}
                    parent.fn_util_MuestraMensaje("Error", Result.vMensaje, "E");
                   
                    parent.fn_util_desbloquearPantalla();
                }
            }, 1000);
        }
    });
}




function fn_ListarPorIdRetoque() {
    ////Bloquear Pantalla
    //parent.fn_util_bloquearPantalla();

    var IdRetoque = $("#IdRetoque").val();
  
    var url = $("#Url_ListarPorIdRetoque").val();
    
    var vRow = "";
    $.ajax({
        type: 'POST',
        url: url,
        async: true,
        data: {  "IdRetoque": IdRetoque },
        datatype: 'JSON',
        //contentType: "application/json; charset=utf-8",
        //beforeSend: function (xhr) {

        //},
        success: function (Result) {

            if (Result.iTipoResultado == 1) {
                //Limpia Explorador
                $("#tblRetoqueProducto > tbody").html("");
                $.each(Result.ListaRetoqueProducto, function (i, item) {

                    vRow += '<tr>';
                    vRow += '<td > ' + item.IdRetoque + ' </td>';
                    vRow += '<td > ' + item.CodigoBarra + ' </td>';
                    vRow += '<td> ' + item.DescripcionProducto + ' </td>';
                    vRow += '<td> ' + item.DescripcionRetoqueProducto + ' </td>';
                    //vRow += '<td> ' + item.HoraInicioRetoqueProducto + '  </td>';
                    //vRow += '<td>' + item.HoraFinRetoqueProducto + '  </td>';
                    vRow += '<td>' + item.TotalRetoqueProducto + '  </td>';
                    vRow += '<td style="text-align: center;"> <a id="btnEditar_' + item.IdRetoqueProducto + '"   class="glyphicon glyphicon-pencil" onclick="javascript:fn_EditarRetoqueProducto(\'' + item.IdRetoqueProducto + '\')"></a>';
                    vRow += '  <a id="btnAgregarProductoDetalle_' + item.IdRetoqueProducto + '"  class="glyphicon glyphicon-plus" onclick="javascript:fn_AgregarProductoProductoDetalle(\'' + item.IdRetoqueProducto + '\',\'' + item.IdRetoque + '\')" </a>';
                    vRow += '  <a id="btnEliminarRetoqueProducto_' + item.IdRetoqueProducto + '"  class="glyphicon glyphicon-remove" onclick="javascript:fn_EliminarRetoqueProducto(\'' + item.IdRetoqueProducto + '\')" </a></td>';
                    vRow += '</tr>';
                    //$('#tblRetoqueProducto > tbody:last-child').append(vRow);
                });
                if (Result.ListaRetoqueProducto==0) {
                    vRow += '<tr><td colspan="6" style="text-align:center;">No se ha encontrado datos</td></tr>';
                    
                } else {
                    vRow += '<tr><td colspan="4" style="text-align:right;">Total de Horas</td>';
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


function fn_EditarRetoqueProducto(IdRetoqueProducto) {

    var url = $("#Url_EditarRetoqueProducto").val();
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
                $("#IdRetoqueProducto").val(Result.RetoqueProducto.IdRetoqueProducto);
                $("#cboProducto").val(Result.RetoqueProducto.IdProducto);
                $("#txtDescripcion").val(Result.RetoqueProducto.DescripcionRetoqueProducto);
                $("#txtHoraInicio").val(Result.RetoqueProducto.HoraInicioRetoqueProducto);
                $("#txtHoraFin").val(Result.RetoqueProducto.HoraFinRetoqueProducto);

            } else {
                parent.fn_util_MuestraMensaje("ERROR.", Result.vMensaje, "E");
            }
        }
    });
}

function fn_EliminarRetoqueProducto(IdRetoqueProducto) {
    //Confirmación
    window.parent.$.confirm({
        title: "CONFIRMACIÓN",
        text: "¿Está seguro que desea eliminar? ",
        confirm: function (button) {
      //Variables
    
    var url = $("#Url_EliminarRetoqueProducto").val();
    $.ajax({
        type: 'POST',
        url: url,
        async: true,
        data: { "IdRetoqueProducto": IdRetoqueProducto },
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
    $("#IdRetoqueProducto").val(0);
    $("#cboProducto").val(1);
    $("#txtDescripcion").val("");
    $("#txtHoraInicio").val("");
    $("#txtHoraFin").val("");

}
//Agregar  Detalle de Producto
function fn_AgregarProductoProductoDetalle(IdRetoqueProducto,IdRetoque) {
 
    parent.$('#mdlNuevo').modal('hide');

    parent.$('#btnGrabarModal').show();
    parent.$("#mdlRPDetalleDialog").css("width", "900px");
    parent.$('#btnCancelarModal').html("Cancelar");
    parent.$("#ifrModalProduco").css("height", "500px");
    parent.$("#ifrModalProduco").css("width", "800px");
    var vFechaActual = $("#txtFechaApertura").val();
    parent.$("#ifrModalProduco").attr("src", "/Proceso/RetoqueProductoDetalle?IdRetoqueProducto=" + IdRetoqueProducto + '&IdRetoque=' + IdRetoque);
    //Mostrar Modal
    parent.$('#mdlRPDetalle').modal('show');
}