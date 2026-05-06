<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="SAWWiki.ContactUs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server" >
            <br /><br />
            <table width="100%">
                <tr>
                    <td align="left" width="20%" colspan="2">
                        Your opinion is valuable to us. So feel free to send any ideas, suggestions, 
                        issues that you think will help improve your SAWWiki experience.<br /><br />
                    </td>
               </tr>
                <tr>
                    <td align="right" width="20%">Name:</td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" Width="300px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtName" Display="Dynamic" ErrorMessage="*" 
                            ValidationGroup="btnSend"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="Label1" runat="server" Text="Email:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" Width="300px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="*" 
                            ValidationGroup="btnSend"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right">Comments:</td>
                    <td valign="top">
                        <table>
                            <tr>
                                <td>
                                   <asp:TextBox ID="txtComment" runat="server" Height="200px" TextMode="MultiLine" 
                                     Width="300px"></asp:TextBox>
                                </td>
                                <td valign="bottom">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ControlToValidate="txtComment" Display="Dynamic" ErrorMessage="*" 
                            ValidationGroup="btnSend"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right">&nbsp;</td>
                    <td>
                        <asp:Button ID="btnSend" runat="server" Text="Send" onclick="btnSend_Click" 
                            ValidationGroup="btnSend"  />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View2" runat="server">
            Thanx for your valuable feedback.<br />
            <br />
            We'll look into your feedback and get back to you as soon 
            as possible. <br /><br />
            Regards,<br />
            Administrator<br />
            www.sawinfotech.com
        </asp:View>
    </asp:MultiView>
</asp:Content>
