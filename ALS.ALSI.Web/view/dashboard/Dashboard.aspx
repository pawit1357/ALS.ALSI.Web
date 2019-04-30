﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="ALS.ALSI.Web.view.dashboard.Dashboard" %>

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
                            <div class="portlet light bordered">
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
                            <div class="portlet light bordered">
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
                        <div class="col-lg-6 col-xs-12 col-sm-12">
                            <div class="portlet light bordered">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="icon-chemistry font-green"></i>
                                        <span class="caption-subject font-green bold uppercase">รายงานยอดเงินลูกค้ารอชำระ/จำวนวันค้าง</span>
                                    </div>
                                    <div class="actions">
                                    </div>
                                </div>
                                <div class="portlet-body">
                                        <div style="width:100%;overflow-x: auto;white-space: nowrap;">
                                            <asp:GridView ID="gvRpt3" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                                CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="customer_id" OnPageIndexChanging="gvJob_PageIndexChanging" OnRowDataBound="gvRpt3_RowDataBound" PageSize="5">
                                                <Columns>
                                                    <asp:BoundField ItemStyle-Width="300" HeaderText="Company Name" DataField="company_name" ItemStyle-HorizontalAlign="Left" SortExpression="company_name" />
                                                    <asp:BoundField ItemStyle-Width="130" HeaderText="Job Number" DataField="job_number" ItemStyle-HorizontalAlign="Left" SortExpression="job_number" />
                                                    <asp:BoundField ItemStyle-Width="120" HeaderText="Invoice" DataField="sample_invoice" ItemStyle-HorizontalAlign="Left" SortExpression="sample_invoice" />
                                                    <asp:BoundField ItemStyle-Width="150" HeaderText="Invoice Date" DataField="sample_invoice_date" ItemStyle-HorizontalAlign="Left" SortExpression="sample_invoice_date" DataFormatString="{0:d MMM yyyy}" />
                                                    <asp:BoundField ItemStyle-Width="80" HeaderText="Overdue Date" DataField="overdue_date" ItemStyle-HorizontalAlign="Left" SortExpression="overdue_date" />
                                                    <asp:BoundField ItemStyle-Width="100" HeaderText="Balance" DataField="sample_invoice_amount" ItemStyle-HorizontalAlign="Left" SortExpression="sample_invoice_amount" />
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
                                    <div id="container5" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
                                </div>
                            </div>
                        </div>
                        
                    </div>
                    <div class="row">
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
            series: <%= jsonSeriesRpt04 %>

        });

        Highcharts.chart('container5', {
            chart: {
                plotBackgroundColor: null,
                plotBorderWidth: null,
                plotShadow: false,
                type: 'pie'
            },
            title: {
                text: 'รายงานยอดเงินลูกค้ารอชำระ'
            },
            tooltip: {
                pointFormat: '<b>{point.name}</b>:{point.y}</b> {point.percentage:.1f} %'
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',
                    dataLabels: {
                        enabled: true,
                        format: '<b>{point.name}</b>:{point.y}</b> {point.percentage:.1f} %',
                        style: {
                            color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                        }
                    }
                }
            },
            series: <%= jsonSeriesRpt031 %>

        });
    </script>
</asp:Content>
