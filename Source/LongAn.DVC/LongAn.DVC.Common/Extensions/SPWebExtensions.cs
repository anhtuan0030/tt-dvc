using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.SharePoint;
using System.Reflection;
using System.IO;
using Microsoft.SharePoint.Utilities;


namespace LongAn.DVC.Common.Extensions
{
    public static class SPWebExtensions
    {
       
        public static void EnsureWebpartPage(this SPWeb web, string url, string title)
        {
            EnsureWebpartPage(web, url, title, false);
        }
        private static bool IsAbsoluteUrl(string url)
        {
            return url.ToLower().StartsWith("http");

            Uri result;
            return Uri.TryCreate(url, UriKind.Absolute, out result);
        }

        public static void EnsureWebpartPage(this SPWeb web, string url, string title, bool overwrite)
        {
            url = url.TrimStart("/".ToCharArray());

            SPFile file = null;
            if (IsAbsoluteUrl(url))
            {
                file = web.Site.RootWeb.GetFile(url);
            }
            else{
                file = web.GetFile(url);
            }

            if (file.Exists && !overwrite) return;

            //if (!file.Exists)
            {
                string folderPath = Path.GetDirectoryName(url);
                SPFolder folder = null;
                if (IsAbsoluteUrl(folderPath))
                {
                    folder = web.Site.RootWeb.GetFolder(folderPath);
                }
                else{
                folder = web.GetFolder(folderPath);
                }
                if (folder == null)
                {
                    return;
                }
                var forms = folder.SubFolders.Cast<SPFolder>().FirstOrDefault(p => p.Name == "Forms");
                if (forms != null) folder = forms;

                string newFilename = Path.GetFileName(url);

                string templateFilename = "spstd8.aspx";
                string hive = SPUtility.GetGenericSetupPath("TEMPLATE\\1033\\STS\\DOCTEMP\\SMARTPGS\\");
                using (FileStream stream = new FileStream(hive + templateFilename, FileMode.Open))
                {


                    SPFileCollection files = folder.Files;
                    SPFile newFile = files.Add(newFilename, stream, true);
                }
                //SPList list = web.Lists[folder.ParentListId];

                //if (list == null) return;


                //string postInformation =

                //  "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +

                //  "<Method>" +

                //    "<SetList Scope=\"Request\">" + list.ID + "</SetList>" +

                //    "<SetVar Name=\"ID\">New</SetVar>" +

                //    "<SetVar Name=\"Cmd\">NewWebPage</SetVar>" +

                //    "<SetVar Name=\"Type\">WebPartPage</SetVar>" +

                //    "<SetVar Name=\"WebPartPageTemplate\">8</SetVar>" +

                //    string.Format("<SetVar Name=\"Title\">{0}</SetVar>" , title)+

                //    "<SetVar Name=\"Overwrite\">true</SetVar>" +

                //  "</Method>";

                //web.ProcessBatchData(postInformation);
            }
        }
        private static SPWeb AddSite(this SPWeb parentSite, string url, string name, string templateName)
        {
            if (string.IsNullOrEmpty(url)) url = name;
            SPWeb newSite = null;
            bool allowUnsafeupdates = parentSite.AllowUnsafeUpdates;
            parentSite.AllowUnsafeUpdates = true;
            try
            {
                //Create the new site from the template
                
                SPWebTemplate template = GetTemplate(templateName, parentSite.Site);
                if (template != null)
                {
                    newSite = parentSite.Webs.Add(url, name, string.Empty, 1033, template, false, false);
                    newSite.Update();
                }

            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            finally
            {
                parentSite.AllowUnsafeUpdates = allowUnsafeupdates;

            }
            return newSite;
        }

        private static SPWebTemplate GetTemplate(string solutionName, SPSite siteCollection)
        {
            SPWebTemplate templateName = null;
            SPWebTemplateCollection coll = siteCollection.GetWebTemplates(1033);

            foreach (SPWebTemplate template in coll)
            {
                if (template.Name.Contains(solutionName))
                {
                    templateName = template;
                }
            }

            return templateName;
        }

        
        private static void DeleteSite(SPWeb existed)
        {
            foreach (SPWeb item in existed.Webs)
            {
                DeleteSite(item);
            }

            if (existed.Webs == null || existed.Webs.Count == 0){
                var parent = existed.ParentWeb;
                existed.Delete();
                //DeleteSite(parent);
            }
            
           
        }
        public static void AddRoleDefinition(this SPWeb web, SPRoleDefinition role, bool hide)
        {
            SPRoleDefinitionCollection roles = web.RoleDefinitions;

            SPWeb m_web = typeof(SPRoleDefinitionCollection).GetField("m_web", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(roles) as SPWeb;
            if (!m_web.HasUniqueRoleDefinitions)
                throw new ArgumentException(SPResource.GetString("CannotCustomizeRoleDefinitionOnInheritedWeb", new object[0]));
            object m_webRequest = typeof(SPWeb).GetProperty("Request", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(m_web, null);
            Type[] signature = new Type[] { typeof(string), typeof(string), typeof(string), typeof(bool), typeof(int), typeof(ulong), typeof(byte), typeof(int) };
            object[] args = new object[] { m_web.Url, role.Name, role.Description, true, role.Order, (ulong)role.BasePermissions, (byte)0, 0 };
            m_webRequest.GetType().GetMethod("AddRoleDef", signature).Invoke(m_webRequest, args);
            typeof(SPRoleDefinitionCollection).GetMethod("ClearAllVars", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(roles, new object[] { });

        }

        public static string GetFeaturePropertyValue(this SPWeb web, string featureId, string key)
        {
            var theFeature = web.Features[new Guid(featureId)];
            if (theFeature != null && theFeature.Properties[key] != null)
                return theFeature.Properties[key].Value;
            return string.Empty;
        }

        public static List<SPContentType> FindAllContentTypesOf(this SPWeb web, SPContentTypeId parent)
        {
            return web.AvailableContentTypes.Cast<SPContentType>()
                .Where<SPContentType>(ct => ct.Id.IsChildOf(parent))
                .ToList<SPContentType>();
        }       
        
        public static SPContentType FindContentTypeReadOnly(this SPWeb web, SPContentTypeId contentTypeId)
        {
            return web.AvailableContentTypes.Cast<SPContentType>().FirstOrDefault(ct => ct.Id == contentTypeId);
        }

        public static SPContentType FindContentType(this SPWeb web, SPContentTypeId contentTypeId)
        {
            SPContentType contentType = web.ContentTypes.Cast<SPContentType>().FirstOrDefault(ct => ct.Id == contentTypeId);
            if (contentType == null && web.ParentWeb != null)
                contentType = web.ParentWeb.FindContentType(contentTypeId);
            return contentType;
        }

        public static List<SPField> FindAllFieldOfType(this SPWeb web, SPFieldType type)
        {
            return web.AvailableFields.Cast<SPField>()
                .Where<SPField>(ct => ct.Type == type)
                .OrderBy(ct => ct.Title)
                .ToList<SPField>();
        }

        public static SPField FindField(this SPWeb web, string fieldId)
        {
            return web.AvailableFields.Cast<SPField>().FirstOrDefault(f => f.Id == new Guid(fieldId));
        }

        public static string GetCustomProperty(this SPWeb web, string key)
        {
            String propValue = String.Empty;
            if (web.Properties.ContainsKey(key))
            {
                propValue = web.Properties[key];
            }

            return propValue;
        }

        public static void SetCustomProperty(this SPWeb web, string key, string value)
        {
            web.AllowUnsafeUpdates = true;
            if (web.Properties.ContainsKey(key))
            {
                web.Properties[key] = value;
            }
            else
            {
                web.Properties.Add(key, value);
            }
            web.Properties.Update();
            web.AllowUnsafeUpdates = false;
        }

        public static bool ListExists(this SPWeb web, string listName)
        {
            var lists = web.Lists;
            foreach (SPList list in lists)
            {
                if (list.Title.Equals(listName))
                    return true;
            }
            return false;
        }
       
        
        public static SPRoleDefinition AddPermissionLevel(this SPWeb web, bool isAdd, bool isEdit, bool isDelete, string roleName)
        {
            return web.AddPermissionLevel(isAdd, isEdit, isDelete, roleName, roleName);
        }

        public static SPRoleDefinition AddPermissionLevel(this SPWeb web, bool isAdd, bool isEdit, bool isDelete, string roleName, string description)
        {
            SPRoleDefinitionCollection roles = web.RoleDefinitions;
            SPRoleDefinition role = null;
            foreach (SPRoleDefinition item in roles)
            {
                if (String.Compare(item.Name.Trim(), roleName) == 0)
                {
                    role = item;
                    break;
                }
            }

            if (role == null)
            {
                web.AllowUnsafeUpdates = true;
                role = new SPRoleDefinition();

                SPBasePermissions edit = SPBasePermissions.ViewListItems;
                SPBasePermissions add = SPBasePermissions.ViewListItems;
                SPBasePermissions delete = SPBasePermissions.ViewListItems;

                if (isEdit)
                {
                    edit = SPBasePermissions.EditListItems;
                }

                if (isAdd)
                {
                    add = SPBasePermissions.AddListItems;
                }

                if (isDelete)
                {
                    delete = SPBasePermissions.DeleteListItems;
                }

                role.BasePermissions = SPBasePermissions.BrowseDirectories | SPBasePermissions.Open | SPBasePermissions.OpenItems |
                                       SPBasePermissions.ViewListItems | SPBasePermissions.ViewFormPages | edit | add | delete |
                                       SPBasePermissions.ViewPages | SPBasePermissions.CancelCheckout | SPBasePermissions.ApproveItems |
                                       SPBasePermissions.ViewVersions;
                role.Description = description;
                role.Name = roleName;
                web.RoleDefinitions.Add(role);
                web.Update();
            }

            return role;
        }

        public static SPRoleDefinition AddPermissionLevel(this SPWeb web, bool isApprove, bool isAdd, bool isEdit, bool isDelete, string roleName)
        {
            SPRoleDefinitionCollection roles = web.RoleDefinitions;
            SPRoleDefinition role = null;
            foreach (SPRoleDefinition item in roles)
            {
                if (String.Compare(item.Name.Trim(), roleName) == 0)
                {
                    role = item;
                    break;
                }
            }

            if (role == null)
            {
                web.AllowUnsafeUpdates = true;
                role = new SPRoleDefinition();
                SPBasePermissions approve = SPBasePermissions.ViewListItems;
                SPBasePermissions edit = SPBasePermissions.ViewListItems;
                SPBasePermissions add = SPBasePermissions.ViewListItems;
                SPBasePermissions delete = SPBasePermissions.ViewListItems;

                if (isApprove)
                {
                    approve = SPBasePermissions.ApproveItems;
                }

                if (isEdit)
                {
                    edit = SPBasePermissions.EditListItems;
                }

                if (isAdd)
                {
                    add = SPBasePermissions.AddListItems;
                }

                if (isDelete)
                {
                    delete = SPBasePermissions.DeleteListItems;
                }

                role.BasePermissions = SPBasePermissions.BrowseDirectories | SPBasePermissions.Open | SPBasePermissions.OpenItems |
                                       SPBasePermissions.ViewListItems | SPBasePermissions.ViewFormPages | edit | add | delete | approve |
                                       SPBasePermissions.ViewPages | SPBasePermissions.CancelCheckout | SPBasePermissions.ViewVersions;

                role.Name = roleName;
                web.RoleDefinitions.Add(role);
                web.Update();
            }

            return role;
        }

        public static void CreateNewGroup(this SPWeb web, string groupName, string groupDescription)
        {
            if (string.IsNullOrEmpty(groupName)) return;
            try
            {

                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    SPUserCollection users = web.AllUsers;
                    SPUser owner = users[web.Author.LoginName];
                    SPMember member = users[web.Author.LoginName];

                    try
                    {
                        //Add the group to the SPWeb web
                        SPGroupCollection groups = web.SiteGroups;
                        groups.Add(groupName, member, owner, groupDescription);

                        //Associate the group with SPWeb
                        web.AssociatedGroups.Add(web.SiteGroups[groupName]);
                        web.Update();
                    }
                    catch { }

                    //Assignment of the roles to the group.
                    SPRoleAssignment assignment = new SPRoleAssignment(web.SiteGroups[groupName]);
                    SPRoleDefinition _role = web.RoleDefinitions.GetByType(SPRoleType.Contributor);
                    assignment.RoleDefinitionBindings.Add(_role);
                    web.RoleAssignments.Add(assignment);
                });
            }
            catch
            {
                // Not catch exception because check group exists
            }
        }

        public static void AddExistedGroup(this SPWeb web, string groupName, SPRoleType role)
        {
            if (string.IsNullOrEmpty(groupName)) return;

            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    web.AssociatedGroups.Add(web.SiteGroups[groupName]);
                    web.Update();

                    //Assignment of the roles to the group.
                    SPRoleAssignment assignment = new SPRoleAssignment(web.SiteGroups[groupName]);
                    SPRoleDefinition _role = web.RoleDefinitions.GetByType(role);
                    assignment.RoleDefinitionBindings.Add(_role);
                    web.RoleAssignments.Add(assignment);
                });
            }
            catch
            { }
        }

