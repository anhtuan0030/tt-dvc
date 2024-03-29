﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DanhSachThuTucHanhChinhUserControl.ascx.cs" Inherits="LongAn.DVC.HomeSite.WebParts.DanhSachThuTucHanhChinh.DanhSachThuTucHanhChinhUserControl" %>

<div class="main-box">
    <div class="main-box-title">
        DANH SÁCH THỦ TỤC HÀNH CHÍNH
    </div>
    <div class="main-box-content">
        <div class="tabs style-2" id="tab-thu-tuc-hanh-chinh">
            <div class="tab-titles">
                <asp:Literal ID="ltTabList" runat="server"></asp:Literal>
            </div>
            <div class="tab-contents">
                <asp:Literal ID="ltTabContent" runat="server"></asp:Literal>
            </div>
        </div>
    </div>
</div>
