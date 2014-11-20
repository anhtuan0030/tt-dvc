using CamlexNET;
using LongAn.DVC.Common;
using LongAn.DVC.Common.Extensions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LongAn.DVC.Helpers
{
    public class DeNghiHelper
    {
        private static DeNghiArgument GetDeNghiArgument(SPWeb web, int itemId)
        {
            DeNghiArgument output = null;
            try
            {
                var deNghiUrl = (web.ServerRelativeUrl + Constants.ListUrlDeNghiCapPhep).Replace("//", "/");
                var deNghiList = web.GetList(deNghiUrl);
                var deNghiItem = deNghiList.GetItemById(itemId);
                output = new DeNghiArgument();
                output.DeNghiID = itemId.ToString();
                output.MaSoBienNhan = deNghiItem[Constants.FieldTitle].ToString();
                output.CaNhanToChuc = deNghiItem[Constants.FieldCaNhanToChuc].ToString();
                var spUserValue = new SPFieldUserValue(web, deNghiItem[Constants.FieldNguoiDeNghi].ToString());
                output.EmailCaNhanToChuc = spUserValue.User.Email;
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            return output;
        }

        public static void InPhieuBienNhan(PrintType type, string itemId, Page page)
        {
            try
            {
                var web = SPContext.Current.Web;
                var deNghiList = web.GetList((web.ServerRelativeUrl + Constants.ListUrlDeNghiCapPhep).Replace("//", "/"));
                string wordLicFile = Microsoft.SharePoint.Utilities.SPUtility.GetVersionedGenericSetupPath(Constants.ConfWordLicFile, 15);
                Aspose.Words.License wordLic = new Aspose.Words.License();
                wordLic.SetLicense(wordLicFile);

                string templateFile = string.Empty;
                SPQuery caml = Camlex.Query().Where(x => x["ID"] == (DataTypes.Counter)itemId)
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
                page.Response.Clear();
                page.Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                page.Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.docx", fileName));
                page.Response.BinaryWrite(ms.ToArray());
                page.Response.Flush();
                page.Response.Close();
                page.Response.End();
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
        }
        public static void SendEmail(SPWeb web, int itemId, string hanhDong)
        {
            try
            {
                LoggingServices.LogMessage("Begin Send Email To Nguoi dung");
                StringDictionary headers = new StringDictionary();
                headers.Add("subject", "[Sở GTVT Long An] - Thông báo");
                headers.Add("content-type","text/html");
                var deNghiArgument = GetDeNghiArgument(web, itemId);
                headers.Add("to", deNghiArgument.EmailCaNhanToChuc);
                //headers.Add("cc",strCC);
                //headers.Add("bcc",strbcc);
                //headers.Add("from",strFrom);

                var deNghiUrl = (SPContext.Current.Web.ServerRelativeUrl +  Constants.ListUrlDeNghiCapPhep).Replace("//","/");
                var currentPage = SPUtility.GetPageUrlPath(HttpContext.Current);
                var linkDisplayUrl = string.Format(Constants.ConfLinkPageDispForm, deNghiUrl, itemId, currentPage);
                var fullDisplayUrl = SPContext.Current.Site.MakeFullUrl(linkDisplayUrl);
                var emailBody = string.Format(Constants.EmailBody, deNghiArgument.CaNhanToChuc, deNghiArgument.MaSoBienNhan, hanhDong, fullDisplayUrl);

                LoggingServices.LogMessage("Send Email To: " + deNghiArgument.EmailCaNhanToChuc + ", Content: " + emailBody);
                SPUtility.SendEmail(web, headers, emailBody);
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("End Send Email To Nguoi dung");
        }
        public static void SaveFileAttachment(FileUpload fileUpload, int deNghiId, string loaiAttachment)
        {
            if (!fileUpload.HasFile)
                return;
            try
            {
                LoggingServices.LogMessage("Begin SaveFileAttachment, file: " + fileUpload.FileName + ", loai attachment: " + loaiAttachment);
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                    {
                        using (SPWeb web = site.OpenWeb(SPContext.Current.Web.ID))
                        {
                            web.AllowUnsafeUpdates = true;
                            //Stream stream = fileUpload.PostedFile.InputStream;
                            var deNghiAttachmentUrl = (web.ServerRelativeUrl + Constants.ListUrlDeNghiAttachment).Replace("//", "/");
                            var attachmentFile = web.GetFolder(deNghiAttachmentUrl);
                            var files = attachmentFile.Files;
                            var documentMetadata = new System.Collections.Hashtable {
                               { Constants.FieldTitle, string.Format("{0}_{1}", deNghiId, loaiAttachment) },
                               { Constants.FieldDeNghi, deNghiId },
                               { Constants.FieldLoaiAttachment, loaiAttachment }
                           };
                            var currentFile = files.Add(string.Format("{0}/{1}_{2}", deNghiAttachmentUrl, DateTime.Now.ToString("yyyyMMddhhmmssfff"), fileUpload.FileName), 
                                fileUpload.FileContent, 
                                documentMetadata,
                                true, 
                                false);
                            web.AllowUnsafeUpdates = false;
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("End SaveFileAttachment, file: " + fileUpload.FileName + ", loai attachment: " + loaiAttachment);
        }
        public static void ClosePopup(Page page)
        {
            page.Response.Clear();
            page.Response.Write(
            string.Format(System.Globalization.CultureInfo.InvariantCulture, @"<script type='text/javascript'> window.frameElement.commonModalDialogClose(1, '{0}');</script>", ""));
            page.Response.End();

        }
        public static void LoadAttachments(int itemId, string type, Control div)
        {
            try
            {
                LoggingServices.LogMessage("Begin LoadAttachments: ItemId: " + itemId + ", type: " + type);
                SPQuery caml = Camlex.Query().Where(x => x[Constants.FieldDeNghi] == (DataTypes.LookupId)itemId.ToString()
                                                    && (string)x[Constants.FieldLoaiAttachment] == type)
                                    .OrderBy(x => new[] { x["ID"] as Camlex.Asc })
                                    .ToSPQuery();
                var deNghiUrl = (SPContext.Current.Web.ServerRelativeUrl + Constants.ListUrlDeNghiAttachment).Replace("//", "/");
                var deNghiAttachmentList = SPContext.Current.Web.GetList(deNghiUrl);
                var deNghiAttachmentItems = deNghiAttachmentList.GetItems(caml).GetDataTable();
                var rowCount = deNghiAttachmentItems != null ? deNghiAttachmentItems.Rows.Count : 0;
                for (int i = 0; i < rowCount; i++)
                {
                    HyperLink link = new HyperLink();
                    link.Text = "File đính kèm " + (i+1);
                    link.NavigateUrl = string.Format("{0}/{1}", deNghiUrl, deNghiAttachmentItems.Rows[i]["LinkFileName"]);
                    link.Target = "_blank";
                    div.Controls.Add(link);
                    if (i < rowCount - 1)
                        div.Controls.Add(new LiteralControl("<br />"));
                }
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("End LoadAttachments: ItemId: " + itemId + ", type: " + type);
        }

        public static CauHinh GetCauHinh(int buocDuyetID)
        {
            CauHinh cauHinh = null;
            try
            {
                LoggingServices.LogMessage("Begin funtion GetCauHinh, BuocDuyet: " + buocDuyetID);
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                    {
                        using (SPWeb web = site.OpenWeb(SPContext.Current.Web.ID))
                        {
                            var cauHinhList = web.GetList((web.ServerRelativeUrl + Constants.ListUrlCauHinh).Replace("//", "/"));
                            var cauHinhItem = cauHinhList.GetItemById(buocDuyetID);
                            if (cauHinhItem != null)
                            {
                                cauHinh = new CauHinh();
                                cauHinh.BuocDuyetID = cauHinhItem.ID;
                                cauHinh.BuocDuyet = cauHinhItem[Fields.Title].ToString();
                                var spGroupText = cauHinhItem[Fields.SPGroup];
                                if (spGroupText != null && !string.IsNullOrEmpty(spGroupText.ToString()))
                                {
                                    var spGroupLookup = new SPFieldLookupValue(spGroupText.ToString());
                                    cauHinh.SPGroup = web.SiteGroups.GetByID(spGroupLookup.LookupId);
                                }

                                cauHinh.CapDuyetText = cauHinhItem[Fields.CapDuyetText].ToString();

                                var trangThaiText = cauHinhItem[Fields.TrangThai];
                                if (trangThaiText != null && !string.IsNullOrEmpty(trangThaiText.ToString()))
                                {
                                    var trangThaiLookup = new SPFieldLookupValue(trangThaiText.ToString());
                                    cauHinh.TrangThai = trangThaiLookup.LookupId;
                                }
                                cauHinh.IsFix = bool.Parse(cauHinhItem[Fields.IsFix].ToString());

                                cauHinh.ActionDuyet = bool.Parse(cauHinhItem[Fields.ActionDuyet].ToString());
                                cauHinh.TieuDeActionDuyet = cauHinhItem[Fields.TieuDeActionDuyet] != null ? cauHinhItem[Fields.TieuDeActionDuyet].ToString() : string.Empty;
                                var nextStepText = cauHinhItem[Fields.NextStep];
                                if (nextStepText != null && !string.IsNullOrEmpty(nextStepText.ToString()))
                                {
                                    var nextStepLookup = new SPFieldLookupValue(nextStepText.ToString());
                                    cauHinh.NextStep = nextStepLookup.LookupId;
                                }

                                cauHinh.ActionTraHoSo = bool.Parse(cauHinhItem[Fields.ActionTraHoSo].ToString());
                                cauHinh.TieuDeActionTraHoSo = cauHinhItem[Fields.TieuDeActionTraHoSo] != null ? cauHinhItem[Fields.TieuDeActionTraHoSo].ToString() : string.Empty;
                                var previousStepText = cauHinhItem[Fields.PreviousStep];
                                if (previousStepText != null && !string.IsNullOrEmpty(previousStepText.ToString()))
                                {
                                    var previousStepLookup = new SPFieldLookupValue(previousStepText.ToString());
                                    cauHinh.PreviousStep = previousStepLookup.LookupId;
                                }

                                cauHinh.ActionTuChoi = bool.Parse(cauHinhItem[Fields.ActionTuChoi].ToString());
                                cauHinh.ActionYeuCauBoSung = bool.Parse(cauHinhItem[Fields.ActionYeuCauBoSung].ToString());
                                cauHinh.ActionPhanCong = bool.Parse(cauHinhItem[Fields.ActionPhanCong].ToString());
                                //cauHinh.ActionCanBoTiepNhan = bool.Parse(cauHinhItem[Fields.ActionCanBoTiepNhan].ToString());
                                var spGroupPhanCongText = cauHinhItem[Fields.SPGroupPhanCong];
                                if (spGroupPhanCongText != null && !string.IsNullOrEmpty(spGroupPhanCongText.ToString()))
                                {
                                    var spGroupLookup = new SPFieldLookupValue(spGroupPhanCongText.ToString());
                                    cauHinh.SPGroupPhanCong = web.SiteGroups.GetByID(spGroupLookup.LookupId);
                                }
                                var spGroupCanBoText = cauHinhItem[Fields.SPGroupTiepNhan];
                                if (spGroupCanBoText != null && !string.IsNullOrEmpty(spGroupCanBoText.ToString()))
                                {
                                    var spGroupLookup = new SPFieldLookupValue(spGroupCanBoText.ToString());
                                    cauHinh.SPGroupTiepNhan = web.SiteGroups.GetByID(spGroupLookup.LookupId);
                                }
                                cauHinh.AllowCapNhatLoaiDuong = bool.Parse(cauHinhItem[Fields.AllowCapNhatLoaiDuong].ToString());
                                cauHinh.AllowCapNhatNgayHen = bool.Parse(cauHinhItem[Fields.AllowCapNhatNgayHen].ToString());
                                cauHinh.IsBoSungHoSo = bool.Parse(cauHinhItem[Fields.IsBoSungHoSo].ToString());
                                cauHinh.IsPhanCong = bool.Parse(cauHinhItem[Fields.IsPhanCong].ToString());
                                //cauHinh.IsXuLyPhanCong = bool.Parse(cauHinhItem[Fields.IsXuLyPhanCong].ToString());
                                cauHinh.StartEnd = cauHinhItem[Fields.StartEnd] != null ? cauHinhItem[Fields.StartEnd].ToString() : string.Empty;
                                cauHinh.IsEmail = bool.Parse(cauHinhItem[Fields.IsEmail].ToString());
                                cauHinh.EmailTemplate = cauHinhItem[Fields.EmailTemplate] != null ? cauHinhItem[Fields.EmailTemplate].ToString() : string.Empty;
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("End funtion GetCauHinh, Cấu hình Title: " + buocDuyetID);
            return cauHinh;
        }

        public static List<CauHinh> GetCauHinh(string startEnd)
        {
            List<CauHinh> cauHinhs = null;
            try
            {
                LoggingServices.LogMessage("Begin funtion GetCauHinh, StartEnd: " + startEnd);
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                    {
                        using (SPWeb web = site.OpenWeb(SPContext.Current.Web.ID))
                        {
                            var cauHinhList = web.GetList((web.ServerRelativeUrl + Constants.ListUrlCauHinh).Replace("//", "/"));
                            SPQuery caml = Camlex.Query().Where(x => (string)x[Fields.StartEnd] == startEnd)
                                    .OrderBy(x => new[] { x["ID"] as Camlex.Asc })
                                    .ToSPQuery();
                            var cauHinhItems = cauHinhList.GetItems(caml);
                            if (cauHinhItems != null && cauHinhItems.Count > 0)
                            {
                                cauHinhs = new List<CauHinh>();
                                foreach (SPListItem cauHinhItem in cauHinhItems)
                                {
                                    CauHinh cauHinh = new CauHinh();
                                    cauHinh.BuocDuyetID = cauHinhItem.ID;
                                    cauHinh.BuocDuyet = cauHinhItem[Fields.Title].ToString();
                                    var spGroupText = cauHinhItem[Fields.SPGroup];
                                    if (spGroupText != null && !string.IsNullOrEmpty(spGroupText.ToString()))
                                    {
                                        var spGroupLookup = new SPFieldLookupValue(spGroupText.ToString());
                                        cauHinh.SPGroup = web.SiteGroups.GetByID(spGroupLookup.LookupId);
                                    }

                                    cauHinh.CapDuyetText = cauHinhItem[Fields.CapDuyetText].ToString();

                                    var trangThaiText = cauHinhItem[Fields.TrangThai];
                                    if (trangThaiText != null && !string.IsNullOrEmpty(trangThaiText.ToString()))
                                    {
                                        var trangThaiLookup = new SPFieldLookupValue(trangThaiText.ToString());
                                        cauHinh.TrangThai = trangThaiLookup.LookupId;
                                    }
                                    cauHinh.IsFix = bool.Parse(cauHinhItem[Fields.IsFix].ToString());

                                    cauHinh.ActionDuyet = bool.Parse(cauHinhItem[Fields.ActionDuyet].ToString());
                                    cauHinh.TieuDeActionDuyet = cauHinhItem[Fields.TieuDeActionDuyet] != null ? cauHinhItem[Fields.TieuDeActionDuyet].ToString() : string.Empty;
                                    var nextStepText = cauHinhItem[Fields.NextStep];
                                    if (nextStepText != null && !string.IsNullOrEmpty(nextStepText.ToString()))
                                    {
                                        var nextStepLookup = new SPFieldLookupValue(nextStepText.ToString());
                                        cauHinh.NextStep = nextStepLookup.LookupId;
                                    }

                                    cauHinh.ActionTraHoSo = bool.Parse(cauHinhItem[Fields.ActionTraHoSo].ToString());
                                    cauHinh.TieuDeActionTraHoSo = cauHinhItem[Fields.TieuDeActionTraHoSo] != null ? cauHinhItem[Fields.TieuDeActionTraHoSo].ToString() : string.Empty;
                                    var previousStepText = cauHinhItem[Fields.PreviousStep];
                                    if (previousStepText != null && !string.IsNullOrEmpty(previousStepText.ToString()))
                                    {
                                        var previousStepLookup = new SPFieldLookupValue(previousStepText.ToString());
                                        cauHinh.PreviousStep = previousStepLookup.LookupId;
                                    }

                                    cauHinh.ActionTuChoi = bool.Parse(cauHinhItem[Fields.ActionTuChoi].ToString());
                                    cauHinh.ActionYeuCauBoSung = bool.Parse(cauHinhItem[Fields.ActionYeuCauBoSung].ToString());
                                    cauHinh.ActionPhanCong = bool.Parse(cauHinhItem[Fields.ActionPhanCong].ToString());
                                    var spGroupPhanCongText = cauHinhItem[Fields.SPGroupPhanCong];
                                    if (spGroupPhanCongText != null && !string.IsNullOrEmpty(spGroupPhanCongText.ToString()))
                                    {
                                        var spGroupLookup = new SPFieldLookupValue(spGroupPhanCongText.ToString());
                                        cauHinh.SPGroupPhanCong = web.SiteGroups.GetByID(spGroupLookup.LookupId);
                                    }
                                    var spGroupCanBoText = cauHinhItem[Fields.SPGroupTiepNhan];
                                    if (spGroupCanBoText != null && !string.IsNullOrEmpty(spGroupCanBoText.ToString()))
                                    {
                                        var spGroupLookup = new SPFieldLookupValue(spGroupCanBoText.ToString());
                                        cauHinh.SPGroupTiepNhan = web.SiteGroups.GetByID(spGroupLookup.LookupId);
                                    }
                                    cauHinh.AllowCapNhatLoaiDuong = bool.Parse(cauHinhItem[Fields.AllowCapNhatLoaiDuong].ToString());
                                    cauHinh.AllowCapNhatNgayHen = bool.Parse(cauHinhItem[Fields.AllowCapNhatNgayHen].ToString());
                                    cauHinh.IsBoSungHoSo = bool.Parse(cauHinhItem[Fields.IsBoSungHoSo].ToString());
                                    cauHinh.IsPhanCong = bool.Parse(cauHinhItem[Fields.IsPhanCong].ToString());
                                    //cauHinh.IsXuLyPhanCong = bool.Parse(cauHinhItem[Fields.IsXuLyPhanCong].ToString());
                                    cauHinh.StartEnd = cauHinhItem[Fields.StartEnd] != null ? cauHinhItem[Fields.StartEnd].ToString() : string.Empty;
                                    cauHinh.IsEmail = bool.Parse(cauHinhItem[Fields.IsEmail].ToString());
                                    cauHinh.EmailTemplate = cauHinhItem[Fields.EmailTemplate] != null ? cauHinhItem[Fields.EmailTemplate].ToString() : string.Empty;
                                    cauHinhs.Add(cauHinh);
                                }
                                LoggingServices.LogMessage("Cấu hình count: " + cauHinhs.Count);
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("End funtion GetCauHinh");
            return cauHinhs;
        }

        public static bool IsCurrentUserInGroup(SPWeb spWeb, SPGroup spGroup)
        {
            bool isMember = false;
            var currentLogin = SPContext.Current.Web.CurrentUser.LoginName;
            try
            {
                LoggingServices.LogMessage("Begin function IsCurrentUserInGroup, SPWeb " + spWeb.Title + ", SPGroup: " + spGroup.Name);
                SPSecurity.RunWithElevatedPrivileges(delegate
                {
                    //using (SPSite site = new SPSite(spWeb.Site.ID))
                    //{
                    //    using (SPWeb web = site.OpenWeb(spWeb.ID))
                    //    {
                            //isMember = web.IsCurrentUserMemberOfGroup(spGroup.ID);
                            isMember = spGroup.Users.GetCollection(new string[] { currentLogin }).Count > 0;
                    //    }
                    //}
                });
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("End function IsCurrentUserInGroup, Result: " + isMember);
            return isMember;
        }

        public static void AddDeNghiHistory(SPWeb spWeb, CapXuLy capXuLy, int deNghiId, string hanhDong, string note = "")
        {
            try
            {
                LoggingServices.LogMessage("Begin AddDeNghiHistory");
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(spWeb.Site.ID))
                    {
                        using (SPWeb web = site.OpenWeb(spWeb.ID))
                        {
                            web.AllowUnsafeUpdates = true;
                            var deNghiHistoryUrl = (web.ServerRelativeUrl + Constants.ListUrlLichSuCapPhep).Replace("//", "/");
                            var deNghiHistoryList = web.GetList(deNghiHistoryUrl);
                            var deNghiHistoryItem = deNghiHistoryList.Items.Add();
                            deNghiHistoryItem[Constants.FieldTitle] = capXuLy;
                            deNghiHistoryItem[Constants.FieldDeNghi] = deNghiId;
                            deNghiHistoryItem[Constants.FieldNguoiXuLy] = spWeb.CurrentUser.ID;
                            deNghiHistoryItem[Constants.FieldNgayXuLy] = DateTime.Now;
                            deNghiHistoryItem[Constants.FieldHanhDong] = hanhDong;
                            deNghiHistoryItem[Constants.FieldMoTa] = note;
                            deNghiHistoryItem.Update();
                            web.AllowUnsafeUpdates = false;
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("End AddDeNghiHistory");
        }


        

        #region Removes
        public static CapXuLy CurrentUserRole(SPWeb spWeb, SPUser user)
        {
            CapXuLy result = CapXuLy.CaNhanToChuc;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(spWeb.Site.ID))
                    {
                        using (SPWeb web = site.OpenWeb(spWeb.ID))
                        {
                            var groupNhanVienTiepNhan = web.SiteGroups[Constants.ConfGroupNhanVienTiepNhan];
                            var groupCanBoXuLy = web.SiteGroups[Constants.ConfGroupCanBoXuLy];
                            var groupTruongPhoPhong = web.SiteGroups[Constants.ConfGroupTruongPhoPhong];
                            var groupLanhDaoSo = web.SiteGroups[Constants.ConfGroupLanhDaoSo];
                            if (user.InGroup(groupNhanVienTiepNhan))
                                result = CapXuLy.MotCua;
                            else if (user.InGroup(groupCanBoXuLy))
                                result = CapXuLy.CanBo;
                            else if (user.InGroup(groupTruongPhoPhong))
                                result = CapXuLy.TruongPhoPhong;
                            else if (user.InGroup(groupLanhDaoSo))
                                result = CapXuLy.LanhDaoSo;
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            return result;
        }
        public static void AddDeNghiHistory(SPWeb spWeb, int deNghi, string title, int buocDuyet, int trangThai, string note = "")
        {
            try
            {
                LoggingServices.LogMessage("Begin AddDeNghiHistory");
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(spWeb.Site.ID))
                    {
                        using (SPWeb web = site.OpenWeb(spWeb.ID))
                        {
                            web.AllowUnsafeUpdates = true;
                            var deNghiHistoryUrl = (web.ServerRelativeUrl + Constants.ListUrlLichSuCapPhep).Replace("//", "/");
                            var deNghiHistoryList = web.GetList(deNghiHistoryUrl);
                            var deNghiHistoryItem = deNghiHistoryList.Items.Add();
                            deNghiHistoryItem[Fields.Title] = title;
                            deNghiHistoryItem[Fields.DeNghi] = deNghi;
                            deNghiHistoryItem[Fields.BuocDuyet] = buocDuyet;
                            deNghiHistoryItem[Fields.NguoiXuLy] = spWeb.CurrentUser.ID;
                            deNghiHistoryItem[Fields.TrangThai] = trangThai;
                            deNghiHistoryItem[Fields.NgayXuLy] = DateTime.Now;
                            deNghiHistoryItem[Fields.MoTa] = note;
                            deNghiHistoryItem.Update();
                            web.AllowUnsafeUpdates = false;
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("End AddDeNghiHistory");
        }


        #endregion Removes
    }
}
