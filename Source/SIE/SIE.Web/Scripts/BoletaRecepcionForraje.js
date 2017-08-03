//Variables Globales 
var msjAbierto = 0;
var indicadores = 0;
var folio = 0;
var cantidadMuestras = 0;
var entradaProductosPendientes = {};

//Inicializar eventos de la pagina
$(document).ready(function () {
    
    $("#txtFolio").inputmask({ "mask": "9", "repeat": 9, "greedy": false });
    $("#txtTicket").inputmask({ "mask": "9", "repeat": 9, "greedy": false });
    $("#txtFolioBuscar").inputmask({ "mask": "9", "repeat": 9, "greedy": false });
    $("#txtFolioOrigen").inputmask({ "mask": "9", "repeat": 9, "greedy": false });
    
    $("#txtFolioBuscar").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            e.preventDefault();
        }
    });

    $("#txtTicket").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) {
            ActualizarTicket($(this));
        }
    });

    $("#txtDescuento").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            e.preventDefault();
        }
    });


    $("#txtDescuento").focusout(function () {
        if (parseFloat($("#txtDescuento").val().replace(/,/g, '').replace(/_/g, '')) > 0) {
            $(this).val(accounting.formatNumber($("#txtDescuento").val().replace(/,/g, '').replace(/_/g, ''), 2, ","));
        } else {
            $(this).val("");
        }
    });

    $("#txtFolioOrigen").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            e.preventDefault();
        }
       
    });

    $("#txtFechaOrigen").focusout(function () {
        if (!ValidarFormatoFechaOrigen()) {
            MensajeFormatoIncorrecto();
            $("#txtFechaOrigen").val("");
            $("#txtFechaOrigen").focus();
            e.preventDefault();
            }       
    });

    $("#txtFechaOrigen").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) {
        e.preventDefault();
        }

    });



    $("#txtHumedadOrigen").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            e.preventDefault();
        }
    });



    $("#txtHumedadOrigen").focusout(function () {
        if (parseFloat($("#txtHumedadOrigen").val().replace(/,/g, '').replace(/_/g, '')) > 0) {
            $(this).val(accounting.formatNumber($("#txtHumedadOrigen").val().replace(/,/g, '').replace(/_/g, ''), 2, ","));
            ObtenerDescuentoGeneral();
        } else {
            $(this).val("");
        }
    });

    $("#txtDescuento").change(function () {
        if ($("#txtDescuento").val().replace(/,/g, '').replace(/_/g, '') > 100) {
            $("#txtDescuento").val("");
            $("#txtDescuento").focus();
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(window.msgMayorPorcentaje, function () {
                    msjAbierto = 0;
                });
            }
        } else {
            if (valor != "") {
                ObtenerPromedioGeneral();
            }
        }
    });

    $('#txtObservaciones').live('keyup change', function() {
        var str = $(this).val();
        var mx = 255;
        if (str.length > mx) {
            $(this).val(str.substr(0, mx));
            return false;
        }
    });

    //Consultar contratos segun el forraje
    $("#cmbForraje").change(function () {
        var forraje = $(this).val();
        ObtenerContratosForraje(forraje);
    });

    $("#btnGuardar").click(function () {
        GuardarCambios();
    });

    $("#btnBuscar").click(function () {
        ObtenerFoliosConMuestra();
    });
    $("#txtFolio").keypress(function (e) {
        var code = (e.keyCode ? e.keyCode : e.which);
        if (code == 13 || code == 9) {
            if ($("#txtFolio").val() != '') {
                $("#txtFolioEntrada").val($("#txtFolio").val());
                EstablecerDatosListaConsulta();
            }
        }

        e.preventDefault();
    });

    $("#lblBuscar").click(function () {
        ObtenerFoliosConMuestra();
        $("#dlgBusquedaFolio").modal("show");
    });

    $("#btnAgregarBuscar").click(function () {
        var renglones = $("input[class=indicadores]:checked");

        if (renglones.length > 0) {
            renglones.each(function () {
                $("#txtFolio").val($(this).attr("folio"));
                $("#txtFolioEntrada").val($(this).attr("folio"));
                ObtenerFolio($(this).attr("registro"));

            });
            
            $("#dlgBusquedaFolio").modal("hide");

        } else {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                $("#dlgBusquedaFolio").modal("hide");
                MensajeSeleccionarFolio();
            }
        }
    });

    $("#btnCancelarBuscar").click(function () {
        $("#dlgCancelarBuscar").modal("show");
        $("#dlgBusquedaFolio").modal("hide");
    });

    $("#btnDialogoSi").click(function () {
        LimpiarFormulario();
    });

    $("#btnSiBuscar").click(function () {
        $("#dlgBusquedaFolio").modal("hide");
        $("#txtFolioBuscar").val("");
        $("#tbBusqueda > tbody").empty();
    });

    $("#btnNoBuscar").click(function () {
        $("#dlgCancelarBuscar").modal("hide");
        $("#dlgBusquedaFolio").modal("show");
    });

    MostrarMuestrasEnTabla();

 });

