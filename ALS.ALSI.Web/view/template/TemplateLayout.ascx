<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TemplateLayout.ascx.cs" Inherits="ALS.ALSI.Web.view.template.TemplateLayout" %>
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
                        <asp:Button ID="btnDHS" runat="server" Text="DHS" CssClass="btn btn-default btn-sm" OnClick="btnCoverPage_Click" />
                        <asp:LinkButton ID="lbDecimal" runat="server" OnClick="LinkButton1_Click" CssClass="btn btn-default"> <i class="fa fa-sort-numeric-asc"></i> ตั้งค่า</asp:LinkButton>

                    </div>
                </div>
                <div class="portlet-body">
                    <asp:Panel ID="pCoverpage" runat="server">
                        <div class="row" id="invDiv" runat="server">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-8">
                                        <h5>METHOD/PROCEDURE:</h5>
                                            <asp:GridView ID="gvMp" runat="server" AutoGenerateColumns="false"
                                                CssClass="table table-striped table-bordered mini" ShowHeaderWhenEmpty="True" ShowFooter="True" DataKeyNames="ID,row_type" OnRowDataBound="gvMp_RowDataBound" OnRowCommand="gvMp_RowCommand" OnRowCancelingEdit="gvMp_RowCancelingEdit" OnRowEditing="gvMp_RowEditing" OnRowUpdating="gvMp_RowUpdating">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="HC01" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litC01" runat="server" Text='<%# Eval("C01")%>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtC01" runat="server" Text='<%# Eval("C01")%>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="HC02" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litC02" runat="server" Text='<%# Eval("C02")%>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtC02" runat="server" Text='<%# Eval("C02")%>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="HC03" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litC03" runat="server" Text='<%# Eval("C03")%>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtC03" runat="server" Text='<%# Eval("C03")%>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="HC04" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litC04" runat="server" Text='<%# Eval("C04")%>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtC04" runat="server" Text='<%# Eval("C04")%>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="HC05" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litC05" runat="server" Text='<%# Eval("C05")%>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtC05" runat="server" Text='<%# Eval("C05")%>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="HC06" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litC06" runat="server" Text='<%# Eval("C06")%>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtC06" runat="server" Text='<%# Eval("C06")%>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <!-- Action -->
                                                    <asp:TemplateField HeaderText="Hide">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnHide" runat="server" ToolTip="Hide" CommandName="Hide" OnClientClick="return confirm('ต้องการซ่อนแถว ?');"
                                                                CommandArgument='<%# Eval("ID")%>'><i class="fa fa-minus"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="btnUndo" runat="server" ToolTip="Undo" CommandName="Normal" OnClientClick="return confirm('ยกเลิกการซ่อนแถว ?');"
                                                                CommandArgument='<%# Eval("ID")%>'><i class="fa fa-refresh"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnEdit" runat="server" ToolTip="Edit" CausesValidation="false" CommandName="Edit" CommandArgument='<%# Eval("ID")%>'><i class="fa fa-edit"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:LinkButton ID="btnUpdate" runat="server" ToolTip="Update" CausesValidation="false"
                                                                CommandName="Update"><i class="fa fa-save"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="LinkCancel" runat="server" ToolTip="Cancel" CausesValidation="false"
                                                                CommandName="Cancel"><i class="fa fa-remove"></i></asp:LinkButton>
                                                        </EditItemTemplate>
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


                                <div class="row">
                                    <div class="col-md-8">
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



                                        <asp:GridView ID="gvCoverPages" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-striped table-bordered mini" ShowHeaderWhenEmpty="True" DataKeyNames="ID,row_type" OnRowDataBound="gvCoverPages_RowDataBound" OnRowCommand="gvCoverPages_RowCommand">
                                            <Columns>
                                                <asp:BoundField HeaderText="DHS Chemical ID" DataField="chemical_id" ItemStyle-HorizontalAlign="Center" />

                                                <asp:TemplateField HeaderText="Compond" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbAnalytes" runat="server" Text='<%# Eval("name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtAnalytes" runat="server" Text='<%# Eval("name")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Maximum Allowable Amount" DataField="ng_part" ItemStyle-HorizontalAlign="Center" />

                                                <asp:TemplateField HeaderText="Results" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litResult" runat="server" Text='<%# Eval("result")%>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtResult" runat="server" Text='<%# Eval("result")%>'></asp:TextBox>
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
                                        <%-- <div class="row">
                                            <div class="col-md-6">--%>
                                        <div class="form-group">
                                            <label class="control-label col-md-3">Component:<span class="required">*</span></label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlComponent" runat="server" CssClass="select2_category form-control" DataTextField="A" DataValueField="ID" OnSelectedIndexChanged="ddlComponent_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--     </div>
                                        </div>--%>
                                        <br />
                                    </asp:Panel>
                                    <asp:Panel ID="pStatus" runat="server">
                                        <%--     <div class="row">
                                            <div class="col-md-6">--%>
                                        <div class="form-group">
                                            <label class="control-label col-md-3">Approve Status:<span class="required">*</span></label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="select2_category form-control" DataTextField="name" DataValueField="ID" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--     </div>
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
                                        <%--     </div>
                                        </div>--%>
                                        <br />
                                    </asp:Panel>
                                    <asp:Panel ID="pDisapprove" runat="server">
                                        <%-- <div class="row">
                                            <div class="col-md-6">--%>
                                        <div class="form-group">
                                            <label class="control-label col-md-3">Assign To:<span class="required">*</span></label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlAssignTo" runat="server" class="select2_category form-control" DataTextField="name" DataValueField="ID" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--    </div>
                                        </div>--%>
                                        <br />
                                    </asp:Panel>
                                    <asp:Panel ID="pDownload" runat="server">
                                        <%--      <div class="row">
                                            <div class="col-md-6">--%>
                                        <div class="form-group">
                                            <label class="control-label col-md-3">Download:</label>
                                            <div class="col-md-6">
                                                <asp:Literal ID="litDownloadIcon" runat="server"></asp:Literal>
                                                <asp:LinkButton ID="lbDownload" runat="server" OnClick="lbDownload_Click">
                                                    <asp:Label ID="lbDownloadName" runat="server" Text="Download"></asp:Label>
                                                </asp:LinkButton><asp:CheckBox ID="cbNoHeader" runat="server" Visible="false" />
                                            </div>
                                        </div>
                                        <%--     </div>
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
                                        <tr>
                                            <td>Unit</td>
                                            <td>
                                                <asp:DropDownList ID="ddlUnit" runat="server" class="select2_category form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged" DataValueField="ID" DataTextField="Name">
                                                </asp:DropDownList></td>
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
