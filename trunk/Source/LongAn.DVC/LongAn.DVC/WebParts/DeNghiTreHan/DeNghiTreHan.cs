using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace LongAn.DVC.WebParts.DeNghiTreHan
{
    [ToolboxItemAttribute(false)]
    public class DeNghiTreHan : WebPart
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/LongAn.DVC.WebParts/DeNghiTreHan/DeNghiTreHanUserControl.ascx";

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

        [WebBrowsable(true),
         WebDisplayName("Option"),
         WebDescription("Lựa chọn"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public LongAn.DVC.Common.DeNghiTreHanOption Option { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Số ngày"),
         WebDescription("Số ngày để tính trễ hạn, sắp trễ hạn"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public int NumDay { get; set; }
        #endregion

        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            if (control != null)
            {
                ((DeNghiTreHanUserControl)control).WebPart = this;
            }
            Controls.Add(control);
        }
    }
}
