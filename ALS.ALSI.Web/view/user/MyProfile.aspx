<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="MyProfile.aspx.cs" Inherits="ALS.ALSI.Web.view.user.MyProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="<%= ResolveUrl("~/assets/global/plugins/jquery.min.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            App.init();

            var form1 = $('#Form1');
            var error1 = $('.alert-error', form1);
            var success1 = $('.alert-success', form1);

            form1.validate({
                errorElement: 'span', //default input error message container
                errorClass: 'help-inline', // default input error message class
                focusInvalid: false, // do not focus the last invalid input
                ignore: "",
                rules: {
                    ctl00$ContentPlaceHolder2$ddlRole: {
                        required: true,
                    },
                    ctl00$ContentPlaceHolder2$txtUser: {
                        minlength: 2,
                        required: true,
                    },
                    ctl00$ContentPlaceHolder2$txtEmail: {
                        minlength: 2,
                        required: true,
                    },
                    ctl00$ContentPlaceHolder2$txtPassword: {
                        minlength: 2,
                        required: true,
                    },
                    ctl00$ContentPlaceHolder2$ddlTitle: {
                        required: true,
                    },
                    ctl00$ContentPlaceHolder2$txtFirstName: {
                        minlength: 2,
                        required: true,
                    },
                    ctl00$ContentPlaceHolder2$txtLastName: {
                        minlength: 2,
                        required: true,
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

        <div class="portlet light bordered">
            <div class="portlet-title">
                <div class="caption">
                    <i class="icon-equalizer font-red-sunglo"></i>
                    <span class="caption-subject font-red-sunglo bold uppercase">[<asp:Label ID="lbCommandName" runat="server" Text=""></asp:Label>]&nbsp;Profile</span>
                    <span class="caption-helper"></span>
                </div>
                <div class="tools">
                    <%--<a href="#" class="collapse"></a>--%>
                </div>
            </div>
            <div class="portlet-body form">
                <div class="form-body">
                    <!-- BEGIN FORM-->
                    <h4 class="form-section">&nbsp;User Information</h4>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Role:<span class="required">*</span></label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlRole" runat="server" class="select2_category form-control" DataTextField="name" DataValueField="ID"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">User:<span class="required">*</span></label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtUser" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Email:<span class="required">*</span></label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtEmail" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Password:<span class="required">*</span></label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtPassword" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">
                                    Status:<span class="required">
										* </span>
                                </label>
                                <div class="radio-list">
                                    <label class="radio-inline">
                                        <asp:RadioButton ID="rdStatusA" GroupName="Status" runat="server" Checked="true" />Active
                                    </label>
                                    <label class="radio-inline">
                                        <asp:RadioButton ID="rdStatusI" GroupName="Status" runat="server" />InAvtive
                                    </label>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Responsible:<span class="required">*</span></label>
                                <div class="col-md-6">
                                    <asp:ListBox ID="lstTypeOfTest" runat="server" DataTextField="Name" DataValueField="ID" SelectionMode="Multiple" class="bs-select form-control"></asp:ListBox>
                                </div>
                            </div>
                        </div>

                    </div>
                    <h4 class="form-section">&nbsp;Personal Information</h4>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Title:<span class="required">*</span></label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlTitle" runat="server" class="select2_category form-control" DataTextField="name" DataValueField="ID"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">First Name:<span class="required">*</span></label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtFirstName" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Last Name:<span class="required">*</span></label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtLastName" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="form-actions">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:LinkButton ID="btnSave" runat="server" class="btn green" OnClick="btnSave_Click"> Save</asp:LinkButton>
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
</asp:Content>
