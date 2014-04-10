<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpData.aspx.cs" Inherits="Form_UpData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>数据填报</title>
    <link href="../jQueryEasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../My97DatePicker/skin/WdatePicker.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../css/zTreeStyle/djxt.css" rel="stylesheet" type="text/css" />
    <link href="../css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />
    <link href="../css/themes/default/default.css" rel="stylesheet" type="text/css" />
    <script src="../jQueryEasyUI/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../js/jquery.ztree.core-3.5.js" type="text/javascript"></script>
    <script src="../Js/kindeditor-min.js" type="text/javascript"></script>
    <script src="../Js/kindeditor.js" type="text/javascript"></script>
    <script src="../Js/lang/zh_CN.js" type="text/javascript"></script>
    <style type="text/css">
        body
        {
            height: 100%;
            margin: 10px;
            padding: 0px;
        }
        #menu
        {
            border: 1px solid #2a88bb;
        }
        .ipt
        {
            width: 80px;
            border: solid 1px black;
        }
        .ipt_zb
        {
            border: solid 1px black;
        }
        .tdh
        {
            width: 90px;
        }
    </style>
    <script type="text/javascript">
    	<!--
        var ids;
        var count_area = 0;
        var connect_0;
        var connect_1;
        var connect_2;
        var connect_3;
        var connect_4;
        var connect_5;
        var connect_6;
        var connect_7;
        var connect_8;
        $(function () {
            SetTime();
            load();
            $("#btnUp").click(function () {
                var keyValue = '';
                var key;
                if (count_area == 3) {
                    key = ids.split(',');
                    if (key.length == 1) {
                        keyValue += key[0] + '~' + connect_0.html() + '`';
                    } else if (key.length == 2) {
                        keyValue += key[0] + '~' + connect_0.html() + '`';
                        keyValue += key[1] + '~' + connect_1.html() + '`';
                    } else if (key.length == 3) {
                        keyValue += key[0] + '~' + connect_0.html() + '`';
                        keyValue += key[1] + '~' + connect_1.html() + '`';
                        keyValue += key[2] + '~' + connect_2.html() + '`';
                    } else if (key.length == 4) {
                        keyValue += key[0] + '~' + connect_0.html() + '`';
                        keyValue += key[1] + '~' + connect_1.html() + '`';
                        keyValue += key[2] + '~' + connect_2.html() + '`';
                        keyValue += key[3] + '~' + connect_3.html() + '`';
                    } else if (key.length == 5) {
                        keyValue += key[0] + '~' + connect_0.html() + '`';
                        keyValue += key[1] + '~' + connect_1.html() + '`';
                        keyValue += key[2] + '~' + connect_2.html() + '`';
                        keyValue += key[3] + '~' + connect_3.html() + '`';
                        keyValue += key[4] + '~' + connect_4.html() + '`';
                    } else if (key.length == 6) {
                        keyValue += key[0] + '~' + connect_0.html() + '`';
                        keyValue += key[1] + '~' + connect_1.html() + '`';
                        keyValue += key[2] + '~' + connect_2.html() + '`';
                        keyValue += key[3] + '~' + connect_3.html() + '`';
                        keyValue += key[4] + '~' + connect_4.html() + '`';
                        keyValue += key[5] + '~' + connect_5.html() + '`';
                    } else if (key.length == 7) {
                        keyValue += key[0] + '~' + connect_0.html() + '`';
                        keyValue += key[1] + '~' + connect_1.html() + '`';
                        keyValue += key[2] + '~' + connect_2.html() + '`';
                        keyValue += key[3] + '~' + connect_3.html() + '`';
                        keyValue += key[4] + '~' + connect_4.html() + '`';
                        keyValue += key[5] + '~' + connect_5.html() + '`';
                        keyValue += key[6] + '~' + connect_6.html() + '`';
                    } else if (key.length == 8) {
                        keyValue += key[0] + '~' + connect_0.html() + '`';
                        keyValue += key[1] + '~' + connect_1.html() + '`';
                        keyValue += key[2] + '~' + connect_2.html() + '`';
                        keyValue += key[3] + '~' + connect_3.html() + '`';
                        keyValue += key[4] + '~' + connect_4.html() + '`';
                        keyValue += key[5] + '~' + connect_5.html() + '`';
                        keyValue += key[6] + '~' + connect_6.html() + '`';
                        keyValue += key[7] + '~' + connect_7.html() + '`';
                    } else if (key.length == 9) {
                        keyValue += key[0] + '~' + connect_0.html() + '`';
                        keyValue += key[1] + '~' + connect_1.html() + '`';
                        keyValue += key[2] + '~' + connect_2.html() + '`';
                        keyValue += key[3] + '~' + connect_3.html() + '`';
                        keyValue += key[4] + '~' + connect_4.html() + '`';
                        keyValue += key[5] + '~' + connect_5.html() + '`';
                        keyValue += key[6] + '~' + connect_6.html() + '`';
                        keyValue += key[7] + '~' + connect_7.html() + '`';
                        keyValue += key[8] + '~' + connect_8.html() + '`';
                    }
                } else {
                    key = ids.split('*');
                    for (var i = 0; i < key.length; i++) {
                        keyValue += key[i] + '~' + jQuery('#' + key[i]).val() + '`';
                    }
                }
                keyValue = keyValue.substring(0, keyValue.length - 1);

                var timeType = "0";
                if (!$("#dv_day").is(":hidden")) {
                    $("#dv_day").show();
                    time = $("#txtDay").val();
                    timeType = "1";
                }
                if (!$("#txtMonth").is(":hidden")) {
                    $("#txtMonth").show();
                    time = $("#txtMonth").val();
                    timeType = "2";
                }
                if (!$("#txtYear").is(":hidden")) {
                    $("#txtYear").show();
                    time = $("#txtYear").val();
                    timeType = "3";
                }
                $.messager.confirm('数据填报', '请确认是否要保存数据!', function (ok) {
                    if (ok) {
                        $.post("UpData.aspx", { param: 'query', time: time, value: escape(keyValue), timeType: timeType }, function (data) {
                            $.messager.alert('数据填报', data.info, 'info');
                        }, 'json');
                    } else {
                        $.messager.alert('数据填报', '数据保存已经取消!', 'info');
                    }
                });
            });

            //公式计算
            $("#btnReckon").click(function () {
                var keyValue = '';
                var key = ids.split('*');
                for (var i = 0; i < key.length; i++) {
                    keyValue += key[i] + '~' + jQuery('#' + key[i]).val() + '`';
                }
                keyValue = keyValue.substring(0, keyValue.length - 1);
                $.post("UpData.aspx", { param: 'reckon', value: escape(keyValue) }, function (data) {
                    var val = data.key;
                    //                    var type = data.type;
                    //                    if (type == "0") {
                    //                        key = val.split('*');
                    //                        //                        for (var i = 0; i < key.length; i++) {
                    //                        $("#" + key[0]).val(key[1]);
                    //                        //                        }
                    //                    } else if (type == "1") {
                    var key = val.split(';');
                    for (var i = 0; i < key.length; i++) {
                        var value = key[i].toString().split('*');
                        $("#" + value[0]).val(value[1]);
                    }
                    //                    }
                }, 'json');
            });
        });
        function getCookie(name) {
            var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");

            if (arr = document.cookie.match(reg))

                return unescape(arr[2]);
            else
                return null;
        }

        var num;
        function load() {
            var orgId = '';
            var treeId = '';

            orgId = getCookie('O_ID'); // parent.document.getElementById("txt_orgId").value;
            treeId = getCookie('T_ID'); //  parent.document.getElementById("txt_treeId").value;

            //            //            if ($("#" + parent.document.getElementById("txt_orgId")).length > 0) {
            //            if (parent.document.getElementById("txt_orgId") != null)
            //                orgId = parent.document.getElementById("txt_orgId").value;
            //            //            }
            //            //            if ($("#" + parent.document.getElementById("txt_treeId")).length > 0) {
            //            if (parent.document.getElementById("txt_treeId") != null)
            //                treeId = parent.document.getElementById("txt_treeId").value;
            //            //            }

            $.post("UpData.aspx", { param: '', id: orgId, treeId: treeId }, function (data) {
                var tb = data.table;
                num = data.num;
                if (num == "0" || num == "1") {
                    $("#dv_day").show();
                    $("#dv_month").hide();
                    $("#dv_year").hide();
                } else if (num == "2") {
                    $("#dv_day").hide();
                    $("#dv_month").show();
                    $("#dv_year").hide();
                } else {
                    $("#dv_day").hide();
                    $("#dv_month").hide();
                    $("#dv_year").show();
                }
                $("#dv_body").html(tb);
                //                ids = data.key;
                if (data.keyid != '')
                    ids = data.keyid;
                else
                    ids = data.key;


                if (Number(data.kind_count) > 0) {
                    count_area = Number(data.areaCount);
                    var key = ids.split(',');
                    var v = data.areaValue.split('`');
                    if (key.length == 1) {
                        connect_0 = KindEditor.create('textarea[name="' + key[0] + '"]');
                        if (data.areaValue != "") {
                            connect_0.html(v[0]);
                        }
                    } else if (key.length == 2) {
                        connect_0 = KindEditor.create('textarea[name="' + key[0] + '"]');
                        connect_1 = KindEditor.create('textarea[name="' + key[1] + '"]');
                        if (data.areaValue != "") {
                            connect_0.html(v[0]);
                            connect_1.html(v[1]);
                        }
                    } else if (key.length == 3) {
                        connect_0 = KindEditor.create('textarea[name="' + key[0] + '"]');
                        connect_1 = KindEditor.create('textarea[name="' + key[1] + '"]');
                        connect_2 = KindEditor.create('textarea[name="' + key[2] + '"]');
                        connect_0.html(v[0]);
                        connect_1.html(v[1]);
                        connect_2.html(v[2]);
                    } else if (key.length == 4) {
                        connect_0 = KindEditor.create('textarea[name="' + key[0] + '"]');
                        connect_1 = KindEditor.create('textarea[name="' + key[1] + '"]');
                        connect_2 = KindEditor.create('textarea[name="' + key[2] + '"]');
                        connect_3 = KindEditor.create('textarea[name="' + key[3] + '"]');
                        if (data.areaValue != "") {
                            connect_0.html(v[0]);
                            connect_1.html(v[1]);
                            connect_2.html(v[2]);
                            connect_3.html(v[3]);
                        }
                    } else if (key.length == 5) {
                        connect_0 = KindEditor.create('textarea[name="' + key[0] + '"]');
                        connect_1 = KindEditor.create('textarea[name="' + key[1] + '"]');
                        connect_2 = KindEditor.create('textarea[name="' + key[2] + '"]');
                        connect_3 = KindEditor.create('textarea[name="' + key[3] + '"]');
                        connect_4 = KindEditor.create('textarea[name="' + key[4] + '"]');
                        if (data.areaValue != "") {
                            connect_0.html(v[0]);
                            connect_1.html(v[1]);
                            connect_2.html(v[2]);
                            connect_3.html(v[3]);
                            connect_4.html(v[4]);
                        }
                    } else if (key.length == 6) {
                        connect_0 = KindEditor.create('textarea[name="' + key[0] + '"]');
                        connect_1 = KindEditor.create('textarea[name="' + key[1] + '"]');
                        connect_2 = KindEditor.create('textarea[name="' + key[2] + '"]');
                        connect_3 = KindEditor.create('textarea[name="' + key[3] + '"]');
                        connect_4 = KindEditor.create('textarea[name="' + key[4] + '"]');
                        connect_5 = KindEditor.create('textarea[name="' + key[5] + '"]');
                        if (data.areaValue != "") {
                            connect_0.html(v[0]);
                            connect_1.html(v[1]);
                            connect_2.html(v[2]);
                            connect_3.html(v[3]);
                            connect_4.html(v[4]);
                            connect_5.html(v[5]);
                        }
                    } else if (key.length == 7) {
                        connect_0 = KindEditor.create('textarea[name="' + key[0] + '"]');
                        connect_1 = KindEditor.create('textarea[name="' + key[1] + '"]');
                        connect_2 = KindEditor.create('textarea[name="' + key[2] + '"]');
                        connect_3 = KindEditor.create('textarea[name="' + key[3] + '"]');
                        connect_4 = KindEditor.create('textarea[name="' + key[4] + '"]');
                        connect_5 = KindEditor.create('textarea[name="' + key[5] + '"]');
                        connect_6 = KindEditor.create('textarea[name="' + key[6] + '"]');
                        if (data.areaValue != "") {
                            connect_0.html(v[0]);
                            connect_1.html(v[1]);
                            connect_2.html(v[2]);
                            connect_3.html(v[3]);
                            connect_4.html(v[4]);
                            connect_5.html(v[5]);
                            connect_6.html(v[6]);
                        }
                    } else if (key.length == 8) {
                        connect_0 = KindEditor.create('textarea[name="' + key[0] + '"]');
                        connect_1 = KindEditor.create('textarea[name="' + key[1] + '"]');
                        connect_2 = KindEditor.create('textarea[name="' + key[2] + '"]');
                        connect_3 = KindEditor.create('textarea[name="' + key[3] + '"]');
                        connect_4 = KindEditor.create('textarea[name="' + key[4] + '"]');
                        connect_5 = KindEditor.create('textarea[name="' + key[5] + '"]');
                        connect_6 = KindEditor.create('textarea[name="' + key[6] + '"]');
                        connect_7 = KindEditor.create('textarea[name="' + key[7] + '"]');
                        if (data.areaValue != "") {
                            connect_0.html(v[0]);
                            connect_1.html(v[1]);
                            connect_2.html(v[2]);
                            connect_3.html(v[3]);
                            connect_4.html(v[4]);
                            connect_5.html(v[5]);
                            connect_6.html(v[6]);
                            connect_7.html(v[7]);
                        }
                    }
                }
            }, 'json');
        }
        function SetTime() {
            var myDate = new Date();
            var date;
            date = myDate.getFullYear() + '-' + Number(myDate.getMonth() + 1) + '-' + Number(myDate.getDate());
            $("#txtDay").val(date);
            $("#txtMonth").val(myDate.getFullYear() + '-' + Number(myDate.getMonth() + 1));
            $("#txtYear").val(myDate.getFullYear());
        }
        var datetime;
        function showDay() {
            datetime = $("#txtDay").val();
            $.post("UpData.aspx", { param: 'datetime', time: datetime, type: '1' }, function (data) {
                var tb = data.table;
                num = data.num;
                if (num == "0" || num == "1") {
                    $("#dv_day").show();
                    $("#dv_month").hide();
                    $("#dv_year").hide();
                } else if (num == "2") {
                    $("#dv_day").hide();
                    $("#dv_month").show();
                    $("#dv_year").hide();
                } else {
                    $("#dv_day").hide();
                    $("#dv_month").hide();
                    $("#dv_year").show();
                }
                $("#dv_body").html(tb);
                //                ids = data.key;
                if (data.keyid != '')
                    ids = data.keyid;
                else
                    ids = data.key;


                if (Number(data.kind_count) > 0) {
                    count_area = Number(data.areaCount);
                    var key = ids.split(',');
                    var v = data.areaValue.split('`');
                    if (key.length == 1) {
                        connect_0 = KindEditor.create('textarea[name="' + key[0] + '"]');
                        if (data.areaValue != "") {
                            connect_0.html(v[0]);
                        }
                    } else if (key.length == 2) {
                        connect_0 = KindEditor.create('textarea[name="' + key[0] + '"]');
                        connect_1 = KindEditor.create('textarea[name="' + key[1] + '"]');
                        if (data.areaValue != "") {
                            connect_0.html(v[0]);
                            connect_1.html(v[1]);
                        }
                    } else if (key.length == 3) {
                        connect_0 = KindEditor.create('textarea[name="' + key[0] + '"]');
                        connect_1 = KindEditor.create('textarea[name="' + key[1] + '"]');
                        connect_2 = KindEditor.create('textarea[name="' + key[2] + '"]');
                        connect_0.html(v[0]);
                        connect_1.html(v[1]);
                        connect_2.html(v[2]);
                    } else if (key.length == 4) {
                        connect_0 = KindEditor.create('textarea[name="' + key[0] + '"]');
                        connect_1 = KindEditor.create('textarea[name="' + key[1] + '"]');
                        connect_2 = KindEditor.create('textarea[name="' + key[2] + '"]');
                        connect_3 = KindEditor.create('textarea[name="' + key[3] + '"]');
                        if (data.areaValue != "") {
                            connect_0.html(v[0]);
                            connect_1.html(v[1]);
                            connect_2.html(v[2]);
                            connect_3.html(v[3]);
                        }
                    } else if (key.length == 5) {
                        connect_0 = KindEditor.create('textarea[name="' + key[0] + '"]');
                        connect_1 = KindEditor.create('textarea[name="' + key[1] + '"]');
                        connect_2 = KindEditor.create('textarea[name="' + key[2] + '"]');
                        connect_3 = KindEditor.create('textarea[name="' + key[3] + '"]');
                        connect_4 = KindEditor.create('textarea[name="' + key[4] + '"]');
                        if (data.areaValue != "") {
                            connect_0.html(v[0]);
                            connect_1.html(v[1]);
                            connect_2.html(v[2]);
                            connect_3.html(v[3]);
                            connect_4.html(v[4]);
                        }
                    } else if (key.length == 6) {
                        connect_0 = KindEditor.create('textarea[name="' + key[0] + '"]');
                        connect_1 = KindEditor.create('textarea[name="' + key[1] + '"]');
                        connect_2 = KindEditor.create('textarea[name="' + key[2] + '"]');
                        connect_3 = KindEditor.create('textarea[name="' + key[3] + '"]');
                        connect_4 = KindEditor.create('textarea[name="' + key[4] + '"]');
                        connect_5 = KindEditor.create('textarea[name="' + key[5] + '"]');
                        if (data.areaValue != "") {
                            connect_0.html(v[0]);
                            connect_1.html(v[1]);
                            connect_2.html(v[2]);
                            connect_3.html(v[3]);
                            connect_4.html(v[4]);
                            connect_5.html(v[5]);
                        }
                    } else if (key.length == 7) {
                        connect_0 = KindEditor.create('textarea[name="' + key[0] + '"]');
                        connect_1 = KindEditor.create('textarea[name="' + key[1] + '"]');
                        connect_2 = KindEditor.create('textarea[name="' + key[2] + '"]');
                        connect_3 = KindEditor.create('textarea[name="' + key[3] + '"]');
                        connect_4 = KindEditor.create('textarea[name="' + key[4] + '"]');
                        connect_5 = KindEditor.create('textarea[name="' + key[5] + '"]');
                        connect_6 = KindEditor.create('textarea[name="' + key[6] + '"]');
                        if (data.areaValue != "") {
                            connect_0.html(v[0]);
                            connect_1.html(v[1]);
                            connect_2.html(v[2]);
                            connect_3.html(v[3]);
                            connect_4.html(v[4]);
                            connect_5.html(v[5]);
                            connect_6.html(v[6]);
                        }
                    } else if (key.length == 8) {
                        connect_0 = KindEditor.create('textarea[name="' + key[0] + '"]');
                        connect_1 = KindEditor.create('textarea[name="' + key[1] + '"]');
                        connect_2 = KindEditor.create('textarea[name="' + key[2] + '"]');
                        connect_3 = KindEditor.create('textarea[name="' + key[3] + '"]');
                        connect_4 = KindEditor.create('textarea[name="' + key[4] + '"]');
                        connect_5 = KindEditor.create('textarea[name="' + key[5] + '"]');
                        connect_6 = KindEditor.create('textarea[name="' + key[6] + '"]');
                        connect_7 = KindEditor.create('textarea[name="' + key[7] + '"]');
                        if (data.areaValue != "") {
                            connect_0.html(v[0]);
                            connect_1.html(v[1]);
                            connect_2.html(v[2]);
                            connect_3.html(v[3]);
                            connect_4.html(v[4]);
                            connect_5.html(v[5]);
                            connect_6.html(v[6]);
                            connect_7.html(v[7]);
                        }
                    }
                }
            }, 'json');
        }
        function showMonth() {
            datetime = $("#txtMonth").val();
            $.post("UpData.aspx", { param: 'datetime', time: datetime, type: '2' }, function (data) {
                var tb = data.table;
                num = data.num;
                if (num == "0" || num == "1") {
                    $("#dv_day").show();
                    $("#dv_month").hide();
                    $("#dv_year").hide();
                } else if (num == "2") {
                    $("#dv_day").hide();
                    $("#dv_month").show();
                    $("#dv_year").hide();
                } else {
                    $("#dv_day").hide();
                    $("#dv_month").hide();
                    $("#dv_year").show();
                }
                $("#dv_body").html(tb);
                //                ids = data.key;
                if (data.keyid != '')
                    ids = data.keyid;
                else
                    ids = data.key;


                if (Number(data.kind_count) > 0) {
                    count_area = Number(data.areaCount);
                    var key = ids.split(',');
                    var v = data.areaValue.split('`');
                    if (key.length == 1) {
                        connect_0 = KindEditor.create('textarea[name="' + key[0] + '"]');
                        if (data.areaValue != "") {
                            connect_0.html(v[0]);
                        }
                    } else if (key.length == 2) {
                        connect_0 = KindEditor.create('textarea[name="' + key[0] + '"]');
                        connect_1 = KindEditor.create('textarea[name="' + key[1] + '"]');
                        if (data.areaValue != "") {
                            connect_0.html(v[0]);
                            connect_1.html(v[1]);
                        }
                    } else if (key.length == 3) {
                        connect_0 = KindEditor.create('textarea[name="' + key[0] + '"]');
                        connect_1 = KindEditor.create('textarea[name="' + key[1] + '"]');
                        connect_2 = KindEditor.create('textarea[name="' + key[2] + '"]');
                        connect_0.html(v[0]);
                        connect_1.html(v[1]);
                        connect_2.html(v[2]);
                    } else if (key.length == 4) {
                        connect_0 = KindEditor.create('textarea[name="' + key[0] + '"]');
                        connect_1 = KindEditor.create('textarea[name="' + key[1] + '"]');
                        connect_2 = KindEditor.create('textarea[name="' + key[2] + '"]');
                        connect_3 = KindEditor.create('textarea[name="' + key[3] + '"]');
                        if (data.areaValue != "") {
                            connect_0.html(v[0]);
                            connect_1.html(v[1]);
                            connect_2.html(v[2]);
                            connect_3.html(v[3]);
                        }
                    } else if (key.length == 5) {
                        connect_0 = KindEditor.create('textarea[name="' + key[0] + '"]');
                        connect_1 = KindEditor.create('textarea[name="' + key[1] + '"]');
                        connect_2 = KindEditor.create('textarea[name="' + key[2] + '"]');
                        connect_3 = KindEditor.create('textarea[name="' + key[3] + '"]');
                        connect_4 = KindEditor.create('textarea[name="' + key[4] + '"]');
                        if (data.areaValue != "") {
                            connect_0.html(v[0]);
                            connect_1.html(v[1]);
                            connect_2.html(v[2]);
                            connect_3.html(v[3]);
                            connect_4.html(v[4]);
                        }
                    } else if (key.length == 6) {
                        connect_0 = KindEditor.create('textarea[name="' + key[0] + '"]');
                        connect_1 = KindEditor.create('textarea[name="' + key[1] + '"]');
                        connect_2 = KindEditor.create('textarea[name="' + key[2] + '"]');
                        connect_3 = KindEditor.create('textarea[name="' + key[3] + '"]');
                        connect_4 = KindEditor.create('textarea[name="' + key[4] + '"]');
                        connect_5 = KindEditor.create('textarea[name="' + key[5] + '"]');
                        if (data.areaValue != "") {
                            connect_0.html(v[0]);
                            connect_1.html(v[1]);
                            connect_2.html(v[2]);
                            connect_3.html(v[3]);
                            connect_4.html(v[4]);
                            connect_5.html(v[5]);
                        }
                    } else if (key.length == 7) {
                        connect_0 = KindEditor.create('textarea[name="' + key[0] + '"]');
                        connect_1 = KindEditor.create('textarea[name="' + key[1] + '"]');
                        connect_2 = KindEditor.create('textarea[name="' + key[2] + '"]');
                        connect_3 = KindEditor.create('textarea[name="' + key[3] + '"]');
                        connect_4 = KindEditor.create('textarea[name="' + key[4] + '"]');
                        connect_5 = KindEditor.create('textarea[name="' + key[5] + '"]');
                        connect_6 = KindEditor.create('textarea[name="' + key[6] + '"]');
                        if (data.areaValue != "") {
                            connect_0.html(v[0]);
                            connect_1.html(v[1]);
                            connect_2.html(v[2]);
                            connect_3.html(v[3]);
                            connect_4.html(v[4]);
                            connect_5.html(v[5]);
                            connect_6.html(v[6]);
                        }
                    } else if (key.length == 8) {
                        connect_0 = KindEditor.create('textarea[name="' + key[0] + '"]');
                        connect_1 = KindEditor.create('textarea[name="' + key[1] + '"]');
                        connect_2 = KindEditor.create('textarea[name="' + key[2] + '"]');
                        connect_3 = KindEditor.create('textarea[name="' + key[3] + '"]');
                        connect_4 = KindEditor.create('textarea[name="' + key[4] + '"]');
                        connect_5 = KindEditor.create('textarea[name="' + key[5] + '"]');
                        connect_6 = KindEditor.create('textarea[name="' + key[6] + '"]');
                        connect_7 = KindEditor.create('textarea[name="' + key[7] + '"]');
                        if (data.areaValue != "") {
                            connect_0.html(v[0]);
                            connect_1.html(v[1]);
                            connect_2.html(v[2]);
                            connect_3.html(v[3]);
                            connect_4.html(v[4]);
                            connect_5.html(v[5]);
                            connect_6.html(v[6]);
                            connect_7.html(v[7]);
                        }
                    }
                }
            }, 'json');
        }
        function showYear() {
            datetime = $("#txtYear").val();
            $.post("UpData.aspx", { param: 'datetime', time: datetime, type: '3' }, function (data) {
                var tb = data.table;
                num = data.num;
                if (num == "0" || num == "1") {
                    $("#dv_day").show();
                    $("#dv_month").hide();
                    $("#dv_year").hide();
                } else if (num == "2") {
                    $("#dv_day").hide();
                    $("#dv_month").show();
                    $("#dv_year").hide();
                } else {
                    $("#dv_day").hide();
                    $("#dv_month").hide();
                    $("#dv_year").show();
                }
                $("#dv_body").html(tb);
                //                ids = data.key;
                if (data.keyid != '')
                    ids = data.keyid;
                else
                    ids = data.key;


                if (Number(data.kind_count) > 0) {
                    count_area = Number(data.areaCount);
                    var key = ids.split(',');
                    var v = data.areaValue.split('`');
                    if (key.length == 1) {
                        connect_0 = KindEditor.create('textarea[name="' + key[0] + '"]');
                        if (data.areaValue != "") {
                            connect_0.html(v[0]);
                        }
                    } else if (key.length == 2) {
                        connect_0 = KindEditor.create('textarea[name="' + key[0] + '"]');
                        connect_1 = KindEditor.create('textarea[name="' + key[1] + '"]');
                        if (data.areaValue != "") {
                            connect_0.html(v[0]);
                            connect_1.html(v[1]);
                        }
                    } else if (key.length == 3) {
                        connect_0 = KindEditor.create('textarea[name="' + key[0] + '"]');
                        connect_1 = KindEditor.create('textarea[name="' + key[1] + '"]');
                        connect_2 = KindEditor.create('textarea[name="' + key[2] + '"]');
                        connect_0.html(v[0]);
                        connect_1.html(v[1]);
                        connect_2.html(v[2]);
                    } else if (key.length == 4) {
                        connect_0 = KindEditor.create('textarea[name="' + key[0] + '"]');
                        connect_1 = KindEditor.create('textarea[name="' + key[1] + '"]');
                        connect_2 = KindEditor.create('textarea[name="' + key[2] + '"]');
                        connect_3 = KindEditor.create('textarea[name="' + key[3] + '"]');
                        if (data.areaValue != "") {
                            connect_0.html(v[0]);
                            connect_1.html(v[1]);
                            connect_2.html(v[2]);
                            connect_3.html(v[3]);
                        }
                    } else if (key.length == 5) {
                        connect_0 = KindEditor.create('textarea[name="' + key[0] + '"]');
                        connect_1 = KindEditor.create('textarea[name="' + key[1] + '"]');
                        connect_2 = KindEditor.create('textarea[name="' + key[2] + '"]');
                        connect_3 = KindEditor.create('textarea[name="' + key[3] + '"]');
                        connect_4 = KindEditor.create('textarea[name="' + key[4] + '"]');
                        if (data.areaValue != "") {
                            connect_0.html(v[0]);
                            connect_1.html(v[1]);
                            connect_2.html(v[2]);
                            connect_3.html(v[3]);
                            connect_4.html(v[4]);
                        }
                    } else if (key.length == 6) {
                        connect_0 = KindEditor.create('textarea[name="' + key[0] + '"]');
                        connect_1 = KindEditor.create('textarea[name="' + key[1] + '"]');
                        connect_2 = KindEditor.create('textarea[name="' + key[2] + '"]');
                        connect_3 = KindEditor.create('textarea[name="' + key[3] + '"]');
                        connect_4 = KindEditor.create('textarea[name="' + key[4] + '"]');
                        connect_5 = KindEditor.create('textarea[name="' + key[5] + '"]');
                        if (data.areaValue != "") {
                            connect_0.html(v[0]);
                            connect_1.html(v[1]);
                            connect_2.html(v[2]);
                            connect_3.html(v[3]);
                            connect_4.html(v[4]);
                            connect_5.html(v[5]);
                        }
                    } else if (key.length == 7) {
                        connect_0 = KindEditor.create('textarea[name="' + key[0] + '"]');
                        connect_1 = KindEditor.create('textarea[name="' + key[1] + '"]');
                        connect_2 = KindEditor.create('textarea[name="' + key[2] + '"]');
                        connect_3 = KindEditor.create('textarea[name="' + key[3] + '"]');
                        connect_4 = KindEditor.create('textarea[name="' + key[4] + '"]');
                        connect_5 = KindEditor.create('textarea[name="' + key[5] + '"]');
                        connect_6 = KindEditor.create('textarea[name="' + key[6] + '"]');
                        if (data.areaValue != "") {
                            connect_0.html(v[0]);
                            connect_1.html(v[1]);
                            connect_2.html(v[2]);
                            connect_3.html(v[3]);
                            connect_4.html(v[4]);
                            connect_5.html(v[5]);
                            connect_6.html(v[6]);
                        }
                    } else if (key.length == 8) {
                        connect_0 = KindEditor.create('textarea[name="' + key[0] + '"]');
                        connect_1 = KindEditor.create('textarea[name="' + key[1] + '"]');
                        connect_2 = KindEditor.create('textarea[name="' + key[2] + '"]');
                        connect_3 = KindEditor.create('textarea[name="' + key[3] + '"]');
                        connect_4 = KindEditor.create('textarea[name="' + key[4] + '"]');
                        connect_5 = KindEditor.create('textarea[name="' + key[5] + '"]');
                        connect_6 = KindEditor.create('textarea[name="' + key[6] + '"]');
                        connect_7 = KindEditor.create('textarea[name="' + key[7] + '"]');
                        if (data.areaValue != "") {
                            connect_0.html(v[0]);
                            connect_1.html(v[1]);
                            connect_2.html(v[2]);
                            connect_3.html(v[3]);
                            connect_4.html(v[4]);
                            connect_5.html(v[5]);
                            connect_6.html(v[6]);
                            connect_7.html(v[7]);
                        }
                    }
                }
            }, 'json');
        }

        function pageHeight() {
            if ($.browser.msie) {
                return document.compatMode == "CSS1Compat" ? document.documentElement.clientHeight :
            document.body.clientHeight;
            } else {
                return self.innerHeight;
            }
        };

        function pageWidth() {
            if ($.browser.msie) {
                return document.compatMode == "CSS1Compat" ? document.documentElement.clientWidth :
            document.body.clientWidth;
            } else {
                return self.innerWidth;
            }
        }; 
        //-->
    </script>
