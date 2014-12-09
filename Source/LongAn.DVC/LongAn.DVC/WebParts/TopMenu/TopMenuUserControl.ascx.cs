using CamlexNET;
using CamlexNET.Impl.Helpers;
using LongAn.DVC.Common;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace LongAn.DVC.WebParts.TopMenu
{
    public partial class TopMenuUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    //string liMenus = "<li class='home'><a href='" + SPContext.Current.Site.RootWeb.ServerRelativeUrl + "'>Trang chủ</a></li>";
                    string liMenus = "<li class='home'><a href='/'>Trang chủ</a></li>";

                    SPWeb spWeb = SPContext.Current.Web;

                    string topmenuUrl = (spWeb.ServerRelativeUrl + Constants.TopMenu.LIST_URL).Replace("//", "/");
                    SPList spListTopMenu = spWeb.GetList(topmenuUrl);


                    if (spListTopMenu != null)
                    {
                        string camlFirstLevel = string.Empty;
                        var firstLevelExpressionsAnd = new List<Expression<Func<SPListItem, bool>>>();
                        firstLevelExpressionsAnd.Add(x => x[Constants.TopMenu.PARENT_COLUMN] == null);
                        firstLevelExpressionsAnd.Add(x => (bool)x[Constants.TopMenu.IS_ACTIVE_COLUMN] == true);
                        camlFirstLevel = Camlex.Query().WhereAll(firstLevelExpressionsAnd).OrderBy(x => x[Constants.TopMenu.ITEM_ORDER_COLUMN] as Camlex.Asc).ToString();

                        SPQuery firstLevelQry = new SPQuery();
                        firstLevelQry.Query = camlFirstLevel;
                        SPListItemCollection firstLevelItems = spListTopMenu.GetItems(firstLevelQry);

                        foreach (SPListItem firstLevelItem in firstLevelItems)
                        {
                            liMenus += "<li><a href='" + firstLevelItem[Constants.TopMenu.HYPERLINK_COLUMN] + "'>" + firstLevelItem[Constants.TopMenu.TITLE_COLUMN] + ShowTotalDeNghiCount(spWeb, firstLevelItem) + "</a>" + GetChildrenNode(spListTopMenu, firstLevelItem) + "</li>";
                        }
                    }

                    ltTopMenu.Text = liMenus;
                }
                catch (Exception ex)
                { }
            }
        }

        private string GetChildrenNode(SPList spListTopMenu, SPListItem parentItem)
        {
            string htmlMenus = string.Empty;

            string camlSubLevel = string.Empty;
            var subLevelExpressionsAnd = new List<Expression<Func<SPListItem, bool>>>();
            subLevelExpressionsAnd.Add(x => x[Constants.TopMenu.PARENT_COLUMN] == (DataTypes.LookupId)parentItem.ID.ToString());
            subLevelExpressionsAnd.Add(x => (bool)x[Constants.TopMenu.IS_ACTIVE_COLUMN] == true);
            camlSubLevel = Camlex.Query().WhereAll(subLevelExpressionsAnd).OrderBy(x => x[Constants.TopMenu.ITEM_ORDER_COLUMN] as Camlex.Asc).ToString();

            SPQuery subLevelQry = new SPQuery();
            subLevelQry.Query = camlSubLevel;
            SPListItemCollection subLevelItems = spListTopMenu.GetItems(subLevelQry);

            if (subLevelItems != null && subLevelItems.Count > 0)
            {
                htmlMenus += "<ul class='sub-menu'>";

                foreach (SPListItem subLevelItem in subLevelItems)
                {
                    htmlMenus += "<li><a href='" + subLevelItem[Constants.TopMenu.HYPERLINK_COLUMN] + "'>" + subLevelItem[Constants.TopMenu.TITLE_COLUMN] + ShowTotalDeNghiCount(SPContext.Current.Web, subLevelItem) + "</a>" + GetChildrenNode(spListTopMenu, subLevelItem) + "</li>";
                }

                htmlMenus += "</ul>";
            }

            return htmlMenus;
        }

        private string ShowTotalDeNghiCount(SPWeb spWeb, SPListItem spItem)
        {
            string results = string.Empty;

            try
            {
                if (spItem["ShowTotal"] != null && Convert.ToBoolean(spItem["ShowTotal"]) == true)
                {
                    //Or condition
                    var orConditions = new List<Expression<Func<SPListItem, bool>>>();
                    var trangThai = spItem["ShowTotalStatus"] != null ? spItem["ShowTotalStatus"].ToString().Trim() : string.Empty;
                    if (!string.IsNullOrEmpty(trangThai))
                    {
                        var trangThaiArray = trangThai.TrimEnd(';').TrimStart(';').Split(';');
                        foreach (var item in trangThaiArray)
                        {
                            orConditions.Add(x => ((string)x[Fields.TrangThai]) == item);
                        }
                    }
                    Expression<Func<Microsoft.SharePoint.SPListItem, bool>> orExpr = null;
                    if (orConditions.Count > 0)
                        orExpr = ExpressionsHelper.CombineOr(orConditions);

                    //And condition
                    var andConditions = new List<Expression<Func<SPListItem, bool>>>();

                    string option = spItem["ShowTotalOption"] != null ? spItem["ShowTotalOption"].ToString() : "None";
                    if (option == "Joined")
                    {
                        andConditions.Add(x => x[Fields.NguoiThamGiaXuLy] == (DataTypes.UserId)SPContext.Current.Web.CurrentUser.ID.ToString());
                    }
                    else if (option == "Handle")
                    {
                        andConditions.Add(x => x[Fields.NguoiChoXuLy] == (DataTypes.UserId)SPContext.Current.Web.CurrentUser.ID.ToString());
                    }

                    Expression<Func<Microsoft.SharePoint.SPListItem, bool>> andExpr = null;
                    if (andConditions != null && andConditions.Count > 0)
                        andExpr = ExpressionsHelper.CombineAnd(andConditions);

                    SPListItemCollection itemCol;
                    //Query
                    if (orExpr != null || andExpr != null)
                    {
                        var expressions = new List<Expression<Func<SPListItem, bool>>>();
                        if (orExpr != null)
                            expressions.Add(orExpr);
                        if (andExpr != null)
                            expressions.Add(andExpr);

                        var camlQuery = Camlex.Query().WhereAll(expressions).ToString();

                        SPQuery spQuery = new SPQuery();
                        spQuery.Query = camlQuery;
                        spQuery.ViewFieldsOnly = true;
                        //spQuery.ViewFields = string.Concat(
                        //                   string.Format("<FieldRef Name='{0}' />", Fields.Title),
                        //                   string.Format("<FieldRef Name='{0}' />", Fields.CaNhanToChuc),
                        //                   string.Format("<FieldRef Name='{0}' />", Fields.LoaiDeNghi),
                        //                   string.Format("<FieldRef Name='{0}' />", Fields.LoaiCapPhep),
                        //                   string.Format("<FieldRef Name='{0}' />", Fields.NgayNopHoSo),
                        //                   string.Format("<FieldRef Name='{0}' />", Fields.TenTrangThaiRef),
                        //                   string.Format("<FieldRef Name='{0}' />", Fields.CauHinhCalRef),
                        //                   string.Format("<FieldRef Name='{0}' />", Fields.NguoiXuLy),
                        //                   string.Format("<FieldRef Name='{0}' />", Fields.NguoiChoXuLy),
                        //                   string.Format("<FieldRef Name='{0}' />", Fields.NguoiDeNghi),
                        //                   string.Format("<FieldRef Name='{0}' />", Fields.NguoiThamGiaXuLy),
                        //                   "<FieldRef Name='ID' />");
                        spQuery.ViewFields = "<FieldRef Name='ID' />";
                        var deNghiUrl = (spWeb.ServerRelativeUrl + Constants.ListUrlDeNghiCapPhep).Replace("//", "/");
                        var deNghiList = spWeb.GetList(deNghiUrl);
                        itemCol = deNghiList.GetItems(spQuery);
                    }
                    else
                    {
                        var deNghiUrl = (spWeb.ServerRelativeUrl + Constants.ListUrlDeNghiCapPhep).Replace("//", "/");
                        var deNghiList = spWeb.GetList(deNghiUrl);
                        itemCol = deNghiList.Items;
                    }

                    if (itemCol != null)
                    {
                        results = " <span style='color:yellow !important;font-weight:bold'> (" + itemCol.Count + ")<span> ";
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            return results;
        }
    }
}
