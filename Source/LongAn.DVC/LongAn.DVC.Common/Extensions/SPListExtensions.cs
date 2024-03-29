﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.SharePoint;
using System.IO;
using System.Reflection;
using Microsoft.SharePoint.Workflow;

namespace LongAn.DVC.Common.Extensions
{
    public static class SPListExtensions
    {
        public static void AssociateWorkflow(this SPList list, string workflowTemplateName, string workflowAssocName, string association)
        {
            list.AssociateWorkflow(workflowTemplateName, workflowAssocName, association, string.Empty);
        }


        public static void AssociateWorkflow(this SPList list, string workflowTemplateName, string workflowAssocName, string association, string wfTaskList)
        {

            try
            {

         
            SPWeb web = list.ParentWeb;
            SPList historyList = null;                          // Workflow history list
            SPList taskList = null;                             // Workflow tasks list
            string workflowTemplateGuid = null;                 // Workflow template Guid
            SPWorkflowTemplate workflowTemplate = null;         // Workflow template
            SPWorkflowAssociation workflowAssociation = null;   // Workflow association
            // Workflow association name


            //workflowTemplateGuid = "BAD855B1-32CE-4bf1-A29E-463678304E1A";
            //workflowTemplate = web.WorkflowTemplates[workflowTemplateName];

            workflowTemplate = web.WorkflowTemplates.GetTemplateByName(workflowTemplateName, System.Globalization.CultureInfo.CurrentCulture);
            if (workflowTemplate == null) 
                return;
            try
            {
                historyList = web.Lists["Workflow History"];
            }
            catch (ArgumentException exc)
            {
                // Create workflow history list
                Guid listGuid = web.Lists.Add("Workflow History", "", SPListTemplateType.WorkflowHistory);
                historyList = web.Lists[listGuid];
                historyList.Hidden = true;
                historyList.Update();
            }

            try
            {
                if (string.IsNullOrEmpty(wfTaskList))
                {
                   wfTaskList = "Workflow Tasks";
                }
                taskList = web.Lists[wfTaskList];
            }
            catch (ArgumentException ex)
            {
                // Create workflow tasks list
                Guid listGuid = web.Lists.Add(wfTaskList, "", SPListTemplateType.Tasks);
                taskList = web.Lists[listGuid];
                taskList.Hidden = true;
                taskList.Update();
            }
            // Allow unsafe updates on web
            web.AllowUnsafeUpdates = true;

            try
            {
                // Create workflow association
                workflowAssociation = SPWorkflowAssociation.CreateListAssociation(
                        workflowTemplate,
                        workflowAssocName, taskList, historyList);

                // Set workflow parameters 
                workflowAssociation.AllowManual = true;
                workflowAssociation.AutoStartCreate = false;
                workflowAssociation.AutoStartChange = false;
                workflowAssociation.AssociationData = association.ToString();
                
                // Add workflow association to my list
                list.WorkflowAssociations.Add(workflowAssociation);

                // Enable workflow
                workflowAssociation.Enabled = true;
            }
            finally
            {
                web.AllowUnsafeUpdates = false;
            }
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }

        }
    
        public static void EnsureEventReciever(this SPList list, System.Type recieverClass, int sequence, SPEventReceiverSynchronization synchronous, params SPEventReceiverType[] recieverTypes)
        {
            if (list == null) return;
            try
            {
                list.ParentWeb.AllowUnsafeUpdates = true;
                string assembly = recieverClass.Assembly.FullName;
                foreach (var item in recieverTypes)
                {
                    if (!list.EventReceivers.Cast<SPEventReceiverDefinition>().Any(P => P.Class == recieverClass.FullName &&
                        P.Assembly == assembly &&
                        P.Type == item))
                    {
                        var def = list.EventReceivers.Add();
                        def.Assembly = assembly;
                        def.Class = recieverClass.FullName;
                        def.Synchronization = synchronous;
                        def.SequenceNumber = sequence;
                        
                        def.Type = recieverTypes[0];
                        foreach (var type in recieverTypes)
                        {
                            def.Type |= type;
                        }
                        def.Update();
                    }
                }
                list.Update();
            }
            catch (Exception)
            {


            }
            finally
            {
                list.ParentWeb.AllowUnsafeUpdates = false;
            }

        }


