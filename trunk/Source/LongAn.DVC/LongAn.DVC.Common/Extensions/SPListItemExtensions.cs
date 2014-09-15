using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System.Text;
using System.IO;
using System.Xml;

namespace LongAn.DVC.Common.Extensions
{
    public static class SPListItemExtensions
    {
        #region Adding permission to an item
        public static void SetPermissions(this SPListItem item, IEnumerable<SPPrincipal> principals, SPRoleType roleType)
        {
            if (item != null)
            {

                foreach (SPPrincipal principal in principals)
                {
                    SPRoleDefinition roleDefinition = item.Web.RoleDefinitions.GetByType(roleType);
                    SetPermissions(item, principal, roleDefinition);
                }
            }
        }


        public static void SetPermissions(this SPListItem item, SPUser user, SPRoleType roleType)
        {
            if (item != null)
            {
                SPRoleDefinition roleDefinition = item.Web.RoleDefinitions.GetByType(roleType);
                SetPermissions(item, (SPPrincipal)user, roleDefinition);
            }
        }

        public static void SetPermissions(this SPListItem item, SPPrincipal principal, SPRoleType roleType)
        {
            if (item != null)
            {
                SPRoleDefinition roleDefinition = item.Web.RoleDefinitions.GetByType(roleType);
                SetPermissions(item, principal, roleDefinition);
            }
        }

        public static void SetPermissions(this SPListItem item, SPUser user, SPRoleDefinition roleDefinition)
        {
            if (item != null)
            {
                SetPermissions(item, (SPPrincipal)user, roleDefinition);
            }
        }

        public static void SetPermissions(this SPListItem item, SPPrincipal principal, SPRoleDefinition roleDefinition)
        {
            if (item != null)
            {
                SPRoleAssignment roleAssignment = new SPRoleAssignment(principal);

                roleAssignment.RoleDefinitionBindings.Add(roleDefinition);
                item.RoleAssignments.Add(roleAssignment);
            }
        }

        #endregion Adding permission to an item

        #region Updating or Modifying Permissions on an item
        public static void ChangePermissions(this SPListItem item, SPPrincipal principal, SPRoleType roleType)
        {
            if (item != null)
            {
                SPRoleDefinition roleDefinition = item.Web.RoleDefinitions.GetByType(roleType);
                ChangePermissions(item, principal, roleDefinition);
            }
        }

        public static void ChangePermissions(this SPListItem item, SPPrincipal principal, SPRoleDefinition roleDefinition)
        {
            SPRoleAssignment roleAssignment = item.RoleAssignments.GetAssignmentByPrincipal(principal);
            if (roleAssignment != null)
            {
                roleAssignment.RoleDefinitionBindings.RemoveAll();
                roleAssignment.RoleDefinitionBindings.Add(roleDefinition);
                roleAssignment.Update();
            }
        }
        #endregion Updating or Modifying Permissions on an item

        public static bool VerifyFieldAccess(this SPListItem item, string fieldname)
        {
            try
            {
                bool result = item.Fields.ContainsField(fieldname);
                if (result)
                    result &= item[fieldname] != null;
                return result;
            }
            finally
            {

            }
            return false;
        }
        public static void RemoveReadPermissions(this SPListItem item)
        {
            //remove all current permissions
            if (!item.HasUniqueRoleAssignments)
            {
                return;
            }
            else
            {
                List<SPRoleAssignment> willbeRemovedAssignments = new List<SPRoleAssignment>();

                var roleAssignements = item.RoleAssignments.Cast<SPRoleAssignment>().ToList();

                foreach (var rs in roleAssignements)
                {
                    foreach (SPRoleDefinition rd in rs.RoleDefinitionBindings)
                    {
                        string permission = rd.BasePermissions.ToString();
                        if (permission.Contains("EditListItems") || permission.Contains("FullMask"))
                        {
                            continue;
                        }

                        willbeRemovedAssignments.Add(rs);
                        break;
                    }
                }

                foreach (var removeItem in willbeRemovedAssignments)
                {
                    item.RoleAssignments.Remove(removeItem.Member);
                }
                item.SystemUpdate();
            }
        }

        public static void RemoveAllPermissions(this SPListItem item)
        {
            //remove all current permissions
            if (!item.HasUniqueRoleAssignments)
            {
                item.BreakRoleInheritance(false);
            }
            else
            {
                while (item.RoleAssignments.Count > 0)
                {
                    item.RoleAssignments.Remove(0);
                }
            }
        }

        public static void SetPermissions(this SPListItem item, string role, string loginName)
        {
            SetPermissions(item, role, loginName, false);
        }

