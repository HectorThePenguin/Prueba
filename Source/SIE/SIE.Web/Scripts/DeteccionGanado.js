var corralAnterior = 0;
var areteValido = 0;
var areteTestigoValido = 0;
var validarPartidaRecepcion = 0;
var msjAbierto = 0;
var rutaPantalla = location.pathname;
var flagValidacion;
$(document).ready(function () {

    //Linea que se utiliza para evitar el error que tiene el bootstrap modal, de que se comporta
    //de manera extraña al levantar mas de 1 modal
    $.fn.modal.Constructor.prototype.enforceFocus = function () { };


    ObtenerSintomasCRB();
    ObtenerSintomasPD();
    ObtenerSintomasProblemaGenerico();
    ObtenerGrados();
    ObtenerProblemasLista();
    ObtenerNotificacionesLista();

    $("#btnAretes").attr("disabled", true);

    $("#txtNoPartida").css("text-align", "right");
    $("#txtCorral").focus();

    //$("#txtCorral").inputmask({ "mask": "*", "repeat": 10, "greedy": false });
    $("#txtNoArete").inputmask({ "mask": "*", "repeat": 15, "greedy": false });
    $("#txtAreteTestigo").inputmask({ "mask": "*", "repeat": 15, "greedy": false });
    $("#txtAreteGanadoMuerto").inputmask({ "mask": "*", "repeat": 15, "greedy": false });
    $("#txtAreteTestigoGanadoMuerto").inputmask({ "mask": "*", "repeat": 15, "greedy": false });
    $("#txtNoFierro").inputmask({ "mask": "*", "repeat": 10, "greedy": false });

    $("#btnGanadoMuerto").css("text-decoration", "none");
    $("#btnGanadoMuerto").css("color", "black");
    $("#btnNotificaciones").css("text-decoration", "none");
    $("#btnNotificaciones").css("color", "black");

    document.getElementById('flFoto').addEventListener('change', HandleFileSelectdvFoto, false);
    document.getElementById('flFotoGanadoMuerto').addEventListener('change', HandleFileSelectdvFotoGanadoMuerto, false);

    $("#imgFoto").css("display", "none");
    $("#imgGanadoMuerto").css("display", "none");
    $("#btnTomarFoto").css("display", "none");
    $("#btnTomarFotoGanadoMuerto").css("display", "none");
    $("#cmbParteGolpeada").attr("disabled", true);

    $("a#imageDetect").attr('href', $('#imgFotoDeteccion').prop('src'));

    $("a#imagenMuerte").attr('href', $('#imgFotoMuerte').prop('src'));

    $('#txtObservaciones').keydown(function (e) {
        var charCode = (e.which) ? e.which : 0;
        var code = e.keyCode || e.which;
        if (!solo_letrasnumeros(e) || code == 13) {
            e.preventDefault();
        }
    });

    $('#txtDescripcion').keydown(function (e) {
        var charCode = (e.which) ? e.which : 0;
        var code = e.keyCode || e.which;
        if (!solo_letrasnumeros(e) || code == 13) {
            e.preventDefault();
        }
    });

    $('#txtObservacionesGanadoMuerto').keydown(function (e) {
        if (!solo_letrasnumeros(e)) {
            e.preventDefault();
        } else {
            var charCode = (event.which) ? event.which : 0;
            if (!((charCode >= 37 && charCode <= 40) || charCode == 8)) {
                if ($('#txtObservacionesGanadoMuerto').val().length >= 254) {
                    var texto = $('#txtObservacionesGanadoMuerto').val();
                    $('#txtObservacionesGanadoMuerto').val(texto.substring(0, 255));
                }
            }
        }
    });

    $("#chkGanadoMuerto").click(function (e) {
        if (ValidarCorral() != false) {
            $("#chkGanadoMuerto").attr('checked', true);
            $("#btnGanadoMuerto").trigger("click");
        } else {
            $("#chkGanadoMuerto").attr('checked', false);
        }
    });

    $("#lblGanadoMuerto").click(function () {
        if (ValidarCorral() != false) {
            $("#chkGanadoMuerto").attr('checked', true);
            $("#btnGanadoMuerto").trigger("click");
        } else {
            $("#chkGanadoMuerto").attr('checked', false);
        }
    });

    $("#btnVer").click(function () {
        if ($(this).attr('disabled') != 'disabled') {
            if (ValidarCorral() != false) {
                if (ValidarPartida() != false) {
                    $("#dvVer").trigger("click");
                }
            }
        }
    });

    $("#chkGolpeado").click(function () {
        if ($("#chkGolpeado").is(":checked")) {
            $("#cmbParteGolpeada").attr("disabled", false);
        } else {
            $("#cmbParteGolpeada").attr("disabled", true);
            $("#cmbParteGolpeada").val(0);
        }
    });

    $("#btnTomarFoto").click(function () {
        $('#flFoto').trigger('click');
    });

    $("#btnTomarFotoGanadoMuerto").click(function () {
        $('#flFotoGanadoMuerto').trigger('click');
    });

    $("#chkNo").click(function () {
        if (ValidarCorral() != false) {
            if ($("#chkNo").is(':checked')) {
                $("#btnTomarFoto").css("display", "block");
                $("#txtNoArete").attr('readOnly', true);
                $("#btnAretes").attr('disabled', true);
                $("#txtNoArete").val("");
                if ($("#chkSi").is(':checked')) {
                    $("#chkSi").attr('checked', false);
                }
            } else {
                $("#btnTomarFoto").css("display", "none");
                $("#imgFoto").css("display", "none");
                $("#txtNoArete").attr('readOnly', false);
                $('#imageDeteccion').html("");
                document.getElementById('dvFoto').innerHTML = '';
            }
        } else {
            $("#chkNo").attr('checked', false);
        }
    });

    $("#chkSi").click(function () {
        if (ValidarCorral() != false) {
            if ($("#chkSi").is(':checked')) {
                $("#btnTomarFoto").css("display", "none");
                $("#txtNoArete").attr('readOnly', false);
                if ($("#chkNo").is(':checked')) {
                    $("#chkNo").attr('checked', false);
                    $('#imageDeteccion').html("");
                    $("#btnAretes").attr("disabled", false);
                    document.getElementById('dvFoto').innerHTML = '';
                }
            }
        } else {
            $("#chkSi").attr('checked', false);
        }
    });


    $("#txtCorral").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) {
            $("#txtNoFierro").focus();
            e.preventDefault();
        }
    });

    $("#txtNoFierro").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            e.preventDefault();
        }
    });

    $("#txtNoArete").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            e.preventDefault();
        }
    });

    $("#txtAreteTestigo").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            e.preventDefault();
        }
    });

    $("#txtAreteGanadoMuerto").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            e.preventDefault();
        }
    });

    $("#txtAreteTestigoGanadoMuerto").keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            e.preventDefault();
        }
    });

    $("#btnCancelarVer").click(function (e) {
        e.preventDefault();
        $("#dlgVer").modal('hide');
        var mensaje = bootbox.dialog({
            message: lblMensajeDialogo,
            buttons: {
                success: {
                    label: "Si",
                    className: "btn SuKarne",
                    callback: function () {
                        mensaje.modal('hide');
                        ObtenerProblemasLista();
                    }
                },
                danger: {
                    label: "No",
                    className: "btn SuKarne",
                    callback: function () {
                        $("#dlgVer").modal('show');
                    }
                }
            }
        });
        return false;
    });

    $("#chkSinArete").change(function () {
        if ($("#chkSinArete").is(":checked")) {
            $("#btnTomarFotoGanadoMuerto").css("display", "block");
            $("#txtAreteGanadoMuerto").attr('readOnly', true);
            $("#txtAreteGanadoMuerto").val("");
        } else {
            $("#btnTomarFotoGanadoMuerto").css("display", "none");
            $("#txtAreteGanadoMuerto").attr('readOnly', false);
        }
    });

    $("#txtAreteGanadoMuerto").change(function () {
        $("#chkSinArete").attr('checked', true);
        $("#chkSinArete").trigger("click");
    });

    $("#txtNoArete").focusout(function (e) {
        if ($(this).val() == "" || !ValidarCorral()) {
            e.preventDefault();
        }
    });

    $("#txtAreteTestigo").focusin(function (e) {
        if (ValidarCorral()) {
            ObtenerExisteArete($("#txtNoArete"), 0);
            flagValidacion = true;
        }
        else {
            e.preventDefault();
        }
    });

    $("#txtAreteTestigoGanadoMuerto").focusin(function () {
        if (ValidarCorral() != false) {
            $("#dlgGanadoMuertoDialogo").modal('show');
            ObtenerExisteArete($("#txtAreteGanadoMuerto"), 1);
        }
    });

    $("#txtNoFierro").focusin(function () {
        ValidarCorral();
    });

    $("#txtNoArete").change(function () {
        if ($("#txtNoArete").val() != "") {
            $("#chkSi").attr('checked', true);
        }
        areteValido = 0;
    });

    $("#txtAreteTestigo").focusout(function () {
        if (flagValidacion === false) {
            ObtenerExisteAreteTestigo($("#txtNoArete"), $("#txtAreteTestigo"), 0);
        }
        flagValidacion = false;
    });

    $("#txtAreteTestigo").change(function () {
        areteTestigoValido = 0;
    });

    $("#txtAreteTestigoGanadoMuerto").change(function () {
        areteTestigoValido = 0;
    });

    $("#btnDialogoSi").click(function () {
        location.href = location.href;
    });

    $("#btnAceptarGanadoMuerto").click(function (e) {
        GuardarGanadoMuerto(e);
    });


    $("#lblGuardarEtiquetaBoton").click(function (e) {
        if ($(this).attr('disabled') != 'disabled') {
            GuardarGanadoEnfermo(e);
        }
    });

    $("#btnCancelarGanadoMuerto").click(function (e) {
        e.preventDefault();
        $("#dlgGanadoMuertoDialogo").modal('hide');
        var mensaje = bootbox.dialog({
            message: lblMensajeDialogo,
            buttons: {
                success: {
                    label: "Si",
                    className: "btn SuKarne",
                    callback: function () {
                        mensaje.modal('hide');
                        $("#txtAreteGanadoMuerto").val("");
                        $("#txtAreteTestigoGanadoMuerto").val("");
                        $("#chkSinArete").attr('checked', true);
                        $("#chkSinArete").trigger("click");
                        $("#dvFotoGanadoMuerto").html("");
                        $("#txtObservacionesGanadoMuerto").val("");
                        $("#chkGanadoMuerto").attr('checked', false);
                    }
                },
                danger: {
                    label: "No",
                    className: "btn SuKarne",
                    callback: function () {
                        $("#dlgGanadoMuertoDialogo").modal('show');
                    }
                }
            }
        });
        return false;
    });

    $("#txtCorral").change(function () {
        corralAnterior = 0;
        Limpiar();
    });

    $('#btnAretes').click(function () {
        if ($('#txtCorral').val() !== "") {
            $('#modalAretes').modal('show');

            var corralInfo = {};
            corralInfo.Codigo = $('#txtCorral').val();
            //filtroTicket.ProductoID = TryParseInt($('#txtProducto').val(), 0);
            var datosMetodos = { 'corralInfo': corralInfo };
            var urlMetodos = rutaPantalla + '/ObtenerAretes';
            var mensajeErrorMetodos = "Ocurrio un error al consultar los aretes del corral";
            EjecutarWebMethod(urlMetodos, datosMetodos, BuscarAretesCorralSucces, mensajeErrorMetodos);
        }

    });

    $('#modalAretes').on('shown.bs.modal', function () {
        $("html").css("margin-right", "-15px");
    });

    $("#divAretes").on("dblclick", "#GridAretes tbody tr", function () {
        var areteIndividual = $(this).find(".arete").text().trim();
        var areteMetalico = $(this).find(".areteMetalico").text().trim();
        $('#txtNoArete').val(areteIndividual);
        $('#txtAreteTestigo').val(areteMetalico);
        $('#txtNoArete').focus();
        $('#modalAretes').modal('hide');
    });

    $('#divAretes').on('click', "#GridAretes tbody tr", function () {
        var selected = $(this).hasClass("highlight");
        $("#GridAretes tbody tr").removeClass("highlight");
        if (!selected) {
            $(this).addClass("highlight");
        }
    });

    $('#btnAgregar').click(function() {
        var renglon = $("#GridAretes tr.highlight");
        if (renglon.length === 0) {
            MostrarMensaje("Debe seleccionar un arete para continuar", null);
            return false;
        }
        var areteIndividual = $(renglon).find(".arete").text().trim();
        var areteMetalico = $(renglon).find(".areteMetalico").text().trim();
        $('#txtNoArete').val(areteIndividual);
        $('#txtAreteTestigo').val(areteMetalico);
        $('#txtNoArete').focus();
        $('#modalAretes').modal('hide');
    });

    $('.cerrarModalAretes').click(function() {
        bootbox.dialog({
            message: window.msgSalirSinSeleccionar,
            buttons: {
                Aceptar: {
                    label: window.Si,
                    callback: function () {
                        $('#modalAretes').modal('hide');
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

});

BuscarAretesCorralSucces = function (msg) {
    if (msg.d != null) {
        var datos = {};
        //CargarRecursosGridSupervisor();
        var recursos = {};
        recursos.Arete = 'Arete';
        recursos.AreteMetalico = 'Arete Metalico';
        datos.Recursos = recursos;
        datos.Aretes = msg.d;
        var divContenedor = $('#divAretes');

        divContenedor.setTemplateURL('../Templates/GridAretes.htm');
        divContenedor.processTemplate(datos);

        $('#GridAretes').dataTable({
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
    }
};


BloquearDesbloquearControlesMuerte = function (bloquear) {
    var elementosBloquear = $('.tablaProblemasSintomas :input');
    elementosBloquear.each(function () {
        $(this).attr('disabled', bloquear);
    });

    $('.bloquearMuerte').each(function () {
        $(this).attr('disabled', bloquear);
    });

    $("#chkNo").attr('disabled', bloquear);
    $('#lblGuardarEtiquetaBoton').attr('disabled', bloquear);
};

Limpiar = function () {
    $("#txtNoPartida").val("");
    $("#txtNoArete").val("");
    $("#txtAreteTestigo").val("");
    $("#chkNo").attr('checked', false);
    $("#chkSi").attr('checked', false);
    $("#btnTomarFoto").css("display", "none");
    $("#imgFoto").css("display", "none");
    $("#txtNoArete").attr('readOnly', false);
    $("#txtAreteGanadoMuerto").inputmask({ "mask": "*", "repeat": 15, "greedy": false });
    $("#txtAreteTestigoGanadoMuerto").inputmask({ "mask": "*", "repeat": 15, "greedy": false });
    $("#txtNoFierro").val("");
    $(".clasePD input[type=checkbox]").attr('checked', false);
    $(".sintoma input[type=checkbox]").attr('checked', false);
    $(".checkSolos").attr('checked', false);
    $("#cmbParteGolpeada").attr("disabled", true);
    $("#cmbParteGolpeada").val(0);
    $('#txtObservaciones').val("");
    $('#txtDescripcion').val("");
    $('#imageDeteccion').html("");
    $("#lblGrave").css("background-color", "");
    $("#lblLeve").css("background-color", "");
    $("#ddlDescripcionGanado").val('0');
    $("input:radio[name='rdGrado']").each(function () {
        this.checked = false;
    });
    $("#chkGolpeado").attr('checked', false);
    $("#chkNo").attr('disabled', false);
    //$("#btnAretes").attr('disabled', true);
    DesbloquearPantalla();
};

//Validar que el usuario tenga permisos suficientes
function EnviarMensajeUsuario() {
    if (msjAbierto == 0) {
        msjAbierto = 1;
        bootbox.alert(msgNoTienePermiso, function () {
            history.go(-1);
            msjAbierto = 0;
            return false;
        });
    }
}

var foco = 0;
//Validaciones para el corral
function ValidarCorral() {
    if (corralAnterior != $("#txtCorral").val() || corralAnterior == 0) {
        if ($("#txtCorral").val() == "") {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(msgIngresarCorral, function () {
                    $("#txtCorral").focus();
                    msjAbierto = 0;
                });
            }
            $("#dlgGanadoMuertoDialogo").modal('hide');
            return false;
        } else {
            var datos = { "corralCodigo": $('#txtCorral').val() };
            $.ajax({
                type: "POST",
                url: "DeteccionGanado.aspx/ObtenerCorral",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(datos),
                async: false,
                error: function (request) {
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        bootbox.alert(request.Message, function () {
                            $("#txtCorral").focus();
                            msjAbierto = 0;
                        });
                    }
                    $("#dlgGanadoMuertoDialogo").modal('hide');
                    return false;
                },
                dataType: "json",
                success: function (data) {
                    $("#btnAretes").attr("disabled", false);
                    if (data.d == null) {
                        if (msjAbierto == 0) {
                            msjAbierto = 1;
                            bootbox.alert(msgCorralNoExiste, function () {
                                $("#txtCorral").focus();
                                msjAbierto = 0;
                            });
                        }
                        $("#dlgGanadoMuertoDialogo").modal('hide');
                        $("#btnAretes").attr("disabled", true);
                        return false;
                    } else if (data.d.TipoCorral.TipoCorralID == -1) {//No es corraleta de sacrificio
                        if (msjAbierto == 0) {
                            msjAbierto = 1;
                            bootbox.alert(msgCorraletaNoSacrificio, function () {
                                $("#txtCorral").focus();
                                msjAbierto = 0;
                            });
                        }
                        $("#dlgGanadoMuertoDialogo").modal('hide');
                        return false;
                    } else {
                        var datos = { "corralID": data.d.CorralID };
                        ObtenerLoteCorral(datos);
                        if (data.d.GrupoCorral == 3) {
                            BloquearDesbloquearControlesMuerte(true);
                        }
                        return true;
                    }

                }
            });
        }
    } else {
        return true;
    }
}

//BloquearControlesFinales()

//Funcion que obtiene el lote del corral
function ObtenerLoteCorral(datos) {
    try {
        $.ajax({
            type: "POST",
            url: "DeteccionGanado.aspx/ObtenerLotesCorral",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(datos),
            error: function (request) {
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(request.Message, function () {
                        $("#txtCorral").focus();
                        msjAbierto = 0;
                    });
                }
                $("#dlgGanadoMuertoDialogo").modal('hide');
                return false;
            },
            dataType: "json",
            success: function (data) {
                if (data.d == null) {
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        bootbox.alert(msgNoTieneLoteActivo, function () {
                            $("#txtCorral").focus();
                            msjAbierto = 0;
                        });
                    }
                    $("#dlgGanadoMuertoDialogo").modal('hide');
                    return false;
                } else {
                    ValidarCorralOperador(datos);
                    return true;
                }
            }
        });
    }
    catch (err) {
        return false;
    }

}

//Funcion que valida si el corral pertenece al operador
function ValidarCorralOperador(datos) {
    try {
        $.ajax({
            type: "POST",
            url: "DeteccionGanado.aspx/ObtenerPerteneceCorralOperador",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(datos),
            error: function (request) {
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(request.Message, function () {
                        $("#txtCorral").focus();
                        msjAbierto = 0;
                    });
                }
                $("#dlgGanadoMuertoDialogo").modal('hide');
                return false;
            },
            dataType: "json",
            success: function (data) {
                if (data.d == 1) {
                    corralAnterior = $("#txtCorral").val();
                    ObtenerPartidaCorral(datos);
                    return true;
                } else {
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        bootbox.alert(msgNoPerteneceCorral, function () {
                            $("#txtCorral").focus();
                            msjAbierto = 0;
                        });
                    }
                    $("#dlgGanadoMuertoDialogo").modal('hide');
                    return false;
                }
            }
        });
    }
    catch (err) {
        return false;
    }

}

//Funcion que valida si pertenece el corral al operador
function ObtenerPartidaCorral(datos) {
    try {
        $.ajax({
            type: "POST",
            url: "DeteccionGanado.aspx/ObtenerPartida",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(datos),
            error: function (request) {
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(request.Message, function () {
                        $("#txtCorral").focus();
                        msjAbierto = 0;
                    });
                }
                return false;
            },
            dataType: "json",
            success: function (data) {
                if (data.d == null) {
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        bootbox.alert(msgCorralNoExiste, function () {
                            $("#txtCorral").focus();
                            msjAbierto = 0;
                        });
                    }
                    return false;
                } else {
                    $("#txtNoPartida").val(data.d.FolioEntrada);
                    var datos = { "corralCodigo": $("#txtCorral").val() };
                    var tipoOrigenId = data.d.TipoOrganizacionOrigenId;
                    $.ajax({
                        type: "POST",
                        url: "DeteccionGanado.aspx/ObtenerCorral",
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify(datos),
                        dataType: "json",
                        success: function (data) {
                            if (data.d) {
                                if (data.d.GrupoCorral == 2 || data.d.GrupoCorral == 3) {
                                    $("#lblNo").html("Sin arete");
                                    $("#lblSi").css("display", "none");
                                    $("#lblPartidaAreteIndivitual").css("display", "none");
                                    $("#chkSi").css("display", "none");
                                    validarPartidaRecepcion = 0;// no se valida partida
                                } else {
                                    $("#lblNo").html("No");
                                    $("#lblSi").css("display", "inline-block");
                                    $("#lblPartidaAreteIndivitual").css("display", "inline-block");
                                    $("#chkSi").css("display", "inline-block");
                                    validarPartidaRecepcion = 1;
                                    
                                }
                                if (data.d.GrupoCorral == 1) {
                                    $("#txtAreteTestigoGanadoMuerto").attr('readOnly', true);
                                    $("#txtAreteTestigo").attr('readOnly', true);
                                } else {
                                    $("#txtAreteTestigoGanadoMuerto").attr('readOnly', false);
                                    $("#txtAreteTestigo").attr('readOnly', false);
                                }

                                if (tipoOrigenId === 3) { //CompraDirecta deteccion por foto
                                    $("#btnTomarFoto").css("display", "block");
                                    $("#txtNoArete").attr('readOnly', true);
                                    $("#txtNoArete").val("");
                                    if ($("#chkSi").is(':checked')) {
                                        $("#chkSi").attr('checked', false);
                                    }
                                    $("#chkNo").attr('checked', true);
                                    $("#chkNo").attr('disabled', true);
                                    $("#btnAretes").attr('disabled', true);
                                    $("#lblSi").css("display", "none");
                                    $("#chkSi").css("display", "none");
                                    $("#txtAreteTestigo").attr('readOnly', true);
                                    MostrarMensaje('La partida es de compra directa, solo detección por foto es permitida', null);
                                }
                            }
                            ObtenerAnimalesPorCorral();
                        }
                    });
                    return true;
                }
            }
        });

    }
    catch (err) {
        return false;
    }

}

