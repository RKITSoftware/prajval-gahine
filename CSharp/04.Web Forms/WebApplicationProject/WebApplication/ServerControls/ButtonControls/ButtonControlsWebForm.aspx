<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ButtonControlsWebForm.aspx.cs" Inherits="WebApplication2.Controls.ButtonControls.ButtonControlsWebForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Submit" />
    <p>
        <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">Submit</asp:LinkButton>
    </p>
    <asp:ImageButton ID="ImageButton1" runat="server" Height="34px" 
        ImageUrl="~/Images/button.png" onclick="ImageButton1_Click" 
        Width="110px" />
    </form>
</body>
</html>
