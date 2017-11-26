$(document).ready(function () {

    ListarProcesoCombo();

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
    debugger;
    var IdRetoque = parseInt($("#IdRetoque").val());
    var IdOperario = $("#cboOperario").val();
    var IdCampania = $("#cboCampania").val();

    var IdJefatura = $("#txtJefatura").val();
    var IdCoordinador = $("#txtCoordinador").val();

    var Jefatura = $("#txtJefatura  option:selected").text();
    var Coordinador = $("#txtCoordinador  option:selected").text();
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
    if ($.trim(IdJefatura) == 0) {
        parent.fn_util_MuestraMensaje("Alerta", "Ingrese Jefatura", "W");
        return false;
    }
    if ($.trim(IdCoordinador) == 0) {
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
                
                $("#txtJefatura option").filter(function () {
                    return this.text == Result.Retoque.Jefatura;
                }).attr('selected', true);
                $("#txtCoordinador option").filter(function () {
                    return this.text == Result.Retoque.Coordinador;
                }).attr('selected', true);
                //$("#txtJefatura").text(Result.Retoque.Jefatura);
                //$("#txtCoordinador").text(Result.Retoque.Coordinador);
                $("#txtFechaApertura").val(Result.Retoque.vFechaApertura);

            } else {
                parent.fn_util_MuestraMensaje("ERROR.", Result.vMensaje, "E");
            }
        }
    });

}

//Permite obtener la lista de los operadores s
function ListarProcesoCombo() {
    //Bloquear Pantalla
    parent.fn_util_bloquearPantalla();

    var url = $("#Url_ListarProcesoCombo").val();
    var Cuenta = $('#cboOperario');

    $.ajax({
        type: "GET",
        url: url,
        async: true,
        dataType: "json",
        cache: false,
        success: function (Result) {

            //setTimeout(function () {
            if (Result.iTipoResultado == 1) {
                var lstLista = Result.ListaOperador
                $.each(lstLista, function (index, item) {
                    $("#cboOperario").append("<option value=" + this.IdOperario + ">" + this.NombreCompleto + "</option>");
                });

                var ListaCampania = Result.ListaCampania
                $.each(ListaCampania, function (index, item) {
                    $("#cboCampania").append("<option value=" + this.IdCampania + ">" + this.Nombre + "</option>");
                });

                var ListaJefatura = Result.ListaJefatura
                $.each(ListaJefatura, function (index, item) {
                    $("#txtJefatura").append("<option value=" + this.IdUsuario + ">" + this.NombreCompleto + "</option>");
                });


                var ListaCoordinador = Result.ListaCoordinador
                $.each(ListaCoordinador, function (index, item) {
                    $("#txtCoordinador").append("<option value=" + this.IdUsuario + ">" + this.NombreCompleto + "</option>");
                });

            } else {
                parent.fn_util_MuestraMensaje("Error", Result.Mensaje, "E");
                parent.fn_util_desbloquearPantalla();
            }
            //}, 1000);
        },
        beforeSend: function (xhr) {
            parent.fn_util_bloquearPantalla();
        },
        complete: function () {
            parent.fn_util_desbloquearPantalla();
        }
    });
}

