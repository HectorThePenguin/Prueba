/// <reference path="../Manejo/Scripts/bootstrap.min.js" />
/// <reference path="../Manejo/Scripts/jquery-2.1.4.min.js" />
/// <reference path="../Manejo/Scripts/json3.min.js" />
/// <reference path="../Manejo/Scripts/knockout-3.3.0.js" />
/// <reference path="../Manejo/Scripts/linq.min.js" />
/// <reference path="../Manejo/Scripts/moment-with-locales.min.js" />
/// <reference path="../Manejo/Scripts/moment.min.js" />
/// <reference path="../Manejo/Scripts/toastr.min.js" />
/// <reference path="../Manejo/Scripts/bootstrap.min.js" />


/***************************************************************
*                    G   L   O   B   A   L                     
****************************************************************/
var init = function () {
    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-bottom-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };
};

var initPin = function () {
    var clonedHeaderRow;

    $(".pin-area").each(function () {
        clonedHeaderRow = $(".pin-frame", this);
        clonedHeaderRow
          .before(clonedHeaderRow.clone())
          .css("width", clonedHeaderRow.width())
          .addClass("floatingPin");
    });

    $(window)
     .scroll(UpdateTableHeaders)
     .trigger("scroll")
     .resize(ResizeHeaders);
};

var ResizeHeaders = function () {
    var clonedHeaderRow;
    var clonedHeaderRowCloned;

    $(".pin-area").each(function () {
        clonedHeaderRow = $(".pin-frame", this).first();
        var clonedHeaderRowCloned = $(".floatingPin", this);
        clonedHeaderRowCloned
            .css("width", clonedHeaderRow.width());
    });
}

var UpdateTableHeaders = function () {
    $(".pin-area").each(function () {

        var el = $(this),
            offset = el.offset(),
            scrollTop = $(window).scrollTop(),
            floatingPin = $(".floatingPin", this)

        if ((scrollTop > offset.top) && (scrollTop < offset.top + el.height())) {
            floatingPin.css({
                "visibility": "visible"
            });
        } else {
            floatingPin.css({
                "visibility": "hidden"
            });
        };
    });
}

var Peticion = function () {
    var s = this;

    s.UsuarioID = ko.observable();
    s.OrganizacionID = ko.observable();
    s.Token = ko.observable();

    return s;
}

/*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
+                       R E Q U E S T S                        +
+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
var Transferencia_Request = function () {
    var s = this;

    s.AnimalID = ko.observable();
    s.LoteID = ko.observable();
    s.Corral = ko.observable();

    return s;
};


var ObtenerCorrales_Request = function () {
    var s = this;

    s.OrganizacionId = ko.observable();

    return s;
};


var ObtenerAretes_Request = function () {
    var s = this;

    s.LoteId = ko.observable();

    return s;
};


var PlanchadoArete_Request = function () {
    var s = this;

    s.AnimalId = ko.observable();
    s.Arete = ko.observable();
    s.LoteID = ko.observable();
    s.Corral = ko.observable();

    return s;
};

var Planchado_Arete_Lote_Request = function () {
    var s = this;

    s.Sacrificio = ko.observableArray([]);
    s.Animal = ko.observableArray([]);
    s.FechaSacrificio = ko.observable();

    return s;
};

var ReemplazoArete_Request = function () {
    var s = this;

    s.AnimalId = ko.observable();
    s.LoteId = ko.observable();

    return s;
};


var ReactivaAnimal_Request = function () {
    var s = this;

    s.AnimalId = ko.observable();

    return s;
};


var ValidarLoteActivo_Request = function () {
    var s = this;

    s.LoteId = ko.observable();

    return s;
};


var ValidarCorralCompleto_Request = function () {
    var s = this;

    s.LoteId = ko.observable();
    return s;
};


var InctivarAnimal_Request = function () {
    var s = this;

    s.AnimalId = ko.observable();

    return s;
};


var ActivarLote_Request = function () {
    var s = this;

    s.LoteId = ko.observable();
    return s;
};

var ObtenerSacrificio_Request = function () {
    var s = this;

    s.OrganizacionID = ko.observable();
    s.FechaSacrificio = ko.observable();
    return s;
};



/***************************************************************
*                         C L A S E S                          *
***************************************************************/
/*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
+             C L A S E      I N D I C A D O R E S             +
++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
var indicadoresClass = function () {
    var s = this;

    s.noqueo = ko.observableArray();
    s.pieleSangre = ko.observableArray();
    s.pielDescarnada = ko.observableArray();
    s.inspeccion = ko.observableArray();
    s.canalCompleta = ko.observableArray();
    s.canalCaliente = ko.observableArray();
    s.viscera = ko.observableArray();

    return s;
};

/*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
+              C L A S E      S A C R I F I C I O              +
+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
var sacrificioClass = function () {
    var s = this;

    s.fechaSacrificio = ko.observableArray();
    s.organziacionId = ko.observableArray();
    s.consecutivo = ko.observableArray();
    s.po = ko.observableArray();
    s.corral = ko.observableArray();
    s.corralSalida = ko.observableArray();
    s.arete = ko.observableArray();
    s.acciones = ko.observableArray();

    var indicadores = new indicadoresClass();

    return s;
};

var peticion = new Peticion();

var AnimalSacrificado = function (m) {
    var s = m || this;

    s.CodigoCorral = ko.observable();
    s.LoteIdSiap = ko.observable();
    s.Lote = ko.observable();
    s.Arete = ko.observable();
    s.FechaSacrificio = ko.observable();
    s.NumeroCorral = ko.observable();
    s.NumeroProceso = ko.observable();
    s.TipoGanado = ko.observable();
    s.TipoGanadoId = ko.observable();
    s.LoteId = ko.observable();
    s.AnimalId = ko.observable();
    s.AnimalIdSiap = ko.observable();
    s.CorralInnova = ko.observable();
    s.Po = ko.observable();
    s.Consecutivo = ko.observable();
    s.Noqueo = ko.observable();
    s.PielSangre = ko.observable();
    s.PielDescarnada = ko.observable();
    s.Viscera = ko.observable();
    s.Inspeccion = ko.observable();
    s.CanalCompleta = ko.observable();
    s.CanalCaliente = ko.observable();

    return s;
};

var Sacrificio = function (m) {
    var s = m || this;

    s.Fecha = ko.observable(null);
    s.Organizacion = ko.observable();
    s.Total = ko.observable(0);
    s.Procesadas = ko.observable(0);
    s.Sacrificio = ko.observableArray([]);

    return s;
};

/***************************************************************
* REMOTO
****************************************************************/

