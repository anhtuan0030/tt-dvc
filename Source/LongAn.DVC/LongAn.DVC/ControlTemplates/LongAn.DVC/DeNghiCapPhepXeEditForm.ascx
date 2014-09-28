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

<link rel="stylesheet" href="/_layouts/15/LongAn.DVC.Form/css/normalize.css"/>
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC.Form/css/fluid_grid.css"/>
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC.Form/css/jquery-ui.min.css"/>
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC.Form/css/superfish.css"/>
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC.Form/css/main.css"/>
<script type="text/javascript" src="/_layouts/15/LongAn.DVC/js/jquery.js"></script>
<script type="text/javascript" src="/_layouts/15/LongAn.DVC/js/jquery-ui.min.js"></script>
<script type="text/javascript" src="/_layouts/15/datepicker.js"></script>
<script type="text/javascript" src="/_layouts/15/LongAn.DVC/js/laform.js"></script>

<style type="text/css">
    #s4-ribbonrow{
        display:none;
    }
    #sideNavBox {
        display:none;
    }

</style>

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
    <div class="container_12">
        <div class="row">
            <div class="grid_12" id="main-frame">
                <h1>Đề nghị cấp phép lưu hành xe quá tải trọng, xe quá khổ</h1>
                <div class="mr-form">
                    <div class="panel-1">
                        <div class="row">
                        <div class="grid_2">
                            <SharePoint:FieldLabel ID="FieldLabel1"  FieldName="KinhGui"  runat="server"  />
                        </div>
                        <div class="field-required-data grid_9">
                            <SharePoint:FormField  runat="server" ID="FormField1"  FieldName="KinhGui"  /> 
                        </div>
                        <div class="grid_1">
                            <span class="star">(*)</span>
                        </div>
                        <div class="clear"></div>
                    </div>
                        <div class="row">
                            <div class="grid_2">
                                <SharePoint:FieldLabel ID="FieldLabel2" FieldName="CaNhanToChuc" runat="server"  />
                            </div>
                            <div class="field-required-data grid_9">
                                <SharePoint:FormField  runat="server" ID="FormField2" FieldName="CaNhanToChuc" /> 
                            </div>
                            <div class="grid_1">
                                <span class="star">(*)</span>
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div class="row">
                            <div class="grid_2">
                                <SharePoint:FieldLabel ID="FieldLabel3" FieldName="DiaChi"  runat="server"  />
                            </div>
                            <div class="field-required-data grid_3">
                                <SharePoint:FormField  runat="server" ID="FormField3" FieldName="DiaChi" />
                            </div>
                            <div class="grid_1">
                                <span class="star">(*)</span>
                            </div>
                            <div class="field-required-data grid_2">
                                <SharePoint:FieldLabel ID="FieldLabel4"  FieldName="DienThoai"  runat="server"  />
                            </div>
                            <div class="grid_3">
                                <SharePoint:FormField  runat="server" ID="FormField4" FieldName="DienThoai" />
                            </div>
                            <div class="grid_1">
                                <span class="star">(*)</span>
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div class="clear"></div>
                        <h2>Phương tiện vận tải</h2>

                        <div class="row">
                            <div class="grid_2">
                                <SharePoint:FieldLabel ID="FieldLabel5"  FieldName="LoaiXe" runat="server"  />
                            </div>
                            <div class="field-required-data grid_9">
                                <SharePoint:FormField  runat="server" ID="FormField5" FieldName="LoaiXe" />
                            </div>
                            <div class="grid_1">
                                <span class="star">(*)</span>
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div class="row">
                            <div class="grid_2">
                                <SharePoint:FieldLabel ID="FieldLabel6"  FieldName="NhanHieuXe" runat="server"  />
                            </div>
                            <div class="field-required-data grid_3">
                                <SharePoint:FormField  runat="server" ID="FormField6" FieldName="NhanHieuXe" />
                            </div>
                            <div class="grid_1">
                                <span class="star">(*)</span>
                            </div>
                            <div class="grid_2">
                                <SharePoint:FieldLabel ID="FieldLabel7"  FieldName="BienSoDangKy" runat="server"  />
                            </div>
                            <div class="field-required-data grid_3">
                                <SharePoint:FormField  runat="server" ID="FormField7" FieldName="BienSoDangKy" />
                            </div>
                            <div class="grid_1">
                                <span class="star">(*)</span>
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div class="row">
                            <div class="grid_8">
                                <SharePoint:FieldLabel ID="FieldLabel8"  FieldName="NhanHieuRoMooc" runat="server"  />
                            </div>
                            <div class="grid_3">
                                <SharePoint:FormField  runat="server" ID="FormField8" FieldName="NhanHieuRoMooc" />
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div class="row">
                            <div class="grid_8">
                                <SharePoint:FieldLabel ID="FieldLabel9"  FieldName="BienSoDangKyRoMooc" runat="server"  />
                            </div>
                            <div class="grid_3">
                                <SharePoint:FormField  runat="server" ID="FormField9" FieldName="BienSoDangKyRoMooc" />
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div class="row">
                            <div class="grid_8">
                                <SharePoint:FieldLabel ID="FieldLabel10" FieldName="KichThuocBaoXe" runat="server"  />
                            </div>
                            <div class="grid_3">
                                <SharePoint:FormField  runat="server" ID="FormField10" FieldName="KichThuocBaoXe" />
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div class="row">
                            <div class="grid_8">
                                <SharePoint:FieldLabel ID="FieldLabel11"  FieldName="KichThuocBaoRoMooc" runat="server"  />
                            </div>
                            <div class="grid_3">
                                <SharePoint:FormField  runat="server" ID="FormField11" FieldName="KichThuocBaoRoMooc" />
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div class="row">
                            <div class="grid_8">
                                <SharePoint:FieldLabel ID="FieldLabel12"  FieldName="TaiTrongThietKeXe" runat="server"  />
                            </div>
                            <div class="grid_3">
                                <SharePoint:FormField  runat="server" ID="FormField12" FieldName="TaiTrongThietKeXe" />
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div class="row">
                            <div class="grid_8">
                                <SharePoint:FieldLabel ID="FieldLabel13"  FieldName="TaiTrongThietKeRoMooc" runat="server"  />
                            </div>
                            <div class="grid_3">
                                <SharePoint:FormField  runat="server" ID="FormField13" FieldName="TaiTrongThietKeRoMooc" />
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div class="row">
                            <div class="grid_3">
                                <SharePoint:FieldLabel ID="FieldLabel14"  FieldName="TrongLuongBanThanXe" runat="server"  />
                            </div>
                            <div class="grid_1">
                                <SharePoint:FormField  runat="server" ID="FormField14" FieldName="TrongLuongBanThanXe" />
                            </div>
                            <div class="grid_4">
                                <SharePoint:FieldLabel ID="FieldLabel15" FieldName="TrongLuongBanThanRoMooc"  runat="server"  />
                            </div>
                            <div class="grid_1">
                                <SharePoint:FormField runat="server" ID="FormField15" FieldName="TrongLuongBanThanRoMooc" />
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div class="row">
            
                            <div class="grid_3">
                                <SharePoint:FieldLabel ID="FieldLabel16"  FieldName="SoTrucCuaXe"  runat="server"  />
                            </div>
                            <div class="grid_1">
                                <SharePoint:FormField  runat="server" ID="FormField16" FieldName="SoTrucCuaXe" />
                            </div>
                            <div class="grid_4">
                                <SharePoint:FieldLabel ID="FieldLabel17"  FieldName="SoTrucSauCuaXe"  runat="server"  />
                            </div>
                            <div class="grid_1">
                                <SharePoint:FormField  runat="server" ID="FormField17" FieldName="SoTrucSauCuaXe" />
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div class="row">
                            <div class="grid_3">
                                <SharePoint:FieldLabel ID="FieldLabel18"  FieldName="SoTrucCuaSoMiRoMooc"  runat="server"  />
                            </div>
                            <div class="grid_1">
                                <SharePoint:FormField  runat="server" ID="FormField18" FieldName="SoTrucCuaSoMiRoMooc" />
                            </div>
                            <div class="grid_4">
                                <SharePoint:FieldLabel ID="FieldLabel19"  FieldName="SoTrucCuaRoMooc"  runat="server"  />
                            </div>
                            <div class="grid_1">
                                <SharePoint:FormField  runat="server" ID="FormField19" FieldName="SoTrucCuaRoMooc" />
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div class="row">
                            <div class="grid_3">
                                <SharePoint:FieldLabel ID="FieldLabel39"  FieldName="SoTrucSauCuaRoMooc"  runat="server"  />
                            </div>
                            <div class="grid_1">
                                <SharePoint:FormField  runat="server" ID="FormField39" FieldName="SoTrucSauCuaRoMooc" />
                            </div>
                            <div class="clear"></div>
                        </div>
                        <h2>Hàng hóa vận chuyển</h2>

                        <div class="row">
                            <div class="grid_3">
                                <SharePoint:FieldLabel ID="FieldLabel20"  FieldName="LoaiHang"  runat="server"  />
                            </div>
                            <div class="grid_8">
                                <SharePoint:FormField  runat="server" ID="FormField20" FieldName="LoaiHang" />
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div class="row">
                            <div class="grid_3">
                                <SharePoint:FieldLabel ID="flbLoaiCapPhep"  FieldName="LoaiCapPhep"  runat="server"  />
                            </div>
                            <div class="grid_8 custom-label">
                                <SharePoint:FormField  runat="server" ID="fldLoaiCapPhep" FieldName="LoaiCapPhep" />

                                <style type="text/css">
                                    .custom-label label {
                                        display: inline !important;
                                    }
                                </style>
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div class="row">
                            <div class="grid_3">
                                <SharePoint:FieldLabel ID="FieldLabel21"  FieldName="TrongLuongHangXinCho"  runat="server"  />
                            </div>
                            <div class="grid_8">
                                <SharePoint:FormField  runat="server" ID="FormField21" FieldName="TrongLuongHangXinCho" />
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div class="row">
                            <div class="grid_3">
                                <SharePoint:FieldLabel ID="FieldLabel22"  FieldName="ChieuRongToanBoXeKhiXepHang"  runat="server"  />
                            </div>
                            <div class="grid_1">
                                <SharePoint:FormField  runat="server" ID="FormField22" FieldName="ChieuRongToanBoXeKhiXepHang" />
                            </div>
                            <div class="grid_3 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-1">
                                <SharePoint:FieldLabel ID="FieldLabel23"  FieldName="HangVuotHaiBenThungXe"  runat="server" />
                            </div>
                            <div class="grid_3">
                                <SharePoint:FormField  runat="server" ID="FormField23" FieldName="HangVuotHaiBenThungXe" />
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div class="row">
                            <div class="grid_3">
                                <SharePoint:FieldLabel ID="FieldLabel24"  FieldName="ChieuDaiToanBoXeKhiXepHang"  runat="server"  />
                            </div>
                            <div class="grid_1">
                                <SharePoint:FormField  runat="server" ID="FormField24" FieldName="ChieuDaiToanBoXeKhiXepHang" />
                            </div>
                            <div class="grid_3 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-1">
                                <SharePoint:FieldLabel ID="FieldLabel25"  FieldName="ChieuCaoToanBoXeKhiXepHang"  runat="server"/>
                            </div>
                            <div class="grid_1">
                                <SharePoint:FormField  runat="server" ID="FormField25" FieldName="ChieuCaoToanBoXeKhiXepHang"/>
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div class="row">
                            <div class="grid_3">
                                <SharePoint:FieldLabel ID="FieldLabel26"  FieldName="HangVuotPhiaTruocThungXe"  runat="server"/>
                            </div>
                            <div class="grid_1">
                                <SharePoint:FormField  runat="server" ID="FormField26" FieldName="HangVuotPhiaTruocThungXe"/>
                            </div>
                            <div class="grid_3 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-1">
                                <SharePoint:FieldLabel ID="FieldLabel27"  FieldName="HangVuotPhiaSauThungXe"  runat="server"/>
                            </div>
                            <div class="grid_3">
                                <SharePoint:FormField  runat="server" ID="FormField27" FieldName="HangVuotPhiaSauThungXe"/>
                            </div>
                            <div class="clear"></div>
                        </div>
                        <h2>Tải trọng lớn nhất được phân bố lên trục xe sau khi xếp hàng hóa lên xe:</h2>

                        <div class="row">
                            <div class="grid_3">
                                <SharePoint:FieldLabel ID="FieldLabel28"  FieldName="TrucDon"  runat="server"/>
                            </div>
                            <div class="grid_1">
                                <SharePoint:FormField  runat="server" ID="FormField28" FieldName="TrucDon"/>
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div class="row">
                            <div class="grid_3">
                                <SharePoint:FieldLabel ID="FieldLabel29"  FieldName="TrucKep"  runat="server"/>
                            </div>
                            <div class="grid_1">
                                <SharePoint:FormField  runat="server" ID="FormField29" FieldName="TrucKep"/>
                            </div>
                            <div class="grid_4">
                                <SharePoint:FieldLabel ID="FieldLabel30"  FieldName="TrucBa"  runat="server"/>
                            </div>
                            <div class="grid_3">
                                <SharePoint:FormField  runat="server" ID="FormField30" FieldName="TrucBa"/>
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div class="row">
                            <div class="grid_3">
                                <SharePoint:FieldLabel ID="FieldLabel31"  FieldName="KhoangCachGiuaHaiTamTruc"  runat="server"/>
                            </div>
                            <div class="grid_1">
                                <SharePoint:FormField  runat="server" ID="FormField31" FieldName="KhoangCachGiuaHaiTamTruc"/>
                            </div>
                            <div class="grid_4">
                                <SharePoint:FieldLabel ID="FieldLabel32"  FieldName="KhoangCachGiuaHaiTamTrucLienKe"  runat="server"/>
                            </div>
                            <div class="grid_3">
                                <SharePoint:FormField  runat="server" ID="FormField32" FieldName="KhoangCachGiuaHaiTamTrucLienKe"/>
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div class="row">
                            <div class="grid_3">
                                <SharePoint:FieldLabel ID="FieldLabel33"  FieldName="NoiDi"  runat="server"/>
                            </div>
                            <div class="grid_8">
                                <SharePoint:FormField  runat="server" ID="FormField33" FieldName="NoiDi"/>
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div class="row">
                            <div class="grid_3">
                                <SharePoint:FieldLabel ID="FieldLabel34"  FieldName="NoiDen"  runat="server"/>
                            </div>
                            <div class="grid_8">
                                <SharePoint:FormField  runat="server" ID="FormField34" FieldName="NoiDen"/>
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div class="row">
                            <div class="grid_11">
                                <SharePoint:FieldLabel ID="FieldLabel35"  FieldName="TuyenDuongVanChuyen"  runat="server" />
                                <SharePoint:FormField  runat="server" ID="FormField35" FieldName="TuyenDuongVanChuyen" />
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div class="row">
                            <div class="grid_3">
                                <SharePoint:FieldLabel ID="FieldLabel36"  FieldName="ThoiGiaDeNghiLuuHanhTu"  runat="server" />
                            </div>
                            <div class="grid_3 date-picker">
                                <SharePoint:FormField  runat="server" ID="FormField36" CssClass="field-required" FieldName="ThoiGiaDeNghiLuuHanhTu"/>
                                <%--<span class="star">(*)</span>--%>
                            </div>
                            <div class="grid_3">
                                <SharePoint:FieldLabel ID="FieldLabel37"  FieldName="ThoiGiaDeNghiLuuHanhDen"  runat="server"  />
                            </div>
                            <div class="grid_2 col-xs-2 date-picker">
                                <SharePoint:FormField  runat="server" ID="FormField37" CssClass="field-required" FieldName="ThoiGiaDeNghiLuuHanhDen"/>
                            </div>
                            <div class="clear"></div>
                        </div>
                    </div>

                    <div class="panel-2">
                        <h2>Danh sách tập tin đính kèm cho hồ sơ</h2>

                        <div class="row">
                            <div class="grid_8">
                                Bản sao giấy đăng ký hoặc giấy đăng ký tạm thời xe, xe đầu kéo, rơ moóc..
                                <span title="This is a required field." class="ms-accentText"> *</span>
                            </div>
                            <div class="grid_3" id="divFileUpload1" runat="server">
                                <asp:FileUpload ID="fileUpload1" runat="server" />
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div class="row">
                            <div class="grid_8">
                                Giấy chứng nhận kiểm định an toàn kỹ thuật và bảo vệ môi trường phương tiện giao
                                    thông cơ giới đường bộ
                                <span title="This is a required field." class="ms-accentText"> *</span>
                            </div>
                            <div class="grid_3" id="divFileUpload2" runat="server">
                                <asp:FileUpload ID="fileUpload2" runat="server" />
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div class="row">
                            <div class="grid_8">
                                Giấy cam kết của chủ phương tiện về quyền sở hữu phương tiện
                                <span title="This is a required field." class="ms-accentText"> *</span>
                            </div>
                            <div class="grid_3"  id="divFileUpload3" runat="server">
                                <asp:FileUpload ID="fileUpload3" runat="server" />
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div class="row">
                            <div class="grid_8">
                                Chứng minh nhân dân của người nộp
                                <span title="This is a required field." class="ms-accentText"> *</span>
                            </div>
                            <div class="grid_3" id="divFileUpload4" runat="server">
                                <asp:FileUpload ID="fileUpload4" runat="server" />
                            </div>
                            <div class="clear"></div>
                        </div>
                    </div>

                    <div class="panel-2" id="divDanhSachYeuCauBoSung" runat="server" visible="false">
                        <h2>Danh sách cầu bổ sung</h2>
                        <div class="row">
                            <div class="grid_11">
                                <table class="the-table">
                                    <tr style="height: 50px;">
                                        <th>STT</th>
                                        <th>Tiêu đề</th>
                                        <th>Mô tả</th>
                                        <th>Ngày yêu cầu</th>
                                        <th>Xác nhận</th>
                                    </tr>
                                    <asp:Repeater ID="repeaterLists" runat="server" OnItemCommand="repeaterLists_ItemCommand" OnItemDataBound="repeaterLists_ItemDataBound">
                                        <ItemTemplate>
                                            <tr class="<%#(((RepeaterItem)Container).ItemIndex+1) % 2 == 0 ? "even" : "odd" %>">
                                                <td>
                                                    <asp:Literal ID="literalSTT" runat="server" Text="<%#(((RepeaterItem)Container).ItemIndex+1) %>"></asp:Literal>
                                                </td>
                                                <td>
                                                    <asp:Literal ID="literalTitle" runat="server"></asp:Literal>
                                                </td>
                                                <td>
                                                    <asp:Literal ID="literalMoTa" runat="server"></asp:Literal>
                                                </td>
                                                <td>
                                                    <asp:Literal ID="literalNgayYeuCau" runat="server"></asp:Literal>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lbtXacNhan" ToolTip="Xác nhận đã cập nhật" CssClass="button waiting just-icon" runat="server"></asp:LinkButton>
                                                    <asp:LinkButton ID="lbtDisable" ToolTip="Đã cập nhật" Enabled="false" CssClass="button done just-icon" style="display:none;" runat="server"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                            <div class="clear"></div>
                        </div>
                    </div>

                    <div class="pull-right">
                        <asp:Button ID="btnSave" OnClientClick="if (!PreSaveItem(1)) return false;" CssClass="button" runat="server" Text="Lưu" Visible="false" style="float:left;"/>
                        <asp:Button ID="btnGuiHoSo" OnClientClick="if (!PreSaveItem(1)) return false;" runat="server" Text="Nộp hồ sơ" CssClass="button" Visible="false" style="float:left;"/>
                        <asp:Button ID="btnCancel" runat="server" Text="Đóng" CssClass="button" style="float:left;"/>
                    </div>
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </div>     
    <table cellpadding="0" cellspacing="0" width="100%" style="padding-top: 7px; display:none;">
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