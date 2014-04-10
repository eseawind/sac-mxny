<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SACSIS.Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>首页</title>
    <style type="text/css">
        body
        {
            height: 100%;
            overflow: auto;
            margin: 0px;
            padding: 0px;
            background-color: #ffffff;
        }
        .div_1
        {
            background-image: url(img/HomePage_1.jpg);
            height: 110px;
            width: 268px;
            display: block;
            background-repeat: no-repeat;
            background-position: center;
            line-height: 110px;
        }
        .div_2
        {
            background-image: url(img/HomePage_2.jpg);
            height: 110px;
            width: 268px;
            display: block;
            background-repeat: no-repeat;
            background-position: center;
            line-height: 110px;
        }
        .div_3
        {
            background-image: url(img/HomePage_3.jpg);
            height: 110px;
            width: 268px;
            display: block;
            background-repeat: no-repeat;
            background-position: center;
            line-height: 110px;
        }
        .div_4
        {
            background-image: url(img/HomePage_4.jpg);
            height: 110px;
            width: 268px;
            display: block;
            background-repeat: no-repeat;
            background-position: center;
            line-height: 110px;
        }
        .div_5
        {
            background-image: url(img/HomePage_5.jpg);
            height: 110px;
            width: 268px;
            display: block;
            background-repeat: no-repeat;
            background-position: center;
            line-height: 110px;
        }
        .div_top_1
        {
            color: #000000;
            font-size: 14px;
            font: "宋体";
            position: absolute;
            width: 140px;
            height: 17px;
        }
        .text_1
        {
            color: #000000;
            font-size: 30px;
            font: "Arial" , Gadget, sans-serif;
            width: 140px;
            height: 17px;
            text-align: center;
        }
        .td1
        {
            background: url(img/HomePage_bg_2.jpg);
            background-repeat: repeat-x;
            font-size: 12px;
            text-align: center;
            height: 24px;
            line-height: 24px;
        }
        .td2
        {
            background-color: #ffffff;
            font-size: 12px;
            text-align: center;
            height: 24px;
            line-height: 24px;
        }
        .td3
        {
            background-color: #efeeff;
            font-size: 12px;
            text-align: center;
            height: 24px;
            line-height: 24px;
        }
    </style>
    <link href="../Js/jQueryEasyUI/themes/gray/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Js/jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Js/jQueryEasyUI/css/djxt.css" rel="stylesheet" type="text/css" />
    <script src="../Js/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../Js/jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="HighChart/highcharts.js" type="text/javascript"></script>
    <script type="text/javascript">
        var list;
        $(function () {
            Init();
            initChart();
            //            setTimeout('initChart()', 1000 * 5); //指定5秒刷新一次 
        });

        function Init() {
            $.post("Index.aspx", { param: 'initTop' }, function (data) {
                $("#sp_zrl").html(data.rl);
                $("#sp_power").html(data.power);
                $("#sp_day").html(data.day);
                $("#sp_month").html(data.month);
                $("#sp_year").html(data.year);
            }, 'json');

            $.post("Index.aspx", { param: 'table' }, function (data) {
                $("#dv_table").html(data.tbl);
            }, 'json');

            $.post("Index.aspx", { param: 'line' }, function (data) {
                list = data.list;
            }, 'json');

        }

        var initChart = function () {
            var dataJsonQS;
            $.ajax({
                url: "../WebService/Line.asmx/SetLine",
                contentType: "application/json; charset=utf-8",
                type: "POST",
                dataType: "json",
                //                data: "{'id':'" + id + "'}",
                beforeSend: function () {
                },
                success: function (json) {
                    list = list;
                    dataJsonQS = $.parseJSON(json.d);
                    if (list.split(',').length == 1) {
                        SetChartData1();
                    } else if (list.split(',').length == 2) {
                        SetChartData2();
                    } else if (list.split(',').length == 3) {
                        SetChartData3();
                    } else if (list.split(',').length == 4) {
                        SetChartData4();
                    } else if (list.split(',').length == 5) {
                        SetChartData5();
                    } else if (list.split(',').length == 6) {
                        SetChartData6();
                    }
                    //                    SetChartData();
                },
                error: function (x, e) {
                    //alert(x.responseText);
                },
                complete: function () {
                }
            });
            var SetChartData1 = function () {
                var chart = new Highcharts.Chart({
                    chart: {
                        renderTo: 'dv_line',
                        type: 'spline'
                    },
                    credits: {
                        enabled: false
                    },
                    title: {
                        text: '',
                        style: {
                            fontFamily: '"微软雅黑"',
                            fontSize: '14pt'
                        }
                    },
                    subtitle: {
                        enable: false
                    },
                    xAxis: {
                        type: 'datetime',
                        dateTimeLabelFormats: {
                            day: '00:00'
                        },
                        maxZoom: 2 * 3600 * 1000,
                        showFirstLabel: true,
                        showLastLabel: true,
                        tickWidth: 0,
                        gridLineWidth: 0.1
                    },
                    yAxis: {
                        title: {
                            text: ''
                        },
                        labels: {
                            formatter: function () {
                                return this.value;
                            }
                        }
                    },
                    //global: { useUTC: true },
                    legend: {
                        enabled: false
                    },
                    tooltip: {
                        crosshairs: {
                            width: 1,
                            color: 'red'
                        },
                        shared: true,
                        xDateFormat: '<b>时间：%H:%M:%S</b>'
                    },
                    plotOptions: {
                        spline: {
                            lineWidth: 0.6,
                            marker: {
                                enabled: false,
                                radius: 0.6,
                                lineColor: 'green'
                            },
                            states: {
                                hover: {
                                    lineWidth: 0.6
                                }
                            }
                        }
                    },
                    series: [{
                        name: list.split(',')[0],
                        lineWidth: 0.6,
                        marker: {
                            symbol: 'circle'
                        },
                        color: 'blue',
                        data: dataJsonQS._strLine1,
                        //pointStart: Date.UTC(2012, 3, 26),
                        pointInterval: 60 * 10 * 1000 //3min
                    }
                    ],
                    exporting: {
                        enabled: false
                    }
                });
            };

            var SetChartData2 = function () {
                var chart = new Highcharts.Chart({
                    chart: {
                        renderTo: 'dv_line',
                        type: 'spline'
                    },
                    credits: {
                        enabled: false
                    },
                    title: {
                        text: '',
                        style: {
                            fontFamily: '"微软雅黑"',
                            fontSize: '14pt'
                        }
                    },
                    subtitle: {
                        enable: false
                    },
                    xAxis: {
                        type: 'datetime',
                        dateTimeLabelFormats: {
                            day: '00:00'
                        },
                        maxZoom: 2 * 3600 * 1000,
                        showFirstLabel: true,
                        showLastLabel: true,
                        tickWidth: 0,
                        gridLineWidth: 0.1
                    },
                    yAxis: {
                        title: {
                            text: ''
                        },
                        labels: {
                            formatter: function () {
                                return this.value;
                            }
                        }
                    },
                    //global: { useUTC: true },
                    legend: {
                        enabled: false
                    },
                    tooltip: {
                        crosshairs: {
                            width: 1,
                            color: 'red'
                        },
                        shared: true,
                        xDateFormat: '<b>时间：%H:%M:%S</b>'
                    },
                    plotOptions: {
                        spline: {
                            lineWidth: 0.6,
                            marker: {
                                enabled: false,
                                radius: 0.6,
                                lineColor: 'green'
                            },
                            states: {
                                hover: {
                                    lineWidth: 0.6
                                }
                            }
                        }
                    },
                    series: [{
                        name: list.split(',')[0],
                        lineWidth: 0.6,
                        marker: {
                            symbol: 'circle'
                        },
                        color: 'blue',
                        data: dataJsonQS._strLine1,
                        //pointStart: Date.UTC(2012, 3, 26),
                        pointInterval: 60 * 10 * 1000 //3min
                    }
                    , {
                        name: list.split(',')[1],
                        lineWidth: 0.6,
                        marker: {
                            symbol: 'circle'
                        },
                        color: 'green',
                        data: dataJsonQS._strLine2,
                        //pointStart: Date.UTC(2012, 3, 26),
                        pointInterval: 60 * 10 * 1000//180 * 1000 //3min
                    }
                    ],
                    exporting: {
                        enabled: false
                    }
                });
            };

            var SetChartData3 = function () {
                var chart = new Highcharts.Chart({
                    chart: {
                        renderTo: 'dv_line',
                        type: 'spline'
                    },
                    credits: {
                        enabled: false
                    },
                    title: {
                        text: '',
                        style: {
                            fontFamily: '"微软雅黑"',
                            fontSize: '14pt'
                        }
                    },
                    subtitle: {
                        enable: false
                    },
                    xAxis: {
                        type: 'datetime',
                        dateTimeLabelFormats: {
                            day: '00:00'
                        },
                        maxZoom: 2 * 3600 * 1000,
                        showFirstLabel: true,
                        showLastLabel: true,
                        tickWidth: 0,
                        gridLineWidth: 0.1
                    },
                    yAxis: {
                        title: {
                            text: ''
                        },
                        labels: {
                            formatter: function () {
                                return this.value;
                            }
                        }
                    },
                    //global: { useUTC: true },
                    legend: {
                        enabled: false
                    },
                    tooltip: {
                        crosshairs: {
                            width: 1,
                            color: 'red'
                        },
                        shared: true,
                        xDateFormat: '<b>时间：%H:%M:%S</b>'
                    },
                    plotOptions: {
                        spline: {
                            lineWidth: 0.6,
                            marker: {
                                enabled: false,
                                radius: 0.6,
                                lineColor: 'green'
                            },
                            states: {
                                hover: {
                                    lineWidth: 0.6
                                }
                            }
                        }
                    },
                    series: [{
                        name: list.split(',')[0],
                        lineWidth: 0.6,
                        marker: {
                            symbol: 'circle'
                        },
                        color: 'blue',
                        data: dataJsonQS._strLine1,
                        //pointStart: Date.UTC(2012, 3, 26),
                        pointInterval: 60 * 10 * 1000 //3min
                    }
                    , {
                        name: list.split(',')[1],
                        lineWidth: 0.6,
                        marker: {
                            symbol: 'circle'
                        },
                        color: 'green',
                        data: dataJsonQS._strLine2,
                        //pointStart: Date.UTC(2012, 3, 26),
                        pointInterval: 60 * 10 * 1000//180 * 1000 //3min
                    }, {
                        name: list.split(',')[2],
                        lineWidth: 0.6,
                        marker: {
                            symbol: 'circle'
                        },
                        color: 'yellow',
                        data: dataJsonQS._strLine3,
                        //pointStart: Date.UTC(2012, 3, 26),
                        pointInterval: 60 * 10 * 1000//180 * 1000 //3min
                    }
                    ],
                    exporting: {
                        enabled: false
                    }
                });
            };

            var SetChartData4 = function () {
                var chart = new Highcharts.Chart({
                    chart: {
                        renderTo: 'dv_line',
                        type: 'spline'
                    },
                    credits: {
                        enabled: false
                    },
                    title: {
                        text: '',
                        style: {
                            fontFamily: '"微软雅黑"',
                            fontSize: '14pt'
                        }
                    },
                    subtitle: {
                        enable: false
                    },
                    xAxis: {
                        type: 'datetime',
                        dateTimeLabelFormats: {
                            day: '00:00'
                        },
                        maxZoom: 2 * 3600 * 1000,
                        showFirstLabel: true,
                        showLastLabel: true,
                        tickWidth: 0,
                        gridLineWidth: 0.1
                    },
                    yAxis: {
                        title: {
                            text: ''
                        },
                        labels: {
                            formatter: function () {
                                return this.value;
                            }
                        }
                    },
                    //global: { useUTC: true },
                    legend: {
                        enabled: false
                    },
                    tooltip: {
                        crosshairs: {
                            width: 1,
                            color: 'red'
                        },
                        shared: true,
                        xDateFormat: '<b>时间：%H:%M:%S</b>'
                    },
                    plotOptions: {
                        spline: {
                            lineWidth: 0.6,
                            marker: {
                                enabled: false,
                                radius: 0.6,
                                lineColor: 'green'
                            },
                            states: {
                                hover: {
                                    lineWidth: 0.6
                                }
                            }
                        }
                    },
                    series: [{
                        name: list.split(',')[0],
                        lineWidth: 0.6,
                        marker: {
                            symbol: 'circle'
                        },
                        color: 'blue',
                        data: dataJsonQS._strLine1,
                        //pointStart: Date.UTC(2012, 3, 26),
                        pointInterval: 60 * 10 * 1000 //3min
                    }
                    , {
                        name: list.split(',')[1],
                        lineWidth: 0.6,
                        marker: {
                            symbol: 'circle'
                        },
                        color: 'green',
                        data: dataJsonQS._strLine2,
                        //pointStart: Date.UTC(2012, 3, 26),
                        pointInterval: 60 * 10 * 1000//180 * 1000 //3min
                    }, {
                        name: list.split(',')[2],
                        lineWidth: 0.6,
                        marker: {
                            symbol: 'circle'
                        },
                        color: 'yellow',
                        data: dataJsonQS._strLine3,
                        //pointStart: Date.UTC(2012, 3, 26),
                        pointInterval: 60 * 10 * 1000//180 * 1000 //3min
                    }, {
                        name: list.split(',')[3],
                        lineWidth: 0.6,
                        marker: {
                            symbol: 'circle'
                        },
                        color: 'Aqua',
                        data: dataJsonQS._strLine4,
                        //pointStart: Date.UTC(2012, 3, 26),
                        pointInterval: 60 * 10 * 1000//180 * 1000 //3min
                    }
                    ],
                    exporting: {
                        enabled: false
                    }
                });
            };

            var SetChartData5 = function () {
                var chart = new Highcharts.Chart({
                    chart: {
                        renderTo: 'dv_line',
                        type: 'spline'
                    },
                    credits: {
                        enabled: false
                    },
                    title: {
                        text: '',
                        style: {
                            fontFamily: '"微软雅黑"',
                            fontSize: '14pt'
                        }
                    },
                    subtitle: {
                        enable: false
                    },
                    xAxis: {
                        type: 'datetime',
                        dateTimeLabelFormats: {
                            day: '00:00'
                        },
                        maxZoom: 2 * 3600 * 1000,
                        showFirstLabel: true,
                        showLastLabel: true,
                        tickWidth: 0,
                        gridLineWidth: 0.1
                    },
                    yAxis: {
                        title: {
                            text: ''
                        },
                        labels: {
                            formatter: function () {
                                return this.value;
                            }
                        }
                    },
                    //global: { useUTC: true },
                    legend: {
                        enabled: false
                    },
                    tooltip: {
                        crosshairs: {
                            width: 1,
                            color: 'red'
                        },
                        shared: true,
                        xDateFormat: '<b>时间：%H:%M:%S</b>'
                    },
                    plotOptions: {
                        spline: {
                            lineWidth: 0.6,
                            marker: {
                                enabled: false,
                                radius: 0.6,
                                lineColor: 'green'
                            },
                            states: {
                                hover: {
                                    lineWidth: 0.6
                                }
                            }
                        }
                    },
                    series: [{
                        name: list.split(',')[0],
                        lineWidth: 0.6,
                        marker: {
                            symbol: 'circle'
                        },
                        color: 'blue',
                        data: dataJsonQS._strLine1,
                        //pointStart: Date.UTC(2012, 3, 26),
                        pointInterval: 60 * 10 * 1000 //3min
                    }
                    , {
                        name: list.split(',')[1],
                        lineWidth: 0.6,
                        marker: {
                            symbol: 'circle'
                        },
                        color: 'green',
                        data: dataJsonQS._strLine2,
                        //pointStart: Date.UTC(2012, 3, 26),
                        pointInterval: 60 * 10 * 1000//180 * 1000 //3min
                    }, {
                        name: list.split(',')[2],
                        lineWidth: 0.6,
                        marker: {
                            symbol: 'circle'
                        },
                        color: 'yellow',
                        data: dataJsonQS._strLine3,
                        //pointStart: Date.UTC(2012, 3, 26),
                        pointInterval: 60 * 10 * 1000//180 * 1000 //3min
                    }, {
                        name: list.split(',')[3],
                        lineWidth: 0.6,
                        marker: {
                            symbol: 'circle'
                        },
                        color: 'Aqua',
                        data: dataJsonQS._strLine4,
                        //pointStart: Date.UTC(2012, 3, 26),
                        pointInterval: 60 * 10 * 1000//180 * 1000 //3min
                    }, {
                        name: list.split(',')[4],
                        lineWidth: 0.6,
                        marker: {
                            symbol: 'circle'
                        },
                        color: 'Gray',
                        data: dataJsonQS._strLine5,
                        //pointStart: Date.UTC(2012, 3, 26),
                        pointInterval: 60 * 10 * 1000//180 * 1000 //3min
                    }
                    ],
                    exporting: {
                        enabled: false
                    }
                });
            };

            var SetChartData6 = function () {
                var chart = new Highcharts.Chart({
                    chart: {
                        renderTo: 'dv_line',
                        type: 'spline'
                    },
                    credits: {
                        enabled: false
                    },
                    title: {
                        text: '',
                        style: {
                            fontFamily: '"微软雅黑"',
                            fontSize: '14pt'
                        }
                    },
                    subtitle: {
                        enable: false
                    },
                    xAxis: {
                        type: 'datetime',
                        dateTimeLabelFormats: {
                            day: '00:00'
                        },
                        maxZoom: 2 * 3600 * 1000,
                        showFirstLabel: true,
                        showLastLabel: true,
                        tickWidth: 0,
                        gridLineWidth: 0.1
                    },
                    yAxis: {
                        title: {
                            text: ''
                        },
                        labels: {
                            formatter: function () {
                                return this.value;
                            }
                        }
                    },
                    //global: { useUTC: true },
                    legend: {
                        enabled: false
                    },
                    tooltip: {
                        crosshairs: {
                            width: 1,
                            color: 'red'
                        },
                        shared: true,
                        xDateFormat: '<b>时间：%H:%M:%S</b>'
                    },
                    plotOptions: {
                        spline: {
                            lineWidth: 0.6,
                            marker: {
                                enabled: false,
                                radius: 0.6,
                                lineColor: 'green'
                            },
                            states: {
                                hover: {
                                    lineWidth: 0.6
                                }
                            }
                        }
                    },
                    series: [{
                        name: list.split(',')[0],
                        lineWidth: 0.6,
                        marker: {
                            symbol: 'circle'
                        },
                        color: 'blue',
                        data: dataJsonQS._strLine1,
                        //pointStart: Date.UTC(2012, 3, 26),
                        pointInterval: 60 * 10 * 1000 //3min
                    }
                    , {
                        name: list.split(',')[1],
                        lineWidth: 0.6,
                        marker: {
                            symbol: 'circle'
                        },
                        color: 'green',
                        data: dataJsonQS._strLine2,
                        //pointStart: Date.UTC(2012, 3, 26),
                        pointInterval: 60 * 10 * 1000//180 * 1000 //3min
                    }, {
                        name: list.split(',')[2],
                        lineWidth: 0.6,
                        marker: {
                            symbol: 'circle'
                        },
                        color: 'yellow',
                        data: dataJsonQS._strLine3,
                        //pointStart: Date.UTC(2012, 3, 26),
                        pointInterval: 60 * 10 * 1000//180 * 1000 //3min
                    }, {
                        name: list.split(',')[3],
                        lineWidth: 0.6,
                        marker: {
                            symbol: 'circle'
                        },
                        color: 'yellow',
                        data: dataJsonQS._strLine4,
                        //pointStart: Date.UTC(2012, 3, 26),
                        pointInterval: 60 * 10 * 1000//180 * 1000 //3min
                    }, {
                        name: list.split(',')[4],
                        lineWidth: 0.6,
                        marker: {
                            symbol: 'circle'
                        },
                        color: 'blue',
                        data: dataJsonQS._strLine5,
                        //pointStart: Date.UTC(2012, 3, 26),
                        pointInterval: 60 * 10 * 1000//180 * 1000 //3min
                    }, {
                        name: list.split(',')[5],
                        lineWidth: 0.6,
                        marker: {
                            symbol: 'circle'
                        },
                        color: 'yellow',
                        data: dataJsonQS._strLine6,
                        //pointStart: Date.UTC(2012, 3, 26),
                        pointInterval: 60 * 10 * 1000//180 * 1000 //3min
                    }
                    ],
                    exporting: {
                        enabled: false
                    }
                });
            };
        }
    </script>
