<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TemplateBlankPA.ascx.cs" Inherits="ALS.ALSI.Web.view.template.TemplateBlankPA" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<form runat="server" id="Form1" method="POST" enctype="multipart/form-data" class="form-horizontal">
    <asp:ToolkitScriptManager ID="ToolkitScript1" runat="server" />

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <contenttemplate>
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
                    <asp:Panel ID="pDSH" runat="server">
                    </asp:Panel>
                    <asp:Panel ID="pCoverpage" runat="server">

                       
                        <asp:Panel ID="pUploadWorkSheet" runat="server">
                            <div class="row" id="Div1" runat="server">
                                <div class="col-md-12">

                                    <div class="form-group">
                                        <label class="control-label col-md-3">Uplod file:</label>

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
                                                        <asp:FileUpload ID="FileUpload2" runat="server" />

                                                    </span>
                                                    <a href="javascript:;" class="input-group-addon btn red fileinput-exists" data-dismiss="fileinput">Remove </a>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                                <asp:Panel ID="pFileDownload" runat="server">
                        <div class="row" id="invDiv" runat="server">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="control-label col-md-3">Download WorkSheet:</label>
                                    <div class="col-md-3">

                                        <h5>
                                            <asp:LinkButton ID="lbDowloadWorkSheet" runat="server" OnClick="lbDowloadWorkSheet_Click"></asp:LinkButton></h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                            </asp:Panel>
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
                                                    <label class="control-label col-md-3">Download WorkSheet:</label>
                                                    <div class="col-md-3">

                                                        <h5>
                                                            <asp:LinkButton ID="lbDownload" runat="server" OnClick="lbDownload_Click"></asp:LinkButton></h5>
                                                        <asp:Label ID="lbDesc" runat="server" Text=""></asp:Label>
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

        </contenttemplate>
        <triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="lbDownload" />
            <asp:PostBackTrigger ControlID="lbDowloadWorkSheet" />
        </triggers>
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
