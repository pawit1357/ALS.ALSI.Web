<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ImportSo.aspx.cs" Inherits="ALS.ALSI.Web.view.template.ImportSo" %>

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
                            <span class="captione">อัพโหลดไฟล์ SO</span>

                        </div>
                        <div class="actions">
                        </div>
                    </div>
                    <div class="portlet-body">
                        <asp:Label ID="lbTotalRecords" runat="server" Text="" Visible="false"></asp:Label>
                        <div style="width: 100%; overflow-x: auto; white-space: nowrap;">
                            <%=Message%>

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
                                            <asp:Button ID="btnUpload" runat="server" class="btn small blue" Text="โหลดข้อมูล" OnClick="btnUpload_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pSo" runat="server">
                                <asp:GridView ID="gvJob" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                    CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="SO" OnPageIndexChanging="gvJob_PageIndexChanging" OnRowEditing="gvJob_RowEditing" OnRowUpdating="gvJob_RowUpdating" OnRowCancelingEdit="gvJob_RowCancelingEdit" PageSize="20" Width="100%">
                                    <Columns>
                                        <asp:TemplateField>  
                                            <ItemTemplate>  
                                                <asp:Button ID="btn_Edit" runat="server" Text="แก้ไข" CommandName="Edit" />  
                                            </ItemTemplate>  
                                            <EditItemTemplate>  
                                                <asp:Button ID="btn_Update" runat="server" Text="อัพเดท" CommandName="Update"/>  
                                                <asp:Button ID="btn_Cancel" runat="server" Text="ยกเลิก" CommandName="Cancel"/>  
                                            </EditItemTemplate>  
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="#" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                        
<%--                                        <asp:BoundField HeaderText="SO" DataField="SO" ItemStyle-HorizontalAlign="Center" SortExpression="SO" />--%>
                                        <%--<asp:BoundField HeaderText="PO" DataField="PO" ItemStyle-HorizontalAlign="Left" SortExpression="PO" />--%>
                                        <%--<asp:BoundField HeaderText="Date" DataField="Date" ItemStyle-HorizontalAlign="Left" SortExpression="Date" />--%>
                                        <%--<asp:BoundField HeaderText="Qty" DataField="Qty" ItemStyle-HorizontalAlign="Left" SortExpression="Qty" />--%>
                                        <%--<asp:BoundField HeaderText="UnitPrice" DataField="UnitPrice" ItemStyle-HorizontalAlign="Left" SortExpression="UnitPrice" />--%>
                                        <%--<asp:BoundField HeaderText="ReportNo" DataField="ReportNo" ItemStyle-HorizontalAlign="Left" SortExpression="ReportNo" />--%>
                                        <asp:TemplateField HeaderText="SO" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">  
                                            <ItemTemplate>  
                                                <asp:Label ID="ltSO" runat="server" Text='<%#Eval("SO") %>'></asp:Label>  
                                            </ItemTemplate>  
                                        </asp:TemplateField>  
                                        <asp:TemplateField HeaderText="Unit/Price" HeaderStyle-HorizontalAlign="Left">  
                                            <ItemTemplate>  
                                                <asp:Label ID="ltUnitPrice" runat="server" Text='<%#Eval("UnitPrice") %>'></asp:Label>  
                                            </ItemTemplate>  
                                            <EditItemTemplate>  
                                                <asp:TextBox ID="txtUnitPrice" runat="server" Text='<%#Eval("UnitPrice") %>' CssClass="form-control" TextMode="MultiLine" Rows="10"></asp:TextBox>  
                                            </EditItemTemplate>  
                                        </asp:TemplateField>  
                                        <asp:TemplateField HeaderText="ReportNo" HeaderStyle-HorizontalAlign="Left">  
                                            <ItemTemplate>  
                                                <asp:Label ID="ltReportNo" runat="server" Text='<%#Eval("ReportNo") %>'></asp:Label>  
                                            </ItemTemplate>  
                                            <EditItemTemplate>  
                                                <asp:TextBox ID="txtReportNo" runat="server" Text='<%#Eval("ReportNo") %>' CssClass="form-control" TextMode="MultiLine" Rows="10"></asp:TextBox>  
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


                            </asp:Panel>
                        </div>

                        <div class="form-actions">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-offset-3 col-md-9">
                                            <asp:Button ID="btnSaveSo" runat="server" class="btn green" Text="บันทึกรายการ" OnClick="btnSaveSo_Click" />
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

