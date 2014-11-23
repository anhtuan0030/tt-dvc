<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeNghiHistory.ascx.cs" Inherits="LongAn.DVC.WebParts.DeNghiHistory.DeNghiHistory" %>
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC.Form/css/normalize.css"/>
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC.Form/css/fluid_grid.css"/>
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC.Form/css/jquery-ui.min.css"/>
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC.Form/css/superfish.css"/>
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC.Form/css/main.css"/>

<style type="text/css">
    .button-custom {
        width: 120px !important;
    }
</style>
<div class="">
	<div class="row">
		<div class="grid_12" id="main-frame">
			<h2 class="">
				LỊCH SỬ LUÂN CHUYỂN HỒ SƠ
			</h2>
			<div class="form" id="searchform">
				<div class="row line">
                    <div class="grid_3" style="margin-top:10px; text-align:right;">
                       Nhập mã biên nhận:                        
                    </div>
                    <div class="grid_4">
                        <asp:TextBox ID="txtMaBienNhan" runat="server"></asp:TextBox>
                    </div>
                    <div class="grid_2">
                        <asp:Button ID="btnTimKiem" runat="server" Text="Tìm kiếm" CssClass="button search button-custom" align="middle" />
                    </div>
                    <div class="clear"></div>
				</div>
			</div>
            <div class="clearfix"></div>
			
            <table class="the-table">
                <tr style="height: 50px;">
                    <th>STT</th>
                    <th>Người xử lý</th>
                    <th>Ngày xử lý</th>
                    <th>Mô tả</th>
                </tr>
                <asp:Repeater ID="repeaterLists" runat="server">
                    <ItemTemplate>
                        <tr class="<%#(((RepeaterItem)Container).ItemIndex+1) % 2 == 0 ? "even" : "odd" %>">
                            <td>
                                <asp:Literal ID="literalSTT" runat="server" Text="<%#(((RepeaterItem)Container).ItemIndex+1) %>"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal ID="literalNguoiXuLy" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal ID="literalNgayXuLy" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal ID="literalMoTa" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
		</div>
	</div>
	<div class="clear"></div>
</div>