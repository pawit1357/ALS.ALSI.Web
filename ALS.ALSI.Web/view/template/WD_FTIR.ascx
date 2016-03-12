<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WD_FTIR.ascx.cs" Inherits="ALS.ALSI.Web.view.template.WD_FTIR" %>

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
                                                    <td>NVR/FTIR</td>
                                                    <td>
                                                        <asp:TextBox ID="txtB21" runat="server" Text=""></asp:TextBox>
                                                        <asp:Label ID="lbB21" runat="server" Text=""></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="txtC21" runat="server" Text=""></asp:TextBox>
                                                        <asp:Label ID="lbC21" runat="server" Text=""></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="txtD21_1" runat="server" Text=""></asp:TextBox>
                                                        <asp:Label ID="lbD21_1" runat="server" Text=""></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="txtE21" runat="server" Text=""></asp:TextBox>
                                                        <asp:Label ID="lbE21" runat="server" Text=""></asp:Label></td>
                                                    <td>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked="true" /></td>
                                                </tr>
                                                <tr runat="server" id="tab1_tr2">
                                                    <td>NVR</td>
                                                    <td>
                                                        <asp:TextBox ID="txtB22" runat="server" Text=""></asp:TextBox>
                                                        <asp:Label ID="lbB22" runat="server" Text=""></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="txtC22" runat="server" Text=""></asp:TextBox>
                                                        <asp:Label ID="lbC22" runat="server" Text=""></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="txtD22" runat="server" Text=""></asp:TextBox>
                                                        <asp:Label ID="lbD22" runat="server" Text=""></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="txtE22" runat="server" Text=""></asp:TextBox>
                                                        <asp:Label ID="lbE22" runat="server" Text=""></asp:Label></td>
                                                    <td>
                                                        <asp:CheckBox ID="CheckBox2" runat="server" Checked="true" /></td>
                                                </tr>

                                                <tr runat="server" id="tab1_tr3">
                                                    <td>FTIR</td>
                                                    <td>
                                                        <asp:TextBox ID="txtB23" runat="server" Text=""></asp:TextBox>
                                                        <asp:Label ID="lbB23" runat="server" Text=""></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="txtC23" runat="server" Text=""></asp:TextBox>
                                                        <asp:Label ID="lbC23" runat="server" Text=""></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="txtD23" runat="server" Text=""></asp:TextBox>
                                                        <asp:Label ID="lbD23" runat="server" Text=""></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="txtE23" runat="server" Text=""></asp:TextBox>
                                                        <asp:Label ID="lbE23" runat="server" Text=""></asp:Label></td>
                                                    <td>
                                                        <asp:CheckBox ID="CheckBox3" runat="server" Checked="true" /></td>
                                                </tr>

                                            </tbody>
                                        </table>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-9">
                                        <h6>Results:</h6>
                                        <h6>The specification is based on Western Digital's document no.
                                                    <asp:Label ID="lbDocRev" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="lbDesc" runat="server" Text=""></asp:Label></h6>
                                        <table class="table table-striped table-hover">
                                            <thead>
                                                <tr runat="server" id="tab2_tr0">
                                                    <th>Required Test</th>
                                                    <th>Analyte </th>
                                                    <th>Specification Limits (<asp:Label ID="lbD27" runat="server" Text="" />)</th>
                                                    <th>Results (<asp:Label ID="lbE27" runat="server" Text="" />)</th>
                                                    <th>PASS / FAIL</th>
                                                    <th runat="server" id="th1"></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr runat="server" id="tab2_tr1">
                                                    <td>NVR</td>
                                                    <td>Non-Volatile Residue (NVR)</td>
                                                    <td>
                                                        <asp:Label ID="lbC28" runat="server" Text=""></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbD28" runat="server" Text=""></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbE28" runat="server" Text=""></asp:Label></td>
                                                    <td>
                                                        <asp:CheckBox ID="CheckBox5" runat="server" Checked="true" />
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tab3_tr1">
                                                    <td>FTIR</td>
                                                    <td>Silicone at Wave No:<br />
                                                        2962, 1261, 1092, 1022 & 800cm-1</td>
                                                    <td>
                                                        <asp:Label ID="lbC30" runat="server" Text=""></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbD30" runat="server" Text=""></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbE30" runat="server" Text=""></asp:Label></td>
                                                    <td>
                                                        <asp:CheckBox ID="CheckBox6" runat="server" Checked="true" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>

                                    </div>
                                </div>
                                <br />

                                <h6>
                                    <%--=CONCATENATE("Remarks: The above analysis was carried out using FTIR spectrometer equipped with a MCT detector & a VATR  accessory. The instrument detection limit for Silicone Oil is ", ROUND('working-FTIR'!$B$24,7),"--%> 
Remarks: The above analysis was carried out using FTIR spectrometer equipped with a MCT detector & a VATR  accessory.
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
                            <div class="col-md-6">

                                <table class="table table-striped table-hover small" id="tb1" runat="server">
                                    <thead>
                                        <tr>
                                            <th colspan="4">Test Data</th>
                                        </tr>
                                        <tr>
                                            <th>Volume of solvent used:</th>
                                            <th>
                                                <asp:TextBox ID="txtNVR_FTIR_B14" runat="server"></asp:TextBox>
                                            </th>
                                            <th>mL</th>
                                            <th></th>
                                        </tr>
                                        <tr>
                                            <th>Surface area (S):</th>
                                            <th>
                                                <asp:TextBox ID="txtNVR_FTIR_B15" runat="server"></asp:TextBox>
                                            </th>
                                            <th>cm<sup>2</sup></th>
                                            <th></th>
                                        </tr>
                                        <tr>
                                            <th>No. of parts extracted (N):</th>
                                            <th>
                                                <asp:TextBox ID="txtNVR_FTIR_B16" runat="server"></asp:TextBox>
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
                                                <asp:TextBox ID="txtNVR_B20" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtNVR_C20" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:Label ID="lbD20" runat="server" Text=""></asp:Label></td>
                                        </tr>

                                        <tr runat="server" id="tr2">
                                            <td>Sample (A)</td>
                                            <td>
                                                <asp:TextBox ID="txtNVR_B21" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtNVR_C21" runat="server" CssClass="m-wrap small"></asp:TextBox></td>
                                            <td>
                                                <asp:Label ID="lbD21" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr runat="server" id="tr3">
                                            <td>Calculations:</td>
                                            <td>NVR (ng/cm<sup>2</sup>)=</td>
                                            <td>(2*(A - B) * 1000) / (S * N)</td>
                                            <td>
                                                <asp:Label ID="lbC26" runat="server" Text=""></asp:Label></td>
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
                                                <asp:TextBox ID="txtFTIR_B30" runat="server"></asp:TextBox></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr runat="server" id="tr6">
                                            <td>Slope of Calibration Plot</td>
                                            <td>
                                                <asp:TextBox ID="txtFTIR_B31" runat="server"></asp:TextBox></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr runat="server" id="tr7">
                                            <td>y-intercept</td>
                                            <td>
                                                <asp:TextBox ID="txtFTIR_B32" runat="server"></asp:TextBox></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr runat="server" id="tr8">
                                            <td>Amount of Silicone Detected (µg)</td>
                                            <td>
                                                <asp:TextBox ID="txtFTIR_B33" runat="server"></asp:TextBox></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr runat="server" id="tr9">
                                            <td>Method Detection Limit, MDL</td>
                                            <td>
                                                <asp:Label ID="txtFTIR_B35" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>ng/cm<sup>2</sup></td>
                                            <td></td>
                                        </tr>
                                        <tr runat="server" id="tr10">
                                            <td>Calculations:</td>
                                            <td>Silicone (ng/cm<sup>2</sup>) =</td>
                                            <td>(2 * silicone weight in ug *1000) / (S * N)</td>
                                            <td>
                                                <asp:Label ID="lbFTIR_C40" runat="server" Text=""></asp:Label></td>
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
                                                <asp:TextBox ID="txtFTIR_B42" runat="server"></asp:TextBox></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr runat="server" id="tr13">
                                            <td>Slope of Calibration Plot</td>
                                            <td>
                                                <asp:TextBox ID="txtFTIR_B43" runat="server"></asp:TextBox></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr runat="server" id="tr14">
                                            <td>y-intercept</td>
                                            <td>
                                                <asp:TextBox ID="txtFTIR_B44" runat="server"></asp:TextBox></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr runat="server" id="tr15">
                                            <td>Amount of Silicone Detected (µg)</td>
                                            <td>
                                                <asp:Label ID="txtFTIR_B45" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr runat="server" id="tr17">
                                            <td>Calculations:</td>
                                            <td>Amide (ng/cm<sup>2</sup>) =</td>
                                            <td>(2*amide weight in ug *1000) / (S * N)</td>
                                            <td>
                                                <asp:Label ID="lbFTIR_C49" runat="server" Text=""></asp:Label></td>
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
                                                    <label class="control-label col-md-3">Detail Spec:<span class="required">*</span></label>
                                                    <div class="col-md-6">
                                                        <asp:DropDownList ID="ddlDetailSpec" runat="server" CssClass="select2_category form-control" DataTextField="A" DataValueField="ID" OnSelectedIndexChanged="ddlDetailSpec_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="control-label col-md-3">Component:<span class="required">*</span></label>
                                                    <div class="col-md-6">
                                                        <asp:DropDownList ID="ddlComponent" runat="server" CssClass="select2_category form-control" DataTextField="A" DataValueField="ID" OnSelectedIndexChanged="ddlComponent_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
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