//Operaciones Ajax
ObtenerFoliosConMuestra = function () {
    var datos = {};
    if ($("#txtFolioBuscar").val() != "") {
        datos = { "folio": $("#txtFolioBuscar").val() };
    } else {
        datos = { "folio": 0 };
    }
    $.ajax({
        type: "POST",
        url: "BoletaRecepcionForraje.aspx/ObtenerEntradaProductosConMuestra",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(datos),
        error: function (request) {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                MensajeError(request.Message);
            }
        },
        dataType: "json",
        success: function (data) {
            if (data.d) {
                var resultado = data.d;
                entradaProductosPendientes = resultado;
                $("#tbBusqueda tbody").html("");
                for (var i = 0; i < resultado.length; i++) {
                    var fecha = new Date(parseInt(resultado[i].Fecha.substr(6)));

                    $("#tbBusqueda tbody").append("<tr>" +
                        "<td class='span1'><label><input type='radio' class='indicadores' name='opciones' id='entrada" + resultado[i].EntradaProductoId + "' folio='" + resultado[i].Folio + "' registro='" + i + "'/></label></td>" +
                        "<td class='span2'><label class=''> &nbsp;" + resultado[i].Folio + "</label></td>" +
                        "<td class='span7'><label>" + resultado[i].RegistroVigilancia.ProveedorMateriasPrimas.Descripcion + "</label></td>" +
                        "<td class='span2' style='text-align:right;'><label>" + fecha.toLocaleDateString() + "</label></td>" +
                        "<td class='span2' style='text-align:right;'><label>" + resultado[i].Estatus.Descripcion + "</label></td>" +
                        "</tr>");
                }
            }
        }
    });
};

ObtenerLimiteCantidadMuestras = function () {
    var limite = 0;
    if (cantidadMuestras == 0) {
        $.ajax({
            type: "POST",
            url: "BoletaRecepcionForraje.aspx/ObtenerLimiteMuestras",
            contentType: "application/json; charset=utf-8",
            error: function (request) {
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    MensajeError(request.Message);
                }
            },
            dataType: "json",
            async: false,
            success: function (data) {
                var datos = data.d;
                if (datos == 0) {
                    MensajeError(msgErrorLimiteMuestras);
                } else {
                    limite = datos;
                }

            }
        });
    } else {
        limite = cantidadMuestras;
    }
   

    return limite;
};

//Obtiene el folio tecleado
ObtenerFolio = function (registroSeleccionado) {
    if (registroSeleccionado != null && registroSeleccionado != undefined) {
        EstablecerDatosListaPendientes(registroSeleccionado);
        DeshabilitaDatosGenerales();
    } 
};

