using CamlexNET;
using CamlexNET.Impl.Helpers;
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
                if (CurrentUserRole == CapXuLy.MotCua)
                {
                    divAddNew.Visible = true;
                    var deNghiUrl = (SPContext.Current.Web.ServerRelativeUrl + Constants.ListUrlDeNghiCapPhep).Replace("//", "/");
                    var currentPage = SPUtility.GetPageUrlPath(HttpContext.Current);
                    //var viewUrl = string.Format(Constants.ConfLinkNewForm, deNghiUrl, currentPage);
                    //lbtAddNew.OnClientClick = viewUrl;
                    var viewUrl = string.Format(Constants.ConfLinkPageNewForm, deNghiUrl, currentPage);
                    hplAddNew.NavigateUrl = viewUrl;
                }
                hplTrangChu.NavigateUrl = LinkTrangChu;
                hplHoSoChoTiepNhan.NavigateUrl = LinkHoSoChoTiepNhan;
                hplHoSoDaTiepNhan.NavigateUrl = LinkHoSoDaTiepNhan;
                hplHoSoChoXuLy.NavigateUrl = LinkHoSoChoXuLy;
                hplHoSoDangXuLy.NavigateUrl = LinkHoSoDangXuLy;
                hplHoSoChoBoSung.NavigateUrl = LinkHoSoChoBoSung;
                hplHoSoChoDuyet.NavigateUrl = LinkHoSoChoDuyet;
                hplHoSoChoCapPhep.NavigateUrl = LinkHoSoChoCapPhep;
                hplHoSoDuocCapPhep.NavigateUrl = LinkHoSoDuocCapPhep;
                hplHoSoBiTuChoi.NavigateUrl = LinkHoSoBiTuChoi;
                hplHoSoDaHoanThanh.NavigateUrl = LinkHoSoDaHoanThanh;
                hplHoSoChuaHoanThanh.NavigateUrl = LinkHoSoChuaHoanThanh;
                hplThongKeBaoCao.NavigateUrl = LinkThongKeBaoCao;
            }
        }

        #region Private Properties
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
         WebDescription("Nhập tiêu đề"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public string DeNghiTitle { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Trạng thái xử lý"),
         WebDescription("Nhập trạng thái xử lý"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public int TrangThai { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Phân trang"),
         WebDescription("Cấu hình phân trang"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public int PageSize { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Link Trang chủ"),
         WebDescription("Linh trang chủ"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public string LinkTrangChu { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Link Hồ sơ chờ tiếp nhận"),
         WebDescription("Một cửa chờ tiếp nhận"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public string LinkHoSoChoTiepNhan { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Link Hồ sơ đã tiếp nhận"),
         WebDescription("Một cửa đang tiếp nhận"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public string LinkHoSoDaTiepNhan { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Link Hồ sơ chờ xử lý"),
         WebDescription("Chờ Trưởng phó phòng phân công / Cán bộ xử lý"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public string LinkHoSoChoXuLy { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Link Hồ sơ đang xử lý"),
         WebDescription("Trưởng phó phòng đã phân công / Cán bộ tiếp nhận xử lý"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public string LinkHoSoDangXuLy { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Link Hồ sơ chờ bổ sung"),
         WebDescription("Hồ sơ chờ bổ sung"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public string LinkHoSoChoBoSung { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Link Hồ sơ chờ duyệt"),
         WebDescription("Trình trưởng phó phòng QLHT"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public string LinkHoSoChoDuyet { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Link Hồ sơ chờ cấp phép"),
         WebDescription("Trình lãnh đạo sở"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public string LinkHoSoChoCapPhep { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Link Hồ sơ được cấp phép"),
         WebDescription("Hồ sơ đã duyệt"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public string LinkHoSoDuocCapPhep { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Link Hồ sơ bị từ chối"),
         WebDescription("Hồ sơ bị từ chối"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public string LinkHoSoBiTuChoi { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Link Hồ sơ đã hoàn thành"),
         WebDescription("Hồ sơ đã xác nhận hoàn thành"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public string LinkHoSoDaHoanThanh { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Link Hồ sơ chưa hoàn thành"),
         WebDescription("Hồ sơ đã xác nhận chưa hoàn thành"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public string LinkHoSoChuaHoanThanh { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Link Thống kê báo cáo"),
         WebDescription("Thống kê báo cáo"),
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
                else
                {
                    _PageDataSource = null;
                    ViewState["TotalPages"] = 0;

                    //this.lblPageInfo.Text = "Page " + (CurrentPage + 1) + " of " + _PageDataSource.PageCount;
                    this.lbtnPrevious.Enabled = false;
                    this.lbtnNext.Enabled = false;
                    this.lbtnFirst.Enabled = false;
                    this.lbtnLast.Enabled = false;

                    this.repeaterLists.DataSource = _PageDataSource;
                    this.repeaterLists.DataBind();
                    this.DoPaging();
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

                var searchConditions = new List<Expression<Func<SPListItem, bool>>>();
                searchConditions.Add(x => ((string)x[Constants.FieldTrangThai]) == trangThaiXuLy.ToString());
                if (!string.IsNullOrEmpty(txtTuKhoa.Text.Trim()))
                    searchConditions.Add(x => ((string)x[Constants.FieldTitle]).Contains(txtTuKhoa.Text.Trim()));
                if (!string.IsNullOrEmpty(txtCaNhanToChuc.Text.Trim()))
                    searchConditions.Add(x => ((string)x[Constants.FieldCaNhanToChuc]).Contains(txtCaNhanToChuc.Text.Trim()));
                if (!string.IsNullOrEmpty(txtSoDienThoai.Text.Trim()))
                    searchConditions.Add(x => ((string)x[Constants.FieldSoDienThoai]).Contains(txtSoDienThoai.Text.Trim()));
                if (!dtcNgayDeNghiDen.IsDateEmpty && !dtcNgayDeNghiTu.IsDateEmpty)
                {
                    searchConditions.Add(x => (x[Constants.FieldNgayNopHoSo]) >= (DataTypes.DateTime)dtcNgayDeNghiTu.SelectedDate.ToString("yyyy-MM-dd"));
                    searchConditions.Add(x => (x[Constants.FieldNgayNopHoSo]) <= (DataTypes.DateTime)dtcNgayDeNghiDen.SelectedDate.ToString("yyyy-MM-dd"));
                }

                var searchExpr = ExpressionsHelper.CombineAnd(searchConditions);

                var userConditions = new List<Expression<Func<SPListItem, bool>>>();
                switch (CurrentUserRole)
                {
                    case CapXuLy.CaNhanToChuc:
                        break;
                    case CapXuLy.MotCua:
                        userConditions.Add(x => x["MotCuaUser"] == (DataTypes.UserId)SPContext.Current.Web.CurrentUser.ID.ToString());
                        userConditions.Add(x => (DataTypes.User)x["MotCuaUser"] == null);
                        break;
                    case CapXuLy.TruongPhoPhong:
                        userConditions.Add(x => x["TruongPhongUser"] == (DataTypes.UserId)SPContext.Current.Web.CurrentUser.ID.ToString());
                        userConditions.Add(x => (DataTypes.User)x["TruongPhongUser"] == null);
                        break;
                    case CapXuLy.CanBo:
                        userConditions.Add(x => x["CanBoUser"] == (DataTypes.UserId)SPContext.Current.Web.CurrentUser.ID.ToString());
                        userConditions.Add(x => (DataTypes.User)x["CanBoUser"] == null);
                        break;
                    case CapXuLy.LanhDaoSo:
                        userConditions.Add(x => x["LanhDaoUser"] == (DataTypes.UserId)SPContext.Current.Web.CurrentUser.ID.ToString());
                        userConditions.Add(x => (DataTypes.User)x["LanhDaoUser"] == null);
                        break;
                    case CapXuLy.VanPhongSo:
                        break;
                    default:
                        break;
                }

                Expression<Func<Microsoft.SharePoint.SPListItem, bool>> userExpr = null;
                if (userConditions != null)
                    userExpr = ExpressionsHelper.CombineOr(userConditions);

                var expressions = new List<Expression<Func<SPListItem, bool>>>();
                expressions.Add(searchExpr);
                if (userExpr != null)
                    expressions.Add(userExpr);

                #region Backup
                /*
                expressions.Add(x => ((string)x[Constants.FieldTrangThai]) == trangThaiXuLy.ToString());
                if (!string.IsNullOrEmpty(txtTuKhoa.Text.Trim()))
                    expressions.Add(x => ((string)x[Constants.FieldTitle]).Contains(txtTuKhoa.Text.Trim()));
                if (!string.IsNullOrEmpty(txtCaNhanToChuc.Text.Trim()))
                    expressions.Add(x => ((string)x[Constants.FieldCaNhanToChuc]).Contains(txtCaNhanToChuc.Text.Trim()));
                if (!string.IsNullOrEmpty(txtSoDienThoai.Text.Trim()))
                    expressions.Add(x => ((string)x[Constants.FieldSoDienThoai]).Contains(txtSoDienThoai.Text.Trim()));
                if (!dtcNgayDeNghiDen.IsDateEmpty && !dtcNgayDeNghiTu.IsDateEmpty)
                {
                    expressions.Add(x => (x[Constants.FieldNgayNopHoSo]) >= (DataTypes.DateTime)dtcNgayDeNghiTu.SelectedDate.ToString("yyyy-MM-dd"));
                    expressions.Add(x => (x[Constants.FieldNgayNopHoSo]) <= (DataTypes.DateTime)dtcNgayDeNghiDen.SelectedDate.ToString("yyyy-MM-dd"));
                }
                //caml.ViewFields = string.Concat("<FieldRef Name='ID' />",                                    
                //                                "<FieldRef Name='Supervisor' />");
                */
                #endregion Backup

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

        private void UpdateItem(int itemId, TrangThaiHoSo trangThaiXuLy, CapXuLy capXuLy)
        {
            try
            {
                LoggingServices.LogMessage("Begin UpdateItem, item id:" + itemId);
                var web = SPContext.Current.Web;
                var deNghiList = web.GetList((web.ServerRelativeUrl + Constants.ListUrlDeNghiCapPhep).Replace("//", "/"));
                var deNghiItem = deNghiList.GetItemById(itemId);
                deNghiItem[Constants.FieldTrangThai] = (int)trangThaiXuLy;
                deNghiItem[Constants.FieldCapDuyet] = capXuLy;
                var currentUserId = SPContext.Current.Web.CurrentUser.ID;
                if (trangThaiXuLy == TrangThaiHoSo.DaTiepNhan)
                    deNghiItem[Constants.FieldMotCuaUser] = currentUserId;
                if (trangThaiXuLy == TrangThaiHoSo.DangXuLy)
                    deNghiItem[Constants.FieldCanBoUser] = currentUserId;
                deNghiItem.Update();
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("End UpdateItem, item id:" + itemId);
        }

        void InPhieuBienNhan(PrintType type, string itemId)
        {
            try
            {
                var web = SPContext.Current.Web;
                var deNghiList = web.GetList((web.ServerRelativeUrl + Constants.ListUrlDeNghiCapPhep).Replace("//", "/"));
                string wordLicFile = Microsoft.SharePoint.Utilities.SPUtility.GetVersionedGenericSetupPath(Constants.ConfWordLicFile, 15);
                Aspose.Words.License wordLic = new Aspose.Words.License();
                wordLic.SetLicense(wordLicFile);

                string templateFile = string.Empty;
                SPQuery caml = Camlex.Query().Where(x => x["ID"] == (DataTypes.Counter)itemId)
                                                    .ToSPQuery();
                if (type == PrintType.PhieuBienNhan)
                    templateFile = Microsoft.SharePoint.Utilities.SPUtility.GetVersionedGenericSetupPath(Constants.ConfWordBienNhanTemplate, 15);
                else if (type == PrintType.GiayCapPhep)
                    templateFile = Microsoft.SharePoint.Utilities.SPUtility.GetVersionedGenericSetupPath(Constants.ConfWordGiayPhepTemplate, 15);

                Aspose.Words.Document doc = new Aspose.Words.Document(templateFile);
                var deNghiItem = deNghiList.GetItems(caml).GetDataTable();

                doc.MailMerge.Execute(deNghiItem);

                string fileName = string.Format(DateTime.Now.ToString("yyyyMMdd_hhmmss"));

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                doc.Save(ms, Aspose.Words.SaveFormat.Docx);
                this.Parent.Page.Response.Clear();
                this.Parent.Page.Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                this.Parent.Page.Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.docx", fileName));
                this.Parent.Page.Response.BinaryWrite(ms.ToArray());
                this.Parent.Page.Response.Flush();
                this.Parent.Page.Response.Close();
                this.Parent.Page.Response.End();
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
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
                    literalNgayDeNghi.Text = rowView[Constants.FieldNgayNopHoSo].ToString();

                    Literal literalTrangThai = (Literal)e.Item.FindControl("literalTrangThai");
                    literalTrangThai.Text = rowView[Constants.FieldTrangThaiText].ToString();

                    HyperLink hplViewItem = (HyperLink)e.Item.FindControl("hplViewItem");
                    var deNghiUrl = (SPContext.Current.Web.ServerRelativeUrl + Constants.ListUrlDeNghiCapPhep).Replace("//", "/");
                    var currentPage = SPUtility.GetPageUrlPath(HttpContext.Current);
                    var viewUrl = string.Format(Constants.ConfLinkPageDispForm, deNghiUrl, commandAgrument, currentPage);
                    hplViewItem.NavigateUrl = viewUrl;

                    LinkButton lbtPrint = (LinkButton)e.Item.FindControl("lbtPrint");
                    lbtPrint.CommandArgument = commandAgrument;

                    LinkButton lbtDisable1 = (LinkButton)e.Item.FindControl("lbtDisable1");
                    LinkButton lbtDisable2 = (LinkButton)e.Item.FindControl("lbtDisable2");
                    LinkButton lbtDisable3 = (LinkButton)e.Item.FindControl("lbtDisable3");
                    LinkButton lbtDisable4 = (LinkButton)e.Item.FindControl("lbtDisable4");

                    switch (CurrentUserRole)
                    {
                        case CapXuLy.MotCua:
                            if (TrangThai == (int)TrangThaiHoSo.ChoTiepNhan)
                            {
                                LinkButton lbtTiepNhan = (LinkButton)e.Item.FindControl("lbtTiepNhan");
                                lbtTiepNhan.CommandName = "TiepNhanHoSo";
                                lbtTiepNhan.Style.Add("display", "block");
                                lbtTiepNhan.CommandArgument = commandAgrument;
                                lbtTiepNhan.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn tiếp nhận hồ sơ này không?')) return false;";
                                lbtDisable1.Style.Add("display", "none");

                                HyperLink hplTuChoiHoSo = (HyperLink)e.Item.FindControl("hplTuChoiHoSo");
                                hplTuChoiHoSo.Style.Add("display", "block");
                                hplTuChoiHoSo.NavigateUrl = string.Format("{0}&Action={1}&Atocken={2}", viewUrl, Constants.ConfActionTC, Constants.ConfQueryStringTC);
                                lbtDisable4.Style.Add("display", "none");
                            }
                            else if (TrangThai == (int)TrangThaiHoSo.DaTiepNhan)
                            {
                                lbtPrint.Style.Add("display", "block");
                                lbtDisable1.Style.Add("display", "none");
                                lbtPrint.CommandName = "InBienNhan";

                                LinkButton lbtChuyenTruongPhong = (LinkButton)e.Item.FindControl("lbtChuyenTruongPhong");
                                lbtChuyenTruongPhong.CommandName = "ChuyenTruongPhoPhong";
                                lbtChuyenTruongPhong.Style.Add("display", "block");
                                lbtChuyenTruongPhong.CommandArgument = commandAgrument;
                                lbtChuyenTruongPhong.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn chuyển hồ sơ này không?')) return false;";
                                lbtDisable2.Style.Add("display", "none");

                                HyperLink hplTuChoiHoSo = (HyperLink)e.Item.FindControl("hplTuChoiHoSo");
                                hplTuChoiHoSo.Style.Add("display", "block");
                                hplTuChoiHoSo.NavigateUrl = string.Format("{0}&Action={1}&Atocken={2}", viewUrl, Constants.ConfActionTC, Constants.ConfQueryStringTC);
                                lbtDisable4.Style.Add("display", "none");
                            }
                            else if (TrangThai == (int)TrangThaiHoSo.DuocCapPhep)
                            {
                                LinkButton lbtHoanThanh = (LinkButton)e.Item.FindControl("lbtHoanThanh");
                                lbtHoanThanh.CommandName = "XacNhanHoanThanh";
                                lbtHoanThanh.Style.Add("display", "block");
                                lbtHoanThanh.CommandArgument = commandAgrument;
                                lbtHoanThanh.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn xác nhận hồ sơ này đã hoàn thành không?')) return false;";
                                lbtDisable1.Style.Add("display", "none");

                                LinkButton lbtChuaHoanThanh = (LinkButton)e.Item.FindControl("lbtChuaHoanThanh");
                                lbtChuaHoanThanh.CommandName = "XacNhanChuaHoanThanh";
                                lbtChuaHoanThanh.Style.Add("display", "block");
                                lbtChuaHoanThanh.CommandArgument = commandAgrument;
                                lbtChuaHoanThanh.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn xác nhận hồ sơ này chưa hoàn thành không?')) return false;";
                                lbtDisable2.Style.Add("display", "none");
                            }
                            else if (TrangThai == (int)TrangThaiHoSo.HoanThanh || TrangThai == (int)TrangThaiHoSo.ChuaHoanThanh)
                            {
                                lbtPrint.Style.Add("display", "block");
                                lbtDisable1.Style.Add("display", "none");
                                lbtPrint.CommandName = "InGiayPhep";
                            }
                            break;
                        case CapXuLy.TruongPhoPhong:
                            if (TrangThai == (int)TrangThaiHoSo.ChoXuLy)
                            {
                                HyperLink hplPhanCongHoSo = (HyperLink)e.Item.FindControl("hplPhanCongHoSo");
                                hplPhanCongHoSo.Style.Add("display", "block");
                                hplPhanCongHoSo.NavigateUrl = string.Format("{0}&Action={1}&Atocken={2}", viewUrl, Constants.ConfActionPC, Constants.ConfQueryStringPC);
                                lbtDisable3.Style.Add("display", "none");

                                HyperLink hplTuChoiHoSo = (HyperLink)e.Item.FindControl("hplTuChoiHoSo");
                                hplTuChoiHoSo.Style.Add("display", "block");
                                hplTuChoiHoSo.NavigateUrl = string.Format("{0}&Action={1}&Atocken={2}", viewUrl, Constants.ConfActionTC, Constants.ConfQueryStringTC);
                                lbtDisable4.Style.Add("display", "none");
                            }
                            else if (TrangThai == (int)TrangThaiHoSo.ChoDuyet)
                            {
                                LinkButton lbtTrinhLanhDaoSo = (LinkButton)e.Item.FindControl("lbtTrinhLanhDaoSo");
                                lbtTrinhLanhDaoSo.CommandName = "TrinhLanhDaoSo";
                                lbtTrinhLanhDaoSo.Style.Add("display", "block");
                                lbtTrinhLanhDaoSo.CommandArgument = commandAgrument;
                                lbtTrinhLanhDaoSo.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn trình lãnh đạo hồ sơ này không?')) return false;";
                                lbtDisable2.Style.Add("display", "none");

                                HyperLink hplTuChoiHoSo = (HyperLink)e.Item.FindControl("hplTuChoiHoSo");
                                hplTuChoiHoSo.Style.Add("display", "block");
                                hplTuChoiHoSo.NavigateUrl = string.Format("{0}&Action={1}&Atocken={2}", viewUrl, Constants.ConfActionTC, Constants.ConfQueryStringTC);
                                lbtDisable4.Style.Add("display", "none");
                            }
                            break;
                        case CapXuLy.CanBo:
                            if (TrangThai == (int)TrangThaiHoSo.ChoXuLy)
                            {
                                LinkButton lbtTiepNhan = (LinkButton)e.Item.FindControl("lbtTiepNhan");
                                lbtTiepNhan.CommandName = "TiepNhanXuLy";
                                lbtTiepNhan.Style.Add("display", "block");
                                lbtTiepNhan.CommandArgument = commandAgrument;
                                lbtTiepNhan.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn tiếp nhận hồ sơ này không?')) return false;";
                                lbtDisable1.Style.Add("display", "none");
                            }
                            else if (TrangThai == (int)TrangThaiHoSo.DangXuLy)
                            {
                                HyperLink hplYeuCauBoSung = (HyperLink)e.Item.FindControl("hplYeuCauBoSung");
                                hplYeuCauBoSung.Style.Add("display", "block");
                                hplYeuCauBoSung.NavigateUrl = string.Format("{0}&Action={1}&Atocken={2}", viewUrl, Constants.ConfActionBS, Constants.ConfQueryStringBS);
                                lbtDisable3.Style.Add("display", "none");

                                LinkButton lbtTrinhTruongPhoPQLHT = (LinkButton)e.Item.FindControl("lbtTrinhTruongPhoPQLHT");
                                lbtTrinhTruongPhoPQLHT.CommandName = "TrinhTruongPhoPhong";
                                lbtTrinhTruongPhoPQLHT.Style.Add("display", "block");
                                lbtTrinhTruongPhoPQLHT.CommandArgument = commandAgrument;
                                lbtTrinhTruongPhoPQLHT.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn trình hồ sơ này không?')) return false;";
                                lbtDisable2.Style.Add("display", "none");

                                HyperLink hplTuChoiHoSo = (HyperLink)e.Item.FindControl("hplTuChoiHoSo");
                                hplTuChoiHoSo.Style.Add("display", "block");
                                hplTuChoiHoSo.NavigateUrl = string.Format("{0}&Action={1}&Atocken={2}", viewUrl, Constants.ConfActionTC, Constants.ConfQueryStringTC);
                                lbtDisable4.Style.Add("display", "none");
                            }
                            break;
                        case CapXuLy.LanhDaoSo:
                            if (TrangThai == (int)TrangThaiHoSo.ChoCapPhep)
                            {
                                LinkButton lbtDuyetHoSo = (LinkButton)e.Item.FindControl("lbtDuyetHoSo");
                                lbtDuyetHoSo.CommandName = "DuyetCapPhepHoSo";
                                lbtDuyetHoSo.Style.Add("display", "block");
                                lbtDuyetHoSo.CommandArgument = commandAgrument;
                                lbtDuyetHoSo.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn duyệt thuận hồ sơ này không?')) return false;";
                                lbtDisable2.Style.Add("display", "none");

                                HyperLink hplTuChoiHoSo = (HyperLink)e.Item.FindControl("hplTuChoiHoSo");
                                hplTuChoiHoSo.Style.Add("display", "block");
                                hplTuChoiHoSo.NavigateUrl = string.Format("{0}&Action={1}&Atocken={2}", viewUrl, Constants.ConfActionTC, Constants.ConfQueryStringTC);
                                lbtDisable4.Style.Add("display", "none");
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
            var commadName = e.CommandName;
            var web = SPContext.Current.Web;
            var printType = PrintType.PhieuBienNhan;
            try
            {
                LoggingServices.LogMessage("Begin link button click, command: " + commadName + ", item id: " + commandText);
                var trangThaiHoSo = TrangThaiHoSo.ChoTiepNhan;
                var capXuLy = CapXuLy.MotCua;
                switch (commadName)
                {
                    case "InBienNhan":
                        printType = PrintType.PhieuBienNhan;
                        break;
                    case "InGiayPhep":
                        printType = PrintType.GiayCapPhep;
                        break;
                    case "TiepNhanHoSo":
                        trangThaiHoSo = TrangThaiHoSo.DaTiepNhan;
                        printType = PrintType.PhieuBienNhan;
                        UpdateItem(commandText, trangThaiHoSo, capXuLy);
                        break;
                    case "ChuyenTruongPhoPhong":
                        trangThaiHoSo = TrangThaiHoSo.ChoXuLy;
                        UpdateItem(commandText, trangThaiHoSo, capXuLy);
                        break;
                    case "XacNhanHoanThanh":
                        trangThaiHoSo = TrangThaiHoSo.HoanThanh;
                        printType = PrintType.GiayCapPhep;
                        UpdateItem(commandText, trangThaiHoSo, capXuLy);
                        break;
                    case "XacNhanChuaHoanThanh":
                        trangThaiHoSo = TrangThaiHoSo.ChuaHoanThanh;
                        UpdateItem(commandText, trangThaiHoSo, capXuLy);
                        break;
                    case "TrinhLanhDaoSo":
                        trangThaiHoSo = TrangThaiHoSo.ChoCapPhep;
                        UpdateItem(commandText, trangThaiHoSo, capXuLy);
                        capXuLy = CapXuLy.LanhDaoSo;
                        break;
                    case "TiepNhanXuLy":
                        trangThaiHoSo = TrangThaiHoSo.DangXuLy;
                        capXuLy = CapXuLy.CanBo;
                        UpdateItem(commandText, trangThaiHoSo, capXuLy);
                        break;
                    case "TrinhTruongPhoPhong":
                        trangThaiHoSo = TrangThaiHoSo.ChoDuyet;
                        capXuLy = CapXuLy.TruongPhoPhong;
                        UpdateItem(commandText, trangThaiHoSo, capXuLy);
                        break;
                    case "DuyetCapPhepHoSo":
                        trangThaiHoSo = TrangThaiHoSo.DuocCapPhep;
                        capXuLy = CapXuLy.MotCua;
                        UpdateItem(commandText, trangThaiHoSo, capXuLy);
                        break;
                }
                DeNghiHelper.AddDeNghiHistory(web, capXuLy, commandText, e.CommandName, string.Empty);
                this.BindItemsList();
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("End link button click, command: " + e.CommandName + ", item id: " + commandText);
            //Print
            if (commadName == "InBienNhan" || commadName == "InGiayPhep")
                InPhieuBienNhan(printType, commandText.ToString());
        }
        #endregion Repeater List
    }
}
