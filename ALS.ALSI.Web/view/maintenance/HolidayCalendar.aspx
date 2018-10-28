<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="HolidayCalendar.aspx.cs" Inherits="ALS.ALSI.Web.view.template.HolidayCalendar" %>

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
        
                            <%=Message %>

        <div class="portlet light bordered">
            <div class="portlet-title">
                <div class="caption">
                    <i class="icon-equalizer font-red-sunglo"></i>
                    <span class="caption-subject font-red-sunglo bold uppercase">อัพโหลดข้อมูลวันหยุดจากไฟล์ Excel (*.xls)</span>
                    <span class="caption-helper"></span>
                </div>
                <div class="tools">
                    <a href="#" class="collapse"></a>
                </div>
            </div>
            <div class="portlet-body form">
                <div class="form-body">
                    <!-- BEGIN FORM-->


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
                                    <h6>***เลือกไฟล์ Excel (*.xls)</h6>
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

            var form1 = $('#Form1');
            var error1 = $('.alert-danger', form1);
            var success1 = $('.alert-success', form1);

            //form1.validate({
            //    errorElement: 'span', //default input error message container
            //    errorClass: 'help-block help-block-error', // default input error message class
            //    focusInvalid: false, // do not focus the last invalid input
            //    ignore: "",  // validate all fields including form hidden input
            //    messages: {
            //        select_multi: {
            //            maxlength: jQuery.validator.format("Max {0} items allowed for selection"),
            //            minlength: jQuery.validator.format("At least {0} items must be selected")
            //        }
            //    },
            //    rules: {

            //        ctl00$ContentPlaceHolder2$ddlSpecification: {
            //            required: true,
            //        },
            //        ctl00$ContentPlaceHolder2$txtName: {
            //            minlength: 2,
            //            required: true,
            //        },
            //        ctl00$ContentPlaceHolder2$txtPathUrl: {
            //            minlength: 2,
            //            required: true,
            //        },
            //        ctl00$ContentPlaceHolder2$txtVersion: {
            //            minlength: 2,
            //            required: true,
            //        }, ctl00$ContentPlaceHolder2$txtDescription: {
            //            minlength: 2,
            //            required: true,
            //        },
            //        ctl00$ContentPlaceHolder2$txtSpecRef: {
            //            number: true,
            //        },

            //    },

            //    invalidHandler: function (event, validator) { //display error alert on form submit              
            //        success1.hide();
            //        error1.show();
            //        Metronic.scrollTo(error1, -200);
            //    },

            //    highlight: function (element) { // hightlight error inputs
            //        $(element)
            //            .closest('.form-group').addClass('has-error'); // set error class to the control group
            //    },

            //    unhighlight: function (element) { // revert the change done by hightlight
            //        $(element)
            //            .closest('.form-group').removeClass('has-error'); // set error class to the control group
            //    },

            //    success: function (label) {
            //        label
            //            .closest('.form-group').removeClass('has-error'); // set success class to the control group
            //    },

            //    submitHandler: function (form) {
            //        form.submit();
            //    }
            //});

        });
    </script>
    <!-- END JAVASCRIPTS -->
</asp:Content>

