﻿toastr.options = {
    "closeButton": false,
    "debug": false,
    "newestOnTop": false,
    "progressBar": false,
    "positionClass": "toast-top-center",
    "preventDuplicates": true,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "3000",
    "timeOut": "2000", // show off
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}

$(document).keypress(function (e) {
    if (e.which === 13) {
        doLogin();
    }
});

$(document).ready(function () {
    $("#btnSubmit").click(function () {
        doLogin();
    });
});

function doLogin() {
    var userName = $("#userName").val();
    var password = $("#password").val();

    if (userName.length === 0) {
        toastr["warning"]("Please enter your username", "Warning:");
    } else if (password.length === 0) {
        toastr["warning"]("Please enter your password", "Warning:");
    } else {
        $.ajax({
            type: 'Post',
            url: "Login/Login?userName=" + $("#userName").val() + "&password=" + $("#password").val(),
            dataType: 'json',
            success: function (data) {

                if (data.status === "WrongUserName") {
                    toastr["error"]("Username is in correct", "Error:");
                }
                else if (data.status === "WrongPassword") {
                    toastr["error"]("Password is in correct", "Error:");
                }
                else if (data.status === "Succsess") {
                    if (data.roleName === "User") {
                        location.href = "/user/GetAssetsDetail";
                    } else if (data.roleName === "Staff") {
                        location.href = "/staff/GetPoRequestsFromUsers";
                    }
                    else if (data.roleName === "Manager") {
                        location.href = "/manager/GetPoRequestsFromStaff";
                    }
                }
                else {
                    toastr["error"]("Data input is in correct", "Error:");
                }
            }
        });
    }
}