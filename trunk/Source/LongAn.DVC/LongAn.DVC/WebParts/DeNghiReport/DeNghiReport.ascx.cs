using CamlexNET;
using CamlexNET.Impl.Helpers;
using LongAn.DVC.Common;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq.Expressions;
using System.Web.UI.WebControls.WebParts;

namespace LongAn.DVC.WebParts.DeNghiReport
{
    [ToolboxItemAttribute(false)]
    public partial class DeNghiReport : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public DeNghiReport()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void GenerateReport(DateTime fromDate, DateTime toDate)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                    {
                        using (SPWeb web = site.OpenWeb(SPContext.Current.Web.ID))
                        {
                            #region Variable Qua Tai - QT
                            int QT_2Truc_QuocLo = 0;
                            int QT_2Truc_DuongTinh = 0;
                            int QT_2Truc_TongCong = 0;

                            int QT_3Truc_QuocLo = 0;
                            int QT_3Truc_DuongTinh = 0;
                            int QT_3Truc_TongCong = 0;

                            int QT_4Truc_QuocLo = 0;
                            int QT_4Truc_DuongTinh = 0;
                            int QT_4Truc_TongCong = 0;

                            int QT_RM3Truc_QuocLo = 0;
                            int QT_RM3Truc_DuongTinh = 0;
                            int QT_RM3Truc_TongCong = 0;

                            int QT_RM4Truc_QuocLo = 0;
                            int QT_RM4Truc_DuongTinh = 0;
                            int QT_RM4Truc_TongCong = 0;

                            int QT_RM5Truc_QuocLo = 0;
                            int QT_RM5Truc_DuongTinh = 0;
                            int QT_RM5Truc_TongCong = 0;

                            int QT_TongCong_QuocLo = 0;
                            int QT_TongCong_DuongTinh = 0;
                            int QT_TongCong_TongCong = 0;
                            #endregion Variable Qua Tai - QT

                            #region Variable Qua Kho - QK
                            int QK_2Truc_QuocLo = 0;
                            int QK_2Truc_DuongTinh = 0;
                            int QK_2Truc_TongCong = 0;

                            int QK_3Truc_QuocLo = 0;
                            int QK_3Truc_DuongTinh = 0;
                            int QK_3Truc_TongCong = 0;

                            int QK_4Truc_QuocLo = 0;
                            int QK_4Truc_DuongTinh = 0;
                            int QK_4Truc_TongCong = 0;

                            int QK_RM3Truc_QuocLo = 0;
                            int QK_RM3Truc_DuongTinh = 0;
                            int QK_RM3Truc_TongCong = 0;

                            int QK_RM4Truc_QuocLo = 0;
                            int QK_RM4Truc_DuongTinh = 0;
                            int QK_RM4Truc_TongCong = 0;

                            int QK_RM5Truc_QuocLo = 0;
                            int QK_RM5Truc_DuongTinh = 0;
                            int QK_RM5Truc_TongCong = 0;

                            int QK_TongCong_QuocLo = 0;
                            int QK_TongCong_DuongTinh = 0;
                            int QK_TongCong_TongCong = 0;
                            #endregion Variable Qua Kho - QK

                            #region Variable Qua Tai va Qua Kho - QTQK
                            int QTQK_2Truc_QuocLo = 0;
                            int QTQK_2Truc_DuongTinh = 0;
                            int QTQK_2Truc_TongCong = 0;

                            int QTQK_3Truc_QuocLo = 0;
                            int QTQK_3Truc_DuongTinh = 0;
                            int QTQK_3Truc_TongCong = 0;

                            int QTQK_4Truc_QuocLo = 0;
                            int QTQK_4Truc_DuongTinh = 0;
                            int QTQK_4Truc_TongCong = 0;

                            int QTQK_RM3Truc_QuocLo = 0;
                            int QTQK_RM3Truc_DuongTinh = 0;
                            int QTQK_RM3Truc_TongCong = 0;

                            int QTQK_RM4Truc_QuocLo = 0;
                            int QTQK_RM4Truc_DuongTinh = 0;
                            int QTQK_RM4Truc_TongCong = 0;

                            int QTQK_RM5Truc_QuocLo = 0;
                            int QTQK_RM5Truc_DuongTinh = 0;
                            int QTQK_RM5Truc_TongCong = 0;

                            int QTQK_TongCong_QuocLo = 0;
                            int QTQK_TongCong_DuongTinh = 0;
                            int QTQK_TongCong_TongCong = 0;
                            #endregion Variable Qua Tai va Qua Kho - QTQK

                            DeNghiReportModel modelQuaTai = new DeNghiReportModel();
                            DeNghiReportModel modelQuaKho = new DeNghiReportModel();
                            DeNghiReportModel modelQuaTaiQuaKho = new DeNghiReportModel();


                            string deNghiUrl = (web.ServerRelativeUrl + Constants.ListUrlDeNghiCapPhep).Replace("//", "/");
                            SPList deNghiList = web.GetList(deNghiUrl);

                            string caml = string.Empty;

                            var expressions = new List<Expression<Func<SPListItem, bool>>>();

                            var expressionsOr = new List<Expression<Func<SPListItem, bool>>>();
                            expressionsOr.Add(x => (int)x[Constants.FieldTrangThai] == (int)TrangThaiHoSo.DuocCapPhep);
                            expressionsOr.Add(x => (int)x[Constants.FieldTrangThai] == (int)TrangThaiHoSo.HoanThanh);
                            expressionsOr.Add(x => (int)x[Constants.FieldTrangThai] == (int)TrangThaiHoSo.ChuaHoanThanh);

                            //if (expressionsOr.Count > 0)
                            //{
                                var orExpr = ExpressionsHelper.CombineOr(expressionsOr);
                                expressions.Add(orExpr);
                            //}

                            var expressionsAnd = new List<Expression<Func<SPListItem, bool>>>();
                            expressionsAnd.Add(x => (DateTime)x[Constants.FieldNgayNopHoSo] >= fromDate);
                            expressionsAnd.Add(x => (DateTime)x[Constants.FieldNgayNopHoSo] < toDate.AddDays(1));

                            //if (expressionsAnd.Count > 0)
                            //{
                                var andExpr = ExpressionsHelper.CombineAnd(expressionsAnd);
                                expressions.Add(andExpr);
                            //}

                            caml = Camlex.Query().WhereAll(expressions).ToString();

                            SPQuery spQuery = new SPQuery();
                            spQuery.Query = caml;

                            SPListItemCollection items = deNghiList.GetItems(spQuery);

                            if (items != null && items.Count > 0)
                            {
                                foreach(SPListItem item in items)
                                {
                                    try
                                    {
                                        int soTrucCuaXe = 0;

                                        bool result = false;
                                        if (item[Constants.FieldSoTrucCuaXe] != null){
                                            result = int.TryParse(item[Constants.FieldSoTrucCuaXe].ToString(), out soTrucCuaXe);
                                        }

                                        LoaiCapPhep loaiCapPhep = GetLoaiCapPhep(item[Constants.FieldLoaiCapPhep]);
                                        
                                        LoaiDuong loaiDuong = GetLoaiDuong(item[Constants.FieldLoaiDuong]);

                                        if (result == true)
                                        {
                                            CalculatedReport(item, soTrucCuaXe, loaiCapPhep, loaiDuong, modelQuaTai);
                                            switch (soTrucCuaXe)
                                            {
                                                case 2:
                                                    switch (loaiCapPhep)
                                                    {
                                                        case LoaiCapPhep.QuaTai:



                                                            break;
                                                        case LoaiCapPhep.QuaKho:

                                                            break;
                                                        case LoaiCapPhep.QuaTaiVaQuaKho:

                                                            break;
                                                    }


                                                    break;
                                                case 3:
                                                    
                                                    break;
                                                case 4:
                                                    
                                                    break;
                                            }
                                        }
                                    }
                                    catch (Exception)
                                    { 
                                    
                                    }
                                }
                            }
                        }
                    }

                });
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
        }

        protected void CalculatedReport(SPListItem item, int soTrucCuaXe, LoaiCapPhep loaiCapPhep, LoaiDuong loaiDuong, DeNghiReportModel model)
        {
            
        }

        protected LoaiCapPhep GetLoaiCapPhep(object obj)
        {
            LoaiCapPhep result = LoaiCapPhep.Empty;

            if (obj != null)
            {
                if (obj.ToString() == "Quá tải")
                {
                    result = LoaiCapPhep.QuaTai;
                }
                else if (obj.ToString() == "Quá khổ")
                {
                    result = LoaiCapPhep.QuaKho;
                }
                else if (obj.ToString().Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries).Length >= 2)
                {
                    result = LoaiCapPhep.QuaTaiVaQuaKho;
                }
            }

            return result;
        }

        protected LoaiDuong GetLoaiDuong(object obj)
        {
            LoaiDuong result = LoaiDuong.Empty;

            if (obj != null)
            {
                if (obj.ToString() == "Quốc lộ")
                {
                    result = LoaiDuong.QuocLo;
                }
                else if (obj.ToString() == "Đường tỉnh")
                {
                    result = LoaiDuong.DuongTinh;
                }
                else if (obj.ToString().Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries).Length >= 2)
                {
                    result = LoaiDuong.QuocLoVaDuongTinh;
                }
            }

            return result;
        }
    }

    public class DeNghiReportModel
    {
        public int Xe2Truc_QuocLo  { get; set; }
        public int Xe2Truc_DuongTinh  { get; set; }
        public int Xe2Truc_TongCong  { get; set; }

        public int Xe3Truc_QuocLo  { get; set; }
        public int Xe3Truc_DuongTinh  { get; set; }
        public int Xe3Truc_TongCong  { get; set; }

        public int Xe4Truc_QuocLo  { get; set; }
        public int Xe4Truc_DuongTinh  { get; set; }
        public int Xe4Truc_TongCong  { get; set; }

        public int XeRM3Truc_QuocLo  { get; set; }
        public int XeRM3Truc_DuongTinh  { get; set; }
        public int XeRM3Truc_TongCong  { get; set; }

        public int XeRM4Truc_QuocLo  { get; set; }
        public int XeRM4Truc_DuongTinh  { get; set; }
        public int XeRM4Truc_TongCong  { get; set; }

        public int XeRM5Truc_QuocLo  { get; set; }
        public int XeRM5Truc_DuongTinh  { get; set; }
        public int XeRM5Truc_TongCong  { get; set; }

        public int TongCong_QuocLo  { get; set; }
        public int TongCong_DuongTinh  { get; set; }
        public int TongCong_TongCong  { get; set; }
    }
}
