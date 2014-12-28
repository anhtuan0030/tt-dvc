<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeNghiReport.ascx.cs" Inherits="LongAn.DVC.WebParts.DeNghiReport.DeNghiReport" %>

<style type="text/css">
.form {
  border: 1px solid #dedede;
  margin-bottom: 15px;
  margin-top: 15px;
  padding: 15px;
}

.form .row.line {
  margin-bottom: 15px;
}

.form .button {
  padding-bottom: 10px;
  padding-top: 10px;
  width: 90%;
}

/*CSS - TINA*/
.tittle{ text-transform:uppercase; font-weight:bold; color:#fff}
.no-border-bottom{ border-bottom:none;}
#data_div{float:left; margin-left:10px;width:930px;border-radius:0px; padding:15px; text-transform: color:#fff; font-size:13px; border-radius:5px; border-bottom:1px #dcdcdc dashed;}
#data_div:hover{float:left; margin-left:10px;width:930px;border-radius:0px; padding:15px; text-transform: color:#fff; font-size:13px; border-radius:5px; background:#98CEF5;}
.bg{background:#17bdde;}
.bg-2{background:#dde6ed;}
.bg-3{background:#C8F8FC;}
.bg-5{background:#bcffcd;}
.format-1{ padding-top:15px; padding-bottom:15px; border-radius:5px; margin-bottom:15px; border:1px #ccc solid;}
.row_5{float: left; width:5%; line-height:22px;padding-right:15px;}
.row_45{float: left; width:45%; line-height:22px; padding-left:15px; padding-right:15px;}
.row_30{float: left; width:30%; line-height:22px; padding-left:5px; padding-right:5px;}
.right{ float:right}
.text_center{ text-align:center}
.text_right{ text-align: right}

.warning {background :#fce1c1;
	border: 1px solid #e1b37c;
	color : #c37820;
}
.style-text-1{ color:#f66; margin-top:5px;}
.style-text-2{ color:#333; margin-top:5px;}
.style-text-3{ margin-top:25px; margin-bottom:15px;}
.style-text-4{ font-size:11px; text-align:center}
.style-p{ text-align:left; padding:5px;}
.border-bottom{border-bottom:1px #333 solid; padding-bottom:5px;}
.border-top{ border-top:2px #666 dotted; margin-top:20px;}
.border-bottom_dash{border-bottom:1px #dcdcdc dashed; padding-bottom:3px;}
.pad_left_c1{ padding-left:15px;}
.pad_left_c2{ padding-left:30px;}
.padding-1{padding:2px;}
.padding{padding:10px;}
.color_red{ color:#f00}
.color_green{ color:green}
.p{ line-height:25px; text-align:justify;}
.size_11{ font-size:11px;}
table tr td{ padding-left:5px;padding-right:5px}

/* Tabs Css */
.tabs input[type=radio] {
    position: absolute;
    /*top: -9999px;*/
    left: -9999px;
}
.tabs {
  width: 100%;
  float: left;
  list-style: none;
  position: relative;
  padding: 0;
  margin: 10px auto;
  clear:both;
  margin-bottom:600px;
}
.tabs li{
  float: left; 
}

.tabs label {
    display: block;
    padding: 10px 20px;
    border-radius: 2px 2px 0 0;
    color: #333;
    font-size: 17px;
    font-weight: normal;
    font-family: arial;
    background: rgba(255,255,255,0.2);
    cursor: pointer;
    position: relative;
    top: 3px;
    -webkit-transition: all 0.2s ease-in-out;
    -moz-transition: all 0.2s ease-in-out;
    -o-transition: all 0.2s ease-in-out;
    transition: all 0.2s ease-in-out;
}
.tabs label:hover {
  background:#dcdcdc;
  top: 0;
}

[id^=tab]:checked + label {
  background: #dcdcdc;
  color:#333;
  top: 0;
}

[id^=tab]:checked ~ [id^=tab-content] {
    display: block;
}
.tab-content{
  z-index: 2;
  display: none;
  text-align: left;
  width: 100%;
  font-size: 13px;
  line-height: 140%;
  padding-top: 10px;
  background: #eee;
  border:1px #dcdcdc solid;
  padding: 15px;
  color:#333;
  position: absolute;
  /*top: 53px;*/
  left: 0;
  box-sizing: border-box;
  -webkit-animation-duration: 0.5s;
  -o-animation-duration: 0.5s;
  -moz-animation-duration: 0.5s;
  animation-duration: 0.5s;
}
.container_table .tab-content h1{
    font-size:20px;
    color: #303257;
    padding: 10px;
    text-transform: uppercase;
    font-family: Arial,"Helvetica Neue",Helvetica,sans-serif;
}
.fadeIn {
    animation-name: fadeIn;
}
.btt{
  border: none;
  background: green;
  color: #fff;
  display: inline-block;
  padding: 5px 10px;
  text-align: center;
  border-radius:5px; width:150px; 
}
.btt button:hover {
  background: #01648d;
}
.text-bold{ font-weight:bold}

.report-table {
    border-collapse: collapse;
    border: 1px solid #dcdcdc;
}
.report-table td {
    border: 1px solid #dcdcdc;
}
.container_table, .container_table .row {
line-height: 25px;
}
</style>

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
                        <asp:Button ID="btnViewReport" runat="server" Text="Xem báo cáo" CssClass="button btnViewReport" align="middle" OnClick="btnViewReport_Click" />
                    </div>
                    <div class="grid_1">

                    </div>
                    <div class="grid_2">
                        <asp:Button ID="btnExportExcel" runat="server" Text="Xuất báo cáo" CssClass="button btnExportExcel" align="middle" OnClick="btnExportExcel_Click" />
                    </div>
                    <div class="clear"></div>
                </div>
            </div>

            <asp:Panel ID="pnlViewBaoCaoCapPhepLuuHanh" runat="server" Visible="false" CssClass="container_table style-1">

                <ul class="tabs">
                    <li>
                        <input type="radio" checked name="tabs" id="tab1">
                        <label for="tab1">QUÁ TẢI TRỌNG - QUÁ KHỔ</label>
                        <div id="tab-content1" class="tab-content animated fadeIn">
                            <div class="text-right border-bottom_dash">PHỤ LỤC 6a</div>
                            <div class="">
                                <div class="row">
                                    <div class="grid_4  text-center style-text-3">UBND TỈNH LONG AN<br>
                                        <span class="border-bottom"><strong>SỞ GIAO THÔNG VẬN TẢI</strong></span><br>
                                        Số:               /SGTVT-GLHX.</div>
                                    <div class="grid_7  text-center style-text-3">
                                        CỘNG HOÀ XÃ HỘI CHỦ NGHĨA VIỆT NAM<br>
                                        <span class="border-bottom">Độc lập - Tự do - Hạnh phúc</span><br>
                                        <em><asp:Label ID="lblCurrentDateQKQT" runat="server" Text=""></asp:Label></em>
                                    </div>
                                </div>
                            </div>
                            <div class="clear"></div>

                            <h1 class="container_table text-center">BÁO CÁO CẤP GIẤY PHÉP LƯU HÀNH CHO XE QUÁ TẢI TRỌNG VÀ XE QUÁ KHỔ GIỚI HẠN</h1>
                            <div class="container_table text-center"><asp:Label ID="lblFromDateToDateQKQT" runat="server" Text=""></asp:Label></div>
                            <div class="container_table text-center"><strong>Kính gửi: Tổng Cục Đường Bộ Việt Nam</strong></div>
                            <table border="1" bordercolor="#dcdcdc" cellspacing="0" cellpadding="0" width="100%" class="report-table style-text-3">
                                <tr class="bg-5">
                                    <td width="6%"><strong>&nbsp;</strong></td>
                                    <td width="9%" class="padding style-text-4"><strong>Xe 02 trục đơn</strong></td>
                                    <td width="7%" class="padding style-text-4"><strong>Xe 03 trục</strong></td>
                                    <td width="7%" class="padding style-text-4"><strong>Xe 04 trục</strong></td>
                                    <td width="12%" class="padding style-text-4"><strong>Tổ hợp xe - sơ mi rơ mooc (03 trục)</strong></td>
                                    <td width="12%" class="padding style-text-4"><strong>Tổ hợp xe - sơ mi rơ mooc (04 trục)</strong></td>
                                    <td width="12%" class="padding style-text-4"><strong>Tổ hợp xe - sơ mi rơ mooc (05 trục)</strong></td>
                                    <td width="7%" class="padding style-text-4"><strong>Tổng cộng</strong></td>
                                    <td width="13%" class="padding style-text-4"><strong>Vận    chuyển hàng quá tải trọng, quá khổ giới hạn</strong></td>
                                    <td width="15%" class="padding text-center"><strong>Ghi chú</strong></td>
                                </tr>
                                <tr class="text-center">
                                    <td class="text-center">&nbsp;</td>
                                    <td class="text-center padding-1">1</td>
                                    <td class="text-center">2</td>
                                    <td class="text-center">3</td>
                                    <td class="text-center">4</td>
                                    <td class="text-center">5</td>
                                    <td class="text-center">6</td>
                                    <td class="text-center">7</td>
                                    <td class="text-center">8</td>
                                    <td class="text-center">9</td>
                                </tr>
                                <tr>
                                    <td nowrap class="padding">Quốc    lộ </td>
<td class="text-center"><asp:Label ID="lblQuaTaiQuaKho_Xe2Truc_QuocLo" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaTaiQuaKho_Xe3Truc_QuocLo" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaTaiQuaKho_Xe4Truc_QuocLo" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaTaiQuaKho_XeRM3Truc_QuocLo" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaTaiQuaKho_XeRM4Truc_QuocLo" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaTaiQuaKho_XeRM5Truc_QuocLo" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaTaiQuaKho_TongCong_QuocLo" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaTaiQuaKho_TongCong_QuocLo8" runat="server" Text=""></asp:Label></td>
                                    <td class="text-center"></td>
                                </tr>
                                <tr>
                                    <td nowrap class="padding">Đường    tỉnh</td>
<td class="text-center"><asp:Label ID="lblQuaTaiQuaKho_Xe2Truc_DuongTinh" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaTaiQuaKho_Xe3Truc_DuongTinh" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaTaiQuaKho_Xe4Truc_DuongTinh" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaTaiQuaKho_XeRM3Truc_DuongTinh" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaTaiQuaKho_XeRM4Truc_DuongTinh" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaTaiQuaKho_XeRM5Truc_DuongTinh" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaTaiQuaKho_TongCong_DuongTinh" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaTaiQuaKho_TongCong_DuongTinh8" runat="server" Text=""></asp:Label></td>
                                    <td class="text-center"></td>
                                </tr>
                                <tr>
                                    <td nowrap class="padding"><strong>Tổng    cộng</strong></td>
<td class="text-center"><asp:Label ID="lblQuaTaiQuaKho_Xe2Truc_TongCong" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaTaiQuaKho_Xe3Truc_TongCong" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaTaiQuaKho_Xe4Truc_TongCong" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaTaiQuaKho_XeRM3Truc_TongCong" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaTaiQuaKho_XeRM4Truc_TongCong" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaTaiQuaKho_XeRM5Truc_TongCong" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaTaiQuaKho_TongCong_TongCong" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaTaiQuaKho_TongCong_TongCong8" runat="server" Text=""></asp:Label></td>
                                    <td class="text-center"></td>
                                </tr>
                            </table>
                            <div class="clear"></div>
                            <div class="warning">*Ghi chú: Mỗi Giấy Phép Lưu  hành xe cấp cho cả hai tuyến (Quốc lộ và Đường tỉnh)</div>
                        </div>
                    </li>
                    <li>
                        <input type="radio" name="tabs" id="tab2">
                        <label for="tab2">QUÁ KHỔ</label>
                        <div id="tab-content2" class="tab-content animated fadeIn">
                            <div class=" text-right border-bottom_dash">PHỤ LỤC 6b</div>
                            <div class="">
                                <div class="row">
                                    <div class="grid_4  text-center style-text-3">UBND TỈNH LONG AN<br>
                                        <span class="border-bottom"><strong>SỞ GIAO THÔNG VẬN TẢI</strong></span><br>
                                        Số:               /SGTVT-GLHX.</div>
                                    <div class="grid_7  text-center style-text-3">
                                        CỘNG HOÀ XÃ HỘI CHỦ NGHĨA VIỆT NAM<br>
                                        <span class="border-bottom">Độc lập - Tự do - Hạnh phúc</span><br>
                                        <em><asp:Label ID="lblCurrentDateQK" runat="server" Text=""></asp:Label></em>
                                    </div>
                                </div>
                            </div>
                            <div class="clear"></div>

                            <h1 class="container_table text-center">BÁO CÁO CẤP GIẤY PHÉP LƯU HÀNH CHO XE QUÁ KHỔ GIỚI HẠN</h1>
                            <div class="container_table text-center"><asp:Label ID="lblFromDateToDateQK" runat="server" Text=""></asp:Label></div>
                            <div class="container_table text-center"><strong>Kính gửi: Tổng Cục Đường Bộ Việt Nam</strong></div>
                            <table border="1" bordercolor="#dcdcdc" cellspacing="0" cellpadding="0" width="100%" class="report-table style-text-3">
                                <tr class="bg-5">
                                    <td width="6%"><strong>&nbsp;</strong></td>
                                    <td width="9%" class="padding style-text-4"><strong>Xe 02 trục đơn</strong></td>
                                    <td width="7%" class="padding style-text-4"><strong>Xe 03 trục</strong></td>
                                    <td width="7%" class="padding style-text-4"><strong>Xe 04 trục</strong></td>
                                    <td width="12%" class="padding style-text-4"><strong>Tổ hợp xe - sơ mi rơ mooc (03    trục)</strong></td>
                                    <td width="12%" class="padding style-text-4"><strong>Tổ hợp xe - sơ mi rơ mooc (04    trục)</strong></td>
                                    <td width="12%" class="padding style-text-4"><strong>Tổ hợp xe - sơ mi rơ mooc (05    trục)</strong></td>
                                    <td width="7%" class="padding style-text-4"><strong>Tổng cộng</strong></td>
                                    <td width="12%" class="padding style-text-4"><strong>Vận chuyển hàng quá khổ giới hạn</strong></td>
                                    <td width="15%" class="padding text_center"><strong>Ghi chú</strong></td>
                                </tr>
                                <tr class="text-center">
                                    <td class="text-center">&nbsp;</td>
                                    <td class="text-center padding-1">1</td>
                                    <td class="text-center">2</td>
                                    <td class="text-center">3</td>
                                    <td class="text-center">4</td>
                                    <td class="text-center">5</td>
                                    <td class="text-center">6</td>
                                    <td class="text-center">7</td>
                                    <td class="text-center">8</td>
                                    <td class="text-center">9</td>
                                </tr>
                                <tr>
                                    <td nowrap class="padding">Quốc    lộ </td>
<td class="text-center"><asp:Label ID="lblQuaKho_Xe2Truc_QuocLo" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaKho_Xe3Truc_QuocLo" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaKho_Xe4Truc_QuocLo" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaKho_XeRM3Truc_QuocLo" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaKho_XeRM4Truc_QuocLo" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaKho_XeRM5Truc_QuocLo" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaKho_TongCong_QuocLo" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaKho_TongCong_QuocLo8" runat="server" Text=""></asp:Label></td>
                                    <td class="text-center"></td>
                                </tr>
                                <tr>
                                    <td nowrap class="padding">Đường    tỉnh</td>
<td class="text-center"><asp:Label ID="lblQuaKho_Xe2Truc_DuongTinh" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaKho_Xe3Truc_DuongTinh" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaKho_Xe4Truc_DuongTinh" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaKho_XeRM3Truc_DuongTinh" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaKho_XeRM4Truc_DuongTinh" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaKho_XeRM5Truc_DuongTinh" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaKho_TongCong_DuongTinh" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaKho_TongCong_DuongTinh8" runat="server" Text=""></asp:Label></td>
                                    <td class="text-center"></td>
                                </tr>
                                <tr>
                                    <td nowrap class="padding"><strong>Tổng    cộng</strong></td>
<td class="text-center"><asp:Label ID="lblQuaKho_Xe2Truc_TongCong" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaKho_Xe3Truc_TongCong" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaKho_Xe4Truc_TongCong" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaKho_XeRM3Truc_TongCong" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaKho_XeRM4Truc_TongCong" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaKho_XeRM5Truc_TongCong" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaKho_TongCong_TongCong" runat="server" Text=""></asp:Label></td>
<td class="text-center"><asp:Label ID="lblQuaKho_TongCong_TongCong8" runat="server" Text=""></asp:Label></td>
                                    <td class="text-center"></td>
                                </tr>
                            </table>
                            <div class="clear"></div>
                            <div class="warning">*Ghi chú: Mỗi Giấy Phép Lưu  hành xe cấp cho cả hai tuyến (Quốc lộ và Đường tỉnh)</div>
                        </div>
                    </li>
                    <li>
                        <input type="radio" name="tabs" id="tab3">
                        <label for="tab3">QUÁ TẢI TRỌNG</label>
                        <div id="tab-content3" class="tab-content animated fadeIn">
                            <div class=" text-right border-bottom_dash">PHỤ LỤC 6c</div>
                            <div class="">
                                <div class="row">
                                    <div class="grid_4  text-center style-text-3">UBND TỈNH LONG AN<br>
                                        <span class="border-bottom"><strong>SỞ GIAO THÔNG VẬN TẢI</strong></span><br>
                                        Số:               /SGTVT-GLHX.</div>
                                    <div class="grid_7  text-center style-text-3">
                                        CỘNG HOÀ XÃ HỘI CHỦ NGHĨA VIỆT NAM<br>
                                        <span class="border-bottom">Độc lập - Tự do - Hạnh phúc</span><br>
                                        <em><asp:Label ID="lblCurrentDateQT" runat="server" Text=""></asp:Label></em>
                                    </div>
                                </div>
                            </div>
                            <div class="clear"></div>
                            <h1 class="container_table text-center">BÁO CÁO CẤP GIẤY PHÉP LƯU HÀNH CHO XE QUÁ TẢI TRỌNG</h1>
                            <div class="container_table text-center"><asp:Label ID="lblFromDateToDateQT" runat="server" Text=""></asp:Label></div>
                            <div class="container_table text-center"><strong>Kính gửi: Tổng Cục Đường Bộ Việt Nam</strong></div>
                            <table border="1" bordercolor="#dcdcdc" cellspacing="0" cellpadding="0" width="100%" class="report-table style-text-3">
                                <tr class="bg-5">
                                    <td width="6%"><strong>&nbsp;</strong></td>
                                    <td width="9%" class="padding style-text-4"><strong>Xe 02 trục đơn</strong></td>
                                    <td width="7%" class="padding style-text-4"><strong>Xe 03 trục</strong></td>
                                    <td width="7%" class="padding style-text-4"><strong>Xe 04 trục</strong></td>
                                    <td width="12%" class="padding style-text-4"><strong>Tổ hợp xe - sơ mi rơ mooc (03    trục)</strong></td>
                                    <td width="12%" class="padding style-text-4"><strong>Tổ hợp xe - sơ mi rơ mooc (04    trục)</strong></td>
                                    <td width="12%" class="padding style-text-4"><strong>Tổ hợp xe - sơ mi rơ mooc (05    trục)</strong></td>
                                    <td width="7%" class="padding style-text-4"><strong>Tổng cộng</strong></td>
                                    <td width="13%" class="padding style-text-4"><strong>Vận chuyển hàng quá tải trọng</strong></td>
                                    <td width="15%" class="padding text_center style-text-4"><strong>Ghi chú</strong></td>
                                </tr>
                                <tr class="text-center">
                                    <td class="text-center">&nbsp;</td>
                                    <td class="text-center padding-1">1</td>
                                    <td class="text-center">2</td>
                                    <td class="text-center">3</td>
                                    <td class="text-center">4</td>
                                    <td class="text-center">5</td>
                                    <td class="text-center">6</td>
                                    <td class="text-center">7</td>
                                    <td class="text-center">8</td>
                                    <td class="text-center">9</td>
                                </tr>
                                <tr>
                                    <td nowrap class="padding">Quốc    lộ </td>
                                    <td class="text-center"><asp:Label ID="lblQuaTai_Xe2Truc_QuocLo" runat="server" Text=""></asp:Label></td>
                                    <td class="text-center"><asp:Label ID="lblQuaTai_Xe3Truc_QuocLo" runat="server" Text=""></asp:Label></td>
                                    <td class="text-center"><asp:Label ID="lblQuaTai_Xe4Truc_QuocLo" runat="server" Text=""></asp:Label></td>
                                    <td class="text-center"><asp:Label ID="lblQuaTai_XeRM3Truc_QuocLo" runat="server" Text=""></asp:Label></td>
                                    <td class="text-center"><asp:Label ID="lblQuaTai_XeRM4Truc_QuocLo" runat="server" Text=""></asp:Label></td>
                                    <td class="text-center"><asp:Label ID="lblQuaTai_XeRM5Truc_QuocLo" runat="server" Text=""></asp:Label></td>
                                    <td class="text-center"><asp:Label ID="lblQuaTai_TongCong_QuocLo" runat="server" Text=""></asp:Label></td>
                                    <td class="text-center"><asp:Label ID="lblQuaTai_TongCong_QuocLo8" runat="server" Text=""></asp:Label></td>

                                    <td class="text-center"></td>
                                </tr>
                                <tr>
                                    <td nowrap class="padding">Đường    tỉnh</td>
                                    <td class="text-center"><asp:Label ID="lblQuaTai_Xe2Truc_DuongTinh" runat="server" Text=""></asp:Label></td>
                                    <td class="text-center"><asp:Label ID="lblQuaTai_Xe3Truc_DuongTinh" runat="server" Text=""></asp:Label></td>
                                    <td class="text-center"><asp:Label ID="lblQuaTai_Xe4Truc_DuongTinh" runat="server" Text=""></asp:Label></td>
                                    <td class="text-center"><asp:Label ID="lblQuaTai_XeRM3Truc_DuongTinh" runat="server" Text=""></asp:Label></td>
                                    <td class="text-center"><asp:Label ID="lblQuaTai_XeRM4Truc_DuongTinh" runat="server" Text=""></asp:Label></td>
                                    <td class="text-center"><asp:Label ID="lblQuaTai_XeRM5Truc_DuongTinh" runat="server" Text=""></asp:Label></td>
                                    <td class="text-center"><asp:Label ID="lblQuaTai_TongCong_DuongTinh" runat="server" Text=""></asp:Label></td>
                                    <td class="text-center"><asp:Label ID="lblQuaTai_TongCong_DuongTinh8" runat="server" Text=""></asp:Label></td>
                                    <td class="text-center"></td>
                                </tr>
                                <tr>
                                    <td nowrap class="padding"><strong>Tổng    cộng</strong></td>
                                    <td class="text-center"><asp:Label ID="lblQuaTai_Xe2Truc_TongCong" runat="server" Text=""></asp:Label></td>
                                    <td class="text-center"><asp:Label ID="lblQuaTai_Xe3Truc_TongCong" runat="server" Text=""></asp:Label></td>
                                    <td class="text-center"><asp:Label ID="lblQuaTai_Xe4Truc_TongCong" runat="server" Text=""></asp:Label></td>
                                    <td class="text-center"><asp:Label ID="lblQuaTai_XeRM3Truc_TongCong" runat="server" Text=""></asp:Label></td>
                                    <td class="text-center"><asp:Label ID="lblQuaTai_XeRM4Truc_TongCong" runat="server" Text=""></asp:Label></td>
                                    <td class="text-center"><asp:Label ID="lblQuaTai_XeRM5Truc_TongCong" runat="server" Text=""></asp:Label></td>
                                    <td class="text-center"><asp:Label ID="lblQuaTai_TongCong_TongCong" runat="server" Text=""></asp:Label></td>
                                    <td class="text-center"><asp:Label ID="lblQuaTai_TongCong_TongCong8" runat="server" Text=""></asp:Label></td>
                                    <td class="text-center"></td>
                                </tr>
                            </table>
                            <div class="clear"></div>
                            <div class="warning">*Ghi chú: Mỗi Giấy Phép Lưu  hành xe cấp cho cả hai tuyến (Quốc lộ và Đường tỉnh)</div>
                        </div>
                    </li>
                </ul>

            </asp:Panel>
		</div>
	</div>
	<div class="clear"></div>
</div>
<div>
	<div class="row">
		<div class="grid_12">
			<h2>
				BÁO CÁO TÌNH HÌNH, KẾT QUẢ GIẢI QUYẾT THỦ TỤC HÀNH CHÍNH 
			</h2>
            <div class="form">
				<div class="row line">
                    <div class="grid_2">
                       Chọn quý:                        
                    </div>
                    <div class="grid_10">
                        <asp:DropDownList ID="ddlQuarterPicker" runat="server"></asp:DropDownList>
                    </div>
                    <div class="clear"></div>
				</div>
                <div>
                    <div class="grid_2">
                        <asp:Button ID="btnExportExcelQuarter" runat="server" Text="Xuất báo cáo" CssClass="button btnExportExcel" align="middle" OnClick="btnExportExcelQuarter_Click" CausesValidation="false" />
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