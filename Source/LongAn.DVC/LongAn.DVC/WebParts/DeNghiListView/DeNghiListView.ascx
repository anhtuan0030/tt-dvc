﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
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
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC.Form/css/main1.css"/>
<link href="/_layouts/15/LongAn.DVC.Form/css/form.main.custom.css" rel="stylesheet" />

<script>
    $(document).ready(function () {
        var searchExpandValue = $("[id$='_hdfSearchExpand']").val();
        if (searchExpandValue == "1")
        {
            $("#linkSearchExpand").addClass("expanded");
            $("#searchform").show();
        }
        $("#linkSearchExpand").click(function () {
            $("#searchform").toggle("slow");
            if (searchExpandValue == "1") {
                $("[id$='_hdfSearchExpand']").val("0");
                $("#linkSearchExpand").removeClass("expanded");
            }
            else {
                $("[id$='_hdfSearchExpand']").val("1");
                $("#linkSearchExpand").addClass("expanded");
            }
        });
    });
</script>

<script type="text/javascript">
    //fix: Ajax second postback not working in Sharepoint
    _spOriginalFormAction = document.forms[0].action;
    _spSuppressFormOnSubmitWrapper = true;
</script>
<%--<div class="container_12">--%>
    <div class="row">
		<div class="grid_12" id="main-frame">
			<div id="top-control">
				<asp:HyperLink ID="hplTrangChu" CssClass="button large-button home" Visible="false" runat="server">Trang chủ</asp:HyperLink>
                <asp:HyperLink ID="hplDanhSachDeNghi" CssClass="button large-button done" Visible="false" runat="server">Danh sách đề nghị</asp:HyperLink>
                <asp:HyperLink ID="hplHoSoChoTiepNhan" CssClass="button large-button add-new" runat="server">
                    Hồ sơ chờ<br/>tiếp nhận
                    <asp:Literal ID="literalChoTiepNhan" Text="(0)" runat="server"></asp:Literal>
                </asp:HyperLink>
                <asp:HyperLink ID="hplHoSoDaTiepNhan" CssClass="button large-button add-finish" runat="server">Hồ sơ đã<br/>tiếp nhận
                    <asp:Literal ID="literalDaTiepNhan" Text="(0)" runat="server"></asp:Literal>
                </asp:HyperLink>
                <asp:HyperLink ID="hplHoSoChoXuLy" CssClass="button large-button waiting" runat="server">Hồ sơ chờ<br/>xử lý
                    <asp:Literal ID="literalChoXuLy" Text="(0)" runat="server"></asp:Literal>
                </asp:HyperLink>
                <asp:HyperLink ID="hplHoSoDangXuLy" CssClass="button large-button process" runat="server">Hồ sơ đang<br/>xử lý
                    <asp:Literal ID="literalDangXuLy" Text="(0)" runat="server"></asp:Literal>
                </asp:HyperLink>
                <asp:HyperLink ID="hplHoSoChoBoSung" CssClass="button large-button waiting-add" runat="server">Hồ sơ chờ<br/>bổ sung
                    <asp:Literal ID="literalChoBoSung" Text="(0)" runat="server"></asp:Literal>
                </asp:HyperLink>
                <asp:HyperLink ID="hplHoSoChoDuyet" CssClass="button large-button waiting" runat="server">Hồ sơ<br/>chờ duyệt
                    <asp:Literal ID="literalChoDuyet" Text="(0)" runat="server"></asp:Literal>
                </asp:HyperLink>
                <asp:HyperLink ID="hplHoSoChoCapPhep" CssClass="button large-button waiting-approval" runat="server">Hồ sơ chờ<br/>cấp phép
                    <asp:Literal ID="literalChoCapPhep" Text="(0)" runat="server"></asp:Literal>
                </asp:HyperLink>
                <asp:HyperLink ID="hplHoSoDuocCapPhep" CssClass="button large-button done" runat="server">Hồ sơ được<br/>cấp phép
                    <asp:Literal ID="literalDuocCapPhep" Text="(0)" runat="server"></asp:Literal>
                </asp:HyperLink>
                <asp:HyperLink ID="hplHoSoBiTuChoi" CssClass="button large-button rejected" runat="server">Hồ sơ bị<br/>từ chối
                    <asp:Literal ID="literalBiTuChoi" Text="(0)" runat="server"></asp:Literal>
                </asp:HyperLink>
                <asp:HyperLink ID="hplHoSoDaHoanThanh" CssClass="button large-button approval" runat="server">Hồ sơ đã<br/>hoàn thành
                    <asp:Literal ID="literalDaHoanThanh" Text="(0)" runat="server"></asp:Literal>
                </asp:HyperLink>
                <asp:HyperLink ID="hplHoSoChuaHoanThanh" CssClass="button large-button none-finish" runat="server">Hồ sơ chưa<br/>hoàn thành
                    <asp:Literal ID="literalChuaHoanThanh" Text="(0)" runat="server"></asp:Literal>
                </asp:HyperLink>
                <asp:HyperLink ID="hplThongKeBaoCao" CssClass="button large-button stat" Visible="false" runat="server">Báo cáo<br/>thống kê</asp:HyperLink>
				<div class="clear"></div>
			</div>
            <div class="row">
                <div class="pull-left">
                    <h2 class="page-header">
				        <asp:Literal ID="literalDeNghiTitle" runat="server"></asp:Literal>
			        </h2>
                </div>
                <div class="pull-right">
                    <div class="pull-left-search">
                        <a href="/Pages/LichSuLuanChuyenHoSo.aspx" class="button task inline-block" style="padding-left:30px; margin-right:10px;">Lịch sử luân chuyển hồ sơ</a>
                        <a id="linkSearchExpand" href="#" class="button button-expand inline-block">Tìm kiếm</a>
                        <asp:HiddenField ID="hdfSearchExpand" Value="0" runat="server" />
                    </div>
                    <div class="pull-left-add" id="divAddNew" runat="server" visible="false">
                        <asp:HyperLink ID="hplAddNew" CssClass="button add-new inline-block danger" runat="server">Thêm mới</asp:HyperLink>
			        </div>
                </div>
                
                <div class="clear"></div>
            </div>
			
			<div class="the-form" id="searchform" style="display:none;">
				<div class="row line">
					<div class="grid_2" style="text-align:right;">
						Mã biên nhận:
					</div>
					<div class="grid_3">
						<asp:TextBox ID="txtTuKhoa" runat="server"></asp:TextBox>
					</div>

					<div class="grid_2" style="text-align:right;">
						Đơn vị:
					</div>
					<div class="grid_4">
						<asp:TextBox ID="txtCaNhanToChuc" runat="server"></asp:TextBox>
					</div>
					<div class="clear"></div>
				</div>
                <div class="row line">
					<div class="grid_2" style="text-align:right;">
						Số điện thoại:
					</div>
					<div class="grid_3">
						<asp:TextBox ID="txtSoDienThoai" runat="server"></asp:TextBox>
					</div>
                    
					<div class="grid_2" style="text-align:right;">
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
				<div class="row line">
                    <div class="grid_5">&nbsp;</div>
					<div class="grid_2">
						<asp:Button ID="btnTimKiem" runat="server" Text="Tìm kiếm" CssClass="button search" align="middle" />
					</div>
                    <div class="grid_5">&nbsp;</div>
					<div class="clear"></div>
				</div>
			</div>
			
			<div class="clearfix"></div>
			<table class="the-table">
				<tr style="height: 50px;">
                    <th>STT</th>
                    <th>Biên nhận</th>
                    <th>Đơn vị đề nghị</th>
                    <th>Loại đề nghị</th>
                    <th>Ngày nộp hồ sơ</th>
                    <th>Trạng thái</th>
                    <th colspan="5">Thao tác</th>
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
                                <asp:Literal ID="literalTrangThai" runat="server"></asp:Literal>
                            </td>
                            
                            <td>
                                <asp:HyperLink ID="hplViewItem" ToolTip="Xem thông tin hồ sơ" CssClass="button view just-icon" runat="server"></asp:HyperLink>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbtDisable1" Enabled="false" ToolTip="Disable" CssClass="button just-icon" style="display:block;" runat="server"></asp:LinkButton>
                                <asp:LinkButton ID="lbtTiepNhan" ToolTip="Tiếp nhận hồ sơ" CssClass="button edit just-icon" style="display:none;" runat="server"></asp:LinkButton>
                                <asp:LinkButton ID="lbtHoanThanh" ToolTip="Xác nhận hồ sơ đã được cấp phép" CssClass="button done just-icon" style="display:none;" runat="server"></asp:LinkButton>
                                <asp:LinkButton ID="lbtPrint" ToolTip="In Biên nhận / Giấy cấp phép" CssClass="button printer just-icon" style="display:none;" runat="server"></asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbtDisable2" Enabled="false" ToolTip="Disable" CssClass="button just-icon" style="display:block;" runat="server"></asp:LinkButton>
                                <%--<asp:LinkButton ID="lbtChuyenTruongPhong" ToolTip="Chuyển trưởng/phó P.QLHT" CssClass="button send-to-manager just-icon" style="display:none;" runat="server"></asp:LinkButton>--%>
                                <asp:HyperLink ID="hplChuyenTruongPhong" ToolTip="Chuyển trưởng/phó P.QLHT" CssClass="button up just-icon" style="display:none;" runat="server"></asp:HyperLink>

                                <%--<asp:LinkButton ID="lbtTrinhTruongPhoPQLHT" ToolTip="Trình trưởng/phó P.QLHT" CssClass="button up just-icon" style="display:none;" runat="server"></asp:LinkButton>--%>
                                <asp:HyperLink ID="hplTrinhTruongPhoPQLHT" ToolTip="Trình trưởng/phó P.QLHT" CssClass="button up just-icon" style="display:none;" runat="server"></asp:HyperLink>
                                <%--<asp:LinkButton ID="lbtTrinhLanhDaoSo" c></asp:LinkButton>--%>
                                <asp:HyperLink ID="hplTrinhLanhDaoSo" ToolTip="Trình trưởng/phó P.QLHT" CssClass="button up just-icon" style="display:none;" runat="server"></asp:HyperLink>
                                <%--<asp:LinkButton ID="lbtDuyetHoSo" ToolTip="Trình trưởng/phó P.QLHT" CssClass="button up just-icon" style="display:none;" runat="server"></asp:LinkButton>--%>
                                <asp:HyperLink ID="hplDuyetHoSo" ToolTip="Trình trưởng/phó P.QLHT" CssClass="button up just-icon" style="display:none;" runat="server"></asp:HyperLink>
                                <asp:LinkButton ID="lbtChuaHoanThanh" ToolTip="Xác nhận hồ sơ chưa hoàn thành" CssClass="button none-finish just-icon" style="display:none;" runat="server"></asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbtDisable3" Enabled="false" ToolTip="Disable" CssClass="button just-icon" style="display:block;" runat="server"></asp:LinkButton>
                                <asp:HyperLink ID="hplYeuCauBoSung" ToolTip="Yêu cầu bổ sung hồ sơ" CssClass="button waiting-add just-icon" style="display:none;" runat="server"></asp:HyperLink>
                                <asp:HyperLink ID="hplPhanCongHoSo" ToolTip="Phân công hồ sơ" CssClass="button edit just-icon" style="display:none;" runat="server"></asp:HyperLink>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbtDisable4" Enabled="false" ToolTip="Disable" CssClass="button just-icon" style="display:block;" runat="server"></asp:LinkButton>
                                <asp:HyperLink ID="hplTuChoiHoSo" ToolTip="Trả / Từ chối duyệt" CssClass="button rejected-2 just-icon" style="display:none;" runat="server"></asp:HyperLink>
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
<%--</div>--%>

	