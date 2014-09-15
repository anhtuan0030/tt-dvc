using CamlexNET;
using LongAn.DVC.Common;
using LongAn.DVC.Common.Extensions;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongAn.DVC.EventReceivers
{
    public class DeNghiCapPhepXeEvent : SPItemEventReceiver
    {
        public override void ItemAdding(SPItemEventProperties properties)
        {
            base.ItemAdding(properties);
        }

        public override void ItemAdded(SPItemEventProperties properties)
        {
            base.ItemAdded(properties);

            #region Generate BienNhan
            using (DisableItemEvent disableItem = new DisableItemEvent())
            {
                var currentItem = properties.ListItem;
                var namDeNghi = DateTime.Now.ToString("yyyy");
                var soThuTuBienNhan = GenerateBienNhan(properties.Web);
                currentItem[Constants.FieldTitle] = Constants.ConfMaLinhVucSGTVT + namDeNghi + GenerateBienNhan(properties.Web);
                currentItem[Constants.FieldNamDeNghi] = namDeNghi;
                currentItem[Constants.FieldSoThuTuBienNhan] = int.Parse(soThuTuBienNhan);
                currentItem.SystemUpdate();
            }
            #endregion Generate BienNhan

            #region Update permission
            SPUser currentUser = properties.Web.CurrentUser;
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite(properties.Web.Site.ID))
                {
                    using (SPWeb web = site.OpenWeb(properties.Web.ID))
                    {
                        SPListItem currentItem = web.Lists[properties.ListId].GetItemById(properties.ListItemId);
                        SPRoleDefinitionCollection roleDefinitions = web.RoleDefinitions;
                        SPRoleDefinition editNotDeleteRoleDefinition = roleDefinitions[Constants.ConfPermissionDeNghi];
                        currentItem.BreakRoleInheritance(true);
                        currentItem.SetPermissions(currentUser, editNotDeleteRoleDefinition);
                        SPGroup group = web.SiteGroups[Constants.ConfGroupNguoiDung];
                        currentItem.RemovePermissions(group);
                    }
                }
            });
            #endregion Update permission
        }

        public override void ItemUpdating(SPItemEventProperties properties)
        {
            base.ItemUpdating(properties);
        }

        public override void ItemUpdated(SPItemEventProperties properties)
        {
            base.ItemUpdated(properties);
        }

        public override void ItemDeleting(SPItemEventProperties properties)
        {
            base.ItemDeleting(properties);
        }

        #region Private functions
        string GenerateBienNhan(SPWeb spWeb)
        {
            string bienNhan = string.Empty;
            try
            {
                LoggingServices.LogMessage("Begin ItemAdded SetSoBienNhan");
                SPQuery caml = Camlex.Query().Where(x => (string)x[Constants.FieldNamDeNghi] == DateTime.Now.ToString("yyyy"))
                                             .OrderBy(x => new[] { x["ID"] as Camlex.Desc }).ToSPQuery();
                caml.RowLimit = 1;
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(spWeb.Site.ID))
                    {
                        using (SPWeb web = site.OpenWeb(spWeb.ID))
                        {
                            var deNghiList = web.GetList(Constants.ListUrlDeNghiCapPhep);
                            var deNghiListItems = deNghiList.GetItems(caml);
                            string soThuTuBienNhan = Constants.ConfSoThuTuBienNhan + "1";
                            if (deNghiListItems != null && deNghiListItems.Count > 0)
                                soThuTuBienNhan = Constants.ConfSoThuTuBienNhan + 
                                    (int.Parse(deNghiListItems[0][Constants.FieldSoThuTuBienNhan].ToString()) + 1);
                            bienNhan = soThuTuBienNhan.Substring(soThuTuBienNhan.Length - 7);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("End ItemAdded SetSoBienNhan");
            return bienNhan;
        }
        #endregion Private functions
    }
}
