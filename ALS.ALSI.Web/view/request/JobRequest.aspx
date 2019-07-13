<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="JobRequest.aspx.cs" Inherits="ALS.ALSI.Web.view.request.JobRequest" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <form id="Form1" method="post" runat="server" class="form-horizontal">
        <%--                                                <asp:Button ID="btnAM" runat="server" class="btn red" Text="Amend" ValidationGroup="GJobInfo" OnClick="btnAM_Click" />--%>
        <asp:HiddenField ID="hJobID" Value="0" runat="server" />

        <div class="portlet light bordered">
            <div class="portlet-title">
                <div class="caption">
                    <i class="icon-equalizer font-red-sunglo"></i>
                    <span class="caption-subject font-red-sunglo bold uppercase">ANALYSIS REQUISITION FORM</span>
                    <span class="caption-helper"></span>
                </div>
                <div class="tools">
                    <%--<button type="button" id="btnCancel" class="btn default">Save changes</button>--%>
                </div>
            </div>
            <div class="portlet-body form">
                <div class="alert alert-danger display-hide">
                    <button class="close" data-close="alert"></button>
                    You have some form errors. Please check below.
                </div>
                <div class="alert alert-success display-hide">
                    <button class="close" data-close="alert"></button>
                    Your form validation is successful!
                </div>
                <div class="form-body">
                    <!-- BEGIN FORM-->
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">
                                    Date Of Request:<span class="required">
										* </span>
                                </label>
                                <div class="col-md-6">
                                    <div class="input-group input-medium date date-picker" data-date="10/2012" data-date-format="dd/mm/yyyy" data-date-viewmode="years" data-date-minviewmode="months">
                                        <asp:TextBox ID="txtDateOfRequest" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <button class="btn default" type="button"><i class="fa fa-calendar"></i></button>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%--                        <div class="form-group">
                            <label class="control-label col-md-3">Disable Past Dates</label>
                            <div class="col-md-3">
                                <div class="input-group input-medium date date-picker" data-date-format="dd-mm-yyyy" data-date-start-date="+0d">
                                    <input type="text" class="form-control" readonly>
                                    <span class="input-group-btn">
                                        <button class="btn default" type="button">
                                            <i class="fa fa-calendar"></i>
                                        </button>
                                    </span>
                                </div>
                                <!-- /input-group -->
                                <span class="help-block">Select date </span>
                            </div>
                        </div>
                        --%>
                    </div>
                    <h4 class="form-section">Customer Information</h4>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">
                                    Contract Person:<span class="required">
										* </span>
                                </label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlContract_person_id" runat="server" class="select2_category form-control" DataTextField="name" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlContract_person_id_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">
                                    Company Name:<span class="required">
										* </span>
                                </label>
                                <div class="col-md-6">
                                    <div class="input-group" style="text-align: left">
                                        <asp:TextBox ID="txtCompanyName" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <a class="btn green" href="#PopupSearchCompany" data-toggle="modal">Select</a>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Department:</label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtDepartment" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Tel. Number:</label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtTelNumber" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Email:</label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtEmail" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Fax. Number:</label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtFax" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Address:</label>
                                <div class="col-md-6">
                                    &nbsp;<asp:DropDownList ID="ddlAddress" runat="server" class="select2_category form-control" DataTextField="address" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlAddress_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Customer Ref No:</label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtCustomer_ref_no" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Company Name to State in Report:</label>
                                <div class="col-md-6">
                                    <textarea id="txtCompany_name_to_state_in_report" class="form-control" runat="server"></textarea>
                                </div>
                            </div>
                        </div>
                    </div>
                    <h4 class="form-section">Sample Infomation:</h4>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">
                                    Job Type:<span class="required">
										* </span>
                                </label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="ddlJobNumber" runat="server" class="select2_category form-control" DataTextField="prefix" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlJobNumber_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">
                                    Job Number:
                                </label>
                                <div class="col-md-6" id="divJobNumber">
                                    <asp:TextBox ID="txtJob_number" name="txtJob_number" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">
                                    Date of Receive:<span class="required">
										* </span>
                                </label>
                                <div class="col-md-6">
                                    <div class="input-group input-medium date date-picker" data-date="10/2012" data-date-format="dd/mm/yyyy" data-date-viewmode="years" data-date-minviewmode="months">
                                        <asp:TextBox ID="txtDate_of_receive" runat="server" class="form-control"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <button class="btn default" type="button"><i class="fa fa-calendar"></i></button>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Spec. Ref. & Rev. No. :</label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtSpecRefRevNo" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <h4 class="form-section">Sample Requisition:</h4>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">
                                    Sample Disposition:<span class="required">
										* </span>
                                </label>
                                <div class="radio-list">
                                    <label class="radio-inline">
                                        <asp:RadioButton ID="rdSample_dipositionYes" GroupName="Sample_diposition" runat="server" Checked="true" />Discard
                                    </label>
                                    <label class="radio-inline">
                                        <asp:RadioButton ID="rdSample_dipositionNo" GroupName="Sample_diposition" runat="server" />Return all
                                    </label>

                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">
                                    Specification:<span class="required">
										* </span>
                                </label>
                                <div class="col-md-6" id="divSpecification">
                                    <asp:DropDownList ID="ddlSecification_id" runat="server" class="select2_category form-control" DataTextField="name" DataValueField="ID" OnSelectedIndexChanged="ddlSecification_id_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">
                                    Type Of Test:<span class="required">
										* </span>
                                </label>
                                <div class="col-md-6" id="divTypeOfTest">
                                    <asp:ListBox ID="lstTypeOfTest" runat="server" DataTextField="customName" DataValueField="ID" SelectionMode="Multiple" class="bs-select form-control"></asp:ListBox>
                                    <div class="col-md-9">
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">
                                    Sample Description:<span class="required">
										* </span>
                                </label>
                                <div class="col-md-6" id="divDescription">
                                    <asp:TextBox ID="txtDescriptoin" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">
                                    Model:<span class="required">
										* </span>
                                </label>
                                <div class="col-md-6" id="divModel">
                                    <asp:TextBox ID="txtModel" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">
                                    Surface Area:<span class="required">
										* </span>
                                </label>
                                <div class="col-md-6" id="divSurfaceArea">
                                    <asp:TextBox ID="txtSurfaceArea" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Remark:</label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtRemark" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">
                                    Part no:<span class="required">&nbsp; </span>
                                </label>
                                <div class="col-md-6" id="divPartNo">
                                    <asp:TextBox ID="txtPartNo" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Part name:</label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtPartName" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">
                                    Lot no:<span class="required">&nbsp; </span>
                                </label>
                                <div class="col-md-6" id="divLotNo">
                                    <asp:TextBox ID="txtLotNo" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Other Ref No:</label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtOtherRefNo" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Uncertainty:</label>
                                <div class="radio-list">
                                    <label class="radio-inline">
                                        <asp:RadioButton ID="rdUncertaintyYes" GroupName="rdUncertainty" runat="server" />Y
                                    </label>
                                    <label class="radio-inline">
                                        <asp:RadioButton ID="rdUncertaintyNo" GroupName="rdUncertainty" runat="server" Checked="true" />N
                                    </label>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">
                                    กำหนดเสร็จ:<span class="required">
										* </span>
                                </label>
                                <div class="col-md-6" id="divDdlCompletionScheduled">
                                    <asp:DropDownList ID="ddlCompletionScheduled" runat="server" class="select2_category form-control" DataTextField="name" DataValueField="ID"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <!-- BEGIN EXAMPLE TABLE PORTLET-->
                            <div class="portlet box blue-dark">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-cogs"></i>Sample List
                                    </div>
                                    <div class="actions">
                                        <asp:LinkButton ID="btnAddSampleInfo" class="btn btn-default btn-sm" runat="server" OnClick="btnAddSampleInfo_Click"> Add Sample <i class="icon-plus"></i></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="portlet-body">
                                    <!-- BEGIN FORM-->
                                    <div style="width: 100%; overflow-x: scroll; overflow-y: hidden; padding-bottom: 10px;" runat="server">

                                        <asp:GridView ID="gvSample" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                            CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="ID,job_id,job_number,no_of_report,uncertainty,status_completion_scheduled,amend_count,retest_count,amend_or_retest" OnRowCancelingEdit="gvSample_RowCancelingEdit" OnRowDataBound="gvSample_RowDataBound" OnRowDeleting="gvSample_RowDeleting" OnRowEditing="gvSample_RowEditing" OnRowUpdating="gvSample_RowUpdating" OnSelectedIndexChanging="gvSample_SelectedIndexChanging" OnPageIndexChanging="gvSample_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Ref No." ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litRefNo" runat="server" Text='<%# Eval("job_number")%>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtRefNo" CssClass="form-control" runat="server" Text='<%# Eval("job_number")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Other Ref No." ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litOtherRefNo" runat="server" Text='<%# Eval("other_ref_no")%>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtOtherRefNo" CssClass="form-control" runat="server" Text='<%# Eval("other_ref_no")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Sample Description (Part description, Part no. etc.)" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litDescriptoin" runat="server" Text='<%# Eval("description")%>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtDescriptoin" CssClass="form-control" runat="server" Text='<%# Eval("description")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="model" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litModel" runat="server" Text='<%# Eval("model")%>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtModel" CssClass="form-control" runat="server" Text='<%# Eval("model")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Surface Area" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litSurfaceArea" runat="server" Text='<%# Eval("surface_area")%>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtSurfaceArea" CssClass="form-control" runat="server" Text='<%# Eval("surface_area")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remark" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litRemark" runat="server" Text='<%# Eval("remarks")%>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtRemark" CssClass="form-control" runat="server" Text='<%# Eval("remarks")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Part No" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litPartNo" runat="server" Text='<%# Eval("part_no")%>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtPartNo" CssClass="form-control" runat="server" Text='<%# Eval("part_no")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="txtPart Name" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litPartName" runat="server" Text='<%# Eval("part_name")%>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtPartName" CssClass="form-control" runat="server" Text='<%# Eval("part_name")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Lot No" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litLotNo" runat="server" Text='<%# Eval("lot_no")%>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtLotNo" CssClass="form-control" runat="server" Text='<%# Eval("lot_no")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="No.of Report:" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litNoOfReport" runat="server" Text='<%# Eval("no_of_report")%>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlNoOfReport" runat="server" CssClass="select2_category form-control"></asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UNCERTAINTY" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litUncertainty" runat="server" Text='<%# Eval("uncertainty")%>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlUncertaint" runat="server" CssClass="select2_category form-control"></asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="STATUS" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="litStatus_completion_scheduled" runat="server" Text='<%# Eval("status_completion_scheduled")%>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlCompletionScheduled" runat="server" CssClass="select2_category form-control" DataTextField="name" DataValueField="ID"></asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit" runat="server" ToolTip="Edit" CommandName="Edit" CommandArgument='<%# Eval("ID")%>'><i class="fa fa-edit"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="btnDelete" runat="server" ToolTip="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                                            CommandArgument='<%# Eval("ID")%>'><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="btnUpdate" runat="server" ToolTip="Update" ValidationGroup="CreditLineGrid"
                                                            CommandName="Update"><i class="fa fa-save"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkCancel" runat="server" ToolTip="Cancel" CausesValidation="false"
                                                            CommandName="Cancel"><i class="fa fa-remove"></i></asp:LinkButton>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />

                                            <EmptyDataTemplate>
                                                <div class="data-not-found">
                                                    <asp:Literal ID="libDataNotFound" runat="server" Text="Data Not found" />
                                                </div>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                    <!-- END FORM-->
                                </div>
                            </div>
                        </div>
                    </div>
                    <h4 class="form-section">Internal Use Only</h4>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">ตัวอย่าง:</label>
                                <div class="radio-list">
                                    <label class="radio-inline">
                                        <asp:RadioButton ID="rdSample_enoughYes" GroupName="Sample_enough" runat="server" Checked="true" />เพียงพอ
                                    </label>
                                    <label class="radio-inline">
                                        <asp:RadioButton ID="rdSample_enoughNo" GroupName="Sample_enough" runat="server" />ไม่เพียงพอ
                                    </label>
                                    <label class="radio-inline">
                                        <asp:RadioButton ID="rdSample_fullYes" GroupName="Sample_full" runat="server" Checked="true" />สมบูรณ์
                                    </label>
                                    <label class="radio-inline">
                                        <asp:RadioButton ID="rdSample_fullNo" GroupName="Sample_full" runat="server" />ไม่สมบูรณ์
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">บุคลากรและปริมาณงาน:</label>
                                <div class="radio-list">
                                    <label class="radio-inline">
                                        <asp:RadioButton ID="rdPersonel_and_workloadYes" GroupName="Personel_and_workload" runat="server" Checked="true" />พร้อม
                                    </label>
                                    <label class="radio-inline">
                                        <asp:RadioButton ID="rdPersonel_and_workloadNo" GroupName="Personel_and_workload" runat="server" />ไม่พร้อม
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">เครื่องมือทดสอบ:</label>
                                <div class="radio-list">
                                    <label class="radio-inline">
                                        <asp:RadioButton ID="rdTest_toolYes" GroupName="Test_tool" runat="server" Checked="true" />พร้อม
                                    </label>
                                    <label class="radio-inline">
                                        <asp:RadioButton ID="rdTest_toolNo" GroupName="Test_tool" runat="server" />ไม่พร้อม
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">วิธีทดสอบ:</label>
                                <div class="radio-list">
                                    <label class="radio-inline">
                                        <asp:RadioButton ID="rdTest_methodYes" GroupName="Test_method" runat="server" Checked="true" />พร้อม
                                    </label>
                                    <label class="radio-inline">
                                        <asp:RadioButton ID="rdTest_methodNo" GroupName="Test_method" runat="server" />ไม่พร้อม
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <h4 class="form-section">Job Status</h4>

                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Status:<span class="required">*</span></label>
                                <div class="col-md-9">
                                    <asp:DropDownList ID="ddlStatus" runat="server" class="select2_category form-control" DataTextField="name" DataValueField="ID"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                    </div>


                    <!-- END FORM-->
                    <div class="form-actions">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnSave" runat="server" class="btn green" Text="Save" OnClick="btnSave_Click" />
                                        <%--        </ContentTemplate>
                    <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />

        </Triggers>
        </asp:UpdatePanel>--%>
                                        <%--                                                <asp:Button ID="btnAM" runat="server" class="btn red" Text="Amend" ValidationGroup="GJobInfo" OnClick="btnAM_Click" />--%>
                                        <asp:LinkButton ID="btnCancel" runat="server" class="btn default cancel" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="PopupSearchCompany" tabindex="-1" role="basic" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                        <h4 class="modal-title">Select Company</h4>
                    </div>
                    <div class="modal-body">
                        <asp:DropDownList ID="ddlCustomer_id" runat="server" class="select2_category form-control" DataTextField="company_name" DataValueField="ID" OnSelectedIndexChanged="ddlCustomer_id_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        <asp:HiddenField ID="hCompanyId" runat="server" />
                    </div>
                    <div class="modal-footer">
                        <button type="button" id="btnSelectCompany" class="btn blue" data-dismiss="modal">OK</button>
                        <%--<button type="button" id="btnCancel" class="btn default">Save changes</button>--%>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>

        <%--        </ContentTemplate>
                    <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />

        </Triggers>
        </asp:UpdatePanel>--%>
    </form>

    <!-- BEGIN PAGE LEVEL SCRIPTS -->
    <script src="/alis/assets/global/plugins/jquery.min.js" type="text/javascript"></script>
    <!-- END PAGE LEVEL SCRIPTS -->
    <script>
        jQuery(document).ready(function () {

            //var form1 = $('#Form1');
            //var error1 = $('.alert-danger', form1);
            //var success1 = $('.alert-success', form1);

            //form1.validate({
            //    errorElement: 'span', //default input error message container
            //    errorClass: 'help-block help-block-error', // default input error message class
            //    focusInvalid: false, // do not focus the last invalid input
            //    ignore: "",  // validate all fields including form hidden input
            //    messages: {
            //        select_multi: {
            //            maxlength: jQuery.validator.format("Max {0} items allowed for selection"),
            //            minlength: jQuery.validator.format("At least {0} items must be selected")
            //        }
            //    },
            //    rules: {

            //        ctl00$ContentPlaceHolder2$txtDateOfRequest: {
            //            required: true,
            //        },
            //        //ctl00$ContentPlaceHolder2$txtCompanyName: {
            //        //    minlength: 2,
            //        //    required: true
            //        //},
            //        ctl00$ContentPlaceHolder2$ddlContract_person_id: {
            //            required: true,
            //        },
            //        ctl00$ContentPlaceHolder2$ddlJobNumber: {
            //            required: true,
            //        },
            //        ctl00$ContentPlaceHolder2$txtDate_of_receive: {
            //            required: true,
            //        },
            //        ctl00$ContentPlaceHolder2$btnAddSampleInfo: {
            //            required: true,
            //        }


            //    },

            //    invalidHandler: function (event, validator) { //display error alert on form submit              
            //        success1.hide();
            //        error1.show();
            //        Metronic.scrollTo(error1, -200);
            //    },

            //    highlight: function (element) { // hightlight error inputs
            //        $(element)
            //            .closest('.form-group').addClass('has-error'); // set error class to the control group
            //    },

            //    unhighlight: function (element) { // revert the change done by hightlight
            //        $(element)
            //            .closest('.form-group').removeClass('has-error'); // set error class to the control group
            //    },

            //    success: function (label) {
            //        label
            //            .closest('.form-group').removeClass('has-error'); // set success class to the control group
            //    },

            //    submitHandler: function (form) {

            //        var rowCount = $('#ContentPlaceHolder2_gvSample tr').length;

            //        if (rowCount >= 2) {
            //            error1.hide();
            //            form.submit();
            //        } else {
            //            //alert("ยังไม่ได้เริ่มรายการใน Sample List.")
            //            Metronic.scrollTo(error1, -400);
            //            error1.show();
            //        }

            //    }
            //});


            ////
            //$("#ContentPlaceHolder2_btnAddSampleInfo").click(function () {

            //    var _txtJob_number = $("#ContentPlaceHolder2_txtJob_number");
            //    var _ddlSecification_id = $('#ContentPlaceHolder2_ddlSecification_id :selected').val();
            //    var _lstTypeOfTest = $("#ContentPlaceHolder2_lstTypeOfTest");
            //    var _txtDescriptoin = $("#ContentPlaceHolder2_txtDescriptoin");

            //    var _txtModel = $("#ContentPlaceHolder2_txtModel");
            //    var _txtSurfaceArea = $("#ContentPlaceHolder2_txtSurfaceArea");


            //    if ($.trim(_txtJob_number.val()) == "") {
            //        _txtJob_number.closest('.form-group').addClass('has-error');
            //        $("#divJobNumber").append("<span id=\"_txtJob_number-error\" class=\"help-block help-block-error\">This field is required.</span>");
            //        return false;
            //    } else {
            //        _txtJob_number.closest('.form-group').removeClass('has-error');
            //    }

            //    if (_ddlSecification_id == "") {
            //        $("#ContentPlaceHolder2_ddlSecification_id").closest('.form-group').addClass('has-error');
            //        $("#divSpecification").append("<span id=\"_ddlSecification_id-error\" class=\"help-block help-block-error\">This field is required.</span>");
            //        return false;
            //    } else {
            //        $("#ContentPlaceHolder2_ddlSecification_id").closest('.form-group').removeClass('has-error');
            //    }

            //    if ($.trim(_lstTypeOfTest.val()) == "") {
            //        _lstTypeOfTest.closest('.form-group').addClass('has-error');
            //        $("#divTypeOfTest").append("<span id=\"_lstTypeOfTest-error\" class=\"help-block help-block-error\">This field is required.</span>");
            //        return false;
            //    } else {
            //        _lstTypeOfTest.closest('.form-group').removeClass('has-error');
            //    }

            //    if ($.trim(_txtDescriptoin.val()) == "") {
            //        _txtDescriptoin.closest('.form-group').addClass('has-error');
            //        $("#divDescription").append("<span id=\"_txtDescriptoin-error\" class=\"help-block help-block-error\">This field is required.</span>");
            //        return false;
            //    } else {
            //        _txtDescriptoin.closest('.form-group').removeClass('has-error');
            //    }

            //    if ($.trim(_txtModel.val()) == "") {
            //        _txtModel.closest('.form-group').addClass('has-error');
            //        $("#divModel").append("<span id=\"_txtModel-error\" class=\"help-block help-block-error\">This field is required.</span>");
            //        return false;
            //    } else {
            //        _txtModel.closest('.form-group').removeClass('has-error');
            //    }

            //    if ($.trim(_txtSurfaceArea.val()) == "") {
            //        _txtSurfaceArea.closest('.form-group').addClass('has-error');
            //        $("#divSurfaceArea").append("<span id=\"_txtSurfaceArea-error\" class=\"help-block help-block-error\">This field is required.</span>");
            //        return false;
            //    } else {
            //        _txtSurfaceArea.closest('.form-group').removeClass('has-error');
            //    }
            //});


        });
    </script>
    <!-- END JAVASCRIPTS -->
</asp:Content>
