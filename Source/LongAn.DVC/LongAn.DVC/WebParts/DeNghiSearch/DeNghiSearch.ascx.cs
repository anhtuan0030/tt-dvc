﻿using CamlexNET;
using CamlexNET.Impl.Helpers;
using LongAn.DVC.Common;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq.Expressions;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace LongAn.DVC.WebParts.DeNghiSearch
{
    [ToolboxItemAttribute(false)]
    public partial class DeNghiSearch : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public DeNghiSearch()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
            btnTimKiem.Click += btnTimKiem_Click;
        }

        void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                var dataTable = GetDeNghi();
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    divResult.Visible = true;
                    var dataRow = dataTable.Rows[0];
                    lblSoBienNhan.Text = dataRow[Fields.Title].ToString();
                    var ngayTiepNhan = dataRow[Fields.NgayTiepNhan].ToString();
                    if (!string.IsNullOrEmpty(ngayTiepNhan))
                        lblNgayNhanHoSo.Text = Convert.ToDateTime(ngayTiepNhan).ToString("dd/MM/yyyy");

                    var ngayHenTra = dataRow[Fields.NgayHenTra].ToString();
                    if (!string.IsNullOrEmpty(ngayHenTra))
                        lblNgayHenTra.Text = Convert.ToDateTime(ngayHenTra).ToString("dd/MM/yyyy");

                    var ngayThucTra = dataRow[Fields.NgayThucTra].ToString();
                    if (!string.IsNullOrEmpty(ngayThucTra))
                        lblNgayThucTra.Text = Convert.ToDateTime(ngayThucTra).ToString("dd/MM/yyyy");
                        
                    var ngayDuocCapPhep = dataRow[Fields.NgayDuocCapPhep].ToString();
                    if (!string.IsNullOrEmpty(ngayDuocCapPhep))
                        lblTinhTrangHoSo.Text = dataRow[Fields.TenTrangThaiRef].ToString();
                    if (!string.IsNullOrEmpty(ngayHenTra) && !string.IsNullOrEmpty(ngayDuocCapPhep))
                    {
                        int soNgayTreHan = (Convert.ToDateTime(ngayDuocCapPhep) - Convert.ToDateTime(ngayHenTra)).Days;
                        //if (soNgayTreHan < 0)
                        var ngayThamDinhTu = dataRow[Fields.NgayThamDinhTu].ToString();
                        var ngayThamDinhDen = dataRow[Fields.NgayThamDinhDen].ToString();
                        int soNgayThamDinh = 0;
                        if(!string.IsNullOrEmpty(ngayThamDinhTu) && !string.IsNullOrEmpty(ngayThamDinhDen))
                            soNgayThamDinh = (Convert.ToDateTime(ngayThamDinhDen) - Convert.ToDateTime(ngayThamDinhTu)).Days;
                        lblSoNgayTreHan.Text = (soNgayTreHan - soNgayThamDinh).ToString();
                    }

                    lblCaNhanToChuc.Text = dataRow[Fields.CaNhanToChuc].ToString();
                    lblDiaChi.Text = dataRow[Fields.DiaChi].ToString();
                    lblDienThoai.Text = dataRow[Fields.DienThoai].ToString();

                    var itemId = dataRow["ID"].ToString();
                    var deNghiHis = GetDeNghiHis(itemId);
                    if (deNghiHis != null && deNghiHis.Rows.Count > 0)
                    {
                        repeaterLists.DataSource = deNghiHis;
                        repeaterLists.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        DataTable GetDeNghiHis(string itemId)
        {
            DataTable dataTable = null;
            try
            {
                LoggingServices.LogMessage("Begin GetDeNghiHis");
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                    {
                        using (SPWeb web = site.OpenWeb(SPContext.Current.Web.ID))
                        {
                            SPQuery caml = Camlex.Query().Where(x => x[Fields.DeNghi] == (DataTypes.LookupId)itemId)
                                    .OrderBy(x => new[] { x["ID"] as Camlex.Asc })
                                    .ToSPQuery();
                            var deNghiUrl = (web.ServerRelativeUrl + Constants.ListUrlLichSuCapPhep).Replace("//", "/");
                            var deNghiList = web.GetList(deNghiUrl);
                            dataTable = deNghiList.GetItems(caml).GetDataTable();
                        }
                    }

                });
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("End GetDeNghiHis");
            return dataTable;
        }

        DataTable GetDeNghi()
        {
            DataTable dataTable = null;
            try
            {
                LoggingServices.LogMessage("Begin GetDeNghi - current user");
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                    {
                        using (SPWeb web = site.OpenWeb(SPContext.Current.Web.ID))
                        {
                            //And condition
                            var andConditions = new List<Expression<Func<SPListItem, bool>>>();
                            if (!string.IsNullOrEmpty(txtMaBienNhan.Text.Trim()))
                                andConditions.Add(x => ((string)x[Fields.Title]).Contains(txtMaBienNhan.Text.Trim()));
                            if (!string.IsNullOrEmpty(txtCaNhanToChuc.Text.Trim()))
                                andConditions.Add(x => ((string)x[Fields.CaNhanToChuc]).Contains(txtCaNhanToChuc.Text.Trim()));
                            if (!string.IsNullOrEmpty(txtSoDienThoai.Text.Trim()))
                                andConditions.Add(x => ((string)x[Fields.DienThoai]).Contains(txtSoDienThoai.Text.Trim()));

                            if (!dtcNgayDeNghiDen.IsDateEmpty && !dtcNgayDeNghiTu.IsDateEmpty)
                            {
                                andConditions.Add(x => (x[Fields.NgayNopHoSo]) >= (DataTypes.DateTime)dtcNgayDeNghiTu.SelectedDate.ToString("yyyy-MM-dd"));
                                andConditions.Add(x => (x[Fields.NgayNopHoSo]) <= (DataTypes.DateTime)dtcNgayDeNghiDen.SelectedDate.ToString("yyyy-MM-dd"));
                            }

                            Expression<Func<Microsoft.SharePoint.SPListItem, bool>> andExpr = null;
                            if (andConditions != null && andConditions.Count > 0)
                            {
                                andExpr = ExpressionsHelper.CombineAnd(andConditions);
                                var expressions = new List<Expression<Func<SPListItem, bool>>>();
                                expressions.Add(andExpr);
                                SPQuery caml = Camlex.Query().WhereAll(expressions).ToSPQuery();

                                //SPQuery caml = Camlex.Query().Where(x => (string)x[Fields.Title] == txtMaBienNhan.Text.Trim())
                                //                       .OrderBy(x => new[] { x["ID"] as Camlex.Desc })
                                //                       .ToSPQuery();
                                caml.RowLimit = 1;
                                var deNghiUrl = (web.ServerRelativeUrl + Constants.ListUrlDeNghiCapPhep).Replace("//", "/");
                                var deNghiList = web.GetList(deNghiUrl);
                                dataTable = deNghiList.GetItems(caml).GetDataTable();
                            }
                        }
                    }
                   
                });
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
            LoggingServices.LogMessage("End GetDeNghi - current user");
            return dataTable;
        }

        protected void repeaterLists_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                DataRowView rowView = (DataRowView)e.Item.DataItem;
                if (rowView != null)
                {
                    Literal literalNgayXuLy = (Literal)e.Item.FindControl("literalNgayXuLy");
                    literalNgayXuLy.Text = DateTime.Parse(rowView[Fields.NgayXuLy].ToString()).ToString("dd/MM/yyyy");

                    Literal literalNguoiXuLy = (Literal)e.Item.FindControl("literalNguoiXuLy");
                    literalNguoiXuLy.Text = rowView[Fields.NguoiXuLy].ToString();

                    Literal literalMoTa = (Literal)e.Item.FindControl("literalMoTa");
                    literalMoTa.Text = rowView[Fields.MoTa].ToString();
                }
            }
            catch (Exception ex)
            {
                LoggingServices.LogException(ex);
            }
        }
    }
}
