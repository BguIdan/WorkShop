<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForumWindow.aspx.cs" Inherits="WebClient.ForumWindow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h1>Sub-Forums</h1>
    <form id="form1" runat="server">
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
    </form>
</body>
</html>
