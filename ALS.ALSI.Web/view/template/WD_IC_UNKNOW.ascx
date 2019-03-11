<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WD_IC_UNKNOW.ascx.cs" Inherits="ALS.ALSI.Web.view.template.WD_IC_UNKNOW" %>
<script type="text/javascript" src="<%= ResolveClientUrl("~/js/jquery-1.8.3.min.js") %>"></script>
<script type="text/javascript">
    $(document).ready(function () {
    });
</script>

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
                        <asp:Button ID="btnCoverPage" runat="server" Text="Cover Page" CssClass="btn btn-default btn-sm" OnClick="btnCoverPage_Click" />
                        <asp:Button ID="btnWorking" runat="server" Text="Workingpg-IC" CssClass="btn btn-default btn-sm" OnClick="btnCoverPage_Click" />
                        <asp:LinkButton ID="lbDecimal" runat="server" OnClick="LinkButton1_Click" CssClass="btn btn-default"> <i class="fa fa-sort-numeric-asc"></i>ตั้งค่า</asp:LinkButton>

                    </div>
                </div>
                <div class="portlet-body">

                    <asp:Panel ID="pCoverpage" runat="server">
                        <div class="row">
                            <div class="col-md-9">
                                <h5>METHOD/PROCEDURE:</h5>
                                <table class="table table-striped table-hover table-bordered">
                                    <thead>
                                        <tr>
                                            <th>Test</th>
                                            <th>Procedure No</th>
                                            <th>Number of pieces used for extraction</th>
                                            <th>Extraction Medium</th>
                                            <th>Extraction Volume</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>IC</td>
                                            <td>
                                                <asp:TextBox ID="txtProcedureNo" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNumOfPiecesUsedForExtraction" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtExtractionMedium" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtExtractionVolume" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-9">
                                <h6>Results:</h6>
                                <%--          <h6>The specification is based on Western Digital's document no.<asp:Label ID="lbDocRev" runat="server" Text=""></asp:Label>
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

                                <asp:GridView ID="gvAnionic" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-striped table-bordered mini" ShowHeaderWhenEmpty="True" ShowFooter="True" DataKeyNames="id,row_type,B" OnRowDataBound="gvAnionic_RowDataBound" OnRowCommand="gvAnionic_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Anionic Contamination" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Literal ID="litAnalytes" runat="server" Text='<%# Eval("A")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specification Limits, (µg/cm2)" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litSpecificationLimits" runat="server" Text='<%# Eval("B")%>'></asp:Literal>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Results, (µg/cm2)" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="litResult" runat="server" Text='<%# Eval("wi")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Method Detection Limit, (µg/cm2)" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="litMethodDetectionLimit" runat="server" Text='<%# Eval("wf")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PASS / FAIL" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="litPassFail" runat="server" Text='<%# Eval("E")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
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
                                <br />
                                <asp:GridView ID="gvCationic" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-striped table-bordered mini" ShowHeaderWhenEmpty="True" ShowFooter="True" DataKeyNames="id,row_type,B" OnRowDataBound="gvCationic_RowDataBound" OnRowCommand="gvCationic_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Cationic Contamination" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Literal ID="litAnalytes" runat="server" Text='<%# Eval("A")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specification Limits, (µg/cm2)" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litSpecificationLimits" runat="server" Text='<%# Eval("B")%>'></asp:Literal>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Results, (µg/cm2)" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="litResult" runat="server" Text='<%# Eval("wi")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Method Detection Limit, (µg/cm2)" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="litMethodDetectionLimit" runat="server" Text='<%# Eval("wf")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PASS / FAIL" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="litPassFail" runat="server" Text='<%# Eval("E")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
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

                    </asp:Panel>

                    <asp:Panel ID="pWorkingIC" runat="server">
                        <asp:Panel ID="pLoadFile" runat="server">

                            <%--            <div class="form-group">
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
                        <%--                        <div class="row" id="invDiv" runat="server">--%>
                        <%--<h4 class="form-section">Results</h4>--%>

                        <div class="row">
                            <div class="col-md-6">
                                <table class="table table-striped table-hover">
                                    <tbody>
                                        <tr>
                                            <td>Total volume (TV) :</td>
                                            <td>
                                                <asp:TextBox ID="txtB11" runat="server" CssClass="form-control"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Surface area (A) :</td>
                                            <td>
                                                <asp:TextBox ID="txtB12" runat="server" CssClass="form-control"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>No of parts extracted (N)</td>
                                            <td>
                                                <asp:TextBox ID="txtB13" runat="server" CssClass="form-control"></asp:TextBox></td>
                                        </tr>

                                    </tbody>
                                </table>
                            </div>
                        </div>




                        <h4 class="form-section">Results</h4>
                        <%--1--%>
                        <div class="row">
                            <div class="col-md-12">

                                <asp:GridView ID="gvResultAnions" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-striped table-bordered mini" ShowHeaderWhenEmpty="True" ShowFooter="True" DataKeyNames="" OnRowCommand="gvAnionic_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Anions" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Literal ID="litType" runat="server" Text='<%# Eval("A")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Conc of water Blankµg/L (B)" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Literal ID="litA" runat="server" Text='<%# Eval("wb")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Conc of Sample µg/L (C)" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litB" runat="server" Text='<%# Eval("wc")%>'></asp:Literal>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dilution Factor" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="litC" runat="server" Text='<%# Eval("wd")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Raw Results ng/cm2" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="litD" runat="server" Text='<%# Eval("we")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Method Detection Limit ng/cm2" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="litE" runat="server" Text='<%# Eval("wf")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Instrument Detection Limit ng/cm2" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="litF" runat="server" Text='<%# Eval("wg")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Below MDL? (1=Yes, 0=No)" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="litG" runat="server" Text='<%# Eval("wh")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Final Conc. of Sample ng/cm2" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="litH" runat="server" Text='<%# Eval("wi")%>'></asp:Label>
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
                                <br />
                                <asp:GridView ID="gvResultCations" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-striped table-bordered mini" ShowHeaderWhenEmpty="True" ShowFooter="True" DataKeyNames="" OnRowCommand="gvAnionic_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Cations" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Literal ID="litType" runat="server" Text='<%# Eval("A")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Conc of water Blankµg/L (B)" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Literal ID="litA" runat="server" Text='<%# Eval("wb")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Conc of Sample µg/L (C)" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litB" runat="server" Text='<%# Eval("wc")%>'></asp:Literal>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dilution Factor" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="litC" runat="server" Text='<%# Eval("wd")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Raw Results ng/cm2" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="litD" runat="server" Text='<%# Eval("we")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Method Detection Limit ng/cm2" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="litE" runat="server" Text='<%# Eval("wf")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Instrument Detection Limit ng/cm2" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="litF" runat="server" Text='<%# Eval("wg")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Below MDL? (1=Yes, 0=No)" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="litG" runat="server" Text='<%# Eval("wh")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Final Conc. of Sample ng/cm2" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="litH" runat="server" Text='<%# Eval("wi")%>'></asp:Label>
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
                        <%--2--%>

                        <!-- END FORM-->
                        <%--                        </div>--%>
                    </asp:Panel>
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
                                                <div id='datepicker' class="input-group date datepicker col-md-6" data-date="" data-date-format="dd/mm/yyyy" data-link-field="dtp_input2"
                                                    style="max-width: 220px">
                                                    <asp:TextBox ID="txtDateAnalyzed" runat="server" CssClass="form-control" size="16" type="text" />
                                                    <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span>
                                                    </span>
                                                </div>
                                                ป้อนวันที่ในรูปแบบ dd/MM/yyyy ( วัน/เดือน/ปี(ค.ศ.) ) ตัวอย่าง 18/02/2018

                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="pSpecification" runat="server">
                                        <%--                                        <div class="row">--%>
                                        <%--                                            <div class="col-md-6">--%>
                                        <div class="form-group">
                                            <label class="control-label col-md-3">Component:<span class="required">*</span></label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlComponent" runat="server" class="select2_category form-control" DataTextField="A" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlComponent_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--        </div>
                                            <div class="col-md-6">--%>
                                        <div class="form-group">
                                            <label class="control-label col-md-3">Detail Spec:<span class="required">*</span></label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlDetailSpec" runat="server" class="select2_category form-control" DataTextField="A" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlDetailSpec_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--                                            </div>--%>
                                        <%--                                        </div>--%>
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
                                        <%--        </div>
                                        </div>--%>
                                        <br />
                                    </asp:Panel>
                                    <asp:Panel ID="pRemark" runat="server">
                                        <%--  <div class="row">
                                            <div class="col-md-6">--%>
                                        <div class="form-group">
                                            <label class="control-label col-md-3">Remark:<span class="required">*</span></label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtRemark" name="txtRemark" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%--          </div>
                                        </div>--%>
                                        <br />
                                    </asp:Panel>
                                    <asp:Panel ID="pDisapprove" runat="server">
                                        <%--         <div class="row">
                                            <div class="col-md-6">--%>
                                        <div class="form-group">
                                            <label class="control-label col-md-3">Assign To:<span class="required">*</span></label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlAssignTo" runat="server" class="select2_category form-control" DataTextField="name" DataValueField="ID" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--        </div>
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
                                                    <asp:Label ID="lbDownloadName" runat="server" Text="Download"></asp:Label></asp:LinkButton>
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

                                        <asp:Label ID="Label9" runat="server" Text=""></asp:Label>
                                        <br />
                                    </asp:Panel>

                                </div>
                            </div>
                            <!-- END Portlet PORTLET-->
                        </div>
                    </div>



                    <div class="form-actions">
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

                    <div class="modal-wide" id="pnlModalDemo" style="display: none;">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h class="modal-title">
                                            กำหนดทศนิยมให้คอลัมภ์</h>
                            </div>
                            <div class="modal-body" style="width: 600px; height: 400px; overflow-x: hidden; overflow-y: scroll; padding-bottom: 10px;">
                                <table class="table table-striped">
                                    <tr>
                                        <td>Conc of water blank,ug/L (B)</td>
                                        <td>
                                            <asp:TextBox ID="txtDecimal01" runat="server" TextMode="Number" CssClass="form-control" Text="4"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Conc of sample,ug/L (C)</td>
                                        <td>
                                            <asp:TextBox ID="txtDecimal02" runat="server" TextMode="Number" CssClass="form-control" Text="4"></asp:TextBox></td>

                                    </tr>
                                    <tr>
                                        <td>Dilution Factor</td>
                                        <td>
                                            <asp:TextBox ID="txtDecimal03" runat="server" TextMode="Number" CssClass="form-control" Text="0"></asp:TextBox></td>

                                    </tr>
                                    <tr>
                                        <td>Raw Result</td>
                                        <td>
                                            <asp:TextBox ID="txtDecimal04" runat="server" TextMode="Number" CssClass="form-control" Text="2"></asp:TextBox></td>

                                    </tr>
                                    <tr>
                                        <td>Method Detection Limit</td>
                                        <td>
                                            <asp:TextBox ID="txtDecimal05" runat="server" TextMode="Number" CssClass="form-control" Text="2"></asp:TextBox></td>

                                    </tr>
                                    <tr>
                                        <td>Instrument Detection Limi</td>
                                        <td>
                                            <asp:TextBox ID="txtDecimal06" runat="server" TextMode="Number" CssClass="form-control" Text="2"></asp:TextBox></td>

                                    </tr>
                                    <tr>
                                        <td>Below Detection</td>
                                        <td>
                                            <asp:TextBox ID="txtDecimal07" runat="server" TextMode="Number" CssClass="form-control" Text="2"></asp:TextBox></td>

                                    </tr>
                                    <tr>
                                        <td>Final Conc of sample</td>
                                        <td>
                                            <asp:TextBox ID="txtDecimal08" runat="server" TextMode="Number" CssClass="form-control" Text="2"></asp:TextBox></td>

                                    </tr>
                                    <tr>
                                        <td>Unit</td>
                                        <td>
                                            <asp:DropDownList ID="ddlUnit" runat="server" class="select2_category form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged" DataValueField="ID" DataTextField="Name">
                                            </asp:DropDownList>

                                            <%--         <asp:DropDownList ID="ddlUnit" runat="server" class="select2_category form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="1">ug/sq cm</asp:ListItem>
                                                <asp:ListItem Value="2">ng/cm2</asp:ListItem>
                                                <asp:ListItem Value="3">mg</asp:ListItem>
                                            </asp:DropDownList>--%>
                                        </td>
                                    </tr>
                                    <%--                      <tr>
                                        <td>for use in Total</td>
                                        <td>
                                            <asp:TextBox ID="txtDecimal09" runat="server" TextMode="Number" CssClass="form-control" Text="2"></asp:TextBox></td>


                                    </tr>--%>
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

                    <asp:LinkButton ID="lnkFake" runat="server"> </asp:LinkButton>
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

                    <asp:LinkButton ID="bnErrListFake" runat="server"> </asp:LinkButton>
                    <asp:ModalPopupExtender ID="modalErrorList" runat="server" PopupControlID="popupErrorList"
                        TargetControlID="bnErrListFake" BackgroundCssClass="modal-backdrop modal-print-form fade in" BehaviorID="mpModalErrorList"
                        CancelControlID="btnPopupErrorList">
                    </asp:ModalPopupExtender>


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
<script src="<%= ResolveUrl("~/assets/global/plugins/jquery.min.js") %>" type="text/javascript"></script>
<script type="text/javascript">
    //On Page Load.
    $(function () {
        SetDatePicker();
    });

    //On UpdatePanel Refresh.
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function (sender, e) {
            if (sender._postBackSettings.panelsToUpdate != null) {
                SetDatePicker();
                $(".datepicker-orient-bottom").hide();
            }
        });
    };

    function SetDatePicker() {
        $("#datepicker").datepicker();
        if ($("#txtDateAnalyzed").val() == "") {
            var dateNow = new Date();
            $('#datepicker').datepicker("setDate", dateNow);
        }
    }
</script>
