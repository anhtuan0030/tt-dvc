using CamlexNET;
using LongAn.DVC.Common;
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
                            //Stream stream = fileUpload.PostedFile.InputStream;
                            var attachmentFile = web.GetFolder(Constants.ListUrlDeNghiAttachment);
                            var files = attachmentFile.Files;
                            var documentMetadata = new System.Collections.Hashtable {
                               { Constants.FieldTitle, string.Format("{0}_{1}", deNghiId, loaiAttachment) },
                               { Constants.FieldDeNghi, deNghiId },
                                { Constants.FieldLoaiAttachment, loaiAttachment }
                           };
                            var currentFile = files.Add(string.Format("{0}/{1}_{2}", Constants.ListUrlDeNghiAttachment, DateTime.Now.ToString("yyyyMMddhhmmss"), fileUpload.FileName), fileUpload.FileContent, documentMetadata, true, false);
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
                var deNghiAttachmentList = SPContext.Current.Web.GetList(Constants.ListUrlDeNghiAttachment);
                var deNghiAttachmentItems = deNghiAttachmentList.GetItems(caml).GetDataTable();
                for (int i = 0; i < deNghiAttachmentItems.Rows.Count; i++)
                {
                    HyperLink link = new HyperLink();
                    link.Text = deNghiAttachmentItems.Rows[i]["LinkFileName"].ToString();
                    link.NavigateUrl = string.Format("{0}/{1}", Constants.ListUrlDeNghiAttachment, deNghiAttachmentItems.Rows[i]["LinkFileName"]);
                    link.Target = "_blank";
                    div.Controls.Add(link);
                }
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("End LoadAttachments: ItemId: " + itemId + ", type: " + type);
        }
    }
}
