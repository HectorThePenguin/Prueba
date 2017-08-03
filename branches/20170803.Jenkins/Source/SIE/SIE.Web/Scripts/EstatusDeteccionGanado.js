

$(document).ready(function () {
    
    $("#txtCorral").inputmask({ "mask": "*", "repeat": 10, "greedy": false });
    $('#txtArete').numericInput();
    //$("#txtArete").inputmask({ "mask": "*", "repeat": 15, "greedy": false });
    $('#txtAreteTestigo').numericInput();
    //$("#txtAreteTestigo").inputmask({ "mask": "*", "repeat": 15, "greedy": false });
    
    $('#txtHora').timepicker();
    
    $('#btnGuardar').attr("disabled", "disabled");
    
    $('#txtCorral').keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == 13 || code == 9) {
            $('#checkSinArete').focus();
        }
    });
    
    $('input').bind("cut copy paste", function (e) {
        e.preventDefault();
    });

    $('#txtArete').focus(function (e) {
        ValidarCorral();
    });
    
    $('#txtAreteTestigo').focus(function (e) {
        if ($.trim($("#txtArete").val()) == "") {
            ValidarCorral();
        } else {
            ValidarArete();
        }
    });
    
    $('#txtAcuerdo').focus(function (e) {
        ValidarAreteTestigo();
    });
    
    $('#checkSinArete').focus(function (e) {
        ValidarCorral();
    });
    
    $("#btnLimpiar").click(function () {
        LimpiarControles();
        $("#txtCorral").focus();
    });

    $("#btnAgregar").click(function () {
        var conceptos = $('input[name=rdConceptos]:checked');
        var conceptoSeleccionado = conceptos[0].attributes["label"].textContent;
        var areteEncontrado = false;

        if (($.trim($("#txtAcuerdo").val()) == "" || $.trim($("#txtCorral").val()) == "") ||
            (($.trim($("#txtArete").val()) == "" && $.trim($("#txtAreteTestigo").val()) == "") && !$("#checkSinArete").is(':checked')) ||
            ($("#checkSinArete").is(':checked') && $('#dvFoto').html() == "")) {

            bootbox.alert(window.mensajeExistenDatosEnBlanco, function() {
                if ($.trim($("#txtCorral").val()) == "") {
                    $('#txtCorral').focus();
                } else if ($("#checkSinArete").is(':checked') && $('#dvFoto').html() == "") {
                    $('#checkSinArete').focus();
                } else if ($.trim($("#txtArete").val()) == "") {
                    $('#txtArete').focus();
                } else if ($.trim($("#txtAreteTestigo").val()) == "") {
                    $('#txtAreteTestigo').focus();
                } else if ($.trim($("#txtAcuerdo").val()) == "") {
                    $('#txtAcuerdo').focus();
                }
            });
        } else {

            if (ValidarArete() && ValidarAreteTestigo()) {
                var nodes = $("#detectados tbody tr");

                $(nodes).each(function() {
                    //console.log(ObtenerTestigoPorArete($('#txtArete').val()));
                    //console.log(ObtenerAretePorTestigo($('#txtAreteTestigo').val()));
                    if (($('#txtArete').val() == this.cells[2].innerHTML && $.trim($('#txtArete').val()) != "")
                        || ($('#txtAreteTestigo').val() == this.cells[3].innerHTML && $.trim($('#txtAreteTestigo').val()) != "")
                        || (ObtenerAretePorTestigo($('#txtAreteTestigo').val()) == this.cells[2].innerHTML && ObtenerAretePorTestigo($.trim($('#txtAreteTestigo').val())) != "" && $.trim($('#txtAreteTestigo').val()) != "")
                        || (ObtenerTestigoPorArete($('#txtArete').val()) == this.cells[3].innerHTML && ObtenerTestigoPorArete($.trim($('#txtArete').val())) != "") && $.trim($('#txtArete').val()) != "") {
                        areteEncontrado = true;
                    }
                });

                if (areteEncontrado) {
                    bootbox.alert(window.mensajeAreteYaFueCapturado, function() {
                        $('#txtArete').focus();
                    });
                } else {

                    $.ajax({
                        type: "POST",
                        url: "EstatusDeteccionGanado.aspx/ValidarAretesDetectado",
                        data: JSON.stringify({ 'arete': $('#txtArete').val(), 'areteTestigo': $('#txtAreteTestigo').val() }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        error: function(request) {
                            bootbox.alert(request.Message);
                        },
                        success: function(data) {
                            if (data.d == 1) {
                                bootbox.alert(window.mensajeAreteYaDetectado, function() {
                                    $("#txtArete").focus();
                                });
                            } else {
                                var imagen = $("#dvFoto a .thumb");
                                var nuevaimagen = "";

                                if (imagen.length > 0) {
                                    nuevaimagen = '<a id="imagengrande' + nodes.length + '"><img src="' + imagen[0].src + '" width="60" height="60" "></a>';
                                }

                                $("#detectados tbody").append('<tr><td class="span2">' + $('#txtVaqueroDetector').val() + '</td><td class="span1">' + $('#txtHora').val() + '</td><td class="span2">' + $('#txtArete').val() + '</td><td class="span2">' + $('#txtAreteTestigo').val() + '</td><td class="span2">' + nuevaimagen + '</td><td id="' + $('input[name=rdConceptos]:checked').val() + '" enfermeria="' + $("#cmbEnfermeria").val() + '" corral="' + $("#txtCorral").val() + '" class="span2">' + conceptoSeleccionado + '</td><td class="span2">' + $('#txtAcuerdo').val() + '</td></tr>');

                                if (imagen.length > 0) {
                                    $('#imagengrande' + nodes.length).click(function() {
                                        var fancy = bootbox.alert("<img src='" + imagen[0].src + "'>", function() {
                                            fancy.modal('hide');
                                        });
                                    });
                                }

                                $("#btnGuardar").removeAttr("disabled");
                                LimpiarControles();
                            }
                        }
                    });
                }
            }
        }
    });
    
    $("#btnCancelar").click(function () {
        bootbox.dialog({
            message: window.mensajeCancelacion,
            title: "",
            buttons: {
                success: {
                    label: "SI",
                    className: "btn-default",
                    callback: function () {
                        $("#txtCorral").val("");
                        LimpiarControles();
                        $("#detectados tbody").html("");
                    }
                },
                danger: {
                    label: "NO",
                    className: "btn-default",
                    callback: function () {
                    }
                }
            }
        });
    });

    $("#btnGuardar").click(function (){        
        var nodes = $("#detectados tbody tr");
        var info = {};
        var listaDetectados = [];
        
        $(nodes).each(function () {
            info = {};
            info.SupervisionGanadoID = 0;
            info.CodigoCorral = this.cells[5].attributes.corral.value;
            info.Arete = this.cells[2].innerHTML;
            info.AreteMetalico = this.cells[3].innerHTML;
            info.FechaDeteccion = this.cells[1].innerHTML;
            info.ConceptoDeteccionID = this.cells[5].id;
            info.Acuerdo = this.cells[6].innerHTML;
            info.Notificacion = 0;
            info.Activo = 0;
            
            if ($(this.cells[4])[0].childNodes.length > 0) {
                var uuid = GeneraUUID();
                info.FotoSupervision = uuid + ".JPG";
                SubirFoto(info.FotoSupervision, $(this.cells[4])[0].childNodes[0].childNodes[0].src);
            } else {
                info.FotoSupervision = "";
            }

            listaDetectados.push(info);
        });
        
        var datos = { 'supervisionParam': listaDetectados };

        $.ajax({
            type: "POST",
            url: "EstatusDeteccionGanado.aspx/GuardarEstatusDeteccion",
            data: JSON.stringify(datos),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            error: function(request) {
                bootbox.alert(request.Message);
            },
            success: function (data) {
                $("#txtCorral").val("");
                bootbox.alert("<img src='../Images/Correct.png'/>&nbsp;"+window.mensajeGuardadoOk, LimpiarControles);
            }
        });
        $('#btnGuardar').attr("disabled", "disabled");
        $("#detectados tbody").html("");
    });
    
    $("#cmbEnfermeria").change(function () {
        LimpiarControles();
        $("#txtCorral").val("");
    });

    $('#checkSinArete').change(function () {
        if ($(this).is(':checked')) {
            $("#txtArete").attr("disabled", "disabled");
            $("#txtAreteTestigo").attr("disabled", "disabled");
            $("#txtArete").val('');
            $("#txtAreteTestigo").val('');
            $("#btnTomarFoto").show();
        } else {
            $("#txtArete").removeAttr("disabled");
            $("#txtAreteTestigo").removeAttr("disabled");
            $("#btnTomarFoto").hide();
            $('#dvFoto').html('');
            $('#flFoto').val('');
        }
    });
    
    $("#btnTomarFoto").click(function () {
        $('#flFoto').trigger('click');
    });
    
    //Funcio que liga el estado change para mistrar la imagen de necropsia
    $('#flFoto').change(function(evt) {
        var files = evt.target.files; // FileList object
        // Loop through the FileList and render image files as thumbnails.
        for (var i = 0, f; f = files[i]; i++) {
            // Only process image files.
            if (!f.type.match('image.*')) {
                continue;
            }
            var reader = new FileReader();
            // Closure to capture the file information.
            reader.onload = (function(theFile) {
                return function(e) {
                    // Render thumbnail.
                    var span = document.createElement('a');
                    span.innerHTML = ['<img class="thumb" src="', e.target.result,
                        '" title="', escape(theFile.name), '"/>'].join('');

                    span.setAttribute('class', 'pull-rigth imagen-seleccion');
                    span.setAttribute('id', 'imageVaca');

                    document.getElementById('dvFoto').innerHTML = '';
                    document.getElementById('dvFoto').insertBefore(span, null);

                    $('#imageVaca').click(function () {
                        var fancy = bootbox.alert("<img src='" + e.target.result + "'>", function() {
                            fancy.modal('hide');
                        });
                    });
                };
            })(f);
            // Read in the image file as a data URL.
            reader.readAsDataURL(f);

        }
    });
});

