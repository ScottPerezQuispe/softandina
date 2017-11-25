$(document).ready(function () {

    debugger;
    var FechaActual = $("#FechaActual").val();
    $('#txtFechaInicio').val(FechaActual);
    $('#txtFechaFin').val(FechaActual);

    $('#FechaIni').datetimepicker({
        locale: 'es',
        format: 'DD/MM/YYYY'
        //defaultDate: n
    });

    $('#FechaFin').datetimepicker({
        locale: 'es',
        format: 'DD/MM/YYYY'
        //defaultDate: n
    });
    
    ObtenerOperador();

    ObtenerCampania();
    ObtenerProducto();

    $("#btnConsultar").click(function () {
        fn_Consultar();
    });


    $('input[type=radio][name=optradio]').change(function () {
      
        
      $("#cboCampania").val(0);
       $("#cboOperario").val(0);
       $("#cboProducto").val(0);
 

    });
});


function fn_Consultar() {

    var IdOpcion = $('input:radio[name=optradio]:checked').val();
    var IdCampania = $("#cboCampania").val();
    var IdOperario = $("#cboOperario").val();
    var IdProducto = $("#cboProducto").val();
    var FechaInicio = $("#txtFechaInicio").val();
    var FechaFin = $("#txtFechaFin").val();


     //Bloquear Pantalla
    parent.fn_util_bloquearPantalla();

    var url = $("#Url_ConsultarRetoque").val();


    $.ajax({
        type: "POST",
        url: url,
        async: true,
        data: { IdOpcion: IdOpcion, IdCampania: IdCampania, IdOperario: IdOperario, IdProducto: IdProducto, FechaInicio: FechaInicio, FechaFin: FechaFin },
        dataType: "JSON",

        success: function (Result) {

            //setTimeout(function () {
            if (Result.iTipoResultado == 1) {
                $("#ifrReport").attr("src", "/Reports/RptRetoqueDiseño.aspx");
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

//Permite obtener la lista de los operadores s
function ObtenerOperador() {
    //Bloquear Pantalla
    parent.fn_util_bloquearPantalla();

    var url = $("#Url_ListarTodoOperador").val();
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


//Permite obtener la lista de los Campaña s
function ObtenerCampania() {
    //Bloquear Pantalla
    parent.fn_util_bloquearPantalla();

    var url = $("#Url_ListarTodoCampania").val();
    var Cuenta = $('#cboCampania');

    $.ajax({
        type: "GET",
        url: url,
        async: true,
        dataType: "json",
        cache: false,
        success: function (Result) {

            //setTimeout(function () {
            if (Result.iTipoResultado == 1) {
                var lstLista = Result.ListaCampania
                $.each(lstLista, function (index, item) {
                    $("#cboCampania").append("<option value=" + this.IdCampania + ">" + this.Nombre + "</option>");
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



//Permite obtener la lista de los Producto
function ObtenerProducto() {
    var url = $("#Url_ListarTodoProducto").val();
    var Cuenta = $('#cboProducto');

    $.ajax({
        type: "GET",
        url: url,
        async: true,
        dataType: "json",
        cache: false,
        success: function (Result) {
            if (Result.iTipoResultado == 1) {
                var lstLista = Result.ListaProducto
                $.each(lstLista, function (index, item) {
                    //$(document.createElement('option'))
                    //    .attr('value', this.IdProducto)
                    //    .text(this.DescripcionProducto)
                    //    .appendTo(Cuenta);
                    $("#cboProducto").append("<option value=" + this.IdProducto + ">" + this.DescripcionProducto + "</option>");

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