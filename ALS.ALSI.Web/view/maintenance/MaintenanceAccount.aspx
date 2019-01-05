﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="MaintenanceAccount.aspx.cs" Inherits="ALS.ALSI.Web.view.template.MaintenanceAccount" %>

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
        <asp:HiddenField ID="hToken" Value="" runat="server" />


        <div class="portlet light bordered">
            <div class="portlet-title">
                <div class="caption">
                    <i class="icon-equalizer font-red-sunglo"></i>
                    <span class="caption-subject font-red-sunglo bold uppercase">อัพโหลดไฟล์ SO</span>
                    <span class="caption-helper"></span>
                </div>
                <div class="tools">
                    <a href="#" class="collapse"></a>
                </div>
            </div>
            <div class="portlet-body form">
                <div class="form-body">
<%=Message2 %>

                    <!-- BEGIN FORM-->
                    <div class="row fileupload-buttonbar">
                        <div class="col-lg-8">
                            <div class="form-group">
                                <label class="control-label col-md-3">เลือกไฟล์ SO<span class="required">*</span></label>
                                <div class="col-md-6">
                                    <span class="btn green fileinput-button">
                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                    </span>
                                </div>
                                <div>
                                    <asp:Button ID="btnUpload" runat="server" class="btn small blue" Text="อัพโหลดไฟล์" OnClick="btnUpload_Click" />
                                </div>
                            </div>
                        </div>

                    </div>
                    <asp:Panel ID="pSo" runat="server">
                        <div class="row">
                            <div class="col-md-8">
                                <div class="form-group">
                                    <label class="control-label col-md-3">ยืนยันข้อมูล SO:</label>
                                    <div class="col-md-8">
                                        <div class="form-group" style="text-align: left">
                                            <br />
                                            <asp:GridView ID="gvJob" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                                CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="SO" OnPageIndexChanging="gvJob_PageIndexChanging" PageSize="20" Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="SO" DataField="SO" ItemStyle-HorizontalAlign="Left" SortExpression="SO" />
                                                    <asp:BoundField HeaderText="PO" DataField="PO" ItemStyle-HorizontalAlign="Left" SortExpression="PO" />
                                                    <%--                                <asp:BoundField HeaderText="Date" DataField="Date" ItemStyle-HorizontalAlign="Left" SortExpression="Date" />--%>
                                                    <asp:BoundField HeaderText="Qty" DataField="Qty" ItemStyle-HorizontalAlign="Left" SortExpression="Qty" />
                                                    <asp:BoundField HeaderText="UnitPrice" DataField="UnitPrice" ItemStyle-HorizontalAlign="Left" SortExpression="UnitPrice" />
                                                    <asp:BoundField HeaderText="ReportNo" DataField="ReportNo" ItemStyle-HorizontalAlign="Left" SortExpression="ReportNo" />

                                                </Columns>
                                                <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />

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
                        </div>
                        <div class="form-actions">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-offset-3 col-md-9">
                                            <asp:Button ID="btnSaveSo" runat="server" class="btn green" Text="Save" OnClick="btnSaveSo_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                </div>
                            </div>
                        </div>
                    </asp:Panel>





                    <!-- END FORM-->
                </div>
            </div>
        </div>

        <div class="portlet light bordered">
            <div class="portlet-title">
                <div class="caption">
                    <i class="icon-equalizer font-red-sunglo"></i>
                    <span class="caption-subject font-red-sunglo bold uppercase">ส่งข้อมูลไปยัง POWER BI (
                        <asp:Label ID="hDataSetId" Value="" runat="server" />)</span>
                    <span class="caption-helper"></span>
                </div>
                <div class="tools">
                    <a href="#" class="collapse"></a>
                </div>
            </div>
            <div class="portlet-body form">
                <div class="form-body">
                    <!-- BEGIN FORM-->
                                         <%=Message %>

                    <div class="row">
                        <div class="col-md-8">
                            <div class="form-group">
                                <label class="control-label col-md-3">เลือกประเภทการส่งข้อมูล:</label>
                                <div class="col-md-8">
                                    <div class="form-group" style="text-align: left">
                                        <br />
                                        <asp:RadioButtonList ID="rdDsBiPostType" runat="server">
                                            <asp:ListItem Value="POST" Selected="True">&nbsp;&nbsp;เพิ่มข้อมูล</asp:ListItem>
                                            <asp:ListItem Value="DELETE">&nbsp;&nbsp;ลบข้อมูล</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-8">
                            <div class="form-group">
                                <label class="control-label col-md-3">เลือก DataSet ที่จะส่งไปยัง Power-BI:</label>
                                <div class="col-md-8">
                                    <div class="form-group" style="text-align: left">
                                        <br />
                                        <asp:RadioButtonList ID="rdDsBi" runat="server">
                                            <asp:ListItem Value="vw_revenue_actual" Selected="True">&nbsp;&nbsp;Revenue-Actual</asp:ListItem>
                                            <asp:ListItem Value="vw_forcast_budget">&nbsp;&nbsp;Forecast-Budget</asp:ListItem>
                                            <asp:ListItem Value="vw_dinv">&nbsp;&nbsp;Daily invoice vs work in process</asp:ListItem>
                                            <asp:ListItem Value="vw_outstanding_balance">&nbsp;&nbsp;Turn around time (TAT) </asp:ListItem>
                                            <asp:ListItem Value="vw_summary_inv_status">&nbsp;&nbsp;Total Invoice</asp:ListItem>
                                        </asp:RadioButtonList>
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
                                        <asp:Button ID="btnSave" runat="server" class="btn green" Text="Send to Power-BI" OnClick="btnSave_Click" />
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

            var form1 = $('#Form1');
            var error1 = $('.alert-danger', form1);
            var success1 = $('.alert-success', form1);
        });
    </script>
    <!-- END JAVASCRIPTS -->
</asp:Content>

