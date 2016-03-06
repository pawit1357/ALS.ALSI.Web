<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WD_GCMS_CVR.ascx.cs" Inherits="ALS.ALSI.Web.view.template.WD_GCMS_CVR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<form runat="server" id="Form1" method="POST" enctype="multipart/form-data" class="form-horizontal">
    <asp:ToolkitScriptManager ID="ToolkitScript1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hProcedureUnit" runat="server" />


            <div class="portlet box blue-dark">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-cogs"></i>WORKING SHEET &nbsp;<i class="icon-tasks"></i> (<asp:Label ID="lbJobStatus" runat="server" Text=""></asp:Label>)           
                    </div>
                    <div class="actions">
                        <asp:Button ID="btnCoverPage" runat="server" Text="Cover Page" CssClass="btn blue" OnClick="btnCoverPage_Click" />
                        <asp:Button ID="btnDHS" runat="server" Text="HC" CssClass="btn blue" OnClick="btnCoverPage_Click" Visible="false" />
                    </div>
                </div>
                <div class="portlet-body">

                    <asp:Panel ID="pDSH" runat="server">
                        <div class="panel panel-success">
                            <div class="panel-heading">
                                <h3 class="panel-title">Notes</h3>
                            </div>
                            <div class="panel-body">
                                <ul>
                                    <li>##ขั้นตอนการอัพโหลด</li>
                                    <li>1. รายการย่อยที่มี - นำหน้าในหน้า Cover Page จะต้องเหมือนกับ ไฟล์ Excel โดยจะต้องอยู่ใน Column <b>Library/ID</b></li>
                                    <li>2. รายการอื่น ๆ ที่ไม่มี - นำหน้าในหน้า Cover Page จะต้องเหมือนกับ ไฟล์ Excel โดยจะต้องอยู่ใน Column <b>Classification</b></li>
                                    <li>3. Major Compound จะโหลดจาก Sheet <b>Major Comp</b> โดยจะไปแสดง 5 ลำดับล่าสุดในหน้า Cover Page ให้เลย</li>

                                </ul>
                            </div>
                        </div>

                        <asp:Panel ID="pLoadFile" runat="server">
                            <asp:Literal ID="litErrorMessage" runat="server"></asp:Literal>

                            <div class="form-group">
                                <label class="control-label col-md-3">ทศนิยม</label>
                                <div class="col-md-9">
                                    <div class="fileinput fileinput-new" data-provides="fileinput">
                                        <asp:LinkButton ID="lbDecimal" runat="server" OnClick="LinkButton1_Click" CssClass="btn btn-default"> <i class="fa fa-sort-numeric-asc"></i> ตั้งค่า</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">Select Worksheet: </label>

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
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3"></label>
                                <div class="col-md-9">
                                    <div class="fileinput fileinput-new" data-provides="fileinput">
                                        <asp:Button ID="btnLoadFile" runat="server" Text="Load" CssClass="btn blue" OnClick="btnLoadFile_Click" />

                                    </div>
                                </div>
                            </div>

                        </asp:Panel>

                        <%--<h4 class="form-section">Manage Source File</h4>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label col-md-3">Choose Source files.:<span class="required">*</span></label>
                                    <div class="col-md-6">
                                        <asp:HiddenField ID="hPathSourceFile" runat="server" />
                                        <span class="btn green fileinput-button">
                                            <i class="fa fa-plus"></i>
                                            <span>Add files...</span>
                                            <asp:FileUpload ID="btnUpload" runat="server" AllowMultiple="true" />
                                        </span>
                                        <h6>***เลือกไฟล์ที่มี *.xls</h6>
                                        <asp:Label ID="lbMessage" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label col-md-3">Generate Data</label>
                                    <div class="col-md-6">
                                        <asp:Button ID="btnLoadFile" runat="server" Text="Submit" CssClass="btn blue" OnClick="btnLoadFile_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>--%>
                        <h4 class="form-section">Manage CAS# Data</h4>
                        <div class="row">
                            <div class="col-md-12">
                                <%--                                <asp:LinkButton ID="lbDecimal" runat="server" OnClick="LinkButton1_Click">การแสดงผลทศนิยม</asp:LinkButton>--%>

                                <h6>
                                    <asp:Label ID="lbResultDesc" runat="server" Text=""></asp:Label></h6>
                                <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="ID" OnRowDataBound="gvResult_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Pk#" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litPk" runat="server" Text='<%# Eval("pk")%>' />
                                                <asp:HiddenField ID="hdfID" Value='<%# Eval("ID")%>' runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="RT" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litRT" runat="server" Text='<%# Eval("rt")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Library/ID" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litLibraryID" runat="server" Text='<%# Eval("library_id")%>' />
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <%--EDIT--%>
                                        <asp:TemplateField HeaderText="Classification" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litClass" runat="server" Text='<%# Eval("classification")%>'></asp:Literal>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CAS#" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litCAS" runat="server" Text='<%# Eval("cas")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Qual" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litQual" runat="server" Text='<%# Eval("qual")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Area" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Literal ID="litArea" runat="server" Text='<%# Eval("area")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount(ng/part)" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litAmount" runat="server" Text='<%# Eval("amount")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
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
                    </asp:Panel>
                    <asp:Panel ID="pCoverpage" runat="server">
                        <div class="row" id="invDiv" runat="server">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-8">
                                        <h5>METHOD/PROCEDURE:</h5>
                                        <table class="table table-striped table-hover table-bordered">
                                            <thead>
                                                <tr>
                                                    <th>Test</th>
                                                    <th>Procedure No</th>
                                                    <th>Number of pieces<br />
                                                        used for extraction</th>
                                                    <th>Extraction<br />
                                                        Medium</th>
                                                    <th>Extraction<br />
                                                        Volume</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>CVR
                                                        <%--                                                        <asp:DropDownList ID="ddlTest" runat="server" AutoPostBack="True" CssClass="select2_category form-control">
                                                            <asp:ListItem Value="1">GCMS - Hydrocarbon Residue</asp:ListItem>
                                                            <asp:ListItem Value="2">CVR</asp:ListItem>
                                                        </asp:DropDownList>--%>

                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtProcedure" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtNumberOfPieces" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtExtractionMedium" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtExtractionVolumn" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-8">
                                        <h6>
                                            <asp:Label ID="lbDescription" runat="server" Text=""></asp:Label>
                                        </h6>

                                        <asp:GridView ID="gvCoverPages" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-striped table-bordered mini" ShowHeaderWhenEmpty="True" ShowFooter="true" DataKeyNames="ID,row_type" OnRowDataBound="gvCoverPage_RowDataBound" OnRowCommand="gvCoverPage_RowCommand" OnRowCancelingEdit="gvCoverPages_RowCancelingEdit" OnRowEditing="gvCoverPages_RowEditing" OnRowUpdating="gvCoverPages_RowUpdating">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Required Test" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litRequireTest" runat="server" Text='<%# Eval("A")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Analytes" DataField="B" ItemStyle-HorizontalAlign="Left" SortExpression="B" />

                                                <asp:TemplateField HeaderText="Specification Limits" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litSpecificationLimits" runat="server" Text='<%# Eval("C")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Results" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litResults" runat="server" Text='<%# Eval("D")%>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlResult" runat="server" class="span12 chosen" AutoPostBack="True"></asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="PASS / FAIL" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litPassFail" runat="server" Text='<%# Eval("E")%>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlPassFail" runat="server" class="span12 chosen" AutoPostBack="True"></asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Hide">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnHide" runat="server" ToolTip="Hide" CommandName="Hide" OnClientClick="return confirm('ต้องการซ่อนแถว ?');"
                                                            CommandArgument='<%# Eval("ID")%>'><i class="fa fa-minus"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="btnUndo" runat="server" ToolTip="Undo" CommandName="Normal" OnClientClick="return confirm('ยกเลิกการซ่อนแถว ?');"
                                                            CommandArgument='<%# Eval("ID")%>'><i class="fa fa-refresh"></i></asp:LinkButton>
                                                    </ItemTemplate>
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
                                            </Columns>


                                            <EmptyDataTemplate>
                                                <div class="data-not-found">
                                                    <asp:Literal ID="libDataNotFound" runat="server" Text="Data Not found" />
                                                </div>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                        <br />
                                        <asp:GridView ID="gvMajorCompounds" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-striped table-bordered mini" ShowHeaderWhenEmpty="True" ShowFooter="true" DataKeyNames="ID,RowState">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Major Compounds (R.T)" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litRt" runat="server" Text='<%# Eval("rt")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Classification" DataField="library_id" ItemStyle-HorizontalAlign="Center" SortExpression="B" />
                                                <asp:TemplateField HeaderText="Result (ng/part)" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litAmout" runat="server" Text='<%# Eval("amount")%>' />
                                                    </ItemTemplate>
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
                            </div>
                        </div>
                    </asp:Panel>
                    <!-- END FORM-->


                            <div class="note note-info">