        public static void EnsureEventReciever(this SPList list, string recieverClass, string assembly, params SPEventReceiverType[] recieverTypes)
        {
            if (list == null) return;
            list.ParentWeb.AllowUnsafeUpdates = true;
            foreach (var item in recieverTypes)
            {
                if (!list.EventReceivers.Cast<SPEventReceiverDefinition>().Any(P => P.Class == recieverClass &&
                    P.Assembly == assembly &&
                    P.Type == item))
                {
                    list.EventReceivers.Add(item, assembly, recieverClass);
                }
            }
            list.Update(true);
            list.ParentWeb.AllowUnsafeUpdates = false;
        }

           
        public static void EnsureEventReciever(this SPList list, System.Type recieverClass, params SPEventReceiverType[] recieverTypes)
        {
            if (list == null) return;
            try
            {
                list.ParentWeb.AllowUnsafeUpdates = true;
                string assembly = recieverClass.Assembly.FullName;
                foreach (var item in recieverTypes)
                {
                    if (!list.EventReceivers.Cast<SPEventReceiverDefinition>().Any(P => P.Class == recieverClass.FullName &&
                        P.Assembly == assembly &&
                        P.Type == item))
                    {
                        list.EventReceivers.Add(item, assembly, recieverClass.FullName);
                    }
                }
                list.Update();
            }
            catch (Exception)
            {


            }
            finally {
                list.ParentWeb.AllowUnsafeUpdates = false;
            }
           
        }

        public static void RemoveEventReciever(this SPList list, System.Type recieverClass, params SPEventReceiverType[] recieverTypes)
        {
            if (list == null) return;

            string assembly = recieverClass.Assembly.FullName;
            List<SPEventReceiverDefinition> eventsToDelete = new List<SPEventReceiverDefinition>();

            foreach (var item in recieverTypes)
            {
                IEnumerable<SPEventReceiverDefinition> events = list.EventReceivers.Cast<SPEventReceiverDefinition>().Where(
                    P => P.Class == recieverClass.FullName && P.Assembly == assembly && P.Type == item);
                
                if (events != null)
                {
                    foreach (SPEventReceiverDefinition er in events)
                    {
                        eventsToDelete.Add(er);
                    }
                }
            }

            foreach (SPEventReceiverDefinition er in eventsToDelete)
            {
                er.Delete();
            }
        }


