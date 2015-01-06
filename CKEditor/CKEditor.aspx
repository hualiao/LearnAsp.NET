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
        <CKeditor:CKEditorControl ID="CKeditorControl1" runat="server" BasePath="~/Scripts/ckeditor" OnLoad="CKeditorControl1_Load"></CKeditor:CKEditorControl>
    </div>
    <textarea id="editor1" name="editor1" rows="10" cols="80"></textarea>
    <input type="submit" value="Submit" />
    </form>
    <script type="text/javascript">
        CKEDITOR.replace('editor1', { htmlEncodeOutput: true});
    </script>
</body>
</html>
