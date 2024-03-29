﻿/**
 * Created by 
 */

$(function () {
    $(".row_45 nobr").each(function () {	
        $(this).parent().html($(this).text());
    });

    if ($("[id*='hdfHiddenFag']").val() != "1")
    {
        $(".checkbox_upload").prop('disabled', true);
    }
});

//Check upload file: 0-newform, 1-editform
function PreSaveAction(formType)
{
    $(".ms-formvalidation").remove();
    var result = true;
    //Field
    $(".field-required-data").each(function () {
        var input = $(this).find("input");
        if (input.val() == "") {
            var parentTag = $(this).parent(".row_validate");
            var titleText = parentTag.find(".validate nobr").text();
            var errorHtml = '<span class="ms-formvalidation"><span role="alert">' + titleText + ' bắt buộc nhập!<br></span></span>';
            input.after(errorHtml);
            result = false;
        }
    });

    if (formType == 0)
    {
        //File upload
        $("[id*='fileUpload']").each(function () {
            var parentTag = $(this).parents(".row_validate");
            var checkBoxValue = parentTag.find(".checkbox_upload").prop("checked");
            if ($(this).val() == "" && checkBoxValue == false) {
                var errorHtml = '<span class="ms-formvalidation"><span role="alert">Chưa chọn tập tin đính kèm<br></span></span>';
                $(this).after(errorHtml);
                result = false;
            }
        });
    }
    return result;
}

function PreSaveYeuCauBoSung()
{
    if($.trim($("[id$='_txtTieuDe']").val()) != "" || $.trim($("[id$='_txtTieuDe']").val()) != "")
        return true;
    return false;
}

function PreSaveItem(formType) {
    if ("function" == typeof (PreSaveAction)) {
        return PreSaveAction(formType);
    }
    return true;
}

function getFieldControl(fieldName, fieldType) {
    var control = $('[id^=' + fieldName + '_][id$=' + fieldType + 'Field]');
    return control;
}
//ex: getFieldControl('KinhGui', 'TextBox');

//******** Dialog With Call Back Starts Here ***********/
function openDialogWithCallBack(tUrl, tTitle) {
    var options = {
        url: tUrl,
        title: tTitle,
        dialogReturnValueCallback: onPopUpCloseCallBack
    };
    SP.UI.ModalDialog.showModalDialog(options);
}

function onPopUpCloseCallBack(result, returnValue) {
    if (result == SP.UI.DialogResult.OK) {
        /* Notify User */
        SP.UI.Status.removeAllStatus(true);
        var sId = SP.UI.Status.addStatus("You have clicked OK !!!");
        SP.UI.Status.setStatusPriColor(sId, 'green');


        //SP.UI.ModalDialog.RefreshPage(result);
        // window.location.href = 'http://www.ashokraja.me';//[url of a page to which the user has to be redirected];
        //window.location.reload(); // forces a redolad of page
    } else if (result == SP.UI.DialogResult.cancel) {
        SP.UI.Status.removeAllStatus(true);
        var sId = SP.UI.Status.addStatus("You have cancelled the Operation !!!");
        SP.UI.Status.setStatusPriColor(sId, 'yellow');
    }
}
//******** Dialog With Call Back Ends Here ***********/

function closePopupAndPassData() {
    var popData = [];
    popData[0] = "post";
    popData[1] = "data";
    SP.UI.ModalDialog.commonModalDialogClose(SP.UI.DialogResult.OK, popData);
}