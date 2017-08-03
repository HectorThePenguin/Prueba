/// <reference path="../assets/plugins/jquery-1.7.1-vsdoc.js" />
/// <reference path="../assets/plugins/data-tables/jquery.dataTables.js" />
/// <reference path="../assets/plugins/jquery-linq/linq-vsdoc.js" />
/// <reference path="~/assets/plugins/bootstrap-bootbox/js/bootbox.min.js" />
/// <reference path="../assets/plugins/data-tables/jquery.FixedHeaderTable.js" />
/// <reference path="~/Scripts/jscomun.js" />

/* [Definición de Variables]*/
var rutaPantalla = window.location.pathname,
    TipoCorralRecepcion = 1,
    Guardado = false;

/* [Region Eventos]*/

BuscarEntrada = function () {
    var info = {};
    var txtEditFolioEntrada = $('#txtEditFolioEntrada');

    info.FolioEntrada = txtEditFolioEntrada.val();
    
    var datos = { 'entradaGanadoInfo': info };

    BloquearPantalla();
    $.ajax({
        type: "POST",
        url: rutaPantalla + '/TraerEntradaGanado',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == null || msg.d.length == 0) {
                bootbox.dialog({
                    message: window.MsgFolioEntradaNoExiste,
                    buttons: {
                        Aceptar: {
                            label: window.Aceptar,
                            callback: function () {
                                DesbloquearPantalla();
                                LimpiarPantalla();
                            }
                        }
                    }
                });
                return false;
            }

            var entrada = msg.d;
            DesbloquearPantalla();
            
            if (entrada.ConteoCapturado == false) {
                bootbox.dialog({
                    message: window.MsgCapturarConteo,
                    buttons: {
                        Aceptar: {
                            label: window.Aceptar,
                            callback: function () {
                                DesbloquearPantalla();
                                LimpiarPantalla();
                            }
                        }
                    }
                });
                return false;
            }

            if (entrada.EntradaGanado.ListaCondicionGanado.length > 0) {
                bootbox.dialog({
                    message: window.MsgRecepcionGanadoCapturada,
                    buttons: {
                        Aceptar: {
                            label: window.Aceptar,
                            callback: function () {
                                DesbloquearPantalla();
                                LimpiarPantalla();
                            }
                        }
                    }
                });
                return false;
            }
            
           
            
            var fechaEntrada = new Date(parseInt(entrada.EntradaGanado.FechaEntrada.replace(/^\D+/g, '')));
            
            txtEditFolioEntrada.attr('disabled', true);
            txtEditFolioEntrada.val(entrada.EntradaGanado.FolioEntrada);
            $("#txtNoEditVigilancia").val(HoraFormateada(fechaEntrada));
            $("#hfEntradaGanadoID").val(entrada.EntradaGanado.EntradaGanadoID);
            $("#txtNoEditOperador").val(entrada.Operador.NombreCompleto);
            
            $("#txtNoEditCabezasOrigen").val(entrada.EntradaGanado.CabezasOrigen);
            
            $("#txtNoEditOrigen").val(entrada.EntradaGanado.OrganizacionOrigen);
            $("#txtNoEditPlacas").val(entrada.Jaula.PlacaJaula);
            $("#hfEmbarqueID").val(entrada.EntradaGanado.EmbarqueID);
            $("#txtNoEditFaltante").val('0');
            
            if (entrada.Corral != null) {
                $("#txtCorral").val(entrada.Corral.Codigo);
                $('#txtCorral').attr("disabled", "disabled");
                $("#hfCorralID").val(entrada.Corral.CorralID);
                $("#hfCapacidadCorral").val(entrada.Corral.Capacidad);
            }
            
            $("#btnGuardar").attr('disabled', false);
            $("#btnCancelar").attr('disabled', false);
            return true;
        },
        error: function () {
            DesbloquearPantalla();
            bootbox.dialog({
                message: window.ErrorAlConsultarCorrales,
                buttons: {
                    Aceptar: {
                        label: window.Aceptar,
                        callback: function () {
                        }
                    }
                }
            });
        }
    });
};

