<%@ Control Language="C#" AutoEventWireup="true" Inherits="SAWWiki.TopTen" Codebehind="TopTen.ascx.cs" %>
<asp:Repeater ID="rptTopTen" runat="server">
    <ItemTemplate>
        <a href="<%# "default.aspx?page=" + HttpUtility.UrlEncode(Eval("PageName").ToString()) %>">
            <%# Eval("PageName") %></a>
        <br />
    </ItemTemplate>
</asp:Repeater>
