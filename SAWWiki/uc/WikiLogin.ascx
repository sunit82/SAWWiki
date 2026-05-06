<%@ Control Language="C#" AutoEventWireup="true" Inherits="SAWWiki.WikiLogin" Codebehind="WikiLogin.ascx.cs" %>

<asp:LoginView ID="LoginView1" runat="server" >
    <LoggedInTemplate>
        <table >
            <tr>
                <td >
                     <asp:Label ID="Label1" runat="server" Text="User:"></asp:Label>
                    <asp:LoginName ID="LoginName1" runat="server" />
                    <br /><br />     
                </td>
            </tr>
        </table>          
    </LoggedInTemplate>
    <AnonymousTemplate>
         <asp:Login ID="login1" runat="server" Title="Sign In" 
             VisibleWhenLoggedIn="True" TextLayout="TextOnTop" 
             PasswordRecoveryText="Forgot Password?" PasswordRecoveryUrl="~/User.aspx" 
             TitleText="">
            <LoginButtonStyle CssClass="loginbutton" />
             <LayoutTemplate>
                <asp:Panel ID="Panel1" runat="server" DefaultButton="LoginButton">
                 <table border="0" cellpadding="1" cellspacing="0"  
                     style="border-collapse:collapse;">
                     <tr>
                         <td>
                             <table border="0" cellpadding="0">
                                 <tr>
                                     <td>
                                         <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td>
                                         <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                             ControlToValidate="UserName" ErrorMessage="User Name is required." 
                                             ToolTip="User Name is required." ValidationGroup="ctl00$ctl00$login1">*</asp:RequiredFieldValidator>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td>
                                         <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td>
                                         <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                             ControlToValidate="Password" ErrorMessage="Password is required." 
                                             ToolTip="Password is required." ValidationGroup="ctl00$ctl00$login1">*</asp:RequiredFieldValidator>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td>
                                         <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time." />
                                     </td>
                                 </tr>
                                 <tr>
                                     <td align="center" style="color:Red;">
                                         <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td align="right">
                                         <asp:Button ID="LoginButton" runat="server" CommandName="Login" 
                                             CssClass="loginbutton" Text="Log In" ValidationGroup="ctl00$ctl00$login1" />
                                     </td>
                                 </tr>
                                 <tr>
                                     <td>
                                         <asp:HyperLink ID="PasswordRecoveryLink" runat="server" 
                                             NavigateUrl="~/User.aspx?mode=forgot">Forgot Password?</asp:HyperLink>
                                     </td>
                                 </tr>
                             </table>
                         </td>
                     </tr>
                 </table>
                 </asp:Panel>
             </LayoutTemplate>
        </asp:Login>
        <br />
        New User? <a href="User.aspx">Register here</a>.
    </AnonymousTemplate>
</asp:LoginView>


