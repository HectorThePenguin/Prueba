/// <reference path="../assets/plugins/jquery-1.7.1-vsdoc.js" />
/// <reference path="../assets/plugins/data-tables/jquery.dataTables.js" />
/// <reference path="../assets/plugins/jquery-linq/linq-vsdoc.js" />
/// <reference path="~/assets/plugins/bootstrap-bootbox/js/bootbox.min.js" />
/// <reference path="../assets/plugins/data-tables/jquery.FixedHeaderTable.js" />




var EstatusAbierto = 1;
var EstatusCerrado = 2;
var CheckList = {};
var Guardado = false;
var Valido = true;
var FechaCerrado;
$(document).ready(function () {

    if ($('#hfErrorImprimir').val() == '1') {
        bootbox.alert(window.ErrorImprimir);
        $('#hfErrorImprimir').val('');
    }
    $('.soloNumeros').bind("cut copy paste", function (e) {
        e.preventDefault();
    });

    $('.soloNumeros').numericInput();
    $('#txtPesoCorte').numeric();
    $('#btnGuardar').click(function () {
        Guardar();
    });
    $('#btnCancelar').click(function () {

        if (Guardado) {
            $('#responsive').modal('hide');
            ConsultarCorrales();
            $('#btnGuardar').attr('disabled', false);
            Guardado = false;
            return false;
        }
        bootbox.dialog({
            message: window.SalirSinGuardar,
            buttons: {
                Aceptar: {
                    label: window.Si,
                    callback: function () {
                        $('#responsive').modal('hide');
                        ConsultarCorrales();
                        return true;
                    }
                },
                Cancelar: {
                    label: window.No,
                    callback: function () {
                        return true;
                    }
                }
            }
        });

    });

    $('body').on("click", ".botonCerrar", function () {
        $("#chkRequiereRevision").attr('checked', false);
        CargarInformacionCorrales(this);
    });

    $('#imgImprimir').click(function () {
        if (!Guardado) {
            bootbox.alert(window.Imprimir);
            return;
        }
        window.open('ImpresionCheckList.aspx?LoteID=' + CheckList.LoteID + '&OrganizacionID=' + $('#hfOrganizacionID').val() + '');
        bootbox.alert(window.Impreso);
    });

    $('body').on("click", ".botonReimprimir", function () {
        var renglonSeleccionado = $(this.parentNode.parentNode);
        var loteID = parseInt(renglonSeleccionado.attr('data-LoteID'));
        window.open('ImpresionCheckList.aspx?LoteID=' + loteID + '&OrganizacionID=' + $('#hfOrganizacionID').val() + '');
        bootbox.alert(window.Impreso);
    });

    ConsultarCorrales();

});

function FormatearCantidad(cantidad) {
    //Seperates the components of the number
    var n = cantidad.toString().split(".");
    //Comma-fies the first part
    n[0] = n[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    //Combines the two sections
    return n.join(".");
}

Guardar = function () {

    BloquearModal();
    Valido = true;
    ValidarAntesGuardar();
    if (!Valido) {
        bootbox.alert(window.DatosObligatorios);
        DesbloquearModal();
        return;
    }
    CargarInformacionGuardar();
    if (!(CheckList.FechaFin instanceof Date)) {
        FormatearFechasGuardar();
    }
    var CheckListCorralInfo = CheckList;
    var datos = { 'checkListCorralInfo': CheckListCorralInfo };

    $.ajax({
        type: "POST",
        url: 'CierreCorral.aspx/Guardar',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == 'OK') {
                bootbox.alert(window.GuardoExito);
                $('#btnGuardar').attr('disabled', true);
                Guardado = true;
                DesbloquearModal();
            }
        },
        error: function () {
            DesbloquearModal();
            bootbox.alert(window.ErrorGuardar);
        }
    });
};

ValidarAntesGuardar = function () {

    var names = {};
    $(':radio').each(function () {
        names[$(this).attr('name')] = true;
    });
    var count = 0;
    $.each(names, function () {
        count++;
    });
    if ($(':radio:checked').length === count) {
        Valido = true;
    } else {
        Valido = false;
    }

    if ($('#txtCabezasConteo').val() == undefined || $('#txtCabezasConteo').val() == '' || $('#txtCabezasConteo').val() == '0') {
        Valido = false;
    }
};


