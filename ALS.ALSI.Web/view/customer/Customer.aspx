<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="ALS.ALSI.Web.view.customer.Customer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <form id="Form1" method="post" runat="server" class="form-horizontal">
        <div class="alert alert-danger display-hide">
            <button class="close" data-close="alert"></button>
            You have some form errors. Please check below.
        </div>
        <div class="alert alert-success display-hide">
            <button class="close" data-close="alert"></button>
            Your form validation is successful!
        </div>
        <div class="portlet light bordered">
            <div class="portlet-title">
                <div class="caption">
                    <i class="icon-equalizer font-red-sunglo"></i>
                    <span class="caption-subject font-red-sunglo bold uppercase">[<asp:Label ID="lbCommandName" runat="server" Text=""></asp:Label>]&nbsp;Customers</span>
                    <span class="caption-helper"></span>
                </div>
                <div class="tools">
                    <%--<a href="#" class="collapse"></a>--%>
                </div>
            </div>
            <div class="portlet-body form">
                <div class="form-body">
                    <!-- BEGIN FORM-->
                    <h4 class="form-section">&nbsp;Company Info</h4>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Customer Name:</label>
                                <div class="col-md-6">
                                    <div class="input-group" style="text-align: left">
                                        <asp:TextBox ID="txtCompanyName" runat="server" class="form-control"></asp:TextBox>
                                        <span class="input-group-btn"></span>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%--<div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Department:</label>
                                <div class="col-md-6">
                                    <div class="input-group" style="text-align: left">
                                        <asp:TextBox ID="txtDepartment" runat="server" class="form-control"></asp:TextBox>
                                        <span class="input-group-btn"></span>
                                    </div>
                                </div>
                            </div>
                        </div>--%>
                    </div>
                    <div class="row">
                        <div class="col-md-9">
                            <!-- BEGIN EXAMPLE TABLE PORTLET-->
                            <div class="portlet box blue-dark">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-cogs"></i>CUSTOMER ADDRESS
                                    </div>
                                    <div class="actions">
                                        <asp:LinkButton ID="btnAddCustomerAddress" runat="server" class="btn btn-default btn-sm" OnClick="btnAddCustomerAddress_Click"><i class="fa fa-pencil"></i> Add</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="portlet-body">
                                    <!-- BEGIN FORM-->
                                    <asp:GridView ID="gvCustomerAddress" runat="server" AutoGenerateColumns="False"
                                        CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="ID" OnRowCancelingEdit="gvCustomerAddress_RowCancelingEdit" OnRowDataBound="gvSample_RowDataBound" OnRowDeleting="gvCustomerAddress_RowDeleting" OnRowEditing="gvCustomerAddress_RowEditing" OnRowUpdating="gvCustomerAddress_RowUpdating" OnSelectedIndexChanging="gvCustomerAddress_SelectedIndexChanging">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Code" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Literal ID="lbCode" runat="server" Text='<%# Eval("code")%>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtCode" runat="server" Text='<%# Eval("code")%>' class="form-control"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Literal ID="litAddress" runat="server" Text='<%# Eval("address")%>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtAddress" runat="server" Text='<%# Eval("address")%>' class="form-control"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Telephone" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Literal ID="litTelephone" runat="server" Text='<%# Eval("telephone")%>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtTelephone" runat="server" Text='<%# Eval("telephone")%>' class="form-control"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fax" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Literal ID="litFax" runat="server" Text='<%# Eval("fax")%>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtFax" runat="server" Text='<%# Eval("fax")%>' class="form-control"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" runat="server" ToolTip="Edit" CommandName="Edit" CommandArgument='<%# Eval("ID")%>'><i class="fa fa-edit"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="btnDelete" runat="server" ToolTip="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                                        CommandArgument='<%# Eval("ID")%>'><i class="fa fa-trash"></i></asp:LinkButton>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:LinkButton ID="btnUpdate" runat="server" ToolTip="Update" ValidationGroup="CreditLineGrid"
                                                        CommandName="Update"><i class="fa fa-save"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="LinkCancel" runat="server" ToolTip="Cancel" CausesValidation="false"
                                                        CommandName="Cancel"><i class="fa fa-remove"></i></asp:LinkButton>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>

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
                    <%--                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Fax Number:</label>
                                <div class="col-md-6">
                                    <div class="input-group" style="text-align: left">
                                        <asp:TextBox ID="txtFax" runat="server" class="form-control"></asp:TextBox>
                                        <span class="input-group-btn"></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Tel Number:</label>
                                <div class="col-md-6">
                                    <div class="input-group" style="text-align: left">
                                        <asp:TextBox ID="txtTelNumber" runat="server" class="form-control"></asp:TextBox>
                                        <span class="input-group-btn"></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Email:</label>
                                <div class="col-md-6">
                                    <div class="input-group" style="text-align: left">
                                        <asp:TextBox ID="txtEmail" runat="server" class="form-control"></asp:TextBox>
                                        <span class="input-group-btn"></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Address:</label>
                                <div class="col-md-6">
                                    <div class="input-group" style="text-align: left">
                                        <textarea id="txtAddress" class="form-control" runat="server"></textarea>
                                        <span class="input-group-btn"></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>--%>

                    <h4 class="form-section">&nbsp;Contract Person</h4>
                    <div class="row">
                        <div class="col-md-9">
                            <!-- BEGIN EXAMPLE TABLE PORTLET-->
                            <div class="portlet box blue-dark">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-cogs"></i>CONTRACT PERSON LIST
                                    </div>
                                    <div class="actions">
                                        <asp:LinkButton ID="btnAdd" runat="server" class="btn btn-default btn-sm" OnClick="lbAdd_Click"><i class="fa fa-pencil"></i> Add</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="portlet-body">
                                    <!-- BEGIN FORM-->
                                    <asp:GridView ID="gvSample" runat="server" AutoGenerateColumns="False"
                                        CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="ID" OnRowCancelingEdit="gvSample_RowCancelingEdit" OnRowDataBound="gvSample_RowDataBound" OnRowDeleting="gvSample_RowDeleting" OnRowEditing="gvSample_RowEditing" OnRowUpdating="gvSample_RowUpdating" OnSelectedIndexChanging="gvSample_SelectedIndexChanging">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Literal ID="litName" runat="server" Text='<%# Eval("name")%>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtName" runat="server" Text='<%# Eval("name")%>' class="form-control"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Phone Number" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Literal ID="litPhone_number" runat="server" Text='<%# Eval("phone_number")%>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtPhone_number" runat="server" Text='<%# Eval("phone_number")%>' class="form-control"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Email" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Literal ID="litEmail" runat="server" Text='<%# Eval("email")%>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtEmail" runat="server" Text='<%# Eval("email")%>' class="form-control"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Department" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Literal ID="litDepartment" runat="server" Text='<%# Eval("department")%>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDepartment" runat="server" Text='<%# Eval("department")%>' class="form-control"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" runat="server" ToolTip="Edit" CommandName="Edit" CommandArgument='<%# Eval("ID")%>'><i class="fa fa-edit"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="btnDelete" runat="server" ToolTip="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                                        CommandArgument='<%# Eval("ID")%>'><i class="fa fa-trash"></i></asp:LinkButton>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:LinkButton ID="btnUpdate" runat="server" ToolTip="Update" ValidationGroup="CreditLineGrid"
                                                        CommandName="Update"><i class="fa fa-save"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="LinkCancel" runat="server" ToolTip="Cancel" CausesValidation="false"
                                                        CommandName="Cancel"><i class="fa fa-remove"></i></asp:LinkButton>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>

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

                    <div class="form-actions">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnSave" runat="server" class="btn green" Text="Save" OnClick="btnSave_Click" />
                                        <asp:LinkButton ID="btnCancel" runat="server" class="btn default" OnClick="btnCancel_Click"> Cancel</asp:LinkButton>
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

            var form1 = $('#Form1');
            var error1 = $('.alert-danger', form1);
            var success1 = $('.alert-success', form1);

            form1.validate({
                errorElement: 'span', //default input error message container
                errorClass: 'help-block help-block-error', // default input error message class
                focusInvalid: false, // do not focus the last invalid input
                ignore: "",  // validate all fields including form hidden input
                messages: {
                    select_multi: {
                        maxlength: jQuery.validator.format("Max {0} items allowed for selection"),
                        minlength: jQuery.validator.format("At least {0} items must be selected")
                    }
                },
                rules: {

                    ctl00$ContentPlaceHolder2$txtCompanyName: {
                        minlength: 2,
                        required: true,
                    },
                    //ctl00$ContentPlaceHolder2$txtDepartment: {
                    //    minlength: 2,
                    //    required: true
                    //},
                    ctl00$ContentPlaceHolder2$txtTelNumber: {
                        minlength: 2,
                        required: true,
                    },

                    //ctl00$ContentPlaceHolder2$txtFax: {
                    //    minlength: 2,
                    //    required: true,
                    //},
                    ctl00$ContentPlaceHolder2$txtEmail: {
                        required: true,
                        email: true
                    },
                    ctl00$ContentPlaceHolder2$txtAddress: {
                        minlength: 2,
                        required: true,
                    }


                },

                invalidHandler: function (event, validator) { //display error alert on form submit              
                    success1.hide();
                    error1.show();
                    Metronic.scrollTo(error1, -200);
                },

                highlight: function (element) { // hightlight error inputs
                    $(element)
                        .closest('.form-group').addClass('has-error'); // set error class to the control group
                },

                unhighlight: function (element) { // revert the change done by hightlight
                    $(element)
                        .closest('.form-group').removeClass('has-error'); // set error class to the control group
                },

                success: function (label) {
                    label
                        .closest('.form-group').removeClass('has-error'); // set success class to the control group
                },

                submitHandler: function (form) {
                    form.submit();
                }
            });

        });
    </script>
    <!-- END JAVASCRIPTS -->
</asp:Content>