TraerCorral = function () {

    var info = {};

    var txtCorral = $("#txtCorral");

    info.Codigo = txtCorral.val();
    info.Organizacion = { };

    var datos = {
        'corralInfo': info,
        'embarqueId': $("#hfEmbarqueID").val()
    };

    BloquearPantalla();
    $.ajax({
        type: "POST",
        url: rutaPantalla + '/TraerCorral',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == null || msg.d.length == 0) {
                bootbox.dialog({
                    message: window.MsgCorralNoExiste,
                    buttons: {
                        Aceptar: {
                            label: window.Aceptar,
                            callback: function () {
                                DesbloquearPantalla();
                                txtCorral.focus();
                                txtCorral.val('');
                                $("#hfCorralID").val("0");
                                $("#hfCapacidadCorral").val('0');
                            }
                        }
                    }
                });
                return false;
            }
            
            var corral = msg.d;

            if (corral.CorralID == 0) {
                bootbox.dialog({
                    message: window.MsgCorralLoteActivo,
                    buttons: {
                        Aceptar: {
                            label: window.Aceptar,
                            callback: function () {
                                DesbloquearPantalla();
                                txtCorral.focus();
                                txtCorral.val('');
                                $("#hfCorralID").val("0");
                                $("#hfCapacidadCorral").val('0');
                            }
                        }
                    }
                });
                return false;
            }
            $("#hfCorralID").val(corral.CorralID);
            $("#hfCapacidadCorral").val(corral.Capacidad);

            if (corral.TipoCorral.TipoCorralID != TipoCorralRecepcion) {
                bootbox.dialog({
                    message: window.MsgTipoCorralNoRecepcion,
                    buttons: {
                        Aceptar: {
                            label: window.Aceptar,
                            callback: function () {
                                DesbloquearPantalla();
                                txtCorral.focus();
                                txtCorral.val('');
                                $("#hfCorralID").val("0");
                                $("#hfCapacidadCorral").val('0');
                            }
                        }
                    }
                });
                return false;
            }
            
            DesbloquearPantalla();
            return true;
        },
        error: function (ex) {
            DesbloquearPantalla();
            bootbox.dialog({
                message: window.ErrorAlConsultarCorrales,
                buttons: {
                    Aceptar: {
                        label: window.Aceptar,
                        callback: function () {

                        }
                    }
                }
            });
        }
    });
};

ValidarAntesGuardar = function () {

    var captura = {};
    var usuarioId = 0;
    var corralId = $("#txtCorral").val() != '' ? $("#hfCorralID").val() : 0;
    var capacidad = $("#txtCorral").val() != '' ? $("#hfCapacidadCorral").val() : 0;
    var condicionJaulaID = $("#ddlCondicionesJaula option:selected").val();
    
    $("#hfCorralID").val(corralId);
    $("#hfCapacidadCorral").val(capacidad);

    captura.EntradaGanadoID = TryParseInt($("#hfEntradaGanadoID").val(), 0);
    captura.FolioEntrada = $("#txtEditFolioEntrada").val();
    captura.CabezasRecibidas = $("#txtEditCabezasRecibidas").val();
    captura.UsuarioModificacionID = 1;
    captura.CorralID = corralId;
    captura.ManejoSinEstres = $("#rdnManejosinEstres").is(':checked');
    captura.Activo = 1;

    var listaCondicion = [];
    var totalCondicion = 0;
    var lista = ObtieneListaGrid(); 
    var mensaje = "";
    var txtControl = {};

    $.each(lista, function () {
        $(this).children("td").each(function () {
            var condicionId = parseInt($(this).attr('data-CondicionID'));
            var cantidad = $('.span3', this).val();
            if (cantidad != "" && cantidad > 0) {
                var condicion = {};
                condicion.EntradaGanadoID = 0;
                condicion.CondicionID = condicionId;
                condicion.Cabezas = cantidad;
                condicion.UsuarioID = usuarioId;
                condicion.Activo = 1;
                
                listaCondicion.push(condicion);
                totalCondicion += parseInt(cantidad);
            }
        });
    });
    
    if (captura.FolioEntrada == 0 || captura.FolioEntrada == "" || isNaN(captura.FolioEntrada)) {
        mensaje = window.MsgFolioEntradaRequerido;
        txtControl = $("#txtEditFolioEntrada");
    } else if (captura.EntradaGanadoID == 0 || captura.EntradaGanadoID == "" || isNaN(captura.EntradaGanadoID)) {
        mensaje = window.MsgFolioEntradaNoExiste;
        txtControl = $("#txtEditFolioEntrada");
    } else if (captura.CabezasRecibidas < 0 || captura.CabezasRecibidas == "" || isNaN(captura.CabezasRecibidas)) {
        mensaje = window.MsgCabezasRecibidasRequerido;
        txtControl = $("#txtEditCabezasRecibidas");
    } else if (captura.CorralID == 0 || captura.CorralID == "" || isNaN(captura.CorralID)) {
        mensaje = window.MsgCorralRequerido;
        txtControl = $("#txtCorral");
    } else if (listaCondicion.length == 0) {
        mensaje = window.MsgCondicionesGanadoRequerido;
        var txtPrimero = $('.span3', lista.children('td')).first();
        txtControl = txtPrimero;
    } else if (captura.CabezasRecibidas != totalCondicion) {
        mensaje = window.MsgCabezasDiferentesCondiciones;
        txtControl = $("#txtEditCabezasRecibidas");
    }
    
    if (condicionJaulaID == 0) {
        mensaje = window.MsgSeleccionarCondicionJaula;
        txtControl = $("#ddlCondicionesJaula");
    }

    if (mensaje != "") {
        bootbox.dialog({
            message: mensaje,
            buttons: {
                Aceptar: {
                    label: window.Aceptar,
                    callback: function() {
                        if (txtControl != { }) {
                            txtControl.focus();
                        }
                    }
                }
            }
        });
        return null;
    }
    captura.ListaCondicionGanado = listaCondicion;
    
    captura.CondicionJaula = { };
    captura.CondicionJaula.CondicionJaulaID = $("#ddlCondicionesJaula option:selected").val();
    captura.CondicionJaula.Descripcion = $("#ddlCondicionesJaula option:selected").text();
    
    return captura;
};

