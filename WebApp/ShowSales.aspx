<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowSales.aspx.cs" Inherits="WebApp.ShowSales" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnTest1" runat="server" Text="Get All Sales" OnClick="btnTest1_Click" />    
        <asp:Label ID="lblResults" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
