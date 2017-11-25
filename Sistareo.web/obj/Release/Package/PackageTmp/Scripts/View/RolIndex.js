var Nombre = "";
var idRolGlobal = 0;
var Order = 0;

$(document).ready(function () {

    //******************************************
    //* Clear iFRAME
    //******************************************
    $("#btnCancelarModal").click(function () {
        $("#ifrModal").contents().find("body").html('');
        //$('#ifrModal').attr('src', '');         
    });

  

    $("#txtValorFiltro").fn_util_validarLetras();

   

    $("#btnNuevo").click(function () {
        
        fn_NuevoRol(0,'');
      
    });



    //Lista Roles
    fn_ListarRoles();

    //Carga Combo
    fn_FiltroLista();
});


//******************************************
//* fn_FiltroLista
//******************************************
function fn_FiltroLista() {
    Nombre = "";
    $("#txtValorFiltro").val("");

    var id = $("#cboFiltro").val();
    if (id == 0) {
        $("#txtValorFiltro").prop("disabled", true);
    } else {
        $("#txtValorFiltro").prop("disabled", false);
        $("#txtValorFiltro").attr("maxlength", 120);
        $("#txtValorFiltro").focus();
    }
}

//******************************************
//* fn_ListarRoles
//******************************************
function fn_ListarRoles() {


    var url = $("#Url_ListarRoles").val();

    //Filtro
    var iTipoFiltro = $("#cboFiltro").val();
    if (iTipoFiltro != 0) {
        var valor = $("#txtValorFiltro").val();
        if ($.trim(valor).length <= 2) {
            fn_util_MuestraMensaje("ALERTA:", "Ingrese al menos 3 caracteres para realizar la búsqueda.", "W");
            return false;
        }
    }

    if (iTipoFiltro == 1) {
        Nombre = valor;
    }

    //Bloquear Pantalla
    fn_util_bloquearPantalla();

    $.ajax({
        type: 'POST',
        url: url,
        async: true,
        data: { "Nombre": Nombre },
        datatype: 'JSON',

        success: function (result) {

            if (result.iTipoResultado == 1) {
                //Muestra Listado
                setTimeout(function () {
                    var htmlListaRol = "";
                    var ListaRol = result.ListaRol;
       
                    //dvVermas                 
                    $.each(ListaRol, function (index, items) {
                      
                        htmlListaRol = htmlListaRol +
                            '<tr id="' + items.IdRol + '" value="' + items.Nombre + '|' + items.Descripcion + '|' + items.TotalUsuario + '">' +
                                
                                 '<td>' + items.Nombre + '</td>' +
                                 '<td>' + items.Descripcion + '</td>' +
                                 '<td style="text-align:center;"><div class="btn btn-xs btn-info btn-circle-xs">' + items.TotalUsuario + '</div></td>' +
                                 '<td style="text-align: center;"><a id="btnEditar_' + items.IdRol + '"   class="glyphicon glyphicon-pencil" onclick="javascript:fn_NuevoRol(\'' + items.IdRol + '\',\'' + items.Nombre + '\')"></a>' +
                                 '<a id="btnEliminarRetoqueProducto_' + items.IdRol + '"  class="glyphicon glyphicon-remove" onclick="javascript:fn_EliminarRol(\'' + items.IdRol + '\')" </a></td>' +
                             '</tr>';
                    });
                    $("#tbContenedor").html(htmlListaRol);
                 
                    if (result.TotalRegistros == 0) {
                        $("#tbContenedor").html("<tr id='0'><td colspan='4' style='text-align:center;'>No existen registros.</td></tr>");
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
//fn_NuevoRol
//***********************************
function fn_NuevoRol(idRol, Nombre) {
    
 

    if (idRol != 0) {
        //Configuración Modal    
        $("#mdlNuevoDialog").css("width", "800px");
        $("#mdlSubTitulo").html(Nombre);
        $("#ifrModal").css("height", "570px");

        $("#btnGrabarModal").html("Grabar");
        $("#mdlTitulo").html("Modificar Rol");
        $("#ifrModal").css("height", "450px");
    } else {
        //Configuración Modal    
        $("#mdlSubTitulo").html("");
        $("#mdlNuevoDialog").css("width", "800px");
        $("#ifrModal").css("height", "570px");

        $("#btnGrabarModal").html("Grabar");
        $("#mdlTitulo").html("Registrar Rol");
        $("#ifrModal").css("height", "450px");
    }

    //limpia icono Modal    
    $("#iIconoModal").addClass("fa-group");

    $("#ifrModal").attr("src", VG_RUTA_SERVIDOR+"Seguridad/NuevoRol?idRol=" + idRol);

    //Mostrar Modal
    $('#mdlNuevo').modal('show');

}

//**************************************
// btnGrabarModal
//**************************************
$("#btnGrabarModal").click(function () {

    var objBotonGrabar = $('#ifrModal').contents().find('#btnGrabar');
    objBotonGrabar.trigger("click");

});

function fn_EliminarRol(IdRol) {
    //Confirmación
    $.confirm({
        title: "CONFIRMACIÓN",
        text: "¿Está seguro que desea eliminar el Rol?",
        confirm: function (button) {
            var url = $("#Url_EliminarRol").val();
            fn_util_bloquearPantalla("Eliminando Rol...");

            $.ajax({
                type: 'POST',
                url: url,
                async: true,
                data: { "IdRol": IdRol },
                datatype: 'JSON',

                success: function (result) {

                    //Muestra Listado
                    setTimeout(function () {
                        if (result.iResultado > 0) {
                            parent.fn_util_desbloquearPantalla();
                            parent.fn_util_MuestraMensaje("", result.vMensaje, "OK");
                
                            fn_ListarRoles();

                        } else {
                            fn_util_MuestraMensaje("Error", result.vMensaje, "E");
                            fn_util_desbloquearPantalla();
                        }
                    }, 1000);
                },
                error: function (data) {

                    fn_util_MuestraMensaje("ERROR.", "No se pudo realizar la acción. Por favor, inténtelo mas tarde.", "E");
                    fn_util_desbloquearPantalla();
                }
            });

        },
        cancel: function (button) { },
        confirmButton: "Aceptar",
        cancelButton: "Cancelar"
    });



  

}