using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace LongAn.DVC.ControlTemplates.LongAn.DVC
{
    public partial class ucUserAuthenticationBox : UserControl
    {
        public string currentUsername = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SPContext.Current.Web.CurrentUser != null)
            {
                pnlUserInfoBox.Visible = true;
                currentUsername = SPContext.Current.Web.CurrentUser.Name;
            }
        }
    }
}
