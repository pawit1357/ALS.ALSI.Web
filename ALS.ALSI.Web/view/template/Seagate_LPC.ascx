<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Seagate_LPC.ascx.cs" Inherits="ALS.ALSI.Web.view.template.Seagate_LPC" %>
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
                        <asp:LinkButton ID="lbDecimal" runat="server" OnClick="LinkButton1_Click" CssClass="btn btn-default"> <i class="fa fa-sort-numeric-asc"></i> ตั้งค่า</asp:LinkButton>
                    </div>
                </div>
                <div class="portlet-body">
                    <asp:Panel ID="pCoverpage" runat="server">
                        <div class="row" id="invDiv" runat="server">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-9">
                                        <h5>METHOD/PROCEDURE:</h5>
                                        <table class="table table-striped table-hover table-bordered">
                                            <thead>
                                                <tr>
                                                    <th>Analysis</th>
                                                    <th>Procedure No</th>
                                                    <th>Number of pieces used<br />
                                                        for extraction</th>
                                                    <th>Extraction<br />
                                                        Medium</th>
                                                    <th>Extraction<br />
                                                        Volume</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ddlA19" runat="server" CssClass="select2_category form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlA19_SelectedIndexChanged">
                                                            <asp:ListItem Value="3">LPC</asp:ListItem>
                                                            <asp:ListItem Value="1">LPC (68 KHz)</asp:ListItem>
                                                            <asp:ListItem Value="2">LPC (132 KHz)</asp:ListItem>
                                                            <asp:ListItem Value="4">LPC(Tray)</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtB19" runat="server" Text="20800040-001  Rev.AC" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCVP_C19" runat="server" Text="1" CssClass="form-control"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtCVP_C19" FilterType="Numbers" runat="server" />

                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtD19" runat="server" Text="0.1 um Filtered Degassed DI Water" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCVP_E19" runat="server" Text="500" CssClass="form-control"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="ftbe" TargetControlID="txtCVP_E19" FilterType="Numbers" runat="server" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>

                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-9">
                                        <h6>Results:</h6>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbSpecDesc" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>

                                                <td>
                                                    <asp:CheckBox ID="cbCheckBox" runat="server" Text="No Spec" OnCheckedChanged="cbCheckBox_CheckedChanged" AutoPostBack="true" /></td>
                                            </tr>
                                        </table>
                                        </h6>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged">
                                            <asp:ListItem Value="0.300" Selected="True">No. of Particles ≥ 0.3 μm (Counts/mL)</asp:ListItem>
                                            <asp:ListItem Value="0.500" Selected="True">No. of Particles ≥ 0.5 μm (Counts/mL)</asp:ListItem>
                                            <asp:ListItem Value="0.600" Selected="True">No. of Particles ≥ 0.6 μm (Counts/mL)</asp:ListItem>
                                        </asp:CheckBoxList>

                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-9">
                                        <asp:GridView ID="gvCoverPage03" CssClass="table table-striped table-hover table-bordered" runat="server" AutoGenerateColumns="False" DataKeyNames="ID,row_state" OnRowDataBound="gvCoverPage03_RowDataBound" OnRowCommand="gvCoverPage03_RowCommand">
                                            <Columns>
                                                <asp:BoundField DataField="LiquidParticleCount" HeaderText="Liquid Particle Count" />
                                                <asp:BoundField DataField="SpecificationLimits" HeaderText="Specification Limits(Counts/cm2)" />
                                                <asp:BoundField DataField="Results" HeaderText="Results" />
                                                <asp:TemplateField HeaderText="Hide">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnHide" runat="server" ToolTip="Hide" CommandName="Hide" OnClientClick="return confirm('ต้องการซ่อนแถว ?');"
                                                            CommandArgument='<%# Eval("ID")%>'><i class="fa fa-minus"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="btnUndo" runat="server" ToolTip="Undo" CommandName="Normal" OnClientClick="return confirm('ยกเลิกการซ่อนแถว ?');"
                                                            CommandArgument='<%# Eval("ID")%>'><i class="fa fa-refresh"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:GridView ID="gvCoverPage05" CssClass="table table-striped table-hover table-bordered" runat="server" AutoGenerateColumns="False" DataKeyNames="ID,row_state" OnRowDataBound="gvCoverPage05_RowDataBound" OnRowCommand="gvCoverPage05_RowCommand">
                                            <Columns>
                                                <asp:BoundField DataField="LiquidParticleCount" HeaderText="Liquid Particle Count" />
                                                <asp:BoundField DataField="SpecificationLimits" HeaderText="Specification Limits(Counts/cm2)" />
                                                <asp:BoundField DataField="Results" HeaderText="Results" />
                                                <asp:TemplateField HeaderText="Hide">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnHide" runat="server" ToolTip="Hide" CommandName="Hide" OnClientClick="return confirm('ต้องการซ่อนแถว ?');"
                                                            CommandArgument='<%# Eval("ID")%>'><i class="fa fa-minus"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="btnUndo" runat="server" ToolTip="Undo" CommandName="Normal" OnClientClick="return confirm('ยกเลิกการซ่อนแถว ?');"
                                                            CommandArgument='<%# Eval("ID")%>'><i class="fa fa-refresh"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:GridView ID="gvCoverPage06" CssClass="table table-striped table-hover table-bordered" runat="server" AutoGenerateColumns="False" DataKeyNames="ID,row_state" OnRowDataBound="gvCoverPage06_RowDataBound" OnRowCommand="gvCoverPage06_RowCommand">
                                            <Columns>
                                                <asp:BoundField DataField="LiquidParticleCount" HeaderText="Liquid Particle Count" />
                                                <asp:BoundField DataField="SpecificationLimits" HeaderText="Specification Limits(Counts/cm2)" />
                                                <asp:BoundField DataField="Results" HeaderText="Results" />
                                                <asp:TemplateField HeaderText="Hide">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnHide" runat="server" ToolTip="Hide" CommandName="Hide" OnClientClick="return confirm('ต้องการซ่อนแถว ?');"
                                                            CommandArgument='<%# Eval("ID")%>'><i class="fa fa-minus"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="btnUndo" runat="server" ToolTip="Undo" CommandName="Normal" OnClientClick="return confirm('ยกเลิกการซ่อนแถว ?');"
                                                            CommandArgument='<%# Eval("ID")%>'><i class="fa fa-refresh"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>


                    <asp:Panel ID="pDSH" runat="server">
                        <asp:Panel ID="pLoadFile" runat="server">

                            <%--                 <div class="form-group">
                                <label class="control-label col-md-3">ทศนิยม</label>
                                <div class="col-md-9">
                                    <div class="fileinput fileinput-new" data-provides="fileinput">
                                        <asp:LinkButton ID="lbDecimal" runat="server" OnClick="LinkButton1_Click" CssClass="btn btn-default"> <i class="fa fa-sort-numeric-asc"></i> ตั้งค่า</asp:LinkButton>
                                    </div>
                                </div>
                            </div>--%>
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



                        <h4 class="form-section">Manage Result Data</h4>
                        <div class="row">
                            <div class="col-md-8">
                                <h6>
                                    <asp:Label ID="lbResultDesc" runat="server" Text=""></asp:Label></h6>
                                <br />

                                <asp:Panel ID="p03" runat="server">
                                    <asp:Label ID="lbParticle" runat="server" Text="No. of Particles ≥ 0.3 μm (Counts/mL) "></asp:Label>
                                    <asp:GridView ID="gvWorkSheet_03" runat="server" CssClass="table table-striped table-hover table-bordered"></asp:GridView>
                                    <table class="table table-striped table-hover table-bordered">
                                        <tr>
                                            <td>Extraction Vol. (mL)</td>
                                            <td>
                                                <asp:Label ID="lbExtractionVol" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Surface Area (cm2)
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSurfaceArea" runat="server" Text="" AutoPostBack="true" OnTextChanged="txtSurfaceArea_TextChanged" CssClass="form-control"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtSurfaceArea" FilterType="Numbers,Custom" ValidChars="." runat="server" />


                                            </td>

                                        </tr>

                                        <tr>
                                            <td>No. of Parts Used
                                            </td>
                                            <td>
                                                <asp:Label ID="lbNoOfPartsUsed" runat="server" Text=""></asp:Label></td>
                                        </tr>

                                        <tr>
                                            <td>Dilution Factor (time)
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDilutionFactor" runat="server" Text="10" AutoPostBack="true" OnTextChanged="txtSurfaceArea_TextChanged" CssClass="form-control"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtDilutionFactor" FilterType="Numbers" runat="server" />



                                            </td>
                                        </tr>

                                    </table>
                                    <asp:GridView ID="gvWorkSheetAverage" CssClass="table table-striped table-hover table-bordered" runat="server"></asp:GridView>
                                    <table class="table table-striped table-hover table-bordered">
                                        <tr>
                                            <td>Average</td>
                                            <td>
                                                <asp:Label ID="lbAverage" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="p05" runat="server">
                                    <asp:Label ID="Label2" runat="server" Text="No. of Particles ≥ 0.5 μm (Counts/mL) "></asp:Label>
                                    <asp:GridView ID="gvWorkSheet_05" runat="server" CssClass="table table-striped table-hover table-bordered"></asp:GridView>
                                    <table class="table table-striped table-hover table-bordered">
                                        <tr>
                                            <td>Extraction Vol. (mL)</td>
                                            <td>
                                                <asp:Label ID="lbExtractionVol05" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Surface Area (cm2)
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSurfaceArea05" runat="server" Text="" AutoPostBack="true" OnTextChanged="txtSurfaceArea_TextChanged" CssClass="form-control"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="txtSurfaceArea" FilterType="Numbers,Custom" ValidChars="." runat="server" />


                                            </td>

                                        </tr>

                                        <tr>
                                            <td>No. of Parts Used
                                            </td>
                                            <td>
                                                <asp:Label ID="lbNoOfPartsUsed05" runat="server" Text=""></asp:Label></td>
                                        </tr>

                                        <tr>
                                            <td>Dilution Factor (time)
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDilutionFactor05" runat="server" Text="10" AutoPostBack="true" OnTextChanged="txtSurfaceArea_TextChanged" CssClass="form-control"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" TargetControlID="txtDilutionFactor" FilterType="Numbers" runat="server" />



                                            </td>
                                        </tr>

                                    </table>
                                    <asp:GridView ID="gvWorkSheetAverage05" CssClass="table table-striped table-hover table-bordered" runat="server"></asp:GridView>
                                    <table class="table table-striped table-hover table-bordered">
                                        <tr>
                                            <td>Average</td>
                                            <td>
                                                <asp:Label ID="lbAverage05" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="p06" runat="server">
                                    <asp:Label ID="Label3" runat="server" Text="No. of Particles ≥ 0.6 μm (Counts/mL) "></asp:Label>
                                    <asp:GridView ID="gvWorkSheet_06" runat="server" CssClass="table table-striped table-hover table-bordered"></asp:GridView>
                                    <table class="table table-striped table-hover table-bordered">
                                        <tr>
                                            <td>Extraction Vol. (mL)</td>
                                            <td>
                                                <asp:Label ID="lbExtractionVol06" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Surface Area (cm2)
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSurfaceArea06" runat="server" Text="" AutoPostBack="true" OnTextChanged="txtSurfaceArea_TextChanged" CssClass="form-control"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" TargetControlID="txtSurfaceArea" FilterType="Numbers,Custom" ValidChars="." runat="server" />


                                            </td>

                                        </tr>

                                        <tr>
                                            <td>No. of Parts Used
                                            </td>
                                            <td>
                                                <asp:Label ID="lbNoOfPartsUsed06" runat="server" Text=""></asp:Label></td>
                                        </tr>

                                        <tr>
                                            <td>Dilution Factor (time)
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDilutionFactor06" runat="server" Text="10" AutoPostBack="true" OnTextChanged="txtSurfaceArea_TextChanged" CssClass="form-control"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" TargetControlID="txtDilutionFactor" FilterType="Numbers" runat="server" />



                                            </td>
                                        </tr>

                                    </table>
                                    <asp:GridView ID="gvWorkSheetAverage06" CssClass="table table-striped table-hover table-bordered" runat="server"></asp:GridView>
                                    <table class="table table-striped table-hover table-bordered">
                                        <tr>
                                            <td>Average</td>
                                            <td>
                                                <asp:Label ID="lbAverage06" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                            </div>
                        </div>
                    </asp:Panel>





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
                                        <%--      <div class="row">
                                            <div class="col-md-6">--%>
                                        <div class="form-group">
                                            <label class="control-label col-md-3">Template Type:<span class="required">*</span></label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlTemplateType" runat="server" CssClass="select2_category form-control">
                                                    <asp:ListItem Value="1">Component</asp:ListItem>
                                                    <asp:ListItem Value="2">IDM</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-md-3">Detail Spec:<span class="required">*</span></label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlSpecification" runat="server" CssClass="select2_category form-control" DataTextField="B" DataValueField="ID" OnSelectedIndexChanged="ddlSpecification_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--         </div>
                                        </div>--%>

                                        <br />
                                    </asp:Panel>
                                    <asp:Panel ID="pStatus" runat="server">
                                        <%--       <div class="row">
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
                                        <%--      <div class="row">
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
                                        <%--   <div class="row">
                                            <div class="col-md-6">--%>
                                        <div class="form-group">
                                            <label class="control-label col-md-3">Assign To:<span class="required">*</span></label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlAssignTo" runat="server" class="select2_category form-control" DataTextField="name" DataValueField="ID" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--    </div>
                                        </div>
                                        <br />--%>
                                    </asp:Panel>
                                    <asp:Panel ID="pDownload" runat="server">
                                        <%--        <div class="row">
                                            <div class="col-md-6">--%>
                                        <div class="form-group">
                                            <label class="control-label col-md-3">Download:</label>
                                            <div class="col-md-6">
                                                <asp:Literal ID="litDownloadIcon" runat="server"></asp:Literal>
                                                <asp:LinkButton ID="lbDownload" runat="server" OnClick="lbDownload_Click">
                                                    <asp:Label ID="lbDownloadName" runat="server" Text="Download"></asp:Label>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lbDownloadPdf" runat="server" OnClick="lbDownloadPdf_Click" Text="ดาวโหลด pdf สำหรับส่งอีเมล์ลูกค้า">
