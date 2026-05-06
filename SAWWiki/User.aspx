<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" 
CodeBehind="User.aspx.cs" Inherits="SAWWiki.User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
        <tr>
            <td valign="top" align="center">
                <br /><br />
                <asp:Panel runat="server" ID="pnlChangePassword" DefaultButton="ChangePassword1$ChangePasswordContainerID$ChangePasswordPushButton">
                    <asp:ChangePassword ID="ChangePassword1" runat="server" Visible="False" ContinueDestinationPageUrl="~/Default.aspx"
                        Font-Names="Verdana" CancelDestinationPageUrl="~/Default.aspx">
                        <ChangePasswordTemplate>
                            <table border="0" cellpadding="1" cellspacing="0" 
                                style="border-collapse:collapse;">
                                <tr>
                                    <td>
                                        <table border="0" cellpadding="0">
                                            <tr>
                                                <td align="center" colspan="2">
                                                    Change Your Password</td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="CurrentPasswordLabel" runat="server" 
                                                        AssociatedControlID="CurrentPassword">Password:</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="CurrentPassword" runat="server" TextMode="Password"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" 
                                                        ControlToValidate="CurrentPassword" ErrorMessage="Password is required." 
                                                        ToolTip="Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="NewPasswordLabel" runat="server" 
                                                        AssociatedControlID="NewPassword">New Password:</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="NewPassword" runat="server" TextMode="Password"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" 
                                                        ControlToValidate="NewPassword" ErrorMessage="New Password is required." 
                                                        ToolTip="New Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="ConfirmNewPasswordLabel" runat="server" 
                                                        AssociatedControlID="ConfirmNewPassword">Confirm New Password:</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="ConfirmNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" 
                                                        ControlToValidate="ConfirmNewPassword" 
                                                        ErrorMessage="Confirm New Password is required." 
                                                        ToolTip="Confirm New Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2">
                                                    <asp:CompareValidator ID="NewPasswordCompare" runat="server" 
                                                        ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" 
                                                        Display="Dynamic" 
                                                        ErrorMessage="The Confirm New Password must match the New Password entry." 
                                                        ValidationGroup="ChangePassword1"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2" style="color:Red;">
                                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Button ID="ChangePasswordPushButton" runat="server" 
                                                        CommandName="ChangePassword" Text="Change Password" 
                                                        ValidationGroup="ChangePassword1" />
                                                </td>
                                                <td align="left">
                                                    <asp:Button ID="CancelPushButton" runat="server" CausesValidation="False" 
                                                        CommandName="Cancel" Text="Cancel" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ChangePasswordTemplate>
                    </asp:ChangePassword>
                </asp:Panel>
                 
                <asp:Panel ID="pnlCreateUser" runat="server" DefaultButton="CreateUserWizard1$__CustomNav0$StepNextButtonButton">
                    <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" OnCreatedUser="CreateUserWizard1_CreatedUser"
                        ContinueDestinationPageUrl="~/Default.aspx">
                        <WizardSteps>
                            <asp:CreateUserWizardStep  ID="CreateUserWizardStep1" runat="server">
                                <ContentTemplate>
                                    <table border="0">
                                        <tr>
                                            <td align="center" colspan="2">
                                                <b>Sign Up for Your New Account</b></td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                                    ControlToValidate="UserName" ErrorMessage="User Name is required." 
                                                    ToolTip="User Name is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                                    ControlToValidate="Password" ErrorMessage="Password is required." 
                                                    ToolTip="Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="ConfirmPasswordLabel" runat="server" 
                                                    AssociatedControlID="ConfirmPassword">Confirm Password:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" 
                                                    ControlToValidate="ConfirmPassword" 
                                                    ErrorMessage="Confirm Password is required." 
                                                    ToolTip="Confirm Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Email" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" 
                                                    ControlToValidate="Email" ErrorMessage="E-mail is required." 
                                                    ToolTip="E-mail is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="QuestionLabel" runat="server" AssociatedControlID="Question">Security Question:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Question" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="QuestionRequired" runat="server" 
                                                    ControlToValidate="Question" ErrorMessage="Security question is required." 
                                                    ToolTip="Security question is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer">Security Answer:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Answer" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" 
                                                    ControlToValidate="Answer" ErrorMessage="Security answer is required." 
                                                    ToolTip="Security answer is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:CompareValidator ID="PasswordCompare" runat="server" 
                                                    ControlToCompare="Password" ControlToValidate="ConfirmPassword" 
                                                    Display="Dynamic" 
                                                    ErrorMessage="The Password and Confirmation Password must match." 
                                                    ValidationGroup="CreateUserWizard1"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2" style="color:Red;">
                                                <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:CreateUserWizardStep>
                            <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
                            </asp:CompleteWizardStep>
                        </WizardSteps>
                    </asp:CreateUserWizard>
                </asp:Panel>
            
                <asp:Panel ID="pnlRecovery" runat="server" DefaultButton="PasswordRecovery1$UserNameContainerID$SubmitButton">
                    <asp:PasswordRecovery ID="PasswordRecovery1" runat="server">
                    </asp:PasswordRecovery>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>

