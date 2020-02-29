$(document).ready(function () {  
    SortDropdownActivate();
    LoadDefault("start");
    StartSearchEngine();
    StartSortEngine();
    SortDropdownHide();
});

function LoadDefault(type) {
    const startPage = 1;
    if (type == "start")
    {
        setTimeout(GetAllTitles(startPage), 5);
    }
    else
    {
        if ($('.card-deck').children().length == 0) {
            SetAlert("not_found");
            setTimeout(GetAllTitles(startPage), 200000);
        };
    }
}
function SortDropdownActivate() {
    $("#sort-button").on("click", function () {
        $('#menu-sort').toggle();
    });

    $("#sort-button").on("mouseenter", function () {
        $('#menu-sort').toggle();
    });
}

function SortDropdownHide() {
    $("#sort-button").on("mouseleave", function () {
        $('#menu-sort').hide();
    });
}

function StartSearchEngine() {
    // Start variables
    let selectedSearchOption = null;
    let searchText = null;
    const startPage = 1;

    $('#search_param').on('change', function (e) {
        e.preventDefault();
        selectedSearchOption
            = $('#search_param').val();
    });
    console.log(selectedSearchOption);
    $("#search_submit").on("click", function (e) {
        e.preventDefault();

        selectedSearchOption = $('#search_param').val();
        searchText = $('#search_text').val();

        console.log(selectedSearchOption);
        console.log(searchText);
        if (searchText != null) {
            if (selectedSearchOption == "author") {
                SearchByAuthor(startPage);
            }
            else if (selectedSearchOption == "title") {
                SearchByTitleName(startPage);
            }
            else if (selectedSearchOption == "genre") {
                SearchByGenre(startPage);
            }
        }
    });
       
}

function StartSortEngine() {
    console.log($('#genre-sort').html());
    const startPage = 1;
    $('#genre-sort').on("click", function (e) {
        e.preventDefault();
        console.log("click");
        SortByGenre(startPage);
        $('#menu-sort').hide();
    });
    $('#rating-sort').on("click", function (e) {
        e.preventDefault();
        SortByRating(startPage);
        $('#menu-sort').hide();
    });

    $('#comment-sort').on("click", function (e) {
        e.preventDefault(startPage);
        SortByComment();
        $('#menu-sort').hide();
    });
}

/**
 * Fetch all titles
 * */
function GetAllTitles(page) {
    $('#titles_list')
        .load('/Title/GetAllTitles', { "page": page }, function () {
            $('#page').on("click", function (ev) {
                let pageNumber = $(ev.target).html();
                console.log(pageNumber);
                GetAllTitles(pageNumber);
            });
        });
}
/**
 * Search functions
 * */
function SearchByAuthor(page) {
    var name = $("#search_text").val();

    name = encodeURIComponent(name);
    page = encodeURIComponent(page);

    $('#titles_list')
        .load('/Title/SearchByAuthor/',
            { name: name, page: page }, function () {
                $('#page').on("click", function (ev) {
                    let pageNumber = $(ev.target).html();
                    SearchByAuthor(pageNumber);
                });
                LoadDefault();
            });
}

function SearchByTitleName(page) {
    var name = $("#search_text").val();

    name = encodeURIComponent(name);
    page = encodeURIComponent(page);

    $('#titles_list')
        .load('/Title/SearchByTitleName/',
            { name: name, page: page }, function () {
                $('#page').on("click", function (ev) {
                    let pageNumber = $(ev.target).html();
                    SearchByTitleName(pageNumber);
                });
                LoadDefault();
           });
}


function SearchByGenre(page) {
    var name = $("#search_text").val();

    $('#titles_list')
        .load('/Title/SearchByGenre/',
            { name: name, "page": page }, function () {
                $('#page').on("click", function (ev) {
                    let pageNumber = $(ev.target).html();
                    SearchByGenre(pageNumber);
                });
                LoadDefault();
            }); 
}

/**
 * Sort functions
 * */
function SortByGenre(page) {
    $('#titles_list')
        .load('/Title/SortByGenre',
            { "page": page }, function () {
                $('#page').on("click", function (ev) {
                    let pageNumber = $(ev.target).html();
                    SortByGenre(pageNumber);
                });
            });      
}

function SortByRating(page) {
    $('#titles_list')
        .load('/Title/SortByRating',
            { "page": page }, function () {
                // click
                $('#page').on("click", function (ev) {
                    let pageNumber = $(ev.target).html();
                    SortByRating(pageNumber);
                });
                // end click
            }); 
}

function SortByComment(page) {
    $('#titles_list')
        .load('/Title/SortByComment',
            { "page": page }, function () {
                $('#page').on("click", function (ev) {
                    let pageNumber = $(ev.target).html();
                    SortByComment(pageNumber);
                });
            }); 
}

function SetAlert(type) {
    // Set position of alert
    alertify.set('notifier', 'position', 'bottom-left');

    // Check type of alert
    switch (type) {
        case "success":
            alertify.notify('Загружено', 'success', 2);
            break;
        case "not_found":
            alertify.notify('Не найдено', 'error', 2);
            break;
    };
}