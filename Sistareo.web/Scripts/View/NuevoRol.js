$(document).ready(function () {

    var IdRol = $("#hddidRol").val();
    if (IdRol != 0) {
        fn_ObtenerRol(IdRol)
    }
   
  //  fn_ObtenerMenu(idRol);

    //****************************
    //Scroll Acceso Detalle
    //****************************
    $('#dvAccesos').slimScroll({
        height: '250px'
    });

    $("#txtNombre").fn_util_validarAlfaNumerico();
    $("#txtDescripcion").fn_util_validarAlfaNumerico();

    $("#btnGrabar").click(function () {
        fn_RegistrarRol();
    });

    fn_ObtenerMenu(IdRol);
});



function fn_RegistrarRol() {

    var IdRol = parseInt($("#hddidRol").val());
    var Nombre = $("#txtNombre").val();
    var Descripcion = $("#txtDescripcion").val();
    var cadenaMenu = "";
    var SiSuperAdmi = $('#SiSuperAdmi').prop('checked');

    if ($.trim(Nombre).length <= 2) {
        parent.fn_util_MuestraMensaje("Alerta", "Ingrese un Nombre mínimo de 3 caracteres", "W");
        return false;
    }

    if ($.trim(Descripcion).length <= 2) {
        parent.fn_util_MuestraMensaje("Alerta", "Ingrese un descripción mínima de 3 caracteres", "W");
        return false;
    }

    //Obtiene opciones de Menu
    $.each($("[Tipo='Seleccionado']"), function (index, item) {
        if ($(item).is(":checked")) {
            if ($(item).val() != 0) {
                cadenaMenu = cadenaMenu + $(item).val() + "|";
            }
        }
    });

    if ($.trim(cadenaMenu).length == 0) {
        parent.fn_util_MuestraMensaje("Alerta", "Debe elegir al menos una opcion de Menú para el Rol", "W");
        return false;
    }

    //Bloquear Pantalla
    parent.$("#btnGrabarModal").button('reset');
    if (IdRol > 0) {
        parent.fn_util_bloquearPantalla("Modificando Rol...");
    } else {
        parent.fn_util_bloquearPantalla("Guardando Rol...");
    }

    //Grabado (Ajax)


    var url = $("#Url_RegistrarRol").val();
    $.ajax({
        type: 'POST',
        url: url,
        //async: true,
        data: { "IdRol": IdRol, "Nombre": Nombre, "Descripcion": Descripcion, "SiSuperAdmi": SiSuperAdmi, "cadenaMenu": cadenaMenu },
        datatype: 'JSON',
        
        success: function (result) {
            parent.$("#btnGrabarModal").button('reset');
            //Muestra Listado
            setTimeout(function () {
                if (result.iResultado != 2) {
                    if (result.iResultado == -1) {
                        parent.fn_util_MuestraMensaje("Error", result.vMensaje, "E");
                        parent.fn_util_desbloquearPantalla();
                    } else if (result.iResultado == 0) {
                        parent.fn_util_MuestraMensaje("Error", "El nombre del rol ya se encuentra registrado.", "E");
                        parent.fn_util_desbloquearPantalla();
                    } else {
                        parent.fn_util_desbloquearPantalla();
                        parent.fn_util_MuestraMensaje("", result.vMensaje, "OK");
                        parent.$("#hddPaginaActual").val(1);
                        parent.idRolGlobal = 0;
                        parent.Order = 0;
                        parent.fn_ListarRoles('B');

                        //Cierra Modal
                        parent.$('#mdlNuevo').modal('hide');
                    }
                } else {
                    parent.fn_util_MuestraMensaje("Error", result.vMensaje, "E");
                    parent.fn_util_desbloquearPantalla();
                }
            }, 1000);
        },
        error: function (data) {
            fn_util_MuestraMensaje("ERROR.", "No se pudo realizar la acción. Por favor, inténtelo mas tarde.", "E");
            parent.fn_util_desbloquearPantalla();
        }
    });

}


