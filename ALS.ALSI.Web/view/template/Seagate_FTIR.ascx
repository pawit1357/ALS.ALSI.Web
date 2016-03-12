<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Seagate_FTIR.ascx.cs" Inherits="ALS.ALSI.Web.view.template.Seagate_FTIR" %>
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
                                        <table class="table table-striped table-hover table-bordered">
                                            <thead>
                                                <tr>
                                                    <th>Analysis</th>
                                                    <th>Procedure No</th>
                                                    <th>Number of pieces<br />
                                                        used for extraction</th>
                                                    <th>Extraction<br />
                                                        Medium</th>
                                                    <th>Extraction
                                        <br />
                                                        Volume</th>
                                                    <th></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr runat="server" id="tab1_tr1">
                                                    <td class="auto-style1">NVR/FTIR</td>
                                                    <td>
                                                        <asp:TextBox ID="txtProcedureNo" runat="server" Text="20800032-001 Rev. B,20800014-001 Rev. G,20800033-001 Rev K" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtNomOfPiece" runat="server" Text="5.8109 g/Estimated surface use 774.79 cm²" TextMode="MultiLine" Rows="2" CssClass="form-control"></asp:TextBox>

                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtExtractionMedium" runat="server" Text="IPA - 24 Hrs(HPLC Grade)" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtExtractionVolumn" runat="server" Text="40ml" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked="true" /></td>
                                                </tr>

                                                <tr runat="server" id="tab1_tr2">
                                                    <td class="auto-style1">NVR</td>
                                                    <td>
                                                        <asp:TextBox ID="txtProcedureNo0" runat="server" Text="20800032-001 Rev. B
20800014-001 Rev. G
35344-001 Rev T"
                                                            TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtNomOfPiece0" runat="server" Rows="2" Text="Estimated surface use cm²" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtExtractionMedium0" runat="server" Text="Ultrapure Water" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtExtractionVolumn0" runat="server" Text="100ml" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="CheckBox2" runat="server" Checked="true" /></td>
                                                </tr>

                                                <tr runat="server" id="tab1_tr3">
                                                    <td class="auto-style1">NVR</td>
                                                    <td>
                                                        <asp:TextBox ID="txtProcedureNo1" runat="server" Text="20800032-001 Rev. B
20800014-001 Rev. G
35344-001 Rev T"
                                                            TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtNomOfPiece1" runat="server" Rows="2" Text="Estimated surface use cm²" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtExtractionMedium1" runat="server" Text="n-hexane (HPLC Grade)" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtExtractionVolumn1" runat="server" Text="100ml" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="CheckBox3" runat="server" Checked="true" /></td>
                                                </tr>

                                                <tr runat="server" id="tab1_tr4">
                                                    <td class="auto-style1">FTIR</td>
                                                    <td>
                                                        <asp:TextBox ID="txtProcedureNo2" runat="server" Text="20800032-001 Rev. B
20800014-001 Rev. G
35344-001 Rev T"
                                                            TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtNomOfPiece2" runat="server" Rows="2" Text="Estimated surface use cm²" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtExtractionMedium2" runat="server" Text="IPA/n-hexane (HPLC Grade)" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtExtractionVolumn2" runat="server" Text="100ml" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="CheckBox4" runat="server" Checked="true" /></td>
                                                </tr>

                                                <tr runat="server" id="tab1_tr5">
                                                    <td class="auto-style1">FTIR</td>
                                                    <td>
                                                        <asp:TextBox ID="txtProcedureNo3" runat="server" Text="20800032-001 Rev. B
20800014-001 Rev. G"
                                                            TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtNomOfPiece3" runat="server" Rows="2" Text="Estimated surface use cm²" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtExtractionMedium3" runat="server" Text="n-hexane (HPLC Grade)" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtExtractionVolumn3" runat="server" Text="10ml" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="CheckBox5" runat="server" Checked="true" /></td>
                                                </tr>

                                                <tr runat="server" id="tab1_tr6">
                                                    <td class="auto-style1">FTIR</td>
                                                    <td>
                                                        <asp:TextBox ID="txtProcedureNo4" runat="server" Text="20800032-001 Rev. B
