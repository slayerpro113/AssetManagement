$(document).ready(function () {
    $("#chair").click(function () {
        getProducts();
    });
});

function getProducts() {
    // show loading;
    $.ajax({
        url: 'Category/Index?categoryId=1',
        data: {

        },

        dataType: 'json',
        success: function (data) {


        }
    });
}