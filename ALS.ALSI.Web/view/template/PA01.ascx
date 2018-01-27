<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PA01.ascx.cs" Inherits="ALS.ALSI.Web.view.template.PA01" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<style type="text/css">
    .auto-style1 {
        height: 26px;
    }
    .auto-style2 {
        width: 10px;
    }
    .auto-style3 {
        height: 27px;
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
                        <asp:Button ID="btnPage01" runat="server" Text="Particle Analysis According" CssClass="btn red-sunglo btn-sm" OnClick="btnCoverPage_Click" />
                        <asp:Button ID="btnPage02" runat="server" Text="Cleanliness Analysis Accordging" CssClass="btn btn-default btn-sm" OnClick="btnCoverPage_Click" />
                        <asp:Button ID="btnPage03" runat="server" Text="Attachment I" CssClass="btn btn-default btn-sm" OnClick="btnCoverPage_Click" />


                    </div>
                </div>
                <div class="portlet-body">

                    <asp:Panel ID="pPage01" runat="server">

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
                                            <asp:GridView ID="gvEop" CssClass="table table-striped table-hover table-bordered" runat="server" AutoGenerateColumns="False" DataKeyNames="ID,row_status" OnRowDataBound="gvEop_RowDataBound" OnRowCommand="gvEop_RowCommand" OnRowCancelingEdit="gvEop_RowCancelingEdit" OnRowDeleting="gvEop_RowDeleting" OnRowEditing="gvEop_RowEditing" OnRowUpdating="gvEop_RowUpdating">
                                                <Columns>
                                                    <%--<asp:BoundField DataField="col_b" HeaderText="Cleanliness Class SKK: 3A_2 (Refer to S252001-1)" />
                                                    <asp:BoundField DataField="col_c" HeaderText="Specification" />--%>
                                                    <asp:TemplateField HeaderText="####" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litC" runat="server" Text='<%# Eval("col_c")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtC" runat="server" Text='<%# Eval("col_c")%>' CssClass="form-control"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="####" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litD" runat="server" Text='<%# Eval("col_d")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtD" runat="server" Text='<%# Eval("col_d")%>' CssClass="form-control"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="####" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litE" runat="server" Text='<%# Eval("col_e")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtE" runat="server" Text='<%# Eval("col_e")%>' CssClass="form-control"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="####" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litF" runat="server" Text='<%# Eval("col_f")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtF" runat="server" Text='<%# Eval("col_f")%>' CssClass="form-control"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="####" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litG" runat="server" Text='<%# Eval("col_g")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtG" runat="server" Text='<%# Eval("col_g")%>' CssClass="form-control"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="####" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litH" runat="server" Text='<%# Eval("col_h")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtH" runat="server" Text='<%# Eval("col_h")%>' CssClass="form-control"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="####" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litI" runat="server" Text='<%# Eval("col_i")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtI" runat="server" Text='<%# Eval("col_i")%>' CssClass="form-control"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="####" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litJ" runat="server" Text='<%# Eval("col_j")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtJ" runat="server" Text='<%# Eval("col_j")%>' CssClass="form-control"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="####" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litK" runat="server" Text='<%# Eval("col_k")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtK" runat="server" Text='<%# Eval("col_k")%>' CssClass="form-control"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="####" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litL" runat="server" Text='<%# Eval("col_l")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtL" runat="server" Text='<%# Eval("col_l")%>' CssClass="form-control"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="####" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litM" runat="server" Text='<%# Eval("col_m")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtM" runat="server" Text='<%# Eval("col_m")%>' CssClass="form-control"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="####" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litN" runat="server" Text='<%# Eval("col_n")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtN" runat="server" Text='<%# Eval("col_n")%>' CssClass="form-control"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="####" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litO" runat="server" Text='<%# Eval("col_o")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtO" runat="server" Text='<%# Eval("col_o")%>' CssClass="form-control"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
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
                                <td colspan="6">Remark: Total is metallic shine+non-metall shine particles without fibers on membrane.
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>
                                    <asp:Label ID="lbRow01" runat="server">Largest metallic shine:</asp:Label></td>
                                <td>X</td>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>
                                    <asp:TextBox ID="txtLms" runat="server"></asp:TextBox></td>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;micron</td>
                            </tr>
                            <tr>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>
                                    <asp:Label ID="lbRow02" runat="server">Largest non-metallic shine:</asp:Label></td>
                                <td>X</td>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>
                                    <asp:TextBox ID="txtLnmp" runat="server"></asp:TextBox></td>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;micron</td>
                            </tr>
                            <tr>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>
                                    <asp:Label ID="lbRow03" runat="server">Longest fiber:</asp:Label></td>
                                <td>X</td>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>
                                    <asp:TextBox ID="txtLf" runat="server"></asp:TextBox></td>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;micron</td>
                            </tr>

                        </table>


                        <table>

                            <tr>
                                <td colspan="2">Deviation:</td>
                            </tr>
                            <tr>
                                <td>Deviation of extraction curve:</td>
                                <td>
                                    <asp:TextBox ID="txtDoec" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Deviation of specification:</td>
                                <td>
                                    <asp:TextBox ID="txtDos" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Remark: N/A is not applicable.</td>
                                <td></td>
                            </tr>
                        </table>

                        <table>
                            <tr>

                                <td class="auto-style1">Customer Limit:</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtCustomerLimit" runat="server" Text="no data given."></asp:TextBox></td>
                            </tr>
                        </table>

                        <table>

                            <tr>
                                <td colspan="6">Blank Test:</td>
                            </tr>
                            <tr>
                                <td>Gravimetry:</td>
                                <td>
                                    <asp:TextBox ID="txtGravimetry" runat="server" Text="n.a"></asp:TextBox></td>
                                <td>mg</td>
                                <td>Largest metallic shine particle:</td>
                                <td>
                                    <asp:TextBox ID="txtLmsp" runat="server" Text=""></asp:TextBox></td>
                                <td>µm</td>
                            </tr>
                            <tr>
                                <td>Extraction value:</td>
                                <td>
                                    <asp:TextBox ID="txtExtractionValue" runat="server" Text="n.a"></asp:TextBox></td>
                                <td>mL</td>
                                <td>Largest non-metallic shine particle:</td>
                                <td>
                                    <asp:TextBox ID="txtLnmsp" runat="server" Text=""></asp:TextBox></td>
                                <td>µm</td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>&nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>

                        <table>

                            <tr>
                                <td colspan="6">Evaluation of Particles:</td>
                            </tr>
                            <tr>
                                <td>Gravimetry:</td>
                                <td>
                                    <asp:TextBox ID="txtEop_G" runat="server" Text="n.a"></asp:TextBox></td>
                                <td>mg</td>
                                <td>Largest metallic shine particle:</td>
                                <td>
                                    <asp:TextBox ID="txtEop_Lmsp" runat="server" Text=""></asp:TextBox></td>
                                <td>µm</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>mL</td>
                                <td>Largest non-metallic shine particle:</td>
                                <td>
                                    <asp:TextBox ID="txtEop_Lnmsp" runat="server" Text=""></asp:TextBox></td>
                                <td>µm</td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>Longest fiber:</td>
                                <td>
                                    <asp:Label ID="lbLf" runat="server" Text="Label"></asp:Label></td>
                                <td>µm</td>
                            </tr>
                        </table>

                        <table>
                            <tr>
                                <td colspan="3">Evaluation of Particles:</td>
                            </tr>
                            <tr>
                                <td>Particle Type</td>
                                <td>Size</td>
                                <td>Value</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtEop_pt" runat="server" Text="See attached file"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtEop_size" runat="server" Text="See attached file"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtEop_value" runat="server" Text="See attached file"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="3">Remark:
                                    <asp:TextBox ID="txtEopRemark" runat="server" Text="-"></asp:TextBox></td>
                            </tr>
                        </table>
                    </asp:Panel>


                    <asp:Panel ID="pPage02" runat="server">
                        <br />
                        <table border="0">
                            <tr>
                                <td rowspan="8">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td rowspan="8" style="vertical-align: top">
                                    <asp:Image ID="img1" runat="server" Style="width: 120px; height: 120px;" />
                                </td>

                                <td rowspan="8" style="vertical-align: top">&nbsp;</td>

                            </tr>
                            <!--
                            <tr>
                                <td style="text-align: right">ALS Reference no:</td>
                                <td>
                                    <asp:TextBox ID="txtAlsReferenceNo" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">Part description:</td>
                                <td>
                                    <asp:TextBox ID="txtPartDescription" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">Lot No.:</td>
                                <td>
                                    <asp:TextBox ID="txtLotNo" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">Date analysed:</td>
                                <td>
                                    <asp:TextBox ID="txtDateAnalyzed" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">Date test completed:</td>
                                <td>
                                    <asp:TextBox ID="txtDateTestComplete" runat="server"></asp:TextBox></td>
                            </tr>
                            -->

                            <tr>
                                <td>&nbsp;
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
                                <td>&nbsp;

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

                        <table>
                            <tr>
                                <td>Controlled surface area:</td>
                            </tr>
                            <tr>
                                <asp:CheckBoxList ID="cbCsa" runat="server">
                                    <asp:ListItem Value="1">Inside only</asp:ListItem>
                                    <asp:ListItem Value="2">Outside only</asp:ListItem>
                                    <asp:ListItem Value="3" Selected="True">Complete component</asp:ListItem>
                                    <asp:ListItem Value="4" Selected="True">According to customer</asp:ListItem>
                                    <asp:ListItem Value="5">Nothing specified</asp:ListItem>
                                </asp:CheckBoxList>

                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>Wetted surface per componet: = </td>
                                <td>
                                    <asp:TextBox ID="txtWspc" runat="server"></asp:TextBox></td>
                                <td>cm2</td>
                            </tr>
                            <tr>
                                <td>Wetted volume per component: = </td>
                                <td>
                                    <asp:TextBox ID="txtWvpc" runat="server"></asp:TextBox></td>
                                <td>cm2</td>
                            </tr>
                            <tr>
                                <td>test lot size: = </td>
                                <td>
                                    <asp:TextBox ID="txtTls" runat="server"></asp:TextBox></td>
                                <td>piece(s)</td>
                            </tr>
                        </table>

                        <table>
                            <tr>
                                <td colspan="3">Specification of test specimen:</td>
                            </tr>
                            <tr>
                                <td>Pre-treatment / conditioning: </td>
                                <td>
                                    <asp:CheckBox ID="cbPreTreatmentConditioning" runat="server" Checked="true" /></td>
                                <td><asp:TextBox ID="txtPreTreatmentConditioning" runat="server" Text="None"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>Packaging to tested:</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBoxList ID="cbPackingToBeTested" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                                        <asp:ListItem Value="0">No</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                        </table>

                        <table>
                            <tr>
                                <td colspan="3">Test arrangement / Environment:</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="cbContainer" runat="server" Checked="true" /></td>
                                <td>
                                    <asp:DropDownList ID="ddlContainer" runat="server" DataTextField="C" DataValueField="ID"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="cbFluid1" runat="server" Checked="true" OnCheckedChanged="cbFluid1_CheckedChanged" AutoPostBack="true" /></td>
                                <td>
                                    <asp:DropDownList ID="ddlFluid1" runat="server" DataTextField="C" DataValueField="ID" OnSelectedIndexChanged="ddlFluid1_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                                <td>
                                    <asp:CheckBox ID="cbFluid2" runat="server" OnCheckedChanged="cbFluid2_CheckedChanged" AutoPostBack="true" /></td>
                                <td>
                                    <asp:DropDownList ID="ddlFluid2" runat="server" DataTextField="C" DataValueField="ID" OnSelectedIndexChanged="ddlFluid2_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                                <td>
                                    <asp:CheckBox ID="cbFluid3" runat="server" OnCheckedChanged="cbFluid3_CheckedChanged" AutoPostBack="true" /></td>
                                <td>
                                    <asp:DropDownList ID="ddlFluid3" runat="server" DataTextField="C" DataValueField="ID" OnSelectedIndexChanged="ddlFluid3_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td>Trade Name:</td>
                                <td>
                                    <asp:TextBox ID="txtTradeName" runat="server"></asp:TextBox></td>
                                <td>Manufacturer:</td>
                                <td>
                                    <asp:TextBox ID="txtManufacturer" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Total quantity [mL]:</td>
                                <td>
                                    <asp:TextBox ID="txtTotalQuantity" runat="server"></asp:TextBox></td>
                                <td>mL:</td>
                            </tr>
                            <tr>
                                <td>Test Environment:</td>
                                <td>Cleanroom class ISO146464-1: class5</td>
                            </tr>

                        </table>
                        <table>
                            <tr>
                                <td>Test specimen held by:</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="cbTshb01" runat="server"></asp:CheckBox>Hand</td>
                                <td>
                                    <asp:CheckBox ID="cbTshb02" runat="server" Checked="true"></asp:CheckBox>Tweezers</td>
                                <td>
                                    <asp:CheckBox ID="cbTshb03" runat="server"></asp:CheckBox><asp:TextBox ID="txtTshb03" runat="server" Text="Other"></asp:TextBox></td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>Positoin of test specimen:</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="cbPots01" runat="server"></asp:CheckBox><asp:TextBox ID="txtPots01" runat="server" Text="Alternating"></asp:TextBox></td>
                            </tr>
                        </table>

                        XXXXX[1]XXXXX 

                        <table>
                            <tr>
                                <td colspan="7">Description of process and extraction:</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="cbDissolving" runat="server" Checked="true" OnCheckedChanged="cbDissolving_CheckedChanged" AutoPostBack="true"></asp:CheckBox></td>
                                <td>dissolving quanitty</td>
                                <td>
                                    <asp:TextBox ID="txtDissolving" runat="server" Text="1000"></asp:TextBox></td>
                                <td>mL</td>
                                <td>dissolving time:</td>
                                <td>
                                    <asp:TextBox ID="txtDissolvingTime" runat="server" Text="-"></asp:TextBox></td>
                                <td>sec.</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="cbPressureRinsing" runat="server" OnCheckedChanged="cbDissolving_CheckedChanged" AutoPostBack="true"></asp:CheckBox></td>
                                <td>Pressure rinsing</td>
                                <td>
                                    <asp:CheckBox ID="cbInternalRinsing" runat="server" OnCheckedChanged="cbDissolving_CheckedChanged" AutoPostBack="true"></asp:CheckBox></td>
                                <td>Internal rinsing</td>
                                <td>
                                    <asp:CheckBox ID="cbAgitation" runat="server" Checked="true" OnCheckedChanged="cbDissolving_CheckedChanged" AutoPostBack="true"></asp:CheckBox></td>
                                <td>Agitation</td>
                                <td></td>
                            </tr>
                        </table>
                        <asp:GridView ID="gvDissolving" CssClass="table table-striped table-hover table-bordered" runat="server" AutoGenerateColumns="False" DataKeyNames="ID,row_status" OnRowDataBound="gvDissolving_RowDataBound" OnRowCommand="gvDissolving_RowCommand" OnRowCancelingEdit="gvDissolving_RowCancelingEdit" OnRowDeleting="gvDissolving_RowDeleting" OnRowEditing="gvDissolving_RowEditing" OnRowUpdating="gvDissolving_RowUpdating">
                            <Columns>

                                <asp:TemplateField HeaderText="####" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Literal ID="litD" runat="server" Text='<%# Eval("col_d")%>'></asp:Literal>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtD" runat="server" Text='<%# Eval("col_d")%>' CssClass="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="####" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Literal ID="litE" runat="server" Text='<%# Eval("col_e")%>'></asp:Literal>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtE" runat="server" Text='<%# Eval("col_e")%>' CssClass="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="####" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Literal ID="litF" runat="server" Text='<%# Eval("col_f")%>'></asp:Literal>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtF" runat="server" Text='<%# Eval("col_f")%>' CssClass="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="####" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Literal ID="litG" runat="server" Text='<%# Eval("col_g")%>'></asp:Literal>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtG" runat="server" Text='<%# Eval("col_g")%>' CssClass="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="####" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Literal ID="litH" runat="server" Text='<%# Eval("col_h")%>'></asp:Literal>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtH" runat="server" Text='<%# Eval("col_h")%>' CssClass="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="####" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Literal ID="litI" runat="server" Text='<%# Eval("col_i")%>'></asp:Literal>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtI" runat="server" Text='<%# Eval("col_i")%>' CssClass="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
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

                        <table>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="cbWashQuantity" runat="server"></asp:CheckBox></td>
                                <td>Wash quantity</td>
                                <td>
                                    <asp:TextBox ID="txtWashQuantity" runat="server" Text="500"></asp:TextBox></td>
                                <td>mL</td>
                                <td>
                                    <asp:CheckBox ID="cbRewashingQuantity" runat="server"></asp:CheckBox></td>
                                <td>Rewashing quantity</td>
                                <td>
                                    <asp:TextBox ID="txtRewashingQuantity" runat="server" Text="500"></asp:TextBox></td>
                                <td>ml</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="cbWashPressureRinsing" runat="server" OnCheckedChanged="cbWashQuantity_CheckedChanged" AutoPostBack="true"></asp:CheckBox></td>
                                <td>Pressure rinsing</td>
                                <td>
                                    <asp:CheckBox ID="cbWashInternalRinsing" runat="server" OnCheckedChanged="cbWashQuantity_CheckedChanged" AutoPostBack="true"></asp:CheckBox></td>
                                <td>Internal rinsing</td>
                                <td>
                                    <asp:CheckBox ID="cbWashAgitation" runat="server" OnCheckedChanged="cbWashQuantity_CheckedChanged" AutoPostBack="true"></asp:CheckBox></td>
                                <td>Agitation</td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>

                        <asp:GridView ID="gvWashing" CssClass="table table-striped table-hover table-bordered" runat="server" AutoGenerateColumns="False" DataKeyNames="ID,row_status" OnRowDataBound="gvWashing_RowDataBound" OnRowCommand="gvWashing_RowCommand" OnRowCancelingEdit="gvWashing_RowCancelingEdit" OnRowDeleting="gvWashing_RowDeleting" OnRowEditing="gvWashing_RowEditing" OnRowUpdating="gvWashing_RowUpdating">
                            <Columns>
                              
                                <asp:TemplateField HeaderText="####" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Literal ID="litD" runat="server" Text='<%# Eval("col_d")%>'></asp:Literal>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtD" runat="server" Text='<%# Eval("col_d")%>' CssClass="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="####" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Literal ID="litE" runat="server" Text='<%# Eval("col_e")%>'></asp:Literal>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtE" runat="server" Text='<%# Eval("col_e")%>' CssClass="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="####" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Literal ID="litF" runat="server" Text='<%# Eval("col_f")%>'></asp:Literal>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtF" runat="server" Text='<%# Eval("col_f")%>' CssClass="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="####" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Literal ID="litG" runat="server" Text='<%# Eval("col_g")%>'></asp:Literal>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtG" runat="server" Text='<%# Eval("col_g")%>' CssClass="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="####" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Literal ID="litH" runat="server" Text='<%# Eval("col_h")%>'></asp:Literal>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtH" runat="server" Text='<%# Eval("col_h")%>' CssClass="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="####" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Literal ID="litI" runat="server" Text='<%# Eval("col_i")%>'></asp:Literal>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtI" runat="server" Text='<%# Eval("col_i")%>' CssClass="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
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


                        <table>
                            <tr>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>Filtration method:</td>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>
                                    <asp:CheckBoxList ID="cbFiltrationMethod" runat="server">
                                        <asp:ListItem Value="1" Selected="True">Vacuum pressure</asp:ListItem>
                                        <asp:ListItem Value="2">Cascade filtration</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                                <td></td>
                            </tr>
                        </table>

                        <table>
                            <tr>
                                <td>Analysis membrane used:</td>

                            </tr>
                            <tr>
                                <td>Manufacturer:</td>
                                <td>
                                    <asp:DropDownList ID="ddlManufacturer" runat="server" DataTextField="C" DataValueField="ID"></asp:DropDownList></td>
                                <td>Material:</td>
                                <td>
                                    <asp:DropDownList ID="ddlMaterial" runat="server" DataTextField="C" DataValueField="ID"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td>Pore size [um]</td>
                                <td>
                                    <asp:TextBox ID="txtPoreSize" runat="server"></asp:TextBox></td>
                                <td>Diameter [mm]</td>
                                <td>
                                    <asp:TextBox ID="txtDiameter" runat="server"></asp:TextBox></td>
                            </tr>
                        </table>

                        <table>
                            <tr>
                                <td>Type of drying:</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="cbOven" runat="server" /></td>
                                <td>Oven</td>
                                <td>
                                    <asp:CheckBox ID="cbDesiccator" runat="server" /></td>
                                <td>Desiccator</td>
                                <td>
                                    <asp:CheckBox ID="cbAmbientAir" runat="server" /></td>
                                <td>Ambient air</td>
                                <td>
                                    <asp:CheckBox ID="cbEasyDry" runat="server" /></td>
                                <td>Easy Dry</td>
                            </tr>
                            <tr>
                                <td>Dry time:</td>
                                <td>
                                    <asp:TextBox ID="txtDryTime" runat="server" Text="30"></asp:TextBox></td>
                                <td>min.</td>
                                <td>Temperature:</td>
                                <td>
                                    <asp:TextBox ID="txtTemperature" runat="server" Text="65"></asp:TextBox></td>
                                <td>'C</td>
                                <td></td>
                            </tr>
                        </table>

                        <table>
                            <tr>
                                <td>Gravimetric analysis:</td>
                            </tr>
                            <tr>
                                <td>Lab balance</td>
                                <td>
                                    <asp:DropDownList ID="ddlGravimetricAlalysis" runat="server" DataTextField="C" DataValueField="ID" OnSelectedIndexChanged="ddlGravimetricAlalysis_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                                <td>Model:</td>
                                <td>
                                    <asp:TextBox ID="txtModel" runat="server" Text="CHG-252"></asp:TextBox></td>
                                <td>Balance resolution:</td>
                                <td>
                                    <asp:TextBox ID="txtBalanceResolution" runat="server" Text="0.0001"></asp:TextBox></td>
                                <td>mg</td>

                            </tr>
                            <tr>
                                <td>Last calibration:</td>
                                <td>
                                    <asp:TextBox ID="txtLastCalibration" runat="server" Text="04.Dec.2017"></asp:TextBox></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>

                        </table>

                        <table>
                            <tr>
                                <td>Microscopeic analysis:</td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="cbZEISSAxioImager2" runat="server" Checked="true" /></td>
                                <td>ZEISS Axio Imager 2</td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="cbMeasuringSoftware" runat="server" Checked="true" /></td>
                                <td>Measuring software</td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="cbAutomated" runat="server" Checked="true" /></td>
                                <td>Automated,pixel scaling</td>
                                <td>
                                    <asp:TextBox ID="txtAutomated" runat="server"></asp:TextBox></td>
                                <td>um/pixel</td>
                            </tr>
                        </table>

                        <table>
                            <tr>
                                <td>Sample Data:</td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>Totalextraction volume,[mL]</td>
                                <td>
                                    <asp:TextBox ID="txtTotalextractionVolume" runat="server" Text="2000"></asp:TextBox></td>
                                <td>Extraction method</td>
                                <td>
                                    <asp:Label ID="lbExtractionMethod" runat="server" Text="Agitation"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Number of components,[ea]</td>
                                <td>
                                    <asp:TextBox ID="txtNumberOfComponents" runat="server" Text="53"></asp:TextBox></td>
                                <td>Extraction time [s]</td>
                                <td>
                                    <asp:Label ID="lbExtractionTime" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Total residue weight, [mg]</td>
                                <td></td>
                                <td>Membrane type / size</td>
                                <td>
                                    <asp:Label ID="lbMembraneType" runat="server" Text="PES / 5 um, 47mm Dia."></asp:Label></td>
                            </tr>
                        </table>

                        <table>
                            <tr>
                                <td>Microscopic Data:</td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td class="auto-style2"></td>
                            </tr>
                            <tr>
                                <td>Scaling pixel:</td>
                                <td>X:<asp:Label ID="lbX" runat="server"></asp:Label>
                                </td>
                                <td>m/pixel</td>
                               
                                <td>Y:<asp:Label ID="lbY" runat="server"></asp:Label></td>
                                 <td>Measured diameter [mm]:</td>
                                <td>
                                    <asp:TextBox ID="txtMeasuredDiameter" runat="server" Text="43.0"></asp:TextBox>
                                </td>
                            </tr>
                        </table>

                        <table>
                            <tr>
                                <td></td>
                                <td>Largest metallic shine particle:</td>
                                <td>Largest non-metallic shineparticle:</td>
                                <td>Longest fiber particle:</td>
                            </tr>
                            <tr>
                                <td>Feret(max) [um]</td>
                                <td>
                                    <asp:TextBox ID="txtFeretLmsp" runat="server" Text="227.08"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtFeretLnms" runat="server" Text="518.97"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtFeretFb" runat="server" Text="285.26"></asp:TextBox></td>
                            </tr>
                        </table>
                        <br />



                        <div class="row">
                            <div class="col-md-12">
                                <!-- BEGIN Portlet PORTLET-->
                                <div class="portlet light">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <caption>
                                                <i class="icon-puzzle font-grey-gallery"></i><span class="caption-subject bold font-grey-gallery uppercase">Microscopic Sample:</span>
                                            </caption>
                                        </div>
                                    </div>
                                    <div class="portlet-body">
                                        <div class="form-group">

                                            <table border="0" style="width: 50%">

                                                <tr>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>Choose File:</td>

                                                    <td>
                                                        <asp:FileUpload ID="FileUpload2" runat="server" /></td>
                                                    <td>
                                                        <asp:Button ID="btnLoadFile" runat="server" Text="Load Data" CssClass="btn blue-hoki" OnClick="btnLoadFile_Click" /></td>
                                                    <td></td>
                                                </tr>

                                            </table>
                                            <br />
                                            <asp:GridView ID="gvMicroscopicAnalysis" CssClass="table table-striped table-hover table-bordered" runat="server" AutoGenerateColumns="False" DataKeyNames="ID,row_status" OnRowDataBound="gvMicroscopicAnalysis_RowDataBound" OnRowCommand="gvMicroscopicAnalysis_RowCommand" OnDataBound="gvMicroscopicAnalysis_OnDataBound" OnRowCancelingEdit="gvMicroscopicAnalysis_RowCancelingEdit" OnRowDeleting="gvMicroscopicAnalysis_RowDeleting" OnRowEditing="gvMicroscopicAnalysis_RowEditing" OnRowUpdating="gvMicroscopicAnalysis_RowUpdating">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litC" runat="server" Text='<%# Eval("col_c")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtC" runat="server" Text='<%# Eval("col_c")%>' CssClass="form-control"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Metal" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litD" runat="server" Text='<%# Eval("col_d")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtD" runat="server" Text='<%# Eval("col_d")%>' CssClass="form-control"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litE" runat="server" Text='<%# Eval("col_e")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtE" runat="server" Text='<%# Eval("col_e")%>' CssClass="form-control"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Metal" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litF" runat="server" Text='<%# Eval("col_f")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtF" runat="server" Text='<%# Eval("col_f")%>' CssClass="form-control"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litG" runat="server" Text='<%# Eval("col_g")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtG" runat="server" Text='<%# Eval("col_g")%>' CssClass="form-control"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Metal" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litH" runat="server" Text='<%# Eval("col_h")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtH" runat="server" Text='<%# Eval("col_h")%>' CssClass="form-control"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                   
                                                    <asp:TemplateField HeaderText="Metal" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litI" runat="server" Text='<%# Eval("col_i")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtI" runat="server" Text='<%# Eval("col_i")%>' CssClass="form-control"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Metal" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litJ" runat="server" Text='<%# Eval("col_j")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtJ" runat="server" Text='<%# Eval("col_j")%>' CssClass="form-control"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Metal" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litK" runat="server" Text='<%# Eval("col_k")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtK" runat="server" Text='<%# Eval("col_k")%>' CssClass="form-control"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Metal" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litL" runat="server" Text='<%# Eval("col_l")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtL" runat="server" Text='<%# Eval("col_l")%>' CssClass="form-control"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Metal" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litM" runat="server" Text='<%# Eval("col_m")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtM" runat="server" Text='<%# Eval("col_m")%>' CssClass="form-control"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Metal" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litN" runat="server" Text='<%# Eval("col_n")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtN" runat="server" Text='<%# Eval("col_n")%>' CssClass="form-control"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Metal" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litO" runat="server" Text='<%# Eval("col_o")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtO" runat="server" Text='<%# Eval("col_o")%>' CssClass="form-control"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Metal" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litP" runat="server" Text='<%# Eval("col_p")%>'></asp:Literal>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtP" runat="server" Text='<%# Eval("col_p")%>' CssClass="form-control"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
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


                    </asp:Panel>

                    <asp:Panel ID="pCcc" runat="server">
                                                <div class="row">
                            <div class="col-md-12">
                                <!-- BEGIN Portlet PORTLET-->
                                <div class="portlet light">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <caption>
                                                <i class="icon-puzzle font-grey-gallery"></i><span class="caption-subject bold font-grey-gallery uppercase"></span>
                                            </caption>
                                        </div>
                                    </div>
                                    <div class="portlet-body">
                                        <div class="form-group">
                                            <table border="1">
                                                <tr>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td colspan="2">Component Cleanliness Code (CCC):</td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>Summarized</td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Total</td>
                                                    <td>Metallic shine</td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>per component</td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox41" runat="server"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox42" runat="server"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>Extended(B/C/D/E/F/G/H/I/J/K)</td>
                                                    <td style="text-decoration-style: dotted">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Total&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>Metallic shine</td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>per component:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td style="text-decoration-style: dotted">
                                                        <asp:Label ID="lbPermembrane" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox43" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                            <table>
                                                <tr>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    <td>Comments: All automatic counting have been revised manually.
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pPage03" runat="server">
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
                                            <table style="text-align: center">
                                                <tr>
                                                    <td colspan="3">
                                                        <asp:Image ID="img2" runat="server" Style="width: 60px; height: 60px;" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="text-align:center">
                                                        <asp:FileUpload ID="fileUploadImg02" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">Overview of filter</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table style="text-align: center">
                                                            <tr>
                                                                <td colspan="6">
                                                                    <asp:Image ID="img3" runat="server" Style="width: 60px; height: 60px;" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6">
                                                                    <asp:FileUpload ID="fileUploadImg03" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" class="auto-style3">Largest metallic shine</td>
                                                            </tr>
                                                            <tr>
                                                                <td>X</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtLms_X" runat="server"></asp:TextBox></td>
                                                                <td>um,</td>
                                                                <td>Y</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtLms_Y" runat="server"></asp:TextBox></td>
                                                                <td>um</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        <table style="text-align: center">
                                                            <tr>
                                                                <td colspan="6">
                                                                    <asp:Image ID="img4" runat="server" Style="width: 60px; height: 60px;" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6">
                                                                    <asp:FileUpload ID="fileUploadImg04" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6">Largest non-metallic shine</td>
                                                            </tr>
                                                            <tr>
                                                                <td>X</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtLnms_X" runat="server"></asp:TextBox></td>
                                                                <td>um,</td>
                                                                <td>Y</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtLnms_Y" runat="server"></asp:TextBox></td>
                                                                <td>um</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        <table style="text-align: center">
                                                            <tr>
                                                                <td colspan="6">
                                                                    <asp:Image ID="img5" runat="server" Style="width: 60px; height: 60px;" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6">
                                                                    <asp:FileUpload ID="fileUploadImg05" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6">Largest fiber</td>
                                                            </tr>
                                                            <tr>
                                                                <td>X</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtLf_X" runat="server"></asp:TextBox></td>
                                                                <td>um,</td>
                                                                <td>Y</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtLf_Y" runat="server"></asp:TextBox></td>
                                                                <td>um</td>
                                                            </tr>
                                                        </table>
                                                    </td>
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


                                        <asp:Panel ID="pEop" runat="server">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">Test result.:</label>
                                                <div class="col-md-6">

                                                    <asp:DropDownList ID="ddlResult" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="0" Selected="True">-NONE-</asp:ListItem>
                                                        <asp:ListItem Value="1">TEST FAILED</asp:ListItem>
                                                        <asp:ListItem Value="2">TEST PASSED</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-md-3">Specification No.:<span class="required">*</span></label>
                                                <div class="col-md-6">
                                                    <asp:DropDownList ID="ddlSpecification" runat="server" CssClass="select2_category form-control" DataTextField="B" DataValueField="ID" OnSelectedIndexChanged="ddlSpecification_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-md-3">Procedure in reference to declining curve.:<span class="required">*</span></label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtPIRTDC" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>


                                            <br />
                                        </asp:Panel>
                                        <!-- 
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
                                                                <asp:LinkButton ID="lbDownload" runat="server" OnClick="lbDownload_Click">
                                                                    <asp:Literal ID="litDownloadIcon" runat="server"></asp:Literal>&nbsp;Download
                                                                </asp:LinkButton></h5>
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
                    </asp:Panel>




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
