<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs"
    Inherits="LongAn.DVC.FormsBasedAuthentication.ChangePassword" DynamicMasterPageFile="~masterurl/custom.master" %>

<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="FBA" Namespace="LongAn.DVC.FormsBasedAuthentication"
    Assembly="LongAn.DVC.FormsBasedAuthentication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9dba9f460226d31d" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    <SharePoint:EncodedLiteral ID="EncodedLiteral1" runat="server" Text="<%$ Resources:FBAPackWebPages, ChangePassword_Title %>"
        EncodeMethod='HtmlEncode' />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
    <SharePoint:EncodedLiteral ID="TitleArea" Text="<%$ Resources:FBAPackWebPages, ChangePassword_Title %>"  EncodeMethod="HtmlEncode"
        runat="server" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <FBA:ChangePasswordWebPart frametype="None" chrometype="None" id="PasswordWebPart" runat="server" 
        changepasswordtitletext="" />
</asp:Content>
