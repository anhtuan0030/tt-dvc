<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucUserAuthenticationBox.ascx.cs" Inherits="LongAn.DVC.ControlTemplates.LongAn.DVC.ucUserAuthenticationBox" %>
<asp:Panel ID="pnlUserInfoBox" runat="server" Visible="false" CssClass="hidden-box">
<script type="text/javascript">
    $(function () {
        var newLI = "<li style='float:right'><a class='menuUserInfo' href='/_layouts/signout.aspx'>Thoát</a></li>";
        newLI += "<li style='float:right'><a class='menuUserInfo'>Xin chào, <%: currentUsername %></a></li>";

        $(".sf-menu").append(newLI);
    });
</script>
</asp:Panel>
