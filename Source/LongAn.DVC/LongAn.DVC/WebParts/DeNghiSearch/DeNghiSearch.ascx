<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeNghiSearch.ascx.cs" Inherits="LongAn.DVC.WebParts.DeNghiSearch.DeNghiSearch" %>

<link rel="stylesheet" href="/_layouts/15/LongAn.DVC.Form/css/main.css"/>

<style type="text/css">
    .button-custom {
        width: 120px !important;
    }
</style>
<div class="row">
	<div class="grid_12" id="main-frame">
		<h1 class="">
			TRA CỨU HỒ SƠ
		</h1>
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
	</div>
</div>

<div class="row">
    <div class="grid_12">
        <h1>Kết quả tra cứu</h1>
        <div class="mr-form">
            <div class="panel-2" >
                <h2>Thông tin hồ sơ</h2>
                <div class="row">
                    <div class="grid_3">
                        Số biên nhận      
                    </div>
                    <div class="grid_9">
                        <asp:Label ID="lblSoBienNhan" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="clear"></div>
                </div>
                <div class="row">
                    <div class="grid_3">
                        Ngày nhận hồ sơ      
                    </div>
                    <div class="grid_9">
                        <asp:Label ID="lblNgayNhanHoSo" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="clear"></div>
                </div>
                <div class="row">
                    <div class="grid_3">
                        Ngày hẹn trả hồ sơ      
                    </div>
                    <div class="grid_9">
                        <asp:Label ID="lblNgayHenTra" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="clear"></div>
                </div>
                <div class="row">
                    <div class="grid_3">
                        Ngày thực trả hồ sơ      
                    </div>
                    <div class="grid_9">
                        <asp:Label ID="lblNgayThucTra" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="clear"></div>
                </div>
                <div class="row">
                    <div class="grid_3">
                        Tình trạng hồ sơ      
                    </div>
                    <div class="grid_9">
                        <asp:Label ID="lblTinhTrangHoSo" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="clear"></div>
                </div>
                <div class="row">
                    <div class="grid_3">
                        Số ngày trễ hạn
                    </div>
                    <div class="grid_9">
                        <asp:Label ID="lblSoNgayTreHan" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="clear"></div>
                </div>
            </div>

            <div class="panel-2" >
                <h2>Thông tin người nộp hồ sơ</h2>
                <div class="row">
                    <div class="grid_3">
                        Họ tên người nhận
                    </div>
                    <div class="grid_9">
                        <asp:Label ID="lblCaNhanToChuc" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="clear"></div>
                </div>
                <div class="row">
                    <div class="grid_3">
                        Địa chỉ     
                    </div>
                    <div class="grid_9">
                        <asp:Label ID="lblDiaChi" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="clear"></div>
                </div>
                <div class="row">
                    <div class="grid_3">
                        Điện thoại
                    </div>
                    <div class="grid_9" >
                        <asp:Label ID="lblDienThoai" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="clear"></div>
                </div>
            </div>

            <div class="panel-2" >
                <h2>Quá trình thụ hồ sơ</h2>
                <div class="row">
                    <div class="grid_12">
                        <table class="the-table">
                            <tr style="height: 50px;">
                                <th>STT</th>
                                <th>Ngày xử lý</th>
                                <th>Người xử lý</th>
                                <th>Mô tả</th>
                            </tr>
                            <asp:Repeater ID="repeaterLists" runat="server" OnItemDataBound="repeaterLists_ItemDataBound">
                                <ItemTemplate>
                                    <tr class="<%#(((RepeaterItem)Container).ItemIndex+1) % 2 == 0 ? "even" : "odd" %>">
                                        <td>
                                            <asp:Literal ID="literalSTT" runat="server" Text="<%#(((RepeaterItem)Container).ItemIndex+1) %>"></asp:Literal>
                                        </td>
                                        <td>
                                            <asp:Literal ID="literalNgayXuLy" runat="server"></asp:Literal>
                                        </td>
                                        <td>
                                            <asp:Literal ID="literalNguoiXuLy" runat="server"></asp:Literal>
                                        </td>
                                        <td>
                                            <asp:Literal ID="literalMoTa" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>

                    <div class="clear"></div>
                </div>
            </div>
        </div>
    </div>
</div>