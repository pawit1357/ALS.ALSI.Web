<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Seagate_HPA_Siam.ascx.cs" Inherits="ALS.ALSI.Web.view.template.Seagate_HPA_Siam" %>
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
                        <asp:Button ID="btnCoverPage" runat="server" Text="Cover Page" CssClass="btn green" OnClick="btnUsLPC_Click" />
                        <asp:Button ID="btnWorksheetForHPAFiltration" runat="server" Text="Worksheet for HPA - Filtration" CssClass="btn blue" OnClick="btnUsLPC_Click" />
                        <asp:Button ID="btnUsLPC03" runat="server" Text="US-LPC(0.3)" CssClass="btn blue" OnClick="btnUsLPC_Click" />
                        <asp:Button ID="btnUsLPC06" runat="server" Text="US-LPC(0.6)" CssClass="btn blue" OnClick="btnUsLPC_Click" />
                        <asp:LinkButton ID="btnShowUnit" runat="server" OnClick="LinkButton1_Click" CssClass="btn btn-default"> <i class="fa fa-sort-numeric-asc"></i> ตั้งค่า</asp:LinkButton>

                    </div>
                </div>
                <div class="portlet-body">
                    <asp:Panel ID="pCoverPage" runat="server">
                        <%--                        <asp:Literal ID="LitErrorMsg2" runat="server"></asp:Literal>--%>

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
                                                <asp:DropDownList ID="ddlLpcType" runat="server" AutoPostBack="True" CssClass="select2_category form-control" OnSelectedIndexChanged="ddlLpcType_SelectedIndexChanged">
                                                    <asp:ListItem Value="1">LPC (68 KHz)</asp:ListItem>
                                                    <asp:ListItem Value="2">LPC (132 KHz)</asp:ListItem>
                                                    <asp:ListItem Value="3">LPC (ALPC 132 KHz)</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtProcedureNo" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNumberOfPieces" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtenderReceiptinAc" TargetControlID="txtNumberOfPieces"
                                                    FilterType="Custom, Numbers" ValidChars="." runat="server" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtExtractionMedium" runat="server" Text="0.1 um Filtered Degassed DI Water" CssClass="form-control"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtExtractionVolume" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtExtractionVolume"
                                                    FilterType="Custom, Numbers" ValidChars="." runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbA20" runat="server" Text="HPA (Filtration Method)"></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtProcedureNo_hpa" runat="server" Text="" CssClass="form-control"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtNumberOfPieces_hpa" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtNumberOfPieces_hpa"
                                                    FilterType="Custom, Numbers" ValidChars="." runat="server" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtExtractionMedium_hpa" runat="server" Text="0.1 um Filtered Degassed DI Water" CssClass="form-control"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtExtractionVolume_hpa" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtExtractionVolume_hpa"
                                                    FilterType="Custom, Numbers" ValidChars="." runat="server" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-9">
                                <div class="row">
                                    <div class="col-md-6">
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
                                        <%--            <h6>The Specification is based on Seagate's Doc
                            <asp:Label ID="lbDocNo" runat="server" Text=""></asp:Label>
                                            for
                            <asp:Label ID="lbCommodity" runat="server" Text=""></asp:Label>
                                        </h6>--%>
                                    </div>
                                </div>
                                <asp:GridView ID="gvLpc03" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-striped table-bordered mini" ShowHeaderWhenEmpty="True" ShowFooter="true" DataKeyNames="id,row_type" OnRowDataBound="gvLpc03_RowDataBound" OnRowCommand="gvLpc03_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Liquid Particle Count (68 KHz)," ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Literal ID="litA" runat="server" Text='<%# Eval("A")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specification Limit,(Counts/cm2)" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litB" runat="server" Text='<%# Eval("B")%>'></asp:Literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Results,(Counts/cm2)" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="litC" runat="server" Text='<%# Eval("C")%>'></asp:Label>
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
                                <asp:GridView ID="gvLpc06" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-striped table-bordered mini" ShowHeaderWhenEmpty="True" ShowFooter="true" DataKeyNames="id,row_type" OnRowDataBound="gvLpc06_RowDataBound" OnRowCommand="gvLpc06_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Liquid Particle Count (68 KHz)," ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Literal ID="litA" runat="server" Text='<%# Eval("A")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specification Limit,(Counts/cm2)" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litB" runat="server" Text='<%# Eval("B")%>'></asp:Literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Results,(Counts/cm2)" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="litC" runat="server" Text='<%# Eval("C")%>'></asp:Label>
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
                                <asp:GridView ID="gvHpa" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-striped table-bordered mini" ShowHeaderWhenEmpty="True" ShowFooter="true" DataKeyNames="id,row_type" OnRowDataBound="gvHpa_RowDataBound" OnRowCommand="gvHpa_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Hard Particle Analysis(LPC 68 KHz)" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Literal ID="litA" runat="server" Text='<%# Eval("A")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specification Limit,(particles/cm2)" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litB" runat="server" Text='<%# Eval("B")%>'></asp:Literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Results,(Counts/cm2)" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="litC" runat="server" Text='<%# Eval("C")%>'></asp:Label>
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
                                <asp:GridView ID="gvClassification" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-striped table-bordered mini" ShowHeaderWhenEmpty="True" ShowFooter="true" DataKeyNames="id,row_type" OnRowDataBound="gvClassification_RowDataBound" OnRowCommand="gvClassification_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Classification" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Literal ID="litA" runat="server" Text='<%# Eval("A")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type of Particles" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Literal ID="litB" runat="server" Text='<%# Eval("B")%>'></asp:Literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Results, Particle/sqcm" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="litC" runat="server" Text='<%# Eval("C")%>'></asp:Label>
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
                                <table class="table table-striped table-bordered mini">
                                    <thead></thead>

                                    <tr runat="server" id="tr103">
                                        <td>Analysis Details</td>
                                        <td class="auto-style4">% Area Analysed (A/7.07mm2)</td>
                                        <td runat="server" id="td103">
                                            <asp:Label ID="lbC144" runat="server" />
                                        </td>
                                    </tr>
                                    <tr runat="server" id="tr104">
                                        <td></td>
                                        <td>Area Analysed (mm2)</td>
                                        <td runat="server" id="td104">
                                            <asp:Label ID="lbC145" runat="server" />
                                        </td>
                                    </tr>
                                    <tr runat="server" id="tr105">
                                        <td></td>
                                        <td>Extraction Volume (mL)</td>
                                        <td runat="server" id="td105">
                                            <asp:Label ID="lbC146" runat="server" />
                                        </td>
                                    </tr>
                                    <tr runat="server" id="tr106">
                                        <td></td>
                                        <td>Filtered Volume (mL)</td>
                                        <td runat="server" id="td106">
                                            <asp:Label ID="lbC147" runat="server" />
                                        </td>
                                    </tr>

                                    <tr runat="server" id="tr107">
                                        <td></td>
                                        <td>No of Parts Extracted</td>
                                        <td runat="server" id="td107">
                                            <asp:Label ID="lbC148" runat="server" />
                                        </td>
                                    </tr>

                                    <tr runat="server" id="tr108">
                                        <td></td>
                                        <td>Magnification</td>
                                        <td runat="server" id="td108">
                                            <asp:TextBox ID="lbC149" runat="server" Text="448"> </asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" TargetControlID="lbC149"
                                                FilterType="Custom, Numbers" ValidChars="." runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <asp:GridView ID="gvTypesOfParticles" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-striped table-bordered mini" ShowHeaderWhenEmpty="True" ShowFooter="true" DataKeyNames="id,row_type">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Types of Particles" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Literal ID="litB" runat="server" Text='<%# Eval("B")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Number of Particle  (Raw Count)" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litRawCounts" runat="server" Text='<%# Eval("RawCounts")%>'></asp:Literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Number of Particle (Particle/sqcm)" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="litC" runat="server" Text='<%# Eval("C")%>'></asp:Label>
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
                                <asp:Label ID="lbImgPath1" runat="server" Text=""></asp:Label></h5>
                                <br />
                                <asp:Image ID="img1" runat="server" Width="120" Height="120" />
                            </div>
                        </div>
                    </asp:Panel>

                    <br />
                    <%--US-LPC(0.3)--%>
                    <asp:Panel ID="pUS_LPC03" runat="server">
                        <div class="row">
                            <div class="col-md-6">
                                <table class="table table-striped table-hover small" id="tb1" runat="server">
                                    <thead>
                                        <tr>
                                            <th></th>
                                            <th colspan="6">No. of Particles ≥ 0.3μm (Counts/ml)</th>
                                        </tr>
                                        <tr>
                                            <th>RUN</th>
                                            <th>Blank 1</th>
                                            <th>Sample 1</th>
                                            <th>Blank 2</th>
                                            <th>Sample 2</th>
                                            <th>Blank 3</th>
                                            <th>Sample 3</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr runat="server" id="tr1">
                                            <td>1</td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC03_B14" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC03_C14" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC03_D14" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC03_E14" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC03_F14" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC03_G14" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                        </tr>
                                        <tr runat="server" id="tr2">
                                            <td>2</td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC03_B15" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC03_C15" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC03_D15" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC03_E15" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC03_F15" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC03_G15" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                        </tr>
                                        <tr runat="server" id="tr3">
                                            <td>3</td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC03_B16" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC03_C16" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC03_D16" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC03_E16" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC03_F16" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC03_G16" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                        </tr>
                                        <tr runat="server" id="tr4">
                                            <td>4</td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC03_B17" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC03_C17" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC03_D17" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC03_E17" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC03_F17" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC03_G17" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                        </tr>
                                        <tr runat="server" id="tr5">
                                            <td>Average of last 3</td>
                                            <td>
                                                <asp:Label ID="txt_UsLPC03_B18" runat="server" CssClass="m-wrap small"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="txt_UsLPC03_C18" runat="server" CssClass="m-wrap small"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="txt_UsLPC03_D18" runat="server" CssClass="m-wrap small"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="txt_UsLPC03_E18" runat="server" CssClass="m-wrap small"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="txt_UsLPC03_F18" runat="server" CssClass="m-wrap small"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="txt_UsLPC03_G18" runat="server" CssClass="m-wrap small"></asp:Label></td>
                                        </tr>
                                        <tr runat="server" id="tr6">
                                            <td>Extraction Vol. (ml)</td>
                                            <td colspan="6">
                                                <asp:Label ID="txt_UsLPC03_B20" runat="server" CssClass="m-wrap small"></asp:Label></td>
                                        </tr>
                                        <tr runat="server" id="tr7">
                                            <td>Surface Area (cm2)</td>
                                            <td colspan="6">
                                                <asp:Label ID="txt_UsLPC03_B21" runat="server" CssClass="m-wrap small"></asp:Label></td>
                                        </tr>
                                        <tr runat="server" id="tr8">
                                            <td>No. of Parts Used</td>
                                            <td colspan="6">
                                                <asp:Label ID="txt_UsLPC03_B22" runat="server" CssClass="m-wrap small"></asp:Label></td>
                                        </tr>
                                        <tr runat="server" id="tr9">
                                            <td>Sample</td>
                                            <td colspan="2">1</td>
                                            <td colspan="2">2</td>
                                            <td colspan="2">3</td>
                                        </tr>
                                        <tr runat="server" id="tr10">
                                            <td>No. of Particles ≥ 0.3µm<br />
                                                (Counts/cm2)</td>
                                            <td colspan="2">
                                                <asp:Label ID="txt_UsLPC03_B25_1" runat="server" CssClass="m-wrap small"></asp:Label></td>
                                            <td colspan="2">
                                                <asp:Label ID="txt_UsLPC03_D25" runat="server" CssClass="m-wrap small"></asp:Label></td>
                                            <td colspan="2">
                                                <asp:Label ID="txt_UsLPC03_F25" runat="server" CssClass="m-wrap small"></asp:Label></td>
                                        </tr>
                                        <tr runat="server" id="tr11">
                                            <td>Average</td>
                                            <td colspan="6">
                                                <asp:Label ID="txt_UsLPC03_B26" runat="server" CssClass="m-wrap small"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>

                    </asp:Panel>
                    <%--US-LPC(0.6)--%>
                    <asp:Panel ID="pUS_LPC06" runat="server">
                        <div class="row">
                            <div class="col-md-6">
                                <table class="table table-striped table-hover small" id="Table1" runat="server">
                                    <thead>
                                        <tr>
                                            <th></th>
                                            <th colspan="6">No. of Particles ≥ 0.6μm (Counts/ml)</th>
                                        </tr>
                                        <tr>
                                            <th>RUN</th>
                                            <th>Blank 1</th>
                                            <th>Sample 1</th>
                                            <th>Blank 2</th>
                                            <th>Sample 2</th>
                                            <th>Blank 3</th>
                                            <th>Sample 3</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr runat="server" id="tr12">
                                            <td>1</td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC06_B14" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC06_C14" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC06_D14" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC06_E14" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC06_F14" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC06_G14" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                        </tr>
                                        <tr runat="server" id="tr13">
                                            <td>2</td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC06_B15" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC06_C15" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC06_D15" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC06_E15" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC06_F15" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC06_G15" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                        </tr>
                                        <tr runat="server" id="tr14">
                                            <td>3</td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC06_B16" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC06_C16" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC06_D16" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC06_E16" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC06_F16" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC06_G16" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                        </tr>
                                        <tr runat="server" id="tr15">
                                            <td>4</td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC06_B17" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC06_C17" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC06_D17" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC06_E17" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC06_F17" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txt_UsLPC06_G17" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                        </tr>
                                        <tr runat="server" id="tr16">
                                            <td>Average of last 3</td>
                                            <td>
                                                <asp:Label ID="txt_UsLPC06_B18" runat="server" CssClass="m-wrap small"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="txt_UsLPC06_C18" runat="server" CssClass="m-wrap small"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="txt_UsLPC06_D18" runat="server" CssClass="m-wrap small"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="txt_UsLPC06_E18" runat="server" CssClass="m-wrap small"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="txt_UsLPC06_F18" runat="server" CssClass="m-wrap small"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="txt_UsLPC06_G18" runat="server" CssClass="m-wrap small"></asp:Label></td>
                                        </tr>
                                        <tr runat="server" id="tr17">
                                            <td>Extraction Vol. (ml)</td>
                                            <td colspan="6">
                                                <asp:Label ID="txt_UsLPC06_B20" runat="server" CssClass="m-wrap small"></asp:Label></td>
                                        </tr>
                                        <tr runat="server" id="tr18">
                                            <td>Surface Area (cm2)</td>
                                            <td colspan="6">
                                                <asp:Label ID="txt_UsLPC06_B21" runat="server" CssClass="m-wrap small"></asp:Label></td>
                                        </tr>
                                        <tr runat="server" id="tr19">
                                            <td>No. of Parts Used</td>
                                            <td colspan="6">
                                                <asp:Label ID="txt_UsLPC06_B22" runat="server" CssClass="m-wrap small"></asp:Label></td>
                                        </tr>
                                        <tr runat="server" id="tr20">
                                            <td>Sample</td>
                                            <td colspan="2">1</td>
                                            <td colspan="2">2</td>
                                            <td colspan="2">3</td>
                                        </tr>
                                        <tr runat="server" id="tr21">
                                            <td>No. of Particles ≥ 0.6µm<br />
                                                (Counts/cm2)</td>
                                            <td colspan="2">
                                                <asp:Label ID="txt_UsLPC06_B25" runat="server" CssClass="m-wrap small"></asp:Label></td>
                                            <td colspan="2">
                                                <asp:Label ID="txt_UsLPC06_D25" runat="server" CssClass="m-wrap small"></asp:Label></td>
                                            <td colspan="2">
                                                <asp:Label ID="txt_UsLPC06_F25" runat="server" CssClass="m-wrap small"></asp:Label></td>
                                        </tr>
                                        <tr runat="server" id="tr22">
                                            <td>Average</td>
                                            <td colspan="6">
                                                <asp:Label ID="txt_UsLPC06_B26" runat="server" CssClass="m-wrap small"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>

                    </asp:Panel>
                    <%--Worksheet for HPA - Filtration--%>
                    <asp:Panel ID="pWorksheetForHPAFiltration" runat="server">

                        <asp:Panel ID="pLoadFile" runat="server">

                            <div class="form-group">
                                <label class="control-label col-md-3">Volume of Extraction (ml), Vt</label>
                                <div class="col-md-9">
                                    <div class="fileinput fileinput-new" data-provides="fileinput">
                                        <asp:TextBox ID="txtB3" runat="server" CssClass="m-wrap small">900</asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="txtB3"
                                            FilterType="Custom, Numbers" ValidChars="." runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">Surface Area (cm2), C</label>
                                <div class="col-md-9">
                                    <div class="fileinput fileinput-new" data-provides="fileinput">
                                        <asp:TextBox ID="txtB4" runat="server" CssClass="m-wrap small" AutoPostBack="True" OnTextChanged="txtB4_TextChanged">0.7322566</asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" TargetControlID="txtB4"
                                            FilterType="Custom, Numbers" ValidChars="." runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">Number of Parts Extracted, N</label>
                                <div class="col-md-9">
                                    <div class="fileinput fileinput-new" data-provides="fileinput">
                                        <asp:TextBox ID="txtB5" runat="server" CssClass="m-wrap small">10</asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" TargetControlID="txtB5"
                                            FilterType="Custom, Numbers" ValidChars="." runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">Volume of Filtration (ml), Vf</label>
                                <div class="col-md-9">
                                    <div class="fileinput fileinput-new" data-provides="fileinput">
                                        <asp:TextBox ID="txtB6" runat="server" CssClass="m-wrap small">50</asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" TargetControlID="txtB6"
                                            FilterType="Custom, Numbers" ValidChars="." runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">Filter Area (sqmm), At</label>
                                <div class="col-md-9">
                                    <div class="fileinput fileinput-new" data-provides="fileinput">
                                        <asp:TextBox ID="txtB7" runat="server" CssClass="m-wrap small" AutoPostBack="True" OnTextChanged="txtB8_TextChanged">7.071</asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" TargetControlID="txtB7"
                                            FilterType="Custom, Numbers" ValidChars="." runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">Percent Area Coverage (%)</label>
                                <div class="col-md-9">
                                    <div class="fileinput fileinput-new" data-provides="fileinput">
                                        <asp:Label ID="txtB8" runat="server" CssClass="text-info" Text="0"></asp:Label>
                                        <%--         <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" TargetControlID="txtB8"
                                            FilterType="Custom, Numbers" ValidChars="." runat="server" />--%>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">Area of Filter Surveyed (sqmm), As</label>
                                <div class="col-md-9">
                                    <div class="fileinput fileinput-new" data-provides="fileinput">
                                        <asp:TextBox ID="txtB9" runat="server" CssClass="m-wrap small" AutoPostBack="True" OnTextChanged="txtB8_TextChanged">3.55</asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" TargetControlID="txtB9"
                                            FilterType="Custom, Numbers" ValidChars="." runat="server" />
                                    </div>
                                </div>
                            </div>


                            <div class="form-group">
                                <label class="control-label col-md-3">Select Source File: </label>

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
                                            <asp:Label ID="lbMessage" runat="server"></asp:Label>
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
                        <div class="row">
                            <div class="col-md-9">

                                <br />
                                <asp:GridView ID="gvWsClassification" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-striped table-bordered mini" ShowHeaderWhenEmpty="True" ShowFooter="true" DataKeyNames="id,row_type,hpa_type" OnRowDataBound="gvWsClassification_RowDataBound">
                                    <Columns>
                                        <%--                                        <asp:TemplateField HeaderText="Classification" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Literal ID="litA" runat="server" Text='<%# Eval("A")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Type of Particles" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Literal ID="litB" runat="server" Text='<%# Eval("B")%>'></asp:Literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Blank Counts (B)" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litBC" runat="server" Text='<%# Eval("BlankCouts")%>'></asp:Literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Raw Counts (Y)" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="litRC" runat="server" Text='<%# Eval("RawCounts")%>'></asp:Literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Counts/sqcm (X)" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="litC" runat="server" Text='<%# Eval("C")%>'></asp:Label>
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
                                        <%--    <div class="row">
                                            <div class="col-md-6">--%>
                                        <div class="form-group">
                                            <label class="control-label col-md-3">Detail Spec:<span class="required">*</span></label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlSpecification" runat="server" CssClass="select2_category form-control" DataTextField="A" DataValueField="ID" OnSelectedIndexChanged="ddlSpecification_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--          </div>
                                        </div>--%>

                                        <br />
                                    </asp:Panel>
                                    <asp:Panel ID="pStatus" runat="server">
                                        <%--        <div class="row">
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
                                        <%--        <div class="row">
                                            <div class="col-md-6">--%>
                                        <div class="form-group">
                                            <label class="control-label col-md-3">Remark:<span class="required">*</span></label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%--   </div>
                                        </div>--%>
                                        <br />
                                    </asp:Panel>
                                    <asp:Panel ID="pDisapprove" runat="server">
                                        <%--       <div class="row">
                                            <div class="col-md-6">--%>
                                        <div class="form-group">
                                            <label class="control-label col-md-3">Assign To:<span class="required">*</span></label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlAssignTo" runat="server" CssClass="select2_category form-control" DataTextField="name" DataValueField="ID" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--     </div>
                                        </div>--%>
                                        <br />
                                    </asp:Panel>
                                    <asp:Panel ID="pDownload" runat="server">
                                        <%--     <div class="row">
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
                                        <%--       </div>
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
                                </div>
                            </div>
                            <!-- END Portlet PORTLET-->
                        </div>
                    </div>

                    <div class="form-actions">
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
                                        <%--<asp:Button ID="btnCalculate" runat="server" Text="Calculate" CssClass="btn green" OnClick="btnCalculate_Click" />--%>
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
                                            กำหนดทศนิยม</h>
                            </div>
                            <div class="modal-body" style="width: 600px; height: 400px; overflow-x: hidden; overflow-y: scroll; padding-bottom: 10px;">
                                <h1>Unit</h1>
                                <table class="table table-striped">

                                    <tr>
                                        <th>Liquid Particle Unit</th>
                                        <th>
                                            <asp:DropDownList ID="ddlLiquidParticleUnit" runat="server" class="select2_category form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlLiquidParticleUnit_SelectedIndexChanged" DataValueField="ID" DataTextField="Name">
                                            </asp:DropDownList>
                                        </th>
                                    </tr>
                                    <tr>
                                        <th>Hard Particle Alalysis Unit</th>
                                        <th>
                                            <asp:DropDownList ID="ddlHardParticleAlalysisUnit" runat="server" class="select2_category form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlHardParticleAlalysisUnit_SelectedIndexChanged" DataValueField="ID" DataTextField="Name">
                                            </asp:DropDownList>
                                        </th>
                                    </tr>
                                    <tr>
                                        <th>Classification Unit</th>
                                        <th>
                                            <asp:DropDownList ID="ddlClassificationUnit" runat="server" class="select2_category form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlClassificationUnit_SelectedIndexChanged" DataValueField="ID" DataTextField="Name">
                                            </asp:DropDownList>
                                        </

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
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnLoadFile" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="lbDownload" />
            <asp:PostBackTrigger ControlID="lbDownloadPdf" />
        </Triggers>
    </asp:UpdatePanel>
</form>