function fn_ObtenerMenu(IdRol) {

    //Bloquear Pantalla
    parent.fn_util_bloquearPantalla("Obteniendo Menú");
    

    var url = $("#Url_ListarMenuRol").val();
    $.ajax({
        type: 'POST',
        url: url,
        async: true,
        data: { "IdRol": IdRol },
        datatype: 'JSON',

        success: function (result) {

            if (result.iTipoResultado == 1) {
                var loenMenu = result.loenMenu;
                var AccesosHtml = "";
                $.each(loenMenu, function (index, item) {
                    if (item.IdOrden == -1) {
                        AccesosHtml = AccesosHtml +
                            '<div class="css_accesos_contenedor css_accesos_n1" nivel="1" acceso="Nivel1_' + item.IdPagina + '" atrPadre="' + item.IdPadre + '" id="' + item.IdPagina + '" atrDetalle="0">' +
                                 '<input type="checkbox" Tipo="Seleccionado" id="1_' + item.IdPagina + '" idPadre="1_' + item.IdPadre + '" onclick="fn_SeleccionMenu(1,' + item.IdPadre + ',' + item.IdPagina + ',' + item.IdOrden + ',this);" value="0"';
                        if (item.Seleccion == true) {
                            AccesosHtml = AccesosHtml + ' Checked '
                        }
                        AccesosHtml = AccesosHtml + ' style="margin-left:6px;"/><i style="cursor:pointer;" onclick="fn_DetalleMenu(\'1\',' + item.IdPagina + ',\'Nivel1_' + item.IdPagina + '\');" class="' + item.EstiloMenu + '"></i><span>' + item.Nombre + '</span>' +
                             '</div>';
                    }
                    if (item.IdOrden == 0) {
                        AccesosHtml = AccesosHtml +
                            '<div class="css_accesos_contenedor" nivel="2" acceso="Nivel2_' + item.IdPagina + '"  style="margin-left:29px;display:none;" atrPadre="' + item.IdPadre + '" id="' + item.IdPagina + '" atrDetalle="0">' +
                                 '<input type="checkbox" Tipo="Seleccionado"  id="2_' + item.IdPagina + '" idPadre="2_' + item.IdPadre + '" onclick="fn_SeleccionMenu(2,' + item.IdPadre + ',' + item.IdPagina + ',' + item.IdOrden + ',this);" value="' + item.IdPagina + '"';
                        if (item.Seleccion == true) {
                            AccesosHtml = AccesosHtml + ' Checked '
                        }
                        AccesosHtml = AccesosHtml + '/><i style="cursor:pointer;" onclick="fn_DetalleMenu(\'2\',' + item.IdPagina + ',\'Nivel2_' + item.IdPagina + '\');" class="' + item.EstiloMenu + '"></i> <span>' + item.Nombre + '</span>' +
                             '</div>';
                    }
                    if (item.IdOrden > 0) {
                        AccesosHtml = AccesosHtml +// atrPadre="' + item.IdPadre + '" 
                            '<div class="css_accesos_contenedor" nivel="3" acceso="Nivel3_' + item.IdPagina + '"  style="margin-left:57px;display:none;"   atrPadre="' + item.IdPadre + '"   id="' + item.IdPagina + '" atrDetalle="0" >' +
                                 '<input type="checkbox"  Tipo="Seleccionado"  id="3_' + item.IdPagina + '" iOrden="3_' + item.IdOrden + '" idPadre="3_' + item.IdPadre + '" onclick="fn_SeleccionMenu(3,' + item.IdPadre + ',' + item.IdPagina + ',' + item.IdOrden + ',this);" value="' + item.IdPagina + '"';
                        if (item.Seleccion == true) {
                            AccesosHtml = AccesosHtml + ' Checked '
                        }
                        AccesosHtml = AccesosHtml + '/><i style="cursor:pointer;" onclick="fn_DetalleMenu(\'3\',' + item.IdPagina + ',\'Nivel3_' + item.IdPagina + '\');" class="' + item.EstiloMenu + '"></i> <span>' + item.Nombre + '</span>' +
                             '</div>';
                    }
                });

                if (AccesosHtml == "") {
                    AccesosHtml = "<center><h2><small>No tiene accesos.</small></h2></center>"
                }
                $("#dvAccesos").html(AccesosHtml);
            }
            else {
                parent.fn_util_MuestraMensaje("ERROR.", "No se pudo listar las opciones de Menú por Rol. Por favor, inténtelo mas tarde.", "E");
            }

            //Desbloquear Pantalla
            parent.fn_util_desbloquearPantalla();
        },
        error: function (data) {

            //Desbloquear Pantalla
            parent.fn_util_desbloquearPantalla();
            parent.fn_util_MuestraMensaje("ERROR.", "No se pudo listar las opciones de Menú por Rol. Por favor, inténtelo mas tarde.", "E");
        }
    });



}