//Funcion que valida si existe un arete individual
function ObtenerExisteArete(id, tipo) {
    var validarDeteccion = true;
    if (tipo == 1) {
        validarDeteccion = false;
    }

    var datos = { "corralCodigo": $('#txtCorral').val(), "arete": id.val(), "validarDeteccion": validarDeteccion };
    if (areteValido != id.val() || areteValido == 0) {
        if (id.val() != "") {
            App.bloquearContenedor($("#dlgGanadoMuertoDialogo .modal-body"));
            $.ajax({
                type: "POST",
                url: "DeteccionGanado.aspx/ObtenerExisteArete",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(datos),
                error: function (request) {
                    App.desbloquearContenedor($("#dlgGanadoMuertoDialogo .modal-body"));
                    if (tipo == 1) {
                        $("#dlgGanadoMuertoDialogo").modal('hide');
                    }
                    var mensaje = bootbox.alert(request.Message, function () {
                        if (tipo == 1) {
                            $("#dlgGanadoMuertoDialogo").modal('show');
                        }
                        mensaje.modal('hide');
                        id.focus();
                    });
                    return false;
                },
                dataType: "json",
                success: function (data) {
                    App.desbloquearContenedor($("#dlgGanadoMuertoDialogo .modal-body"));
                    if (data.d == 1) {
                        areteValido = $(id).val();
                        return true;
                    } else if (data.d == 2) {
                        if (tipo == 1) {
                            $("#dlgGanadoMuertoDialogo").modal('hide');
                        }
                        var mensaje = bootbox.alert(msgAreteDetectado, function () {
                            if (tipo == 1) {
                                $("#dlgGanadoMuertoDialogo").modal('show');
                            }
                            mensaje.modal('hide');
                            id.focus();
                        });
                        return false;
                    } else if (data.d == 3) {
                        if (tipo == 1) {
                            $("#dlgGanadoMuertoDialogo").modal('hide');
                        }
                        var mensaje = bootbox.alert(msgAreteMuerto, function () {
                            if (tipo == 1) {
                                $("#dlgGanadoMuertoDialogo").modal('show');
                            }
                            mensaje.modal('hide');
                            id.focus();
                        });
                        return false;
                    } else {
                        if (tipo == 1) {
                            $("#dlgGanadoMuertoDialogo").modal('hide');
                        }
                        var mensaje = bootbox.alert(msgAreteNoExiste, function () {
                            if (tipo == 1) {
                                $("#dlgGanadoMuertoDialogo").modal('show');
                            }
                            mensaje.modal('hide');
                            $(id).val('');
                            id.focus();
                        });
                        return false;
                    }
                }
            });
        } else {
            return true;
        }
    } else {
        return true;
    }
}

