    $(document).ready(function () {
        $("#btnRecall").click(function () {
            swal({
                title: "Are you sure?",
                icon: "warning",
                buttons: true
            }).then(function (isConfirm) {
                if (isConfirm) {
                    doRecallAsset();
                }
                //cancel
                else {
                    swal.close();
                    $('#recallPopup').modal('hide');

                }
            });
        });
    });

function OpenRecallPopup(assetId, barcode, assetName, employeeName) {
    $('#assetId').val(assetId);
    $('#barcode').val(barcode);
    $('#assetName').val(assetName);
    $('#employeeName').val(employeeName);
}

function doRecallAsset() {
    $.ajax({
        type: 'Post',
        url: "/Asset/RecallAsset?assetId=" + $("#assetId").val() + "&recallRemark=" + $("#recallRemark").val() + "&staffRecall=" + $("#staffRecall").val(),
        dataType: 'json',
        success: function (data) {
            if (data.status === "Success") {
                swal({
                    title: "Recall Asset",
                    text: "Successfully",
                    icon: "success",
                    buttons: false,
                    timer: 1300
                });
                $('#recallPopup').modal('hide');
                InUseGrid.Refresh();
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