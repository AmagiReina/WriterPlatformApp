$(document).ready(function () {
    GetGenres();
});

function GetGenres() {
    $.ajax({
        type: "GET",
        url: "/Title/GetGenres",
        datatype: "Json",
        success: function (data) {
            $.each(data, function (index, value) {
                $("#GenreId")
                    .append('<option value="' + value.Id + '">' + value.GenreName + '</option>');
            })
        }
    })
}