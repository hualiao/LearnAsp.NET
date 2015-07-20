<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImageGridDemo.aspx.cs" Inherits="WebApp.ImageGridDemo" %>
<%@ Register TagPrefix="cc" TagName="ImageControl" Src="~/Controls/ImageGrid.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%--<cc:ImageControl runat="server" ID="MyImageGrid"  />--%>
        <cc:ImageControl runat="server" ID="ImageControl1" Title="C# Wallpapers" AdminMode="true" />
    </div>
    </form>
</body>
</html>
