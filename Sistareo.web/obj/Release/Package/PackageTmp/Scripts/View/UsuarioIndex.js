//Variables Globales - Filtros;
var Nombres = "";
var Usuario = "";
var vNombreUsuarioGlobal = "";
var iCodigoEstadoGlobal = "0";
var idCodigoUsuario = "0";
$(document).ready(function () {


    //******************************************
    //* Clear iFRAME
    //******************************************
    $("#btnCancelarModal").click(function () {
        $("#ifrModal").contents().find("body").html('');
        //$('#ifrModal').attr('src', '');         
    });


    //$("#txtValorFiltro").fn_util_validarLetras();



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

    //fn_ListarUsuarios('B');

    //Carga Combo
    fn_FiltroLista();
    //Listar Usuario
    fn_ListarUsuarios()


    //**************************************
    // btnGrabarModal
    //**************************************
    $("#btnGrabarModal").click(function () {

        var objBotonGrabar = $('#ifrModal').contents().find('#btnGrabar');
        objBotonGrabar.trigger("click");

    });
});


//***********************************
//fn_abreNuevo
//***********************************
function fn_abreNuevo(IdUsuario) {




    if (IdUsuario != 0) {
        //Configuración Modal    
        $("#mdlNuevoDialog").css("width", "900px");
        $("#ifrModal").css("height", "400px");
        $("#ifrModal").css("width", "850px");
        $("#btnGrabarModal").html("Grabar");
        $("#mdlTitulo").html("Modificar Usuario");
        //limpia icono Modal    
       
    } else {
        //Configuración Modal    
        $("#mdlNuevoDialog").css("width", "900px");
        $("#mdlSubTitulo").html("Crear Usuario");
        $("#ifrModal").css("height", "400px");
        $("#ifrModal").css("width", "850px");
        $("#btnGrabarModal").html("Grabar");
        $("#mdlTitulo").html("Nuevo Usuario");

  
    }

    $("#ifrModal").attr("src", VG_RUTA_SERVIDOR+"Seguridad/NuevoUsuario?IdUsuario=" + IdUsuario);

    //Mostrar Modal
    $('#mdlNuevo').modal('show');


}


function fn_FiltroLista() {
    $("#hddPaginaActual").val(1);
    $("#txtValorFiltro").val("");
    Nombres = "";
    Usuario = "";
    var id = $("#cboFiltroUsuario").val();
    if (id == 0) {
        $("#txtValorFiltro").prop("disabled", true);
    } else {
        $("#txtValorFiltro").prop("disabled", false);
        if (id == 1) {
            $("#txtValorFiltro").attr("maxlength", 120);
        } else if (id == 2) {
            $("#txtValorFiltro").attr("maxlength", 50);
        }
    }
}


function fn_ListarUsuarios() {

    $("#hddNombreUsuario").val(0);

    var url = $("#Url_ListarUsuario").val();
    //Filtro
    var iTipoFiltro = $("#cboFiltroUsuario").val();
    if (iTipoFiltro != 0) {
        var valor = $.trim($("#txtValorFiltro").val());
        if (valor.length <= 2) {
            fn_util_MuestraMensaje("ALERTA:", "Ingrese al menos 3 caracteres para realizar la búsqueda.", "W");
            return false;
        }
    }

    if (iTipoFiltro == 1) {
        Nombres = valor;
    }
    if (iTipoFiltro == 2) {
        Usuario = valor;
    }

    //Bloquear Pantalla
    fn_util_bloquearPantalla();

    //Grabado (Ajax)


    $.ajax({
        type: 'POST',
        url: url,
        async: true,
        data: { "Nombres": Nombres, "Usuario": Usuario },
        datatype: 'JSON',
    
        success: function (result) {

            if (result.iTipoResultado == 1) {
                //Muestra Listado
                setTimeout(function () {
                    var htmlListaUsuario = "";
                    var loenUsuario = result.ListaUsuario;



                    $.each(loenUsuario, function (index, items) {
                        htmlListaUsuario = htmlListaUsuario + '<tr id="' + items.NombreUsuario + '" style="cursor: pointer;">' +
                            '<td>' + items.NombreCompleto + '</td>' +
                            '<td>' + items.NombreUsuario + '</td>' +
                            '<td><i class="fa fa-envelope contact-type"></i> ' + items.DNI + '</td>' +
                     
                            '<td style="text-align: center;"> <a id="btnEditar_' + items.IdUsuario + '"   class="glyphicon glyphicon-pencil" onclick="javascript:fn_abreNuevo(\'' + items.IdUsuario + '\')"></a>' +
                        '<a id="btnEliminarUsuario_' + items.IdUsuario + '"  class="glyphicon glyphicon-remove" onclick="javascript:fn_EliminarUsuario(\'' + items.IdUsuario + '\')" </a></td>' +
                        '</tr>';
                    });
                    ;
                    $("#tbContenedorUUsuario").html(htmlListaUsuario);

                    if (loenUsuario.length == 0) {
                        $("#tbContenedorUUsuario").html("<tr id='0'><td colspan='5' style='text-align:center;'>No existen Registros.</td></tr>");
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



function fn_EliminarUsuario(IdUsuario) {

    //Confirmación
    window.parent.$.confirm({
        title: "CONFIRMACIÓN",
        text: "¿Está seguro que desea eliminar? ",
        confirm: function (button) {

            //Grabado (Ajax)
            parent.fn_util_bloquearPantalla();


            var url = $("#Url_EliminarUsuario").val();
            $.ajax({
                type: 'POST',
                url: url,
                async: true,
                data: { "IdUsuario": IdUsuario },
                datatype: 'JSON',

                success: function (Result) {

                    setTimeout(function () {
                        if (Result.iTipoResultado) {

                            parent.fn_util_desbloquearPantalla();
                            parent.fn_util_MuestraMensaje("", Result.vMensaje, "OK");
                            //Muestra Listado
                            fn_ListarUsuarios();



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