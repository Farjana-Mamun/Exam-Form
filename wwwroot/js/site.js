
$(document).ready(function () {
    
});

function addQuestion() {
    $.ajax({
        url: '/Templates/Question/AddQuestionModal',
        type: 'GET',
        success: function (data) {
            console.log(data);
            $("#addQuestion_modal_placeholder").html(data);
            $("#questionModal").modal('show');
        },
        error: function () {
            alert("An error occurred while loading the modal.");
        }
    });
}

$('#addQuestionBtn').on('click', function () {
    $("#questionModal").modal('show');
})

$('#headerCheckBox').on('change', function () {
    $('.bodyCheckBox').prop('checked', this.checked);
});

$('.bodyCheckBox').on('change', function () {
    if ($('.bodyCheckBox:checked').length === $('.bodyCheckBox').length) {
        $('#headerCheckBox').prop('checked', true);
    } else {
        $('#headerCheckBox').prop('checked', false);
    }
});

function getSelectedUserIds() {
    var selectedIds = [];
    $('.bodyCheckBox:checked').each(function () {
        selectedIds.push($(this).val());
    });
    return selectedIds;
}

$('#btnDelete').on('click', function () {
    var selectedIds = getSelectedUserIds();
    if (selectedIds.length === 0) {
        alert('No users selected!');
        return;
    }
    if (confirm('Are you sure you want to delete the selected users?')) {
        $.ajax({
            type: 'POST',
            url: '/Accounts/Administration/DeleteUsers',
            data: { userIds: selectedIds },
            success: function (response) {
                if (response.success) {
                    location.reload();
                } else {
                    alert(response.message);
                }
            }
        });
    }
});

$('#btnBlock').on('click', function () {
    var selectedIds = getSelectedUserIds();
    if (selectedIds.length === 0) {
        alert('No users selected!');
        return;
    }
    $.ajax({
        type: 'POST',
        url: '/Accounts/Administration/BlockUsers',
        data: { userIds: selectedIds },
        success: function (response) {
            if (response.success) {
                location.reload();
            } else {
                alert(response.message);
            }
        }
    });
});

$('#btnUnblock').on('click', function () {
    var selectedIds = getSelectedUserIds();
    if (selectedIds.length === 0) {
        alert('No users selected!');
        return;
    }
    $.ajax({
        type: 'POST',
        url: '/Accounts/Administration/UnblockUsers',
        data: { userIds: selectedIds },
        success: function (response) {
            if (response.success) {
                location.reload();
            } else {
                alert(response.message);
            }
        }
    });
});

$('#btnAddToAdmin').on('click', function () {
    var selectedIds = getSelectedUserIds();
    if (selectedIds.length === 0) {
        alert('No users selected!');
        return;
    }
    $.ajax({
        type: 'POST',
        url: '/Accounts/Administration/AddToAdmin',
        data: { userIds: selectedIds },
        success: function (response) {
            if (response.success) {
                location.reload();
            } else {
                alert(response.message);
            }
        }
    });
});

$('#btnRemoveFromAdmin').on('click', function () {
    var selectedIds = getSelectedUserIds();
    if (selectedIds.length === 0) {
        alert('No users selected!');
        return;
    }
    $.ajax({
        type: 'POST',
        url: '/Accounts/Administration/RemoveFromAdmin',
        data: { userIds: selectedIds },
        success: function (response) {
            if (response.success) {
                location.reload();
            } else {
                alert(response.message);
            }
        }
    });
});