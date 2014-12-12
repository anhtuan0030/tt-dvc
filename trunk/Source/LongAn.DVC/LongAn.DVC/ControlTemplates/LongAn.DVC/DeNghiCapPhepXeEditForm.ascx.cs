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
            btnSave.Click +=btnSave_Click;
            btnNopHoSo.Click += btnNopHoSo_Click;
            btnBoSungHoSo.Click += btnBoSungHoSo_Click;
            repeaterFileUpload1.ItemDataBound += repeaterFileUpload_ItemDataBound;
            repeaterFileUpload2.ItemDataBound += repeaterFileUpload_ItemDataBound;
            repeaterFileUpload3.ItemDataBound += repeaterFileUpload_ItemDataBound;
            repeaterFileUpload4.ItemDataBound += repeaterFileUpload_ItemDataBound;

            repeaterFileUpload1.ItemCommand += repeaterFileUpload_ItemCommand;
            repeaterFileUpload2.ItemCommand += repeaterFileUpload_ItemCommand;
            repeaterFileUpload3.ItemCommand += repeaterFileUpload_ItemCommand;
            repeaterFileUpload4.ItemCommand += repeaterFileUpload_ItemCommand;
            //
            base.OnInit(e);
        }

        void repeaterFileUpload_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DeleteFileUpload")
            {
                string commandText = e.CommandArgument.ToString();
                try
                {
                    LoggingServices.LogMessage("Begin DeleteFileUpload, item id:" + commandText);

                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {
                        using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                        {
                            using (SPWeb web = site.OpenWeb(SPContext.Current.Web.ID))
                            {
                                web.AllowUnsafeUpdates = true;
                                var deNghiUrl = (web.ServerRelativeUrl + Constants.ListUrlDeNghiAttachment).Replace("//", "/");
                                var deNghiAttachmentList = web.GetList(deNghiUrl);
                                var commandTextArray = commandText.Split('|');
                                var fileUpload = deNghiAttachmentList.GetItemById(int.Parse(commandTextArray[0]));
                                fileUpload.Delete();
                                switch (commandTextArray[1])
                                {
                                    case Constants.AttachmentGiayDangKy:
                                        repeaterFileUpload1.DataSource = DeNghiHelper.LoadAttachments(SPContext.Current.ItemId, Constants.AttachmentGiayDangKy);
                                        repeaterFileUpload1.DataBind();
                                        break;
                                    case Constants.AttachmentGiayChungNhanKiemDinh:
                                        repeaterFileUpload2.DataSource = DeNghiHelper.LoadAttachments(SPContext.Current.ItemId, Constants.AttachmentGiayChungNhanKiemDinh);
                                        repeaterFileUpload2.DataBind();
                                        break;
                                    case Constants.AttachmentGiayCamKet:
                                        repeaterFileUpload3.DataSource = DeNghiHelper.LoadAttachments(SPContext.Current.ItemId, Constants.AttachmentGiayCamKet);
                                        repeaterFileUpload3.DataBind();
                                        break;
                                    default:
                                        repeaterFileUpload4.DataSource = DeNghiHelper.LoadAttachments(SPContext.Current.ItemId, Constants.AttachmentCMND);
                                        repeaterFileUpload4.DataBind();
                                        break;
                                }
                                web.AllowUnsafeUpdates = false;
                            }
                        }
                    });
                    
                }
                catch (Exception ex)
                {
                    LoggingServices.LogException(ex);
                }
                LoggingServices.LogMessage("End DeleteFileUpload, item id:" + commandText);
            }
        }

        void repeaterFileUpload_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                LoggingServices.LogMessage("Begin ItemDataBound FileUplad");
                DataRowView rowView = (DataRowView)e.Item.DataItem;
                if (rowView != null)
                {
                    string commandAgrument = rowView["ID"].ToString();
                    commandAgrument += "|" + rowView[Constants.FieldLoaiAttachment].ToString();
                    var deNghiUrl = (SPContext.Current.Web.ServerRelativeUrl + Constants.ListUrlDeNghiAttachment).Replace("//", "/");
                    HyperLink hplFile = (HyperLink)e.Item.FindControl("hplFile");
                    hplFile.NavigateUrl = string.Format("{0}/{1}", deNghiUrl, rowView["LinkFileName"]);
                    hplFile.Text = rowView["LinkFileName"].ToString().Substring(18);
                    hplFile.Target = "_blank";
                    
                    LinkButton lbtDelete = (LinkButton)e.Item.FindControl("lbtDelete");
                    lbtDelete.CommandArgument = commandAgrument;
                    lbtDelete.CommandName = "DeleteFileUpload";
                    lbtDelete.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn xóa file đính kèm này không?')) return false;";
                }
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("End ItemDataBound FileUplad");
        }

        void btnBoSungHoSo_Click(object sender, EventArgs e)
        {
            var cauHinh = DeNghiHelper.GetCauHinh(int.Parse(hdfNextStep.Value));
            UpdateItem(cauHinh, true, false);
        }

        void btnNopHoSo_Click(object sender, EventArgs e)
        {
            var cauHinh = DeNghiHelper.GetCauHinh(int.Parse(hdfNextStep.Value));
            UpdateItem(cauHinh, true, false);
            //Log to history
            DeNghiHelper.AddDeNghiHistory(SPContext.Current.Web
                , SPContext.Current.ListItem.ID
                , SPContext.Current.ListItem.Title
                , int.Parse(hdfCauHinhID.Value)
                , int.Parse(hdfTrangThaiID.Value)
                , hdfCapDuyetText.Value);
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            UpdateItem(null, false, false);
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
                var buocDuyet = SPContext.Current.ListItem[Fields.BuocDuyet];
                var buocDuyetValue = new SPFieldLookupValue(buocDuyet.ToString()).LookupId;
                var cauHinh = DeNghiHelper.GetCauHinh(buocDuyetValue);
                if (cauHinh != null)
                {
                    var spListItem = SPContext.Current.ListItem;
                    var nguoiDeNghi = spListItem[Fields.NguoiDeNghi];
                    var nguoiDeNghiVal = new SPFieldLookupValue(nguoiDeNghi.ToString());
                    if (nguoiDeNghiVal.LookupId == SPContext.Current.Web.CurrentUser.ID
                        && (cauHinh.StartEnd == Constants.CauHinh_YCBS || cauHinh.StartEnd == Constants.CauHinh_Start))
                    {
                        //Show button
                        hdfCauHinhID.Value = cauHinh.BuocDuyetID.ToString();
                        hdfTrangThaiID.Value = cauHinh.TrangThai.ToString();
                        hdfCapDuyetText.Value = cauHinh.TrangThai.ToString();
                        hdfNextStep.Value = cauHinh.NextStep.ToString();
                        //hdfPreStep.Value = cauHinh.PreviousStep.ToString();
                        if (cauHinh.StartEnd == Constants.CauHinh_Start)
                            btnNopHoSo.Visible = true;
                        //Load yeu cau bo sung
                        LoadYeuCauBoSung();
                    }
                    else
                    {
                        LoggingServices.LogMessage("Invalid user edit - current NguoiDeNghi: " + nguoiDeNghiVal.LookupValue);
                        var redirectUrl = Request.QueryString["Source"];
                        if (redirectUrl == null || string.IsNullOrEmpty(redirectUrl.ToString()))
                            redirectUrl = "/";
                        Response.Redirect(redirectUrl, true);
                    }
                }

                //Load attachment
                repeaterFileUpload1.DataSource = DeNghiHelper.LoadAttachments(SPContext.Current.ItemId, Constants.AttachmentGiayDangKy);
                repeaterFileUpload1.DataBind();
                repeaterFileUpload2.DataSource = DeNghiHelper.LoadAttachments(SPContext.Current.ItemId, Constants.AttachmentGiayChungNhanKiemDinh);
                repeaterFileUpload2.DataBind();
                repeaterFileUpload3.DataSource = DeNghiHelper.LoadAttachments(SPContext.Current.ItemId, Constants.AttachmentGiayCamKet);
                repeaterFileUpload3.DataBind();
                repeaterFileUpload4.DataSource = DeNghiHelper.LoadAttachments(SPContext.Current.ItemId, Constants.AttachmentCMND);
                repeaterFileUpload4.DataBind();
            }
        }

        void UpdateItem(CauHinh newCauHinh, bool isCapNhatBuocDuyet, bool isBoSungHoSo)
        {
            if (!this.Page.IsValid)
                return;

            var longOperation = new SPLongOperation(this.Page);
            longOperation.LeadingHTML = "Please wait while the operation is running";
            longOperation.TrailingHTML = "Once the operation is finished you will be redirected to result page";
            longOperation.Begin();
            var spListItem = SPContext.Current.ListItem;
            var currentUser = SPContext.Current.Web.CurrentUser;
            //Cap nhat buoc duyet va trang thai
            if (isCapNhatBuocDuyet)
            {
                spListItem[Fields.TrangThai] = newCauHinh != null ? newCauHinh.TrangThai : new SPFieldLookupValue(SPContext.Current.ListItem[Fields.TrangThai].ToString()).LookupId;
                spListItem[Fields.BuocDuyet] = newCauHinh != null ? newCauHinh.BuocDuyetID : new SPFieldLookupValue(SPContext.Current.ListItem[Fields.BuocDuyet].ToString()).LookupId;
                //Cap nhat nguoi cho xu ly
                var nguoiChoXuLy = new SPFieldUserValueCollection();
                foreach (SPUser user in newCauHinh.SPGroup.Users)
                {
                    nguoiChoXuLy.Add(new SPFieldUserValue(SPContext.Current.Web, user.ID, user.LoginName));
                }
                spListItem[Fields.NguoiChoXuLy] = nguoiChoXuLy;
            }
            spListItem[Fields.NguoiXuLy] = currentUser.ID;
            //Save item
            SaveButton.SaveItem(SPContext.Current, false, "Updated by " + currentUser.LoginName);
            //Save file upload
            var itemId = SPContext.Current.ItemId;
            DeNghiHelper.SaveFileAttachment(fileUpload1, itemId, Constants.AttachmentGiayDangKy);
            DeNghiHelper.SaveFileAttachment(fileUpload2, itemId, Constants.AttachmentGiayChungNhanKiemDinh);
            DeNghiHelper.SaveFileAttachment(fileUpload3, itemId, Constants.AttachmentGiayCamKet);
            DeNghiHelper.SaveFileAttachment(fileUpload4, itemId, Constants.AttachmentCMND);
            //Add history (không add khi save item)
            //DeNghiHelper.AddDeNghiHistory(SPContext.Current.Web, CapXuLy.MotCua, itemId, HanhDong.NopHoSo.ToString(), string.Empty);
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
                SPQuery caml = Camlex.Query().Where(x => x[Fields.DeNghi] == (DataTypes.LookupId)SPContext.Current.ItemId.ToString())
                                    .OrderBy(x => new[] { x["ID"] as Camlex.Desc })
                                    .ToSPQuery();
                var yeuCauBoSungUrl = (SPContext.Current.Web.ServerRelativeUrl + Constants.ListUrlYeuCauBoSung).Replace("//", "/");
                var yeuCauBoSungList = SPContext.Current.Web.GetList(yeuCauBoSungUrl);
                var yeuCauBoSungItems = yeuCauBoSungList.GetItems(caml).GetDataTable();
                if (yeuCauBoSungItems != null && yeuCauBoSungItems.Rows.Count > 0)
                {
                    var boSungRow = yeuCauBoSungItems.Select("DaBoSung=0");
                    if (boSungRow.Length == 0)
                        btnBoSungHoSo.Visible = true;
                    repeaterLists.DataSource = yeuCauBoSungItems;
                    repeaterLists.DataBind();
                    divDanhSachYeuCauBoSung.Visible = true;
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
                var spWeb = SPContext.Current.Web;
                LoggingServices.LogMessage("Begin UpdateYeuCauBoSung");
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(spWeb.Site.ID))
                    {
                        using (SPWeb web = site.OpenWeb(spWeb.ID))
                        {
                            web.AllowUnsafeUpdates = true;
                            var yeuCauBoSungUrl = (web.ServerRelativeUrl + Constants.ListUrlYeuCauBoSung).Replace("//", "/");
                            var yeuCauBoSungList = web.GetList(yeuCauBoSungUrl);
                            var yeuCauBoSungItem = yeuCauBoSungList.GetItemById(itemId);
                            yeuCauBoSungItem[Fields.DaBoSung] = true;
                            yeuCauBoSungItem[Fields.NgayBoSung] = DateTime.Now;
                            yeuCauBoSungItem.Update();
                            web.AllowUnsafeUpdates = false;
                        }
                    }
                });
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
                    literalTitle.Text = rowView[Fields.Title].ToString();

                    Literal literalMoTa = (Literal)e.Item.FindControl("literalMoTa");
                    literalMoTa.Text = rowView[Fields.MoTa].ToString();

                    Literal literalNgayYeuCau = (Literal)e.Item.FindControl("literalNgayYeuCau");
                    literalNgayYeuCau.Text = rowView[Constants.FieldCreated].ToString();

                    LinkButton lbtXacNhan = (LinkButton)e.Item.FindControl("lbtXacNhan");
                    LinkButton lbtDisable = (LinkButton)e.Item.FindControl("lbtDisable");
                    if (rowView["DaBoSung"].ToString() == "0")
                    {
                        lbtDisable.Style.Add("display", "none");
                        lbtXacNhan.CommandName = "XacNhanBoSungHoSo";
                        lbtXacNhan.Style.Add("display", "block");
                        lbtXacNhan.CommandArgument = commandAgrument;
                        lbtXacNhan.OnClientClick = "if (!confirm('Bạn có chắc chắn muốn xác nhận đã bổ sung không?')) return false;";
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
