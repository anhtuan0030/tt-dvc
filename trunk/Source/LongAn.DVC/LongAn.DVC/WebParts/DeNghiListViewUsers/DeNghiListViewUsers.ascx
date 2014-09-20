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
			<%--<div id="top-control">
				<a href="#" class="button large-button home">Trang chủ</a>
				<a href="#" class="button large-button add-new">Thêm mới hồ sơ</a>
				<a href="#" class="button large-button added">Hồ sơ đã tiếp nhận</a>
				<a href="#" class="button large-button process">Hồ sơ đang xử lý</a>
				<a href="#" class="button large-button waiting">Hồ sơ chờ cấp phép</a>
				<a href="#" class="button large-button done">Hồ sơ hoàn thành</a>
				<a href="#" class="button large-button stat">Báo cáo, thống kê</a>
				<div class="clear"></div>
			</div>--%>
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
			<div class="clear"></div>
		</div>
	</div>
	<div class="clear"></div>
</div>