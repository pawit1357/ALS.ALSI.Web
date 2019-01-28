<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="ALS.ALSI.Web.view.dashboard.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>
    <script src="https://code.highcharts.com/modules/export-data.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <form id="Form1" method="post" runat="server" class="form-horizontal">

        <div class="portlet light bordered">
            <div class="portlet-title">
                <div class="caption">
                    <i class="icon-graph"></i>
                    <span class="caption-subject font-blue bold uppercase">Report DashBoard</span>
                    <span class="caption-helper"></span>
                </div>
                <div class="tools">
                </div>
            </div>
            <div class="portlet-body form">
                <div class="form-body">
                    <div class="row">
                        <!-- RPT1 -->
                        <div class="col-lg-6 col-xs-12 col-sm-12">
                            <div class="portlet light ">

                                <div class="portlet-title">
                                    <div class="actions"></div>
                                </div>
                                <div class="portlet-body">
                                    <div id="container" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
                                </div>
                            </div>
                        </div>
                        <!-- RPT2 -->
                        <div class="col-lg-6 col-xs-12 col-sm-12">
                            <div class="portlet light ">
                                <div class="portlet-title">
                                    <div class="actions"></div>
                                </div>
                                <div class="portlet-body">
                                    <div id="container2" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <!-- RPT3 -->
                        <br />
                        <br />       <br />
                        <br />       <br />
                        <br />
                        xx
                        <div class="col-lg-6 col-xs-12 col-sm-12">
                            <div class="portlet light bordered">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="icon-chemistry font-green"></i>
                                        <span class="caption-subject font-green bold uppercase">Turn Around Time (TAT)</span>
                                    </div>
                                    <div class="actions">
  
                                    </div>
                                </div>
                                <div class="portlet-body">
                                    <div class="table-scrollable">
                                        <asp:GridView ID="gvRpt3" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                            CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="company_id" OnPageIndexChanging="gvJob_PageIndexChanging" OnRowDataBound="gvRpt3_RowDataBound" PageSize="5">
                                            <Columns>
                                                <asp:BoundField HeaderText="Company Name" DataField="company_name" ItemStyle-HorizontalAlign="Left" SortExpression="company_name" />
                                                <asp:BoundField HeaderText="Invoice" DataField="sample_invoice" ItemStyle-HorizontalAlign="Left" SortExpression="sample_invoice" />
                                                <asp:BoundField HeaderText="Overdue Date" DataField="overdue_date" ItemStyle-HorizontalAlign="Left" SortExpression="overdue_date" />
                                                <asp:BoundField HeaderText="Balance" DataField="outstanding_balance" ItemStyle-HorizontalAlign="Left" SortExpression="outstanding_balance" />
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />

                                            <EmptyDataTemplate>
                                                <div class="data-not-found">
                                                    <asp:Literal ID="libDataNotFound" runat="server" Text="Data Not found" />
                                                </div>
                                            </EmptyDataTemplate>
                                        </asp:GridView>

                                    </div>
                                </div>
                            </div>

                        </div>
                        <!-- RPT4 -->
                        <div class="col-lg-6 col-xs-12 col-sm-12">
                            <div class="portlet light ">
                                <div class="portlet-title">
                                    <div class="actions"></div>
                                </div>
                                <div class="portlet-body">
                                    <div id="container4" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
                                </div>
                            </div>
                        </div>

                    </div>



                </div>
            </div>
        </div>

    </form>
    <script>
        /* -report1:Revenue-Actual- */
        Highcharts.chart('container', {
            chart: {
                type: 'column'
            },
            title: {
                text: 'Revenue-Actual'
            },
            subtitle: {
                text: 'Source: ALS'
            },
            xAxis: {
                categories: [
                    'Jan',
                    'Feb',
                    'Mar',
                    'Apr',
                    'May',
                    'Jun',
                    'Jul',
                    'Aug',
                    'Sep',
                    'Oct',
                    'Nov',
                    'Dec'
                ],
                crosshair: true
            },
            yAxis: {
                min: 0,
                title: {
                    text: 'Amout(Bath)'
                }
            },
            tooltip: {
                headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                    '<td style="padding:0"><b>{point.y:.1f} บาท</b></td></tr>',
                footerFormat: '</table>',
                shared: true,
                useHTML: true
            },
            plotOptions: {
                column: {
                    pointPadding: 0.2,
                    borderWidth: 0
                }
            },
            series: <%= jsonSeriesRpt01 %>
        });

        /* -report2:Daily invoice vs work inprocess- */
        Highcharts.chart('container2', {
            chart: {
                type: 'areaspline'
            },
            title: {
                text: 'Daily invoice vs work inprocess'
            },
            subtitle: {
                text: 'Source: ALS'
            },
            xAxis: {
                type: 'datetime',
                dateTimeLabelFormats: { // don't display the dummy year
                    day: "%A, %b %e, %Y"

                },
                title: {
                    text: 'Date'
                }
            },
            yAxis: {
                title: {
                    text: 'Record (s)'
                },
                min: 0
            },
            tooltip: {
                headerFormat: '<b>{series.name}</b><br>',
                pointFormat: '{point.x:%e. %b}: {point.y:.0f} record(s)'
            },

            plotOptions: {
                spline: {
                    marker: {
                        enabled: true
                    }
                }
            },

            colors: ['#e77378', '#d3d3d'],

            // Define the data points. All series have a dummy year
            // of 1970/71 in order to be compared on the same x axis. Note
            // that in JavaScript, months start at 0 for January, 1 for February etc.
            series: <%= jsonSeriesRpt02 %>
               
        });


        /* -report4:Forecast And Budget- */

        Highcharts.chart('container4', {
            chart: {
                type: 'line'
            },
            title: {
                text: 'Forecast And Budget'
            },
            subtitle: {
                text: 'Source: ALS'
            },
            xAxis: {
                type: 'datetime',
                dateTimeLabelFormats: { // don't display the dummy year
                    day: "%A, %b %e, %Y"

                },
                title: {
                    text: 'Date'
                }
            },
            yAxis: {
                title: {
                    text: 'Baht'
                },
                min: 0
            },
            tooltip: {
                headerFormat: '<b>{series.name}</b><br>',
                pointFormat: '{point.x:%e. %b}: {point.y:.0f} Baht'
            },

            plotOptions: {
                spline: {
                    marker: {
                        enabled: true
                    }
                }
            },

            colors: ['#2f7ed8', '#0d233a', '#8bbc21', '#910000'],

            // Define the data points. All series have a dummy year
            // of 1970/71 in order to be compared on the same x axis. Note
            // that in JavaScript, months start at 0 for January, 1 for February etc.
            series: <%= jsonSeriesRpt04 %>

        });

        /*
        Highcharts.chart('container4', {

            title: {
                text: 'Solar Employment Growth by Sector, 2010-2016'
            },

            subtitle: {
                text: 'Source: thesolarfoundation.com'
            },

            yAxis: {
                title: {
                    text: 'Number of Employees'
                }
            },
            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'middle'
            },

            plotOptions: {
                series: {
                    label: {
                        connectorAllowed: false
                    },
                    pointStart: 2010
                }
            },

            series: ,

            responsive: {
                rules: [{
                    condition: {
                        maxWidth: 500
                    },
                    chartOptions: {
                        legend: {
                            layout: 'horizontal',
                            align: 'center',
                            verticalAlign: 'bottom'
                        }
                    }
                }]
            }

        });
        */
    </script>
</asp:Content>
