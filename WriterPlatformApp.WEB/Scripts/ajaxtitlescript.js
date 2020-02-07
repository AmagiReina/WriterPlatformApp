$(document).ready(function () {
    
    let id = $('#Id').val();
    console.log(id);
    GetDetails(id);
});

function GetDetails(id) {
    $.ajax({
        type: "GET",
        datatype: "json",
        url: "/Title/GetDetails/",
        data: {
            id: id
        },
        success: function (data) {
            $("#details").html(data);
        },
        error: function () {
            alert("failed");
        }
    });
}