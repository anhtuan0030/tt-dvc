<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeNghiSearch.ascx.cs" Inherits="LongAn.DVC.WebParts.DeNghiSearch.DeNghiSearch" %>

<div>
	<div class="row">
		<div class="grid_12" id="main-frame">
			<h2 class="">
				TRA CỨU HỒ SƠ
			</h2>
			<div class="form" id="searchform">
				<div class="row line">
                    <div class="grid_">&nbsp;</div>
                    <div class="grid_2" style="margin-top:10px">
                       Mã biên nhận:                        
                    </div>
                    <div class="grid_4">
                        <asp:TextBox ID="txtMaBienNhan" runat="server"></asp:TextBox>
                    </div>
                    <div class="grid_2">
                        <asp:Button ID="btnTimKiem" runat="server" Text="Tìm kiếm" CssClass="button search btn btn-default" align="middle" />
                    </div>
                    <div class="clear"></div>
				</div>
			</div>
            <div>
                <h3><u>Kết quả tra cứu</u></h3>
                <div class="row" style="padding: 5px 0">
                    <div class="grid_1">&nbsp;</div>
                    <div class="grid_3">
                       Mã biên nhận:                        
                    </div>
                    <div class="grid_4">
                        <asp:Literal ID="literalMaBienNhan" runat="server"></asp:Literal>
                    </div>
                    <div class="clear"></div>
				</div>
                <div class="row" style="padding: 5px 0">
                    <div class="grid_1">&nbsp;</div>
                    <div class="grid_3">
                       Cá nhân / Tổ chức:                        
                    </div>
                    <div class="grid_4">
                        <asp:Literal ID="literalDonVi" runat="server"></asp:Literal>
                    </div>
                    <div class="clear"></div>
				</div>
                <div class="row" style="padding: 5px 0">
                    <div class="grid_1">&nbsp;</div>
                    <div class="grid_3">
                       Trạng thái xử lý:                        
                    </div>
                    <div class="grid_4">
                        <asp:Literal ID="literalTrangThai" runat="server"></asp:Literal>
                    </div>
                    <div class="clear"></div>
				</div>
            </div>
		</div>
	</div>
	<div class="clear"></div>
</div>