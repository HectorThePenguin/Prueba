//Archivo Javascript que contiene el control y handlers de la funcionalidad Salida por Muerte con la seleccion de arete
//Autor: Andres Vejar
//Nos aseguramos que estén definidas
//algunas funciones básicas

//Document Ready Function, la pagina esta completamente cargada
$(document).ready(function() {
    //Datos de inicializacion de formulario
    document.getElementById('files').addEventListener('change', HandleFileSelect, false);
    $("#btnGuardar").html(btnGuardarText);
    $("#btnGuardar").prop('disabled', true);
    $("#btnCancelar").html(btnCancelarText);

    $("a#imageDetect").attr('href', $('#imgFotoDeteccion').prop('src'));
    $("a#imageDetect").fancybox();

    $("a#imageNecrop").fancybox();

    $('#txtObservaciones').bind("cut copy paste", function(e) {
        e.preventDefault();
    });

     $('#txtObservaciones').keypress(function (e) {
         if (!solo_letrasnumeros(e)) {
             e.preventDefault();
         } else {
             var charCode = (event.which) ? event.which : 0;
             if (!((charCode >= 37 && charCode <= 40) || charCode == 8)) {
                 if ($('#txtObservaciones').val().length >= 254) {
                     var texto = $('#txtObservaciones').val();
                     $('#txtObservaciones').val(texto.substring(0, 255));
                 }
             }
         }
     });

     $('#txtObservaciones').keyup(function () {
         if ($('#txtObservaciones').val().length >= 255) {
             var texto = $('#txtObservaciones').val();
             $('#txtObservaciones').val(texto.substring(0, 255));
         }
     });

    $("#btnTomarFoto").click(function () {
        $('#files').trigger('click');
    });

    if ($('#cmbProblemas option').length == 0) {
        MostrarNoHayProblemas();
    }

    $('#btnGuardar').click(function () {
        GuardarSalida();
    });

    $("#cmbProblemas").change(function () {

        if ($('#cmbProblemas').val() != 0)
            HabilitarGuardado();
        else
            $("#btnGuardar").prop('disabled', true);
    });

    $('#btnCancelar').click(function () {
        bootbox.dialog({
                message: msgDlgCancelar,
                //title: msgTitulo,
                //closeButton: true,
                buttons: {
                    success: {
                        label: "Si",
                        className: "btn SuKarne",
                        callback: function () {
                            GoSalidaPorMuerte();
                        }
                    },
                    danger: {
                        label: "No",
                        className: "btn SuKarne",
                        callback: function () {
                            ;
                        }
                    }
                }
            });
    });
    

});

/*
 * Guardar salida del arete por necropsia
 */
function GuardarSalida(parameters) {
    var archivoSeleccionado = document.getElementById('files').files[0];

    if ($('#txtObservaciones').val().trim() == '') {
        MostrarObservacionesObligatorias();
        return false;
    }

    $("#btnGuardar").prop('disabled', true);

    if (archivoSeleccionado != null) {
        var archivo = GenerateUUID(); //creamos el uuid para la imagen
        var muerteId = $('#hdMuerteId').val();
        var animalId = $('#hdAnimalId').val();
        var loteId = $('#hdLoteId').val();
        var corralId = $('#hdCorralId').val();

        var arete = $('#txtNumeroArete').val();
        var corral = $('#txtCorral').val();
        var areteMetalico = $('#txtAreteMetalico').val();
        var problemaSeleccionado = $('#cmbProblemas').val();

        var observaciones = $('#txtObservaciones').val();
        var peso = $('#txtPeso').val();

        var parametros = {};
        parametros.CorralCodigo = corral;
        parametros.Arete = arete;
        parametros.AreteMetalico = areteMetalico;
        parametros.ProblemaId = problemaSeleccionado;
        parametros.FotoNecropsia = archivo + '.jpg';
        parametros.Observaciones = observaciones;
        parametros.Peso = peso;

        parametros.CorralId = corralId;
        parametros.LoteId = loteId;
        parametros.MuerteId = muerteId;
        parametros.AnimalId = animalId;

        //cargamos el archivo
        UploadFile(parametros.FotoNecropsia, 1);
        //Guardamos la salida

        App.bloquearContenedor($(".contenedor-salidapormuerte"));
        $.ajax({
            type: "POST",
            url: "SalidaPorMuerteSeleccion.aspx/GuardarSalida",
            contentType: "application/json; charset=utf-8",
            data: SerialiceJSON(parametros),
            dataType: "json",
            success: function (data) {
                App.desbloquearContenedor($(".contenedor-salidapormuerte"));
                if (data.d.EsValido == true) {
                    MostrarGuardadoExito(data);
                } else {
                    MostrarGuardadoFallido(data.d.Mensaje);
                }
            },
            error: function (request) {
                App.desbloquearContenedor($(".contenedor-salidapormuerte"));
                MostrarGuardadoFallido(request);
            }
        });
       
    } else {
        bootbox.alert(msgSinImagen, function () {
            $("#btnGuardar").prop('disabled', false);
        });
    }

}

