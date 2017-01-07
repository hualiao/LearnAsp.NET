<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EvalAndBind.aspx.cs" Inherits="WebApp.EvalAndBind" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Data binding demo</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:GridView 
            ID="grdTest" 
            runat="server" 
            AutoGenerateEditButton="true" 
            AutoGenerateColumns="false" 
            DataSourceID="mySource">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <%# Eval("Name") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox 
                            ID="edtName" 
                            runat="server" 
                            Text='<%# Bind("Name") %>' 
                        />
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </form>

    <asp:ObjectDataSource 
        ID="mySource" 
        runat="server"
        SelectMethod="Select" 
        UpdateMethod="Update" 
        TypeName="WebApp.DemoObject.CustomDataSource" />
</body>
</html>
