var chart1;
$(function () {
    chart1 = new Highcharts.Chart({
        //$('#container').highcharts
        chart: {
            renderTo: "container",
            type: 'spline'
        },
        title: {
            text: '数据趋势图'
        },
        subtitle: {//副标题
            text: ''
        },
        exporting: {
            enabled: false //用来设置是否显示‘打印’,'导出'等功能按钮，不设置时默认为显示 
        },
        xAxis: {
            type: 'datetime', reversed: true, allowDecimals: false,
            tickPixelInterval: 50,
            //reversed: true(颠倒排列顺序), allowDecimals: false(显示为整数)，tickPixelInterval:50(刻度间隔)
            labels: { formatter: function () {
                var vDate = new Date(this.value);
                return vDate.getHours() + ":" + vDate.getMinutes() + ":" + vDate.getSeconds();
            }
            }
        },
        yAxis: {
            title: {
                text: '百分比 (%)'
            }
        },
        global: {
            useUTC: false
        }
    });

    var vars = new Array(), hash;

    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    if (vars["operate_id"] == "0") {

        //$("#aadd").hide();

        $.post("ChartManage.aspx", { chart: vars["chart_id"] }, function (data) {

            $("#hf_value").attr("value", data.str_para_id);
            if (data.str_para_id.length > 0) {
                for (var i = 0; i < data.str_para_id.length; i++) {
                    array_total.push(data.str_para_id[i]);
                }
                getLine(data);
            }
        }, 'json');
    }


    $('#aadd').live("click", function () {

        change_type(2);
        $("#txt_point_name").attr("value", "");
        $("#div_point_name").html('');

        $('#add').show();
        $("#add").attr('title', '添加菜单');
        var dlg = $('#add').dialog({
    });
    dlg.parent().appendTo(jQuery("form:first"));
});

$('#aedit').live("click", function () {
    //alert($("#hf_value").val());
    if ($("#hf_value").val() != "") {
        var str_data = "<table><tr>";
        var count = 0;
        if ($("#hf_value").val().split(',').length > 0) {
            for (var i = 0; i < $("#hf_value").val().split(',').length; i++) {
                count++;
                if (count % 2 == 0) {
                    str_data += "<td width=\"300px\"><input type='checkbox' checked='checked' name='" + $("#hf_value").val().split(',')[i].split('|')[1] + "' value='" + $("#hf_value").val().split(',')[i].split('|')[0] + "'>&nbsp;&nbsp;&nbsp;" + $("#hf_value").val().split(',')[i].split('|')[1] + "&nbsp;&nbsp;<br></td></tr>";
                }
                else {
                    str_data += "<td width=\"300px\"><input type='checkbox' checked='checked' name='" + $("#hf_value").val().split(',')[i].split('|')[1] + "' value='" + $("#hf_value").val().split(',')[i].split('|')[0] + "'>&nbsp;&nbsp;&nbsp;" + $("#hf_value").val().split(',')[i].split('|')[1] + "&nbsp;&nbsp;</td>";
                }
                if ((count % 2 != 0) && (i == $("#hf_value").val().split(',').length - 1)) {
                    str_data += "</tr>";
                }
                //str_data += "<option value=" + $("#hf_value").val().split(';')[i].split(',')[0] + ">" + $("#hf_value").val().split(';')[i].split(',')[1] + "</option>";
            }
            str_data += "</table>";
        }
        $("#div_value_edit").html(str_data);
        $('#edit').show();
        $("#edit").attr('title', '编辑菜单');
        var dlg = $('#edit').dialog({
    });
    dlg.parent().appendTo(jQuery("form:first"));
}
else {
    alert("请先添加测点！");
}
});

$('#seach').live("click", function () {
    if ($("#hf_value").val() != "") {
        if ($("#txt_jiange").val() != "") {
            var rating_time = "";
            if (($("#stime").val() != "") && ($("#etime").val() != "")) {
                rating_time = $("#stime").val() + "," + $("#etime").val();
            }
            $.post("ChartManage.aspx", { rating: $("#hf_value").val(), rating_time: rating_time, jiange: $("#txt_jiange").val() + "|" + $("#sec_jiange").find("option:selected").val() }, function (data) {
                getLine(data);
            }, 'json');
        }
        else {
            alert("请输入时间间隔！");
        }
    }
    else {
        alert("请先选择测点！");
    }
});

$('#preserve').live("click", function () {

    if ($("#hf_value").val() != "") {
        if (vars["operate_id"] == "0") {
            $.post("ChartManage.aspx", { chart_name: "1&" + vars["chart_id"] + "&" + $("#hf_value").val() }, function (data) {

            }, 'json');
            alert("保存成功！");
        }
        else {
            $('#pre').show();
            $("#pre").attr('title', '保存模板');
            var dlg = $('#pre').dialog({
        });
        dlg.parent().appendTo(jQuery("form:first"));
    }
}
else {
    alert("当前已选测点为空，请选择要保存的测点！");
}
});

$("#QX_SURE").live("click", function () {
    $('#add').dialog('close');
});
$("#QX_UPDATE").live("click", function () {
    $('#edit').dialog('close');
});
$("#QX_PRE").live("click", function () {
    $('#pre').dialog('close');
});
});

