$(document).ready(function () {
    ObtenerProducto();
});

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