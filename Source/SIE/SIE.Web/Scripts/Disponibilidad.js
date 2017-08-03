/// <reference path="../assets/plugins/jquery-1.7.1-vsdoc.js" />
/// <reference path="~/assets/plugins/bootstrap-bootbox/js/bootbox.min.js" />
/// <reference path="~/Scripts/jscomun.js" />

var rutaPantalla = location.pathname;
var valorAnterior;
var valorZilmax = '6';
var fechaInicioSemana6;

$(document).ready(function () {
    CargarSemanas();
    CargarFechaInicioSemana6();

    $('#ddlSemanas').click(function () {
        valorAnterior = $("#ddlSemanas").val();
    });

    $('#ddlSemanas').change(function () {
        var valorViejo = valorAnterior;
        if (ValidarExisteModificacion()) {
            bootbox.dialog({
                message: window.CambiarSemana,
                buttons: {
                    Aceptar: {
                        label: window.Si,
                        callback: function () {
                            CargarLotesDisponibilidad();
                        }
                    },
                    Cancelar: {
                        label: window.No,
                        callback: function () {
                            $("#ddlSemanas").val(valorViejo);
                        }
                    }
                }
            });
        } else {
            CargarLotesDisponibilidad();
        }

    });
    $('#btnGuardar').click(function () {
        Guardar();
    });

    $('#btnCancelar').click(function () {
        if (ValidarExisteModificacion()) {
            bootbox.dialog({
                message: window.SalirSinGuardar,
                buttons: {
                    Aceptar: {
                        label: window.Si,
                        callback: function () {
                            LimpiarPantalla();
                            CargarLotesDisponibilidad();
                        }
                    },
                    Cancelar: {
                        label: window.No,
                        callback: function () {
                        }
                    }
                }
            });
        } else {
            LimpiarPantalla();
            CargarLotesDisponibilidad();
        }
    });

    $('#GridCorrales').on('click', '.semanasMas', function () {
        var renglon = $(this.parentNode.parentNode.parentNode.parentNode);
        //if ($(renglon).attr('data-editandoDias') == 'false') {
            var textbox = $(this.parentNode.parentNode).find('.semanas');
            var valor = TryParseInt($(textbox).val(), 0);
            if (valor < 5) {
                textbox.val(valor + 1);
                if (valor + 1 != 0) {
                    $(renglon).attr('data-editandoSemanas', 'true');
                    $(renglon).attr('data-modificado', '1');
                } else {
                    $(renglon).attr('data-editandoSemanas', 'false');
                    $(renglon).attr('data-modificado', '0');
                }
                var fechaAsignadaTexto = $(renglon).find('.fechaDisponibilidad').text();
                var fechaAsignada = ToDate(fechaAsignadaTexto);
                fechaAsignada = fechaAsignada.addDays(7);
                $(renglon).find('.fechaDisponibilidad').text(FechaFormateada(fechaAsignada));

                
            }
        //}

    });
    $('#GridCorrales').on('click', '.semanasMenos', function () {
        var renglon = $(this).closest('tr'); //$(this.parentNode.parentNode.parentNode.parentNode);
        //if ($(renglon).attr('data-editandoDias') == 'false') {
            var textbox = $(this.parentNode.parentNode).find('.semanas');
            var valor = TryParseInt($(textbox).val(), 0);
            if (valor > -5) {
                if ($('#ddlSemanas').val() == valorZilmax) {
                    if((valor - 1) == -2) {
                        bootbox.dialog({
                            message: window.Zilmax,
                            buttons: {
                                Aceptar: {
                                    label: window.Aceptar,
                                    callback: function () {
                                    }
                                }
                            }
                        });
                        return false;
                    }
                }
                textbox.val(valor - 1);
                if (valor - 1 != 0) {
                    $(renglon).attr('data-editandoSemanas', 'true');
                    $(renglon).attr('data-modificado', '1');
                } else {
                    $(renglon).attr('data-editandoSemanas', 'false');
                    $(renglon).attr('data-modificado', '0');
                }
                var fechaAsignadaTexto = $(renglon).find('.fechaDisponibilidad').text();
                var fechaAsignada = ToDate(fechaAsignadaTexto);
                fechaAsignada = fechaAsignada.removeDays(7);
                if (fechaAsignada < fechaInicioSemana6) {
                    bootbox.dialog({
                        message: window.Zilmax,
                        buttons: {
                            Aceptar: {
                                label: window.Aceptar,
                                callback: function () {
                                }
                            }
                        }
                    });
                    textbox.val(valor);
                    return false;
                }
                $(renglon).find('.fechaDisponibilidad').text(FechaFormateada(fechaAsignada));
            }
        //}
        return true;
    });

    $('#GridCorrales').on('click', '.diasMas', function () {
        var renglon = $(this.parentNode.parentNode.parentNode.parentNode);
        //if ($(renglon).attr('data-editandoSemanas') == 'false') {
            var textbox = $(this.parentNode.parentNode).find('.dias');
            if ($(textbox).attr('data-SumarDias') == 'true') {
                var valor = TryParseInt($(textbox).val(), 0);
                if (valor < 7) {
                    textbox.val(valor + 1);
                    if (valor + 1 != 0) {
                        $(renglon).attr('data-editandoDias', 'true');
                        $(renglon).attr('data-modificado', '1');
                    } else {
                        $(renglon).attr('data-editandoDias', 'false');
                        $(renglon).attr('data-modificado', '0');
                    }
                    var fechaAsignadaTexto = $(renglon).find('.fechaDisponibilidad').text();
                    var fechaAsignada = ToDate(fechaAsignadaTexto);
                    fechaAsignada = fechaAsignada.addDays(1);
                    $(renglon).find('.fechaDisponibilidad').text(FechaFormateada(fechaAsignada));
                    
                }
            }
        //}
    });
    $('#GridCorrales').on('click', '.diasMenos', function () {
        var renglon = $(this.parentNode.parentNode.parentNode.parentNode);
        //if ($(renglon).attr('data-editandoSemanas') == 'false') {
            var textbox = $(this.parentNode.parentNode).find('.dias');
            if ($(textbox).attr('data-SumarDias') == 'true') {
                var valor = TryParseInt($(textbox).val(), 0);
                if (valor > -7) {
                    textbox.val(valor - 1);
                    if (valor - 1 != 0) {
                        $(renglon).attr('data-editandoDias', 'true');
                        $(renglon).attr('data-modificado', '1');
                    } else {
                        $(renglon).attr('data-editandoDias', 'false');
                        $(renglon).attr('data-modificado', '0');
                    }

                    var fechaAsignadaTexto = $(renglon).find('.fechaDisponibilidad').text();
                    var fechaAsignada = ToDate(fechaAsignadaTexto);
                    fechaAsignada = fechaAsignada.removeDays(1);
                    if (fechaAsignada < fechaInicioSemana6) {
                        bootbox.dialog({
                            message: window.Zilmax,
                            buttons: {
                                Aceptar: {
                                    label: window.Aceptar,
                                    callback: function () {
                                        
                                    }
                                }
                            }
                        });
                        textbox.val(valor);
                        return false;
                    }
                    $(renglon).find('.fechaDisponibilidad').text(FechaFormateada(fechaAsignada));
                    
                }
            }
        //}
    });

});

