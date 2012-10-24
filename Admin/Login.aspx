<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Admin_Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Авторизация пользователя</title>
    <link href="../bootstrap.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        /* Override some defaults */
        html, body
        {
            background-color: #eee;
        }
        body
        {
            padding-top: 100px;
        }
        .container
        {
            width: 300px;
        }
        
        /* The white background content wrapper */
        .container > .content
        {
            background-color: #fff;
            padding: 20px;
            margin: 0 -20px;
            -webkit-border-radius: 10px 10px 10px 10px;
            -moz-border-radius: 10px 10px 10px 10px;
            border-radius: 10px 10px 10px 10px;
            -webkit-box-shadow: 0 1px 2px rgba(0,0,0,.15);
            -moz-box-shadow: 0 1px 2px rgba(0,0,0,.15);
            box-shadow: 0 1px 2px rgba(0,0,0,.15);
        }
        
        .login-form
        {
            margin-left: 65px;
        }
        
        legend
        {
            margin-right: -50px;
            font-weight: bold;
            color: #404040;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <div class="content">
            <div class="row">
                <div class="login-form">
                <h2>Вход на сайт</h2>
                    <asp:Login ID="ctrLogin" FailureText="Неправильное имя или пароль" runat="server"
                        Width="100%">
                        <LayoutTemplate>
                            <fieldset>
                                <div class="clearfix">
                                    <asp:TextBox ID="UserName" runat="server" CssClass="textform"
                                        ClientIDMode="Static" placeholder="Логин" title="Логин"/>
                                </div>
                                <div class="clearfix">
                                    <asp:TextBox ID="Password" runat="server" CssClass="textform"
                                        TextMode="Password" ClientIDMode="Static" placeholder="Пароль" title="Пароль"/>
                                </div>
                               <asp:Button ID="btnLogin" runat="server" Text="Вход" CssClass="btn btn-primary"  CommandName="Login" />
                            </fieldset>
                        </LayoutTemplate>
                    </asp:Login>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
