<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainWindow.aspx.cs" Inherits="WebClient.MainWindow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <p>
        <img alt="LOGO" src="http://www.tudiabetes.org/forum/uploads/default/original/3X/3/5/35d47232d1d9cb26dcd2a226952f98137a9080c8.jpg" style="height: 186px; width: 874px" />
    </p>

    </div>
        <p>
            choose forum:
            <asp:DropDownList ID="forum_dropList" runat="server" OnDataBinding="refreshForumList">
            </asp:DropDownList>
        </p>
        <p>
            please login:&nbsp;&nbsp;&nbsp; as guest
            <asp:CheckBox ID="CheckBox_Guest" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" />
        </p>
        <p>
            enter user name:
            <asp:TextBox ID="ID" runat="server"></asp:TextBox>
        </p>
        <p>
            entrer password:
            <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="Btn_ImSuperUser" runat="server" Text="Im A Super User" OnClick="Btn_ImSuperUser_Click" />
            <asp:Button ID="Btn_Login" runat="server" Text="Login" OnClick="Btn_Login_Click" />
            <asp:Button ID="Btn_signUp" runat="server" Text="Sign up" OnClick="Btn_signUp_Click" />
        </p>
    </form>
</body>
</html>
