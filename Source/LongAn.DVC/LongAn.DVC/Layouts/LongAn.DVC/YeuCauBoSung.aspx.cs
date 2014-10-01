using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using LongAn.DVC.Common;
using LongAn.DVC.Helpers;

namespace LongAn.DVC.Layouts.LongAn.DVC
{
    public partial class YeuCauBoSung : LayoutsPageBase
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
            if (currentUserRole == CapXuLy.CanBo)
            {
                if (action == Constants.ConfActionBS)
                {
                    if (actionTocken == Constants.ConfQueryStringBS)
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
            DeNghiHelper.AddDeNghiHistory(SPContext.Current.Web, CapXuLy.CanBo, SPContext.Current.ItemId, HanhDong.PhanCongHoSo.ToString());
            UpdateItem(TrangThaiHoSo.ChoBoSung, CapXuLy.CaNhanToChuc);
            AddBoSungYeuCau(SPContext.Current.Web);
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
            spListItem.Update();
            //Redirect to page
            var redirectUrl = Request.QueryString["Source"];
            if (redirectUrl == null || string.IsNullOrEmpty(redirectUrl.ToString()))
                redirectUrl = "/";
            longOperation.End(redirectUrl, Microsoft.SharePoint.Utilities.SPRedirectFlags.DoNotEndResponse, System.Web.HttpContext.Current, "");
        }

        void AddBoSungYeuCau(SPWeb web)
        {
            try
            {
                LoggingServices.LogMessage("Begin AddBoSungYeuCau");
                var yeuCauBoSungUrl = (web.ServerRelativeUrl + Constants.ListUrlYeuCauBoSung).Replace("//", "/");
                var yeuCauBoSungList = web.GetList(yeuCauBoSungUrl);
                var yeuCauBoSungItem = yeuCauBoSungList.Items.Add();
                yeuCauBoSungItem[Constants.FieldDeNghi] = SPContext.Current.ItemId;
                yeuCauBoSungItem[Constants.FieldTitle] = txtTieuDe.Text.Trim();
                yeuCauBoSungItem[Constants.FieldMoTa] = txtDienGiaiChiTiet.Text.Trim();
                yeuCauBoSungItem.Update();
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("End AddBoSungYeuCau");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}
