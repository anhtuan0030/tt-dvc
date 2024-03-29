﻿using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using LongAn.DVC.Common;
using System.Data;
using LongAn.DVC.Helpers;

namespace LongAn.DVC.Layouts.LongAn.DVC
{
    public partial class PhanCongHoSo : LayoutsPageBase
    {
        #region Properties
        private CapXuLy CurrentUserRole
        {
            get
            {
                var currentUserRole = CapXuLy.CaNhanToChuc;
                if (ViewState[Constants.ConfViewStateCapXuLy] == null)
                {
                    currentUserRole = DeNghiHelper.CurrentUserRole(SPContext.Current.Web, SPContext.Current.Web.CurrentUser);
                    ViewState[Constants.ConfViewStateCapXuLy] = currentUserRole;
                }
                else
                    currentUserRole = (CapXuLy)ViewState[Constants.ConfViewStateCapXuLy];
                return currentUserRole;
            }
        }
        #endregion Properties

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            btnCancel.Click += btnCancel_Click;
            var currentUserRole = CurrentUserRole;
            var action = Request.QueryString.Get("Action");
            var actionTocken = Request.QueryString.Get("Atocken");
            var sourcePage = Request.QueryString.Get("Source");
            if (currentUserRole == CapXuLy.TruongPhoPhong)
            {
                if (action == Constants.ConfActionPC)
                {
                    if (actionTocken == Constants.ConfQueryStringPC)
                    {
                        btnSave.Visible = true;
                        btnSave.Click += btnSave_Click;
                    }
                }
            }
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            var redirectUrl = Request.QueryString["Source"];
            if (redirectUrl == null || string.IsNullOrEmpty(redirectUrl.ToString()))
                redirectUrl = "/";
            Response.Redirect(redirectUrl);
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            DeNghiHelper.AddDeNghiHistory(SPContext.Current.Web, CapXuLy.TruongPhoPhong, SPContext.Current.ItemId, HanhDong.PhanCongHoSo.ToString(), txtGhiChu.Text.Trim());
            UpdateItem(TrangThaiHoSo.DangXuLy, CapXuLy.CanBo);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlUsers.DataSource = GetCanBoXuLy();
                ddlUsers.DataValueField = "ID";
                ddlUsers.DataTextField = "Name";
                ddlUsers.DataBind();
            }
        }

        void UpdateItem(TrangThaiHoSo trangThai, CapXuLy capXuLy)
        {
            if (!this.Page.IsValid)
                return;

            var longOperation = new SPLongOperation(this.Page);
            longOperation.LeadingHTML = "Please wait while the operation is running";
            longOperation.TrailingHTML = "Once the operation is finished you will be redirected to result page";
            longOperation.Begin();
            //Save file upload
            var deNghiList = SPContext.Current.List;
            var spListItem = deNghiList.GetItemById(SPContext.Current.ItemId);
            spListItem[Constants.FieldCapDuyet] = (int)capXuLy;
            spListItem[Constants.FieldTrangThai] = (int)trangThai;
            spListItem[Constants.FieldCanBoUser] = ddlUsers.SelectedValue;
            spListItem[Constants.FieldTruongPhongUser] = SPContext.Current.Web.CurrentUser;
            spListItem.Update();
            //Redirect to page
            var redirectUrl = Request.QueryString["Source"];
            if (redirectUrl == null || string.IsNullOrEmpty(redirectUrl.ToString()))
                redirectUrl = "/";
            longOperation.End(redirectUrl, Microsoft.SharePoint.Utilities.SPRedirectFlags.DoNotEndResponse, System.Web.HttpContext.Current, "");
        }

        DataTable GetCanBoXuLy()
        {
            DataTable result = new DataTable();
            DataColumn[] columns = new DataColumn[] { 
                new DataColumn("ID"),
                new DataColumn("Name")
            };
            result.Columns.AddRange(columns);
            try
            {
                LoggingServices.LogMessage("Begin GetCanBoXuLy");
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                    {
                        using (SPWeb web = site.OpenWeb(SPContext.Current.Web.ID))
                        {
                            var canBoGroup = web.SiteGroups[Constants.ConfGroupCanBoXuLy];
                            foreach (SPUser user in canBoGroup.Users)
                            {
                                DataRow row = result.NewRow();
                                row["ID"] = user.ID;
                                row["Name"] = user.Name;
                                result.Rows.Add(row);
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("End GetCanBoXuLy");
            return result;
        }
    }
}
