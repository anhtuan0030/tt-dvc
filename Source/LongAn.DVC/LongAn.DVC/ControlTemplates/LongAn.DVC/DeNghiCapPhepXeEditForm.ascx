<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeNghiCapPhepXeEditForm.ascx.cs" Inherits="LongAn.DVC.ControlTemplates.LongAn.DVC.DeNghiCapPhepXeEditForm" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBar" src="~/_controltemplates/15/ToolBar.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBarButton" src="~/_controltemplates/15/ToolBarButton.ascx" %>

<link rel="stylesheet" href="/_layouts/15/LongAn.DVC/css/bootstrap.min.css"/>
<!--[if lt IE9]>
    <link rel="stylesheet" href="/_layouts/15/LongAn.DVC/css/bootstrap-non-responsive.min.css"/>
<![endif]-->
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC/css/jquery-ui.structure.min.css"/>
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC/css/jquery-ui.min.css"/>
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC/css/bootstrap-theme.min.css"/>
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC/css/main.css"/>
<script type="text/javascript" src="/_layouts/15/LongAn.DVC/js/jquery.js"></script>
<script type="text/javascript" src="/_layouts/15/LongAn.DVC/js/jquery-ui.min.js"></script>
<script type="text/javascript" src="/_layouts/15/LongAn.DVC/js/datepicker-vi.js"></script>
<script type="text/javascript" src="/_layouts/15/LongAn.DVC/js/form.js"></script>

<style type="text/css">
    #s4-ribbonrow{
        display:none;
    }
</style>

