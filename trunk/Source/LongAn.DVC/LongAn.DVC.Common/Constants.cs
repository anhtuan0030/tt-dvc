using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongAn.DVC.Common
{
    public enum CapXuLy
    {
        CaNhanToChuc = 0,
        NhanVienTiepNhan = 1,
        TruongPhoPhong = 2,
        CanBoXuLy = 3,
        LanhDaoSo = 4,
        VanPhongSo = 5
    }

    public enum TrangThaiXuLy
    {
        KhoiTao,
        DaTiepNhan,
        ChoXuLy,
        DaPhanCong,
        ChoTruongPhongDuyet,
        ChoLanhDaoDuyet,
        HoSoDuocDuyet,
        HoSoBiTuChoi
        //TaoMoi,
        //ChoBoSung,
        //ChoDuyet,
        //DaDuyet,
        //TuChoi
    }

    public class Constants
    {
        //Configurations
        public const string ConfSoThuTuBienNhan = "0000000";

        public const string ConfViewStateCapXuLy = "CapXuLy";

        public const string ConfMaLinhVucSGTVT = "812";

        public const string ConfGroupNguoiDung = "DVC Người dùng";
        public const string ConfGroupNhanVienTiepNhan = "DVC Nhân viên tiếp nhận";
        public const string ConfGroupTruongPhoPhong = "DVC Trưởng phó phòng";
        public const string ConfGroupCanBoXuLy = "DVC Cán bộ xử lý";
        public const string ConfGroupLanhDaoSo = "DVC Lãnh đạo sở";

        public const string ConfPermissionDeNghi = "Add Edit List Item";
        public const string ConfPermissionDeNghiDes = "Can add, edit but not delete list item";
        //List url
        public const string ListUrlDeNghiCapPhep = "/Lists/DeNghiCapPhepXe";
        public const string ListUrlDeNghiAttachment = "/Lists/DeNghiAttachment";
        //Loại attachments
        public const string AttachmentGiayDangKy = "GiayDangKy";
        public const string AttachmentGiayChungNhanKiemDinh = "GiayChungNhanKiemDinh";
        public const string AttachmentGiayCamKet = "GiayCamKet";
        public const string AttachmentCMND = "CMND";

        public const string FieldTitle = "Title";

        //DeNghi attachments
        public const string FieldTrangThai = "TrangThai";
        public const string FieldCapDuyet = "CapDuyet";
        public const string FieldDeNghi = "DeNghi";
        public const string FieldLoaiAttachment = "LoaiAttachment";
        //De nghi
        public const string FieldDeNghiGUID = "DeNghiGUID";
        public const string FieldNamDeNghi = "NamDeNghi";
        public const string FieldSoThuTuBienNhan = "SoThuTuBienNhan";
    }
}
