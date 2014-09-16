using CamlexNET;
using LongAn.DVC.Common;
using LongAn.DVC.Helpers;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace LongAn.DVC.WebParts.DeNghiListViewUser
{
    [ToolboxItemAttribute(false)]
    public partial class DeNghiListViewUser : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public DeNghiListViewUser()
        {
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

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.BindItemsList();
                //Link & Title
                literalDeNghiTitle.Text = DeNghiTitle;
                var deNghiUrl = (SPContext.Current.Web.ServerRelativeUrl + Constants.ListUrlDeNghiCapPhep).Replace("//", "/");
                var currentPage = SPUtility.GetPageUrlPath(HttpContext.Current);
                var viewUrl = string.Format(Constants.ConfLinkNewForm, deNghiUrl, currentPage);
                lbtAddNew.OnClientClick = viewUrl;
            }
        }

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

                    if (dataTable.Rows.Count > PageSize)
                        divPagging.Visible = true;
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
                    string commandAgrument = rowView["ID"].ToString();

                    Literal literalLoaiCapPhep = (Literal)e.Item.FindControl("literalLoaiCapPhep");
                    literalLoaiCapPhep.Text = rowView[Constants.FieldLoaiDeNghi].ToString();

                    Literal literalNgayDeNghi = (Literal)e.Item.FindControl("literalNgayDeNghi");
                    literalNgayDeNghi.Text = rowView[Constants.FieldCreated].ToString();

                    Literal literalTrangThai = (Literal)e.Item.FindControl("literalTrangThai");
                    literalTrangThai.Text = rowView[Constants.FieldTrangThaiText].ToString();

                    var deNghiUrl = (SPContext.Current.Web.ServerRelativeUrl + Constants.ListUrlDeNghiCapPhep).Replace("//", "/");
                    var currentPage = SPUtility.GetPageUrlPath(HttpContext.Current);
                    LinkButton lbtViewItem = (LinkButton)e.Item.FindControl("lbtViewItem");
                    var viewUrl = string.Format(Constants.ConfLinkDispForm, deNghiUrl, commandAgrument, currentPage);
                    lbtViewItem.OnClientClick = viewUrl;

                    LinkButton lbtEditItem = (LinkButton)e.Item.FindControl("lbtEditItem");
                    var editUrl = string.Format(Constants.ConfLinkEditForm, deNghiUrl, commandAgrument, currentPage);
                    lbtEditItem.OnClientClick = editUrl;

                    LinkButton lbtDeleteItem = (LinkButton)e.Item.FindControl("lbtDeleteItem");
                    lbtDeleteItem.CommandName = "OnDeleteItemClick";
                    lbtDeleteItem.CommandArgument = commandAgrument;
                    lbtDeleteItem.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn xóa hồ sơ này không?')) return false;";

                    LinkButton lbtNopHoSo = (LinkButton)e.Item.FindControl("lbtNopHoSo");
                    lbtNopHoSo.CommandName = "OnNopHoSoClick";
                    lbtNopHoSo.CommandArgument = commandAgrument;
                    lbtNopHoSo.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn nộp hồ sơ này không?')) return false;";
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
                                var deNghiList = web.GetList((web.ServerRelativeUrl + Constants.ListUrlDeNghiCapPhep).Replace("//", "/"));
                                var deNghiItem = deNghiList.GetItemById(int.Parse(commandText));
                                deNghiItem.Delete();
                            }
                        }
                    });
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
                    deNghiItem[Constants.FieldTrangThai] = (int)TrangThaiXuLy.DaTiepNhan;
                    deNghiItem[Constants.FieldCapDuyet] = (int)CapXuLy.NhanVienTiepNhan;
                    deNghiItem.Update();
                }
                catch (Exception ex)
                {
                    LoggingServices.LogException(ex);
                }
                LoggingServices.LogMessage("End OnNopHoSoClick, item id:" + commandText);
            }
        }

        DataTable GetDeNghi(int trangThaiXuLy)
        {
            DataTable dataTable = null;
            var currentUserRole = CapXuLy.CaNhanToChuc;
            if (ViewState[Constants.ConfViewStateCapXuLy] == null)
            {
                currentUserRole = DeNghiHelper.CurrentUserRole(SPContext.Current.Web, SPContext.Current.Web.CurrentUser);
                ViewState[Constants.ConfViewStateCapXuLy] = currentUserRole;
            }
            else
                currentUserRole = (CapXuLy)ViewState[Constants.ConfViewStateCapXuLy];
            try
            {
                LoggingServices.LogMessage("Begin GetDeNghi, Cap Duyet: " + currentUserRole + ", Trang Thai Xu Ly: " + trangThaiXuLy);
                SPQuery caml = Camlex.Query().Where(x => (string)x[Constants.FieldTrangThai] == trangThaiXuLy.ToString())
                                                    .OrderBy(x => new[] { x["ID"] as Camlex.Desc })
                                                    .ToSPQuery();
                var deNghiUrl = (SPContext.Current.Web.ServerRelativeUrl + Constants.ListUrlDeNghiCapPhep).Replace("//", "/");
                var deNghiList = SPContext.Current.Web.GetList(deNghiUrl);
                dataTable = deNghiList.GetItems(caml).GetDataTable();
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("End GetDeNghi, Cap Duyet: " + currentUserRole + ", Trang Thai Xu Ly: " + trangThaiXuLy);
            return dataTable;
        }
    }
}
