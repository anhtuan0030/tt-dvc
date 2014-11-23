using CamlexNET;
using LongAn.DVC.Common;
using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace LongAn.DVC.WebParts.DeNghiHistory
{
    [ToolboxItemAttribute(false)]
    public partial class DeNghiHistory : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public DeNghiHistory()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
            repeaterLists.ItemDataBound += repeaterLists_ItemDataBound;
            btnTimKiem.Click += btnTimKiem_Click;
        }

        void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaBienNhan.Text.Trim()))
                return;
            repeaterLists.DataSource = GetDeNghi(txtMaBienNhan.Text.Trim());
            repeaterLists.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        DataTable GetDeNghi(string maBienNhan)
        {
            DataTable dataTable = null;
            try
            {
                LoggingServices.LogMessage("Begin GetDeNghiHis - Mã biên nhận: " + maBienNhan);
                SPQuery caml = Camlex.Query().Where(x => (string)x[Fields.Title] == maBienNhan)
                                                    .OrderBy(x => new[] { x["ID"] as Camlex.Asc })
                                                    .ToSPQuery();
                var deNghiUrl = (SPContext.Current.Web.ServerRelativeUrl + Constants.ListUrlLichSuCapPhep).Replace("//", "/");
                var deNghiList = SPContext.Current.Web.GetList(deNghiUrl);
                dataTable = deNghiList.GetItems(caml).GetDataTable();
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("End GetDeNghiHis");
            return dataTable;
        }

        protected void repeaterLists_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                DataRowView rowView = (DataRowView)e.Item.DataItem;
                if (rowView != null)
                {
                    string commandAgrument = rowView["ID"].ToString();

                    Literal literalNguoiXuLy = (Literal)e.Item.FindControl("literalNguoiXuLy");
                    literalNguoiXuLy.Text = rowView["NguoiXuLy"].ToString();

                    Literal literalNgayXuLy = (Literal)e.Item.FindControl("literalNgayXuLy");
                    literalNgayXuLy.Text = rowView["NgayXuLy"].ToString();

                    Literal literalHanhDong = (Literal)e.Item.FindControl("literalHanhDong");
                    literalHanhDong.Text = rowView["HanhDong"].ToString();

                    Literal literalMoTa = (Literal)e.Item.FindControl("literalMoTa");
                    literalMoTa.Text = rowView["MoTa"].ToString();
                }
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
        }
    }
}
