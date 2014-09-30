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
            if (SPContext.Current.Web.CurrentUser != null && !SPContext.Current.Web.CurrentUser.InGroup(Constants.ConfGroupNguoiDung) == true)
            {
                IsDVCQuanTri = true;
            }
        }
    }
}