</head>
<body style="font-size: 12px;">
    <table>
        <tr>
            <td>
                <div id="dv_day" style="margin-top: 5px; height: 30px;">
                    &nbsp; &nbsp 开始时间: &nbsp; &nbsp;
                    <input type="text" id="txtDay" style="text-align: center;" runat="server" onfocus="WdatePicker({skin:'whyGreen',dateFmt:'yyyy-MM-dd'})"
                        class="Wdate" onchange="showDay()" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </div>
                <div id="dv_month" style="margin-top: 5px; height: 30px; display: none;">
                    &nbsp; &nbsp 开始时间: &nbsp; &nbsp;
                    <input type="text" id="txtMonth" style="text-align: center;" runat="server" onfocus="WdatePicker({skin:'whyGreen',dateFmt:'yyyy-MM'})"
                        class="Wdate" onchange="showMonth()" />
                </div>
                <div id="dv_year" style="margin-top: 5px; height: 30px; display: none;">
                    &nbsp; &nbsp 开始时间: &nbsp; &nbsp;
                    <input type="text" id="txtYear" style="text-align: center;" runat="server" onfocus="WdatePicker({skin:'whyGreen',dateFmt:'yyyy'})"
                        class="Wdate" onchange="showYear()" />
                </div>
            </td>
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a id="btnReckon" href="#"
                    class="easyui-linkbutton" data-options="iconCls:'icon-ok'">计&nbsp;&nbsp;算</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <a id="btnUp" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-ok'">保&nbsp;&nbsp;存</a>
            </td>
        </tr>
    </table>
    <div id="dv_body">
    </div>
</body>
</html>
