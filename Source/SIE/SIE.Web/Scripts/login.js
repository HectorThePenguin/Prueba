var rutaPantalla = location.pathname;


$(document).ready(function () {
    App.init();

    $('#btnAccesar').click(function () {
        var esValido = $('.login-form').validate().form();
        if (esValido) {
            Accesar();
        }
    });

    $('.login-form input').keypress(function (e) {
        if (e.which == 13) {
            if ($('.login-form').validate().form()) {
                Accesar();
            }
            return false;
        }
    });

    $('.login-form').validate({
        errorElement: 'label', //default input error message container
        errorClass: 'authError', // default input error message class
        focusInvalid: false, // do not focus the last invalid input

        rules: {
            username: {
                required: true
            },
            password: {
                required: true
            }
        },

        invalidHandler: function (event, validator) { //display error alert on form submit   
            $('.alert-error', $('.login-form')).show();
        },
        success: function (label) {
            label.closest('.control-group').removeClass('error');
            label.remove();
        },
        errorPlacement: function (error, element) {
        },
        submitHandler: function (form) {
        }
    });
});


Accesar = function () {
    var usuario = $('#txtUsuario').val();
    var contra = $('#txtContra').val();
    var datos = { 'usuario': usuario, 'contra': contra };
    BloquearPantalla();
    $.ajax({
        type: "POST",
        url:   rutaPantalla + '/VerificarUsuario',
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (acceso) {
            DesbloquearPantalla();
            
            if (acceso.d == null) {
                bootbox.dialog({
                    title: window.InformacionTitulo,
                    message: window.ErrorUsuario,
                    buttons: {
                        Aceptar: {
                            label: window.BtnAceptar,
                            callback: function () {
                            }
                        }
                    }
                });
                
                
            }
            else {
                switch (acceso.d.NivelAcceso) {
                    case 1: $(location).attr('href', '../Principal.aspx');
                        break;
                    case 2:
                        var rutaServidor = location.host;
                        myRedirect('http://siap.sukarne.com/Siap_web_Abasto/Principal.aspx', "Usuario", acceso.d.UsuarioActiveDirectory);
                        
                        //myRedirect('http://' + rutaServidor + '/Piloto_SIAPWeb/Principal.aspx', "Usuario", acceso.d.UsuarioActiveDirectory);
                        break;
                    case 3: $(location).attr('href', '../Acceso.aspx');
                        break;
                    default:
                        $(location).attr('href', '../Principal.aspx');
                }
                
            }
        },
        error: function (e) {
            DesbloquearPantalla();
            bootbox.dialog({
                title: window.ErrorTitulo,
                message: window.ErrorValidar,
                buttons: {
                    Aceptar: {
                        label: window.BtnAceptar,
                        callback: function () {
                        }
                    }
                }
            });
        }
    });
    

   
};
var myRedirect = function (redirectUrl, arg, value) {
    var form = $('<form action="' + redirectUrl + '" method="post">' +
    '<input type="hidden" name="' + arg + '" value="' + value + '"></input>' + '</form>');
    $('body').append(form);
    $(form).submit();
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