20800014-001 Rev. G"
                                                            TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtNomOfPiece4" runat="server" Rows="2" Text="1 pc." TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtExtractionMedium4" runat="server" Text="n-hexane (HPLC Grade)" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtExtractionVolumn4" runat="server" Text="20ml" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="CheckBox6" runat="server" Checked="true" /></td>
                                                </tr>

                                            </tbody>
                                        </table>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-9">
                                        <h6>Results:</h6>
                                        <h6>The Specification is based on Seagate's Doc 
                                                    <asp:Label ID="lbDocRev" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="lbDesc" runat="server" Text=""></asp:Label></h6>

                                        <table class="table table-striped table-hover">
                                            <thead>
                                                <tr runat="server" id="Tr28">
                                                    <th>Non-Volatile Residue</th>
                                                    <th>Specification Limits (<asp:Label ID="lbUnitNvr" runat="server" />)</th>
                                                    <th>Results(<asp:Label ID="lbUnitNvr_1" runat="server" />)</th>
                                                    <th runat="server" id="th2">
                                                        <asp:CheckBox ID="CheckBox14" runat="server" Checked="true" />
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr runat="server" id="Tr29">
                                                    <td>NVR (DI Water)</td>
                                                    <td>
                                                        <asp:Label ID="lbB29Spec" runat="server" Text=""></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbB29Result" runat="server" Text=""></asp:Label></td>
                                                    <td>
                                                        <asp:CheckBox ID="CheckBox7" runat="server" Checked="true" />
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="Tr30">
                                                    <td>NVR (IPA/Hexane)</td>
                                                    <td>
                                                        <asp:Label ID="lbB30Spec" runat="server" Text=""></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbB30Result" runat="server" Text=""></asp:Label></td>
                                                    <td>
                                                        <asp:CheckBox ID="CheckBox13" runat="server" Checked="true" />
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="Tr31">
                                                    <td>NVR (IPA)</td>
                                                    <td>
                                                        <asp:Label ID="lbB31Spec" runat="server" Text=""></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbB31Result" runat="server" Text=""></asp:Label></td>
                                                    <td>
                                                        <asp:CheckBox ID="CheckBox16" runat="server" Checked="true" />
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="Tr32">
                                                    <td>NVR (Acetone)</td>
                                                    <td>
                                                        <asp:Label ID="lbB32Spec" runat="server" Text=""></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbB32Result" runat="server" Text=""></asp:Label></td>
                                                    <td>
                                                        <asp:CheckBox ID="CheckBox17" runat="server" Checked="true" />
                                                    </td>
                                                </tr>



                                            </tbody>


                                        </table>
                                        <br />
                                        <table class="table table-striped table-hover">
                                            <thead>
                                                <tr runat="server" id="tab2_tr0">
                                                    <th>Organic Contamination</th>
                                                    <th>Specification Limits (<asp:Label ID="lbUnitFtir" runat="server" />)</th>
                                                    <th>Results(<asp:Label ID="lbUnitFtir_1" runat="server" />)</th>
                                                    <th runat="server" id="th1">
                                                        <asp:CheckBox ID="CheckBox15" runat="server" Checked="true" />
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr runat="server" id="tab2_tr1">
                                                    <td>Silicone</td>
                                                    <td>
                                                        <asp:Label ID="lbB32" runat="server" Text=""></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbC32" runat="server" Text=""></asp:Label></td>
                                                    <td id="th1_cb1">
                                                        <asp:CheckBox ID="CheckBox8" runat="server" Checked="true" />
                                                    </td>
                                                </tr>

                                                <tr runat="server" id="tab2_tr2">
                                                    <td>Silicone Oil </td>
                                                    <td>
                                                        <asp:Label ID="lbB33" runat="server" Text=""></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbC33" runat="server" Text=""></asp:Label></td>
                                                    <td id="th1_cb2">
                                                        <asp:CheckBox ID="CheckBox9" runat="server" Checked="true" />
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tab2_tr3">
                                                    <td>Hydrocarbon</td>
                                                    <td>
                                                        <asp:Label ID="lbB34" runat="server" Text=""></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbC34" runat="server" Text=""></asp:Label></td>
                                                    <td id="th1_cb3">
                                                        <asp:CheckBox ID="CheckBox10" runat="server" Checked="true" /></td>
                                                </tr>
                                                <tr runat="server" id="tab2_tr4">
                                                    <td>Phthalate</td>
                                                    <td>
                                                        <asp:Label ID="lbB35" runat="server" Text=""></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbC35" runat="server" Text=""></asp:Label></td>
                                                    <td id="th1_cb4">
                                                        <asp:CheckBox ID="CheckBox11" runat="server" Checked="true" />
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tab2_tr5">
                                                    <td>Amide</td>
                                                    <td>
                                                        <asp:Label ID="lbB36" runat="server" Text=""></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbC36" runat="server" Text=""></asp:Label></td>
                                                    <td id="th1_cb5">
                                                        <asp:CheckBox ID="CheckBox12" runat="server" Checked="true" /></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <br />

                                &nbsp;<h6>
                                    <%--=CONCATENATE("Remarks: The above analysis was carried out using FTIR spectrometer equipped with a MCT detector & a VATR  accessory. The instrument detection limit for Silicone Oil is ", ROUND('working-FTIR'!$B$24,7),"--%> 
Remarks: The above analysis was carried out using FTIR spectrometer equipped with a MCT detector & a VATR  accessory. The instrument detection limit for Silicone Oil is
                    <asp:Label ID="lbA42" runat="server" Text=""></asp:Label>

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
                    <%--working-FTIR--%>
                    <asp:Panel ID="PWorking" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <table class="table table-striped table-hover table-bordered" id="tb1" runat="server">
                                    <thead>
                                        <tr>
                                            <th>Surface area per part (e) =</th>
                                            <th>
                                                <asp:TextBox ID="txtWB13" runat="server" Text="20"></asp:TextBox></th>
                                            <th>cm2</th>
                                            <th></th>
                                            <th></th>
                                        </tr>
                                        <tr>
                                            <th>No. of parts extracted (f) = </th>
                                            <th>
                                                <asp:TextBox ID="txtWB14" runat="server" Text="1"></asp:TextBox></th>
                                            <th></th>
                                            <th></th>
                                            <th></th>
                                        </tr>
                                        <tr>
                                            <th>Total Surface area (A) =</th>
                                            <th>
                                                <asp:TextBox ID="txtWB15" runat="server" Text="20"></asp:TextBox></th>
                                            <th>cm2</th>
                                            <th></th>
                                            <th></th>
                                        </tr>
                                        <tr>
                                            <th>Unit</th>
                                            <th>
                                                <asp:DropDownList ID="ddlUnit" runat="server" CssClass="select2_category form-control" AutoPostBack="True">
                                                    <asp:ListItem Value="1" Selected="True">ng/cm2</asp:ListItem>
                                                    <asp:ListItem Value="2">ug/cm2</asp:ListItem>
                                                </asp:DropDownList></th>
                                            <th></th>
                                            <th></th>
                                            <th></th>
                                        </tr>
                                        <tr>
                                            <th colspan="5"></th>
                                        </tr>
                                        <tr>
                                            <th></th>
                                            <th>Silicone Oil</th>
                                            <th>Amide Slip Agent</th>
                                            <th>Phthalate</th>
                                            <th>Hydrocarbon</th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr runat="server" id="tr25">
                                            <td>Equation of calibration curve,y = mx + c</td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label3" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label4" runat="server"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr runat="server" id="tr1">
                                            <td>Peak height measured, y (mAbs)</td>
                                            <td>
                                                <asp:TextBox ID="txtWB20" runat="server" CssClass="m-wrap small" Text="0.00"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtWC20" runat="server" CssClass="m-wrap small" Text="0.00"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtWD20" runat="server" CssClass="m-wrap small" Text="0.00"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtWE20" runat="server" CssClass="m-wrap small" Text="0.00"></asp:TextBox></td>
                                            <td></td>
                                        </tr>
                                        <tr runat="server" id="tr2">
                                            <td>Slope of curve, m</td>
                                            <td>
                                                <asp:TextBox ID="txtWB21" runat="server" CssClass="m-wrap small" Text="33.9730"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtWC21" runat="server" CssClass="m-wrap small" Text="57.1800"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtWD21" runat="server" CssClass="m-wrap small" Text="20.3790"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtWE21" runat="server" CssClass="m-wrap small" Text="19.1740"></asp:TextBox></td>
                                            <td></td>
                                        </tr>
                                        <tr runat="server" id="tr3">
                                            <td>y -intercept, c</td>
                                            <td>
                                                <asp:TextBox ID="txtWB22" runat="server" CssClass="m-wrap small" Text="-0.0029"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtWC22" runat="server" CssClass="m-wrap small" Text="-0.0490"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtWD22" runat="server" CssClass="m-wrap small" Text="0.0533"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtWE22" runat="server" CssClass="m-wrap small" Text="0.0969"></asp:TextBox></td>
                                            <td></td>
                                        </tr>
                                        <tr runat="server" id="tr4">
                                            <td>Amount , x (ug) = (y - c) / m
                                            </td>
                                            <td>
                                                <asp:Label ID="lbWB23" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbWC23" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbWD23" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbWE23" runat="server" Text=""></asp:Label>

                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr runat="server" id="tr5">
                                            <td>Instrument Detection Limit (ug)

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtWB24" runat="server" CssClass="m-wrap small" Text="0.02"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtWC24" runat="server" CssClass="m-wrap small" Text="0.00"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtWD24" runat="server" CssClass="m-wrap small" Text="0.00"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtWE24" runat="server" CssClass="m-wrap small" Text="0.02"></asp:TextBox></td>
                                            <td></td>
                                        </tr>
                                        <tr runat="server" id="tr6">
                                            <td>Instrument Detection Limit (ug/sqcm)

                                            </td>
                                            <td>
                                                <asp:Label ID="lbWB25" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbWC25" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbWD25" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbWE25" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr runat="server" id="tr7">
                                            <td>Amount (ng/cm2)
                                            </td>
                                            <td>
                                                <asp:Label ID="lbWB26" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbWC26" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbWD26" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbWE26" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr runat="server" id="tr8">
                                            <td>Amount (ug/cm2)
                                            </td>
                                            <td>
                                                <asp:Label ID="lbWB27" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbWC27" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbWD27" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbWE27" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td></td>
                                        </tr>

                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </asp:Panel>
                    <%--NVR--%>
                    <asp:Panel ID="PNvr" runat="server">
                        <div class="row">
                            <div class="col-md-9">
                                <table class="table table-striped table-hover small" id="Table1" runat="server">
                                    <thead>
                                        <tr>
                                            <th>Unit</th>
                                            <th>
                                                <asp:DropDownList ID="ddlNvrUnit" runat="server" CssClass="select2_category form-control" AutoPostBack="True">
                                                    <asp:ListItem Value="1" Selected="True">ug</asp:ListItem>
                                                    <asp:ListItem Value="2">ug/cm2</asp:ListItem>
                                                </asp:DropDownList>

                                            </th>
                                            <th></th>
                                            <th></th>
                                            <th></th>
                                        </tr>
                                        <tr>
                                            <th></th>
                                            <th>Control</th>
                                            <th>Sample</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr runat="server" id="tr26">
                                            <td>Pan No.</td>
                                            <td>
                                                <asp:Label ID="Label5" runat="server">1</asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label6" runat="server">2</asp:Label>
                                            </td>

                                        </tr>
                                        <tr runat="server" id="tr9">
                                            <td>Room temp.( oC)</td>
                                            <td>
                                                <asp:TextBox ID="nvrB16" runat="server" CssClass="m-wrap small" Text="0"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="nvrC16" runat="server" CssClass="m-wrap small" Text="0"></asp:TextBox></td>

                                        </tr>
                                        <tr runat="server" id="tr10">
                                            <td>Desiccator humidity</td>
                                            <td>
                                                <asp:TextBox ID="nvrB17" runat="server" CssClass="m-wrap small" Text="0"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="nvrC17" runat="server" CssClass="m-wrap small" Text="0"></asp:TextBox></td>

                                        </tr>
                                        <tr runat="server" id="tr11">
                                            <td>Pan Tare weight (ug)</td>
                                            <td>
                                                <asp:TextBox ID="nvrB18" runat="server" CssClass="m-wrap small" Text="0"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="nvrC18" runat="server" CssClass="m-wrap small" Text="0"></asp:TextBox></td>

                                        </tr>

                                        <tr runat="server" id="tr12">
                                            <td>Room temp.( oC)</td>
                                            <td>
                                                <asp:TextBox ID="nvrB20" runat="server" CssClass="m-wrap small" Text="0"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="nvrC20" runat="server" CssClass="m-wrap small" Text="0"></asp:TextBox>
                                            </td>

                                        </tr>
                                        <tr runat="server" id="tr13">
                                            <td>Desiccator humidity</td>
                                            <td>
                                                <asp:TextBox ID="nvrB21" runat="server" CssClass="m-wrap small" Text="0"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="nvrC21" runat="server" CssClass="m-wrap small" Text="0"></asp:TextBox></td>

                                        </tr>
                                        <tr runat="server" id="tr14">
                                            <td>1st weighing (ug)

                                            </td>
                                            <td>
                                                <asp:TextBox ID="nvrB22" runat="server" CssClass="m-wrap small" Text="0"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="nvrC22" runat="server" CssClass="m-wrap small" Text="0"></asp:TextBox>
                                            </td>

                                        </tr>



                                        <tr runat="server" id="tr15">
                                            <td>Room temp.( oC)</td>
                                            <td>
                                                <asp:TextBox ID="nvrB24" runat="server" CssClass="m-wrap small" Text="0"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="nvrC24" runat="server" CssClass="m-wrap small" Text="0"></asp:TextBox>
                                            </td>

                                        </tr>
                                        <tr runat="server" id="tr16">
                                            <td>Desiccator humidity</td>
                                            <td>
                                                <asp:TextBox ID="nvrB25" runat="server" CssClass="m-wrap small" Text="0"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="nvrC25" runat="server" CssClass="m-wrap small" Text="0"></asp:TextBox></td>

                                        </tr>
                                        <tr runat="server" id="tr17">
                                            <td>2nd weighing (ug)

                                            </td>
                                            <td>
                                                <asp:TextBox ID="nvrB26" runat="server" CssClass="m-wrap small" Text="0"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="nvrC26" runat="server" CssClass="m-wrap small" Text="0"></asp:TextBox>
                                            </td>

                                        </tr>




                                        <tr runat="server" id="tr18">
                                            <td>Room temp.( oC)</td>
                                            <td>
                                                <asp:TextBox ID="nvrB28" runat="server" CssClass="m-wrap small" Text="0"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="nvrC28" runat="server" CssClass="m-wrap small" Text="0"></asp:TextBox>
                                            </td>

                                        </tr>
                                        <tr runat="server" id="tr19">
                                            <td>Desiccator humidity</td>
                                            <td>
                                                <asp:TextBox ID="nvrB29" runat="server" CssClass="m-wrap small" Text="0"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="nvrC29" runat="server" CssClass="m-wrap small" Text="0"></asp:TextBox></td>

                                        </tr>
                                        <tr runat="server" id="tr20">
                                            <td>3rd weighing (ug)

                                            </td>
                                            <td>
                                                <asp:TextBox ID="nvrB30" runat="server" CssClass="m-wrap small" Text="0"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="nvrC30" runat="server" CssClass="m-wrap small" Text="0"></asp:TextBox>
                                            </td>

                                        </tr>

                                        <tr runat="server" id="tr23">
                                            <td>Room temp.( oC)</td>
                                            <td>
                                                <asp:TextBox ID="nvrB32" runat="server" CssClass="m-wrap small" Text="0"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="nvrC32" runat="server" CssClass="m-wrap small" Text="0"></asp:TextBox>
                                            </td>

                                        </tr>
                                        <tr runat="server" id="tr24">
                                            <td>Desiccator humidity</td>
                                            <td>
                                                <asp:TextBox ID="nvrB33" runat="server" CssClass="m-wrap small" Text="0"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="nvrC33" runat="server" CssClass="m-wrap small" Text="0"></asp:TextBox></td>

                                        </tr>
                                        <tr runat="server" id="tr27">
                                            <td>Final weighing (ug)

                                            </td>
                                            <td>
                                                <asp:TextBox ID="nvrB34" runat="server" CssClass="m-wrap small" Text="0"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="nvrC34" runat="server" CssClass="m-wrap small" Text="0"></asp:TextBox>
                                            </td>

                                        </tr>



                                        <tr runat="server" id="tr21">
                                            <td>NVR(ug)

                                            </td>
                                            <td>
                                                <asp:Label ID="nvrB36" runat="server" Text="0"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="nvrC36" runat="server" Text="0"></asp:Label>
                                            </td>

                                        </tr>
                                        <tr runat="server" id="tr22">
                                            <td>NVR (ug/g) = ppm

                                            </td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:Label ID="nvrC37" runat="server" Text="0"></asp:Label>
                                            </td>

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
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="control-label col-md-3">Specification:<span class="required">*</span></label>
                                                    <div class="col-md-6">
                                                        <asp:DropDownList ID="ddlSpecification" runat="server" CssClass="select2_category form-control" DataTextField="B" DataValueField="ID" OnSelectedIndexChanged="ddlSpecification_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
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
                                                        <asp:TextBox ID="txtRemark"  runat="server" CssClass="form-control"></asp:TextBox>
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
                                                        <asp:DropDownList ID="ddlAssignTo" runat="server" CssClass="select2_category form-control" DataTextField="name" DataValueField="ID" AutoPostBack="true"></asp:DropDownList>
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
<%--                                    <asp:Panel ID="pUploadfile" runat="server">

                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="control-label col-md-3">Uplod file:</label>
                                                    <div class="col-md-6">
                                                        <asp:HiddenField ID="hPathSourceFile" runat="server" />
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
                                                <asp:TextBox ID="txtDecimal05" runat="server" TextMode="Number" CssClass="form-control" Text="4"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Amount (ng/cm2)</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal06" runat="server" TextMode="Number" CssClass="form-control" Text="3"></asp:TextBox></td>
                                        </tr>


                                    </table>
                                    <h1>NVR</h1>
                                    <table class="table table-striped">
                                        <tr>
                                            <td>Control</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal07" runat="server" TextMode="Number" CssClass="form-control" Text="2"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Sample</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal08" runat="server" TextMode="Number" CssClass="form-control" Text="4"></asp:TextBox></td>
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