function Hc() {
    var chart;
    Highcharts.theme = {
        colors: ['#058DC7', '#50B432', '#ED561B', '#DDDF00', '#24CBE5', '#64E572', '#FF9655', '#FFF263', '#6AF9C4'],
        chart: {
            backgroundColor: {
                linearGradient: [0, 0, 500, 500],
                stops: [
                    [0, 'rgb(255, 255, 255)'],
                    [1, 'rgb(240, 240, 255)']
                 ]
            },
            borderWidth: 2,
            plotBackgroundColor: 'rgba(255, 255, 255, .9)',
            plotShadow: true,
            plotBorderWidth: 1
        },
        title: {
            style: {
                color: '#000',
                font: 'bold 16px "Trebuchet MS", Verdana, sans-serif'
            }
        },
        global: { useUTC: false },
        subtitle: {
            style: {
                color: '#666666',
                font: 'bold 12px "Trebuchet MS", Verdana, sans-serif'
            }
        },
        xAxis: {
            gridLineWidth: 1,
            lineColor: '#000',
            tickColor: '#000',
            labels: {
                style: {
                    color: '#000',
                    font: '11px Trebuchet MS, Verdana, sans-serif'
                }
            },
            title: {
                style: {
                    color: '#333',
                    fontWeight: 'bold',
                    fontSize: '12px',
                    fontFamily: 'Trebuchet MS, Verdana, sans-serif'

                }
            }
        },
        yAxis: {
            minorTickInterval: 'auto',
            lineColor: '#000',
            lineWidth: 1,
            tickWidth: 1,
            tickColor: '#000',
            labels: {
                style: {
                    color: '#000',
                    font: '11px Trebuchet MS, Verdana, sans-serif'
                }
            },
            title: {
                style: {
                    color: '#333',
                    fontWeight: 'bold',
                    fontSize: '12px',
                    fontFamily: 'Trebuchet MS, Verdana, sans-serif'
                }
            }
        },
        legend: {
            itemStyle: {
                font: '9pt Trebuchet MS, Verdana, sans-serif',
                color: 'black'

            },
            itemHoverStyle: {
                color: '#039'
            },
            itemHiddenStyle: {
                color: 'gray'
            }
        },
        labels: {
            style: {
                color: '#99b'
            }
        }
    };
}

