<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TraCuuThuTucHanhChinhUserControl.ascx.cs" Inherits="LongAn.DVC.HomeSite.WebParts.TraCuuThuTucHanhChinh.TraCuuThuTucHanhChinhUserControl" %>

<div class="main-box">
    <div class="main-box-title">
        TRA CỨU THỦ TỤC HÀNH CHÍNH
    </div>
    <div class="main-box-content">
        <div class="field-row">
            <label for="loai-co-quan">Loại cơ quan thực hiện</label>
            <asp:DropDownList ID="ddlLoaiCoQuan" runat="server"></asp:DropDownList>
            <div class="clear"></div>
        </div>
        <div class="field-row">
            <label for="co-quan-thuc-hien">Cơ quan thực hiện</label>
            <asp:DropDownList ID="ddlCoQuan" runat="server"></asp:DropDownList>
            <div class="clear"></div>
        </div>
        <div class="field-row">
            <label for="linh-vuc">Lĩnh vực</label>
            <asp:DropDownList ID="ddlLinhVuc" runat="server"></asp:DropDownList>
            <div class="clear"></div>
        </div>
        <div class="field-row">
            <label for="muc-do">Mức độ</label>
            <asp:DropDownList ID="ddlMucDo" runat="server"></asp:DropDownList>
            <div class="clear"></div>
        </div>
        <div class="field-row">
            <label for="ten-thu-tuc">Từ khóa</label>
            <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
            <div class="clear"></div>
        </div>
        <div class="field-row field-set">
            <asp:Button ID="btnSearch" runat="server" Text="Tra cứu" CssClass="btn button search" OnClick="btnSearch_Click"/>
            <asp:Button ID="btnRefresh" runat="server" Text="Làm mới" CssClass="btn button refresh"/>
            <div class="clear"></div>
        </div>

        <asp:Literal ID="ltNotification" runat="server"></asp:Literal>

        <div class="text-right">
            <asp:Literal ID="ltTotalRecords" runat="server"></asp:Literal>
        </div>

        <asp:Literal ID="ltResults" runat="server"></asp:Literal>
    </div>
</div>