        public static string GetCustomProperty(this SPList list, string key)
        {
            SPFolder rootFolder = list.RootFolder;
            if (rootFolder.Properties[key] != null)
            {
                return rootFolder.Properties[key].ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public static void SetCustomProperty(this SPList list, string key, string value)
        {
            list.ParentWeb.AllowUnsafeUpdates = true;
            SPFolder rootFolder = list.RootFolder;
            rootFolder.Properties[key] = value;
            rootFolder.Update();
            list.ParentWeb.AllowUnsafeUpdates = false;
        }

        public static SPListItemCollection FindItems(this SPList list, string fieldName, string fieldValue)
        {
            SPQuery myquery = new SPQuery();

            string internalName = string.Empty;
            string type = string.Empty;
            list.GetColumnProperties(fieldName, out internalName, out type);

            myquery.Query = string.Format(@"<Where><Eq><FieldRef Name='{0}' />
                                                        <Value Type='{1}'>{2}</Value>
                                                    </Eq>
                                            </Where>", internalName, type, fieldValue);
            return list.GetItems(myquery);
        }

        public static bool IsValueUnique(this SPList list, string fieldName, string fieldValue, int currentItemId)
        {
            SPQuery query = new SPQuery();
            query.Query = string.Format(@"<Where>
                                            <Eq><FieldRef Name='{0}' />
                                                        <Value Type='Text'>{1}</Value>
                                                    </Eq>
                                            </Where>", fieldName, fieldValue);
            if (currentItemId > 0)
            {
                query.Query = string.Format(
                            @"
                                <Where>
                                   <And>
                                     <Eq>
                                        <FieldRef Name='{0}' />
                                        <Value Type='Text'>{1}</Value>
                                     </Eq>
                                     <Neq>
                                        <FieldRef Name='ID' />
                                        <Value Type='Text'>{2}</Value>
                                     </Neq>
                                  </And>
                               </Where>
                            ", fieldName, fieldValue, currentItemId);
            }

            return (list.GetItems(query).Count == 0 );
        }

        public static SPContentTypeId EnsureContentTypeInListWithoutPrivileges(this SPList list, string contentTypeId)
        {
            SPContentTypeId ctIdReturn = SPContentTypeId.Empty;
            if (!list.ContentTypesEnabled)
                list.ContentTypesEnabled = true;

            SPContentTypeId sourceCTId = new SPContentTypeId(contentTypeId);
            SPContentTypeId foundCTId = list.ContentTypes.BestMatch(sourceCTId);
            bool found = (foundCTId.Parent.CompareTo(sourceCTId) == 0);
            SPContentType ct = list.ParentWeb.FindContentType(sourceCTId);

            if (found)
            {
                ctIdReturn = list.ContentTypes[ct.Name].Id;
            }
            else
            {
                if (ct != null)
                    ctIdReturn = list.ContentTypes.Add(ct).Id;
            }
            list.Update();

            return ctIdReturn;
        }

        public static SPContentTypeId EnsureContentTypeInList(this SPList list, string contentTypeId)
        {
            SPContentTypeId ctIdReturn = SPContentTypeId.Empty;
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite(list.ParentWeb.Site.ID))
                {
                    using (SPWeb web = site.OpenWeb(list.ParentWeb.ID))
                    {
                        try
                        {
                            if (SPContext.Current != null && SPContext.Current.Web != null) SPContext.Current.Web.AllowUnsafeUpdates = true;
                            web.AllowUnsafeUpdates = true;

                            SPList listUpdate = web.Lists[list.ID];

                            if (!listUpdate.ContentTypesEnabled)
                                listUpdate.ContentTypesEnabled = true;

                            SPContentTypeId sourceCTId = new SPContentTypeId(contentTypeId);
                            SPContentTypeId foundCTId = listUpdate.ContentTypes.BestMatch(sourceCTId);
                            bool found = (foundCTId.Parent.CompareTo(sourceCTId) == 0);
                            SPContentType ct = listUpdate.ParentWeb.FindContentType(sourceCTId);

                            if (found)
                            {
                                ctIdReturn = listUpdate.ContentTypes[ct.Name].Id;
                            }
                            else
                            {
                                if (ct != null)
                                {
                                    ctIdReturn = listUpdate.ContentTypes.Add(ct).Id;                                   
                                }
                            }
                            listUpdate.Update();                         
                        }
                        catch(Exception e)
                        {
                            
                        }
                        finally
                        {
                            if (SPContext.Current != null && SPContext.Current.Web!=null) SPContext.Current.Web.AllowUnsafeUpdates = false;
                            web.AllowUnsafeUpdates = false;
                        }
                    }
                }
            });

            return ctIdReturn;
        }

        public static SPContentTypeId FindContentType(this SPList list, string contentTypeId)
        {
            SPContentTypeId ctIdReturn = SPContentTypeId.Empty;

            SPContentTypeId sourceCTId = new SPContentTypeId(contentTypeId);
            SPContentTypeId foundCTId = list.ContentTypes.BestMatch(sourceCTId);
            bool found = (foundCTId.Parent.CompareTo(sourceCTId) == 0);
            SPContentType ct = list.ParentWeb.FindContentType(sourceCTId);

            if (found)
            {
                ctIdReturn = list.ContentTypes[ct.Name].Id;
            }
            return ctIdReturn;
        }

        

        //public static List<SPFile> GetRelateddocuments(this SPListItem item)
        //{
        //    List<SPFile> listFile = new List<SPFile>();

        //    string contractNumberFieldName = item.Web.Site.GetFeaturePropertyValue(Constants.Workflow.INTEL_WF_FEATURE_ID, Constants.Workflow.CONTRACT_NUMBER_FIELD_NAME);
        //    if (string.IsNullOrEmpty(contractNumberFieldName) || !item.Fields.ContainsField(contractNumberFieldName))
        //        return null;

        //    if (item[contractNumberFieldName] == null) return null;

        //    string contentTypeFieldName = item.Web.Site.GetFeaturePropertyValue(Constants.Workflow.INTEL_WF_FEATURE_ID, Constants.Workflow.CONTENT_TYPE_FIELD_NAME);
        //    if (string.IsNullOrEmpty(contentTypeFieldName) || !item.Web.Site.RootWeb.AvailableFields.ContainsField(contentTypeFieldName))
        //        return null;

        //    string includeWithDeliveryFieldName = item.Web.Site.GetFeaturePropertyValue(Constants.Workflow.INTEL_WF_FEATURE_ID, Constants.Workflow.INCLUDE_WITH_DELIVERY_FIELD_NAME);
        //    if (string.IsNullOrEmpty(includeWithDeliveryFieldName) || !item.Web.Site.RootWeb.AvailableFields.ContainsField(includeWithDeliveryFieldName))
        //        return null;

        //    string relatedContractNumberFieldName = item.Web.Site.GetFeaturePropertyValue(Constants.Workflow.INTEL_WF_FEATURE_ID, Constants.Workflow.RELATED_CONTRACT_NUMBER_FIELD_NAME);
        //    if (string.IsNullOrEmpty(relatedContractNumberFieldName) || !item.Web.Site.RootWeb.AvailableFields.ContainsField(relatedContractNumberFieldName))
        //        return null;

        //    SPField contractNumberField = item.Fields[contractNumberFieldName];
        //    SPField contentTypeField = item.Web.Site.RootWeb.AvailableFields[contentTypeFieldName];
        //    SPField includeWithDeliveryField = item.Web.Site.RootWeb.AvailableFields[includeWithDeliveryFieldName];
        //    SPField relatedContractNumberField = item.Web.Site.RootWeb.AvailableFields[relatedContractNumberFieldName];

        //    SearchCriteria criteriaContentType = new SearchCriteria();
        //    criteriaContentType.FieldId = contentTypeField.Id.ToString();
        //    criteriaContentType.FieldType = contentTypeField.Type;
        //    criteriaContentType.Operator = Operators.Equal;
        //    criteriaContentType.Value = "Attachment";

        //    SearchCriteria criteriaIncludeWithDelivery = new SearchCriteria();
        //    criteriaIncludeWithDelivery.FieldId = includeWithDeliveryField.Id.ToString();
        //    criteriaIncludeWithDelivery.FieldType = includeWithDeliveryField.Type;
        //    criteriaIncludeWithDelivery.Operator = Operators.Equal;
        //    criteriaIncludeWithDelivery.Value = "1";

        //    SearchCriteria criteriaRelatedContractNumber = new SearchCriteria();
        //    criteriaRelatedContractNumber.FieldId = relatedContractNumberField.Id.ToString();
        //    criteriaRelatedContractNumber.FieldType = relatedContractNumberField.Type;
        //    criteriaRelatedContractNumber.Operator = Operators.Equal;
        //    criteriaRelatedContractNumber.Value = item[contractNumberFieldName].ToString();

        //    List<SearchCriteria> searchCriteriaList = new List<SearchCriteria>();
        //    searchCriteriaList.Add(criteriaContentType);
        //    searchCriteriaList.Add(criteriaIncludeWithDelivery);
        //    searchCriteriaList.Add(criteriaRelatedContractNumber);

        //    SearchDefinition searchDefinition = new SearchDefinition();
        //    searchDefinition.UseGlobalOperatorOR = false;
        //    searchDefinition.SearchCriteriaList = searchCriteriaList;

        //    FieldSetting contractNumberFieldResult = new FieldSetting();
        //    contractNumberFieldResult.FieldId = contractNumberField.Id.ToString();
        //    List<FieldSetting> resultFields = new List<FieldSetting>();
        //    resultFields.Add(contractNumberFieldResult);

        //    string strCAML = CAMLHelper.GetCAMLQueryFromSearchDefinition(searchDefinition);

        //    SPSiteDataQuery dataQuery = new SPSiteDataQuery();
        //    dataQuery.Query = strCAML;
        //    dataQuery.Lists = CAMLHelper.GetCAMLListBaseTypeSearch(SearchListBaseType.DocumentLibrary);
        //    dataQuery.ViewFields = CAMLHelper.GetCAMLFieldsResult(item.Web, null, resultFields);
        //    dataQuery.Webs = CAMLHelper.GetCAMLScopeSearch(0);

        //    try
        //    {
        //        DataTable searchResultsData = null;
        //        searchResultsData = item.Web.GetSiteData(dataQuery);
        //        foreach (DataRow row in searchResultsData.Rows)
        //        {
        //            SPList list = item.Web.Lists.GetList(new Guid(row["ListId"].ToString()), false);
        //            SPListItem itemGet = list.GetItemById(Convert.ToInt32(row["ID"].ToString()));
        //            if (itemGet.File != null)
        //                listFile.Add(itemGet.File);
        //        }
        //    }
        //    catch { }
        //    return listFile;
        //}

        public static void AddEventReceiver(this SPList list, string eventName, SPEventReceiverType type, string assemblyName, string className)
        {
            SPEventReceiverDefinition eventDefinition = list.EventReceivers.Add();
            eventDefinition.Name = eventName;
            eventDefinition.Type = type;
            eventDefinition.Synchronization = SPEventReceiverSynchronization.Synchronous;
            eventDefinition.Assembly = assemblyName;
            eventDefinition.Class = className;
            eventDefinition.Update();
        }

        
        public static void CopyAllContentTypesToList(this SPList source, SPList destinationList)
        {
            //Copy Content Types
            foreach (SPContentType ct in source.ContentTypes)
            {
                SPContentType webCT = source.ParentWeb.AvailableContentTypes.Cast<SPContentType>()
                                                               .Where(p => p.Name == ct.Name)
                                                               .FirstOrDefault();
                if (webCT == null) continue;

                SPContentType listCT = destinationList.ContentTypes.Cast<SPContentType>()
                                                      .Where(p => p.Name == webCT.Name)
                                                      .FirstOrDefault();

                if (listCT == null)
                    destinationList.ContentTypes.Add(ct);
            }
            destinationList.Update();
        }

        public static void CopyAllViewsToList(this SPList source, SPList destinationList)
        {
            //Copy View
            try
            {
                List<Guid> ids = destinationList.Views.Cast<SPView>().Select(p => p.ID).ToList();
                foreach (var id in ids) destinationList.Views.Delete(id);

                foreach (SPView view in source.Views)
                {

                    SPView existView = destinationList.Views.Cast<SPView>()
                                                                   .Where(p => p.Title == view.Title)
                                                                   .FirstOrDefault();

                    if (existView != null) continue;

                    destinationList.Views.Add(view.Title, view.ViewFields.ToStringCollection(), view.Query, view.RowLimit, view.Paged, view.DefaultView);
                }
                destinationList.Update();
            }
            catch (Exception ex)
            {
                //CCIUtility.LogError(ex.Message, "Hypertek.IOffice.Common");
            }
        }

        public static void CopyAllItemsToList(this SPList source, SPList destinationList)
        {
            //using (SPWeb web = SPContext.Current.Web)
            SPWeb web = SPContext.Current.Web;
            {
                web.AllowUnsafeUpdates = true;
                foreach (SPListItem item in source.Items)
                {
                    //SPListItem SourceItem = CurrentList.GetItemById(int.Parse(Request.QueryString["ID"]));
                    SPListItem newDestItem;

                    if (item.File == null)
                    {
                        newDestItem = destinationList.Items.Add();

                        foreach (string fileName in item.Attachments)
                        {
                            SPFile file = item.ParentList.ParentWeb.GetFile(item.Attachments.UrlPrefix + fileName);
                            byte[] imageData = file.OpenBinary();
                            newDestItem.Attachments.Add(fileName, imageData);
                        }

                        try
                        {
                            foreach (SPField field in item.Fields)
                                if ((!field.ReadOnlyField) && (field.InternalName != "Attachments"))
                                    newDestItem[field.InternalName] = item[field.InternalName];
                        }
                        catch { }
                        //newDestItem["Title"] = "";
                    }
                    else
                    {
                        SPFolder currentFolder = item.Folder == null ? source.RootFolder : item.Folder;
                        SPFile newFile = currentFolder.Files.Add(item.File.Name, item.File.OpenBinary());
                        currentFolder.Update();
                        newDestItem = newFile.Item;

                        foreach (SPField field in item.Fields)
                            if ((!field.ReadOnlyField) && (field.InternalName != "Attachments") && (field.InternalName != "FileLeafRef"))
                                newDestItem[field.InternalName] = item[field.InternalName];
                    }

                    newDestItem.Update();
                }
                web.AllowUnsafeUpdates = false;
            }
        }

        public static void AddPermission(this SPList list, SPGroup group, SPRoleDefinition role)
        {
            try
            {
                if (!list.HasUniqueRoleAssignments)
                {
                    list.BreakRoleInheritance(true);
                }

                SPRoleAssignment groupRoleAssignment = new SPRoleAssignment(group);
                groupRoleAssignment.RoleDefinitionBindings.RemoveAll();
                groupRoleAssignment.RoleDefinitionBindings.Add(role);
                list.RoleAssignments.Add(groupRoleAssignment);

                list.Update();
            }
            catch (Exception ex)
            {
                
            }
        }

        #region Adding permission to list
        public static void SetPermissions(this SPList list, IEnumerable<SPPrincipal> principals, SPRoleType roleType)
        {
            if (list != null)
            {
                foreach (SPPrincipal principal in principals)
                {
                    SPRoleDefinition roleDefinition = list.ParentWeb.RoleDefinitions.GetByType(roleType);
                    SetPermissions(list, principal, roleDefinition);
                }
            }
        }


        public static void SetPermissions(this SPList list, SPUser user, SPRoleType roleType)
        {
            if (list != null)
            {
                SPRoleDefinition roleDefinition = list.ParentWeb.RoleDefinitions.GetByType(roleType);
                SetPermissions(list, (SPPrincipal)user, roleDefinition);
            }
        }

        public static void SetPermissions(this SPList list, SPPrincipal principal, SPRoleType roleType)
        {
            if (list != null)
            {
                SPRoleDefinition roleDefinition = list.ParentWeb.RoleDefinitions.GetByType(roleType);
                SetPermissions(list, principal, roleDefinition);
            }
        }

        public static void SetPermissions(this SPList list, SPUser user, SPRoleDefinition roleDefinition)
        {
            if (list != null)
            {
                SetPermissions(list, (SPPrincipal)user, roleDefinition);
            }
        }

        public static void SetPermissions(this SPList list, SPPrincipal principal, SPRoleDefinition roleDefinition)
        {
            if (list != null)
            {
                SPRoleAssignment roleAssignment = new SPRoleAssignment(principal);

                roleAssignment.RoleDefinitionBindings.Add(roleDefinition);
                list.RoleAssignments.Add(roleAssignment);
            }
        }

        #endregion Adding permission to list

        #region Updating or Modifying Permissions on an item
        public static void ChangePermissions(this SPList list, SPPrincipal principal, SPRoleType roleType)
        {
            if (list != null)
            {
                SPRoleDefinition roleDefinition = list.ParentWeb.RoleDefinitions.GetByType(roleType);
                ChangePermissions(list, principal, roleDefinition);
            }
        }

        public static void ChangePermissions(this SPList list, SPPrincipal principal, SPRoleDefinition roleDefinition)
        {
            SPRoleAssignment roleAssignment = list.RoleAssignments.GetAssignmentByPrincipal(principal);
            if (roleAssignment != null)
            {
                roleAssignment.RoleDefinitionBindings.RemoveAll();
                roleAssignment.RoleDefinitionBindings.Add(roleDefinition);
                roleAssignment.Update();
            }
        }
        #endregion Updating or Modifying Permissions on an item

        public static void ClearPermission(this SPList list)
        {
            if (!list.HasUniqueRoleAssignments)
            {
                list.BreakRoleInheritance(true);
            }
            int countListRole = list.RoleAssignments.Count;
            for (int i = 0; i < countListRole; i++)
            {
                list.RoleAssignments.Remove(0);
            }
        }

        public static void AddECBMenu(this SPList list, string title, string location, string url, SPBasePermissions basePermission)
        {
            SPUserCustomActionCollection spUserCustomActionCollection = list.UserCustomActions;
            var spUserCustomAction = spUserCustomActionCollection.FirstOrDefault(p => p.Title == title);
            if (spUserCustomAction == null)
            {
                spUserCustomAction = spUserCustomActionCollection.Add();
                spUserCustomAction.Location = location;
                spUserCustomAction.Sequence = 100;
                spUserCustomAction.Title = title;
                if (basePermission != null)
                {
                    spUserCustomAction.Rights = basePermission;
                }
                spUserCustomAction.Url = url;
                spUserCustomAction.Update();
            }
        }

        /// <summary>
        /// Get column internal name and column type
        /// </summary>
        /// <param name="list">The current list</param>
        /// <param name="column">Column title or column internal name</param>
        /// <param name="columnName">Internal column name</param>
        /// <param name="columnType">Column type</param>
        public static void GetColumnProperties(this SPList list, string column,
                                                 out string columnName, out string columnType)
        {
            columnName = string.Empty;
            columnType = string.Empty;

            foreach (SPField fld in list.Fields)
            {
                if (column.Trim().CompareTo(fld.Title) == 0 || fld.InternalName.CompareTo(column.Trim()) == 0)
                {
                    columnName = fld.InternalName;
                    columnType = fld.TypeAsString;
                    break;
                }
            }
        }
    }
}
