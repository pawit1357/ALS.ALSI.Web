<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Seagate_GCMS.ascx.cs" Inherits="ALS.ALSI.Web.view.template.Seagate_GCMS" %>
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
                        <asp:Button ID="btnCoverPage" runat="server" Text="Cover Page" CssClass="btn blue" OnClick="btnCoverPage_Click" />
                        <asp:Button ID="btnRH" runat="server" Text="RHC Base Hub" CssClass="btn blue" OnClick="btnCoverPage_Click" />
                        <asp:Button ID="btnExtractable" runat="server" Text="Workingpg - Extractable" CssClass="btn blue" OnClick="btnCoverPage_Click" />
                        <asp:Button ID="btnMotorOil" runat="server" Text="Workingpg - Motor Oil" CssClass="btn blue" OnClick="btnCoverPage_Click" />
                    </div>
                </div>
                <div class="portlet-body">
                    <asp:Panel ID="pCoverpage" runat="server">
                        <div class="row">
                            <div class="col-md-10">
                                <h5>METHOD/PROCEDURE:</h5>
                                <table class="table table-striped table-hover table-bordered">
                                    <thead>
                                        <tr>
                                            <th>Analysis</th>
                                            <th>Procedure No</th>
                                            <th>Sample Size</th>
                                            <th>Extraction<br />
                                                Medium</th>
                                            <th>Extraction<br />
                                                Vol(ml)</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>

                                            <td>GCMS Extractable</td>
                                            <td>
                                                <asp:TextBox ID="txtProcedure" runat="server" CssClass="form-control"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtSampleSize" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtExtractionMedium" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtExtractionVolumn" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-10">
                                <h6>Results:</h6>
                                <h6>
                                    <asp:Label ID="lbDescription" runat="server" Text=""></asp:Label>
                                </h6>
                                <asp:GridView ID="gvCoverPages" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-striped table-bordered mini" ShowHeaderWhenEmpty="True" ShowFooter="true" DataKeyNames="ID,row_type" OnRowDataBound="gvCoverPages_RowDataBound" OnRowCommand="gvCoverPages_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Motor Oil Contamination" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Literal ID="litMotorOilContamination" runat="server" Text='<%# Eval("A")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Maximum Allowable Amount" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litMaximumAllowableAmout" runat="server" Text='<%# Eval("B")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Results" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litResults" runat="server" Text='<%# Eval("C")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Hide">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnHide" runat="server" ToolTip="Hide" CommandName="Hide" OnClientClick="return confirm('ต้องการซ่อนแถว ?');"
                                                    CommandArgument='<%# Eval("ID")%>'><i class="fa fa-minus"></i></asp:LinkButton>
                                                <asp:LinkButton ID="btnUndo" runat="server" ToolTip="Show" CommandName="Normal" OnClientClick="return confirm('ยกเลิกการซ่อนแถว ?');"
                                                    CommandArgument='<%# Eval("ID")%>'><i class="fa fa-refresh"></i></asp:LinkButton>
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
                        <asp:Label ID="lbRemark1" runat="server" Text="Note: This report was performed test by ALS Singapore."></asp:Label><br />
                        <asp:Label ID="lbRemark2" runat="server" Text=""></asp:Label><br />
                        <asp:Label ID="lbRemark3" runat="server" Text=""></asp:Label>
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


                    <asp:Panel ID="pRH" runat="server">
                        <h4 class="form-section">Manage CAS# Data</h4>
                        <div class="row">
                            <div class="col-md-11">
                                <h6>
                                    <asp:Label ID="lbResultDesc" runat="server" Text=""></asp:Label></h6>
                                <h4>RHC Base</h4>
                                <asp:GridView ID="gvRHCBase" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="ID" OnRowDataBound="gvRHCBase_RowDataBound">
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

                                    </Columns>
                                    <EmptyDataTemplate>
                                        <div class="data-not-found">
                                            <asp:Literal ID="libDataNotFound" runat="server" Text="Data Not found" />
                                        </div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                                <br />
                                <h4>RHC Hub</h4>
                                <asp:GridView ID="gvRHCHub" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="ID" OnRowDataBound="gvRHCHub_RowDataBound">
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

                                    </Columns>
                                    <EmptyDataTemplate>
                                        <div class="data-not-found">
                                            <asp:Literal ID="libDataNotFound" runat="server" Text="Data Not found" />
                                        </div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                                <br />
                                <br />

                                <br></br>


                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pExtractable" runat="server">
                        <div class="row">
                            <div class="col-md-6">
                                <table class="table table-striped table-hover table-bordered">
                                    <tbody>
                                        <tr>
                                            <td>Motor Base / Base</td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Surface area per part  =</td>
                                            <td>
                                                <asp:TextBox ID="txtB13" runat="server"></asp:TextBox></td>
                                            <td>cm<sup>2</sup></td>
                                        </tr>
                                        <tr>
                                            <td>No. of parts extracted = </td>
                                            <td>
                                                <asp:TextBox ID="txtB14" runat="server"></asp:TextBox></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Total Surface area   =</td>
                                            <td>
                                                <asp:Label ID="lbB15" runat="server"></asp:Label></td>
                                            <td>cm<sup>2</sup></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <br />
                                <table class="table table-striped table-hover table-bordered">
                                    <tbody>
                                        <tr>
                                            <td>Motor Hub</td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Surface area per part  =</td>
                                            <td>
                                                <asp:TextBox ID="txtB18" runat="server"></asp:TextBox></td>
                                            <td>cm<sup>2</sup></td>
                                        </tr>
                                        <tr>
                                            <td>No. of parts extracted = </td>
                                            <td>
                                                <asp:TextBox ID="txtB19" runat="server"></asp:TextBox></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Total Surface area   =</td>
                                            <td>
                                                <asp:Label ID="lbB20" runat="server"></asp:Label></td>
                                            <td>cm<sup>2</sup></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <br />
                                <table>
                                    <thead>
                                        <tr>
                                            <th>Total Organic Compound (TOC)</th>
                                            <th>Area ≤ DOP</th>
                                            <th>Area &gt; DOP</th>
                                            </t>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <!-- PART 1 -->
                                        <tr>
                                            <td>Peak Area of Standard (C16H34)</td>
                                            <td>
                                                <asp:TextBox ID="txtB23" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtC23" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Concentration of Standard (ng)</td>
                                            <td>
                                                <asp:TextBox ID="txtB24" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtC24" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Dilution Factor for Hub</td>
                                            <td>
                                                <asp:TextBox ID="txtB25" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtC25" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Dilution Factor for Base 2.5"/3.5"</td>
                                            <td>
                                                <asp:TextBox ID="txtB26" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtC26" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Recovery of internal standard (Hub)</td>
                                            <td>
                                                <asp:TextBox ID="txtB27" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtC27" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Recovery of internal standard (Base)</td>
                                            <td>
                                                <asp:TextBox ID="txtB28" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtC28" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <!-- PART 2 -->
                                        <tr>
                                            <td>Motor Hub</td>
                                            <td>
                                                <asp:TextBox ID="txtB30" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtC30" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Hub - Blank Area</td>
                                            <td>
                                                <asp:TextBox ID="txtB31" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtC31" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Hub (blank substracted))</td>
                                            <td>
                                                <asp:Label ID="lbB32" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbC32" runat="server"></asp:Label></td>
                                        </tr>
                                        <!-- PART 3 -->
                                        <tr>
                                            <td>Motor Base / Base</td>
                                            <td>
                                                <asp:TextBox ID="txtB34" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtC34" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Base - Blank Area</td>
                                            <td>
                                                <asp:TextBox ID="txtB35" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtC35" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Base (blank substracted)</td>
                                            <td>
                                                <asp:Label ID="lbB36" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbC36" runat="server"></asp:Label></td>
                                        </tr>

                                    </tbody>
                                </table>
                                <br />
                                <table>
                                    <thead>
                                        <tr>
                                            <th>Repeated Hydrocarbon (C20 - C40)</th>
                                            <th>Area of RHC</th>
                                        </tr>
                                    </thead>
                                    <tr>
                                        <td>Hub - Repeated Hydrocarbon</td>
                                        <td>
                                            <asp:TextBox ID="txtB39" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Base - Repeated Hydrocarbon</td>
                                        <td>
                                            <asp:TextBox ID="txtB40" runat="server"></asp:TextBox></td>
                                    </tr>
                                </table>
                                <br />
                                <table class="table table-striped table-hover table-bordered">
                                    <tbody>
                                        <tr>
                                            <td></td>
                                            <td>Compounds ≤ DOP (ng/cm<sup>2</sup>)</td>
                                            <td>Compounds > DOP (ng/cm<sup>2</sup>)</td>
                                            <td>Repeated Hydrocarbon(ng/part)</td>
                                        </tr>
                                        <!-- PART 1 -->
                                        <tr>
                                            <td>Motor Hub</td>
                                            <td>
                                                <asp:Label ID="lbB43" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbC43" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbD43" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Motor Base / Base 2.5"/3.5"</td>
                                            <td>
                                                <asp:Label ID="lbB44" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbC44" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbD44" runat="server"></asp:Label></td>
                                        </tr>

                                    </tbody>
                                </table>
                                <br />
                                <table class="table table-striped table-hover table-bordered">
                                    <tbody>
                                        <tr>
                                            <td>Minimum RHC Detection Limit is</td>
                                            <td>
                                                <asp:TextBox ID="txtB47" runat="server"></asp:TextBox></td>
                                            <td>ng/part.</td>
                                        </tr>
                                        <tr>
                                            <td>Minimum RHC Detection Limit of base is</td>
                                            <td>
                                                <asp:TextBox ID="txtB48" runat="server"></asp:TextBox></td>
                                            <td>ng/sqcm</td>
                                        </tr>
                                        <tr>
                                            <td>MMinimum RHC Detection Limit of hub is </td>
                                            <td>
                                                <asp:TextBox ID="txtB49" runat="server"></asp:TextBox></td>
                                            <td>ng/sqcm</td>
                                        </tr>
                                    </tbody>
                                </table>
                                <br />
                                <table class="table table-striped table-hover table-bordered">
                                    <tbody>
                                        <tr>
                                            <td>R-Hub</td>
                                            <td>
                                                <asp:TextBox ID="txtB51" runat="server"></asp:TextBox></td>
                                            <td>R = (X/Y)*(C/A)*D</td>
                                        </tr>
                                        <tr>
                                            <td>X</td>
                                            <td>
                                                <asp:TextBox ID="txtB52" runat="server"></asp:TextBox></td>
                                            <td>Peak area of D10 in sample</td>
                                        </tr>
                                        <tr>
                                            <td>Y</td>
                                            <td>
                                                <asp:TextBox ID="txtB53" runat="server"></asp:TextBox></td>
                                            <td>Peak area of D10 in working standard</td>
                                        </tr>
                                        <tr>
                                            <td>C</td>
                                            <td>
                                                <asp:TextBox ID="txtB54" runat="server"></asp:TextBox></td>
                                            <td>Concentration of D10</td>
                                        </tr>
                                        <tr>
                                            <td>A</td>
                                            <td>
                                                <asp:TextBox ID="txtB55" runat="server"></asp:TextBox></td>
                                            <td>Total concentration spike to sample</td>
                                        </tr>
                                        <tr>
                                            <td>D</td>
                                            <td>
                                                <asp:TextBox ID="txtB56" runat="server"></asp:TextBox></td>
                                            <td>Dilution factor</td>
                                        </tr>
                                        <!-- R-Base --->
                                        <tr>
                                            <td>R-Base</td>
                                            <td>
                                                <asp:TextBox ID="txtB57" runat="server"></asp:TextBox></td>
                                            <td>R = (X/Y)*(C/A)*D</td>
                                        </tr>
                                        <tr>
                                            <td>X</td>
                                            <td>
                                                <asp:TextBox ID="txtB58" runat="server"></asp:TextBox></td>
                                            <td>Peak area of D10 in sample</td>
                                        </tr>
                                        <tr>
                                            <td>Y</td>
                                            <td>
                                                <asp:TextBox ID="txtB59" runat="server"></asp:TextBox></td>
                                            <td>Peak area of D10 in working standard</td>
                                        </tr>
                                        <tr>
                                            <td>C</td>
                                            <td>
                                                <asp:TextBox ID="txtB60" runat="server"></asp:TextBox></td>
                                            <td>Concentration of D10</td>
                                        </tr>
                                        <tr>
                                            <td>A</td>
                                            <td>
                                                <asp:TextBox ID="txtB61" runat="server"></asp:TextBox></td>
                                            <td>Total concentration spike to sample</td>
                                        </tr>
                                        <tr>
                                            <td>D</td>
                                            <td>
                                                <asp:TextBox ID="txtB62" runat="server"></asp:TextBox></td>
                                            <td>Dilution factor</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pMotorOil" runat="server">
                        <div class="row">
                            <div class="col-md-6">
                                <br />
                                <table class="table table-striped table-hover table-bordered">
                                    <tbody>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:DropDownList ID="ddlBaseType" runat="server" CssClass="select2_category form-control">
                                                    <asp:ListItem Value="0">-</asp:ListItem>
                                                    <asp:ListItem Value="1">2.5</asp:ListItem>
                                                    <asp:ListItem Value="2">3.5</asp:ListItem>
                                                </asp:DropDownList>

                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Surface area of Base  =</td>
                                            <td>
                                                <asp:TextBox ID="txtB13_MO" runat="server"></asp:TextBox></td>
                                            <td>cm<sup>2</sup></td>
                                        </tr>
                                        <tr>

                                            <td>Surface area of Hub  =</td>
                                            <td>
                                                <asp:TextBox ID="txtB14_MO" runat="server"></asp:TextBox></td>
                                            <td>cm<sup>2</sup></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <br />
                                <table class="table table-striped table-hover table-bordered">
                                    <tbody>
                                        <tr>
                                            <td>M/Z</td>
                                            <td>
                                                <asp:TextBox ID="txtB17_MO" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtC17_MO" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtD17_MO" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Retention Time</td>
                                            <td>
                                                <asp:TextBox ID="txtB18_MO" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtC18_MO" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtD18_MO" runat="server"></asp:TextBox></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table class="table table-striped table-hover table-bordered">
                                    <thead>
                                        <tr>
                                            <td>Motor Oil Compound</td>
                                            <td>Area</td>
                                            <td>Area</td>
                                            <td>Area</td>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>Peak Area of Motor Oil</td>
                                            <td>
                                                <asp:TextBox ID="txtB20_MO" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtC20_MO" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtD20_MO" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Concentration of Standard (ng)</td>
                                            <td>
                                                <asp:TextBox ID="txtB21_MO" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtC21_MO" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtD21_MO" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Dilution Factor for Hub</td>
                                            <td>
                                                <asp:TextBox ID="txtB22_MO" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtC22_MO" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtD22_MO" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Dilution Factor for Base 2.5" / 3.5</td>
                                            <td>
                                                <asp:TextBox ID="txtB23_MO" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtC23_MO" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtD23_MO" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Motor Hub</td>
                                            <td>
                                                <asp:TextBox ID="txtB26_MO" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtC26_MO" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtD26_MO" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Motor Base / Base 2.5&quot; / 3.5&quot;&quot;</td>
                                            <td>
                                                <asp:TextBox ID="txtB28_MO" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtC28_MO" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtD28_MO" runat="server"></asp:TextBox></td>

                                        </tr>

                                    </tbody>
                                </table>
                                <br />
                                <table class="table table-striped table-hover table-bordered">
                                    <thead>
                                        <tr>
                                            <td>Motor Oil Comtamination</td>
                                            <td>Result (ng/cm2)</td>
                                            <td>Result (ng/cm2)</td>
                                            <%--                                            <td>Result (ng/cm2)</td>--%>
                                            <td>Total (ng/cm2)</td>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>Hub</td>
                                            <td>
                                                <asp:Label ID="lbB30_MO" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbC30_MO" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbD30_MO" runat="server"></asp:Label></td>
                                            <%--  <td>
                                                <asp:Label ID="lbE30_MO" runat="server"></asp:Label></td>--%>
                                        </tr>
                                        <tr>
                                            <td>Motor Base / Base 2.5"/ 3.5"</td>
                                            <td>
                                                <asp:Label ID="lbB31_MO" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbC31_MO" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbD31_MO" runat="server"></asp:Label></td>
                                            <%--               <td>
                                                <asp:Label ID="lbE31_MO" runat="server"></asp:Label></td>--%>
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
                                  <%--      <div class="row">
                                            <div class="col-md-6">--%>
                                                <div class="form-group">
                                                    <label class="control-label col-md-3">Component:<span class="required">*</span></label>
                                                    <div class="col-md-6">
                                                        <asp:DropDownList ID="ddlComponent" runat="server" CssClass="select2_category form-control" DataTextField="A" DataValueField="ID" OnSelectedIndexChanged="ddlComponent_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                    </div>
                                                </div>
                                        <%--    </div>
                                        </div>--%>

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
                                         <%--   </div>
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
                                    <%--    <div class="row">
                                            <div class="col-md-6">--%>
                                                <div class="form-group">
                                                    <label class="control-label col-md-3">Assign To:<span class="required">*</span></label>
                                                    <div class="col-md-6">
                                                        <asp:DropDownList ID="ddlAssignTo" runat="server" class="select2_category form-control" DataTextField="name" DataValueField="ID" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                </div>
                                         <%--   </div>
                                        </div>--%>
                                        <br />
                                    </asp:Panel>
                                    <asp:Panel ID="pDownload" runat="server">
                                    <%--    <div class="row">
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
                                    <h3>Workingpg-Extractable</h3>
                                    <table class="table table-striped">
                                        <tr>
                                            <td>Recovery of internal standard (Hub)</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal01" runat="server" TextMode="Number" CssClass="form-control" Text="2"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Recovery of internal standard (Base)</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal02" runat="server" TextMode="Number" CssClass="form-control" Text="2"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Compunds <= DOP	</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal03" runat="server" TextMode="Number" CssClass="form-control" Text="2"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Compunds >= DOP	</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal04" runat="server" TextMode="Number" CssClass="form-control" Text="2"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Repeated Hydrocarcon</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal05" runat="server" TextMode="Number" CssClass="form-control" Text="2"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Minimum RHC Detection Limit</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal06" runat="server" TextMode="Number" CssClass="form-control" Text="3"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>R-Hub</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal07" runat="server" TextMode="Number" CssClass="form-control" Text="2"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>R-Base</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal08" runat="server" TextMode="Number" CssClass="form-control" Text="2"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                    <h3>Workingpg-Motor Oil</h3>
                                    <table class="table table-striped">
                                        <tr>
                                            <td>Surface area of Base</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal09" runat="server" TextMode="Number" CssClass="form-control" Text="2"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Surface area of Hub</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal10" runat="server" TextMode="Number" CssClass="form-control" Text="2"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>M/Z</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal11" runat="server" TextMode="Number" CssClass="form-control" Text="0"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Retention Time</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal12" runat="server" TextMode="Number" CssClass="form-control" Text="0"></asp:TextBox></td>
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

        </Triggers>
    </asp:UpdatePanel>
    <%--EDIT--%>
</form>

<!-- BEGIN PAGE LEVEL SCRIPTS -->
<script src="<%= ResolveUrl("~/assets/global/plugins/jquery.min.js") %>" type="text/javascript"></script>
<!-- END PAGE LEVEL SCRIPTS -->
<script>
    jQuery(document).ready(function () {

    });
</script>
<!-- END JAVASCRIPTS -->