Guardar = function () {
    
    BloquearPantalla();

    var info = ValidarAntesGuardar();
    if (info == null || info == 'undefined') {
        DesbloquearPantalla();
        return false;
    }

    var datos = { 'entradaGanadoInfo': info };
    $.ajax({
        type: "POST",
        url: rutaPantalla + '/Guardar',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == 'OK') {
                DesbloquearPantalla();
                bootbox.dialog({
                    message: window.MsgGuardadoConExito,
                    buttons: {
                        Aceptar: {
                            label: window.Aceptar,
                            callback: function () {
                                LimpiarPantalla();
                                $('#btnGuardar').attr('disabled', false);
                                Guardado = true;
                            }
                        }
                    }
                });
            } else if (msg.d == 'ERROR') {
                DesbloquearPantalla();
                bootbox.dialog({
                    message: window.MsgCorralLoteActivo,
                    buttons: {
                        Aceptar: {
                            label: window.Aceptar,
                            callback: function() {
                                var txt = $("#txtCorral");
                                txt.val('');
                                txt.focus();
                            }
                        }
                    }
                });
            }
        },
        error: function () {
            DesbloquearPantalla();
            bootbox.dialog({
                message: window.MsgErrorGuardar,
                buttons: {
                    Aceptar: {
                        label: window.Aceptar,
                        callback: function () {
                        }
                    }
                }
            });
        }
    });
    return true;
};

LimpiarPantalla = function () {
  
    Guardado = false;
    $("#hfEntradaGanadoID").val("0");
    $("#hfEmbarqueID").val("0");

    $('#txtEditFolioEntrada').val('');
    $('#txtEditFolioEntrada').attr('disabled', false);

    $('#txtCorral').val('');
    $('#txtCorral').attr('disabled', false);
    $("#txtEditCabezasRecibidas").val('');
    $("#hfEntradaGanadoID").val('0');
    $("#hfEmbarqueID").val('0');
    $("#hfCorralID").val('0');
    $("#rdnManejosinEstres").val('');
    $("#hfEntradaGanadoID").val('0');
    $("#txtNoEditOrigen").val('');
    $("#txtNoEditVigilancia").val('');
    $("#txtNoEditOperador").val('');
    $("#txtNoEditCabezasOrigen").val('');
    $("#txtNoEditPlacas").val('');
    $("#txtNoEditCabezasCorral").val('0');
    $("#txtNoEditFaltante").val('0');
    
    $('#btnGuardar').attr('disabled', true);
    $('#btnCancelar').attr('disabled', true);
    $("#ddlCondicionesJaula").val(0);
    
    var rdnManejosinEstres = $('#rdnManejosinEstres');
    rdnManejosinEstres.attr('checked', false);
    
    LlenarCondiciones();
};

ObtieneListaGrid = function() {
    var lista = $('#divGridCondiciones table tbody tr');
    return lista;
};

