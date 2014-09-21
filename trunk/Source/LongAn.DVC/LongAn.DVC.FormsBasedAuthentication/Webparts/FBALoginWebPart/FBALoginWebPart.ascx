<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.SharePoint.IdentityModel, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FBALoginWebPart.ascx.cs" Inherits="LongAn.DVC.FormsBasedAuthentication.Webparts.FBALoginWebPart.FBALoginWebPart" %>

<asp:Login ID="Login1" runat="server" Width="100%"
    FailureText="Đăng nhập không thành công!" 
    onauthenticate="Login1_Authenticate" CreateUserText="Đăng ký" CreateUserUrl="/Pages/DangKyTaiKhoan.aspx" DisplayRememberMe="False" LoginButtonText="Đăng nhập" PasswordLabelText="Mật khẩu:" RememberMeText="Nhớ mặt khẩu" TitleText="Đăng nhập" UserNameLabelText="Tên đăng nhập:" UserNameRequiredErrorMessage="Vui lòng nhập Tên đăng nhập." PasswordRequiredErrorMessage="Vui lòng nhập Mật khẩu." PasswordRecoveryText="Quên mật khẩu ?" PasswordRecoveryUrl="/">
    <LayoutTemplate>
        <div class="box icon-1">
            <div class="box-title">
                Đăng nhập
            </div>
            <div class="box-content form">
                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Tên đăng nhập:</asp:Label>
                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="Vui lòng nhập Tên đăng nhập." ToolTip="Vui lòng nhập Tên đăng nhập." ValidationGroup="ctl00$Login1">*</asp:RequiredFieldValidator>
                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Mật khẩu:</asp:Label>
                <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Vui lòng nhập Mật khẩu." ToolTip="Vui lòng nhập Mật khẩu." ValidationGroup="ctl00$Login1">*</asp:RequiredFieldValidator>
                <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                <div class="row">
                    <div class="grid_6">
                        <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Đăng nhập" CssClass="btn btn-default" ValidationGroup="ctl00$Login1" />
                    </div>
                    <div class="clear"></div>
                </div>
                <ul class="bullet-1">
                    <li><asp:HyperLink ID="CreateUserLink" runat="server" NavigateUrl="/Pages/DangKyTaiKhoan.aspx">Đăng ký tài khoản</asp:HyperLink></li>
                    <li><asp:HyperLink ID="PasswordRecoveryLink" runat="server" NavigateUrl="/Pages/KhoiPhucMatKhau.aspx">Quên mật khẩu ?</asp:HyperLink></li>
                </ul>
            </div>
        </div>
    </LayoutTemplate>
</asp:Login>