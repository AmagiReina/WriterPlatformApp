$(document).ready(function () {
    $("#change-password-btn").on("click", function (e) {
        e.preventDefault();
        changePassword();
    });
});

function changePassword() {
    var data = {
        OldPassword: $("#OldPassword").val(),
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
            window.location.href = "/Account/Login";
        },
        error: function (error) {
            
        }
    });
}