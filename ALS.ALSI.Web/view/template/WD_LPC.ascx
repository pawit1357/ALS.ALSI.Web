<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WD_LPC.ascx.cs" Inherits="ALS.ALSI.Web.view.template.WD_LPC" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<form runat="server" id="Form1" method="POST" enctype="multipart/form-data" class="form-horizontal">
    <asp:ToolkitScriptManager ID="ToolkitScript1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="portlet box blue-dark">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-cogs"></i>WORKING SHEET &nbsp;<i class="icon-tasks"></i> (<asp:Label ID="lbJobStatus" runat="server" Text=""></asp:Label>)
                    </div>
                    <div class="actions">
                        <asp:Button ID="btnCoverPage" runat="server" Text="Cover Page" CssClass="btn btn-default btn-sm" OnClick="btnUsLPC_Click" />
                        <asp:Button ID="btnWorkSheet" runat="server" Text="WorkSheet" CssClass="btn blue" OnClick="btnUsLPC_Click" />
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="row">

                        <div class="col-md-12">
                            <asp:Panel ID="pCoverPage" runat="server">

                                <div class="row">
                                    <div class="col-md-9">
                                        <h5>METHOD/PROCEDURE:</h5>
                                        <table class="table table-striped table-hover table-bordered">
                                            <thead>
                                                <tr>
                                                    <th>Test</th>
                                                    <th>Procedure No</th>
                                                    <th>Number of pieces used<br />
                                                        for extraction</th>
                                                    <th>Extraction<br />
                                                        Medium</th>
                                                    <th>Extraction Volume<br />
                                                        (mL)</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>LPC</td>
                                                    <td>
                                                        <asp:TextBox ID="txtB21" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtC21" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtD21" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtE21" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-9">
                                        <h6>Results:</h6>
                                        <h6>The specification is based on Western Digital's document no.
                                            <asp:Label ID="lbSpecRev" runat="server" Text=""></asp:Label>for
                                            <asp:Label ID="lbComponent" runat="server" Text=""></asp:Label>
                                        </h6>
                                    </div>
                                </div>
                                <br />
                                <%-- Liquid Particle Count (68 KHz) 0.3 --%>
                                <div class="row">
                                    <div class="col-md-9">

                                        <asp:GridView ID="gvSpec" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-striped table-bordered mini" ShowHeaderWhenEmpty="True" DataKeyNames="ID,row_type" OnRowDataBound="gvSpec_RowDataBound" OnRowCommand="gvSpec_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Required Test" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbRequiredTest" runat="server" Text='LPC'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Particle Bin Size (µm)" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litAccumulativeSize" runat="server" Text='<%# Eval("B")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Specification Limits(Particles/cm2)" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litSpecLimit" runat="server" Text='<%# Eval("C")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Average of 5 data points(Particles/cm2)" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litAverage" runat="server" Text='<%# Eval("D")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PASS / FAIL" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litPassFail" runat="server" Text='<%# Eval("E")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Hide">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnHide" runat="server" ToolTip="Hide" CommandName="Hide" OnClientClick="return confirm('ต้องการซ่อนแถว ?');"
                                                            CommandArgument='<%# Eval("ID")%>'><i class="fa fa-minus"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="btnUndo" runat="server" ToolTip="Undo" CommandName="Normal" OnClientClick="return confirm('ยกเลิกการซ่อนแถว ?');"
                                                            CommandArgument='<%# Eval("ID")%>'><i class="fa fa-refresh"></i></asp:LinkButton>
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


                            </asp:Panel>

                            <asp:Panel ID="pDSH" runat="server">
                                <asp:Panel ID="pLoadFile" runat="server">

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
                                                        <asp:FileUpload ID="btnUpload" runat="server" AllowMultiple="true" />

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
                                <%--                                <h4 class="form-section">Manage Source File</h4>
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
                                                <h6>***เลือกไฟล์ที่มี *.txt</h6>
                                                <asp:Label ID="lbMessage" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label col-md-3">Generate Data</label>
                                            <div class="col-md-6">
                                                <asp:LinkButton ID="lbDecimal" runat="server" OnClick="LinkButton1_Click">การแสดงผลทศนิยม</asp:LinkButton>

                                                <asp:Button ID="btnLoadFile" runat="server" Text="Submit" CssClass="btn blue" OnClick="btnLoadFile_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>--%>
                            </asp:Panel>
                            <br />


                            <br />
                            <%-- Test Method: 92-004230 Rev. AK	--%>
                            <div class="row">
                                <div class="col-md-6">
                                    <table class="table table-striped table-hover" id="tab2" runat="server">
                                        <tbody>
                                            <tr runat="server" id="tr4">
                                                <td colspan="2">Test Method: <asp:Label ID="lbTestMethod" runat="server" Text="Label"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>Surface Area, cm²</td>
                                                <td>
                                                    <asp:TextBox ID="txtB48" runat="server"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>Extraction Vol, ml</td>
                                                <td>
                                                    <asp:TextBox ID="txtB49" runat="server" Text=""></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>Sample Vol, ml</td>
                                                <td>
                                                    <asp:TextBox ID="txtB50" runat="server" Text=""></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>No. of Parts</td>
                                                <td>
                                                    <asp:TextBox ID="txtB51" runat="server" Text=""></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>Wash Method</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlWashMethod" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlWashMethod_SelectedIndexChanged">
                                                        <asp:ListItem Value="Rinse">Rinse</asp:ListItem>
                                                        <asp:ListItem Value="Ultrasonic">Ultrasonic</asp:ListItem>
                                                        <asp:ListItem Value="Flip">Flip</asp:ListItem>
                                                        <asp:ListItem Value="Shake">Shake</asp:ListItem>
                                                    </asp:DropDownList>

                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <br />
                            <%--Tank Conditions--%>
                            <asp:Panel ID="pTankConditions" runat="server">
                                <div class="row">
                                    <div class="col-md-6">
                                        <table class="table table-striped table-hover" id="Table1" runat="server">
                                            <thead>
                                                <tr>
                                                    <th></th>
                                                    <th>Total Tank  Volume</th>
                                                    <th>Sonication Freq.</th>
                                                    <th>Ultrasonic Power</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>Tank Conditions</td>
                                                    <td>
                                                        <asp:TextBox ID="txtB54" runat="server" Text="20.2 L"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtC54" runat="server" Text="68 kHz"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtD54" runat="server" Text="4.8 W/L"></asp:TextBox></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </asp:Panel>
                            <br />
                            <%--Run--%>
                            <div class="row">
                                <div class="col-md-6">

                                    <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False"
                                        CssClass="table table-striped table-bordered mini" ShowHeaderWhenEmpty="True" DataKeyNames="ID,row_type" OnRowDataBound="gvResult_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Run" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbRun" runat="server" Text='<%# Eval("A")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Accumulative Size (µm)" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Literal ID="litAccumulativeSize" runat="server" Text='<%# Eval("B")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Blank(Counts/ml)" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Literal ID="litBlank" runat="server" Text='<%# Eval("C")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sample(Counts/ml)" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Literal ID="litSample" runat="server" Text='<%# Eval("D")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Blank-corrected(Counts/part)" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Literal ID="litBlankCorredted" runat="server" Text='<%# Eval("E")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Blank-corrected(Counts/cm²)" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Literal ID="litBlankCorredtedCM2" runat="server" Text='<%# Eval("F")%>' />
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
                            <br />
                            <%--Statistics--%>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:GridView ID="gvStatic" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-striped table-bordered mini" ShowHeaderWhenEmpty="True" DataKeyNames="ID,row_type" OnRowDataBound="gvStatic_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Statistics" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="lbStatistics" runat="server" Text='<%# Eval("A")%>'></asp:Literal>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Accumulative Size (µm)" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litAccumulativeSize" runat="server" Text='<%# Eval("B")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Blank-corrected(Counts/part)" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litBlankCorredted" runat="server" Text='<%# Eval("E")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Blank-corrected(Counts/cm²)" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litBlankCorredtedCM2" runat="server" Text='<%# Eval("F")%>' />
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
                                        <%--                                        <div class="row">--%>
                                        <%--                                            <div class="col-md-6">--%>
                                        <div class="form-group">
                                            <label class="control-label col-md-3">Component:<span class="required">*</span></label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlComponent" runat="server" CssClass="select2_category form-control" DataTextField="A" DataValueField="ID" OnSelectedIndexChanged="ddlComponent_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--</div>
                                            <div class="col-md-6">--%>
                                        <div class="form-group">
                                            <label class="control-label col-md-3">Detail Spec:<span class="required">*</span></label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlSpecification" runat="server" CssClass="select2_category form-control" DataTextField="A" DataValueField="ID" OnSelectedIndexChanged="ddlSpecification_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--                                            </div>--%>

                                        <%--                                        </div>--%>
                                        <br />
                                    </asp:Panel>
                                    <asp:Panel ID="pStatus" runat="server">
                                        <%--  <div class="row">
                                            <div class="col-md-6">--%>
                                        <div class="form-group">
                                            <label class="control-label col-md-3">Approve Status:<span class="required">*</span></label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="select2_category form-control" DataTextField="name" DataValueField="ID" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--    </div>
                                        </div>--%>
                                        <br />
                                    </asp:Panel>
                                    <asp:Panel ID="pRemark" runat="server">
                                        <%--    <div class="row">
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
                                        <%--          <div class="row">
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
                                                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                                        </span>
                                                        <h6>***อัพโหลดไฟล์ *.docx|doc</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <asp:Label ID="Label213" runat="server" Text=""></asp:Label>
                                        <br />
                                    </asp:Panel>--%>
                                </div>
                            </div>
                            <!-- END Portlet PORTLET-->
                        </div>
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
                                            <td>Blank</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal01" runat="server" TextMode="Number" CssClass="form-control" Text="2"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Sample</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal02" runat="server" TextMode="Number" CssClass="form-control" Text="2"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Blank-corrected </td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal03" runat="server" TextMode="Number" CssClass="form-control" Text="0"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Sample-corrected </td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal04" runat="server" TextMode="Number" CssClass="form-control" Text="0"></asp:TextBox></td>
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
