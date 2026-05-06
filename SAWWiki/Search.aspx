<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="SAWWiki.Search" CodeBehind="Search.aspx.cs" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel runat="server" DefaultButton="btnSearch">
        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>&nbsp;
        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
    </asp:Panel>
    <br />
    <br />
    <asp:Label ID="lblNoResults" runat="server" Visible="false"></asp:Label>
    <asp:GridView ID="grdSearch" EmptyDataText="<h2>Sorry, no results were found</h2>"
        runat="server" AllowPaging="True" DataKeyNames="PageName" OnSelectedIndexChanged="grdSearch_SelectedIndexChanged"
        OnPageIndexChanging="grdSearch_PageIndexChanging" AutoGenerateColumns="False"
        Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None">
        <RowStyle BackColor="#E3EAEB" />
        <Columns>
            <asp:ButtonField ButtonType="link" CommandName="Select" DataTextField="PageName"
                HeaderText="Page" ItemStyle-Width="30%">
                <ItemStyle Width="30%"></ItemStyle>
            </asp:ButtonField>
            <asp:BoundField DataField="PageText" HeaderText="Text" />
        </Columns>
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#2C7A8E" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#7C6F57" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
</asp:Content>
