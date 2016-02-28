<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WD_IC.ascx.cs" Inherits="ALS.ALSI.Web.view.template.WD_IC" %>
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
                                <h6>The specification is based on Western Digital's document no.<asp:Label ID="lbDocRev" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lbDesc" runat="server" Text=""></asp:Label></h6>

                                <asp:GridView ID="gvAnionic" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-striped table-bordered mini" ShowHeaderWhenEmpty="True" ShowFooter="true" DataKeyNames="id,row_type,B" OnRowDataBound="gvAnionic_RowDataBound" OnRowCommand="gvAnionic_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Analytes" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Literal ID="litAnalytes" runat="server" Text='<%# Eval("A")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specification Limits, (µg/cm2)" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litSpecificationLimits" runat="server" Text='<%# Eval("B")%>'></asp:Literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Results, (µg/cm2)" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="litResult" runat="server" Text='<%# Eval("wi")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Method Detection Limit, (µg/cm2)" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="litMethodDetectionLimit" runat="server" Text='<%# Eval("wf")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PASS / FAIL" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="litPassFail" runat="server" Text='<%# Eval("E")%>'></asp:Label>
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
                                <br />
                                <asp:GridView ID="gvCationic" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-striped table-bordered mini" ShowHeaderWhenEmpty="True" ShowFooter="true" DataKeyNames="id,row_type,B" OnRowDataBound="gvCationic_RowDataBound" OnRowCommand="gvCationic_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Analytes" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Literal ID="litAnalytes" runat="server" Text='<%# Eval("A")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specification Limits, (µg/cm2)" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litSpecificationLimits" runat="server" Text='<%# Eval("B")%>'></asp:Literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Results, (µg/cm2)" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="litResult" runat="server" Text='<%# Eval("wi")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Method Detection Limit, (µg/cm2)" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="litMethodDetectionLimit" runat="server" Text='<%# Eval("wf")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PASS / FAIL" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="litPassFail" runat="server" Text='<%# Eval("E")%>'></asp:Label>
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

                    </asp:Panel>

                    <asp:Panel ID="pWorkingIC" runat="server">
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
                                        <tr>
                                            <td>Unit</td>
                                            <td>
                                                <asp:DropDownList ID="ddlUnit" runat="server" class="select2_category form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="1">ug/sq cm</asp:ListItem>
                                                    <asp:ListItem Value="1000">ng/cm2</asp:ListItem>
                                                    <asp:ListItem Value="0.001">mg</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>




                        <h4 class="form-section">Results</h4>
                        <%--1--%>
                        <div class="row">
                            <div class="col-md-12">
                                <table class="table table-striped table-hover table-bordered">
                                    <thead>
                                        <tr>
                                            <th>Anions</th>
                                            <th>Conc of water blank,ug/L (B)</th>
                                            <th>Conc of sample,ug/L (C)</th>
                                            <th>Dilution Factor</th>
                                            <th>Raw Result (<asp:Label ID="Label1" runat="server"></asp:Label>
                                                )</th>

                                            <th>Method Detection Limit (<asp:Label ID="Label2" runat="server"></asp:Label>
                                                )</th>
                                            <th>Instrument Detection Limit (ug/L)</th>
                                            <th>Below Detection? (1=Yes, 0=No)</th>
                                            <th>Final Conc of sample, (<asp:Label ID="Label3" runat="server"></asp:Label>
                                                )</th>
                                            <th>for use in Total (<asp:Label ID="Label4" runat="server"></asp:Label>
                                                )</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>Fluoride, F</td>
                                            <td>
                                                <asp:TextBox ID="txtB17" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtC17" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtD17" runat="server">1</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbAnE17" runat="server"></asp:Label></td>

                                            <td>
                                                <asp:Label ID="lbAnF17" runat="server"></asp:Label></td>
                                            <td>0.5</td>
                                            <td>
                                                <asp:Label ID="lbAnH17" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbAnI17" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbAnJ17" runat="server"></asp:Label></td>

                                        </tr>
                                        <tr>
                                            <td>Chloride, Cl</td>
                                            <td>
                                                <asp:TextBox ID="txtB18" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtC18" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtD18" runat="server">1</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbAnE18" runat="server"></asp:Label></td>

                                            <td>
                                                <asp:Label ID="lbAnF18" runat="server"></asp:Label></td>
                                            <td>0.5</td>
                                            <td>
                                                <asp:Label ID="lbAnH18" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbAnI18" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbAnJ18" runat="server"></asp:Label></td>

                                        </tr>
                                        <tr>
                                            <td>Bromide, Br</td>
                                            <td>
                                                <asp:TextBox ID="txtB19" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtC19" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtD19" runat="server">1</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbAnE19" runat="server"></asp:Label></td>

                                            <td>
                                                <asp:Label ID="lbAnF19" runat="server"></asp:Label></td>
                                            <td>0.5</td>
                                            <td>
                                                <asp:Label ID="lbAnH19" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbAnI19" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbAnJ19" runat="server"></asp:Label></td>

                                        </tr>
                                        <tr>
                                            <td>Nitrate, NO3</td>
                                            <td>
                                                <asp:TextBox ID="txtB20" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtC20" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtD20" runat="server">1</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbAnE20" runat="server"></asp:Label></td>

                                            <td>
                                                <asp:Label ID="lbAnF20" runat="server"></asp:Label></td>
                                            <td>0.5</td>
                                            <td>
                                                <asp:Label ID="lbAnH20" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbAnI20" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbAnJ20" runat="server"></asp:Label></td>

                                        </tr>
                                        <tr>
                                            <td>Sulfate, SO4</td>
                                            <td>
                                                <asp:TextBox ID="txtB21" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtC21" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtD21" runat="server">1</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbAnE21" runat="server"></asp:Label></td>

                                            <td>
                                                <asp:Label ID="lbAnF21" runat="server"></asp:Label></td>
                                            <td>0.5</td>
                                            <td>
                                                <asp:Label ID="lbAnH21" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbAnI21" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbAnJ21" runat="server"></asp:Label></td>

                                        </tr>
                                        <tr>
                                            <td>Phosphate, PO4</td>
                                            <td>
                                                <asp:TextBox ID="txtB22" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtC22" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtD22" runat="server">1</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbAnE22" runat="server"></asp:Label></td>

                                            <td>
                                                <asp:Label ID="lbAnF22" runat="server"></asp:Label></td>
                                            <td>0.5</td>
                                            <td>
                                                <asp:Label ID="lbAnH22" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbAnI22" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbAnJ22" runat="server"></asp:Label></td>

                                        </tr>
                                        <tr>
                                            <td>Total</td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:Label ID="lbAnI23" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbAnJ23" runat="server"></asp:Label></td>

                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <%--2--%>
                        <div class="row">
                            <div class="col-md-12">
                                <table class="table table-striped table-hover table-bordered">
                                    <thead>
                                        <tr>
                                            <th>Cations</th>
                                            <th>Conc of water blank,ug/L (B)</th>
                                            <th>Conc of sample,ug/L (C)</th>
                                            <th>Dilution Factor</th>
                                            <th>Raw Result (<asp:Label ID="Label5" runat="server"></asp:Label>
                                                )</th>

                                            <th>Method Detection Limit (<asp:Label ID="Label6" runat="server"></asp:Label>
                                                )</th>
                                            <th>Instrument Detection Limit (ug/L)</th>
                                            <th>Below Detection? (1=Yes, 0=No)</th>
                                            <th>FinalConc of sample, (<asp:Label ID="Label7" runat="server"></asp:Label>
                                                )</th>
                                            <th>for use in Total (<asp:Label ID="Label8" runat="server"></asp:Label>
                                                )</th>

                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>Lithium, Li</td>
                                            <td>
                                                <asp:TextBox ID="txtB26" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtC26" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtD26" runat="server">1</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbAnE26" runat="server"></asp:Label></td>

                                            <td>
                                                <asp:Label ID="lbAnF26" runat="server"></asp:Label></td>
                                            <td>0.6</td>
                                            <td>
                                                <asp:Label ID="lbAnH26" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbAnI26" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbAnJ26" runat="server"></asp:Label></td>


                                        </tr>
                                        <tr>
                                            <td>Sodium, Na</td>
                                            <td>
                                                <asp:TextBox ID="txtB27" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtC27" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtD27" runat="server">1</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbAnE27" runat="server"></asp:Label></td>

                                            <td>
                                                <asp:Label ID="lbAnF27" runat="server"></asp:Label></td>
                                            <td>0.6</td>
                                            <td>
                                                <asp:Label ID="lbAnH27" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbAnI27" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbAnJ27" runat="server"></asp:Label></td>

                                        </tr>
                                        <tr>
                                            <td>Ammonium, NH4</td>


                                            <td>
                                                <asp:TextBox ID="txtB28" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtC28" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtD28" runat="server">1</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbAnE28" runat="server"></asp:Label></td>

                                            <td>
                                                <asp:Label ID="lbAnF28" runat="server"></asp:Label></td>
                                            <td>0.6</td>
                                            <td>
                                                <asp:Label ID="lbAnH28" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbAnI28" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbAnJ28" runat="server"></asp:Label></td>

                                        </tr>
                                        <tr>
                                            <td>Potassium, K</td>


                                            <td>
                                                <asp:TextBox ID="txtB29" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtC29" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtD29" runat="server">1</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbAnE29" runat="server"></asp:Label></td>

                                            <td>
                                                <asp:Label ID="lbAnF29" runat="server"></asp:Label></td>
                                            <td>0.6</td>
                                            <td>
                                                <asp:Label ID="lbAnH29" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbAnI29" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbAnJ29" runat="server"></asp:Label></td>

                                        </tr>
                                        <tr>
                                            <td>Magnesium, Mg
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtB30" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtC30" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtD30" runat="server">1</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbAnE30" runat="server"></asp:Label></td>

                                            <td>
                                                <asp:Label ID="lbAnF30" runat="server"></asp:Label></td>
                                            <td>0.6</td>
                                            <td>
                                                <asp:Label ID="lbAnH30" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbAnI30" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbAnJ30" runat="server"></asp:Label></td>

                                        </tr>
                                        <tr>
                                            <td>Calcium, Ca
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtB31" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtC31" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtD31" runat="server">1</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbAnE31" runat="server"></asp:Label></td>

                                            <td>
                                                <asp:Label ID="lbAnF31" runat="server"></asp:Label></td>
                                            <td>0.6</td>
                                            <td>
                                                <asp:Label ID="lbAnH31" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbAnI31" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbAnJ31" runat="server"></asp:Label></td>

                                        </tr>
                                        <tr>
                                            <td>Total</td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:Label ID="lbAnI32" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbAnJ32" runat="server"></asp:Label></td>

                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <!-- END FORM-->
                        <%--                        </div>--%>
                    </asp:Panel>

                    <div class="form-actions">

                        <asp:Panel ID="pSpecification" runat="server">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label col-md-3">Component:<span class="required">*</span></label>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="ddlComponent" runat="server" class="select2_category form-control" DataTextField="A" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlComponent_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label col-md-3">Detail Spec:<span class="required">*</span></label>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="ddlDetailSpec" runat="server" class="select2_category form-control" DataTextField="A" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlDetailSpec_SelectedIndexChanged"></asp:DropDownList>
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
                            <asp:Label ID="lbMessage" runat="server" Text=""></asp:Label>
                            <br />
                        </asp:Panel>
                        <br />
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
                                            <asp:TextBox ID="txtDecimal01" runat="server" TextMode="Number" CssClass="form-control" Text="2"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Conc of sample,ug/L (C)</td>
                                        <td>
                                            <asp:TextBox ID="txtDecimal02" runat="server" TextMode="Number" CssClass="form-control" Text="2"></asp:TextBox></td>

                                    </tr>
                                    <tr>
                                        <td>Dilution Factor</td>
                                        <td>
                                            <asp:TextBox ID="txtDecimal03" runat="server" TextMode="Number" CssClass="form-control" Text="2"></asp:TextBox></td>

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
                                        <td>for use in Total</td>
                                        <td>
                                            <asp:TextBox ID="txtDecimal09" runat="server" TextMode="Number" CssClass="form-control" Text="2"></asp:TextBox></td>


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