function change_type(a) {

    var par = "";

    if (a == 1) {
        GetChecked(4);
        $("#div_point_name").html('');
        par = $("#ddl_level1").find("option:selected").val();
    }
    else if (a == 2) {
        GetChecked(2);
        par = par =  "," + $("#ddl_level1").find("option:selected").val();
    }
    $.post(
        "Get_Chart_Data.aspx",
        {
            sec_type: par
        },
    function (data) {
        var array = new Array();
        array = data.split('|');
        $("#ddl_level1").empty();
        $("#ddl_level2").empty();
        $("#ddl_level3").empty();
        
        if (data == "") {
            $("#ddl_level1").append("<option value=请选择>-请选择-</option>");
            $("#ddl_level2").append("<option value=请选择>-请选择-</option>");
            $("#ddl_level3").append("<option value=请选择>-请选择-</option>");
            if (par == "T_BASE_PARAID_SUN") {
                $("#ddl_level4").append("<option value=请选择>-请选择-</option>");
            }
        }
        else {
            $("#ddl_level1").append(array[0]);
            $("#ddl_level2").append(array[1]);
            $("#ddl_level3").append(array[2]);
            if (par == "T_BASE_PARAID_SUN") {
                $("#ddl_level4").append(array[3]);
            }
        }
    },

    "html");
}
function change_level1() {
    GetChecked(4);
    $("#div_point_name").html('');
    var par = $("#sec_type").find("option:selected").val() + "," + $("#ddl_level1").find("option:selected").val();
    $.post(
                    	    "../Trend/Get_Chart_Data.aspx",
                    	    {
                    	        ddl_level1: par
                    	    },
                        function (data) {
                            var array = new Array();
                            array = data.split('|');
                            $("#ddl_level2").empty();
                            $("#ddl_level3").empty();
                            if (par == "T_BASE_PARAID_SUN") {
                                $("#ddl_level4").empty();
                            }
                            if (data == "") {
                                $("#ddl_level2").append("<option value=请选择>-请选择-</option>");
                                $("#ddl_level3").append("<option value=请选择>-请选择-</option>");
                                if (par == "T_BASE_PARAID_SUN") {
                                    $("#ddl_level4").append("<option value=请选择>-请选择-</option>");
                                }
                            }
                            else {
                                $("#ddl_level2").append(array[0]);
                                $("#ddl_level3").append(array[1]);
                                if (par == "T_BASE_PARAID_SUN") {
                                    $("#ddl_level4").append(array[2]);
                                }
                            }
                        },

                        "html");
}
function change_level2() {
    GetChecked(4);
    $("#div_point_name").html('');
    var par = $("#sec_type").find("option:selected").val() + "," + $("#ddl_level1").find("option:selected").val() + "," + $("#ddl_level2").find("option:selected").val();
    $.post(
                    	    "Get_Chart_Data.aspx",
                    	    {
                    	        ddl_level2: par
                    	    },
                        function (data) {
                            var array = new Array();
                            array = data.split('|');
                            $("#ddl_level3").empty();
                            if (par == "T_BASE_PARAID_SUN") {
                                $("#ddl_level4").empty();
                            }
                            if (data == "") {
                                $("#ddl_level3").append("<option value=请选择>-请选择-</option>");
                                if (par == "T_BASE_PARAID_SUN") {
                                    $("#ddl_level4").append("<option value=请选择>-请选择-</option>");
                                }
                            }
                            else {
                                $("#ddl_level3").append(array[0]);
                                if (par == "T_BASE_PARAID_SUN") {
                                    $("#ddl_level4").append(array[1]);
                                }
                            }
                        },

                        "html");
}
function change_level3() {
    GetChecked(4);
    $("#div_point_name").html('');
    var par = $("#sec_type").find("option:selected").val() + "," + $("#ddl_level1").find("option:selected").val() + "," + $("#ddl_level2").find("option:selected").val() + "," + $("#ddl_level3").find("option:selected").val();
    $.post(
                    	    "Get_Chart_Data.aspx",
                    	    {
                    	        ddl_level3: par
                    	    },
                        function (data) {
                            var array = new Array();
                            array = data.split('|');
                            if (par == "T_BASE_PARAID_SUN") {
                                $("#ddl_level4").empty();
                            }
                            if (data == "") {
                                if (par == "T_BASE_PARAID_SUN") {
                                    $("#ddl_level4").append("<option value=请选择>-请选择-</option>");
                                }
                            }
                            else {
                                if (par == "T_BASE_PARAID_SUN") {
                                    $("#ddl_level4").append(array[0]);
                                }
                            }
                        },

                        "html");
}
function query() {
    GetChecked(4);
    var point_name = $("#txt_point_name").val();
    //if (point_name != "") {
    var par = "";


    par = point_name + "," + $("#sec_type").find("option:selected").val() + "," + $("#ddl_level1").find("option:selected").val() + "," + $("#ddl_level2").find("option:selected").val() + "," + $("#ddl_level3").find("option:selected").val();

    $.post(
                    	    "Get_Chart_Data.aspx",
                    	    {
                    	        point_name: par + "-" + array_total
                    	    },
                        function (data) {
                            var array = new Array();
                            array = data.split(';');
                            var checkboxs = '';
                            if (data != "") {
                                $("#div_point_name").html('');
                                $("#div_point_name").html(data);
                                //                                for (var i = 0; i < array_total.length; i++) {

                                //                                    for (var j = 0; j < $("#div_point_name :checkbox").length; j++) {
                                //                                        if ($("#div_point_name :checkbox")[j].value == array_total[i].split('|')[0]) {
                                //                                            $("#div_point_name :checkbox")[j].checked = true;
                                //                                        }
                                //                                    }
                                //                                }

                            }
                            else {
                                $("#div_point_name").html('');
                                alert("没有查询到任何测点！");
                                return false;
                            }

                        },
                        "html");
    //    }
    //    else {
    //        alert("请输入节点！");
    //        $("#div_point_name").html('');
    //        return false;
    //    }
}
function sure_query() {

    GetChecked();
    var rating_time = "";
    if (array_total.length != 0) {
        if (($("#stime").val() != "") && ($("#etime").val() != "")) {
            rating_time = $("#stime").val() + "," + $("#etime").val();
        }
        //alert($("#hf_value").val());
        //$("#hf_value_his").attr("value", $("#hf_value").val());
        //$("#hf_value").attr("value", "");
        //alert($("#hf_value_his").val());
        $.post("ChartManage.aspx", { rating: $("#hf_value").val(), rating_time: rating_time }, function (data) {
            getLine(data);
        }, 'json');

        $('#add').dialog('close');
        //            $.unblockUI();
    }
    else {
        alert("请选择测点!");
    }
}

