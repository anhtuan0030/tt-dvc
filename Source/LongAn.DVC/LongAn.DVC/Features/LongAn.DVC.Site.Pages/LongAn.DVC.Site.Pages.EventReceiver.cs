using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Publishing;
using LongAn.DVC.Common;
using LongAn.DVC.Common.Extensions;

namespace LongAn.DVC.Features.Feature1
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("894b4304-b0a2-4279-acae-c7353eb655aa")]
    public class LongAnDVCSitePagesEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            var site = (SPSite)properties.Feature.Parent;
            var web = site.RootWeb;

            //Ensure "DVC Nguoi dung" group for MembershipRegister webpart
            CreateGroup(web);

            if (PublishingWeb.IsPublishingWeb(web))
            {
                PublishingWeb pubWeb = PublishingWeb.GetPublishingWeb(web);
                //Get the file name
                SPFile welcomeFile = web.GetFile("Pages/Homepage.aspx");
                //Assign the new filename to the DefaultPage property
                pubWeb.DefaultPage = welcomeFile;
                //Update the Publishing Web.
                pubWeb.Update();
            }
            

            //provision DVC Master page
            web.MasterUrl = site.RootWeb.ServerRelativeUrl.TrimEnd('/') + "/_catalogs/masterpage/DVC_Admin.master";
            web.CustomMasterUrl = site.RootWeb.ServerRelativeUrl.TrimEnd('/') + "/_catalogs/masterpage/DVC.master";
            web.Update();
        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            var site = (SPSite)properties.Feature.Parent;
            site.RootWeb.MasterUrl = site.RootWeb.ServerRelativeUrl.TrimEnd('/') + "/_catalogs/masterpage/seattle.master";
            site.RootWeb.CustomMasterUrl = site.RootWeb.ServerRelativeUrl.TrimEnd('/') + "/_catalogs/masterpage/seattle.master";
            site.RootWeb.Update();
        }


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

        private void CreateGroup(SPWeb web)
        {
            web.CreateNewGroup(Constants.ConfGroupNguoiDung, Constants.ConfGroupNguoiDung, SPRoleType.Reader);
        }
    }
}
