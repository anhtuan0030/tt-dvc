<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeNghiListView.ascx.cs" Inherits="LongAn.DVC.WebParts.DeNghiListView.DeNghiListView" %>
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC/css/bootstrap.min.css"/>
<!--[if lt IE9]>
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC/css/bootstrap-non-responsive.min.css"/>
<![endif]-->
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC/css/jquery-ui.structure.min.css"/>
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC/css/jquery-ui.min.css"/>
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC/css/bootstrap-theme.min.css"/>
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC/css/form.main.css"/>
<script src="/_layouts/15/LongAn.DVC/js/jquery.js"></script>
<script src="/_layouts/15/LongAn.DVC/js/jquery-ui.min.js"></script>
<script src="/_layouts/15/LongAn.DVC/js/modernizr.js"></script>
<script src="/_layouts/15/LongAn.DVC/js/detectizr.min.js"></script>
<script src="/_layouts/15/LongAn.DVC/js/scripts.js"></script>
<div class="container1">
    <div class="row">
        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9">
            <div id="top-control">
                <asp:HyperLink ID="hplTrangChu" CssClass="button large-button home" runat="server">Trang chủ</asp:HyperLink>
                <asp:HyperLink ID="hplHoSoDaTiepNhan" CssClass="button large-button added" runat="server">Hồ sơ đã tiếp nhận</asp:HyperLink>
                <asp:HyperLink ID="hplHoSoDangXuLy" CssClass="button large-button process" runat="server">Hồ sơ đang xử lý</asp:HyperLink>
                <asp:HyperLink ID="HyperLink1" CssClass="button large-button waiting" runat="server">Hồ sơ chờ cấp phép</asp:HyperLink>
                <asp:HyperLink ID="HyperLink2" CssClass="button large-button done" runat="server">Hồ sơ hoàn thành</asp:HyperLink>
                <asp:HyperLink ID="HyperLink3" CssClass="button large-button stat" runat="server">Báo cáo, thống kê</asp:HyperLink>
                <div class="clearfix"></div>
            </div>
            <h2 class="page-header">
                <asp:Literal ID="literalDeNghiTitle" runat="server"></asp:Literal>
            </h2>
            <div class="the-form searchform">
                <div class="row line">
                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                        <asp:TextBox ID="txtTuKhoa" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                        <asp:Button ID="btnTimKiem" runat="server" Text="Tìm kiếm" CssClass="button search" />
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
                <asp:LinkButton ID="lbtAddNew" CssClass="button add-new" runat="server">Thêm mới</asp:LinkButton>
            </div>
            <div class="clearfix"></div>

            <table class="the-table">
                <tr>
                    <th>STT</th>
                    <th>Biên nhận</th>
                    <th>Cá nhân tổ chức</th>
                    <th>Loại đề nghị</th>
                    <th>Ngày tạo</th>
                    <th></th>
                    <th></th>
                    <th></th>
                    <th></th>
                </tr>
                <asp:Repeater ID="repeaterLists" runat="server"  OnItemCommand="repeaterLists_ItemCommand" OnItemDataBound="repeaterLists_ItemDataBound">
                    <ItemTemplate>
                        <tr class="<%#(((RepeaterItem)Container).ItemIndex+1) % 2 == 0 ? "even" : "odd" %>">
                            <td>
                                <asp:Literal ID="literalSTT" runat="server" Text="<%#(((RepeaterItem)Container).ItemIndex+1) %>"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal ID="literalTitle" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal ID="literalCaNhanToChuc" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal ID="literalLoaiCapPhep" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:Literal ID="literalNgayDeNghi" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbtViewItem" CssClass="button view just-icon" runat="server"></asp:LinkButton>
                            </td>
                            <td>
                                <a href="#" class="button printer just-icon"></a>
                            </td>
                            <td>
                                
                                <a href="#" class="button attachment just-icon"></a>
                            </td>
                            <td>
                                <a href="#" class="button edit just-icon"></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            
            <div id="divPagging" visible="false" runat="server" class="pag pull-right">
                <%--<button class="button begin small"></button>--%>
                <asp:LinkButton ID="lbtnFirst" runat="server" CssClass="button begin small" CausesValidation="false" OnClick="lbtnFirst_Click"></asp:LinkButton>
                <%--<button class="button prev small"></button>--%>
                <asp:LinkButton ID="lbtnPrevious" runat="server" CssClass="button prev small" CausesValidation="false" OnClick="lbtnPrevious_Click"></asp:LinkButton>
                <asp:Repeater ID="repeaterPage" runat="server" OnItemCommand="repeaterPage_ItemCommand" OnItemDataBound="repeaterPage_ItemDataBound">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkbtnPaging" runat="server" CommandArgument='<%# Eval("PageIndex") %>'
                            CommandName="Paging" Text='<%# Eval("PageText") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:LinkButton ID="lbtnNext" runat="server" CssClass="button next small" CausesValidation="false"
                    OnClick="lbtnNext_Click"></asp:LinkButton>
                <%--<button class="button next small"></button>--%>
                <asp:LinkButton ID="lbtnLast" runat="server" CssClass="button end small" CausesValidation="false"
                    OnClick="lbtnLast_Click"></asp:LinkButton>
                <%--<button class="button end small"></button>--%>
                <div class="clearfix"></div>
            </div>
            
            <div class="clearfix"></div>
        </div>
    </div>
</div>