        public static void SetPermissions(this SPListItem item, string role, string loginName, bool reThrow)
        {
            List<string> loginNames = new List<string>(1) { loginName };
            SetPermissions(item, role, loginNames, reThrow);
        }

        public static void SetPermissions(this SPListItem item, string role, List<string> loginNames)
        {
            SetPermissions(item, role, loginNames, false);
        }

        public static void SetPermissions(this SPListItem item, string role, List<string> loginNames, bool reThrow)
        {
            if (!item.HasUniqueRoleAssignments)
            {
                item.BreakRoleInheritance(true);
            }

            try
            {
                SPRoleDefinition roleDefinition = item.Web.RoleDefinitions[role];

                foreach (string login in loginNames)
                {
                    string loginName = login;

                    if (loginName.StartsWith(";#"))
                    {
                        loginName = loginName.Split(";#".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];
                    }
                    else if (loginName.Contains(";#"))
                    {
                        loginName = loginName.Split(";#".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1];
                    }

                    SPPrincipal principal = item.Web.Site.FindUserOrSiteGroup(loginName);

                    if (principal != null)
                    {
                        SPRoleAssignment roleAssignment = new SPRoleAssignment(principal);

                        roleAssignment.RoleDefinitionBindings.Add(roleDefinition);

                        item.RoleAssignments.Add(roleAssignment);
                    }
                }
            }
            catch (SPException spEx)
            {
                if (reThrow)
                    throw spEx;
            }
        }

        public static void RemovePermissions(this SPListItem item, string loginName)
        {
            if (!item.HasUniqueRoleAssignments)
                item.BreakRoleInheritance(true);

            SPPrincipal loginPrincipal = item.Web.Site.FindUserOrSiteGroup(loginName);

            if (loginPrincipal != null)
                item.RoleAssignments.Remove(loginPrincipal);
        }

        public static SPUser GetOwner(this SPListItem item)
        {
            string ownerValue = item[SPBuiltInFieldId.Author].ToString();
            int ownerId = int.Parse(ownerValue.Split(";#".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0]);
            return item.Web.SiteUsers.GetByID(ownerId);
        }

        public static string DisplayFormUrl(this SPListItem item)
        {
            SPList list = item.ParentList;
            SPWeb web = list.ParentWeb;

            string webUrl = web.Url;
            string dispUrl = item.ContentType.DisplayFormUrl;

            if (dispUrl == "")
                dispUrl = list.Forms[PAGETYPE.PAGE_DISPLAYFORM].Url;

            bool isLayouts = dispUrl.StartsWith("_layouts/", StringComparison.CurrentCultureIgnoreCase);

            dispUrl = String.Format("{0}/{1}?ID={2}", webUrl, dispUrl, item.ID);

            if (isLayouts)
                dispUrl = String.Format("{0}&List={1}", dispUrl, SPEncode.UrlEncode(list.ID + ""));

            return dispUrl;
        }

