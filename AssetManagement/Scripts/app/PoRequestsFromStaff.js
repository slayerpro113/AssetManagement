function ApproveQuote(poRequestId, quoteId) {
    swal({
        title: "Are you sure?",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then(function (isConfirm) {
        if (isConfirm) {
            $.ajax({
                type: 'Post',
                url: "/Manager/SelectQuote?poRequestId=" + poRequestId + "&quoteId=" + quoteId,
                dataType: 'json',
                success: function (data) {
                    if (data.status === "Success") {
                        swal({
                            title: "Successfully",
                            text: "The quote has been selected",
                            icon: "success",
                            buttons: false,
                            timer: 1500
                        });
                        masterGrid.Refresh();
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