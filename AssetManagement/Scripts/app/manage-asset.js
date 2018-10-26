function OpenAssignPopup(assetId, barcode, assetName) {
    $('#assetId2').val(assetId);
    $('#barcode2').val(barcode);
    $('#assetName2').val(assetName);
    AssignPopup.Show();
}

$(document).ready(function () {
    $("#btnAssign").click(function () {
        doAssignAsset();
    });

    $("#btnCancel2").click(function () {
        AssignPopup.Hide();
    });
});

function doAssignAsset() {
    var employeeId = ComboBox.GetValue();

    if (employeeId === null) {
        $("#message2").html("Please, Select an employee to assign!");
    }
    else {
        $.ajax({
            type: 'Post',
            url: "/Asset/AssignAsset?assetId=" + $("#assetId2").val() + "&employeeId=" + employeeId + "&assignRemark=" + $("#assignRemark").val() + "&StaffAssign=" + $("#staffRecall").val(),
            dataType: 'json',
            success: function (data) {
                if (data.status === "Success") {
                    AssignPopup.Hide();
                    gvFocusedRow.Refresh();
                } else {
                    alert('Please, Try again');
                }
            }
        });
    }
}

//handle recall pop up
$(document).ready(function () {
    $("#btnRecall").click(function () {
        doRecallAsset();
    });
    $("#btnCancel1").click(function () {
        RecallPopup.Hide();
    });
});

function OpenRecallPopup(assetId, barcode, assetName, employeeName) {
    $('#assetId').val(assetId);
    $('#barcode').val(barcode);
    $('#assetName').val(assetName);
    $('#employeeName').val(employeeName);
    RecallPopup.Show();
}

function doRecallAsset() {
    $.ajax({
        type: 'Post',
        url: "/Asset/RecallAsset?assetId=" + $("#assetId").val() + "&recallRemark=" + $("#recallRemark").val() + "&staffRecall=" + $("#staffRecall").val(),
        dataType: 'json',
        success: function (data) {
            if (data.status === "Success") {
                RecallPopup.Hide();
                gvFocusedRow.Refresh();
            } else {
                alert('Please, Try again');
            }
        }
    });
}
