<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Template.aspx.cs" Inherits="ALS.ALSI.Web.view.template.Template" %>

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
                    <span class="caption-subject font-red-sunglo bold uppercase">[<asp:Label ID="lbCommandName" runat="server" Text=""></asp:Label>]&nbsp;Template (Specification,Detail Spec,Component)</span>
                    <span class="caption-helper"></span>
                </div>
                <div class="tools">
                    <a href="#" class="collapse"></a>
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
                                    <asp:DropDownList ID="ddlSpecification" runat="server" class="select2_category form-control" DataTextField="name" DataValueField="ID" AutoPostBack="True"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

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
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">
                                    Template File Path:<span class="required">
										* </span>
                                </label>
                                <div class="col-md-6">
                                    <div class="input-group" style="text-align: left">
                                        <asp:TextBox ID="txtPathUrl" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <a class="btn green" href="#PopupTemplate" data-toggle="modal">Select</a>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row fileupload-buttonbar">
                        <div class="col-lg-7">
                            <div class="form-group">
                                <label class="control-label col-md-3">Path Source File:<span class="required">*</span></label>
                                <div class="col-md-6">
                                    <asp:HiddenField ID="hPathSourceFile" runat="server" />
                                    <span class="btn green fileinput-button">
                                        <i class="fa fa-plus"></i>
                                        <span>Add files...</span>
                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                    </span>
                                    <h6>***เลือกไฟล์ที่มี tab component,detail spec,specification</h6>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Spec Ref Field:<span class="required">*</span></label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtSpecRef" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <h4 class="form-section">Version History</h4>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Version:<span class="required">*</span></label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtVersion" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Description:<span class="required">*</span></label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtDescription" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Requestor:<span class="required">*</span></label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtRequestor" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>


                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">CreatedBy:<span class="required">*</span></label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtCreatedBy" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">VerifiedBy:<span class="required">*</span></label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtVerifiedBy" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">Demo Notes</h3>
                        </div>
                        <div class="panel-body">
                            <ul>
                                <li>##ขั้นตอนการอัพเดท Specification,Detail Spec,Component</li>
                                <li>เลือกไฟล์ที่มี </li>
                                <li>1.1 กรณียังไม่มี template ให้สร้างไฟล์ *.ascx ก่อน</li>
                                <li>1.2 กรณีมี template *.ascx แล้ว สามารเลือกไฟล์ *.ascx อ้างอิงแล้วอัพเดท specification,detail spec หรือ component ใหม่ได้เลย</li>
                                <li>2. โหลด Detail Spec/Component/Specification จาก Excel</li>
                                <li>3. The maximum file size for uploads in this demo is <strong>5 MB</strong> (default file size is unlimited).</li>
                                <li>4. Only Excel files (<strong>*.xlt</strong>) are allowed in this demo (by default there is no file type restriction).</li>
                                <li>** spec ref มาจาก column ที่มีค่า spefRef ค่าที่ใส่จะต้องเป็น ลำดับ column-1 เช่น specRef อยู่ในcolumn 5 ค่าที่ต้องใส่ก็จะเป็น 4</li>
                            </ul>
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

        <%--POPUP--%>
        <div class="modal fade" id="PopupTemplate" tabindex="-1" role="basic" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                        <h4 class="modal-title">Choose Template File:</h4>
                    </div>
                    <div class="modal-body">
                        <asp:DropDownList ID="ddlPathUrl" runat="server" DataTextField="path_url" DataValueField="ID" CssClass="select2_category form-control"></asp:DropDownList>

                        <asp:HiddenField ID="hCompanyId" runat="server" />
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton ID="btnSelectTemplate" class="btn green" runat="server" OnClick="btnSelectTemplate_Click"> Select <i class="icon-check"></i></asp:LinkButton>

                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
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

                    ctl00$ContentPlaceHolder2$ddlSpecification: {
                        required: true,
                    },
                    ctl00$ContentPlaceHolder2$txtName: {
                        minlength: 2,
                        required: true,
                    },
                    ctl00$ContentPlaceHolder2$txtPathUrl: {
                        minlength: 2,
                        required: true,
                    },
                    ctl00$ContentPlaceHolder2$txtVersion: {
                        minlength: 2,
                        required: true,
                    }, ctl00$ContentPlaceHolder2$txtDescription: {
                        minlength: 2,
                        required: true,
                    },
                    ctl00$ContentPlaceHolder2$txtSpecRef: {
                        number: true,
                    },

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