</head>
<body>
    <table height="100%" width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center" height="110">
                <div>
                    <table height="100%" width="100%" border="0" cellpadding="0" cellspacing="0" bordercolor="#000000">
                        <tr>
                            <td class="div_1" align="center" valign="middle">
                                <span id="sp_zrl" class="text_1"></span>
                            </td>
                            <td class="div_2" align="center" valign="middle">
                                <span id="sp_power" class="text_1"></span>
                            </td>
                            <td class="div_3" align="center" valign="middle">
                                <span id="sp_day" class="text_1"></span>
                            </td>
                            <td class="div_4" align="center" valign="middle">
                                <span id="sp_month" class="text_1"></span>
                            </td>
                            <td class="div_5" align="center" valign="middle">
                                <span id="sp_year" class="text_1"></span>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td align="left" valign="top" style="background-image: url(img/HomePage_bg_1.jpg);
                height: 31px; background-repeat: repeat-x;" height="31px">
                <img alt="" src="img/HomePage_6.jpg" />
            </td>
        </tr>
        <tr>
            <td>
                <div id="dv_line" style="width: 1320px; height: 350px; margin: 0 auto;">
                </div>
            </td>
        </tr>
        <tr>
            <td align="left" valign="top" style="background-image: url(img/HomePage_bg_1.jpg);
                height: 31px; background-repeat: repeat-x;" height="31px">
                <img alt="" src="img/HomePage_7.jpg" />
            </td>
        </tr>
        <tr>
            <td align="center" valign="middle">
                <div id="container1" style="width: 1320px; height: 126px; margin: 0 auto;">
                    <div id="dv_table">
                    </div>
                </div>
            </td>
        </tr>
    </table>
</body>
</html>
