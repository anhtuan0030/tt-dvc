using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using LongAn.DVC.Common;
using Microsoft.SharePoint.Administration;
using LongAn.DVC.TimerJobs;

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
            CreateJob(web, Constants.SyncReportJobName);
        }

        void EnsureListPermission(SPWeb web)
        {
            try
            {
                LoggingServices.LogMessage("Begin EnsureListPermission");
                //Set list permission
                var deNghiUrl = (SPContext.Current.Web.ServerRelativeUrl + Constants.ListUrlDeNghiCapPhep).Replace("//", "/");
                SPList deNghi = web.GetList(deNghiUrl);
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
            LoggingServices.LogMessage("End EnsureListPermission");
        }

        private static void DeleteJob(SPWeb web, string jobName)
        {
            try
            {
                LoggingServices.LogMessage("Begin Delete Job: " + jobName);
                foreach (SPJobDefinition job in web.Site.WebApplication.JobDefinitions)
                {
                    if (job.Name == jobName)
                    {
                        job.Delete();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("End Delete Job: " + jobName);
        }

        private static void CreateJob(SPWeb web, string jobName)
        {
            try
            {
                LoggingServices.LogMessage("Begin Create Timesheet HO job");
                //Delete exists job
                DeleteJob(web, jobName);
                //Create new job
                SyncReportJob job = new SyncReportJob(jobName, web.Site.WebApplication);
                job.Properties[Constants.SiteIdProperty] = web.Site.ID.ToString();
                job.Properties[Constants.WebIdProperty] = web.ID.ToString();
                //Schedule
                SPDailySchedule schedule = new SPDailySchedule();
                schedule.BeginHour = 21;
                schedule.BeginMinute = 5;
                schedule.EndHour = 23;
                schedule.EndMinute = 30;
                job.Schedule = schedule;
                job.Update();
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
