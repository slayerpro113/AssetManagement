

var categoryName;
function OpenAssignPopup(poRequestId, employeeId, category, employeeName) {
    $('#poRequestId').val(poRequestId);
    $('#employeeId').val(employeeId);
    $('#employeeName').val(employeeName);

    categoryName = category;
    CallbackPanel.PerformCallback();
}

$(document).ready(function() {
    $("#btnAssign").click(function() {
        doAssignAsset();
    });
});

function doAssignAsset() {
    var assetId = AssetCbb.GetValue();

    if (assetId === null) {
        $("#assetMessage").html("Please, select an asset to assign!");
        timeoutAsset();
    }
    else {
        $.ajax({
            type: 'Post',
            url: "/Staff/AssignAsset?poRequestId=" + $("#poRequestId").val() + "&employeeId=" + $("#employeeId").val() + "&assetId=" + assetId + "&assignRemark=" + $("#assignRemark").val() + "&StaffAssign=" + $("#StaffAssign").val(),
            dataType: 'json',
            success: function (data) {
                if (data.status === "Success") {
                    swal({
                        title: "Assign Asset",
                        text: "Successfully",
                        icon: "success",
                        buttons: false,
                        timer: 1300
                    });
                    $('#assignPopup').modal('hide');
                    gvFilterRow.Refresh();
                } else {
                    swal({
                        title: "Something Went Wrong",
                        text: "Please try again!",
                        icon: "error",
                        buttons: false,
                        timer: 1300
                    });
                }
            }
        });
    }
}

function timeoutAsset() {
    setTimeout(function () {
        $("#assetMessage").empty();
    }, 1300);
}


//----------------------Quote popup

function OpenQuotePopup(poRequestId) {
    $("#poRequestId2").val(poRequestId);
    $("#formQuote")[0].reset();
}

$(document).ready(function() {
    $("#btnSubmit").click(function() {
        doEnterQuote();
    });
});

function doEnterQuote() {
    var image = $('#image').val();
    var productName = $('#productName').val();
    var brand = $('#brand').val();
    var price = $('#price').val();
    var vendor = $('#vendor').val();
    var warranty = $('#warranty').val();
    var note = $('#note').val();
    var poRequestId = $('#poRequestId2').val();

    if (image.length === 0 || productName.length === 0  || vendor === "Choose vendor" || price.length === 0) {
        $("#quoteMessage").html("Please, fill all required fields!");
        timeout();
    }
    else {
        var formData = new FormData();
        formData.append('image', $('input[type=file]')[0].files[0]);
        formData.append('productName', productName);
        formData.append('brand', brand);
        formData.append('vendor', vendor);
        formData.append('price', price);
        formData.append('warranty', warranty);
        formData.append('note', note);
        formData.append('poRequestId', poRequestId);

        $.ajax({
            type: 'Post',
            enctype: 'multipart/form-data',
            url: "/Staff/HandleQuote/",
            data: formData,
            dataType: 'json',
            processData: false,
            contentType: false,

            success: function (data) {
                if (data.status === "Success") {
                    swal({
                        title: "Assign Asset",
                        text: "Successfully",
                        icon: "success",
                        buttons: false,
                        timer: 1300
                    });
                    $('#quotePopup').modal('hide');
                    gvFilterRow.Refresh();
                } else {
                    swal({
                        title: "Something Went Wrong",
                        text: "Please try again!",
                        icon: "error",
                        buttons: false,
                        timer: 1300
                    });
                }
            }
        });
    }
}


function timeout() {
    setTimeout(function () {
        $("#quoteMessage").empty();
    }, 1300);
}


//---------------------- Submit request
function SubmitRequest(poRequestId, staffSubmit) {
    swal({
        title: "Are you sure?",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then(function(isConfirm) {
        if (isConfirm) {
            $.ajax({
                type: 'Post',
                url: "/Staff/SubmitRequest?poRequestId=" + poRequestId + "&staffSubmit=" + staffSubmit,
                dataType: 'json',
                success: function(data) {
                    if (data.status === "Success") {
                        swal({
                            title: "Successfully",
                            text: "The request has been sent",
                            icon: "success",
                            buttons: false,
                            timer: 1300
                        });
                        gvFilterRow.Refresh();
                    } else {
                        swal({
                            title: "Something Went Wrong",
                            text: "Please try again!",
                            icon: "error",
                            buttons: false,
                            timer: 1300
                        });
                    }
                }
            });
        }
        //cancel
        else {
            swal.close();
        }
    });
}

//------------------Create Order
function createOrder() {
    var poRequestIdString = $('#requestId').val();

    $.ajax({
        type: 'Post',
        url: "/Staff/CreateOrder?poRequestIdString=" + poRequestIdString,
        dataType: 'json',
        success: function (data) {
            if (data.status === "Success") {
                swal({
                    title: "Create Order",
                    text: "Successfully",
                    icon: "success",
                    buttons: false,
                    timer: 1300
                });
                gvFilterRow.Refresh();
            } else {
                swal({
                    title: "Something Went Wrong",
                    text: "Please try again!",
                    icon: "error",
                    buttons: false,
                    timer: 1300
                });
            }
        }
    });
}