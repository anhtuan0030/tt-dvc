using CamlexNET;
using LongAn.DVC.Common;
using LongAn.DVC.Helpers;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace LongAn.DVC.WebParts.DeNghiListView
{
    [ToolboxItemAttribute(false)]
    public partial class DeNghiListView : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public DeNghiListView()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
            btnTimKiem.Click += btnTimKiem_Click;
        }

        void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                this.BindItemsList();
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.BindItemsList();
                //Link & Title
                literalDeNghiTitle.Text = DeNghiTitle;
                if (CurrentUserRole == (int)CapXuLy.NhanVienTiepNhan)
                {
                    divAddNew.Visible = true;
                    var deNghiUrl = (SPContext.Current.Web.ServerRelativeUrl + Constants.ListUrlDeNghiCapPhep).Replace("//", "/");
                    var currentPage = SPUtility.GetPageUrlPath(HttpContext.Current);
                    var viewUrl = string.Format(Constants.ConfLinkNewForm, deNghiUrl, currentPage);
                    lbtAddNew.OnClientClick = viewUrl;   
                }
                hplTrangChu.NavigateUrl = LinkTrangChu;
                hplHoSoDaTiepNhan.NavigateUrl = LinkHoSoDaTiepNhan;
                hplHoSoChuaPhanCong.NavigateUrl = LinkHoSoChoPhanCong;
                hplHoSoDaPhanCong.NavigateUrl = LinkHoSoDaPhanCong;
                hplHoSoChoDuyet.NavigateUrl = LinkHoSoChoDuyet;
                hplHoSoChoCapPhep.NavigateUrl = LinkHoSoChoCapPhep;
                hplHoSoDaCapPhep.NavigateUrl = LinkHoSoDaCapPhep;
                hplHoSoBiTuChoi.NavigateUrl = LinkHoSoBiTuChoi;
                hplThongKeBaoCao.NavigateUrl = LinkThongKeBaoCao;
            }
        }

        #region Private Properties
        private int CurrentUserRole
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
                return (int)currentUserRole;
            }
        }
        #endregion Private Properties

        #region Paging Properties
        private int CurrentPage
        {
            get
            {
                object objPage = ViewState["_CurrentPage"];
                int _CurrentPage = 0;
                if (objPage == null)
                {
                    _CurrentPage = 0;
                }
                else
                {
                    _CurrentPage = (int)objPage;
                }
                return _CurrentPage;
            }
            set { ViewState["_CurrentPage"] = value; }
        }
        private int fistIndex
        {
            get
            {

                int _FirstIndex = 0;
                if (ViewState["_FirstIndex"] == null)
                {
                    _FirstIndex = 0;
                }
                else
                {
                    _FirstIndex = Convert.ToInt32(ViewState["_FirstIndex"]);
                }
                return _FirstIndex;
            }
            set { ViewState["_FirstIndex"] = value; }
        }
        private int lastIndex
        {
            get
            {

                int _LastIndex = 0;
                if (ViewState["_LastIndex"] == null)
                {
                    _LastIndex = 0;
                }
                else
                {
                    _LastIndex = Convert.ToInt32(ViewState["_LastIndex"]);
                }
                return _LastIndex;
            }
            set { ViewState["_LastIndex"] = value; }
        }
        #endregion

        #region PagedDataSource
        PagedDataSource _PageDataSource = new PagedDataSource();
        #endregion

        #region WebPart Properties

        [WebBrowsable(true),
         WebDisplayName("Tiêu đề"),
         WebDescription("This Accepts text Input"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public string DeNghiTitle { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Trạng thái xử lý"),
         WebDescription("This Accepts number Input"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public int TrangThai { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Phân trang"),
         WebDescription("This Accepts number Input"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public int PageSize { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Link Trang chủ"),
         WebDescription("This Accepts text Input"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public string LinkTrangChu { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Link Hồ sơ đã tiếp nhận"),
         WebDescription("This Accepts text Input"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public string LinkHoSoDaTiepNhan { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Link Hồ sơ chờ phân công"),
         WebDescription("This Accepts text Input"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public string LinkHoSoChoPhanCong { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Link Hồ sơ đã phân công"),
         WebDescription("This Accepts text Input"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public string LinkHoSoDaPhanCong { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Link Hồ sơ chờ duyệt"),
         WebDescription("This Accepts text Input"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public string LinkHoSoChoDuyet { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Link Hồ sơ chờ cấp phép"),
         WebDescription("This Accepts text Input"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public string LinkHoSoChoCapPhep { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Link Hồ sơ đã cấp phép"),
         WebDescription("This Accepts text Input"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public string LinkHoSoDaCapPhep { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Link Hồ sơ bị từ chối"),
         WebDescription("This Accepts text Input"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public string LinkHoSoBiTuChoi { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Link Thống kê báo cáo"),
         WebDescription("This Accepts text Input"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public string LinkThongKeBaoCao { get; set; }

        #endregion WebPart Properties

        #region Repeater Page
        protected void repeaterPage_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Paging"))
            {
                CurrentPage = Convert.ToInt16(e.CommandArgument.ToString());
                this.BindItemsList();
            }
        }

        protected void repeaterPage_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                LinkButton lnkbtnPage = (LinkButton)e.Item.FindControl("lnkbtnPaging");
                if (lnkbtnPage.CommandArgument.ToString() == CurrentPage.ToString())
                {
                    lnkbtnPage.Enabled = false;
                    //lnkbtnPage.Style.Add("fone-size", "14px");
                    lnkbtnPage.Font.Bold = true;
                }
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
        }

        #endregion Repeater Page

        #region Private Function
        private void BindItemsList()
        {
            LoggingServices.LogMessage("Begin BindItemsList");
            DataTable dataTable = this.GetDeNghi(TrangThai);
            try
            {
                divPagging.Visible = false;
                if (dataTable != null)
                {
                    _PageDataSource.DataSource = dataTable.DefaultView;
                    _PageDataSource.AllowPaging = true;
                    _PageDataSource.PageSize = PageSize;
                    _PageDataSource.CurrentPageIndex = CurrentPage;
                    ViewState["TotalPages"] = _PageDataSource.PageCount;

                    //this.lblPageInfo.Text = "Page " + (CurrentPage + 1) + " of " + _PageDataSource.PageCount;
                    this.lbtnPrevious.Enabled = !_PageDataSource.IsFirstPage;
                    this.lbtnNext.Enabled = !_PageDataSource.IsLastPage;
                    this.lbtnFirst.Enabled = !_PageDataSource.IsFirstPage;
                    this.lbtnLast.Enabled = !_PageDataSource.IsLastPage;

                    this.repeaterLists.DataSource = _PageDataSource;
                    this.repeaterLists.DataBind();
                    this.DoPaging();

                    if (dataTable != null && dataTable.Rows.Count > PageSize)
                        divPagging.Visible = true;
                }

            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("End BindItemsList");
        }

        private void DoPaging()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PageIndex");
            dt.Columns.Add("PageText");

            fistIndex = CurrentPage - 5;

            if (CurrentPage > 5)
            {
                lastIndex = CurrentPage + 5;
            }
            else
            {
                lastIndex = 10;
            }
            if (lastIndex > Convert.ToInt32(ViewState["TotalPages"]))
            {
                lastIndex = Convert.ToInt32(ViewState["TotalPages"]);
                fistIndex = lastIndex - 10;
            }

            if (fistIndex < 0)
            {
                fistIndex = 0;
            }

            for (int i = fistIndex; i < lastIndex; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i + 1;
                dt.Rows.Add(dr);
            }

            this.repeaterPage.DataSource = dt;
            this.repeaterPage.DataBind();
        }

        protected void lbtnLast_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            this.BindItemsList();
        }

        protected void lbtnFirst_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            this.BindItemsList();
        }

        protected void lbtnNext_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            this.BindItemsList();
        }

        protected void lbtnPrevious_Click(object sender, EventArgs e)
        {
            CurrentPage -= 1;
            this.BindItemsList();
        }

        private DataTable GetDeNghi(int trangThaiXuLy)
        {
            DataTable dataTable = null;
            try
            {
                LoggingServices.LogMessage("Begin GetDeNghi, Trang Thai Xu Ly: " + trangThaiXuLy);
                //SPQuery caml = Camlex.Query().Where(x => (string)x[Constants.FieldTrangThai] == trangThaiXuLy.ToString())
                //                                    .OrderBy(x => new[] { x["ID"] as Camlex.Desc })
                //                                    .ToSPQuery();
                var expressions = new List<Expression<Func<SPListItem, bool>>>();
                expressions.Add(x => ((string)x[Constants.FieldTrangThai]) == trangThaiXuLy.ToString());
                if (!string.IsNullOrEmpty(txtTuKhoa.Text.Trim()))
                    expressions.Add(x => ((string)x[Constants.FieldTitle]).Contains(txtTuKhoa.Text.Trim()));
                if (!string.IsNullOrEmpty(txtCaNhanToChuc.Text.Trim()))
                    expressions.Add(x => ((string)x["Title"]).Contains(txtCaNhanToChuc.Text.Trim()));
                if (!string.IsNullOrEmpty(txtSoDienThoai.Text.Trim()))
                    expressions.Add(x => ((string)x["Title"]).Contains(txtSoDienThoai.Text.Trim()));
                if (!dtcNgayDeNghiDen.IsDateEmpty && !dtcNgayDeNghiTu.IsDateEmpty)
                {
                    expressions.Add(x => (x["Title"]) >= (DataTypes.DateTime)dtcNgayDeNghiTu.SelectedDate.ToString("yyyy-MM-dd"));
                    expressions.Add(x => (x["Title"]) <= (DataTypes.DateTime)dtcNgayDeNghiDen.SelectedDate.ToString("yyyy-MM-dd"));
                }
                //caml.ViewFields = string.Concat("<FieldRef Name='ID' />",                                    
                //                                "<FieldRef Name='Supervisor' />");


                var camlQuery = Camlex.Query().WhereAll(expressions).ToString();
                SPQuery spQuery = new SPQuery();
                spQuery.Query = camlQuery;

                var deNghiUrl = (SPContext.Current.Web.ServerRelativeUrl + Constants.ListUrlDeNghiCapPhep).Replace("//", "/");
                var deNghiList = SPContext.Current.Web.GetList(deNghiUrl);
                dataTable = deNghiList.GetItems(spQuery).GetDataTable();

#if DEBUG
                LoggingServices.LogMessage("Caml query: " + camlQuery);
                LoggingServices.LogMessage("Data count: " + (dataTable != null ? dataTable.Rows.Count : 0));
#endif
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("End GetDeNghi, Trang Thai Xu Ly: " + trangThaiXuLy);
            return dataTable;
        }

        private void UpdateItem(int itemId, TrangThaiXuLy trangThaiXuLy, CapXuLy capXuLy)
        {
            try
            {
                LoggingServices.LogMessage("Begin UpdateItem, item id:" + itemId);
                var web = SPContext.Current.Web;
                var deNghiList = web.GetList((web.ServerRelativeUrl + Constants.ListUrlDeNghiCapPhep).Replace("//", "/"));
                var deNghiItem = deNghiList.GetItemById(itemId);
                deNghiItem[Constants.FieldTrangThai] = (int)trangThaiXuLy;
                deNghiItem[Constants.FieldCapDuyet] = (int)capXuLy;
                deNghiItem.Update();
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("End UpdateItem, item id:" + itemId);
        }

        #endregion Private Function

        #region Repeater List
        protected void repeaterLists_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                DataRowView rowView = (DataRowView)e.Item.DataItem;
                if (rowView != null)
                {
                    string commandAgrument = rowView["ID"].ToString();
                    //int trangThai = int.Parse(rowView[Constants.FieldTrangThai].ToString());
                    Literal literalTitle = (Literal)e.Item.FindControl("literalTitle");
                    literalTitle.Text = rowView[Constants.FieldTitle].ToString();

                    Literal literalCaNhanToChuc = (Literal)e.Item.FindControl("literalCaNhanToChuc");
                    literalCaNhanToChuc.Text = rowView[Constants.FieldCaNhanToChuc].ToString();

                    Literal literalLoaiCapPhep = (Literal)e.Item.FindControl("literalLoaiCapPhep");
                    literalLoaiCapPhep.Text = rowView[Constants.FieldLoaiDeNghi].ToString();

                    Literal literalNgayDeNghi = (Literal)e.Item.FindControl("literalNgayDeNghi");
                    literalNgayDeNghi.Text = rowView[Constants.FieldCreated].ToString();

                    LinkButton lbtViewItem = (LinkButton)e.Item.FindControl("lbtViewItem");
                    //lbtViewItem.CommandName = "ClickLinkButton";
                    //lbtViewItem.CommandArgument = commandAgrument;
                    var deNghiUrl = (SPContext.Current.Web.ServerRelativeUrl + Constants.ListUrlDeNghiCapPhep).Replace("//", "/");
                    var currentPage = SPUtility.GetPageUrlPath(HttpContext.Current);
                    var viewUrl = string.Format(Constants.ConfLinkDispForm, deNghiUrl, commandAgrument, currentPage);
                    lbtViewItem.OnClientClick = viewUrl;

                    switch (CurrentUserRole)
                    {
                        case (int)CapXuLy.NhanVienTiepNhan:
                            if (TrangThai == (int)TrangThaiXuLy.DaTiepNhan)
                            {
                                LinkButton lbtChuyenTruongPhong = (LinkButton)e.Item.FindControl("lbtChuyenTruongPhong");
                                lbtChuyenTruongPhong.CommandName = "ClickChuyenTruongPhong";
                                lbtChuyenTruongPhong.Style.Add("display", "block");
                                lbtChuyenTruongPhong.CommandArgument = commandAgrument;
                                lbtChuyenTruongPhong.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn chuyển hồ sơ này không?')) return false;";
                                LinkButton lblDisable2 = (LinkButton)e.Item.FindControl("lblDisable2");
                                lblDisable2.Style.Add("display", "none");
                            }
                            break;
                        case (int)CapXuLy.TruongPhoPhong:
                            if (TrangThai == (int)TrangThaiXuLy.ChoXuLy)
                            {
                                LinkButton lbtPhanCongHoSo = (LinkButton)e.Item.FindControl("lbtPhanCongHoSo");
                                lbtPhanCongHoSo.CommandName = "ClickPhanCongHoSo";
                                lbtPhanCongHoSo.Style.Add("display", "block");
                                lbtPhanCongHoSo.CommandArgument = commandAgrument;
                                lbtPhanCongHoSo.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn chuyển hồ sơ này không?')) return false;";
                                LinkButton lblDisable2 = (LinkButton)e.Item.FindControl("lblDisable2");
                                lblDisable2.Style.Add("display", "none");
                            }
                            else if (TrangThai == (int)TrangThaiXuLy.ChoTruongPhongDuyet)
                            {
                                LinkButton lbtTrinhLanhDaoSo = (LinkButton)e.Item.FindControl("lbtTrinhLanhDaoSo");
                                lbtTrinhLanhDaoSo.CommandName = "ClickTrinhLanhDaoSo";
                                lbtTrinhLanhDaoSo.Style.Add("display", "block");
                                lbtTrinhLanhDaoSo.CommandArgument = commandAgrument;
                                lbtTrinhLanhDaoSo.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn trình lãnh đạo hồ sơ này không?')) return false;";
                                LinkButton lblDisable1 = (LinkButton)e.Item.FindControl("lblDisable1");
                                lblDisable1.Style.Add("display", "none");

                                //LinkButton lbtTuChoiHoSo = (LinkButton)e.Item.FindControl("lbtTuChoiHoSo");
                                //lbtTuChoiHoSo.CommandName = "ClickTuChoiHoSo";
                                //lbtTuChoiHoSo.Visible = true;
                                //lbtTuChoiHoSo.CommandArgument = commandAgrument;
                                //lbtTuChoiHoSo.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn từ chối hồ sơ này không?')) return false;";
                            }
                            break;
                        case (int)CapXuLy.CanBoXuLy:
                            if (TrangThai == (int)TrangThaiXuLy.ChoXuLy || TrangThai == (int)TrangThaiXuLy.DaPhanCong)
                            {
                                LinkButton lbtYeuCauBoSung = (LinkButton)e.Item.FindControl("lbtYeuCauBoSung");
                                lbtYeuCauBoSung.CommandName = "ClickYeuCauBoSung";
                                lbtYeuCauBoSung.Style.Add("display", "block");
                                lbtYeuCauBoSung.CommandArgument = commandAgrument;
                                lbtYeuCauBoSung.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn yêu cầu hồ sơ này gửi bổ sung không?')) return false;";

                                LinkButton lblDisable2 = (LinkButton)e.Item.FindControl("lblDisable2");
                                lblDisable2.Style.Add("display", "none");

                                LinkButton lbtTrinhTruongPhoPQLHT = (LinkButton)e.Item.FindControl("lbtTrinhTruongPhoPQLHT");
                                lbtTrinhTruongPhoPQLHT.CommandName = "ClickTrinhTruongPhoPQLHT";
                                lbtTrinhTruongPhoPQLHT.Style.Add("display", "block");
                                lbtTrinhTruongPhoPQLHT.CommandArgument = commandAgrument;
                                lbtTrinhTruongPhoPQLHT.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn trình hồ sơ này không?')) return false;";

                                LinkButton lblDisable1 = (LinkButton)e.Item.FindControl("lblDisable1");
                                lblDisable1.Style.Add("display", "none");
                            }
                            break;
                        case (int)CapXuLy.LanhDaoSo:
                            if (TrangThai == (int)TrangThaiXuLy.ChoLanhDaoDuyet)
                            {
                                LinkButton lbtDuyetHoSo = (LinkButton)e.Item.FindControl("lbtDuyetHoSo");
                                lbtDuyetHoSo.CommandName = "ClickDuyetHoSo";
                                lbtDuyetHoSo.Style.Add("display", "block");
                                lbtDuyetHoSo.CommandArgument = commandAgrument;
                                lbtDuyetHoSo.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn duyệt thuận hồ sơ này không?')) return false;";

                                LinkButton lbtTuChoiHoSo = (LinkButton)e.Item.FindControl("lbtTuChoiHoSo");
                                lbtTuChoiHoSo.CommandName = "ClickTuChoiHoSo";
                                lbtTuChoiHoSo.Style.Add("display", "block");
                                lbtTuChoiHoSo.CommandArgument = commandAgrument;
                                lbtTuChoiHoSo.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn từ chối hồ sơ này không?')) return false;";

                                LinkButton lblDisable1 = (LinkButton)e.Item.FindControl("lblDisable1");
                                lblDisable1.Style.Add("display", "none");
                                LinkButton lblDisable2 = (LinkButton)e.Item.FindControl("lblDisable2");
                                lblDisable2.Style.Add("display", "none");
                            }
                                break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
        }

        protected void repeaterLists_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int commandText = int.Parse(e.CommandArgument.ToString());
            try
            {
                LoggingServices.LogMessage("Begin link button click, command: " + e.CommandName + ", item id: " + commandText);
                if (e.CommandName == "ClickChuyenTruongPhong")
                    UpdateItem(commandText, TrangThaiXuLy.ChoXuLy, CapXuLy.TruongPhoPhong);
                else if (e.CommandName == "ClickPhanCongHoSo")// xử lý trên form phân công xử lý hồ sơ
                    UpdateItem(commandText, TrangThaiXuLy.DaPhanCong, CapXuLy.CanBoXuLy);
                else if (e.CommandName == "ClickTrinhLanhDaoSo")
                    UpdateItem(commandText, TrangThaiXuLy.ChoLanhDaoDuyet, CapXuLy.LanhDaoSo);
                else if (e.CommandName == "ClickTuChoiHoSo")
                    UpdateItem(commandText, TrangThaiXuLy.HoSoBiTuChoi, CapXuLy.CaNhanToChuc);
                else if (e.CommandName == "ClickYeuCauBoSung")// xử lý trên form yêu cầu bổ sung hồ sơ
                    UpdateItem(commandText, TrangThaiXuLy.HoSoBiTuChoi, CapXuLy.CaNhanToChuc);
                else if (e.CommandName == "ClickTrinhTruongPhoPQLHT")
                    UpdateItem(commandText, TrangThaiXuLy.ChoTruongPhongDuyet, CapXuLy.TruongPhoPhong);
                else if (e.CommandName == "ClickDuyetHoSo")
                    UpdateItem(commandText, TrangThaiXuLy.HoSoDuocDuyet, CapXuLy.NhanVienTiepNhan);
                this.BindItemsList();
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("End link button click, command: " + e.CommandName + ", item id: " + commandText);
        }
        #endregion Repeater List
    }
}
