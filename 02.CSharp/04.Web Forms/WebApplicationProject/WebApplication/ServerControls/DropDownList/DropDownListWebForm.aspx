<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DropDownListWebForm.aspx.cs" Inherits="WebApplication2.Controls.DropDownList.DropDownListWebForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:DropDownList ID="DropDownList1" runat="server">
            <%-- compile or design time items(choices) addition in dropdownlist %>
            <%--<asp:ListItem Value="1">Male</asp:ListItem>
            <asp:ListItem Value="2">Female</asp:ListItem>--%>
        </asp:DropDownList>
    </div>
    </form>
</body>
</html>
