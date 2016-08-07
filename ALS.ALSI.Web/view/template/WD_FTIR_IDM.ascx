<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WD_FTIR_IDM.ascx.cs" Inherits="ALS.ALSI.Web.view.template.WD_FTIR_IDM" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<style type="text/css">
    .auto-style1 {
        height: 26px;
    }
</style>


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
                        <asp:Button ID="btnCoverPage" runat="server" Text="Cover Page" CssClass="btn btn-default btn-sm" OnClick="btnNVRFTIR_Click" />
                        <asp:Button ID="btnNVRFTIR" runat="server" Text="NVR-FTIR(Hex)" CssClass="btn blue" OnClick="btnNVRFTIR_Click" />
                    </div>
                </div>
                <div class="portlet-body">
                    <h4 class="form-section">Results</h4>
                    <asp:Panel ID="pCoverpage" runat="server">
                        <div class="row" id="invDiv" runat="server">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-9">
                                        <h5>METHOD/PROCEDURE:</h5>

                                        <asp:GridView ID="gvMethodProcedure" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-striped table-bordered mini" ShowHeaderWhenEmpty="True" ShowFooter="True" DataKeyNames="ID,row_type" OnRowDataBound="gvMethodProcedure_RowDataBound" OnRowCommand="gvProcedure_RowCommand" OnRowCancelingEdit="gvMethodProcedure_RowCancelingEdit" OnRowEditing="gvMethodProcedure_RowEditing" OnRowUpdating="gvMethodProcedure_RowUpdating">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Analysis" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litAnalysis" runat="server" Text='<%# Eval("A")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Procedure No" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litProcedureNo" runat="server" Text='<%# Eval("B")%>' />
                                                    </ItemTemplate>
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

                                <div class="row">
                                    <div class="col-md-9">
                                        <h6>Results:</h6>
                                        <%--        <h6>The specification is based on Western Digital's document no.
                                                    <asp:Label ID="lbDocRev" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="lbDesc" runat="server" Text=""></asp:Label></h6>--%>

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
                                                <asp:TemplateField HeaderText="Required test" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litRequiredTest" runat="server" Text='<%# Eval("A")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Analyte" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litAnalyte" runat="server" Text='<%# Eval("B")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Specification Limits (ng/cm2)" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litSpecificationLimits" runat="server" Text='<%# Eval("C")%>'></asp:Literal>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Results (ng/cm2)" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="litExtractionMedium" runat="server" Text='<%# Eval("D")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="PASS/FAIL" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="litPass_Fail" runat="server" Text='<%# Eval("E")%>'></asp:Label>
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

                                <h6>
                                    <%--=CONCATENATE("Remarks: The above analysis was carried out using FTIR spectrometer equipped with a MCT detector & a VATR  accessory. The instrument detection limit for Silicone Oil is ", ROUND('working-FTIR'!$B$24,7),"--%> 
Note: The above analysis was carried out using FTIR spectrometer equipped with a MCT detector & a VATR  accessory.
The instrument detection limit for silicone oil is 
                    <asp:Label ID="lbA31" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lbB31" runat="server" Text=""></asp:Label>
                                </h6>
                            </div>
                        </div>
                    </asp:Panel>

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
                    <%--NVR-FTIR(Hex)--%>
                    <asp:Panel ID="PWorking" runat="server">
                        <div class="row">
                            <div class="col-md-9">

                                <table class="table table-striped table-hover small" id="tb1" runat="server">
                                    <thead>
                                        <tr>
                                            <th colspan="4">Test Data</th>
                                        </tr>
                                        <tr>
                                            <th class="auto-style1">Volume of solvent used:</th>
                                            <th class="auto-style1">
                                                <asp:TextBox ID="txtNVR_FTIR_B14" runat="server" placeholder="ดึงข้อมูลจาก B14"></asp:TextBox>
                                            </th>
                                            <th class="auto-style1">
                                                <asp:Label ID="lbNVR_FTIR_B14" runat="server" Text=""></asp:Label></th>
                                            <th class="auto-style1"></th>
                                        </tr>
                                        <tr>
                                            <th>Surface area (S):</th>
                                            <th>
                                                <asp:TextBox ID="txtNVR_FTIR_B15" runat="server" placeholder="ดึงข้อมูลจาก B15"></asp:TextBox>
                                            </th>
                                            <th>
                                                <asp:Label ID="lbNVR_FTIR_B15" runat="server" Text=""></asp:Label></th>
                                            <th></th>
                                        </tr>
                                        <tr>
                                            <th>No. of parts extracted (N):</th>
                                            <th>
                                                <asp:TextBox ID="txtNVR_FTIR_B16" runat="server" placeholder="ดึงข้อมูลจาก B16"></asp:TextBox>
                                            </th>
                                            <th></th>
                                            <th></th>
                                        </tr>
                                        <tr>
                                            <th colspan="4">NVR Analysis</th>
                                        </tr>
                                        <tr>
                                            <th></th>
                                            <th>Wt.of Empty Pan (µg)</th>
                                            <th>Wt.of Pan + Residue (µg)</th>
                                            <th>Wt. of Residue (µg)</th>
                                        </tr>
                                    </thead>
                                    <tbody>

                                        <tr runat="server" id="tr1">
                                            <td>Blank (B)</td>
                                            <td>
                                                <asp:TextBox ID="txtNVR_B20" runat="server" CssClass="m-wrap small" placeholder="ดึงข้อมูลจาก B20"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtNVR_C20" runat="server" CssClass="m-wrap small" placeholder="ดึงข้อมูลจาก C20"></asp:TextBox></td>
                                            <td>
                                                <asp:Label ID="lbD20" runat="server" Text=""></asp:Label></td>
                                        </tr>

                                        <tr runat="server" id="tr2">
                                            <td>Sample (A)</td>
                                            <td>
                                                <asp:TextBox ID="txtNVR_B21" runat="server" CssClass="m-wrap small" placeholder="ดึงข้อมูลจาก B21"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtNVR_C21" runat="server" CssClass="m-wrap small" placeholder="ดึงข้อมูลจาก BC21"></asp:TextBox></td>
                                            <td>
                                                <asp:Label ID="lbD21" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr runat="server" id="tr3">
                                            <td>Calculations:</td>
                                            <td>NVR (ng/cm<sup>2</sup>)=</td>
                                            <td>(2*(A - B) * 1000) / (S * N)</td>
                                            <td>
                                                <asp:TextBox ID="lbC26" runat="server" CssClass="m-wrap small" placeholder="ดึงข้อมูลจาก C27"></asp:TextBox>
                                                <%--                                                <asp:Label ID="lbC26" runat="server" Text=""></asp:Label> <span style="color:red">ตำแหน่ง C27</span> --%>

                                            </td>
                                        </tr>

                                    </tbody>
                                </table>

                                <br />

                                <table class="table table-striped table-hover small" id="Table1" runat="server">
                                    <thead>
                                        <tr>
                                            <th colspan="4">FTIR Analysis</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr runat="server" id="tr4">
                                            <td>Silicone</td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr runat="server" id="tr5">
                                            <td>Peak Ht at 800cm-<sup>1</sup></td>
                                            <td>
                                                <asp:TextBox ID="txtFTIR_B30" runat="server" placeholder="ดึงข้อมูลจาก B31"></asp:TextBox></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr runat="server" id="tr6">
                                            <td>Slope of Calibration Plot</td>
                                            <td>
                                                <asp:TextBox ID="txtFTIR_B31" runat="server" placeholder="ดึงข้อมูลจาก B32"></asp:TextBox></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr runat="server" id="tr7">
                                            <td>y-intercept</td>
                                            <td>
                                                <asp:TextBox ID="txtFTIR_B32" runat="server" placeholder="ดึงข้อมูลจาก B33"></asp:TextBox></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr runat="server" id="tr8">
                                            <td>Amount of Silicone Detected (µg)</td>
                                            <td>
                                                <asp:TextBox ID="txtFTIR_B33" runat="server" placeholder="ดึงข้อมูลจาก B34"></asp:TextBox></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr runat="server" id="tr9">
                                            <td>Method Detection Limit, MDL</td>
                                            <td>
                                                <asp:TextBox ID="txtFTIR_B35" runat="server" Text="" placeholder="ดึงข้อมูลจาก B36"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbSilicone" runat="server" Text="ng/cm2"></asp:Label></td>
                                            <td></td>
                                        </tr>

                                        <tr runat="server" id="tr10">
                                            <td>Calculations:</td>
                                            <td>Silicone (ng/cm<sup>2</sup>) =</td>
                                            <td>(2 * silicone weight in ug *1000) / (S * N)</td>
                                            <td>
                                                <asp:TextBox ID="lbFTIR_C40" runat="server" Text="" placeholder="ดึงข้อมูลจาก B40"></asp:TextBox></td>
                                        </tr>
                                        <tr runat="server" id="tr18">
                                            <td></td>
                                            <td></td>
                                            <td>Reported as</td>
                                            <td>
                                                <asp:TextBox ID="txtC41" runat="server" Text="" placeholder="ดึงข้อมูลจาก C41"></asp:TextBox></td>
                                        </tr>


                                        <tr runat="server" id="tr11">
                                            <td>Amide</td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr runat="server" id="tr12">
                                            <td>Peak Ht at cm-<sup>1</sup></td>
                                            <td>
                                                <asp:TextBox ID="txtFTIR_B42" runat="server" placeholder="ดึงข้อมูลจาก B43"></asp:TextBox></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr runat="server" id="tr13">
                                            <td>Slope of Calibration Plot</td>
                                            <td>
                                                <asp:TextBox ID="txtFTIR_B43" runat="server" placeholder="ดึงข้อมูลจาก B44"></asp:TextBox></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr runat="server" id="tr14">
                                            <td>y-intercept</td>
                                            <td>
                                                <asp:TextBox ID="txtFTIR_B44" runat="server" placeholder="ดึงข้อมูลจาก B45"></asp:TextBox></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr runat="server" id="tr15">
                                            <td>Amount of Silicone Detected (µg)</td>
                                            <td>
                                                <asp:TextBox ID="txtFTIR_B45" runat="server" placeholder="ดึงข้อมูลจาก B46"></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr runat="server" id="tr16">
                                            <td>Method Detection Limit, MDL</td>
                                            <td>
                                                <asp:TextBox ID="txtFTIR_B48" runat="server" Text="" placeholder="ดึงข้อมูลจาก B48"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbAmide" runat="server" Text="ng/cm2"></asp:Label></td>
                                            <td></td>
                                        </tr>
                                        <tr runat="server" id="tr17">
                                            <td>Calculations:</td>
                                            <td>Amide (ng/cm<sup>2</sup>) =</td>
                                            <td>(2*amide weight in ug *1000) / (S * N)</td>
                                            <td>
                                                <asp:TextBox ID="lbFTIR_C49" runat="server" Text="" placeholder="ดึงข้อมูลจาก B52"></asp:TextBox></td>
                                        </tr>
                                        <tr runat="server" id="tr19">
                                            <td></td>
                                            <td></td>
                                            <td>Reported as</td>
                                            <td>
                                                <asp:TextBox ID="txtC53" runat="server" Text="" placeholder="ดึงข้อมูลจาก C53"></asp:TextBox></td>
                                        </tr>
                                    </tbody>
                                </table>
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

                                        <div class="form-group">
                                            <label class="control-label col-md-3">Component:<span class="required">*</span></label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlComponent" runat="server" CssClass="select2_category form-control" DataTextField="A" DataValueField="ID" OnSelectedIndexChanged="ddlComponent_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="control-label col-md-3">Detail Spec:<span class="required">*</span></label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlDetailSpec" runat="server" CssClass="select2_category form-control" DataTextField="A" DataValueField="ID" OnSelectedIndexChanged="ddlDetailSpec_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>


                                        <br />
                                    </asp:Panel>
                                    <asp:Panel ID="pStatus" runat="server">
                                        <%--    <div class="row">
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
                                        <%-- <div class="row">
                                            <div class="col-md-6">--%>
                                        <div class="form-group">
                                            <label class="control-label col-md-3">Remark:<span class="required">*</span></label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtRemark" name="txtRemark" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%--         </div>
                                        </div>--%>
                                        <br />
                                    </asp:Panel>
                                    <asp:Panel ID="pDisapprove" runat="server">
                                        <%--     <div class="row">
                                            <div class="col-md-6">--%>
                                        <div class="form-group">
                                            <label class="control-label col-md-3">Assign To:<span class="required">*</span></label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlAssignTo" runat="server" class="select2_category form-control" DataTextField="name" DataValueField="ID" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--         </div>
                                        </div>--%>
                                        <br />
                                    </asp:Panel>
                                    <asp:Panel ID="pDownload" runat="server">
                                        <%--   <div class="row">
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
                                    <table class="table table-striped">
                                        <tr>
                                            <td>Blank (B)</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal01" runat="server" TextMode="Number" CssClass="form-control" Text="2"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Sample (A)</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal02" runat="server" TextMode="Number" CssClass="form-control" Text="4"></asp:TextBox></td>

                                        </tr>
                                        <tr>
                                            <td>Peak Ht at 800cm-1</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal03" runat="server" TextMode="Number" CssClass="form-control" Text="4"></asp:TextBox></td>

                                        </tr>
                                        <tr>
                                            <td>Slope of Calibration Plot</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal04" runat="server" TextMode="Number" CssClass="form-control" Text="4"></asp:TextBox></td>

                                        </tr>
                                        <tr>
                                            <td>y-intercept</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal05" runat="server" TextMode="Number" CssClass="form-control" Text="4"></asp:TextBox></td>

                                        </tr>
                                        <tr>
                                            <td>Amount of Silicone Detected (µg)</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal06" runat="server" TextMode="Number" CssClass="form-control" Text="4"></asp:TextBox></td>

                                        </tr>
                                        <tr>
                                            <td>Method Detection Limit, MDL</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal07" runat="server" TextMode="Number" CssClass="form-control" Text="7"></asp:TextBox></td>

                                        </tr>
                                        <tr>
                                            <td>Calculations:</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal08" runat="server" TextMode="Number" CssClass="form-control" Text="2"></asp:TextBox></td>

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

                        <!-- END POPUP -->

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