function ObtenerExisteAreteTestigo(idArete, idAreteTestigo, tipo) {
    var validarDeteccion = true;
    if (tipo == 1) {
        validarDeteccion = false;
    }

    var datos = { "corralCodigo": $('#txtCorral').val(), "arete": idArete.val(), "areteTestigo": idAreteTestigo.val(), "validarDeteccion": validarDeteccion };
    if (areteTestigoValido != idAreteTestigo.val() || areteTestigoValido == 0) {
        if (idAreteTestigo.val() != "") {
            $.ajax({
                type: "POST",
                url: "DeteccionGanado.aspx/ObtenerExisteAreteTestigo",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(datos),
                error: function (request) {
                    if (msjAbierto == 0) {
                        msjAbierto = 1;
                        var mensaje = bootbox.alert(request.Message, function () {
                            mensaje.modal('hide');
                            idAreteTestigo.focus();
                            msjAbierto = 0;
                        });
                    }
                    return false;
                },
                dataType: "json",
                success: function (data) {
                    if (data.d == 1) {
                        areteTestigoValido = idAreteTestigo.val();
                        return true;
                    } else if (data.d == 2) {
                        if (tipo == 1) {
                            $("#dlgGanadoMuertoDialogo").modal('hide');
                        }
                        if (msjAbierto == 0) {
                            msjAbierto = 1;
                            var mensaje = bootbox.alert(msgAreteDetectado, function () {
                                if (tipo == 1) {
                                    $("#dlgGanadoMuertoDialogo").modal('show');
                                }
                                mensaje.modal('hide');
                                id.focus();
                                msjAbierto = 0;
                            });
                        }
                        return false;
                    } else if (data.d == 3) {
                        if (tipo == 1) {
                            $("#dlgGanadoMuertoDialogo").modal('hide');
                        }
                        if (msjAbierto == 0) {
                            msjAbierto = 1;
                            var mensaje = bootbox.alert(msgAreteMuerto, function () {
                                if (tipo == 1) {
                                    $("#dlgGanadoMuertoDialogo").modal('show');
                                }
                                mensaje.modal('hide');
                                id.focus();
                                msjAbierto = 0;
                            });
                        }
                        return false;
                    } else {
                        if (tipo == 1) {
                            $("#dlgGanadoMuertoDialogo").modal('hide');
                        }
                        if (msjAbierto == 0) {
                            msjAbierto = 1;
                            var mensaje = bootbox.alert(msgAreteNoExiste, function () {
                                if (tipo == 1) {
                                    $("#dlgGanadoMuertoDialogo").modal('show');
                                }
                                mensaje.modal('hide');
                                idAreteTestigo.focus();
                                msjAbierto = 0;
                            });
                        }
                        return false;
                    }
                }
            });
        } else {
            return true;
        }
    } else {
        return true;
    }
}

