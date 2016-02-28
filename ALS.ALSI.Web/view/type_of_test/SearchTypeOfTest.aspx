<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SearchTypeOfTest.aspx.cs" Inherits="ALS.ALSI.Web.view.type_of_test.SearchTypeOfTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <form id="Form1" method="post" runat="server" class="form-horizontal">


        <div class="portlet light bordered">
            <div class="portlet-title">
                <div class="caption">
                    <i class="icon-equalizer font-red-sunglo"></i>
                    <span class="caption-subject font-red-sunglo bold uppercase">Search Condition&nbsp;</span>
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
                                <label class="control-label col-md-3">Specification:<span class="required">*</span></label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlSpecification" runat="server" class="select2_category form-control" DataTextField="name" DataValueField="ID"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

  <%--                      <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Name:<span class="required">*</span></label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlTypeOfTest" runat="server" class="select2_category form-control" DataTextField="name" DataValueField="ID"></asp:DropDownList>
                                </div>
                            </div>
                        </div>--%>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Name:<span class="required">*</span></label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtName" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-actions">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:LinkButton ID="btnSearch" runat="server" class="btn green" OnClick="btnSearch_Click1"><i class="icon-search"></i> Search</asp:LinkButton>
                                        <asp:LinkButton ID="btnCancel" runat="server" class="btn default" OnClick="btnCancel_Click"><i class="icon-refresh"></i> Cancel</asp:LinkButton>
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


        <div class="portlet box blue-dark">
            <div class="portlet-title">
                <div class="caption">
                    <i class="fa fa-cogs"></i>JOB RUNNING
                </div>
                <div class="actions">
                    <asp:LinkButton ID="lbAdd" runat="server" class="btn btn-default btn-sm" OnClick="lbAdd_Click"><i class="icon-pencil"></i> Add</asp:LinkButton>

                </div>
            </div>
            <div class="portlet-body">
                <!-- BEGIN FORM-->
                <asp:Label ID="lbTotalRecords" runat="server" Text="" Visible="false"></asp:Label>
                <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False"
                    CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="id" OnRowCommand="gvResult_RowCommand" OnRowDeleting="gvResult_RowDeleting" OnPageIndexChanging="gvResult_PageIndexChanging" AllowPaging="True" PageSize="20">
                    <Columns>
                        <asp:BoundField HeaderText="ID" DataField="ID" ItemStyle-HorizontalAlign="Left" SortExpression="ID" />
                        <asp:BoundField HeaderText="Specification" DataField="specification" ItemStyle-HorizontalAlign="Left" SortExpression="specification" />
                        <asp:BoundField HeaderText="Prefix" DataField="prefix" ItemStyle-HorizontalAlign="Left" SortExpression="prefix" />
                        <asp:BoundField HeaderText="Name" DataField="name" ItemStyle-HorizontalAlign="Left" SortExpression="name" />
                        <%--<asp:BoundField HeaderText="Under" DataField="parent" ItemStyle-HorizontalAlign="Left" SortExpression="parent" />--%>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEdit" runat="server" ToolTip="Edit" CommandName="Edit" CommandArgument='<%# Eval("ID")%>'><i class="fa fa-edit"></i></asp:LinkButton>
                                <asp:LinkButton ID="btnDelete" runat="server" ToolTip="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                    CommandArgument='<%# Eval("ID")%>'><i class="fa fa-trash"></i></asp:LinkButton>
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

