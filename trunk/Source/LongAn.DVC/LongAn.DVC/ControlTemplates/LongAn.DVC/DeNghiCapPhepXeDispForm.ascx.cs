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
            #region variables
            var context = SPContext.Current;
            var currentStatus = int.Parse(SPContext.Current.ListItem[Constants.FieldTrangThai].ToString());
            var currentUserRole = CurrentUserRole;
            var action = Request.QueryString.Get("Action");
            var actionTocken = Request.QueryString.Get("Atocken");
            var sourcePage = Request.QueryString.Get("Source");
            var rejectLink = string.Format(Constants.ConfLinkTuChoiPage,
                context.Web.ServerRelativeUrl.TrimEnd('/'),
                context.ListId,
                context.ItemId,
                sourcePage);
            var additionalLink = string.Format(Constants.ConfLinkBoSungPage,
                context.Web.ServerRelativeUrl.TrimEnd('/'),
                context.ListId,
                context.ItemId,
                sourcePage);
            var assignLink = string.Format(Constants.ConfLinkPhanCongPage,
                context.Web.ServerRelativeUrl.TrimEnd('/'),
                context.ListId,
                context.ItemId,
                sourcePage);
            #endregion variables

            #region View button
            switch (currentUserRole)
            {
                #region MotCua
                case CapXuLy.MotCua:
                    if (currentStatus == (int)TrangThaiHoSo.ChoTiepNhan)
                    {
                        btnTiepNhan.Visible = true;
                        btnTiepNhan.CommandName = HanhDong.TiepNhanHoSo.ToString();
                        btnTiepNhan.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn tiếp nhận hồ sơ này không?')) return false;";
                        btnTiepNhan.Command += btnTiepNhan_Command;

                        btnTraHoSo.Visible = true;
                        btnTraHoSo.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn từ chối hồ sơ này không?')) return false;";
                        btnTraHoSo.CommandArgument = rejectLink;
                        btnTraHoSo.Command += btnRedirect_Command;
                    }
                    else if (currentStatus == (int)TrangThaiHoSo.DaTiepNhan)
                    {
                        btnPrint.Visible = true;
                        btnPrint.Click += btnPrint_Click;

                        btnTrinhXuLy.Visible = true;
                        btnTrinhXuLy.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn chuyển hồ sơ này không?')) return false;";
                        btnTrinhXuLy.Click += btnTrinhXuLy_Click;

                        btnTraHoSo.Visible = true;
                        btnTraHoSo.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn từ chối hồ sơ này không?')) return false;";
                        btnTraHoSo.CommandArgument = rejectLink;
                        btnTraHoSo.Command += btnRedirect_Command;
                    }
                    else if (currentStatus == (int)TrangThaiHoSo.DuocCapPhep)
                    {
                        btnHoanThanh.Visible = true;
                        btnHoanThanh.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn xác nhận hồ sơ này đã hoàn thành không?')) return false;";
                        btnHoanThanh.Click += btnHoanThanh_Click;

                        btnChuaHoanThanh.Visible = true;
                        btnChuaHoanThanh.CommandName = "XacNhanChuaHoanThanh";
                        btnChuaHoanThanh.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn xác nhận hồ sơ này chưa hoàn thành không?')) return false;";
                        btnChuaHoanThanh.Click += btnChuaHoanThanh_Click;

                        btnPrintGiayPhep.Visible = true;
                        btnPrintGiayPhep.Click += btnPrintGiayPhep_Click;
                    }
                    else if (currentStatus == (int)TrangThaiHoSo.HoanThanh || currentStatus == (int)TrangThaiHoSo.ChuaHoanThanh)
                    {
                        btnPrintGiayPhep.Visible = true;
                        btnPrintGiayPhep.CommandName = "InGiayPhep";
                    }
                    break;
                #endregion MotCua
                #region TruongPhoPhong
                case CapXuLy.TruongPhoPhong:
                    if (currentStatus == (int)TrangThaiHoSo.ChoDuyet)
                    {
                        btnTrinhLanhDao.Visible = true;
                        btnTrinhLanhDao.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn trình lãnh đạo hồ sơ này không?')) return false;";
                        btnTrinhLanhDao.Click += btnTrinhLanhDao_Click;

                        btnTraHoSo.Visible = true;
                        btnTraHoSo.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn từ chối hồ sơ này không?')) return false;";
                        btnTraHoSo.CommandArgument = rejectLink;
                        btnTraHoSo.Command += btnRedirect_Command;
                    }
                    else if (currentStatus == (int)TrangThaiHoSo.ChoXuLy)
                    {
                        btnPhanCongHoSo.Visible = true;
                        btnPhanCongHoSo.CommandArgument = assignLink;
                        btnPhanCongHoSo.Command += btnRedirect_Command;
                    }
                    break;
                #endregion TruongPhoPhong
                #region CanBo
                case CapXuLy.CanBo:
                    if (currentStatus == (int)TrangThaiHoSo.ChoXuLy)
                    {
                        divLoaiDuong.Visible = true;
                        btnTiepNhan.Visible = true;
                        btnTiepNhan.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn tiếp nhận hồ sơ này không?')) return false;";
                        btnTiepNhan.CommandName = HanhDong.TiepNhanXuLy.ToString();
                        btnTiepNhan.Command += btnTiepNhan_Command;
                    }
                    else if (currentStatus == (int)TrangThaiHoSo.DangXuLy)
                    {
                        divLoaiDuong.Visible = true;
                        btnYeuCauBoSung.Visible = true;
                        btnYeuCauBoSung.CommandArgument = additionalLink;
                        btnYeuCauBoSung.Command += btnYeuCauBoSung_Command;

                        btnTrinhTruongPhong.Visible = true;
                        btnTrinhTruongPhong.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn trình hồ sơ này không?')) return false;";
                        btnTrinhTruongPhong.Click += btnTrinhTruongPhong_Click;

                        btnTraHoSo.Visible = true;
                        btnTraHoSo.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn từ chối hồ sơ này không?')) return false;";
                        btnTraHoSo.CommandArgument = rejectLink;
                        btnTraHoSo.Command += btnRedirect_Command;
                    }
                    break;
                #endregion CanBo
                #region LanhDaoSo
                case CapXuLy.LanhDaoSo:
                    if (currentStatus == (int)TrangThaiHoSo.ChoCapPhep)
                    {
                        btnDuyetHoSo.Visible = true;
                        btnDuyetHoSo.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn duyệt thuận hồ sơ này không?')) return false;";
                        btnDuyetHoSo.Click += btnDuyetHoSo_Click;

                        btnTraHoSo.Visible = true;
                        btnTraHoSo.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn từ chối hồ sơ này không?')) return false;";
                        btnTraHoSo.CommandArgument = rejectLink;
                        btnTraHoSo.Command += btnRedirect_Command;
                    }
                    break;
                #endregion LanhDaoSo
                default:
                    btnPrint.Visible = true;
                    btnPrint.Click += btnPrint_Click;
                    break;
            }
            #endregion View button

            divLoaiDuongDisp.Visible = !divLoaiDuong.Visible;

            base.OnInit(e);
        }

        void btnYeuCauBoSung_Command(object sender, CommandEventArgs e)
        {
            string redirectUrl = e.CommandArgument.ToString();
            if (redirectUrl == null || string.IsNullOrEmpty(redirectUrl))
                redirectUrl = "/";
            Response.Redirect(redirectUrl);
        }

        void btnDuyetHoSo_Click(object sender, EventArgs e)
        {
            DeNghiHelper.AddDeNghiHistory(SPContext.Current.Web, CapXuLy.LanhDaoSo, SPContext.Current.ItemId, HanhDong.DuyetCapPhep.ToString());
            UpdateItem(TrangThaiHoSo.DuocCapPhep, CapXuLy.MotCua);
            DeNghiHelper.SendEmail(SPContext.Current.Web, SPContext.Current.ItemId, "được cấp phép");
        }

        void btnTrinhTruongPhong_Click(object sender, EventArgs e)
        {
            DeNghiHelper.AddDeNghiHistory(SPContext.Current.Web, CapXuLy.CanBo, SPContext.Current.ItemId, HanhDong.TrinhTruongPhoPhong.ToString());
            UpdateItem(TrangThaiHoSo.ChoDuyet, CapXuLy.TruongPhoPhong);
        }

        void btnRedirect_Command(object sender, CommandEventArgs e)
        {
            string redirectUrl = e.CommandArgument.ToString();
            if (redirectUrl == null || string.IsNullOrEmpty(redirectUrl))
                redirectUrl = "/";
            Response.Redirect(redirectUrl);
        }

        void btnTrinhLanhDao_Click(object sender, EventArgs e)
        {
            DeNghiHelper.AddDeNghiHistory(SPContext.Current.Web, CapXuLy.TruongPhoPhong, SPContext.Current.ItemId, HanhDong.TrinhLanhDaoSo.ToString());
            UpdateItem(TrangThaiHoSo.ChoCapPhep, CapXuLy.LanhDaoSo);
        }

        void btnPrintGiayPhep_Click(object sender, EventArgs e)
        {
            DeNghiHelper.InPhieuBienNhan(PrintType.GiayCapPhep, SPContext.Current.ItemId.ToString(), this.Page);
        }

        void btnChuaHoanThanh_Click(object sender, EventArgs e)
        {
            DeNghiHelper.AddDeNghiHistory(SPContext.Current.Web, CapXuLy.MotCua, SPContext.Current.ItemId, HanhDong.XacNhanChuaHoanThanh.ToString());
            UpdateItem(TrangThaiHoSo.ChuaHoanThanh, CapXuLy.MotCua);
        }

        void btnHoanThanh_Click(object sender, EventArgs e)
        {
            DeNghiHelper.AddDeNghiHistory(SPContext.Current.Web, CapXuLy.MotCua, SPContext.Current.ItemId, HanhDong.XacNhanHoanThanh.ToString());
            UpdateItem(TrangThaiHoSo.HoanThanh, CapXuLy.MotCua);
        }

        void btnTrinhXuLy_Click(object sender, EventArgs e)
        {
            DeNghiHelper.AddDeNghiHistory(SPContext.Current.Web, CapXuLy.MotCua, SPContext.Current.ItemId, HanhDong.ChuyenTruongPhoPhong.ToString());
            UpdateItem(TrangThaiHoSo.ChoXuLy, CapXuLy.TruongPhoPhong);
        }

        void btnTiepNhan_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == HanhDong.TiepNhanHoSo.ToString())
            {
                DeNghiHelper.AddDeNghiHistory(SPContext.Current.Web, CapXuLy.MotCua, SPContext.Current.ItemId, HanhDong.TiepNhanHoSo.ToString());
                DeNghiHelper.SendEmail(SPContext.Current.Web, SPContext.Current.ItemId, "được tiếp nhận");
                UpdateItem(TrangThaiHoSo.DaTiepNhan, CapXuLy.MotCua);
            }
            else if (e.CommandName == HanhDong.TiepNhanXuLy.ToString())
            {
                DeNghiHelper.AddDeNghiHistory(SPContext.Current.Web, CapXuLy.CanBo, SPContext.Current.ItemId, HanhDong.TiepNhanXuLy.ToString());
                UpdateItem(TrangThaiHoSo.DangXuLy, CapXuLy.CanBo);
            }
        }

        void btnPrint_Click(object sender, EventArgs e)
        {
            DeNghiHelper.InPhieuBienNhan(PrintType.PhieuBienNhan, SPContext.Current.ItemId.ToString(), this.Page);
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

                var lanDuong = currentItem[Constants.FieldLanXeDuocChay] != null ? currentItem[Constants.FieldLanXeDuocChay].ToString() : string.Empty;
                txtLanXeDuocChay.Text = lanDuong;
                var tocDo = currentItem[Constants.FieldTocDoDuocChay] != null ? currentItem[Constants.FieldTocDoDuocChay].ToString() : string.Empty;
                txtTocDoDuocChay.Text = tocDo;
            }
        }

        void UpdateItem(TrangThaiHoSo trangThai, CapXuLy capXuLy)
        {
            if (!this.Page.IsValid)
                return;

            var longOperation = new SPLongOperation(this.Page);
            longOperation.LeadingHTML = "Please wait while the operation is running";
            longOperation.TrailingHTML = "Once the operation is finished you will be redirected to result page";
            longOperation.Begin();
            
            var deNghiList = SPContext.Current.List;
            var spListItem = deNghiList.GetItemById(SPContext.Current.ItemId);
            spListItem[Constants.FieldCapDuyet] = (int)capXuLy;
            spListItem[Constants.FieldTrangThai] = (int)trangThai;
            var currentUser = SPContext.Current.Web.CurrentUser;
            if (capXuLy == CapXuLy.MotCua && trangThai == TrangThaiHoSo.DaTiepNhan)
                spListItem[Constants.FieldMotCuaUser] = currentUser;
            else if(capXuLy == CapXuLy.CanBo)
                spListItem[Constants.FieldCanBoUser] = currentUser;
            if (divLoaiDuong.Visible)
            {
                SPFieldMultiChoiceValue values = new SPFieldMultiChoiceValue();
                for (int i = 0; i < chkListLoaiDuong.Items.Count; i++)
                {
                    if (chkListLoaiDuong.Items[i].Selected)
                    {
                        values.Add(chkListLoaiDuong.Items[i].Text);
                    }
                }
                spListItem[Constants.FieldLoaiDuong] = values;
                spListItem[Constants.FieldLanXeDuocChay] = txtLanXeDuocChay.Text.Trim();
                spListItem[Constants.FieldTocDoDuocChay] = txtTocDoDuocChay.Text.Trim();
            }
            spListItem.Update();
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

        
    }
}
