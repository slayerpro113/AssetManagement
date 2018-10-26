﻿

var categoryName;
function OpenAssignPopup(poRequestId, category, employeeName) {
    $('#poRequestId').val(poRequestId);
    $('#employeeName').val(employeeName);

    categoryName = category;
    CallbackPanel.PerformCallback();
}

$("#btnAssign").click(function () {
    doAssignAsset();
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
            url: "/Staff/AssignAsset?poRequestId=" + $("#poRequestId").val() + "&assetId=" + assetId + "&assignRemark=" + $("#assignRemark").val() + "&StaffAssign=" + $("#StaffAssign").val(),
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

$("#btnSubmit").click(function () {
    doEnterQuote();
});

function doEnterQuote() {
    var image = $('#image').val();
    var productName = $('#productName').val();
    var brand = $('#brand').val();
    var category = $('#category').val();
    var price = $('#price').val();
    var vendor = $('#vendor').val();
    var warranty = $('#warranty').val();
    var note = $('#note').val();
    var poRequestId = $('#poRequestId2').val();

    if (image.length === 0 || productName.length === 0 || category === "Choose category" || vendor === "Choose vendor" || price.length === 0) {
        $("#quoteMessage").html("Please, fill all required fields!");
        timeout();
    }
    else {
        var formData = new FormData();
        formData.append('image', $('input[type=file]')[0].files[0]);
        formData.append('productName', productName);
        formData.append('brand', brand);
        formData.append('category', category);
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
