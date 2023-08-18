// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$('#btnsearch').on('click', function () {
    alert($('#searchintput').val());
    $.ajax({
        url: "/Home/index",
        type: "get",
        data: {
            Search: $('#searchintput').val(), page: "1"
        },
        
        success: function () {
            window.location.href = "/Home/search/" + $('#searchintput').val() + "?"
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    })
})
