<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Seagate_Corrosion.ascx.cs" Inherits="ALS.ALSI.Web.view.template.Seagate_Corrosion" %>
<script src="<%= ResolveUrl("~/assets/global/plugins/jquery.min.js") %>" type="text/javascript"></script>

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
                                            <td>Corrosion test</td>
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
                                        <asp:TemplateField HeaderText="Temperature Humidity Parameters" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litTemperature_humidity_parameters" runat="server"  Text='<%# Eval("temperature_humidity_parameters")%>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlhTemperature_humidity_parameters" runat="server" class="select2_category form-control" AutoPostBack="True" DataTextField="C" DataValueField="ID"></asp:DropDownList>
                                                <asp:HiddenField ID="hTemperature_humidity_parameters" Value='<%# Eval("temperature_humidity_parameters_id")%>' runat="server" />
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specification" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litSpecification" runat="server" Text='<%# Eval("specification")%>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtSpecification" runat="server" Text='<%# Eval("specification")%>'  CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Results" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litResult" runat="server" Text='<%# Eval("result")%>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlResult" runat="server" class="select2_category form-control" AutoPostBack="True"></asp:DropDownList>
                                                <asp:HiddenField ID="hResult" Value='<%# Eval("result")%>' runat="server" />
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
                            <div class="col-md-9">
                                <div class="form-group">
                                    <label class="control-label col-md-3">Choose Source files.:<span class="required">*</span></label>
                                    <div class="col-md-6">
                                        <asp:HiddenField ID="hPathSourceFile" runat="server" />
                                        <span class="btn green fileinput-button">
                                            <i class="fa fa-plus"></i>
                                            <span>Add files...</span>
                                            <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="true" />
                                        </span>
                                        -<span><asp:Button ID="btnLoadFile" runat="server" CssClass="btn blue" OnClick="btnLoadFile_Click" Text="Upload" />
                                            ***เลือกชนิดไฟล์ที่เป็น *.jpg</span>
                                    </div>
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
                    <div class="form-actions">
                        <asp:Panel ID="pSpecification" runat="server">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label col-md-3">Component:<span class="required">*</span></label>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="ddlComponent" runat="server" class="select2_category form-control" DataTextField="A" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlComponent_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label col-md-3">Specification:<span class="required">*</span></label>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="ddlSpecification" runat="server" class="select2_category form-control" DataTextField="B" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlSpecification_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pStatus" runat="server">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label col-md-3">Approve Status:<span class="required">*</span></label>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="select2_category form-control" DataTextField="name" DataValueField="ID" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                        </asp:Panel>
                        <asp:Panel ID="pRemark" runat="server">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label col-md-3">Remark:<span class="required">*</span></label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtRemark" name="txtRemark" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                        </asp:Panel>
                        <asp:Panel ID="pDisapprove" runat="server">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label col-md-3">Assign To:<span class="required">*</span></label>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="ddlAssignTo" runat="server" class="select2_category form-control" DataTextField="name" DataValueField="ID" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                        </asp:Panel>
                        <asp:Panel ID="pDownload" runat="server">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label col-md-3">Download:</label>
                                        <div class="col-md-6">
                                            <i class="icon-download-alt"></i>
                                            <asp:LinkButton ID="lbDownload" runat="server" OnClick="lbDownload_Click">
                                                <asp:Label ID="lbDownloadName" runat="server" Text="Download"></asp:Label>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                        </asp:Panel>
                        <asp:Panel ID="pUploadfile" runat="server">

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
                            <%--                    <ul>
                        <li>The maximum file size for uploads in this demo is <strong>5 MB</strong> (default file size is unlimited).</li>
                        <li>Only word or pdf files (<strong>DOC, DOCX, PDF</strong>) are allowed in this demo (by default there is no file type restriction).</li>
                    </ul>
                    <br />--%>
                            <asp:Label ID="lbMessage" runat="server" Text=""></asp:Label>
                            <br />
                        </asp:Panel>
                        <br />
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
