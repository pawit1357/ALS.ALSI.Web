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
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label col-md-3">เลือกไฟล์ SO(*.txt)<span class="required"></span></label>
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
                                        <label class="control-label col-md-3">So Code.:</label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtSoCode" runat="server" CssClass="form-control"></asp:TextBox>
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
                                            <asp:Button ID="Button2" runat="server" class="btn green" Text="ค้นหา" OnClick="Button2_Click" />&nbsp;&nbsp;

                                        </div>
                                        <div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- BEGIN FORM-->
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Notes</h3>
                                </div>
                                <div class="panel-body">
                                    <ul>
                                        <li>##วิธีการแก้ไข</li>
                                        <li>Code,Quantity,Unit/Price ใช้วิธี Enter อีก 1 บันทัดในการแบ่งกลุ่ม</li>
                                        <li>ReportNo ใช้ | ในการแบ่งกลุ่มไม่ต้อง  Enter แบ่งกลุ่ม</li>

                                    </ul>
                                </div>
                            </div>

                            <asp:Panel ID="pSo" runat="server">
                                <asp:GridView ID="gvJob" runat="server" AutoGenerateColumns="False" AllowPaging="True" AlternatingRowStyle-HorizontalAlign="Center"
                                    CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="so,id" OnPageIndexChanging="gvJob_PageIndexChanging" OnRowEditing="gvJob_RowEditing" OnRowUpdating="gvJob_RowUpdating" OnRowCancelingEdit="gvJob_RowCancelingEdit" OnRowCommand="gvJob_RowCommand" OnRowDataBound="gvJob_RowDataBound" OnRowDeleting="gvJob_RowDeleting" PageSize="20" Width="80%">
                                    <AlternatingRowStyle HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAllSign" runat="server" AutoPostBack="true" Text="Check All" OnCheckedChanged="chkAllSign_CheckedChanged"/>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbSelect" runat="server" />
                                                <asp:HiddenField ID="hid" runat="server" Value='<%# Eval("id")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ลบ" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDel" runat="server" CommandArgument='<%# String.Concat(Eval("id")) %>' CommandName="Delete" ToolTip="Delete"><i class="fa fa-trash-o" onclick='return confirm("ต้องการลบ So รายการนี้ใช่หรือไม่ ?");'></i></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="โหลดข้อมูล" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnLoad" runat="server" CommandArgument='<%# String.Concat(Eval("id")) %>' CommandName="View" ToolTip="Load"><i class="fa fa-cog"></i></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="แก้ไข">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btn_Edit" runat="server" CommandArgument='<%# String.Concat(Eval("id")) %>' CommandName="Edit" ToolTip="แก้ไข"><i class="fa fa-edit"></i></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="btn_Update" runat="server" CommandArgument='<%# String.Concat(Eval("id")) %>' CommandName="Update" ToolTip="อัพเดท"><i class="fa fa-save"></i></asp:LinkButton>
                                                &nbsp;&nbsp;
                                                <asp:LinkButton ID="btn_Cancel" runat="server" CommandArgument='<%# String.Concat(Eval("id")) %>' CommandName="Cancel" ToolTip="ยกเลิก"><i class="fa fa-undo"></i></asp:LinkButton>
                                            </EditItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="SO No." ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="ltSO" runat="server" Text='<%#Eval("so") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="ReportNo">
                                            <ItemTemplate>
                                                <asp:Label ID="ltReportNo" runat="server" Text='<%#Eval("report_no") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtReportNo" runat="server" CssClass="form-control" Rows="10" Text='<%#Eval("report_no") %>' TextMode="MultiLine"></asp:TextBox>
                                            </EditItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="ltSoDesc" runat="server" Text='<%#Eval("so_desc") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtSoDesc" runat="server" CssClass="form-control" Rows="10" Text='<%#Eval("so_desc") %>' TextMode="MultiLine"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="ltQuantity" runat="server" Text='<%#Eval("quantity") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" Rows="10" Text='<%#Eval("quantity") %>' TextMode="MultiLine"></asp:TextBox>
                                            </EditItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Unit/Price">
                                            <ItemTemplate>
                                                <asp:Label ID="ltUnitPrice" runat="server" Text='<%#Eval("unit_price") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtUnitPrice" runat="server" CssClass="form-control" Rows="10" Text='<%#Eval("unit_price") %>' TextMode="MultiLine"></asp:TextBox>
                                            </EditItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="ltStatus" runat="server" Text='<%#Eval("status") %>'></asp:Literal>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
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
                        <%=MsgLogs%>
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

