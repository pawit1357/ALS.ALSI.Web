<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SearchCustomer.aspx.cs" Inherits="ALS.ALSI.Web.view.customer.SearchCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <form id="Form1" method="post" runat="server" class="form-horizontal">
         
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
                                <label class="control-label col-md-3">Customer Code:</label>
                                <div class="col-md-6">
                                    <div class="input-group" style="text-align: left">
                                        <asp:TextBox ID="txtCustomerCode" runat="server" class="form-control"></asp:TextBox>

                                        <span class="input-group-btn"></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Company Name:</label>
                                <div class="col-md-6">
                                    <div class="input-group" style="text-align: left">
                                        <asp:TextBox ID="txtCompanyName" runat="server" class="form-control"></asp:TextBox>

                                        <span class="input-group-btn"></span>
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
                                        <asp:LinkButton ID="btnSearch" runat="server" class="btn green" OnClick="btnSearch_Click"><i class="fa fa-search"></i> Search</asp:LinkButton>
                                        <asp:LinkButton ID="btnCancel" runat="server" class="btn default" OnClick="btnCancel_Click"><i class="fa fa-refresh"></i> Cancel</asp:LinkButton>
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


        <!-- BEGIN EXAMPLE TABLE PORTLET-->
        <div class="portlet box blue-dark">
            <div class="portlet-title">
                <div class="caption">
                    <i class="fa fa-cogs"></i>CUSTOMER
                </div>
                <div class="actions">

                    <asp:LinkButton ID="btnAdd" runat="server" class="btn btn-default btn-sm" OnClick="lbAdd_Click"><i class="icon-pencil"></i> Add</asp:LinkButton>

                </div>
            </div>
            <div class="portlet-body">
                <!-- BEGIN FORM-->

                <asp:Label ID="lbTotalRecords" runat="server" Text="" Visible="false"></asp:Label>
                <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False"
                    CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="id" OnRowCommand="gvResult_RowCommand" OnRowDeleting="gvResult_RowDeleting" OnPageIndexChanging="gvResult_PageIndexChanging" AllowPaging="True" PageSize="20" AllowSorting="True" OnSorting="gvResult_Sorting">
                    <Columns>
                        <asp:BoundField HeaderText="Code" DataField="customer_code" ItemStyle-HorizontalAlign="Left"/>
                        <asp:BoundField HeaderText="Name" DataField="company_name" ItemStyle-HorizontalAlign="Left" SortExpression="company_name" />
                        <asp:BoundField HeaderText="Address" DataField="address" ItemStyle-HorizontalAlign="Left"  />
<%--                        <asp:BoundField HeaderText="Mobile" DataField="mobile_number" ItemStyle-HorizontalAlign="Left" SortExpression="mobile_number" />--%>
<%--                        <asp:BoundField HeaderText="Email" DataField="email_address" ItemStyle-HorizontalAlign="Left" SortExpression="email_address" />--%>
                        <%--<asp:BoundField HeaderText="Branch" DataField="branch" ItemStyle-HorizontalAlign="Left" SortExpression="branch" />--%>
<%--                        <asp:BoundField HeaderText="Department" DataField="department" ItemStyle-HorizontalAlign="Left" SortExpression="department" />--%>
<%--                        <asp:BoundField HeaderText="Province" DataField="province" ItemStyle-HorizontalAlign="Left" SortExpression="province" />--%>

                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnView" runat="server" ToolTip="View" CommandName="View" CommandArgument='<%# Eval("id")%>'><i class="fa fa-list-alt"></i></asp:LinkButton>
                                <asp:LinkButton ID="btnEdit" runat="server" ToolTip="Edit" CommandName="Edit" CommandArgument='<%# Eval("id")%>'><i class="fa fa-edit"></i></asp:LinkButton>
                                <asp:LinkButton ID="btnDelete" runat="server" ToolTip="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                    CommandArgument='<%# Eval("id")%>'><i class="fa fa-trash"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                                                <PagerStyle HorizontalAlign = "Right"  CssClass="pagination-ys" />

                    <EmptyDataTemplate>
                        <div class="data-not-found">
                            <asp:Literal ID="libDataNotFound" runat="server" Text="Data Not found" />
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>


                <!-- END FORM-->
            </div>
        </div>


    </form>

    <!-- BEGIN PAGE LEVEL SCRIPTS -->
    <script src="/alis/assets/global/plugins/jquery.min.js" type="text/javascript"></script>    <script src="<%= ResolveUrl("~/assets/global/plugins/jquery.min.js") %>" type="text/javascript"></script>    <!-- END PAGE LEVEL SCRIPTS -->
    <script>
        /*
        jQuery(document).ready(function () {
       
            var table = $('#ContentPlaceHolder2_gvResult');
            // begin: third table
            table.dataTable({
                // set the initial value
                "pageLength": 5,
                "order": [[1, "desc"]]
            });
       
        });
        */
    </script>
    <!-- END JAVASCRIPTS -->
</asp:Content>
