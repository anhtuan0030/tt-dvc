<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeNghiReport.ascx.cs" Inherits="LongAn.DVC.WebParts.DeNghiReport.DeNghiReport" %>

<div>
	<div class="row">
		<div class="grid_12">
			<h2>
				BÁO CÁO CẤP PHÉP LƯU HÀNH
			</h2>
            <div class="form" id="searchform">
				<div class="row line">
                    <div class="grid_2" style="margin-top:10px">
                       Từ ngày:                        
                    </div>
                    <div class="grid_4">
                        <SharePoint:DateTimeControl ID="dtcFromDate" DateOnly="true" LocaleId="1066" IsRequiredField="true" ErrorMessage="Vui lòng chọn ngày hợp lệ" runat="server" />
                    </div>
                    <div class="grid_2" style="margin-top:10px">
                       Đến ngày:                        
                    </div>
                    <div class="grid_4">
                        <SharePoint:DateTimeControl ID="dtcToDate" DateOnly="true" LocaleId="1066" IsRequiredField="true" ErrorMessage="Vui lòng chọn ngày hợp lệ" runat="server" />
                    </div>
                    <div class="clear"></div>
				</div>
                <div>
                    <div class="grid_2">
                        <asp:Button ID="btnExportExcel" runat="server" Text="Xuất báo cáo" CssClass="button btnExportExcel" align="middle" OnClick="btnExportExcel_Click" />
                    </div>
                    <div class="clear"></div>
                </div>
            </div>
		</div>
	</div>
	<div class="clear"></div>
</div>

<script type="text/javascript">
    $(function () {
        $(".btnExportExcel").click(function () {
            _spFormOnSubmitCalled = false;
        });
    });
</script>