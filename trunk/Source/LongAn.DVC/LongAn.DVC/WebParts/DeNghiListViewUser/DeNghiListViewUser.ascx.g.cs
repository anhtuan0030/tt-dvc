﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LongAn.DVC.WebParts.DeNghiListViewUser {
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
    
    
    public partial class DeNghiListViewUser {
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Literal literalDeNghiTitle;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.LinkButton lbtAddNew;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Repeater repeaterLists;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.LinkButton lbtnFirst;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.LinkButton lbtnPrevious;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Repeater repeaterPage;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.LinkButton lbtnNext;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.LinkButton lbtnLast;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.HtmlControls.HtmlGenericControl divPagging;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebPartCodeGenerator", "12.0.0.0")]
        public static implicit operator global::System.Web.UI.TemplateControl(DeNghiListViewUser target) 
        {
            return target == null ? null : target.TemplateControl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Literal @__BuildControlliteralDeNghiTitle() {
            global::System.Web.UI.WebControls.Literal @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Literal();
            this.literalDeNghiTitle = @__ctrl;
            @__ctrl.ID = "literalDeNghiTitle";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.LinkButton @__BuildControllbtAddNew() {
            global::System.Web.UI.WebControls.LinkButton @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.LinkButton();
            this.lbtAddNew = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "lbtAddNew";
            @__ctrl.CssClass = "button add-new";
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("Thêm mới"));
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.DataBoundLiteralControl @__BuildControl__control3() {
            global::System.Web.UI.DataBoundLiteralControl @__ctrl;
            @__ctrl = new global::System.Web.UI.DataBoundLiteralControl(2, 1);
            @__ctrl.TemplateControl = this;
            @__ctrl.SetStaticString(0, "\r\n                        <tr class=\"");
            @__ctrl.SetStaticString(1, "\">\n                            <td>\r\n                                ");
            @__ctrl.DataBinding += new System.EventHandler(this.@__DataBind__control3);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        public void @__DataBind__control3(object sender, System.EventArgs e) {
            System.Web.UI.WebControls.RepeaterItem Container;
            System.Web.UI.DataBoundLiteralControl target;
            target = ((System.Web.UI.DataBoundLiteralControl)(sender));
            Container = ((System.Web.UI.WebControls.RepeaterItem)(target.BindingContainer));
            target.SetDataBoundString(0, global::System.Convert.ToString((((RepeaterItem)Container).ItemIndex+1) % 2 == 0 ? "odd" : "even", global::System.Globalization.CultureInfo.CurrentCulture));
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Literal @__BuildControl__control4() {
            global::System.Web.UI.WebControls.Literal @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Literal();
            @__ctrl.TemplateControl = this;
            @__ctrl.ID = "literalSTT";
            @__ctrl.DataBinding += new System.EventHandler(this.@__DataBinding__control4);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        public void @__DataBinding__control4(object sender, System.EventArgs e) {
            System.Web.UI.WebControls.Literal dataBindingExpressionBuilderTarget;
            System.Web.UI.WebControls.RepeaterItem Container;
            dataBindingExpressionBuilderTarget = ((System.Web.UI.WebControls.Literal)(sender));
            Container = ((System.Web.UI.WebControls.RepeaterItem)(dataBindingExpressionBuilderTarget.BindingContainer));
            dataBindingExpressionBuilderTarget.Text = global::System.Convert.ToString((((RepeaterItem)Container).ItemIndex+1) , global::System.Globalization.CultureInfo.CurrentCulture);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Literal @__BuildControl__control5() {
            global::System.Web.UI.WebControls.Literal @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Literal();
            @__ctrl.TemplateControl = this;
            @__ctrl.ID = "literalLoaiCapPhep";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Literal @__BuildControl__control6() {
            global::System.Web.UI.WebControls.Literal @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Literal();
            @__ctrl.TemplateControl = this;
            @__ctrl.ID = "literalNgayDeNghi";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Literal @__BuildControl__control7() {
            global::System.Web.UI.WebControls.Literal @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Literal();
            @__ctrl.TemplateControl = this;
            @__ctrl.ID = "literalTrangThai";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.LinkButton @__BuildControl__control8() {
            global::System.Web.UI.WebControls.LinkButton @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.LinkButton();
            @__ctrl.TemplateControl = this;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "lbtViewItem";
            @__ctrl.CssClass = "button view just-icon";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.LinkButton @__BuildControl__control9() {
            global::System.Web.UI.WebControls.LinkButton @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.LinkButton();
            @__ctrl.TemplateControl = this;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "lbtEditItem";
            @__ctrl.CssClass = "button edit just-icon";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.LinkButton @__BuildControl__control10() {
            global::System.Web.UI.WebControls.LinkButton @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.LinkButton();
            @__ctrl.TemplateControl = this;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "lbtDeleteItem";
            @__ctrl.CssClass = "button remove danger just-icon";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.LinkButton @__BuildControl__control11() {
            global::System.Web.UI.WebControls.LinkButton @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.LinkButton();
            @__ctrl.TemplateControl = this;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "lbtNopHoSo";
            @__ctrl.CssClass = "button up just-icon";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private void @__BuildControl__control2(System.Web.UI.Control @__ctrl) {
            global::System.Web.UI.DataBoundLiteralControl @__ctrl1;
            @__ctrl1 = this.@__BuildControl__control3();
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(@__ctrl1);
            global::System.Web.UI.WebControls.Literal @__ctrl2;
            @__ctrl2 = this.@__BuildControl__control4();
            @__parser.AddParsedSubObject(@__ctrl2);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                            </td>\n                            <td>\r\n           " +
                        "                     "));
            global::System.Web.UI.WebControls.Literal @__ctrl3;
            @__ctrl3 = this.@__BuildControl__control5();
            @__parser.AddParsedSubObject(@__ctrl3);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                            </td>\n                            <td>\r\n           " +
                        "                     "));
            global::System.Web.UI.WebControls.Literal @__ctrl4;
            @__ctrl4 = this.@__BuildControl__control6();
            @__parser.AddParsedSubObject(@__ctrl4);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                            </td>\n                            <td>\r\n           " +
                        "                     "));
            global::System.Web.UI.WebControls.Literal @__ctrl5;
            @__ctrl5 = this.@__BuildControl__control7();
            @__parser.AddParsedSubObject(@__ctrl5);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\n                            </td>\n                            <td>\r\n            " +
                        "                    "));
            global::System.Web.UI.WebControls.LinkButton @__ctrl6;
            @__ctrl6 = this.@__BuildControl__control8();
            @__parser.AddParsedSubObject(@__ctrl6);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                            </td>\n                            <td>\n            " +
                        "                    "));
            global::System.Web.UI.WebControls.LinkButton @__ctrl7;
            @__ctrl7 = this.@__BuildControl__control9();
            @__parser.AddParsedSubObject(@__ctrl7);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\n                            </td>\n                            <td>\n             " +
                        "                   "));
            global::System.Web.UI.WebControls.LinkButton @__ctrl8;
            @__ctrl8 = this.@__BuildControl__control10();
            @__parser.AddParsedSubObject(@__ctrl8);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\n                            </td>\n                            <td>\n             " +
                        "                   "));
            global::System.Web.UI.WebControls.LinkButton @__ctrl9;
            @__ctrl9 = this.@__BuildControl__control11();
            @__parser.AddParsedSubObject(@__ctrl9);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\n                            </td>\n                        </tr>\r\n               " +
                        "     "));
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Repeater @__BuildControlrepeaterLists() {
            global::System.Web.UI.WebControls.Repeater @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Repeater();
            this.repeaterLists = @__ctrl;
            @__ctrl.ItemTemplate = new System.Web.UI.CompiledTemplateBuilder(new System.Web.UI.BuildTemplateMethod(this.@__BuildControl__control2));
            @__ctrl.ID = "repeaterLists";
            @__ctrl.ItemCommand -= new System.Web.UI.WebControls.RepeaterCommandEventHandler(this.repeaterLists_ItemCommand);
            @__ctrl.ItemCommand += new System.Web.UI.WebControls.RepeaterCommandEventHandler(this.repeaterLists_ItemCommand);
            @__ctrl.ItemDataBound -= new System.Web.UI.WebControls.RepeaterItemEventHandler(this.repeaterLists_ItemDataBound);
            @__ctrl.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.repeaterLists_ItemDataBound);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.LinkButton @__BuildControllbtnFirst() {
            global::System.Web.UI.WebControls.LinkButton @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.LinkButton();
            this.lbtnFirst = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "lbtnFirst";
            @__ctrl.CssClass = "button begin small";
            @__ctrl.CausesValidation = false;
            @__ctrl.Click -= new System.EventHandler(this.lbtnFirst_Click);
            @__ctrl.Click += new System.EventHandler(this.lbtnFirst_Click);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.LinkButton @__BuildControllbtnPrevious() {
            global::System.Web.UI.WebControls.LinkButton @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.LinkButton();
            this.lbtnPrevious = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "lbtnPrevious";
            @__ctrl.CssClass = "button prev small";
            @__ctrl.CausesValidation = false;
            @__ctrl.Click -= new System.EventHandler(this.lbtnPrevious_Click);
            @__ctrl.Click += new System.EventHandler(this.lbtnPrevious_Click);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.LinkButton @__BuildControl__control13() {
            global::System.Web.UI.WebControls.LinkButton @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.LinkButton();
            @__ctrl.TemplateControl = this;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "lnkbtnPaging";
            @__ctrl.CommandName = "Paging";
            @__ctrl.DataBinding += new System.EventHandler(this.@__DataBinding__control13);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        public void @__DataBinding__control13(object sender, System.EventArgs e) {
            System.Web.UI.WebControls.LinkButton dataBindingExpressionBuilderTarget;
            System.Web.UI.WebControls.RepeaterItem Container;
            dataBindingExpressionBuilderTarget = ((System.Web.UI.WebControls.LinkButton)(sender));
            Container = ((System.Web.UI.WebControls.RepeaterItem)(dataBindingExpressionBuilderTarget.BindingContainer));
            dataBindingExpressionBuilderTarget.CommandArgument = global::System.Convert.ToString( Eval("PageIndex") , global::System.Globalization.CultureInfo.CurrentCulture);
            dataBindingExpressionBuilderTarget.Text = global::System.Convert.ToString( Eval("PageText") , global::System.Globalization.CultureInfo.CurrentCulture);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private void @__BuildControl__control12(System.Web.UI.Control @__ctrl) {
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                        "));
            global::System.Web.UI.WebControls.LinkButton @__ctrl1;
            @__ctrl1 = this.@__BuildControl__control13();
            @__parser.AddParsedSubObject(@__ctrl1);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n                    "));
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Repeater @__BuildControlrepeaterPage() {
            global::System.Web.UI.WebControls.Repeater @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Repeater();
            this.repeaterPage = @__ctrl;
            @__ctrl.ItemTemplate = new System.Web.UI.CompiledTemplateBuilder(new System.Web.UI.BuildTemplateMethod(this.@__BuildControl__control12));
            @__ctrl.ID = "repeaterPage";
            @__ctrl.ItemCommand -= new System.Web.UI.WebControls.RepeaterCommandEventHandler(this.repeaterPage_ItemCommand);
            @__ctrl.ItemCommand += new System.Web.UI.WebControls.RepeaterCommandEventHandler(this.repeaterPage_ItemCommand);
            @__ctrl.ItemDataBound -= new System.Web.UI.WebControls.RepeaterItemEventHandler(this.repeaterPage_ItemDataBound);
            @__ctrl.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.repeaterPage_ItemDataBound);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.LinkButton @__BuildControllbtnNext() {
            global::System.Web.UI.WebControls.LinkButton @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.LinkButton();
            this.lbtnNext = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "lbtnNext";
            @__ctrl.CssClass = "button next small";
            @__ctrl.CausesValidation = false;
            @__ctrl.Click -= new System.EventHandler(this.lbtnNext_Click);
            @__ctrl.Click += new System.EventHandler(this.lbtnNext_Click);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.LinkButton @__BuildControllbtnLast() {
            global::System.Web.UI.WebControls.LinkButton @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.LinkButton();
            this.lbtnLast = @__ctrl;
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            @__ctrl.ID = "lbtnLast";
            @__ctrl.CssClass = "button end small";
            @__ctrl.CausesValidation = false;
            @__ctrl.Click -= new System.EventHandler(this.lbtnLast_Click);
            @__ctrl.Click += new System.EventHandler(this.lbtnLast_Click);
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.HtmlControls.HtmlGenericControl @__BuildControldivPagging() {
            global::System.Web.UI.HtmlControls.HtmlGenericControl @__ctrl;
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlGenericControl("div");
            this.divPagging = @__ctrl;
            @__ctrl.ID = "divPagging";
            @__ctrl.Visible = false;
            ((System.Web.UI.IAttributeAccessor)(@__ctrl)).SetAttribute("class", "pag pull-right");
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\n                \n                "));
            global::System.Web.UI.WebControls.LinkButton @__ctrl1;
            @__ctrl1 = this.@__BuildControllbtnFirst();
            @__parser.AddParsedSubObject(@__ctrl1);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\n                \n                "));
            global::System.Web.UI.WebControls.LinkButton @__ctrl2;
            @__ctrl2 = this.@__BuildControllbtnPrevious();
            @__parser.AddParsedSubObject(@__ctrl2);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\n                "));
            global::System.Web.UI.WebControls.Repeater @__ctrl3;
            @__ctrl3 = this.@__BuildControlrepeaterPage();
            @__parser.AddParsedSubObject(@__ctrl3);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\n                "));
            global::System.Web.UI.WebControls.LinkButton @__ctrl4;
            @__ctrl4 = this.@__BuildControllbtnNext();
            @__parser.AddParsedSubObject(@__ctrl4);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\n                \n                "));
            global::System.Web.UI.WebControls.LinkButton @__ctrl5;
            @__ctrl5 = this.@__BuildControllbtnLast();
            @__parser.AddParsedSubObject(@__ctrl5);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\n                \n                <div class=\"clearfix\"></div>\n            "));
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private void @__BuildControlTree(global::LongAn.DVC.WebParts.DeNghiListViewUser.DeNghiListViewUser @__ctrl) {
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@"
<link rel=""stylesheet"" href=""/_layouts/15/LongAn.DVC/css/bootstrap.min.css""/>
<!--[if lt IE9]>
<link rel=""stylesheet"" href=""/_layouts/15/LongAn.DVC/css/bootstrap-non-responsive.min.css""/>
<![endif]-->
<link rel=""stylesheet"" href=""/_layouts/15/LongAn.DVC/css/jquery-ui.structure.min.css""/>
<link rel=""stylesheet"" href=""/_layouts/15/LongAn.DVC/css/jquery-ui.min.css""/>
<link rel=""stylesheet"" href=""/_layouts/15/LongAn.DVC/css/bootstrap-theme.min.css""/>
<link rel=""stylesheet"" href=""/_layouts/15/LongAn.DVC/css/form.main.css""/>
<script src=""/_layouts/15/LongAn.DVC/js/jquery.js""></script>
<script src=""/_layouts/15/LongAn.DVC/js/jquery-ui.min.js""></script>
<script src=""/_layouts/15/LongAn.DVC/js/modernizr.js""></script>
<script src=""/_layouts/15/LongAn.DVC/js/detectizr.min.js""></script>
<script src=""/_layouts/15/LongAn.DVC/js/scripts.js""></script>
<div class=""container1"">
    <div class=""row"">
        <div class=""col-lg-9 col-md-9 col-sm-9 col-xs-9"">
            <h2 class=""page-header"">
                "));
            global::System.Web.UI.WebControls.Literal @__ctrl1;
            @__ctrl1 = this.@__BuildControlliteralDeNghiTitle();
            @__parser.AddParsedSubObject(@__ctrl1);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\n            </h2>\n            \n            <div class=\"pull-right\">\r\n           " +
                        "     "));
            global::System.Web.UI.WebControls.LinkButton @__ctrl2;
            @__ctrl2 = this.@__BuildControllbtAddNew();
            @__parser.AddParsedSubObject(@__ctrl2);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@"
            </div>
            <div class=""clearfix""></div>

            <table class=""the-table"">
                <tr>
                    <th>STT</th>
                    <th>Loại đề nghị</th>
                    <th>Ngày đề nghị</th>
                    <th>Tình trạng</th>
                    <th>Xem</th>
                    <th>Sửa</th>
                    <th>Xóa</th>
                    <th>Nộp</th>
                </tr>
                "));
            global::System.Web.UI.WebControls.Repeater @__ctrl3;
            @__ctrl3 = this.@__BuildControlrepeaterLists();
            @__parser.AddParsedSubObject(@__ctrl3);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\n            </table>\n            \n            "));
            global::System.Web.UI.HtmlControls.HtmlGenericControl @__ctrl4;
            @__ctrl4 = this.@__BuildControldivPagging();
            @__parser.AddParsedSubObject(@__ctrl4);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\n            \n            <div class=\"clearfix\"></div>\n        </div>\n    </div>\n" +
                        "</div>"));
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