function sure_update() {
    GetChecked(3);
    $("#hf_value").attr("value", '');
    for (var i = 0; i < $("#div_value_edit :checkbox:checked").length; i++) {
        if ($("#hf_value").val() != "") {
            $("#hf_value").attr("value", $("#hf_value").val() + "," + $("#div_value_edit :checkbox:checked")[i].value + "|" + $("#div_value_edit :checkbox:checked")[i].name);
        }
        else {
            $("#hf_value").attr("value", $("#div_value_edit :checkbox:checked")[i].value + "|" + $("#div_value_edit :checkbox:checked")[i].name)
        }
    }
    if ($("#hf_value").val() != "") {
        var rating_time = "";
        if (($("#stime").val() != "") && ($("#etime").val() != "")) {
            rating_time = $("#stime").val() + "," + $("#etime").val();
        }
        $.post("ChartManage.aspx", { rating: $("#hf_value").val(), rating_time: rating_time }, function (data) {
            getLine(data);
        }, 'json');
    }
    else {
        getLine(null);
    }
    $('#edit').dialog('close');
}

function sure_preserve() {
    if ($("#txt_chart_name").val() != "") {
        var vars = new Array(), hash;

        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1].split('#')[0];
        }

        $.post("ChartManage.aspx", { chart_name: "2&" + $("#txt_chart_name").val() + "&" + vars["user_id"] + "&" + $("#hf_value").val() }, function (data) {

        }, 'json');
        $('#pre').dialog('close');
        alert("保存成功！");
    }
    else {
        alert("请输入模板名称！");
    }
}

