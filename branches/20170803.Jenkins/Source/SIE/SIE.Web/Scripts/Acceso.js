/// <reference path="../assets/plugins/jquery-1.7.1-vsdoc.js" />
var rutaServidor = "http://localhost:3032/";
//var rutaServidor = location.host;
$(document).ready(function () {
    $('.ch-img-1').click(function() {
        $(location).attr('href', 'Principal.aspx');
    });
    $('.ch-img-2').click(function () {
        console.log( rutaServidor + 'Principal.aspx');
        var usuario = $('#hfUsuario').val();
        //myRedirect('http://siap.sukarne.com/Siap_web_Abasto/Principal.aspx', "Usuario", usuario);
        myRedirect(rutaServidor + 'Principal.aspx', "Usuario", usuario);
        //window.location.replace('http://' + rutaServidor + '/SIE.WEBPRUEBA/Principal.aspx?Usuario=' + usuario +'');
    });
});

var myRedirect = function (redirectUrl, arg, value) {
    var form = $('<form action="' + redirectUrl + '" method="post">' +
    '<input type="hidden" name="' + arg + '" value="' + value + '"></input>' + '</form>');
    $('body').append(form);
    $(form).submit();
};