//Guarda los cambios
GuardarCambios = function () {
    if (DatosRequeridosValidos()) {
        $("#btnGuardar").hide();
        var listaIndicadores = [];
        var listaMuestras = [];
        var indicador = {};
        var muestras = {};
        var bandera;//1.- Aprobada:2.- Pendiente por Autorizar;3.- Autorizado:
        var promedio;
        var descuento;
        

        descuento = $("#txtDescuento").val();

        if (descuento == '') {
            descuento = 0;
        }

        if ($(".muestra1").length > 0) {
            $(".muestra1").each(function () {
                var porcentaje = 0;
                if($(this).is("input")){
                    porcentaje = parseFloat($(this).val());
                } else {
                    porcentaje = parseFloat($(this).text());
                }
                muestras = { "EntradaProductoMuestraId": $(this).attr("id-detalle"),"Porcentaje": porcentaje };
                listaMuestras.push(muestras);
            });
        }

        if ($("#txtFolio").val() != "") {
            if ($(".muestra2").length > 0) {
                $(".muestra2").each(function () {
                    var porcentaje = 0;
                    if ($(this).is("input")) {
                        porcentaje = parseFloat($(this).val());
                    } else {
                        porcentaje = parseFloat($(this).text());
                    }
                    muestras = { "EntradaProductoMuestraId": $(this).attr("id-detalle"), "Porcentaje": porcentaje, "Descuento": descuento };
                    listaMuestras.push(muestras);
                });
            }
        }
        promedio = $("#txtPromedioHumedad").val();
        porcentajepermitidoMin = parseFloat($("#hdPorcentajePermitidoMin").val());
        porcentajepermitidoMax = parseFloat($("#hdPorcentajePermitidoMax").val());


        var folioOrigen;
        var fechaOrigen;
        var fechaAux;
        var humedadOrigen;
        var promedioMuestras;
        if ($("#txtCalidadOrigenID").val() == "1") {
            folioOrigen = $("#txtFolioOrigen").val();
            fechaAux = $("#txtFechaOrigen").val();
            var from2 = fechaAux.split("/");
            //var f = (from2[2] + "/" + from2[1] + "/" + from2[0]);
            fechaOrigen = new Date(from2[2] + "/" + from2[1] + "/" + from2[0]);
            humedadOrigen = $("#txtHumedadOrigen").val();
            promedioMuestras = $("#txtPromedioHumedad").val();
        }
        else {
            folioOrigen = 0;
            fechaOrigen = new Date;
            if ($("#txtHumedadOrigen").val() != "" && $("#txtHumedadOrigen").val() != null) {
                humedadOrigen = $("#txtHumedadOrigen").val();
            } else {
                humedadOrigen = 0.0;
            }
            
            promedioMuestras = $("#txtPromedioHumedad").val();
        }
        
        
        if (promedio >= porcentajepermitidoMin && promedio <= porcentajepermitidoMax){
            bandera = 1;
        } else {
            bandera = 2;
        }

        indicador = { "Indicador": { "IndicadorId": $("#txtIndicador").val() }, "ProductoMuestras": listaMuestras };
        listaIndicadores.push(indicador);

        if (listaIndicadores.length > 0 || $("#txtFolio").val() > 0) {
            var entradaProducto = {};

            if ($("#txtFolio").val() > 0) {

                entradaProducto = { "EntradaProductoId": $("#txtEntradaProductoId").val(), "Folio": $("#txtFolio").val(), "Contrato": { "ContratoId": $("#cmbContrato").val() }, "TipoContrato": { "TipoContratoId": $("#cmbDestino").val() }, "Producto": { "ProductoId": $("#cmbForrajes").val() }, "ProductoDetalle": listaIndicadores, "RegistroVigilancia": { "FolioTurno": $("#txtTicket").val() }, "Observaciones": $("#txtObservaciones").val(), "DatosOrigen": { "folioOrigen": folioOrigen, "fechaOrigen": fechaOrigen, "humedadOrigen": humedadOrigen, "promedioMuestras": promedioMuestras } };
            } else {
                entradaProducto = { "Contrato": { "ContratoId": $("#cmbContrato").val() }, "TipoContrato": { "TipoContratoId": $("#cmbDestino").val() }, "Producto": { "ProductoId": $("#cmbForrajes").val() }, "ProductoDetalle": listaIndicadores, "RegistroVigilancia": { "FolioTurno": $("#txtTicket").val() }, "Observaciones": $("#txtObservaciones").val(), "DatosOrigen": { "folioOrigen": folioOrigen, "fechaOrigen": fechaOrigen, "humedadOrigen": humedadOrigen, "promedioMuestras": promedioMuestras } };
            }


            App.bloquearContenedor($(".container-fluid")); 
            var datos = { "entradaProducto": entradaProducto, "Bandera": bandera, "PorcentajePermitidoMin": porcentajepermitidoMin, "PorcentajePermitidoMax": porcentajepermitidoMax };

            $.ajax({
                type: "POST",
                url: "BoletaRecepcionForraje.aspx/GuardarEntradaProducto",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(datos),
                error: function (request) {
                    App.desbloquearContenedor($(".container-fluid"));
                    var error = JSON.parse(request.responseText);
                    var funcion = window[error.Message];
                    $("#btnGuardar").show();

                    if (typeof funcion == 'function') {
                        funcion();
                    } else {
                        MensajeError(error.Message);
                    }
                    
                },
                dataType: "json",
                success: function (data) {
                    App.desbloquearContenedor($(".container-fluid"));
                    var datos = data.d;
                    if (datos.Estatus.EstatusId == 0) {
                        MensajeError(msgErrorInterno);
                        $("#btnGuardar").show();
                    } else if (datos.Estatus.EstatusId == 25) {
                        MensajeRechazo(datos.Folio);
                    } else if (datos.Estatus.EstatusId == 27) {
                        MensajePendiente(datos.Folio);
                    }else { //estatus Aprobado o Pendiente de autorizar
                        MensajeGuardarExito(datos.Folio);
                    }
                }
            });
        } else {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                MensajeDatosObligatorios();
                $("#btnGuardar").show();
            }
        }
    }
};

