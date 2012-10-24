<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testroles.aspx.cs" Inherits="testroles" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1><font color="Red">Страничка для прямого добавления ролей на сайт не выкладывать</font></h1>
    Все роли:
        <asp:ListBox ID="lstRoles" runat="server"></asp:ListBox><br />
    Все пользователи:
        <asp:ListBox ID="lstUsers" runat="server"></asp:ListBox><br />
    Добавить роль:
        <asp:TextBox ID="txtRolesAdd" runat="server"></asp:TextBox>
        <asp:Button ID="btnRolesAdd" runat="server" Text="Добавить" 
            onclick="btnRolesAdd_Click" /><br />
    Добавить администратора:
        <asp:TextBox ID="txtAdminAdd" runat="server"></asp:TextBox>
        <asp:Button ID="btnAdminAdd" runat="server" Text="Добавить" 
            onclick="btnAdminAdd_Click" /><br />
        <asp:Button ID="btnDeleteTestUser" runat="server" Text="Delete test user" 
            onclick="btnDeleteTestUser_Click" />
    </div>
    </form>
</body>
</html>
