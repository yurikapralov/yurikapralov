﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login_old.aspx.cs" Inherits="Admin_Login_old" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Авторизация пользователя</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style=" padding-top:300px; padding-left:400px;">
        <asp:Login ID="Login1" runat="server" BackColor="#F7F6F3" BorderColor="#E6E2D8" 
            BorderPadding="4" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" 
            Font-Size="Medium" ForeColor="#333333" 
            FailureText="Непраильные логин/пароль" LoginButtonText="Войти" 
            PasswordLabelText="Пароль:" PasswordRequiredErrorMessage="Введите пароль" 
            RememberMeText="Запомнить меня" UserNameLabelText="Логин:" 
            UserNameRequiredErrorMessage="Введите логин">
            <TextBoxStyle Font-Size="0.8em" />
            <LoginButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" 
                BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" />
            <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
            <TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.9em" 
                ForeColor="White" />
        </asp:Login>  
    </div>
    </form>
</body>
</html>
