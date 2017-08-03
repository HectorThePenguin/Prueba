$(document).ready(function () {
    $(".ui-datepicker-trigger").addClass("hidden");
    $(".btn-default").attr("disabled", "disabled");
    $('#timepicker1').attr("disabled", "disabled");
    $('#timepicker1').timepicker({
        minuteStep: 1,
        defaultTime: false
    });

    if ($("#hiddenEsVencida").val() == "1") {
        $("#ControlesTabla").addClass("hidden");
        $("#btnAutorizar").addClass("hidden");
        $("#btnRechazar").addClass("hidden");
    }
    if ($("#hiddenEsNuevo").val() == "1") {
        $("#textHistorico").addClass("hidden");
    }
    var tieneAccion = false;
    $("#btnCancelar").attr("disabled", false);
    var filtroDeIncidencias = {
        moduloID: $("#hiddenModuloID").val(),
        alertaID: $("#hiddenAlertaID").val(),
        estatusID: $("#hiddenEstatusAnteriorID").val()
    };
    var listaIncidencias;
    var usuarioResponsable;
    var esAutorizacion = false;
    $.ajax({
        type: "POST",
        url: "TablaIncidencias.aspx/ObtenerIncidencias",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(filtroDeIncidencias),
        success: function (data) {
            listaIncidencias = JSON.parse(data.d, function (key, value) {
                if (typeof value === 'string') {
                    var d = /\/Date\((\d*)\)\//.exec(value);
                    return (d) ? new Date(+d[1]) : value;
                }
                return value;
            });
            console.log(listaIncidencias);
        }
    });



    $("#btnRechazar").html(btnRechazarText);
    $("#btnAutorizar").html(btnAutorizarText);

    //INICIALIZA LA DATATABLE DE INCIDENCIAS
    var tabla = $("#TablaIncidenciasFiltrada").DataTable({
        "sSscrollX": "100%",
        "sDom": "lfrt<'row-fluid'<'span6'i><'span6'p>>",
        "sPaginationType": "bootstrap",
        "oLanguage": {
            "sProcessing": "Procesando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "No se encontraron resultados",
            "sEmptyTable": "Ningún dato disponible en esta tabla",
            "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            "sInfoPostFix": "",
            "sSearch": "Buscar:",
            "sUrl": "",
            "sInfoThousands": ",",
            "sLoadingRecords": "Cargando...",
            "oPaginate": {
                "sFirst": "Primero",
                "sLast": "Último",
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            },
            "oAria": {
                "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                "sSortDescending": ": Activar para ordenar la columna de manera descendente"
            }
        },
        "fnDrawCallback": function (oSettings) {
            if (currentValue != null) {
                $(".radioIncidencia:input[value!='" + currentValue + "']").prop("checked", false);
            }

        }
    });

    //Mueve la informacion de longitud y el filtro fuera del cabecero
    $('div.dataTables_filter').appendTo("#tableControlsFilter");
    $('div.dataTables_length').appendTo("#tableControlsLenght");

    //Declara variable global que guardará el id de incidencia seleccionado
    var currentValue;

    //Cuando se presione un radiobutton en la tabla se obtendrá el valor de id y se pedirá el seguimiento de esta incidencia
    $("#TablaIncidenciasFiltrada tbody").on("click", "input", function () {
        if ($("#hiddenEsVencida").val() == "1") {
            $("#ControlesTabla").removeClass("hidden");
            $("#btnAutorizar").removeClass("hidden");
            $("#btnRechazar").removeClass("hidden");
        };
        
        //Limpiamos los campos al seleccionar otro radiobuton
        $(".ui-datepicker-trigger").addClass("hidden");
        $("#TextAreaComentarios").val("");
        $("#datepicker").val("");
        $("#DropDownAcciones").val("");
        $("#txtComentariosAnteriores").val("");
        $("#btnAutorizar").attr("disabled", true);
        $("#btnRechazar").attr("disabled", true);
        $("#btnGuardar").attr("disabled", true);
        $("#btnCancelar").attr("disabled", false);
        $("#TextAreaComentarios").prop("disabled", "disabled");
        $("#txtComentariosAnteriores").prop("disabled", "disabled");
        $('.bootstrap-timepicker').prop("disabled", "disabled");
        $("#DropDownAcciones").prop("disabled", "disabled");
        esAutorizacion = false;

        if ($("#hiddenEsNuevo").val() == "1") {
            $("#TextAreaComentarios").prop("disabled", "");
            //Inicializa el control para seleccionar la fecha
            $(function () {
                $("#datepicker").datepicker({
                    firstDay: 1,
                    showOn: 'button',
                    minDate: 0,
                    buttonImage: '../assets/img/calander.png',
                    onSelect: function (date) {
                        fechaSeleccionada = date;
                    },
                });
                $.datepicker.setDefaults($.datepicker.regional['es']);
            });
            var date = new Date();
            var time = formatAMPM(date);
            $('.bootstrap-timepicker').timepicker('setTime', time);
            
        }
        //Carga de campos automaticos
        var fechaVencimientoIncidencia = new Date();
        
        fechaVencimientoIncidencia = $(this).parents("tr").find("#fechavencimiento").html().substring(0, 10);
        usuarioResponsable = $(this).parents("tr").find("#usuarioresponsableid").html();

        currentValue = $(this).prop("value");

        var datos = {
            "incidenciaID": currentValue
        };

        $.ajax({
            type: "POST",
            url: "TablaIncidencias.aspx/ObtenerSeguimientoPorIncidenciaID",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(datos),
            error: function (request) {
                bootbox.alert("Ocurrió un error", function () {

                });
            },
            success: function (data) {
                var parsed = JSON.parse(data.d, function (key, value) {
                    if (typeof value === 'string') {
                        var d = /\/Date\((\d*)\)\//.exec(value);
                        return (d) ? new Date(+d[1]) : value;
                    }
                    return value;
                });
                console.log(parsed);
                if (parsed != null) {
                    var x = parsed[0].FechaVencimientoAnterior;
                    var fechaFormateada = ("0" + x.getDate()).slice(-2) + "/"
                        + ("0" + (x.getMonth() + 1)).slice(-2) + "/"
                        + x.getFullYear();
                    $("#datepicker").val(fechaFormateada);
                    var horaFormateada = formatAMPM(parsed[0].FechaVencimientoAnterior);
                    var comentariosAnteriores = "";
                    $.each(parsed, function (i, obj) {
                        if (obj.Comentarios != null) {
                            comentariosAnteriores += obj.Comentarios + "\n";
                        }
                    });

                    $("#DropDownAcciones").val(parsed[0].Accion.AccionID);
                    $("#txtComentariosAnteriores").val(comentariosAnteriores);
                    $('.bootstrap-timepicker').timepicker('setTime', horaFormateada);
                    //SI ES ESTATUS NUEVA PERO TIENE ACCION REGISTRADA
                    if ($("#hiddenEsNuevo").val() == "1" && parsed != null) {

                        $('.bootstrap-timepicker').timepicker('setTime', horaFormateada);
                        $('.bootstrap-timepicker').prop("disabled", "disabled");
                        $("#DropDownAcciones").val(parsed[0].Accion.AccionID);

                        if (parsed[0].UsuarioResponsable.UsuarioID != 0 && parsed[0].UsuarioResponsable.UsuarioID == $("#HiddenUsuarioID").val()) {
                            $("#btnGuardar").attr("disabled", false);
                            $("#textHistorico").removeClass("hidden");
                            $("#DropDownAcciones").prop("disabled", "");
                        }
                        else if (parsed[0].UsuarioResponsable.UsuarioID == 0) {
                            $("#btnGuardar").attr("disabled", false);
                            $("#textHistorico").removeClass("hidden");
                            $("#DropDownAcciones").prop("disabled", "");
                        }
                    }
                    //SI ES RECHAZADA
                    if ($("#hiddenEsRechazado").val() == "1") {

                        if (parsed[0].NivelAlertaActual.NivelAlertaId == $("#hiddenNivelAlertaUsuario").val()) {
                            if ($("#hiddenNivelAlertaUsuario").val() == $("#HiddenNivelAlertaConfigurado").val()) {
                                if (parsed[0].UsuarioResponsable.UsuarioID == $("#HiddenUsuarioID").val()  || parsed[0].UsuarioResponsable.UsuarioID == 0) {
                                    $("#TextAreaComentarios").prop("disabled", "");
                                    $('.bootstrap-timepicker').timepicker('setTime', horaFormateada);
                                    $('.bootstrap-timepicker').prop("disabled", "");
                                    $("#DropDownAcciones").prop("disabled", "");
                                    $("#btnGuardar").attr("disabled", false);
                                    $("#btnCancelar").attr("disabled", false);
                                }
                            }
                            else {
                                $("#TextAreaComentarios").prop("disabled", "");
                                $("#btnAutorizar").attr("disabled", false);
                                $("#btnRechazar").attr("disabled", false);
                            }
                        }
                        else {
                            $('.bootstrap-timepicker').timepicker('setTime', horaFormateada);
                            $('.bootstrap-timepicker').prop("disabled", "disabled");
                            $("#DropDownAcciones").prop("disabled", "disabled");
                            $("#btnAutorizar").attr("disabled", true);
                            $("#btnRechazar").attr("disabled", true);

                            if (parsed[0].Accion.AccionID != 0) {
                                tieneAccion = true;
                                $("#DropDownAcciones").val(parsed[0].Accion.AccionID);
                                if (parsed[0].UsuarioResponsable.UsuarioID != $("#HiddenUsuarioID").val()) {
                                    $("#DropDownAcciones").prop("disabled", "disabled");
                                }
                            }
                            else {
                                $("#btnAutorizar").attr("disabled", true);
                            }
                        }
                    }
                        //SI ES REGISTRADA
                    if ($("#hiddenEsRegistrado").val() == "1" &&
                        parsed[0].NivelAlertaActual.NivelAlertaId == $("#hiddenNivelAlertaUsuario").val()) {
                        $('.bootstrap-timepicker').timepicker('setTime', horaFormateada);
                        $("#TextAreaComentarios").prop("disabled", "");
                        $("#DropDownAcciones").val(parsed[0].Accion.AccionID);
                        $("#btnAutorizar").attr("disabled", false);
                        $("#btnRechazar").attr("disabled", false);
                    }
                    //SI ES VENCIDA
                    if ($("#hiddenEsVencida").val() == "1") {
                        var incidenciaInfo = $.grep(listaIncidencias, function (e) {
                            return e.id == currentValue;
                        });


                        if (parsed[0].Accion.AccionID != 0) {

                            $("#DropDownAcciones").val(parsed[0].Accion.AccionID);
                            tieneAccion = false;
                        }

                        if (parsed[0].NivelAlertaActual.NivelAlertaId == $("#hiddenNivelAlertaUsuario").val()) {
                            $("#TextAreaComentarios").prop("disabled", "");
                            $("#btnRechazar").attr("disabled", false);
                        }
                    }
                }
                else if ($("#hiddenEsNuevo").val() == "1" && parsed == null) {
                    if (parseInt($("#hiddenNivelAlertaUsuario").val()) == parseInt($("#HiddenNivelAlertaConfigurado").val())) {
                        $("#textHistorico").addClass("hidden");
                        $('.bootstrap-timepicker').prop("disabled", "");
                        $("#datepicker").val(fechaVencimientoIncidencia);
                        $("#btnGuardar").attr("disabled", false);
                        $("#btnCancelar").attr("disabled", false);
                        $("#DropDownAcciones").prop("disabled", "");
                    }

                }
                else if ($("#hiddenEsVencida").val() == "1" && parsed == null) {
                    if (parseInt($("#hiddenNivelAlertaUsuario").val()) == parseInt($("#HiddenNivelAlertaConfigurado").val())) {
                        $("#textHistorico").addClass("hidden");
                        $('.bootstrap-timepicker').prop("disabled", "");
                        $("#datepicker").val(fechaVencimientoIncidencia);
                        $("#btnGuardar").attr("disabled", false);
                        $("#btnCancelar").attr("disabled", false);
                        $("#DropDownAcciones").prop("disabled", "");
                    }

                }
            }
            //}
        });
    });

    //funcion que agrega 0 a las fechas de javascript
    function addZero(i) {
        if (i < 10) {
            i = "0" + i;
        }
        return i;
    }

    //funcion que formatea am/pm la fecha de javascript
    function formatAMPM(date) {
        var hours = date.getHours();
        var minutes = date.getMinutes();
        var ampm = hours >= 12 ? 'PM' : 'AM';
        hours = hours % 12;
        hours = hours ? hours : 12; // the hour '0' should be '12'
        minutes = minutes < 10 ? '0' + minutes : minutes;
        var strTime = hours + ':' + minutes + ' ' + ampm;
        return strTime;
    }

    //funcion que arma el objeto IncidenciaInfo de SK
    function armarIncidenciaInfo() {
        var nivelAlerta;
        if (esAutorizacion) {
            nivelAlerta = {
                NivelAlertaId: $("#hiddenNivelAlertaUsuario").val()
            };
        }
        else
        {
            nivelAlerta = {
                NivelAlertaId: $("#hiddenNivelAlertaID").val()
            };
        }
        var usuarioResponsable = {
            UsuarioID: 0
        };
        if ($("#hiddenEsVencida").val() == "1" && !tieneAccion) {
            var accion = {
                AccionID: 0
            };
        }
        else if ($("#hiddenEsRechazado").val() == "1" && ($("#DropDownAcciones").val() == "" || parseInt($("#DropDownAcciones").val()) == 0)) {
            var accion = {
                AccionID: 0
            };
        }
        else {
            var accion = {
                AccionID: $("#DropDownAcciones").val()
            };
        }
        var estatus = {
            EstatusId: 0
        };
        var modulo = {
            ModuloID: $("#hiddenModuloID").val()
        }
        var alerta = {
            AlertaID: $("#hiddenAlertaID").val(),
            Modulo: modulo
        };
        var estatusAnterior = {
            EstatusId: $("#hiddenEstatusAnteriorID").val()
        };
        var incidenciaSeguimiento = {
            EstatusAnterior: estatusAnterior
        };
        var incidencia = {
            IncidenciasID: currentValue,
            Comentarios: $("#TextAreaComentarios").val(),
            NivelAlerta: nivelAlerta,
            UsuarioResponsable: usuarioResponsable,
            Accion: accion,
            Estatus: estatus,
            Alerta: alerta,
            ListaIncidenciaSeguimiento: [incidenciaSeguimiento]
        };
        return incidencia;
    }

    //Funcion para cachar los botónes presionados
    $(".btn-default").on("click", function () {
        if ($(this).attr("disabled") != "disabled") {
            var cadenaUrl = "";
            var salir = false;
            esAutorizacion = false;
            //Dependiendo del id del botón escoje que petición realizará
            switch ($(this).prop("id")) {
                case "btnGuardar":
                    cadenaUrl = "TablaIncidencias.aspx/ActualizarIncidencia";
                    break;
                case "btnRechazar":
                    cadenaUrl = "TablaIncidencias.aspx/RechazarIncidencia";
                    break;
                case "btnAutorizar":
                    esAutorizacion = true;
                    cadenaUrl = "TablaIncidencias.aspx/AutorizarIncidencia";
                    break;
                case "btnCancelar":

                    bootbox.dialog({
                        message: msgSalirSinGuardar,
                        title: tituloSalirSinGuardar,
                        buttons: {
                            success: {
                                label: "Si",
                                className: "btn SuKarne",
                                callback: function () {
                                    window.location.href = "../Alertas/AlertasSIAP.aspx";
                                }
                            },
                            danger: {
                                label: "No",
                                className: "btn SuKarne",
                                callback: function () {
                                }
                            }
                        }
                    });
                    break;
            }
            if (cadenaUrl != "") {
                //Se valida si las incidencias son nuevas o rechazada
                if ($("#hiddenEsRechazado").val() == "1" || $("#hiddenEsNuevo").val() == "1" || $("#hiddenEsRegistrado").val() == "1") {
                    //Se valida si no hay radiobutton seleccionado
                    if (currentValue == null) {
                        bootbox.alert("<img src=\"../assets/plugins/jquery-validation/demo/marketo/images/warning.gif\" /> " + window.mensajeSeleccionarIncidencia, function () {
                            setTimeout(function () { }, 500);
                        });
                    }
                        //Se valida si no hay fecha seleccionada
                    else if ($.trim($("#datepicker").val()) == "") {
                        bootbox.alert("<img src=\"../assets/plugins/jquery-validation/demo/marketo/images/warning.gif\" /> " + window.mensajeSeleccionarFecha, function () {
                            setTimeout(function () { }, 500);
                        });
                    }
                        //Se valida si no hay accion seleccionada
                    else if ($.trim($("#DropDownAcciones").val()) == "" && $("#HiddenNivelAlertaConfigurado").val() == $("#hiddenNivelAlertaUsuario").val()) {
                        bootbox.alert("<img src=\"../assets/plugins/jquery-validation/demo/marketo/images/warning.gif\" /> " + window.mensajeSeleccionarAccion, function () {
                            setTimeout(function () { }, 500);
                        });
                    } else {
                        guardarIncidencia(cadenaUrl);
                    }

                } else {
                    guardarIncidencia(cadenaUrl);
                }
            }
        }

    });

    function guardarIncidencia(cadenaUrl) {
        //Llamo a crear el objeto incidenciainfo
        var incidencia = armarIncidenciaInfo();
        //PARAMETROS AJAX
        var datos = {
            incidencia: incidencia,
            "fecha": $("#datepicker").val() + " " + $('#timepicker1').val()
        }
        //console.log(datos, cadenaUrl);
        $.ajax({
            type: "POST",
            url: cadenaUrl,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(datos),
            error: function (request) {
                bootbox.alert("Ocurrió un error", function () {
                    console.log(request);
                });
            },
            success: function (data) {
                //Si la respuesta es true significa que guardo correctamente
                if (data.d == true) {
                    bootbox.alert("<img src=\"../Images/Correct.png\" /> Datos guardados con éxito.", function () {
                        //Pongo a "Cargar" el body y mando a llamar la página para reconstruir la tabla.
                        $body = $("body");
                        $body.addClass("cargando");
                        $('body').append($('<form/>')
                            .attr({ 'action': '../Alertas/TablaIncidencias.aspx', 'method': 'post', 'id': 'replacer' })
                            .append($('<input/>')
                                .attr({ 'type': 'hidden', 'name': 'moduloID', 'value': incidencia.Alerta.Modulo.ModuloID })
                            )
                            .append($('<input/>')
                                .attr({ 'type': 'hidden', 'name': 'alertaID', 'value': incidencia.Alerta.AlertaID })
                            )
                            .append($('<input/>')
                                .attr({ 'type': 'hidden', 'name': 'estatusID', 'value': $("#hiddenEstatusAnteriorID").val() })
                            )
                        ).find('#replacer').submit();
                    });
                } else if (data.d == null) {
                    bootbox.alert("<img src=\"../assets/plugins/jquery-validation/demo/marketo/images/warning.gif\" /> No se puede seleccionar una fecha menor a la actual", function () {

                    });
                }
            }
        });
    }

    $("#TablaIncidenciasFiltrada_length").change(function () {
        var numFilas = $("#TablaIncidenciasFiltrada_length").find("select").val();
        localStorage.setItem("NumRegistrosGrid", numFilas);
    });

    var filas = localStorage.getItem("NumRegistrosGrid");
    $("#TablaIncidenciasFiltrada_length").find("select").val(filas);
    $("#TablaIncidenciasFiltrada_length").find("select").find("option[value=10]").removeAttr('selected');
    $("#TablaIncidenciasFiltrada_length").find("select").find("option[value=" + filas + "]").attr('selected', 'selected');
    $("#TablaIncidenciasFiltrada_length").find("select").change();
    $("#TablaIncidenciasFiltrada_length").find("select").onchange();
    
});
