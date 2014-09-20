﻿using LongAn.DVC.Common;
using LongAn.DVC.Helpers;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
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
            FormField1.ControlMode = SPControlMode.Display;
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
                (currentStatus == (int)TrangThaiXuLy.KhoiTao || 
                currentStatus == (int)TrangThaiXuLy.HoSoChoBoSung ||
                currentStatus == (int)TrangThaiXuLy.HoSoBiTuChoi))
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
            UpdateItem(TrangThaiXuLy.DaTiepNhan);
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
            var isDlg = Request.QueryString["IsDlg"];
            if (isDlg != null && isDlg.ToString() == "1")
                //Close popup
                DeNghiHelper.ClosePopup(this.Page);
            else
                longOperation.End(string.Empty);
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

        void UpdateItem(TrangThaiXuLy trangThai)
        {
            if (!this.Page.IsValid)
                return;

            var longOperation = new SPLongOperation(this.Page);
            longOperation.LeadingHTML = "Please wait while the operation is running";
            longOperation.TrailingHTML = "Once the operation is finished you will be redirected to result page";
            longOperation.Begin();
            //Save file upload
            SPContext.Current.ListItem[Constants.FieldTrangThai] = (int)trangThai;
            SaveButton.SaveItem(SPContext.Current, false, "Updated by " + SPContext.Current.Web.CurrentUser.LoginName);
            //Redirect to page
            var isDlg = Request.QueryString["IsDlg"];
            if (isDlg != null && isDlg.ToString() == "1")
                //Close popup
                DeNghiHelper.ClosePopup(this.Page);
            else
                longOperation.End(string.Empty);
        }
    }
}
