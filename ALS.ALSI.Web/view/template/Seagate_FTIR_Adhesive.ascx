<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Seagate_FTIR_Adhesive.ascx.cs" Inherits="ALS.ALSI.Web.view.template.Seagate_FTIR_Adhesive" %>
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
                        <asp:Button ID="btnCoverPage" runat="server" Text="CoverPage" CssClass="btn green" OnClick="btnWorkingFTIR_Click" />
                        <asp:Button ID="btnWorkingFTIR" runat="server" Text="FTIR" CssClass="btn blue" OnClick="btnWorkingFTIR_Click" />
                        <asp:Button ID="btnWorkingNVR" runat="server" Text="NVR" CssClass="btn blue" OnClick="btnWorkingFTIR_Click" />
                        <asp:LinkButton ID="lbDecimal" runat="server" OnClick="LinkButton1_Click" CssClass="btn btn-default"> <i class="fa fa-sort-numeric-asc"></i> ตั้งค่า</asp:LinkButton>
                    </div>
                </div>
                <div class="portlet-body">
                    <%-- cover-page --%>
                    <asp:Panel ID="pCoverPage" runat="server">
                        <div class="row" id="invDiv" runat="server">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-12">
                                        <h5>METHOD/PROCEDURE:</h5>
                                        <div class="col-md-9">

                                            <asp:GridView ID="gvMethodProcedure" runat="server" AutoGenerateColumns="False"
                                                CssClass="table table-striped table-bordered mini" ShowHeaderWhenEmpty="True" ShowFooter="True" DataKeyNames="ID,row_type" OnRowDataBound="gvMethodProcedure_RowDataBound" OnRowCommand="gvProcedure_RowCommand" OnRowCancelingEdit="gvMethodProcedure_RowCancelingEdit" OnRowEditing="gvMethodProcedure_RowEditing" OnRowUpdating="gvMethodProcedure_RowUpdating">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Analysis" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litAnalysis" runat="server" Text='<%# Eval("A")%>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtAnalysis" runat="server" Text='<%# Eval("A")%>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Procedure No" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litProcedureNo" runat="server" Text='<%# Eval("B")%>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtProcedureNo" runat="server" Text='<%# Eval("B")%>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Number of pieces used for extraction" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litNumberOfPiecesUsedForExtraction" runat="server" Text='<%# Eval("C")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtNumberOfPiecesUsedForExtraction" runat="server" Text='<%# Eval("C")%>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Extraction Medium" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="litExtractionMedium" runat="server" Text='<%# Eval("D")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtExtractionMedium" runat="server" Text='<%# Eval("D")%>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Extraction Volume" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="litExtractionVolume" runat="server" Text='<%# Eval("E")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtExtractionVolume" runat="server" Text='<%# Eval("E")%>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
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

                                        </div>


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
                                        <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-striped table-bordered mini" ShowHeaderWhenEmpty="True" ShowFooter="True" DataKeyNames="ID,row_type" OnRowDataBound="gvResult_RowDataBound" OnRowCommand="gvResult_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Non-Volatile Residue" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litOrganicContamination" runat="server" Text='<%# Eval("A")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Specification Limits (ng/cm2)" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litSpecificationLimits" runat="server" Text='<%# Eval("B")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Results (ng/cm2)" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litResults" runat="server" Text='<%# Eval("C")%>'></asp:Literal>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
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


                                        <br />
                                        <asp:GridView ID="gvResult1" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-striped table-bordered mini" ShowHeaderWhenEmpty="True" ShowFooter="True" DataKeyNames="ID,row_type" OnRowDataBound="gvResult1_RowDataBound" OnRowCommand="gvResult1_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Silicone Contamination" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litOrganicContamination" runat="server" Text='<%# Eval("A")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Specification Limits (ng/cm2)" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litSpecificationLimits" runat="server" Text='<%# Eval("B")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Results (ng/cm2)" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litResults" runat="server" Text='<%# Eval("C")%>'></asp:Literal>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
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
                                <br />

                                &nbsp;<h6>
                                    <%--=CONCATENATE("Remarks: The above analysis was carried out using FTIR spectrometer equipped with a MCT detector & a VATR  accessory. The instrument detection limit for Silicone Oil is ", ROUND('working-FTIR'!$B$24,7),"--%> 
Note: The above analysis was carried out using FTIR spectrometer equipped with a MCT detector & a VATR  accessory. The instrument detection limit for Silicone Oil is
                    <asp:Label ID="lbA42" runat="server" Text=""></asp:Label>

                                </h6>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pLoadFile" runat="server">

                        <%--                     <div class="form-group">
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
                                            <asp:FileUpload ID="FileUpload1" runat="server" />

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
                    <%--working-FTIR--%>
                    <asp:Panel ID="PWorking" runat="server">
                        <div class="row">
                            <div class="col-md-9">
                                <table class="table table-striped table-hover table-bordered" id="Table2" runat="server">
                                    <thead>

                                        <tr>
                                            <th>Surface area per part (e) =</th>
                                            <th>
                                                <asp:TextBox ID="txtWB13" runat="server" Text="0"></asp:TextBox></th>
                                            <th>
                                                <asp:Label ID="lbW13Unit" runat="server" Text=""></asp:Label></th>
                                        </tr>
                                        <tr>
                                            <th>No. of parts extracted (f) = </th>
                                            <th>
                                                <asp:TextBox ID="txtWB14" runat="server" Text="0"></asp:TextBox></th>
                                            <th></th>
                                        </tr>
                                        <tr>
                                            <th>Total Surface area (A) =</th>
                                            <th>
                                                <asp:TextBox ID="txtWB15" runat="server" Text="0"></asp:TextBox></th>
                                            <th>
                                                <asp:Label ID="lbW15Unit" runat="server" Text=""></asp:Label></th>
                                        </tr>

                                    </thead>
                                </table>


                                <asp:GridView ID="gvWftir" CssClass="table table-striped table-bordered mini" runat="server" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Literal ID="litA" runat="server" Text='<%# Eval("A")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Seal & Label for HDA internal, Facestock" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Literal ID="litB" runat="server" Text='<%# Eval("B")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Seal & Label for HDA internal, Adhesive side" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litC" runat="server" Text='<%# Eval("C")%>'></asp:Literal>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Release Liner, ultra-low Silicone, facing adhesive side" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litD" runat="server" Text='<%# Eval("D")%>'></asp:Literal>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Release Liner, non-Silicone, facing adhesive side" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litE" runat="server" Text='<%# Eval("E")%>'></asp:Literal>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                       <asp:TemplateField HeaderText="Release Liner, ultra-low Silicone facing adhesive (inside)" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litF" runat="server" Text='<%# Eval("F")%>'></asp:Literal>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                       <asp:TemplateField HeaderText="Release Liner, ultra-low Silicone facing adhesive (outside)" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litG" runat="server" Text='<%# Eval("G")%>'></asp:Literal>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
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
                    <%--NVR--%>
                    <asp:Panel ID="PNvr" runat="server">
                        <div class="row">
                            <div class="col-md-9">

                                <asp:GridView ID="gvNVr" CssClass="table table-striped table-bordered mini" runat="server" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Literal ID="litA" runat="server" Text='<%# Eval("A")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Control" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Literal ID="litB" runat="server" Text='<%# Eval("B")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sample" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litC" runat="server" Text='<%# Eval("C")%>'></asp:Literal>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
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

                                                                        <asp:Panel ID="pAnalyzeDate" runat="server">

                                        <div class="form-group">
                                            <label class="control-label col-md-3">
                                                Date Analyzed:<span class="required">
										* </span>
                                            </label>
                                            <div class="col-md-6">
                                                <div class="input-group input-medium date date-picker" data-date="10/2012" data-date-format="dd/mm/yyyy" data-date-viewmode="years" data-date-minviewmode="months">
                                                    <asp:TextBox ID="txtDateAnalyzed" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                                    <span class="input-group-btn">
                                                        <button class="btn default" type="button"><i class="fa fa-calendar"></i></button>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="pSpecification" runat="server">
                                        <%--     <div class="row">
                                            <div class="col-md-6">--%>
                                        <div class="form-group">
                                            <label class="control-label col-md-3">Specification:<span class="required">*</span></label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlSpecification" runat="server" CssClass="select2_category form-control" DataTextField="B" DataValueField="ID" OnSelectedIndexChanged="ddlSpecification_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--   </div>
                                        </div>--%>
                                        <br />
                                    </asp:Panel>
                                    <asp:Panel ID="pStatus" runat="server">
                                        <%--      <div class="row">
                                            <div class="col-md-6">--%>
                                        <div class="form-group">
                                            <label class="control-label col-md-3">Approve Status:<span class="required">*</span></label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="select2_category form-control" DataTextField="name" DataValueField="ID" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--     </div>
                                        </div>--%>
                                        <br />
                                    </asp:Panel>
                                    <asp:Panel ID="pRemark" runat="server">
                                        <%--  <div class="row">
                                            <div class="col-md-6">--%>
                                        <div class="form-group">
                                            <label class="control-label col-md-3">Remark:<span class="required">*</span></label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%--      </div>
                                        </div>--%>
                                        <br />
                                    </asp:Panel>
                                    <asp:Panel ID="pDisapprove" runat="server">
                                        <%--    <div class="row">
                                            <div class="col-md-6">--%>
                                        <div class="form-group">
                                            <label class="control-label col-md-3">Assign To:<span class="required">*</span></label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlAssignTo" runat="server" CssClass="select2_category form-control" DataTextField="name" DataValueField="ID" AutoPostBack="true"></asp:DropDownList>
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
                                        <%--    </div>
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
                                                            <asp:FileUpload ID="btnUpload" runat="server" />

                                                        </span>
                                                        <a href="javascript:;" class="input-group-addon btn red fileinput-exists" data-dismiss="fileinput">Remove </a>

                                                    </div>
                                                </div>
                                                <p class="text-success">อัพโหลดไฟล์ที่ได้ทำการแก้ไขเสร็จแล้ว</p>

                                            </div>
                                        </div>

                                        <asp:Label ID="Label7" runat="server" Text=""></asp:Label>
                                        <br />
                                    </asp:Panel>
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
                                            กำหนดทศนิยม</h>
                                </div>
                                <div class="modal-body" style="width: 600px; height: 400px; overflow-x: hidden; overflow-y: scroll; padding-bottom: 10px;">
                                    <h1>FTIR</h1>
                                    <table class="table table-striped">
                                        <tr>
                                            <td>Peak height measured, y (mAbs)</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal01" runat="server" TextMode="Number" CssClass="form-control" Text="2"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Slope of curve, m</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal02" runat="server" TextMode="Number" CssClass="form-control" Text="4"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>y -intercept, c</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal03" runat="server" TextMode="Number" CssClass="form-control" Text="4"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Amount , x (ug) = (y - c) / m</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal04" runat="server" TextMode="Number" CssClass="form-control" Text="5"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Instrument Detection Limit (ug)</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal05" runat="server" TextMode="Number" CssClass="form-control" Text="3"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Instrument Detection Limit (ug/sqcm)</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal06" runat="server" TextMode="Number" CssClass="form-control" Text="4"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Amount (ng/cm2)</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal07" runat="server" TextMode="Number" CssClass="form-control" Text="3"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                    <h1>NVR</h1>
                                    <table class="table table-striped">
                                        <tr>
                                            <td>Control</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal08" runat="server" TextMode="Number" CssClass="form-control" Text="2"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Sample</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal09" runat="server" TextMode="Number" CssClass="form-control" Text="4"></asp:TextBox></td>
                                        </tr>


                                    </table>
                                    <h1>Unit</h1>
                                    <table class="table table-striped">

                                        <tr>
                                            <th>Unit</th>
                                            <th>
                                                <asp:DropDownList ID="ddlUnit" runat="server" class="select2_category form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged" DataValueField="ID" DataTextField="Name">
                                                </asp:DropDownList>

                                                <%--           <asp:DropDownList ID="ddlUnit" runat="server" CssClass="select2_category form-control" AutoPostBack="True">
                                                    <asp:ListItem Value="1" Selected="True">ng/cm2</asp:ListItem>
                                                    <asp:ListItem Value="2">ug/cm2</asp:ListItem>
                                                </asp:DropDownList>--%>


                                            </th>
                                            <th></th>
                                            <th></th>
                                            <th></th>
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