ContarCondiciones = function() {
    var lista = ObtieneListaGrid();
    var totalCondicion = 0;

    $.each(lista, function() {
        $(this).children("td").each(function() {
            var cantidad = $('.span3', this).val();
            if (cantidad != "") {
                totalCondicion += parseInt(cantidad);
            }
        });
    });
    return totalCondicion;
};

CalculaCabezasCorral = function () {

    var lista = ObtieneListaGrid();
    var totalCondicion = 0;

    $.each(lista, function () {
        $(this).children("td").each(function () {
            var condicionId = parseInt($(this).attr('data-CondicionID'));
            var cantidad = $('.span3', this).val();
            //Condición = 3 corresponde al ganado Muerto
            if (cantidad != "" && condicionId != 3) { 
                totalCondicion += parseInt(cantidad);
            }
        });
    });
    $("#txtNoEditCabezasCorral").val(totalCondicion);
};

LlenarCondiciones = function () {
    var datos = {};

    BloquearPantalla();
    $.ajax({
        type: "POST",
        url: rutaPantalla + '/TraerCondiciones',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == null || msg.d.length == 0) {
                bootbox.alert(window.MsgSinCondicionesGanado);
                DesbloquearPantalla();
                $('#divGridCondiciones').html('<strong>' + window.MsgSinCondicionesGanado + '</strong>');
                $('#divGridCondiciones').processTemplate();
                return false;
            }
           
            var datosCondiciones = {};
            datosCondiciones.Condiciones = msg.d;

            $('#divGridCondiciones').html('');
            $('#divGridCondiciones').setTemplateURL('../Templates/GridTarjetaRecepcionGanado.htm');
            $('#divGridCondiciones').processTemplate(datosCondiciones);

            DesbloquearPantalla();
            $('#txtEditFolioEntrada').focus();
            
            var lista = ObtieneListaGrid();

            $.each(lista, function() {
                $(this).children("td").each(function() {

                    var padre = this;

                    var txt = $('.span3', padre);
                    var condicionId = parseInt($(padre).attr('data-CondicionID'));

                    txt.keydown(function (e) {
                        var code = (e.keyCode ? e.keyCode : e.which);
                        if (code == 13 && $(this).val() != "") {

                            var inputs = $(this).closest('form').find(':input:enabled');
                            inputs.eq(inputs.index(this) + 1).focus();
                        }
                    });
                    if (condicionId != 3) {
                        txt.focusout(function () {
                            CalculaCabezasCorral(txt.val());
                        });
                    }
                });
            });            
            return true;
        },
        error: function () {
            DesbloquearPantalla();
            bootbox.dialog({
                message: window.MsgErrorConsultarCondiciones,
                buttons: {
                    Aceptar: {
                        label: window.Aceptar,
                        callback: function () {
                        }
                    }
                }
            });
        }
    });
};

TraerCondicionesJaula = function() {
    EjecutarWebMethod(window.location.pathname + '/ConcultarCondicionesJaula', {}, obtenerCondicionesJaulaExitoso
                , "Ocurrió un error al consultar la condiciones de la jaula");
};

obtenerCondicionesJaulaExitoso = function(msg) {
    $('#ddlCondicionesJaula').html('');

    var valores = {};
    var recursos = {};
    recursos.Seleccione = "Seleccione";
    valores.Recursos = recursos;

    var listaValores = new Array();
    if (msg.d != null || msg.d.length > 0) {
        var condiciones = msg.d;
        for (var i = 0; i < condiciones.length; i++) {
            var valor = {};
            valor.Clave = condiciones[i].CondicionJaulaID;
            valor.Descripcion = condiciones[i].Descripcion;

            listaValores.push(valor);
        }
    }
    valores.Valores = listaValores;

    $('#ddlCondicionesJaula').setTemplateURL('../Templates/ComboGenerico.htm');
    $('#ddlCondicionesJaula').processTemplate(valores);
};

CalculaFaltante = function(valor) {

    var faltante = 0;
    var cabezasOrigen = parseInt($("#txtNoEditCabezasOrigen").val());

    if (!isNaN(cabezasOrigen) && cabezasOrigen > 0) {
        faltante = cabezasOrigen - valor;   
    }

    $("#txtNoEditFaltante").val(faltante);

};

