
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