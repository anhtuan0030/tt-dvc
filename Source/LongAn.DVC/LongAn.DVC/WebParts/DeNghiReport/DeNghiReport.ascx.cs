using Aspose.Cells;
using CamlexNET;
using CamlexNET.Impl.Helpers;
using LongAn.DVC.Common;
using LongAn.DVC.Helpers;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq.Expressions;
using System.Web;
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
            if (!Page.IsPostBack)
            {
                //Check permission
                if (SPContext.Current.Web.CurrentUser == null)
                {
                    HttpContext.Current.Response.Redirect(SPContext.Current.Web.ServerRelativeUrl);
                }
                else
                {
                    var currentUserRole = DeNghiHelper.CurrentUserRole(SPContext.Current.Web, SPContext.Current.Web.CurrentUser);

                    if (currentUserRole == CapXuLy.CaNhanToChuc)
                    {
                        HttpContext.Current.Response.Redirect(SPContext.Current.Web.ServerRelativeUrl);
                    }
                }
            }
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
                            //int QT_2Truc_QuocLo = 0;
                            //int QT_2Truc_DuongTinh = 0;
                            //int QT_2Truc_TongCong = 0;

                            //int QT_3Truc_QuocLo = 0;
                            //int QT_3Truc_DuongTinh = 0;
                            //int QT_3Truc_TongCong = 0;

                            //int QT_4Truc_QuocLo = 0;
                            //int QT_4Truc_DuongTinh = 0;
                            //int QT_4Truc_TongCong = 0;

                            //int QT_RM3Truc_QuocLo = 0;
                            //int QT_RM3Truc_DuongTinh = 0;
                            //int QT_RM3Truc_TongCong = 0;

                            //int QT_RM4Truc_QuocLo = 0;
                            //int QT_RM4Truc_DuongTinh = 0;
                            //int QT_RM4Truc_TongCong = 0;

                            //int QT_RM5Truc_QuocLo = 0;
                            //int QT_RM5Truc_DuongTinh = 0;
                            //int QT_RM5Truc_TongCong = 0;

                            //int QT_TongCong_QuocLo = 0;
                            //int QT_TongCong_DuongTinh = 0;
                            //int QT_TongCong_TongCong = 0;
                            #endregion Variable Qua Tai - QT

                            #region Variable Qua Kho - QK
                            //int QK_2Truc_QuocLo = 0;
                            //int QK_2Truc_DuongTinh = 0;
                            //int QK_2Truc_TongCong = 0;

                            //int QK_3Truc_QuocLo = 0;
                            //int QK_3Truc_DuongTinh = 0;
                            //int QK_3Truc_TongCong = 0;

                            //int QK_4Truc_QuocLo = 0;
                            //int QK_4Truc_DuongTinh = 0;
                            //int QK_4Truc_TongCong = 0;

                            //int QK_RM3Truc_QuocLo = 0;
                            //int QK_RM3Truc_DuongTinh = 0;
                            //int QK_RM3Truc_TongCong = 0;

                            //int QK_RM4Truc_QuocLo = 0;
                            //int QK_RM4Truc_DuongTinh = 0;
                            //int QK_RM4Truc_TongCong = 0;

                            //int QK_RM5Truc_QuocLo = 0;
                            //int QK_RM5Truc_DuongTinh = 0;
                            //int QK_RM5Truc_TongCong = 0;

                            //int QK_TongCong_QuocLo = 0;
                            //int QK_TongCong_DuongTinh = 0;
                            //int QK_TongCong_TongCong = 0;
                            #endregion Variable Qua Kho - QK

                            #region Variable Qua Tai va Qua Kho - QTQK
                            //int QTQK_2Truc_QuocLo = 0;
                            //int QTQK_2Truc_DuongTinh = 0;
                            //int QTQK_2Truc_TongCong = 0;

                            //int QTQK_3Truc_QuocLo = 0;
                            //int QTQK_3Truc_DuongTinh = 0;
                            //int QTQK_3Truc_TongCong = 0;

                            //int QTQK_4Truc_QuocLo = 0;
                            //int QTQK_4Truc_DuongTinh = 0;
                            //int QTQK_4Truc_TongCong = 0;

                            //int QTQK_RM3Truc_QuocLo = 0;
                            //int QTQK_RM3Truc_DuongTinh = 0;
                            //int QTQK_RM3Truc_TongCong = 0;

                            //int QTQK_RM4Truc_QuocLo = 0;
                            //int QTQK_RM4Truc_DuongTinh = 0;
                            //int QTQK_RM4Truc_TongCong = 0;

                            //int QTQK_RM5Truc_QuocLo = 0;
                            //int QTQK_RM5Truc_DuongTinh = 0;
                            //int QTQK_RM5Truc_TongCong = 0;

                            //int QTQK_TongCong_QuocLo = 0;
                            //int QTQK_TongCong_DuongTinh = 0;
                            //int QTQK_TongCong_TongCong = 0;
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
                                        bool isAddedAlready = false;

                                        int soTrucCuaXe = 0;

                                        bool resultSoTrucCuaXe = false;
                                        if (item[Constants.FieldSoTrucCuaXe] != null){
                                            resultSoTrucCuaXe = int.TryParse(item[Constants.FieldSoTrucCuaXe].ToString(), out soTrucCuaXe);
                                        }

                                        int soTrucCuaRoMooc = 0;

                                        bool resultSoTrucCuaRoMooc = false;
                                        if (item[Constants.FieldSoTrucCuaRoMooc] != null)
                                        {
                                            resultSoTrucCuaRoMooc = int.TryParse(item[Constants.FieldSoTrucCuaRoMooc].ToString(), out soTrucCuaRoMooc);
                                        }

                                        LoaiCapPhep loaiCapPhep = GetLoaiCapPhep(item[Constants.FieldLoaiCapPhep]);
                                        
                                        LoaiDuong loaiDuong = GetLoaiDuong(item[Constants.FieldLoaiDuong]);

                                        if (resultSoTrucCuaXe == true)
                                        {
                                            switch (soTrucCuaXe)
                                            {
                                                case 2:
                                                    switch (loaiCapPhep)
                                                    {
                                                        case LoaiCapPhep.QuaTai:
                                                            switch (loaiDuong)
                                                            {
                                                                case LoaiDuong.QuocLo:
                                                                    modelQuaTai.Xe2Truc_QuocLo += 1;
                                                                    modelQuaTai.Xe2Truc_TongCong += 1;

                                                                    modelQuaTai.TongCong_QuocLo += 1;
                                                                    modelQuaTai.TongCong_TongCong += 1;
                                                                    isAddedAlready = true;
                                                                    break;
                                                                case LoaiDuong.DuongTinh:
                                                                    modelQuaTai.Xe2Truc_DuongTinh += 1;
                                                                    modelQuaTai.Xe2Truc_TongCong += 1;

                                                                    modelQuaTai.TongCong_DuongTinh += 1;
                                                                    modelQuaTai.TongCong_TongCong += 1;
                                                                    isAddedAlready = true;
                                                                    break;
                                                                case LoaiDuong.QuocLoVaDuongTinh:
                                                                    modelQuaTai.Xe2Truc_QuocLo += 1;
                                                                    modelQuaTai.Xe2Truc_DuongTinh += 1;
                                                                    modelQuaTai.Xe2Truc_TongCong += 1;

                                                                    modelQuaTai.TongCong_QuocLo += 1;
                                                                    modelQuaTai.TongCong_DuongTinh += 1;
                                                                    modelQuaTai.TongCong_TongCong += 1;
                                                                    isAddedAlready = true;
                                                                    break;
                                                            }
                                                            break;
                                                        case LoaiCapPhep.QuaKho:
                                                            switch (loaiDuong)
                                                            {
                                                                case LoaiDuong.QuocLo:
                                                                    modelQuaKho.Xe2Truc_QuocLo += 1;
                                                                    modelQuaKho.Xe2Truc_TongCong += 1;

                                                                    modelQuaKho.TongCong_QuocLo += 1;
                                                                    modelQuaKho.TongCong_TongCong += 1;
                                                                    isAddedAlready = true;
                                                                    break;
                                                                case LoaiDuong.DuongTinh:
                                                                    modelQuaKho.Xe2Truc_DuongTinh += 1;
                                                                    modelQuaKho.Xe2Truc_TongCong += 1;

                                                                    modelQuaKho.TongCong_DuongTinh += 1;
                                                                    modelQuaKho.TongCong_TongCong += 1;
                                                                    isAddedAlready = true;
                                                                    break;
                                                                case LoaiDuong.QuocLoVaDuongTinh:
                                                                    modelQuaKho.Xe2Truc_QuocLo += 1;
                                                                    modelQuaKho.Xe2Truc_DuongTinh += 1;
                                                                    modelQuaKho.Xe2Truc_TongCong += 1;

                                                                    modelQuaKho.TongCong_QuocLo += 1;
                                                                    modelQuaKho.TongCong_DuongTinh += 1;
                                                                    modelQuaKho.TongCong_TongCong += 1;
                                                                    isAddedAlready = true;
                                                                    break;
                                                            }
                                                            break;
                                                        case LoaiCapPhep.QuaTaiVaQuaKho:
                                                            switch (loaiDuong)
                                                            {
                                                                case LoaiDuong.QuocLo:
                                                                    modelQuaTaiQuaKho.Xe2Truc_QuocLo += 1;
                                                                    modelQuaTaiQuaKho.Xe2Truc_TongCong += 1;

                                                                    modelQuaTaiQuaKho.TongCong_QuocLo += 1;
                                                                    modelQuaTaiQuaKho.TongCong_TongCong += 1;
                                                                    isAddedAlready = true;
                                                                    break;
                                                                case LoaiDuong.DuongTinh:
                                                                    modelQuaTaiQuaKho.Xe2Truc_DuongTinh += 1;
                                                                    modelQuaTaiQuaKho.Xe2Truc_TongCong += 1;

                                                                    modelQuaTaiQuaKho.TongCong_DuongTinh += 1;
                                                                    modelQuaTaiQuaKho.TongCong_TongCong += 1;
                                                                    isAddedAlready = true;
                                                                    break;
                                                                case LoaiDuong.QuocLoVaDuongTinh:
                                                                    modelQuaTaiQuaKho.Xe2Truc_QuocLo += 1;
                                                                    modelQuaTaiQuaKho.Xe2Truc_DuongTinh += 1;
                                                                    modelQuaTaiQuaKho.Xe2Truc_TongCong += 1;

                                                                    modelQuaTaiQuaKho.TongCong_QuocLo += 1;
                                                                    modelQuaTaiQuaKho.TongCong_DuongTinh += 1;
                                                                    modelQuaTaiQuaKho.TongCong_TongCong += 1;
                                                                    isAddedAlready = true;
                                                                    break;
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case 3:
                                                    switch (loaiCapPhep)
                                                    {
                                                        case LoaiCapPhep.QuaTai:
                                                            switch (loaiDuong)
                                                            {
                                                                case LoaiDuong.QuocLo:
                                                                    modelQuaTai.Xe3Truc_QuocLo += 1;
                                                                    modelQuaTai.Xe3Truc_TongCong += 1;

                                                                    modelQuaTai.TongCong_QuocLo += 1;
                                                                    modelQuaTai.TongCong_TongCong += 1;
                                                                    isAddedAlready = true;
                                                                    break;
                                                                case LoaiDuong.DuongTinh:
                                                                    modelQuaTai.Xe3Truc_DuongTinh += 1;
                                                                    modelQuaTai.Xe3Truc_TongCong += 1;

                                                                    modelQuaTai.TongCong_DuongTinh += 1;
                                                                    modelQuaTai.TongCong_TongCong += 1;
                                                                    isAddedAlready = true;
                                                                    break;
                                                                case LoaiDuong.QuocLoVaDuongTinh:
                                                                    modelQuaTai.Xe3Truc_QuocLo += 1;
                                                                    modelQuaTai.Xe3Truc_DuongTinh += 1;
                                                                    modelQuaTai.Xe3Truc_TongCong += 1;

                                                                    modelQuaTai.TongCong_QuocLo += 1;
                                                                    modelQuaTai.TongCong_DuongTinh += 1;
                                                                    modelQuaTai.TongCong_TongCong += 1;
                                                                    isAddedAlready = true;
                                                                    break;
                                                            }
                                                            break;
                                                        case LoaiCapPhep.QuaKho:
                                                            switch (loaiDuong)
                                                            {
                                                                case LoaiDuong.QuocLo:
                                                                    modelQuaKho.Xe3Truc_QuocLo += 1;
                                                                    modelQuaKho.Xe3Truc_TongCong += 1;

                                                                    modelQuaKho.TongCong_QuocLo += 1;
                                                                    modelQuaKho.TongCong_TongCong += 1;
                                                                    isAddedAlready = true;
                                                                    break;
                                                                case LoaiDuong.DuongTinh:
                                                                    modelQuaKho.Xe3Truc_DuongTinh += 1;
                                                                    modelQuaKho.Xe3Truc_TongCong += 1;

                                                                    modelQuaKho.TongCong_DuongTinh += 1;
                                                                    modelQuaKho.TongCong_TongCong += 1;
                                                                    isAddedAlready = true;
                                                                    break;
                                                                case LoaiDuong.QuocLoVaDuongTinh:
                                                                    modelQuaKho.Xe3Truc_QuocLo += 1;
                                                                    modelQuaKho.Xe3Truc_DuongTinh += 1;
                                                                    modelQuaKho.Xe3Truc_TongCong += 1;

                                                                    modelQuaKho.TongCong_QuocLo += 1;
                                                                    modelQuaKho.TongCong_DuongTinh += 1;
                                                                    modelQuaKho.TongCong_TongCong += 1;
                                                                    isAddedAlready = true;
                                                                    break;
                                                            }
                                                            break;
                                                        case LoaiCapPhep.QuaTaiVaQuaKho:
                                                            switch (loaiDuong)
                                                            {
                                                                case LoaiDuong.QuocLo:
                                                                    modelQuaTaiQuaKho.Xe3Truc_QuocLo += 1;
                                                                    modelQuaTaiQuaKho.Xe3Truc_TongCong += 1;

                                                                    modelQuaTaiQuaKho.TongCong_QuocLo += 1;
                                                                    modelQuaTaiQuaKho.TongCong_TongCong += 1;
                                                                    isAddedAlready = true;
                                                                    break;
                                                                case LoaiDuong.DuongTinh:
                                                                    modelQuaTaiQuaKho.Xe3Truc_DuongTinh += 1;
                                                                    modelQuaTaiQuaKho.Xe3Truc_TongCong += 1;

                                                                    modelQuaTaiQuaKho.TongCong_DuongTinh += 1;
                                                                    modelQuaTaiQuaKho.TongCong_TongCong += 1;
                                                                    isAddedAlready = true;
                                                                    break;
                                                                case LoaiDuong.QuocLoVaDuongTinh:
                                                                    modelQuaTaiQuaKho.Xe3Truc_QuocLo += 1;
                                                                    modelQuaTaiQuaKho.Xe3Truc_DuongTinh += 1;
                                                                    modelQuaTaiQuaKho.Xe3Truc_TongCong += 1;

                                                                    modelQuaTaiQuaKho.TongCong_QuocLo += 1;
                                                                    modelQuaTaiQuaKho.TongCong_DuongTinh += 1;
                                                                    modelQuaTaiQuaKho.TongCong_TongCong += 1;
                                                                    isAddedAlready = true;
                                                                    break;
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case 4:
                                                    switch (loaiCapPhep)
                                                    {
                                                        case LoaiCapPhep.QuaTai:
                                                            switch (loaiDuong)
                                                            {
                                                                case LoaiDuong.QuocLo:
                                                                    modelQuaTai.Xe4Truc_QuocLo += 1;
                                                                    modelQuaTai.Xe4Truc_TongCong += 1;

                                                                    modelQuaTai.TongCong_QuocLo += 1;
                                                                    modelQuaTai.TongCong_TongCong += 1;
                                                                    isAddedAlready = true;
                                                                    break;
                                                                case LoaiDuong.DuongTinh:
                                                                    modelQuaTai.Xe4Truc_DuongTinh += 1;
                                                                    modelQuaTai.Xe4Truc_TongCong += 1;

                                                                    modelQuaTai.TongCong_DuongTinh += 1;
                                                                    modelQuaTai.TongCong_TongCong += 1;
                                                                    isAddedAlready = true;
                                                                    break;
                                                                case LoaiDuong.QuocLoVaDuongTinh:
                                                                    modelQuaTai.Xe4Truc_QuocLo += 1;
                                                                    modelQuaTai.Xe4Truc_DuongTinh += 1;
                                                                    modelQuaTai.Xe4Truc_TongCong += 1;

                                                                    modelQuaTai.TongCong_QuocLo += 1;
                                                                    modelQuaTai.TongCong_DuongTinh += 1;
                                                                    modelQuaTai.TongCong_TongCong += 1;
                                                                    isAddedAlready = true;
                                                                    break;
                                                            }
                                                            break;
                                                        case LoaiCapPhep.QuaKho:
                                                            switch (loaiDuong)
                                                            {
                                                                case LoaiDuong.QuocLo:
                                                                    modelQuaKho.Xe4Truc_QuocLo += 1;
                                                                    modelQuaKho.Xe4Truc_TongCong += 1;

                                                                    modelQuaKho.TongCong_QuocLo += 1;
                                                                    modelQuaKho.TongCong_TongCong += 1;
                                                                    isAddedAlready = true;
                                                                    break;
                                                                case LoaiDuong.DuongTinh:
                                                                    modelQuaKho.Xe4Truc_DuongTinh += 1;
                                                                    modelQuaKho.Xe4Truc_TongCong += 1;

                                                                    modelQuaKho.TongCong_DuongTinh += 1;
                                                                    modelQuaKho.TongCong_TongCong += 1;
                                                                    isAddedAlready = true;
                                                                    break;
                                                                case LoaiDuong.QuocLoVaDuongTinh:
                                                                    modelQuaKho.Xe4Truc_QuocLo += 1;
                                                                    modelQuaKho.Xe4Truc_DuongTinh += 1;
                                                                    modelQuaKho.Xe4Truc_TongCong += 1;

                                                                    modelQuaKho.TongCong_QuocLo += 1;
                                                                    modelQuaKho.TongCong_DuongTinh += 1;
                                                                    modelQuaKho.TongCong_TongCong += 1;
                                                                    isAddedAlready = true;
                                                                    break;
                                                            }
                                                            break;
                                                        case LoaiCapPhep.QuaTaiVaQuaKho:
                                                            switch (loaiDuong)
                                                            {
                                                                case LoaiDuong.QuocLo:
                                                                    modelQuaTaiQuaKho.Xe4Truc_QuocLo += 1;
                                                                    modelQuaTaiQuaKho.Xe4Truc_TongCong += 1;

                                                                    modelQuaTaiQuaKho.TongCong_QuocLo += 1;
                                                                    modelQuaTaiQuaKho.TongCong_TongCong += 1;
                                                                    isAddedAlready = true;
                                                                    break;
                                                                case LoaiDuong.DuongTinh:
                                                                    modelQuaTaiQuaKho.Xe4Truc_DuongTinh += 1;
                                                                    modelQuaTaiQuaKho.Xe4Truc_TongCong += 1;

                                                                    modelQuaTaiQuaKho.TongCong_DuongTinh += 1;
                                                                    modelQuaTaiQuaKho.TongCong_TongCong += 1;
                                                                    isAddedAlready = true;
                                                                    break;
                                                                case LoaiDuong.QuocLoVaDuongTinh:
                                                                    modelQuaTaiQuaKho.Xe4Truc_QuocLo += 1;
                                                                    modelQuaTaiQuaKho.Xe4Truc_DuongTinh += 1;
                                                                    modelQuaTaiQuaKho.Xe4Truc_TongCong += 1;

                                                                    modelQuaTaiQuaKho.TongCong_QuocLo += 1;
                                                                    modelQuaTaiQuaKho.TongCong_DuongTinh += 1;
                                                                    modelQuaTaiQuaKho.TongCong_TongCong += 1;
                                                                    isAddedAlready = true;
                                                                    break;
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                        }

                                        if (resultSoTrucCuaRoMooc == true)
                                        {
                                            switch (soTrucCuaRoMooc)
                                            {
                                                case 3:
                                                    switch (loaiCapPhep)
                                                    {
                                                        case LoaiCapPhep.QuaTai:
                                                            switch (loaiDuong)
                                                            {
                                                                case LoaiDuong.QuocLo:
                                                                    modelQuaTai.XeRM3Truc_QuocLo += 1;
                                                                    modelQuaTai.XeRM3Truc_TongCong += 1;

                                                                    if (isAddedAlready == false)
                                                                    {
                                                                        modelQuaTai.TongCong_QuocLo += 1;
                                                                        modelQuaTai.TongCong_TongCong += 1;
                                                                    }
                                                                    break;
                                                                case LoaiDuong.DuongTinh:
                                                                    modelQuaTai.XeRM3Truc_DuongTinh += 1;
                                                                    modelQuaTai.XeRM3Truc_TongCong += 1;

                                                                    if (isAddedAlready == false)
                                                                    {
                                                                        modelQuaTai.TongCong_DuongTinh += 1;
                                                                        modelQuaTai.TongCong_TongCong += 1;
                                                                    }
                                                                    break;
                                                                case LoaiDuong.QuocLoVaDuongTinh:
                                                                    modelQuaTai.XeRM3Truc_QuocLo += 1;
                                                                    modelQuaTai.XeRM3Truc_DuongTinh += 1;
                                                                    modelQuaTai.XeRM3Truc_TongCong += 1;

                                                                    if (isAddedAlready == false)
                                                                    {
                                                                        modelQuaTai.TongCong_QuocLo += 1;
                                                                        modelQuaTai.TongCong_DuongTinh += 1;
                                                                        modelQuaTai.TongCong_TongCong += 1;
                                                                    }
                                                                    break;
                                                            }
                                                            break;
                                                        case LoaiCapPhep.QuaKho:
                                                            switch (loaiDuong)
                                                            {
                                                                case LoaiDuong.QuocLo:
                                                                    modelQuaKho.XeRM3Truc_QuocLo += 1;
                                                                    modelQuaKho.XeRM3Truc_TongCong += 1;

                                                                    if (isAddedAlready == false)
                                                                    {
                                                                        modelQuaKho.TongCong_QuocLo += 1;
                                                                        modelQuaKho.TongCong_TongCong += 1;
                                                                    }
                                                                    break;
                                                                case LoaiDuong.DuongTinh:
                                                                    modelQuaKho.XeRM3Truc_DuongTinh += 1;
                                                                    modelQuaKho.XeRM3Truc_TongCong += 1;

                                                                    if (isAddedAlready == false)
                                                                    {
                                                                        modelQuaKho.TongCong_DuongTinh += 1;
                                                                        modelQuaKho.TongCong_TongCong += 1;
                                                                    }
                                                                    break;
                                                                case LoaiDuong.QuocLoVaDuongTinh:
                                                                    modelQuaKho.XeRM3Truc_QuocLo += 1;
                                                                    modelQuaKho.XeRM3Truc_DuongTinh += 1;
                                                                    modelQuaKho.XeRM3Truc_TongCong += 1;

                                                                    if (isAddedAlready == false)
                                                                    {
                                                                        modelQuaKho.TongCong_QuocLo += 1;
                                                                        modelQuaKho.TongCong_DuongTinh += 1;
                                                                        modelQuaKho.TongCong_TongCong += 1;
                                                                    }
                                                                    break;
                                                            }
                                                            break;
                                                        case LoaiCapPhep.QuaTaiVaQuaKho:
                                                            switch (loaiDuong)
                                                            {
                                                                case LoaiDuong.QuocLo:
                                                                    modelQuaTaiQuaKho.XeRM3Truc_QuocLo += 1;
                                                                    modelQuaTaiQuaKho.XeRM3Truc_TongCong += 1;

                                                                    if (isAddedAlready == false)
                                                                    {
                                                                        modelQuaTaiQuaKho.TongCong_QuocLo += 1;
                                                                        modelQuaTaiQuaKho.TongCong_TongCong += 1;
                                                                    }
                                                                    break;
                                                                case LoaiDuong.DuongTinh:
                                                                    modelQuaTaiQuaKho.XeRM3Truc_DuongTinh += 1;
                                                                    modelQuaTaiQuaKho.XeRM3Truc_TongCong += 1;

                                                                    if (isAddedAlready == false)
                                                                    {
                                                                        modelQuaTaiQuaKho.TongCong_DuongTinh += 1;
                                                                        modelQuaTaiQuaKho.TongCong_TongCong += 1;
                                                                    }
                                                                    break;
                                                                case LoaiDuong.QuocLoVaDuongTinh:
                                                                    modelQuaTaiQuaKho.XeRM3Truc_QuocLo += 1;
                                                                    modelQuaTaiQuaKho.XeRM3Truc_DuongTinh += 1;
                                                                    modelQuaTaiQuaKho.XeRM3Truc_TongCong += 1;

                                                                    if (isAddedAlready == false)
                                                                    {
                                                                        modelQuaTaiQuaKho.TongCong_QuocLo += 1;
                                                                        modelQuaTaiQuaKho.TongCong_DuongTinh += 1;
                                                                        modelQuaTaiQuaKho.TongCong_TongCong += 1;
                                                                    }
                                                                    break;
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case 4:
                                                    switch (loaiCapPhep)
                                                    {
                                                        case LoaiCapPhep.QuaTai:
                                                            switch (loaiDuong)
                                                            {
                                                                case LoaiDuong.QuocLo:
                                                                    modelQuaTai.XeRM4Truc_QuocLo += 1;
                                                                    modelQuaTai.XeRM4Truc_TongCong += 1;

                                                                    if (isAddedAlready == false)
                                                                    {
                                                                        modelQuaTai.TongCong_QuocLo += 1;
                                                                        modelQuaTai.TongCong_TongCong += 1;
                                                                    }
                                                                    break;
                                                                case LoaiDuong.DuongTinh:
                                                                    modelQuaTai.XeRM4Truc_DuongTinh += 1;
                                                                    modelQuaTai.XeRM4Truc_TongCong += 1;

                                                                    if (isAddedAlready == false)
                                                                    {
                                                                        modelQuaTai.TongCong_DuongTinh += 1;
                                                                        modelQuaTai.TongCong_TongCong += 1;
                                                                    }
                                                                    break;
                                                                case LoaiDuong.QuocLoVaDuongTinh:
                                                                    modelQuaTai.XeRM4Truc_QuocLo += 1;
                                                                    modelQuaTai.XeRM4Truc_DuongTinh += 1;
                                                                    modelQuaTai.XeRM4Truc_TongCong += 1;

                                                                    if (isAddedAlready == false)
                                                                    {
                                                                        modelQuaTai.TongCong_QuocLo += 1;
                                                                        modelQuaTai.TongCong_DuongTinh += 1;
                                                                        modelQuaTai.TongCong_TongCong += 1;
                                                                    }
                                                                    break;
                                                            }
                                                            break;
                                                        case LoaiCapPhep.QuaKho:
                                                            switch (loaiDuong)
                                                            {
                                                                case LoaiDuong.QuocLo:
                                                                    modelQuaKho.XeRM4Truc_QuocLo += 1;
                                                                    modelQuaKho.XeRM4Truc_TongCong += 1;

                                                                    if (isAddedAlready == false)
                                                                    {
                                                                        modelQuaKho.TongCong_QuocLo += 1;
                                                                        modelQuaKho.TongCong_TongCong += 1;
                                                                    }
                                                                    break;
                                                                case LoaiDuong.DuongTinh:
                                                                    modelQuaKho.XeRM4Truc_DuongTinh += 1;
                                                                    modelQuaKho.XeRM4Truc_TongCong += 1;

                                                                    if (isAddedAlready == false)
                                                                    {
                                                                        modelQuaKho.TongCong_DuongTinh += 1;
                                                                        modelQuaKho.TongCong_TongCong += 1;
                                                                    }
                                                                    break;
                                                                case LoaiDuong.QuocLoVaDuongTinh:
                                                                    modelQuaKho.XeRM4Truc_QuocLo += 1;
                                                                    modelQuaKho.XeRM4Truc_DuongTinh += 1;
                                                                    modelQuaKho.XeRM4Truc_TongCong += 1;
                                                                   
                                                                    if (isAddedAlready == false)
                                                                    {
                                                                        modelQuaKho.TongCong_QuocLo += 1;
                                                                        modelQuaKho.TongCong_DuongTinh += 1;
                                                                        modelQuaKho.TongCong_TongCong += 1;
                                                                    }
                                                                    break;
                                                            }
                                                            break;
                                                        case LoaiCapPhep.QuaTaiVaQuaKho:
                                                            switch (loaiDuong)
                                                            {
                                                                case LoaiDuong.QuocLo:
                                                                    modelQuaTaiQuaKho.XeRM4Truc_QuocLo += 1;
                                                                    modelQuaTaiQuaKho.XeRM4Truc_TongCong += 1;

                                                                    if (isAddedAlready == false)
                                                                    {
                                                                        modelQuaTaiQuaKho.TongCong_QuocLo += 1;
                                                                        modelQuaTaiQuaKho.TongCong_TongCong += 1;
                                                                    }
                                                                    break;
                                                                case LoaiDuong.DuongTinh:
                                                                    modelQuaTaiQuaKho.XeRM4Truc_DuongTinh += 1;
                                                                    modelQuaTaiQuaKho.XeRM4Truc_TongCong += 1;

                                                                    if (isAddedAlready == false)
                                                                    {
                                                                        modelQuaTaiQuaKho.TongCong_DuongTinh += 1;
                                                                        modelQuaTaiQuaKho.TongCong_TongCong += 1;
                                                                    }
                                                                    break;
                                                                case LoaiDuong.QuocLoVaDuongTinh:
                                                                    modelQuaTaiQuaKho.XeRM4Truc_QuocLo += 1;
                                                                    modelQuaTaiQuaKho.XeRM4Truc_DuongTinh += 1;
                                                                    modelQuaTaiQuaKho.XeRM4Truc_TongCong += 1;

                                                                    if (isAddedAlready == false)
                                                                    {
                                                                        modelQuaTaiQuaKho.TongCong_QuocLo += 1;
                                                                        modelQuaTaiQuaKho.TongCong_DuongTinh += 1;
                                                                        modelQuaTaiQuaKho.TongCong_TongCong += 1;
                                                                    }
                                                                    break;
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case 5:
                                                    switch (loaiCapPhep)
                                                    {
                                                        case LoaiCapPhep.QuaTai:
                                                            switch (loaiDuong)
                                                            {
                                                                case LoaiDuong.QuocLo:
                                                                    modelQuaTai.XeRM5Truc_QuocLo += 1;
                                                                    modelQuaTai.XeRM5Truc_TongCong += 1;

                                                                    if (isAddedAlready == false)
                                                                    {
                                                                        modelQuaTai.TongCong_QuocLo += 1;
                                                                        modelQuaTai.TongCong_TongCong += 1;
                                                                    }
                                                                    break;
                                                                case LoaiDuong.DuongTinh:
                                                                    modelQuaTai.XeRM5Truc_DuongTinh += 1;
                                                                    modelQuaTai.XeRM5Truc_TongCong += 1;

                                                                    if (isAddedAlready == false)
                                                                    {
                                                                        modelQuaTai.TongCong_DuongTinh += 1;
                                                                        modelQuaTai.TongCong_TongCong += 1;
                                                                    }
                                                                    break;
                                                                case LoaiDuong.QuocLoVaDuongTinh:
                                                                    modelQuaTai.XeRM5Truc_QuocLo += 1;
                                                                    modelQuaTai.XeRM5Truc_DuongTinh += 1;
                                                                    modelQuaTai.XeRM5Truc_TongCong += 1;

                                                                    if (isAddedAlready == false)
                                                                    {
                                                                        modelQuaTai.TongCong_QuocLo += 1;
                                                                        modelQuaTai.TongCong_DuongTinh += 1;
                                                                        modelQuaTai.TongCong_TongCong += 1;
                                                                    }
                                                                    break;
                                                            }
                                                            break;
                                                        case LoaiCapPhep.QuaKho:
                                                            switch (loaiDuong)
                                                            {
                                                                case LoaiDuong.QuocLo:
                                                                    modelQuaKho.XeRM5Truc_QuocLo += 1;
                                                                    modelQuaKho.XeRM5Truc_TongCong += 1;

                                                                    if (isAddedAlready == false)
                                                                    {
                                                                        modelQuaKho.TongCong_QuocLo += 1;
                                                                        modelQuaKho.TongCong_TongCong += 1;
                                                                    }
                                                                    break;
                                                                case LoaiDuong.DuongTinh:
                                                                    modelQuaKho.XeRM5Truc_DuongTinh += 1;
                                                                    modelQuaKho.XeRM5Truc_TongCong += 1;

                                                                    if (isAddedAlready == false)
                                                                    {
                                                                        modelQuaKho.TongCong_DuongTinh += 1;
                                                                        modelQuaKho.TongCong_TongCong += 1;
                                                                    }
                                                                    break;
                                                                case LoaiDuong.QuocLoVaDuongTinh:
                                                                    modelQuaKho.XeRM5Truc_QuocLo += 1;
                                                                    modelQuaKho.XeRM5Truc_DuongTinh += 1;
                                                                    modelQuaKho.XeRM5Truc_TongCong += 1;

                                                                    if (isAddedAlready == false)
                                                                    {
                                                                        modelQuaKho.TongCong_QuocLo += 1;
                                                                        modelQuaKho.TongCong_DuongTinh += 1;
                                                                        modelQuaKho.TongCong_TongCong += 1;
                                                                    }
                                                                    break;
                                                            }
                                                            break;
                                                        case LoaiCapPhep.QuaTaiVaQuaKho:
                                                            switch (loaiDuong)
                                                            {
                                                                case LoaiDuong.QuocLo:
                                                                    modelQuaTaiQuaKho.XeRM5Truc_QuocLo += 1;
                                                                    modelQuaTaiQuaKho.XeRM5Truc_TongCong += 1;

                                                                    if (isAddedAlready == false)
                                                                    {
                                                                        modelQuaTaiQuaKho.TongCong_QuocLo += 1;
                                                                        modelQuaTaiQuaKho.TongCong_TongCong += 1;
                                                                    }
                                                                    break;
                                                                case LoaiDuong.DuongTinh:
                                                                    modelQuaTaiQuaKho.XeRM5Truc_DuongTinh += 1;
                                                                    modelQuaTaiQuaKho.XeRM5Truc_TongCong += 1;

                                                                    if (isAddedAlready == false)
                                                                    {
                                                                        modelQuaTaiQuaKho.TongCong_DuongTinh += 1;
                                                                        modelQuaTaiQuaKho.TongCong_TongCong += 1;
                                                                    }
                                                                    break;
                                                                case LoaiDuong.QuocLoVaDuongTinh:
                                                                    modelQuaTaiQuaKho.XeRM5Truc_QuocLo += 1;
                                                                    modelQuaTaiQuaKho.XeRM5Truc_DuongTinh += 1;
                                                                    modelQuaTaiQuaKho.XeRM5Truc_TongCong += 1;

                                                                    if (isAddedAlready == false)
                                                                    {
                                                                        modelQuaTaiQuaKho.TongCong_QuocLo += 1;
                                                                        modelQuaTaiQuaKho.TongCong_DuongTinh += 1;
                                                                        modelQuaTaiQuaKho.TongCong_TongCong += 1;
                                                                    }
                                                                    break;
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        LoggingServices.LogException(ex);
                                    }
                                }
                            }

                            #region [Export Excel]
                            try
                            {
                                string licenseFile = SPUtility.GetVersionedGenericSetupPath(Constants.AsposeCellLicPath, 15);
                                //Set license
                                Aspose.Cells.License l = new Aspose.Cells.License();
                                l.SetLicense(licenseFile);
                                WorkbookDesigner designer = new WorkbookDesigner();
                                designer.Workbook = new Workbook(SPUtility.GetVersionedGenericSetupPath(Constants.DeNghiReportPath, 15));

                                string currentDate = string.Format("Long An, ngày {0} tháng {1} năm {2}", DateTime.Now.ToString("dd"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("yyyy"));
                                string fromDateToDate = string.Format("Thời gian báo cáo: từ {0} đến {1}", fromDate.ToString("dd/MM/yyyy"), toDate.ToString("dd/MM/yyyy"));

                                designer.SetDataSource("CurrentDate", currentDate);
                                designer.SetDataSource("FromDateToDate", fromDateToDate);
                                designer.SetDataSource("QuaTai", modelQuaTai);
                                designer.SetDataSource("QuaKho", modelQuaKho);
                                designer.SetDataSource("QuaTaiQuaKho", modelQuaTaiQuaKho);
                                designer.Process();

                                string exportName = "BaoCaoCapPhepLuuHanhXe" + DateTime.Now.ToString("_yyyyMMdd") + ".xls";
                                MemoryStream ms = new MemoryStream();
                                designer.Workbook.Save(ms, FileFormatType.Excel97To2003);
                                //Response to client
                                this.Page.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                this.Page.Response.AppendHeader("Content-Disposition", "attachment; filename=" + exportName);
                                this.Page.Response.Flush();
                                this.Page.Response.BinaryWrite(ms.ToArray());
                                this.Page.Response.End();
                            }
                            catch (Exception ex)
                            {
                                LoggingServices.LogException(ex);
                            }
                            #endregion [Export Excel]

                        }
                    }

                });
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
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

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (!dtcFromDate.IsDateEmpty && dtcFromDate.IsValid && !dtcToDate.IsDateEmpty && dtcToDate.IsValid)
            {
                GenerateReport(dtcFromDate.SelectedDate, dtcToDate.SelectedDate);
            }
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

        public DeNghiReportModel()
        {
            Xe2Truc_QuocLo  = 0;
            Xe2Truc_DuongTinh  = 0;
            Xe2Truc_TongCong  = 0;
            
            Xe3Truc_QuocLo  = 0;
            Xe3Truc_DuongTinh  = 0;
            Xe3Truc_TongCong  = 0;
            
            Xe4Truc_QuocLo  = 0;
            Xe4Truc_DuongTinh  = 0;
            Xe4Truc_TongCong  = 0;
            
            XeRM3Truc_QuocLo  = 0;
            XeRM3Truc_DuongTinh  = 0;
            XeRM3Truc_TongCong  = 0;
            
            XeRM4Truc_QuocLo  = 0;
            XeRM4Truc_DuongTinh  = 0;
            XeRM4Truc_TongCong  = 0;
            
            XeRM5Truc_QuocLo  = 0;
            XeRM5Truc_DuongTinh  = 0;
            XeRM5Truc_TongCong  = 0;
            
            TongCong_QuocLo  = 0;
            TongCong_DuongTinh  = 0;
            TongCong_TongCong  = 0;
        }
    }
}
