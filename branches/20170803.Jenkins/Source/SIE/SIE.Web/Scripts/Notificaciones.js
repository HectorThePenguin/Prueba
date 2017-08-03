/// <reference path="../assets/plugins/jquery-1.7.1-vsdoc.js" />
/// <reference path="../assets/plugins/jquery-linq/linq-vsdoc.js" />
/// <reference path="~/assets/plugins/bootstrap-bootbox/js/bootbox.min.js" />
/// <reference path="~/Scripts/jscomun.js" />

jQuery(document).ready(function() {

    BloquearPantalla();
    obtenerNotificacionesAutorizadas();

});

obtenerNotificacionesAutorizadas = function () {
    EjecutarWebMethod(window.location.pathname + '/ObtenerNotificacionesAutorizadas', {}, desplegarNotificacionesAutorizadas
                    , "Ocurrió un error al consultar las notificaciones");
};

desplegarNotificacionesAutorizadas = function(msg) {
    if (msg.d == null || msg.d.length == 0) {
        bootbox.dialog({
            message: "No tiene notificaciones pendiente de revisar.",
            buttons: {
                Aceptar: {
                    label: "Ok",
                    callback: function () {
                        DesbloquearPantalla();
                    }
                }
            }
        });
        return;
    }
    var notifiaciones = msg.d;
    var valores = {};
    
    var recursos = {};

    recursos.Ticket= "Ticket Boleta";
    recursos.Fecha = "Fecha";
    recursos.Producto = "Producto";
    recursos.Proveedor = "Proveedor";
    recursos.VerBoleta = "Ver Boleta";
    
    valores.Recursos = recursos;

    var notifiacionesAutorizadas = new Array();
    for (var i = 0; i < notifiaciones.length; i++) {
        var valor = { };
        valor.Folio = notifiaciones[i].Folio;
        valor.Fecha = FechaFormateada(new Date(parseInt(notifiaciones[i].Fecha.replace(/^\D+/g, ''))));
        valor.Producto = notifiaciones[i].Producto.Descripcion;
        valor.Proveedor = notifiaciones[i].Contrato.Proveedor.Descripcion;
        valor.Imagen = "../Images/menu/Notificacion32.png";
        valor.EntradaProductoID = notifiaciones[i].EntradaProductoId;
        
        notifiacionesAutorizadas.push(valor);
    }
    valores.Notifiaciones = notifiacionesAutorizadas;

    $('#divGridNotificacionesAutorizadas').html('');
    $('#divGridNotificacionesAutorizadas').setTemplateURL('../Templates/GridNotificacionesAutorizadas.htm');
    $('#divGridNotificacionesAutorizadas').processTemplate(valores);
    
    DesbloquearPantalla();
};
