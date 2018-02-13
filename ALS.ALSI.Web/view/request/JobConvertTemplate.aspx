<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="JobConvertTemplate.aspx.cs" Inherits="ALS.ALSI.Web.view.request.JobConvertTemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <form runat="server" id="Form1" method="POST" enctype="multipart/form-data" class="form-horizontal">
        <div class="row">
            <div class="col-md-12">
                <!-- BEGIN EXAMPLE TABLE PORTLET-->
                <div class="portlet box blue-dark">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="fa fa-cogs"></i>Sample Detail
                        </div>
                        <div class="actions">
                        </div>
                    </div>
                    <div class="portlet-body">
                        <!-- BEGIN FORM-->

                        <asp:GridView ID="gvJob" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="ID">
                            <Columns>
                                <%--<asp:BoundField HeaderText="Date Received." DataField="create_date" ItemStyle-HorizontalAlign="Left" SortExpression="create_date" DataFormatString="{0:dd-MM-yyyy}" />--%>
                                <asp:BoundField HeaderText="Ref No." DataField="job_number" ItemStyle-HorizontalAlign="Left" SortExpression="job_number" />
                                <%--<asp:BoundField HeaderText="Comp" DataField="customer" ItemStyle-HorizontalAlign="Left" SortExpression="customer" />--%>
                                <%--<asp:BoundField HeaderText="Contact" DataField="contract_person" ItemStyle-HorizontalAlign="Left" SortExpression="contract_person" />--%>
                                <%--<asp:BoundField HeaderText="S/N" DataField="sn" ItemStyle-HorizontalAlign="Left" SortExpression="sn" />--%>
                                <asp:BoundField HeaderText="Description" DataField="description" ItemStyle-HorizontalAlign="Left" SortExpression="description" />
                                <asp:BoundField HeaderText="Model" DataField="model" ItemStyle-HorizontalAlign="Left" SortExpression="model" />
                                <asp:BoundField HeaderText="Surface Area" DataField="surface_area" ItemStyle-HorizontalAlign="Left" SortExpression="surface_area" />
                                <%--<asp:BoundField HeaderText="Remarks" DataField="remarks" ItemStyle-HorizontalAlign="Left" SortExpression="remarks" />--%>
                                <asp:BoundField HeaderText="Specification" DataField="specification" ItemStyle-HorizontalAlign="Left" SortExpression="specification" />
                                <asp:BoundField HeaderText="Type of test" DataField="type_of_test" ItemStyle-HorizontalAlign="Left" SortExpression="type_of_test" />
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
                        <!-- END FORM-->
                    </div>
                </div>
            </div>
        </div>

        <div class="portlet light bordered">
            <div class="portlet-title">
                <div class="caption">
                    <i class="icon-equalizer font-red-sunglo"></i>
                    <span class="caption-subject font-red-sunglo bold uppercase">Select template</span>
                    <span class="caption-helper"></span>
                </div>
                <div class="tools">
                    <%--<a href="#" class="collapse"></a>--%>
                </div>
            </div>
            <div class="portlet-body form">
                <div class="form-body">
                    <!-- BEGIN FORM-->
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Template:<span class="required">*</span></label>
                                <div class="col-md-9">
                                    <asp:DropDownList ID="ddlTemplate" runat="server" class="select2_category form-control" DataTextField="name" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlTemplate_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                    </div>

                    <asp:Panel ID="pUploadfile" runat="server">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label col-md-3">Uplod Spec: </label>

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
                            </div>
                        </div>
                        <br />
                    </asp:Panel>
                    <!-- 
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">Demo Notes</h3>
                        </div>
                        <div class="panel-body">
                            <ul>
                                <li>##ขั้นตอนการอัพเดท Specification,Detail Spec,Component</li>
                                <li>เลือกไฟล์ที่มี </li>
                                <li>1.1 กรณียังไม่มี template ให้สร้างไฟล์ *.ascx ก่อน</li>
                                <li>1.2 กรณีมี template *.ascx แล้ว สามารเลือกไฟล์ *.ascx อ้างอิงแล้วอัพเดท specification,detail spec หรือ component ใหม่ได้เลย</li>
                                <li>2. โหลด Detail Spec/Component/Specification จาก Excel</li>
                                <li>3. The maximum file size for uploads in this demo is <strong>5 MB</strong> (default file size is unlimited).</li>
                                <li>4. Only Excel files (<strong>*.xlt</strong>) are allowed in this demo (by default there is no file type restriction).</li>
                                <li>** spec ref มาจาก column ที่มีค่า spefRef ค่าที่ใส่จะต้องเป็น ลำดับ column-1 เช่น specRef อยู่ในcolumn 5 ค่าที่ต้องใส่ก็จะเป็น 4</li>
                            </ul>
                        </div>
                    </div>
                    -->
                    <div class="form-actions">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnSave" runat="server" class="btn green" Text="Save" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnCancel" runat="server" class="cancel btn" Text="Cancel" OnClick="btnCancel_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                            </div>
                        </div>
                    </div>
                    <!-- END FORM-->
                </div>
            </div>
        </div>
    </form>

    <!-- BEGIN PAGE LEVEL SCRIPTS -->
    <script src="<%= ResolveUrl("~/assets/global/plugins/jquery.min.js") %>" type="text/javascript"></script>
    <!-- END PAGE LEVEL SCRIPTS -->
    <script>
        jQuery(document).ready(function () {

        });
    </script>
    <!-- END JAVASCRIPTS -->
</asp:Content>
