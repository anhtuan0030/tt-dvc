using CamlexNET;
using LongAn.DVC.Common;
using LongAn.DVC.Helpers;
using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace LongAn.DVC.ControlTemplates.LongAn.DVC
{
    public partial class DeNghiCapPhepXeDispForm : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DeNghiHelper.LoadAttachments(SPContext.Current.ItemId, Constants.AttachmentGiayDangKy, divFileUpload1);
                DeNghiHelper.LoadAttachments(SPContext.Current.ItemId, Constants.AttachmentGiayChungNhanKiemDinh, divFileUpload2);
                DeNghiHelper.LoadAttachments(SPContext.Current.ItemId, Constants.AttachmentGiayCamKet, divFileUpload3);
                DeNghiHelper.LoadAttachments(SPContext.Current.ItemId, Constants.AttachmentCMND, divFileUpload4);
            }
        }

        
    }
}
