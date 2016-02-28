<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SearchSampleLog.aspx.cs" Inherits="ALS.ALSI.Web.view.sampleLog.SearchSampleLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="<%= ResolveClientUrl("~/js/jquery-1.8.3.min.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            App.init(); // init the rest of plugins and elements

            var form1 = $('#Form1');
            var error1 = $('.alert-error', form1);
            var success1 = $('.alert-success', form1);

            form1.validate({
                errorElement: 'span', //default input error message container
                errorClass: 'help-inline', // default input error message class
                focusInvalid: false, // do not focus the last invalid input
                ignore: "",
                rules: {
                    ctl00$ContentPlaceHolder2$txtId: {
                        digits: true
                    },
                },

                invalidHandler: function (event, validator) { //display error alert on form submit              
                    success1.hide();
                    error1.show();
                    App.scrollTo(error1, -200);
                },

                highlight: function (element) { // hightlight error inputs
                    $(element)
                        .closest('.help-inline').removeClass('ok'); // display OK icon
                    $(element)
                        .closest('.control-group').removeClass('success').addClass('error'); // set error class to the control group
                },

                unhighlight: function (element) { // revert the change dony by hightlight
                    $(element)
                        .closest('.control-group').removeClass('error'); // set error class to the control group
                },

                success: function (label) {
                    label
                        .addClass('valid').addClass('help-inline ok') // mark the current input as valid and display OK icon
                    .closest('.control-group').removeClass('error').addClass('success'); // set success class to the control group
                },


                submitHandler: function (form) {
                    form.submit();
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <form id="Form1" method="post" runat="server" class="form-horizontal">
        <div class="alert alert-error hide">
            <button class="close" data-dismiss="alert"></button>
            You have some form errors. Please check below.
        </div>
        <div class="alert alert-success hide">
            <button class="close" data-dismiss="alert"></button>
            Your form validation is successful!
        </div>
<%--        <div class="row-fluid">
            <div class="span12">
                <div class="portlet">
                    <div class="portlet-title">
                        <h4><i class="icon-reorder"></i>Search Condition</h4>
                        <div class="actions">
                            <div class="btn-group">
                                <asp:LinkButton ID="btnSearch" runat="server" class="btn mini yellow" OnClick="btnSearch_Click1"><i class="icon-search"></i> Search</asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" runat="server" class="btn mini black" OnClick="btnCancel_Click"><i class="icon-refresh"></i> Cancel</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="portlet-body">
                        <div class="span6 ">
                            <div class="control-group">
                                <label class="control-label" for="txtId">ID:</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtId" runat="server" class="m-wrap span6"></asp:TextBox>
                                </div>
                            </div>

                        </div>
                        <div class="span6 ">
                            <div class="control-group">
                                <label class="control-label" for="txtName">NAME:</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtName" runat="server" class="m-wrap span6"></asp:TextBox>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>--%>

        <div class="row-fluid">
            <div class="span12">
                <div class="portlet box grey">
                    <div class="portlet-title">
                        <h4><i class="icon-bar-chart"></i>Search Result</h4>
                        <div class="actions">
                            <div class="btn-group">
                                <%--<asp:LinkButton ID="btnAdd" runat="server" class="btn mini blue" OnClick="lbAdd_Click"><i class="icon-pencil"></i> Add</asp:LinkButton>--%>
                            </div>
                        </div>
                    </div>
                    <div class="portlet-body">
                        <asp:Label ID="lbTotalRecords" runat="server" Text=""></asp:Label>
                        <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="id" OnPageIndexChanging="gvResult_PageIndexChanging" PageSize="20">
                            <Columns>

                                <asp:BoundField HeaderText="ID" DataField="ID" ItemStyle-HorizontalAlign="Left" SortExpression="ID" />
                                <asp:BoundField HeaderText="Job Remark" DataField="job_remark" ItemStyle-HorizontalAlign="Left" SortExpression="job_remark" />
                                <asp:BoundField HeaderText="Date" DataField="date" ItemStyle-HorizontalAlign="Left" SortExpression="date" />

<%--                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" ToolTip="Edit" CommandName="Edit" CommandArgument='<%# Eval("ID")%>'><i class="icon-edit"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnDelete" runat="server" ToolTip="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                            CommandArgument='<%# Eval("ID")%>'><i class="icon-trash"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            </Columns>
                                                        <PagerStyle HorizontalAlign = "Right"  CssClass="pagination-ys" />

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

                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
