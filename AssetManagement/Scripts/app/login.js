toastr.options = {
    "closeButton": false,
    "debug": false,
    "newestOnTop": false,
    "progressBar": false,
    "positionClass": "toast-top-center",
    "preventDuplicates": true,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "1000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}

$(document).ready(function () {
    $("#btnSubmit").click(function () {
        doLogin();
    });
});

function doLogin() {
    // show loading;
    $.ajax({
        url: 'Login/Login',
        type: 'Post',
        data: {
            UserName: $("#userName").val(),
            Password: $("#password").val()
        },

        dataType: 'json',
        success: function (data) {

            if (data.status === "WrongUserName") {
                toastr["error"]("Username is in correct", "Error:");
            }
            else if (data.status === "WrongPassword") {
                toastr["error"]("Password is in correct", "Error:");
            }
            else if (data.status === "Succsess") {
                location.href = "/Home/Index";
            }
            else {
                toastr["error"]("Data input is in correct", "Error:");
            }
        }
    });
}