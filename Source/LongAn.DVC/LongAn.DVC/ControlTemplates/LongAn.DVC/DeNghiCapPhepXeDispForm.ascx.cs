using CamlexNET;
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
    public partial class DeNghiCapPhepXeDispForm : UserControl
    {
        protected override void OnInit(EventArgs e)
        {
            FormField1.ControlMode = SPControlMode.Edit;
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
            switch (currentUserRole)
            {
                case CapXuLy.NhanVienTiepNhan:
                    if (currentStatus == (int)TrangThaiXuLy.DaTiepNhan)
                    {
                        btnTrinhXuLy.Visible = true;
                        btnTrinhXuLy.Click += btnTrinhXuLy_Click;
                    }
                    break;
                case CapXuLy.CanBoXuLy:
                    if (currentStatus == (int)TrangThaiXuLy.ChoXuLy)
                    {
                        btnYeuCauBoSung.Visible = true;
                        btnTrinhTruongPhong.Visible = true;
                        btnTraHoSo.Visible = true;
                        btnYeuCauBoSung.Click += btnYeuCauBoSung_Click;
                        btnTrinhTruongPhong.Click += btnTrinhTruongPhong_Click;
                        btnTraHoSo.Click += btnTraHoSo_Click;
                    }
                    break;
                case CapXuLy.TruongPhoPhong:
                    if (currentStatus == (int)TrangThaiXuLy.ChoTruongPhongDuyet)
                    {
                        btnTrinhLanhDao.Visible = true;
                        btnTraHoSo.Visible = true;
                        btnTrinhLanhDao.Click += btnTrinhLanhDao_Click;
                        btnTraHoSo.Click += btnTraHoSo_Click;
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
                        btnDuyetHoSo.Click += btnDuyetHoSo_Click;
                        btnTraHoSo.Click += btnTraHoSo_Click;
                    }
                    break;
                default:
                    break;
            }
            base.OnInit(e);
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            var redirectUrl = Request.QueryString["Source"];
            if (redirectUrl == null || string.IsNullOrEmpty(redirectUrl.ToString()))
                redirectUrl = "/";
            Response.Redirect(redirectUrl);
        }

        void btnDuyetHoSo_Click(object sender, EventArgs e)
        {
            UpdateItem(TrangThaiXuLy.HoSoDuocDuyet, CapXuLy.NhanVienTiepNhan);
        }

        void btnTrinhLanhDao_Click(object sender, EventArgs e)
        {
            UpdateItem(TrangThaiXuLy.ChoLanhDaoDuyet, CapXuLy.LanhDaoSo);
        }

        void btnTraHoSo_Click(object sender, EventArgs e)
        {
            UpdateItem(TrangThaiXuLy.HoSoBiTuChoi, CapXuLy.CaNhanToChuc);
        }

        void btnTrinhTruongPhong_Click(object sender, EventArgs e)
        {
            UpdateItem(TrangThaiXuLy.ChoTruongPhongDuyet, CapXuLy.TruongPhoPhong);
        }

        void btnYeuCauBoSung_Click(object sender, EventArgs e)
        {
            UpdateItem(TrangThaiXuLy.HoSoChoBoSung, CapXuLy.CaNhanToChuc);
        }

        void btnTrinhXuLy_Click(object sender, EventArgs e)
        {
            UpdateItem(TrangThaiXuLy.ChoXuLy, CapXuLy.TruongPhoPhong);
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

        void UpdateItem(TrangThaiXuLy trangThai, CapXuLy capXuLy)
        {
            if (!this.Page.IsValid)
                return;

            var longOperation = new SPLongOperation(this.Page);
            longOperation.LeadingHTML = "Please wait while the operation is running";
            longOperation.TrailingHTML = "Once the operation is finished you will be redirected to result page";
            longOperation.Begin();
            //Save file upload
            var deNghiList = SPContext.Current.List;
            var spListItem = deNghiList.GetItemById(SPContext.Current.ItemId);
            spListItem[Constants.FieldCapDuyet] = (int)capXuLy;
            spListItem[Constants.FieldTrangThai] = (int)trangThai;
            spListItem.Update();
            //Redirect to page
            var redirectUrl = Request.QueryString["Source"];
            if (redirectUrl == null || string.IsNullOrEmpty(redirectUrl.ToString()))
                redirectUrl = "/";
            longOperation.End(redirectUrl);
        }
    }
}
