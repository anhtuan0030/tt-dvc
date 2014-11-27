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

namespace LongAn.DVC.WebParts.DeNghiListViewUsers
{
    [ToolboxItemAttribute(false)]
    public partial class DeNghiListViewUsers : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public DeNghiListViewUsers()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
            repeaterLists.ItemDataBound +=repeaterLists_ItemDataBound;
            repeaterLists.ItemCommand +=repeaterLists_ItemCommand;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                //Link & Title
                literalDeNghiTitle.Text = DeNghiTitle;
                var currentPage = SPUtility.GetPageUrlPath(HttpContext.Current);
                var deNghiUrl = (SPContext.Current.Web.ServerRelativeUrl + Constants.ListUrlDeNghiCapPhep).Replace("//", "/");
                hdfCurrentUrl.Value = currentPage;
                hdfDeNghiUrl.Value = deNghiUrl;
                //Enable add new hyperlink
                var cauHinhs = DeNghiHelper.GetCauHinh(Constants.CauHinh_Start);
                var isMember = false;
                foreach (var item in cauHinhs)
                {
                    isMember = DeNghiHelper.IsCurrentUserInGroup(SPContext.Current.Web, item.SPGroup);
                    if (isMember)
                    {
                        var newUrl = string.Format(Constants.ConfLinkPageNewForm, deNghiUrl, currentPage);
                        hplAddNew.NavigateUrl = newUrl;
                        divAddNew.Visible = true;
                        break;
                    }
                }
                this.BindItemsList();
            }
        }

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
         WebDisplayName("Phân trang"),
         WebDescription("Cấu hình phân trang"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public int PageSize { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Tiêu đề"),
         WebDescription("Nhập tiêu đề"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public string DeNghiTitle { get; set; }

        #endregion WebPart Properties
        
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

        private void BindItemsList()
        {
            DataTable dataTable = this.GetDeNghi();
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

                    if (dataTable.Rows.Count > PageSize)
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

        protected void repeaterLists_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                DataRowView rowView = (DataRowView)e.Item.DataItem;
                if (rowView != null)
                {
                    var literalTitle = (Literal)e.Item.FindControl("literalTitle");
                    literalTitle.Text = rowView[Fields.Title].ToString();

                    var literalCaNhanToChuc = (Literal)e.Item.FindControl("literalCaNhanToChuc");
                    literalCaNhanToChuc.Text = rowView[Fields.CaNhanToChuc].ToString();

                    var literalLoaiCapPhep = (Literal)e.Item.FindControl("literalLoaiCapPhep");
                    literalLoaiCapPhep.Text = rowView[Fields.LoaiDeNghi].ToString();

                    var literalNgayDeNghi = (Literal)e.Item.FindControl("literalNgayDeNghi");
                    literalNgayDeNghi.Text = string.IsNullOrEmpty(rowView[Fields.NgayNopHoSo].ToString()) ? string.Empty : Convert.ToDateTime(rowView[Fields.NgayNopHoSo].ToString()).ToString("dd/MM/yyyy");

                    var literalTrangThai = (Literal)e.Item.FindControl("literalTrangThai");
                    literalTrangThai.Text = rowView[Fields.TenTrangThaiRef].ToString();

                    var hplXuLy = (HyperLink)e.Item.FindControl("hplXuLy");
                    var startEnd = DeNghiHelper.GetValueFormCauHinhCal(rowView[Fields.CauHinhCalRef].ToString(), Fields.StartEnd);
                    var rowId = rowView["ID"].ToString();
                    if (startEnd == Constants.CauHinh_Start || startEnd == Constants.CauHinh_YCBS)
                    {
                        hplXuLy.NavigateUrl = string.Format(Constants.ConfLinkPageEditForm, hdfDeNghiUrl.Value, rowId, hdfCurrentUrl.Value);
                    }
                    else
                    {
                        hplXuLy.NavigateUrl = string.Format(Constants.ConfLinkPageDispForm, hdfDeNghiUrl.Value, rowId, hdfCurrentUrl.Value);
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
            string commandText = e.CommandArgument.ToString();
            if (e.CommandName == "OnDeleteItemClick")
            {
                try
                {
                    LoggingServices.LogMessage("Begin OnDeleteItemClick, item id:" + commandText);
                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {
                        using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                        {
                            using (SPWeb web = site.OpenWeb(SPContext.Current.Web.ID))
                            {
                                web.AllowUnsafeUpdates = true;
                                var deNghiList = web.GetList((web.ServerRelativeUrl + Constants.ListUrlDeNghiCapPhep).Replace("//", "/"));
                                var deNghiItem = deNghiList.GetItemById(int.Parse(commandText));
                                deNghiItem.Delete();
                                web.AllowUnsafeUpdates = false;
                            }
                        }
                    });
                    this.BindItemsList();
                }
                catch (Exception ex)
                {
                    LoggingServices.LogException(ex);
                }
                LoggingServices.LogMessage("End OnDeleteItemClick, item id:" + commandText);
            }
            else if (e.CommandName == "OnNopHoSoClick")
            {
                try
                {
                    LoggingServices.LogMessage("Begin OnNopHoSoClick, item id:" + commandText);
                    var web = SPContext.Current.Web;
                    var deNghiList = web.GetList((web.ServerRelativeUrl + Constants.ListUrlDeNghiCapPhep).Replace("//", "/"));
                    var deNghiItem = deNghiList.GetItemById(int.Parse(commandText));
                    deNghiItem[Constants.FieldTrangThai] = (int)TrangThaiHoSo.ChoTiepNhan;
                    deNghiItem[Constants.FieldCapDuyet] = (int)CapXuLy.MotCua;
                    deNghiItem[Constants.FieldNgayNopHoSo] = DateTime.Now;
                    deNghiItem.Update();
                    DeNghiHelper.AddDeNghiHistory(web, CapXuLy.CaNhanToChuc, int.Parse(commandText), HanhDong.NopHoSo.ToString(), string.Empty);
                    this.BindItemsList();
                }
                catch (Exception ex)
                {
                    LoggingServices.LogException(ex);
                }
                LoggingServices.LogMessage("End OnNopHoSoClick, item id:" + commandText);
            }
        }

        DataTable GetDeNghi()
        {
            DataTable dataTable = null;
            try
            {
                LoggingServices.LogMessage("Begin GetDeNghi-User");

                var andConditions = new List<Expression<Func<SPListItem, bool>>>();
                if (!string.IsNullOrEmpty(txtTuKhoa.Text.Trim()))
                    andConditions.Add(x => ((string)x[Fields.Title]).Contains(txtTuKhoa.Text.Trim()));
                if (!string.IsNullOrEmpty(txtCaNhanToChuc.Text.Trim()))
                    andConditions.Add(x => ((string)x[Fields.CaNhanToChuc]).Contains(txtCaNhanToChuc.Text.Trim()));
                if (!string.IsNullOrEmpty(txtSoDienThoai.Text.Trim()))
                    andConditions.Add(x => ((string)x[Fields.DienThoai]).Contains(txtSoDienThoai.Text.Trim()));
                if (!dtcNgayDeNghiDen.IsDateEmpty && !dtcNgayDeNghiTu.IsDateEmpty)
                {
                    andConditions.Add(x => (x[Fields.NgayNopHoSo]) >= (DataTypes.DateTime)dtcNgayDeNghiTu.SelectedDate.ToString("yyyy-MM-dd"));
                    andConditions.Add(x => (x[Fields.NgayNopHoSo]) <= (DataTypes.DateTime)dtcNgayDeNghiDen.SelectedDate.ToString("yyyy-MM-dd"));
                }

                andConditions.Add(x => x[Fields.NguoiDeNghi] == (DataTypes.UserId)SPContext.Current.Web.CurrentUser.ID.ToString());

                Expression<Func<Microsoft.SharePoint.SPListItem, bool>> andExpr = ExpressionsHelper.CombineAnd(andConditions);
                var expressions = new List<Expression<Func<SPListItem, bool>>>();
                expressions.Add(andExpr);
                var camlQuery = Camlex.Query().WhereAll(expressions).ToString();

                LoggingServices.LogMessage("CAML Query: " + camlQuery);

                SPQuery spQuery = new SPQuery();
                spQuery.Query = camlQuery;
                spQuery.ViewFieldsOnly = true;
                spQuery.ViewFields = string.Concat(
                                   string.Format("<FieldRef Name='{0}' />", Fields.Title),
                                   string.Format("<FieldRef Name='{0}' />", Fields.CaNhanToChuc),
                                   string.Format("<FieldRef Name='{0}' />", Fields.LoaiDeNghi),
                                   string.Format("<FieldRef Name='{0}' />", Fields.LoaiCapPhep),
                                   string.Format("<FieldRef Name='{0}' />", Fields.NgayNopHoSo),
                                   string.Format("<FieldRef Name='{0}' />", Fields.TenTrangThaiRef),
                                   string.Format("<FieldRef Name='{0}' />", Fields.CauHinhCalRef),
                                   string.Format("<FieldRef Name='{0}' />", Fields.NguoiXuLy),
                                   string.Format("<FieldRef Name='{0}' />", Fields.NguoiChoXuLy),
                                   string.Format("<FieldRef Name='{0}' />", Fields.NguoiDeNghi),
                                   string.Format("<FieldRef Name='{0}' />", Fields.NguoiThamGiaXuLy),
                                   "<FieldRef Name='ID' />");
                var deNghiUrl = (SPContext.Current.Web.ServerRelativeUrl + Constants.ListUrlDeNghiCapPhep).Replace("//", "/");
                var deNghiList = SPContext.Current.Web.GetList(deNghiUrl);
                dataTable = deNghiList.GetItems(spQuery).GetDataTable();
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("End GetDeNghi-User");
            return dataTable;
        }
    }
}
