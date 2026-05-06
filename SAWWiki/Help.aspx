<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Help.aspx.cs" Inherits="SAWWiki.Help" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br /><br />
    In order to create and format your wiki pages your need to create your page using wiki syntax.
    Like any wiki site, SAWWiki has its wiki syntax as explained below.<br />
    You can also find a "Quick Reference" guide in the "Edit Page" of wiki pages.<br />
    <ul>
        <li><b>Wiki Link</b></li><br />
        To create a new page, first a Wiki Link has to be added on parent page.<br />
        Wiki Link is a single word camel-cased between square braces.<br />
        e.g. <b>[NewTopicTitle]</b>
        <li><b>Named Link</b></li><br />
        Named Link is a hyperlink to redirect to any page.<br />
        Named Link has a 'Link Text' followed by pipe sysmble(|) followed by 'Link URL', all 3 between curly braces.<br />
        e.g. <b>{SAWiNFOTECH|http://www.sawinfotech.com}</b>
        <li><b>Bold</b></li><br />
        To format a part of text with Bold font, encapsulate that text between double asterisks.<br />
        e.g.<b>**Some Text**</b>
        <li><b>Italic</b></li><br />
        To format a part of text with Italic font, encapsulate that text between double quotes.<br />
        e.g.<b>"Some Text"</b>
        <li><b>Underline</b></li><br />
        To underline a part of text, encapsulate that text between double underscores.<br />
        e.g.<b>__Some Text__</b>
        <li><b>Center</b></li><br />
        To center a part of text, encapsulate that text between equal-to symbol.<br />
        e.g.<b>=Some Text=</b>
        <li><b>Headings</b></li>
        To format a text to be shown as heading as compared to H1, H2, H3 tags of html, prefix text by
        exclamation mark 'n' times where n = 1 for H1 and so on.<br />
        e.g. <b>!Some Heading</b> for Heading 1(H1)<br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;      <b>!!Some Heading</b> for Heading 2(H2)
        <li><b>Line</b></li><br />
        To draw a horizontal line use four hyphens.<br />
        e.g. <b>----</b>
        <li><b>Bulleted List</b></li><br />
        To get a bulleted list put a 'plus' symbol followed by text.<br />
        e.g. <b>+ Bulleted text</b>
        <li><b>Numbered List</b></li><br />
        To get a numbered list put a 'hash' symbol followed by text.<br />
        e.g. <b># Numbered text</b>
        
    </ul>
    
</asp:Content>
