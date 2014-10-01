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
        MotCua = 1,
        TruongPhoPhong = 2,
        CanBo = 3,
        LanhDaoSo = 4,
        VanPhongSo = 5
    }

    public enum HanhDong
    {
        NopHoSo,
        TiepNhanHoSo,
        ChuyenTruongPhoPhong,
        TiepNhanXuLy,
        TrinhTruongPhoPhong,
        PhanCongHoSo,
        YeuCauBoSung,
        TrinhLanhDaoSo,
        DuyetCapPhep,
        XacNhanHoanThanh,
        XacNhanChuaHoanThanh,
        TuChoiHoSo

    }

    public enum TrangThaiHoSo
    {
        KhoiTao,
        ChoTiepNhan,
        DaTiepNhan,
        ChoXuLy,
        DangXuLy,
        ChoBoSung,
        ChoDuyet,
        ChoCapPhep,
        DuocCapPhep,
        BiTuChoi,
        HoanThanh,
        ChuaHoanThanh
    }

    public enum PrintType
    {
        PhieuBienNhan,
        GiayCapPhep
    }

    public enum LoaiDuong
    {
        Empty,
        QuocLo,
        DuongTinh,
        QuocLoVaDuongTinh
    }

    public enum LoaiCapPhep
    {   
        Empty,
        QuaTai,
        QuaKho,
        QuaTaiVaQuaKho
    }

    public class Constants
    {
        //Configurations
        public const string ConfSoThuTuBienNhan = "0000000";

        public const string ConfQueryStringBS = "4709-95d5-994c89d0c201";
        public const string ConfQueryStringPC = "58c7-da84-764cded0c311";
        public const string ConfQueryStringTC = "7e98-9a4f-7c4d3dh5c3g1";
        public const string ConfQueryStringTNXL = "7fec-1g4c-2j91bf9xf9h";
        public const string ConfActionBS = "Abshs";
        public const string ConfActionPC = "Apchs";
        public const string ConfActionTC = "Atchs";
        public const string ConfActionTNXL = "Atnxl";

        public const string ConfViewStateCapXuLy = "CapXuLy";
        public const string ConfViewStateDeNghiListId = "DeNghiListId";

        public const string ConfMaLinhVucSGTVT = "812";

        public const string ConfGroupNguoiDung = "DVC Người dùng";
        public const string ConfGroupNhanVienTiepNhan = "DVC Nhân viên tiếp nhận";
        public const string ConfGroupTruongPhoPhong = "DVC Trưởng phó phòng";
        public const string ConfGroupCanBoXuLy = "DVC Cán bộ xử lý";
        public const string ConfGroupLanhDaoSo = "DVC Lãnh đạo sở";

        public const string ConfWordLicFile = "TEMPLATE\\LAYOUTS\\LongAn.DVC\\lic\\Aspose.Words.lic";
        public const string ConfWordBienNhanTemplate = "TEMPLATE\\LAYOUTS\\LongAn.DVC\\templates\\BienNhanTemplate.docx";
        public const string ConfWordGiayPhepTemplate = "TEMPLATE\\LAYOUTS\\LongAn.DVC\\templates\\GiayPhepTemplate.docx";

        public const string ConfLinkDispForm = "javascript:OpenPopUpPage('{0}/DispForm.aspx?ID={1}&Source={2}');return false;";
        public const string ConfLinkEditForm = "javascript:OpenPopUpPage('{0}/EditForm.aspx?ID={1}&Source={2}');return false;";
        public const string ConfLinkNewForm = "javascript:OpenPopUpPage('{0}/NewForm.aspx?Source={1}');return false;";
        public const string ConfLinkPageDispForm = "{0}/DispForm.aspx?ID={1}&Source={2}";
        public const string ConfLinkPageEditForm = "{0}/EditForm.aspx?ID={1}&Source={2}";
        public const string ConfLinkPageNewForm = "{0}/NewForm.aspx?Source={1}";

        public const string ConfLinkTuChoiPage = "{0}/_Layouts/LongAn.DVC/TuChoiHoSo.aspx?List={1}&ID={2}&Source={3}&Action=Atchs&Atocken=7e98-9a4f-7c4d3dh5c3g1";
        public const string ConfLinkPhanCongPage = "{0}/_Layouts/LongAn.DVC/PhanCongHoSo.aspx?List={1}&ID={2}&Source={3}&Action=Apchs&Atocken=58c7-da84-764cded0c311";
        public const string ConfLinkBoSungPage = "{0}/_Layouts/LongAn.DVC/YeuCauBoSung.aspx?List={1}&ID={2}&Source={3}&Action=Abshs&Atocken=4709-95d5-994c89d0c201";
        
        public const string ConfPermissionDeNghi = "Add Edit List Item";
        public const string ConfPermissionDeNghiDes = "Can add, edit but not delete list item";
        //List url
        public const string ListUrlDeNghiCapPhep = "/Lists/DeNghiCapPhepXe";
        public const string ListUrlDeNghiAttachment = "/Lists/DeNghiAttachment";
        public const string ListUrlYeuCauBoSung = "/Lists/YeuCauBoSung";
        public const string ListUrlLichSuCapPhep = "/Lists/LichSuCapPhep";
        //Loại attachments
        public const string AttachmentGiayDangKy = "GiayDangKy";
        public const string AttachmentGiayChungNhanKiemDinh = "GiayChungNhanKiemDinh";
        public const string AttachmentGiayCamKet = "GiayCamKet";
        public const string AttachmentCMND = "CMND";

        public const string FieldTitle = "Title";
        public const string FieldCreated = "Created";

        //Aspose lic path
        public const string AsposeCellLicPath = "TEMPLATE\\LAYOUTS\\LongAn.DVC\\lic\\Aspose.Cells.lic";
        public const string AsposeWordLicPath = "TEMPLATE\\LAYOUTS\\LongAn.DVC\\lic\\Aspose.Words.lic";

        //Excel Template Path
        public const string DeNghiReportPath = "TEMPLATE\\LAYOUTS\\LongAn.DVC\\ExcelTemplates\\BaoCaoCapPhepLuuHanhXe.xls";

        //DeNghi attachments
        public const string FieldNguoiDeNghi = "NguoiDeNghi";
        public const string FieldLoaiDeNghi = "LoaiDeNghi";
        public const string FieldTrangThai = "TrangThai";
        public const string FieldTrangThaiText = "TrangThaiText";
        public const string FieldCapDuyet = "CapDuyet";
        public const string FieldDeNghi = "DeNghi";
        public const string FieldLoaiAttachment = "LoaiAttachment";
        //De nghi
        public const string FieldCaNhanToChuc = "CaNhanToChuc";
        public const string FieldSoDienThoai = "SoDienThoai";
        public const string FieldMotCuaUser = "MotCuaUser";
        public const string FieldTruongPhongUser = "TruongPhongUser";
        public const string FieldCanBoUser = "CanBoUser";
        public const string FieldLanhDaoUser = "LanhDaoUser";

        public const string FieldNgayNopHoSo = "NgayNopHoSo";
        public const string FieldNgayXuLy = "NgayXuLy";
        public const string FieldNguoiXuLy = "NguoiXuLy";
        public const string FieldHanhDong = "HanhDong";

        public const string FieldLyDoTuChoi = "LyDoTuChoi";

        public const string FieldDeNghiGUID = "DeNghiGUID";
        public const string FieldNamDeNghi = "NamDeNghi";
        public const string FieldSoThuTuBienNhan = "SoThuTuBienNhan";
        public const string FieldMoTa = "MoTa";

        public const string FieldLoaiDuong = "LoaiDuong"; //Quốc lộ, Đường tỉnh
        public const string FieldIdLoaiDuong = "{0C841B02-EC0F-45BA-8E39-F27A6B70AF02}";
        public const string FieldLanXeDuocChay = "LanXeDuocChay";
        public const string FieldTocDoDuocChay = "TocDoDuocChay";

        public const string FieldLoaiCapPhep = "LoaiCapPhep"; //Quá tải, Quá khổ
        public const string FieldSoTrucCuaXe = "SoTrucCuaXe";
        public const string FieldSoTrucCuaRoMooc = "SoTrucCuaRoMooc";

        public const string EmailBody = @"Dear {0}, <br>Hồ sơ có mã biên nhận: {1} đã/phải {2}.<br>Link: {3}";
    }
}