//Genera la tabla donde se capturaran las muestras.
MostrarMuestrasEnTabla = function (DetalleMuesta1,DetalleMuesta2) {
    cantidadMuestras = ObtenerLimiteCantidadMuestras();
    $("#tbMuestras > tbody").empty();

    if ((DetalleMuesta1 == undefined || DetalleMuesta1.length == 0)
        && (DetalleMuesta2 == undefined || DetalleMuesta2.length == 0)) {
        for (i = 0 ; i < cantidadMuestras ; i++) {
            $('#tbMuestras > tbody:last').append('<tr><td><span class="campoRequerido">*</span><input maxlength="5" type="tel" class="muestra1 span11" id-detalle="0" name="muestra1-' + i + '" /></td><td></td></tr>');
        }
    } else if (DetalleMuesta1.length > 0 && (DetalleMuesta2 == undefined || DetalleMuesta2.length == 0)) {
        for (i = 0 ; i < cantidadMuestras ; i++) {
            var muestra = DetalleMuesta1[i].Porcentaje;
            var idMuestra = DetalleMuesta1[i].EntradaProductoMuestraId;
            $('#tbMuestras > tbody:last').append('<tr><td class="muestra1 " style="text-align:right" id-detalle="' + idMuestra + '"><span class="span11">' + muestra + '</span></td><td><span class="campoRequerido">*</span><input maxlength="5" type="tel" class="muestra2 span11" id-detalle="0" name="muestra2-' + i + '" /></td></tr>');
        }
    } else {
        for (i = 0 ; i < cantidadMuestras ; i++) {
            if (DetalleMuesta1[i] != null) {
                var muestra1 = DetalleMuesta1[i].Porcentaje;
                var idMuestra1 = DetalleMuesta1[i].EntradaProductoMuestraId;
            }

            if (DetalleMuesta2[i] != null) {
                var muestra2 = DetalleMuesta2[i].Porcentaje;
                var idMuestra2 = DetalleMuesta2[i].EntradaProductoMuestraId;
            }
            $('#tbMuestras > tbody:last').append('<tr><td class="muestra1" style="text-align:right" id-detalle="' + idMuestra1 + '"><span class="span11">' + muestra1 + '</span></td><td class="muestra2" style="text-align:right" id-detalle="' + idMuestra2 + '"><span class="span11">' + muestra2 + '</span></td></tr>');
        }
    }

    if ($(".muestra1").length > 0) {
        $(".muestra1").change(function () {
            var valor = $(this).val();
            if (valor > 100) {
                $(this).val("");
                $(this).focus();
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(window.msgMayorPorcentaje, function() {
                        msjAbierto = 0;
                    });
                }
            } else {
                if (valor != "") {
                    ObtenerPromedioGeneral();
                    ObtenerDescuentoGeneral();
                }
            }
        });
    }

    $(".muestra1").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            e.preventDefault();
        }
    });

    $(".muestra1").focusout(function () {
        var lista = $(".muestra1");
        lista.each(function () {
            if (parseFloat($(this).val().replace(/,/g, '').replace(/_/g, '')) > 0) {
                $(this).val(accounting.formatNumber($(this).val().replace(/,/g, '').replace(/_/g, ''), 2, ","));
            } else {
                $(this).val("");
            }
        });
    });

    $(".muestra2").focusout(function () {
        var lista = $(".muestra2");
        lista.each(function () {
            if (parseFloat($(this).val().replace(/,/g, '').replace(/_/g, '')) > 0) {
                $(this).val(accounting.formatNumber($(this).val().replace(/,/g, '').replace(/_/g, ''), 2, ","));
            } else {
                $(this).val("");
            }
        });
    });

    if ($(".muestra1").length > 0) {
        $(".muestra2").change(function () {
            var valor = $(this).val();
            if (valor > 100) {
                $(this).val("");
                $(this).focus();
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(window.msgMayorPorcentaje, function() {
                        msjAbierto = 0;
                    });
                }
            } else {
                if (valor != "") {
                    ObtenerPromedioGeneral();
                    ObtenerDescuentoGeneral();
                }
            }
        });
    }

    $(".muestra2").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            e.preventDefault();
        }
    });
    
}

//Obtiene el folio de la lista de entradas de productos pendientes de autorizar 
//que se almacenaron en la variable --entradaProductosPendientes--
EstablecerDatosListaPendientes = function (registro) {
    if (entradaProductosPendientes.length > 0) {
        var entradaProducto = entradaProductosPendientes[registro];
        entradaProducto.AlmacenInventarioLote = {};
        entradaProducto.AlmacenMovimiento = {};
        entradaProducto.Contrato = {};
        entradaProducto.Estatus = {};
        entradaProducto.OperadorAlmacen = {};
        entradaProducto.OperadorAnalista = {};
        entradaProducto.OperadorAutoriza = {};
        entradaProducto.OperadorBascula = {};
        entradaProducto.Organizacion = {};
        entradaProducto.RegistroVigilancia = {};
        entradaProducto.TipoContrato = {};
        entradaProducto.Producto = {};
        entradaProducto.DatosOrigen = {};
        
        entradaProducto.Fecha = new Date();
        entradaProducto.FechaCreacion = new Date();
        entradaProducto.FechaDestara = new Date();
        entradaProducto.FechaFinDescarga = new Date();
        entradaProducto.FechaInicioDescarga = new Date();
        entradaProducto.FechaModificacion = new Date();
        entradaProducto.FechaEmbarque = new Date();

        var datos = { "entradaProducto": entradaProducto };
        $.ajax({
            type: "POST",
            url: "BoletaRecepcionForraje.aspx/ObtenerEntradaProducto",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(datos),
            error: function(request) {
                App.desbloquearContenedor($(".container-fluid"));
                var error = JSON.parse(request.responseText);
                var funcion = window[error.Message];
                $("#btnGuardar").show();

                if (typeof funcion == 'function') {
                    funcion();
                } else {
                    MensajeError(error.Message);
                }
            },
            dataType: "json",
            success: function (data) {
                if (data.d) {
                    if (data.d.Estatus.EstatusId == $('#txtAutorizado').val()) {
                        if (data.d.ProductoDetalle[0].ProductoMuestras.length == 30) {
                            MensajeAutorizado();
                        } else {
                            LlenaDatosGenerales(data.d);
                            deshabilitarDatosOrigen();
                            LlenaDatosOrigen(data.d);
                            LlenaDatosMuestras(data.d);
							CalcularDescuento();
                        }
                    }
                    else if (data.d.Estatus.EstatusId == $('#txtAprobado').val()) {
                        if (data.d.ProductoDetalle[0].ProductoMuestras.length == 30) {
                            MensajeAprobado();
                        } else {
                            LlenaDatosGenerales(data.d);
                            deshabilitarDatosOrigen();
                            LlenaDatosOrigen(data.d);
                            LlenaDatosMuestras(data.d);
							CalcularDescuento();
                        }
                    }
                    else if (data.d.Estatus.EstatusId == $('#txtRechazado').val()) {
                        MensajeRechazado();
                    }
                    else if (data.d.Estatus.EstatusId == $('#txtPendienteAutorizar').val()) {
                        MensajePedienteAutorizar();
                        LlenaDatosGenerales(data.d);
                        LlenaDatosOrigen(data.d);
                        LlenaDatosMuestras(data.d);
						CalcularDescuento();
                        DeshabilitaDatosGenerales();
                        deshabilitarDatosOrigen();
                        DeshabilitarTablaMuestra();

                    }
                } else {
                    MensajeFolioInvalido();
                }
            }
        });
    }
}

