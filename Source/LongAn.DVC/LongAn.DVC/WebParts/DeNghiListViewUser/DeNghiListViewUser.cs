using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace LongAn.DVC.WebParts.DeNghiListViewUser
{
    [ToolboxItemAttribute(false)]
    public class DeNghiListViewUser : WebPart
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/LongAn.DVC.WebParts/DeNghiListViewUser/DeNghiListViewUserUserControl.ascx";

        #region WebPart Properties

        [WebBrowsable(true),
         WebDisplayName("Phân trang"),
         WebDescription("Cấu hình phân trang"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public int PageSize { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Tiêu đề"),
         WebDescription("Nhập tiêu đề"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public string DeNghiTitle { get; set; }

        #endregion WebPart Properties

        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            if (control != null)
            {
                ((DeNghiListViewUserUserControl)control).WebPart = this;
            }
            Controls.Add(control);
        }
    }
}
