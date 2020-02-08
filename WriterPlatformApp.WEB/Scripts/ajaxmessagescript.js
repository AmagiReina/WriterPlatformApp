$(document).ready(function () {
    getAllMessages();

    $(".add-comment").on("click", function (e) {
        e.preventDefault();
        AddMessage();
    });

});

function getAllMessages() {
    let id = $('#Id').val();
    console.log(id);
    $.ajax({
        type: "POST",
        datatype: "json",
        url: "/Message/Index/",
        data: {
            id: id
        },
        success: function (data) {
            $("#commentSection").html(data);
        },
        error: function () {
            alert("failed");
        }
    });
}

function AddMessage() {
    var message = {
        MessageText: $("#message-text").val(),
        MessageDate: moment(),
        TitleId: $('#Id').val()
    };
    $.ajax({
        type: "POST",
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(message),
        url: "/Message/Add",
        success: function (result) {
            console.log(message.TitleId);
            getAllMessages();
        },
        error: function () {
            alert("failed");
        }
    })
}





