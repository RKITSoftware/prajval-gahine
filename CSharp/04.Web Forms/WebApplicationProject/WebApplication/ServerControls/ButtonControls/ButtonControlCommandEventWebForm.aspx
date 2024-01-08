<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ButtonControlCommandEventWebForm.aspx.cs" Inherits="WebApplication2.Controls.ButtonControls.ButtonControlCommandEventWebForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <asp:Button ID="Button1" runat="server" Text="Create" CommandName="create" 
        oncommand="Button_Command"/>
    <asp:Button ID="Button2" runat="server" Text="Update" CommandName="update" oncommand="Button_Command"/>
    <asp:Button ID="Button3" runat="server" Text="Delete First" 
        CommandName="delete" CommandArgument="first" oncommand="Button_Command"/>
    <asp:Button ID="Button4" runat="server" Text="Delete Last" CommandName="delete" CommandArgument="last" oncommand="Button_Command"/>
    <p>
        <asp:Label ID="Label1" runat="server"></asp:Label>
    </p>
    </form>
</body>
</html>