LimpiarPantalla = function () {
    $('#ddlSemanas').val('6');
    $('#GridCorrales').html('');
};

ValidarExisteModificacion = function () {
    var existeModificacion = false;
    $('#GridCorrales table tbody tr').each(function () {
        var modificado = $(this).attr('data-modificado');
        if (modificado == '1') {
            existeModificacion = true;
            return false;
        }
        return true;
    });
    return existeModificacion;
};

Guardar = function () {
    var filtro = CargarInformacionGuardar();

    if (filtro.ListaLoteDisponibilidad.length == 0) {
        bootbox.dialog({
            message: window.GuardarSinCambios,
            buttons: {
                Aceptar: {
                    label: window.Aceptar,
                    callback: function () {
                    }
                }
            }
        });
        return false;
    }
    var datos = { 'filtroDisponilidadInfo': filtro };
    if (datos != null) {
        BloquearPantalla();
        $.ajax({
            type: "POST",
            url: rutaPantalla + '/Guardar',
            data: JSON.stringify(datos),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function () {
                DesbloquearPantalla();
                bootbox.dialog({
                    message: window.GuardadoExito,
                    buttons: {
                        Aceptar: {
                            label: window.Aceptar,
                            callback: function () {
                                CargarLotesDisponibilidad();
                            }
                        }
                    }
                });
            },
            error: function () {
                DesbloquearPantalla();
                bootbox.dialog({
                    message: window.ErrorGuardar,
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
    }
    return true;
};

CargarInformacionGuardar = function () {
    var filtro = {};
    var listaLotes = new Array();
    var indice = 0;
    $('#GridCorrales table tbody tr').each(function () {
        var modificado = $(this).attr('data-modificado');
        if (modificado == '1') {
            var lote = {};
            lote.FechaDisponibilidad = ToDate($(this).find('.fechaDisponibilidad').text());
            lote.LoteID = $(this).attr('data-loteid');
            lote.FechaInicioLote = new Date(parseInt($(this).attr('data-FechaInicio').replace(/^\D+/g, '')));
            lote.DiasEngorda = $(this).attr('data-diasEngorda');
            listaLotes[indice] = lote;
            indice = indice + 1;
        }
    });
    filtro.ListaLoteDisponibilidad = listaLotes;
    filtro.Semanas = $('#ddlSemanas').val();
    return filtro;
};

//Funcion para cargar las Semanas de la Disponibilidad
CargarSemanas = function () {
    var datos = {};
    BloquearPantalla();
    $.ajax({
        type: "POST",
        url: rutaPantalla + '/TraerSemanas',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == null || msg.d.length == 0) {
                DesbloquearPantalla();
                bootbox.dialog({
                    message: window.SinSemanas,
                    buttons: {
                        Aceptar: {
                            label: window.Aceptar,
                            callback: function () {

                            }
                        }
                    }
                });
                return false;
            }
            $(msg.d).each(function () {
                var semana = this;
                var html = '<option value=' + semana.SemanaID + '>' + semana.Descripcion + ' </option>';
                $('#ddlSemanas').append(html);
            });
            CargarLotesDisponibilidad();
            //DesbloquearPantalla();
            return true;
        },
        error: function () {
            DesbloquearPantalla();
            bootbox.dialog({
                message: window.ErrorSemanas,
                buttons: {
                    Aceptar: {
                        label: 'Aceptar',
                        callback: function () {

                        }
                    }
                }
            });
        }
    });
};

//Función para cargar la información de las Calidades de Ganado para su captura
CargarLotesDisponibilidad = function () {
    var filtro = {};
    var semanas = TryParseInt($('#ddlSemanas').val(), 0);
    filtro.Semanas = semanas;
    filtro.Fecha = new Date();
    var datos = { 'filtroDisponilidadInfo': filtro };
    BloquearPantalla();
    $.ajax({
        type: "POST",
        url: rutaPantalla + '/TraerDisponibilidad',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            if (msg.d == null || msg.d.length == 0) {
                DesbloquearPantalla();
                bootbox.dialog({
                    message: window.SinCorrales,
                    buttons: {
                        Aceptar: {
                            label: window.Aceptar,
                            callback: function () {
                                $('#GridCorrales').html('');
                            }
                        }
                    }
                });
                return false;
            }
            $(msg.d).each(function () {
                var lote = this;
                var fechaDisponibilidad = new Date(parseInt(lote.FechaDisponibilidad.replace(/^\D+/g, '')));
                lote.FechaDisponibilidad = FechaFormateada(fechaDisponibilidad);

                var fechaAsignada = new Date(parseInt(lote.FechaAsignada.replace(/^\D+/g, '')));
                lote.FechaAsignada = FechaFormateada(fechaAsignada);
            });
            var corrales = {};
            var recursos = {};
            recursos.CabeceroCorral = window.CabeceroCorral;
            recursos.CabeceroLote = window.CabeceroLote;
            recursos.CabeceroTipo = window.CabeceroTipo;
            recursos.CabeceroClasificacion = window.CabeceroClasificacion;
            recursos.CabeceroCabezas = window.CabeceroCabezas;
            recursos.CabeceroPesoOrigen = window.CabeceroPesoOrigen;
            recursos.CabeceroMerma = window.CabeceroMerma;
            recursos.CabeceroPesoProyectado = window.CabeceroPesoProyectado;
            recursos.CabeceroGananciaDiaria = window.CabeceroGananciaDiaria;
            recursos.CabeceroDiasEngorda = window.CabeceroDiasEngorda;
            recursos.CabeceroDiasProyectados = window.CabeceroDiasProyectados;

            recursos.Cabecero1FechaReimplante = window.Cabecero1FechaReimplante;
            recursos.Cabecero1Peso = window.Cabecero1Peso;
            recursos.Cabecero1GananciaDiaria = window.Cabecero1GananciaDiaria;

            recursos.Cabecero2FechaReimplante = window.Cabecero2FechaReimplante;
            recursos.Cabecero2Peso = window.Cabecero2Peso;
            recursos.Cabecero2GananciaDiaria = window.Cabecero2GananciaDiaria;

            recursos.Cabecero3FechaReimplante = window.Cabecero3FechaReimplante;
            recursos.Cabecero3Peso = window.Cabecero3Peso;
            recursos.Cabecero3GananciaDiaria = window.Cabecero3GananciaDiaria;

            recursos.CabeceroDiasF4 = window.CabeceroDiasF4;
            recursos.CabeceroDiasZilmax = window.CabeceroDiasZilmax;

            recursos.CabeceroFechaDisponibilidad = window.CabeceroFechaDisponibilidad;
            recursos.CabeceroFechaAsignada = window.CabeceroFechaAsignada;
            recursos.CabeceroSemanas = window.CabeceroSemanas;
            recursos.CabeceroDias = window.CabeceroDias;
            corrales.Corrales = msg.d;
            corrales.Recursos = recursos;

            $('#GridCorrales').html('');
            $('#GridCorrales').setTemplateURL('../Templates/GridCorralesDisponibilidad.htm');
            $('#GridCorrales').processTemplate(corrales);
            DesbloquearPantalla();
            return true;
        },
        error: function () {
            DesbloquearPantalla();
            bootbox.dialog({
                message: window.ErrorCorrales,
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

//Función para obtener la fecha de inicio de la semana 6
CargarFechaInicioSemana6 = function () {
    var datos = {};
    BloquearPantalla();
    $.ajax({
        type: "POST",
        url: rutaPantalla + '/TraerFechaInicioSemana6',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            fechaInicioSemana6 = new Date(parseInt(msg.d.replace(/^\D+/g, '')));
            return true;
        },
        error: function () {
            DesbloquearPantalla();
            bootbox.dialog({
                message: 'Ocurrio un error al consultar la fecha de inicio de la semana zilmax',
                buttons: {
                    Aceptar: {
                        label: 'Aceptar',
                        callback: function () {

                        }
                    }
                }
            });
        }
    });
};