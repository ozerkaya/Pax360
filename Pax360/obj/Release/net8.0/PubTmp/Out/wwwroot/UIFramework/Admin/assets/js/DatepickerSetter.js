$(document).ready(function () {
    if (document.getElementById("Search_Tarih1") != null) {
        var date_input = $('input[name="Search_Tarih1"]');
        var container = $('.bootstrap-iso form').length > 0 ? $('.bootstrap-iso form').parent() : "body";
        var options = {
            format: 'dd.mm.yyyy',
            container: container,
            todayHighlight: true,
            autoclose: true,

            daysOfWeekHighlighted: "0",
            language: "tr",
            locale: "tr",
        };
        date_input.datepicker(options);
    }
});

$(document).ready(function () {
    if (document.getElementById("Search_Tarih2") != null) {
        var date_input = $('input[name="Search_Tarih2"]');
        var container = $('.bootstrap-iso form').length > 0 ? $('.bootstrap-iso form').parent() : "body";
        var options = {
            format: 'dd.mm.yyyy',
            container: container,
            todayHighlight: true,
            autoclose: true,

            daysOfWeekHighlighted: "0",
            language: "tr",
            locale: "tr",
        };
        date_input.datepicker(options);
    }
});

$(document).ready(function () {
    if (document.getElementById("Order_Operation_randevuVerilenTarih") != null) {
        var date_input = $('input[name="Order.Operation.randevuVerilenTarih"]');
        var container = $('.bootstrap-iso form').length > 0 ? $('.bootstrap-iso form').parent() : "body";
        var options = {
            format: 'dd.mm.yyyy',
            container: container,
            todayHighlight: true,
            autoclose: true,

            daysOfWeekHighlighted: "0",
            language: "tr",
            locale: "tr",
        };
        date_input.datepicker(options);
    }
});

$(document).ready(function () {
    if (document.getElementById("Order_Operation_mudahaleTarihi") != null) {
        var date_input = $('input[name="Order.Operation.mudahaleTarihi"]');
        var container = $('.bootstrap-iso form').length > 0 ? $('.bootstrap-iso form').parent() : "body";
        var options = {
            format: 'dd.mm.yyyy',
            container: container,
            todayHighlight: true,
            autoclose: true,

            daysOfWeekHighlighted: "0",
            language: "tr",
            locale: "tr",
        };
        date_input.datepicker(options);
    }
});

$(document).ready(function () {
    if (document.getElementById("Order_Operation_mudahaleTarihi") != null) {
        var date_input = $('input[name="Order.Operation.randevuMudahaleTarih"]');
        var container = $('.bootstrap-iso form').length > 0 ? $('.bootstrap-iso form').parent() : "body";
        var options = {
            format: 'dd.mm.yyyy',
            container: container,
            todayHighlight: true,
            autoclose: true,

            daysOfWeekHighlighted: "0",
            language: "tr",
            locale: "tr",
        };
        date_input.datepicker(options);
    }
});

$(document).ready(function () {
    if (document.getElementById("Search_Tarih1") != null) {
        document.getElementById("Search_Tarih1").value = document.getElementById("Search_Tarih1").value.replace("1.01.0001", "").replace("01/01/0001", "").replace("1/1/0001", "");
        document.getElementById("Search_Tarih1").value = document.getElementById("Search_Tarih1").value.replace(" 00:00:00", "").replace(" 12:00:00 AM", "");
    }

    if (document.getElementById("Search_Tarih2") != null) {
        document.getElementById("Search_Tarih2").value = document.getElementById("Search_Tarih2").value.replace("1.01.0001", "").replace("01/01/0001", "").replace("1/1/0001", "");
        document.getElementById("Search_Tarih2").value = document.getElementById("Search_Tarih2").value.replace(" 00:00:00", "").replace(" 12:00:00 AM", "");
    }

    if (document.getElementById("Order.Operation.randevuVerilenTarih") != null) {
        document.getElementById("Order.Operation.randevuVerilenTarih").value = document.getElementById("Order.Operation.randevuVerilenTarih").value.replace("1.01.0001", "").replace("01/01/0001", "").replace("1/1/0001", "");
        document.getElementById("Order.Operation.randevuVerilenTarih").value = document.getElementById("Order.Operation.randevuVerilenTarih").value.replace(" 00:00:00", "").replace(" 12:00:00 AM", "");
    }

    if (document.getElementById("Order.Operation.mudahaleTarihi") != null) {
        document.getElementById("Order.Operation.mudahaleTarihi").value = document.getElementById("Order.Operation.mudahaleTarihi").value.replace("1.01.0001", "").replace("01/01/0001", "").replace("1/1/0001", "");
        document.getElementById("Order.Operation.mudahaleTarihi").value = document.getElementById("Order.Operation.mudahaleTarihi").value.replace(" 00:00:00", "").replace(" 12:00:00 AM", "");
    }

    if (document.getElementById("Order.Operation.randevuMudahaleTarih") != null) {
        document.getElementById("Order.Operation.randevuMudahaleTarih").value = document.getElementById("Order.Operation.randevuMudahaleTarih").value.replace("1.01.0001", "").replace("01/01/0001", "").replace("1/1/0001", "");
        document.getElementById("Order.Operation.randevuMudahaleTarih").value = document.getElementById("Order.Operation.randevuMudahaleTarih").value.replace(" 00:00:00", "").replace(" 12:00:00 AM", "");
    }
});






