/**
 * Created by 
 */

//Check upload file: 0-newform, 1-editform
function PreSaveAction(formType)
{
    $(".ms-formvalidation").remove();
    var result = true;
    //Field
    $(".field-required-data").each(function () {
        var input = $(this).find("input");
        if (input.val() == "") {
            var errorHtml = '<span class="ms-formvalidation"><span role="alert">Trường bắt buộc nhập!<br></span></span>';
            input.after(errorHtml);
            result = false;
        }
    });
    if (formType == 0)
    {
        //File upload
        $("[id*='fileUpload']").each(function () {
            if ($(this).val() == "") {
                var errorHtml = '<span class="ms-formvalidation"><span role="alert">Chưa attach file đính kèm<br></span></span>';
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