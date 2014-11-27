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
        protected override void OnInit(EventArgs e)
        {
            btnCancel.Click += btnCancel_Click;
            btnDuyet.Click += btnDuyet_Click;
            btnTraHoSo.Click += btnTraHoSo_Click;
            btnTuChoi.Click += btnTuChoi_Click;
            btnPhanCong.Click += btnPhanCong_Click;
            btnCanBoTiepNhan.Click += btnCanBoTiepNhan_Click;
            btnYeuCauBoSung.Click += btnYeuCauBoSung_Click;

            btnInBienNhan.Click += btnInBienNhan_Click;
            btnInGiayPhep.Click += btnInGiayPhep_Click;

            base.OnInit(e);
        }

        void btnInGiayPhep_Click(object sender, EventArgs e)
        {
            InPhieuBienNhan(PrintType.GiayCapPhep, SPContext.Current.ItemId);
        }

        void btnInBienNhan_Click(object sender, EventArgs e)
        {
            InPhieuBienNhan(PrintType.PhieuBienNhan, SPContext.Current.ItemId);
        }

        void btnCanBoTiepNhan_Click(object sender, EventArgs e)
        {
            LoggingServices.LogMessage("CanBoTiepNhan Click");
            var cauHinh = DeNghiHelper.GetCauHinh(int.Parse(hdfNextStep.Value));
            UpdateItem(cauHinh
                , true
                , false
                , SPContext.Current.Web.CurrentUser
                , btnCanBoTiepNhan.Text);// Cán bộ tiếp nhận hồ sơ
        }

        void btnYeuCauBoSung_Click(object sender, EventArgs e)
        {
            LoggingServices.LogMessage("YeuCaBoSung Click");
            var cauHinhs = DeNghiHelper.GetCauHinh(Constants.CauHinh_YCBS);
            if (cauHinhs != null && cauHinhs.Count > 0)
            {
                var cauHinh = cauHinhs[0];
                UpdateItem(cauHinh
                    , true
                    , true
                    , SPContext.Current.Web.CurrentUser
                    , btnYeuCauBoSung.Text); //Yêu cầu bổ sung hồ sơ
            }
            else
            {
                var redirectUrl = Request.QueryString["Source"];
                if (redirectUrl == null || string.IsNullOrEmpty(redirectUrl.ToString()))
                    redirectUrl = "/";
                Response.Redirect(redirectUrl);
            }
        }

        void btnPhanCong_Click(object sender, EventArgs e)
        {
            LoggingServices.LogMessage("PhanCongLai Click");
            if (!string.IsNullOrEmpty(ddlUsers.SelectedValue))
            {
                var spUserValue = new SPFieldUserValue(SPContext.Current.Web, int.Parse(ddlUsers.SelectedValue), ddlUsers.SelectedItem.Text);
                //SPContext.Current.Web.Users.GetByID(int.Parse(ddlUsers.SelectedValue));
                UpdateItem(null
                    , false
                    , false
                    , spUserValue.User
                    , btnPhanCong.Text);//Phân công lại hồ sơ
            }
        }      

        void btnTuChoi_Click(object sender, EventArgs e)
        {
            LoggingServices.LogMessage("TuChoi Click");
            var cauHinhs = DeNghiHelper.GetCauHinh(Constants.CauHinh_TCHS);
            if (cauHinhs != null && cauHinhs.Count > 0)
            {
                var cauHinh = cauHinhs[0];
                UpdateItem(cauHinh
                    , true
                    , false
                    , SPContext.Current.Web.CurrentUser
                    , btnTuChoi.Text); //Từ chối cấp phép hồ sơ
            }
            else
            {
                var redirectUrl = Request.QueryString["Source"];
                if (redirectUrl == null || string.IsNullOrEmpty(redirectUrl.ToString()))
                    redirectUrl = "/";
                Response.Redirect(redirectUrl);
            }
        }

        void btnTraHoSo_Click(object sender, EventArgs e)
        {
            LoggingServices.LogMessage("TraHoSo Click");
            var cauHinh = DeNghiHelper.GetCauHinh(int.Parse(hdfPreStep.Value));
            UpdateItem(cauHinh
                , true
                , false
                , SPContext.Current.Web.CurrentUser
                , hdfCapDuyetText.Value);
        }

        void btnDuyet_Click(object sender, EventArgs e)
        {
            LoggingServices.LogMessage("Duyet Click");
            var cauHinh = DeNghiHelper.GetCauHinh(int.Parse(hdfNextStep.Value));
            UpdateItem(cauHinh
                , true
                , false
                , SPContext.Current.Web.CurrentUser
                , hdfCapDuyetText.Value);
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
                if (cauHinh != null && !cauHinh.IsBoSungHoSo)
                {
                    hdfCauHinhID.Value = cauHinh.BuocDuyetID.ToString();
                    hdfTrangThaiID.Value = cauHinh.TrangThai.ToString();
                    hdfCapDuyetText.Value = cauHinh.CapDuyetText;
                    hdfNextStep.Value = cauHinh.NextStep.ToString();
                    hdfPreStep.Value = cauHinh.PreviousStep.ToString();
                    //var currentMember = DeNghiHelper.IsCurrentUserInGroup(SPContext.Current.Web, cauHinh.SPGroup);
                    var nguoiChoXuLy = Convert.ToString(SPContext.Current.ListItem[Fields.NguoiChoXuLy]);
                    var currentMember = DeNghiHelper.IsCurrentUserInUserCollection(SPContext.Current.Web, nguoiChoXuLy);
                    if (currentMember)
                    {
                        #region Action Duyet
                        if (cauHinh.ActionDuyet)
                        {
                            btnDuyet.Visible = true;
                            btnDuyet.Text = cauHinh.TieuDeActionDuyet != null ? cauHinh.TieuDeActionDuyet : "Duyệt hồ sơ";
                            if (cauHinh.ActionPhanCong && !cauHinh.IsPhanCong)
                                divPhanCongHoSo.Visible = true;
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
                            //divYeuCauBoSung.Visible = true;
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
                            var ngayHenTraVal = DateTime.Now;
                            var ngayHenTra = SPContext.Current.ListItem[Fields.NgayHenTra];
                            if (ngayHenTra != null && !string.IsNullOrEmpty(ngayHenTra.ToString()))
                            {
                                ngayHenTraVal = DateTime.Parse(ngayHenTra.ToString());
                            }
                            else
                            {
                                var ngayTiepNhan = SPContext.Current.ListItem[Fields.NgayTiepNhan];
                                var ngayTiepNhanVal = DateTime.Now;
                                if (ngayTiepNhan != null && !string.IsNullOrEmpty(ngayTiepNhan.ToString()))
                                    ngayTiepNhanVal = DateTime.Parse(ngayTiepNhan.ToString());
                                ngayHenTraVal = CalThamSoNgay(ngayTiepNhanVal, Constants.ConfSoNgayXuLyKey);
                            }
                            dtcNgayHenTra.SelectedDate = ngayHenTraVal;
                        }
                        #endregion Cap Nhat Ngay Hen Tra

                        #region Allow In giay bien nhan
                        if (cauHinh.AllowInBienNhan)
                        {
                            btnInBienNhan.Visible = true;
                        }
                        #endregion Allow In giay bien nhan

                        #region Allow In giay cap phep
                        if (cauHinh.AllowInGiayPhep)
                        {
                            btnInGiayPhep.Visible = true;
                        }
                        #endregion Allow In giay cap phep
                    }

                    #region Action Phan Cong
                    if (cauHinh.ActionPhanCong)
                    {
                        var dataTable = GetCanBoXuLy(cauHinh.SPGroupTiepNhan);
                        ddlUsers.DataSource = dataTable;
                        ddlUsers.DataValueField = "ID";
                        ddlUsers.DataTextField = "Name";
                        ddlUsers.DataBind();

                        var isTPP = DeNghiHelper.IsCurrentUserInGroup(SPContext.Current.Web, cauHinh.SPGroupPhanCong);
                        if (cauHinh.IsPhanCong && isTPP)
                        {
                            divPhanCongHoSo.Visible = true;
                            btnPhanCong.Visible = true;
                        }
                        var isCanBoXuLy = DeNghiHelper.IsCurrentUserInGroup(SPContext.Current.Web, cauHinh.SPGroupTiepNhan);
                        if (!cauHinh.IsPhanCong && isCanBoXuLy)
                        {
                            btnCanBoTiepNhan.Visible = true;
                            divPhanCongHoSo.Visible = false;
                        }
                    }
                    #endregion Action Phan Cong
                }
            }
            //Load attachment file
            DeNghiHelper.LoadAttachments(SPContext.Current.ItemId, Constants.AttachmentGiayDangKy, divFileUpload1);
            DeNghiHelper.LoadAttachments(SPContext.Current.ItemId, Constants.AttachmentGiayChungNhanKiemDinh, divFileUpload2);
            DeNghiHelper.LoadAttachments(SPContext.Current.ItemId, Constants.AttachmentGiayCamKet, divFileUpload3);
            DeNghiHelper.LoadAttachments(SPContext.Current.ItemId, Constants.AttachmentCMND, divFileUpload4);
        }


        private DateTime CalThamSoNgay(DateTime ngayTiepNhan, string key)
        {
            DateTime output = ngayTiepNhan;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                    {
                        using (SPWeb web = site.OpenWeb(SPContext.Current.Web.ID))
                        {
                            var thamSoUrl = (web.ServerRelativeUrl + Constants.ListUrlThamSo).Replace("//", "/");
                            var thamSoList = web.GetList(thamSoUrl);
                            SPQuery caml = Camlex.Query().Where(x => (string)x[Fields.Title] == key)
                                    .ToSPQuery();
                            var thamSoItem = thamSoList.GetItems(caml);
                            if (thamSoItem != null && thamSoItem.Count > 0)
                            {
                                //Add
                                int soNgayXuLy = 0;
                                int.TryParse(thamSoItem[0]["Value"].ToString(), out soNgayXuLy);
                                output = output.AddDays(soNgayXuLy);

                                //Get weekday
                                int weekDay = DeNghiHelper.GetFullWorkingDaysBetween(ngayTiepNhan, output);
                                soNgayXuLy = soNgayXuLy + (soNgayXuLy - weekDay);
                                output = ngayTiepNhan.AddDays(soNgayXuLy);

                                //Get holiday
                                var ngayNghiUrl = (web.ServerRelativeUrl + Constants.ListUrlNgayLe).Replace("//", "/");
                                var ngayNghiList = web.GetList(ngayNghiUrl);
                                caml = Camlex.Query().Where(x => (DateTime)x["Ngay"] >= ngayTiepNhan && (DateTime)x["Date"] <= output)
                                    .ToSPQuery();
                                var ngayLeItems = ngayNghiList.GetItems(caml);
                                if (ngayLeItems != null && ngayLeItems.Count > 0)
                                {
                                    output.AddDays(ngayLeItems.Count);
                                }
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            return output;
        }

        void UpdateItem(CauHinh newCauHinh, bool isCapNhatBuocDuyet, bool isYeuCauBoSung, SPUser spUser, string capDuyetText)
        {
            if (!this.Page.IsValid)
                return;

            var longOperation = new SPLongOperation(this.Page);
            longOperation.LeadingHTML = "Please wait while the operation is running";
            longOperation.TrailingHTML = "Once the operation is finished you will be redirected to result page";
            longOperation.Begin();
            
            var deNghiList = SPContext.Current.List;
            var spListItem = deNghiList.GetItemById(SPContext.Current.ItemId);
            spListItem[Fields.NguoiXuLy] = spUser.ID;

            var nguoiThamGiaXuLy = Convert.ToString(spListItem[Fields.NguoiThamGiaXuLy]);
            if (!string.IsNullOrEmpty(nguoiThamGiaXuLy.ToString()))
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
            //Cap nhat tiep nhan ho so
            if (newCauHinh != null && newCauHinh.StartEnd == Constants.CauHinh_TNHS)
            {
                spListItem[Fields.NgayTiepNhan] = DateTime.Now;
            }
            //Cap nhat ngay thuc tra ho so
            if (newCauHinh != null && newCauHinh.StartEnd == Constants.CauHinh_End)
            {
                spListItem[Fields.NgayThucTra] = DateTime.Now;
            }
            //Cap nhat ngay thuc tra ho so
            if (newCauHinh != null && newCauHinh.StartEnd == Constants.CauHinh_YCBS)
            {
                spListItem[Fields.NguoiChoXuLy] = spListItem[Fields.NguoiDeNghi];
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
            if (isYeuCauBoSung)// divYeuCauBoSung.Visible)
            {
                AddBoSungYeuCau(spListItem.Title, spListItem.ID, txtNhanXet.Text.Trim(), spUser.ID);
            }
            //Cap nhat phan cong ho so
            if (divPhanCongHoSo.Visible)
            {
                //var spUserValue = new SPFieldUserValue(SPContext.Current.Web, int.Parse(ddlUsers.SelectedValue), ddlUsers.SelectedItem.Text);
                spListItem[Fields.NguoiChoXuLy] = ddlUsers.SelectedValue;
                spListItem[Fields.NguoiXuLy] = ddlUsers.SelectedValue;
            }
            spListItem.Update();

            //Log to history
            DeNghiHelper.AddDeNghiHistory(SPContext.Current.Web
                , SPContext.Current.ItemId
                , SPContext.Current.ListItem.Title
                , int.Parse(hdfCauHinhID.Value)
                , int.Parse(hdfTrangThaiID.Value)
                , capDuyetText);

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

        void InPhieuBienNhan(PrintType type, int itemId)
        {
            try
            {
                LoggingServices.LogMessage("Begin InPhieuBienNhan");
                var web = SPContext.Current.Web;
                var deNghiList = web.GetList((web.ServerRelativeUrl + Constants.ListUrlDeNghiCapPhep).Replace("//", "/"));
                string wordLicFile = Microsoft.SharePoint.Utilities.SPUtility.GetVersionedGenericSetupPath(Constants.ConfWordLicFile, 15);
                Aspose.Words.License wordLic = new Aspose.Words.License();
                wordLic.SetLicense(wordLicFile);

                string templateFile = string.Empty;
                SPQuery caml = Camlex.Query().Where(x => x["ID"] == (DataTypes.Counter)itemId.ToString())
                                                    .ToSPQuery();
                if (type == PrintType.PhieuBienNhan)
                    templateFile = Microsoft.SharePoint.Utilities.SPUtility.GetVersionedGenericSetupPath(Constants.ConfWordBienNhanTemplate, 15);
                else if (type == PrintType.GiayCapPhep)
                    templateFile = Microsoft.SharePoint.Utilities.SPUtility.GetVersionedGenericSetupPath(Constants.ConfWordGiayPhepTemplate, 15);

                Aspose.Words.Document doc = new Aspose.Words.Document(templateFile);
                var deNghiItem = deNghiList.GetItems(caml).GetDataTable();

                doc.MailMerge.Execute(deNghiItem);

                string fileName = string.Format(DateTime.Now.ToString("yyyyMMdd_hhmmss"));

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                doc.Save(ms, Aspose.Words.SaveFormat.Docx);
                this.Page.Response.Clear();
                this.Page.Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                this.Page.Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.docx", fileName));
                this.Page.Response.BinaryWrite(ms.ToArray());
                this.Page.Response.Flush();
                this.Page.Response.Close();
                this.Page.Response.End();
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("End InPhieuBienNhan");
        }
    }
}
