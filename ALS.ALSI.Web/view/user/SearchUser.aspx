<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SearchUser.aspx.cs" Inherits="ALS.ALSI.Web.view.user.SearchUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <form id="Form2" method="post" runat="server" class="form-horizontal">
        <%--      
<asp:LinkButton ID="btnSearch" runat="server" class="btn mini yellow" OnClick="btnSearch_Click1"><i class="icon-search"></i> Search</asp:LinkButton>
<asp:LinkButton ID="btnCancel" runat="server" class="btn mini black" OnClick="btnCancel_Click"><i class="icon-refresh"></i> Cancel</asp:LinkButton>
        --%>
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
                                <label class="control-label col-md-3">User Name:</label>
                                <div class="col-md-6">
                                    <div class="form-group" style="text-align: left">
                                        <asp:TextBox ID="txtName" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Email:</label>
                                <div class="col-md-6">
                                    <div class="form-group" style="text-align: left">
                                        <asp:TextBox ID="txtEmail" runat="server" class="form-control"></asp:TextBox>
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
                        <!-- BEGIN FORM-->
                        <asp:Label ID="lbTotalRecords" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False"
                            CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="id" OnRowCommand="gvResult_RowCommand" OnRowDeleting="gvResult_RowDeleting" OnPageIndexChanging="gvResult_PageIndexChanging" AllowPaging="True" PageSize="20">
                            <Columns>
                                <asp:BoundField HeaderText="Role" DataField="role" ItemStyle-HorizontalAlign="Left" SortExpression="role" />
                                <asp:BoundField HeaderText="Username" DataField="username" ItemStyle-HorizontalAlign="Left" SortExpression="username" />
                                <asp:BoundField HeaderText="Email" DataField="email" ItemStyle-HorizontalAlign="Left" SortExpression="email" />
                                <asp:BoundField HeaderText="Last login" DataField="latest_login" ItemStyle-HorizontalAlign="Left" SortExpression="latest_login" DataFormatString="{0:dd-MM-yyyy HH:mm:ss}" />
                                <asp:BoundField HeaderText="Create date" DataField="create_date" ItemStyle-HorizontalAlign="Left" SortExpression="create_date" DataFormatString="{0:dd-MM-yyyy}" />
                                <asp:BoundField HeaderText="Status" DataField="status" ItemStyle-HorizontalAlign="Left" SortExpression="status" />

                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" ToolTip="Edit" CommandName="Edit" CommandArgument='<%# Eval("id")%>'><i class="fa fa-edit"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnDelete" runat="server" ToolTip="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                            CommandArgument='<%# Eval("id")%>'><i class="fa fa-trash"></i></asp:LinkButton>
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
                        <!-- END FORM-->
                    </div>
                </div>
            </div>
        </div>
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
                "pageLength": 10,
            });
            */

        });

    </script>
    <!-- END JAVASCRIPTS -->
</asp:Content>
