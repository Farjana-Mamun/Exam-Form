
var templateId = 0;

$(document).ready(function () {

});

$(function () {
    $("#questionListSortable").sortable({
        stop: function (event, ui) {
            //$Module.reorderQuestionList(this);

            //var questionList = [];
            //var questionCount = 0;
            //$(element).children(".question-item").each(function () {
            //    var id = $(this).attr('data-question-id');
            //    var question = {
            //        SurveyQuestionId: id,
            //        SurveyQuestionSetId: $(this).attr('data-question-set-id'),
            //        DisplayOrder: ++questionCount,
            //    }
            //    questionList.push(question);
            //});
            //var procedure = {
            //    ProcedureId: $("#module_setup_module_id").val(),
            //    QuestionList: questionList
            //}
            //$.blockUI({});
            //$.ajax({
            //    url: "/Administration/ModuleSurvey/ReorderSurveyQuestion",
            //    type: 'POST',
            //    dataType: "json",
            //    data: procedure,
            //    beforeSend: function () {
            //        //OpenAppProgressModal();
            //    },
            //    success: function (result) {
            //        $.unblockUI({});
            //        if (result.Success == false) {
            //            //App.ToastrNotifierError(result.error);
            //        }
            //        else {
            //            $Module.onQuestionListChanges(element);
            //        }

            //    },
            //    error: function (error) {
            //        $.unblockUI({});
            //        console.log(error);
            //    }

            //});
        }
    });
    $(".question-list").disableSelection();
});

$('#templateQuestionForm').on('submit', function (e) {
    e.preventDefault();
    var formData = new FormData($(this)[0]);
    console.log(formData);
   
    $.ajax({
        url: '/Templates/Question/AddTemplateQuestion',
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            $('#questionModal').modal('hide');
            $('#question_list_sortable_placeholder').html(response);
        },
        error: function (xhr, status, error) {
            console.error("AJAX Error:", error);
        }
    });
});

$('#templateSetupForm').on('submit', function (e) {
    e.preventDefault();
    var formData = new FormData($(this)[0]);
    var tagList = tagify.value.map(tag => tag.value).join(',');
    formData.append("Tags", tagList);

    if ($("#templateImage")[0].files[0] != null) {
        formData.append("Image", $("#templateImage")[0].files[0]);
    } else {
        formData.append("Image", $("#editTemplateImage").val());
    }

    $.ajax({
        url: '/Templates/Template/SaveTemplateSetup',
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response.message === "Success") {
                templateId = response.id;
                new bootstrap.Tab($('#template-question-tab')).show();
            }
        },
        error: function (xhr, status, error) {
            console.error("AJAX Error:", error);
        }
    });
});

function questionTypeEvent(element) {
    if (element.value == 4) {
        $.ajax({
            url: '/Templates/Question/AddQuestionCheckbox?questionType=' + element.value,
            type: 'GET',
            success: function (data) {
                $("#add_question_checkbox_modal_placeholder").html(data);
            },
            error: function (e) {
                alert("An error occurred while loading the modal." + e);
            }
        });
    }
    else {
        $("#add_question_checkbox_modal_placeholder").html("");
    }
}

$('#addQuestionBtn').on('click', function () {
    $.ajax({
        url: '/Templates/Question/AddQuestionModal',
        type: 'GET',
        success: function (data) {
            $("#addQuestion_modal_placeholder").html(data);
            $('#questionTemplateId').val(templateId);
            $("#questionModal").modal('show');
        },
        error: function (e) {
            alert("An error occurred while loading the modal." + e);
        }
    });
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