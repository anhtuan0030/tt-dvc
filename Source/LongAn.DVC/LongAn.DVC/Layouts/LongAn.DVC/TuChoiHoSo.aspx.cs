using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using LongAn.DVC.Common;
using LongAn.DVC.Helpers;
using System.Web;

namespace LongAn.DVC.Layouts.LongAn.DVC
{
    public partial class TuChoiHoSo : LayoutsPageBase
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

            if (action == Constants.ConfActionTC)
            {
                if (actionTocken == Constants.ConfQueryStringTC)
                {
                    btnSave.Visible = true;
                    if (currentUserRole == CapXuLy.MotCua)
                        btnSave.Click += btnTraHoSo_Click;
                    else if (currentUserRole == CapXuLy.CanBo)
                        btnSave.Click += btnCanBoTraHoSo_Click;
                    else if (currentUserRole == CapXuLy.TruongPhoPhong)
                        btnSave.Click += btnTruongPhongTraHoSo_Click;
                    else if (currentUserRole == CapXuLy.LanhDaoSo)
                        btnSave.Click += btnLanhDaoTraHoSo_Click;
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
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #region Tra Ho So
        void btnTraHoSo_Click(object sender, EventArgs e)
        {
            DeNghiHelper.AddDeNghiHistory(SPContext.Current.Web, CapXuLy.MotCua, SPContext.Current.ItemId, HanhDong.TuChoiHoSo.ToString(), txtLyDo.Text);
            DeNghiHelper.SendEmail(SPContext.Current.Web, SPContext.Current.ItemId, "được cấp phép");
            UpdateItem(TrangThaiHoSo.BiTuChoi, CapXuLy.CaNhanToChuc);
        }

        void btnCanBoTraHoSo_Click(object sender, EventArgs e)
        {
            DeNghiHelper.AddDeNghiHistory(SPContext.Current.Web, CapXuLy.CanBo, SPContext.Current.ItemId, HanhDong.TuChoiHoSo.ToString(), txtLyDo.Text);
            DeNghiHelper.SendEmail(SPContext.Current.Web, SPContext.Current.ItemId, "được cấp phép");
            UpdateItem(TrangThaiHoSo.BiTuChoi, CapXuLy.CaNhanToChuc);
        }

        void btnTruongPhongTraHoSo_Click(object sender, EventArgs e)
        {
            DeNghiHelper.AddDeNghiHistory(SPContext.Current.Web, CapXuLy.TruongPhoPhong, SPContext.Current.ItemId, HanhDong.TuChoiHoSo.ToString(), txtLyDo.Text);
            DeNghiHelper.SendEmail(SPContext.Current.Web, SPContext.Current.ItemId, "được cấp phép");
            UpdateItem(TrangThaiHoSo.BiTuChoi, CapXuLy.CaNhanToChuc);
        }

        void btnLanhDaoTraHoSo_Click(object sender, EventArgs e)
        {
            DeNghiHelper.AddDeNghiHistory(SPContext.Current.Web, CapXuLy.LanhDaoSo, SPContext.Current.ItemId, HanhDong.TuChoiHoSo.ToString(), txtLyDo.Text);
            DeNghiHelper.SendEmail(SPContext.Current.Web, SPContext.Current.ItemId, "bị từ chối");
            UpdateItem(TrangThaiHoSo.BiTuChoi, CapXuLy.CaNhanToChuc);
        }
        #endregion Tra Ho So

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
            spListItem[Constants.FieldLyDoTuChoi] = txtLyDo.Text.Trim();
            spListItem.Update();
            //Redirect to page
            var redirectUrl = Request.QueryString["Source"];
            if (redirectUrl == null || string.IsNullOrEmpty(redirectUrl.ToString()))
                redirectUrl = "/";
            longOperation.End(redirectUrl, Microsoft.SharePoint.Utilities.SPRedirectFlags.DoNotEndResponse, HttpContext.Current, "");
        }
    }
}
