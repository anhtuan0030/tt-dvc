using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using LongAn.DVC.Common.Extensions;
using LongAn.DVC.Common;

namespace LongAn.DVC.Features.Site
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("dc2247c1-4143-4c9f-a9d5-a511ac2a790d")]
    public class LongAnDVCEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            var site = (SPSite)properties.Feature.Parent;

            CreateGroup(site.RootWeb);
            EnsurePermissionLevel(site.RootWeb, Constants.ConfPermissionDeNghi, Constants.ConfPermissionDeNghiDes);
        }

        private void CreateGroup(SPWeb web)
        {
            web.CreateNewGroup(Constants.ConfGroupNguoiDung, Constants.ConfGroupNguoiDung, SPRoleType.Reader);
            web.CreateNewGroup(Constants.ConfGroupNhanVienTiepNhan, Constants.ConfGroupNhanVienTiepNhan, SPRoleType.Reader);
            web.CreateNewGroup(Constants.ConfGroupTruongPhoPhong, Constants.ConfGroupTruongPhoPhong, SPRoleType.Reader);
            web.CreateNewGroup(Constants.ConfGroupCanBoXuLy, Constants.ConfGroupCanBoXuLy, SPRoleType.Reader);
            web.CreateNewGroup(Constants.ConfGroupLanhDaoSo, Constants.ConfGroupLanhDaoSo, SPRoleType.Reader);
            try
            {
                SPGroup authenticatedGroup = web.SiteGroups[Constants.ConfGroupNguoiDung];
                authenticatedGroup.Users.Add("NT Authority\\Authenticated Users", string.Empty, "Authenticated Users", string.Empty);
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
        }

        private void EnsurePermissionLevel(SPWeb web, string name, string description)
        {
            try
            {
                SPRoleDefinition roleDef = new SPRoleDefinition();

                bool isNotExist = true;
                foreach (SPRoleDefinition role in web.RoleDefinitions)
                {
                    if (role.Name == name)
                    {
                        isNotExist = false;
                        break;
                    }
                }
                if (isNotExist)
                {
                    web.AllowUnsafeUpdates = true;
                    roleDef.BasePermissions =
                    SPBasePermissions.AddListItems |
                    SPBasePermissions.EditListItems |
                    SPBasePermissions.ViewVersions |
                    SPBasePermissions.CreateAlerts |
                    SPBasePermissions.ViewPages |
                    SPBasePermissions.BrowseUserInfo |
                    SPBasePermissions.UseRemoteAPIs |
                    SPBasePermissions.UseClientIntegration |
                    SPBasePermissions.Open |
                    SPBasePermissions.ViewFormPages |
                    SPBasePermissions.OpenItems;
                    roleDef.Name = name;
                    roleDef.Description = description;
                    web.RoleDefinitions.Add(roleDef);
                    web.Update();
                    web.AllowUnsafeUpdates = false;
                }
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
        }
       
        // Uncomment the method below to handle the event raised before a feature is deactivated.

        //public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
