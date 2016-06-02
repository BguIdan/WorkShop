﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainWindow.aspx.cs" Inherits="WebClient.MainWindow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Styles/bootstrap.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server" class="form-horizontal">
    <div>
    <p>
        <img alt="LOGO" src="http://www.tudiabetes.org/forum/uploads/default/original/3X/3/5/35d47232d1d9cb26dcd2a226952f98137a9080c8.jpg" style="height: 186px; width: 874px" />
    </p>

    </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
        <p>
            <asp:label AssociatedControlID="forum_dropList" runat="server" Cssclass="control-label">choose forum:</asp:label>
            <asp:DropDownList ID="forum_dropList" runat="server" OnDataBinding="refreshForumList">
            </asp:DropDownList>
        </p>
                </div>
                         <div class="col-sm-offset-2 col-sm-10">

        <p>
                        <asp:label AssociatedControlID="ID" runat="server" Cssclass="control-label">please log in:</asp:label>
            <asp:label AssociatedControlID="ID" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;As Guest:</asp:label>

            <asp:CheckBox ID="CheckBox_Guest" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" />
        </p>
                             </div>
        <p>
            <asp:label AssociatedControlID="ID" runat="server" Cssclass="col-sm-2 control-label">User Name</asp:label>
            <asp:TextBox ID="ID" type="email" runat="server" class="myText-control"></asp:TextBox>
        </p>
        <p>
            <asp:label AssociatedControlID="Password" runat="server" Cssclass="col-sm-2 control-label">Password</asp:label>
            <asp:TextBox ID="Password" type="email" runat="server" class="myText-control"></asp:TextBox>
        </p>
             <div class="col-sm-offset-2 col-sm-10">
        <p>
            <asp:Button ID="Btn_ImSuperUser" class="btn btn-default" runat="server" Text="Im A Super User" OnClick="Btn_ImSuperUser_Click" />
            <asp:Button ID="Btn_Login" class="btn btn-default" runat="server" Text="Login" OnClick="Btn_Login_Click" />
            <asp:Button ID="Btn_signUp" class="btn btn-default" runat="server" Text="Sign up" OnClick="Btn_signUp_Click" />
        </p>
            </div>
        </div>
    </form>
</body>
</html>
