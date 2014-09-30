using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace LongAn.DVC.Common.Extensions
{
    public static class SPUserExtensions
    {
        public static bool InGroup(this SPUser spUser, SPGroup spGroup)
        {
            return spUser.Groups.Cast<SPGroup>()
              .Any(g => g.ID == spGroup.ID);
        }
        public static bool InGroup(this SPUser spUser, string GroupName)
        {
            return spUser.Groups.Cast<SPGroup>().Any(g => g.Name.ToLower() == GroupName.ToLower());
        }
    }
}
