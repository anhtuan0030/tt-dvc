<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreviewInGiayPhep.aspx.cs" Inherits="LongAn.DVC.Layouts.LongAn.DVC.PreviewInGiayPhep" DynamicMasterPageFile="~masterurl/default.master" %>

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
            <div class="grid_4  text-center">UBND TỈNH LONG AN<br>
                <span class="border-bottom"><strong>SỞ GIAO THÔNG VẬN TẢI</strong></span><br>
                Số:               /SGTVT-GLHX.</div>
            <div class="grid_7  text-center">CỘNG HOÀ XÃ HỘI CHỦ NGHĨA VIỆT NAM<br>
                <span class="border-bottom">Độc lập - Tự do - Hạnh phúc</span><br>
                Long An, ngày 
                <asp:Literal ID="literalNgay" runat="server"></asp:Literal> tháng 
                <asp:Literal ID="literalThang" runat="server"></asp:Literal> năm 
                <asp:Literal ID="literalNam" runat="server"></asp:Literal>
            </div>
        </div>
    </div>
    </div>
    <div class="clear"></div>
    <h1 class="text-center" style="font-size: 17px">GIẤY PHÉP LƯU HÀNH XE QUÁ TẢI TRỌNG, XE QUÁ KHỔ GIỚI HẠN, XE VẬN CHUYỂN
        <br>
        HÀNG SIÊU TRƯỜNG, SIÊU TRỌNG
 TRÊN ĐƯỜNG BỘ </h1>
    <div class="row text-center">Có giá trị đến hết ngày 
        <asp:Literal ID="literalHieuLucNgay" runat="server"></asp:Literal> tháng 
        <asp:Literal ID="literalHieuLucThang" runat="server"></asp:Literal> năm 
        <asp:Literal ID="literalHieuLucNam" runat="server"></asp:Literal>
    </div>

    <div class="container_12">
        <p>
            - Căn cứ Khoản 2 Điều 28 Luật Giao thông đường  bộ ngày 13/11/2008;<br>
            - Căn cứ Thông tư số 07/2010/TT-BGTVT ngày  11/02/2010 của Bộ trưởng Bộ Giao thông vận tải quy định về tải trọng, khổ giới  hạn của đường bộ; lưu hành xe quá tải trọng, xe quá khổ giới hạn, xe bánh xích  trên đường bộ; vận chuyển hàng siêu trường, siêu trọng; giới hạn xếp hàng hóa  trên phương tiện giao thông đường bộ khi tham gia giao thông trên đường bộ;<br>
            - Căn cứ Thông tư số 03/2011/TT-BGTVT ngày  22/02/2011 của Bộ trưởng Bộ Giao thông vận tải về sửa đổi, bổ sung Thông tư số  07/2010/TT-BGTVT ngày 11/02/2010 của của Bộ trưởng Bộ Giao thông vận tải về quy  định về tải trọng, khổ giới hạn của đường bộ; lưu hành xe quá tải trọng, xe quá  khổ giới hạn, xe bánh xích trên đường bộ; vận chuyển hàng siêu trường, siêu  trọng; giới hạn xếp hàng hóa trên phương tiện giao thông đường bộ khi tham gia  giao thông trên đường bộ;<br>
            - Căn cứ Thông tư số.65/2013/TT-BGTVT ngày 31 tháng 12 năm 2013 của Bộ trưởng Bộ  Giao thông vận tải về sửa đổi, bổ sung một số điều của Thông tư số  07/2010/TT-BGTVT ngày 11/02/2010 của Bộ trưởng Bộ Giao thông vận tải quy định  về tải trọng, khổ giới hạn của đường bộ; lưu hành xe quá tải trọng, xe quá khổ  giới hạn, xe bánh xích trên đường bộ; vận chuyển hàng siêu trường, siêu trọng;  giới hạn xếp hàng hóa trên phương tiện giao thông đường bộ khi tham gia giao  thông trên đường bộ;<br>
            - Xét hồ sơ đề nghị  cấp giấy phép lưu hành xe quá tải trọng, xe quá khổ giới hạn của 
            <strong>
                <asp:Literal ID="literalCaNhanToChuc" runat="server"></asp:Literal>
            </strong> ngày 
            <asp:Literal ID="literalNgayNop" runat="server"></asp:Literal>,
        </p>
    </div>
    <div class="container_12">
        <h1 class="bg-2">Cho phép lưu hành xe quá tải trọng, xe quá khổ giới hạn trên đường bộ, cụ thể như sau:</h1>
        <div class=" grid_3">
            Xe (nhãn hiệu xe):  
        </div>
        <div class=" grid_3">
            <asp:Literal ID="literalNhanHieuXe" runat="server"></asp:Literal>
        </div>
        <div class=" grid_2 text-right">
            Biển số đăng ký: 
        </div>
        <div class=" grid_4">
            <asp:Literal ID="literalBienSoDangKy" runat="server"></asp:Literal>
        </div>

        <div class=" grid_3">
            kéo sơ mi rơ moóc/rơ moóc:  
        </div>
        <div class=" grid_3">
            <asp:Literal ID="literalNhanHieuRoMooc" runat="server"></asp:Literal>
        </div>
        <div class=" grid_2 text-right">
            Biển số đăng ký: 
        </div>
        <div class=" grid_4">
            <asp:Literal ID="literalBienSoDangKyRoMooc" runat="server"></asp:Literal>
        </div>
    </div>
    <div class="container_12 border-bottom_dash">
        <div class=" grid_3">
            Của:  
        </div>
        <div class=" grid_9">
            <asp:Literal ID="literalCaNhanToChuc2" runat="server"></asp:Literal>
        </div>
    </div>
    <div class="container_12 border-bottom_dash">
        <h1 class="bg-2">Với các thông số như sau:</h1>
        <div class=" grid_3 color_green">
            Loại hàng hóa:   
        </div>
        <div class=" grid_3">
            <asp:Literal ID="literalLoaiHang" runat="server"></asp:Literal>
        </div>
        <div class=" grid_2 text-right">
            Sau khi đã tháo bớt phụ kiện (nếu có):  
        </div>
        <div class=" grid_4">
            
        </div>
    </div>
    <div class="container_12 border-bottom_dash">
        <div class=" grid_12 color_green">
            Kích thước toàn bộ xe sau khi xếp hàng lên xe   
        </div>

        <div class=" grid_3">
            &gt;&gt; Chiều dài:   
        </div>
        <div class=" grid_2">
            <span class="color_red pad_left_c1">
                <asp:Literal ID="literalChieDaiToanBoXeKhiXepHang" runat="server"></asp:Literal>
            </span> m
        </div>
        <div class=" grid_4">
            &gt;&gt; Chiều rộng:
        </div>
        <div class=" grid_3">
            <span class="color_red">
                <asp:Literal ID="literalChieuRongToanBoXeKhiXepHang" runat="server"></asp:Literal>
            </span> m
        </div>

        <div class=" grid_3">
            &nbsp;&nbsp;&nbsp;&nbsp; Hàng vượt phía sau thùng xe:  
        </div>
        <div class=" grid_2"><span class="color_red pad_left_c1">
            <asp:Literal ID="literalHangVuotPhiaSauThungXe" runat="server"></asp:Literal>
                             </span> m</div>
        <div class=" grid_4">
            &nbsp;&nbsp;&nbsp;&nbsp; Hàng vượt ra hai bên thùng xe: 
        </div>
        <div class=" grid_3">
            <span class="color_red">
                <asp:Literal ID="literalHangVuotHaiBenThungXe" runat="server"></asp:Literal>
            </span> m
        </div>
        <div class=" grid_3">
            &nbsp;&nbsp;&nbsp;&nbsp; Hàng vượt phía trước thùng xe:  
        </div>
        <div class=" grid_2"><span class="color_red pad_left_c1">
            <asp:Literal ID="literalHangVuotPhiaTruocThungXe" runat="server"></asp:Literal>
                             </span> m</div>
        <div class=" grid_4">
            &gt;&gt; Chiều cao (tính từ mặt đường trở lên): 
        </div>
        <div class=" grid_3">
            <span class="color_red">
                <asp:Literal ID="literalChieuCaoToanBoXeKhiXepHang" runat="server"></asp:Literal>
            </span> m
        </div>
        <div class="clear"></div>
        <div class=" grid_5 color_green">
            Tổng trọng lượng của xe và hàng hóa xếp trên xe: &nbsp;&nbsp;&nbsp;&nbsp; <span class="color_red">xx</span> <span style="color: #333333">tấn</span>
        </div>
        <div class=" grid_6 ">(Trong đó trọng lượng bản thân của xe đầu kéo là …… tấn, sơ mi rơ moóc hoặc rơ moóc là ……… tấn và hàng hóa là …….. tấn).        </div>
        <div class="row">
            <div class=" grid_12 color_green style-text-3">
                Tải trọng lớn nhất được phân bổ lên các trục xe sau khi xếp hàng hóa lên xe: 
            </div>
            <div class=" grid_3 pad_left_c1">
                Trục đơn:    
            </div>
            <div class=" grid_8 ">
                <span class="color_red">xx</span> tấn 
            </div>
            <div class=" grid_3 pad_left_c1">
                Trục kép:    
            </div>
            <div class=" grid_8 ">
                <span class="color_red">xx</span> tấn, khoảng cách giữa hai tâm trục, d= ………………m; 
            </div>
            <div class=" grid_3 pad_left_c1">
                Trục ba:    
            </div>
            <div class=" grid_8 ">
                <span class="color_red">xx</span> tấn, khoảng cách giữa hai tâm trục liền kề, d= ......m. 
            </div>

        </div>
        <div class="row">
            <div class=" grid_2 color_green">
                Nơi đi:    
            </div>
            <div class=" grid_10">
                .....................
            </div>
            <div class=" grid_2 color_green">
                Nơi đến:    
            </div>
            <div class=" grid_10 ">
                .....................
            </div>
            <div class=" grid_2 color_green">
                Các tuyến được đi:
            </div>
            <div class=" grid_10 ">
                .....................
            </div>
        </div>

    </div>

    </div>
    <div class="container_12">
        <h1 class="bg-2">Các điều kiện quy định khi lưu hành xe trên đường bộ:</h1>
        <p>
            - Chủ phương tiện, người lái xe phải tuân thủ, chấp hành các quy định  của Luật Giao thông đường bộ khi lưu hành xe quá tải trọng, xe quá khổ giới  hạn, xe vận chuyển hàng siêu trường, siêu trọng trên đường bộ nhằm đảm bảo an  toàn giao thông.<br>
            - Xe phải có hệ thống hãm đủ hiệu lực (kể cả sơ mi rơ moóc hoặc rơ moóc  kéo theo). Hệ thống liên kết nối xe đầu kéo với sơ mi rơ moóc hoặc rơ moóc phải  chắc chắn, bảo đảm an toàn và đúng quy định của nhà sản xuất.<br>
            - Khi qua cầu, xe chạy đúng làn …………. với tốc độ …………. để tránh gây  xung kích và tránh gây ra sự lệch tâm làm tăng sự ảnh hưởng của tải trọng lên  hệ thống dầm mặt cầu. Nghiêm cấm dừng, đỗ phanh, hãm xe trên cầu.<br>
            - Các điều kiện quy định cần thiết bảo đảm an toàn khác.<br>
            - Phải chịu sự kiểm tra, kiểm soát của các lực lượng kiểm soát giao  thông trên đường.<br>
            - Khi có nhu cầu đổi lại giấy phép lưu hành mới phải nộp lại giấy này.
        </p>
        </p>
    </div>
    <div class="clear"></div>
    <div class="container_12">
        <div class="grid_7  ">
            <strong>Nơi nhận:</strong>
            <br>
            - ............................................................................. ;
	  <br>
            - Thanh tra GTVT; Ban QLDA CTGT;
      <br>
            - Lưu:VT, QLHT(01b). 
        </div>
        <div class="grid_4  text-center"><strong>GIÁM ĐỐC </strong></span></div>
    </div>
    <div class="container_12" align="center">
        <button class="btt style-text-3" type="submit">Xuất giấy phép</button>
    </div>
    <div class="clear"></div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    In Giấy Phép
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    In Giấy Phép
</asp:Content>