FormatearFechasGuardar = function () {

    var fecha = new Date();
    CheckList.Fecha = fecha;

    var fechaCorte = new Date(parseInt(CheckList.FechaCorte.replace(/^\D+/g, '')));
    CheckList.FechaCorte = fechaCorte;

    var fechaSacrificio = new Date(parseInt(CheckList.FechaSacrificio.replace(/^\D+/g, '')));
    CheckList.FechaSacrificio = fechaSacrificio;

    var fechaAbierto = new Date(parseInt(CheckList.FechaAbierto.replace(/^\D+/g, '')));
    CheckList.FechaAbierto = fechaAbierto;

    var fecha1Reimplante = new Date(parseInt(CheckList.Fecha1Reimplante.replace(/^\D+/g, '')));
    CheckList.Fecha1Reimplante = fecha1Reimplante;

    var fecha2Reimplante = new Date(parseInt(CheckList.Fecha2Reimplante.replace(/^\D+/g, '')));
    CheckList.Fecha2Reimplante = fecha2Reimplante;


    var fechaCerrado = ToDate(CheckList.FechaCerrado);
    CheckList.FechaCerrado = fechaCerrado;

    var fechaInicio = new Date(parseInt(CheckList.FechaInicio.replace(/^\D+/g, '')));
    CheckList.FechaInicio = fechaInicio;

    var fechaFin = new Date(parseInt(CheckList.FechaFin.replace(/^\D+/g, '')));
    CheckList.FechaFin = fechaFin;
};

CargarInformacionGuardar = function () {

    CheckList.CabezasConteo = parseInt($('#txtCabezasConteo').val());
    CheckList.RequiereRevision = $("#chkRequiereRevision").is(':checked');
    $('#divGridConcepto table tbody tr').each(function () {

        var bueno = $('.radioBueno', this).is(':checked');
        var descripcionConcepto = $('.columnaDescripcion', this).text();
        var observacion = $('.observaciones', this).val();

        var conceptos = Enumerable.From(CheckList.ListaConceptos).Where(function (x) { return x.ConceptoDescripcion.trim() == descripcionConcepto.trim(); })
            .Select(function (x) { return x; }).ToArray();

        var concepto = Enumerable.From(conceptos).FirstOrDefault();
        if (concepto != undefined && concepto != null) {
            concepto.Observaciones = observacion;
            if (bueno) {
                concepto.Bueno = true;
            } else {
                concepto.Malo = true;
            }
        }
    });

    $('#divGridAcciones table tbody tr').each(function () {

        var observaciones = $('.comboObservacion', this).children(':selected').text();
        var descripcionAccion = $('.columnaDescripcion', this).text();
        var accionTomada = $('.accionesTomadas', this).val();

        var acciones = Enumerable.From(CheckList.ListaAcciones).Where(function (x) { return x.Descripcion.trim() == descripcionAccion.trim(); })
            .Select(function (x) { return x; }).ToArray();

        var accion = Enumerable.From(acciones).FirstOrDefault();

        if (accion != undefined && accion != null) {
            accion.Observacion = observaciones;
            accion.AccionesTomadas = accionTomada;
        }

    });

};

