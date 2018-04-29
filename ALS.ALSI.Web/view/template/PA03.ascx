<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PA03.ascx.cs" Inherits="ALS.ALSI.Web.view.template.PA03" %>


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
                        <asp:Button ID="btnPage01" runat="server" Text="P01" CssClass="btn red-sunglo btn-sm" OnClick="btnCoverPage_Click" />
                        <asp:Button ID="btnPage02" runat="server" Text="P02" CssClass="btn btn-default btn-sm" OnClick="btnCoverPage_Click" />
                        <asp:Button ID="btnPage03" runat="server" Text="P03" CssClass="btn btn-default btn-sm" OnClick="btnCoverPage_Click" />
                        <asp:Button ID="btnPage04" runat="server" Text="P04" CssClass="btn btn-default btn-sm" OnClick="btnCoverPage_Click" />
                        <asp:Button ID="btnPage05" runat="server" Text="P05" CssClass="btn btn-default btn-sm" OnClick="btnCoverPage_Click" />
                        <asp:Button ID="btnPage06" runat="server" Text="P06" CssClass="btn btn-default btn-sm" OnClick="btnCoverPage_Click" />
                        <asp:Button ID="btnPage07" runat="server" Text="P07" CssClass="btn btn-default btn-sm" OnClick="btnCoverPage_Click" />
                        <asp:Button ID="btnPage08" runat="server" Text="P08" CssClass="btn btn-default btn-sm" OnClick="btnCoverPage_Click" Visible="false" />
                        <asp:LinkButton ID="btnShowUnit" runat="server" OnClick="btnShowUnit_Click" CssClass="btn green"> <i class="fa fa-sort-numeric-asc"></i> SetUp  (Float)</asp:LinkButton>


                    </div>
                </div>
                <div class="portlet-body">

                    <asp:Panel ID="pPage01" runat="server">

                        <h4 class="caption-subject bold uppercase"><i class="fa fa-clone"></i>&nbsp;&nbsp;Evaluation of Particles</h4>


                        <asp:GridView CssClass="table table-striped table-bordered table-advance table-hover" ID="gvEop" runat="server" AutoGenerateColumns="False" DataKeyNames="ID,row_status" OnRowDataBound="gvEop_RowDataBound" OnRowCommand="gvEop_RowCommand" OnRowCancelingEdit="gvEop_RowCancelingEdit" OnRowDeleting="gvEop_RowDeleting" OnRowEditing="gvEop_RowEditing" OnRowUpdating="gvEop_RowUpdating">
                            <Columns>
                                <asp:TemplateField HeaderText="1Particle size [µm]" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Literal ID="litC" runat="server" Text='<%# Eval("col_c")%>'></asp:Literal>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtC" runat="server" Text='<%# Eval("col_c")%>' CssClass="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="25-50" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Literal ID="litD" runat="server" Text='<%# Eval("col_d")%>'></asp:Literal>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtD" runat="server" Text='<%# Eval("col_d")%>' CssClass="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="50-100" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Literal ID="litE" runat="server" Text='<%# Eval("col_e")%>'></asp:Literal>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtE" runat="server" Text='<%# Eval("col_e")%>' CssClass="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="100-150" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Literal ID="litF" runat="server" Text='<%# Eval("col_f")%>'></asp:Literal>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtF" runat="server" Text='<%# Eval("col_f")%>' CssClass="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="150-200" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Literal ID="litG" runat="server" Text='<%# Eval("col_g")%>'></asp:Literal>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtG" runat="server" Text='<%# Eval("col_g")%>' CssClass="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="200-300" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Literal ID="litH" runat="server" Text='<%# Eval("col_h")%>'></asp:Literal>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtH" runat="server" Text='<%# Eval("col_h")%>' CssClass="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="300-400" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Literal ID="litI" runat="server" Text='<%# Eval("col_i")%>'></asp:Literal>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtI" runat="server" Text='<%# Eval("col_i")%>' CssClass="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="400-600" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Literal ID="litJ" runat="server" Text='<%# Eval("col_j")%>'></asp:Literal>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtJ" runat="server" Text='<%# Eval("col_j")%>' CssClass="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="600-800" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Literal ID="litK" runat="server" Text='<%# Eval("col_k")%>'></asp:Literal>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtK" runat="server" Text='<%# Eval("col_k")%>' CssClass="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="800-1000" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Literal ID="litL" runat="server" Text='<%# Eval("col_l")%>'></asp:Literal>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtL" runat="server" Text='<%# Eval("col_l")%>' CssClass="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=">1000" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Literal ID="litM" runat="server" Text='<%# Eval("col_m")%>'></asp:Literal>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtM" runat="server" Text='<%# Eval("col_m")%>' CssClass="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="2Fiber" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Literal ID="litN" runat="server" Text='<%# Eval("col_n")%>'></asp:Literal>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtN" runat="server" Text='<%# Eval("col_n")%>' CssClass="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" ToolTip="Edit" CommandName="Edit" CommandArgument='<%# Eval("ID")%>' CausesValidation="false"><i class="fa fa-edit"></i></asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="btnUpdate" runat="server" ToolTip="Update" CausesValidation="false"
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
                        <div class="note note-success">
                            Remark: 1 Particles is metallic shine+non-metall shine particles without fibers on membrane.
