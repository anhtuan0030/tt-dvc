<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeNghiRedirect.ascx.cs" Inherits="LongAn.DVC.WebParts.DeNghiRedirect.DeNghiRedirect" %>

<div>
	<div class="row">
		<div class="grid_12">
			<h2>
				THÔNG BÁO
			</h2>
            <div class="form" id="searchform">
				<div class="row line">
                    Vui lòng đăng nhập để sử dụng chức năng đăng ký Dịch vụ công.
                </div>   
				<div class="row line">
                    Nếu chưa có tài khoản, bạn có thể đăng ký tại <a href="/Pages/DangKyTaiKhoan.aspx">đây</a>.
                </div>                       
            </div>
		</div>
	</div>
	<div class="clear"></div>
</div>