<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Seagate_Copperwire.ascx.cs" Inherits="ALS.ALSI.Web.view.template.Seagate_Copperwire" %>
<script src="<%= ResolveUrl("~/assets/global/plugins/jquery.min.js") %>" type="text/javascript"></script>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<script type="text/javascript">
    $(document).ready(function () {
        $('.date-picker').datepicker();

        var form1 = $('#Form1');
        var error1 = $('.alert-error', form1);
        var success1 = $('.alert-success', form1);

        form1.validate({
            errorElement: 'span', //default input error message container
            errorClass: 'help-inline', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "",
            rules: {
                ctl00$ContentPlaceHolder2$ctl00$txtDesc: {
                    required: true,
                }
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

<form runat="server" id="Form1" method="POST" enctype="multipart/form-data" class="form-horizontal">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="alert alert-danger display-hide">
                <button class="close" data-close="alert"></button>
                You have some form errors. Please check below.
            </div>
            <div class="alert alert-success display-hide">
                <button class="close" data-close="alert"></button>
                Your form validation is successful!
            </div>

            <div class="portlet box blue-dark">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-cogs"></i>WORKING SHEET &nbsp;<i class="icon-tasks"></i> (<asp:Label ID="lbJobStatus" runat="server" Text=""></asp:Label>)
                    </div>
                    <div class="actions">
                    </div>
                </div>
                <div class="portlet-body">
                    <asp:Panel ID="PWorking" runat="server">
                        <div class="row">
                            <div class="col-md-9">
                                <h5>METHOD/PROCEDURE</h5>
                                <table class="table table-striped table-hover">
                                    <thead>
                                        <tr>
                                            <th>Test</th>
                                            <th>Procedure No</th>
                                            <th>Number of pieces used for extraction</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>Copper Wire Corrosion</td>
                                            <td>
                                                <asp:TextBox ID="txtProcedureNo" runat="server" Text="20400058-001 Rev B" CssClass="form-control"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNumberOfPiecesUsedForExtraction" runat="server" Text="32" CssClass="form-control"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <%--3--%>
                        <div class="row">
                            <div class="col-md-9">
                                <asp:Label ID="lbResultDesc" runat="server" Text=""></asp:Label>
                                <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-striped table-bordered mini" ShowHeaderWhenEmpty="True" ShowFooter="True" DataKeyNames="ID" OnRowDataBound="gvResult_RowDataBound" OnRowCommand="gvResult_RowCommand" OnRowEditing="gvResult_RowEditing" OnRowUpdating="gvResult_RowUpdating" OnRowCancelingEdit="gvResult_RowCancelingEdit">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Copper Wire Corrosion test" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litCopperWireCorrosionTest" runat="server" Text='<%# Eval("copper_wire_corrosion_test")%>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtCopperWireCorrosionTest" runat="server" Text='<%# Eval("copper_wire_corrosion_test")%>' CssClass="form-control"></asp:TextBox>

                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Specification Limit" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litSpecification" runat="server" Text='<%# Eval("specification_limit")%>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtSpecification" runat="server" Text='<%# Eval("specification_limit")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Result 1" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litResult1" runat="server" Text='<%# Eval("result")%>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlResult1" runat="server" class="select2_category form-control" AutoPostBack="True"></asp:DropDownList>
                                                <asp:HiddenField ID="hResult1" Value='<%# Eval("result")%>' runat="server" />
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Result 2" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litResult2" runat="server" Text='<%# Eval("result2")%>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlResult2" runat="server" class="select2_category form-control" AutoPostBack="True"></asp:DropDownList>
                                                <asp:HiddenField ID="hResult2" Value='<%# Eval("result2")%>' runat="server" />
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Result 3" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litResult3" runat="server" Text='<%# Eval("result3")%>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlResult3" runat="server" class="select2_category form-control" AutoPostBack="True"></asp:DropDownList>
                                                <asp:HiddenField ID="hResult3" Value='<%# Eval("result3")%>' runat="server" />
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" ToolTip="Edit" CommandName="Edit" CommandArgument='<%# Eval("ID")%>'><i class="fa fa-edit"></i></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="btnUpdate" runat="server" ToolTip="Update" ValidationGroup="CreditLineGrid"
                                                    CommandName="Update"><i class="fa fa-save"></i></asp:LinkButton>
                                                <asp:LinkButton ID="LinkCancel" runat="server" ToolTip="Cancel" CausesValidation="false"
                                                    CommandName="Cancel"><i class="fa fa-remove"></i></asp:LinkButton>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Hide">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnHide" runat="server" ToolTip="Hide" CommandName="Hide" OnClientClick="return confirm('ต้องการซ่อนแถว ?');"
                                                    CommandArgument='<%# Eval("id")%>'><i class="fa fa-minus"></i></asp:LinkButton>
                                                <asp:LinkButton ID="btnUndo" runat="server" ToolTip="Undo" CommandName="Normal" OnClientClick="return confirm('ยกเลิกการซ่อนแถว ?');"
                                                    CommandArgument='<%# Eval("id")%>'><i class="fa fa-refresh"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                            </div>
                        </div>
                        <br />
                    </asp:Panel>

                    <asp:Panel ID="pRefImage" runat="server">
                        <h4 class="form-section">Photo Before:</h4>
                        <div class="row">
                            <div class="form-group">
                                <label class="control-label col-md-3">Uplod Photo: </label>

                                <div class="col-md-3">
                                    <div class="fileinput fileinput-new" data-provides="fileinput">
                                        <div class="input-group input-large">
                                            <div class="form-control uneditable-input input-fixed input-large" data-trigger="fileinput">
                                                <i class="fa fa-file fileinput-exists"></i>&nbsp;
                                                               
                                            <span class="fileinput-filename"></span>
                                            </div>
                                            <span class="input-group-addon btn default btn-file">
                                                <span class="fileinput-new">Select file </span>
                                                <span class="fileinput-exists">Change </span>
                                                <asp:FileUpload ID="FileUpload1" runat="server" />

                                            </span>
                                            <a href="javascript:;" class="input-group-addon btn red fileinput-exists" data-dismiss="fileinput">Remove </a>

                                        </div>
                                    </div>
                                    <asp:Button ID="btnLoadFile" runat="server" CssClass="btn blue" OnClick="btnLoadFile_Click" Text="Upload" />
                                    ***เลือกชนิดไฟล์ที่เป็น *.jpg</span>
                                    <%--                                                <p class="text-success">อัพโหลดไฟล์ที่ได้ทำการแก้ไขเสร็จแล้ว</p>--%>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <div class="row">
                        <div class="col-md-3">
                            <asp:GridView ID="gvRefImages" runat="server" AutoGenerateColumns="False"
                                CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" ShowFooter="true" DataKeyNames="id,sample_id" OnRowCommand="gvRefImages_RowCommand" OnRowDeleting="gvRefImages_RowDeleting" OnRowCancelingEdit="gvRefImages_RowCancelingEdit" OnRowDataBound="gvRefImages_RowDataBound" OnRowEditing="gvRefImages_RowEditing" OnRowUpdating="gvRefImages_RowUpdating">
                                <Columns>

                                    <asp:TemplateField HeaderText="Image Order" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Literal ID="litImgType" runat="server" />
                                            <asp:HiddenField ID="hImgType" Value='<%# Eval("img_type")%>' runat="server" />

                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlImgType" runat="server" class="select2_category form-control" AutoPostBack="True"></asp:DropDownList>
                                            <asp:HiddenField ID="hImgType" Value='<%# Eval("img_type")%>' runat="server" />
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Image" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Image ID="path_img1" runat="server" ImageUrl='<%# Eval("path_img1")%>' Width="120" Height="120" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" runat="server" ToolTip="Edit" CommandName="Edit" CommandArgument='<%# Eval("ID")%>'><i class="fa fa-edit"></i></asp:LinkButton>
                                            <asp:LinkButton ID="btnDelete" runat="server" ToolTip="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID")%>'><i class="fa fa-trash-o"></i></asp:LinkButton>

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
                        </div>
                    </div>

                    <!-- END FORM-->
                    <div class="row">
                        <div class="col-md-12">
                            <!-- BEGIN Portlet PORTLET-->
                            <div class="portlet light">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="icon-puzzle font-grey-gallery"></i>
                                        <span class="caption-subject bold font-grey-gallery uppercase">Operation </span>
                                    </div>
                                </div>
                                <div class="portlet-body">
                                    <asp:Panel ID="pSpecification" runat="server">
                                        <%-- <div class="row">
                                            <div class="col-md-6">--%>
                                        <div class="form-group">
                                            <label class="control-label col-md-3">Specification:<span class="required">*</span></label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlSpecification" runat="server" class="select2_category form-control" DataTextField="C" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlSpecification_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--    </div>
                                        </div>--%>
                                    </asp:Panel>
                                    <asp:Panel ID="pStatus" runat="server">
                                        <%-- <div class="row">
                                            <div class="col-md-6">--%>
                                        <div class="form-group">
                                            <label class="control-label col-md-3">Approve Status:<span class="required">*</span></label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="select2_category form-control" DataTextField="name" DataValueField="ID" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--      </div>
                                        </div>--%>
                                        <br />
                                    </asp:Panel>
                                    <asp:Panel ID="pRemark" runat="server">
                                        <%--   <div class="row">
                                            <div class="col-md-6">--%>
                                        <div class="form-group">
                                            <label class="control-label col-md-3">Remark:<span class="required">*</span></label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtRemark" name="txtRemark" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%--    </div>
                                        </div>--%>
                                        <br />
                                    </asp:Panel>
                                    <asp:Panel ID="pDisapprove" runat="server">
                                        <%--  <div class="row">
                                            <div class="col-md-6">--%>
                                        <div class="form-group">
                                            <label class="control-label col-md-3">Assign To:<span class="required">*</span></label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlAssignTo" runat="server" class="select2_category form-control" DataTextField="name" DataValueField="ID" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--    </div>
                                        </div>--%>
                                        <br />
                                    </asp:Panel>
                                    <asp:Panel ID="pDownload" runat="server">
                                        <%--  <div class="row">
                                            <div class="col-md-6">--%>
                                        <div class="form-group">
                                            <label class="control-label col-md-3">Download:</label>
                                            <div class="col-md-6">
                                                <asp:Literal ID="litDownloadIcon" runat="server"></asp:Literal>
                                                <asp:LinkButton ID="lbDownload" runat="server" OnClick="lbDownload_Click">
                                                    <asp:Label ID="lbDownloadName" runat="server" Text="Download"></asp:Label>
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <%--      </div>
                                        </div>--%>
                                        <br />
                                    </asp:Panel>
                                    <asp:Panel ID="pUploadfile" runat="server">
                                        <div class="form-group" >
                                            <label class="control-label col-md-3">Uplod file: </label>

                                            <div class="col-md-3">
                                                <div class="fileinput fileinput-new" data-provides="fileinput">
                                                    <div class="input-group input-large">
                                                        <div class="form-control uneditable-input input-fixed input-large" data-trigger="fileinput">
                                                            <i class="fa fa-file fileinput-exists"></i>&nbsp;
                                                               
                                            <span class="fileinput-filename"></span>
                                                        </div>
                                                        <span class="input-group-addon btn default btn-file">
                                                            <span class="fileinput-new">Select file </span>
                                                            <span class="fileinput-exists">Change </span>
                                                            <asp:FileUpload ID="btnUpload" runat="server" />

                                                        </span>
                                                        <a href="javascript:;" class="input-group-addon btn red fileinput-exists" data-dismiss="fileinput">Remove </a>

                                                    </div>
                                                </div>
                                                <p class="text-success">อัพโหลดไฟล์ที่ได้ทำการแก้ไขเสร็จแล้ว</p>

                                            </div>
                                        </div>

                                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                        <br />
                                    </asp:Panel>
                                    <%--                                    <asp:Panel ID="pUploadfile" runat="server">

                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="control-label col-md-3">Uplod file:</label>
                                                    <div class="col-md-6">
                                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                                        <span class="btn green fileinput-button">
                                                            <i class="fa fa-plus"></i>
                                                            <span>Add files...</span>
                                                            <asp:FileUpload ID="btnUpload" runat="server" />
                                                        </span>
                                                        <h6>***อัพโหลดไฟล์ *.docx|doc</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Label ID="lbMessage" runat="server" Text=""></asp:Label>
                                        <br />
                                    </asp:Panel>--%>
                                </div>
                            </div>
                            <!-- END Portlet PORTLET-->
                        </div>
                    </div>



                    <div class="form-actions">
                        <!-- POPUP -->

                        <div class="modal-wide" id="popupErrorList" style="display: none;">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h class="modal-title">
                                            รายการปัญหา</h>
                                </div>
                                <div class="modal-body" style="width: 600px; height: 400px; overflow-x: hidden; overflow-y: scroll; padding-bottom: 10px;">
                                    <asp:Literal ID="litErrorMessage" runat="server"></asp:Literal>

                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnPopupErrorList" CssClass="btn default" Style="margin-top: 10px;" runat="server" Text="ปิด" />
                                </div>
                            </div>
                            <!-- /.modal-content -->
                        </div>
                        <!-- /.modal-dialog -->

                        <asp:LinkButton ID="bnErrListFake" runat="server">
                        </asp:LinkButton>
                        <asp:ModalPopupExtender ID="modalErrorList" runat="server" PopupControlID="popupErrorList"
                            TargetControlID="bnErrListFake" BackgroundCssClass="modal-backdrop modal-print-form fade in" BehaviorID="mpModalErrorList"
                            CancelControlID="btnPopupErrorList">
                        </asp:ModalPopupExtender>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn green" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- END PAGE CONTENT-->
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnLoadFile" />
            <asp:PostBackTrigger ControlID="lbDownload" />


        </Triggers>
    </asp:UpdatePanel>
</form>
