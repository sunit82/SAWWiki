<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="SAWWiki.UserMembership" CodeBehind="UserMembership.aspx.cs" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <asp:GridView ID="grdUsers" runat="server" DataKeyNames="UserName" AllowPaging="True"
        AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
        GridLines="Vertical" OnPageIndexChanging="grdUsers_PageIndexChanging" OnSelectedIndexChanged="grdUsers_SelectedIndexChanged"
        BorderColor="Silver" Width="100%">
        <Columns>
            <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:CheckBoxField ItemStyle-HorizontalAlign="Center" DataField="IsApproved" HeaderText="Active"
                SortExpression="IsApproved" />
            <asp:BoundField ItemStyle-Width="80px" DataField="CreationDate" HeaderText="Create Date" SortExpression="CreationDate" />
            <asp:BoundField ItemStyle-Width="80px" DataField="LastLoginDate" HeaderText="Last Login Date" SortExpression="LastLoginDate" />            
            <asp:ButtonField Text="Edit" ItemStyle-HorizontalAlign="Center" CommandName="select"
                HeaderText="Edit" />
        </Columns>
        <HeaderStyle BackColor="Silver" HorizontalAlign="Center" />
    </asp:GridView>
    <asp:Panel ID="pnlUserDetails" runat="server">
        <table width="100%">
            <tr>
                <td valign="top" style="width:70%;">
                    <asp:DetailsView DefaultMode="Edit" ID="dtvUsers" runat="server" AutoGenerateRows="False"
                        CellPadding="4" GridLines="None" OnItemDeleting="dtvUsers_ItemDeleting" OnItemUpdating="dtvUsers_ItemUpdating"
                        HeaderText="Edit User Details" OnItemCommand="dtvUsers_ItemCommand" 
                        OnModeChanging="dtvUsers_ModeChanging" HeaderStyle-BackColor="Silver" 
                        BorderStyle="None" Width="100%">
                        <Fields>
                            <asp:BoundField DataField="UserName" HeaderText="UserName:" SortExpression="UserName"
                                ReadOnly="True" />
                            <asp:CheckBoxField DataField="IsApproved" HeaderText="Active:" SortExpression="IsApproved" />
                            <asp:BoundField DataField="Email" HeaderText="Email:" SortExpression="Email"  ItemStyle-Width="250px" ControlStyle-Width="250px" />
                            <asp:TemplateField HeaderText="Comments:">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtUserComments" runat="server" Width="250px" Rows="5" TextMode="MultiLine"
                                        Text='<%# Bind("Comment") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:ButtonField ButtonType="Button"  ControlStyle-Width="80px" CommandName="update" Text="Update" />
                            <asp:ButtonField ButtonType="Button" ControlStyle-Width="80px" CommandName="delete" Text="Delete" />
                            <asp:ButtonField ButtonType="button" ControlStyle-Width="80px" CommandName="cancel" Text="Cancel" />
                        </Fields>
                        <HeaderStyle BackColor="Silver" Font-Bold="True" />
                    </asp:DetailsView>
                </td>
                <td valign="top" >
                    <asp:DataList ID="dlstUserRoles" runat="server" CellPadding="0"
                        ForeColor="#333333" Width="100%" >
                        <HeaderTemplate>
                            <td colspan="1" style="background-color:Silver; font-weight:bold;">
                                Edit User Roles
                            </td>
                        </HeaderTemplate>
                       
                        <ItemTemplate>
                            <td style="padding:2px 0px 2px 0px;">
                                <asp:CheckBox ID="chkRole" runat="server" Checked='<%# (Eval("IsInRole").ToString() == "true") ? true: false  %>'
                                Text='<%# Eval("RoleName") %>'  />
                            </td>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
        </table>
    </asp:Panel>
    
</asp:Content>