2Fiber according to 0442S00025 chapter 1.3.

                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label2" runat="server" CssClass="control-label col-md-3">Largest metallic shine:</asp:Label>
                                    <div class="col-md-6">
                                        <br />
                                        <strong>
                                            <asp:Label ID="txtLms" runat="server"></asp:Label></strong>
                                        <%--                                    <span class="help-block">This is inline help </span>--%>
                                    </div>
                                    <div>
                                        micron<br />
                                    </div>
                                </div>
                            </div>
                            <!--/span-->
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label3" runat="server" CssClass="control-label col-md-3"></asp:Label>
                                    <div class="col-md-6">
                                        <%--                                    <span class="help-block">This is inline help </span>--%>
                                    </div>
                                    <div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <!--/span-->
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label20" runat="server" CssClass="control-label col-md-3">Largest non-metallic shine:</asp:Label>
                                    <div class="col-md-6">
                                        <br />
                                        <strong>
                                            <asp:Label ID="txtLnmp" runat="server"></asp:Label></strong>
                                        <%--                                    <span class="help-block">This is inline help </span>--%>
                                    </div>
                                    <div>
                                        micron<br />
                                    </div>
                                </div>
                            </div>
                            <!--/span-->
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label21" runat="server" CssClass="control-label col-md-3"></asp:Label>
                                    <div class="col-md-6">
                                        <%--                                    <span class="help-block">This is inline help </span>--%>
                                    </div>
                                    <div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <!--/span-->
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label22" runat="server" CssClass="control-label col-md-3">Longest fiber:</asp:Label>
                                    <div class="col-md-6">
                                        <br />
                                        <strong>
                                            <asp:Label ID="txtLf" runat="server"></asp:Label></strong>
                                        <%--                                    <span class="help-block">This is inline help </span>--%>
                                    </div>
                                    <div>
                                        micron<br />
                                    </div>
                                </div>
                            </div>
                            <!--/span-->
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label23" runat="server" CssClass="control-label col-md-3"></asp:Label>
                                    <div class="col-md-6">
                                        <%--                                    <span class="help-block">This is inline help </span>--%>
                                    </div>
                                    <div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <!--/span-->
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pPage02" runat="server">
                        <h4 class="caption-subject bold uppercase"><i class="fa fa-clone"></i>&nbsp;&nbsp;Specifications</h4>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label51" runat="server" CssClass="control-label col-md-3">Specification No.:</asp:Label>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="ddlSpecification" runat="server" DataTextField="C" DataValueField="ID" CssClass="form-control" OnSelectedIndexChanged="ddlSpecification_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                    <div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <!--/span-->
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label52" runat="server" CssClass="control-label col-md-3">Procedure in reference to declining curve.:</asp:Label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtPIRTDC" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                    <div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <!--/span-->
                        </div>
                        <h4 class="caption-subject bold uppercase"><i class="fa fa-clone"></i>&nbsp;&nbsp;Deviation</h4>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label4" runat="server" CssClass="control-label col-md-3">Deviation of extraction curve:</asp:Label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtDoec" runat="server" CssClass="form-control" Text="n.a"></asp:TextBox>
                                        <%--                                    <span class="help-block">This is inline help </span>--%>
                                    </div>
                                    <div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <!--/span-->
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label17" runat="server" CssClass="control-label col-md-3"></asp:Label>
                                    <div class="col-md-6">
                                        <%--                                    <span class="help-block">This is inline help </span>--%>
                                    </div>
                                    <div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <!--/span-->
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label18" runat="server" CssClass="control-label col-md-3">Deviation of specification:</asp:Label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtDos" runat="server" CssClass="form-control" Text="n.a"></asp:TextBox>
                                        <%--                                    <span class="help-block">This is inline help </span>--%>
                                    </div>
                                    <div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <!--/span-->
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label19" runat="server" CssClass="control-label col-md-3"></asp:Label>
                                    <div class="col-md-6">
                                        <%--                                    <span class="help-block">This is inline help </span>--%>
                                    </div>
                                    <div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <!--/span-->
                        </div>
                        <div class="note note-success">
                            Remark: N/A is not applicable.
                        </div>

                        <h4 class="caption-subject bold uppercase"></h4>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label15" runat="server" CssClass="control-label col-md-3">Customer Limit:</asp:Label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtCustomerLimit" runat="server" CssClass="form-control" Text="no data given."></asp:TextBox>
                                        <%--                                    <span class="help-block">This is inline help </span>--%>
                                    </div>
                                    <div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <!--/span-->
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label16" runat="server" CssClass="control-label col-md-3"></asp:Label>
                                    <div class="col-md-6">
                                        <%--                                    <span class="help-block">This is inline help </span>--%>
                                    </div>
                                    <div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <!--/span-->
                        </div>

                        <h4 class="caption-subject bold uppercase"><i class="fa fa-clone"></i>&nbsp;&nbsp;Blank Test</h4>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label5" runat="server" CssClass="control-label col-md-3">Gravimetry:</asp:Label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtGravimetry" runat="server" CssClass="form-control" Text="n.a"></asp:TextBox>
                                        <%--                                    <span class="help-block">This is inline help </span>--%>
                                    </div>
                                    <div>
                                        <br />
                                        mg
                                    </div>
                                </div>
                            </div>
                            <!--/span-->
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label7" runat="server" CssClass="control-label col-md-3">Largest metallic shine particle:</asp:Label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtLmsp" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                        <%--                                    <span class="help-block">This is inline help </span>--%>
                                    </div>
                                    <div>
                                        <br />
                                        um
                                    </div>
                                </div>
                            </div>
                            <!--/span-->
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label8" runat="server" CssClass="control-label col-md-3">Extraction value:</asp:Label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtExtractionValue" runat="server" CssClass="form-control" Text="n.a"></asp:TextBox>
                                        <%--                                    <span class="help-block">This is inline help </span>--%>
                                    </div>
                                    <div>
                                        <br />
                                        mL
                                    </div>
                                </div>
                            </div>
                            <!--/span-->
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label9" runat="server" CssClass="control-label col-md-3">Largest non-metallic shine particle:</asp:Label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtLnmsp" runat="server" CssClass="form-control" Text=""></asp:TextBox>

                                        <%--                                    <span class="help-block">This is inline help </span>--%>
                                    </div>
                                    <div>
                                        <br />
                                        um
                                    </div>
                                </div>
                            </div>
                            <!--/span-->
                        </div>

                        <h4 class="caption-subject bold uppercase"><i class="fa fa-clone"></i>&nbsp;&nbsp;Evaluation</h4>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label6" runat="server" CssClass="control-label col-md-3">Gravimetry:</asp:Label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtEop_G" runat="server" CssClass="form-control" Text="" OnTextChanged="txtEop_G_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        <%--                                    <span class="help-block">This is inline help </span>--%>
                                    </div>
                                    <div>
                                        <br />
                                        mg
                                    </div>
                                </div>
                            </div>
                            <!--/span-->
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label10" runat="server" CssClass="control-label col-md-3">Largest metallic shine particle:</asp:Label>
                                    <div class="col-md-6">
                                        <br />
                                        <strong>
                                            <asp:Label ID="txtEop_Lmsp" runat="server" Text=""></asp:Label></strong>&nbsp;&nbsp;um
                                    </div>
                                </div>
                            </div>
                            <!--/span-->
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label11" runat="server" CssClass="control-label col-md-3"></asp:Label>
                                    <div class="col-md-6">
                                    </div>
                                    <div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <!--/span-->
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label12" runat="server" CssClass="control-label col-md-3">Largest non-metallic shine particle:</asp:Label>
                                    <div class="col-md-6">
                                        <br />
                                        <strong>
                                            <asp:Label ID="txtEop_Lnmsp" runat="server" Text=""></asp:Label></strong>&nbsp;&nbsp;um
                                    </div>

                                </div>
                            </div>
                            <!--/span-->
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label13" runat="server" CssClass="control-label col-md-3"></asp:Label>
                                    <div class="col-md-6">
                                    </div>
                                </div>
                            </div>
                            <!--/span-->
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label14" runat="server" CssClass="control-label col-md-3">Longest fiber:</asp:Label>
                                    <div class="col-md-6">
                                        <strong>
                                            <asp:Label ID="lbLf" runat="server" Text=""></asp:Label></strong>&nbsp;&nbsp;um
                                    </div>
                                </div>
                            </div>
                            <!--/span-->
                        </div>

                        <h4 class="caption-subject bold uppercase"><i class="fa fa-clone"></i>&nbsp;&nbsp;Evaluation of Particles</h4>
                        <table class="table table-striped table-bordered table-advance table-hover">
                            <tr>
                                <td style="text-align: center">Particle Type</td>
                                <td style="text-align: center">Size</td>
                                <td style="text-align: center">Value</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtEop_pt" runat="server" CssClass="form-control" Text="See attached file"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtEop_size" runat="server" CssClass="form-control" Text="See attached file"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtEop_value" runat="server" CssClass="form-control" Text="See attached file"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="3">Remark:
                                    <asp:TextBox ID="txtEopRemark" runat="server" CssClass="form-control" Text="-"></asp:TextBox></td>
                            </tr>
                        </table>

                    </asp:Panel>
                    <asp:Panel ID="pPage03" runat="server">
                        <br />
                        <h4 class="caption-subject bold uppercase"><i class="fa fa-clone"></i>&nbsp;&nbsp;Upload Picture</h4>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label24" runat="server" CssClass="control-label col-md-3"></asp:Label>
                                    <div class="col-md-6">
                                        <asp:Image ID="img1" runat="server" Style="width: 300px; height: 240px;" BorderStyle="Dotted" ImageUrl="~/images/no_img.png" />
                                    </div>

                                </div>
                            </div>
                            <!--/span-->
                            <div class="col-md-6">
                                <div class="form-group">

                                    <h5>Controlled surface area</h5>
                                    <asp:CheckBoxList ID="cbCsa" runat="server">
                                        <asp:ListItem Value="1">&nbsp;&nbsp;Inside only</asp:ListItem>
                                        <asp:ListItem Value="2">&nbsp;&nbsp;Outside only</asp:ListItem>
                                        <asp:ListItem Value="3" Selected="True">&nbsp;&nbsp;Complete component</asp:ListItem>
                                        <asp:ListItem Value="4" Selected="True">&nbsp;&nbsp;According to customer</asp:ListItem>
                                        <asp:ListItem Value="5">&nbsp;&nbsp;Nothing specified</asp:ListItem>
                                    </asp:CheckBoxList>

                                </div>
                            </div>
                            <!--/span-->
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label col-md-3"></label>

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
                                            <asp:Button ID="btnLoadImg1" runat="server" Text="Load" CssClass="btn blue" OnClick="btnLoadImg1_Click" />

                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3"></label>
                                <div class="col-md-3">
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <!--/span-->
                            <div class="col-md-3">
                                <div class="form-group">
                                    <div class="col-md-6">
                                    </div>
                                    <div>
                                    </div>
                                </div>
                            </div>
                            <!--/span-->
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label25" runat="server" CssClass="control-label col-md-4">Wetted surface per componet: = </asp:Label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtWspc" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div>
                                        cm2<br />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <!--/span-->
                            <div class="col-md-3">
                                <div class="form-group">
                                    <div class="col-md-6">
                                    </div>
                                    <div>
                                    </div>
                                </div>
                            </div>
                            <!--/span-->
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label26" runat="server" CssClass="control-label col-md-4">Wetted volume per component: =</asp:Label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtWvpc" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div>
                                        cm3<br />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <!--/span-->
                            <div class="col-md-3">
                                <div class="form-group">
                                    <div class="col-md-6">
                                    </div>
                                    <div>
                                    </div>
                                </div>
                            </div>
                            <!--/span-->
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label27" runat="server" CssClass="control-label col-md-4">test lot size: = </asp:Label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtTls" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div>
                                        piece (s)<br />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <h4 class="caption-subject bold uppercase"><i class="fa fa-clone"></i>&nbsp;&nbsp;Specification of test specimen</h4>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label28" runat="server" CssClass="control-label col-md-3">Pre-treatment / conditioning:</asp:Label>
                                    <div class="mt-checkbox-inline">
                                        <label class="mt-checkbox mt-checkbox-outline">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="cbPreTreatmentConditioning" runat="server" Checked="true" /></td>
                                                    <td>&nbsp;&nbsp;&nbsp;</td>
                                                    <td>
                                                        <asp:TextBox ID="txtPreTreatmentConditioning" runat="server" CssClass="form-control" Text="None"></asp:TextBox></td>
                                                </tr>
                                            </table>


                                            <span></span>
                                        </label>

                                    </div>
                                </div>
                            </div>
                            <!--/span-->
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label29" runat="server" CssClass="control-label col-md-3"></asp:Label>
                                    <div class="col-md-3">
                                    </div>
                                    <div>
                                    </div>
                                </div>
                            </div>
                            <!--/span-->
                        </div>

                        <h4 class="caption-subject bold uppercase"><i class="fa fa-clone"></i>&nbsp;&nbsp;Packaging to be tested</h4>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label30" runat="server" CssClass="control-label col-md-3"></asp:Label>
                                    <div class="mt-checkbox-inline">
                                        <label class="mt-checkbox mt-checkbox-outline">
                                            <asp:CheckBoxList ID="cbPackingToBeTested" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1" Selected="True">&nbsp;&nbsp;Yes&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="0">&nbsp;&nbsp;No&nbsp;&nbsp;</asp:ListItem>
                                            </asp:CheckBoxList>

                                            <span></span>
                                        </label>

                                    </div>
                                </div>
                            </div>
                        </div>

                        <h4 class="caption-subject bold uppercase"><i class="fa fa-clone"></i>&nbsp;&nbsp;Test arrangement / Environment</h4>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label ID="Label36" runat="server" CssClass="control-label col-md-3"></asp:Label>
                                    <div class="col-md-12">
                                        <table class="table table-striped table-bordered table-advance table-hover">
                                            <tr>
                                                <td style="text-align: center">
                                                    <asp:CheckBox ID="cbContainer" runat="server" Checked="true" /></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlContainer" runat="server" DataTextField="D" DataValueField="ID" CssClass="form-control"></asp:DropDownList></td>
                                                <td colspan="4"></td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center">
                                                    <asp:CheckBox ID="cbFluid1" runat="server" Checked="true" OnCheckedChanged="cbFluid1_CheckedChanged" AutoPostBack="true" /></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlFluid1" runat="server" DataTextField="D" DataValueField="ID" OnSelectedIndexChanged="ddlFluid1_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList></td>
                                                <td style="text-align: center">
                                                    <asp:CheckBox ID="cbFluid2" runat="server" OnCheckedChanged="cbFluid2_CheckedChanged" AutoPostBack="true" /></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlFluid2" runat="server" DataTextField="D" DataValueField="ID" OnSelectedIndexChanged="ddlFluid2_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList></td>
                                                <td style="text-align: center">
                                                    <asp:CheckBox ID="cbFluid3" runat="server" OnCheckedChanged="cbFluid3_CheckedChanged" AutoPostBack="true" /></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlFluid3" runat="server" DataTextField="D" DataValueField="ID" OnSelectedIndexChanged="ddlFluid3_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList></td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div>
                                    </div>
                                </div>
                            </div>
                            <!--/span-->

                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label31" runat="server" CssClass="control-label col-md-3">Trade Name:</asp:Label>
                                    <div class="col-md-6">
                                        <br />
                                        <strong>
                                            <asp:Label ID="txtTradeName" runat="server"></asp:Label></strong>
                                    </div>
                                    <div>
                                    </div>
                                </div>
                            </div>
                            <!--/span-->
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label32" runat="server" CssClass="control-label col-md-3">Manufacturer:</asp:Label>
                                    <div class="col-md-6">
                                        <br />
                                        <strong>
                                            <asp:Label ID="txtManufacturer" runat="server"></asp:Label></strong>

                                        <%--<span class="help-block">This is inline help </span>--%>
                                    </div>
                                    <div>
                                        <br />

                                    </div>
                                </div>
                            </div>
                            <!--/span-->
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label34" runat="server" CssClass="control-label col-md-3">Total quantity [mL]:</asp:Label>
                                    <div class="col-md-6">

                                        <asp:Label ID="txtTotalQuantity" runat="server"></asp:Label>
                                    </div>
                                    <div>
                                    </div>
                                </div>
                            </div>
                            <!--/span-->
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label35" runat="server" CssClass="control-label col-md-3"></asp:Label>
                                    <div class="col-md-6">
                                        <%--<span class="help-block">This is inline help </span>--%>
                                        mL
                                    </div>
                                    <div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <!--/span-->
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label33" runat="server" CssClass="control-label col-md-3">Test Environment:</asp:Label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtTextEnvironment" runat="server" CssClass="form-control" Text="Cleanroom class ISO146464-1: class5"></asp:TextBox>
                                    </div>
                                    <div>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <h4 class="caption-subject bold uppercase"><i class="fa fa-clone"></i>&nbsp;&nbsp;Test specimen held by</h4>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <table class="table table-striped table-bordered table-advance table-hover">
                                            <tr>
                                                <td style="text-align: center">
                                                    <asp:CheckBox ID="cbTshb01" runat="server"></asp:CheckBox></td>
                                                <td>&nbsp;&nbsp;Hand</td>
                                                <td style="text-align: center">
                                                    <asp:CheckBox ID="cbTshb02" runat="server" Checked="true"></asp:CheckBox></td>
                                                <td>&nbsp;&nbsp;Tweezers</td>
                                                <td style="text-align: center">
                                                    <asp:CheckBox ID="cbTshb03" runat="server"></asp:CheckBox>&nbsp;&nbsp;</td>
                                                <td style="text-align: center">
                                                    <asp:TextBox ID="txtTshb03" runat="server" Text="Other" CssClass="form-control"></asp:TextBox></td>
                                            </tr>

                                            <tr>
                                                <td colspan="2" style="text-align: right">Positoin of test specimen:</td>
                                                <td style="text-align: center">
                                                    <asp:CheckBox ID="cbPots01" runat="server" Checked="true"></asp:CheckBox></td>
                                                <td style="text-align: center" colspan="3">
                                                    <asp:TextBox ID="txtPots01" runat="server" Text="Alternating" CssClass="form-control"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div>
                                    </div>
                                </div>
                            </div>
                            <!--/span-->

                        </div>

                    </asp:Panel>
                    <asp:Panel ID="pPage04" runat="server">

                        <h4 class="caption-subject bold uppercase"><i class="fa fa-clone"></i>&nbsp;&nbsp;Description of process and extraction</h4>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label ID="Label38" runat="server" CssClass="control-label col-md-1"></asp:Label>
                                    <div class="col-md-12">
                                        <table class="table table-striped table-bordered table-advance table-hover">
                                            <tr>
                                                <td style="text-align: center">
                                                    <asp:CheckBox ID="cbDissolving" runat="server" Checked="true" OnCheckedChanged="cbDissolving_CheckedChanged" AutoPostBack="true"></asp:CheckBox></td>
                                                <td>dissolving quanitty</td>
                                                <td>
                                                    <asp:TextBox ID="txtDissolving" runat="server" Text="1000" CssClass="form-control" OnTextChanged="txtDissolving_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                                                <td>mL</td>
                                                <td>dissolving time:</td>
                                                <td>
                                                    <asp:TextBox ID="txtDissolvingTime" runat="server" Text="-" CssClass="form-control"></asp:TextBox></td>
                                                <td>sec.</td>
                                            </tr>
                                            <tr>

                                                <td style="text-align: right">
                                                    <asp:CheckBox ID="cbAgitation" runat="server" OnCheckedChanged="cbDissolving_CheckedChanged" AutoPostBack="true" Checked="true"></asp:CheckBox></td>
                                                <td>Agitation</td>
                                                <td style="text-align: right">
                                                    <asp:CheckBox ID="cbPressureRinsing" runat="server" OnCheckedChanged="cbDissolving_CheckedChanged" AutoPostBack="true"></asp:CheckBox></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlRinsing" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlRinsing_SelectedIndexChanged" AutoPostBack="true">

                                                        <asp:ListItem Value="0">Pressure rinsing</asp:ListItem>
                                                        <asp:ListItem Value="1">Internal rinsingn</asp:ListItem>

                                                    </asp:DropDownList>


                                                </td>
                                                <td style="text-align: right">
                                                    <asp:CheckBox ID="cbUntrasonic" runat="server" OnCheckedChanged="cbDissolving_CheckedChanged" AutoPostBack="true"></asp:CheckBox></td>
                                                <td>Ultrasonic</td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </div>

                                </div>
                            </div>
                            <!--/span-->

                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label ID="Label37" runat="server" CssClass="control-label col-md-1"></asp:Label>
                                    <div class="col-md-12">
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
                                                <%--                                                <asp:TemplateField HeaderText="Hide">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnHide" runat="server" ToolTip="Hide" CommandName="Hide" OnClientClick="return confirm('ต้องการซ่อนแถว ?');"
                                                            CommandArgument='<%# Eval("ID")%>'><i class="fa fa-minus"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="btnUndo" runat="server" ToolTip="Undo" CommandName="Normal" OnClientClick="return confirm('ยกเลิกการซ่อนแถว ?');"
                                                            CommandArgument='<%# Eval("ID")%>'><i class="fa fa-refresh"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                </div>
                            </div>
                            <!--/span-->

                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label ID="Label39" runat="server" CssClass="control-label col-md-1"></asp:Label>
                                    <div class="col-md-12">
                                        <table class="table table-striped table-bordered table-advance table-hover">
                                            <tr>
                                                <td style="text-align: center">
                                                    <asp:CheckBox ID="cbWashQuantity" runat="server" Checked="true"></asp:CheckBox></td>
                                                <td>Wash quantity</td>
                                                <td>
                                                    <asp:TextBox ID="txtWashQuantity" runat="server" Text="500" CssClass="form-control" OnTextChanged="txtDissolving_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                                                <td>mL</td>
                                                <td style="text-align: right">
                                                    <asp:CheckBox ID="cbRewashingQuantity" runat="server"></asp:CheckBox></td>
                                                <td>Rewashing quantity</td>
                                                <td>
                                                    <asp:TextBox ID="txtRewashingQuantity" runat="server" Text="500" CssClass="form-control" OnTextChanged="txtDissolving_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                                                <td>mL</td>
                                            </tr>
                                            <tr>

                                                <td style="text-align: right">
                                                    <asp:CheckBox ID="cbWashAgitation" runat="server" OnCheckedChanged="cbWashQuantity_CheckedChanged" AutoPostBack="true" Checked="true"></asp:CheckBox></td>
                                                <td>Agitation</td>
                                                <td></td>
                                                <td style="text-align: center">
                                                    <asp:CheckBox ID="cbWashPressureRinsing" runat="server" OnCheckedChanged="cbWashQuantity_CheckedChanged" AutoPostBack="true"></asp:CheckBox></td>
                                                <td>
                                                    <asp:DropDownList ID="dllWashPressureRinsing" runat="server" CssClass="form-control" OnSelectedIndexChanged="dllWashPressureRinsing_SelectedIndexChanged" AutoPostBack="true">

                                                        <asp:ListItem Value="0">Pressure rinsing</asp:ListItem>
                                                        <asp:ListItem Value="1">Internal rinsingn</asp:ListItem>

                                                    </asp:DropDownList></td>
                                                <td style="text-align: right">
                                                    <asp:CheckBox ID="cbWashUltrasonic" runat="server" OnCheckedChanged="cbWashQuantity_CheckedChanged" AutoPostBack="true"></asp:CheckBox></td>
                                                <td>Ultrasonic</td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </div>

                                </div>
                            </div>
                            <!--/span-->

                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label ID="Label40" runat="server" CssClass="control-label col-md-1"></asp:Label>
                                    <div class="col-md-12">
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
                                                <%--                                                <asp:TemplateField HeaderText="Hide">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnHide" runat="server" ToolTip="Hide" CommandName="Hide" OnClientClick="return confirm('ต้องการซ่อนแถว ?');"
                                                            CommandArgument='<%# Eval("ID")%>'><i class="fa fa-minus"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="btnUndo" runat="server" ToolTip="Undo" CommandName="Normal" OnClientClick="return confirm('ยกเลิกการซ่อนแถว ?');"
                                                            CommandArgument='<%# Eval("ID")%>'><i class="fa fa-refresh"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                </div>
                            </div>
                            <!--/span-->

                        </div>

                        <h4 class="caption-subject bold uppercase"><i class="fa fa-clone"></i>&nbsp;&nbsp;Filtration method</h4>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label ID="Label41" runat="server" CssClass="control-label col-md-1"></asp:Label>
                                    <div class="col-md-12">
                                        <asp:CheckBoxList ID="cbFiltrationMethod" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Selected="True">&nbsp;&nbsp;Vacuum pressure&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="2">&nbsp;&nbsp;Cascade filtration&nbsp;&nbsp;</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>

                                </div>
                            </div>
                            <!--/span-->
                        </div>

                        <h4 class="caption-subject bold uppercase"><i class="fa fa-clone"></i>&nbsp;&nbsp;Analysis membrane used</h4>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label ID="Label42" runat="server" CssClass="control-label col-md-1"></asp:Label>
                                    <div class="col-md-12">
                                        <table class="table table-striped table-bordered table-advance table-hover">
                                            <tr>
                                                <td>Manufacturer:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlManufacturer" runat="server" DataTextField="D" DataValueField="ID" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlManufacturer_SelectedIndexChanged"></asp:DropDownList></td>
                                                <td>Material:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlMaterial" runat="server" DataTextField="D" DataValueField="ID" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlMaterial_SelectedIndexChanged"></asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td>Pore size [um]</td>
                                                <td>
                                                    <asp:TextBox ID="txtPoreSize" runat="server" CssClass="form-control" OnTextChanged="txtPoreSize_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                                                <td>Diameter [mm]</td>
                                                <td>
                                                    <asp:TextBox ID="txtDiameter" runat="server" CssClass="form-control" OnTextChanged="txtDiameter_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </div>

                                </div>
                            </div>
                        </div>

                        <h4 class="caption-subject bold uppercase"><i class="fa fa-clone"></i>&nbsp;&nbsp;Type of drying</h4>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label ID="Label43" runat="server" CssClass="control-label col-md-1"></asp:Label>
                                    <div class="col-md-12">
                                        <table class="table table-striped table-bordered table-advance table-hover">
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:CheckBox ID="cbOven" runat="server" Checked="true" /></td>
                                                <td>Oven</td>
                                                <td style="text-align: right">
                                                    <asp:CheckBox ID="cbDesiccator" runat="server" Checked="true" /></td>
                                                <td>Desiccator</td>
                                                <td style="text-align: right">
                                                    <asp:CheckBox ID="cbAmbientAir" runat="server" /></td>
                                                <td>Ambient air</td>
                                                <td style="text-align: right">
                                                    <asp:CheckBox ID="cbEasyDry" runat="server" /></td>
                                                <td>Easy Dry</td>
                                            </tr>
                                            <tr>
                                                <td>Dry time:</td>
                                                <td>
                                                    <asp:TextBox ID="txtDryTime" runat="server" Text="30" CssClass="form-control"></asp:TextBox></td>
                                                <td>min.</td>
                                                <td>Temperature:</td>
                                                <td>
                                                    <asp:TextBox ID="txtTemperature" runat="server" Text="65" CssClass="form-control"></asp:TextBox></td>
                                                <td colspan="3">'C</td>
                                            </tr>
                                        </table>
                                    </div>

                                </div>
                            </div>
                        </div>

                        <h4 class="caption-subject bold uppercase"><i class="fa fa-clone"></i>&nbsp;&nbsp;Gravimetric analysis</h4>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label ID="Label44" runat="server" CssClass="control-label col-md-1"></asp:Label>
                                    <div class="col-md-12">
                                        <table class="table table-striped table-bordered table-advance table-hover">
                                            <tr>
                                                <td>Lab balance</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlGravimetricAlalysis" runat="server" DataTextField="D" DataValueField="ID" OnSelectedIndexChanged="ddlGravimetricAlalysis_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList></td>
                                                <td>Model:</td>
                                                <td>
                                                    <asp:TextBox ID="txtModel" runat="server" Text="CHG-252" CssClass="form-control"></asp:TextBox></td>
                                                <td>Balance resolution:</td>
                                                <td>
                                                    <asp:TextBox ID="txtBalanceResolution" runat="server" Text="0.0001" CssClass="form-control"></asp:TextBox></td>
                                                <td>mg</td>

                                            </tr>
                                            <tr>
                                                <td>Last calibration:</td>
                                                <td>
                                                    <asp:TextBox ID="txtLastCalibration" runat="server" Text="04.Dec.2017" CssClass="form-control"></asp:TextBox></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                            </tr>

                                        </table>
                                    </div>

                                </div>
                            </div>
                        </div>

                        <h4 class="caption-subject bold uppercase"><i class="fa fa-clone"></i>&nbsp;&nbsp;Microscopeic analysis</h4>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label ID="Label45" runat="server" CssClass="control-label col-md-1"></asp:Label>
                                    <div class="col-md-12">
                                        <table class="table table-striped table-bordered table-advance table-hover">
                                            <tr>
                                                <td style="text-align: center">
                                                    <asp:CheckBox ID="cbZEISSAxioImager2" runat="server" Checked="true" /></td>
                                                <td>ZEISS Axio Imager 2</td>
                                                <td colspan="4"></td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center">
                                                    <asp:CheckBox ID="cbMeasuringSoftware" runat="server" Checked="true" /></td>
                                                <td>Measuring software</td>
                                                <td style="text-align: center">
                                                    <asp:CheckBox ID="cbAutomated" runat="server" Checked="true" /></td>
                                                <td>Automated,pixel scaling</td>
                                                <td style="text-align: center">
                                                    <asp:TextBox ID="txtAutomated" runat="server" CssClass="form-control" OnTextChanged="txtAutomated_TextChanged" AutoPostBack="true" Text="1.43"></asp:TextBox></td>
                                                <td>um/pixel</td>
                                            </tr>
                                        </table>
                                    </div>

                                </div>
                            </div>
                        </div>


                    </asp:Panel>
                    <asp:Panel ID="pPage05" runat="server">
                        <h4 class="caption-subject bold uppercase"><i class="fa fa-clone"></i>&nbsp;&nbsp;Project Data:</h4>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label ID="Label53" runat="server" CssClass="control-label col-md-1"></asp:Label>
                                    <div class="col-md-12">
                                        <table class="table table-striped table-bordered table-advance table-hover">
                                            <tr>
                                                <td>Customer:</td>
                                                <td>
                                                    <strong>
                                                        <asp:Label ID="pdCustomer" runat="server" Text=""></asp:Label></strong></td>
                                                <td>ALS reference no.:</td>
                                                <td>
                                                    <strong>
                                                        <asp:Label ID="pdAlsRefNo" runat="server" Text=""></asp:Label></strong></td>
                                            </tr>
                                            <tr>
                                                <td>Part name:</td>
                                                <td><strong>
                                                    <asp:Label ID="pdPartName" runat="server" Text=""></asp:Label></strong></td>
                                                <td>Analysis date:</td>
                                                <td>
                                                    <strong>
                                                        <asp:Label ID="pdAnalysisDate" runat="server" Text="-"></asp:Label></strong></td>
                                            </tr>
                                            <tr>
                                                <td>Part no.:</td>
                                                <td><strong>
                                                    <asp:Label ID="paPartNo" runat="server" Text="-"></asp:Label>
                                                </strong></td>
                                                <td>Operator name:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlOperatorName" runat="server" DataTextField="C" DataValueField="ID" CssClass="form-control"></asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td>Lot no.:</td>
                                                <td><strong>
                                                    <asp:Label ID="pdLotNo" runat="server" Text="-"></asp:Label>
                                                </strong></td>
                                                <td>Specification:</td>
                                                <td>
                                                    <strong>
                                                        <asp:Label ID="pdSpecification" runat="server" Text=""></asp:Label></strong></td>
                                            </tr>
                                        </table>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <h4 class="caption-subject bold uppercase"><i class="fa fa-clone"></i>&nbsp;&nbsp;Sample Data</h4>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label ID="Label46" runat="server" CssClass="control-label col-md-1"></asp:Label>
                                    <div class="col-md-12">
                                        <table class="table table-striped table-bordered table-advance table-hover">
                                            <tr>
                                                <td>Totalextraction volume,[mL]</td>
                                                <td>
                                                    <strong>
                                                        <asp:Label ID="txtTotalextractionVolume" runat="server" Text="2000"></asp:Label></strong></td>
                                                <td>Extraction method</td>
                                                <td>
                                                    <strong>
                                                        <asp:Label ID="lbExtractionMethod" runat="server" Text="Agitation"></asp:Label></strong></td>
                                            </tr>
                                            <tr>
                                                <td>Number of components,[ea]</td>
                                                <td>
                                                    <asp:TextBox ID="txtNumberOfComponents" runat="server" Text="53" CssClass="form-control"></asp:TextBox></td>
                                                <td>Extraction time [s]</td>
                                                <td>
                                                    <strong>
                                                        <asp:Label ID="lbExtractionTime" runat="server" Text="-"></asp:Label></strong></td>
                                            </tr>
                                            <tr>
                                                <td>Total residue weight, [mg]</td>
                                                <td><strong>
                                                    <asp:Label ID="lbTotalResidueWeight" runat="server" Text="-"></asp:Label>
                                                </strong></td>
                                                <td>Membrane type / size</td>
                                                <td>
                                                    <strong>
                                                        <asp:Label ID="lbMembraneType" runat="server" Text=""></asp:Label></strong></td>
                                            </tr>
                                        </table>
                                    </div>

                                </div>
                            </div>
                        </div>

                        <h4 class="caption-subject bold uppercase"><i class="fa fa-clone"></i>&nbsp;&nbsp;Microscopic Data</h4>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label ID="Label47" runat="server" CssClass="control-label col-md-1"></asp:Label>
                                    <div class="col-md-12">
                                        <table class="table table-striped table-bordered table-advance table-hover">
                                            <tr>
                                                <td style="text-align: right">Scaling pixel:</td>
                                                <td style="text-align: center">X:
                                                </td>
                                                <td style="text-align: center">
                                                    <strong>
                                                        <asp:Label ID="lbX" runat="server"></asp:Label></strong></td>
                                                <td style="text-align: center">m/pixel</td>
                                                <td style="text-align: center">Y:</td>
                                                <td style="text-align: center">
                                                    <strong>
                                                        <asp:Label ID="lbY" runat="server"></asp:Label></strong></td>
                                                <td style="text-align: center">m/pixel</td>
                                                <td style="text-align: center">Measured diameter [mm]:</td>
                                                <td style="text-align: center">
                                                    <asp:TextBox ID="txtMeasuredDiameter" runat="server" Text="43.0" CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label ID="Label48" runat="server" CssClass="control-label col-md-1"></asp:Label>
                                    <div class="col-md-12">
                                        <table class="table table-striped table-bordered table-advance table-hover">
                                            <tr>
                                                <td></td>
                                                <td style="text-align: center">Largest metallic shine particle:</td>
                                                <td style="text-align: center">Largest non-metallic shineparticle:</td>
                                                <td style="text-align: center">Longest fiber particle:</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">Feret(max) [um]</td>
                                                <td>
                                                    <asp:TextBox ID="txtFeretLmsp" runat="server" Text="" CssClass="form-control" OnTextChanged="txtFeretLmsp_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtFeretLnms" runat="server" Text="" CssClass="form-control" OnTextChanged="txtFeretLnms_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtFeretFb" runat="server" Text="" CssClass="form-control" OnTextChanged="txtFeretFb_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </div>

                                </div>
                            </div>
                        </div>

                        <h4 class="caption-subject bold uppercase"><i class="fa fa-clone"></i>&nbsp;&nbsp;Microscopic Sample</h4>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label ID="Label54" runat="server" CssClass="control-label col-md-1">Select:</asp:Label>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlPer" runat="server" DataTextField="C" DataValueField="ID" CssClass="form-control" OnSelectedIndexChanged="ddlPer_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

                                    </div>
                                    <span class="input-group-btn"></span>
                                </div>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label ID="Label49" runat="server" CssClass="control-label col-md-1"></asp:Label>
                                    <div class="col-md-12">
                                        <asp:GridView ID="gvMicroscopicAnalysis" CssClass="table table-striped table-hover table-bordered" runat="server" AutoGenerateColumns="False" DataKeyNames="ID,row_status" OnRowDataBound="gvMicroscopicAnalysis_RowDataBound" OnRowCommand="gvMicroscopicAnalysis_RowCommand" OnDataBound="gvMicroscopicAnalysis_OnDataBound" OnRowCancelingEdit="gvMicroscopicAnalysis_RowCancelingEdit" OnRowDeleting="gvMicroscopicAnalysis_RowDeleting" OnRowEditing="gvMicroscopicAnalysis_RowEditing" OnRowUpdating="gvMicroscopicAnalysis_RowUpdating">
                                            <Columns>

                                                <asp:TemplateField HeaderText="Particle size[µm]" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litC" runat="server" Text='<%# Eval("col_c")%>'></asp:Literal>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtC" runat="server" Text='<%# Eval("col_c")%>' CssClass="form-control"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Code" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litD" runat="server" Text='<%# Eval("col_d")%>'></asp:Literal>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtD" runat="server" Text='<%# Eval("col_d")%>' CssClass="form-control"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Particles on membrane/<br>Total" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litE" runat="server" Text='<%# Eval("col_e")%>'></asp:Literal>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtE" runat="server" Text='<%# Eval("col_e")%>' CssClass="form-control"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Particles on membrane/<br>Non-metallic<br>shine" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litF" runat="server" Text='<%# Eval("col_f")%>'></asp:Literal>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtF" runat="server" Text='<%# Eval("col_f")%>' CssClass="form-control"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Particles on membrane/<br>Metallic<br>shine" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litG" runat="server" Text='<%# Eval("col_g")%>'></asp:Literal>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtG" runat="server" Text='<%# Eval("col_g")%>' CssClass="form-control"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Particles on membrane/<br>2Fiber" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litH" runat="server" Text='<%# Eval("col_h")%>'></asp:Literal>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtH" runat="server" Text='<%# Eval("col_h")%>' CssClass="form-control"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Particles per component/<br>Total" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litI" runat="server" Text='<%# Eval("col_i")%>'></asp:Literal>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtI" runat="server" Text='<%# Eval("col_i")%>' CssClass="form-control"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Particles per component/<br>Non-metallic<br>shine" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litJ" runat="server" Text='<%# Eval("col_j")%>'></asp:Literal>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtJ" runat="server" Text='<%# Eval("col_j")%>' CssClass="form-control"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Particles per component/<br>Metallic<br>shine" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litK" runat="server" Text='<%# Eval("col_k")%>'></asp:Literal>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtK" runat="server" Text='<%# Eval("col_k")%>' CssClass="form-control"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Particles per component/<br>2Fiber" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litL" runat="server" Text='<%# Eval("col_l")%>'></asp:Literal>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtL" runat="server" Text='<%# Eval("col_l")%>' CssClass="form-control"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
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
                                <%--  <div class="note note-success">
                                  Remark:	1Metallic + Non-metallic particles without fibers.
	2Fibers defined as Non-metallic, Length/Width > 10.                 
                                </div>
                                --%>
                            </div>
                            <!--/span-->

                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label col-md-3"></label>

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
                                                    <asp:FileUpload ID="FileUpload2" runat="server" AllowMultiple="True" />

                                                </span>
                                                <a href="javascript:;" class="input-group-addon btn red fileinput-exists" data-dismiss="fileinput">Remove </a>


                                            </div>
                                            <br />
                                            <asp:Button ID="btnLoadFile" runat="server" Text="Load Data" CssClass="btn blue-hoki" OnClick="btnLoadFile_Click" />

                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3"></label>
                                <div class="col-md-3">
                                </div>
                            </div>

                        </div>



                    </asp:Panel>
                    <asp:Panel ID="pPage06" runat="server">
                        <h4 class="caption-subject bold uppercase"><i class="fa fa-clone"></i>&nbsp;&nbsp;CCC (Component Cleanliness Code)</h4>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label ID="Label50" runat="server" CssClass="control-label col-md-1"></asp:Label>
                                    <div class="col-md-12">
                                        <table class="table table-striped table-bordered table-advance table-hover">
                                            <tr>
                                                <td>Summarized</td>
                                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Total</td>
                                                <td>Metallic shine</td>
                                            </tr>
                                            <tr>
                                                <td>per component</td>
                                                <td>
                                                    <asp:TextBox ID="txtPerComponentTotal" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtPerComponentMetallicShine" runat="server" CssClass="form-control"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>Extended<br />
                                                    (B/C/D/E/F/G/H/I/J/K)</td>
                                                <td style="text-decoration-style: dotted">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Total&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                <td>Metallic shine</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="lbPer" runat="server" Text="Per Component" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                <td style="text-decoration-style: dotted">
                                                    <asp:Label ID="lbPermembraneTotal" runat="server" Text="" CssClass="form-control"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                <td>
                                                    <asp:TextBox ID="txtPermembraneMetallicShine" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>

                                </div>
                                <div class="note note-success">
                                    Comments: All automatic counting have been revised manually.               
                                </div>
                                <div class="note note-success">
                                    1: Metallic shine+Non-metall shine particles without fibers.
                                    <br />
                                    2: fiber difined to Length/Width > 10
                                    <br />
                                    3: The system rounds the number of particles mathetically when to one compnent
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pPage07" runat="server">
                        <h4 class="caption-subject bold uppercase"><i class="fa fa-clone"></i>&nbsp;&nbsp;Cleanliness analysis according ISO 16232</h4>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <table class="table table-striped table-bordered table-advance table-hover">
                                            <tr>
                                                <td colspan="3" style="text-align: center">
                                                    <asp:Image ID="img2" runat="server" Style="width: 240px; height: 240px;" BorderStyle="Dotted" ImageUrl="~/images/no_img.png" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="text-align: center">Overview of filter</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table style="text-align: center">
                                                        <tr>
                                                            <td colspan="6">
                                                                <asp:Image ID="img3" runat="server" Style="width: 200px; height: 180px;" BorderStyle="Dotted" ImageUrl="~/images/no_img.png" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6" class="auto-style3">Largest metallic shine</td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;X&nbsp;</td>
                                                            <td>
                                                                <asp:TextBox ID="txtLms_X" runat="server" CssClass="form-control" Width="120px"></asp:TextBox></td>
                                                            <td>&nbsp;um,&nbsp;</td>
                                                            <td>&nbsp;Y&nbsp;</td>
                                                            <td>
                                                                <asp:TextBox ID="txtLms_Y" runat="server" CssClass="form-control" Width="120px"></asp:TextBox></td>
                                                            <td>&nbsp;um&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table style="text-align: center">
                                                        <tr>
                                                            <td colspan="6" style="text-align: center">
                                                                <asp:Image ID="img4" runat="server" Style="width: 200px; height: 180px;" BorderStyle="Dotted" ImageUrl="~/images/no_img.png" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6">Largest non-metallic shine</td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;X&nbsp;</td>
                                                            <td>
                                                                <asp:TextBox ID="txtLnms_X" runat="server" CssClass="form-control" Width="120px"></asp:TextBox></td>
                                                            <td>&nbsp;um,&nbsp;</td>
                                                            <td>&nbsp;Y&nbsp;</td>
                                                            <td>
                                                                <asp:TextBox ID="txtLnms_Y" runat="server" CssClass="form-control" Width="120px"></asp:TextBox></td>
                                                            <td>&nbsp;um&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table style="text-align: center">
                                                        <tr>
                                                            <td colspan="6" style="text-align: center">
                                                                <asp:Image ID="img5" runat="server" Style="width: 200px; height: 180px;" BorderStyle="Dotted" ImageUrl="~/images/no_img.png" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="6">Largest fiber</td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;&nbsp;</td>
                                                            <td>
                                                                <asp:TextBox ID="txtLf_X" runat="server" CssClass="form-control" Width="120px" Visible="false"></asp:TextBox></td>
                                                            <td>&nbsp;&nbsp;</td>
                                                            <td>&nbsp;Y&nbsp;</td>
                                                            <td>
                                                                <asp:TextBox ID="txtLf_Y" runat="server" CssClass="form-control" Width="120px"></asp:TextBox></td>
                                                            <td>&nbsp;um&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label col-md-3">Overview of filter</label>

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
                                            <br />


                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3"></label>
                                <div class="col-md-3">
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label col-md-3">Largest metallic shine</label>

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
                                            <br />


                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3"></label>
                                <div class="col-md-3">
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label col-md-3">Largest non-metallic shine</label>

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
                                            <br />


                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3"></label>
                                <div class="col-md-3">
                                </div>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label col-md-3">Largest fiber</label>

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
                                            <br />
                                            <asp:Button ID="btnLoadImg" runat="server" Text="Load" CssClass="btn blue" OnClick="btnLoadImg_Click" />


                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-md-3"></label>
                                <div class="col-md-3">
                                </div>
                            </div>

                        </div>



                    </asp:Panel>
                    <asp:Panel ID="pPage08" runat="server">
                        <h4 class="caption-subject bold uppercase"><i class="fa fa-clone"></i>&nbsp;&nbsp;Particle nature determination by SEM/EDX</h4>



                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <table class="table table-striped table-bordered table-advance table-hover">
                                            <tr>
                                                <td colspan="2"><b>Image</b></td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center">
                                                    <asp:Image ID="Image1" runat="server" Style="width: 500px; height: 350px;" BorderStyle="Dotted" ImageUrl="~/images/no_img.png" />
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td colspan="2"><b>SEM/EDX Parameters</b></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Magnification:</td>
                                                            <td>
                                                                <asp:TextBox ID="txtParamMagnification1" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>WD:</td>
                                                            <td>
                                                                <asp:TextBox ID="txtParamWd1" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>EHT:</td>
                                                            <td>
                                                                <asp:TextBox ID="txtParamEht1" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>Detector:</td>
                                                            <td>
                                                                <asp:TextBox ID="txtParamDetector1" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">&nbsp;

                                                      <div class="fileinput fileinput-new" data-provides="fileinput">
                                                          <div class="input-group input-large">
                                                              <div class="form-control uneditable-input input-fixed input-large" data-trigger="fileinput">
                                                                  <i class="fa fa-file fileinput-exists"></i>&nbsp; <span class="fileinput-filename"></span>
                                                              </div>
                                                              <span class="input-group-addon btn default btn-file"><span class="fileinput-new">Select file </span><span class="fileinput-exists">Change </span>
                                                                  <asp:FileUpload ID="fileUpload3" runat="server" />
                                                              </span><a class="input-group-addon btn red fileinput-exists" data-dismiss="fileinput" href="javascript:;">Remove </a>
                                                          </div>
                                                          <br />
                                                          <asp:Button ID="btnLoadParamImg1" runat="server" CssClass="btn blue" OnClick="btnLoadParamImg1_Click" Text="Load" />
                                                      </div>


                                                            </td>
                                                        </tr>
                                                    </table>

                                                </td>

                                            </tr>
                                            <tr>
                                                <td colspan="2"><b>SEM photograph of the largest metallic shine particle by SEM/EDX</b></td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center">
                                                    <asp:Image ID="Image2" runat="server" BorderStyle="Dotted" ImageUrl="~/images/no_img.png" Style="width: 500px; height: 350px;" />
                                                </td>
                                                <td>&nbsp;
                                                    <table>
                                                        <tr>
                                                            <td colspan="3"><b>Element composition:</b></td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtEC01" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                            <td>
                                                                <asp:TextBox ID="txtEC01_SHOT" runat="server" CssClass="form-control" Width="100"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnAdd" runat="server" CssClass="btn blue" Text="Add" OnClick="btnAdd_Click" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:GridView ID="gvCompositionElement" DataKeyNames="id" runat="server" CssClass="table table-striped table-bordered table-advance table-hover" AutoGenerateColumns="False" OnRowDeleting="gvCompositionElement_RowDeleting">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="col_e" HeaderText="Element" HeaderStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="col_f" HeaderText="Element(acronym)" />
                                                                        <asp:TemplateField HeaderText="">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="btnDelete" runat="server" ToolTip="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                                                                    CommandArgument='<%# Eval("id")%>'><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                                            </ItemTemplate>

                                                                        </asp:TemplateField>
                                                                    </Columns>

                                                                </asp:GridView>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                        </tr>

                                                        <tr>
                                                            <td colspan="4">&nbsp;

                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <div class="fileinput fileinput-new" data-provides="fileinput">
                                                                    <div class="input-group input-large">
                                                                        <div class="form-control uneditable-input input-fixed input-large" data-trigger="fileinput">
                                                                            <i class="fa fa-file fileinput-exists"></i>&nbsp; <span class="fileinput-filename"></span>
                                                                        </div>
                                                                        <span class="input-group-addon btn default btn-file"><span class="fileinput-new">Select file </span><span class="fileinput-exists">Change </span>
                                                                            <asp:FileUpload ID="fileUpload4" runat="server" />
                                                                        </span><a class="input-group-addon btn red fileinput-exists" data-dismiss="fileinput" href="javascript:;">Remove </a>
                                                                    </div>
                                                                    <br />
                                                                    <asp:Button ID="btnLoadParamImg2" runat="server" CssClass="btn blue" OnClick="btnLoadParamImg2_Click" Text="Load" />
                                                                </div>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2"><b>EDX spectram of the largest metallic shine particle</b></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <table class="table table-striped table-bordered table-advance table-hover">
                                            <tr>
                                                <td colspan="2"><b>Image</b></td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center">
                                                    <asp:Image ID="Image3" runat="server" Style="width: 500px; height: 350px;" BorderStyle="Dotted" ImageUrl="~/images/no_img.png" />
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td colspan="2"><b>SEM/EDX Parameters</b></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Magnification:</td>
                                                            <td>
                                                                <asp:TextBox ID="txtParamMagnification2" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>WD:</td>
                                                            <td>
                                                                <asp:TextBox ID="txtParamWd2" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>EHT:</td>
                                                            <td>
                                                                <asp:TextBox ID="txtParamEht2" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>Detector:</td>
                                                            <td>
                                                                <asp:TextBox ID="txtParamDetector2" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">&nbsp;
                                                                <div class="fileinput fileinput-new" data-provides="fileinput">
                                                                    <div class="input-group input-large">
                                                                        <div class="form-control uneditable-input input-fixed input-large" data-trigger="fileinput">
                                                                            <i class="fa fa-file fileinput-exists"></i>&nbsp; <span class="fileinput-filename"></span>
                                                                        </div>
                                                                        <span class="input-group-addon btn default btn-file"><span class="fileinput-new">Select file </span><span class="fileinput-exists">Change </span>
                                                                            <asp:FileUpload ID="fileUpload5" runat="server" />
                                                                        </span><a class="input-group-addon btn red fileinput-exists" data-dismiss="fileinput" href="javascript:;">Remove </a>
                                                                    </div>
                                                                    <br />
                                                                    <asp:Button ID="btnLoadParamImg3" runat="server" CssClass="btn blue" OnClick="btnLoadParamImg3_Click" Text="Load" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>

                                                </td>

                                            </tr>
                                            <tr>
                                                <td colspan="2"><b>SEM photograph of Middle member by SEM/EDX</b></td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center">
                                                    <asp:Image ID="Image4" runat="server" BorderStyle="Dotted" ImageUrl="~/images/no_img.png" Style="width: 500px; height: 350px;" />
                                                </td>
                                                <td>&nbsp;
                                                    <table>
                                                        <tr>
                                                            <td colspan="3"><b>Element composition:</b></td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtEC02" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                            <td>
                                                                <asp:TextBox ID="txtEC02_SHOT" runat="server" CssClass="form-control" Width="100"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnAdd2" runat="server" CssClass="btn blue" Text="Add" OnClick="btnAdd2_Click" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:GridView ID="gvCompositionElement2" DataKeyNames="id" runat="server" CssClass="table table-striped table-bordered table-advance table-hover" AutoGenerateColumns="False" OnRowDeleting="gvCompositionElement2_RowDeleting">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="col_e" HeaderText="Element" HeaderStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="col_f" HeaderText="Element(acronym)" />
                                                                        <asp:TemplateField HeaderText="">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="btnDelete" runat="server" ToolTip="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                                                                    CommandArgument='<%# Eval("id")%>'><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                                            </ItemTemplate>

                                                                        </asp:TemplateField>
                                                                    </Columns>

                                                                </asp:GridView>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                        </tr>

                                                        <tr>
                                                            <td colspan="4">&nbsp;

                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <div class="fileinput fileinput-new" data-provides="fileinput">
                                                                    <div class="input-group input-large">
                                                                        <div class="form-control uneditable-input input-fixed input-large" data-trigger="fileinput">
                                                                            <i class="fa fa-file fileinput-exists"></i>&nbsp; <span class="fileinput-filename"></span>
                                                                        </div>
                                                                        <span class="input-group-addon btn default btn-file"><span class="fileinput-new">Select file </span><span class="fileinput-exists">Change </span>
                                                                            <asp:FileUpload ID="fileUpload6" runat="server" />
                                                                        </span><a class="input-group-addon btn red fileinput-exists" data-dismiss="fileinput" href="javascript:;">Remove </a>
                                                                    </div>
                                                                    <br />
                                                                    <asp:Button ID="btnLoadParamImg4" runat="server" CssClass="btn blue" OnClick="btnLoadParamImg4_Click" Text="Load" />
                                                                </div>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2"><b>EDX spectrum of Middle member</b></td>
                                            </tr>
                                        </table>
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
                                            :: Setup Float ::</h>
                                </div>
                                <div class="modal-body" style="width: 600px; height: 400px; overflow-x: hidden; overflow-y: scroll; padding-bottom: 10px;">
                                    <table class="table table-striped">
                                        <tr>
                                            <td colspan="2">Particles per component</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">Total:</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal01" runat="server" TextMode="Number" CssClass="form-control" Text="2"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">Non-metallic shine:</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal02" runat="server" TextMode="Number" CssClass="form-control" Text="2"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">Metallic shine:</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal03" runat="server" TextMode="Number" CssClass="form-control" Text="2"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">Fiber:</td>
                                            <td>
                                                <asp:TextBox ID="txtDecimal04" runat="server" TextMode="Number" CssClass="form-control" Text="2"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:Button ID="btnSrChemistTest" runat="server" Text="Sr.Chemist (Calulate)" CssClass="btn btn-default btn-sm" OnClick="btnSrChemistTest_Click" /></td>
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
            <asp:PostBackTrigger ControlID="btnLoadParamImg1" />
            <asp:PostBackTrigger ControlID="btnLoadParamImg2" />
            <asp:PostBackTrigger ControlID="btnLoadParamImg3" />
            <asp:PostBackTrigger ControlID="btnLoadParamImg4" />

            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="lbDownload" />
            <%--                        <asp:PostBackTrigger ControlID="btnShowUnit" />--%>
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