var corralAux = "", areteAux = "", areteTestigoAux = ""; //Variable para que cuando pierda el foco y ya se haya validado el campo, no lo vuelva a validar.
var listaAretes = [];
var corralCompraDirecta = false;

ValidarCorral = function () {
    
   if ($.trim($('#txtCorral').val()) == "") {
        LimpiarControles();
        $('#txtCorral').focus();
        corralAux = "";
    }
    else{
        if (corralAux != $.trim($('#txtCorral').val())) {
            corralAux = $.trim($('#txtCorral').val());

            $("#txtVaqueroDetector").val("");
            $("#txtHora").val("");
            $("#txtArete").val("");
            $("#txtAreteTestigo").val("");
            $("#txtTipoCorralId").val("0");
            areteAux = "";
            areteTestigoAux = "";
            listaAretes = [];
            
            var info = {};
            info.Codigo = $.trim($('#txtCorral').val());

            var datos = { 'corralInfo': info, 'enfermeria': $("#cmbEnfermeria").val() };

            $.ajax({
                type: "POST",
                url: "EstatusDeteccionGanado.aspx/ObtenerDatosDelCorral",
                data: JSON.stringify(datos),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error: function(request) {
                    bootbox.alert(request.Message);
                },
                success: function(data) {
                    if (data.d) {
                        if (data.d.perteneceAEnfermeria == 1) {
                            $("#txtTipoCorralId").val(data.d.TipoCorral.TipoCorralID);
                            if (data.d.Operador) {
                                $("#txtVaqueroDetector").val(data.d.Operador.Nombre + " " + data.d.Operador.ApellidoPaterno + " " + data.d.Operador.ApellidoMaterno);
                            } else {
                                $("#txtVaqueroDetector").val(window.lblSinOperador);
                            }
                            ObtenerHoraServidor();
                            ObtenerAnimalesPorCorral();
                        } else {
                            LimpiarControles();
                            corralAux = "";
                            bootbox.alert(window.mensajeCorralNoPerteneceAEnfermeria, function() {
                                $("#txtCorral").focus();
                            });
                        }
                    } else {
                        LimpiarControles();
                        corralAux = "";
                        bootbox.alert(window.mensajeCorralNoValido, function() {
                            $("#txtCorral").focus();
                        });
                    }
                }
            });
        }
    }
};

