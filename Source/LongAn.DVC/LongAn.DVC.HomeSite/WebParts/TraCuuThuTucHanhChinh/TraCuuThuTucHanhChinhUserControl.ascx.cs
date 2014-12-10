using CamlexNET;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace LongAn.DVC.HomeSite.WebParts.TraCuuThuTucHanhChinh
{
    public partial class TraCuuThuTucHanhChinhUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SPWeb spWeb = SPContext.Current.Web;

                string webUrl = spWeb.ServerRelativeUrl.TrimEnd('/');

                //string htmlNotification = string.Empty;
                //string htmlTotalRecords = string.Empty;
                //string htmlResults = string.Empty;

                SPList spListLoaiCoQuan = spWeb.GetList(webUrl + HomeSiteConstants.ListLoaiCoQuan);
                SPList spListCoQuan = spWeb.GetList(webUrl + HomeSiteConstants.ListCoQuan);
                SPList spListLinhVuc = spWeb.GetList(webUrl + HomeSiteConstants.ListLinhVuc);
                SPList spListMucDo = spWeb.GetList(webUrl + HomeSiteConstants.ListMucDo);
                SPList spListDVC = spWeb.GetList(webUrl + HomeSiteConstants.ListDVC);

                if (spListLoaiCoQuan != null && spListMucDo != null && spListMucDo != null && spListCoQuan != null && spListDVC != null)
                {
                    SPListItemCollection loaiCoQuanItems = spListLoaiCoQuan.Items;
                    BindDropDownList(loaiCoQuanItems, ddlLoaiCoQuan);

                    SPListItemCollection coQuanItems = spListCoQuan.Items;
                    BindDropDownList(coQuanItems, ddlCoQuan);

                    SPListItemCollection linhVucItems = spListLinhVuc.Items;
                    BindDropDownList(linhVucItems, ddlLinhVuc);

                    SPListItemCollection mucDoItems = spListMucDo.Items;
                    BindDropDownList(mucDoItems, ddlMucDo); 
                }
            }
        }

        private void BindDropDownList(SPListItemCollection itemCol, DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("Tất cả", "0"));

            var list = itemCol.Cast<SPListItem>().Select(item => new { Title = item.Title, Value = item.ID.ToString() }).OrderBy(item => item.Title);

            foreach (var data in list)
            {
                ddl.Items.Add(new ListItem(data.Title, data.Value));
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SPWeb spWeb = SPContext.Current.Web;

            string webUrl = spWeb.ServerRelativeUrl.TrimEnd('/');

            string htmlNotification = string.Empty;
            string htmlTotalRecords = string.Empty;
            string htmlResults = string.Empty;

            SPList spListLoaiCoQuan = spWeb.GetList(webUrl + HomeSiteConstants.ListLoaiCoQuan);
            SPList spListCoQuan = spWeb.GetList(webUrl + HomeSiteConstants.ListCoQuan);
            SPList spListLinhVuc = spWeb.GetList(webUrl + HomeSiteConstants.ListLinhVuc);
            SPList spListMucDo = spWeb.GetList(webUrl + HomeSiteConstants.ListMucDo);
            SPList spListDVC = spWeb.GetList(webUrl + HomeSiteConstants.ListDVC);

            if (spListLoaiCoQuan != null && spListMucDo != null && spListMucDo != null && spListCoQuan != null && spListDVC != null)
            {
                string camlDVC = string.Empty;
                var dvcExpressionsAnd = new List<Expression<Func<SPListItem, bool>>>();

                if (ddlLoaiCoQuan.SelectedValue != "0")
                {
                    dvcExpressionsAnd.Add(x => x["LoaiCoQuanThucHien"] == (DataTypes.LookupId)ddlLoaiCoQuan.SelectedValue);
                }

                if (ddlCoQuan.SelectedValue != "0")
                {
                    dvcExpressionsAnd.Add(x => x["CoQuanThucHien"] == (DataTypes.LookupId)ddlCoQuan.SelectedValue);
                }

                if (ddlLinhVuc.SelectedValue != "0")
                {
                    dvcExpressionsAnd.Add(x => x["LinhVuc"] == (DataTypes.LookupId)ddlLinhVuc.SelectedValue);
                }

                if (ddlMucDo.SelectedValue != "0")
                {
                    dvcExpressionsAnd.Add(x => x["MucDo"] == (DataTypes.LookupId)ddlMucDo.SelectedValue);
                }

                if (!string.IsNullOrEmpty(txtKeyword.Text.Trim()))
                {
                    dvcExpressionsAnd.Add(x => ((string)x["Title"]).Contains(txtKeyword.Text.Trim()));
                }

                SPListItemCollection dvcItems;

                if (dvcExpressionsAnd.Count > 0)
                {
                    camlDVC = Camlex.Query().WhereAll(dvcExpressionsAnd).ToString();

                    SPQuery dvcQry = new SPQuery();
                    dvcQry.Query = camlDVC;
                    dvcItems = spListDVC.GetItems(dvcQry);
                }
                else
                {
                    dvcItems = spListDVC.Items;
                }

                if (dvcItems.Count > 0)
                {
                    ltNotification.Visible = false;

                    htmlTotalRecords = "Tổng số có <b class='red'>" + dvcItems.Count.ToString() + "</b> thủ tục";

                    

                    //sample, may not use
                    //var results = dvcItems.Cast<SPListItem>().GroupBy(item => new { CoQuan = item["CoQuanThucHien"].ToString(), LinhVuc = item["LinhVuc"].ToString(), MucDo = item["MucDo"].ToString() })
                    //                        .Select(group => new
                    //                                {
                    //                                    CoQuan = group.Key.CoQuan.ToString().Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries)[1],
                    //                                    LinhVuc = group.Key.LinhVuc.ToString().Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries)[1],
                    //                                    MucDo = group.Key.MucDo.ToString().Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries)[1],
                    //                                    Count = group.Count()
                    //                                })
                    //                        .OrderBy(item => new { item.CoQuan, item.LinhVuc, item.MucDo });

                    var resultDonVi = dvcItems.Cast<SPListItem>().GroupBy(item =>  item["CoQuanThucHien"])
                                            .Select(group => new
                                            {
                                                CoQuan = group.Key.ToString().Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries)[1],
                                                Count = group.Count()
                                            })
                                            .OrderBy(item =>  item.CoQuan);

                    foreach (var donvi in resultDonVi)
                    {
                        //open for DonVi + set value
                        htmlResults += "<div class='acco-tabs'><div class='acco-tab-title'>Đơn vị: " + donvi.CoQuan + ", tổng số: "+ donvi.Count.ToString() +"</div>";

                        //open for LinhVuc
                        htmlResults += "<div class='acco-tab-content'><div class='acco-tabs'>";

                        var resultLinhVuc = dvcItems.Cast<SPListItem>().Where(item => item["CoQuanThucHien"].ToString().Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries)[1] == donvi.CoQuan)
                                                .GroupBy(item => item["LinhVuc"])
                                                .Select(group => new
                                                {
                                                    LinhVuc = group.Key.ToString().Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries)[1],
                                                    Count = group.Count()
                                                })
                                                .OrderBy(item => item.LinhVuc);

                        foreach (var linhvuc in resultLinhVuc)
                        {
                            //set value LinhVuc
                            htmlResults += "<div class='acco-tab-title active'>Lĩnh vực: " + linhvuc.LinhVuc + "; Tổng số: "+ linhvuc.Count.ToString() +"</div>";

                            //open for DVC
                            htmlResults += "<div class='acco-tab-content active'><div class='doc-list'>";

                            var resultDVC = dvcItems.Cast<SPListItem>().Where(item => item["CoQuanThucHien"].ToString().Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries)[1] == donvi.CoQuan && item["LinhVuc"].ToString().Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries)[1] == linhvuc.LinhVuc)
                                                .Select(item => new
                                                {
                                                    DVCTitle = item["Title"].ToString(),
                                                    MucDo = item["MucDo"].ToString().Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries)[1],
                                                    Link = item["Hyperlink"].ToString()
                                                })
                                                .OrderBy(item => item.DVCTitle).ToList();

                            for (int i = 0; i < resultDVC.Count; i++ )
                            {
                                var dvcItem = resultDVC[i];
                                //set value DVC
                                htmlResults += "<div class='doc-item'><div class='num'><div class='corner'>&nbsp;</div><div class='number'>" + (i + 1).ToString() + "</div></div><a href='" + dvcItem.Link + "'>" + dvcItem.DVCTitle + "</a><div style='float: right; padding: 10px; margin-right: 80px; background-color: #ff7f01;'><a href='" + dvcItem.Link + "' style='color:white;font-weight: bold;'>Đăng ký</a></div><div class='label'>" + dvcItem.MucDo + "</div><div class='clearfix'></div></div>";
                            }

                            //close for DVC
                            htmlResults += "</div></div>";
                        }

                        //close for LinhVuc
                        htmlResults += "</div></div>";

                        //close for DonVi
                        htmlResults += "</div>";
                    }

                    ltTotalRecords.Text = htmlTotalRecords;
                    ltResults.Text = htmlResults;
                }
                else
                {
                    ltNotification.Visible = true;
                    ltNotification.Text = "Không tìm thấy thủ tục hành chính thỏa điều kiện tra cứu.";

                    ltTotalRecords.Text = string.Empty;
                    ltResults.Text = string.Empty;
                }
            }
        }

        protected void ddlLoaiCoQuan_SelectedIndexChanged(object sender, EventArgs e)
        {
            SPWeb spWeb = SPContext.Current.Web;

            string webUrl = spWeb.ServerRelativeUrl.TrimEnd('/');

            SPList spListCoQuan = spWeb.GetList(webUrl + HomeSiteConstants.ListCoQuan);
            SPList spListLinhVuc = spWeb.GetList(webUrl + HomeSiteConstants.ListLinhVuc);



            if (ddlLoaiCoQuan.SelectedValue != "0")
            {
                string camlCoQuan = string.Empty;
                var coquanExpressionsAnd = new List<Expression<Func<SPListItem, bool>>>();
                coquanExpressionsAnd.Add(x => x["LoaiCoQuanThucHien"] == (DataTypes.LookupId)ddlLoaiCoQuan.SelectedValue);

                camlCoQuan = Camlex.Query().WhereAll(coquanExpressionsAnd).ToString();

                SPQuery qryCoQuan = new SPQuery();
                qryCoQuan.Query = camlCoQuan;
                SPListItemCollection coQuanItems = spListCoQuan.GetItems(qryCoQuan);
                BindDropDownList(coQuanItems, ddlCoQuan);



                string camlLinhVuc = string.Empty;
                var linhvucExpressionsAnd = new List<Expression<Func<SPListItem, bool>>>();
                linhvucExpressionsAnd.Add(x => x["LoaiCoQuanThucHien"] == (DataTypes.LookupId)ddlLoaiCoQuan.SelectedValue);

                camlLinhVuc = Camlex.Query().WhereAll(linhvucExpressionsAnd).ToString();

                SPQuery qryLinhVuc = new SPQuery();
                qryLinhVuc.Query = camlLinhVuc;
                SPListItemCollection linhvucItems = spListLinhVuc.GetItems(qryLinhVuc);
                BindDropDownList(linhvucItems, ddlLinhVuc);
            }
            else
            {
                SPListItemCollection coQuanItems = spListCoQuan.Items;
                BindDropDownList(coQuanItems, ddlCoQuan);

                SPListItemCollection linhVucItems = spListLinhVuc.Items;
                BindDropDownList(linhVucItems, ddlLinhVuc);
            }
        }

        protected void ddlCoQuan_SelectedIndexChanged(object sender, EventArgs e)
        {
            SPWeb spWeb = SPContext.Current.Web;

            string webUrl = spWeb.ServerRelativeUrl.TrimEnd('/');

            SPList spListLinhVuc = spWeb.GetList(webUrl + HomeSiteConstants.ListLinhVuc);


            if (ddlCoQuan.SelectedValue != "0")
            {
                string camlLinhVuc = string.Empty;
                var linhvucExpressionsAnd = new List<Expression<Func<SPListItem, bool>>>();
                linhvucExpressionsAnd.Add(x => x["CoQuanThucHien"] == (DataTypes.LookupId)ddlCoQuan.SelectedValue);

                if (ddlLoaiCoQuan.SelectedValue != "0")
                {
                    linhvucExpressionsAnd.Add(x => x["LoaiCoQuanThucHien"] == (DataTypes.LookupId)ddlLoaiCoQuan.SelectedValue);
                }

                camlLinhVuc = Camlex.Query().WhereAll(linhvucExpressionsAnd).ToString();

                SPQuery qryLinhVuc = new SPQuery();
                qryLinhVuc.Query = camlLinhVuc;
                SPListItemCollection linhvucItems = spListLinhVuc.GetItems(qryLinhVuc);
                BindDropDownList(linhvucItems, ddlLinhVuc);
            }
            else
            {
                SPListItemCollection linhVucItems = spListLinhVuc.Items;
                BindDropDownList(linhVucItems, ddlLinhVuc);
            }
        }
    }
}
