<table width="100%" border="0" cellpadding="0" class="FBATable">
    <tr style="display:none">
    <td align="center" colspan="2">
        <asp:Label ID="UserNameTitle" runat="server" /></td>
    </tr>
    <tr>
    <td align="center" colspan="2">
        <asp:Label ID="UserNameInstruction" runat="server" /></td>
    </tr>
    <tr>
    <td align="left" valign="top" width="170px">
        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName"></asp:Label></td>
    <td>
        <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" CssClass="fba_error" ControlToValidate="UserName"  Display="Dynamic"></asp:RequiredFieldValidator>
    </td>
    </tr>
    <tr>
    <td align="center" valign="top" colspan="2" style="color: red">
        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
    </td>
    </tr>
    <tr>
        <td></td>
    <td align="left">
        <asp:Button ID="SubmitButton" Visible="false" runat="server" CommandName="Submit" />
        <asp:LinkButton ID="SubmitLinkButton" Visible="false" runat="server" CommandName="Submit" />
        <asp:ImageButton ID="SubmitImageButton" Visible="false" runat="server" CommandName="Submit" />
    </td>
    </tr>
</table>

<style>
    .fba_error {
        color:red;
    }

    .FBATable td {
        padding: 4px;
    }
</style>