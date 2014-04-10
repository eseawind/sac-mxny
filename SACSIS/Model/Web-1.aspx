<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Web-1.aspx.cs" Inherits="SACSIS.Model.Web_1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../Js/My97DatePicker/skin/WdatePicker.css" rel="stylesheet" type="text/css" />
    <script src="../Js/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../Js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {


        });
        function change_value() {
            alert($("#txtMonth").val());
        }
    </script>
</head>
<body>
    <input type="text" id="txtMonth" style="text-align: center;" runat="server" readonly="readonly"
        onchange="change_value()" onfocus="WdatePicker({skin:'whyGreen',dateFmt:'yyyy-MM-dd'})"
        class="Wdate" />
</body>
</html>
