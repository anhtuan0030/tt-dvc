<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeNghiListViewUser.ascx.cs" Inherits="LongAn.DVC.WebParts.DeNghiListViewUser.DeNghiListViewUser" %>
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC/css/bootstrap.min.css"/>
<!--[if lt IE9]>
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC/css/bootstrap-non-responsive.min.css"/>
<![endif]-->
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC/css/jquery-ui.structure.min.css"/>
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC/css/jquery-ui.min.css"/>
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC/css/bootstrap-theme.min.css"/>
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC/css/form.main.css"/>
<script src="/_layouts/15/LongAn.DVC/js/jquery.js"></script>
<script src="/_layouts/15/LongAn.DVC/js/jquery-ui.min.js"></script>
<script src="/_layouts/15/LongAn.DVC/js/modernizr.js"></script>
<script src="/_layouts/15/LongAn.DVC/js/detectizr.min.js"></script>
<script src="/_layouts/15/LongAn.DVC/js/scripts.js"></script>
<div class="container1">
    <div class="row">
        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9">
            <h2 class="page-header">
                <asp:Literal ID="literalDeNghiTitle" runat="server"></asp:Literal>
            </h2>
            
            <div class="pull-right">
                <asp:LinkButton ID="lbtAddNew" CssClass="button add-new" runat="server">Thêm mới</asp:LinkButton>
            </div>
            <div class="clearfix"></div>

            <table class="the-table">
                <tr>
                    <th>STT</th>
                    <th>Loại đề nghị</th>
                    <th>Ngày đề nghị</th>
                    <th>Tình trạng</th>
                    <th>Xem</th>
                    <th>Sửa</th>
                    <th>Xóa</th>
                    <th>Nộp</th>
                </tr>
                <asp:Repeater ID="repeaterLists" runat="server"  OnItemCommand="repeaterLists_ItemCommand" OnItemDataBound="repeaterLists_ItemDataBound">
                    <ItemTemplate>
                        <tr class="<%#(((RepeaterItem)Container).ItemIndex+1) % 2 == 0 ? "odd" : "even" %>">
                            <td>
                                <asp:Literal ID="literalSTT" runat="server" Text="<%#(((RepeaterItem)Container).ItemIndex+1) %>"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal ID="literalLoaiCapPhep" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal ID="literalNgayDeNghi" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal ID="literalTrangThai" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbtViewItem" CssClass="button view just-icon" runat="server"></asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbtEditItem" CssClass="button edit just-icon" runat="server"></asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbtDeleteItem" CssClass="button remove danger just-icon" runat="server"></asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbtNopHoSo" CssClass="button up just-icon" runat="server"></asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            
            <div id="divPagging" visible="false" runat="server" class="pag pull-right">
                <%--<button class="button begin small"></button>--%>
                <asp:LinkButton ID="lbtnFirst" runat="server" CssClass="button begin small" CausesValidation="false" OnClick="lbtnFirst_Click"></asp:LinkButton>
                <%--<button class="button prev small"></button>--%>
                <asp:LinkButton ID="lbtnPrevious" runat="server" CssClass="button prev small" CausesValidation="false" OnClick="lbtnPrevious_Click"></asp:LinkButton>
                <asp:Repeater ID="repeaterPage" runat="server" OnItemCommand="repeaterPage_ItemCommand" OnItemDataBound="repeaterPage_ItemDataBound">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkbtnPaging" runat="server" CommandArgument='<%# Eval("PageIndex") %>'
                            CommandName="Paging" Text='<%# Eval("PageText") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:LinkButton ID="lbtnNext" runat="server" CssClass="button next small" CausesValidation="false"
                    OnClick="lbtnNext_Click"></asp:LinkButton>
                <%--<button class="button next small"></button>--%>
                <asp:LinkButton ID="lbtnLast" runat="server" CssClass="button end small" CausesValidation="false"
                    OnClick="lbtnLast_Click"></asp:LinkButton>
                <%--<button class="button end small"></button>--%>
                <div class="clearfix"></div>
            </div>
            
            <div class="clearfix"></div>
        </div>
    </div>
</div>