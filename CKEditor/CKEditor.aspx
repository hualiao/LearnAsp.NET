<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CKEditor.aspx.cs" Inherits="CKEditorDemo.CKEditor" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKeditor" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CKEditor</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <CKeditor:CKEditorControl ID="CKeditorControl1" runat="server" BasePath="~/Scripts/ckeditor" OnPreRender="CKeditorControl1_PreRender"></CKeditor:CKEditorControl>
    </div>
    </form>
</body>
</html>
