<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeNghiView.ascx.cs" Inherits="LongAn.DVC.WebParts.DeNghiView.DeNghiView" %>
<link rel="stylesheet" href="/_layouts/15/LongAn.Form/css/bootstrap.min.css"/>
<!--[if lt IE9]>
<link rel="stylesheet" href="/_layouts/15/LongAn.Form/css/bootstrap-non-responsive.min.css"/>
<![endif]-->
<link rel="stylesheet" href="/_layouts/15/LongAn.Form/css/jquery-ui.structure.min.css"/>
<link rel="stylesheet" href="/_layouts/15/LongAn.Form/css/jquery-ui.min.css"/>
<link rel="stylesheet" href="/_layouts/15/LongAn.Form/css/bootstrap-theme.min.css"/>
<link rel="stylesheet" href="/_layouts/15/LongAn.Form/css/form.main.css"/>
<script src="/_layouts/15/LongAn.Form/js/jquery.js"></script>
<script src="/_layouts/15/LongAn.Form/js/jquery-ui.min.js"></script>
<script src="/_layouts/15/LongAn.Form/js/datepicker-vi.js"></script>
<script src="/_layouts/15/LongAn.Form/js/modernizr.js"></script>
<script src="/_layouts/15/LongAn.Form/js/detectizr.min.js"></script>
<script src="/_layouts/15/LongAn.Form/js/scripts.js"></script>
<div class="container1">
    <div class="row">
        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9">
            <div id="top-control">
                <a href="#" class="button large-button home">Trang chủ</a>
                <a href="#" class="button large-button add-new">Thêm mới hồ sơ</a>
                <a href="#" class="button large-button added">Hồ sơ đã tiếp nhận</a>
                <a href="#" class="button large-button process">Hồ sơ đang xử lý</a>
                <a href="#" class="button large-button waiting">Hồ sơ chờ cấp phép</a>
                <a href="#" class="button large-button done">Hồ sơ hoàn thành</a>
                <a href="#" class="button large-button stat">Báo cáo, thống kê</a>
                <div class="clearfix"></div>
            </div>
            <h2 class="page-header">
                <asp:Literal ID="literalTitle" runat="server"></asp:Literal>
            </h2>
            <div class="the-form">
                <div class="row line">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                        <input type="text" name="s" id="s"/>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <button class="button search">Tìm kiếm (*)</button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <label><input type="radio" name="match"/>Tìm chính xác</label>
                        <label><input type="radio" name="match"/>Tất cả</label>
                        <label><input type="radio" name="match"/>Từ bất kỳ</label>
                    </div>
                </div>
            </div>
            <div class="pull-right">
                <a class="button add-new" href="#">Thêm mới</a>
            </div>
            <div class="clearfix"></div>

            <table class="the-table">
                <tr>
                    <th>Biên nhận</th>
                    <th>Cá nhân tổ chức</th>
                    <th>Đơn vị đăng ký</th>
                    <th>Loại </th>
                    <th>Ngày đăng ký</th>
                    <th></th>
                    <th></th>
                    <th></th>
                    <th></th>
                </tr>
                <asp:Repeater ID="rptDeNghi" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>46</td>
                            <td>SGTVTLA-00000033</td>
                            <td>22/08/2014</td>
                            <td>
                                Công ty Tin học ABC
                            </td>
                            <td>
                                Cấp giấy phép A
                            </td>
                            <td>
                                <a href="#" class="button attachment just-icon"></a>
                            </td>
                            <td>
                                <a href="#" class="button printer just-icon"></a>
                            </td>
                            <td>
                                <a href="#" class="button view just-icon"></a>
                            </td>
                            <td>
                                <a href="#" class="button edit just-icon"></a>
                            </td>
                            <td>
                                <label><input type="checkbox"/>Xóa</label>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            
            <div class="pag pull-right">
                <label for="go-to">Trang</label>
                <button class="button begin small"></button>
                <button class="button prev small"></button>
                <input type="text" id="go-to" value="1" class="small"/>
                <button class="button next small"></button>
                <button class="button end small"></button>
                <div class="clearfix"></div>
                Kết quả: 1 to 12 of 12
            </div>
            <div class="clearfix"></div>
        </div>
    </div>
</div>