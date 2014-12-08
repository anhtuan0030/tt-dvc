using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace LongAn.DVC.HomeSite.WebParts.TraCuuThuTucHanhChinh
{
    [ToolboxItemAttribute(false)]
    public class TraCuuThuTucHanhChinh : WebPart
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/LongAn.DVC.HomeSite.WebParts/TraCuuThuTucHanhChinh/TraCuuThuTucHanhChinhUserControl.ascx";

        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            Controls.Add(control);
        }
    }
}
