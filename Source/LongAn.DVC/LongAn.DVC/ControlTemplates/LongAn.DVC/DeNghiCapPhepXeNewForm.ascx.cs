﻿using CamlexNET;
using LongAn.DVC.Common;
using LongAn.DVC.Helpers;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace LongAn.DVC.ControlTemplates.LongAn.DVC
{
    public partial class DeNghiCapPhepXeNewForm : UserControl
    {
        #region Properties
        int BuocDuyetID = 0;
        string CapDuyetText = string.Empty;
        int TrangThai = 0;
        int NextStep = 0;
        #endregion Properties
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            var cauHinhs = DeNghiHelper.GetCauHinh(Constants.CauHinh_Start);
            var isMember = false;
            foreach (var item in cauHinhs)
            {
                isMember = DeNghiHelper.IsCurrentUserInGroup(SPContext.Current.Web, item.SPGroup);
                if (isMember)
                {
                    hdfCauHinhID.Value = item.BuocDuyetID.ToString();
                    hdfTrangThaiID.Value = item.TrangThai.ToString();
                    hdfCapDuyetText.Value = item.CapDuyetText.ToString();
                    CapDuyetText = item.CapDuyetText;
                    TrangThai = item.TrangThai;
                    NextStep = item.NextStep;
                    BuocDuyetID = item.BuocDuyetID;
                    break;
                }
            }
            if (!isMember)
            {
                var redirectUrl = Request.QueryString["Source"];
                if (redirectUrl == null || string.IsNullOrEmpty(redirectUrl.ToString()))
                    redirectUrl = "/";
                Response.Redirect(redirectUrl);
            }
            btnSave.Click += btnSave_Click;
            btnGuiHoSo.Click += btnGuiHoSo_Click;
            btnCancel.Click += btnCancel_Click;
            //SPContext.Current.FormContext.OnSaveHandler += new EventHandler(DeNghiSaveHandler);
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            var redirectUrl = Request.QueryString["Source"];
            if (redirectUrl == null || string.IsNullOrEmpty(redirectUrl.ToString()))
                redirectUrl = "/";
            Response.Redirect(redirectUrl);
        }

        void btnGuiHoSo_Click(object sender, EventArgs e)
        {
            var cauHinh = DeNghiHelper.GetCauHinh(NextStep);
            if (cauHinh != null)
            {
                SaveItem(cauHinh.BuocDuyetID, cauHinh.TrangThai, cauHinh.CapDuyetText, cauHinh.SPGroup);
            }
        }

        protected void DeNghiSaveHandler(object sender, EventArgs e)
        {
            //Only click save button
            LoggingServices.LogMessage("Fire DeNghiSaveHandler");
            this.Page.Validate();
            if (!this.Page.IsValid)
                return;
            return;
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            SaveItem(BuocDuyetID, TrangThai, CapDuyetText, null);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        void SaveItem(int buocDuyet, int trangThai, string note, SPGroup spGroup)
        {
            if (!this.Page.IsValid)
                return;

            var longOperation = new SPLongOperation(this.Page);
            longOperation.LeadingHTML = "Please wait while the operation is running";
            longOperation.TrailingHTML = "Once the operation is finished you will be redirected to result page";
            longOperation.Begin();

            //Set default value
            string deNghiGuid = Guid.NewGuid().ToString();
            SPContext.Current.ListItem[Fields.DeNghiGUID] = deNghiGuid;
            SPContext.Current.ListItem[Fields.TrangThai] = trangThai;
            SPContext.Current.ListItem[Fields.BuocDuyet] = buocDuyet;
            SPContext.Current.ListItem[Fields.NgayNopHoSo] = DateTime.Now;
            SPContext.Current.ListItem[Fields.NguoiDeNghi] = SPContext.Current.Web.CurrentUser;
            SPContext.Current.ListItem[Fields.NguoiChoXuLy] = SPContext.Current.Web.CurrentUser;
            if (spGroup != null)
            {
                //var cauHinh = DeNghiHelper.GetCauHinh(buocDuyet);
                var nguoiChoXuLy = new SPFieldUserValueCollection();
                foreach (SPUser user in spGroup.Users)
                {
                    nguoiChoXuLy.Add(new SPFieldUserValue(SPContext.Current.Web, user.ID, user.LoginName));
                }
                SPContext.Current.ListItem[Fields.NguoiChoXuLy] = nguoiChoXuLy;
            }

            SaveButton.SaveItem(SPContext.Current, false, note);
            var deNghiList = SPContext.Current.List;
            int itemId = 0;
            SPQuery caml = Camlex.Query().Where(x => (string)x[Constants.FieldDeNghiGUID] == deNghiGuid)
                                    .OrderBy(x => new[] { x[Constants.FieldTitle] as Camlex.Asc })
                                    .ToSPQuery();
            caml.RowLimit = 1;
            do
            {
                var deNghiListItems = deNghiList.GetItems(caml);
                if (deNghiListItems != null && deNghiListItems.Count > 0)
                {
                    itemId = deNghiListItems[0].ID;
                    break;
                }
            } while (true);

            //Save file upload
            DeNghiHelper.SaveFileAttachment(fileUpload1, itemId, Constants.AttachmentGiayDangKy);
            DeNghiHelper.SaveFileAttachment(fileUpload2, itemId, Constants.AttachmentGiayChungNhanKiemDinh);
            DeNghiHelper.SaveFileAttachment(fileUpload3, itemId, Constants.AttachmentGiayCamKet);
            DeNghiHelper.SaveFileAttachment(fileUpload4, itemId, Constants.AttachmentCMND);
            //Log to history
            DeNghiHelper.AddDeNghiHistory(SPContext.Current.Web
                , itemId
                , SPContext.Current.ListItem.Title 
                , int.Parse(hdfCauHinhID.Value)
                , int.Parse(hdfTrangThaiID.Value)
                , hdfCapDuyetText.Value);
            //Redirect to page
            var redirectUrl = Request.QueryString["Source"];
            if (redirectUrl == null || string.IsNullOrEmpty(redirectUrl.ToString()))
                redirectUrl = "/";
            longOperation.End(redirectUrl, Microsoft.SharePoint.Utilities.SPRedirectFlags.DoNotEndResponse, HttpContext.Current, "");
        }
    }
}
