<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.SharePoint.IdentityModel, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FBALoginWebPart.ascx.cs" Inherits="LongAn.DVC.FormsBasedAuthentication.Webparts.FBALoginWebPart.FBALoginWebPart" %>

<asp:Login ID="Login1" runat="server" 
    FailureText="Đăng nhập không thành công!" 
    onauthenticate="Login1_Authenticate" CreateUserText="Đăng ký" CreateUserUrl="/registeraccount" DisplayRememberMe="False" LoginButtonText="Đăng nhập" PasswordLabelText="Mật khẩu:" RememberMeText="Nhớ mặt khẩu" TitleText="Đăng nhập" UserNameLabelText="Tên đăng nhập:" UserNameRequiredErrorMessage="Vui lòng nhập Tên đăng nhập." PasswordRequiredErrorMessage="Vui lòng nhập Mật khẩu." PasswordRecoveryText="Quên mật khẩu ?" PasswordRecoveryUrl="/">
    <LayoutTemplate>
        <table cellpadding="1" cellspacing="0" style="border-collapse:collapse;">
            <tr>
                <td>
                    <table cellpadding="0">
                        <tr>
                            <td align="center" colspan="2">Đăng nhập</td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Tên đăng nhập:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="Vui lòng nhập Tên đăng nhập." ToolTip="Vui lòng nhập Tên đăng nhập." ValidationGroup="ctl00$Login1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Mật khẩu:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Vui lòng nhập Mật khẩu." ToolTip="Vui lòng nhập Mật khẩu." ValidationGroup="ctl00$Login1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="color:Red;">
                                <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2">
                                <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Đăng nhập" ValidationGroup="ctl00$Login1" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:HyperLink ID="CreateUserLink" runat="server" NavigateUrl="/registeraccount">Đăng ký</asp:HyperLink>
<br />
                                <asp:HyperLink ID="PasswordRecoveryLink" runat="server" NavigateUrl="/">Quên mật khẩu ?</asp:HyperLink>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </LayoutTemplate>
</asp:Login>