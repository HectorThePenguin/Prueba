/// <reference path="../assets/plugins/jquery-1.7.1-vsdoc.js" />
/// <reference path="../assets/plugins/data-tables/jquery.dataTables.js" />
/// <reference path="../assets/plugins/jquery-linq/linq-vsdoc.js" />
/// <reference path="~/assets/plugins/bootstrap-bootbox/js/bootbox.min.js" />
/// <reference path="../assets/plugins/data-tables/jquery.FixedHeaderTable.js" />

/*[Funciones Generales]*/

//Función para verificar si el evento pertenece a una tecla de número
isNumberKey = function (evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    return !(charCode > 31 && (charCode < 48 || charCode > 57));
};

//Fucnión para regresar la la parte izquierda de una cadena
Left = function (valor, longitud) {
    if (longitud <= 0)
        return "";
    else if (longitud > String(valor).length)
        return valor;
    else
        return String(valor).substring(0, longitud);
};

//Fucnión para regresar la la parte derecha de una cadena
Right = function (valor, longitud) {
    if (longitud <= 0)
        return "";
    else if (longitud > String(valor).length)
        return valor;
    else {
        var iLen = String(valor).length;
        return String(valor).substring(iLen, iLen - longitud);
    }
};

//Fucnión para verificar la longitud de un objeto
maxLengthCheck = function (object) {
    if (object.value.length > object.maxLength) {
        object.value = object.value.slice(0, object.maxLength);
    }
};

//Función para regresar una hora en formato hh:mm:ss
HoraFormateada = function (valor) {
    var fecha = new Date(valor),
        hora = Right('00' + fecha.getHours(), 2),
        minutos = Right('00' + fecha.getMinutes(), 2),
        segundos = Right('00' + fecha.getSeconds(), 2);

    return [hora, minutos, segundos].join(':');
};

//Función para regresar una fecha en formato dd/mm/yyyy
FechaFormateada = function (date) {
    var d = new Date(date),
        month = Right('00' + (d.getMonth() + 1), 2),
        day = Right('00' + d.getDate(), 2),
        year = Right('0000' + d.getFullYear(), 4);

    if (year > 2050) {
        day = '01';
        month = '01';
        year = '0001';
    }
    return [day, month, year].join('/');
};

//Función para intentar Parsear un valor a entero si no es valido regresa un default Value
TryParseInt = function (str, defaultValue) {
    var retValue = defaultValue;
    if (str != null) {
        if (str.length > 0) {
            if (!isNaN(str)) {
                retValue = parseInt(str);
            }
        }
    }
    return retValue;
};

TryParseDecimal = function (str, defaultValue) {
    var retValue = defaultValue;
    if (str != null) {
        if (str.length > 0) {
            if (!isNaN(str)) {
                retValue = parseFloat(str);
            }
        }
    }
    return retValue;
};

/*[Funciones Pantalla]*/

//Función para bloquear la pantalla mientras se ejecuta una operación
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

//Función para bloquear la pantalla mientras se ejecuta una operación
BloquearDiv = function (control) {
    var lock = document.getElementById(control);
    if (lock) {
        lock.className = 'LockOn';
        $('#' + control).spin(
            {
                top: '30',
                color: '#6E6E6E'
            });
    }
};

//Función para desbloquear la pantalla mientras se ejecuta una operación
DesbloquearDiv = function (control) {
    $('#' + control).spin(false);
    var lock = document.getElementById(control);
    lock.className = 'LockOff';
};

//Función para desbloquear la pantalla mientras se ejecuta una operación
DesbloquearPantalla = function () {
    $("#skm_LockPane").spin(false);
    var lock = document.getElementById('skm_LockPane');
    lock.className = 'LockOff';
};

//Función para bloquear la pantalla  modal mientras se ejecuta una operación
BloquearModal = function () {
    var lock = document.getElementById('divModal');
    if (lock) {
        lock.className = 'LockOnModal';
        $('#divModal').spin(
            {
                top: '30',
                color: '#6E6E6E'
            });
    }
};

//Función para desbloquear la pantalla modal mientras se ejecuta una operación
DesbloquearModal = function () {
    $("#divModal").spin(false);
    var lock = document.getElementById('divModal');
    lock.className = 'LockOff';
};

//Función para agregar días a una Fecha
Date.prototype.addDays = function (days) {
    var dat = new Date(this.valueOf());
    dat.setDate(dat.getDate() + days);
    return dat;
};

//Función para agregar días a una Fecha
Date.prototype.removeDays = function (days) {
    var dat = new Date(this.valueOf());
    dat.setDate(dat.getDate() - days);
    return dat;
};

//Función para convertir una cadena a Fecha, formato cadena dd/mm/yyyy
ToDate = function (str1) {
    // str1 format should be dd/mm/yyyy. Separator can be anything e.g. / or -. It wont effect
    var dt1 = parseInt(str1.substring(0, 2));
    var mon1 = parseInt(str1.substring(3, 5));
    var yr1 = parseInt(str1.substring(6, 10));
    var date1 = new Date(yr1, mon1 - 1, dt1);
    return date1;
};

//Función para regresar la ruta root de la pagina
function getRootUrl() {
    var defaultPorts = { "http:": 80, "https:": 443 };

    return window.location.protocol + "//" + window.location.hostname
    + (((window.location.port)
    && (window.location.port != defaultPorts[window.location.protocol]))
    ? (":" + window.location.port) : "");
}

//Funcion para manejar las llamadas a los WebMethods
EjecutarWebMethod = function (url, datos, funcionSuccess, mensajeError, param) {
    param = param || { };
    var opt = $.extend({ }, {
        type: "POST",
        url: url,
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(msg) {
            funcionSuccess(msg);
        },
        error: function(e) {
            DesbloquearPantalla();
            bootbox.alert(mensajeError);
        }
    }, param);
    $.ajax(opt);
};

//Función para obtener la diferencia de Horas entre 2 fechas
DiferenciaHorasFechas = function (fecha1, fecha2) {
    return Math.abs(fecha1 - fecha2) / 36e5;
};

//Función para mostrar los mensajes
MostrarMensaje = function (mensaje, funcionCallback) {
    bootbox.dialog({
        message: mensaje,
        buttons: {
            Aceptar: {
                label: 'Ok',
                callback: funcionCallback
            }
        }
    });
};
//Formatea la cantidad separando con comas los miles
function FormatearCantidad(cantidad) {
    //Seperates the components of the number
    var n = cantidad.toString().split(".");
    //Comma-fies the first part
    n[0] = n[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    //Combines the two sections
    return n.join(".");
}