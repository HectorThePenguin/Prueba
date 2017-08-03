
var fechaSeleccionada = "";

$(document).ready(function () {

    $('input').bind("cut copy paste", function (e) {
        e.preventDefault();
    });

    $('#btnBuscar').click(function () {
        BuscarCorralesEvaluados();
    });

    $('#btnLimpiar').click(function () {
        InicializarBusqueda();
    });

    $(function () {
        $("#datepicker").datepicker({
            firstDay: 1,
            showOn: 'button',
            buttonImage: '../assets/img/calander.png',
            onSelect: function (date) {
                fechaSeleccionada = date;
            },
        });
        $.datepicker.setDefaults($.datepicker.regional['es']);
    });

    $('body').on("click", ".btnImprimirEva", function () {
        var evaluacionCorralInfo = {};
        evaluacionCorralInfo = CargarInformacionImprimir($(this.parentNode.parentNode));
        ImprimirCorralesEvaluados(evaluacionCorralInfo);

    });


    InicializarBusqueda();

});

ImprimirCorralesEvaluados = function (evaluacionCorralInfo) {

    var datos = { 'evaluacionCorralInfo': evaluacionCorralInfo };

    BloquearPantalla();

    $.ajax({
        type: "POST",
        url: 'EvaluacionPartidaImpresion.aspx/ImprimirCorralesEvaluados',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d.EsValido == false) {
                bootbox.alert(data.d.Mensaje);
            } else {
                window.open('../EvaluacionPartida.pdf', '_blank');
            }
            DesbloquearPantalla();
        },
        error: function () {
            DesbloquearPantalla();
            //bootbox.alert(window.ErrorAlConsultarCorrales);
        }
    });

};


//Función para cargar la información de las evaluaciones que se van a Guardar.
CargarInformacionImprimir = function (renglonSeleccionado) {
    var evaluacionCorral = {};
    var corralInfo = {};
    var operadorInfo = {};
    
    evaluacionCorral.EvaluacionID = renglonSeleccionado.attr('data-EvaluacionID');
    corralInfo.Codigo = renglonSeleccionado.attr('data-CorralCodigo');
    evaluacionCorral.Corral = corralInfo;
    evaluacionCorral.Cabezas = renglonSeleccionado.attr('data-Cabezas');
    evaluacionCorral.PartidasAgrupadas = renglonSeleccionado.attr('data-PartidasAgrupadas');
    evaluacionCorral.HoraEvaluacion = renglonSeleccionado.attr('data-HoraEvaluacion');
    var fecha = renglonSeleccionado.attr('data-FechaRecepcion').split("/");
    evaluacionCorral.FechaRecepcion = new Date([fecha[1], fecha[0], fecha[2]].join("/"));

    evaluacionCorral.FechaRecepcion.setHours(0, 0, 0, 0);

    evaluacionCorral.OrganizacionOrigenAgrupadas = renglonSeleccionado.attr('data-OrganizacionOrigenAgrupadas')
                                                                        .replace("_", " ").replace("_", " ")
                                                                        .replace("_", " ").replace("_", " ");
    fecha = renglonSeleccionado.attr('data-FechaEvaluacion').split("/");
    evaluacionCorral.FechaEvaluacion = new Date([fecha[1], fecha[0], fecha[2]].join("/"));
    //alert("FechaRecepcion: " + evaluacionCorral.FechaRecepcion + " ***** FechaEvaluacion: " + evaluacionCorral.FechaEvaluacion);
    operadorInfo.OperadorID = renglonSeleccionado.attr('data-OperadorOperadorID');
    evaluacionCorral.Operador = operadorInfo;
    evaluacionCorral.NivelGarrapata = renglonSeleccionado.attr('data-NivelGarrapata');
    evaluacionCorral.EsMetafilaxia = renglonSeleccionado.attr('data-EsMetafilaxia');
    evaluacionCorral.MetafilaxiaAutorizada = renglonSeleccionado.attr('data-MetafilaxiaAutorizada');
    evaluacionCorral.Justificacion = renglonSeleccionado.attr('data-Justificacion');
    
    return evaluacionCorral;
};
    
InicializarBusqueda = function () {
    var fecha = new Date();
    fechaSeleccionada = FechaFormateada(fecha);
    BuscarCorralesEvaluados();

    $("#datepicker").val(fechaSeleccionada);
};

RecursosBindTitulos = function () {
    var recursos = {};
    recursos.CabeceroFolio = window.CabeceroFolio;
    recursos.CabeceroCorral = window.CabeceroCorral;
    recursos.CabeceroLote = window.CabeceroLote;
    recursos.CabeceroCabezas = window.CabeceroCabezas;
    recursos.CabeceroKilosLlegada = window.CabeceroKilosLlegada;
    recursos.CabeceroFechaRecepcion = window.CabeceroFechaRecepcion;
    recursos.CabeceroFechaEvaluacion = window.CabeceroFechaEvaluacion;
    recursos.CabeceroOrigen = window.CabeceroOrigen;
    recursos.CabeceroSalida = window.CabeceroSalida;
    recursos.CabeceroOpcion = window.CabeceroOpcion;
    return recursos;
};

DesbloquearModal = function () {
    $("#divModal").spin(false);
    var lock = document.getElementById('divModal');
    lock.className = 'LockOff';
};

BuscarCorralesEvaluados = function () {
    
    var datos = { 'filtroEvaluacion': fechaSeleccionada };

    var corrales;
    BloquearPantalla();
    
    $.ajax({
        type: "POST",
        url: 'EvaluacionPartidaImpresion.aspx/TraerCorralesEvaluados',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == null || msg.d.length == 0) {
                corrales = msg.d;
                var recursos = RecursosBindTitulos();
                var corralesFinal = {};
                corralesFinal.Corrales = corrales;
                corralesFinal.Recursos = recursos;

                $('#GridCorralesEvaluados').html('');
                $('#GridCorralesEvaluados').setTemplateURL('../Templates/GridEvaluacionPartida.htm');
                $('#GridCorralesEvaluados').processTemplate(corralesFinal);

                $('#CorralesEvaluados').dataTable({
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
                
                bootbox.alert(window.SinCorrales);
                DesbloquearPantalla();
                return;
            }
            corrales = msg.d;
            if (corrales != null) {
                                
                var recursos = RecursosBindTitulos();
                var corralesFinal = {};
                corralesFinal.Corrales = corrales;
                corralesFinal.Recursos = recursos;

                $('#GridCorralesEvaluados').html('');
                $('#GridCorralesEvaluados').setTemplateURL('../Templates/GridEvaluacionPartida.htm');
                $('#GridCorralesEvaluados').processTemplate(corralesFinal);

                $('#CorralesEvaluados').dataTable({
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