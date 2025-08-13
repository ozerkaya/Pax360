function getNotification() {
    var temp = "notification";
    $.ajax({
        type: 'POST',
        url: '/Notifications/GetNotification/',
        data: JSON.stringify(temp),
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data.length > 0) {
                document.getElementById("countNotification").innerHTML = data.length;

                var ul = document.getElementById("notificationul");
                ul.innerHTML = "";

                var ultext = "<li><h5>Notifications</h5></li>"

                for (var i = 0; i < data.length; i++) {
                    ultext = ultext + "<li>";
                    ultext = ultext + "<a href=\"/Notifications/Inbox?id=" + data[i].id + "\" class=\"user-list-item\">";
                    ultext = ultext + "<div class=\"icon bg-danger\">";
                    ultext = ultext + "<i class=\"mdi mdi-comment\"></i>";
                    ultext = ultext + "</div>";
                    ultext = ultext + "<div class=\"user-desc\">";
                    ultext = ultext + "<span class=\"name\">" + data[i].notificationtype + "</span>";
                    ultext = ultext + "<span class=\"name\">Receiver: " + data[i].agent + "</span>";
                    ultext = ultext + "<span class=\"time\">" + data[i].date + "</span>";
                    ultext = ultext + "</div></a></li>";
                }

                ultext = ultext + "<li class=\"all-msgs text-center\"><p class=\"m-0\"><a href=\"/Notifications/Inbox\">See all Notification</a></p></li>"
                ul.innerHTML = ultext;
            }
            else {
                document.getElementById("countNotification").innerHTML = "";
            }


        },
        error: function () {
        }
    });
}

setInterval(function () {
    getNotification();

}, 10000);

$(document).ready(function () {
    getNotification();
});