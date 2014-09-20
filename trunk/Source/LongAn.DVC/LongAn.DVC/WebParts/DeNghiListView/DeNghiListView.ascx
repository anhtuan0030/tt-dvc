<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeNghiListView.ascx.cs" Inherits="LongAn.DVC.WebParts.DeNghiListView.DeNghiListView" %>
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
                <asp:HyperLink ID="hplHoSoDaTiepNhan" CssClass="button large-button added" runat="server">Hồ sơ đã tiếp nhận</asp:HyperLink>
                <asp:HyperLink ID="hplHoSoChuaPhanCong" CssClass="button large-button process" runat="server">Hồ sơ chờ xử lý</asp:HyperLink>
                <asp:HyperLink ID="hplHoSoDaPhanCong" CssClass="button large-button waiting" runat="server">Hồ sơ đang xử lý</asp:HyperLink>
                <asp:HyperLink ID="hplHoSoChoDuyet" CssClass="button large-button done" runat="server">Hồ sơ chờ duyệt</asp:HyperLink>
                <asp:HyperLink ID="hplHoSoChoCapPhep" CssClass="button large-button stat" runat="server">Hồ sơ chờ cấp phép</asp:HyperLink>
                <asp:HyperLink ID="hplHoSoDaCapPhep" CssClass="button large-button stat" runat="server">Hồ sơ đã cấp phép</asp:HyperLink>
                <asp:HyperLink ID="hplHoSoBiTuChoi" CssClass="button large-button stat" Visible="false" runat="server">Hồ sơ bị từ chối</asp:HyperLink>
                <asp:HyperLink ID="hplThongKeBaoCao" CssClass="button large-button stat" runat="server">Báo cáo, thống kê</asp:HyperLink>
				<div class="clear"></div>
			</div>
			<h2 class="page-header">
				<asp:Literal ID="literalDeNghiTitle" runat="server"></asp:Literal>
			</h2>
			<div class="the-form" id="searchform">
				<div class="row line">
                    <div class="grid_1">&nbsp;</div>
					<div class="grid_1">
						Mã biên nhận:
					</div>
					<div class="grid_3">
						<asp:TextBox ID="txtTuKhoa" runat="server"></asp:TextBox>
					</div>
                    <div class="grid_1">&nbsp;</div>
					<div class="grid_1">
						Đơn vị:
					</div>
					<div class="grid_4">
						<asp:TextBox ID="txtCaNhanToChuc" runat="server"></asp:TextBox>
					</div>
					<div class="clear"></div>
				</div>
                <div class="row line">
                    <div class="grid_1">&nbsp;</div>
					<div class="grid_1">
						Số điện thoại:
					</div>
					<div class="grid_3">
						<asp:TextBox ID="txtSoDienThoai" runat="server"></asp:TextBox>
					</div>
                    <div class="grid_1">&nbsp;</div>
					<div class="grid_1">
						Ngày đề nghị:
					</div>
					<div class="grid_2">
						<SharePoint:DateTimeControl ID="dtcNgayDeNghiTu" DateOnly="true" LocaleId="1066" runat="server" />
					</div>
                    <div class="grid_2">
                        <SharePoint:DateTimeControl ID="dtcNgayDeNghiDen" DateOnly="true" LocaleId="1066" runat="server" />
                    </div>
					<div class="clear"></div>
				</div>
				<div class="row">
                    <div class="grid_5">&nbsp;</div>
					<div class="grid_2">
						    <asp:Button ID="btnTimKiem" runat="server" Text="Tìm kiếm" CssClass="button search" align="middle" />
					</div>
                    <div class="grid_5">&nbsp;</div>
					<div class="clear"></div>
				</div>
			</div>
			<div class="pull-right" id="divAddNew" visible="false" runat="server">
				<asp:LinkButton ID="lbtAddNew" CssClass="button add-new" runat="server">Thêm mới</asp:LinkButton>
			</div>
			<div class="clearfix"></div>
			<table class="the-table">
				<tr>
                    <th>STT</th>
                    <th>Biên nhận</th>
                    <th>Đơn vị đề nghị</th>
                    <th>Loại đề nghị</th>
                    <th>Ngày tạo</th>
                    <th colspan="3">Thao tác</th>
                </tr>
                <asp:Repeater ID="repeaterLists" runat="server"  OnItemCommand="repeaterLists_ItemCommand" OnItemDataBound="repeaterLists_ItemDataBound">
                    <ItemTemplate>
                        <tr class="<%#(((RepeaterItem)Container).ItemIndex+1) % 2 == 0 ? "even" : "odd" %>">
                            <td>
                                <asp:Literal ID="literalSTT" runat="server" Text="<%#(((RepeaterItem)Container).ItemIndex+1) %>"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal ID="literalTitle" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal ID="literalCaNhanToChuc" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal ID="literalLoaiCapPhep" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal ID="literalNgayDeNghi" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbtViewItem" ToolTip="Xem chi tiết" CssClass="button view just-icon" runat="server"></asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="lblDisable1" Enabled="false" ToolTip="Disable" CssClass="button view just-icon" style="display:block;" runat="server"></asp:LinkButton>
                                <asp:LinkButton ID="lbtChuyenTruongPhong" ToolTip="Chuyển trưởng/phó P.QLHT" CssClass="button up just-icon" style="display:none;" runat="server"></asp:LinkButton>
                                <asp:LinkButton ID="lbtTrinhTruongPhoPQLHT" ToolTip="Trình trưởng/phó P.QLHT" CssClass="button up just-icon" style="display:none;" runat="server"></asp:LinkButton>
                                <asp:LinkButton ID="lbtTrinhLanhDaoSo" ToolTip="Trình lãnh đạo duyệt" CssClass="button up just-icon" style="display:none;" runat="server"></asp:LinkButton>
                                <asp:LinkButton ID="lbtDuyetHoSo" ToolTip="Duyệt hồ sơ" CssClass="button up just-icon" style="display:none;" runat="server"></asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="lblDisable2" Enabled="false" ToolTip="Disable" CssClass="button view just-icon" style="display:block;" runat="server"></asp:LinkButton>
                                <asp:LinkButton ID="lbtYeuCauBoSung" ToolTip="Yêu cầu bổ sung hồ sơ" CssClass="button edit just-icon" style="display:none;" runat="server"></asp:LinkButton>
                                <asp:LinkButton ID="lbtPhanCongHoSo" ToolTip="Phân công hồ sơ" CssClass="button edit just-icon" style="display:none;" runat="server"></asp:LinkButton>
                                <asp:LinkButton ID="lbtTuChoiHoSo" ToolTip="Trả / Từ chối duyệt" CssClass="button edit just-icon" style="display:none;" runat="server"></asp:LinkButton>
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