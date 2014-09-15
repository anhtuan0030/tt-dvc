<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeNghiNew.ascx.cs" Inherits="LongAn.DVC.WebParts.DeNghiNew.DeNghiNew" %>
<div class="container">
    <div class="row">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <h1>Đề nghị cấp phép lưu hành xe quá tải trọng, xe quá khổ</h1>
            <div class="panel-1">
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <SharePoint:FieldLabel ID="FieldLabel1"  FieldName="KinhGui"  runat="server"  />
                    </div>
                    <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9">
                        <SharePoint:FormField  runat="server" ID="FormField1"  FieldName="KinhGui"  /> 
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                        <span class="star">(*)</span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <SharePoint:FieldLabel ID="FieldLabel2"  FieldName="CaNhanToChuc"  runat="server"  />
                    </div>
                    <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9">
                        <SharePoint:FormField  runat="server" ID="FormField2" FieldName="CaNhanToChuc" /> 
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                        <span class="star">(*)</span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <SharePoint:FieldLabel ID="FieldLabel3"  FieldName="DiaChi"  runat="server"  />
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <SharePoint:FormField  runat="server" ID="FormField3" FieldName="DiaChi" />
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                        <span class="star">(*)</span>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <SharePoint:FieldLabel ID="FieldLabel4"  FieldName="DienThoai"  runat="server"  />
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <SharePoint:FormField  runat="server" ID="FormField4" FieldName="DienThoai" />
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                        <span class="star">(*)</span>
                    </div>
                </div>
                <h2>Phương tiện vận tải</h2>

                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <SharePoint:FieldLabel ID="FieldLabel5"  FieldName="LoaiXe"  runat="server"  />
                    </div>
                    <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9">
                        <SharePoint:FormField  runat="server" ID="FormField5" FieldName="LoaiXe" />
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                        <span class="star">(*)</span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <SharePoint:FieldLabel ID="FieldLabel6"  FieldName="NhanHieuXe"  runat="server"  />
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <SharePoint:FormField  runat="server" ID="FormField6" FieldName="NhanHieuXe" />
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                        <span class="star">(*)</span>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <SharePoint:FieldLabel ID="FieldLabel7"  FieldName="BienSoDangKy"  runat="server"  />
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <SharePoint:FormField  runat="server" ID="FormField7" FieldName="BienSoDangKy" />
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                        <span class="star">(*)</span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                        <SharePoint:FieldLabel ID="FieldLabel8"  FieldName="NhanHieuRoMooc"  runat="server"  />
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <SharePoint:FormField  runat="server" ID="FormField8" FieldName="NhanHieuRoMooc" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                        <SharePoint:FieldLabel ID="FieldLabel9"  FieldName="BienSoDangKyRoMooc"  runat="server"  />
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <SharePoint:FormField  runat="server" ID="FormField9" FieldName="BienSoDangKyRoMooc" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                        <SharePoint:FieldLabel ID="FieldLabel10" FieldName="KichThuocBaoXe" runat="server"  />
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <SharePoint:FormField  runat="server" ID="FormField10" FieldName="KichThuocBaoXe" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                        <SharePoint:FieldLabel ID="FieldLabel11"  FieldName="KichThuocBaoRoMooc"  runat="server"  />
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <SharePoint:FormField  runat="server" ID="FormField11" FieldName="KichThuocBaoRoMooc" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                        <SharePoint:FieldLabel ID="FieldLabel12"  FieldName="TaiTrongThietKeXe"  runat="server"  />
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <SharePoint:FormField  runat="server" ID="FormField12" FieldName="TaiTrongThietKeXe" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                        <SharePoint:FieldLabel ID="FieldLabel13"  FieldName="TaiTrongThietKeRoMooc"  runat="server"  />
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <SharePoint:FormField  runat="server" ID="FormField13" FieldName="TaiTrongThietKeRoMooc" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <SharePoint:FieldLabel ID="FieldLabel14"  FieldName="TrongLuongBanThanXe"  runat="server"  />
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                        <SharePoint:FormField  runat="server" ID="FormField14" FieldName="TrongLuongBanThanXe" />
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                        <SharePoint:FieldLabel ID="FieldLabel15"  FieldName="TrongLuongBanThanRoMooc"  runat="server"  />
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                        <SharePoint:FormField  runat="server" ID="FormField15" FieldName="TrongLuongBanThanRoMooc" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <SharePoint:FieldLabel ID="FieldLabel16"  FieldName="SoTrucCuaXe"  runat="server"  />
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                        <SharePoint:FormField  runat="server" ID="FormField16" FieldName="SoTrucCuaXe" />
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                        <SharePoint:FieldLabel ID="FieldLabel17"  FieldName="SoTrucSauCuaXe"  runat="server"  />
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                        <SharePoint:FormField  runat="server" ID="FormField17" FieldName="SoTrucSauCuaXe" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <SharePoint:FieldLabel ID="FieldLabel18"  FieldName="SoTrucCuaRoMooc"  runat="server"  />
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                        <SharePoint:FormField  runat="server" ID="FormField18" FieldName="SoTrucCuaRoMooc" />
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                        <SharePoint:FieldLabel ID="FieldLabel19"  FieldName="SoTrucSauCuaRoMooc"  runat="server"  />
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                        <SharePoint:FormField  runat="server" ID="FormField19" FieldName="SoTrucSauCuaRoMooc" />
                    </div>
                </div>
                <h2>Hàng hóa vận chuyển</h2>

                <div class="row">
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <SharePoint:FieldLabel ID="FieldLabel20"  FieldName="LoaiHang"  runat="server"  />
                    </div>
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                        <SharePoint:FormField  runat="server" ID="FormField20" FieldName="LoaiHang" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <SharePoint:FieldLabel ID="FieldLabel21"  FieldName="TrongLuongHangXinCho"  runat="server"  />
                    </div>
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                        <SharePoint:FormField  runat="server" ID="FormField21" FieldName="TrongLuongHangXinCho" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <SharePoint:FieldLabel ID="FieldLabel22"  FieldName="ChieuRongToanBoXeKhiXepHang"  runat="server"  />
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                        <SharePoint:FormField  runat="server" ID="FormField22" FieldName="ChieuRongToanBoXeKhiXepHang" />
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-1">
                        <SharePoint:FieldLabel ID="FieldLabel23"  FieldName="HangVuotHaiBenThungXe"  runat="server"  />
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <SharePoint:FormField  runat="server" ID="FormField23" FieldName="HangVuotHaiBenThungXe" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <SharePoint:FieldLabel ID="FieldLabel24"  FieldName="ChieuDaiToanBoXeKhiXepHang"  runat="server"  />
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                        <SharePoint:FormField  runat="server" ID="FormField24" FieldName="ChieuDaiToanBoXeKhiXepHang" />
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-1">
                        <SharePoint:FieldLabel ID="FieldLabel25"  FieldName="ChieuCaoToanBoXeKhiXepHang"  runat="server"  />
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <SharePoint:FormField  runat="server" ID="FormField25" FieldName="ChieuCaoToanBoXeKhiXepHang" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <SharePoint:FieldLabel ID="FieldLabel26"  FieldName="HangVuotPhiaTruocThungXe"  runat="server"  />
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                        <SharePoint:FormField  runat="server" ID="FormField26" FieldName="HangVuotPhiaTruocThungXe" />
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-1">
                        <SharePoint:FieldLabel ID="FieldLabel27"  FieldName="HangVuotPhiaSauThungXe"  runat="server"  />
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <SharePoint:FormField  runat="server" ID="FormField27" FieldName="HangVuotPhiaSauThungXe" />
                    </div>
                </div>
                <h3>Tải trọng lớn nhất được phân bố lên trục xe sau khi xếp hàng hóa lên xe:</h3>

                <div class="row">
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <SharePoint:FieldLabel ID="FieldLabel28"  FieldName="TrucDon"  runat="server"  />
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                        <SharePoint:FormField  runat="server" ID="FormField28" FieldName="TrucDon" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <SharePoint:FieldLabel ID="FieldLabel29"  FieldName="TrucKep"  runat="server"  />
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                        <SharePoint:FormField  runat="server" ID="FormField29" FieldName="TrucKep" />
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                        <SharePoint:FieldLabel ID="FieldLabel30"  FieldName="TrucBa"  runat="server"  />
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <SharePoint:FormField  runat="server" ID="FormField30" FieldName="TrucBa" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <SharePoint:FieldLabel ID="FieldLabel31"  FieldName="KhoangCachGiuaHaiTamTruc"  runat="server"  />
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                        <SharePoint:FormField  runat="server" ID="FormField31" FieldName="KhoangCachGiuaHaiTamTruc" />
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                        <SharePoint:FieldLabel ID="FieldLabel32"  FieldName="KhoangCachGiuaHaiTamTrucLienKe"  runat="server"  />
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <SharePoint:FormField  runat="server" ID="FormField32" FieldName="KhoangCachGiuaHaiTamTrucLienKe" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <SharePoint:FieldLabel ID="FieldLabel33"  FieldName="NoiDi"  runat="server"  />
                    </div>
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                        <SharePoint:FormField  runat="server" ID="FormField33" FieldName="NoiDi" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <SharePoint:FieldLabel ID="FieldLabel34"  FieldName="NoiDen"  runat="server"  />
                    </div>
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                        <SharePoint:FormField  runat="server" ID="FormField34" FieldName="NoiDen" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-11 col-md-11 col-sm-11 col-xs-11">
                        <SharePoint:FieldLabel ID="FieldLabel35"  FieldName="TuyenDuongVanChuyen"  runat="server"  />
                        <SharePoint:FormField  runat="server" ID="FormField35" FieldName="TuyenDuongVanChuyen" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <SharePoint:FieldLabel ID="FieldLabel36"  FieldName="ThoiGiaDeNghiLuuHanhTu"  runat="server"  />
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 date-picker">
                        <SharePoint:FormField  runat="server" ID="FormField36" FieldName="ThoiGiaDeNghiLuuHanhTu" />                                        
                        <span class="star">(*)</span>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <SharePoint:FieldLabel ID="FieldLabel37"  FieldName="ThoiGiaDeNghiLuuHanhDen"  runat="server"  />
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2 date-picker">
                        <SharePoint:FormField  runat="server" ID="FormField37" FieldName="ThoiGiaDeNghiLuuHanhDen" />
                        <span class="star">(*)</span>
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
                        <label for="giay-dang-ky">Bản sao giấy đăng ký hoặc giấy đăng ký tạm thời xe, xe đầu kéo, rơ moóc..</label>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <asp:FileUpload ID="fileUpload1" runat="server" />
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                        <span class="star">(*)</span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                        <label for="giay-kiem-dinh">Giấy chứng nhận kiểm định an toàn kỹ thuật và bảo vệ môi trường phương tiện giao
                            thông cơ giới đường bộ</label>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <asp:FileUpload ID="fileUpload2" runat="server" />
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                        <span class="star">(*)</span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                        <label for="giay-cam-ket">Giấy cam kết của chủ phương tiện về quyền sở hữu phương tiện</label>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <asp:FileUpload ID="fileUpload3" runat="server" />
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                        <span class="star">(*)</span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                        <label for="cmnd">Chứng minh nhân dân của người nộp</label>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                        <asp:FileUpload ID="fileUpload4" runat="server" />
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                        <span class="star">(*)</span>
                    </div>
                </div>
            </div>
            <div class="pull-right">
                <asp:Button ID="btnSave" runat="server" Text="Thêm mới" />
                <asp:Button ID="btnCancel" runat="server" Text="Làm lại" />
            </div>
            <div class="clearfix"></div>
        </div>
    </div>
</div>