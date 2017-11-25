<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptRetoqueDiseño.aspx.cs" Inherits="Sistareo.web.Reports.RptRetoqueDiseño" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport"  content="width-device-width"/>
    <title>Reporte Diseño y Retoque</title>
    <script runat="server">

    </script>
</head>
<body>
    <form id="form1" runat="server">
   <div >
       <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
       <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" >
       </rsweb:ReportViewer>

   </div>
    </form>
</body>
</html>
