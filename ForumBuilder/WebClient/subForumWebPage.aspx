﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="subForumWebPage.aspx.cs" Inherits="WebClient.subForumWebPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="bodyHolder" runat="server">
    <div>
        <p>
    <asp:Label ID="forumNameLabel" runat="server" CssClass="control-label"></asp:Label>
        </p>
    <asp:Label ID="subForumNameLabel" runat="server" CssClass="control-label"></asp:Label>
    </div>
    <asp:Table ID="ThreadTable" runat="server" ></asp:Table>
</asp:Content>
