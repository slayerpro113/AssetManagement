

//-----------------------Assign

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
            url: "/Staff/AssignAsset?poRequestId=" + $("#poRequestId").val() + "&employeeId=" + $("#employeeId").val() + "&assetId=" + assetId + "&staffAssignId=" + $("#staffAssignId").val(),
            dataType: 'json',
            success: function (data) {
                if (data.status === "Success") {
                    swal({
                        title: "Successfully",
                        text: "The asset has been assigned",
                        icon: "success",
                        buttons: false,
                        timer: 1500
                    });
                    $('#assignPopup').modal('hide');
                    gvRowSelection.Refresh();
                } else {
                    swal({
                        title: "Something Went Wrong",
                        text: "Please try again!",
                        icon: "error",
                        buttons: false,
                        timer: 1500
                    });
                }
            }
        });
    }
}

function timeoutAsset() {
    setTimeout(function () {
        $("#assetMessage").empty();
    }, 1500);
}


//----------------------Quote popup

function OpenQuotePopup(poRequestId, category) {
    $("#formQuote")[0].reset();
    $("#poRequestId2").val(poRequestId);
    $("#category").val(category);
}

$(document).ready(function() {
    $("#btnSubmit").click(function() {
        doEnterQuote();
    });
});

function doEnterQuote() {
    var productName = $('#productName').val();
    var brand = $('#brand').val();
    var price = $('#price').val();
    var vendor = $('#vendor').val();
    var warranty = $('#warranty').val();
    var poRequestId = $('#poRequestId2').val();

    if (productName.trim().length === 0 || brand.trim().length === 0 || vendor === "Choose vendor" || price.length === 0) {
        $("#quoteMessage").html("Please, fill all required fields!");
        timeout();
    }
    else {
        var formData = new FormData();
        formData.append('productName', productName.trim());
        formData.append('brand', brand.trim());
        formData.append('vendor', vendor);
        formData.append('price', price);
        formData.append('warranty', warranty);
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
                        title: "Successfully",
                        text: "The new quote has been created",
                        icon: "success",
                        buttons: false,
                        timer: 1500
                    });
                    $('#quotePopup').modal('hide');
                    gvRowSelection.Refresh();
                } else {
                    swal({
                        title: "Something Went Wrong",
                        text: "Please try again!",
                        icon: "error",
                        buttons: false,
                        timer: 1500
                    });
                }
            }
        });
    }
}


function timeout() {
    setTimeout(function () {
        $("#quoteMessage").empty();
    }, 1500);
}


//---------------------- Submit request
function SubmitRequest(poRequestId, staffSubmitId) {
    swal({
        title: "Are you sure?",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then(function(isConfirm) {
        if (isConfirm) {
            $.ajax({
                type: 'Post',
                url: "/Staff/SubmitRequest?poRequestId=" + poRequestId + "&staffSubmitId=" + staffSubmitId,
                dataType: 'json',
                success: function(data) {
                    if (data.status === "Success") {
                        swal({
                            title: "Successfully",
                            text: "The request has been sent",
                            icon: "success",
                            buttons: false,
                            timer: 1500
                        });
                        gvRowSelection.Refresh();
                    } else {
                        swal({
                            title: "Something Went Wrong",
                            text: "Please try again!",
                            icon: "error",
                            buttons: false,
                            timer: 1500
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

    if (poRequestIdString.length === 0) {
        $("#orderMessage").html("Please select at least one order!");
        timeout();
    } else {
        swal({
            title: "Are you sure?",
            icon: "warning",
            buttons: true,
            dangerMode: true
        }).then(function (isConfirm) {
            if (isConfirm) {
                $.ajax({
                    type: 'Post',
                    url: "/Staff/CreateOrder?poRequestIdString=" + poRequestIdString + "&staffCreateId=" + $('#staffCreateId').val() ,
                    dataType: 'json',
                    success: function (data) {
                        if (data.status === "Success") {
                            swal({
                                title: "Successfully",
                                text: "The order has been created",
                                icon: "success",
                                buttons: false,
                                timer: 1500
                            });
                            $("#count").html("0");
                            gvRowSelection.Refresh();
                        } else {
                            swal({
                                title: "Something Went Wrong",
                                text: "Please try again!",
                                icon: "error",
                                buttons: false,
                                timer: 1500
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

    function timeout() {
        setTimeout(function () {
            $("#orderMessage").empty();
        }, 1500);
    }
}



//-----------------Autocomplete

