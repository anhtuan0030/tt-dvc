using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongAn.DVC.Common
{
    public class LoggingServices: SPDiagnosticsServiceBase
    {
        private LoggingServices() :
            base("LongAn DVC Logging Service", SPFarm.Local) { }


        private const string LOG_INFO = "LongAn DVC Info";
        private const string LOG_ERROR = "LongAn DVC Error";
        private static string PRODUCT_DIAGNOSTIC_NAME = "LongAn DVC Log";

        protected override IEnumerable<SPDiagnosticsArea> ProvideAreas()
        {
            List<SPDiagnosticsArea> areas = new List<SPDiagnosticsArea>{
                new SPDiagnosticsArea(PRODUCT_DIAGNOSTIC_NAME, new List<SPDiagnosticsCategory>{
                  new SPDiagnosticsCategory(LOG_INFO, TraceSeverity.Verbose, EventSeverity.Information),
                  new SPDiagnosticsCategory(LOG_ERROR, TraceSeverity.Unexpected, EventSeverity.Warning),
                })
            };

            return areas;
        }


        private static LoggingServices _current;
        public static LoggingServices Current
        {
            get
            {
                if (_current == null)
                    _current = new LoggingServices();
                return _current;
            }
        }

        public static void LogMessage(string message)
        {
            LogMessage(LOG_INFO, message);
        }

        public static void LogMessage(string categoryName, string message)
        {
            SPDiagnosticsCategory category =
            LoggingServices.Current.Areas[PRODUCT_DIAGNOSTIC_NAME].Categories[categoryName];
            LoggingServices.Current.WriteTrace(0, category, TraceSeverity.Verbose, message);
        }

        public static void LogError(string message)
        {
            LogError(LOG_ERROR, message);
        }

        private static void LogError(string categoryName, string message)
        {
            SPDiagnosticsCategory category =
            LoggingServices.Current.Areas[PRODUCT_DIAGNOSTIC_NAME].Categories[categoryName];
            LoggingServices.Current.WriteTrace(0, category, TraceSeverity.Unexpected, message);
        }

        public static void LogException(Exception ex)
        {
            SPDiagnosticsCategory category =
            LoggingServices.Current.Areas[PRODUCT_DIAGNOSTIC_NAME].Categories[LOG_ERROR];
            LoggingServices.Current.WriteTrace(0,
                category,
                TraceSeverity.Unexpected, 
                string.Format("{0} - {1} - {2} {3}{4}", ex.Source, 
                    ex.TargetSite == null ? "Unknow" : ex.TargetSite.Name, 
                    ex.Message,
                    Environment.NewLine,
                    ex.StackTrace));
        }

    }
}
