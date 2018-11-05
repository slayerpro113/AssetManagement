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
        timeoutMessage();
    }
    else {
        $.ajax({
            type: 'Post',
            url: "/Asset/AssignWithoutRequest?assetId=" + $("#assetId").val() + "&employeeId=" + employeeId + "&assignRemark=" + $("#assignRemark").val() + "&staffAssign=" + $("#staffAssign").val(),
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
                    InStockGrid.Refresh();
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

function timeoutMessage() {
    setTimeout(function () {
        $("#errorMessage").empty();
    }, 1300);
}


// Enter Depreciation

function OpenDepreciatePopup(assetId, assetName) {
    $("#assetId2").val(assetId);
    $("#assetName2").val(assetName);
}

$(document).ready(function () {
    $("#btnOK").click(function () {
        doEnterDepreciation();
    });
});

function doEnterDepreciation() {
    var barcode = $('#barcodeInput').val();
    var monthsOfDepreciation = $('#depreciation').val();

    if (barcode.length === 0 || monthsOfDepreciation === 0) {
        $("#message").html("Please, fill all the required fields.");
        timeoutMessage();
    } else if (monthsOfDepreciation > 84) {
        $("#message").html("The maximum months are 84!");
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
                        title: "Enter Information",
                        text: "Successfully",
                        icon: "success",
                        buttons: false,
                        timer: 1300
                    });
                    $('#depreciatePopup').modal('hide');
                    InStockGrid.Refresh();
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

function timeoutMessage() {
    setTimeout(function () {
        $("#message").empty();
    }, 1300);
}

