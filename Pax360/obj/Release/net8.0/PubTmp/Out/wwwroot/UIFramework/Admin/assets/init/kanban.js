$(function () {
    $("ul.droptrue").sortable({
        connectWith: "ul",
        dropOnEmpty: true,
    });

    $("ul.dropfalse").sortable({
        connectWith: "ul",
        dropOnEmpty: false,
    });

    $("#sortable1, #sortable2, #sortable3").disableSelection();

    $("#sortable1, #sortable2, #sortable3").sortable(
        {
            receive: function (event, ui) {
                var datavalue = ui.item[0].id + "#" + ui.item.index() + "#" + event.target.id;

                $.ajax({
                    type: 'POST',
                    url: '/Plannings/UpdateKanban/',
                    data: JSON.stringify(datavalue),
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                    },
                    error: function () {
                    }
                });
                return true;
            }
        }).disableSelection();
});