<%@ Register TagPrefix="FBA" Namespace="LongAn.DVC.FormsBasedAuthentication.HIP" Assembly="LongAn.DVC.FormsBasedAuthentication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9dba9f460226d31d" %>

<table width="100%" border="0" cellpadding="0"  id="MembershipRequestTable" class="MembershipRequestTable" runat="server" >
	<tbody>
        <tr>
			<td align="left" colspan="2"><span class="field-required">(*)</span>: Trường bắt buộc nhập dữ liệu</td>
		</tr>
        <tr>
			<td align="center" colspan="2"><asp:Label ID="Header" runat="server" /></td>
		</tr>
        <tr>
			<td align="center" colspan="2"><asp:Label ID="Instruction" runat="server" /></td>
		</tr>
        <tr>
			<td align="right" valign="top" width="200px"><asp:Label ID="UserNameLabel" AssociatedControlID="UserName" CssClass="field" runat="server" /><span class="field-required">(*)</span></td>
            <td>
                <asp:TextBox ID="UserName" runat="server" CssClass="input_width"></asp:TextBox>
                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" Display="Dynamic" CssClass="fba_error"></asp:RequiredFieldValidator></td>
		</tr>
        <tr id="PasswordRow" runat="server" visible="false">
            <td align="right" valign="top">
                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" CssClass="field" /> <span class="field-required">(*)</span></td>
            <td>
                <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="input_width"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"  Display="Dynamic" CssClass="fba_error" />
            </td>
        </tr>
        <tr id="ConfirmPasswordRow" runat="server" visible="false">
            <td align="right" valign="top">
                <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword" CssClass="field"/> <span class="field-required">(*)</span></td>
            <td>
                <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" CssClass="input_width"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword" Display="Dynamic" CssClass="fba_error" />
            </td>
        </tr>
        <tr>
			<td align="right" valign="top"><asp:Label ID="FirstNameLabel" AssociatedControlID="FirstName" CssClass="field" runat="server" /><span class="field-required">(*)</span></td>
            <td>
                <asp:TextBox ID="FirstName" runat="server" CssClass="input_width"></asp:TextBox>
                <asp:RequiredFieldValidator ID="FirstNameRequired" runat="server" ControlToValidate="FirstName" Display="Dynamic" CssClass="fba_error"></asp:RequiredFieldValidator></td>
		</tr>
        <tr>
			<td align="right" valign="top"><asp:Label ID="LastNameLabel" AssociatedControlID="LastName" CssClass="field" runat="server" /><span class="field-required">(*)</span></td>
            <td>
                <asp:TextBox ID="LastName" runat="server" CssClass="input_width"></asp:TextBox>
                <asp:RequiredFieldValidator ID="LastNameRequired" runat="server" ControlToValidate="LastName" Display="Dynamic" CssClass="fba_error"></asp:RequiredFieldValidator></td>
		</tr>
        <tr>
			<td align="right" valign="top"><asp:Label ID="EmailLabel" AssociatedControlID="Email" CssClass="field" runat="server" /><span class="field-required">(*)</span></td>
            <td>
                <asp:TextBox ID="Email" runat="server" CssClass="FBAEmail input_width"></asp:TextBox>
                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email" Display="Dynamic" CssClass="fba_error"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="EmailRegexValidator" runat="server" ControlToValidate="Email" Display="Dynamic" ErrorMessage="Email không hợp lệ" ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" EnableClientScript="True" CssClass="fba_error"></asp:RegularExpressionValidator>
            </td>
		</tr>
        <tr id="QuestionRow" runat="server" visible="false">
			<td align="right" valign="top"><asp:Label ID="QuestionLabel" AssociatedControlID="Question" runat="server" /></td>
            <td>
                <asp:TextBox ID="Question" runat="server" CssClass="input_width"></asp:TextBox>
                <asp:RequiredFieldValidator ID="QuestionRequired" runat="server" ControlToValidate="Question" Display="Dynamic" CssClass="fba_error"></asp:RequiredFieldValidator></td>
		</tr>
        <tr id="AnswerRow" runat="server" visible="false">
			<td align="right" valign="top"><asp:Label ID="AnswerLabel" AssociatedControlID="Answer" runat="server" /></td>
            <td>
                <asp:TextBox ID="Answer" runat="server" EnableViewState="False" CssClass="input_width"></asp:TextBox>
                <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" ControlToValidate="Answer" Display="Dynamic" CssClass="fba_error"></asp:RequiredFieldValidator></td>
		</tr>
        <tr id="HipPictureRow" runat="server" visible="false">
			<td align="right" valign="top"><asp:Label ID="HipPictureLabel" AssociatedControlID="HipPicture" CssClass="field" runat="server" /></td>
            <td align="left">
                <asp:Label ID="HipInstructionsLabel" runat="server" /><br />
                <FBA:ImageHipChallenge ID="HipPicture" Width="210" Height="70" runat="server" /><br />
                <asp:Label ID="HipPictureDescriptionLabel" runat="server" />
            </td>
		</tr>
        <tr id="HipAnswerRow" runat="server" visible="false">
			<td align="right" valign="top"><asp:Label ID="HipAnswerLabel" AssociatedControlID="HipAnswer" CssClass="field" runat="server" /><span class="field-required">(*)</span></td>
            <td>
                <asp:TextBox ID="HipAnswer" runat="server" CssClass="input_width"></asp:TextBox>
                <FBA:HipValidator ID="HipAnswerValidator" runat="server" ControlToValidate="HipAnswer" HipChallenge="HipPicture"  Display="Dynamic" CssClass="fba_error" /><br />
                <asp:LinkButton ID="HipReset" runat="server" CommandName="HipReset" CausesValidation="false" />
            </td>
		</tr>
        <tr id="ConfirmPasswordCompareRow" runat="server" visible="false">
	        <td align="center" colspan="2">
                <asp:CompareValidator ID="ConfirmPasswordCompare" ControlToValidate="ConfirmPassword" ControlToCompare="Password"
                runat="server" Display="Dynamic" CssClass="fba_error"></asp:CompareValidator></td>
        </tr>
        <tr>
            <td align="center" colspan="2" style="color: red">
                <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                <asp:Literal ID="FBAErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td></td>
			<td align="left">
                <asp:Button ID="CreateUserButton" Visible="false" runat="server" CommandName="MoveNext" CssClass="CreateUserButton btn btn-default"/>
                <asp:LinkButton ID="CreateUserLinkButton" Visible="false" runat="server" CommandName="MoveNext" />
                <asp:ImageButton ID="CreateUserImageButton" Visible="false" runat="server" CommandName="MoveNext" />&nbsp;
                <asp:Button ID="CancelButton" Visible="false" runat="server" CommandName="Cancel" CausesValidation="false" />
                <asp:LinkButton ID="CancelLinkButton" Visible="false" runat="server" CommandName="Cancel"  CausesValidation="false" />
                <asp:ImageButton ID="CancelImageButton" Visible="false" runat="server" CommandName="Cancel"  CausesValidation="false" /></td>
		</tr>
	</tbody>
</table>

<style>
    .fba_error {
        color:red;
    }
    .field-required {
        color:red;
    }
    .field{
        font-weight:bold;
    }
    .MembershipRequestTable td {
        padding: 4px;
    }

    .input_width {
        width:200px;
    }
</style>

<script type="text/javascript">
    $(function () {
        $(".CreateUserButton").click(function () {
            if ($.trim($(".FBAEmail").val()) != '')
            {
                var regEmail = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
                
                if (regEmail.test($(".FBAEmail").val()) == false) {
                    $("span[id$=EmailRegexValidator]").show();
                    return false;
                }
                else {
                    $("span[id$=EmailRegexValidator]").hide();
                }
            }
        });
    });
</script>