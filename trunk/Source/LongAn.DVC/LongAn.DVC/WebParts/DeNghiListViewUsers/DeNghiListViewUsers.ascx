<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeNghiListViewUsers.ascx.cs" Inherits="LongAn.DVC.WebParts.DeNghiListViewUsers.DeNghiListViewUsers" %>
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC.Form/css/normalize.css"/>
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC.Form/css/fluid_grid.css"/>
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC.Form/css/jquery-ui.min.css"/>
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC.Form/css/superfish.css"/>
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC.Form/css/main.css"/>
<script src="/_layouts/15/LongAn.DVC/js/jquery.js"></script>


<div class="container_12">
	<div class="row">
		<div class="grid_12" id="main-frame">
			<div id="top-control">
				<asp:HyperLink ID="hplTrangChu" CssClass="button large-button home" runat="server">Trang chủ</asp:HyperLink>
                <asp:HyperLink ID="hplDanhSachDeNghi" CssClass="button large-button done" runat="server">Danh sách đề nghị</asp:HyperLink>
                <asp:HyperLink ID="hplThongTinHuongDan" CssClass="button large-button stat" runat="server">Thông tin hướng dẫn</asp:HyperLink>
				<div class="clear"></div>
			</div>
			<h2 class="page-header">
				<asp:Literal ID="literalDeNghiTitle" runat="server"></asp:Literal>
			</h2>
		
			<div class="pull-right">
				<%--<asp:LinkButton ID="lbtAddNew" CssClass="button add-new" runat="server">Thêm mới</asp:LinkButton>--%>
                <asp:HyperLink ID="hplAddNew" CssClass="button add-new" runat="server">Thêm mới</asp:HyperLink>
			</div>
			<div class="clearfix"></div>
			
            <table class="the-table">
                <tr>
                    <th>STT</th>
                    <th>Mã biên nhận</th>
                    <th>Loại đề nghị</th>
                    <th>Ngày đề nghị</th>
                    <th>Tình trạng</th>
                    <th>Xem</th>
                    <th>Sửa</th>
                    <th>Xóa</th>
                    <th>Nộp</th>
                </tr>
                <asp:Repeater ID="repeaterLists" runat="server">
                    <ItemTemplate>
                        <tr class="<%#(((RepeaterItem)Container).ItemIndex+1) % 2 == 0 ? "even" : "odd" %>">
                            <td>
                                <asp:Literal ID="literalSTT" runat="server" Text="<%#(((RepeaterItem)Container).ItemIndex+1) %>"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal ID="literalMaBienNhan" runat="server"></asp:Literal>
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
                                <%--<asp:LinkButton ID="lbtViewItem" CssClass="button view just-icon" runat="server"></asp:LinkButton>--%>
                                <asp:HyperLink ID="lbtViewItem" ToolTip="Xem chi tiết đề nghị" CssClass="button view just-icon" runat="server"></asp:HyperLink>
                            </td>
                            <td>
                                <%--<asp:LinkButton ID="lbtEditItem" CssClass="button edit just-icon" runat="server"></asp:LinkButton>--%>
                                <asp:HyperLink ID="lbtEditItem" ToolTip="Chỉnh sửa đề nghị" CssClass="button view just-icon" runat="server"></asp:HyperLink>
                                <asp:LinkButton ID="lbtDisable1" ToolTip="Disabled" Enabled="false" CssClass="button just-icon" style="display:none;" runat="server"></asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbtDeleteItem" ToolTip="Xóa đề nghị" CssClass="button remove danger just-icon" runat="server"></asp:LinkButton>
                                <asp:LinkButton ID="lbtDisable2" ToolTip="Disabled" Enabled="false" CssClass="button just-icon" style="display:none;" runat="server"></asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbtNopHoSo" ToolTip="Nộp hồ sơ" CssClass="button up just-icon" runat="server"></asp:LinkButton>
                                <asp:LinkButton ID="lbtDisable3" ToolTip="Disabled" Enabled="false" CssClass="button just-icon" style="display:none;" runat="server"></asp:LinkButton>
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
			<div class="clear"></div>
		</div>
	</div>
	<div class="clear"></div>
</div>