EstablecerDatosListaConsulta = function (registro) {
    
    var datos = { "folio": $("#txtFolio").val() };
        $.ajax({
            type: "POST",
            url: "BoletaRecepcionForraje.aspx/ObtenerEntradaProductoConsulta",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(datos),
            error: function (request) {
                App.desbloquearContenedor($(".container-fluid"));
                var error = JSON.parse(request.responseText);
                var funcion = window[error.Message];
                $("#btnGuardar").show();

                if (typeof funcion == 'function') {
                    funcion();
                } else {
                    MensajeError(error.Message);
                }
            },
            dataType: "json",
            success: function (data) {
                if (data.d) {
                    //alert(data.d.ProductoDetalle[0].ProductoMuestras.length);
                    if (data.d.Estatus.EstatusId == $('#txtAutorizado').val()) {
                        if (data.d.ProductoDetalle[0].ProductoMuestras.length == 30) {
                            MensajeAutorizado();
                        } else {
                            LlenaDatosGenerales(data.d);
                            deshabilitarDatosOrigen();
                            LlenaDatosOrigen(data.d);
                            LlenaDatosMuestras(data.d);
                            CalcularDescuento();
                        }
                    }
                    else if (data.d.Estatus.EstatusId == $('#txtAprobado').val()) {
                        if (data.d.ProductoDetalle[0].ProductoMuestras.length == 30) {
                            MensajeAprobado();
                        } else {
                            LlenaDatosGenerales(data.d);
                            deshabilitarDatosOrigen();
                            LlenaDatosMuestras(data.d);
                            LlenaDatosOrigen(data.d);
                        }
                    }
                    else if (data.d.Estatus.EstatusId == $('#txtRechazado').val()) {
                        MensajeRechazado();
                    }
                    else if (data.d.Estatus.EstatusId == $('#txtPendienteAutorizar').val()) {
                        MensajePedienteAutorizar();
                        LlenaDatosGenerales(data.d);
                        LlenaDatosMuestras(data.d);
                        LlenaDatosOrigen(data.d);
                        DeshabilitaDatosGenerales();
                        deshabilitarDatosOrigen();
                        DeshabilitarTablaMuestra();

                    }
                    
                 } else {
                    MensajeFolioInvalido();
                }
            }
        });
    
}

LlenaDatosOrigen = function(entradaProducto) {
    var datosOrigen = entradaProducto.datosOrigen;
    if (datosOrigen.folioOrigen != 0) {
        $("#txtFolioOrigen").val(datosOrigen.folioOrigen);
        //var from = datosOrigen.fechaFormateada;
        //var from2 = from.split("-");
        //var f = (from2[2] + "/" + from2[1] + "/" + from2[0]);
        $("#txtFechaOrigen").val(datosOrigen.fechaFormateada);
        $("#txtHumedadOrigen").val(datosOrigen.humedadOrigen);
        //"13-01-2011".replace( /(\d{2})-(\d{2})-(\d{4})/, "$2/$1/$3") 
    }
    $("#txtCalidadOrigenID").val(entradaProducto.Contrato.CalidadOrigen);
    $("#txtPorcentajePermitido").val(entradaProducto.Contrato.ListaContratoDetalleInfo[0].PorcentajePermitido);
}