//Funcion validar partida seleccionada
function ValidarPartida() {
    var contador = 0;
    if (validarPartidaRecepcion) {
        $(".clasePartida input[type=checkbox]:checked").each(function () {
            if ($(this).is(':checked')) {
                contador += 1;
            }
        });
        if (contador > 0) {
            return true;
        } else {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(msgSeleccionarPartida, function () {
                    $("#chkSi").focus();
                    msjAbierto = 0;
                });
            }
            return false;
        }
    } else {
        return true;
    }
}

//Para mostrar un dialogo con mensaje generico y el boton OK
function MostrarMensajeOK(Texto, Titulo, Id) {
    bootbox.dialog({
        message: Texto,
        title: Titulo,
        close: function () {
            if (Id != "") {
                $(Id).focus();
            }
        },
        closeButton: true,
        buttons: {
            success: {
                label: "Ok",
                className: "btn SuKarne",
                callback: function () {
                    if (Id != "") {
                        $(Id).focus();
                    }
                }
            }
        }
    });
};

function ObtenerProblemasLista() {
    BloquearPantalla();
    $.ajax({
        type: "POST",
        url: "DeteccionGanado.aspx/ObtenerProblemasLista",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function () {
            MostrarMensajeOK(window.msgNoHayProblemasSecundarios, "", "");
            DesbloquearPantalla();
        },
        success: function (data) {
            if (data.d == null) {
                return;
            }

            var respuesta = {};
            respuesta.Problemas = data.d;
            respuesta.Recursos = LlenarRecursosOtros();

            AgregaElementosTablaOtros(respuesta);
            DesbloquearPantalla();
        }
    });


};