CargarInformacionCorrales = function (row) {
    var renglonSeleccionado = $(row.parentNode.parentNode);
    var loteID = parseInt(renglonSeleccionado.attr('data-LoteID'));
    FechaCerrado = $('.columnaFechaFin', renglonSeleccionado).text();

    var FiltroCierreCorral = {};
    FiltroCierreCorral.LoteID = loteID;
    var datos = { 'filtroCierreCorral': FiltroCierreCorral };


    BloquearPantalla();
    $.ajax({
        type: "POST",
        url: 'CierreCorral.aspx/TraerCorralCheckList',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == null || msg.d.length == 0) {
                DesbloquearPantalla();
                bootbox.alert(window.CheckListSinInformacion);
                return false;
            }

            if (msg.d.Corral == 'PARTIDA') {
                DesbloquearPantalla();
                bootbox.alert(window.PartidaAbierta + msg.d.LoteID);
                return false;
            }
            
            if (msg.d.Corral == 'CONTEOCERO') {
                DesbloquearPantalla();
                bootbox.alert(window.ConteoCero);
                return false;
            }
            
            if (msg.d.Corral == 'PESOCOMPRA') {
                DesbloquearPantalla();
                bootbox.alert(window.PesoCompra);
                return false;
            }


            CheckList = msg.d;
            $('#txtCorral').val(CheckList.Corral);
            $('#txtLote').val(CheckList.Lote);
            var fechaActual = new Date();
            $('#txtFechaCorral').val(FechaFormateada(fechaActual));

            $('#txtPesoCorte').val(FormatearCantidad(CheckList.PesoCorte));
            $('#txtCabezasSistemas').val(CheckList.CabezasSistema);
            //var fechaSacrificio = new Date(parseInt(CheckList.FechaSacrificio.replace(/^\D+/g, '')));
            //$('#txtFechaSacrificio').val(FechaFormateada(fechaSacrificio));
            var fechaAbierto = new Date(parseInt(CheckList.FechaAbierto.replace(/^\D+/g, '')));
            $('#txtFechaAbierto').val(FechaFormateada(fechaAbierto));
            //var fecha1Reimplante = new Date(parseInt(CheckList.Fecha1Reimplante.replace(/^\D+/g, '')));
            //$('#txtFecha1Reimplante').val(FechaFormateada(fecha1Reimplante));

            CheckList.FechaCerrado = FechaCerrado;
            $('#txtFechaCerrado').val(CheckList.FechaCerrado);
            //var fechaCerrado = new Date(parseInt(CheckList.FechaCerrado.replace(/^\D+/g, '')));
            //$('#txtFechaCerrado').val(FechaFormateada(fechaCerrado));
            //var fecha2Reimplante = new Date(parseInt(CheckList.Fecha2Reimplante.replace(/^\D+/g, '')));
            //$('#txtFecha2Reimplante').val(FechaFormateada(fecha2Reimplante));

            //var diasEngorda = parseInt(DiferenciaFechas(fechaAbierto, fechaActual, 'days'));
            //CheckList.DiasEngorda = diasEngorda;

            //$('#txtDiasEngorda').val(CheckList.DiasEngorda + window.Dias);
            $('#txtOcupacion').val(CheckList.Ocupacion + ' %');
            $('#txtTipo').val(CheckList.Tipo);
            $('#txtRaza').val(CheckList.Raza);
            $('#txtCabezasConteo').val('');


            var listaAccionesFinal = {};
            var recursos = {};
            recursos.Estructura = window.CabeceroEstructura;
            recursos.Observaciones = window.CabeceroAccionesObservaciones;
            recursos.AccionesTomadas = window.CabeceroAccionesTomadas;

            listaAccionesFinal.Acciones = CheckList.ListaAcciones;
            listaAccionesFinal.Recursos = recursos;

            var listaConceptosFinal = {};
            var recursosConceptos = {};
            recursosConceptos.Concepto = window.CabeceroConcepto;
            recursosConceptos.Bueno = window.CabeceroBueno;
            recursosConceptos.Malo = window.CabeceroMalo;
            recursosConceptos.Observaciones = window.CabeceroObservaciones;

            listaConceptosFinal.Conceptos = CheckList.ListaConceptos;
            listaConceptosFinal.Recursos = recursosConceptos;

            $('#divGridAcciones').html('');
            $('#divGridConcepto').html('');
            $('#divGridAcciones').setTemplateURL('../Templates/GridAccionesCierreCorral.htm');
            $('#divGridAcciones').processTemplate(listaAccionesFinal);

            $('#divGridConcepto').setTemplateURL('../Templates/GridConceptosCierreCorral.htm');
            $('#divGridConcepto').processTemplate(listaConceptosFinal);

            DesbloquearPantalla();
            $('#responsive').modal('show');

            $('.Alfanumerico').alpha({
                allow: '0123456789',
                disallow: '!@#$%^&*()+=[]\\\';,/{}|":<>?~`.-_¿¡¨¨~``^^°´´'
            });
            return true;


        },
        error: function () {
            DesbloquearPantalla();
            bootbox.alert(window.ErrorInformacionCorral);
        }
    });
};

maxLengthCheck = function (object) {
    if (object.value.length > object.maxLength) {
        object.value = object.value.slice(0, object.maxLength);
    }
};

ToDate = function (str1) {
    // str1 format should be dd/mm/yyyy. Separator can be anything e.g. / or -. It wont effect
    var dt1 = parseInt(str1.substring(0, 2));
    var mon1 = parseInt(str1.substring(3, 5));
    var yr1 = parseInt(str1.substring(6, 10));
    var date1 = new Date(yr1, mon1 - 1, dt1);
    return date1;
};

BloquearPantalla = function () {
    var lock = document.getElementById('skm_LockPane');
    if (lock) {
        lock.className = 'LockOn';
        $('#skm_LockPane').spin(
            {
                top: '30',
                color: '#6E6E6E'
            });
    }
};

DesbloquearPantalla = function () {
    $("#skm_LockPane").spin(false);
    var lock = document.getElementById('skm_LockPane');
    lock.className = 'LockOff';
};

BloquearModal = function () {
    var lock = document.getElementById('divModal');
    if (lock) {
        lock.className = 'LockOn';
        $('#divModal').spin(
            {
                top: '30',
                color: '#6E6E6E'
            });
    }
};

DesbloquearModal = function () {
    $("#divModal").spin(false);
    var lock = document.getElementById('divModal');
    lock.className = 'LockOff';
};

