<%@ Control Language="C#" AutoEventWireup="true" Inherits="SAWWiki.GoToPage" Codebehind="GoToPage.ascx.cs" %>
<asp:Panel runat="server" ID="pnlGoTo" DefaultButton="btnGoTo" >
    <table width="23%">
        <tr>
            <td align="center">
                Enter a Page Name:
                <br />
                <asp:TextBox ID="txtGoTo" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Button ID="btnGoTo" runat="server" Text="Go" OnClick="btnGoTo_Click" />
            </td>
        </tr>
    </table>
</asp:Panel>