var FuncionesRemotas = function (m) {
    var s = m;

    s.Transferencia = function (data, callback, error) {
        peticion.Datos = ko.observable(data);
        var req = {
            req: ko.observable(peticion)
        };
        apiRemote.Transferencia().toRequest({
            data: ko.toJSON(req),
            success: callback,
            error: error
        });
    };

    s.Obtener_Aretes_Corral = function (data, callback, error) {
        peticion.Datos = ko.observable(data);
        var req = {
            req: ko.observable(peticion)
        };

        apiRemote.Obtener_Aretes_Corral.toRequest({
            data: ko.toJSON(req),
            success: callback,
            error: error
        });
    };

    s.ObtenerCorrales = function (data, callback, error) {
        peticion.Datos = ko.observable(data);
        var req = {
            req: ko.observable(peticion)
        };
        apiRemote.ObtenerCorrales.toRequest({
            data: ko.toJSON(req),
            success: callback,
            error: error
        });
    };

    s.PlanchadoArete = function (data, callback, error) {
        peticion.Datos = ko.observable(data);
        var req = {
            req: ko.observable(peticion)
        };
        apiRemote.PlanchadoArete.toRequest({
            data: ko.toJSON(req),
            success: callback,
            error: error
        });
    };

    s.PlanchadoAreteLote = function (data, callback, error) {
        peticion.Datos = ko.observable(data);
        var req = {
            req: ko.observable(peticion)
        };
        apiRemote.PlanchadoAreteLote.toRequest({
            data: ko.toJSON(req),
            success: callback,
            error: error
        });
    };

    s.ReemplazoArete = function (data, callback, error) {
        peticion.Datos = ko.observable(data);
        var req = {
            req: ko.observable(peticion)
        };
        apiRemote.ReemplazoArete.toRequest({
            data: ko.toJSON(req),
            success: callback,
            error: error
        });
    };

    s.ReactivaAnimal = function (data, callback, error) {
        peticion.Datos = ko.observable(data);
        var req = {
            req: ko.observable(peticion)
        };
        apiRemote.ReactivaAnimal.toRequest({
            data: ko.toJSON(req),
            success: callback,
            error: error
        });
    };

    s.ValidarLoteActivo = function (data, callback, error) {
        peticion.Datos = ko.observable(data);
        var req = {
            req: ko.observable(peticion)
        };
        apiRemote.ValidarLoteActivo.toRequest({
            data: ko.toJSON(req),
            success: callback,
            error: error
        });
    };

    s.ValidarCorralCompleto = function (data, callback, error) {
        peticion.Datos = ko.observable(data);
        var req = {
            req: ko.observable(peticion)
        };
        apiRemote.ValidarCorralCompleto.toRequest({
            data: ko.toJSON(req),
            success: callback,
            error: error
        });
    };

    s.InctivarAnimal = function (data, callback, error) {
        peticion.Datos = ko.observable(data);
        var req = {
            req: ko.observable(peticion)
        };
        apiRemote.InctivarAnimal.toRequest({
            data: ko.toJSON(req),
            success: callback,
            error: error
        });
    };

    s.ActivarLote = function (data, callback, error) {
        peticion.Datos = ko.observable(data);
        var req = {
            req: ko.observable(peticion)
        };
        apiRemote.ActivarLote.toRequest({
            data: ko.toJSON(req),
            success: callback,
            error: error
        });
    };

    s.ObtenerSacrificio = function (data, callback, error) {
        peticion.Datos = ko.observable(data);
        var req = {
            req: ko.observable(peticion)
        };

        apiRemote.ObtenerSacrificio().toRequest({
            data: ko.toJSON(req),
            success: callback,
            error: error
        });
    };
};


