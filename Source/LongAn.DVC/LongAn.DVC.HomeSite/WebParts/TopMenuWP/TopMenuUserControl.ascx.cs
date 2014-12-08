using CamlexNET;
using LongAn.DVC.Common;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace LongAn.DVC.HomeSite.WebParts.TopMenu
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
                    string liMenus = string.Empty;

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
                            liMenus += "<li><a href='" + firstLevelItem[Constants.TopMenu.HYPERLINK_COLUMN] + "'>" + firstLevelItem[Constants.TopMenu.TITLE_COLUMN] + "</a>" + GetChildrenNode(spListTopMenu, firstLevelItem) + "</li>";
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
                    htmlMenus += "<li><a href='" + subLevelItem[Constants.TopMenu.HYPERLINK_COLUMN] + "'>" + subLevelItem[Constants.TopMenu.TITLE_COLUMN] + "</a>" + GetChildrenNode(spListTopMenu, subLevelItem) + "</li>";
                }

                htmlMenus += "</ul>";
            }

            return htmlMenus;
        }
    }
}
