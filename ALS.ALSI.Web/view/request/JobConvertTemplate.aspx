﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="JobConvertTemplate.aspx.cs" Inherits="ALS.ALSI.Web.view.request.JobConvertTemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <form id="Form1" method="post" runat="server" class="form-horizontal">
        <div class="row">
            <div class="col-md-12">
                <!-- BEGIN EXAMPLE TABLE PORTLET-->
                <div class="portlet box blue-dark">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="fa fa-cogs"></i>Sample Detail
                        </div>
                        <div class="actions">
                        </div>
                    </div>
                    <div class="portlet-body">
                        <!-- BEGIN FORM-->

                        <asp:GridView ID="gvJob" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="ID">
                            <Columns>
                                <%--<asp:BoundField HeaderText="Date Received." DataField="create_date" ItemStyle-HorizontalAlign="Left" SortExpression="create_date" DataFormatString="{0:dd-MM-yyyy}" />--%>
                                <asp:BoundField HeaderText="Ref No." DataField="job_number" ItemStyle-HorizontalAlign="Left" SortExpression="job_number" />
                                <%--<asp:BoundField HeaderText="Comp" DataField="customer" ItemStyle-HorizontalAlign="Left" SortExpression="customer" />--%>
                                <%--<asp:BoundField HeaderText="Contact" DataField="contract_person" ItemStyle-HorizontalAlign="Left" SortExpression="contract_person" />--%>
                                <%--<asp:BoundField HeaderText="S/N" DataField="sn" ItemStyle-HorizontalAlign="Left" SortExpression="sn" />--%>
                                <asp:BoundField HeaderText="Description" DataField="description" ItemStyle-HorizontalAlign="Left" SortExpression="description" />
                                <asp:BoundField HeaderText="Model" DataField="model" ItemStyle-HorizontalAlign="Left" SortExpression="model" />
                                <asp:BoundField HeaderText="Surface Area" DataField="surface_area" ItemStyle-HorizontalAlign="Left" SortExpression="surface_area" />
                                <%--<asp:BoundField HeaderText="Remarks" DataField="remarks" ItemStyle-HorizontalAlign="Left" SortExpression="remarks" />--%>
                                <asp:BoundField HeaderText="Specification" DataField="specification" ItemStyle-HorizontalAlign="Left" SortExpression="specification" />
                                <asp:BoundField HeaderText="Type of test" DataField="type_of_test" ItemStyle-HorizontalAlign="Left" SortExpression="type_of_test" />
                            </Columns>
                            <PagerTemplate>
                                <div class="pagination">
                                    <ul>
                                        <li>
                                            <asp:LinkButton ID="btnFirst" runat="server" CommandName="Page" CommandArgument="First"
                                                CausesValidation="false" ToolTip="First Page"><i class="icon-fast-backward"></i></asp:LinkButton>
                                        </li>
                                        <li>
                                            <asp:LinkButton ID="btnPrev" runat="server" CommandName="Page" CommandArgument="Prev"
                                                CausesValidation="false" ToolTip="Previous Page"><i class="icon-backward"></i> Prev</asp:LinkButton>
                                        </li>
                                        <asp:PlaceHolder ID="pHolderNumberPage" runat="server" />
                                        <li>
                                            <asp:LinkButton ID="btnNext" runat="server" CommandName="Page" CommandArgument="Next"
                                                CausesValidation="false" ToolTip="Next Page">Next <i class="icon-forward"></i></asp:LinkButton>
                                        </li>
                                        <li>
                                            <asp:LinkButton ID="btnLast" runat="server" CommandName="Page" CommandArgument="Last"
                                                CausesValidation="false" ToolTip="Last Page"><i class="icon-fast-forward"></i></asp:LinkButton>
                                        </li>
                                    </ul>
                                </div>
                            </PagerTemplate>
                            <EmptyDataTemplate>
                                <div class="data-not-found">
                                    <asp:Literal ID="libDataNotFound" runat="server" Text="Data Not found" />
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <!-- END FORM-->
                    </div>
                </div>
            </div>
        </div>

        <div class="portlet light bordered">
            <div class="portlet-title">
                <div class="caption">
                    <i class="icon-equalizer font-red-sunglo"></i>
                    <span class="caption-subject font-red-sunglo bold uppercase">Select template</span>
                    <span class="caption-helper"></span>
                </div>
                <div class="tools">
                    <%--<a href="#" class="collapse"></a>--%>
                </div>
            </div>
            <div class="portlet-body form">
                <div class="form-body">
                    <!-- BEGIN FORM-->
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Template:<span class="required">*</span></label>
                                <div class="col-md-9">
                                    <asp:DropDownList ID="ddlTemplate" runat="server" class="select2_category form-control" DataTextField="name" DataValueField="ID"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="form-actions">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnSave" runat="server" class="btn green" Text="Save" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnCancel" runat="server" class="cancel btn" Text="Cancel" OnClick="btnCancel_Click" />
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
    </form>

    <!-- BEGIN PAGE LEVEL SCRIPTS -->
    <script src="<%= ResolveUrl("~/assets/global/plugins/jquery.min.js") %>" type="text/javascript"></script>
    <!-- END PAGE LEVEL SCRIPTS -->
    <script>
        jQuery(document).ready(function () {

            $('.select2_category').select2({
                placeholder: "Select an option",
                allowClear: true
            });

            var table = $('#gvJob');

            // begin: third table
            table.dataTable({

                // Internationalisation. For more info refer to http://datatables.net/manual/i18n
                "language": {
                    "aria": {
                        "sortAscending": ": activate to sort column ascending",
                        "sortDescending": ": activate to sort column descending"
                    },
                    "emptyTable": "No data available in table",
                    "info": "Showing _START_ to _END_ of _TOTAL_ entries",
                    "infoEmpty": "No entries found",
                    "infoFiltered": "(filtered1 from _MAX_ total entries)",
                    "lengthMenu": "Show _MENU_ entries",
                    "search": "Search:",
                    "zeroRecords": "No matching records found"
                },

                // Uncomment below line("dom" parameter) to fix the dropdown overflow issue in the datatable cells. The default datatable layout
                // setup uses scrollable div(table-scrollable) with overflow:auto to enable vertical scroll(see: assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js). 
                // So when dropdowns used the scrollable div should be removed. 
                //"dom": "<'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r>t<'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>",

                "bStateSave": true, // save datatable state(pagination, sort, etc) in cookie.

                "lengthMenu": [
                    [5, 15, 20, -1],
                    [5, 15, 20, "All"] // change per page values here
                ],
                // set the initial value
                "pageLength": 5,
                "language": {
                    "lengthMenu": " _MENU_ records"
                },
                "columnDefs": [{  // set default column settings
                    'orderable': false,
                    'targets': [0]
                }, {
                    "searchable": false,
                    "targets": [0]
                }],
                "order": [
                    [1, "asc"]
                ] // set first column as a default sort by asc
            });

            //var tableWrapper = jQuery('#sample_3_wrapper');

            //table.find('.group-checkable').change(function () {
            //    var set = jQuery(this).attr("data-set");
            //    var checked = jQuery(this).is(":checked");
            //    jQuery(set).each(function () {
            //        if (checked) {
            //            $(this).attr("checked", true);
            //        } else {
            //            $(this).attr("checked", false);
            //        }
            //    });
            //    jQuery.uniform.update(set);
            //});

            tableWrapper.find('.dataTables_length select').select2(); // initialize select2 dropdown
        });
    </script>
    <!-- END JAVASCRIPTS -->
</asp:Content>