ValidaFolioEstatusMuestras = function (entradaProducto) {

    if (entradaProducto.Estatus.EstatusId == $('#txtAutorizado').val()) {
        if (entradaProducto.ProductoDetalle[0].ProductoMuestras.length == 30) {
            bootbox.alert(msgTicketAutorizado, function () {
            });
        } 
    }
    else if (entradaProducto.Estatus.EstatusId == $('#txtAprobado').val()) {
        bootbox.alert(msgTicketAprovado, function () {
        });
    }
    else if (entradaProducto.Estatus.EstatusId == $('#txtRechazado').val()) {
        
        bootbox.alert(msgTicketRechazado, function () {
        });
    }
    else if (entradaProducto.Estatus.EstatusId == $('#txtPendienteAutorizar').val()) {
        $("#btnGuardar").hide();
        bootbox.alert(msgNecesitaAutorizacion, function () {
        });
    }
}
//Llenar los campos de la pagina.
LlenaDatosGenerales = function (entradaProducto) {
    var contrato = entradaProducto.Contrato;
    if (entradaProducto.Contrato.TipoFlete.TipoFleteId != '1' && entradaProducto.Contrato.TipoFlete.TipoFleteId != 1) {
        var proveedor = entradaProducto.RegistroVigilancia.ProveedorMateriasPrimas;
        var chofer = entradaProducto.RegistroVigilancia.ProveedorChofer.Chofer;
        var camion = entradaProducto.RegistroVigilancia.Camion;
    }

    var indicador = entradaProducto.ProductoDetalle[0].Indicador;
    var forraje = entradaProducto.Producto;
    var tipoContrato = entradaProducto.TipoContrato;

    $("#cmbContrato").append("<option value='" + contrato.ContratoId + "'>" + contrato.Folio + "</option>");
    if (entradaProducto.Contrato.TipoFlete.TipoFleteId != '1' && entradaProducto.Contrato.TipoFlete.TipoFleteId != 1) {
        $("#cmbProveedor").append("<option value='" + proveedor.ProveedorID + "'>" + proveedor.Descripcion + "</option>");
        $("#cmbPlacas").append("<option value='" + camion.CamionId + "'>" + camion.PlacaCamion + "</option>");
        $("#cmbChofer").append("<option value='" + chofer.ChoferID + "'>" + chofer.NombreCompleto + "</option>");
    } else {
        $("#cmbProveedor").append("<option value='0'>" + entradaProducto.RegistroVigilancia.Transportista + "</option>");
        $("#cmbPlacas").append("<option value='0'>" + entradaProducto.RegistroVigilancia.CamionCadena + "</option>");
        $("#cmbChofer").append("<option value='0'>" + entradaProducto.RegistroVigilancia.Chofer + "</option>");
    }
    $("#cmbForrajes").append("<option value='" + forraje.ProductoId + "'>" + forraje.ProductoDescripcion + "</option>");
    $("#txtObservaciones").val(entradaProducto.Observaciones);
    $("#txtTipoContrato").val(contrato.TipoContrato.TipoContratoId);
    $("#txtTicket").val(entradaProducto.RegistroVigilancia.FolioTurno);
    $("#txtEntradaProductoId").val(entradaProducto.EntradaProductoId);
    $("#cmbDestino").val(tipoContrato.TipoContratoId);

    $("#txtAoturizadoPor").val(entradaProducto.OperadorAutoriza.NombreCompleto);
    //$("#txtDescuento").prop("disabled", false);
    
    if (indicador != null && indicador != undefined) {
        $("#txtIndicador").val(indicador.IndicadorId);
    }

    $("#hdPorcentajePermitidoMin").val(entradaProducto.RegistroVigilancia.porcentajePromedioMin);
    $("#hdPorcentajePermitidoMax").val(entradaProducto.RegistroVigilancia.porcentajePromedioMax);
};

LlenaDatosMuestras = function (entradaProducto) {
    var muestras = entradaProducto.ProductoDetalle[0].ProductoMuestras;
    var cantidadMuestras = ObtenerLimiteCantidadMuestras();

    if (muestras != null && muestras != undefined) {
        if (muestras.length > 0) {
            if (muestras.length > cantidadMuestras) {
                var muestras1 = {};
                var muestras2 = {};
                for (i = 0; i < muestras.length ; i++) {
                    if(i < cantidadMuestras){
                        muestras1[i] = muestras[i];
                    } else {
                        muestras2[i - cantidadMuestras] = muestras[i];
                    }
                    
                }
                MostrarMuestrasEnTabla(muestras1, muestras2);

            } else {
                MostrarMuestrasEnTabla(muestras, undefined);
            }
            ObtenerPromedioGeneral();
        }
    }
};

DeshabilitaDatosGenerales = function () {
    $("#cmbContrato").attr("readonly", true);
    $("#cmbDestino").attr("readonly", true);
    $("#cmbForrajes").attr("readonly", true);
    $("#txtFolio").attr("readonly", true);
    $("#txtTicket").attr("readonly", true);
    $("#txtObservaciones").attr("readonly", true);

    $("#cmbContrato").attr("disabled", true);
    $("#cmbDestino").attr("disabled", true);
    $("#cmbForrajes").attr("disabled", true);
    $("#txtFolio").attr("disabled", true);
    $("#txtTicket").attr("disabled", true);
    $("#txtObservaciones").attr("disabled", true);
 }

HabilitaDatosGenerales = function () {
    $("#cmbContrato").attr("readonly", false);
    $("#cmbDestino").attr("readonly", false);
    $("#cmbForrajes").attr("readonly", false);
    $("#txtFolio").attr("readonly", false);
    $("#txtTicket").attr("readonly", false);
    $("#txtObservaciones").attr("readonly", false);

    $("#cmbContrato").attr("disabled", false);
    $("#cmbDestino").attr("disabled", false);
    $("#cmbForrajes").attr("disabled", false);
    $("#txtFolio").attr("disabled", false);
    $("#txtTicket").attr("disabled", false);
    $("#txtObservaciones").attr("disabled", false);
}

DeshabilitarTablaMuestra = function()
{
    $(".muestra1").attr("disabled", true);
    $(".muestra2").attr("disabled", true);
}

deshabilitarDatosOrigen = function() {
    $("#txtFolioOrigen").attr("disabled", true);
    $("#txtFechaOrigen").attr("disabled", true);
    $("#txtHumedadOrigen").attr("disabled", true);
}
habilitarDatosOrigen = function () {
    $("#txtFolioOrigen").attr("disabled", false);
    $("#txtFechaOrigen").attr("disabled", false);
    $("#txtHumedadOrigen").attr("disabled", false);
}

