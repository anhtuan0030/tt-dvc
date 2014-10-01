using CamlexNET;
using LongAn.DVC.Common;
using LongAn.DVC.Helpers;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace LongAn.DVC.ControlTemplates.LongAn.DVC
{
    public partial class DeNghiCapPhepXeEditForm : UserControl
    {
        protected override void OnInit(EventArgs e)
        {
            btnCancel.Click += btnCancel_Click;
            //
            var currentStatus = int.Parse(SPContext.Current.ListItem[Constants.FieldTrangThai].ToString());
            var currentUserRole = CapXuLy.CaNhanToChuc;
            if (ViewState[Constants.ConfViewStateCapXuLy] == null)
            {
                currentUserRole = DeNghiHelper.CurrentUserRole(SPContext.Current.Web, SPContext.Current.Web.CurrentUser);
                ViewState[Constants.ConfViewStateCapXuLy] = currentUserRole;
            }
            else
                currentUserRole = (CapXuLy)ViewState[Constants.ConfViewStateCapXuLy];

            if (currentUserRole == CapXuLy.CaNhanToChuc && 
                (currentStatus == (int)TrangThaiHoSo.KhoiTao ||
                currentStatus == (int)TrangThaiHoSo.ChoBoSung))
            {
                divDanhSachYeuCauBoSung.Visible = true;
                if (!IsPostBack)
                    LoadYeuCauBoSung();

                btnSave.Visible = true;
                btnGuiHoSo.Visible = true;
                btnSave.Click += btnSave_Click;
                btnGuiHoSo.Click += btnGuiHoSo_Click;
            }

            base.OnInit(e);
        }

        void btnGuiHoSo_Click(object sender, EventArgs e)
        {
            UpdateItem(TrangThaiHoSo.ChoTiepNhan);
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.Page.IsValid)
                return;

            var longOperation = new SPLongOperation(this.Page);
            longOperation.LeadingHTML = "Please wait while the operation is running";
            longOperation.TrailingHTML = "Once the operation is finished you will be redirected to result page";
            longOperation.Begin();
            SaveButton.SaveItem(SPContext.Current, false, "Updated by " + SPContext.Current.Web.CurrentUser.LoginName);
            //Save file upload
            var itemId = SPContext.Current.ItemId;
            DeNghiHelper.SaveFileAttachment(fileUpload1, itemId, Constants.AttachmentGiayDangKy);
            DeNghiHelper.SaveFileAttachment(fileUpload2, itemId, Constants.AttachmentGiayChungNhanKiemDinh);
            DeNghiHelper.SaveFileAttachment(fileUpload3, itemId, Constants.AttachmentGiayCamKet);
            DeNghiHelper.SaveFileAttachment(fileUpload4, itemId, Constants.AttachmentCMND);
            //Redirect to page
            var redirectUrl = Request.QueryString["Source"];
            if (redirectUrl == null || string.IsNullOrEmpty(redirectUrl.ToString()))
                redirectUrl = "/";
            longOperation.End(redirectUrl, Microsoft.SharePoint.Utilities.SPRedirectFlags.DoNotEndResponse, HttpContext.Current, "");
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
            if (!IsPostBack)
            {
                DeNghiHelper.LoadAttachments(SPContext.Current.ItemId, Constants.AttachmentGiayDangKy, divFileUpload1);
                DeNghiHelper.LoadAttachments(SPContext.Current.ItemId, Constants.AttachmentGiayChungNhanKiemDinh, divFileUpload2);
                DeNghiHelper.LoadAttachments(SPContext.Current.ItemId, Constants.AttachmentGiayCamKet, divFileUpload3);
                DeNghiHelper.LoadAttachments(SPContext.Current.ItemId, Constants.AttachmentCMND, divFileUpload4);
            }
        }

        void UpdateItem(TrangThaiHoSo trangThai)
        {
            if (!this.Page.IsValid)
                return;

            var longOperation = new SPLongOperation(this.Page);
            longOperation.LeadingHTML = "Please wait while the operation is running";
            longOperation.TrailingHTML = "Once the operation is finished you will be redirected to result page";
            longOperation.Begin();
            SPContext.Current.ListItem[Constants.FieldTrangThai] = (int)trangThai;
            SPContext.Current.ListItem[Constants.FieldCapDuyet] = (int)CapXuLy.MotCua;
            SaveButton.SaveItem(SPContext.Current, false, "Updated by " + SPContext.Current.Web.CurrentUser.LoginName);
            //Save file upload
            var itemId = SPContext.Current.ItemId;
            DeNghiHelper.SaveFileAttachment(fileUpload1, itemId, Constants.AttachmentGiayDangKy);
            DeNghiHelper.SaveFileAttachment(fileUpload2, itemId, Constants.AttachmentGiayChungNhanKiemDinh);
            DeNghiHelper.SaveFileAttachment(fileUpload3, itemId, Constants.AttachmentGiayCamKet);
            DeNghiHelper.SaveFileAttachment(fileUpload4, itemId, Constants.AttachmentCMND);
            DeNghiHelper.AddDeNghiHistory(SPContext.Current.Web, CapXuLy.MotCua, itemId, HanhDong.NopHoSo.ToString(), string.Empty);
            //Redirect to page
            var redirectUrl = Request.QueryString["Source"];
            if (redirectUrl == null || string.IsNullOrEmpty(redirectUrl.ToString()))
                redirectUrl = "/";
            longOperation.End(redirectUrl, Microsoft.SharePoint.Utilities.SPRedirectFlags.DoNotEndResponse, HttpContext.Current, "");
        }

        #region YeuCauBoSung

        public void LoadYeuCauBoSung()
        {
            try
            {
                LoggingServices.LogMessage("Begin YeuCauBoSung");
                SPQuery caml = Camlex.Query().Where(x => x[Constants.FieldDeNghi] == (DataTypes.LookupId)SPContext.Current.ItemId.ToString())
                                    .OrderBy(x => new[] { x["ID"] as Camlex.Asc })
                                    .ToSPQuery();
                var yeuCauBoSungUrl = (SPContext.Current.Web.ServerRelativeUrl + Constants.ListUrlYeuCauBoSung).Replace("//", "/");
                var yeuCauBoSungList = SPContext.Current.Web.GetList(yeuCauBoSungUrl);
                var yeuCauBoSungItems = yeuCauBoSungList.GetItems(caml).GetDataTable();
                if (yeuCauBoSungItems != null && yeuCauBoSungItems.Rows.Count > 0)
                {
                    repeaterLists.DataSource = yeuCauBoSungItems;
                    repeaterLists.DataBind();
                }
                else
                {
                    divDanhSachYeuCauBoSung.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("Begin YeuCauBoSung");
        }

        void UpdateYeuCauBoSung(int itemId)
        {
            try
            {
                LoggingServices.LogMessage("Begin UpdateYeuCauBoSung");
                var yeuCauBoSungUrl = (SPContext.Current.Web.ServerRelativeUrl + Constants.ListUrlYeuCauBoSung).Replace("//", "/");
                var yeuCauBoSungList = SPContext.Current.Web.GetList(yeuCauBoSungUrl);
                var yeuCauBoSungItem = yeuCauBoSungList.GetItemById(itemId);
                yeuCauBoSungItem["DaBoSung"] = true;
                yeuCauBoSungItem["NgayBoSung"] = DateTime.Now;
                yeuCauBoSungItem.Update();
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("End UpdateYeuCauBoSung");
        }

        protected void repeaterLists_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                DataRowView rowView = (DataRowView)e.Item.DataItem;
                if (rowView != null)
                {
                    string commandAgrument = rowView["ID"].ToString();

                    Literal literalTitle = (Literal)e.Item.FindControl("literalTitle");
                    literalTitle.Text = rowView[Constants.FieldTitle].ToString();

                    Literal literalMoTa = (Literal)e.Item.FindControl("literalMoTa");
                    literalMoTa.Text = rowView[Constants.FieldMoTa].ToString();

                    Literal literalNgayYeuCau = (Literal)e.Item.FindControl("literalNgayYeuCau");
                    literalNgayYeuCau.Text = rowView[Constants.FieldCreated].ToString();

                    LinkButton lbtXacNhan = (LinkButton)e.Item.FindControl("lbtXacNhan");
                    LinkButton lbtDisable = (LinkButton)e.Item.FindControl("lbtDisable");
                    var trangThai = int.Parse(SPContext.Current.ListItem[Constants.FieldTrangThai].ToString());
                    if (trangThai == (int)TrangThaiHoSo.ChoBoSung)
                    {
                        lbtDisable.Style.Add("display", "none");
                        lbtXacNhan.CommandName = "XacNhanBoSungHoSo";
                        lbtXacNhan.Style.Add("display", "block");
                        lbtXacNhan.CommandArgument = commandAgrument;
                        lbtXacNhan.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn xóa hồ sơ này không?')) return false;";
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
        }

        protected void repeaterLists_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string commandText = e.CommandArgument.ToString();
            if (e.CommandName == "XacNhanBoSungHoSo")
            {
                try
                {
                    LoggingServices.LogMessage("Begin XacNhanBoSungHoSo, item id:" + commandText);
                    UpdateYeuCauBoSung(int.Parse(commandText));
                    LoadYeuCauBoSung();
                }
                catch (Exception ex)
                {
                    LoggingServices.LogException(ex);
                }
                LoggingServices.LogMessage("End XacNhanBoSungHoSo, item id:" + commandText);
            }
        }
        #endregion YeuCauBoSung
    }
}