        public static string GetCustomProperty(this SPListItem listItem, string key)
        {
            if (listItem.Properties[key] != null)
            {
                return listItem.Properties[key].ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public static void SetCustomProperty(this SPListItem listItem, string key, string value)
        {
            try
            {
                listItem.Properties[key] = value;
                listItem.SystemUpdate();
            }
            catch
            {
                using (SPWeb web = listItem.ParentList.ParentWeb)
                {
                    web.AllowUnsafeUpdates = true;
                    SPList list = web.Lists[listItem.ParentList.ID];
                    SPListItem item = list.GetItemById(listItem.ID);
                    item.Properties[key] = value;
                    item.SystemUpdate();
                    web.AllowUnsafeUpdates = false;
                }
            }
        }

        public static void CopyMetadataTo(this SPListItem listItem, SPListItem destinationItem)
        {
            string[] ignoreFields = { "ContentType", "Content Type", "Name" };
            listItem.CopyMetadataTo(destinationItem, ignoreFields);
        }

        public static void CopyMetadataTo(this SPListItem listItem, SPListItem destinationItem, SPContentType destinationCT)
        {
            string[] ignoreFields = { "ContentType", "Content Type", "Name" };
            listItem.CopyMetadataTo(destinationItem, destinationCT, ignoreFields);
        }

        public static void CopyMetadataTo(this SPListItem listItem, SPListItem destinationItem, SPContentType destinationContentType, string[] ignoreFields)
        {
            SPFieldCollection destFields = destinationContentType == null ? listItem.Fields : destinationContentType.Fields;

            CopyMetadata(listItem, destinationItem, ignoreFields, destFields);
        }

        private static void CopyMetadata(SPListItem listItem, SPListItem destinationItem, string[] ignoreFields, SPFieldCollection destFields)
        {
            SPFieldCollection sourceFields = listItem.Fields;
            int retry = 0;
            bool success = false;
            while (retry < 5 & !success)
            {
                retry++;
                try
                {
                    foreach (SPField destField in destFields)
                    {
                        if (sourceFields.ContainsField(destField.Title))
                        {
                            SPField sourceField = sourceFields.GetField(destField.Title);
                            if (!sourceField.ReadOnlyField && !sourceField.Hidden && !ignoreFields.Contains(destField.Title))
                            {
                                object oldValue = null;
                                try
                                {
                                    oldValue = destinationItem[destField.Title];
                                    destinationItem[destField.Title] = listItem[sourceField.Title];
                                }
                                catch
                                {
                                    destinationItem[destField.Title] = oldValue;
                                }
                            }
                        }
                    }
                    destinationItem.SystemUpdate();
                    success = true;
                }
                catch (Exception ex)
                {
                    Thread.Sleep(1000);
                }
            }
        }

        public static void CopyMetadataTo(this SPListItem listItem, SPListItem destinationItem, string[] ignoreFields)
        {
            SPFieldCollection destFields = destinationItem.Fields;
            CopyMetadata(listItem, destinationItem, ignoreFields, destFields);
        }

        public static SPListItem CopyFile(this SPListItem listItem, SPFolder destFolder)
        {
            byte[] content = listItem.File.OpenBinary();
            SPFile destFile = destFolder.Files.Add(destFolder.ServerRelativeUrl + "/" + listItem.File.Name, content);
            return destFile.Item;
        }

        

        

        public static string GetStringValue(this SPListItem item, string fieldname)
        {
            if (item[fieldname] == null || string.IsNullOrEmpty(item[fieldname].ToString().Trim()))
                return string.Empty;
            SPField field = item.Fields[fieldname];
            string ReturnValue;
            switch (field.Type)
            {
                case SPFieldType.Lookup:
                    if (((SPFieldLookup)field).AllowMultipleValues == false)
                    {
                        SPFieldLookupValue lookup = new SPFieldLookupValue(item[fieldname].ToString());
                        ReturnValue = lookup.LookupValue;
                    }
                    else
                    {
                        SPFieldLookupValueCollection lookup = new SPFieldLookupValueCollection(item[fieldname].ToString());
                        ReturnValue = "";
                        foreach (SPFieldLookupValue v in lookup)
                        {
                            ReturnValue += v.LookupValue + ";";
                        }
                        ReturnValue.TrimEnd(';');
                    }
                    break;
                case SPFieldType.User:
                    if (((SPFieldUser)field).AllowMultipleValues == false)
                    {
                        SPFieldUserValue users = new SPFieldUserValue(item.Web, item[fieldname].ToString());
                        ReturnValue = users.User.Name;
                    }
                    else
                    {
                        SPFieldUserValueCollection users = new SPFieldUserValueCollection(item.Web, item[fieldname].ToString());
                        ReturnValue = users[0].User.Name;
                    }
                    break;
                case SPFieldType.MultiChoice:
                    SPFieldMultiChoiceValue values = new SPFieldMultiChoiceValue(item[fieldname].ToString());
                    ReturnValue = "";
                    for (int i = 0; i < values.Count; i++)
                    {
                        ReturnValue += values[i].ToString() + ";";
                    }
                    ReturnValue.TrimEnd(';');
                    break;
                default:
                    ReturnValue = item[fieldname].ToString().Trim();
                    break;
            }
            return ReturnValue;
        }

        public static List<SPPrincipal> GetEffectivePrincipals(this SPListItem item, params SPBasePermissions[] permissions){
            List<SPPrincipal> users = new List<SPPrincipal>();
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (var site = new SPSite(item.Web.Site.ID))
                using(var web = site.OpenWeb(item.Web.ID))
                {
                    var list = web.Lists[item.ParentList.ID];
                    var elevatedItem = list.GetItemById(item.ID);
                    foreach (SPRoleAssignment roles in elevatedItem.RoleAssignments)
                    {
                        if (users.Contains(roles.Member)) continue;

                        foreach (SPRoleDefinition binding in roles.RoleDefinitionBindings)
                        {
                            XmlDocument xmldoc = new XmlDocument();
                            xmldoc.LoadXml(binding.Xml);
                            XmlNode nodes = xmldoc.DocumentElement;
                            string[] permissionLevels = nodes.Attributes["BasePermissions"].Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            if (permissionLevels.Any(p => permissions.Any(x => x.ToString() == p)))
                            //if (permissions.Contains(binding.BasePermissions ))
                            {
                                users.Add(roles.Member);
                            }
                        }
                    }
                }


            });
          
           return users.Distinct().ToList();
        }
    }
}
        
