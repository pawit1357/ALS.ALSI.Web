<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FT_IC.ascx.cs" Inherits="ALS.ALSI.Web.view.template.FT_IC" %>
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
                            <div class="col-md-12">
                                <h5>METHOD/PROCEDURE:
                                     <asp:GridView CssClass="table table-striped table-bordered table-advance table-hover" ID="gvMethodProcedure" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowDataBound="gvMethodProcedure_RowDataBound" OnRowCommand="gvMethodProcedure_RowCommand" OnRowCancelingEdit="gvMethodProcedure_RowCancelingEdit" OnRowDeleting="gvMethodProcedure_RowDeleting" OnRowEditing="gvMethodProcedure_RowEditing" OnRowUpdating="gvMethodProcedure_RowUpdating">
                                         <Columns>
                                             <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lb_col_1" runat="server" Text='<%# Eval("col_1")%>'></asp:Label>
                                                 </ItemTemplate>
                                                 <EditItemTemplate>
                                                     <asp:TextBox ID="txt_col_1" runat="server" Text='<%# Eval("col_1")%>' CssClass="form-control"></asp:TextBox>
                                                 </EditItemTemplate>
                                                 <ItemStyle HorizontalAlign="Right" />
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lb_col_2" runat="server" Text='<%# Eval("col_2")%>'></asp:Label>
                                                 </ItemTemplate>
                                                 <EditItemTemplate>
                                                     <asp:TextBox ID="txt_col_2" runat="server" Text='<%# Eval("col_2")%>' CssClass="form-control"></asp:TextBox>
                                                 </EditItemTemplate>
                                                 <ItemStyle HorizontalAlign="Right" />
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lb_col_3" runat="server" Text='<%# Eval("col_3")%>'></asp:Label>
                                                 </ItemTemplate>
                                                 <EditItemTemplate>
                                                     <asp:TextBox ID="txt_col_3" runat="server" Text='<%# Eval("col_3")%>' CssClass="form-control"></asp:TextBox>
                                                 </EditItemTemplate>
                                                 <ItemStyle HorizontalAlign="Right" />
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lb_col_4" runat="server" Text='<%# Eval("col_4")%>'></asp:Label>
                                                 </ItemTemplate>
                                                 <EditItemTemplate>
                                                     <asp:TextBox ID="txt_col_4" runat="server" Text='<%# Eval("col_4")%>' CssClass="form-control"></asp:TextBox>
                                                 </EditItemTemplate>
                                                 <ItemStyle HorizontalAlign="Right" />
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lb_col_5" runat="server" Text='<%# Eval("col_5")%>'></asp:Label>
                                                 </ItemTemplate>
                                                 <EditItemTemplate>
                                                     <asp:TextBox ID="txt_col_5" runat="server" Text='<%# Eval("col_5")%>' CssClass="form-control"></asp:TextBox>
                                                 </EditItemTemplate>
                                                 <ItemStyle HorizontalAlign="Right" />
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lb_col_6" runat="server" Text='<%# Eval("col_6")%>'></asp:Label>
                                                 </ItemTemplate>
                                                 <EditItemTemplate>
                                                     <asp:TextBox ID="txt_col_6" runat="server" Text='<%# Eval("col_6")%>' CssClass="form-control"></asp:TextBox>
                                                 </EditItemTemplate>
                                                 <ItemStyle HorizontalAlign="Right" />
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lb_col_7" runat="server" Text='<%# Eval("col_7")%>'></asp:Label>
                                                 </ItemTemplate>
                                                 <EditItemTemplate>
                                                     <asp:TextBox ID="txt_col_7" runat="server" Text='<%# Eval("col_7")%>' CssClass="form-control"></asp:TextBox>
                                                 </EditItemTemplate>
                                                 <ItemStyle HorizontalAlign="Right" />
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lb_col_8" runat="server" Text='<%# Eval("col_8")%>'></asp:Label>
                                                 </ItemTemplate>
                                                 <EditItemTemplate>
                                                     <asp:TextBox ID="txt_col_8" runat="server" Text='<%# Eval("col_8")%>' CssClass="form-control"></asp:TextBox>
                                                 </EditItemTemplate>
                                                 <ItemStyle HorizontalAlign="Right" />
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lb_col_9" runat="server" Text='<%# Eval("col_9")%>'></asp:Label>
                                                 </ItemTemplate>
                                                 <EditItemTemplate>
                                                     <asp:TextBox ID="txt_col_9" runat="server" Text='<%# Eval("col_9")%>' CssClass="form-control"></asp:TextBox>
                                                 </EditItemTemplate>
                                                 <ItemStyle HorizontalAlign="Right" />
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lb_col_10" runat="server" Text='<%# Eval("col_10")%>'></asp:Label>
                                                 </ItemTemplate>
                                                 <EditItemTemplate>
                                                     <asp:TextBox ID="txt_col_10" runat="server" Text='<%# Eval("col_10")%>' CssClass="form-control"></asp:TextBox>
                                                 </EditItemTemplate>
                                                 <ItemStyle HorizontalAlign="Right" />
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lb_col_11" runat="server" Text='<%# Eval("col_11")%>'></asp:Label>
                                                 </ItemTemplate>
                                                 <EditItemTemplate>
                                                     <asp:TextBox ID="txt_col_11" runat="server" Text='<%# Eval("col_11")%>' CssClass="form-control"></asp:TextBox>
                                                 </EditItemTemplate>
                                                 <ItemStyle HorizontalAlign="Right" />
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lb_col_12" runat="server" Text='<%# Eval("col_12")%>'></asp:Label>
                                                 </ItemTemplate>
                                                 <EditItemTemplate>
                                                     <asp:TextBox ID="txt_col_12" runat="server" Text='<%# Eval("col_12")%>' CssClass="form-control"></asp:TextBox>
                                                 </EditItemTemplate>
                                                 <ItemStyle HorizontalAlign="Right" />
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lb_col_13" runat="server" Text='<%# Eval("col_13")%>'></asp:Label>
                                                 </ItemTemplate>
                                                 <EditItemTemplate>
                                                     <asp:TextBox ID="txt_col_13" runat="server" Text='<%# Eval("col_13")%>' CssClass="form-control"></asp:TextBox>
                                                 </EditItemTemplate>
                                                 <ItemStyle HorizontalAlign="Right" />
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lb_col_14" runat="server" Text='<%# Eval("col_14")%>'></asp:Label>
                                                 </ItemTemplate>
                                                 <EditItemTemplate>
                                                     <asp:TextBox ID="txt_col_14" runat="server" Text='<%# Eval("col_14")%>' CssClass="form-control"></asp:TextBox>
                                                 </EditItemTemplate>
                                                 <ItemStyle HorizontalAlign="Right" />
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lb_col_15" runat="server" Text='<%# Eval("col_15")%>'></asp:Label>
                                                 </ItemTemplate>
                                                 <EditItemTemplate>
                                                     <asp:TextBox ID="txt_col_15" runat="server" Text='<%# Eval("col_15")%>' CssClass="form-control"></asp:TextBox>
                                                 </EditItemTemplate>
                                                 <ItemStyle HorizontalAlign="Right" />
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lb_col_16" runat="server" Text='<%# Eval("col_16")%>'></asp:Label>
                                                 </ItemTemplate>
                                                 <EditItemTemplate>
                                                     <asp:TextBox ID="txt_col_16" runat="server" Text='<%# Eval("col_16")%>' CssClass="form-control"></asp:TextBox>
                                                 </EditItemTemplate>
                                                 <ItemStyle HorizontalAlign="Right" />
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lb_col_17" runat="server" Text='<%# Eval("col_17")%>'></asp:Label>
                                                 </ItemTemplate>
                                                 <EditItemTemplate>
                                                     <asp:TextBox ID="txt_col_17" runat="server" Text='<%# Eval("col_17")%>' CssClass="form-control"></asp:TextBox>
                                                 </EditItemTemplate>
                                                 <ItemStyle HorizontalAlign="Right" />
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lb_col_18" runat="server" Text='<%# Eval("col_18")%>'></asp:Label>
                                                 </ItemTemplate>
                                                 <EditItemTemplate>
                                                     <asp:TextBox ID="txt_col_18" runat="server" Text='<%# Eval("col_18")%>' CssClass="form-control"></asp:TextBox>
                                                 </EditItemTemplate>
                                                 <ItemStyle HorizontalAlign="Right" />
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lb_col_19" runat="server" Text='<%# Eval("col_19")%>'></asp:Label>
                                                 </ItemTemplate>
                                                 <EditItemTemplate>
                                                     <asp:TextBox ID="txt_col_19" runat="server" Text='<%# Eval("col_19")%>' CssClass="form-control"></asp:TextBox>
                                                 </EditItemTemplate>
                                                 <ItemStyle HorizontalAlign="Right" />
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lb_col_20" runat="server" Text='<%# Eval("col_20")%>'></asp:Label>
                                                 </ItemTemplate>
                                                 <EditItemTemplate>
                                                     <asp:TextBox ID="txt_col_20" runat="server" Text='<%# Eval("col_20")%>' CssClass="form-control"></asp:TextBox>
                                                 </EditItemTemplate>
                                                 <ItemStyle HorizontalAlign="Right" />
                                             </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Edit">
                                                 <ItemTemplate>
                                                     <asp:LinkButton ID="btnEdit" runat="server" ToolTip="Edit" CommandName="Edit" CommandArgument='<%# Eval("id")%>' CausesValidation="false"><i class="fa fa-edit"></i></asp:LinkButton>
                                                 </ItemTemplate>
                                                 <EditItemTemplate>
                                                     <asp:LinkButton ID="btnUpdate" runat="server" ToolTip="Update" CausesValidation="false"
                                                         CommandName="Update"><i class="fa fa-save"></i></asp:LinkButton>
                                                     <asp:LinkButton ID="LinkCancel" runat="server" ToolTip="Cancel" CausesValidation="false"
                                                         CommandName="Cancel"><i class="fa fa-remove"></i></asp:LinkButton>
                                                 </EditItemTemplate>

                                             </asp:TemplateField>

                                         </Columns>
                                     </asp:GridView>


                                </h5>

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

                                <asp:GridView CssClass="table table-striped table-bordered table-advance table-hover" ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowDataBound="gvMethodProcedure_RowDataBound" OnRowCommand="gvMethodProcedure_RowCommand" OnRowCancelingEdit="gvMethodProcedure_RowCancelingEdit" OnRowDeleting="gvMethodProcedure_RowDeleting" OnRowEditing="gvMethodProcedure_RowEditing" OnRowUpdating="gvMethodProcedure_RowUpdating">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_1" runat="server" Text='<%# Eval("col_1")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_1" runat="server" Text='<%# Eval("col_1")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_2" runat="server" Text='<%# Eval("col_2")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_2" runat="server" Text='<%# Eval("col_2")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_3" runat="server" Text='<%# Eval("col_3")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_3" runat="server" Text='<%# Eval("col_3")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_4" runat="server" Text='<%# Eval("col_4")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_4" runat="server" Text='<%# Eval("col_4")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_5" runat="server" Text='<%# Eval("col_5")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_5" runat="server" Text='<%# Eval("col_5")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_6" runat="server" Text='<%# Eval("col_6")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_6" runat="server" Text='<%# Eval("col_6")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_7" runat="server" Text='<%# Eval("col_7")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_7" runat="server" Text='<%# Eval("col_7")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_8" runat="server" Text='<%# Eval("col_8")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_8" runat="server" Text='<%# Eval("col_8")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_9" runat="server" Text='<%# Eval("col_9")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_9" runat="server" Text='<%# Eval("col_9")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_10" runat="server" Text='<%# Eval("col_10")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_10" runat="server" Text='<%# Eval("col_10")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_11" runat="server" Text='<%# Eval("col_11")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_11" runat="server" Text='<%# Eval("col_11")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_12" runat="server" Text='<%# Eval("col_12")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_12" runat="server" Text='<%# Eval("col_12")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_13" runat="server" Text='<%# Eval("col_13")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_13" runat="server" Text='<%# Eval("col_13")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_14" runat="server" Text='<%# Eval("col_14")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_14" runat="server" Text='<%# Eval("col_14")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_15" runat="server" Text='<%# Eval("col_15")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_15" runat="server" Text='<%# Eval("col_15")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_16" runat="server" Text='<%# Eval("col_16")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_16" runat="server" Text='<%# Eval("col_16")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_17" runat="server" Text='<%# Eval("col_17")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_17" runat="server" Text='<%# Eval("col_17")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_18" runat="server" Text='<%# Eval("col_18")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_18" runat="server" Text='<%# Eval("col_18")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_19" runat="server" Text='<%# Eval("col_19")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_19" runat="server" Text='<%# Eval("col_19")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_20" runat="server" Text='<%# Eval("col_20")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_20" runat="server" Text='<%# Eval("col_20")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" ToolTip="Edit" CommandName="Edit" CommandArgument='<%# Eval("id")%>' CausesValidation="false"><i class="fa fa-edit"></i></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="btnUpdate" runat="server" ToolTip="Update" CausesValidation="false"
                                                    CommandName="Update"><i class="fa fa-save"></i></asp:LinkButton>
                                                <asp:LinkButton ID="LinkCancel" runat="server" ToolTip="Cancel" CausesValidation="false"
                                                    CommandName="Cancel"><i class="fa fa-remove"></i></asp:LinkButton>
                                            </EditItemTemplate>

                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                                <br />
                                <asp:GridView CssClass="table table-striped table-bordered table-advance table-hover" ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowDataBound="gvMethodProcedure_RowDataBound" OnRowCommand="gvMethodProcedure_RowCommand" OnRowCancelingEdit="gvMethodProcedure_RowCancelingEdit" OnRowDeleting="gvMethodProcedure_RowDeleting" OnRowEditing="gvMethodProcedure_RowEditing" OnRowUpdating="gvMethodProcedure_RowUpdating">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_1" runat="server" Text='<%# Eval("col_1")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_1" runat="server" Text='<%# Eval("col_1")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_2" runat="server" Text='<%# Eval("col_2")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_2" runat="server" Text='<%# Eval("col_2")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_3" runat="server" Text='<%# Eval("col_3")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_3" runat="server" Text='<%# Eval("col_3")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_4" runat="server" Text='<%# Eval("col_4")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_4" runat="server" Text='<%# Eval("col_4")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_5" runat="server" Text='<%# Eval("col_5")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_5" runat="server" Text='<%# Eval("col_5")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_6" runat="server" Text='<%# Eval("col_6")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_6" runat="server" Text='<%# Eval("col_6")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_7" runat="server" Text='<%# Eval("col_7")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_7" runat="server" Text='<%# Eval("col_7")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_8" runat="server" Text='<%# Eval("col_8")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_8" runat="server" Text='<%# Eval("col_8")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_9" runat="server" Text='<%# Eval("col_9")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_9" runat="server" Text='<%# Eval("col_9")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_10" runat="server" Text='<%# Eval("col_10")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_10" runat="server" Text='<%# Eval("col_10")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_11" runat="server" Text='<%# Eval("col_11")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_11" runat="server" Text='<%# Eval("col_11")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_12" runat="server" Text='<%# Eval("col_12")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_12" runat="server" Text='<%# Eval("col_12")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_13" runat="server" Text='<%# Eval("col_13")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_13" runat="server" Text='<%# Eval("col_13")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_14" runat="server" Text='<%# Eval("col_14")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_14" runat="server" Text='<%# Eval("col_14")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_15" runat="server" Text='<%# Eval("col_15")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_15" runat="server" Text='<%# Eval("col_15")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_16" runat="server" Text='<%# Eval("col_16")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_16" runat="server" Text='<%# Eval("col_16")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_17" runat="server" Text='<%# Eval("col_17")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_17" runat="server" Text='<%# Eval("col_17")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_18" runat="server" Text='<%# Eval("col_18")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_18" runat="server" Text='<%# Eval("col_18")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_19" runat="server" Text='<%# Eval("col_19")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_19" runat="server" Text='<%# Eval("col_19")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_20" runat="server" Text='<%# Eval("col_20")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_20" runat="server" Text='<%# Eval("col_20")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" ToolTip="Edit" CommandName="Edit" CommandArgument='<%# Eval("id")%>' CausesValidation="false"><i class="fa fa-edit"></i></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="btnUpdate" runat="server" ToolTip="Update" CausesValidation="false"
                                                    CommandName="Update"><i class="fa fa-save"></i></asp:LinkButton>
                                                <asp:LinkButton ID="LinkCancel" runat="server" ToolTip="Cancel" CausesValidation="false"
                                                    CommandName="Cancel"><i class="fa fa-remove"></i></asp:LinkButton>
                                            </EditItemTemplate>

                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>

                            </div>
                        </div>

                    </asp:Panel>

                    <asp:Panel ID="pWorkingIC" runat="server">
                        <asp:Panel ID="pLoadFile" runat="server">
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
                                <asp:GridView ID="GridView5" runat="server" CssClass="table table-striped table-bordered table-advance table-hover"></asp:GridView>
                                <asp:GridView CssClass="table table-striped table-bordered table-advance table-hover" ID="GridView3" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowDataBound="gvMethodProcedure_RowDataBound" OnRowCommand="gvMethodProcedure_RowCommand" OnRowCancelingEdit="gvMethodProcedure_RowCancelingEdit" OnRowDeleting="gvMethodProcedure_RowDeleting" OnRowEditing="gvMethodProcedure_RowEditing" OnRowUpdating="gvMethodProcedure_RowUpdating">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_1" runat="server" Text='<%# Eval("col_1")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_1" runat="server" Text='<%# Eval("col_1")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_2" runat="server" Text='<%# Eval("col_2")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_2" runat="server" Text='<%# Eval("col_2")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_3" runat="server" Text='<%# Eval("col_3")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_3" runat="server" Text='<%# Eval("col_3")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_4" runat="server" Text='<%# Eval("col_4")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_4" runat="server" Text='<%# Eval("col_4")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_5" runat="server" Text='<%# Eval("col_5")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_5" runat="server" Text='<%# Eval("col_5")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_6" runat="server" Text='<%# Eval("col_6")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_6" runat="server" Text='<%# Eval("col_6")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_7" runat="server" Text='<%# Eval("col_7")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_7" runat="server" Text='<%# Eval("col_7")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_8" runat="server" Text='<%# Eval("col_8")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_8" runat="server" Text='<%# Eval("col_8")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_9" runat="server" Text='<%# Eval("col_9")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_9" runat="server" Text='<%# Eval("col_9")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_10" runat="server" Text='<%# Eval("col_10")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_10" runat="server" Text='<%# Eval("col_10")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_11" runat="server" Text='<%# Eval("col_11")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_11" runat="server" Text='<%# Eval("col_11")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_12" runat="server" Text='<%# Eval("col_12")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_12" runat="server" Text='<%# Eval("col_12")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_13" runat="server" Text='<%# Eval("col_13")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_13" runat="server" Text='<%# Eval("col_13")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_14" runat="server" Text='<%# Eval("col_14")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_14" runat="server" Text='<%# Eval("col_14")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_15" runat="server" Text='<%# Eval("col_15")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_15" runat="server" Text='<%# Eval("col_15")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_16" runat="server" Text='<%# Eval("col_16")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_16" runat="server" Text='<%# Eval("col_16")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_17" runat="server" Text='<%# Eval("col_17")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_17" runat="server" Text='<%# Eval("col_17")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_18" runat="server" Text='<%# Eval("col_18")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_18" runat="server" Text='<%# Eval("col_18")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_19" runat="server" Text='<%# Eval("col_19")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_19" runat="server" Text='<%# Eval("col_19")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_20" runat="server" Text='<%# Eval("col_20")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_20" runat="server" Text='<%# Eval("col_20")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" ToolTip="Edit" CommandName="Edit" CommandArgument='<%# Eval("id")%>' CausesValidation="false"><i class="fa fa-edit"></i></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="btnUpdate" runat="server" ToolTip="Update" CausesValidation="false"
                                                    CommandName="Update"><i class="fa fa-save"></i></asp:LinkButton>
                                                <asp:LinkButton ID="LinkCancel" runat="server" ToolTip="Cancel" CausesValidation="false"
                                                    CommandName="Cancel"><i class="fa fa-remove"></i></asp:LinkButton>
                                            </EditItemTemplate>

                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                                <br />
                                <asp:GridView CssClass="table table-striped table-bordered table-advance table-hover" ID="GridView4" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowDataBound="gvMethodProcedure_RowDataBound" OnRowCommand="gvMethodProcedure_RowCommand" OnRowCancelingEdit="gvMethodProcedure_RowCancelingEdit" OnRowDeleting="gvMethodProcedure_RowDeleting" OnRowEditing="gvMethodProcedure_RowEditing" OnRowUpdating="gvMethodProcedure_RowUpdating">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_1" runat="server" Text='<%# Eval("col_1")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_1" runat="server" Text='<%# Eval("col_1")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_2" runat="server" Text='<%# Eval("col_2")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_2" runat="server" Text='<%# Eval("col_2")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_3" runat="server" Text='<%# Eval("col_3")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_3" runat="server" Text='<%# Eval("col_3")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_4" runat="server" Text='<%# Eval("col_4")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_4" runat="server" Text='<%# Eval("col_4")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_5" runat="server" Text='<%# Eval("col_5")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_5" runat="server" Text='<%# Eval("col_5")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_6" runat="server" Text='<%# Eval("col_6")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_6" runat="server" Text='<%# Eval("col_6")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_7" runat="server" Text='<%# Eval("col_7")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_7" runat="server" Text='<%# Eval("col_7")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_8" runat="server" Text='<%# Eval("col_8")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_8" runat="server" Text='<%# Eval("col_8")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_9" runat="server" Text='<%# Eval("col_9")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_9" runat="server" Text='<%# Eval("col_9")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_10" runat="server" Text='<%# Eval("col_10")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_10" runat="server" Text='<%# Eval("col_10")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_11" runat="server" Text='<%# Eval("col_11")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_11" runat="server" Text='<%# Eval("col_11")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_12" runat="server" Text='<%# Eval("col_12")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_12" runat="server" Text='<%# Eval("col_12")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_13" runat="server" Text='<%# Eval("col_13")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_13" runat="server" Text='<%# Eval("col_13")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_14" runat="server" Text='<%# Eval("col_14")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_14" runat="server" Text='<%# Eval("col_14")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_15" runat="server" Text='<%# Eval("col_15")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_15" runat="server" Text='<%# Eval("col_15")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_16" runat="server" Text='<%# Eval("col_16")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_16" runat="server" Text='<%# Eval("col_16")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_17" runat="server" Text='<%# Eval("col_17")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_17" runat="server" Text='<%# Eval("col_17")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_18" runat="server" Text='<%# Eval("col_18")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_18" runat="server" Text='<%# Eval("col_18")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_19" runat="server" Text='<%# Eval("col_19")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_19" runat="server" Text='<%# Eval("col_19")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lb_col_20" runat="server" Text='<%# Eval("col_20")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_col_20" runat="server" Text='<%# Eval("col_20")%>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" ToolTip="Edit" CommandName="Edit" CommandArgument='<%# Eval("id")%>' CausesValidation="false"><i class="fa fa-edit"></i></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="btnUpdate" runat="server" ToolTip="Update" CausesValidation="false"
                                                    CommandName="Update"><i class="fa fa-save"></i></asp:LinkButton>
                                                <asp:LinkButton ID="LinkCancel" runat="server" ToolTip="Cancel" CausesValidation="false"
                                                    CommandName="Cancel"><i class="fa fa-remove"></i></asp:LinkButton>
                                            </EditItemTemplate>

                                        </asp:TemplateField>

                                    </Columns>
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
                                        <div class="form-group">
                                            <label class="control-label col-md-3">Component:<span class="required">*</span></label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlComponent" runat="server" class="select2_category form-control" DataTextField="A" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlComponent_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
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
                                                    <asp:Label ID="lbDownloadName" runat="server" Text="Download"></asp:Label>
                                                </asp:LinkButton>
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
                                            <%--                                            <asp:DropDownList ID="ddlUnit" runat="server" class="select2_category form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged" DataValueField="ID" DataTextField="Name">
                                            </asp:DropDownList>--%>

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
