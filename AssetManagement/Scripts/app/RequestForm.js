﻿
$(document).ready(function () {
    $("#btnSubmit").click(function () {
        doAssetRequest();
    });

    $("#btnReset").click(function () {
        $("#formRequest")[0].reset();
    });
});

function doAssetRequest() {
    var device = $("#device").val();
    var description = $("#description").val();

    if (device === "Choose option" || description.length === 0 || description.trim().length === 0) {
        $("#errorMessage").html("Please fill all the required fields !");
        timeout();
    }
    else {
        $.ajax({
            type: 'Post',
            url: "/User/HandlePoRequest?employeeId=" + $("#employeeId").val() + "&description=" + description.trim() + "&device=" + device,
            dataType: 'json',
            success: function (data) {
                if (data.status === "Success") {
                    swal({
                        title: "Successfully",
                        text: "Your request has Been sent",
                        icon: "success",
                        buttons: false,
                        timer: 1500
                    });
                    $("#formRequest")[0].reset();
                    location.href = "/user/RequestsHistory";
                }
                else if (data.status === "Existed") {
                    swal({
                        title: "Your Request For This Device Has Been Sent before",
                        text: "Please wait for we handle",
                        icon: "warning",
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'Close'
                    }).then(function () {
                        swal.close();
                        $("#formRequest")[0].reset();
                    });
                }
                else {
                    swal({
                        title: "Something Went Wrong",
                        text: "Please try again!",
                        icon: "error",
                        buttons: false,
                        timer: 1500
                    });
                    $("#formRequest")[0].reset();
                }
            }
        });
    }
}

function timeout() {
    setTimeout(function () {
        $("#errorMessage").empty();
    }, 1500);
}