$(document).ready(function () {

    //Linea que se utiliza para evitar el error que tiene el bootstrap modal, de que se comporta
    //de manera extraña al levantar mas de 1 modal
    $.fn.modal.Constructor.prototype.enforceFocus = function () { };
    
    var txtEditFolioEntrada = $('#txtEditFolioEntrada'),
        txtEditCabezasRecibidas = $('#txtEditCabezasRecibidas'),
        txtCorral = $('#txtCorral'),
        rdnManejosinEstres = $('#rdnManejosinEstres'),
        txtNoEditOrigen = $("#txtNoEditOrigen"),
        txtNoEditVigilancia = $("#txtNoEditVigilancia"),
        txtNoEditOperador = $("#txtNoEditOperador"),
        txtNoEditCabezasOrigen = $("#txtNoEditCabezasOrigen"),
        txtNoEditPlacas = $("#txtNoEditPlacas"),
        txtNoEditCabezasCorral = $("#txtNoEditCabezasCorral"),
        txtNoEditFecha = $('#txtNoEditFecha'),
        txtNoEditFaltante = $("#txtNoEditFaltante");
    
    txtNoEditOrigen.attr('disabled', true);
    txtNoEditVigilancia.attr('disabled', true);
    txtNoEditOperador.attr('disabled', true);
    txtNoEditCabezasOrigen.attr('disabled', true);
    txtNoEditPlacas.attr('disabled', true);
    txtNoEditCabezasCorral.attr('disabled', true);
    txtNoEditFecha.attr('disabled', true);
    txtNoEditFaltante.attr('disabled', true);

    txtNoEditCabezasOrigen.css('text-align', 'right');
    txtNoEditCabezasCorral.css('text-align', 'right');
    txtNoEditFaltante.css('text-align', 'right');

    var fecha = new Date();
    txtNoEditFecha.val(FechaFormateada(fecha));
    
    txtEditFolioEntrada.numeric().attr("maxlength", "8");
    txtEditFolioEntrada.keydown(function (e) {
        var code = (e.keyCode ? e.keyCode : e.which);
        if (code == 13 && $(this).val() != "") {
            var inputs = $(this).closest('form').find(':input:enabled');
            inputs.eq(inputs.index(this) + 1).focus();
        }
    });
    txtEditFolioEntrada.focusout(function (e) {

        if ($(this).val() != "") {
            if (!BuscarEntrada()) {
                e.preventDefault();
            }
        }
    });

    txtEditCabezasRecibidas.numeric();
    txtEditCabezasRecibidas.attr("maxlength", "8");
    txtEditCabezasRecibidas.focusout(function () {
        if ($(this).val() != "") {
            CalculaFaltante($(this).val());
        }
    });
    txtEditCabezasRecibidas.keydown(function (e) {
        var code = (e.keyCode ? e.keyCode : e.which);
        if (code == 13 && $(this).val() != "") {
            var inputs = $(this).closest('form').find(':input:enabled');
            inputs.eq(inputs.index(this) + 1).focus();
        }
    });
    txtEditCabezasRecibidas.css('text-align', 'right');
    
    txtCorral.attr("maxlength", "10");
    txtCorral.focusout(function (e) {
        if ($(this).val() != "") {
            if (!TraerCorral()) {
                e.preventDefault();
            }
        }
    });
    txtCorral.keydown(function (e) {
        var code = (e.keyCode ? e.keyCode : e.which);
        if (code == 13 && $(this).val() != "") {
            var inputs = $(this).closest('form').find(':input:enabled');
            inputs.eq(inputs.index(this) + 1).focus();
        }
    });
    
    $("#ddlCondicionesJaula").keydown(function (e) {
        var code = (e.keyCode ? e.keyCode : e.which);
        if (code == 13 && $(this).val() != "") {
            var inputs = $(this).closest('form').find(':input:enabled');
            inputs.eq(inputs.index(this) + 1).focus();
        }
    });

    
    rdnManejosinEstres.attr('checked', false);
    
    LlenarCondiciones();
    TraerCondicionesJaula();
    
    $('#btnGuardar').click(function () {
        Guardar();
    });

    $('#btnGuardar').attr('disabled', true);
    $('#btnCancelar').attr('disabled', true);

    $('#btnCancelar').click(function () {
        if (Guardado) {
            $('#btnGuardar').attr('disabled', false);
            Guardado = false;
            return false;
        }
        bootbox.dialog({
            message: window.MsgSalirSinGuardar,
            buttons: {
                success: {
                    label: "Si",
                    callback: function () {
                        txtEditFolioEntrada.val('');
                        LimpiarPantalla();
                    }
                },
                danger: {
                    label: "No",
                    callback: function () {
                        return true;
                    }
                }
             }
        });
        return true;
    });
});
 