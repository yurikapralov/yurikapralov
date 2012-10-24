<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderPrint.aspx.cs" Inherits="Admin_OrderPrint" %>

<%@ Register src="../Controls/Order.ascx" tagname="Order" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Печать заказа</title>
    <link href="bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css" />
</head>
<body onload="javascript:window.print();">
    <form id="form1" runat="server">
    <div>
    
        <uc1:Order ID="OrderCtrl" runat="server" ViewOrderStatus="true" />
    
    </div>
    </form>
</body>
</html>