ObtenerAnimalesPorCorral = function () {

    var info = {};
    var infoCorral = {};
    
    info.Codigo = $("#txtCorral").val();
    infoCorral.TipoCorralID = $("#txtTipoCorralId").val();
    info.TipoCorral = infoCorral;
    
    var datos = { 'corralInfo': info};

    $.ajax({
        type: "POST",
        url: "EstatusDeteccionGanado.aspx/ObtenerAnimalesPorCodigoCorral",
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (request) {
            bootbox.alert(request.Message);
        },
        success: function (data) {
            
            var recepcionYNoEsCompraDirecta = false;
            
            if (data.d) {
                if (data.d.length == 1) {
                    if (data.d[0].AnimalID == -1) {
                        recepcionYNoEsCompraDirecta = true;
                    }
                }

                if (recepcionYNoEsCompraDirecta) {
                    bootbox.alert(window.mensajeCorralSinAretes, LimpiarControles);
                } else {
                    $(data.d).each(function() {
                        listaAretes.push(this);
                    });

                    var aretes = [], aretesTestigos = [];
                    $('#txtArete').typeahead({
                        source: aretes
                    });

                    $('#txtAreteTestigo').typeahead({
                        source: aretesTestigos
                    });

                    $(listaAretes).each(function() {
                        aretes.push($(this)[0].Arete);
                        aretesTestigos.push($(this)[0].AreteMetalico);
                    });
                    $('#txtArete').typeahead({
                        source: aretes
                    });

                    $('#txtAreteTestigo').typeahead({
                        source: aretesTestigos
                    });
                }
            } else {
                corralCompraDirecta = true;
            }
        }
    });
};

