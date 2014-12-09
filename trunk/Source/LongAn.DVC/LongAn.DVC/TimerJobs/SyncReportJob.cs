using CamlexNET;
using LongAn.DVC.Common;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongAn.DVC.TimerJobs
{
    class SyncReportJob: SPJobDefinition
    {
        public SyncReportJob()
            : base()
        {

        }

        public SyncReportJob(string jobName, SPService service,
               SPServer server, SPJobLockType lockType)
            : base(jobName, service, server, lockType)
        {
            this.Title = Constants.SyncReportJobName;
        }

        public SyncReportJob(string jobName, SPWebApplication webapp)
            : base(jobName, webapp, null, SPJobLockType.ContentDatabase)
        {
            this.Title = Constants.SyncReportJobName;
        }

        public override void Execute(Guid targetInstanceId)
        {
            try
            {
                LoggingServices.LogMessage("Begin Sync Report DVC");
                SPWebApplication webapp = this.Parent as SPWebApplication;
                SPContentDatabase contentDb = webapp.ContentDatabases[targetInstanceId];
                var siteId = this.Properties[Constants.SiteIdProperty] as string;
                var webId = this.Properties[Constants.WebIdProperty] as string;
                using (SPSite site = new SPSite(new Guid(siteId)))
                {
                    using (SPWeb web = site.OpenWeb(new Guid(webId)))
                    {
                        var date = DateTime.Today.ToString("yyyy-MM-dd");
                        //Get Connection String
                        var thamSoUrl = (web.ServerRelativeUrl + Constants.ListUrlThamSo).Replace("//", "/");
                        var thamSoList = web.GetList(thamSoUrl);
                        SPQuery caml = Camlex.Query().Where(x => (string)x[Fields.Title] == Constants.ConnectionString)
                                .ToSPQuery();
                        var items = thamSoList.GetItems(caml);
                        if (items != null && items.Count > 0)
                        {
                            var connectionString = items[0]["Value"].ToString();
                            LoggingServices.LogMessage("DVC Connection String: " + connectionString);
                            //Get List DeNghi
                            var deNghiList = web.GetList((web.ServerRelativeUrl + Constants.ListUrlDeNghiCapPhep).Replace("//", "/"));
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
                            SPQuery camlTonTruoc = Camlex.Query().Where(x => x[SPBuiltInFieldId.Created] < (DataTypes.DateTime)date
                                                        && x[Fields.NgayDuocCapPhep] == null && x[Fields.NgayHuyHoSo] == null)
                                                        //.OrderBy(x => new[] { x["ID"] as Camlex.Asc })
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
                            SPQuery camlDaHuy = Camlex.Query()
                                                    .Where(x => x[Fields.NgayHuyHoSo] == (DataTypes.DateTime)date.ToString())
                                                    .ToSPQuery();
                            camlDaHuy.ViewFields = viewFields;
                            camlDaHuy.ViewFieldsOnly = true;
                            var daHuy = 0;
                            var daHuyItems = deNghiList.GetItems(camlDaHuy);
                            if (daHuyItems != null)
                                daHuy = daHuyItems.Count;
                            LoggingServices.LogMessage("Caml DaHuy: " + camlDaHuy.Query + ", ItemCount: " + daHuy);
                            #endregion Calc DaHuy

                            #region Calc MoiNhan
                            SPQuery camlMoiNhan = Camlex.Query()
                                                    .Where(x => x[Fields.NgayNopHoSo] == (DataTypes.DateTime)date.ToString()
                                                    && x[Fields.NgayDuocCapPhep] == null && x[Fields.NgayHuyHoSo] == null)
                                                    .ToSPQuery();
                            camlMoiNhan.ViewFields = viewFields;
                            camlMoiNhan.ViewFieldsOnly = true;
                            var moiNhan = 0;
                            var moiNhanItems = deNghiList.GetItems(camlMoiNhan);
                            if(moiNhanItems != null)
                                moiNhan = moiNhanItems.Count;
                            LoggingServices.LogMessage("Caml MoiNhan: " + camlMoiNhan.Query + ", ItemCount: " + moiNhan);
                            #endregion Calc MoiNhan

                            #region Calc DaGiaiQuyet
                            SPQuery camlDaGiaiQuyet = Camlex.Query().Where(x => x[Fields.NgayDuocCapPhep] == (DataTypes.DateTime)date)
                                //.OrderBy(x => new[] { x["ID"] as Camlex.Asc })
                                                        .ToSPQuery();
                            camlDaGiaiQuyet.ViewFields = viewFields;
                            camlDaGiaiQuyet.ViewFieldsOnly = true;
                            var dtDaGiaiQuyet = deNghiList.GetItems(camlDaGiaiQuyet).GetDataTable();
                            var daGQDungHan = 0;
                            var daGQQuaHan = 0;
                            if (dtDaGiaiQuyet != null && dtDaGiaiQuyet.Rows.Count > 0)
                            {
                                var dataRows = dtDaGiaiQuyet.Select(string.Format("{0}<{1}", Fields.NgayHenTra, Fields.NgayDuocCapPhep));
                                daGQQuaHan = dataRows != null ? dataRows.Count() : 0;
                                daGQDungHan = dtDaGiaiQuyet.Rows.Count - daGQQuaHan;
                            }
                            LoggingServices.LogMessage("Caml DaGiaiQuyet: " + camlTonTruoc.Query);

                            #endregion Calc DaGiaiQuyet

                            #region Calc ChuaGiaiQuyet
                            var chuaGQTrongHan = moiNhan;
                            var chuaGQQuaHan = 0;
                            if (tonTruocItems != null)
	                        {
                                var dtChuaGiaiQuyet = tonTruocItems.GetDataTable();
                                if (dtChuaGiaiQuyet != null && dtChuaGiaiQuyet.Rows.Count > 0)
                                {
                                    var dataRows = dtChuaGiaiQuyet.Select(string.Format("{0}<#{1}#", Fields.NgayHenTra, date));
                                    chuaGQQuaHan = dataRows != null ? dataRows.Count() : 0;
                                    chuaGQTrongHan = tonTruoc + moiNhan - chuaGQQuaHan;   
                                }
	                        }
                            
                            #endregion Calc ChuaGiaiQuyet

                            #region Calc DaTra
                            SPQuery camlDaTra = Camlex.Query().Where(x => x[Fields.NgayThucTra] == (DataTypes.DateTime)date
                                                                    && (string)x[Fields.TinhTrangTraHoSo] == Constants.TinhTrangTraHoSo_DaTra)
                                //.OrderBy(x => new[] { x["ID"] as Camlex.Asc })
                                                        .ToSPQuery();
                            camlDaTra.ViewFields = viewFields;
                            camlDaTra.ViewFieldsOnly = true;
                            var daTraItmes = deNghiList.GetItems(camlDaTra);
                            var daTra = 0;
                            if (daTraItmes != null)
                                daTra = daTraItmes.Count;
                            LoggingServices.LogMessage("Caml DaTra: " + camlDaTra.Query + ", ItemCount: " + daTra);

                            #endregion Calc DaTra

                            #region Calc ChuaTra
                            SPQuery camlChuaTra = Camlex.Query().Where(x => x[Fields.NgayThucTra] == (DataTypes.DateTime)date
                                                                        && (string)x[Fields.TinhTrangTraHoSo] == Constants.TinhTrangTraHoSo_ChuaTra)
                                //.OrderBy(x => new[] { x["ID"] as Camlex.Asc })
                                                        .ToSPQuery();
                            camlChuaTra.ViewFields = viewFields;
                            camlChuaTra.ViewFieldsOnly = true;
                            var chuaTraItmes = deNghiList.GetItems(camlChuaTra);
                            var chuaTra = 0;
                            if (chuaTraItmes != null)
                                chuaTra = chuaTraItmes.Count;
                            LoggingServices.LogMessage("Caml ChuaTra: " + camlChuaTra.Query + ", ItemCount: " + chuaTra);

                            #endregion Calc ChuaTra

                            #region BoTucHoSo
                            var boSungHoSoList = web.GetList((web.ServerRelativeUrl + Constants.ListUrlYeuCauBoSung).Replace("//", "/"));
                            SPQuery camlBoTucHoSo = Camlex.Query().Where(x => x[SPBuiltInFieldId.Created] == (DataTypes.DateTime)date)
                                //.OrderBy(x => new[] { x["ID"] as Camlex.Asc })
                                                        .ToSPQuery();
                            var boTucHoSoItems = boSungHoSoList.GetItems(camlBoTucHoSo);
                            var boTucHoSo = 0;
                            if (boTucHoSoItems != null)
                                boTucHoSo = boTucHoSoItems.Count;
                            LoggingServices.LogMessage("Caml BoTucHoSo: " + camlBoTucHoSo.Query + ", ItemCount: " + boTucHoSo);
                            #endregion BoTucHoSo

                            //Declare parameter
                            //SqlParameter[] parameters = new SqlParameter[11];
                            //parameters[0] = new SqlParameter("@TonTruoc", 0);
                            //parameters[1] = new SqlParameter("@MoiNhan", 0);
                            //parameters[2] = new SqlParameter("@DaHuy", 0);
                            //parameters[3] = new SqlParameter("@DaGQDungHan", 0);
                            //parameters[4] = new SqlParameter("@DaGQQuaHan", 0);
                            //parameters[5] = new SqlParameter("@ChuaGQTrongHan", 0);
                            //parameters[6] = new SqlParameter("@ChuaGQQuaHan", 0);
                            //parameters[7] = new SqlParameter("@BoTucHoSo", 0);
                            //parameters[8] = new SqlParameter("@DaTra", 0);
                            //parameters[9] = new SqlParameter("@ChuaTra", 0);
                            //parameters[10] = new SqlParameter("@NgayCapNhat", date);
                            //Insert Data BaoCaoTongHop
                            InsertData(connectionString, "[dbo].[sp_SyncBaoCaoTongHop]"
                                , tonTruoc
                                , moiNhan
                                , daHuy
                                , daGQDungHan
                                , daGQQuaHan
                                , chuaGQTrongHan
                                , chuaGQQuaHan
                                , boTucHoSo
                                , daTra
                                , chuaTra
                                , date);
                            //Insert Data ThongKeTongHop
                            //ExecuteNonQuery(connectionString, "[dbo].[sp_SyncThongKeTongHop]", parameters);
                            InsertData(connectionString, "[dbo].[sp_SyncThongKeTongHop]"
                                , tonTruoc
                                , moiNhan
                                , daHuy
                                , daGQDungHan
                                , daGQQuaHan
                                , chuaGQTrongHan
                                , chuaGQQuaHan
                                , boTucHoSo
                                , daTra
                                , chuaTra
                                , date);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("End Sync Report DVC");
        }

        private int InsertData(string connectionString
            , string storeName
            , int tonTruoc
            , int moiNhan
            , int daHuy
            , int daGQDungHan
            , int daGQQuaHan
            , int chuaGQTrongHan
            , int chuaGQQuaHan
            , int boTucHoSo
            , int daTra
            , int chuaTra
            , string ngayCapNhat)
        {
            int recordInsert = 0;
            try
            {
                LoggingServices.LogMessage("Beggin insert data to db: " + connectionString
                    + " StoreName: " + storeName);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(storeName, connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TonTruoc", tonTruoc);
                        command.Parameters.AddWithValue("@MoiNhan", moiNhan);
                        command.Parameters.AddWithValue("@DaHuy", daHuy);
                        command.Parameters.AddWithValue("@DaGQDungHan", daGQDungHan);
                        command.Parameters.AddWithValue("@DaGQQuaHan", daGQQuaHan);
                        command.Parameters.AddWithValue("@ChuaGQTrongHan", chuaGQTrongHan);
                        command.Parameters.AddWithValue("@ChuaGQQuaHan", chuaGQQuaHan);
                        command.Parameters.AddWithValue("@BoTucHoSo", boTucHoSo);
                        command.Parameters.AddWithValue("@DaTra", daTra);
                        command.Parameters.AddWithValue("@ChuaTra", chuaTra);
                        command.Parameters.AddWithValue("@NgayCapNhat", ngayCapNhat);
                        recordInsert = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("End insert data to db! Number of record inserted: " + recordInsert);
            return recordInsert;
        }

        public int ExecuteNonQuery(string connectionString, string procedureName, SqlParameter[] parameters)
        {
            LoggingServices.LogMessage("Beggin insert data to db: " + connectionString
                    + " StoreName: " + procedureName);
            SqlConnection oConnection = new SqlConnection(connectionString);
            SqlCommand oCommand = new SqlCommand(procedureName, oConnection);
            oCommand.CommandType = CommandType.StoredProcedure;
            int iReturnValue = 0;
            oConnection.Open();
            using (SqlTransaction oTransaction = oConnection.BeginTransaction())
            {
                try
                {
                    if (parameters != null)
                        oCommand.Parameters.AddRange(parameters);
                    oCommand.Transaction = oTransaction;
                    iReturnValue = oCommand.ExecuteNonQuery();
                    oTransaction.Commit();
                }
                catch (Exception ex)
                {
                    oTransaction.Rollback();
                    LoggingServices.LogException(ex);
                }
                finally
                {
                    if (oConnection.State == ConnectionState.Open)
                        oConnection.Close();
                    oConnection.Dispose();
                    oCommand.Dispose();
                }
            }
            LoggingServices.LogMessage("End insert data to db! Number of record inserted: " + iReturnValue);
            return iReturnValue;
        }
        protected override bool HasAdditionalUpdateAccess()
        {
            return true;
        }
    }
}
