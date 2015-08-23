<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InlineExpression.aspx.cs" Inherits="FrontEnd.InlineExpression" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div>
        <p>Equals: <%= this.TestValue %></p>
        <p>Pound: <%# this.TestValue %></p>
        <p>Equals label: <asp:Label runat="server" ID="_equals" Text="<%= this.TestValue %>" /></p>
        <p>Pound label: <asp:Label runat="server" ID="_pound" Text="<%# this.TestValue %>" /></p>
    </div>
    </div>
    </form>
</body>
</html>
