<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreviewInBienNhan.aspx.cs" Inherits="LongAn.DVC.Layouts.LongAn.DVC.PreviewInBienNhan" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <link rel="stylesheet" href="/_layouts/15/LongAn.DVC/AppPages/css/normalize.css"/>
    <link rel="stylesheet" href="/_layouts/15/LongAn.DVC/AppPages/css/fluid_grid.css"/>
    <link rel="stylesheet" href="/_layouts/15/LongAn.DVC/AppPages/css/jquery-ui.min.css"/>
    <link rel="stylesheet" href="/_layouts/15/LongAn.DVC/AppPages/css/superfish.css"/>
    <link rel="stylesheet" href="/_layouts/15/LongAn.DVC/AppPages/css/main.css"/>    
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div class="container_12">
        <div class="row">
            <div class="grid_6  text-center">
                UBND TỈNH LONG AN<br>
                <span class="border-bottom"><strong>SỞ GIAO THÔNG VẬN TẢI</strong></span><br>
                BỘ PHẬN TIẾP NHẬN & TRẢ KẾT QUẢ
                <br>
                Số:               /SGTVT-GLHX.        
            </div>
            <div class="grid_6  text-center">
                <strong>CỘNG HOÀ XÃ HỘI CHỦ NGHĨA VIỆT NAM</strong><br>
                <span class="border-bottom"><strong>Độc lập - Tự do - Hạnh phúc</strong></span><br>
                <em>Long An, ngày <asp:Literal ID="literalNgay" runat="server"></asp:Literal> 
                    tháng <asp:Literal ID="literalThang" runat="server"></asp:Literal> 
                    năm <asp:Literal ID="literalNam" runat="server"></asp:Literal></em>
            </div>
        </div>
    </div>
    <div class="clear"></div>
    <h1 class="text-center" style="font-size: 20px">BIÊN NHẬN HỒ SƠ </h1>
    <div class="row text-center">
        <h1>CẤP PHÉP LƯU HÀNH XE QUÁ TẢI TRỌNG, XE QUÁ KHỔ GIỚI HẠN, XE VẬN CHUYỂN HÀNG SIÊU TRƯỜNG, SIÊU TRỌNG TRÊN ĐƯỜNG BỘ</h1>
    </div>
    <div class="container_12 pad_left_c1">
        Bộ phận tiếp nhận & Trả kết quả có nhận được hồ sơ: <span class="text-bold">Cấp phép lưu hành xe quá tải trọng, xe quá khổ giới hạn, xe vận chuyển hàng siêu trường, siệu trọng trên đường bộ.</span>
    </div>
    <div class="clear"></div>
    <div class="container_12 border-bottom_dash">
        <div class=" grid_3">Người nộp :</div>
        <div class=" grid_8 text-bold">
            <asp:Literal ID="literalCaNhanToChuc" runat="server"></asp:Literal>
        </div>
    </div>
    <div class="container_12 border-bottom_dash">
        <div class=" grid_3">Địa chỉ đăng ký :</div>
        <div class=" grid_8 text-bold">
            <asp:Literal ID="literalDiaChi" runat="server"></asp:Literal>
        </div>
    </div>
    <div class="container_12 border-bottom_dash">
        <div class=" grid_3">Thông tin liên lạc :</div>
        <div class=" grid_8 text-bold">
            <asp:Literal ID="literalDienThoai" runat="server"></asp:Literal>
        </div>
    </div>
    <div class="clear"></div>
    <div class="container_12">
        <table border="1" cellspacing="0" cellpadding="0" width="100%" style="margin-left: 10px; margin-top: 15px;">
            <tr>
                <td width="5%" rowspan="2" class="text-center">STT</td>
                <td width="65%" rowspan="2">Các loại chứng từ có trong hồ sơ</td>
                <td width="30%" colspan="2" class="text-center">Số lượng tờ của    mỗi bản</td>
            </tr>
            <tr>
                <td width="15%" class="text-center">Bản chính</td>
                <td width="15%" class="text-center">Bản sao</td>
            </tr>
            <tr>
                <td width="45" class="text-center">1</td>
                <td width="347">Bản cam kết bảo vệ môi trường</td>
                <td class="text-center">3</td>
                <td class="text-center">&nbsp;</td>
            </tr>
            <tr>
                <td width="45" class="text-center">2</td>
                <td width="347">Dự án đầu tư ( báo cáo nghiên cứu khả thi )</td>
                <td class="text-center">1</td>
                <td class="text-center">&nbsp;</td>
            </tr>
            <tr>
                <td width="45" class="text-center">3</td>
                <td width="347">Phương án sản xuất,kinh doanh</td>
                <td class="text-center">1</td>
                <td class="text-center">&nbsp;</td>
            </tr>
        </table>
    </div>
    <div class="clear"></div>
    <div class="container_12 border-bottom_dash style-text-3">
        <div class=" grid_3">Hẹn đến ngày :</div>
        <div class=" grid_8 text-bold">
            <asp:Literal ID="literalNgayHenTra" runat="server"></asp:Literal> 
            trở lại để được thông báo kết quả. </div>
    </div>
    <div class="container_12 border-bottom_dash">
        <div class=" grid_3">Địa chỉ  :</div>
        <div class=" grid_8 text-bold">76 Hùng Vương – Phường 2 – Thành Phố Tân An – Long An.</div>
    </div>
    <div class="container_12 border-bottom_dash">
        <div class=" grid_3">Điện thoại  :</div>
        <div class=" grid_8 text-bold">072.831.725</div>
    </div>
    <div class=" clear"></div>
    <div class="container_12">
        <div class="row">
            <div class="grid_6 text-center style-text-3">
                <span class=" text-bold"><strong>NGƯỜI NỘP HỒ SƠ</span><br>
                ( Ký và ghi rõ họ tên)                                                                                                                                                                               
                <br>
                <br>
                <br>
                <br>
                <br>
                <asp:Literal ID="literalCaNhanToChucKy" runat="server"></asp:Literal>                                                                                   
            </div>
            <div class="grid_6  text-center style-text-3">
                <strong>SƠ                                                                                     NGƯỜI NHẬN HỒ SƠ</strong><br>
                <span class=" text-bold">Ký và ghi rõ họ tên)</span><br>
                <br>
                <br>
                <br>
                <br>
                <em>Nguyễn Thị Hương Mai</em>
            </div>
        </div>
    </div>
    <div class="container_12 pad_left_c1 border-top bg-2">
        <h1 style="font-size: 40px; border-top: 1px #ccc dotted">*
            <asp:Literal ID="literalTitle1" runat="server"></asp:Literal>
            *</h1>
    </div>
    <div class="container_12 pad_left_c1  bg-2">
        - Tra cứu qua SMS: Nhập <strong>
            <asp:Literal ID="literalTitle2" runat="server"></asp:Literal>
        </strong> gởi đến số <strong>072.3618888</strong>
    </div>
    <div class="container_12 pad_left_c1 bg-2 ">
        - Tra cứu qua điện thoại: gọi <strong>0723888888</strong> và nhấn 
        <strong>
            <asp:Literal ID="literalTitle3" runat="server"></asp:Literal>
        </strong>
    </div>
    <div class="container_12 pad_left_c1 bg-2">
        - Tra cứu qua website: Truy cập <strong>motcua.longan.gov.vn</strong> sử dụng 
        <strong><asp:Literal ID="literalTitle4" runat="server"></asp:Literal></strong> để tra cứu
    </div>

    <div class="container_12" align="center">
        <button class="btt style-text-3" type="submit">Xuất biên nhận</button>
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
In Biên Nhận
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
In Biên Nhận
</asp:Content>