//Cabecero de la tabla
function LlenarRecursosOtros() {
    Resources = {};
    Resources.HeaderIdProblema = hIdProblema;
    Resources.HeaderProblemasEnfermeria = hDescripcion;
    Resources.HeaderSeleccione = hSeleccione;
    return Resources;
};

function AgregaElementosTablaOtros(datos) {
    if (datos != null) {
        $('#TablaOtros').setTemplateURL('../Templates/GridDeteccionGanadoVer.htm');
        $('#TablaOtros').processTemplate(datos);
    } else {
        $('#TablaOtros').html("");
    }
};

function ObtenerNotificacionesLista() {
    BloquearPantalla();
    $.ajax({
        type: "POST",
        url: "DeteccionGanado.aspx/ObtenerNotificacionesOperador",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function () {
            //MostrarMensajeOK(msgNoHayNotificaciones, "", "");
            DesbloquearPantalla();
        },
        success: function (data) {
            if (data.d == null) {
                $("#lblNotificaciones").html("Notificaciones(0)");
                $("#btnNotificaciones").attr("href", "#dvNoHayNotificaciones");
                DesbloquearPantalla();
                return;
            }

            var respuesta = {};
            respuesta.Notificaciones = data.d;
            respuesta.Recursos = LlenarRecursosNotificaciones();

            $("#lblNotificaciones").html("Notificaciones(" + respuesta.Notificaciones.length + ")");
            MostrarMensajeOK("Tiene " + respuesta.Notificaciones.length + " notificaciones pendientes de atender.");
            AgregaElementosTablaNotificaciones(respuesta);
            DesbloquearPantalla();
        }
    });
};

//Cabecero de la tabla
function LlenarRecursosNotificaciones() {
    Resources = {};
    Resources.HeaderCorral = hCorral;
    Resources.HeaderConcepto = hConcepto;
    Resources.HeaderAcuerdo = hAcuerdo;
    Resources.HeaderNoArete = hNoArete;
    Resources.HeaderNoAreteTestigo = hNoAreteTestigo;
    Resources.HeaderFoto = hFoto;
    return Resources;
};

function AgregaElementosTablaNotificaciones(datos) {
    if (datos != null) {
        $('#TablaNotificaciones').setTemplateURL('../Templates/GridDeteccionGanadoNotificaciones.htm');
        $('#TablaNotificaciones').processTemplate(datos);
    } else {
        $('#TablaNotificaciones').html("");
    }
};

var listaAretes = [];
//Funcion que obtiene los aretes del corral
function ObtenerAnimalesPorCorral() {
    try {
        var datos = { 'corralCodigo': $("#txtCorral").val() };
        $.ajax({
            type: "POST",
            url: "DeteccionGanado.aspx/ObtenerAnimalesPorCodigoCorral",
            data: JSON.stringify(datos),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            error: function (request) {
                bootbox.alert(request.Message);
            },
            success: function (data) {
                listaAretes = [];
                if (data.d != null) {
                    $(data.d).each(function () {
                        listaAretes.push(this.Arete);
                    });
                }
                $('#txtNoArete').typeahead({
                    source: listaAretes
                });
                $('#txtAreteGanadoMuerto').typeahead({
                    source: listaAretes
                });
            }
        });
    }
    catch (err) {
        //Handle errors here
    }

};

function ObtenerSintomasCRB() {
    BloquearPantalla();
    var datos = { 'problema': 1 };
    $.ajax({
        type: "POST",
        url: "DeteccionGanado.aspx/ObtenerListaSintomas",
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (request) {
            bootbox.alert(request.Message);
            DesbloquearPantalla();
        },
        success: function (data) {
            if (data.d) {
                $("#dvSintomasCRB").html('');
                $("#dvSintomasCRB").append('<div class="space10"></div>');
                $(data.d).each(function () {
                    $("#dvSintomasCRB").append('<div class="span8"><label>' + $(this)[0].Descripcion + ' </label></div><div class="span1"><span class="claseCRB checks sintoma"><input id="chk' + $(this)[0].SintomaID + '" type="checkbox" value="' + $(this)[0].SintomaID + '" label="' + $(this)[0].Descripcion + '"></span></div>');
                });
                AsignaClickClaseCRB();
                DesbloquearPantalla();
            }
        }
    });
}

function ObtenerSintomasProblemaGenerico() {
    BloquearPantalla();
    var datos = { 'problema': 17 };
    $.ajax({
        type: "POST",
        url: "DeteccionGanado.aspx/ObtenerListaSintomas",
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (request) {
            bootbox.alert(request.Message);
            DesbloquearPantalla();
        },
        success: function (data) {
            if (data.d) {
                $("#dvSintomasProblemaGenerico").html('');
                $("#dvSintomasProblemaGenerico").append('<div class="space10"></div>');
                $(data.d).each(function () {
                    $("#dvSintomasProblemaGenerico").append('<div class="span9"><label>' + $(this)[0].Descripcion + ': </label></div><div class="span1"><span class="checks sintoma checkSolos sintomaGenerico"><input id="chk' + $(this)[0].SintomaID + '" type="checkbox" value="' + $(this)[0].SintomaID + '" label="' + $(this)[0].Descripcion + '"></span></div>');
                });
            }
            AsignaClickClaseGenerico();
            DesbloquearPantalla();
        }
    });
}

function ObtenerSintomasPD() {
    BloquearPantalla();
    var datos = { 'problema': 4 };
    $.ajax({
        type: "POST",
        url: "DeteccionGanado.aspx/ObtenerListaSintomas",
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (request) {
            bootbox.alert(request.Message);
            DesbloquearPantalla();
        },
        success: function (data) {
            if (data.d) {
                $("#dvSintomasPD").html('');
                $("#dvSintomasPD").append('<div class="space10"></div>');
                $(data.d).each(function () {
                    $("#dvSintomasPD").append('<div class="span9"><label>' + $(this)[0].Descripcion + ': </label></div><div class="span1"><span class="clasePD checks sintoma"><input id="chk' + $(this)[0].SintomaID + '" type="checkbox" value="' + $(this)[0].SintomaID + '" label="' + $(this)[0].Descripcion + '"></span></div>');
                });
                AsignaClickClasePD();
                DesbloquearPantalla();
            }
        }
    });
}

