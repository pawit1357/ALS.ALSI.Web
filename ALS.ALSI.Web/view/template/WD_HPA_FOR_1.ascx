﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WD_HPA_FOR_1.ascx.cs" Inherits="ALS.ALSI.Web.view.template.WD_HPA_FOR_1" %>

<form runat="server" id="Form1" method="POST" enctype="multipart/form-data">
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
                        <asp:Button ID="btnCoverPage" runat="server" Text="Cover Page" CssClass="btn green" OnClick="btnCoverPage_Click" />
                        <asp:Button ID="btnWorkSheet" runat="server" Text="Work Sheet" CssClass="btn blue" OnClick="btnCoverPage_Click" />
                    </div>
                </div>
                <div class="portlet-body">
                    <asp:Panel ID="pCoverPage" runat="server">


                        <div class="row" id="invDiv" runat="server">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-9">
                                        <h5>METHOD:</h5>
                                        <table class="table table-striped table-hover table-bordered">
                                            <thead>
                                                <tr>
                                                    <th>Particle Analysis by SEM EDX</th>
                                                    <th>Taped Area For Drive Parts</th>
                                                    <th>No. of Times Taped</th>
                                                    <th>Surface Area Analysed (cm2)</th>
                                                    <th>Particle Ranges</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtA23" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtB23" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtC23" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtD23" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtE23" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-9">
                                        <h6>Results:</h6>
                                        <h6>The Specification is based on WD's specification Doc No 
                            <asp:Label ID="lbDocNo" runat="server" Text=""></asp:Label>
                                            for
                            <asp:Label ID="lbComponent" runat="server" Text=""></asp:Label>
                                        </h6>
                                    </div>
                                </div>
                                <%-- RESULT--%>
                                <div class="row">
                                    <div class="col-md-9">

                                        <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-striped table-bordered mini" ShowHeaderWhenEmpty="True" ShowFooter="true" DataKeyNames="ID,row_type" OnRowDataBound="gvResult_RowDataBound" OnRowCommand="gvResult_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Test" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litTest" runat="server" Text='' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Particle Classification" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litB" runat="server" Text='<%# Eval("B")%>'></asp:Literal>
                                                        <asp:HiddenField ID="hParent" runat="server" Value='<%# Eval("Parent")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Result(Particles/cm2)" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litC" runat="server" Text='<%# Eval("C")%>'></asp:Literal>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Specification Limit(Particles/cm2)" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="litD" runat="server" Text='<%# Eval("D")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PASS / FAIL" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="litE" runat="server" Text='<%# Eval("E")%>'></asp:Label>
                                                    </ItemTemplate>
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
                                            <EmptyDataTemplate>
                                                <div class="data-not-found">
                                                    <asp:Literal ID="libDataNotFound" runat="server" Text="Data Not found" />
                                                </div>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>


                                <h6>Total hard particles/cm2 = total no. of hard particles/(no. of times taped x surface area analysed)</h6>
                                <br />

                                <h5>Stage Mimic of
                    <asp:Label ID="lbA34" runat="server" Text=""></asp:Label><br />
                                    <asp:Label ID="lbImgPath1" runat="server" Text=""></asp:Label></h5>

                                <asp:Image ID="img1" runat="server" Width="120" Height="120" />
                                <%--<asp:FileUpload ID="FileUpload1" runat="server" />--%>
                                <h6>Please refer to attachment for details of results</h6>
                                <h6>Note: This report was performed test by ALS Singapore.</h6>
                                <br />

                                <h5>Analysis Table on
                    <asp:Label ID="lbA48" runat="server" Text=""></asp:Label></h5>
                                <%-- Analysis Table --%>
                                <div class="row">
                                    <div class="col-md-9">
                                        <asp:GridView ID="gvResult_1" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-striped table-bordered mini" ShowHeaderWhenEmpty="True" ShowFooter="true" DataKeyNames="ID,row_type" OnRowDataBound="gvResult_1_RowDataBound" OnRowCommand="gvResult_1_RowCommand" OnRowDeleting="gvResult_1_RowDeleting" OnRowEditing="gvResult_1_RowEditing" OnRowUpdating="gvResult1_RowUpdating">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Elemental Composition" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litB" runat="server" Text='<%# Eval("B")%>' />
                                                        <asp:HiddenField ID="hParent" runat="server" Value='<%# Eval("Parent")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Count" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litC" runat="server" Text='<%# Eval("C")%>'></asp:Literal>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtC" runat="server" Text='<%# Eval("C")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Particle/cm2" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litD" runat="server" Text='<%# Eval("D")%>'></asp:Literal>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remark" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litE" runat="server" Text='<%# Eval("E")%>'></asp:Literal>
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
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pDSH" runat="server">
                        <h4 class="form-section">Manage Source File</h4>
                        <div class="row">
                            <div class="col-md-9">
                                <div class="form-group">
                                    <label class="control-label col-md-3">Choose Source files.:<span class="required">*</span></label>
                                    <div class="col-md-6">
                                        <asp:HiddenField ID="hPathSourceFile" runat="server" />
                                        <span class="btn green fileinput-button">
                                            <i class="fa fa-plus"></i>
                                            <span>Add files...</span>
                                            <asp:FileUpload ID="btnUpload" runat="server" AllowMultiple="true" />
                                        </span>
                                        <asp:Button ID="btnLoadFile" runat="server" Text="Upload" CssClass="btn blue" OnClick="btnLoadFile_Click" />
                                        <span>***เลือกชนิดไฟล์ที่เป็น *.jpg</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <h4 class="form-section">Manage CAS# Data</h4>
                        <div class="row">
                            <div class="col-md-12">
                                <h6>
                                    <asp:Label ID="lbResultDesc" runat="server" Text=""></asp:Label></h6>
                            </div>
                        </div>

                    </asp:Panel>
                    <!-- END FORM-->
                    <div class="form-actions">
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
                                                <asp:FileUpload ID="FileUpload1" runat="server" />
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
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnCalculate" runat="server" CssClass="btn green" OnClick="btnCalculate_Click" Text="Calculate" />
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
            <asp:PostBackTrigger ControlID="btnLoadFile" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="lbDownload" />

        </Triggers>
    </asp:UpdatePanel>
</form>
