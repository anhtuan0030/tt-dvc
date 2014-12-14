<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBar" src="~/_controltemplates/15/ToolBar.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBarButton" src="~/_controltemplates/15/ToolBarButton.ascx" %>

<%@ Register Src="~/_controltemplates/15/LongAn.DVC/DeNghiCapPhepXeNewForm.ascx" TagPrefix="uc" TagName="DeNghiCapPhepXeNewForm" %>
<%@ Register Src="~/_controltemplates/15/LongAn.DVC/DeNghiCapPhepXeEditForm.ascx" TagPrefix="uc" TagName="DeNghiCapPhepXeEditForm" %>
<%@ Register Src="~/_controltemplates/15/LongAn.DVC/DeNghiCapPhepXeDispForm.ascx" TagPrefix="uc" TagName="DeNghiCapPhepXeDispForm" %>

<%@ Register Src="~/_controltemplates/15/LongAn.DVC/DeNghiNewForm.ascx" TagPrefix="uc" TagName="DeNghiNewForm" %>
<%@ Register Src="~/_controltemplates/15/LongAn.DVC/DeNghiEditForm.ascx" TagPrefix="uc" TagName="DeNghiEditForm" %>
<%@ Register Src="~/_controltemplates/15/LongAn.DVC/DeNghiDispForm.ascx" TagPrefix="uc" TagName="DeNghiDispForm" %>

<SharePoint:RenderingTemplate ID="DeNghiCapPhepXeNewTemplate" runat="server">
  <Template>
        <uc:DeNghiCapPhepXeNewForm ID="DeNghiCapPhepXeNewTemplate1" runat="server" />
  </Template>
</SharePoint:RenderingTemplate>

<SharePoint:RenderingTemplate ID="DeNghiCapPhepXeEditTemplate" runat="server">
  <Template>
        <uc:DeNghiCapPhepXeEditForm ID="DeNghiCapPhepXeEditTemplate1" runat="server" />
  </Template>
</SharePoint:RenderingTemplate>

<SharePoint:RenderingTemplate ID="DeNghiCapPhepXeDispTemplate" runat="server">
  <Template>
        <uc:DeNghiCapPhepXeDispForm ID="DeNghiCapPhepXeDispTemplate1" runat="server" />
  </Template>
</SharePoint:RenderingTemplate>

<SharePoint:RenderingTemplate ID="DeNghiNewTemplate" runat="server">
  <Template>
        <uc:DeNghiNewForm ID="DeNghiNewTemplate1" runat="server" />
  </Template>
</SharePoint:RenderingTemplate>

<SharePoint:RenderingTemplate ID="DeNghiEditTemplate" runat="server">
  <Template>
        <uc:DeNghiEditForm ID="DeNghiEditTemplate1" runat="server" />
  </Template>
</SharePoint:RenderingTemplate>

<SharePoint:RenderingTemplate ID="DeNghiDispTemplate" runat="server">
  <Template>
        <uc:DeNghiDispForm ID="DeNghiDispTemplate1" runat="server" />
  </Template>
</SharePoint:RenderingTemplate>