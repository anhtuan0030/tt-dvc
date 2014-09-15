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
            var errorHtml = '<span class="ms-formvalidation"><span role="alert">Trường không được để trống<br></span></span>';
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

function PreSaveItem() {
    if ("function" == typeof (PreSaveAction)) {
        return PreSaveAction();
    }
    return true;
}

function getFieldControl(fieldName, fieldType) {
    var control = $('[id^=' + fieldName + '_][id$=' + fieldType + 'Field]');
    return control;
}