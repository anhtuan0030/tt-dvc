using CamlexNET;
using LongAn.DVC.Common;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
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
                        var date = DateTime.Today.ToString();
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
                            SPQuery camlTonTruoc = Camlex.Query().Where(x => x[SPBuiltInFieldId.Modified] == (DataTypes.DateTime)date
                                                        && x[Fields.NgayThucTra] == null)
                                                        //.OrderBy(x => new[] { x["ID"] as Camlex.Asc })
                                                        .ToSPQuery();
                            SPQuery camlMoiNhan = Camlex.Query()
                                                    .Where(x => x[SPBuiltInFieldId.Created] == (DataTypes.DateTime)date.ToString())
                                                    .ToSPQuery();
                            var deNghiItems = deNghiList.GetItems(camlTonTruoc);
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

        protected override bool HasAdditionalUpdateAccess()
        {
            return true;
        }
    }
}
