using LongAn.DVC.Helpers;
using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using System.Web;
using Microsoft.SharePoint.Utilities;

namespace LongAn.DVC.WebParts.DeNghiRedirect
{
    [ToolboxItemAttribute(false)]
    public partial class DeNghiRedirect : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public DeNghiRedirect()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.QueryString["ControlMode"] == null)
            {
                var currentUserRole = DeNghiHelper.CurrentUserRole(SPContext.Current.Web, SPContext.Current.Web.CurrentUser);
                switch (currentUserRole)
                {
                    case LongAn.DVC.Common.CapXuLy.CaNhanToChuc:
                        //SPUtility.Redirect(LinkHoSoDeNghi, SPRedirectFlags.Default, HttpContext.Current);
                        HttpContext.Current.Response.Redirect(LinkHoSoDeNghi);
                        break;
                    case LongAn.DVC.Common.CapXuLy.NhanVienTiepNhan:
                        //SPUtility.Redirect(LinkHoSoDaTiepNhan, SPRedirectFlags.Default, HttpContext.Current);
                        HttpContext.Current.Response.Redirect(LinkHoSoDaTiepNhan);
                        break;
                    case LongAn.DVC.Common.CapXuLy.TruongPhoPhong:
                        //SPUtility.Redirect(LinkHoSoChoDuyet, SPRedirectFlags.Default, HttpContext.Current);
                        HttpContext.Current.Response.Redirect(LinkHoSoChoDuyet);
                        break;
                    case LongAn.DVC.Common.CapXuLy.CanBoXuLy:
                        //SPUtility.Redirect(LinkHoSoChoPhanCong, SPRedirectFlags.Default, HttpContext.Current);
                        HttpContext.Current.Response.Redirect(LinkHoSoChoPhanCong);
                        break;
                    case LongAn.DVC.Common.CapXuLy.LanhDaoSo:
                        //SPUtility.Redirect(LinkHoSoChoCapPhep, SPRedirectFlags.Default, HttpContext.Current);
                        HttpContext.Current.Response.Redirect(LinkHoSoChoCapPhep);
                        break;
                    case LongAn.DVC.Common.CapXuLy.VanPhongSo:
                        //SPUtility.Redirect(LinkHoSoDaCapPhep, SPRedirectFlags.Default, HttpContext.Current);
                        HttpContext.Current.Response.Redirect(LinkHoSoDaCapPhep);
                        break;
                    default:
                        HttpContext.Current.Response.Redirect("/");
                        break;
                }
            }
        }

        #region WebPart Properties
        [WebBrowsable(true),
         WebDisplayName("Link Danh sách đề nghị"),
         WebDescription("This Accepts text Input"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public string LinkHoSoDeNghi { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Link Hồ sơ đã tiếp nhận"),
         WebDescription("Một cửa đang tiếp nhận"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public string LinkHoSoDaTiepNhan { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Link Hồ sơ chờ xử lý"),
         WebDescription("Chờ Trưởng phó phòng phân công / Cán bộ xử lý"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public string LinkHoSoChoPhanCong { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Link Hồ sơ chờ duyệt"),
         WebDescription("Trình trưởng phó phòng QLHT"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public string LinkHoSoChoDuyet { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Link Hồ sơ chờ cấp phép"),
         WebDescription("Trình lãnh đạo sở"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public string LinkHoSoChoCapPhep { get; set; }

        [WebBrowsable(true),
         WebDisplayName("Link Hồ sơ đã cấp phép"),
         WebDescription("Hồ sơ đã duyệt"),
         Personalizable(PersonalizationScope.Shared),
         Category("LongAn.DVC")]
        public string LinkHoSoDaCapPhep { get; set; }

        #endregion WebPart Properties
    }
}