//Calculo del promedio de las muestras
ObtenerPromedioMuestra = function (muestra) {
    var promedio = 0;
    var totalMuestra = 0;

    if ($("." + muestra).length > 0) {
        $("." + muestra).each(function (index) {
            if ($(this).is("input")) {
                if ($(this).val() != "") {
                    var valor = parseFloat($(this).val());
                    totalMuestra += valor;
                }
            } else {
                if ($(this).text() != "") {
                    var valor = parseFloat($(this).text());
                    totalMuestra += valor;
                }
            }
            
        });

        if (totalMuestra > 0) {
            promedio = totalMuestra / cantidadMuestras;
        }
    }

    return promedio;
}

ObtenerPromedioGeneral = function () {

    var promedioMuestra1 = 0;
    var promedioMuestra2 = 0;
    var promedioGeneral = 0;

    promedioMuestra1 = ObtenerPromedioMuestra("muestra1");
    promedioMuestra2 = ObtenerPromedioMuestra("muestra2");

    if (promedioMuestra1 > 0 && promedioMuestra2 > 0) {

        promedioGeneral = (promedioMuestra1 + promedioMuestra2) / 2
    } else {
        promedioGeneral = promedioMuestra1;
    }

    if (promedioGeneral > 0) {
        $("#txtPromedioHumedad").val(promedioGeneral.toFixed(2));
    }
}

//Validaciones
DatosRequeridosValidos = function () {
    var resultado = true;

    if ($("#txtTicket").val() == "" || $("#txtObservaciones").val() == "" 
            || ($("#cmbForrajes").val() == "0" || $("#cmbForrajes").val() == "" || $("#cmbForrajes").val() == null )
            || ($("#cmbContrato").val() == "0" || $("#cmbContrato").val() == "" || $("#cmbContrato").val() == null )
            || ($("#cmbDestino").val() == "0" || $("#cmbDestino").val() == "" || $("#cmbDestino").val() == null )) {

        MensajeDatosObligatorios();
        resultado = false;
    } else {
        if ($("#txtFolioEntrada").val() != "0" && $("#txtFolioEntrada").val() != "") {
            if (!MuestrasValidas("muestra2")) {
                MensajeDatosObligatorios();
                resultado = false;
            } 
        } else {
            if (!MuestrasValidas("muestra1")) {
                MensajeDatosObligatorios();
                resultado = false;
            }
        }
    }
    if ($("#txtCalidadOrigenID").val() == "1") {
        if (($("#txtFolioOrigen").val() == "0" || $("#txtFolioOrigen").val() == "" || $("#txtFolioOrigen").val() == null)
            || ($("#txtFechaOrigen").val() == "0" || $("#txtFechaOrigen").val() == "" || $("#txtFechaOrigen").val() == null)
            || ($("#txtHumedadOrigen").val() == "0" || $("#txtHumedadOrigen").val() == "" || $("#txtHumedadOrigen").val() == null)) {
            MensajeDatosObligatorios();
            resultado = false;
        }
    } 



    if (!resultado && ($("#txtFolioEntrada").val() != "0" && $("#txtFolioEntrada").val() != "")) {
        DeshabilitaDatosGenerales();
    }

    return resultado;
};

MuestrasValidas = function (muestra) {
    var resultado = true;
    if ($('.' + muestra + '').length > 0) {
        $('.' + muestra + '').each(function () {
            if ($(this).val() == "") {
                resultado = false;
                return;
            }
        });
    }

    return resultado;
};

LimpiarFormulario = function () {
    location.href = location.href;
};

//Determina si el promedio de las primeras 15 muestras son mayor al promedio permitido en el contrato
//si es mayor, se calcula el descuento a aplicar
CalcularDescuento = function () {
    var humedadOrigen;
    if ($("#txtHumedadOrigen").val() == null || $("#txtHumedadOrigen").val() == "") {
        humedadOrigen = 0;
    } else {
        humedadOrigen = $("#txtHumedadOrigen").val();
    }

    var datos = { "folio": parseFloat($("#txtTicket").val()), "promedioHumedad": parseFloat($("#txtPromedioHumedad").val()), "humedadOrigen": humedadOrigen };
    $.ajax({
            type: "POST",
            url: "BoletaRecepcionForraje.aspx/CalcularDescuento",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(datos),
            error: function (request) {
                MensajeError(request.Message);

            },
            dataType: "json",
            async: false,
            success: function(data) {
                var datos = data.d;
                if (datos != 0) {
                    $("#txtDescuento").val(datos.toFixed(2));
                }
            }
        });
};


//Obtiene el descuento calaculado
ObtenerDescuentoGeneral = function () {
    $("#txtDescuento").val("");
    if (parseFloat($("#txtPorcentajePermitido").val()) < parseFloat($("#txtPromedioHumedad").val())) {
        if (parseFloat($("#txtCalidadOrigenID").val()) == 0)
        {
            //es destino
            if (parseFloat($("#txtPromedioHumedad").val()) >= parseFloat($("#txtPorcentajePermitido").val())) {
                var descuentoD = parseFloat($("#txtPromedioHumedad").val()) - parseFloat($("#txtPorcentajePermitido").val());
                $("#txtDescuento").val(descuentoD.toFixed(2));
            } else {
                $("#txtDescuento").val("0");
            }
        }
        else
        {
            //es origen
            if ($("#txtHumedadOrigen").val() != null || $("#txtHumedadOrigen").val() != "") {
                if (parseFloat($("#txtHumedadOrigen").val()) >= parseFloat($("#txtPorcentajePermitido").val())) {
                    var descuentoO = parseFloat($("#txtHumedadOrigen").val()) - parseFloat($("#txtPorcentajePermitido").val());
                    $("#txtDescuento").val(descuentoO.toFixed(2));
                } else {
                    $("#txtDescuento").val("0");
                }
            } else {
                $("#txtDescuento").val("0");
            }

        }
    } else {
        $("#txtDescuento").val("0");
    }
}


