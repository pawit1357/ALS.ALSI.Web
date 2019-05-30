<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ImportInv.aspx.cs" Inherits="ALS.ALSI.Web.view.template.ImportInv" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <form id="Form1" method="post" runat="server" class="form-horizontal">
        <div class="alert alert-danger display-hide">
            <button class="close" data-close="alert"></button>
            You have some form errors. Please check below.
        </div>
        <div class="alert alert-success display-hide">
            <button class="close" data-close="alert"></button>
            Your form validation is successful!
        </div>
        <div class="row">
            <div class="col-sm-12">
                <!-- BEGIN EXAMPLE TABLE PORTLET-->
                <div class="portlet box blue-dark">

                    <div class="portlet-title">
                        <div class="caption">
                            <i class=" icon-layers font-green"></i>
                            <span class="captione">อัพโหลดไฟล์ Invoice</span>

                        </div>
                        <div class="actions">
                        </div>
                    </div>
                    <div class="portlet-body">
                        <asp:Label ID="lbTotalRecords" runat="server" Text="" Visible="false"></asp:Label>
                        <div style="width: 100%; overflow-x: auto; white-space: nowrap;">
                            <%=MessageINv%>

                            <!-- BEGIN FORM-->
<div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label col-md-3">เลือกไฟล์ Invoice(*.txt)<span class="required"></span></label>
                                        <div class="col-md-6">
                                            <span class="btn green fileinput-button">
                                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                            </span>&nbsp;&nbsp;
                                            <asp:Button ID="btnUpload" runat="server" class="btn small blue" Text="โหลดไฟล์" OnClick="btnUpload_Click" />&nbsp;&nbsp;
                                            <asp:Button ID="Button1" runat="server" class="btn small" Text="โหลดข้อมูล(กลุ่ม)" OnClick="btnBatchLoad_Click" />

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label col-md-3">Status.:</label>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="ddlStatus" runat="server" class="select2_category form-control" Width="200px">
                                                <asp:ListItem Value=""><--- ทั้งหมด ---></asp:ListItem>
                                                <asp:ListItem Value="I" Selected="True">In Complete</asp:ListItem>
                                                <asp:ListItem Value="C">Complete</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label col-md-3"></label>
                                        <div class="col-md-6">
                                            <asp:Button ID="btnSearch" runat="server" class="btn green" Text="ค้นหา" OnClick="btnSearch_Click" />&nbsp;&nbsp;

                                        </div>
                                        <div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pSo" runat="server">
                                <asp:GridView ID="gvJob" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                    CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="so,id" OnPageIndexChanging="gvJob_PageIndexChanging" OnRowEditing="gvJob_RowEditing" OnRowUpdating="gvJob_RowUpdating" OnRowCancelingEdit="gvJob_RowCancelingEdit" OnRowCommand="gvJob_RowCommand" OnRowDataBound="gvJob_RowDataBound" PageSize="20" Width="80%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbSelect" runat="server" />
                                                <asp:HiddenField ID="hid" runat="server" Value='<%# Eval("id")%>'></asp:HiddenField>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ReportNo" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="ltReportNo" runat="server" Text='<%#Eval("report_no") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtReportNo" runat="server" Text='<%#Eval("report_no") %>' CssClass="form-control" TextMode="MultiLine" Rows="10"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="so" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="ltSO" runat="server" Text='<%#Eval("so") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Invoice No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="ltInvNo" runat="server" Text='<%#Eval("inv_no") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtInvNo" runat="server" Text='<%#Eval("inv_no") %>' CssClass="form-control" TextMode="MultiLine" Rows="10"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Invoice Amount" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="ltInvAmt" runat="server" Text='<%#Eval("inv_amt") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtInvAmt" runat="server" Text='<%#Eval("inv_amt") %>' CssClass="form-control" TextMode="MultiLine" Rows="10"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Invoice Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="ltInvDate" runat="server" Text='<%#Eval("inv_date") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="ltInvStatus" runat="server" Text='<%#Eval("inv_status") %>'></asp:Literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />
                                    <EmptyDataTemplate>
                                        <div class="data-not-found">
                                            <asp:Literal ID="libDataNotFound" runat="server" Text="Data Not found" />
                                        </div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </asp:Panel>
                        </div>

                    </div>
                </div>
            </div>
        </div>

    </form>
    <!-- BEGIN PAGE LEVEL SCRIPTS -->
    <script src="<%= ResolveUrl("~/assets/global/plugins/jquery.min.js") %>" type="text/javascript"></script>
    <!-- END PAGE LEVEL SCRIPTS -->
    <script>
        jQuery(document).ready(function () {

            var form1 = $('#Form1');
            var error1 = $('.alert-danger', form1);
            var success1 = $('.alert-success', form1);
        });
    </script>
    <!-- END JAVASCRIPTS -->
</asp:Content>

