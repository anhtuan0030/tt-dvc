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
                //Check permission
                if (SPContext.Current.Web.CurrentUser == null)
                {
                    HttpContext.Current.Response.Redirect(SPContext.Current.Web.ServerRelativeUrl);
                }

                var currentUserRole = DeNghiHelper.CurrentUserRole(SPContext.Current.Web, SPContext.Current.Web.CurrentUser);
                if (currentUserRole != CapXuLy.CaNhanToChuc)
                    HttpContext.Current.Response.Redirect(LinkTrangChu);
                this.BindItemsList();
                //Link & Title
                literalDeNghiTitle.Text = DeNghiTitle;
                #region Using popup
                var deNghiUrl = (SPContext.Current.Web.ServerRelativeUrl + Constants.ListUrlDeNghiCapPhep).Replace("//", "/");
                var currentPage = SPUtility.GetPageUrlPath(HttpContext.Current);
                //var viewUrl = string.Format(Constants.ConfLinkNewForm, deNghiUrl, currentPage);
                //lbtAddNew.OnClientClick = viewUrl;
                #endregion Using popup
                var viewUrl = string.Format(Constants.ConfLinkPageNewForm, deNghiUrl, currentPage);
                hplAddNew.NavigateUrl = viewUrl;
                hplTrangChu.NavigateUrl = LinkTrangChu;
                hplDanhSachDeNghi.NavigateUrl = LinkDanhSachHoSo;
                hplThongTinHuongDan.NavigateUrl = LinkThongTinHuongDan;
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
         WebDisplayName("Tiêu đề"),
         WebDescription("This Accepts text Input"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public string DeNghiTitle { get; set; }

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
         WebDisplayName("Link Danh sách hồ sơ"),
         WebDescription("This Accepts text Input"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public string LinkDanhSachHoSo { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Link Thông tin hướng dẫn"),
         WebDescription("This Accepts text Input"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public string LinkThongTinHuongDan { get; set; }
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
                    string commandAgrument = rowView["ID"].ToString();

                    Literal literalLoaiCapPhep = (Literal)e.Item.FindControl("literalLoaiCapPhep");
                    literalLoaiCapPhep.Text = rowView[Constants.FieldLoaiDeNghi].ToString();

                    Literal literalMaBienNhan = (Literal)e.Item.FindControl("literalMaBienNhan");
                    literalMaBienNhan.Text = rowView[Constants.FieldTitle].ToString();

                    Literal literalNgayDeNghi = (Literal)e.Item.FindControl("literalNgayDeNghi");
                    literalNgayDeNghi.Text = rowView[Constants.FieldNgayNopHoSo].ToString();

                    Literal literalTrangThai = (Literal)e.Item.FindControl("literalTrangThai");
                    literalTrangThai.Text = rowView[Constants.FieldTrangThaiText].ToString();

                    var deNghiUrl = (SPContext.Current.Web.ServerRelativeUrl + Constants.ListUrlDeNghiCapPhep).Replace("//", "/");
                    var currentPage = SPUtility.GetPageUrlPath(HttpContext.Current);
                    HyperLink lbtViewItem = (HyperLink)e.Item.FindControl("lbtViewItem");
                    var viewUrl = string.Format(Constants.ConfLinkPageDispForm, deNghiUrl, commandAgrument, currentPage);
                    lbtViewItem.NavigateUrl = viewUrl;

                    HyperLink lbtEditItem = (HyperLink)e.Item.FindControl("lbtEditItem");
                    LinkButton lbtDeleteItem = (LinkButton)e.Item.FindControl("lbtDeleteItem");
                    LinkButton lbtNopHoSo = (LinkButton)e.Item.FindControl("lbtNopHoSo");
                    var trangThai = int.Parse(rowView[Constants.FieldTrangThai].ToString());
                    if (trangThai == (int)TrangThaiHoSo.KhoiTao
                        || trangThai == (int)TrangThaiHoSo.ChoBoSung)
                    {
                        
                        var editUrl = string.Format(Constants.ConfLinkPageEditForm, deNghiUrl, commandAgrument, currentPage);
                        lbtEditItem.NavigateUrl = editUrl;
                        lbtNopHoSo.CommandName = "OnNopHoSoClick";
                        lbtNopHoSo.CommandArgument = commandAgrument;
                        lbtNopHoSo.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn nộp hồ sơ này không?')) return false;";
                        if(trangThai == (int)TrangThaiHoSo.KhoiTao)
                        {
                            lbtDeleteItem.CommandName = "OnDeleteItemClick";
                            lbtDeleteItem.CommandArgument = commandAgrument;
                            lbtDeleteItem.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn xóa hồ sơ này không?')) return false;";
                        }
                    }
                    else
                    {
                        lbtEditItem.Style.Add("display", "none");
                        lbtDeleteItem.Style.Add("display", "none");
                        lbtNopHoSo.Style.Add("display", "none");
                        LinkButton lbtDisable1 = (LinkButton)e.Item.FindControl("lbtDisable1");
                        lbtDisable1.Style.Add("display", "block");
                        LinkButton lbtDisable2 = (LinkButton)e.Item.FindControl("lbtDisable2");
                        lbtDisable2.Style.Add("display", "block");
                        LinkButton lbtDisable3 = (LinkButton)e.Item.FindControl("lbtDisable3");
                        lbtDisable3.Style.Add("display", "block");
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
                LoggingServices.LogMessage("Begin GetDeNghi - current user");
                SPQuery caml = Camlex.Query().Where(x => x["NguoiDeNghi"] == (DataTypes.UserId)SPContext.Current.Web.CurrentUser.ID.ToString())
                                                    .OrderBy(x => new[] { x["ID"] as Camlex.Desc })
                                                    .ToSPQuery();
                caml.ViewFields = string.Concat("<FieldRef Name='ID' />",
                                                "<FieldRef Name='Title' />",
                                                "<FieldRef Name='LoaiDeNghi' />",
                                                "<FieldRef Name='NgayNopHoSo' />",
                                                "<FieldRef Name='TrangThai' />",
                                                "<FieldRef Name='TrangThaiText' />");
                var deNghiUrl = (SPContext.Current.Web.ServerRelativeUrl + Constants.ListUrlDeNghiCapPhep).Replace("//", "/");
                var deNghiList = SPContext.Current.Web.GetList(deNghiUrl);
                dataTable = deNghiList.GetItems(caml).GetDataTable();
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("End GetDeNghi - current user");
            return dataTable;
        }
    }
}
