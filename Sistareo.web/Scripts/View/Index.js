$(document).ready(function () {

    var sMensaje = $("#hddMensaje").val();
    if ($.trim(sMensaje).length != 0) {
        fn_util_MuestraMensaje("Exito:", sMensaje, "OK");
    }

    fn_ObtenerInformacionBasica();

    var Ruta = $("#hhdRuta").val();
    $("#ifrIntranet").prop("src", Ruta);

});


function fn_ObtenerInformacionBasica() {

    var url = $("#Url_ObtenerInformacionUsuario").val();
    $.ajax({
        type: 'POST',
        url: url,
        async: true,
    
        datatype: 'JSON',

        success: function (result) {

            var htmlMenu = "";
            var loenMenu = result.loenMenu;
            var oenUsuario = result.oenUsuario;
            var iCodigoModulos = [];
            var Ruta = $("#hddRutaImagesPerfil").val();

            $("#spUsuario").html(oenUsuario.Nombres + ' ' + oenUsuario.Apellidos);
            $("#spRolPrincipal").html(oenUsuario.NombreRol);
           
            $.each(loenMenu, function (index, item) {
                var Modulo = item.IdModulo;
                var posicion = iCodigoModulos.indexOf(Number(Modulo));
                if (posicion == -1) {
                    if (item.vValor == "1") {
                        htmlMenu = htmlMenu + '<li id="' + item.URL + '"><a href="' + item.URL + '"><i class="' + item.EstiloModulo + '"></i><span class="nav-label">' + item.Modulo + '</span></a></li>';
                    } else {
                        htmlMenu = htmlMenu + '<li><a href="#"><i class="' + item.EstiloModulo + '"></i><span class="nav-label">' + item.Modulo + '</span></a>';
                        htmlMenu = htmlMenu + '<ul class="nav nav-second-level">';
                        $.each(loenMenu, function (index1, hijo) {
                            if (hijo.IdModulo == item.IdModulo && hijo.IdTipoOpcionMenu == 1) {
                                htmlMenu = htmlMenu + '<li id="' + hijo.URL + '"><a href="' + hijo.URL + '">' + hijo.Nombre + '</a></li>';
                            }
                        });
                        htmlMenu = htmlMenu + '</ul></li>';
                    }
                    iCodigoModulos.push(Number(Modulo));
                }
            });
            $("#side-menu").append(htmlMenu);
            var url = $("#hddRutaMenu").val();
            $("[id='" + url + "']").addClass("active").parent().parent().addClass("active");
            $("#side-menu").metisMenu();
        },
        error: function (data) {

            //Desbloquear Pantalla
            parent.fn_util_desbloquearPantalla();
            fn_util_MuestraMensaje("ERROR.", "No se pudo obtener la información del usuario.", "E");
        }
    });




}