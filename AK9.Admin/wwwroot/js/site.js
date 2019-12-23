$('#confirmationModel').on('show.bs.modal', function (event) {
    let button = $(event.relatedTarget);

    let delUrl = button.data('delurl');
    let delId = button.data('delid');
    let targetDiv = button.data('targetdiv');
    let objectName = button.data('objectname');

    var modal = $(this);
    //modal.find('.modal-title').text('New message to ' + recipient);
    modal.find('.modal-body #lblMessage').html('Are you sure you want to delete the ' + objectName.toLowerCase() + '?');

    modal.find('#btnYes').unbind('click').click(function () {
        $.ajax({
            method: "GET",
            url: delUrl,
            data: { id: delId },
            success: function (result) {

                if (result.status === 'Succeed') {
                    $('#' + targetDiv).html(result.view);
                    SuccessMessage(objectName + ' deleted successfully');
                }
                else {
                    FailureMessage('Some error occured while deleting ' + objectName.toLowerCase());
                }
            },
            error: function () {
                FailureMessage('Some error occured while deleting ' + objectName.toLowerCase());
            },
            complete: function () {
                $('#confirmationModel').modal('hide');
            }
        });
    });
});

function SuccessMessage(msg) {
    toastr.success(msg);
}

function FailureMessage() {
    toastr.error(msg);
}