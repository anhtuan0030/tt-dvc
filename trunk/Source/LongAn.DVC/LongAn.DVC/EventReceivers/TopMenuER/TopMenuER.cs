using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using System.Collections.Generic;
using System.Linq.Expressions;
using LongAn.DVC.Common;
using CamlexNET;

namespace LongAn.DVC.EventReceivers.TopMenuER
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class TopMenuER : SPItemEventReceiver
    {
        /// <summary>
        /// An item is being added.
        /// </summary>
        public override void ItemAdding(SPItemEventProperties properties)
        {
            base.ItemAdding(properties);

            IncreaseItemOrderNo(properties);

            TrimTextFieldValue(properties, Constants.TopMenu.TITLE_COLUMN);
            TrimTextFieldValue(properties, Constants.TopMenu.HYPERLINK_COLUMN);
        }

        /// <summary>
        /// An item is being updated.
        /// </summary>
        public override void ItemUpdating(SPItemEventProperties properties)
        {
            base.ItemUpdating(properties);

            IncreaseItemOrderNo(properties);

            TrimTextFieldValue(properties, Constants.TopMenu.TITLE_COLUMN);
            TrimTextFieldValue(properties, Constants.TopMenu.HYPERLINK_COLUMN);
        }

        /// <summary>
        /// An item is being deleted.
        /// </summary>
        public override void ItemDeleting(SPItemEventProperties properties)
        {
            base.ItemDeleting(properties);
        }


        private void IncreaseItemOrderNo(SPItemEventProperties properties)
        {
            if (properties.AfterProperties[Constants.TopMenu.ITEM_ORDER_COLUMN] != null && !string.IsNullOrEmpty(properties.AfterProperties[Constants.TopMenu.ITEM_ORDER_COLUMN].ToString()))
            {
                int orderNo = 0;
                int.TryParse(properties.AfterProperties[Constants.TopMenu.ITEM_ORDER_COLUMN].ToString(), out orderNo);

                if (orderNo != 0)
                {
                    string strItemId = properties.ListItem == null ? "0" : properties.ListItemId.ToString();

                    string caml = string.Empty;
                    var expressionsAnd = new List<Expression<Func<SPListItem, bool>>>();

                    expressionsAnd.Add(x => ((int)x[Constants.TopMenu.ITEM_ORDER_COLUMN]) >= orderNo);
                    expressionsAnd.Add(x => (x["ID"]) != (DataTypes.Counter)strItemId);

                    if (properties.AfterProperties[Constants.TopMenu.PARENT_COLUMN] != null && !string.IsNullOrEmpty(properties.AfterProperties[Constants.TopMenu.PARENT_COLUMN].ToString().Trim()))
                    {
                        expressionsAnd.Add(x => (x[Constants.TopMenu.PARENT_COLUMN]) == (DataTypes.LookupId)properties.AfterProperties[Constants.TopMenu.PARENT_COLUMN].ToString());
                    }

                    caml = Camlex.Query().WhereAll(expressionsAnd).OrderBy(x => x[Constants.TopMenu.ITEM_ORDER_COLUMN] as Camlex.Asc).ToString();

                    SPQuery spQuery = new SPQuery();
                    spQuery.Query = caml;

                    SPList currentList = properties.List;
                    SPListItemCollection items = currentList.GetItems(spQuery);

                    foreach (SPListItem item in items)
                    {
                        orderNo++;

                        using (DisableItemEvent scope = new DisableItemEvent())
                        {
                            item[Constants.TopMenu.ITEM_ORDER_COLUMN] = orderNo;
                            item.SystemUpdate(false);
                        }
                    }
                }
            }
            else
            {
                properties.AfterProperties[Constants.TopMenu.ITEM_ORDER_COLUMN] = GetLatestItemOrderNo(properties) + 1;
            }
        }

        private int GetLatestItemOrderNo(SPItemEventProperties properties)
        {
            int orderNo = 0;

            string caml = string.Empty;
            var expressionsAnd = new List<Expression<Func<SPListItem, bool>>>();

            if (properties.AfterProperties[Constants.TopMenu.PARENT_COLUMN] != null && !string.IsNullOrEmpty(properties.AfterProperties[Constants.TopMenu.PARENT_COLUMN].ToString().Trim()))
            {
                expressionsAnd.Add(x => (x[Constants.TopMenu.PARENT_COLUMN]) == (DataTypes.LookupId)properties.AfterProperties[Constants.TopMenu.PARENT_COLUMN].ToString());
            }

            if (expressionsAnd.Count > 0)
                caml = Camlex.Query().WhereAll(expressionsAnd).OrderBy(x => x[Constants.TopMenu.ITEM_ORDER_COLUMN] as Camlex.Desc).ToString();
            else
                caml = Camlex.Query().OrderBy(x => x[Constants.TopMenu.ITEM_ORDER_COLUMN] as Camlex.Desc).ToString();

            SPQuery spQuery = new SPQuery();
            spQuery.Query = caml;

            spQuery.RowLimit = 1;

            SPList currentList = properties.List;
            SPListItemCollection items = currentList.GetItems(spQuery);

            if (items != null && items.Count > 0)
            {
                SPListItem item = items[0];

                if (item != null)
                {
                    orderNo = Convert.ToInt32(item[Constants.TopMenu.ITEM_ORDER_COLUMN]);
                }
            }

            return orderNo;
        }

        private void TrimTextFieldValue(SPItemEventProperties properties, string fieldName)
        {
            if (properties.AfterProperties[fieldName] != null)
            {
                properties.AfterProperties[fieldName] = properties.AfterProperties[fieldName].ToString().Trim();
            }
            else
            {
                properties.AfterProperties[fieldName] = string.Empty;
            }
        }
    }
}