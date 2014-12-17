using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongAn.DVC.Helpers
{
    [Serializable]
    public class CauHinh
    {
        public CauHinh()
        {
            BuocDuyet = "New Cấu hình - null";
        }
        public int BuocDuyetID { get; set; }
        public string BuocDuyet { get; set; }//Title
        public SPGroup SPGroup { get; set; }
        public string CapDuyetText { get; set; }
        public int TrangThai { get; set; }
        public bool IsFix { get; set; }
        public bool ActionDuyet { get; set; }
        public string TieuDeActionDuyet { get; set; }
        public int NextStep { get; set; }
        public bool ActionTraHoSo { get; set; }
        public string TieuDeActionTraHoSo { get; set; }
        public int PreviousStep { get; set; }
        public bool ActionTuChoi { get; set; }
        public bool ActionYeuCauBoSung { get; set; }
        public bool ActionPhanCong { get; set; }
        //public bool ActionCanBoTiepNhan { get; set; }
        public SPGroup SPGroupPhanCong { get; set; }
        public SPGroup SPGroupTiepNhan { get; set; }
        public bool AllowCapNhatLoaiDuong { get; set; }

        public bool AllowCapNhatNgayLuuHanh { get; set; }
        public bool AllowCapNhatNgayHen { get; set; }
        public bool IsBoSungHoSo { get; set; }
        public bool IsPhanCong { get; set; }
        //public bool IsXuLyPhanCong { get; set; }
        public string StartEnd { get; set; }
        public bool IsEmail { get; set; }
        public string EmailTemplate { get; set; }
        public bool AllowInBienNhan { get; set; }
        public bool AllowInGiayPhep { get; set; }
        public bool AllowEditDeNghi { get; set; }
        public bool AllowThamDinh { get; set; }
        public override string ToString()
        {
            return BuocDuyet;
        }
    }
}
