    $(document).ready(function () {
        $("#btnRecall").click(function () {
            swal({
                title: "Are you sure?",
                icon: "warning",
                buttons: true,
                dangerMode: true
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
        url: "/Asset/RecallAsset?assetId=" + $("#assetId").val() + "&staffRecallId=" + $("#staffRecallId").val(),
        dataType: 'json',
        success: function (data) {
            if (data.status === "Success") {
                swal({
                    title: "Successfully ",
                    text: "The asset has been recalled",
                    icon: "success",
                    buttons: false,
                    timer: 1500
                });
                $('#recallPopup').modal('hide');
                InUseGrid.Refresh();
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