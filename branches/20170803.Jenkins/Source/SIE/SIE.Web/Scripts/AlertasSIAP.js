$(document).ready(function () {

    $.fn.extend({
        treed: function (o) {

            var openedClass = 'round_minus44.png';
            var closedClass = 'round_plus44.png';

            if (typeof o != 'undefined') {
                if (typeof o.openedClass != 'undefined') {
                    openedClass = o.openedClass;
                }
                if (typeof o.closedClass != 'undefined') {
                    closedClass = o.closedClass;
                }
            };

            //Inicializa cada uno de los niveles mayores
            var tree = $(this);
            tree.addClass("tree");
            tree.find('li').has("ul").each(function () {
                var branch = $(this); //elementos li con hijos ul
                branch.prepend("<img src='../Images/" + closedClass + "' class='indicator' height='25' width='25'></img>");
                branch.addClass('branch');
                branch.on('click', function (e) {
                    if (this == e.target) {
                        var icon = $(this).children('img:first');
                        icon.attr('src', function (index, attr) {
                            var srcClosed = "../Images/" + closedClass;
                            var srcOpened = "../Images/" + openedClass;
                            return attr == srcClosed ? srcOpened : srcClosed;
                        });
                        $(this).children().children().toggle();
                    }
                });
                branch.children().children().toggle();
            });
            //Dispara el evento con una clase indicador
            tree.find('.branch .indicator').each(function () {
                $(this).on('click', function () {
                    $(this).closest('li').click();
                });
            });
            //Dispara el evento si contiene un anchor en vez de texto
            tree.find('.branch>a').each(function () {
                $(this).on('click', function (e) {
                    $(this).closest('li').click();
                    e.preventDefault();
                });
            });
            //Dispara el evento si contiene un botón en vez de texto
            tree.find('.branch>button').each(function () {
                $(this).on('click', function (e) {
                    $(this).closest('li').click();
                    e.preventDefault();
                });
            });
            //Dispara el evento si contiene un span en vez de texto
            tree.find('.branch>span').each(function () {
                $(this).on('click', function (e) {
                    $(this).closest('li').click();
                    e.preventDefault();
                });
            });
        }
    });

    $('#tree1').treed();


    //Manda a llamar el submit de la form pasando los parametros del treeview
    $(".post").on("click", function () {
        var parametros = $(this).prop("id").split("|");
        $body = $("body");
        $body.addClass("cargando");
        console.log(parametros);
        $('<input />').attr('type', 'hidden')
        .attr('name', "moduloID")
        .attr('value', parametros[0])
        .appendTo('#idform');
        $('<input />').attr('type', 'hidden')
        .attr('name', "alertaID")
        .attr('value', parametros[1])
        .appendTo('#idform');
        $('<input />').attr('type', 'hidden')
        .attr('name', "estatusID")
        .attr('value', parametros[2])
        .appendTo('#idform');
        $("#idform").submit();

    });
});