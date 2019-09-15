<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="ALS.ALSI.Web.view.dashboard.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>
    <script src="https://code.highcharts.com/modules/export-data.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <form id="Form1" method="post" runat="server" class="form-horizontal">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Scripts/AjaxControlToolkit/Bundle" />
            </Scripts>
        </asp:ScriptManager>

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
                    <!-- criteria -->

                    <!--
                  <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Fiscal Year:</label>
                                <div class="col-md-6">
                                    <div class="form-group" style="text-align: left">
                                        <asp:DropDownList ID="ddlPhysicalYear" runat="server" class="select2_category form-control" DataTextField="year" DataValueField="year"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    -->

                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Company:</label>
                                <div class="col-md-6">
                                    <div class="form-group" style="text-align: left">
                                        <asp:DropDownList ID="ddlCompany" runat="server" class="select2_category form-control" DataTextField="company_name" DataValueField="ID"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">BOI/NON-BOI:</label>
                                <div class="col-md-6">
                                    <div class="form-group" style="text-align: left">
                                        <asp:DropDownList ID="ddlBoiNonBoi" runat="server" class="select2_category form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Invoice date (From):</label>
                                <div class="col-md-6">
                                    <div class="form-group" style="text-align: left">
                                        <div class="input-group input-medium date date-picker" data-date="10/2012" data-date-format="dd/mm/yyyy" data-date-viewmode="years" data-date-minviewmode="months">
                                            <asp:TextBox ID="txtStartDate" runat="server" class="form-control"></asp:TextBox>
                                            <span class="input-group-btn">
                                                <button class="btn default" type="button"><i class="fa fa-calendar"></i></button>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Invoice date (To):</label>
                                <div class="col-md-6">
                                    <div class="form-group" style="text-align: left">
                                        <div class="input-group input-medium date date-picker" data-date="10/2012" data-date-format="dd/mm/yyyy" data-date-viewmode="years" data-date-minviewmode="months">
                                            <asp:TextBox ID="txtEndDate" runat="server" class="form-control"></asp:TextBox>
                                            <span class="input-group-btn">
                                                <button class="btn default" type="button"><i class="fa fa-calendar"></i></button>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3"></label>
                                <div class="col-md-6">
                                    <asp:Button ID="btnSearch" runat="server" class="btn green" Text="ค้นหา" OnClick="btnSearch_Click" />&nbsp;&nbsp;

                                </div>
                                <div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- end criteria -->

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
                                        <span class="caption-subject font-green bold uppercase">รายงาน Turn Around Time </span>
                                    </div>
                                    <div class="actions">
                                    </div>
                                </div>
                                <div class="portlet-body">
                                    <div style="width: 100%; overflow-x: auto; white-space: nowrap;">
                                        <asp:UpdatePanel ID="upnlpB" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:GridView ID="gvRpt3" runat="server" AutoGenerateColumns="False" AllowPaging="True" ShowFooter="true"
                                                    CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="job_number" OnPageIndexChanging="gvJob_PageIndexChanging" OnRowDataBound="gvRpt3_RowDataBound" OnRowCreated="gvRpt3_RowCreated" PageSize="5">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField ItemStyle-Width="300" HeaderText="Job No." DataField="job_number" ItemStyle-HorizontalAlign="Left" SortExpression="job_number" />
                                                        <asp:BoundField ItemStyle-Width="120" HeaderText="Job Type" DataField="jobType" ItemStyle-HorizontalAlign="Left" SortExpression="jobType" />
                                                        <asp:BoundField ItemStyle-Width="150" HeaderText="jobStatus" DataField="jobStatus" ItemStyle-HorizontalAlign="Left" SortExpression="jobStatus"/>
                                                        <asp:BoundField ItemStyle-Width="150" HeaderText="Date Of Receive" DataField="date_of_receive" ItemStyle-HorizontalAlign="Left" SortExpression="date_of_receive" DataFormatString="{0:d MMM yyyy}" />
                                                        <asp:BoundField ItemStyle-Width="150" HeaderText="Login" DataField="Login" ItemStyle-HorizontalAlign="Left" SortExpression="Login"/>
                                                        <asp:BoundField ItemStyle-Width="150" HeaderText="Chemist" DataField="Chemist" ItemStyle-HorizontalAlign="Left" SortExpression="Chemist"/>
                                                        <asp:BoundField ItemStyle-Width="150" HeaderText="SRChemist" DataField="SRChemist" ItemStyle-HorizontalAlign="Left" SortExpression="SRChemist"/>
                                                        <asp:BoundField ItemStyle-Width="150" HeaderText="AdminWord" DataField="AdminWord" ItemStyle-HorizontalAlign="Left" SortExpression="AdminWord"/>
                                                        <asp:BoundField ItemStyle-Width="150" HeaderText="LabManager" DataField="LabManager" ItemStyle-HorizontalAlign="Left" SortExpression="LabManager"/>
                                                        <asp:BoundField ItemStyle-Width="150" HeaderText="AdminPdf" DataField="AdminPdf" ItemStyle-HorizontalAlign="Left" SortExpression="AdminPdf"/>                                                        
                                                        <asp:BoundField ItemStyle-Width="150" HeaderText="AdminPdf" DataField="AdminPdf" ItemStyle-HorizontalAlign="Left" SortExpression="AdminPdf"/>
                                                        <asp:BoundField ItemStyle-Width="150" HeaderText="date_admin_sent_to_cus" DataField="date_admin_sent_to_cus" ItemStyle-HorizontalAlign="Left" SortExpression="date_admin_sent_to_cus" DataFormatString="{0:d MMM yyyy}" />

                                                    </Columns>
                                                    <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <%-- <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Right" />--%>
                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <EmptyDataTemplate>
                                                        <div class="data-not-found">
                                                            <asp:Literal ID="libDataNotFound" runat="server" Text="Data Not found" />
                                                        </div>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <!-- RPT4 -->
                        <!--
                        <div class="col-lg-6 col-xs-12 col-sm-12">
                            <div class="portlet light ">
                                <div class="portlet-title">
                                    <div class="actions"></div>
                                </div>
                                <div class="portlet-body">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="control-label col-md-6">ช่วงข้อมูลที่จะแสดง (จากมากไปหาน้อย):</label>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlPeriod" runat="server" class="select2_category form-control" Width="200px" OnSelectedIndexChanged="ddlPeriod_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="0,20"> 1-20</asp:ListItem>
                                                        <asp:ListItem Value="20,40">20-40</asp:ListItem>
                                                        <asp:ListItem Value="40,60">40-60</asp:ListItem>
                                                        <asp:ListItem Value="60,80">60-80</asp:ListItem>
                                                        <asp:ListItem Value="80,100">80-100</asp:ListItem>
                                                        <asp:ListItem Value="100,120">100-120</asp:ListItem>
                                                        <asp:ListItem Value="120,140">120-140</asp:ListItem>
                                                        <asp:ListItem Value="140,160">140-160</asp:ListItem>
                                                        <asp:ListItem Value="160,180">160-180</asp:ListItem>
                                                        <asp:ListItem Value="180,200">180-200</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="container5" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
                                </div>
                            </div>
                        </div>
                        -->
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


                            <div class="row">
                        <!-- RPT4 -->
                        <div class="col-lg-6 col-xs-12 col-sm-12">
                            <div class="portlet light ">
                                <div class="portlet-title">
                                    <div class="actions"></div>
                                </div>
                                <div class="portlet-body">
                                    <div id="container55" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
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
                type: 'line'
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
                },
                categories:  <%= jsonSeriesRpt02Categories %>

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
                },
                categories:  <%= jsonSeriesRpt04Categories %>

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
            //rangeSelector: {
            //    enabled: true,
            //    selected: 1
            //},
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