</asp:LinkButton>
                                            </div>
                                        </div>
                                        <%--        </div>
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

                                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
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
                                            <td>Average of last 3</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal03" runat="server" TextMode="Number" CssClass="form-control" Text="0"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Average</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal04" runat="server" TextMode="Number" CssClass="form-control" Text="0"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>No. of Particles</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal05" runat="server" TextMode="Number" CssClass="form-control" Text="0"></asp:TextBox></td>
                                        </tr>

                                        <tr>
                                            <td>Unit</td>
                                            <td>
                                                <asp:DropDownList ID="ddlUnit" runat="server" AutoPostBack="True" class="select2_category form-control" DataTextField="Name" DataValueField="ID" OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <%--      <asp:DropDownList ID="ddlUnit" runat="server" class="select2_category form-control">
                                                    <asp:ListItem Selected="True" Value="1">ug/sq cm</asp:ListItem>
                                                    <asp:ListItem Value="2">ng/cm2</asp:ListItem>
                                                    <asp:ListItem Value="3">mg/g</asp:ListItem>
                                                    <asp:ListItem Value="4">mg</asp:ListItem>
                                                </asp:DropDownList>--%></td>
                                        </tr>






                                    </table>
                                </div>
                                <div class="modal-footer">
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
            <asp:PostBackTrigger ControlID="lbDownloadPdf" />
        </Triggers>
    </asp:UpdatePanel>
</form>

