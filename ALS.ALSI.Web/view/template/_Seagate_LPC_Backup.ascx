<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="_Seagate_LPC_Backup.ascx.cs" Inherits="ALS.ALSI.Web.view.template.Seagate_LPC_Backup" %>
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
                                                        <asp:DropDownList ID="ddlA19" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlA19_SelectedIndexChanged" CssClass="select2_category form-control">
                                                            <asp:ListItem Value="1">LPC (68 KHz)</asp:ListItem>
                                                            <asp:ListItem Value="2">LPC (132 KHz)</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtB19" runat="server" Text="20800040-001  Rev.AC" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCVP_C19" runat="server" Text="1" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtD19" runat="server" Text="0.1 um Filtered Degassed DI Water" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCVP_E19" runat="server" Text="500" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-9">
                                        <h6>Results:</h6>
                                        <h6>The Specification is based on Seagate's Doc .
                            <asp:Label ID="lbDocNo" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="lbDocRev" runat="server" Text=""></asp:Label>for
                            <asp:Label ID="lbCommodity" runat="server" Text=""></asp:Label>
                                        </h6>
                                    </div>
                                </div>

                                <%-- Liquid Particle Count (68 KHz) 0.3 --%>
                                <div class="row">
                                    <div class="col-md-9">
                                        <table class="table table-striped table-hover" id="tb1_1" runat="server">
                                            <thead>

                                                <tr>
                                                    <th>Liquid Particle Count (68 KHz)</th>
                                                    <th>Specification Limits<br />
                                                        (<asp:Label ID="lbUnit1" runat="server" Text="" />)</th>
                                                    <th>Results<br />
                                                        (<asp:Label ID="lbUnit2" runat="server" Text="" />)</th>
                                                    <th runat="server" id="th1">
                                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked="True" />
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>

                                                <tr runat="server" id="tr23">
                                                    <td>
                                                        <asp:Label ID="Label4" runat="server" Text="Total number of particles ≥ 0.3μm"></asp:Label></td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td runat="server" id="td1">&nbsp;</td>
                                                </tr>
                                                <tr runat="server" id="tr24">
                                                    <td>
                                                        <asp:Label ID="Label5" runat="server" Text="1st Run"></asp:Label></td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="lbC25" runat="server" Text="" /></td>
                                                    <td runat="server" id="td2">&nbsp;</td>
                                                </tr>
                                                <tr runat="server" id="tr25">
                                                    <td>
                                                        <asp:Label ID="Label8" runat="server" Text="2nd Run"></asp:Label></td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="lbC26" runat="server" Text="" /></td>
                                                    <td runat="server" id="td3">&nbsp;</td>
                                                </tr>
                                                <tr runat="server" id="tr26">
                                                    <td>
                                                        <asp:Label ID="Label18" runat="server" Text="3rd Run"></asp:Label></td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="lbC27" runat="server" Text="" /></td>
                                                    <td runat="server" id="td4">&nbsp;</td>
                                                </tr>
                                                <tr runat="server" id="tr27">
                                                    <td>
                                                        <asp:Label ID="Label21" runat="server" Text="Average"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbB28" runat="server" Text="" /></td>
                                                    <td>
                                                        <asp:Label ID="lbC28" runat="server" Text="" /></td>
                                                    <td runat="server" id="td5">&nbsp;</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <%-- Liquid Particle Count (68 KHz) 0.6 --%>
                                <div class="row">
                                    <div class="col-md-9">
                                        <table class="table table-striped table-hover" id="tb2" runat="server">
                                            <thead>

                                                <tr>
                                                    <th>Liquid Particle Count (68 KHz)</th>
                                                    <th>Specification Limits<br />
                                                        (<asp:Label ID="lbUnit3" runat="server" Text="" />)</th>
                                                    <th>Results<br />
                                                        (<asp:Label ID="lbUnit4" runat="server" Text="" />)</th>
                                                    <th runat="server" id="th2">
                                                        <asp:CheckBox ID="CheckBox2" runat="server" Checked="True" />
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>

                                                <tr runat="server" id="tr28">
                                                    <td>
                                                        <asp:Label ID="Label9" runat="server" Text="Total number of particles ≥ 0.6μm"></asp:Label></td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td runat="server" id="td6">&nbsp;</td>
                                                </tr>
                                                <tr runat="server" id="tr29">
                                                    <td>
                                                        <asp:Label ID="Label10" runat="server" Text="1st Run"></asp:Label></td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="lbC32" runat="server" Text="" /></td>
                                                    <td runat="server" id="td7">&nbsp;</td>
                                                </tr>
                                                <tr runat="server" id="tr30">
                                                    <td>
                                                        <asp:Label ID="Label12" runat="server" Text="2nd Run"></asp:Label></td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="lbC33" runat="server" Text="" /></td>
                                                    <td runat="server" id="td8">&nbsp;</td>
                                                </tr>
                                                <tr runat="server" id="tr31">
                                                    <td>
                                                        <asp:Label ID="Label14" runat="server" Text="3rd Run"></asp:Label></td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="lbC34" runat="server" Text="" /></td>
                                                    <td runat="server" id="td9">&nbsp;</td>
                                                </tr>
                                                <tr runat="server" id="tr32">
                                                    <td>
                                                        <asp:Label ID="Label16" runat="server" Text="Average"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbB35" runat="server" Text="" /></td>
                                                    <td>
                                                        <asp:Label ID="lbC35" runat="server" Text="" /></td>
                                                    <td runat="server" id="td10">&nbsp;</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <%-- Liquid Particle Count (132 KHz) 0.3 --%>
                                <div class="row">
                                    <div class="col-md-9">
                                        <table class="table table-striped table-hover" id="tb3" runat="server">
                                            <thead>

                                                <tr>
                                                    <th>Liquid Particle Count (132 KHz)</th>
                                                    <th>Specification Limits<br />
                                                        (<asp:Label ID="lbUnit5" runat="server" Text="" />)</th>
                                                    <th>Results<br />
                                                        (<asp:Label ID="lbUnit6" runat="server" Text="" />)</th>
                                                    <th runat="server" id="th3">
                                                        <asp:CheckBox ID="CheckBox3" runat="server" Checked="True" />
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>

                                                <tr runat="server" id="tr33">
                                                    <td>
                                                        <asp:Label ID="Label15" runat="server" Text="Total number of particles ≥ 0.3μm"></asp:Label></td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td runat="server" id="td11">&nbsp;</td>
                                                </tr>
                                                <tr runat="server" id="tr34">
                                                    <td>
                                                        <asp:Label ID="Label17" runat="server" Text="1st Run"></asp:Label></td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="lbC39" runat="server" Text="" /></td>
                                                    <td runat="server" id="td12">&nbsp;</td>
                                                </tr>
                                                <tr runat="server" id="tr35">
                                                    <td>
                                                        <asp:Label ID="Label20" runat="server" Text="2nd Run"></asp:Label></td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="lbC40" runat="server" Text="" /></td>
                                                    <td runat="server" id="td13">&nbsp;</td>
                                                </tr>
                                                <tr runat="server" id="tr36">
                                                    <td>
                                                        <asp:Label ID="Label23" runat="server" Text="3rd Run"></asp:Label></td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="lbC41" runat="server" Text="" /></td>
                                                    <td runat="server" id="td14">&nbsp;</td>
                                                </tr>
                                                <tr runat="server" id="tr37">
                                                    <td>
                                                        <asp:Label ID="Label25" runat="server" Text="Average"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbB42" runat="server" Text="" /></td>
                                                    <td>
                                                        <asp:Label ID="lbC42" runat="server" Text="" /></td>
                                                    <td runat="server" id="td15">&nbsp;</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <%-- Liquid Particle Count (132 KHz) 0.6 --%>
                                <div class="row">
                                    <div class="col-md-9">
                                        <table class="table table-striped table-hover" id="tb4" runat="server">
                                            <thead>

                                                <tr>
                                                    <th>Liquid Particle Count (132 KHz)</th>
                                                    <th>Specification Limits<br />
                                                        (<asp:Label ID="lbUnit7" runat="server" Text="" />)</th>
                                                    <th>Results<br />
                                                        (<asp:Label ID="lbUnit8" runat="server" Text="" />)</th>
                                                    <th runat="server" id="th4">
                                                        <asp:CheckBox ID="CheckBox4" runat="server" Checked="True" />
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>

                                                <tr runat="server" id="tr38">
                                                    <td>
                                                        <asp:Label ID="Label24" runat="server" Text="Total number of particles ≥ 0.6μm"></asp:Label></td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td runat="server" id="td16">&nbsp;</td>
                                                </tr>
                                                <tr runat="server" id="tr39">
                                                    <td>
                                                        <asp:Label ID="Label26" runat="server" Text="1st Run"></asp:Label></td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="lbC46" runat="server" Text="" /></td>
                                                    <td runat="server" id="td17">&nbsp;</td>
                                                </tr>
                                                <tr runat="server" id="tr40">
                                                    <td>
                                                        <asp:Label ID="Label28" runat="server" Text="2nd Run"></asp:Label></td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="lbC47" runat="server" Text="" /></td>
                                                    <td runat="server" id="td18">&nbsp;</td>
                                                </tr>
                                                <tr runat="server" id="tr41">
                                                    <td>
                                                        <asp:Label ID="Label30" runat="server" Text="3rd Run"></asp:Label></td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="lbC48" runat="server" Text="" /></td>
                                                    <td runat="server" id="td19">&nbsp;</td>
                                                </tr>
                                                <tr runat="server" id="tr42">
                                                    <td>
                                                        <asp:Label ID="Label32" runat="server" Text="Average"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbB49" runat="server" Text="" /></td>
                                                    <td>
                                                        <asp:Label ID="lbC49" runat="server" Text="" /></td>
                                                    <td runat="server" id="td20">&nbsp;</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
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

                        <h4 class="form-section">Manage Result Data</h4>
                        <div class="row">
                            <div class="col-md-12">
                                <h6>
                                    <asp:Label ID="lbResultDesc" runat="server" Text=""></asp:Label></h6>
                                <%--US-LPC(0.3)--%>
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
                                                        <asp:TextBox ID="txt_UsLPC03_B18" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_UsLPC03_C18" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_UsLPC03_D18" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_UsLPC03_E18" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_UsLPC03_F18" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_UsLPC03_G18" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                                </tr>
                                                <tr runat="server" id="tr6">
                                                    <td>Extraction Vol. (ml)</td>
                                                    <td colspan="6">
                                                        <asp:TextBox ID="txt_UsLPC03_B20" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                                </tr>
                                                <tr runat="server" id="tr7">
                                                    <td>Surface Area (cm2)</td>
                                                    <td colspan="6">
                                                        <asp:TextBox ID="txt_UsLPC03_B21" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                                </tr>
                                                <tr runat="server" id="tr8">
                                                    <td>No. of Parts Used</td>
                                                    <td colspan="6">
                                                        <asp:TextBox ID="txt_UsLPC03_B22" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
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
                                                        <asp:TextBox ID="txt_UsLPC03_B25" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="txt_UsLPC03_D25" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="txt_UsLPC03_F25" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                                </tr>
                                                <tr runat="server" id="tr11">
                                                    <td>Average</td>
                                                    <td colspan="6">
                                                        <asp:TextBox ID="txt_UsLPC03_B26" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <%--US-LPC(0.6)--%>
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
                                                        <asp:TextBox ID="txt_UsLPC06_B18" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_UsLPC06_C18" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_UsLPC06_D18" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_UsLPC06_E18" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_UsLPC06_F18" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txt_UsLPC06_G18" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                                </tr>
                                                <tr runat="server" id="tr17">
                                                    <td>Extraction Vol. (ml)</td>
                                                    <td colspan="6">
                                                        <asp:TextBox ID="txt_UsLPC06_B20" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                                </tr>
                                                <tr runat="server" id="tr18">
                                                    <td>Surface Area (cm2)</td>
                                                    <td colspan="6">
                                                        <asp:TextBox ID="txt_UsLPC06_B21" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                                </tr>
                                                <tr runat="server" id="tr19">
                                                    <td>No. of Parts Used</td>
                                                    <td colspan="6">
                                                        <asp:TextBox ID="txt_UsLPC06_B22" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
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
                                                        <asp:TextBox ID="txt_UsLPC06_B25" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="txt_UsLPC06_D25" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="txt_UsLPC06_F25" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                                </tr>
                                                <tr runat="server" id="tr22">
                                                    <td>Average</td>
                                                    <td colspan="6">
                                                        <asp:TextBox ID="txt_UsLPC06_B26" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
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
                                            <td>No of Particles</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal03" runat="server" TextMode="Number" CssClass="form-control" Text="2"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Average</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal04" runat="server" TextMode="Number" CssClass="form-control" Text="2"></asp:TextBox></td>
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

