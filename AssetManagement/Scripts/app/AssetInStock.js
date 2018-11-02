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
            url: "/Asset/AssignAsset?assetId=" + $("#assetId").val() + "&employeeId=" + employeeId + "&assignRemark=" + $("#assignRemark").val() + "&staffAssign=" + $("#staffAssign").val(),
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