/***************************************************************
*                         L  O  C  A  L 
****************************************************************/
var ExcepcionesController = function (m) {
    var s = m || this;

    new FuncionesRemotas(s);

    var TransferirCorral = {
        ConConflicto: function () {
            var b = s.LoteIdSiap() ? s.LoteIdSiap() == s.LoteId() : true;
            return !b;
        }
        , Corregir: function (callback) {
            new TransferenciaModel(s).Save(function (d) {
                if (d.d.Resultado) {
                    callback();
                    s.LoteIdSiap(s.LoteId());
                    s.CodigoCorral(s.NumeroCorral());
                    s.AnimalID(s.AnimalID);
                    toastr["success"]("Animal transferido con éxito");
                }
                else {
                    toastr["error"](d.d.Mensaje);
                }
            });
        }
    };

    var Planchado = {
        ConConflicto: function () {
            var b = !(
                      (!$.AnimalIdSiap && s.Noqueo() && s.PielSangre() && s.PielDescarnada() && s.Inspeccion() && s.CanalCompleta() && s.CanalCaliente() && s.Viscera()) 
                      ||
                      (!($.AnimalIdSiap && $.Noqueo && $.PielSangre && $.PielDescarnada && $.Inspeccion && $.CanalCompleta && $.CanalCaliente && $.Viscera))
                     );
            return !b;
        }
        , Corregir: function (callback) {
            $("#planchado").modal();
            s.cTransferencia.ePlanchado.AretesSinRelacion.removeAll();
            s.cTransferencia.ePlanchado.RelacionAretes.removeAll();
            s.cTransferencia.ePlanchado.ColeccionAGrabar([]);

            var array = ko.toJS(modelSacrificio.Sacrificio());
        
            var yes = Enumerable.From(array)
                .Where("!$.AnimalIdSiap && $.Noqueo && $.PielSangre && $.PielDescarnada && $.Inspeccion && $.CanalCompleta && $.CanalCaliente && $.Viscera")
                .Select(function (x) {
                    return {
                        Arete: x.Arete,
                        Corral: x.NumeroCorral,
                        LoteId: x.LoteId,
                        LoteIdSiap: x.LoteIdSiap,
                        AnimalId: x.AnimalId,
                        AnimalIdSiap: x.AnimalIdSiap
                    }
                }).ToArray();

            var no = Enumerable.From(array)
                .Where("$.AnimalIdSiap && !($.Noqueo && $.PielSangre && $.PielDescarnada && $.Inspeccion && $.CanalCompleta && $.CanalCaliente && $.Viscera)")
                .Select(function (x) {
                    return {
                        Arete: x.Arete,
                        Corral: x.NumeroCorral,
                        LoteId: x.LoteId,
                        LoteIdSiap: x.LoteIdSiap,
                        AnimalId: x.AnimalId,
                        AnimalIdSiap: x.AnimalIdSiap
                    }
                }).ToArray();

            var gy = Enumerable.From(yes).GroupBy("$.LoteId").Select(function (g) {
                return g.Select(function (x, i) {
                    x.Indice = i + 1;
                    return x;
                }).ToArray();
            }).SelectMany("$").ToArray();

            s.cTransferencia.ePlanchado.gpoYes(gy);
            
            var gn = Enumerable.From(no).GroupBy("$.LoteId").Select(function (g) {
                return g.Select(function (x, i) {
                    x.Indice = i + 1;
                    return x;
                }).ToArray();
            }).SelectMany("$").ToArray();

            s.cTransferencia.ePlanchado.gpoNo(gn);

            var relacion = Enumerable
              .From(s.cTransferencia.ePlanchado.gpoYes())
              .Join(s.cTransferencia.ePlanchado.gpoNo()
                    , function (x) { return x.LoteId.toString().PadLeft('0', 10) + x.Indice.toString().PadLeft('0', 3); }
                    , function (x) { return x.LoteId.toString().PadLeft('0', 10) + x.Indice.toString().PadLeft('0', 3); }
                    , "a,b=> { loteid: a.LoteId, consecutivo: a.Indice, arete: a.Arete, areteViejo: b.Arete, animalid: b.AnimalIdSiap, corral: b.Corral }")
              .ToArray();

            s.cTransferencia.ePlanchado.RelacionAretes(relacion);
            

            var sry = Enumerable
                    .From(relacion)
                    .Join(s.cTransferencia.ePlanchado.gpoYes()
                            , "$.arete"
                            , "$.Arete"
                            , "a,b=>b")
                    .ToArray();

            s.cTransferencia.ePlanchado.sinRelacionYes(sry);

            var srn = Enumerable
                    .From(relacion)
                    .Join(s.cTransferencia.ePlanchado.gpoNo()
                            , "$.areteViejo"
                            , "$.Arete"
                            , "a,b=>b")
                    .ToArray();

            s.cTransferencia.ePlanchado.sinRelacionNo(srn);

            s.cTransferencia.ePlanchado.gpoYes.removeAll(s.cTransferencia.ePlanchado.sinRelacionYes());
            var sinRelacion = s.cTransferencia.ePlanchado.gpoYes;
            s.cTransferencia.ePlanchado.gpoNo.removeAll(s.cTransferencia.ePlanchado.sinRelacionNo());

            Enumerable
                .From(s.cTransferencia.ePlanchado.gpoNo())
                .ForEach( function (gn) {
                    sinRelacion.push(gn);
                });

            s.cTransferencia.ePlanchado.AretesSinRelacion(sinRelacion());

        }
    };

    var Excepciones = [TransferirCorral, Planchado];

    s.MostrarExcepcion = function (callback) {
        var excepcion = Enumerable
            .From(Excepciones)
            .Where("$.ConConflicto()")
            .FirstOrDefault();

        if (excepcion)
            excepcion.Corregir(callback);
    };

    return s;
};