function fn_SeleccionMenu(nivel, idPadre, idMenu, piOrden, obj) {
    var contador = 0;
    $.each($("[idPadre='" + nivel + "_" + idPadre + "']"), function (index, Nivel2) {
        if ($(Nivel2).is(":checked")) {
            contador++;
        }
    });

    if (contador > 0) {
        var anteriorNivel = nivel - 1;
        $("[id='" + anteriorNivel + "_" + idPadre + "']").prop("checked", true);
    }

    if (contador == 0) {
        var anteriorNivel = nivel - 1;
        $("[id='" + anteriorNivel + "_" + idPadre + "']").prop("checked", false);
    }

    var siguienteNivel = nivel + 1;
    if ($(obj).is(":checked")) {
        $.each($("[idPadre='" + siguienteNivel + "_" + idMenu + "']"), function (index, Nivel2) {
            $(Nivel2).prop("checked", true);
            if (siguienteNivel == 2) {
                var idmenu2 = $(Nivel2).val();
                var siguienteNivel3 = siguienteNivel + 1;
                $.each($("[idPadre='" + siguienteNivel3 + "_" + idmenu2 + "']"), function (index, Nivel3) {
                    $(Nivel3).prop("checked", true);
                });
            }
        });
    } else {
        $.each($("[idPadre='" + siguienteNivel + "_" + idMenu + "']"), function (index, Nivel2) {
            $(Nivel2).prop("checked", false);
            if (siguienteNivel == 2) {
                var idmenu2 = $(Nivel2).val();
                var siguienteNivel3 = siguienteNivel + 1;
                $.each($("[idPadre='" + siguienteNivel3 + "_" + idmenu2 + "']"), function (index, Nivel3) {
                    $(Nivel3).prop("checked", false);
                });
            }
        });
    }

    //If Nivel 3
    if (nivel == 3) {
        if (piOrden == 1) {
            if (!$(obj).is(":checked")) {
                $("[id^='3_']").prop("checked", false);
                $("[id='2_" + idPadre + "']").prop("checked", false);
            }
        } else {
            $("[iOrden='3_1']").prop("checked", true);
        }
    }

}



function fn_DetalleMenu(nivel, idPadre, obj) {

    debugger;
    var Detalle = $("[acceso='" + obj + "']").attr("atrDetalle");
    if (Detalle == 0) {
        $.each($("[atrPadre=" + idPadre + "]"), function (index, value) {
            var atrPadre = $(value).attr("atrPadre");
            if (atrPadre == idPadre) {
                $(value).show();
            }
        });
        $("[acceso='" + obj + "']").attr("atrDetalle", "1");
    } else {
        nivel++;
        $.each($("[atrPadre=" + idPadre + "][nivel='" + nivel + "']"), function (index, value) {
            var id = $(value).attr("id");
            var atrPadre = $(value).attr("atrPadre");
            $(value).hide();
            $(value).attr("atrDetalle", "0");
            if (atrPadre == idPadre) {
                nivel++;
                $.each($("[atrPadre=" + id + "][nivel='" + nivel + "']"), function (index, Nivel2) {
                    var id2 = $(Nivel2).attr("id");
                    var atrPadre2 = $(Nivel2).attr("atrPadre");
                    if (id == atrPadre2) {
                        $(Nivel2).hide();
                        $(Nivel2).attr("atrDetalle", "0");
                    }
                });
            }
        });
        $("[acceso='" + obj + "']").attr("atrDetalle", "0");
    }
}



function fn_ObtenerRol(IdRol) {

    //Bloquear Pantalla
    parent.fn_util_bloquearPantalla();
    

    //Grabado (Ajax)
    var url = $("#Url_ObtenerRol").val();
    $.ajax({
        type: 'POST',
        url: url,
        async: true,
        data: { "IdRol": IdRol },
        datatype: 'JSON',

        success: function (Result) {
            if (Result.iTipoResultado == 1) {
                var oRol = Result.Rol;
                $("#txtNombre").val(oRol.Nombre);
                $("#txtDescripcion").val(oRol.Descripcion);
                //var isAdmin = (oRol.SiSuperAdmi == 1) ? true : false;
                $("#SiSuperAdmi").prop("checked", oRol.SiSuperAdmi);

                //Desbloquear Pantalla
                parent.fn_util_desbloquearPantalla();

            } else {
                parent.fn_util_MuestraMensaje("ERROR.", Result.vError, "E");

                //Desbloquear Pantalla
                parent.fn_util_desbloquearPantalla();
            }
        },
        error:function () {
            fn_util_MuestraMensaje("ERROR.", "No se pudo Obtener el detalle del rol. Por favor, inténtelo mas tarde.", "E");
            parent.fn_util_desbloquearPantalla();
        }
    });


}