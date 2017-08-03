/// <reference path="../assets/plugins/jquery-1.7.1-vsdoc.js" />
/// <reference path="../assets/plugins/jquery-linq/linq-vsdoc.js" />
/// <reference path="~/assets/plugins/bootstrap-bootbox/js/bootbox.min.js" />

obtenerMenu = function () {
    $.ajax({
        type: "POST",
        url: 'Principal.aspx/CargarMenu',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == null || msg.d.length == 0) {
                bootbox.alert('El usuario no tiene opciones asignadas');
                return;
            }
            var opcionesMenu = msg.d;
            var conceptos = Enumerable.From(opcionesMenu).OrderBy(function (x) { return x.OrdenFormulario; }).Select(function (x) { return x; }).ToArray();
            var modulos = Enumerable.From(opcionesMenu).OrderBy(function(x) { return x.OrdenModulo; }).Select(function(x) { return x.Modulo; }).Distinct().ToArray();
            var menu = {};
            menu.Opciones = conceptos;
            menu.Modulos = modulos;

            $('#Menu').html('');
            $('#Menu').setTemplateURL('Templates/Menu.htm');
            $('#Menu').processTemplate(menu);
            
            App.init();
        },
        error: function () {
            bootbox.alert('Error al consultar las opciones de Menu');
        }
    });
}

cargarPagina = function (pagina) {
    //$("#body").html('<object data="' + pagina + '" style="position:relative">');
    $("#body").load(pagina);
}
