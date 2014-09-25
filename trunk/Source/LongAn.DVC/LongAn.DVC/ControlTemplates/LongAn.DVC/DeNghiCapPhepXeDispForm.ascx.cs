using CamlexNET;
using LongAn.DVC.Common;
using LongAn.DVC.Helpers;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System;
using System.Data;
using System.Web;
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
            var action = Request.QueryString.Get("Action");
            var actionTocken = Request.QueryString.Get("Atocken");

            ShowActions(currentUserRole, action, actionTocken);

            #region Backup
            /*
            switch (currentUserRole)
            {
                case CapXuLy.MotCua:
                    if (currentStatus == (int)TrangThaiHoSo.DaTiepNhan)
                    {
                        btnTrinhXuLy.Visible = true;
                        btnTrinhXuLy.Click += btnTrinhXuLy_Click;

                        ShowActions(CapXuLy.MotCua, action, actionTocken);
                    }
                    break;
                case CapXuLy.CanBo:
                    if (currentStatus == (int)TrangThaiHoSo.KhoiTao)
                    {
                        btnTiepNhan.Visible = true;
                        btnTiepNhan.Click += btnTiepNhan_Click;
                    }
                    else if (currentStatus == (int)TrangThaiHoSo.ChoXuLy ||
                        currentStatus == (int)TrangThaiHoSo.DangXuLy)
                    {
                        if (Request.QueryString["BS"] != null 
                            && Request.QueryString["BS"].ToString() == Constants.ConfQueryStringBS)
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
                    if (currentStatus == (int)TrangThaiHoSo.ChoDuyet)
                    {
                        btnTrinhLanhDao.Visible = true;
                        btnTraHoSo.Visible = true;
                        btnTrinhLanhDao.Click += btnTrinhLanhDao_Click;
                        btnTraHoSo.Click += btnTraHoSo_Click;
                    }
                    else if (currentStatus == (int)TrangThaiHoSo.ChoXuLy)
                    {
                        divPhanCongHoSo.Visible = true;
                        ShowActions(CapXuLy.TruongPhoPhong, action, actionTocken);
                    }
                    break;
                case CapXuLy.LanhDaoSo:
                    if (currentStatus == (int)TrangThaiHoSo.ChoCapPhep)
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
            */
            #endregion Backup

            base.OnInit(e);
        }

        void ShowActions(CapXuLy capXuLy, string action, string tocken)
        {
            if (action == null || tocken == null)
                return;
            switch (action)
            {
                case Constants.ConfActionPC:
                    if (tocken == Constants.ConfQueryStringPC)
                    {
                        divPhanCongHoSo.Visible = true;
                        btnPhanCongHoSo.Visible = true;
                        if (!IsPostBack)
                        {
                            ddlUsers.DataSource = GetCanBoXuLy();
                            ddlUsers.DataValueField = "ID";
                            ddlUsers.DataTextField = "Name";
                            ddlUsers.DataBind();
                        }
                        btnPhanCongHoSo.Click +=btnPhanCongHoSo_Click;
                    }
                    break;
                case Constants.ConfActionBS:
                    if (tocken == Constants.ConfQueryStringBS)
                    {
                        divYeuCauBoSung.Visible = true;
                        btnYeuCauBoSung.Visible = true;
                        btnYeuCauBoSung.Click+=btnYeuCauBoSung_Click;
                    }
                    break;
                case Constants.ConfActionTC:
                    if (tocken == Constants.ConfQueryStringTC)
                    {
                        btnTraHoSo.Visible = true;
                        divThongTinTuChoi.Visible = true;
                        if (capXuLy == CapXuLy.MotCua)
                            btnTraHoSo.Click += btnTraHoSo_Click;
                        else if(capXuLy == CapXuLy.CanBo)
                            btnTraHoSo.Click+= btnCanBoTraHoSo_Click;
                        else if (capXuLy == CapXuLy.TruongPhoPhong)
                            btnTraHoSo.Click += btnTruongPhongTraHoSo_Click;
                        else if (capXuLy == CapXuLy.LanhDaoSo)
                            btnTraHoSo.Click += btnLanhDaoTraHoSo_Click;
                    }
                    break;
                default:
                    break;
            }
        }

        #region Tra Ho So
        void btnTraHoSo_Click(object sender, EventArgs e)
        {
            DeNghiHelper.AddDeNghiHistory(SPContext.Current.Web, CapXuLy.MotCua, SPContext.Current.ItemId, HanhDong.TuChoiHoSo.ToString(), txtLyDo.Text);
            UpdateItem(TrangThaiHoSo.BiTuChoi, CapXuLy.CaNhanToChuc, false);
        }

        void btnCanBoTraHoSo_Click(object sender, EventArgs e)
        {
            DeNghiHelper.AddDeNghiHistory(SPContext.Current.Web, CapXuLy.CanBo, SPContext.Current.ItemId, HanhDong.TuChoiHoSo.ToString(), txtLyDo.Text);
            UpdateItem(TrangThaiHoSo.BiTuChoi, CapXuLy.CaNhanToChuc, false);
        }

        void btnTruongPhongTraHoSo_Click(object sender, EventArgs e)
        {
            DeNghiHelper.AddDeNghiHistory(SPContext.Current.Web, CapXuLy.TruongPhoPhong, SPContext.Current.ItemId, HanhDong.TuChoiHoSo.ToString(), txtLyDo.Text);
            UpdateItem(TrangThaiHoSo.BiTuChoi, CapXuLy.CaNhanToChuc, false);
        }

        void btnLanhDaoTraHoSo_Click(object sender, EventArgs e)
        {
            DeNghiHelper.AddDeNghiHistory(SPContext.Current.Web, CapXuLy.LanhDaoSo, SPContext.Current.ItemId, HanhDong.TuChoiHoSo.ToString(), txtLyDo.Text);
            UpdateItem(TrangThaiHoSo.BiTuChoi, CapXuLy.CaNhanToChuc, false);
        }
        #endregion Tra Ho So
        
        void btnPhanCongHoSo_Click(object sender, EventArgs e)
        {
            DeNghiHelper.AddDeNghiHistory(SPContext.Current.Web, CapXuLy.TruongPhoPhong, SPContext.Current.ItemId, HanhDong.PhanCongHoSo.ToString(), txtGhiChu.Text);
            UpdateItem(TrangThaiHoSo.DangXuLy, CapXuLy.CanBo, false);
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            var redirectUrl = Request.QueryString["Source"];
            if (redirectUrl == null || string.IsNullOrEmpty(redirectUrl.ToString()))
                redirectUrl = "/";
            Response.Redirect(redirectUrl);
        }

        void btnYeuCauBoSung_Click(object sender, EventArgs e)
        {
            DeNghiHelper.AddDeNghiHistory(SPContext.Current.Web, CapXuLy.CanBo, SPContext.Current.ItemId, HanhDong.YeuCauBoSung.ToString(), txtTieuDe.Text);
            UpdateItem(TrangThaiHoSo.ChoBoSung, CapXuLy.CaNhanToChuc, true);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DeNghiHelper.LoadAttachments(SPContext.Current.ItemId, Constants.AttachmentGiayDangKy, divFileUpload1);
                DeNghiHelper.LoadAttachments(SPContext.Current.ItemId, Constants.AttachmentGiayChungNhanKiemDinh, divFileUpload2);
                DeNghiHelper.LoadAttachments(SPContext.Current.ItemId, Constants.AttachmentGiayCamKet, divFileUpload3);
                DeNghiHelper.LoadAttachments(SPContext.Current.ItemId, Constants.AttachmentCMND, divFileUpload4);

                //var yeuCauBoSungUrl = (SPContext.Current.Web.ServerRelativeUrl + Constants.ListUrlYeuCauBoSung).Replace("//", "/");
                //var yeuCauBoSungList = SPContext.Current.Web.GetList(yeuCauBoSungUrl);
                SPFieldMultiChoice causeOfCustomer = (SPFieldMultiChoice)SPContext.Current.List.Fields[new Guid(Constants.FieldIdLoaiDuong)];
                chkListLoaiDuong.DataSource = causeOfCustomer.Choices;
                chkListLoaiDuong.DataBind();

                var currentItem = SPContext.Current.ListItem;
                var loaiDuongString = currentItem[Constants.FieldLoaiDuong] != null ? currentItem[Constants.FieldLoaiDuong].ToString() : string.Empty;
                for (int i = 0; i < chkListLoaiDuong.Items.Count; i++)
                {
                    if (string.IsNullOrEmpty(loaiDuongString))
                        break;
                    if (loaiDuongString.Contains(chkListLoaiDuong.Items[i].Text))
                    {
                        chkListLoaiDuong.Items[i].Selected = true;
                    }
                }
            }
        }

        void UpdateItem(TrangThaiHoSo trangThai, CapXuLy capXuLy, bool isBoSung)
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
            if (trangThai == TrangThaiHoSo.ChoXuLy)
                spListItem[Constants.FieldCaNhanToChuc] = ddlUsers.SelectedValue;
            
            spListItem.Update();
            if (isBoSung)
            {
                AddBoSungYeuCau(SPContext.Current.Web);
            }
            //Redirect to page
            var redirectUrl = Request.QueryString["Source"];
            if (redirectUrl == null || string.IsNullOrEmpty(redirectUrl.ToString()))
                redirectUrl = "/";
            longOperation.End(redirectUrl, Microsoft.SharePoint.Utilities.SPRedirectFlags.DoNotEndResponse, HttpContext.Current, "");
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
                yeuCauBoSungItem[Constants.FieldDeNghi] = SPContext.Current.ItemId;
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