ConsultarCorrales = function () {
    var FiltroCierreCorral = {};
    FiltroCierreCorral.FechaEjecucion = new Date();
    var datos = { 'filtroCierreCorral': FiltroCierreCorral };

    var corrales;
    BloquearPantalla();
    $.ajax({
        type: "POST",
        url: 'CierreCorral.aspx/TraerCorrales',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == null || msg.d.length == 0) {
                bootbox.alert(window.SinCorrales);
                DesbloquearPantalla();
                return;
            }
            corrales = msg.d;
            if (corrales != null) {

                $(corrales).each(function () {
                    var fechaInicio = new Date(parseInt(this.FechaInicio.replace(/^\D+/g, '')));
                    var fechaFin = new Date(parseInt(this.FechaFin.replace(/^\D+/g, '')));

                    this.FechaInicio = FechaFormateada(fechaInicio);
                    this.FechaFin = FechaFormateada(fechaFin);

                    if (this.Estatus == EstatusCerrado) {
                        this.Estatus = 'circuloCerrado';
                        this.EstatusDescripcion = window.Cerrado;
                    } else {
                        this.EstatusDescripcion = window.Abierto;
                        var fechaActual = new Date();
                        var diasDiferencia = parseInt(DiferenciaFechas(fechaFin, fechaActual, 'days'));
                        if (diasDiferencia > 3) {
                            this.Estatus = 'circuloRetraso';
                        } else {
                            this.Estatus = 'circuloAbierto';
                        }
                    }
                });

                var corralesFinal = {};
                var recursos = {};
                recursos.Corral = window.CabeceroCorral;
                recursos.Lote = window.CabeceroLote;
                recursos.TotalCabezas = window.CabeceroTotalCabezas;
                recursos.CabezasActuales = window.CabeceroCabezasActuales;
                recursos.CabezasRestantes = window.CabeceroCabezasRestantes;
                recursos.FechaInicio = window.CabeceroFechaInicio;
                recursos.FechaFin = window.CabeceroFechaFin;
                recursos.Estatus = window.CabeceroEstatus;
                recursos.Opcion = window.CabeceroOpcion;

                corralesFinal.Corrales = corrales;
                corralesFinal.Recursos = recursos;

                $('#GridCorrales').html('');
                $('#GridCorrales').setTemplateURL('../Templates/GridCierreCorral.htm');
                $('#GridCorrales').processTemplate(corralesFinal);

                $('#Corrales').dataTable({
                    "oLanguage": {
                        "oPaginate": {
                            "sFirst": window.PrimeraPagina,
                            "sLast": window.UltimaPagina,
                            "sNext": window.Siguiente,
                            "sPrevious": window.Anterior
                        },
                        "sEmptyTable": window.SinDatos,
                        "sInfo": window.Mostrando,
                        "sInfoEmpty": window.SinInformacion,
                        "sInfoFiltered": window.Filtrando,
                        "sLengthMenu": window.Mostrar,
                        "sLoadingRecords": window.Cargando,
                        "sProcessing": window.Procesando,
                        "sSearch": window.Buscar,
                        "sZeroRecords": window.SinRegistros
                    }
                });

                DesbloquearPantalla();
            }
        },
        error: function () {
            DesbloquearPantalla();
            bootbox.alert(window.ErrorAlConsultarCorrales);
        }
    });
};

RemoverEspacios = function (elementoremover) {
    $(elementoremover).each(function () {
        $.trim(this);
    });
};

DiferenciaFechas = function (date1, date2, interval) {
    var second = 1000, minute = second * 60, hour = minute * 60, day = hour * 24, week = day * 7;
    date1 = new Date(date1);
    date2 = new Date(date2);
    var timediff = date2 - date1;
    if (isNaN(timediff)) return NaN;
    switch (interval) {
        case "years":
            return date2.getFullYear() - date1.getFullYear();
        case "months":
            return (
                (date2.getFullYear() * 12 + date2.getMonth())
                    -
                    (date1.getFullYear() * 12 + date1.getMonth())
            );
        case "weeks":
            return Math.floor(timediff / week);
        case "days":
            return Math.floor(timediff / day);
        case "hours":
            return Math.floor(timediff / hour);
        case "minutes":
            return Math.floor(timediff / minute);
        case "seconds":
            return Math.floor(timediff / second);
        default:
            return undefined;
    }
};

FechaFormateada = function (date) {
    var d = new Date(date),
       month = '' + (d.getMonth() + 1),
       day = '' + d.getDate(),
       year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    if (year > 2050) {
        day = '01';
        month = '01';
        year = '0001';
    }
    return [day, month, year].join('/');
};
