using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace LongAn.DVC.WebParts.DeNghiListViewTotal
{
    [ToolboxItemAttribute(false)]
    public class DeNghiListViewTotal : WebPart
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/LongAn.DVC.WebParts/DeNghiListViewTotal/DeNghiListViewTotalUserControl.ascx";

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
        public LongAn.DVC.Common.DeNghiOption Option { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Trạng thái hồ sơ"),
         WebDescription("Nhập mã trạng thái (cách nhau bằng ';')"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public string DeNghiTrangThai { get; set; }

        #endregion WebPart Properties
        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            if (control!= null)
            {
                ((DeNghiListViewTotalUserControl)control).WebPart = this;
            }
            Controls.Add(control);
            
        }
    }
}
