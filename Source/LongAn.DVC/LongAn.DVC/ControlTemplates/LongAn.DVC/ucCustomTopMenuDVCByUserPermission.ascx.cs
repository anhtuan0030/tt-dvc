using LongAn.DVC.Common;
using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using LongAn.DVC.Common.Extensions;

namespace LongAn.DVC.ControlTemplates.LongAn.DVC
{
    public partial class ucCustomTopMenuDVCByUserPermission : UserControl
    {
        public bool IsDVCQuanTri = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            SPWeb spWeb = SPContext.Current.Web;
            SPUser curUser = spWeb.CurrentUser;

            if (curUser != null && !curUser.InGroup(Constants.ConfGroupNguoiDung) == true && curUser.ID != spWeb.Site.SystemAccount.ID)
            {
                IsDVCQuanTri = true;
            }
        }
    }
}
