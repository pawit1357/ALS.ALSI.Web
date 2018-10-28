<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SearchHolidayCalendar.aspx.cs" Inherits="ALS.ALSI.Web.view.template.SearchHolidayCalendar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <form action="#" class="form-horizontal" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hPrefix" Value="1" runat="server" />
                <div class="portlet light bordered">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="icon-equalizer font-red-sunglo"></i>
                            <span class="caption-subject font-red-sunglo bold uppercase">Search Condition</span>
                            <span class="caption-helper"></span>
                        </div>
                        <div class="tools">
                        </div>
                    </div>
                    <div class="portlet-body form">
                        <div class="form-body">
                            <!-- BEGIN FORM-->

                            <div class="row">

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label col-md-3">Year:</label>
                                        <div class="col-md-6">
                                            <div class="form-group" style="text-align: left">
                                                <asp:DropDownList ID="ddlCalYear" runat="server" class="select2_category form-control" DataTextField="year" DataValueField="year"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="row">
                                            <div class="col-md-offset-3 col-md-9">
                                                <asp:LinkButton ID="LinkButton1" runat="server" class="btn green" OnClick="btnSearch_Click"><i class="icon-search"></i> Search</asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton2" runat="server" class="btn default" OnClick="btnCancel_Click"><i class="icon-refresh"></i> Cancel</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                    </div>
                                </div>
                            </div>

                            <!-- END FORM-->
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <!-- BEGIN EXAMPLE TABLE PORTLET-->
                        <div class="portlet box blue-dark">
                            <div class="portlet-title">
                                <div class="caption">
                                    <i class="fa fa-cogs"></i>Search Result
                                </div>
                                <div class="actions">
                                    <asp:LinkButton ID="btnAdd" runat="server" class="btn btn-default btn-sm" OnClick="lbAdd_Click"><i class="icon-pencil"></i> Add</asp:LinkButton>
                                </div>
                            </div>
                            <div class="portlet-body">
                                <asp:Label ID="lbTotalRecords" runat="server" Text="" Visible="false"></asp:Label>

                                <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="DATE_HOLIDAYS" OnRowCommand="gvResult_RowCommand" OnRowDeleting="gvResult_RowDeleting" OnPageIndexChanging="gvResult_PageIndexChanging" AllowPaging="True" PageSize="50" OnRowDataBound="gvResult_RowDataBound"  >
                                    <Columns>
                                        <asp:BoundField HeaderText="วันที่" DataField="DATE_HOLIDAYS" ItemStyle-HorizontalAlign="Left" SortExpression="DATE_HOLIDAYS" DataFormatString="{0:dd-M-yyyy}" />
                                        <asp:BoundField HeaderText="ปี" DataField="YEAR_HOLIDAYS" ItemStyle-HorizontalAlign="Left" SortExpression="YEAR_HOLIDAYS" />
                                        <asp:BoundField HeaderText="รายละเอียด" DataField="DESCRIPTION_SUMMARY" ItemStyle-HorizontalAlign="Left" SortExpression="DESCRIPTION_SUMMARY" />

                                         <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDelete" runat="server" ToolTip="Delete" CommandName="Delete" CommandArgument='<%# Eval("DATE_HOLIDAYS")%>'  OnClientClick="return confirm('Are you sure you want to delete this item ?');"><i class="fa fa-trash" ></i></asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <!-- BEGIN PAGE LEVEL SCRIPTS -->
    <script src="<%= ResolveUrl("~/assets/global/plugins/jquery.min.js") %>" type="text/javascript"></script>
    <!-- END PAGE LEVEL SCRIPTS -->
    <script>
        jQuery(document).ready(function () {
            /*
            var table = $('#ContentPlaceHolder2_gvResult');
            // begin: third table
            table.dataTable({
                // set the initial value
                "pageLength": 50,
            });
            */
        });
    </script>
    <!-- END JAVASCRIPTS -->
</asp:Content>
