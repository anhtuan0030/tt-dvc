﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LongAn.DVC.WebParts.DeNghiSearch {
    using System.Web.UI.WebControls.Expressions;
    using System.Web.UI.HtmlControls;
    using System.Collections;
    using System.Text;
    using System.Web.UI;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using Microsoft.SharePoint.WebPartPages;
    using System.Web.SessionState;
    using System.Configuration;
    using Microsoft.SharePoint;
    using System.Web;
    using System.Web.DynamicData;
    using System.Web.Caching;
    using System.Web.Profile;
    using System.ComponentModel.DataAnnotations;
    using System.Web.UI.WebControls;
    using System.Web.Security;
    using System;
    using Microsoft.SharePoint.Utilities;
    using System.Text.RegularExpressions;
    using System.Collections.Specialized;
    using System.Web.UI.WebControls.WebParts;
    using Microsoft.SharePoint.WebControls;
    using System.CodeDom.Compiler;
    
    
    public partial class DeNghiSearch {
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.TextBox txtMaBienNhan;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Button btnTimKiem;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Literal literalMaBienNhan;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Literal literalDonVi;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Literal literalTrangThai;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebPartCodeGenerator", "12.0.0.0")]
        public static implicit operator global::System.Web.UI.TemplateControl(DeNghiSearch target) 
        {
            return target == null ? null : target.TemplateControl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.TextBox @__BuildControltxtMaBienNhan() {
            global::System.Web.UI.WebControls.TextBox @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.TextBox();
            this.txtMaBienNhan = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "txtMaBienNhan";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Button @__BuildControlbtnTimKiem() {
            global::System.Web.UI.WebControls.Button @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Button();
            this.btnTimKiem = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "btnTimKiem";
            @__ctrl.Text = "Tìm kiếm";
            @__ctrl.CssClass = "button search button-custom";
            ((System.Web.UI.IAttributeAccessor)(@__ctrl)).SetAttribute("align", "middle");
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Literal @__BuildControlliteralMaBienNhan() {
            global::System.Web.UI.WebControls.Literal @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Literal();
            this.literalMaBienNhan = @__ctrl;
            @__ctrl.ID = "literalMaBienNhan";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Literal @__BuildControlliteralDonVi() {
            global::System.Web.UI.WebControls.Literal @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Literal();
            this.literalDonVi = @__ctrl;
            @__ctrl.ID = "literalDonVi";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Literal @__BuildControlliteralTrangThai() {
            global::System.Web.UI.WebControls.Literal @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Literal();
            this.literalTrangThai = @__ctrl;
            @__ctrl.ID = "literalTrangThai";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private void @__BuildControlTree(global::LongAn.DVC.WebParts.DeNghiSearch.DeNghiSearch @__ctrl) {
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@"

<link rel=""stylesheet"" href=""/_layouts/15/LongAn.DVC.Form/css/normalize.css""/>
<link rel=""stylesheet"" href=""/_layouts/15/LongAn.DVC.Form/css/fluid_grid.css""/>
<link rel=""stylesheet"" href=""/_layouts/15/LongAn.DVC.Form/css/jquery-ui.min.css""/>
<link rel=""stylesheet"" href=""/_layouts/15/LongAn.DVC.Form/css/superfish.css""/>
<link rel=""stylesheet"" href=""/_layouts/15/LongAn.DVC.Form/css/main.css""/>

<style type=""text/css"">
    .button-custom {
        width: 120px !important;
    }
</style>
<div class="""">
	<div class=""row"">
		<div class=""grid_12"" id=""main-frame"">
			<h2 class="""">
				TRA CỨU HỒ SƠ
			</h2>
			<div class=""form"" id=""searchform"">
				<div class=""row line"">
                    <div class=""grid_3"" style=""margin-top:10px"">
                       Nhập mã biên nhận:                        
                    </div>
                    <div class=""grid_4"">
                        "));
            global::System.Web.UI.WebControls.TextBox @__ctrl1;
            @__ctrl1 = this.@__BuildControltxtMaBienNhan();
            @__parser.AddParsedSubObject(@__ctrl1);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                    </div>\r\n                    <div class=\"grid_2\">\r\n         " +
                        "               "));
            global::System.Web.UI.WebControls.Button @__ctrl2;
            @__ctrl2 = this.@__BuildControlbtnTimKiem();
            @__parser.AddParsedSubObject(@__ctrl2);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@"
                    </div>
                    <div class=""clear""></div>
				</div>
			</div>
            <div>
                <h3><u>Kết quả tra cứu</u></h3>
                <div class=""row"" style=""padding: 5px 0"">
                    <div class=""grid_1"">&nbsp;</div>
                    <div class=""grid_3"" style=""text-align:right"">
                       Mã biên nhận:                        
                    </div>
                    <div class=""grid_4"">
                        <b>"));
            global::System.Web.UI.WebControls.Literal @__ctrl3;
            @__ctrl3 = this.@__BuildControlliteralMaBienNhan();
            @__parser.AddParsedSubObject(@__ctrl3);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@"</b>
                    </div>
                    <div class=""clear""></div>
				</div>
                <div class=""row"" style=""padding: 5px 0"">
                    <div class=""grid_1"">&nbsp;</div>
                    <div class=""grid_3"" style=""text-align:right"">
                       Cá nhân / Tổ chức:                        
                    </div>
                    <div class=""grid_4"">
                        <b>"));
            global::System.Web.UI.WebControls.Literal @__ctrl4;
            @__ctrl4 = this.@__BuildControlliteralDonVi();
            @__parser.AddParsedSubObject(@__ctrl4);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@"</b>
                    </div>
                    <div class=""clear""></div>
				</div>
                <div class=""row"" style=""padding: 5px 0"">
                    <div class=""grid_1"">&nbsp;</div>
                    <div class=""grid_3"" style=""text-align:right"">
                       Trạng thái xử lý:                        
                    </div>
                    <div class=""grid_4"">
                        <b>"));
            global::System.Web.UI.WebControls.Literal @__ctrl5;
            @__ctrl5 = this.@__BuildControlliteralTrangThai();
            @__parser.AddParsedSubObject(@__ctrl5);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("</b>\r\n                    </div>\r\n                    <div class=\"clear\"></div>\r\n" +
                        "\t\t\t\t</div>\r\n            </div>\r\n\t\t</div>\r\n\t</div>\r\n\t<div class=\"clear\"></div>\r\n<" +
                        "/div>"));
        }
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private void InitializeControl() {
            this.@__BuildControlTree(this);
            this.Load += new global::System.EventHandler(this.Page_Load);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected virtual object Eval(string expression) {
            return global::System.Web.UI.DataBinder.Eval(this.Page.GetDataItem(), expression);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected virtual string Eval(string expression, string format) {
            return global::System.Web.UI.DataBinder.Eval(this.Page.GetDataItem(), expression, format);
        }
    }
}