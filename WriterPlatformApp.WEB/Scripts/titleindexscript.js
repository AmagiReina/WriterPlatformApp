$(document).ready(function () {
    HideAlert();
    SortDropdownActivate();

    if ($('#titles_list').children().length == 0) {
        GetAllTitles();
    }     
    StartSearchEngine();
    StartSortEngine();
});

function SortDropdownActivate() {
    $("#sort-button").on("click", function () {
        $('#menu-sort').toggle();
    });
}

function ShowAlert() {
    $('#alert-div').addClass("alert alert-dismissible alert-danger");

    $('#alert-div')
        .html("<p class='text-center'>test</p>");
    $('#alert-div').show();
}

function HideAlert() {
    $('#alert-div').hide();
}

function StartSearchEngine() {
    let selectedSearchOption
        = null;
    let searchText
        = $('#search_text').val();
    $('#search_param').on('change', function () {
        selectedSearchOption
            = $('#search_param').val();

        $("#search_submit").click(function (e) {
            console.log(selectedSearchOption);
            e.preventDefault();

            if (searchText != null) {
                if (selectedSearchOption == "author") {
                    SearchByAuthor();
                }
                else if (selectedSearchOption == "title") {
                    SearchByTitleName();
                }
                else if (selectedSearchOption == "genre") {
                    SearchByGenre();
                }
            }
        });
    });
}

function StartSortEngine() {
    console.log($('#genre-sort').html());
    $('#genre-sort').on("click", function (e) {
        e.preventDefault();
        console.log("click");
        SortByGenre();
    });
    $('#rating-sort').on("click", function (e) {
        e.preventDefault();
        SortByRating();
    });

    $('#comment-sort').on("click", function (e) {
        e.preventDefault();
        SortByComment();
    });
}

/**
 * Fetch all titles
 * */
function GetAllTitles() {
    $('#titles_list')
        .load('/Title/GetAllTitles'); 
}
/**
 * Search functions
 * */
function SearchByAuthor() {
    var name = $("#search_text").val();
    console.log(name);

    name = encodeURIComponent(name);

    $('#titles_list')
            .load('/Title/SearchByAuthor/',
                {name : name} );
    
}

function SearchByTitleName() {
    var name = $("#search_text").val();

    name = encodeURIComponent(name);
    console.log(name);

    $('#titles_list')
        .load('/Title/SearchByTitleName/',
            { name: name });
}


function SearchByGenre() {
    var name = $("#search_text").val();

    $('#titles_list')
        .load('/Title/SearchByGenre/',
            { name: name }); 
}

/**
 * Sort functions
 * */
function SortByGenre() {
    $('#titles_list')
        .load('/Title/SortByGenre');
}

function SortByRating() {
    $('#titles_list')
        .load('/Title/SortByRating');
}

function SortByComment() {
    $('#titles_list')
        .load('/Title/SortByComment');
}