var FuncionesLocales = function (m) {
    var s = m;

    s.Cnt = new ExcepcionesController(s);

    s.Filtrar = function () {
        s.Filtrado(!s.Filtrado());
    };

    s.Corregir = function () {
        s.Cnt.MostrarExcepcion(function (d) {
            s.Noqueo(true);
            s.PielSangre(true);
            s.PielDescarnada(true);
            s.Inspeccion(true);
            s.CanalCompleta(true);
            s.CanalCaliente(true);
            s.Viscera(true);
        });
    };
};

/***************************************************************
*                        M  O  D  E  L  O
****************************************************************/
var ChildModel = function (p) {
    var s = this;

    new AnimalSacrificado(s);

    var iClass = {
        fa: "fa fa-"
        , ok: "check text-success"
        , fail: "close text-danger"
    };

    s.iNoqueo = ko.computed(function () { return iClass.fa + (s.Noqueo() ? iClass.ok : iClass.fail); });
    s.iPielEnSangre = ko.computed(function () { return iClass.fa + (s.PielSangre() ? iClass.ok : iClass.fail); });
    s.iPielDescarnada = ko.computed(function () { return iClass.fa + (s.PielDescarnada() ? iClass.ok : iClass.fail); });
    s.iInspeccion = ko.computed(function () { return iClass.fa + (s.Inspeccion() ? iClass.ok : iClass.fail); });
    s.iCanalCompleta = ko.computed(function () { return iClass.fa + (s.CanalCompleta() ? iClass.ok : iClass.fail); });
    s.iCanalCaliente = ko.computed(function () { return iClass.fa + (s.CanalCaliente() ? iClass.ok : iClass.fail); });
    s.iVisceras = ko.computed(function () { return iClass.fa + (s.Viscera() ? iClass.ok : iClass.fail); });

    s.cTransferencia = p.eTransferencia;

    s.ConConflicto = ko.computed(function () {
        var b = 
            s.Noqueo() && s.PielSangre() && s.PielDescarnada() && s.Inspeccion() && s.CanalCompleta() && s.CanalCaliente() && s.Viscera() 
            && s.LoteIdSiap()
            && s.LoteId() == s.LoteIdSiap();
        return !b;
    });

    s.enlaceFiltrado = ko.computed(function () {
        return p.Filtrado() ? s.ConConflicto() : true;
    });

    new FuncionesLocales(s);

    return s;
};