ValidarArete = function () {
    var existeArete = false, respuesta = true;
    if (areteAux != $.trim($("#txtArete").val())) {
        areteAux = $.trim($("#txtArete").val());

        areteTestigoAux = "";
        //console.log(corralCompraDirecta);
        if (!corralCompraDirecta) { //Diferente de Recepcion
            $(listaAretes).each(function() {
                if (areteAux == $(this)[0].Arete) {
                    existeArete = true;
                }
            });
            
            if (!existeArete) {
                respuesta = ValidaEsAnimalDeCargaInicial(areteAux);
            }
        }
    }
    return respuesta;
};

ValidaEsAnimalDeCargaInicial = function (areteAux) {
    
    var info = {};
    info.Arete = $.trim(areteAux);
    var datos = { 'animalInfo': info };

    $.ajax({
        type: "POST",
        url: "EstatusDeteccionGanado.aspx/ValidaEsAnimalDeCargaInicial",
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (request) {
            bootbox.alert(request.Message);
            return false;
        },
        success: function (data) {
            if (data.d) {
                return true;
            } else {
                $('#txtArete').val("");
                areteAux = "";
                bootbox.alert(window.mensajeAreteInvalido, function () {
                    $('#txtArete').focus();
                });
                return false;
            }
        }
    });
    
};

ValidarAreteTestigo = function () {
    var existeArete = false, areteMetalicoPerteneceArete = false, respuesta = true;

    if ($.trim($("#txtCorral").val()) == "") {
        $('#txtCorral').focus();
        areteTestigoAux = "";
    }
    else if ($.trim($("#txtAreteTestigo").val()) == "" && $.trim($("#txtArete").val()) == "") {
        if (!corralCompraDirecta) { 
            $('#txtAreteTestigo').focus();
            areteTestigoAux = "";
        }
    } else {
        if (areteTestigoAux != $.trim($("#txtAreteTestigo").val())) {
            areteTestigoAux = $.trim($("#txtAreteTestigo").val());

            if (!corralCompraDirecta) {
                $(listaAretes).each(function() {
                    if (areteTestigoAux == $(this)[0].AreteMetalico) {
                        existeArete = true;
                        if (($.trim($("#txtArete").val()) == $(this)[0].Arete) || $.trim($("#txtArete").val()) == "") {
                            areteMetalicoPerteneceArete = true;
                        }
                    }
                });

                if (!existeArete) {
                    $('#txtAreteTestigo').val("");
                    areteTestigoAux = "";
                    if ($.trim($('#txtArete').val()) == "") {
                        bootbox.alert(window.mensajeAreteInvalido, function() {
                            $('#txtAreteTestigo').focus();
                        });
                        respuesta = false;
                    } else {
                        bootbox.alert(window.mensajeAreteTestigoNoPertenece, function() {
                            $('#txtAreteTestigo').focus();
                        });
                        respuesta = false;
                    }
                } else if (!areteMetalicoPerteneceArete) {
                    //console.log("3333");
                    $('#txtAreteTestigo').val("");
                    areteTestigoAux = "";
                    bootbox.alert(window.mensajeAreteTestigoNoPertenece, function() {
                        $('#txtAreteTestigo').focus();
                    });
                    respuesta = false;
                }
            }
        }
        else if ($.trim($("#txtAreteTestigo").val()) == "") {
            ValidarArete();
        }
    }
    return respuesta;
};

