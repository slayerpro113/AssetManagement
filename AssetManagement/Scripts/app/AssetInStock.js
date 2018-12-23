

//---------------------------Assign

function OpenAssignPopup(assetId, barcode, assetName) {
    $('#assetId').val(assetId);
    $('#barcode').val(barcode);
    $('#assetName').val(assetName);
}

$(document).ready(function () {
    $("#btnAssign").click(function () {
        doAssignAsset();
    });
});

function doAssignAsset() {
    var employeeId = ComboBox.GetValue();

    if (employeeId === null) {
        $("#errorMessage").html("Please, Select an employee to assign!");
        timeout();
    }
    else {
        $.ajax({
            type: 'Post',
            url: "/Asset/AssignWithoutRequest?assetId=" + $("#assetId").val() + "&employeeId=" + employeeId + "&staffAssignId=" + $("#staffAssignId").val(),
            dataType: 'json',
            success: function (data) {
                if (data.status === "Success") {
                    swal({
                        title: "Successfully",
                        text: "The asset has been assigned.",
                        icon: "success",
                        buttons: false,
                        timer: 1500
                    });
                    $('#assignPopup').modal('hide');
                    InStockGrid.Refresh();
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
        $("#errorMessage").empty();
    }, 1500);
}


//------------------------- Enter Depreciation

function OpenDepreciatePopup(assetId, assetName) {
    $("#assetId2").val(assetId);
    $("#assetName2").val(assetName);
}

$(document).ready(function () {
    $("#btnOK").click(function () {
        doEnterDepreciation();
    });

    $("#btnCancel2").click(function () {
        $("#depreciateForm")[0].reset();
        $("#imgBarcode").attr("src", "");
    });
});

function doEnterDepreciation() {
    var barcode = $('#barcodeInput').val();
    var monthsOfDepreciation = $('#depreciation').val();

    if (barcode.length === 0 || monthsOfDepreciation.length === 0) {
        $("#message").html("Please, fill all the required fields.");
        timeoutMessage();
    } else if (monthsOfDepreciation > 120) {
        $("#message").html("The maximum months are 120!");
        timeoutMessage();
    }
    else {
        $.ajax({
            type: 'Post',
            url: "/Asset/HandleEnterDetail?assetId=" + $("#assetId2").val() + "&barcode=" + barcode + "&monthsOfDepreciation=" + monthsOfDepreciation,
            dataType: 'json',
            success: function (data) {
                if (data.status === "Success") {
                    swal({
                        title: "Successfully",
                        text: "Information has been saved",
                        icon: "success",
                        buttons: false,
                        timer: 1500
                    });
                    $('#depreciatePopup').modal('hide');
                    $("#depreciateForm")[0].reset();
                    InStockGrid.Refresh();
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

function timeoutMessage() {
    setTimeout(function () {
        $("#message").empty();
    }, 1500);
}

//-----------Check maxlength
function maxLengthCheck(object) {
    if (object.value.length > object.maxLength)
        object.value = object.value.slice(0, object.maxLength);
}