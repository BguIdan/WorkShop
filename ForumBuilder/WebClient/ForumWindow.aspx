<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site1.Master" CodeBehind="ForumWindow.aspx.cs" Inherits="WebClient.ForumWindow" %>

<asp:Content ID="forumWindowContent" ContentPlaceHolderID="bodyHolder" runat="server">
    <h1>Sub-Forums</h1>
    <div>
        <asp:menu ID="menu" Orientation="Horizontal" runat="server" Enabled="true" onmenuitemclick="NavigationMenu_MenuItemClick">
            <Items>
                <asp:MenuItem Text="Create Sub-Forum" Value="AddSub" Enabled="true"/>
                <asp:MenuItem Text="View Private Messages" Value="privateMessages" Enabled="true"/>
                <asp:MenuItem Text="Set\Change Preferences" Value="Set" Enabled="true"/>
                <asp:MenuItem Text="Sign Up (Guest)" Value="SignUP" Enabled="true"/>
                <asp:MenuItem Text="Logout" Value="menuLogout" Enabled="true"/>
            </Items>
        </asp:menu>
        <p>
    </p>
        <p>
        <asp:Label ID="lbl_forumName" runat="server" Text="Label"></asp:Label>
    </p>
    <p>
        <asp:Table ID="tbl_subForumList" runat="server"></asp:Table>
    </p>
    </div>
    </asp:Content>