var TransferenciaModel = function (p) {
    var s = p || this;
    s.Titulo = ko.observable("Transferir arete hacia Corral");
    s.Save = function (callback) {
        var param = new Transferencia_Request();
        param.AnimalID(s.AnimalIdSiap());
        param.LoteID(s.LoteId());
        param.Corral(s.CodigoCorral());
        s.Transferencia(param, callback);
    };
    return s;
};

var PlanchadoModel = function (p) {
    var s = p || this;

    s.Titulo = ko.observable("Planchado de Aretes por Lote");
    s.LoteIdSiap = ko.observable();
    s.RelacionAretes = ko.observableArray([]);
    s.ColeccionAGrabar = ko.observableArray([]);
    s.AretesSinRelacion = ko.observableArray([]);

    s.sinRelacionYes = ko.observableArray([]);
    s.sinRelacionNo = ko.observableArray([]);

    s.gpoYes = ko.observableArray([]);
    s.gpoNo  = ko.observableArray([]);


    s.PlanchadoAreteIndividual = function (x) {
        s.ColeccionAGrabar().push({
            animalid: x.animalid,
            arete: x.arete,
            corral: x.corral,
            loteid: x.loteid
        });
        SavePlanchado();
    };

    s.PlanchadoAretesCorral = function () {
        var coleccion = Enumerable
                        .From(ko.toJS(s.RelacionAretes()))
                        .Select(function (ra) {
                            return {
                                animalid: ra.animalid,
                                arete: ra.arete,
                                corral: ra.corral,
                                loteid: ra.loteid
                            }
                        }).ToArray();

        s.ColeccionAGrabar(coleccion);
        SavePlanchado();
    };

    s.PlanchadoAreteActivoLote = function () {

        var sacrificio = Enumerable
                        .From(s.Sacrificio())
                        .Where("$.AnimalId")
                        .Select("{AnimalId: $.AnimalId, LoteId: $.LoteId, Arete: $.Arete}")
                        .ToArray();

        var animal = Enumerable
            .From(s.AretesSinRelacion())
            .Select("{Arete:$.Arete, LoteId:$.LoteId}")
            .ToArray();

        var param = new Planchado_Arete_Lote_Request();

        param.Sacrificio(sacrificio);
        param.Animal(animal);
        param.FechaSacrificio(s.fechaSacrificio());

        s.PlanchadoAreteLote(ko.toJS(param), mapearAreteLote);
    };

    var mapearAreteLote = function (d) {

        if (d.d.Resultado) {
            var relacion = Enumerable
                        .From(d.d.Datos)
                        .Select("{ animalid: $.AnimalID, arete: $.Arete, loteid: $.LoteID, corral: $.Corral }")
                        .ToArray();


            if (Enumerable.From(relacion).Where("!$.animalid").Count() > 0)
            { toastr["warning"]("Los aretes en la orden de sacrifcio pendientes de programa son menos a los aretes pendientes a Planchar"); }
            else
            {
                relacion = Enumerable.From(relacion).Where("$.AnimalId").ToArray();

                s.ColeccionAGrabar(relacion);
                SavePlanchado();
            }
        }
        else { toastr["error"](d.d.Mensaje) };
    }

    var SavePlanchado = function ()
    {
        var param = s.ColeccionAGrabar().map(function (obj) {
            var ret = new PlanchadoArete_Request();

            ret.AnimalId(obj.animalid);
            ret.Arete(obj.arete);
            ret.LoteID(obj.loteid);
            ret.Corral(obj.corral);

            return ret;
        });

        s.PlanchadoArete(ko.toJS(param), ajustarPantallas);

    };


    var ajustarPantallas = function (d) {

        if (d.d.Resultado) {
            ajustarRealcion(d);

            ajustarSinRealcion(d);
        }
        else { toastr["error"](d.d.Mensaje)};
    };

   var ajustarRealcion = function (d) {
        
        var identifica = Enumerable
                        .From(s.ColeccionAGrabar())
                        .Join(s.RelacionAretes(),
                            "$.arete",
                            "$.arete",
                            "a,b=>b")
                        .ToArray();

        s.RelacionAretes.removeAll(identifica);

        identifica.length>0?ajustarSacrificioRelacion(d):true;
        
    };

   var ajustarSacrificioRelacion = function (d) {

       var ficticio = Enumerable
       .From(s.ColeccionAGrabar())
       .Join(s.Sacrificio(),
             "$.arete",
             "$.Arete()",
             "a,b=>b")
       .Where("$.AnimalId")
       .ToArray();

       //Eliminando el arete SIAP de la lista
       s.Sacrificio.removeAll(ficticio);

       Enumerable
       .From(s.Sacrificio())
       .Join(d.d.Datos, "$.AnimalId()", "$.AnimalID", "a,b=>a")
       .ForEach(function (a) {

           var animal = Enumerable
               .From(d.d.Datos)
               .Where("$.AnimalID =='" + a.AnimalId() + "'").FirstOrDefault();

           a.Arete(animal.Arete);
           a.CodigoCorral(a.NumeroCorral());
           a.Noqueo(true);
           a.PielSangre(true);
           a.PielDescarnada(true);
           a.Viscera(true);
           a.Inspeccion(true);
           a.CanalCompleta(true);
           a.CanalCaliente(true);

       });

       s.calculoTotales();

   };

    var ajustarSinRealcion = function (d) {

        var identifica = Enumerable
                        .From(s.ColeccionAGrabar())
                        .Join(s.AretesSinRelacion(),
                            "$.arete",
                            "$.Arete",
                            "a,b=>b")
                        .ToArray();

        s.AretesSinRelacion.removeAll(identifica);

        //identifica.length>0 ? s.ColeccionAGrabar([]):true;

        identifica.length > 0 ? ajustarSacrificioSinRelacion(d) : true;

    }

    var ajustarSacrificioSinRelacion = function (d) {

        var ficticio = Enumerable
        .From(s.ColeccionAGrabar())
        .Join(s.Sacrificio(),
              "$.arete",
              "$.Arete()",
              "a,b=>b")
        .Where("$.AnimalId")
        .ToArray();

        Enumerable
        .From(s.Sacrificio())
        .Join(d.d.Datos, "$.Arete()", "$.Arete", "a,b=>a")
        .ForEach(function (a) {

            var animal = Enumerable
                .From(d.d.Datos)
                .Where("$.Arete =='" + a.Arete() + "'").FirstOrDefault();

            a.AnimalId(animal.AnimalID);
            a.CodigoCorral(a.NumeroCorral());
        });

        s.calculoTotales();
    };

    return s;
};


