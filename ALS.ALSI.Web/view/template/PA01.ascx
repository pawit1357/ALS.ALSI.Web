<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PA01.ascx.cs" Inherits="ALS.ALSI.Web.view.template.PA01" %>


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
                        <asp:Button ID="btnDHS" runat="server" Text="WorkSheet" CssClass="btn btn-default btn-sm" OnClick="btnCoverPage_Click" />

                    </div>
                </div>
                <div class="portlet-body">
                    <asp:Panel ID="pCoverpage" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <!-- BEGIN Portlet PORTLET-->
                                <div class="portlet light">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="icon-puzzle font-grey-gallery"></i>
                                            <span class="caption-subject bold font-grey-gallery uppercase">Evaluation of Particles:</span>
                                        </div>
                                    </div>
                                    <div class="portlet-body">
                                        <div class="form-group">
                                            <asp:GridView ID="gvEop" CssClass="table table-striped table-hover table-bordered" runat="server" AutoGenerateColumns="False" DataKeyNames="ID,row_status" OnRowDataBound="gvEop_RowDataBound" OnRowCommand="gvEop_RowCommand">
                                                <Columns>
                                                    <asp:BoundField DataField="col_b" HeaderText="Cleanliness Class SKK: 3A_2 (Refer to S252001-1)" />
                                                    <asp:BoundField DataField="col_c" HeaderText="Specification" />
                                                    <asp:BoundField DataField="col_d" HeaderText="Result quantity per part" />
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
                        </div>
                        <table border="0">
                            <tr>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td colspan="5">Remark: 1The specification provided by customer.
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>
                                    <asp:Label ID="lbRow01" runat="server">Largest Metallic Particle:</asp:Label></td>
                                <td>X</td>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>
                                    <asp:Label ID="lbLmp" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>
                                    <asp:Label ID="lbRow02" runat="server">Largest Non-metallic Particle:</asp:Label></td>
                                <td>X</td>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>
                                    <asp:Label ID="lbLnmp" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>
                                    <asp:Label ID="lbRow03" runat="server">Largest Fiber:</asp:Label></td>
                                <td>X</td>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>
                                    <asp:Label ID="lbLf" runat="server"></asp:Label></td>
                            </tr>

                        </table>
                        <br />
                        <table border="0">
                            <tr>
                                <td rowspan="8">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td rowspan="8">
                                    <asp:Image ID="img1" runat="server" Style="width: 60px; height: 60px;" /></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>ALS Reference no.:</td>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>
                                    <asp:TextBox ID="txtAlsReferenceNo" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Part description:</td>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>
                                    <asp:TextBox ID="txtPartDescription" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Lot No.:</td>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>
                                    <asp:TextBox ID="txtLotNo" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Date analysed:</td>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>
                                    <asp:TextBox ID="txtDateAnalyzed" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Date test completed:</td>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>
                                    <asp:TextBox ID="txtDateTestComplete" runat="server"></asp:TextBox></td>
                            </tr>


                            <tr>
                                <td colspan="3">&nbsp;
                                                        <div class="row" id="Div3" runat="server">
                                                            <div class="col-md-12">

                                                                <div class="form-group">
                                                                    <label class="control-label col-md-3">Uplod file(source file):</label>

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
                                                                                    <asp:FileUpload ID="fileUploadImg01" runat="server" />

                                                                                </span>
                                                                                <a href="javascript:;" class="input-group-addon btn red fileinput-exists" data-dismiss="fileinput">Remove </a>


                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>




                                </td>
                            </tr>


                            <tr>
                                <td colspan="3">&nbsp;

                                                                            <div class="form-group">
                                                                                <label class="control-label col-md-3"></label>
                                                                                <div class="col-md-9">
                                                                                    <div class="fileinput fileinput-new" data-provides="fileinput">
                                                                                        <asp:Button ID="btnLoadImg1" runat="server" Text="Load" CssClass="btn blue" OnClick="btnLoadImg1_Click" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                </td>
                            </tr>


                        </table>

                        <div class="row">
                            <div class="col-md-12">
                                <!-- BEGIN Portlet PORTLET-->
                                <div class="portlet light">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="icon-puzzle font-grey-gallery"></i>
                                            <span class="caption-subject bold font-grey-gallery uppercase">Description of process and extraction:</span>
                                        </div>
                                    </div>
                                    <div class="portlet-body">
                                        <div class="form-group">
                                            <table border="0">
                                                <tr>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>Extraction Procedure:</td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>aIn-house method refers to ISO16232 and</td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td></td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>
                                                        <asp:TextBox ID="txtExtractionProcedure" runat="server"></asp:TextBox></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>Analysis Environment:</td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>Controlled laboratory, (class ISO14644-1: class 5)</td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>Extraction method:</td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>Extraction medium:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtExtractionMedium" runat="server"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td></td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>Shaking / Rewash Q’ty:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtShkingRewashQty" runat="server"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td></td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>Wetted surface per component:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtWettedSurfacePerComponent" runat="server"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td></td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>Total tested size:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtTotalTestedSize" runat="server"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>Type of method:</td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>
                                                        <asp:CheckBoxList ID="cbTypeOfMethod" runat="server">
                                                            <asp:ListItem Value="1">Agitation acc.to ISO16232-2</asp:ListItem>
                                                            <asp:ListItem Value="2">Pressure Rinse (medium pressure) acc.to ISO16232-3</asp:ListItem>
                                                            <asp:ListItem Value="3">Ultrasonic acc.to ISO16232-4</asp:ListItem>
                                                        </asp:CheckBoxList></td>
                                                    <td></td>
                                                </tr>
                                                <tr>


                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>Filtration method:</td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>
                                                        <asp:CheckBoxList ID="cbFiltrationMethod" runat="server">
                                                            <asp:ListItem Value="1">Vacuum pressure</asp:ListItem>
                                                            <asp:ListItem Value="2">Cascade filtration</asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>Analysis Membrane used:</td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>
                                                        <asp:TextBox ID="lbAnalysisMembraneUsed" runat="server" Text="47mm Dia., Nylon 5 µm MFPD"></asp:TextBox></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>Type of drying:</td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>
                                                        <asp:CheckBoxList ID="cbTypeOfDrying" runat="server">
                                                            <asp:ListItem Value="1">Oven Temperature 80C, 1 hour</asp:ListItem>
                                                            <asp:ListItem Value="2">Desiccator 24C / 33%RH, 24 hours</asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>Mass of Contaminant:</td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>Gravimetric analysis acc.to ISO16232-6</td>
                                                    <td></td>
                                                </tr>

                                                <tr>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>Particle sizing / counting / determination:</td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>
                                                        <asp:CheckBoxList ID="cbParticleSizingCoungtingDetermination" runat="server">
                                                            <asp:ListItem Value="1">by Optical Microscope acc.to ISO16232-7</asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>Pixel scaling:</td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>
                                                        <asp:TextBox ID="txtPixelScaling" runat="server" Text=""></asp:TextBox></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>Camera resolution:</td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>
                                                        <asp:TextBox ID="txtCameraResolution" runat="server" Text=""></asp:TextBox></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td></td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>
                                                        <asp:CheckBoxList ID="cbParticleSizingCoungtingDetermination2" runat="server">
                                                            <asp:ListItem Value="2">by SEM/EDX acc.to ISO16232-8</asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>




                        <div class="row">
                            <div class="col-md-12">
                                <!-- BEGIN Portlet PORTLET-->
                                <div class="portlet light">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <caption>
                                                <i class="icon-puzzle font-grey-gallery"></i><span class="caption-subject bold font-grey-gallery uppercase">Gravimetry:</span>
                                            </caption>
                                        </div>
                                    </div>
                                    <div class="portlet-body">
                                        <div class="form-group">
                                            <asp:GridView ID="gvGravimetry" CssClass="table table-striped table-hover table-bordered" runat="server" AutoGenerateColumns="False" DataKeyNames="ID,row_status" OnRowDataBound="gvGravimetry_RowDataBound" OnRowCommand="gvGravimetry_RowCommand">
                                                <Columns>
                                                    <asp:BoundField DataField="col_a" HeaderText="Membrane filter weight" />
                                                    <asp:BoundField DataField="col_b" HeaderText="Blank" />
                                                    <asp:BoundField DataField="col_c" HeaderText="Component" />
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
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <!-- BEGIN Portlet PORTLET-->
                                <div class="portlet light">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <caption>
                                                <i class="icon-puzzle font-grey-gallery"></i><span class="caption-subject bold font-grey-gallery uppercase">Microscopic Analysis:</span>
                                            </caption>
                                        </div>
                                    </div>
                                    <div class="portlet-body">
                                        <div class="form-group">
                                            <div class="row" id="Div1" runat="server">
                                                <div class="col-md-12">

                                                    <div class="form-group">
                                                        <caption>
                                                            <label class="control-label col-md-3">
                                                                Uplod file(source file):</label>
                                                            <div class="col-md-3">
                                                                <div class="fileinput fileinput-new" data-provides="fileinput">
                                                                    <div class="input-group input-large">
                                                                        <div class="form-control uneditable-input input-fixed input-large" data-trigger="fileinput">
                                                                            <i class="fa fa-file fileinput-exists"></i>&nbsp; <span class="fileinput-filename"></span>
                                                                        </div>
                                                                        <span class="input-group-addon btn default btn-file"><span class="fileinput-new">Select file </span><span class="fileinput-exists">Change </span>
                                                                            <asp:FileUpload ID="FileUpload2" runat="server" />
                                                                        </span><a class="input-group-addon btn red fileinput-exists" data-dismiss="fileinput" href="javascript:;">Remove </a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </caption>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-3"></label>
                                                        <div class="col-md-9">
                                                            <div class="fileinput fileinput-new" data-provides="fileinput">
                                                                <asp:Button ID="btnLoadFile" runat="server" Text="Load" CssClass="btn blue" OnClick="btnLoadFile_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:GridView ID="gvMicroscopicAnalysis" CssClass="table table-striped table-hover table-bordered" runat="server" AutoGenerateColumns="False" DataKeyNames="ID,row_status" OnRowDataBound="gvMicroscopicAnalysis_RowDataBound" OnRowCommand="gvMicroscopicAnalysis_RowCommand" OnDataBound="gvMicroscopicAnalysis_OnDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="col_a" HeaderText="Size class" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="col_b" HeaderText="Size range(um)" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="col_c" HeaderText="Total" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="col_d" HeaderText="Metal" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="col_e" HeaderText="Total" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="col_f" HeaderText="Metal" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="col_g" HeaderText="Total" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="col_h" HeaderText="Metal" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
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


                                            <table border="0">
                                                <tr>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>Remark:	1Metallic + Non-metallic particles without fibers.
	2Fibers defined as Non-metallic, Length/Width > 10.

                                                    </td>
                                                </tr>
                                            </table>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <!-- BEGIN Portlet PORTLET-->
                                <div class="portlet light">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <caption>
                                                <i class="icon-puzzle font-grey-gallery"></i><span class="caption-subject bold font-grey-gallery uppercase">Component Cleanliness Code (CCC):</span>
                                            </caption>
                                        </div>
                                    </div>
                                    <div class="portlet-body">
                                        <div class="form-group">
                                            <table border="1">
                                                <tr>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Extended (B/C/E/D/E/F/G/H/I/J/K)
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td></td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Total</td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;per membrane:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td style="text-decoration-style: dotted">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:Label ID="lbPermembrane" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                </tr>
                                            </table>
                                            <br />
                                            <table>
                                                <tr>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>Remark:	1Metallic + Non-metallic particles without fibers.
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col-md-12">
                                <!-- BEGIN Portlet PORTLET-->
                                <div class="portlet light">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <caption>
                                                <i class="icon-puzzle font-grey-gallery"></i><span class="caption-subject bold font-grey-gallery uppercase">Attachment I:</span>
                                            </caption>
                                        </div>
                                    </div>
                                    <div class="portlet-body">
                                        <div class="form-group">
                                            <table border="0">

                                                <tr>
                                                    <td>
                                                        <asp:Image ID="img2" runat="server" Style="width: 60px; height: 60px;" /></td>
                                                    <td>
                                                        <div class="row" id="Div4" runat="server">
                                                            <div class="col-md-12">

                                                                <div class="form-group">
                                                                    <label class="control-label col-md-3">Uplod file(source file):</label>

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
                                                                                    <asp:FileUpload ID="fileUploadImg02" runat="server" />

                                                                                </span>
                                                                                <a href="javascript:;" class="input-group-addon btn red fileinput-exists" data-dismiss="fileinput">Remove </a>


                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        </t
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Image ID="img3" runat="server" Style="width: 60px; height: 60px;" /></td>
                                                    <td>
                                                        <div class="row" id="Div5" runat="server">
                                                            <div class="col-md-12">

                                                                <div class="form-group">
                                                                    <label class="control-label col-md-3">Uplod file(source file):</label>

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
                                                                                    <asp:FileUpload ID="fileUploadImg03" runat="server" />

                                                                                </span>
                                                                                <a href="javascript:;" class="input-group-addon btn red fileinput-exists" data-dismiss="fileinput">Remove </a>


                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        </t
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Image ID="img4" runat="server" Style="width: 60px; height: 60px;" /></td>
                                                    <td>
                                                        <div class="row" id="Div6" runat="server">
                                                            <div class="col-md-12">

                                                                <div class="form-group">
                                                                    <label class="control-label col-md-3">Uplod file(source file):</label>

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
                                                                                    <asp:FileUpload ID="fileUploadImg04" runat="server" />

                                                                                </span>
                                                                                <a href="javascript:;" class="input-group-addon btn red fileinput-exists" data-dismiss="fileinput">Remove </a>


                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        </t
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Image ID="img5" runat="server" Style="width: 60px; height: 60px;" /></td>
                                                    <td>
                                                        <div class="row" id="Div7" runat="server">
                                                            <div class="col-md-12">

                                                                <div class="form-group">
                                                                    <label class="control-label col-md-3">Uplod file(source file):</label>

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
                                                                                    <asp:FileUpload ID="fileUploadImg05" runat="server" />

                                                                                </span>
                                                                                <a href="javascript:;" class="input-group-addon btn red fileinput-exists" data-dismiss="fileinput">Remove </a>


                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        </t
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Image ID="img6" runat="server" Style="width: 60px; height: 60px;" /></td>
                                                    <td>
                                                        <div class="row" id="Div8" runat="server">
                                                            <div class="col-md-12">

                                                                <div class="form-group">
                                                                    <label class="control-label col-md-3">Uplod file(source file):</label>

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
                                                                                    <asp:FileUpload ID="fileUploadImg06" runat="server" />

                                                                                </span>
                                                                                <a href="javascript:;" class="input-group-addon btn red fileinput-exists" data-dismiss="fileinput">Remove </a>


                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        </t
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Image ID="img7" runat="server" Style="width: 60px; height: 60px;" /></td>
                                                    <td>
                                                        <div class="row" id="Div9" runat="server">
                                                            <div class="col-md-12">

                                                                <div class="form-group">
                                                                    <label class="control-label col-md-3">Uplod file(source file):</label>

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
                                                                                    <asp:FileUpload ID="fileUploadImg07" runat="server" />

                                                                                </span>
                                                                                <a href="javascript:;" class="input-group-addon btn red fileinput-exists" data-dismiss="fileinput">Remove </a>


                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        </t
                                                </tr>
                                            </table>

                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-md-3"></label>
                                            <div class="col-md-9">
                                                <div class="fileinput fileinput-new" data-provides="fileinput">
                                                    <asp:Button ID="btnLoadImg" runat="server" Text="Load" CssClass="btn blue" OnClick="btnLoadImg_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pUploadWorkSheet" runat="server">
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

                                    <!-- 
                                    <asp:Panel ID="pEop" runat="server">

                                        <div class="form-group">
                                            <label class="control-label col-md-3">Evaluation of Particles:<span class="required">*</span></label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlEop" runat="server" CssClass="select2_category form-control" DataTextField="B" DataValueField="ID" OnSelectedIndexChanged="ddlEop_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <br />
                                    </asp:Panel>
                                    <asp:Panel ID="pMa" runat="server">

                                        <div class="form-group">
                                            <label class="control-label col-md-3">Evaluation of Particles:<span class="required">*</span></label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlMa" runat="server" CssClass="select2_category form-control" DataTextField="B" DataValueField="ID" OnSelectedIndexChanged="ddlMa_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <br />
                                    </asp:Panel>
                                        -->

                                    <asp:Panel ID="pStatus" runat="server">

                                        <div class="form-group">
                                            <label class="control-label col-md-3">Approve Status:<span class="required">*</span></label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="select2_category form-control" DataTextField="name" DataValueField="ID" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <br />
                                    </asp:Panel>
                                    <asp:Panel ID="pRemark" runat="server">

                                        <div class="form-group">
                                            <label class="control-label col-md-3">Remark:<span class="required">*</span></label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <br />
                                    </asp:Panel>
                                    <asp:Panel ID="pDisapprove" runat="server">

                                        <div class="form-group">
                                            <label class="control-label col-md-3">Assign To:<span class="required">*</span></label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlAssignTo" runat="server" CssClass="select2_category form-control" DataTextField="name" DataValueField="ID" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <br />
                                    </asp:Panel>
                                    <asp:Panel ID="pDownload" runat="server">
                                        <div class="row" id="Div2" runat="server">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label class="control-label col-md-3">Download:</label>
                                                    <div class="col-md-3">

                                                        <h5>
                                                            <asp:LinkButton ID="lbDownload" runat="server" OnClick="lbDownload_Click"></asp:LinkButton></h5>
                                                        <asp:Label ID="lbDesc" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <%--                                        <div class="form-group">
                                            <label class="control-label col-md-3">Download:</label>
                                            <div class="col-md-6">
                                                <asp:Literal ID="litDownloadIcon" runat="server"></asp:Literal>
                                                <asp:LinkButton ID="lbDownload" runat="server" OnClick="lbDownload_Click">
                                                    <asp:Label ID="lbDownloadName" runat="server" Text="Download"></asp:Label>
                                                </asp:LinkButton>
                                            </div>
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

                        <div class="modal-wide" id="pnlModalDemo" style="display: none;">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h class="modal-title">
                                            กำหนดทศนิยมให้คอลัมภ์</h>
                                </div>
                                <div class="modal-body" style="width: 600px; height: 400px; overflow-x: hidden; overflow-y: scroll; padding-bottom: 10px;">
                                    <table class="table table-striped">
                                        <tr>
                                            <td>Amout</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal01" runat="server" TextMode="Number" CssClass="form-control" Text="2"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Total Outgassing</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal02" runat="server" TextMode="Number" CssClass="form-control" Text="2"></asp:TextBox></td>
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
                                    <asp:Button ID="Button1" CssClass="btn default" Style="margin-top: 10px;" runat="server" Text="ปิด" />
                                </div>
                            </div>
                            <!-- /.modal-content -->
                        </div>
                        <!-- /.modal-dialog -->

                        <asp:LinkButton ID="bnErrListFake" runat="server">
                        </asp:LinkButton>
                        <asp:ModalPopupExtender ID="modalErrorList" runat="server" PopupControlID="popupErrorList"
                            TargetControlID="bnErrListFake" BackgroundCssClass="modal-backdrop modal-print-form fade in" BehaviorID="mpModalErrorList"
                            CancelControlID="btnClose">
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
                            <div class="col-md-6">
                            </div>
                        </div>
                    </div>

                </div>
            </div>


        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnLoadFile" />
            <asp:PostBackTrigger ControlID="btnLoadImg" />
            <asp:PostBackTrigger ControlID="btnLoadImg1" />

            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="lbDownload" />


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
