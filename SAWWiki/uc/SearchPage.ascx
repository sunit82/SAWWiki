<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchPage.ascx.cs" 
Inherits="SAWWiki.SearchPage" %>

<asp:Panel runat="server" ID="pnlSearch" DefaultButton="btnSearch" >
    <table >
        <tr>
            <td align="center">                 
                <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
            </td>        
            <td>
                <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" OnClick="btnSearch_Click" />
            </td>
        </tr>         
    </table>
</asp:Panel>