var ListadoSimpleModel = function (p) {
    var s = p || this;

    new FuncionesRemotas(s);

    s.acciones = ko.observable();
    s.ObjetoOrigen = ko.observable();

    var titulo_ls;
    switch (s.acciones()) {
        case "Planchado":
            titulo_ls = "Planchado de Aretes";
            break;
        case "Reemplazo":
            titulo_ls = "Reemplazo de Aretes";
            break;
    };

    s.pl_titulo = ko.observable(titulo_ls);
    s.pl_arete = ko.observableArray([]);
    s.animalID_ls = ko.observable();


    s.ListadoSimple_Save = function () {
        switch (s.acciones()) {
            case "Planchado":

                s.PlanchadoArete(s.animalID_ls(), function () {
                    s.ObjetoOrigen.CanalCompleta(true);
                    s.ObjetoOrigen.CanalCaliente(true);
                    s.ObjetoOrigen.Visceras(true);

                    toastr["Error"]("Planchado de arete exitoso");

                },
                function () {
                    toastr["Error"]("Ocurrio un error al Planchar el arete");
                });

                break;
            case "Reemplazo":
                s.ReemplazoArete(s.AnimalID(), function () {
                    s.ObjetoOrigen.PielDescarnada(true);

                    toastr["Error"]("Reemplazo de arete exitoso");
                },
                function () {
                    toastr["Error"]("Ocurrio un error al reemplazar el arete");
                });

                break;
        };
    };

    return s;
};

