using CamlexNET;
using LongAn.DVC.Common;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongAn.DVC.EventReceivers
{
    public class DeNghiCapPhepXeEvent : SPItemEventReceiver
    {
        public override void ItemAdding(SPItemEventProperties properties)
        {
            base.ItemAdding(properties);
        }

        public override void ItemAdded(SPItemEventProperties properties)
        {
            base.ItemAdded(properties);
            using (DisableItemEvent disableItem = new DisableItemEvent())
            {
                var currentItem = properties.ListItem;
                var namDeNghi = DateTime.Now.ToString("yyyy");
                var soThuTuBienNhan = GenerateBienNhan(properties.Web);
                currentItem[Constants.FieldTitle] = namDeNghi + GenerateBienNhan(properties.Web);
                currentItem[Constants.FieldNamDeNghi] = namDeNghi;
                currentItem[Constants.FieldSoThuTuBienNhan] = int.Parse(soThuTuBienNhan);
                currentItem.SystemUpdate();
            }
        }

        public override void ItemUpdating(SPItemEventProperties properties)
        {
            base.ItemUpdating(properties);
        }

        public override void ItemUpdated(SPItemEventProperties properties)
        {
            base.ItemUpdated(properties);
        }

        public override void ItemDeleting(SPItemEventProperties properties)
        {
            base.ItemDeleting(properties);
        }

        #region Private functions
        string GenerateBienNhan(SPWeb spWeb)
        {
            string bienNhan = string.Empty;
            try
            {
                LoggingServices.LogMessage("Begin ItemAdded SetSoBienNhan");
                SPQuery caml = Camlex.Query().Where(x => (string)x[Constants.FieldNamDeNghi] == DateTime.Now.ToString("yyyy"))
                                             .OrderBy(x => new[] { x["ID"] as Camlex.Desc }).ToSPQuery();
                caml.RowLimit = 1;
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(spWeb.Site.ID))
                    {
                        using (SPWeb web = site.OpenWeb(spWeb.ID))
                        {
                            var deNghiList = web.GetList(Constants.ListUrlDeNghiCapPhep);
                            var deNghiListItems = deNghiList.GetItems(caml);
                            string soThuTuBienNhan = Constants.ConfSoThuTuBienNhan + "1";
                            if (deNghiListItems != null && deNghiListItems.Count > 0)
                                soThuTuBienNhan = Constants.ConfSoThuTuBienNhan + 
                                    (int.Parse(deNghiListItems[0][Constants.FieldSoThuTuBienNhan].ToString()) + 1);
                            bienNhan = soThuTuBienNhan.Substring(soThuTuBienNhan.Length - 7);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("End ItemAdded SetSoBienNhan");
            return bienNhan;
        }
        #endregion Private functions
    }
}
