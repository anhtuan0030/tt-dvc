<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeNghiListViewUserUserControl.ascx.cs" Inherits="LongAn.DVC.WebParts.DeNghiListViewUser.DeNghiListViewUserUserControl" %>
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC.Form/css/normalize.css"/>
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC.Form/css/fluid_grid.css"/>
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC.Form/css/jquery-ui.min.css"/>
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC.Form/css/superfish.css"/>
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC.Form/css/main.css"/>
<link rel="stylesheet" href="/_layouts/15/LongAn.DVC.Form/css/main1.css"/>
<link href="/_layouts/15/LongAn.DVC.Form/css/form.main.custom.css" rel="stylesheet" />

<style>

#searchform .button {
    width: 120px;
}
</style>

<script>
    $(document).ready(function () {
        var searchExpandValue = $("[id$='_hdfSearchExpand']").val();
        if (searchExpandValue == "1") {
            $("#linkSearchExpand").addClass("expanded");
            $("#searchform").show();
        }
        $("#linkSearchExpand").click(function () {
            $("#searchform").toggle("slow");
            if ($("[id$='_hdfSearchExpand']").val() == "1") {
                $("[id$='_hdfSearchExpand']").val("0");
                $("#linkSearchExpand").removeClass("expanded");
            }
            else {
                $("[id$='_hdfSearchExpand']").val("1");
                $("#linkSearchExpand").addClass("expanded");
            }
        });
    });
</script>

<div class="">
	<div class="row">
		<div class="grid_12" id="main-frame">
			<div class="row">
                <div class="pull-left">
                    <h2 class="page-header">
				        <asp:Literal ID="literalDeNghiTitle" runat="server"></asp:Literal>
			        </h2>
                </div>
                <div class="pull-right">
                    <div class="pull-left-search">
                        <a id="linkSearchExpand" href="#" class="button button-expand inline-block">Tìm kiếm</a>
                        <asp:HiddenField ID="hdfSearchExpand" Value="1" runat="server" />
                        <asp:HiddenField ID="hdfCurrentUrl" Value="0" runat="server" />
                        <asp:HiddenField ID="hdfDeNghiUrl" Value="0" runat="server" />
                    </div>
                    <div class="pull-left-add" id="divAddNew" runat="server" visible="false">
                        <asp:HyperLink ID="hplAddNew" CssClass="button add-new inline-block danger" runat="server">Thêm mới</asp:HyperLink>
			        </div>
                </div>
                
                <div class="clear"></div>
            </div>
			
			<div class="the-form" id="searchform" style="display:none;">
				<div class="row line">
					<div class="grid_2" style="text-align:right;">
						Mã biên nhận:
					</div>
					<div class="grid_3">
						<asp:TextBox ID="txtTuKhoa" Text="" runat="server"></asp:TextBox>
					</div>

					<div class="grid_2" style="text-align:right;">
						Đơn vị:
					</div>
					<div class="grid_4">
						<asp:TextBox ID="txtCaNhanToChuc" runat="server"></asp:TextBox>
					</div>
					<div class="clear"></div>
				</div>
                <div class="row line">
					<div class="grid_2" style="text-align:right;">
						Số điện thoại:
					</div>
					<div class="grid_3">
						<asp:TextBox ID="txtSoDienThoai" runat="server"></asp:TextBox>
					</div>
                    
					<div class="grid_2" style="text-align:right;">
						Ngày đề nghị:
					</div>
					<div class="grid_2">
						<SharePoint:DateTimeControl ID="dtcNgayDeNghiTu" DateOnly="true" LocaleId="1066" runat="server" />
					</div>
                    <div class="grid_2">
                        <SharePoint:DateTimeControl ID="dtcNgayDeNghiDen" DateOnly="true" LocaleId="1066" runat="server" />
                    </div>
					<div class="clear"></div>
				</div>
				<div class="row line">
                    <div class="grid_5">&nbsp;</div>
					<div class="grid_2">
						<asp:Button ID="btnTimKiem" runat="server" Text="Tìm kiếm" CssClass="button search" align="middle" />
					</div>
                    <div class="grid_5">&nbsp;</div>
					<div class="clear"></div>
				</div>
			</div>

            <div class="clearfix"></div>
			
            <table class="the-table">
				<tr style="height: 50px;">
                    <th>STT</th>
                    <th>Biên nhận</th>
                    <th>Tên Cá nhân/Tổ chức</th>
                    <th>Loại đề nghị</th>
                    <th>Ngày nộp</th>
                    <th>Trạng thái</th>
                    <th colspan="1">Xóa</th>
                </tr>
                <asp:Repeater ID="repeaterLists" runat="server"  OnItemCommand="repeaterLists_ItemCommand" OnItemDataBound="repeaterLists_ItemDataBound">
                    <ItemTemplate>
                        <tr class="<%#(((RepeaterItem)Container).ItemIndex+1) % 2 == 0 ? "even" : "odd" %>">
                            <td>
                                <asp:Literal ID="literalSTT" runat="server" Text="<%#(((RepeaterItem)Container).ItemIndex+1) %>"></asp:Literal>
                            </td>
                            <td>
                                <%--<asp:Literal ID="literalTitle" runat="server"></asp:Literal>--%>
                                <asp:HyperLink ID="hplTitle" ToolTip="" runat="server"></asp:HyperLink>
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
                                <asp:Literal ID="literalTrangThai" runat="server"></asp:Literal>
                            </td>
                            
                            <td>
                                <asp:LinkButton ID="lbtDelete" style="display:none;" runat="server">Xóa</asp:LinkButton>
                                <%--<asp:HyperLink ID="hplXuLy" ToolTip="Xử lý hồ sơ" CssClass="button view just-icon" runat="server"></asp:HyperLink>--%>
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
			<div class="clear"></div>
            
		</div>
	</div>
	<div class="clear"></div>
</div>