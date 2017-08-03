//Archivo Javascript que contiene el control y handlers de la funcionalidad Salida por Muerte
//Autor: Andres Vejar
usuarioValido = true;

//Document Ready Function, la pagina esta completamente cargada
$(document).ready(function () {
    if(usuarioValido)
        ObtenerAretesMuertos();
});


/*
 * Obtener la lista de Aretes muertos para salida
 */
ObtenerAretesMuertos = function () {
    //modificar para obtener los movimientos
    $.ajax({
        type: "POST",
        url: "SalidaPorMuerte.aspx/ObtenerAretesMuertos",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (request) {
            MostrarFalloCargarDatos();
        },
        success: function (data)
        {
            if (data.d == null) {
                MostrarNoHayDatos();
                return;
            }

            var respuesta = {};
            respuesta.Aretes = data.d;
            respuesta.Recursos = LlenarRecursos();

            

            if (data.d.EsValido == false) {
                AgregaElementosTabla(respuesta);
               
            } else {
                AgregaElementosTabla(respuesta);
                
            }
        }
    });


};

/*
 * Funcion para mostrar el mensaje de que no existen muertes para salida
 */
MostrarNoHayDatos = function () {
    bootbox.dialog({
        message: msgNoHayMuertes,
        //title: msgSalidaPorMuerteTitulo,
        //closeButton: true,
        buttons: {
            success: {
                label: "Ok",
                //className: "btn SuKarne",
                callback: function () {
                    ;
                }
            }
        }
    });
};

/*
 * Funcion para mostrar el fallo al cargar los datos de salida
 */
MostrarFalloCargarDatos = function() {
    bootbox.dialog({
        message: msgFalloCargarDatos,
        //title: msgSalidaPorMuerteTitulo,
        //closeButton: true,
        buttons: {
            success: {
                label: "Ok",
                //className: "btn SuKarne",
                callback: function() {
                    ;
                }
            }
        }
    });
};

/*
 * Funcion para agregar los elementos al template de la tabla
 */
AgregaElementosTabla = function (datos) {
    if (datos != null) {
        $('#TablaAretesMuertos').setTemplateURL('../Templates/GridSalidaPorMuerte.htm');
        $('#TablaAretesMuertos').processTemplate(datos);
    } else {
        $('#TablaAretesMuertos').html("");
    }
};

/*
 * Funcion que muestra el mensaje de privilegios insuficientes
 */
EnviarMensajeUsuario = function() {
    usuarioValido = false;
    bootbox.dialog({
        message: msgNoTienePermiso,
        //title: msgSalidaPorMuerteTitulo,
        //closeButton: true,
        buttons: {
            success: {
                label: "Ok",
                //className: "btn SuKarne",
                callback: function() {
                    document.location.href = '../Principal.aspx';
                }
            }
        }
    });
};

/*
 * Funcion para llenar la lista de recursos para la tabla
 */
LlenarRecursos = function () {
    Resources = {};
    Resources.HeaderArete = hArete;
    Resources.HeaderAreteMetalico = hAreteMetalico
    Resources.HeaderCorral = hCorral;
    Resources.HeaderFecha = hFecha;
    Resources.HeaderOpcion = hOpcion;
    return Resources;
};