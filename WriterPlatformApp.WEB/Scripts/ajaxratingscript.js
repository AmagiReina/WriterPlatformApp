$(document).ready(function () {
    GetRatings();   
    $(document).ajaxComplete(function (event, xhr, settings) {       
        if (settings.url == "/RatingType/Index") {
            console.log("add");
            $('#RatingSubmit').on("click", function () {
                SetRating();
            });
        }  
    });
});

function GetRatingById() {
    let id = $('#Id').val();
    $.ajax({
        type: "GET",
        url: "/Rating/Get/",
        datatype: "Json",
        data: {
            titleId: id
        },
        success: function (data) {
            console.log(data.RatingTypes.RatingNumber);
            $("#ratingSelect").val(data.RatingTypes.RatingNumber);
        }
    });
}

function GetRatings() {
    $.ajax({
        type: "GET",
        url: "/RatingType/Index",
        datatype: "Json",
        success: function (data) {
            $('#ratingSection').html(data);
        },
        error: function () {
            alert("failed");
        }
    }).done(function () {
        GetRatingById();
    });
}

function SetRating() {
    var rating = {
        RatingTypeId: $("#ratingSelect").val(),
        TitleId: $('#Id').val()
    };
    $.ajax({
        type: "POST",
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(rating),
        url: "/Rating/Add",
        success: function (result) {
            location.reload();
        },
        error: function () {
            alert("failed");
        }
    })
}