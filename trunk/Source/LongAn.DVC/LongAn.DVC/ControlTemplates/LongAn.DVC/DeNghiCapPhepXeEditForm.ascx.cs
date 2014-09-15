using LongAn.DVC.Common;
using LongAn.DVC.Helpers;
using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace LongAn.DVC.ControlTemplates.LongAn.DVC
{
    public partial class DeNghiCapPhepXeEditForm : UserControl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            btnCancel.Click += btnCancel_Click;
            btnSave.Click += btnSave_Click;
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.Page.IsValid)
                return;

            var longOperation = new SPLongOperation(this.Page);
            longOperation.LeadingHTML = "Please wait while the operation is running";
            longOperation.TrailingHTML = "Once the operation is finished you will be redirected to result page";
            longOperation.Begin();
            //Save file upload
            var itemId = SPContext.Current.ItemId;
            DeNghiHelper.SaveFileAttachment(fileUpload1, itemId, Constants.AttachmentGiayDangKy);
            DeNghiHelper.SaveFileAttachment(fileUpload2, itemId, Constants.AttachmentGiayChungNhanKiemDinh);
            DeNghiHelper.SaveFileAttachment(fileUpload3, itemId, Constants.AttachmentGiayCamKet);
            DeNghiHelper.SaveFileAttachment(fileUpload4, itemId, Constants.AttachmentCMND);
            //Redirect to page
            //longOperation.End(string.Empty);
            //Close popup
            DeNghiHelper.ClosePopup(this.Page);
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            //Close popup
            DeNghiHelper.ClosePopup(this.Page);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DeNghiHelper.LoadAttachments(SPContext.Current.ItemId, Constants.AttachmentGiayDangKy, divFileUpload1);
                DeNghiHelper.LoadAttachments(SPContext.Current.ItemId, Constants.AttachmentGiayChungNhanKiemDinh, divFileUpload2);
                DeNghiHelper.LoadAttachments(SPContext.Current.ItemId, Constants.AttachmentGiayCamKet, divFileUpload3);
                DeNghiHelper.LoadAttachments(SPContext.Current.ItemId, Constants.AttachmentCMND, divFileUpload4);
            }
        }
    }
}
