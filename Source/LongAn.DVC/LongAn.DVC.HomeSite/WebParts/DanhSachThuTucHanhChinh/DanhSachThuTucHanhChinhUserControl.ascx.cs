using CamlexNET;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace LongAn.DVC.HomeSite.WebParts.DanhSachThuTucHanhChinh
{
    public partial class DanhSachThuTucHanhChinhUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SPWeb spWeb = SPContext.Current.Web;

                string webUrl = spWeb.ServerRelativeUrl.TrimEnd('/') + "/";

                string htmlTabList = string.Empty;
                string htmlTabContent = string.Empty;

                SPList spListLoaiCoQuan = spWeb.GetList(spWeb.ServerRelativeUrl.TrimEnd('/') + HomeSiteConstants.ListLoaiCoQuan);
                SPList spListCoQuan = spWeb.GetList(spWeb.ServerRelativeUrl.TrimEnd('/') + HomeSiteConstants.ListCoQuan);
                SPList spListDVC = spWeb.GetList(spWeb.ServerRelativeUrl.TrimEnd('/') + HomeSiteConstants.ListDVC);

                if (spListLoaiCoQuan != null && spListCoQuan != null && spListDVC != null)
                {
                    SPListItemCollection loaiCoQuanItems = spListLoaiCoQuan.Items;

                    for (int i = 0; i < loaiCoQuanItems.Count; i++ )
                    {
                        SPListItem lcqItem = loaiCoQuanItems[i];

                        htmlTabList += "<a href='#tab-" + (i + 1).ToString() + "' data-parent='tab-thu-tuc-hanh-chinh' class='tab-title " + (i == 0 ? "active": "") + "'>" + lcqItem.Title + "</a>";

                        string camlDVC = string.Empty;
                        var dvcExpressionsAnd = new List<Expression<Func<SPListItem, bool>>>();
                        dvcExpressionsAnd.Add(x => x["LoaiCoQuanThucHien"] == (DataTypes.LookupId)lcqItem.ID.ToString());

                        camlDVC = Camlex.Query().WhereAll(dvcExpressionsAnd).OrderBy(x => x["CoQuanThucHien"] as Camlex.Asc).ToString();

                        SPQuery dvcQry = new SPQuery();
                        dvcQry.Query = camlDVC;
                        SPListItemCollection dvcItems = spListDVC.GetItems(dvcQry);

                        if (dvcItems != null && dvcItems.Count > 0)
                        {
                            htmlTabContent += "<div class='tab-content " + (i == 0 ? "active" : "") + "' id='tab-" + (i + 1).ToString() + "'><ul class='featured-list'><li><ul>";

                            var results = dvcItems.Cast<SPListItem>().GroupBy(item => item["CoQuanThucHien"])
                                                    .Select(group => new
                                                    {
                                                        GroupName = group.Key.ToString().Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries)[1],
                                                        GroupId = group.Key.ToString().Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries)[0],
                                                        Count = group.Count()
                                                    })
                                                    .OrderBy(item => item.GroupName);

                            foreach (var data in results)
                            {
                                string url = "#";
                                SPListItem coquan = spListCoQuan.GetItemById(Convert.ToInt32(data.GroupId));
                                if (coquan != null)
                                {
                                    url = coquan["Hyperlink"] != null ? coquan["Hyperlink"].ToString() : "#";
                                }

                                htmlTabContent += "<li><img src='" + webUrl + "_layouts/15/LongAn.DVC.HomeSite/images/default-logo.jpg' /><a href='" + url + "'>" + data.GroupName + "</a><p>Tổng số thủ tục: " + data.Count.ToString() + "</p><div class='clearfix'></div></li>";
                            }

                            htmlTabContent += "</ul></li></ul></div>";
                        }
                    }
                }

                ltTabList.Text = htmlTabList;
                ltTabContent.Text = htmlTabContent;
            }
        }
    }
}
