String.prototype.toRequest = function (options) {
    var s = this;

    $("#espera").modal({
        backdrop: 'static'
        , keyboard: false
        , show: true
    });

    var onSuccess = options.success || function (d) {
        toastr["success"]('Con Exito');
    };

    var onError = options.error || function (e) {
        toastr.error(e.responseText, "Error");
    };

    options = $.extend({
        url: s
        , type: 'post'
        , contentType: 'application/json; charset=utf-8'
        , dataType: 'json'
        , cache: false
        , async: false
    },
    options
    );

    delete options.success;
    delete options.error;

    var promise = $.ajax(options);
    promise.done(onSuccess)
    promise.fail(onError)
    promise.always(function () {
        $("#espera").modal('hide');
    });
};

String.prototype.PadLeft = function (c, l) {
    var s = this;
    for (var i = 0; s.length < l; i++) {
        s = c + s;
    }
    return s.toString();
};

var apiRemote = {
    base: 'ControlSacrificio.aspx/'
};


apiRemote = $.extend({
    Transferencia: function () { return apiRemote.base + 'Transferencia' }
    , Obtener_Aretes_Corral: apiRemote.base + 'Obtener_Aretes_Corral'
    , ObtenerCorrales: apiRemote.base + 'Obtener_Corrales'
    , ReactivaAnimal: apiRemote.base + 'Reactiva_Animal'
    , InctivarAnimal: apiRemote.base + 'Inactivar_Animal'
    , PlanchadoArete: apiRemote.base + 'Planchado_Arete'
    , PlanchadoAreteLote: apiRemote.base + 'Planchado_AreteLote'
    , ReemplazoArete: apiRemote.base + 'Reemplazo_Arete'
    , ValidarLoteActivo: apiRemote.base + 'Valida_Lote_Activo'
    , ValidarCorralCompleto: apiRemote.base + 'Valida_Corral_Completo'
    , ActivarLote: apiRemote.base + 'Activar_Lote'
    , ObtenerSacrificio: function () { return apiRemote.base + 'Obtener_Sacrificio'; }
}, apiRemote);
