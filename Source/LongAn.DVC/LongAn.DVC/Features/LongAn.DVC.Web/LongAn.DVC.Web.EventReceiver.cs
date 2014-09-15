using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using LongAn.DVC.Common;

namespace LongAn.DVC.Features.Web
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("907fa3c6-43f4-473b-a14c-5c33abbdecd6")]
    public class LongAnDVCEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            var web = (SPWeb)properties.Feature.Parent;
            EnsureListPermission(web);
        }

        void EnsureListPermission(SPWeb web)
        {
            try
            {
                //Set list permission
                SPList deNghi = web.GetList(Constants.ListUrlDeNghiCapPhep);
                if (deNghi != null)
                {
                    if (!deNghi.HasUniqueRoleAssignments)
                    {
                        deNghi.BreakRoleInheritance(false);
                        SPRoleDefinition roleDefinition = web.RoleDefinitions[Constants.ConfPermissionDeNghi];
                        SPRoleAssignment roleAssignment = new SPRoleAssignment(web.SiteGroups[Constants.ConfGroupNguoiDung]);
                        roleAssignment.RoleDefinitionBindings.Add(roleDefinition);
                        deNghi.RoleAssignments.Add(roleAssignment);
                        roleAssignment = new SPRoleAssignment(web.SiteGroups[Constants.ConfGroupNhanVienTiepNhan]);
                        roleAssignment.RoleDefinitionBindings.Add(roleDefinition);
                        deNghi.RoleAssignments.Add(roleAssignment);
                        roleAssignment = new SPRoleAssignment(web.SiteGroups[Constants.ConfGroupTruongPhoPhong]);
                        roleAssignment.RoleDefinitionBindings.Add(roleDefinition);
                        deNghi.RoleAssignments.Add(roleAssignment);
                        roleAssignment = new SPRoleAssignment(web.SiteGroups[Constants.ConfGroupCanBoXuLy]);
                        roleAssignment.RoleDefinitionBindings.Add(roleDefinition);
                        deNghi.RoleAssignments.Add(roleAssignment);
                        roleAssignment = new SPRoleAssignment(web.SiteGroups[Constants.ConfGroupLanhDaoSo]);
                        roleAssignment.RoleDefinitionBindings.Add(roleDefinition);
                        deNghi.RoleAssignments.Add(roleAssignment);
                    }
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
