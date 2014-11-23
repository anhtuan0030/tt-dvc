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
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace LongAn.DVC.WebParts.DeNghiListViewTotal
{
    public partial class DeNghiListViewTotalUserControl : UserControl
    {
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

        [WebBrowsable(true),
         WebDisplayName("Trạng thái xử lý"),
         WebDescription("Nhập trạng thái xử lý"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public int TrangThai { get; set; }

        #endregion WebPart Properties

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
            var listItems = this.GetDeNghi(TrangThai);
            DataTable dataTable = listItems.GetDataTable();
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

        private SPListItemCollection GetDeNghi(int trangThaiXuLy)
        {
            SPListItemCollection dataTable = null;
            try
            {
                LoggingServices.LogMessage("Begin GetDeNghi, Trang Thai Xu Ly: " + trangThaiXuLy);
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("End GetDeNghi, Trang Thai Xu Ly: " + trangThaiXuLy);
            return dataTable;
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
            try
            {
                LoggingServices.LogMessage("Begin link button click, command: " + commadName + ", item id: " + commandText);
                
                this.BindItemsList();
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("End link button click, command: " + e.CommandName + ", item id: " + commandText);
        }
        #endregion Repeater List

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Link & Title
                literalDeNghiTitle.Text = DeNghiTitle;
            }
        }
    }
}