function ObtenerGrados() {
    BloquearPantalla();
    $.ajax({
        type: "POST",
        url: "DeteccionGanado.aspx/ObtenerGrados",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (request) {
            bootbox.alert(request.Message);
            DesbloquearPantalla();
        },
        success: function (data) {
            if (data.d) {
                $("#dvRadios").html('');
                $("#dvRadios").append('<div class="space10"></div>');
                $(data.d).each(function () {
                    $("#dvRadios").append('<div class="span1"><label></div><div class="span1"><span class="clasePD"><input id="rd' + $(this)[0].GradoID + '" grado="' + $(this)[0].GradoID + '" type="radio" name="rdGrado" value="' + $(this)[0].NivelGravedad + '" label="' + $(this)[0].Descripcion + '">' + $(this)[0].Descripcion + '</label</span></div>');
                });
                AsignaChangeRadios();
                DesbloquearPantalla();
            }
        }
    });
}

//Funcio que liga el estado change para mistrar la imagen de necropsia
function HandleFileSelectdvFoto(evt) {
    var files = evt.target.files; // FileList object
    // Loop through the FileList and render image files as thumbnails.
    for (var i = 0, f; f = files[i]; i++) {
        // Only process image files.
        if (!f.type.match('image.*')) {
            continue;
        }
        var reader = new FileReader();
        // Closure to capture the file information.
        reader.onload = (function (theFile) {
            return function (e) {
                // Render thumbnail.
                var span = document.createElement('a');
                span.innerHTML = ['<img class="thumb" src="', e.target.result,
                                  '" title="', escape(theFile.name), '"/>'].join('');

                span.setAttribute('class', 'pull-rigth imagen-seleccion');
                span.setAttribute('id', 'imageDeteccion');

                document.getElementById('dvFoto').innerHTML = '';
                document.getElementById('dvFoto').insertBefore(span, null);

                $('#imageDeteccion').click(function () {
                    var fancy = bootbox.alert("<img src='" + e.target.result + "'>", function () {
                        fancy.modal('hide');
                    });
                });
            };
        })(f);
        // Read in the image file as a data URL.
        reader.readAsDataURL(f);

    }
}

//Funcio que liga el estado change para mistrar la imagen de necropsia
function HandleFileSelectdvFotoGanadoMuerto(evt) {
    var files = evt.target.files; // FileList object
    // Loop through the FileList and render image files as thumbnails.
    for (var i = 0, f; f = files[i]; i++) {
        // Only process image files.
        if (!f.type.match('image.*')) {
            continue;
        }
        var reader = new FileReader();
        // Closure to capture the file information.
        reader.onload = (function (theFile) {
            return function (e) {
                // Render thumbnail.
                var span = document.createElement('a');
                span.innerHTML = ['<img class="thumb alineacionCentro" src="', e.target.result,
                                  '" title="', escape(theFile.name), '"/>'].join('');

                span.setAttribute('class', 'pull-rigth imagen-seleccion');
                span.setAttribute('id', 'imageMuerte');

                document.getElementById('dvFotoGanadoMuerto').innerHTML = '';
                document.getElementById('dvFotoGanadoMuerto').insertBefore(span, null);

                $('#imageMuerte').click(function () {
                    $("#dlgGanadoMuertoDialogo").modal('hide');
                    var fancy = bootbox.alert("<img src='" + e.target.result + "'>", function () {
                        fancy.modal('hide');
                        $("#dlgGanadoMuertoDialogo").modal('show');
                    });
                });
            };
        })(f);
        // Read in the image file as a data URL.
        reader.readAsDataURL(f);

    }
}

function AsignaClickClaseCRB() {
    $(".claseCRB").change(function (e) {
        if (ValidarCorral() == true) {
            if (ValidarPartida() == true) {
                if (ValidarAretesDeteccion() == true) {
                    var contador = 0;
                    $(".claseCRB input[type=checkbox]:checked").each(function () {
                        if ($(this).is(':checked')) {
                            contador += 1;
                        }
                    });
                    if (contador > 2) {
                        $("#chkCRB").attr('checked', true);
                    } else {
                        $("#chkCRB").attr('checked', false);
                    }
                } else {
                    e.preventDefault();
                    $(".claseCRB input[type=checkbox]").attr('checked', false);
                }
            } else {
                e.preventDefault();
                $(".claseCRB input[type=checkbox]").attr('checked', false);
            }
        } else {
            e.preventDefault();
            $(".claseCRB input[type=checkbox]").attr('checked', false);
        }
    });

    $("#chk1").click(function () {
        if ($("#chk2").is(':checked')) {
            $("#chk2").attr('checked', false);
        }
    });

    $("#chk2").click(function () {
        if ($("#chk1").is(':checked')) {
            $("#chk1").attr('checked', false);
        }
    });
}

function AsignaClickClasePD() {
    $(".clasePD").change(function (e) {
        if (ValidarCorral() == true) {
            if (ValidarPartida() == true) {
                if (ValidarAretesDeteccion() == true) {
                    var contador = 0;
                    $(".clasePD input[type=checkbox]:checked").each(function () {
                        if ($(this).is(':checked')) {
                            $("#chkPD").attr('checked', true);
                            contador += 1;
                        }
                    });
                    if (contador == 0) {
                        $("#chkPD").attr('checked', false);
                    }
                } else {
                    e.preventDefault();
                    $(".clasePD input[type=checkbox]").attr('checked', false);
                }
            } else {
                e.preventDefault();
                $(".clasePD input[type=checkbox]").attr('checked', false);
            }
        } else {
            e.preventDefault();
            $(".clasePD input[type=checkbox]").attr('checked', false);
        }
    });
}

function AsignaClickClaseGenerico() {
    $(".checkSolos").change(function (e) {
        if (ValidarCorral() == true) {
            if (ValidarPartida() == true) {
                if (ValidarAretesDeteccion() == true) {

                } else {
                    e.preventDefault();
                    $(".sintoma input[type=checkbox]").attr('checked', false);
                    $(".checkSolos input[type=checkbox]").attr('checked', false);
                    $("#cmbParteGolpeada").attr("disabled", true);
                }
            } else {
                e.preventDefault();
                $(".sintoma input[type=checkbox]").attr('checked', false);
                $(".checkSolos input[type=checkbox]").attr('checked', false);
                $("#cmbParteGolpeada").attr("disabled", true);
            }
        } else {
            e.preventDefault();
            $(".sintoma input[type=checkbox]").attr('checked', false);
            $(".checkSolos input[type=checkbox]").attr('checked', false);
            $("#cmbParteGolpeada").attr("disabled", true);
        }
    });
}

