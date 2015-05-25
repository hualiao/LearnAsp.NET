<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListDistinct.aspx.cs" Inherits="WebApp.ListDistinct1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:GridView runat="server" ID="grv_Products"></asp:GridView>
    <br />
    <asp:DropDownList runat="server" ID="drp_ProductNames"></asp:DropDownList>
    </div>
    </form>
</body>
</html>
