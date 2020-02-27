$(document).ready(function () {
    $("#change-password-btn").on("click", function (e) {
        e.preventDefault();
        changePassword();
    });
});

function changePassword() {
    var data = {
        NewPassword: $("#NewPassword").val(),
        ConfirmNewPassword: $("#ConfirmNewPassword").val()
    };
        $.ajax({
            type: "POST",
            datatype: "json",
            contentType: "application/json;charset=utf-8",
            url: "/Account/ChangePassword",
            data: JSON.stringify(data),
            success: function (response) {
                if (response.NewPassword == response.ConfirmNewPassword) {

                    $("#modal-text").text("Ваш пароль успешно изменен.");
                    $("#modal-dialog").modal("toggle");


                    $("#modal-dialog").on("hidden.bs.modal", function () {
                        window.location.href = "/Account/Login";
                    });
                }
               
            },
            error: function (error) {
                console.log("error");
            }
        });
 
}