var array_total = new Array();
function GetChecked(b) {
    if (b != 2) {
        var point_name = "";
        if (b == 3) {
            point_name = $("#div_value_edit :checkbox");
        }
        else {
            point_name = $("#div_point_name :checkbox");
        }

        for (var i = 0; i < point_name.length; i++) {
            if (point_name[i].checked == true) {
                var num = 0;
                $.each(array_total, function (key, val) {

                    if ((val != undefined) && (point_name[i].value == val.split('|')[0])) {
                        num++;
                        //array.push(point_name[i].value);
                    }
                });
                if (num == 0) {
                    array_total.push(point_name[i].value + "|" + point_name[i].name);
                }
            }
            else {
                $.each(array_total, function (key, val) {
                    if ((val != undefined) && (point_name[i].value == val.split('|')[0])) {
                        delete array_total[key];
                    }
                });
            }
        }
        array_total.sort();
        var a = 0;
        $.each(array_total, function (key, val) {
            if (val != undefined) {
                a++;
            }
        });
        array_total.length = a;
        array_total.join(";");
        $("#hf_value").attr("value", array_total);

        if (((b == 2) || (b == 4)) && (array_total.length > 0)) {
            var count = 0, str_data = "<table><tr>";
            //sb.Append("<table><tr>");
            for (var j = 0; j < array_total.length; j++) {
                count++;
                if (count % 2 == 0) {
                    str_data += "<td width=\"300px\"><label>" + array_total[j].split('|')[1] + "</label>&nbsp;&nbsp;&nbsp;<br></td></tr>";
                }
                else {
                    str_data += "<td width=\"300px\"><label>" + array_total[j].split('|')[1] + "&nbsp;&nbsp;</td>";
                }
                if ((count % 2 != 0) && (j == array_total.length - 1)) {
                    str_data += "</tr>";
                }
            }
            str_data += "</table>";
            $("#point_choose").html(str_data);
        }
        else {
            $("#point_choose").html('');
        }
    }
}


function getLine(list) {
    var highchartsOptions = Highcharts.setOptions(Highcharts.theme);
    if (list != null) {
        chart = new Highcharts.Chart({
            chart: {
                renderTo: 'container',
                type: 'spline',
                zoomType: 'x'
            },
            title: {
                text: list.title
            },
            exporting: {
                enabled: false //用来设置是否显示‘打印’,'导出'等功能按钮，不设置时默认为显示 
            },
            xAxis: {
                type: 'datetime',
                labels: { formatter: function () {
                    return Highcharts.dateFormat('%H:%M:%S', this.value);
                }
                }
            },
            colors: ['#058DC7', '#50B432', '#ED561B', '#DDDF00', '#24CBE5', '#64E572', '#FF9655', '#FFF263', '#6AF9C4'],
            yAxis: list.y_data,
            tooltip: {
                xDateFormat: '<b>' + '%Y-%m-%d %H:%M:%S' + '</b>',
                crosshairs: {

                    width: 2,
                    color: 'red'
                },
                shared: true
            },
            plotOptions: {
                spline: {
                    lineWidth: 0.4,
                    states: {
                        hover: {
                            lineWidth: 0.5
                        }
                    },
                    marker: {
                        enabled: false
                    }
                }
            },
            series: list.list
        });
    }
    else {
        chart = new Highcharts.Chart({
            chart: {
                renderTo: 'container',
                type: 'spline'
            },
            title: {
                text: '趋势呈现数据图'
            },
            xAxis: {
                type: 'datetime'
            },
            exporting: {
                enabled: false //用来设置是否显示‘打印’,'导出'等功能按钮，不设置时默认为显示 
            },
            yAxis: {
                title: {
                    text: ''
                },
                min: 0
            }
        });
    }
}

function formatDate(now) {
    var year = now.getYear();
    var month = now.getMonth() + 1;
    var date = now.getDate();
    var hour = now.getHours();
    var minute = now.getMinutes();
    var second = now.getSeconds();
    return year + "-" + month + "-" + date + "   " + hour + ":" + minute + ":" + second;
}
