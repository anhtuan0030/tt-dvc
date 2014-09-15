using System;
using System.Linq;
using System.Web;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;

namespace LongAn.DVC.Common.Extensions
{
    public static class SPSiteExtensions
    {
        public static string GetFeaturePropertyValue(this SPSite site, string featureId, string key)
        {
            var theFeature = site.Features[new Guid(featureId)];
            if (theFeature != null && theFeature.Properties[key] != null)
                return theFeature.Properties[key].Value;
            return string.Empty;
        }

        public static SPPrincipal FindUserOrSiteGroup(this SPSite site, string userOrGroup)
        {
            SPPrincipal myUser = null;

            if (SPUtility.IsLoginValid(site, userOrGroup) || string.Compare(userOrGroup,@"sharepoint\system",true) == 0)
            {
                myUser = site.RootWeb.EnsureUser(userOrGroup);
            }
            else
            {   //might be a group
                foreach (SPGroup g in site.RootWeb.SiteGroups)
                {
                    if (string.Compare(g.Name,userOrGroup,true) == 0)
                        myUser = g;
                }
            }
            return myUser;
        }

        public static SPUser GetUser(this SPSite site, string loginName)
        {
            SPUser myUser = null;

            try
            {
                if (SPUtility.IsLoginValid(site, loginName) || string.Compare(loginName, @"sharepoint\system", true) == 0)
                {
                    myUser = site.RootWeb.EnsureUser(loginName);
                }
            }
            catch { }
            return myUser;
        }

        public static SPUser GetUserByEmail(this SPSite site, string email)
        {
            SPUser myUser = null;
            try
            {
                string loginName = SPUtility.GetLoginNameFromEmail(site, email);
                if (!string.IsNullOrEmpty(loginName))
                {
                    myUser = site.GetUser(loginName);
                }
                else
                {
                    myUser = site.RootWeb.AllUsers.Cast<SPUser>().FirstOrDefault(f => string.Compare(f.Email, email, true) == 0);
                }
            }
            catch { }

            return myUser;
        }

        public static string CreateSite(this SPSite site, string tempalteName, string siteName, string title, string description)
        {
            string siteDepartmentUrl = string.Empty;
            try
            {
                site.AllowUnsafeUpdates = true;

                SPWebTemplateCollection templates = site.GetWebTemplates(1033);
                var deptsite = templates.Cast<SPWebTemplate>().Where(p => p.Name.Contains(tempalteName)).FirstOrDefault();

                SPWeb web = site.RootWeb.Webs.Add(siteName, title, description, 1033, deptsite.Name, true, false);

                web.Update();
                siteDepartmentUrl = web.Url;

                web.Dispose();
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            return siteDepartmentUrl;
        }
    }
}