var Modelo = function () {
    var s = this;

    new Sacrificio(s);

    s.Estatus = ko.observable();

    s.EstatusClass = ko.computed(function () {
        return s.Procesadas() == s.Total() ? "text-success" : "text-danger";
    });

    s.eTransferencia = new TransferenciaModel(s);
    s.eListadoSimple = new ListadoSimpleModel(s);
    s.ePlanchado = new PlanchadoModel(s);

    s.fechaSacrificio = ko.observable();

    s.Filtrado = ko.observable(false);

    s.FiltradoClass = ko.computed(function () {
        var clase = "form-control btn ";
        return s.Filtrado() ? clase + "btn-danger" : clase + "btn-success";
    });

    s.FiltradoTexto = ko.computed(function () {
        return s.Filtrado() ? Etiquetas.Conflictos : Etiquetas.Todos;
    });

    s.Organizacion = ko.observable();

    s.calculoTotales = function () {

        s.Total(Enumerable.From(s.Sacrificio.peek()).Count());
        s.Procesadas(Enumerable.From(s.Sacrificio.peek()).Where("!$.ConConflicto()").Count());
        s.Procesadas() == s.Total() ? s.Estatus(Etiquetas.Completo) : s.Estatus(Etiquetas.Incompleto);

    };

    s.ConsultaSacrificio = function () {

        var param = new ObtenerSacrificio_Request();
        param.OrganizacionID = 0;
        param.FechaSacrificio(moment(s.fechaSacrificio()).format('YYYY/MM/DD'));

        s.ObtenerSacrificio(param, function (d) {
            if (d.d.Resultado) {
                if (d.d.Datos.Sacrificio != null) {
                    s.Organizacion(d.d.Datos.Organizacion);

                    var info = d.d.Datos.Sacrificio.map(function (obj) {
                        var ret = new ChildModel(s);

                        ret.CodigoCorral(obj.CodigoCorral);
                        ret.LoteIdSiap(obj.LoteIdSiap);
                        ret.Lote(obj.Lote);
                        ret.Arete(obj.Arete);
                        ret.FechaSacrificio(obj.FechaSacrificio);
                        ret.NumeroCorral(obj.NumeroCorral);
                        ret.NumeroProceso(obj.NumeroProceso);
                        //ret.TipoGanado(obj.TipoGanado);
                        ret.TipoGanado(obj.TipoGanadoId);
                        ret.TipoGanadoId(obj.TipoGanadoId);
                        ret.LoteId(obj.LoteId);
                        ret.AnimalId(obj.AnimalId);
                        ret.AnimalIdSiap(obj.AnimalId);
                        ret.CorralInnova(obj.CorralInnova);
                        ret.Po(obj.Po);
                        ret.Consecutivo(obj.Consecutivo);
                        ret.Noqueo(obj.Noqueo);
                        ret.PielSangre(obj.PielSangre);
                        ret.PielDescarnada(obj.PielDescarnada);
                        ret.Viscera(obj.Viscera);
                        ret.Inspeccion(obj.Inspeccion);
                        ret.CanalCompleta(obj.CanalCompleta);
                        ret.CanalCaliente(obj.CanalCaliente);

                        return ret;
                    });

                    s.Sacrificio(info);

                    s.calculoTotales();
                };
            }
            else {
                toastr["warning"](d.d.Mensaje);
            };
        }, function (e) {
            toastr["error"]("Error al Consultar Sacrificio");
        });
    };



    new FuncionesRemotas(s);
    new FuncionesLocales(s);

    return s;
};

var modelSacrificio = undefined;