ObtenerHoraServidor = function () {

    $.ajax({
        type: "POST",
        url: "EstatusDeteccionGanado.aspx/ObtenerHoraServidor",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (request) {
            bootbox.alert(request.Message);
        },
        success: function (data) {
            $('#txtHora').val(data.d);
        }
    });
};

LlenarConceptosDeteccion = function () {
    $.ajax({
        type: "POST",
        url: "EstatusDeteccionGanado.aspx/ObtenerListadoDeConceptos",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (request) {
            bootbox.alert(request.Message);
        },
        success: function (data) {
            if (data.d) {
                $(data.d).each(function () {
                    $("#listaDeConceptos").append('<label><input id="rdConcepto' + $(this)[0].ConceptoID + '" name="rdConceptos" type="radio" value="' + $(this)[0].ConceptoID + '" label="' + $(this)[0].ConceptoDescripcion + '"> ' + $(this)[0].ConceptoDescripcion + '</label>');
                });
            } else {
                bootbox.alert(window.mensajeErrorAlConsultarConceptos, function() {
                    history.go(-1);
                    return false;
                });
                corralAux = -1;
            }
            LimpiarControles();
        }
    });
};

SubirFoto = function(nombreArchivo, src) {
    var fd = new FormData();
    fd.append("fileupload", src);
    fd.append("filename", nombreArchivo);
    fd.append("tipo", 2);
    fd.append("carpetaFoto", 3);
    var xhr = new XMLHttpRequest();
    xhr.open('POST', '../Fakes/fileupload.aspx');
    xhr.send(fd);
};

GeneraUUID = function () {
    var d = new Date().getTime();
    var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = (d + Math.random() * 16) % 16 | 0;
        d = Math.floor(d / 16);
        return (c == 'x' ? r : (r & 0x7 | 0x8)).toString(16);
    });
    return uuid;
};

function EnviarMensajeUsuarioErrorSession(mensaje) {
    bootbox.alert(mensaje, function() {
        history.go(-1);
        return false;
    });
}

function EnviarMensajeUsuario(mensaje) {
    bootbox.alert(mensaje, function () {
        history.go(-1);
        return false;
    });
}

function LimpiarControles() {
    $("#cmbEnfermeria").focus();
    $("#txtVaqueroDetector").val("");
    $("#txtHora").val("");
    $("#txtArete").val("");
    $("#txtAreteTestigo").val("");
    $("#txtAcuerdo").val("");
    $('input[name=rdConceptos]')[0].click();
    $("#txtTipoCorralID").val("");
    $("#txtArete").removeAttr("disabled");
    $("#txtAreteTestigo").removeAttr("disabled");
    $("#checkSinArete").attr('checked', false);
    $("#btnTomarFoto").hide();
    $('#dvFoto').html('');
    $('#flFoto').val('');
    
    corralAux = "";
    areteAux = "";
    areteTestigoAux = "";
    listaAretes = [];
    corralCompraDirecta = false;
}

function ObtenerTestigoPorArete(arete) {
    var areteTestigo = "";
    $(listaAretes).each(function () {
        if (arete == $(this)[0].Arete) {
            areteTestigo = $(this)[0].AreteMetalico;
        }
    });
    
    return areteTestigo;
}

function ObtenerAretePorTestigo(areteTestigo) {
    var arete = "";
    $(listaAretes).each(function () {
        if (areteTestigo == $(this)[0].AreteMetalico) {
            arete = $(this)[0].Arete;
        }
    });
    return arete;
}