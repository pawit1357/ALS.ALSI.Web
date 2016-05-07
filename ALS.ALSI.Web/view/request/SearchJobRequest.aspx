<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SearchJobRequest.aspx.cs" Inherits="ALS.ALSI.Web.view.request.SearchJobRequest" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <form id="form1" runat="server">
        <%--        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
        <asp:HiddenField ID="hPrefix" Value="1" runat="server" />

        <div class="portlet light bordered">
            <div class="portlet-title">
                <div class="caption">
                    <i class="icon-equalizer font-red-sunglo"></i>
                    <span class="caption-subject font-red-sunglo bold uppercase">Search Condition</span>
                    <span class="caption-helper"></span>
                </div>
                <div class="tools">
                </div>
            </div>
            <div class="portlet-body form">
                <div class="form-body">
                    <!-- BEGIN FORM-->

                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Status:</label>
                                <div class="col-md-6">
                                    <div class="form-group" style="text-align: left">
                                        <asp:DropDownList ID="ddlJobStatus" runat="server" class="select2_category form-control" DataTextField="NAME" DataValueField="ID" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">ALS Ref No.:</label>
                                <div class="col-md-6">
                                    <div class="form-group" style="text-align: left">
                                        <asp:TextBox ID="txtREfNo" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Company:</label>
                                <div class="col-md-6">
                                    <div class="form-group" style="text-align: left">
                                        <asp:DropDownList ID="ddlCompany" runat="server" class="select2_category form-control" DataTextField="company_name" DataValueField="ID"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Specification:</label>
                                <div class="col-md-6">
                                    <div class="form-group" style="text-align: left">
                                        <asp:DropDownList ID="ddlSpecification" runat="server" class="select2_category form-control" DataTextField="NAME" DataValueField="ID"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Type Of Test:</label>
                                <div class="col-md-6">
                                    <div class="form-group" style="text-align: left">
                                        <asp:DropDownList ID="ddlTypeOfTest" runat="server" class="select2_category form-control" DataTextField="NAME" DataValueField="ID"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">PO:</label>
                                <div class="col-md-6">
                                    <div class="form-group" style="text-align: left">
                                        <asp:TextBox ID="txtPo" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Invoice:</label>
                                <div class="col-md-6">
                                    <div class="form-group" style="text-align: left">
                                        <asp:TextBox ID="txtInvoice" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Received Report From:</label>
                                <div class="col-md-6">
                                    <div class="input-group input-medium date date-picker" data-date="10/2012" data-date-format="dd/mm/yyyy" data-date-viewmode="years" data-date-minviewmode="months">
                                        <asp:TextBox ID="txtReceivedReportFrom" runat="server" class="form-control"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <button class="btn default" type="button"><i class="fa fa-calendar"></i></button>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Received Report To:</label>
                                <div class="col-md-6">
                                    <div class="input-group input-medium date date-picker" data-date="10/2012" data-date-format="dd/mm/yyyy" data-date-viewmode="years" data-date-minviewmode="months">
                                        <asp:TextBox ID="txtReceivedReportTo" runat="server" class="form-control"></asp:TextBox>
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
                                <label class="control-label col-md-3">Report To Customer From:</label>
                                <div class="col-md-6">
                                    <div class="input-group input-medium date date-picker" data-date="10/2012" data-date-format="dd/mm/yyyy" data-date-viewmode="years" data-date-minviewmode="months">
                                        <asp:TextBox ID="txtReportToCustomerFrom" runat="server" class="form-control"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <button class="btn default" type="button"><i class="fa fa-calendar"></i></button>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Report To Customer To:</label>
                                <div class="col-md-6">
                                    <div class="input-group input-medium date date-picker" data-date="10/2012" data-date-format="dd/mm/yyyy" data-date-viewmode="years" data-date-minviewmode="months">
                                        <asp:TextBox ID="txtReportToCustomerTo" runat="server" class="form-control"></asp:TextBox>
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
                                <label class="control-label col-md-3">Duedate From:</label>
                                <div class="col-md-6">
                                    <div class="input-group input-medium date date-picker" data-date="10/2012" data-date-format="dd/mm/yyyy" data-date-viewmode="years" data-date-minviewmode="months">
                                        <asp:TextBox ID="txtDuedateFrom" runat="server" class="form-control"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <button class="btn default" type="button"><i class="fa fa-calendar"></i></button>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Duedate Customer To:</label>
                                <div class="col-md-6">
                                    <div class="input-group input-medium date date-picker" data-date="10/2012" data-date-format="dd/mm/yyyy" data-date-viewmode="years" data-date-minviewmode="months">
                                        <asp:TextBox ID="txtDuedateTo" runat="server" class="form-control"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <button class="btn default" type="button"><i class="fa fa-calendar"></i></button>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="form-actions">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:LinkButton ID="LinkButton1" runat="server" class="btn green" OnClick="btnSearch_Click"><i class="icon-search"></i> Search</asp:LinkButton>
                                        <asp:LinkButton ID="LinkButton2" runat="server" class="btn default" OnClick="btnCancel_Click"><i class="icon-refresh"></i> Cancel</asp:LinkButton>
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

        <div class="row">
            <div class="col-sm-12">
                <!-- BEGIN EXAMPLE TABLE PORTLET-->
                <div class="portlet light portlet-fit portlet-datatable bordered" id="form_wizard_1">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class=" icon-layers font-green"></i>
                            <span class="caption-subject font-green sbold uppercase">Search Result</span>

                        </div>
                        <div class="actions">
                            - 
                      

                            <asp:LinkButton ID="btnElp" runat="server" class="btn btn-default btn-sm" OnClick="btnElp_Click"> ELP</asp:LinkButton>
                            <asp:LinkButton ID="btnEls" runat="server" class="btn btn-default btn-sm" OnClick="btnElp_Click"> ELS</asp:LinkButton>
                            <asp:LinkButton ID="btnEln" runat="server" class="btn btn-default btn-sm" OnClick="btnElp_Click"> ELN</asp:LinkButton>

                            <asp:LinkButton ID="btnFa" runat="server" class="btn btn-default btn-sm" OnClick="btnElp_Click"> FA</asp:LinkButton>
                            <asp:LinkButton ID="btnElwa" runat="server" class="btn btn-default btn-sm" OnClick="btnElp_Click"> ELWA</asp:LinkButton>
                            <asp:LinkButton ID="btnGrp" runat="server" class="btn btn-default btn-sm" OnClick="btnElp_Click"> GRP</asp:LinkButton>
                            <asp:LinkButton ID="btnTrb" runat="server" class="btn btn-default btn-sm" OnClick="btnElp_Click"> TRB</asp:LinkButton>


                            <asp:LinkButton ID="btnExportExcel" runat="server" class="btn btn-circle btn-icon-only btn-default" OnClick="btnExportExcel_Click">   
                                <i class=" icon-printer"></i></asp:LinkButton>



                            <asp:LinkButton ID="lbAddJob" runat="server" class="btn btn-default btn-sm" OnClick="lbAddJob_Click"><i class="fa fa-plus"></i> Add</asp:LinkButton>

                        </div>
                    </div>
                    <div class="portlet-body">
                        <asp:Label ID="lbTotalRecords" runat="server" Text="" Visible="false"></asp:Label>

                        <asp:GridView ID="gvJob" runat="server" AutoGenerateColumns="False"
                            CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="ID,job_status,job_role,status_completion_scheduled,step1owner,step2owner,step3owner,step4owner,step5owner,step6owner" OnRowCommand="gvJob_RowCommand" OnPageIndexChanging="gvJob_PageIndexChanging" OnRowDataBound="gvJob_RowDataBound" AllowPaging="True" PageSize="50">
                            <Columns>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>

                                        <asp:LinkButton ID="btnInfo" runat="server" ToolTip="Info" CommandName="View" CommandArgument='<%# Eval("ID")%>'><i class="fa fa-search"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnEdit" runat="server" ToolTip="Edit" CommandName="Edit" CommandArgument='<%# Eval("ID")%>'><i class="fa fa-edit"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnConvertTemplete" runat="server" ToolTip="Convert Template" CommandName="ConvertTemplate" CommandArgument='<%# String.Concat(Eval("ID"),ALS.ALSI.Biz.Constant.Constants.CHAR_COMMA,Eval("SN"))%>'><i class="fa fa-tasks"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnWorkFlow" runat="server" ToolTip="Work Flow" CommandName="Workflow" CommandArgument='<%# String.Concat(Eval("ID"),ALS.ALSI.Biz.Constant.Constants.CHAR_COMMA,Eval("SN"))%>'><i class="fa fa-briefcase"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnChangeStatus" runat="server" ToolTip="Change Status" CommandName="ChangeStatus" CommandArgument='<%# String.Concat(Eval("ID"),ALS.ALSI.Biz.Constant.Constants.CHAR_COMMA,Eval("SN"))%>'><i class="fa fa-refresh"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnChangeDueDate" runat="server" ToolTip="Change DueDate" CommandName="ChangeDueDate" CommandArgument='<%# String.Concat(Eval("ID"),ALS.ALSI.Biz.Constant.Constants.CHAR_COMMA,Eval("SN"))%>'><i class="fa fa-calculator"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnChangeReportDate" runat="server" ToolTip="Change Report Date" CommandName="ChangeReportDate" CommandArgument='<%# String.Concat(Eval("ID"),ALS.ALSI.Biz.Constant.Constants.CHAR_COMMA,Eval("SN"))%>'><i class="fa fa-calculator"></i></asp:LinkButton>

                                        <asp:LinkButton ID="btnChangePo" runat="server" ToolTip="Change PO & Invoice" CommandName="ChangePo" CommandArgument='<%# String.Concat(Eval("ID"),ALS.ALSI.Biz.Constant.Constants.CHAR_COMMA,Eval("SN"))%>'><i class="fa fa-credit-card"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnChangeInvoice" runat="server" ToolTip="Chnage Invoice" CommandName="ChangeInvoice" CommandArgument='<%# String.Concat(Eval("ID"),ALS.ALSI.Biz.Constant.Constants.CHAR_COMMA,Eval("SN"))%>'><i class="fa fa-tags"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnPrintLabel" runat="server" ToolTip="Print Label" CommandName="Print" CommandArgument='<%# String.Concat(Eval("ID"),ALS.ALSI.Biz.Constant.Constants.CHAR_COMMA,Eval("SN"))%>'><i class="fa fa-print"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnAmend" runat="server" ToolTip="Change Amend" CommandName="Amend" CommandArgument='<%# String.Concat(Eval("ID"),ALS.ALSI.Biz.Constant.Constants.CHAR_COMMA,Eval("SN"))%>'><i class="fa fa-pencil-square"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnReTest" runat="server" ToolTip="ReTest" CommandName="Retest" CommandArgument='<%# String.Concat(Eval("ID"),ALS.ALSI.Biz.Constant.Constants.CHAR_COMMA,Eval("SN"))%>'><i class="fa fa-retweet"></i></asp:LinkButton>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Literal ID="litStatus" runat="server">&nbsp;</asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Literal ID="ltJobStatus" runat="server" Text="-"></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Received." DataField="date_srchemist_complate" ItemStyle-HorizontalAlign="Center" SortExpression="date_srchemist_complate" DataFormatString="{0:dd-MM-yyyy}">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField HeaderText="Report Sent to Customer" DataField="date_admin_sent_to_cus" ItemStyle-HorizontalAlign="Center" SortExpression="date_admin_sent_to_cus" DataFormatString="{0:dd-MM-yyyy}">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField HeaderText="Receive Date." DataField="receive_date" ItemStyle-HorizontalAlign="Center" SortExpression="receive_date" DataFormatString="{0:dd-MM-yyyy}">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Due Date." DataField="due_date" ItemStyle-HorizontalAlign="Center" SortExpression="due_date" DataFormatString="{0:dd-MM-yyyy}">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <%--<asp:BoundField HeaderText="Ref No." DataField="job_number" ItemStyle-HorizontalAlign="Left" SortExpression="job_number" />--%>
                                <asp:TemplateField HeaderText="ALS Ref No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lbJobNumber" runat="server" Text='<%# Eval("job_number")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Cus Ref No." DataField="customer_ref_no" ItemStyle-HorizontalAlign="Left" SortExpression="customer_ref_no">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="S'Ref No." DataField="s_pore_ref_no" ItemStyle-HorizontalAlign="Left" SortExpression="s_pore_ref_no">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <%--<asp:BoundField HeaderText="Other" DataField="customer_po_ref" ItemStyle-HorizontalAlign="Left" SortExpression="customer_po_ref" />--%>

                                <%--                                        <asp:BoundField HeaderText="Company" DataField="customer" ItemStyle-HorizontalAlign="Left" SortExpression="customer" />--%>
                                <asp:TemplateField HeaderText="Company">
                                    <%--                    <HeaderTemplate>
                                                <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" DataTextField="company_name" DataValueField="ID" class="select2_category form-control" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </HeaderTemplate>--%>
                                    <ItemTemplate>
                                        <asp:Literal ID="ltCompany" runat="server" Text='<%# Eval("customer")%>'></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField HeaderText="Invoice" DataField="sample_invoice" ItemStyle-HorizontalAlign="Left" SortExpression="sample_invoice">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>

                                <asp:BoundField HeaderText="Po" DataField="sample_po" ItemStyle-HorizontalAlign="Left" SortExpression="sample_po">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>



                                <asp:BoundField HeaderText="Contact" DataField="contract_person" ItemStyle-HorizontalAlign="Left" SortExpression="contract_person">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <%--<asp:BoundField HeaderText="S/N" DataField="sn" ItemStyle-HorizontalAlign="Left" SortExpression="sn" />--%>
                                <asp:BoundField HeaderText="Description" DataField="description" ItemStyle-HorizontalAlign="Left" SortExpression="description">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Model" DataField="model" ItemStyle-HorizontalAlign="Left" SortExpression="model">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Surface Area" DataField="surface_area" ItemStyle-HorizontalAlign="Left" SortExpression="surface_area">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <%--<asp:BoundField HeaderText="Remarks" DataField="remarks" ItemStyle-HorizontalAlign="Left" SortExpression="remarks" />--%>
                                <asp:BoundField HeaderText="Specification" DataField="specification" ItemStyle-HorizontalAlign="Left" SortExpression="specification">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Type of test" DataField="type_of_test" ItemStyle-HorizontalAlign="Left" SortExpression="type_of_test">



                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>



                            </Columns>
                            <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />

                            <EmptyDataTemplate>
                                <div class="data-not-found">
                                    <asp:Literal ID="libDataNotFound" runat="server" Text="Data Not found" />
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>

                        <!-- STATUS BAR -->

                        <br />
                        <div class="note note-info">

                            <p>JOB STATUS:</p>
                            <div class="btn-group btn-group-xs btn-group-solid"><i class="fa fa-desktop">: Convert Template</i></div>
                            <div class="btn-group btn-group-xs btn-group-solid"><i class="fa fa-book"></i>: Select Spec</div>
                            <div class="btn-group btn-group-xs btn-group-solid"><i class="fa fa-flask"></i>: Chemist Testing</div>
                            <div class="btn-group btn-group-xs btn-group-solid"><i class="fa fa-check-square-o">: Sr.Chemist Checking</i></div>
                            <div class="btn-group btn-group-xs btn-group-solid"><i class="fa fa-user-md">: Lab Manager Checking</i></div>
                            <div class="btn-group btn-group-xs btn-group-solid"><i class="fa fa-file-word-o">: Convert to Word</i></div>
                            <div class="btn-group btn-group-xs btn-group-solid"><i class="fa fa-file-pdf-o">: Convert to Pdf</i></div>
                            <div class="btn-group btn-group-xs btn-group-solid"><i class="fa fa-truck">:Complete </i></div>
                            <div class="btn-group btn-group-xs btn-group-solid"><i class="fa fa-lock">: Hold</i></div>
                            <div class="btn-group btn-group-xs btn-group-solid"><i class="fa fa-trash-o">:Cancel </i></div>
                            <p>STATUS:</p>
                            <button type="button" class="btn red btn-sm">Cancel</button>
                            <button type="button" class="btn green btn-sm">Complete</button>
                            <button type="button" class="btn purple btn-sm">Hold</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>



        <%--            </ContentTemplate>
        </asp:UpdatePanel>--%>
    </form>


</asp:Content>
