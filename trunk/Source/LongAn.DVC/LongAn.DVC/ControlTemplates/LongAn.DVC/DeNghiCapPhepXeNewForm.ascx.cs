using CamlexNET;
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
        public List<CauHinh> CauHinh
        {
            get
            {
                if (ViewState["CauHinh"] != null)
                {
                    return (List<CauHinh>)ViewState["CauHinh"];
                }
                else
                {
                    var deNghis = DeNghiHelper.GetCauHinh("Start");
                    ViewState["CauHinh"] = deNghis;
                    return deNghis;
                }
            }
        }
        string CapDuyetText = string.Empty;
        int TrangThai = 0;
        int NextStep = 0;
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            //SPRibbon ribbon = SPRibbon.GetCurrent(this.Page);
            //if (ribbon != null)
            //{
            //    ribbon.TrimById("Ribbon.ListForm.Edit");
            //    //ribbon.CommandUIVisible = false;
            //}
            var cauHinhs = CauHinh;
            var isMember = false;
            foreach (var item in cauHinhs)
            {
                isMember = DeNghiHelper.IsCurrentUserInGroup(SPContext.Current.Web, item.SPGroup);
                if (isMember)
                {
                    CapDuyetText = item.CapDuyetText;
                    TrangThai = item.TrangThai;
                    NextStep = item.NextStep;
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
            SaveItem(NextStep, TrangThai, CapDuyetText);
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
            SaveItem(0, TrangThai, CapDuyetText);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        void SaveItem(int buocDuyet, int trangThai, string note)
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
            SPContext.Current.ListItem[Fields.TrangThai] = TrangThai;
            SPContext.Current.ListItem[Fields.BuocDuyet] = NextStep;
            SPContext.Current.ListItem[Fields.NgayNopHoSo] = DateTime.Now;
            SPContext.Current.ListItem[Fields.NguoiDeNghi] = SPContext.Current.Web.CurrentUser;
            SaveButton.SaveItem(SPContext.Current, false, string.Empty);
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
            DeNghiHelper.AddDeNghiHistory(SPContext.Current.Web, itemId, buocDuyet, trangThai, note);
            //Redirect to page
            var redirectUrl = Request.QueryString["Source"];
            if (redirectUrl == null || string.IsNullOrEmpty(redirectUrl.ToString()))
                redirectUrl = "/";
            longOperation.End(redirectUrl, Microsoft.SharePoint.Utilities.SPRedirectFlags.DoNotEndResponse, HttpContext.Current, "");
        }
    }
}