function AsignaChangeRadios() {
    $("input[name=rdGrado]").change(function (e) {
        if (ValidarCorral() == true) {
            if (ValidarPartida() == true) {
                if (ValidarAretesDeteccion() == true) {
                    if (this.value == "L") {
                        $("#lblLeve").css("background-color", "green");
                        $("#lblGrave").css("background-color", "");
                    } else if (this.value == "G") {
                        $("#lblGrave").css("background-color", "red");
                        $("#lblLeve").css("background-color", "");
                    }
                } else {
                    e.preventDefault();
                }
            } else {
                e.preventDefault();
            }
        } else {
            e.preventDefault();
        }
    });
}
function GuardarGanadoMuerto(e) {
    e.preventDefault();
    var archivoSeleccionado = document.getElementById('flFotoGanadoMuerto').files[0];
    if (ValidarAretesMuerte() == true) {
        if ((((archivoSeleccionado != null && $("#chkSinArete").is(":checked")) || (!($("#chkSinArete").is(":checked")))))) {
            var parametros = {};

            parametros.CorralCodigo = $("#txtCorral").val();
            parametros.Arete = $("#txtAreteGanadoMuerto").val();
            parametros.AreteMetalico = $("#txtAreteTestigoGanadoMuerto").val();
            parametros.Observaciones = $("#txtObservacionesGanadoMuerto").val();

            if ($("#txtObservacionesGanadoMuerto").val() == "") {
                if (msjAbierto == 0) {
                    msjAbierto = 1;
                    bootbox.alert(msgDatosBlanco, function () {
                        $("#dlgGanadoMuertoDialogo").modal('show');
                        msjAbierto = 0;
                    });
                }
                return false;
            }

            if ($("#chkSinArete").is(":checked")) {
                var uui = GenerateUUID();
                if (uui) {
                    parametros.FotoDeteccion = uui + '-det.jpg';
                    //cargamos el archivo
                    UploadFile(parametros.FotoDeteccion, "flFotoGanadoMuerto", 2);
                    //Guardamos la salida
                }
            } else {
                parametros.FotoDeteccion = "";
            }

            $("#dlgGanadoMuertoDialogo").modal('hide');
            Bloquear();
            var datos = { "muerte": parametros };
            $.ajax({
                type: "POST",
                url: "DeteccionGanado.aspx/GuardarMuerte",
                data: JSON.stringify(datos),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error: function (request) {
                    Desbloquear();
                    bootbox.alert(request.Message, function () {
                        $("#dlgGanadoMuertoDialogo").modal('show');
                    });

                },
                success: function (data) {
                    Desbloquear();
                    if (data.d) {
                        if (data.d == 1) {
                            $("#dlgGanadoMuertoDialogo").modal('hide');
                            bootbox.alert("<img src='../Images/Correct.png'/>&nbsp;" + msgDatosGuardados, function () {
                                location.href = location.href;
                            });
                        } else {
                            if (msjAbierto == 0) {
                                msjAbierto = 1;
                                bootbox.alert(msgOcurrioErrorGrabar, function () {
                                    $("#dlgGanadoMuertoDialogo").modal('show');
                                    msjAbierto = 0;
                                });
                            }
                        }
                    }
                }
            });

        } else if ((archivoSeleccionado == null && $("#chkSinArete").is(":checked"))) {
            bootbox.dialog({
                message: msgTomarFoto,
                closeButton: true,
                buttons: {
                    success: {
                        label: "Ok",
                        className: "btn SuKarne",
                        callback: function () {
                            $("#dlgGanadoMuertoDialogo").modal('show');
                        }
                    },
                }
            });
        } else {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                bootbox.alert(msgDatosBlanco, function () {
                    $("#dlgGanadoMuertoDialogo").modal('show');
                    msjAbierto = 0;
                });
            }
            return false;
        }
    }
}

Bloquear = function () {
    $.blockUI(
    {
        message: '<img src="../assets/img/ajax-loading.gif" align="">',
        css: {
            top: '10%',
            border: 'none',
            padding: '2px',
            backgroundColor: 'none'
        },
        overlayCSS: {
            backgroundColor: '#000',
            opacity: 0.05,
            cursor: 'wait'
        }
    });
};

Desbloquear = function () {
    $.unblockUI();
};

function GuardarGanadoEnfermo(e) {
    e.preventDefault();
    var archivoSeleccionado = document.getElementById('flFoto').files[0];
    if (ValidarCorral() == true) {
        if (ValidarPartida() == true) {
            if (ValidarAretesDeteccion() == true) {
                if (archivoSeleccionado != null || !($("#chkNo").is(":checked"))) {
                    if ($("#ddlDescripcionGanado").val() != "0") {
                        var parametros = {};

                        if (!(($("#chkGolpeado").is(":checked") && $("#cmbParteGolpeada").val() > 0) || !($("#chkGolpeado").is(":checked")))) {
                            if (msjAbierto == 0) {
                                msjAbierto = 1;
                                bootbox.alert(msgDatosBlanco, function () {
                                    msjAbierto = 0;
                                });
                            }
                            return false;
                        }

                        if ($("#txtDescripcion").val() == "") {
                            if (msjAbierto == 0) {
                                msjAbierto = 1;
                                bootbox.alert(msgDatosBlanco, function () {
                                    msjAbierto = 0;
                                });
                            }
                            return false;
                        }
                        parametros.CorralCodigo = $("#txtCorral").val();
                        parametros.Arete = $("#txtNoArete").val();
                        parametros.AreteMetalico = $("#txtAreteTestigo").val();
                        parametros.Observaciones = $("#txtObservaciones").val();
                        parametros.NoFierro = $("#txtNoFierro").val();
                        //parametros.DescripcionGanado = $("#txtDescripcion").val();
                        parametros.DescripcionGanadoID = $("#ddlDescripcionGanado").val();

                        var listaProblemas = [];
                        listaProblemas = ObtenerListaProblemas();
                        parametros.Problemas = listaProblemas;

                        var listaSintomas = [];
                        listaSintomas = ObtenerListaSintomas();
                        parametros.Sintomas = listaSintomas;

                        var listaGenericos = [];
                        listaGenericos = ObtenerListaGenericos();

                        var grado = $('input[name=rdGrado]:checked');
                        if (grado[0]) {
                            parametros.GradoID = grado[0].attributes["grado"].textContent;
                        } else {
                            if (msjAbierto == 0) {
                                msjAbierto = 1;
                                bootbox.alert(msgDatosBlanco, function () {
                                    msjAbierto = 0;
                                });
                            }
                            return false;
                        }
                        if (parametros.GradoID == false) {
                            if (msjAbierto == 0) {
                                msjAbierto = 1;
                                bootbox.alert(msgDatosBlanco, function () {
                                    msjAbierto = 0;
                                });
                            }
                            return false;
                        }


                        var datos = { "deteccion": parametros };

                        if (parametros.GradoID > 0 && listaSintomas.length > 0 && (listaProblemas.length > 0 || $("#cmbParteGolpeada").val() > 0) && listaGenericos.length > 0) {
                            if ($("#chkNo").is(":checked")) {
                                var uui = GenerateUUID();
                                if (uui) {
                                    parametros.FotoDeteccion = uui + '-det' + '.jpg';
                                    //cargamos el archivo
                                    UploadFile(parametros.FotoDeteccion, "flFoto", 1);
                                    //Guardamos la salida
                                }
                            } else {
                                parametros.FotoDeteccion = "";
                            }

                            Bloquear();

                            $.ajax({
                                type: "POST",
                                url: "DeteccionGanado.aspx/GuardarDeteccion",
                                data: JSON.stringify(datos),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                error: function (request) {
                                    Desbloquear();
                                    bootbox.alert(request.Message);
                                },
                                success: function (data) {
                                    Desbloquear();
                                    if (data.d == 0) {
                                        bootbox.alert("<img src='../Images/Correct.png'/>&nbsp;" + msgDatosGuardados, function () {
                                            //location.href = location.href;
                                            Limpiar();
                                            ObtenerNotificacionesLista();
                                        });
                                    } else {
                                        if (data.d == 2) {
                                            if (msjAbierto == 0) {
                                                msjAbierto = 1;
                                                bootbox.alert(window.msgGradoCorralRecepcion, function () {
                                                    msjAbierto = 0;
                                                });
                                            }
                                        }
                                        else if (data.d == 3) {
                                            if (msjAbierto == 0) {
                                                msjAbierto = 1;
                                                bootbox.alert(window.msgAreteDetectado, function () {
                                                    msjAbierto = 0;
                                                });
                                            }
                                        } else {
                                            if (msjAbierto == 0) {
                                                msjAbierto = 1;
                                                bootbox.alert(msgOcurrioErrorGrabar, function () {
                                                    msjAbierto = 0;
                                                });
                                                
                                            }
                                        }
                                    }
                                }
                            });
                        } else {
                            if (msjAbierto == 0) {
                                msjAbierto = 1;
                                bootbox.alert(msgDatosBlanco, function () {
                                    msjAbierto = 0;
                                });
                            }
                        }
                    } else {
                        if (msjAbierto == 0) {
                            msjAbierto = 1;
                            bootbox.alert(msgDatosBlanco, function () {
                                msjAbierto = 0;
                            });
                        }
                    }
                } else {
                    bootbox.dialog({
                        message: msgTomarFoto,
                        closeButton: true,
                        buttons: {
                            success: {
                                label: "Ok",
                                className: "btn SuKarne",
                                callback: function () {
                                    ;
                                }
                            },
                        }
                    });
                }
            }
        }
    }
}