<%--                                <h4 class="block">#</h4>--%>
                                <p>
                        <asp:Panel ID="pSpecification" runat="server">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label col-md-3">Component:<span class="required">*</span></label>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="ddlComponent" runat="server" CssClass="select2_category form-control" DataTextField="A" DataValueField="ID" OnSelectedIndexChanged="ddlComponent_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label col-md-3">Specification:<span class="required">*</span></label>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="ddlSpecification" runat="server" CssClass="select2_category form-control" DataTextField="A" DataValueField="ID" OnSelectedIndexChanged="ddlSpecification_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
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
                            <div class="form-group">
                                <label class="control-label col-md-3">Download: </label>

                                <div class="col-md-3">
                                    <div class="fileinput fileinput-new" data-provides="fileinput">
                                        <div class="input-group input-large">

                                            <asp:LinkButton ID="lbDownload" runat="server" OnClick="lbDownload_Click" CssClass="btn btn-default">
                                                <i class="fa fa-download"></i>
                                                <asp:Label ID="lbDownloadName" runat="server" Text="Download"></asp:Label>
                                            </asp:LinkButton>

                                        </div>
                                    </div>
                                </div>
                            </div>

                        </asp:Panel>
                        <asp:Panel ID="pUploadfile" runat="server">

                            <div class="form-group">
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
                                                <asp:FileUpload ID="FileUpload1" runat="server" />

                                            </span>
                                            <a href="javascript:;" class="input-group-addon btn red fileinput-exists" data-dismiss="fileinput">Remove </a>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <%--                    <ul>
                        <li>The maximum file size for uploads in this demo is <strong>5 MB</strong> (default file size is unlimited).</li>
                        <li>Only word or pdf files (<strong>DOC, DOCX, PDF</strong>) are allowed in this demo (by default there is no file type restriction).</li>
                    </ul>
                    <br />--%>
                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                            <br />
                        </asp:Panel>

                                </p>
                            </div>


                    <div class="form-actions">


                        <div class="modal-wide" id="pnlModalDemo" style="display: none;">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h class="modal-title">
                                            กำหนดทศนิยมให้คอลัมภ์</h>
                                </div>
                                <div class="modal-body" style="width: 600px; height: 400px; overflow-x: hidden; overflow-y: scroll; padding-bottom: 10px;">
                                    <table class="table table-striped">
                                        <tr>
                                            <td>Amout</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal01" runat="server" TextMode="Number" CssClass="form-control" Text="3"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Total Organic Compound</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal02" runat="server" TextMode="Number" CssClass="form-control" Text="3"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="modal-footer">
                                    <%--                                <asp:Button ID="OK" runat="server" CssClass="btn purple" Style="margin-top: 10px; margin-left: 20px;" Text="บันทึก" OnClick="OK_Click" />--%>

                                    <asp:Button ID="btnClose" CssClass="btn default" Style="margin-top: 10px;" runat="server" Text="ปิด" />
                                </div>
                            </div>
                            <!-- /.modal-content -->
                        </div>
                        <!-- /.modal-dialog -->

                        <asp:LinkButton ID="lnkFake" runat="server">
                        </asp:LinkButton>
                        <asp:ModalPopupExtender ID="ModolPopupExtender" runat="server" PopupControlID="pnlModalDemo"
                            TargetControlID="lnkFake" BackgroundCssClass="modal-backdrop modal-print-form fade in" BehaviorID="mpModalDemo"
                            CancelControlID="btnClose">
                        </asp:ModalPopupExtender>


                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn green" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="disable btn" OnClick="btnCancel_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnLoadFile" />
            <asp:PostBackTrigger ControlID="lbDownload" />
            <asp:PostBackTrigger ControlID="lbDecimal" />

        </Triggers>
    </asp:UpdatePanel>
</form>

<!-- BEGIN PAGE LEVEL SCRIPTS -->
<script src="<%= ResolveUrl("~/assets/global/plugins/jquery.min.js") %>" type="text/javascript"></script>
<!-- END PAGE LEVEL SCRIPTS -->
<script>
    jQuery(document).ready(function () {


    });
</script>
<!-- END JAVASCRIPTS -->
