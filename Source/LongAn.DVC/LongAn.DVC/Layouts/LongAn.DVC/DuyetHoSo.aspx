<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DuyetHoSo.aspx.cs" Inherits="LongAn.DVC.Layouts.LongAn.DVC.DuyetHoSo" DynamicMasterPageFile="~masterurl/default.master" %>


<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <link rel="stylesheet" href="/_layouts/15/LongAn.DVC.Form/css/normalize.css"/>
    <link rel="stylesheet" href="/_layouts/15/LongAn.DVC.Form/css/fluid_grid.css"/>
    <link rel="stylesheet" href="/_layouts/15/LongAn.DVC.Form/css/jquery-ui.min.css"/>
    <link rel="stylesheet" href="/_layouts/15/LongAn.DVC.Form/css/superfish.css"/>
    <link rel="stylesheet" href="/_layouts/15/LongAn.DVC.Form/css/main.css"/>
    <%--<script type="text/javascript" src="/_layouts/15/LongAn.DVC/js/jquery.js"></script>--%>
    <script type="text/javascript" src="/_layouts/15/LongAn.DVC/js/laform.js"></script>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div class="container_12">
        <div class="row">
            <div class="grid_12" id="main-frame">
                <h1>Đề nghị cấp phép lưu hành xe quá tải trọng, xe quá khổ</h1>
                <div class="mr-form">
                    <div class="panel-2" id="divYeuCauBoSung" runat="server" >
                        <h2>Duyệt đề nghị cấp phép</h2>

                        <div class="row">
                            <div class="grid_2">
                                Tiêu đề
                                <span title="This is a required field." class="ms-accentText"> *</span>
                            </div>
                            <div class="grid_9">
                                <asp:TextBox ID="txtTieuDe" runat="server"></asp:TextBox>
                            </div>
                            <div class="clear"></div>
                        </div>

                        <div class="row">
                            <div class="grid_2">
                                Diễn giải chi tiết
                                <span title="This is a required field." class="ms-accentText"> *</span>
                            </div>
                            <div class="grid_9">
                                <asp:TextBox ID="txtDienGiaiChiTiet" TextMode="MultiLine" runat="server"></asp:TextBox>
                            </div>
                            <div class="clear"></div>
                        </div>
                    </div>

                    <div class="pull-right">
                        <asp:Button ID="btnSave" runat="server" Text="Đồng ý" CssClass="button" Visible="false" style="float:left;"/>
                        <asp:Button ID="btnCancel" runat="server" Text="Hủy" CssClass="button" style="float:left;"/>
                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="clearfix"></div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
Đề nghị cấp phép
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
Duyệt hồ sơ
</asp:Content>