        /// <summary>
        /// Add group to SPWeb and set permission for this group
        /// </summary>
        /// <param name="web"></param>
        /// <param name="groupName"></param>
        /// <param name="groupDescription"></param>
        /// <param name="role"></param>
        /// <param name="member"></param>
        public static void CreateNewGroup(this SPWeb web, string groupName, string groupDescription, SPRoleType role)
        {
            if (string.IsNullOrEmpty(groupName)) return;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite spSite = new SPSite(web.Site.ID))
                    {
                        using (SPWeb spWeb = spSite.OpenWeb(web.ID))
                        {
                            SPUserCollection users = spWeb.AllUsers;
                            SPUser owner = users[spWeb.Author.LoginName];
                            SPMember member = users[spWeb.Author.LoginName];

                            //Add the group to the SPWeb web
                            SPGroupCollection groups = spWeb.SiteGroups;
                            var isExits = groups.Cast<SPGroup>().FirstOrDefault(p => p.Name == groupName);
                            if (isExits == null)
                            {
                                groups.Add(groupName, member, owner, groupDescription);
                            }
                            //Associate the group with SPWeb
                            spWeb.AssociatedGroups.Add(spWeb.SiteGroups[groupName]);
                            spWeb.Update();

                            //Assignment of the roles to the group.
                            SPRoleAssignment assignment = new SPRoleAssignment(spWeb.SiteGroups[groupName]);
                            SPRoleDefinition _role = spWeb.RoleDefinitions.GetByType(role);
                            assignment.RoleDefinitionBindings.Add(_role);
                            spWeb.RoleAssignments.Add(assignment);
                        }
                    }
                });
            }
            catch
            {
                // Not catch exception because check group exists
            }
        }

        public static void CreateNewGroup(this SPWeb web, string groupName, string groupDescription, SPRoleDefinition role)
        {
            if (string.IsNullOrEmpty(groupName)) return;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite spSite = new SPSite(web.Site.ID))
                    {
                        using (SPWeb spWeb = spSite.OpenWeb(web.ID))
                        {
                            SPUserCollection users = spWeb.AllUsers;
                            SPUser owner = users[spWeb.Author.LoginName];
                            SPMember member = users[spWeb.Author.LoginName];

                            //Add the group to the SPWeb web
                            SPGroupCollection groups = spWeb.SiteGroups;
                            var isExits = groups.Cast<SPGroup>().FirstOrDefault(p => p.Name == groupName);
                            if (isExits == null)
                            {
                                groups.Add(groupName, member, owner, groupDescription);
                            }
                            //Associate the group with SPWeb
                            spWeb.AssociatedGroups.Add(spWeb.SiteGroups[groupName]);
                            spWeb.Update();

                            //Assignment of the roles to the group.
                            SPRoleAssignment assignment = new SPRoleAssignment(spWeb.SiteGroups[groupName]);
                            assignment.RoleDefinitionBindings.Add(role);
                            spWeb.RoleAssignments.Add(assignment);
                        }
                    }
                });
            }
            catch
            {
                // Not catch exception because check group exists
            }
        }

        public static SPPrincipal GetPrinciple(this SPWeb web, string userOrGroup)
        {
            SPPrincipal principal;
            SPGroupCollection groups = web.SiteGroups;
            SPGroup group = null;

            foreach (SPGroup item in groups)
            {
                if (String.Compare(item.Name.Trim(), userOrGroup.Trim()) == 0)
                {
                    group = item;
                    break;
                }
            }

            if (group != null)
            {
                principal = group;
            }
            else
            {
                SPUser addedUser = null;
                try
                {
                    addedUser = web.EnsureUser(userOrGroup);
                    principal = addedUser;
                }
                catch (Exception ex)
                {
                    principal = null;
                    LoggingServices.LogException(ex);
                }
            }

            return principal;
        }
    }
}