//Mensajes
MensajeTicketNoExiste = function () {
    bootbox.alert(msgTicketNoExiste, function () {
        msjAbierto = 0;
        LimpiarFormulario();
    });
};

MensajeCancelar = function () {
    bootbox.alert(msgCancelar, function () {
        msjAbierto = 0;
    });
};

MensajeCancelarBuscar = function () {
    bootbox.alert(msgCancelarBuscar, function () {
        msjAbierto = 0;
    });
};

MensajeGuardarExito = function (Folio) {
    bootbox.alert("<img src='../Images/Correct.png'/>Ticket: "+ Folio +". "+ msgGuardarExito, function () {
        //LimpiarFormulario();
        location.href = location.href;
    });
};

MensajeDatosObligatorios = function () {
    bootbox.alert(msgDatosObligatorios, function () {
        msjAbierto = 0;
    });
};

MensajeFolioInvalido = function () {
    bootbox.alert(msgFolioInvalido, function () {
        msjAbierto = 0;
    });
};

MensajeError = function (msgError) {
    bootbox.alert(msgError, function () {
        msjAbierto = 0;
    });
}

MensajeRechazo = function (Folio) {
    bootbox.alert("<img src='../Images/Correct.png'/>Folio: " + Folio + ". " + msgForrajeRechazado, function () {
        location.href = location.href;
    });
}

MensajePendiente = function (Folio) {
    bootbox.alert("<img src='../Images/Correct.png'/>Ticket: " + Folio + " " + msgForrajePorAutorizar, function () {
        location.href = location.href;
    });
}

MensajeSeleccionarFolio = function () {
    bootbox.alert(msgSeleccionarFolio, function () {
        $("#dlgBusquedaFolio").modal("show");
        msjAbierto = 0;
    });
};

MensajeSinFoliosPendientes = function () {
    bootbox.alert(msgSinFolios, function () {
        $("#dlgBusquedaFolio").modal("show");
        msjAbierto = 0;
    });
};

MensajeProductoPrevioRechazo = function () {
    bootbox.alert(msgEntradaRechazado, function () {
    });
};

MensajeFormatoIncorrecto = function () {
    bootbox.alert(msgFechaFormatoIncorrecto, function () {
    });
};


MensajeProductoPrevioAprobado = function () {
    bootbox.alert(msgEntradaAprobado, function () {
    });
}

MensajeRechazado = function () {
    bootbox.alert(msgTicketRechazado, function () {
    });
}

MensajePedienteAutorizar = function () {
    bootbox.alert(msgTicketPendienteAutorizar, function () {
    });
}


MensajeProductoPrevioPendiente = function () {
    bootbox.alert(msgEntradaPendiente, function () {
    });
}

MensajeProveedorSinContrato = function() {
    bootbox.alert(msgProveedorSinContrato, function () {
        LimpiarFormulario();
    });
};

MensajeProductoErrorRangos = function () {
    bootbox.alert(msgProductoErrorRangos, function () {
        LimpiarFormulario();
    });
};


MensajeProveedorSinChofer = function() {
    bootbox.alert(msgProveedorSinChofer, function () {
        LimpiarFormulario();
    });
};

MensajeChoferSinCamion = function() {
    bootbox.alert(msgProveedorSinPlacas, function () {
        LimpiarFormulario();
    });
};

MensajeProveedorSinProductos = function() {
    bootbox.alert(msgProveedorSinProducto, function () {
        LimpiarFormulario();
    });
};

MensajeNoValidoMuestras = function() {
    bootbox.alert(msgNoValidoMuestras, function () {
    });
}

MensajeAprobado = function () {
    bootbox.alert(msgTicketAprobado, function () {
    });
}

MensajeAutorizado = function () {
    bootbox.alert(msgTicketAutorizado, function () {
    });
}

SesionExpirada = function() {
    bootbox.alert(msgSesionExpirada, function () {
        window.location.href = '../Seguridad/login.aspx';
    });
};
MensajeFolioYaRegistrado = function () {
    bootbox.alert(msgMensajeFolioYaRegistrado, function () {
        msjAbierto = 0;
        LimpiarFormulario();
    });
};
MensajeFolioFechaSalida = function () {
    bootbox.alert(msgFolioFechaSalida, function () {
        msjAbierto = 0;
        LimpiarFormulario();
    });
};


function ValidarFormatoFechaOrigen() {
    var fecha = $("#txtFechaOrigen").val();
    var fechaf = fecha.split("/");
    var d = fechaf[0];
    var m = fechaf[1];
    var y = fechaf[2];
    return m > 0 && m < 13 && y > 0 && y < 32768 && d > 0 && d <= (new Date(y, m, 0)).getDate();
}

ValidarCalidadOrigen = function() {
    if ($("#txtCalidadOrigenID").val() == "1") {
        habilitarDatosOrigen();
    } else {
        deshabilitarDatosOrigen();
    }
}