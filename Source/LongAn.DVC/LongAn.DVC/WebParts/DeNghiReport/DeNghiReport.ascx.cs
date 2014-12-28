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
using System.Globalization;
using System.IO;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Linq;

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
                //else
                //{
                //    var currentUserRole = DeNghiHelper.CurrentUserRole(SPContext.Current.Web, SPContext.Current.Web.CurrentUser);

                //    if (currentUserRole == CapXuLy.CaNhanToChuc)
                //    {
                //        HttpContext.Current.Response.Redirect(SPContext.Current.Web.ServerRelativeUrl);
                //    }
                //}

                DateTime firstDateOfQuarter = new DateTime(2014, 10, 1); //the first day of Q4 2014
                DateTime currentDate = DateTime.Today;

                do
                {
                    ddlQuarterPicker.Items.Add(new ListItem { Text = "Quý " + GetQuarter(firstDateOfQuarter).ToString() + " năm " + firstDateOfQuarter.Year.ToString(), Value = firstDateOfQuarter.ToString("yyyy-MM-dd") });
                    firstDateOfQuarter = firstDateOfQuarter.AddMonths(3);
                }
                while (currentDate > firstDateOfQuarter);
            }
        }

        protected int GetQuarter(DateTime date)
        {
            if (date.Month >= 1 && date.Month <= 3)
                return 1;
            else if (date.Month >= 4 && date.Month <= 6)
                return 2;
            else if (date.Month >= 7 && date.Month <= 9)
                return 3;
            else
                return 4;
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

                            //var expressionsOr = new List<Expression<Func<SPListItem, bool>>>();
                            //expressionsOr.Add(x => (string)x[Constants.FieldTrangThai] == ((int)TrangThaiHoSo.DuocCapPhep).ToString());
                            //expressionsOr.Add(x => (string)x[Constants.FieldTrangThai] == ((int)TrangThaiHoSo.HoanThanh).ToString());
                            //expressionsOr.Add(x => (string)x[Constants.FieldTrangThai] == ((int)TrangThaiHoSo.ChuaHoanThanh).ToString());

                            //if (expressionsOr.Count > 0)
                            //{
                            //var orExpr = ExpressionsHelper.CombineOr(expressionsOr);
                            //expressions.Add(orExpr);
                            //}

                            var expressionsAnd = new List<Expression<Func<SPListItem, bool>>>();
                            //expressionsAnd.Add(x => x[Fields.NgayDuocCapPhep] != null);
                            expressionsAnd.Add(x => (DateTime)x[Fields.NgayDuocCapPhep] >= fromDate);
                            expressionsAnd.Add(x => (DateTime)x[Fields.NgayDuocCapPhep] < toDate.AddDays(1));

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

                                //designer.SetDataSource("QuaTai", modelQuaTai);
                                designer.SetDataSource("QuaTai_Xe2Truc_QuocLo", modelQuaTai.Xe2Truc_QuocLo);
                                designer.SetDataSource("QuaTai_Xe2Truc_DuongTinh", modelQuaTai.Xe2Truc_DuongTinh);
                                designer.SetDataSource("QuaTai_Xe2Truc_TongCong", modelQuaTai.Xe2Truc_TongCong);
                                designer.SetDataSource("QuaTai_Xe3Truc_QuocLo", modelQuaTai.Xe3Truc_QuocLo);
                                designer.SetDataSource("QuaTai_Xe3Truc_DuongTinh", modelQuaTai.Xe3Truc_DuongTinh);
                                designer.SetDataSource("QuaTai_Xe3Truc_TongCong", modelQuaTai.Xe3Truc_TongCong);
                                designer.SetDataSource("QuaTai_Xe4Truc_QuocLo", modelQuaTai.Xe4Truc_QuocLo);
                                designer.SetDataSource("QuaTai_Xe4Truc_DuongTinh", modelQuaTai.Xe4Truc_DuongTinh);
                                designer.SetDataSource("QuaTai_Xe4Truc_TongCong", modelQuaTai.Xe4Truc_TongCong);
                                designer.SetDataSource("QuaTai_XeRM3Truc_QuocLo", modelQuaTai.XeRM3Truc_QuocLo);
                                designer.SetDataSource("QuaTai_XeRM3Truc_DuongTinh", modelQuaTai.XeRM3Truc_DuongTinh);
                                designer.SetDataSource("QuaTai_XeRM3Truc_TongCong", modelQuaTai.XeRM3Truc_TongCong);
                                designer.SetDataSource("QuaTai_XeRM4Truc_QuocLo", modelQuaTai.XeRM4Truc_QuocLo);
                                designer.SetDataSource("QuaTai_XeRM4Truc_DuongTinh", modelQuaTai.XeRM4Truc_DuongTinh);
                                designer.SetDataSource("QuaTai_XeRM4Truc_TongCong", modelQuaTai.XeRM4Truc_TongCong);
                                designer.SetDataSource("QuaTai_XeRM5Truc_QuocLo", modelQuaTai.XeRM5Truc_QuocLo);
                                designer.SetDataSource("QuaTai_XeRM5Truc_DuongTinh", modelQuaTai.XeRM5Truc_DuongTinh);
                                designer.SetDataSource("QuaTai_XeRM5Truc_TongCong", modelQuaTai.XeRM5Truc_TongCong);
                                designer.SetDataSource("QuaTai_TongCong_QuocLo", modelQuaTai.TongCong_QuocLo);
                                designer.SetDataSource("QuaTai_TongCong_DuongTinh", modelQuaTai.TongCong_DuongTinh);
                                designer.SetDataSource("QuaTai_TongCong_TongCong", modelQuaTai.TongCong_TongCong);


                                //designer.SetDataSource("QuaKho", modelQuaKho);
                                designer.SetDataSource("QuaKho_Xe2Truc_QuocLo", modelQuaKho.Xe2Truc_QuocLo);
                                designer.SetDataSource("QuaKho_Xe2Truc_DuongTinh", modelQuaKho.Xe2Truc_DuongTinh);
                                designer.SetDataSource("QuaKho_Xe2Truc_TongCong", modelQuaKho.Xe2Truc_TongCong);
                                designer.SetDataSource("QuaKho_Xe3Truc_QuocLo", modelQuaKho.Xe3Truc_QuocLo);
                                designer.SetDataSource("QuaKho_Xe3Truc_DuongTinh", modelQuaKho.Xe3Truc_DuongTinh);
                                designer.SetDataSource("QuaKho_Xe3Truc_TongCong", modelQuaKho.Xe3Truc_TongCong);
                                designer.SetDataSource("QuaKho_Xe4Truc_QuocLo", modelQuaKho.Xe4Truc_QuocLo);
                                designer.SetDataSource("QuaKho_Xe4Truc_DuongTinh", modelQuaKho.Xe4Truc_DuongTinh);
                                designer.SetDataSource("QuaKho_Xe4Truc_TongCong", modelQuaKho.Xe4Truc_TongCong);
                                designer.SetDataSource("QuaKho_XeRM3Truc_QuocLo", modelQuaKho.XeRM3Truc_QuocLo);
                                designer.SetDataSource("QuaKho_XeRM3Truc_DuongTinh", modelQuaKho.XeRM3Truc_DuongTinh);
                                designer.SetDataSource("QuaKho_XeRM3Truc_TongCong", modelQuaKho.XeRM3Truc_TongCong);
                                designer.SetDataSource("QuaKho_XeRM4Truc_QuocLo", modelQuaKho.XeRM4Truc_QuocLo);
                                designer.SetDataSource("QuaKho_XeRM4Truc_DuongTinh", modelQuaKho.XeRM4Truc_DuongTinh);
                                designer.SetDataSource("QuaKho_XeRM4Truc_TongCong", modelQuaKho.XeRM4Truc_TongCong);
                                designer.SetDataSource("QuaKho_XeRM5Truc_QuocLo", modelQuaKho.XeRM5Truc_QuocLo);
                                designer.SetDataSource("QuaKho_XeRM5Truc_DuongTinh", modelQuaKho.XeRM5Truc_DuongTinh);
                                designer.SetDataSource("QuaKho_XeRM5Truc_TongCong", modelQuaKho.XeRM5Truc_TongCong);
                                designer.SetDataSource("QuaKho_TongCong_QuocLo", modelQuaKho.TongCong_QuocLo);
                                designer.SetDataSource("QuaKho_TongCong_DuongTinh", modelQuaKho.TongCong_DuongTinh);
                                designer.SetDataSource("QuaKho_TongCong_TongCong", modelQuaKho.TongCong_TongCong);


                                //designer.SetDataSource("QuaTaiQuaKho", modelQuaTaiQuaKho);
                                designer.SetDataSource("QuaTaiQuaKho_Xe2Truc_QuocLo", modelQuaTaiQuaKho.Xe2Truc_QuocLo);
                                designer.SetDataSource("QuaTaiQuaKho_Xe2Truc_DuongTinh", modelQuaTaiQuaKho.Xe2Truc_DuongTinh);
                                designer.SetDataSource("QuaTaiQuaKho_Xe2Truc_TongCong", modelQuaTaiQuaKho.Xe2Truc_TongCong);
                                designer.SetDataSource("QuaTaiQuaKho_Xe3Truc_QuocLo", modelQuaTaiQuaKho.Xe3Truc_QuocLo);
                                designer.SetDataSource("QuaTaiQuaKho_Xe3Truc_DuongTinh", modelQuaTaiQuaKho.Xe3Truc_DuongTinh);
                                designer.SetDataSource("QuaTaiQuaKho_Xe3Truc_TongCong", modelQuaTaiQuaKho.Xe3Truc_TongCong);
                                designer.SetDataSource("QuaTaiQuaKho_Xe4Truc_QuocLo", modelQuaTaiQuaKho.Xe4Truc_QuocLo);
                                designer.SetDataSource("QuaTaiQuaKho_Xe4Truc_DuongTinh", modelQuaTaiQuaKho.Xe4Truc_DuongTinh);
                                designer.SetDataSource("QuaTaiQuaKho_Xe4Truc_TongCong", modelQuaTaiQuaKho.Xe4Truc_TongCong);
                                designer.SetDataSource("QuaTaiQuaKho_XeRM3Truc_QuocLo", modelQuaTaiQuaKho.XeRM3Truc_QuocLo);
                                designer.SetDataSource("QuaTaiQuaKho_XeRM3Truc_DuongTinh", modelQuaTaiQuaKho.XeRM3Truc_DuongTinh);
                                designer.SetDataSource("QuaTaiQuaKho_XeRM3Truc_TongCong", modelQuaTaiQuaKho.XeRM3Truc_TongCong);
                                designer.SetDataSource("QuaTaiQuaKho_XeRM4Truc_QuocLo", modelQuaTaiQuaKho.XeRM4Truc_QuocLo);
                                designer.SetDataSource("QuaTaiQuaKho_XeRM4Truc_DuongTinh", modelQuaTaiQuaKho.XeRM4Truc_DuongTinh);
                                designer.SetDataSource("QuaTaiQuaKho_XeRM4Truc_TongCong", modelQuaTaiQuaKho.XeRM4Truc_TongCong);
                                designer.SetDataSource("QuaTaiQuaKho_XeRM5Truc_QuocLo", modelQuaTaiQuaKho.XeRM5Truc_QuocLo);
                                designer.SetDataSource("QuaTaiQuaKho_XeRM5Truc_DuongTinh", modelQuaTaiQuaKho.XeRM5Truc_DuongTinh);
                                designer.SetDataSource("QuaTaiQuaKho_XeRM5Truc_TongCong", modelQuaTaiQuaKho.XeRM5Truc_TongCong);
                                designer.SetDataSource("QuaTaiQuaKho_TongCong_QuocLo", modelQuaTaiQuaKho.TongCong_QuocLo);
                                designer.SetDataSource("QuaTaiQuaKho_TongCong_DuongTinh", modelQuaTaiQuaKho.TongCong_DuongTinh);
                                designer.SetDataSource("QuaTaiQuaKho_TongCong_TongCong", modelQuaTaiQuaKho.TongCong_TongCong);



                                designer.Process();

                                string exportName = "BaoCaoCapPhepLuuHanhXe" + DateTime.Now.ToString("_yyyyMMdd") + ".xls";
                                MemoryStream ms = new MemoryStream();
                                designer.Workbook.Save(ms, FileFormatType.Excel97To2003);
                                //Response to client
                                this.Page.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                this.Page.Response.AppendHeader("Content-Disposition", "attachment; filename=" + exportName);
                                this.Page.Response.Flush();
                                this.Page.Response.BinaryWrite(ms.ToArray());
                                //this.Page.Response.End();
                                HttpContext.Current.ApplicationInstance.CompleteRequest();
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
                var arrValues = obj.ToString().Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries);

                if (obj.ToString().Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries).Length >= 2)
                {
                    result = LoaiCapPhep.QuaTaiVaQuaKho;
                }
                else if (arrValues[0] == "Quá tải")
                {
                    result = LoaiCapPhep.QuaTai;
                }
                else if (arrValues[0] == "Quá khổ")
                {
                    result = LoaiCapPhep.QuaKho;
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

        protected void btnExportExcelQuarter_Click(object sender, EventArgs e)
        {
            DateTime fromDate = DateTime.ParseExact(ddlQuarterPicker.SelectedValue, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime toDate = fromDate.AddMonths(3).AddDays(-1);
            GenerateReportByQuarter(fromDate, toDate);
        }

        protected void GenerateReportByQuarter(DateTime fromDate, DateTime toDate)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                    {
                        using (SPWeb web = site.OpenWeb(SPContext.Current.Web.ID))
                        {
                            string deNghiUrl = (web.ServerRelativeUrl + Constants.ListUrlDeNghiCapPhep).Replace("//", "/");
                            SPList deNghiList = web.GetList(deNghiUrl);

                            //Define Query to get data
                            var viewFields = string.Concat(
                                       string.Format("<FieldRef Name='{0}' />", Fields.Title),
                                       string.Format("<FieldRef Name='{0}' />", Fields.CaNhanToChuc),
                                       string.Format("<FieldRef Name='{0}' />", Fields.NgayTiepNhan),
                                       string.Format("<FieldRef Name='{0}' />", Fields.NgayNopHoSo),
                                       string.Format("<FieldRef Name='{0}' />", Fields.NgayHenTra),
                                       string.Format("<FieldRef Name='{0}' />", Fields.NgayThucTra),
                                       string.Format("<FieldRef Name='{0}' />", Fields.NgayDuocCapPhep),
                                       string.Format("<FieldRef Name='{0}' />", Fields.NgayBoSung),
                                       "<FieldRef Name='ID' />");

                            #region Calc TonTruoc
                            SPQuery camlTonTruoc = Camlex.Query().Where(x => (DateTime)x[Fields.NgayTiepNhan] < fromDate
                                                        && ((((DateTime)x[Fields.NgayDuocCapPhep] >= fromDate && (DateTime)x[Fields.NgayDuocCapPhep] <= toDate) || ((DateTime)x[Fields.NgayHuyHoSo] >= fromDate && (DateTime)x[Fields.NgayHuyHoSo] <= toDate))
                                                            || (x[Fields.NgayDuocCapPhep] == null && x[Fields.NgayHuyHoSo] == null))
                                                        )
                                                        .ToSPQuery();
                            camlTonTruoc.ViewFields = viewFields;
                            camlTonTruoc.ViewFieldsOnly = true;
                            var tonTruocItems = deNghiList.GetItems(camlTonTruoc);
                            var tonTruoc = 0;
                            if (tonTruocItems != null)
                                tonTruoc = tonTruocItems.Count;
                            LoggingServices.LogMessage("Caml TonTruoc: " + camlTonTruoc.Query + ", ItemCount: " + tonTruoc);
                            #endregion Calc TonTruoc

                            #region Calc DaHuy
                            //SPQuery camlDaHuy = Camlex.Query()
                            //                        .Where(x => x[Fields.NgayHuyHoSo] == (DataTypes.DateTime)date.ToString())
                            //                        .ToSPQuery();
                            //camlDaHuy.ViewFields = viewFields;
                            //camlDaHuy.ViewFieldsOnly = true;
                            //var daHuy = 0;
                            //var daHuyItems = deNghiList.GetItems(camlDaHuy);
                            //if (daHuyItems != null)
                            //    daHuy = daHuyItems.Count;
                            //LoggingServices.LogMessage("Caml DaHuy: " + camlDaHuy.Query + ", ItemCount: " + daHuy);
                            #endregion Calc DaHuy

                            #region Calc MoiNhan
                            SPQuery camlMoiNhan = Camlex.Query()
                                                    .Where(x => (DateTime)x[Fields.NgayTiepNhan] >= fromDate && (DateTime)x[Fields.NgayTiepNhan] <= toDate)
                                                    .ToSPQuery();
                            camlMoiNhan.ViewFields = viewFields;
                            camlMoiNhan.ViewFieldsOnly = true;
                            var moiNhan = 0;
                            var moiNhanItems = deNghiList.GetItems(camlMoiNhan);
                            if (moiNhanItems != null)
                                moiNhan = moiNhanItems.Count;
                            LoggingServices.LogMessage("Caml MoiNhan: " + camlMoiNhan.Query + ", ItemCount: " + moiNhan);
                            #endregion Calc MoiNhan


                            #region Calc DaGiaiQuyet
                            SPQuery camlDaGiaiQuyet = Camlex.Query()
                                                    .Where(x => (((DateTime)x[Fields.NgayTiepNhan] < fromDate && (((DateTime)x[Fields.NgayDuocCapPhep] >= fromDate && (DateTime)x[Fields.NgayDuocCapPhep] <= toDate) || ((DateTime)x[Fields.NgayHuyHoSo] >= fromDate && (DateTime)x[Fields.NgayHuyHoSo] <= toDate)))
                                                                    || ((DateTime)x[Fields.NgayTiepNhan] >= fromDate && (DateTime)x[Fields.NgayTiepNhan] <= toDate))
                                                                && (x[Fields.NgayDuocCapPhep] != null || x[Fields.NgayHuyHoSo] != null)
                                                        )
                                                    .ToSPQuery();
                            camlDaGiaiQuyet.ViewFields = viewFields;
                            camlDaGiaiQuyet.ViewFieldsOnly = true;

                            var daGiaiQuyet_DungHan = 0;
                            var daGiaiQuyet_QuaHan = 0;

                            var daGiaiQuyetItems = deNghiList.GetItems(camlDaGiaiQuyet);
                            if (daGiaiQuyetItems != null)
                            {
                                daGiaiQuyet_DungHan = daGiaiQuyetItems.Cast<SPListItem>().Where(item => (DateTime)item[Fields.NgayDuocCapPhep] <= (DateTime)item[Fields.NgayHenTra])
                                                        .Select(item => new
                                                        {
                                                            Title = item["Title"].ToString()
                                                        })
                                                        .ToList().Count;

                                daGiaiQuyet_QuaHan = daGiaiQuyetItems.Cast<SPListItem>().Where(item => (DateTime)item[Fields.NgayDuocCapPhep] > (DateTime)item[Fields.NgayHenTra])
                                                        .Select(item => new
                                                        {
                                                            Title = item["Title"].ToString()
                                                        })
                                                        .ToList().Count;
                            }

                            LoggingServices.LogMessage("Caml DaGiaiQuyet: " + camlDaGiaiQuyet.Query + ", ItemCount daGiaiQuyet_DungHan: " + daGiaiQuyet_DungHan);
                            LoggingServices.LogMessage("Caml DaGiaiQuyet: " + camlDaGiaiQuyet.Query + ", ItemCount daGiaiQuyet_QuaHan: " + daGiaiQuyet_QuaHan);
                            #endregion Calc DaGiaiQuyet

                            #region Calc DaGiaiQuyet_QuaHan
                            //SPQuery camlDaGiaiQuyet_QuaHan = Camlex.Query()
                            //                        .Where(x => (DateTime)x[Fields.NgayNopHoSo] >= fromDate && (DateTime)x[Fields.NgayNopHoSo] <= toDate)
                            //                        .ToSPQuery();
                            //camlDaGiaiQuyet_QuaHan.ViewFields = viewFields;
                            //camlDaGiaiQuyet_QuaHan.ViewFieldsOnly = true;
                            //var daGiaiQuyet_QuaHan = 0;
                            //var daGiaiQuyet_QuaHanItems = deNghiList.GetItems(camlDaGiaiQuyet_QuaHan);
                            //if (daGiaiQuyet_QuaHanItems != null)
                            //    daGiaiQuyet_QuaHan = daGiaiQuyet_QuaHanItems.Count;
                            //LoggingServices.LogMessage("Caml DaGiaiQuyet_QuaHan: " + camlDaGiaiQuyet_QuaHan.Query + ", ItemCount: " + daGiaiQuyet_QuaHan);
                            #endregion Calc DaGiaiQuyet_QuaHan

                            #region Calc DangGiaiQuyet
                            SPQuery camlDangGiaiQuyet = Camlex.Query()
                                                    .Where(x => (((DateTime)x[Fields.NgayTiepNhan] < fromDate)
                                                                    || ((DateTime)x[Fields.NgayTiepNhan] >= fromDate && (DateTime)x[Fields.NgayTiepNhan] <= toDate))
                                                                && (x[Fields.NgayDuocCapPhep] == null && x[Fields.NgayHuyHoSo] == null)
                                                        )
                                                    .ToSPQuery();
                            camlDangGiaiQuyet.ViewFields = viewFields;
                            camlDangGiaiQuyet.ViewFieldsOnly = true;
                            var dangGiaiQuyet_ChuaDenHan = 0;
                            var dangGiaiQuyet_QuaHan = 0;
                            var dangGiaiQuyetItems = deNghiList.GetItems(camlDangGiaiQuyet);
                            if (dangGiaiQuyetItems != null)
                            {
                                dangGiaiQuyet_ChuaDenHan = dangGiaiQuyetItems.Cast<SPListItem>().Where(item => (DateTime)item[Fields.NgayHenTra] <= DateTime.Today)
                                                            .Select(item => new
                                                            {
                                                                Title = item["Title"].ToString()
                                                            })
                                                            .ToList().Count;

                                dangGiaiQuyet_QuaHan = dangGiaiQuyetItems.Cast<SPListItem>().Where(item => (DateTime)item[Fields.NgayHenTra] > DateTime.Today)
                                                            .Select(item => new
                                                            {
                                                                Title = item["Title"].ToString()
                                                            })
                                                            .ToList().Count;
                                                            }
                            LoggingServices.LogMessage("Caml DangGiaiQuyet: " + camlDangGiaiQuyet.Query + ", ItemCount dangGiaiQuyet_ChuaDenHan: " + dangGiaiQuyet_ChuaDenHan);
                            LoggingServices.LogMessage("Caml DangGiaiQuyet: " + camlDangGiaiQuyet.Query + ", ItemCount dangGiaiQuyet_QuaHan: " + dangGiaiQuyet_QuaHan);
                            #endregion Calc DangGiaiQuyet

                            #region Calc DangGiaiQuyet_QuaHan
                            //SPQuery camlDangGiaiQuyet_QuaHan = Camlex.Query()
                            //                        .Where(x => (DateTime)x[Fields.NgayNopHoSo] >= fromDate && (DateTime)x[Fields.NgayNopHoSo] <= toDate)
                            //                        .ToSPQuery();
                            //camlDangGiaiQuyet_QuaHan.ViewFields = viewFields;
                            //camlDangGiaiQuyet_QuaHan.ViewFieldsOnly = true;
                            //var dangGiaiQuyet_QuaHan = 0;
                            //var dangGiaiQuyet_QuaHanItems = deNghiList.GetItems(camlDangGiaiQuyet_QuaHan);
                            //if (dangGiaiQuyet_QuaHanItems != null)
                            //    dangGiaiQuyet_QuaHan = dangGiaiQuyet_QuaHanItems.Count;
                            //LoggingServices.LogMessage("Caml DangGiaiQuyet_QuaHan: " + camlDangGiaiQuyet_QuaHan.Query + ", ItemCount: " + dangGiaiQuyet_QuaHan);
                            #endregion Calc DangGiaiQuyet_QuaHan

                            #region [Export Word]
                            try
                            {
                                string licenseFile = SPUtility.GetVersionedGenericSetupPath(Constants.AsposeCellLicPath, 15);
                                //Set license
                                string wordLicFile = Microsoft.SharePoint.Utilities.SPUtility.GetVersionedGenericSetupPath(Constants.ConfWordLicFile, 15);
                                Aspose.Words.License wordLic = new Aspose.Words.License();
                                wordLic.SetLicense(wordLicFile);

                                string templateFile = Microsoft.SharePoint.Utilities.SPUtility.GetVersionedGenericSetupPath(Constants.DeNghiReportQuarterPath, 15);
                                Aspose.Words.Document doc = new Aspose.Words.Document(templateFile);

                                // Fill the fields in the document with user data.
                                doc.MailMerge.Execute(
                                    new string[] { "NgayBC", "ThangBC", "NamBC", "Quy", "Nam", "NhanGiaiQuyet_Tong", "NhanGiaiQuyet_ChuyenKyTruoc", "NhanGiaiQuyet_MoiTiepNhan", "DaGiaiQuyet_Tong", "DaGiaiQuyet_DungHan", "DaGiaiQuyet_QuaHan", "DangGiaiQuyet_Tong", "DangGiaiQuyet_ChuaDenHan", "DangGiaiQuyet_QuaHan" },
                                    new object[] { DateTime.Today.ToString("dd"), DateTime.Today.ToString("MM"), DateTime.Today.ToString("yyyy"), GetQuarter(fromDate).ToString(), fromDate.ToString("yyyy"), tonTruoc + moiNhan, tonTruoc, moiNhan, daGiaiQuyet_DungHan + daGiaiQuyet_QuaHan, daGiaiQuyet_DungHan, daGiaiQuyet_QuaHan, dangGiaiQuyet_ChuaDenHan + dangGiaiQuyet_QuaHan, dangGiaiQuyet_ChuaDenHan, dangGiaiQuyet_QuaHan });

                                string exportName = "TinhHinhKetQuaGiaiQuyetTTHC_Quy" + GetQuarter(fromDate).ToString() + "_" + fromDate.ToString("yyyy") + DateTime.Now.ToString("_yyyyMMdd") + ".docx";
                                MemoryStream ms = new MemoryStream();
                                doc.Save(ms, Aspose.Words.SaveFormat.Docx);

                                //Response to client
                                this.Page.Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                                this.Page.Response.AppendHeader("Content-Disposition", "attachment; filename=" + exportName);
                                this.Page.Response.Flush();
                                this.Page.Response.BinaryWrite(ms.ToArray());
                                //this.Page.Response.End();
                                HttpContext.Current.ApplicationInstance.CompleteRequest();
                            }
                            catch (Exception ex)
                            {
                                LoggingServices.LogException(ex);
                            }
                            #endregion [Export Word]
                        }
                    }

                });
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
        }

        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            if (!dtcFromDate.IsDateEmpty && dtcFromDate.IsValid && !dtcToDate.IsDateEmpty && dtcToDate.IsValid)
            {
                GenerateViewReport(dtcFromDate.SelectedDate, dtcToDate.SelectedDate);
            }
        }

        protected void GenerateViewReport(DateTime fromDate, DateTime toDate)
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

                            //var expressionsOr = new List<Expression<Func<SPListItem, bool>>>();
                            //expressionsOr.Add(x => (string)x[Constants.FieldTrangThai] == ((int)TrangThaiHoSo.DuocCapPhep).ToString());
                            //expressionsOr.Add(x => (string)x[Constants.FieldTrangThai] == ((int)TrangThaiHoSo.HoanThanh).ToString());
                            //expressionsOr.Add(x => (string)x[Constants.FieldTrangThai] == ((int)TrangThaiHoSo.ChuaHoanThanh).ToString());

                            //if (expressionsOr.Count > 0)
                            //{
                            //var orExpr = ExpressionsHelper.CombineOr(expressionsOr);
                            //expressions.Add(orExpr);
                            //}

                            var expressionsAnd = new List<Expression<Func<SPListItem, bool>>>();
                            //expressionsAnd.Add(x => x[Fields.NgayDuocCapPhep] != null);
                            expressionsAnd.Add(x => (DateTime)x[Fields.NgayDuocCapPhep] >= fromDate);
                            expressionsAnd.Add(x => (DateTime)x[Fields.NgayDuocCapPhep] < toDate.AddDays(1));

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
                                foreach (SPListItem item in items)
                                {
                                    try
                                    {
                                        bool isAddedAlready = false;

                                        int soTrucCuaXe = 0;

                                        bool resultSoTrucCuaXe = false;
                                        if (item[Constants.FieldSoTrucCuaXe] != null)
                                        {
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

                            #region [Generate View]
                            try
                            {

                                string currentDate = string.Format("Long An, ngày {0} tháng {1} năm {2}", DateTime.Now.ToString("dd"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("yyyy"));
                                lblCurrentDateQKQT.Text = currentDate;
                                lblCurrentDateQK.Text = currentDate;
                                lblCurrentDateQT.Text = currentDate;

                                string fromDateToDate = string.Format("Thời gian báo cáo: từ {0} đến {1}", fromDate.ToString("dd/MM/yyyy"), toDate.ToString("dd/MM/yyyy"));
                                lblFromDateToDateQKQT.Text = fromDateToDate;
                                lblFromDateToDateQK.Text = fromDateToDate;
                                lblFromDateToDateQT.Text = fromDateToDate;

                                lblQuaTai_Xe2Truc_QuocLo.Text = modelQuaTai.Xe2Truc_QuocLo.ToString();
                                lblQuaTai_Xe2Truc_DuongTinh.Text = modelQuaTai.Xe2Truc_DuongTinh.ToString();
                                lblQuaTai_Xe2Truc_TongCong.Text = modelQuaTai.Xe2Truc_TongCong.ToString();
                                lblQuaTai_Xe3Truc_QuocLo.Text = modelQuaTai.Xe3Truc_QuocLo.ToString();
                                lblQuaTai_Xe3Truc_DuongTinh.Text = modelQuaTai.Xe3Truc_DuongTinh.ToString();
                                lblQuaTai_Xe3Truc_TongCong.Text = modelQuaTai.Xe3Truc_TongCong.ToString();
                                lblQuaTai_Xe4Truc_QuocLo.Text = modelQuaTai.Xe4Truc_QuocLo.ToString();
                                lblQuaTai_Xe4Truc_DuongTinh.Text = modelQuaTai.Xe4Truc_DuongTinh.ToString();
                                lblQuaTai_Xe4Truc_TongCong.Text = modelQuaTai.Xe4Truc_TongCong.ToString();
                                lblQuaTai_XeRM3Truc_QuocLo.Text = modelQuaTai.XeRM3Truc_QuocLo.ToString();
                                lblQuaTai_XeRM3Truc_DuongTinh.Text = modelQuaTai.XeRM3Truc_DuongTinh.ToString();
                                lblQuaTai_XeRM3Truc_TongCong.Text = modelQuaTai.XeRM3Truc_TongCong.ToString();
                                lblQuaTai_XeRM4Truc_QuocLo.Text = modelQuaTai.XeRM4Truc_QuocLo.ToString();
                                lblQuaTai_XeRM4Truc_DuongTinh.Text = modelQuaTai.XeRM4Truc_DuongTinh.ToString();
                                lblQuaTai_XeRM4Truc_TongCong.Text = modelQuaTai.XeRM4Truc_TongCong.ToString();
                                lblQuaTai_XeRM5Truc_QuocLo.Text = modelQuaTai.XeRM5Truc_QuocLo.ToString();
                                lblQuaTai_XeRM5Truc_DuongTinh.Text = modelQuaTai.XeRM5Truc_DuongTinh.ToString();
                                lblQuaTai_XeRM5Truc_TongCong.Text = modelQuaTai.XeRM5Truc_TongCong.ToString();
                                lblQuaTai_TongCong_QuocLo.Text = modelQuaTai.TongCong_QuocLo.ToString();
                                lblQuaTai_TongCong_DuongTinh.Text = modelQuaTai.TongCong_DuongTinh.ToString();
                                lblQuaTai_TongCong_TongCong.Text = modelQuaTai.TongCong_TongCong.ToString();
                                lblQuaTai_TongCong_QuocLo8.Text = modelQuaTai.TongCong_QuocLo.ToString();
                                lblQuaTai_TongCong_DuongTinh8.Text = modelQuaTai.TongCong_DuongTinh.ToString();
                                lblQuaTai_TongCong_TongCong8.Text = modelQuaTai.TongCong_TongCong.ToString();

                                lblQuaKho_Xe2Truc_QuocLo.Text = modelQuaKho.Xe2Truc_QuocLo.ToString();
                                lblQuaKho_Xe2Truc_DuongTinh.Text = modelQuaKho.Xe2Truc_DuongTinh.ToString();
                                lblQuaKho_Xe2Truc_TongCong.Text = modelQuaKho.Xe2Truc_TongCong.ToString();
                                lblQuaKho_Xe3Truc_QuocLo.Text = modelQuaKho.Xe3Truc_QuocLo.ToString();
                                lblQuaKho_Xe3Truc_DuongTinh.Text = modelQuaKho.Xe3Truc_DuongTinh.ToString();
                                lblQuaKho_Xe3Truc_TongCong.Text = modelQuaKho.Xe3Truc_TongCong.ToString();
                                lblQuaKho_Xe4Truc_QuocLo.Text = modelQuaKho.Xe4Truc_QuocLo.ToString();
                                lblQuaKho_Xe4Truc_DuongTinh.Text = modelQuaKho.Xe4Truc_DuongTinh.ToString();
                                lblQuaKho_Xe4Truc_TongCong.Text = modelQuaKho.Xe4Truc_TongCong.ToString();
                                lblQuaKho_XeRM3Truc_QuocLo.Text = modelQuaKho.XeRM3Truc_QuocLo.ToString();
                                lblQuaKho_XeRM3Truc_DuongTinh.Text = modelQuaKho.XeRM3Truc_DuongTinh.ToString();
                                lblQuaKho_XeRM3Truc_TongCong.Text = modelQuaKho.XeRM3Truc_TongCong.ToString();
                                lblQuaKho_XeRM4Truc_QuocLo.Text = modelQuaKho.XeRM4Truc_QuocLo.ToString();
                                lblQuaKho_XeRM4Truc_DuongTinh.Text = modelQuaKho.XeRM4Truc_DuongTinh.ToString();
                                lblQuaKho_XeRM4Truc_TongCong.Text = modelQuaKho.XeRM4Truc_TongCong.ToString();
                                lblQuaKho_XeRM5Truc_QuocLo.Text = modelQuaKho.XeRM5Truc_QuocLo.ToString();
                                lblQuaKho_XeRM5Truc_DuongTinh.Text = modelQuaKho.XeRM5Truc_DuongTinh.ToString();
                                lblQuaKho_XeRM5Truc_TongCong.Text = modelQuaKho.XeRM5Truc_TongCong.ToString();
                                lblQuaKho_TongCong_QuocLo.Text = modelQuaKho.TongCong_QuocLo.ToString();
                                lblQuaKho_TongCong_DuongTinh.Text = modelQuaKho.TongCong_DuongTinh.ToString();
                                lblQuaKho_TongCong_TongCong.Text = modelQuaKho.TongCong_TongCong.ToString();
                                lblQuaKho_TongCong_QuocLo8.Text = modelQuaKho.TongCong_QuocLo.ToString();
                                lblQuaKho_TongCong_DuongTinh8.Text = modelQuaKho.TongCong_DuongTinh.ToString();
                                lblQuaKho_TongCong_TongCong8.Text = modelQuaKho.TongCong_TongCong.ToString();

                                lblQuaTaiQuaKho_Xe2Truc_QuocLo.Text = modelQuaTaiQuaKho.Xe2Truc_QuocLo.ToString();
                                lblQuaTaiQuaKho_Xe2Truc_DuongTinh.Text = modelQuaTaiQuaKho.Xe2Truc_DuongTinh.ToString();
                                lblQuaTaiQuaKho_Xe2Truc_TongCong.Text = modelQuaTaiQuaKho.Xe2Truc_TongCong.ToString();
                                lblQuaTaiQuaKho_Xe3Truc_QuocLo.Text = modelQuaTaiQuaKho.Xe3Truc_QuocLo.ToString();
                                lblQuaTaiQuaKho_Xe3Truc_DuongTinh.Text = modelQuaTaiQuaKho.Xe3Truc_DuongTinh.ToString();
                                lblQuaTaiQuaKho_Xe3Truc_TongCong.Text = modelQuaTaiQuaKho.Xe3Truc_TongCong.ToString();
                                lblQuaTaiQuaKho_Xe4Truc_QuocLo.Text = modelQuaTaiQuaKho.Xe4Truc_QuocLo.ToString();
                                lblQuaTaiQuaKho_Xe4Truc_DuongTinh.Text = modelQuaTaiQuaKho.Xe4Truc_DuongTinh.ToString();
                                lblQuaTaiQuaKho_Xe4Truc_TongCong.Text = modelQuaTaiQuaKho.Xe4Truc_TongCong.ToString();
                                lblQuaTaiQuaKho_XeRM3Truc_QuocLo.Text = modelQuaTaiQuaKho.XeRM3Truc_QuocLo.ToString();
                                lblQuaTaiQuaKho_XeRM3Truc_DuongTinh.Text = modelQuaTaiQuaKho.XeRM3Truc_DuongTinh.ToString();
                                lblQuaTaiQuaKho_XeRM3Truc_TongCong.Text = modelQuaTaiQuaKho.XeRM3Truc_TongCong.ToString();
                                lblQuaTaiQuaKho_XeRM4Truc_QuocLo.Text = modelQuaTaiQuaKho.XeRM4Truc_QuocLo.ToString();
                                lblQuaTaiQuaKho_XeRM4Truc_DuongTinh.Text = modelQuaTaiQuaKho.XeRM4Truc_DuongTinh.ToString();
                                lblQuaTaiQuaKho_XeRM4Truc_TongCong.Text = modelQuaTaiQuaKho.XeRM4Truc_TongCong.ToString();
                                lblQuaTaiQuaKho_XeRM5Truc_QuocLo.Text = modelQuaTaiQuaKho.XeRM5Truc_QuocLo.ToString();
                                lblQuaTaiQuaKho_XeRM5Truc_DuongTinh.Text = modelQuaTaiQuaKho.XeRM5Truc_DuongTinh.ToString();
                                lblQuaTaiQuaKho_XeRM5Truc_TongCong.Text = modelQuaTaiQuaKho.XeRM5Truc_TongCong.ToString();
                                lblQuaTaiQuaKho_TongCong_QuocLo.Text = modelQuaTaiQuaKho.TongCong_QuocLo.ToString();
                                lblQuaTaiQuaKho_TongCong_DuongTinh.Text = modelQuaTaiQuaKho.TongCong_DuongTinh.ToString();
                                lblQuaTaiQuaKho_TongCong_TongCong.Text = modelQuaTaiQuaKho.TongCong_TongCong.ToString();
                                lblQuaTaiQuaKho_TongCong_QuocLo8.Text = modelQuaTaiQuaKho.TongCong_QuocLo.ToString();
                                lblQuaTaiQuaKho_TongCong_DuongTinh8.Text = modelQuaTaiQuaKho.TongCong_DuongTinh.ToString();
                                lblQuaTaiQuaKho_TongCong_TongCong8.Text = modelQuaTaiQuaKho.TongCong_TongCong.ToString();


                                pnlViewBaoCaoCapPhepLuuHanh.Visible = true;
                            }
                            catch (Exception ex)
                            {
                                LoggingServices.LogException(ex);
                            }
                            #endregion [Generate View]

                        }
                    }

                });
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
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
