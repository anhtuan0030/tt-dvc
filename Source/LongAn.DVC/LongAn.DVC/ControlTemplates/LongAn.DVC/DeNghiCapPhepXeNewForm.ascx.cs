using CamlexNET;
using LongAn.DVC.Common;
using LongAn.DVC.Helpers;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace LongAn.DVC.ControlTemplates.LongAn.DVC
{
    public partial class DeNghiCapPhepXeNewForm : UserControl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            //SPRibbon ribbon = SPRibbon.GetCurrent(this.Page);
            //if (ribbon != null)
            //{
            //    ribbon.TrimById("Ribbon.ListForm.Edit");
            //    //ribbon.CommandUIVisible = false;
            //}
            btnSave.Click += btnSave_Click;
            btnGuiHoSo.Click += btnGuiHoSo_Click;
            btnCancel.Click += btnCancel_Click;
            SPContext.Current.FormContext.OnSaveHandler += new EventHandler(DeNghiSaveHandler);
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
            SaveItem(TrangThaiXuLy.DaTiepNhan, CapXuLy.NhanVienTiepNhan);
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
            SaveItem(TrangThaiXuLy.KhoiTao, CapXuLy.CaNhanToChuc);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        void SaveItem(TrangThaiXuLy trangThai, CapXuLy capXuLy)
        {
            try
            {
                if (!this.Page.IsValid)
                    return;

                var longOperation = new SPLongOperation(this.Page);
                longOperation.LeadingHTML = "Please wait while the operation is running";
                longOperation.TrailingHTML = "Once the operation is finished you will be redirected to result page";
                longOperation.Begin();

                //Set default value
                string deNghiGuid = Guid.NewGuid().ToString();
                SPContext.Current.ListItem[Constants.FieldDeNghiGUID] = deNghiGuid;
                SPContext.Current.ListItem[Constants.FieldTrangThai] = (int)trangThai;
                SPContext.Current.ListItem[Constants.FieldTrangThai] = (int)capXuLy;
                SaveButton.SaveItem(SPContext.Current, false, "Thêm mới / gửi đề nghị");
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
                //Redirect to page
                var redirectUrl = Request.QueryString["Source"];
                if (redirectUrl == null || string.IsNullOrEmpty(redirectUrl.ToString()))
                    redirectUrl = "/";
                longOperation.End(redirectUrl);
                //Close popup
                //DeNghiHelper.ClosePopup(this.Page);
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
        }

        
    }
}
