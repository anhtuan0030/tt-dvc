using CamlexNET;
using LongAn.DVC.Common;
using LongAn.DVC.Common.Extensions;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LongAn.DVC.Helpers
{
    public class DeNghiHelper
    {
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
        public static void AddDeNghiHistory(SPWeb spWeb, CapXuLy capXuLy, int deNghiId, string hanhDong, string note)
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
    }
}
