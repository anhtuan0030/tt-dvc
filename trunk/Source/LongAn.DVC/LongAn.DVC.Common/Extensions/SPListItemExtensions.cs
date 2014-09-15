using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using System.Collections;

namespace LongAn.DVC.Common.Extensions
{
    public static class SPListItemExtension
    {
        #region Adding Permissions to an item

        //SPGroup group = web.Groups[0];
        //SPUser user = web.Users[0];
        //SPUser user2 = web.EnsureUser("mangaldas.mano");
        //SPUser user3 = web.EnsureUser("Domain Users"); ;
        //SPPrincipal[] principals = { group, user, user2, user3 };
        public static void SetPermissions(this SPListItem item, IEnumerable principals, SPRoleType roleType)
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

        public static void SetPermissions(this SPListItem item, IEnumerable principals, SPRoleDefinition roleDefiniton)
        {
            if (item != null)
            {
                foreach (SPPrincipal principal in principals)
                {
                    SetPermissions(item, principal, roleDefiniton);
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
        #endregion Adding Permissions to an item

        #region Deleting all user Permissions from an item
        public static void RemovePermissions(this SPListItem item, SPUser user)
        {
            if (item != null)
            {
                RemovePermissions(item, user as SPPrincipal);
            }
        }

        public static void RemovePermissions(this SPListItem item, SPPrincipal principal)
        {
            if (item != null)
            {
                item.RoleAssignments.Remove(principal);
                item.SystemUpdate();
            }
        }
        #endregion Deleting all user Permissions from an item

        #region Removing specific roles from an item
        public static void RemovePermissionsSpecificRole(this SPListItem item, SPPrincipal principal, SPRoleDefinition roleDefinition)
        {
            if (item != null)
            {
                SPRoleAssignment roleAssignment = item.RoleAssignments.GetAssignmentByPrincipal(principal);
                if (roleAssignment != null)
                {
                    if (roleAssignment.RoleDefinitionBindings.Contains(roleDefinition))
                    {
                        roleAssignment.RoleDefinitionBindings.Remove(roleDefinition);
                        roleAssignment.Update();
                    }
                }
            }
        }

        public static void RemovePermissionsSpecificRole(this SPListItem item, SPPrincipal principal, SPRoleType roleType)
        {
            if (item != null)
            {
                SPRoleDefinition roleDefinition = item.Web.RoleDefinitions.GetByType(roleType);
                RemovePermissionsSpecificRole(item, principal, roleDefinition);
            }
        }
        #endregion Removing specific roles from an item

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
    }
}
