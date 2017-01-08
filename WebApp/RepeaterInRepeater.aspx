<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RepeaterInRepeater.aspx.cs" Inherits="WebApp.RepeaterInRepeater" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Repeater ID="ParentRepeater" runat="server" OnItemDataBound="ItemBound">
            <ItemTemplate>
                <asp:Label ID="lblParentId" runat="server" Text='<%#Eval("ParentID").ToString() %>'>'></asp:Label>
                <br />
                <!-- Repeated data -->
                <asp:Repeater ID="ChildRepeater" runat="server">
                    <ItemTemplate>
                        <!-- Nested repeated data -->
                        Name:<%#Eval("Name") %> No:<%#Eval("No") %>
                        <br />
                    </ItemTemplate>
                </asp:Repeater>
            </ItemTemplate>
        </asp:Repeater>

        <asp:Repeater ID="parentRepeaterControlLevel" runat="server">
            <ItemTemplate>
                <%# Eval("ParentID").ToString() %>
                <br />
                <!-- Repeated data -->
                <asp:Repeater ID="childRepeaterControlLevel" runat="server" OnDataBinding="childRepeaterControlLevel_DataBinding">
                    <ItemTemplate>
                        <!-- Nested repeated data -->
                        Name:<%#Eval("Name") %> No:<%#Eval("No") %>
                        <br />
                    </ItemTemplate>
                </asp:Repeater>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