<table>
    <tr>
	    <td>
		    <span id='part1'>
			    <SharePoint:InformationBar runat="server"/>
			    <div id="listFormToolBarTop" style="display:none;">
			        <wssuc:ToolBar CssClass="ms-formtoolbar" id="toolBarTbltop" RightButtonSeparator="&amp;#160;" runat="server">
					    <Template_RightButtons>
						    <SharePoint:NextPageButton runat="server"/>
						    <SharePoint:SaveButton runat="server"/>
						    <SharePoint:GoBackButton runat="server"/>
					    </Template_RightButtons>
			        </wssuc:ToolBar>
			    </div>
			    <SharePoint:FormToolBar runat="server"/>
			    <SharePoint:ItemValidationFailedMessage runat="server"/>
			    <table class="ms-formtable" style="margin-top: 8px; display:none;" border="0" cellpadding="0" cellspacing="0" width="100%">
			        <SharePoint:ChangeContentType runat="server"/>
			        <SharePoint:FolderFormFields runat="server"/>
			        <%--<SharePoint:ListFieldIterator runat="server"/>--%>
			        <SharePoint:ApprovalStatus runat="server"/>
			        <SharePoint:FormComponent TemplateName="AttachmentRows" ComponentRequiresPostback="false" runat="server"/>
			    </table>
                <div style="width:1024px;" class="container">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <h1>Đề nghị cấp phép lưu hành xe quá tải trọng, xe quá khổ</h1>
                            <div class="mr-form">
                                <div class="panel-1">
                                    <div class="row">
                                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                            <SharePoint:FieldLabel ID="FieldLabel1"  FieldName="KinhGui" ControlMode="Display"  runat="server"  />
                                        </div>
                                        <div class="field-required-data col-lg-9 col-md-9 col-sm-9 col-xs-9">
                                            <SharePoint:FormField  runat="server" ID="FormField1"  ControlMode="Display"  FieldName="KinhGui"  /> 
                                        </div>
                                        <%--<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                            <span class="star">(*)</span>
                                        </div>--%>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                            <SharePoint:FieldLabel ID="FieldLabel2" FieldName="CaNhanToChuc" ControlMode="Display" runat="server"  />
                                        </div>
                                        <div class="field-required-data col-lg-9 col-md-9 col-sm-9 col-xs-9">
                                            <SharePoint:FormField  runat="server" ID="FormField2" ControlMode="Display" FieldName="CaNhanToChuc" /> 
                                        </div>
                                        <%--<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                            <span class="star">(*)</span>
                                        </div>--%>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                            <SharePoint:FieldLabel ID="FieldLabel3" FieldName="DiaChi"  ControlMode="Display" runat="server"  />
                                        </div>
                                        <div class="field-required-data col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                            <SharePoint:FormField  runat="server" ID="FormField3" ControlMode="Display" FieldName="DiaChi" />
                                        </div>
                                        <%--<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                            <span class="star">(*)</span>
                                        </div>--%>
                                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                            <SharePoint:FieldLabel ID="FieldLabel4"  FieldName="DienThoai"  ControlMode="Display" runat="server"  />
                                        </div>
                                        <div class="field-required-data col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                            <SharePoint:FormField  runat="server" ID="FormField4" ControlMode="Display" FieldName="DienThoai" />
                                        </div>
                                        <%--<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                            <span class="star">(*)</span>
                                        </div>--%>
                                    </div>
                                    <h2>Phương tiện vận tải</h2>

                                    <div class="row">
                                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                            <SharePoint:FieldLabel ID="FieldLabel5"  FieldName="LoaiXe" ControlMode="Display" runat="server"  />
                                        </div>
                                        <div class="field-required-data col-lg-9 col-md-9 col-sm-9 col-xs-9">
                                            <SharePoint:FormField  runat="server" ID="FormField5" ControlMode="Display" FieldName="LoaiXe" />
                                        </div>
                                        <%--<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                            <span class="star">(*)</span>
                                        </div>--%>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                            <SharePoint:FieldLabel ID="FieldLabel6"  FieldName="NhanHieuXe" ControlMode="Display" runat="server"  />
                                        </div>
                                        <div class="field-required-data col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                            <SharePoint:FormField  runat="server" ID="FormField6" ControlMode="Display" FieldName="NhanHieuXe" />
                                        </div>
                                        <%--<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                            <span class="star">(*)</span>
                                        </div>--%>
                                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                            <SharePoint:FieldLabel ID="FieldLabel7"  FieldName="BienSoDangKy" ControlMode="Display" runat="server"  />
                                        </div>
                                        <div class="field-required-data col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                            <SharePoint:FormField  runat="server" ID="FormField7" ControlMode="Display" FieldName="BienSoDangKy" />
                                        </div>
                                        <%--<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                            <span class="star">(*)</span>
                                        </div>--%>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                                            <SharePoint:FieldLabel ID="FieldLabel8"  FieldName="NhanHieuRoMooc" ControlMode="Display" runat="server"  />
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                            <SharePoint:FormField  runat="server" ID="FormField8" ControlMode="Display" FieldName="NhanHieuRoMooc" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                                            <SharePoint:FieldLabel ID="FieldLabel9"  FieldName="BienSoDangKyRoMooc" ControlMode="Display" runat="server"  />
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                            <SharePoint:FormField  runat="server" ID="FormField9" ControlMode="Display" FieldName="BienSoDangKyRoMooc" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                                            <SharePoint:FieldLabel ID="FieldLabel10" FieldName="KichThuocBaoXe" ControlMode="Display" runat="server"  />
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                            <SharePoint:FormField  runat="server" ID="FormField10" ControlMode="Display" FieldName="KichThuocBaoXe" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                                            <SharePoint:FieldLabel ID="FieldLabel11"  FieldName="KichThuocBaoRoMooc" ControlMode="Display" runat="server"  />
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                            <SharePoint:FormField  runat="server" ID="FormField11" ControlMode="Display" FieldName="KichThuocBaoRoMooc" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                                            <SharePoint:FieldLabel ID="FieldLabel12"  FieldName="TaiTrongThietKeXe" ControlMode="Display" runat="server"  />
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                            <SharePoint:FormField  runat="server" ID="FormField12" ControlMode="Display" FieldName="TaiTrongThietKeXe" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                                            <SharePoint:FieldLabel ID="FieldLabel13"  FieldName="TaiTrongThietKeRoMooc" ControlMode="Display" runat="server"  />
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                            <SharePoint:FormField  runat="server" ID="FormField13" ControlMode="Display" FieldName="TaiTrongThietKeRoMooc" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                            <SharePoint:FieldLabel ID="FieldLabel14"  FieldName="TrongLuongBanThanXe" ControlMode="Display" runat="server"  />
                                        </div>
                                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                            <SharePoint:FormField  runat="server" ID="FormField14" ControlMode="Display" FieldName="TrongLuongBanThanXe" />
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                            <SharePoint:FieldLabel ID="FieldLabel15"  FieldName="TrongLuongBanThanRoMooc" ControlMode="Display"  runat="server"  />
                                        </div>
                                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                            <SharePoint:FormField  runat="server" ID="FormField15" ControlMode="Display" FieldName="TrongLuongBanThanRoMooc" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                            <SharePoint:FieldLabel ID="FieldLabel16"  FieldName="SoTrucCuaXe" ControlMode="Display"  runat="server"  />
                                        </div>
                                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                            <SharePoint:FormField  runat="server" ID="FormField16" ControlMode="Display" FieldName="SoTrucCuaXe" />
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                            <SharePoint:FieldLabel ID="FieldLabel17"  FieldName="SoTrucSauCuaXe" ControlMode="Display"  runat="server"  />
                                        </div>
                                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                            <SharePoint:FormField  runat="server" ID="FormField17" FieldName="SoTrucSauCuaXe"  ControlMode="Display"/>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                            <SharePoint:FieldLabel ID="FieldLabel18"  FieldName="SoTrucCuaRoMooc"  runat="server" ControlMode="Display"  />
                                        </div>
                                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                            <SharePoint:FormField  runat="server" ID="FormField18" FieldName="SoTrucCuaRoMooc" ControlMode="Display" />
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                            <SharePoint:FieldLabel ID="FieldLabel19"  FieldName="SoTrucSauCuaRoMooc"  runat="server" ControlMode="Display"  />
                                        </div>
                                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                            <SharePoint:FormField  runat="server" ID="FormField19" FieldName="SoTrucSauCuaRoMooc" ControlMode="Display" />
                                        </div>
                                    </div>
                                    <h2>Hàng hóa vận chuyển</h2>

                                    <div class="row">
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                            <SharePoint:FieldLabel ID="FieldLabel20"  FieldName="LoaiHang"  runat="server"  ControlMode="Display" />
                                        </div>
                                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                                            <SharePoint:FormField  runat="server" ID="FormField20" FieldName="LoaiHang"  ControlMode="Display"/>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                            <SharePoint:FieldLabel ID="FieldLabel21"  FieldName="TrongLuongHangXinCho"  runat="server" ControlMode="Display"  />
                                        </div>
                                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                                            <SharePoint:FormField  runat="server" ID="FormField21" FieldName="TrongLuongHangXinCho" ControlMode="Display" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                            <SharePoint:FieldLabel ID="FieldLabel22"  FieldName="ChieuRongToanBoXeKhiXepHang"  runat="server" ControlMode="Display"  />
                                        </div>
                                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                            <SharePoint:FormField  runat="server" ID="FormField22" FieldName="ChieuRongToanBoXeKhiXepHang" ControlMode="Display" />
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-1">
                                            <SharePoint:FieldLabel ID="FieldLabel23"  FieldName="HangVuotHaiBenThungXe"  runat="server" ControlMode="Display" />
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                            <SharePoint:FormField  runat="server" ID="FormField23" FieldName="HangVuotHaiBenThungXe" ControlMode="Display" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                            <SharePoint:FieldLabel ID="FieldLabel24"  FieldName="ChieuDaiToanBoXeKhiXepHang"  runat="server" ControlMode="Display"  />
                                        </div>
                                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                            <SharePoint:FormField  runat="server" ID="FormField24" FieldName="ChieuDaiToanBoXeKhiXepHang" ControlMode="Display" />
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-1">
                                            <SharePoint:FieldLabel ID="FieldLabel25"  FieldName="ChieuCaoToanBoXeKhiXepHang"  runat="server" ControlMode="Display"/>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                            <SharePoint:FormField  runat="server" ID="FormField25" FieldName="ChieuCaoToanBoXeKhiXepHang" ControlMode="Display"/>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                            <SharePoint:FieldLabel ID="FieldLabel26"  FieldName="HangVuotPhiaTruocThungXe"  runat="server" ControlMode="Display"/>
                                        </div>
                                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                            <SharePoint:FormField  runat="server" ID="FormField26" FieldName="HangVuotPhiaTruocThungXe" ControlMode="Display"/>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-1">
                                            <SharePoint:FieldLabel ID="FieldLabel27"  FieldName="HangVuotPhiaSauThungXe"  runat="server" ControlMode="Display"/>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                            <SharePoint:FormField  runat="server" ID="FormField27" FieldName="HangVuotPhiaSauThungXe" ControlMode="Display"/>
                                        </div>
                                    </div>
                                    <h3>Tải trọng lớn nhất được phân bố lên trục xe sau khi xếp hàng hóa lên xe:</h3>

                                    <div class="row">
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                            <SharePoint:FieldLabel ID="FieldLabel28"  FieldName="TrucDon"  runat="server" ControlMode="Display"/>
                                        </div>
                                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                            <SharePoint:FormField  runat="server" ID="FormField28" FieldName="TrucDon" ControlMode="Display"/>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                            <SharePoint:FieldLabel ID="FieldLabel29"  FieldName="TrucKep"  runat="server" ControlMode="Display"/>
                                        </div>
                                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                            <SharePoint:FormField  runat="server" ID="FormField29" FieldName="TrucKep" ControlMode="Display"/>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                            <SharePoint:FieldLabel ID="FieldLabel30"  FieldName="TrucBa"  runat="server" ControlMode="Display"/>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                            <SharePoint:FormField  runat="server" ID="FormField30" FieldName="TrucBa" ControlMode="Display"/>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                            <SharePoint:FieldLabel ID="FieldLabel31"  FieldName="KhoangCachGiuaHaiTamTruc"  runat="server" ControlMode="Display"/>
                                        </div>
                                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                            <SharePoint:FormField  runat="server" ID="FormField31" FieldName="KhoangCachGiuaHaiTamTruc" ControlMode="Display"/>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                            <SharePoint:FieldLabel ID="FieldLabel32"  FieldName="KhoangCachGiuaHaiTamTrucLienKe"  runat="server" ControlMode="Display"/>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                            <SharePoint:FormField  runat="server" ID="FormField32" FieldName="KhoangCachGiuaHaiTamTrucLienKe" ControlMode="Display"/>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                            <SharePoint:FieldLabel ID="FieldLabel33"  FieldName="NoiDi"  runat="server" ControlMode="Display"/>
                                        </div>
                                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                                            <SharePoint:FormField  runat="server" ID="FormField33" FieldName="NoiDi" ControlMode="Display"/>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                            <SharePoint:FieldLabel ID="FieldLabel34"  FieldName="NoiDen"  runat="server" ControlMode="Display"/>
                                        </div>
                                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                                            <SharePoint:FormField  runat="server" ID="FormField34" FieldName="NoiDen" ControlMode="Display"/>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-11 col-md-11 col-sm-11 col-xs-11">
                                            <SharePoint:FieldLabel ID="FieldLabel35"  FieldName="TuyenDuongVanChuyen"  runat="server"  ControlMode="Display"/>
                                            <SharePoint:FormField  runat="server" ID="FormField35" FieldName="TuyenDuongVanChuyen"  ControlMode="Display"/>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                            <SharePoint:FieldLabel ID="FieldLabel36"  FieldName="ThoiGiaDeNghiLuuHanhTu"  runat="server" ControlMode="Display" />
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                            <SharePoint:FormField  runat="server" ID="FormField36" CssClass="field-required" FieldName="ThoiGiaDeNghiLuuHanhTu" ControlMode="Display"/>                                        
                                            <%--<span class="star">(*)</span>--%>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                            <SharePoint:FieldLabel ID="FieldLabel37"  FieldName="ThoiGiaDeNghiLuuHanhDen"  runat="server"  />
                                        </div>
                                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                                            <SharePoint:FormField  runat="server" ID="FormField37" CssClass="field-required" FieldName="ThoiGiaDeNghiLuuHanhDen" ControlMode="Display"/>
                                            <%--<span class="star">(*)</span>--%>
                                        </div>
                                    </div>
                                    <%--<div class="row">
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                            <label for="trang-thai">Trạng thái</label>
                                        </div>
                                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                                            <select name="trang-thai" id="trang-thai">
                                                <option value="1">Đã active</option>
                                                <option value="0">Chưa active</option>
                                            </select>
                                        </div>
                                    </div>--%>
                                </div>

                                <div class="panel-2">
                                    <h5>Danh sách tập tin đính kèm cho hồ sơ</h5>
                                    <div class="row">
                                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                                            Bản sao giấy đăng ký hoặc giấy đăng ký tạm thời xe, xe đầu kéo, rơ moóc..
                                            <span title="This is a required field." class="ms-accentText"> *</span>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" id="divFileUpload1" runat="server">
                                            <asp:FileUpload ID="fileUpload1" runat="server" Visible="false"/>
                                        </div>
                                        <%--<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                            <span class="star">(*)</span>
                                        </div>--%>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                                            Giấy chứng nhận kiểm định an toàn kỹ thuật và bảo vệ môi trường phương tiện giao
                                                thông cơ giới đường bộ
                                            <span title="This is a required field." class="ms-accentText"> *</span>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" id="divFileUpload2" runat="server">
                                            <asp:FileUpload ID="fileUpload2" runat="server" Visible="false"/>
                                        </div>
                                        <%--<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                            <span class="star">(*)</span>
                                        </div>--%>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                                            Giấy cam kết của chủ phương tiện về quyền sở hữu phương tiện
                                            <span title="This is a required field." class="ms-accentText"> *</span>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" id="divFileUpload3" runat="server">
                                            <asp:FileUpload ID="fileUpload3" runat="server" Visible="false"/>
                                        </div>
                                        <%--<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                            <span class="star">(*)</span>
                                        </div>--%>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                                            Chứng minh nhân dân của người nộp
                                            <span title="This is a required field." class="ms-accentText"> *</span>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" id="divFileUpload4" runat="server">
                                            <asp:FileUpload ID="fileUpload4" runat="server" Visible="false"/>
                                        </div>
                                        <%--<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                            <span class="star">(*)</span>
                                        </div>--%>
                                    </div>
                                </div>
                                <div class="pull-right">
                                    <asp:Button ID="btnSave" OnClientClick="if (!PreSaveItem(1)) return false;" CssClass="button" runat="server" Text="Lưu" Visible="false" />
                                    <asp:Button ID="btnGuiHoSo" OnClientClick="if (!PreSaveItem(1)) return false;" runat="server" Text="Gửi hồ sơ" CssClass="button" Visible="false"/>
                                    <asp:Button ID="btnTrinhXuLy" runat="server" Text="Trình xử lý" CssClass="button" Visible="false"/>
                                    <asp:Button ID="btnPhanCongHoSo" runat="server" Text="Phân công hồ sơ" CssClass="button" Visible="false"/>
                                    <asp:Button ID="btnTrinhTruongPhong" runat="server" Text="Trình trưởng/phó P.QLHT" CssClass="button" Visible="false"/>
                                    <asp:Button ID="btnTrinhLanhDao" runat="server" Text="Trình lãnh đạo" CssClass="button" Visible="false"/>
                                    <asp:Button ID="btnDuyetHoSo" runat="server" Text="Duyệt hồ sơ" CssClass="button" Visible="false"/>
                                    <asp:Button ID="btnTraHoSo" runat="server" Text="Trả về" CssClass="button" Visible="false"/>
                                    <asp:Button ID="btnCancel" runat="server" Text="Đóng" CssClass="button" />
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                    </div>
                </div>
			    <table cellpadding="0" cellspacing="0" width="100%" style="padding-top: 7px;">
                    <tr>
                        <td width="100%">
			                <SharePoint:ItemHiddenVersion runat="server"/>
			                <SharePoint:ParentInformationField runat="server"/>
			                <SharePoint:InitContentType runat="server"/>
			                <wssuc:ToolBar CssClass="ms-formtoolbar" id="toolBarTbl" RightButtonSeparator="&amp;#160;" runat="server">
					            <Template_Buttons>
						            <SharePoint:CreatedModifiedInfo runat="server"/>
					            </Template_Buttons>
					            <Template_RightButtons>
						            <SharePoint:SaveButton runat="server" Visible="false" />
						            <SharePoint:GoBackButton runat="server" Visible="false" />
					            </Template_RightButtons>
			                </wssuc:ToolBar>
			            </td>
                    </tr>
			    </table>
		    </span>
	    </td>
	    <td valign="top">
		    <SharePoint:DelegateControl runat="server" ControlId="RelatedItemsPlaceHolder"/>
	    </td>
    </tr>
    </table>