//Funcion que envia el post para guardar el arhivo
function UploadFile(nombreArchivo, id, tipoFoto) {
    var fd = new FormData();
    fd.append("fileupload", document.getElementById(id).files[0]);
    fd.append("filename", nombreArchivo);
    fd.append("tipo", 1);
    fd.append("carpetaFoto", tipoFoto);
    var xhr = new XMLHttpRequest();
    xhr.upload.addEventListener('progress', UploadProgress, false);
    xhr.onreadystatechange = StateChange;
    xhr.open('POST', '../Fakes/fileupload.aspx');
    xhr.send(fd);

}

//Funcion que muestra el progreso de la carga
function UploadProgress(event) {
    var percent = parseInt(event.loaded / event.total * 100);
    $('#attach_link').text('Uploading: ' + percent + '%');

}

//Funcion que muestra el cambio de estado del evento de carga de archivo
function StateChange(event) {

    if (event.target.readyState == 4) {
        if (event.target.status == 200 || event.target.status == 304) {
            //$('#attach_link').text('Agregar archivo »');
        }
        else {
        }

    }

}

function GenerateUUID() {
    var d = new Date().getTime();
    var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = (d + Math.random() * 16) % 16 | 0;
        d = Math.floor(d / 16);
        return (c == 'x' ? r : (r & 0x7 | 0x8)).toString(16);
    });
    return uuid;
};


function ObtenerListaProblemas() {
    var listaProblemas = [];
    $('.problema input[type=checkbox]:checked').each(function () {
        var problema = {};
        problema.ProblemaId = $(this).attr("value");

        listaProblemas.push(problema);
    });
    return listaProblemas;
}

function ObtenerListaGenericos() {
    var listaGenericos = [];
    $('.sintomaGenerico input[type=checkbox]:checked').each(function () {
        var generico = {};
        generico.ProblemaId = $(this).attr("value");

        listaGenericos.push(generico);
    });
    return listaGenericos;
}

function ObtenerListaSintomas() {
    var listaSintomas = [];
    $('.sintoma input[type=checkbox]:checked').each(function () {
        var sintoma = {};
        sintoma.SintomaID = $(this).attr("value");

        listaSintomas.push(sintoma);
    });

    if ($("#cmbParteGolpeada").val() > 0) {
        var sintoma = {};
        sintoma.SintomaID = $("#cmbParteGolpeada").val();
        listaSintomas.push(sintoma);
    }
    return listaSintomas;
}

function ValidarAretesDeteccion() {
    if ($("#chkSi").is(':checked') || (validarPartidaRecepcion == 0 && (($("#txtNoArete").val() != "" || $("#txtAreteTestigo").val() != "") || !($("#chkNo").is(':checked'))))) {
        if ($("#txtNoArete").val() != "" || $("#txtAreteTestigo").val() != "") {
            if (ObtenerExisteArete($("#txtNoArete"), 0) == true) {
                if (ObtenerExisteAreteTestigo($("#txtNoArete"), $("#txtAreteTestigo"), 0) == true) {
                    return true;
                } else {
                    return false;
                }
            } else {
                return false;
            }
        } else {
            if (msjAbierto == 0) {
                msjAbierto = 1;
                var mensaje = bootbox.alert(msgCapturarArete, function () {
                    msjAbierto = 0;
                });
            }
            return false;
        }
    } else {
        return true;
    }
}

function ValidarAretesMuerte() {
    if (!($("#chkSinArete").is(':checked'))) {
        if ($("#txtAreteGanadoMuerto").val() != "" || $("#txtAreteTestigoGanadoMuerto").val() != "") {
            if (ObtenerExisteArete($("#txtAreteGanadoMuerto"), 1) == true) {
                if (ObtenerExisteAreteTestigo($("#txtAreteGanadoMuerto"), $("#txtAreteTestigoGanadoMuerto"), 1) == true) {
                    return true;
                } else {
                    return false;
                }
            } else {
                return false;
            }
        } else {
            $("#dlgGanadoMuertoDialogo").modal('hide');
            if (msjAbierto == 0) {
                msjAbierto = 1;
                var mensaje = bootbox.alert(msgDatosBlanco, function () {
                    $("#dlgGanadoMuertoDialogo").modal('show');
                    mensaje.modal('hide');
                    msjAbierto = 0;
                });
            }
            return false;
        }
    } else {
        return true;
    }
}

/*
 * Funcion para validar letras y numeros
 */
solo_letrasnumeros = function (event) {
    var charCode = (event.which) ? event.which : 0;

    if (charCode < 32 ||
        (charCode > 47 && charCode < 58) ||
        (charCode >= 65 && charCode <= 90) ||
        (charCode >= 97 && charCode <= 122) ||
        (charCode >= 48 && charCode <= 57) ||
        charCode == 44 || charCode == 46 || charCode == 32 ||
        charCode == 8 || charCode == 0 || charCode == 241 ||
        charCode == 209 || charCode == 193 || charCode == 201 ||
        charCode == 205 || charCode == 211 || charCode == 218 ||
        charCode == 225 || charCode == 233 || charCode == 237 ||
        charCode == 243 || charCode == 250 || charCode == 188 || charCode == 190 || (charCode >= 37 && charCode <= 40))
        return true;
    else {
        return false;
    }
};


mostrarImagen = function (id) {
    $("#dlgNotificaciones").modal("hide");
    var fancy = bootbox.alert("<div height='500'><label class='span12'><img class= 'alieacionCentro' width='300' height='300' style='width:400px;height:400px;' src='" + $(id).attr("src") + "'></label></div>", function () {
        $("#dlgNotificaciones").modal("show");
        fancy.modal('hide');
    });
}