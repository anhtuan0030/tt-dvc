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
    public partial class DeNghiCapPhepXeDispForm : UserControl
    {
        #region Properties
        //public CauHinh CauHinh
        //{
        //    get
        //    {
        //        if (ViewState["CauHinh"] != null)
        //        {
        //            return (CauHinh)ViewState["CauHinh"];
        //        }
        //        else
        //        {
        //            var buocDuyet = SPContext.Current.ListItem[Fields.BuocDuyet];
        //            var buocDuyetValue = new SPFieldLookupValue(buocDuyet.ToString()).LookupId;
        //            if (buocDuyetValue != 0)
        //            {
        //                var deNghi = DeNghiHelper.GetCauHinh(buocDuyetValue);
        //                ViewState["CauHinh"] = deNghi;
        //                return deNghi;
        //            }
        //            return null;
        //        }
        //    }
        //}
        #endregion Properties
        protected override void OnInit(EventArgs e)
        {
            btnCancel.Click += btnCancel_Click;
            btnDuyet.Click += btnDuyet_Click;
            btnTraHoSo.Click += btnTraHoSo_Click;
            btnTuChoi.Click += btnTuChoi_Click;
            btnPhanCong.Click += btnPhanCong_Click;
            btnCanBoTiepNhan.Click += btnCanBoTiepNhan_Click;
            btnYeuCauBoSung.Click += btnYeuCauBoSung_Click;

            base.OnInit(e);
        }

        void btnCanBoTiepNhan_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void btnYeuCauBoSung_Click(object sender, EventArgs e)
        {
            LoggingServices.LogMessage("Begin YeuCaBoSung Click");
            var cauHinhs = DeNghiHelper.GetCauHinh(Constants.CauHinh_YCBS);
            if (cauHinhs != null && cauHinhs.Count > 0)
            {
                var cauHinh = cauHinhs[0];
                UpdateItem(cauHinh.BuocDuyetID, cauHinh.TrangThai, cauHinh.CapDuyetText, true, false, SPContext.Current.Web.CurrentUser);
            }
            else
            {
                var redirectUrl = Request.QueryString["Source"];
                if (redirectUrl == null || string.IsNullOrEmpty(redirectUrl.ToString()))
                    redirectUrl = "/";
                Response.Redirect(redirectUrl);
            }
            LoggingServices.LogMessage("End YeuCaBoSung Click");
        }

        void btnPhanCong_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlUsers.SelectedValue))
            {
                var spUser = SPContext.Current.Web.Users.GetByID(int.Parse(ddlUsers.SelectedValue));
                UpdateItem(0, 0, "Phân công lại hồ sơ", false, false, spUser);
            }
        }      

        void btnTuChoi_Click(object sender, EventArgs e)
        {
            
        }

        void btnTraHoSo_Click(object sender, EventArgs e)
        {
            var cauHinh = DeNghiHelper.GetCauHinh(int.Parse(hdfPreStep.Value));
            UpdateItem(cauHinh.BuocDuyetID, cauHinh.TrangThai, cauHinh.CapDuyetText, true, false, SPContext.Current.Web.CurrentUser);
        }

        void btnDuyet_Click(object sender, EventArgs e)
        {
            var cauHinh = DeNghiHelper.GetCauHinh(int.Parse(hdfNextStep.Value));
            UpdateItem(cauHinh.BuocDuyetID, cauHinh.TrangThai, cauHinh.CapDuyetText, true, false, SPContext.Current.Web.CurrentUser);
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

                var isBoSungVal = SPContext.Current.ListItem[Fields.IsBoSungHoSo];
                var isBoSung =  isBoSungVal != null ? bool.Parse(isBoSungVal.ToString()) : false;
                if (isBoSung)
                {
                    var buocDuyet = SPContext.Current.ListItem[Fields.BuocDuyet];
                    var buocDuyetValue = new SPFieldLookupValue(buocDuyet.ToString()).LookupId;
                    var cauHinh = DeNghiHelper.GetCauHinh(buocDuyetValue);
                    if (cauHinh != null)
                    {
                        hdfNextStep.Value = cauHinh.NextStep.ToString();
                        hdfPreStep.Value = cauHinh.PreviousStep.ToString();
                        var currentMember = DeNghiHelper.IsCurrentUserInGroup(SPContext.Current.Web, cauHinh.SPGroup);
                        if (currentMember)
                        {
                            #region Action Duyet
                            if (cauHinh.ActionDuyet)
                            {
                                btnDuyet.Visible = true;
                                btnDuyet.Text = cauHinh.TieuDeActionDuyet != null ? cauHinh.TieuDeActionDuyet : "Duyệt hồ sơ";
                            }
                            #endregion Action Duyet

                            #region Action Tra Ho So
                            if (cauHinh.ActionTraHoSo)
                            {
                                btnTraHoSo.Visible = true;
                                btnTraHoSo.Text = cauHinh.TieuDeActionTraHoSo != null ? cauHinh.TieuDeActionTraHoSo : "Trả hồ sơ";
                            }
                            #endregion Action Tra Ho So

                            #region Action Tu Choi
                            if (cauHinh.ActionTuChoi)
                            {
                                btnTuChoi.Visible = true;
                            }
                            #endregion Action Tu Choi

                            #region Action Yeu Cau Bo Sung
                            if (cauHinh.ActionYeuCauBoSung)
                            {
                                btnYeuCauBoSung.Visible = true;
                                divYeuCauBoSung.Visible = true;
                            }
                            #endregion Action Yeu Cau Bo Sung

                            #region Cap Nhat Loai Duong
                            if (cauHinh.AllowCapNhatLoaiDuong)
                            {
                                divLoaiDuong.Visible = true;
                                divLoaiDuongDisp.Visible = false;
                                SPFieldMultiChoice loaiDuong = (SPFieldMultiChoice)SPContext.Current.List.Fields[new Guid(Constants.FieldIdLoaiDuong)];
                                chkListLoaiDuong.DataSource = loaiDuong.Choices;
                                chkListLoaiDuong.DataBind();

                                var currentItem = SPContext.Current.ListItem;
                                var loaiDuongString = currentItem[Constants.FieldLoaiDuong] != null ? currentItem[Constants.FieldLoaiDuong].ToString() : string.Empty;
                                for (int i = 0; i < chkListLoaiDuong.Items.Count; i++)
                                {
                                    if (string.IsNullOrEmpty(loaiDuongString))
                                        break;
                                    if (loaiDuongString.Contains(chkListLoaiDuong.Items[i].Text))
                                    {
                                        chkListLoaiDuong.Items[i].Selected = true;
                                    }
                                }

                                var lanDuong = currentItem[Constants.FieldLanXeDuocChay] != null ? currentItem[Constants.FieldLanXeDuocChay].ToString() : string.Empty;
                                txtLanXeDuocChay.Text = lanDuong;
                                var tocDo = currentItem[Constants.FieldTocDoDuocChay] != null ? currentItem[Constants.FieldTocDoDuocChay].ToString() : string.Empty;
                                txtTocDoDuocChay.Text = tocDo;
                            }
                            #endregion Cap Nhat Loai Duong

                            #region Cap Nhat Ngay Hen Tra
                            if (cauHinh.AllowCapNhatNgayHen)
                            {
                                divNgayHen.Visible = true;
                            }
                            #endregion Cap Nhat Ngay Hen Tra
                        }
                        
                        #region Action Phan Cong
                        if (cauHinh.ActionPhanCong)
                        {
                            
                            var isPhanCongVal = SPContext.Current.ListItem[Fields.IsPhanCong];
                            var isPhanCong = isPhanCongVal != null ? bool.Parse(isPhanCongVal.ToString()) : false;
                            var isTPP = DeNghiHelper.IsCurrentUserInGroup(SPContext.Current.Web, cauHinh.SPGroupPhanCong);
                            if (isPhanCong && isTPP)
                            {
                                btnPhanCong.Visible = true;
                                divPhanCongHoSo.Visible = true;
                                var dataTable = GetCanBoXuLy(cauHinh.SPGroupTiepNhan);
                                ddlUsers.DataSource = dataTable;
                                ddlUsers.DataValueField = "ID";
                                ddlUsers.DataTextField = "Name";
                                ddlUsers.DataBind();
                            }
                            var isCanBoXuLy = DeNghiHelper.IsCurrentUserInGroup(SPContext.Current.Web, cauHinh.SPGroupTiepNhan);
                            if(!isPhanCong && isCanBoXuLy)
                            {
                                btnCanBoTiepNhan.Visible = true;
                            }
                        }
                        #endregion Action Phan Cong
                    }

                }
            }
        }

        void UpdateItem(int buocDuyet, int trangThai, string note, bool isCapNhatBuocDuyet, bool isTiepNhan, SPUser spUser)
        {
            if (!this.Page.IsValid)
                return;

            var longOperation = new SPLongOperation(this.Page);
            longOperation.LeadingHTML = "Please wait while the operation is running";
            longOperation.TrailingHTML = "Once the operation is finished you will be redirected to result page";
            longOperation.Begin();
            
            var deNghiList = SPContext.Current.List;
            var spListItem = deNghiList.GetItemById(SPContext.Current.ItemId);
            //var currentUser = SPContext.Current.Web.CurrentUser;
            spListItem[Fields.NguoiXuLy] = spUser.ID;

            var nguoiThamGiaXuLy = spListItem[Fields.NguoiThamGiaXuLy];
            if (nguoiThamGiaXuLy != null && !string.IsNullOrEmpty(nguoiThamGiaXuLy.ToString()))
            {
                var nguoiThamGiaXuLyLookup = new SPFieldLookupValueCollection(nguoiThamGiaXuLy.ToString());
                nguoiThamGiaXuLyLookup.Add(new SPFieldLookupValue(spUser.ID, spUser.LoginName));
                spListItem[Fields.NguoiThamGiaXuLy] = nguoiThamGiaXuLyLookup;
            }
            else
            {
                spListItem[Fields.NguoiThamGiaXuLy] = spUser.ID;
            }
            spListItem[Fields.NoteAppend] = txtNhanXet.Text.Trim();
            //Cap nhat buoc duyet va trang thai
            if (isCapNhatBuocDuyet)
            {
                spListItem[Fields.TrangThai] = trangThai;
                spListItem[Fields.BuocDuyet] = buocDuyet;
            }
            //Cap nhat tiep nhan ho so
            if (isTiepNhan)
            {
                spListItem[Fields.IsPhanCong] = true;
            }
            //Cap nhat loai duong
            if (divLoaiDuong.Visible)
            {
                SPFieldMultiChoiceValue values = new SPFieldMultiChoiceValue();
                for (int i = 0; i < chkListLoaiDuong.Items.Count; i++)
                {
                    if (chkListLoaiDuong.Items[i].Selected)
                    {
                        values.Add(chkListLoaiDuong.Items[i].Text);
                    }
                }
                spListItem[Fields.LoaiDuong] = values;
                spListItem[Fields.LanXeDuocChay] = txtLanXeDuocChay.Text.Trim();
                spListItem[Fields.TocDoDuocChay] = txtTocDoDuocChay.Text.Trim();
            }
            //Cap nhat ngay hen tra
            if (divNgayHen.Visible)
            {
                spListItem[Fields.NgayHenTra] = dtcNgayHenTra.SelectedDate;
            }
            //Cap nhat yeu cau bo sung
            if (divYeuCauBoSung.Visible)
            {
                AddBoSungYeuCau(spListItem.Title, spListItem.ID, txtNhanXet.Text.Trim(), spUser.ID);
            }
            //Cap nhat phan cong ho so
            spListItem.Update();
            //Log to history
            DeNghiHelper.AddDeNghiHistory(SPContext.Current.Web, SPContext.Current.ItemId, SPContext.Current.ListItem.Title, buocDuyet, trangThai, note);
            //Redirect to page
            var redirectUrl = Request.QueryString["Source"];
            if (redirectUrl == null || string.IsNullOrEmpty(redirectUrl.ToString()))
                redirectUrl = "/";
            longOperation.End(redirectUrl, Microsoft.SharePoint.Utilities.SPRedirectFlags.DoNotEndResponse, HttpContext.Current, "");
        }

        void AddBoSungYeuCau(string soBienNhan, int deNghiId, string noiDung, int nguoiYeuCau)
        {
            try
            {
                var spWeb = SPContext.Current.Web;
                LoggingServices.LogMessage("Begin AddBoSungYeuCau");
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(spWeb.Site.ID))
                    {
                        using (SPWeb web = site.OpenWeb(spWeb.ID))
                        {
                            web.AllowUnsafeUpdates = true;
                            var yeuCauBoSungUrl = (web.ServerRelativeUrl + Constants.ListUrlYeuCauBoSung).Replace("//", "/");
                            var yeuCauBoSungList = web.GetList(yeuCauBoSungUrl);
                            var yeuCauBoSungItem = yeuCauBoSungList.Items.Add();
                            yeuCauBoSungItem[Fields.Title] = soBienNhan;
                            yeuCauBoSungItem[Fields.MoTa] = noiDung;
                            yeuCauBoSungItem[Fields.DeNghi] = deNghiId;
                            yeuCauBoSungItem[Fields.NguoiYeuCau] = nguoiYeuCau;
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
            LoggingServices.LogMessage("End AddBoSungYeuCau");
        }

        DataTable GetCanBoXuLy(SPGroup group)
        {
            if (group == null)
                return null;
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
                            var canBoGroup = web.SiteGroups.GetByID(group.ID);
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