/****************************************************************
*                          P r u e b a s                        *
****************************************************************/
$(function () {
    init();

    //make the header fixed on scroll
    //$('.table-fixed-header').fixedHeader();

    modelSacrificio = new Modelo();

    ko.applyBindings(modelSacrificio);


    //var x1 = new ChildModel(modelSacrificio);
    //x1.Arete("99999");
    //x1.LoteId(131231);
    //x1.CodigoCorral("f54");
    //x1.LoteIdSiap("3123");
    //x1.Lote("2132");
    //x1.FechaSacrificio("201513123");
    //x1.NumeroCorral("F33");
    //x1.NumeroProceso("1312");
    //x1.TipoGanado("1");
    //x1.TipoGanadoId("1");
    //x1.AnimalId();
    //x1.AnimalIdSiap(3121);
    //x1.CorralInnova("13ASD");
    //x1.Po("3423");
    //x1.Consecutivo(1);
    //x1.Noqueo(true);
    //x1.PielSangre(true);
    //x1.PielDescarnada(true);
    //x1.Viscera(true);
    //x1.Inspeccion(true);
    //x1.CanalCompleta(true);
    //x1.CanalCaliente(true);

    //modelSacrificio.Sacrificio.push(x1);


    //var x1 = new ChildModel(modelSacrificio);
    //x1.Arete("111111111");
    //x1.LoteId(131231);
    //x1.CodigoCorral("f54");
    //x1.LoteIdSiap("3123");
    //x1.Lote("2132");
    //x1.FechaSacrificio("201513123");
    //x1.NumeroCorral("F33");
    //x1.NumeroProceso("1312");
    //x1.TipoGanado("1");
    //x1.TipoGanadoId("1");
    //x1.AnimalId();
    //x1.AnimalIdSiap(3121);
    //x1.CorralInnova("13ASD");
    //x1.Po("3423");
    //x1.Consecutivo(1);
    //x1.Noqueo(true);
    //x1.PielSangre(true);
    //x1.PielDescarnada(true);
    //x1.Viscera(true);
    //x1.Inspeccion(true);
    //x1.CanalCompleta(true);
    //x1.CanalCaliente(true);

    //modelSacrificio.Sacrificio.push(x1);

    //var x1 = new ChildModel(modelSacrificio);
    //x1.Arete("88888888");
    //x1.LoteId(131231);
    //x1.CodigoCorral("f54");
    //x1.LoteIdSiap("3123");
    //x1.Lote("2132");
    //x1.FechaSacrificio("201513123");
    //x1.NumeroCorral("F33");
    //x1.NumeroProceso("1312");
    //x1.TipoGanado("1");
    //x1.TipoGanadoId("1");
    //x1.AnimalId();
    //x1.AnimalIdSiap(8888);
    //x1.CorralInnova("13ASD");
    //x1.Po("3423");
    //x1.Consecutivo(1);
    //x1.Noqueo(true);
    //x1.PielSangre(true);
    //x1.PielDescarnada(true);
    //x1.Viscera(true);
    //x1.Inspeccion(true);
    //x1.CanalCompleta(true);
    //x1.CanalCaliente(true);

    //modelSacrificio.Sacrificio.push(x1);

    //var x1 = new ChildModel(modelSacrificio);
    //x1.Arete("222222");
    //x1.LoteId(131231);
    //x1.CodigoCorral("f54");
    //x1.LoteIdSiap("3123");
    //x1.Lote("2132");
    //x1.FechaSacrificio("201513123");
    //x1.NumeroCorral("F33");
    //x1.NumeroProceso("1312");
    //x1.TipoGanado("1");
    //x1.TipoGanadoId("1");
    //x1.AnimalId();
    //x1.AnimalIdSiap(3121);
    //x1.CorralInnova("13ASD");
    //x1.Po("3423");
    //x1.Consecutivo(1);
    //x1.Noqueo(true);
    //x1.PielSangre(true);
    //x1.PielDescarnada(true);
    //x1.Viscera(true);
    //x1.Inspeccion(true);
    //x1.CanalCompleta(true);
    //x1.CanalCaliente(true);

    //modelSacrificio.Sacrificio.push(x1);


    //var x1 = new ChildModel(modelSacrificio);
    //x1.Arete("444444");
    //x1.LoteId(131231);
    //x1.CodigoCorral("f54");
    //x1.LoteIdSiap("3123");
    //x1.Lote("2132");
    //x1.FechaSacrificio("201513123");
    //x1.NumeroCorral("F33");
    //x1.NumeroProceso("1312");
    //x1.TipoGanado("1");
    //x1.TipoGanadoId("1");
    //x1.AnimalId();
    //x1.AnimalIdSiap(3121);
    //x1.CorralInnova("13ASD");
    //x1.Po("3423");
    //x1.Consecutivo(1);
    //x1.Noqueo(true);
    //x1.PielSangre(true);
    //x1.PielDescarnada(true);
    //x1.Viscera(true);
    //x1.Inspeccion(true);
    //x1.CanalCompleta(true);
    //x1.CanalCaliente(true);

    //modelSacrificio.Sacrificio.push(x1);

    //modelSacrificio.Total(Enumerable.From(modelSacrificio.Sacrificio.peek()).Count());
    //modelSacrificio.Procesadas(Enumerable.From(modelSacrificio.Sacrificio.peek()).Where("!$.ConConflicto()").Count());
    //modelSacrificio.Procesadas() == modelSacrificio.Total() ? modelSacrificio.Estatus(Etiquetas.Completo) : modelSacrificio.Estatus(Etiquetas.Incompleto);

    $('#transferencia').on('hidden.bs.modal', function () {
        modelSacrificio.eTransferencia.Inicializar();
    });

});