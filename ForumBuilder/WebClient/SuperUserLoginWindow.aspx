<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SuperUserLoginWindow.aspx.cs" Inherits="WebClient.SuperUserLoginWindow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h1>Super User</h1>
    <form id="form1" runat="server">
    <div>
        <p>

            Enter userName:
            <asp:TextBox ID="textBox_userName" runat="server"></asp:TextBox>

        </p>
        <p>

            Enter Password:
            <asp:TextBox ID="textBox_password" runat="server" TextMode="Password"></asp:TextBox>

        </p>
        <p>

            Enter Email:
            <asp:TextBox ID="textBox_email" runat="server"></asp:TextBox>

        </p>
        <p>

            <asp:Button ID="btn_back" runat="server" Text="Back" OnClick="btnClick_back"/>
            <asp:Button ID="btn_login" runat="server" Text="Log In" OnClick="btnClick_login"/>

        </p>
    
    </div>
    </form>
</body>
</html>