/* 
 *Funcion que muestra else dialogo de que no existen Problemas configurados
 */
function MostrarNoHayProblemas(data) {
    bootbox.dialog({
        message: msgNoHayProblemas,
        //title: msgTitulo,
        //closeButton: true,
        buttons: {
            success: {
                label: "Ok",
                //className: "btn SuKarne",
                callback: function () {
                    GoSalidaPorMuerte();
                }
            }
        }
    });
};

MostrarObservacionesObligatorias = function()
{
    bootbox.dialog({
        message: msgObservacionesObligatorias,
        //title: msgTitulo,
        //closeButton: true,
        buttons: {
            success: {
                label: "Ok",
                //className: "btn SuKarne",
                callback: function () {
                    $('txtObservaciones').focus();
                }
            }
        }
    });
}

MostrarFalloCargarDatos = function() {

    bootbox.dialog({
        message: msgFalloAlCargarDatos,
        //title: msgTitulo,
        //closeButton: true,
        buttons: {
            success: {
                label: "Ok",
                //className: "btn SuKarne",
                callback: function() {
                    GoSalidaPorMuerte();
                }
            }
        }
    });

};

/*
 * Funcion que muestra el dialogo de guardado con exito
 */
function MostrarGuardadoExito(data) {
    bootbox.dialog({
        message: "<img src='../Images/Correct.png'/>&nbsp;" + msgGuardadoExito,
        //title: msgTitulo,
        //closeButton: true,
        buttons: {
            success: {
                label: "Ok",
                //className: "btn SuKarne",
                callback: function () {
                    GoSalidaPorMuerte();
                }
            }
        }
    });
};

/*
 * Funcion que muestra el guardado fallido
 */
function MostrarGuardadoFallido(mensaje) {
        bootbox.dialog({
        message: msgGuardadoFallido + ': ' + mensaje,
        //title: msgTitulo,
        //closeButton: true,
        buttons: {
            success: {
                label: "Ok",
                //className: "btn SuKarne",
                callback: function () {
                    HabilitarGuardado();
                }
            }
        }
    });
}

//funcion que habilita el boton de guardado
function HabilitarGuardado()
{
    $('#btnGuardar').prop("disabled", false);
}
//Funcion que envia a la pantalla de salida por muerte
function GoSalidaPorMuerte() {
    document.location.href = 'SalidaPorMuerte.aspx';
}

//Funcion que envia el post para guardar el arhivo
function UploadFile(nombreArchivo, tipo) {

    var fd = new FormData();
    fd.append("fileupload", document.getElementById('files').files[0]);
    fd.append("filename", nombreArchivo);
    fd.append("tipo", tipo);
    fd.append("carpetaFoto", 4);//Fotos de necropsia
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
//Funcio que liga el estado change para mistrar la imagen de necropsia
HandleFileSelect = function(evt) {
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
                span.setAttribute('id', 'imageNecrop');
                span.setAttribute('href', e.target.result);

                document.getElementById('list').innerHTML = '';
                document.getElementById('list').insertBefore(span, null);
            };
        })(f);
        // Read in the image file as a data URL.
        reader.readAsDataURL(f);

    }
};

function solo_letras(event) {

    keychar = String.fromCharCode(event);

    var charCode = (event.which) ? event.which : 0;
    if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || (charCode >= 48 && charCode <= 57) || charCode == 44 || charCode == 46 || charCode == 32 || charCode == 8 || charCode == 0 || charCode == 241 || charCode == 209 || charCode == 193 || charCode == 201 || charCode == 205 || charCode == 211 || charCode == 218 || charCode == 225 || charCode == 233 || charCode == 237 || charCode == 243 || charCode == 250)
        return true;

    else {

        return false;
    }


}

/*
 * Genera el identificador unico
 */
GenerateUUID = function() {
    var d = new Date().getTime();
    var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = (d + Math.random() * 16) % 16 | 0;
        d = Math.floor(d / 16);
        return (c == 'x' ? r : (r & 0x7 | 0x8)).toString(16);
    });
    return uuid;
};

//-------------------------------------------------------------------
// Generar codigo JSON
//-------------------------------------------------------------------
SerialiceJSON = function (value) {
    var data = { "value": JSON.stringify(value) };
    return JSON.stringify(data);
};