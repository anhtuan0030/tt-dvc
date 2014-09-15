using LongAn.DVC.Common;
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
            base.OnInit(e);
            btnCancel.Click += btnCancel_Click;
            btnSave.Click += btnSave_Click;
            btnGuiHoSo.Click += btnGuiHoSo_Click;
            btnTrinhXuLy.Click += btnTrinhXuLy_Click;
            btnPhanCongHoSo.Click += btnPhanCongHoSo_Click;
            btnTrinhTruongPhong.Click += btnTrinhTruongPhong_Click;
            btnTrinhLanhDao.Click += btnTrinhLanhDao_Click;
            btnDuyetHoSo.Click += btnDuyetHoSo_Click;
            btnTraHoSo.Click += btnTraHoSo_Click;
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

            switch (currentUserRole)
            {
                case CapXuLy.CaNhanToChuc:
                    if (currentStatus == (int)TrangThaiXuLy.KhoiTao ||
                        currentStatus == (int)TrangThaiXuLy.HoSoBiTuChoi)
                    {
                        btnSave.Visible = true;
                        btnGuiHoSo.Visible = true;
                        fileUpload1.Visible = true;
                        fileUpload2.Visible = true;
                        fileUpload3.Visible = true;
                        fileUpload4.Visible = true;

                        #region Enable form field
                        var controlModeEdit = Microsoft.SharePoint.WebControls.SPControlMode.Edit;
                        FormField1.ControlMode = controlModeEdit;
                        FormField2.ControlMode = controlModeEdit;
                        FormField3.ControlMode = controlModeEdit;
                        FormField4.ControlMode = controlModeEdit;
                        FormField5.ControlMode = controlModeEdit;
                        FormField6.ControlMode = controlModeEdit;
                        FormField7.ControlMode = controlModeEdit;
                        FormField8.ControlMode = controlModeEdit;
                        FormField9.ControlMode = controlModeEdit;
                        FormField10.ControlMode = controlModeEdit;
                        FormField11.ControlMode = controlModeEdit;
                        FormField12.ControlMode = controlModeEdit;
                        FormField13.ControlMode = controlModeEdit;
                        FormField14.ControlMode = controlModeEdit;
                        FormField15.ControlMode = controlModeEdit;
                        FormField16.ControlMode = controlModeEdit;
                        FormField17.ControlMode = controlModeEdit;
                        FormField18.ControlMode = controlModeEdit;
                        FormField19.ControlMode = controlModeEdit;
                        FormField20.ControlMode = controlModeEdit;
                        FormField21.ControlMode = controlModeEdit;
                        FormField22.ControlMode = controlModeEdit;
                        FormField23.ControlMode = controlModeEdit;
                        FormField24.ControlMode = controlModeEdit;
                        FormField25.ControlMode = controlModeEdit;
                        FormField26.ControlMode = controlModeEdit;
                        FormField27.ControlMode = controlModeEdit;
                        FormField28.ControlMode = controlModeEdit;
                        FormField29.ControlMode = controlModeEdit;
                        FormField30.ControlMode = controlModeEdit;
                        FormField31.ControlMode = controlModeEdit;
                        FormField32.ControlMode = controlModeEdit;
                        FormField33.ControlMode = controlModeEdit;
                        FormField34.ControlMode = controlModeEdit;
                        FormField35.ControlMode = controlModeEdit;
                        FormField36.ControlMode = controlModeEdit;
                        FormField37.ControlMode = controlModeEdit;
                        #endregion Enable form field
                    }
                    break;
                case CapXuLy.NhanVienTiepNhan:
                    if (currentStatus == (int)TrangThaiXuLy.DaTiepNhan)
                    {
                        btnTrinhXuLy.Visible = true;
                        btnTraHoSo.Visible = true;
                    }
                    break;
                case CapXuLy.CanBoXuLy:
                    if (currentStatus == (int)TrangThaiXuLy.ChoXuLy)
                    {
                        btnTrinhTruongPhong.Visible = true;
                        btnTraHoSo.Visible = true;
                    }
                    break;
                case CapXuLy.TruongPhoPhong:
                    if (currentStatus == (int)TrangThaiXuLy.ChoTruongPhongDuyet)
                    {
                        btnTrinhLanhDao.Visible = true;
                        btnTraHoSo.Visible = true;
                    }
                    else if (currentStatus == (int)TrangThaiXuLy.ChoXuLy)
                    {
                        btnPhanCongHoSo.Visible = true;
                    }
                    break;
                case CapXuLy.LanhDaoSo:
                    if (currentStatus == (int)TrangThaiXuLy.ChoLanhDaoDuyet)
                    {
                        btnDuyetHoSo.Visible = true;
                        btnTraHoSo.Visible = true;
                    }
                    break;
                case CapXuLy.VanPhongSo:
                    break;
                default:
                    break;
            }
        }

        void btnTraHoSo_Click(object sender, EventArgs e)
        {
            UpdateItem(TrangThaiXuLy.HoSoBiTuChoi);
        }

        void btnDuyetHoSo_Click(object sender, EventArgs e)
        {
            UpdateItem(TrangThaiXuLy.HoSoDuocDuyet);
        }

        void btnTrinhLanhDao_Click(object sender, EventArgs e)
        {
            UpdateItem(TrangThaiXuLy.ChoLanhDaoDuyet);
        }

        void btnTrinhTruongPhong_Click(object sender, EventArgs e)
        {
            UpdateItem(TrangThaiXuLy.ChoTruongPhongDuyet);
        }

        void btnPhanCongHoSo_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        void btnTrinhXuLy_Click(object sender, EventArgs e)
        {
            UpdateItem(TrangThaiXuLy.ChoXuLy);
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
