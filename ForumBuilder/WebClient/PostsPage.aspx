<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PostsPage.aspx.cs" Inherits="WebClient.PostsPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="bodyHolder" runat="server">

        <div>
        <p>
    <asp:Label ID="forumNameLabel" runat="server" CssClass="control-label"></asp:Label>
        </p>
    <asp:Label ID="subForumNameLabel" runat="server" CssClass="control-label"></asp:Label>
    </div>
    <asp:Table ID="PostsTable" runat="server" CssClass="table-hover" ></asp:Table>


</asp:Content>
