using CamlexNET;
using LongAn.DVC.Common;
using LongAn.DVC.Helpers;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace LongAn.DVC.ControlTemplates.LongAn.DVC
{
    public partial class DeNghiCapPhepXeDispForm : UserControl
    {
        #region Properties
        private CapXuLy CurrentUserRole
        {
            get
            {
                var currentUserRole = CapXuLy.CaNhanToChuc;
                if (ViewState[Constants.ConfViewStateCapXuLy] == null)
                {
                    currentUserRole = DeNghiHelper.CurrentUserRole(SPContext.Current.Web, SPContext.Current.Web.CurrentUser);
                    ViewState[Constants.ConfViewStateCapXuLy] = currentUserRole;
                }
                else
                    currentUserRole = (CapXuLy)ViewState[Constants.ConfViewStateCapXuLy];
                return currentUserRole;
            }
        }
        #endregion Properties
        protected override void OnInit(EventArgs e)
        {
            btnCancel.Click += btnCancel_Click;
            //
            var currentStatus = int.Parse(SPContext.Current.ListItem[Constants.FieldTrangThai].ToString());
            var currentUserRole = CurrentUserRole;
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
                    if (currentStatus == (int)TrangThaiXuLy.ChoXuLy || 
                        currentStatus == (int)TrangThaiXuLy.DaPhanCong)
                    {
                        if (Request.QueryString["BS"] != null && Request.QueryString["BS"].ToString() == Constants.ConfQueryStringBS)
                        {
                            divYeuCauBoSung.Visible = true;
                            btnYeuCauBoSung.Visible = true;
                            btnYeuCauBoSung.Click += btnYeuCauBoSung_Click;
                        }
                        btnTrinhTruongPhong.Visible = true;
                        btnTrinhTruongPhong.Click += btnTrinhTruongPhong_Click;

                        divLoaiDuong.Visible = true;
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
                        divPhanCongHoSo.Visible = true;
                        if (Request.QueryString["PC"] != null && Request.QueryString["PC"].ToString() == Constants.ConfQueryStringPC)
                        {
                            if (!IsPostBack)
                            {
                                ddlUsers.DataSource = GetCanBoXuLy();
                                ddlUsers.DataValueField = "ID";
                                ddlUsers.DataTextField = "Name";
                                ddlUsers.DataBind();
                            }
                            btnPhanCongHoSo.Visible = true;
                            btnPhanCongHoSo.Click += btnPhanCongHoSo_Click;
                        }
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

        void btnPhanCongHoSo_Click(object sender, EventArgs e)
        {
            UpdateItem(TrangThaiXuLy.DaPhanCong, CapXuLy.CanBoXuLy, false);
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
            UpdateItem(TrangThaiXuLy.HoSoDuocDuyet, CapXuLy.NhanVienTiepNhan, false);
        }

        void btnTrinhLanhDao_Click(object sender, EventArgs e)
        {
            UpdateItem(TrangThaiXuLy.ChoLanhDaoDuyet, CapXuLy.LanhDaoSo, false);
        }

        void btnTraHoSo_Click(object sender, EventArgs e)
        {
            UpdateItem(TrangThaiXuLy.HoSoBiTuChoi, CapXuLy.CaNhanToChuc, false);
        }

        void btnTrinhTruongPhong_Click(object sender, EventArgs e)
        {
            UpdateItem(TrangThaiXuLy.ChoTruongPhongDuyet, CapXuLy.TruongPhoPhong, false);
        }

        void btnYeuCauBoSung_Click(object sender, EventArgs e)
        {
            UpdateItem(TrangThaiXuLy.HoSoChoBoSung, CapXuLy.CaNhanToChuc, true);
        }

        void btnTrinhXuLy_Click(object sender, EventArgs e)
        {
            UpdateItem(TrangThaiXuLy.ChoXuLy, CapXuLy.TruongPhoPhong, false);
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

        void UpdateItem(TrangThaiXuLy trangThai, CapXuLy capXuLy, bool isBoSung)
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
            if (trangThai == TrangThaiXuLy.DaPhanCong)
                spListItem[Constants.FieldDeNghiAdmin] = ddlUsers.SelectedValue;
            
            spListItem.Update();
            if (isBoSung)
            {
                AddBoSungYeuCau(SPContext.Current.Web);
            }
            //Redirect to page
            var redirectUrl = Request.QueryString["Source"];
            if (redirectUrl == null || string.IsNullOrEmpty(redirectUrl.ToString()))
                redirectUrl = "/";
            longOperation.End(redirectUrl);
        }

        DataTable GetCanBoXuLy()
        {
            DataTable result = new DataTable();
            DataColumn[] columns = new DataColumn[] { 
                new DataColumn("ID"),
                new DataColumn("Name")
            };
            result.Columns.AddRange(columns);
            try
            {
                LoggingServices.LogMessage("Begin GetCanBoXuLy");
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                    {
                        using (SPWeb web = site.OpenWeb(SPContext.Current.Web.ID))
                        {
                            var canBoGroup = web.SiteGroups[Constants.ConfGroupCanBoXuLy];
                            foreach (SPUser user in canBoGroup.Users)
                            {
                                DataRow row = result.NewRow();
                                row["ID"] = user.ID;
                                row["Name"] = user.Name;
                                result.Rows.Add(row);
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("End GetCanBoXuLy");
            return result;
        }

        void AddBoSungYeuCau(SPWeb web)
        {
            try
            {
                LoggingServices.LogMessage("Begin AddBoSungYeuCau");
                var yeuCauBoSungUrl = (web.ServerRelativeUrl + Constants.ListUrlYeuCauBoSung).Replace("//", "/");
                var yeuCauBoSungList = web.GetList(yeuCauBoSungUrl);
                var yeuCauBoSungItem = yeuCauBoSungList.Items.Add();
                yeuCauBoSungItem[Constants.FieldTitle] = txtTieuDe.Text.Trim();
                yeuCauBoSungItem[Constants.FieldMoTa] = txtDienGiaiChiTiet.Text.Trim();
                yeuCauBoSungItem.Update();
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("End AddBoSungYeuCau");
        }
    }
}
