using LongAn.DVC.Common;
using LongAn.DVC.Helpers;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace LongAn.DVC.ControlTemplates.LongAn.DVC
{
    public partial class DeNghiCapPhepXeEditForm : UserControl
    {
        protected override void OnInit(EventArgs e)
        {
            btnCancel.Click += btnCancel_Click;
            //
            var currentStatus = int.Parse(SPContext.Current.ListItem[Constants.FieldTrangThai].ToString());
            var currentUserRole = CapXuLy.CaNhanToChuc;
            if (ViewState[Constants.ConfViewStateCapXuLy] == null)
            {
                currentUserRole = DeNghiHelper.CurrentUserRole(SPContext.Current.Web, SPContext.Current.Web.CurrentUser);
                ViewState[Constants.ConfViewStateCapXuLy] = currentUserRole;
            }
            else
                currentUserRole = (CapXuLy)ViewState[Constants.ConfViewStateCapXuLy];

            if (currentUserRole == CapXuLy.CaNhanToChuc && 
                (currentStatus == (int)TrangThaiHoSo.KhoiTao ||
                currentStatus == (int)TrangThaiHoSo.ChoBoSung))
            {
                btnSave.Visible = true;
                btnGuiHoSo.Visible = true;
                btnSave.Click += btnSave_Click;
                btnGuiHoSo.Click += btnGuiHoSo_Click;
            }
            base.OnInit(e);
        }

        void btnGuiHoSo_Click(object sender, EventArgs e)
        {
            UpdateItem(TrangThaiHoSo.ChoTiepNhan);
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.Page.IsValid)
                return;

            var longOperation = new SPLongOperation(this.Page);
            longOperation.LeadingHTML = "Please wait while the operation is running";
            longOperation.TrailingHTML = "Once the operation is finished you will be redirected to result page";
            longOperation.Begin();
            SaveButton.SaveItem(SPContext.Current, false, "Updated by " + SPContext.Current.Web.CurrentUser.LoginName);
            //Save file upload
            var itemId = SPContext.Current.ItemId;
            DeNghiHelper.SaveFileAttachment(fileUpload1, itemId, Constants.AttachmentGiayDangKy);
            DeNghiHelper.SaveFileAttachment(fileUpload2, itemId, Constants.AttachmentGiayChungNhanKiemDinh);
            DeNghiHelper.SaveFileAttachment(fileUpload3, itemId, Constants.AttachmentGiayCamKet);
            DeNghiHelper.SaveFileAttachment(fileUpload4, itemId, Constants.AttachmentCMND);
            //Redirect to page
            var redirectUrl = Request.QueryString["Source"];
            if (redirectUrl == null || string.IsNullOrEmpty(redirectUrl.ToString()))
                redirectUrl = "/";
            longOperation.End(redirectUrl, Microsoft.SharePoint.Utilities.SPRedirectFlags.DoNotEndResponse, HttpContext.Current, "");
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            var redirectUrl = Request.QueryString["Source"];
            if (redirectUrl == null || string.IsNullOrEmpty(redirectUrl.ToString()))
                redirectUrl = "/";
            Response.Redirect(redirectUrl);
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

        void UpdateItem(TrangThaiHoSo trangThai)
        {
            if (!this.Page.IsValid)
                return;

            var longOperation = new SPLongOperation(this.Page);
            longOperation.LeadingHTML = "Please wait while the operation is running";
            longOperation.TrailingHTML = "Once the operation is finished you will be redirected to result page";
            longOperation.Begin();
            SPContext.Current.ListItem[Constants.FieldTrangThai] = (int)trangThai;
            SPContext.Current.ListItem[Constants.FieldCapDuyet] = (int)CapXuLy.MotCua;
            SaveButton.SaveItem(SPContext.Current, false, "Updated by " + SPContext.Current.Web.CurrentUser.LoginName);
            //Save file upload
            var itemId = SPContext.Current.ItemId;
            DeNghiHelper.SaveFileAttachment(fileUpload1, itemId, Constants.AttachmentGiayDangKy);
            DeNghiHelper.SaveFileAttachment(fileUpload2, itemId, Constants.AttachmentGiayChungNhanKiemDinh);
            DeNghiHelper.SaveFileAttachment(fileUpload3, itemId, Constants.AttachmentGiayCamKet);
            DeNghiHelper.SaveFileAttachment(fileUpload4, itemId, Constants.AttachmentCMND);
            DeNghiHelper.AddDeNghiHistory(SPContext.Current.Web, CapXuLy.MotCua, itemId, HanhDong.NopHoSo.ToString(), string.Empty);
            //Redirect to page
            var redirectUrl = Request.QueryString["Source"];
            if (redirectUrl == null || string.IsNullOrEmpty(redirectUrl.ToString()))
                redirectUrl = "/";
            longOperation.End(redirectUrl, Microsoft.SharePoint.Utilities.SPRedirectFlags.DoNotEndResponse, HttpContext.Current, "");
        }
    }
}
