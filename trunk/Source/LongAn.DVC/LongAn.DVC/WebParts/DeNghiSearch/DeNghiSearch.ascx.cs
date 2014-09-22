using CamlexNET;
using LongAn.DVC.Common;
using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Data;
using System.Web.UI.WebControls.WebParts;

namespace LongAn.DVC.WebParts.DeNghiSearch
{
    [ToolboxItemAttribute(false)]
    public partial class DeNghiSearch : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public DeNghiSearch()
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
            var dataTable = GetDeNghi();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                literalMaBienNhan.Text = dataTable.Rows[0][Constants.FieldTitle].ToString();
                literalDonVi.Text = dataTable.Rows[0][Constants.FieldCaNhanToChuc].ToString();
                literalTrangThai.Text = dataTable.Rows[0][Constants.FieldTrangThaiText].ToString();
            }
            else
            {
                literalMaBienNhan.Text = "Không tồn tại";
                literalDonVi.Text = string.Empty;
                literalTrangThai.Text = string.Empty;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        DataTable GetDeNghi()
        {
            DataTable dataTable = null;
            try
            {
                LoggingServices.LogMessage("Begin GetDeNghi - current user");
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                    {
                        using (SPWeb web = site.OpenWeb(SPContext.Current.Web.ID))
                        {
                            SPQuery caml = Camlex.Query().Where(x => (string)x[Constants.FieldTitle] == txtMaBienNhan.Text.Trim())
                                                   .OrderBy(x => new[] { x["ID"] as Camlex.Desc })
                                                   .ToSPQuery();
                            caml.RowLimit = 1;
                            var deNghiUrl = (web.ServerRelativeUrl + Constants.ListUrlDeNghiCapPhep).Replace("//", "/");
                            var deNghiList = web.GetList(deNghiUrl);
                            dataTable = deNghiList.GetItems(caml).GetDataTable();
                        }
                    }
                   
                });
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
