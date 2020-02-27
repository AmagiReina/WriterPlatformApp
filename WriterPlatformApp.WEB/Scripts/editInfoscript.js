$(document).ready(function () {
    $("#edit-info-button").on("click", function () {
        $("#modal-text").text("Ваши данные успешно изменены.");
        $("#modal-dialog").modal("toggle");


        $("#modal-dialog").on("hidden.bs.modal", function () {
            window.location.href = "/Account/Login";
        });
    });

    $("delete-acc-button").on("click", function () {
        $("#modal-text").text("Ваш аккаунт успешно удален.");
        $("#modal-dialog").modal("toggle");


        $("#modal-dialog").on("hidden.bs.modal", function () {
            window.location.href = "/Account/Login";
        });

    });
});
