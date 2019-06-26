<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SumIncome.aspx.cs" Inherits="ALS.ALSI.Web.view.request.SumIncome" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <form id="form1" runat="server">

        <asp:HiddenField ID="hPrefix" Value="ELP" runat="server" />

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
                                <label class="control-label col-md-3">Fiscal Year:</label>
                                <div class="col-md-6">
                                    <div class="form-group" style="text-align: left">
                                        <asp:DropDownList ID="ddlPhysicalYear" runat="server" class="select2_category form-control" DataTextField="year" DataValueField="year"></asp:DropDownList>
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
                                <label class="control-label col-md-3">BOI/NON-BOI:</label>
                                <div class="col-md-6">
                                    <div class="form-group" style="text-align: left">
                                        <asp:DropDownList ID="ddlBoiNonBoi" runat="server" class="select2_category form-control"></asp:DropDownList>
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
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">PO:</label>
                                <div class="col-md-6">
                                    <div class="form-group" style="text-align: left">
                                        <asp:TextBox ID="txtSamplePo" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Invoice date (From):</label>
                                <div class="col-md-6">
                                    <div class="input-group input-medium date date-picker" data-date="10/2012" data-date-format="dd/mm/yyyy" data-date-viewmode="years" data-date-minviewmode="months">
                                        <asp:TextBox ID="txtInvoiceDateFrom" runat="server" class="form-control"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <button class="btn default" type="button"><i class="fa fa-calendar"></i></button>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-3">Invoice date (To):</label>
                                <div class="col-md-6">
                                    <div class="input-group input-medium date date-picker" data-date="10/2012" data-date-format="dd/mm/yyyy" data-date-viewmode="years" data-date-minviewmode="months">
                                        <asp:TextBox ID="txtInvoiceDateTo" runat="server" class="form-control"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <button class="btn default" type="button"><i class="fa fa-calendar"></i></button>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <br />

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

                <div class="portlet box blue-dark">

                    <div class="portlet-title">
                        <div class="caption">
                            <i class=" icon-layers font-green"></i>
                            <span class="captione">Search Result</span>

                        </div>
                        <div class="actions">

                            <asp:LinkButton ID="btnExportExcel" runat="server" class="btn btn-block btn-default" OnClick="btnExportExcel_Click">   
                                <i class=" icon-screen-desktop"></i></asp:LinkButton>

                        </div>
                    </div>
                    <div class="portlet-body">
                        <asp:Label ID="lbTotalRecords" runat="server" Text="" Visible="false"></asp:Label>

                        <asp:GridView ID="gvJob" runat="server" AutoGenerateColumns="False"
                            CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="" OnRowCommand="gvJob_RowCommand" OnPageIndexChanging="gvJob_PageIndexChanging" OnRowDataBound="gvJob_RowDataBound" AllowPaging="True" PageSize="50">
                            <Columns>

                                <asp:BoundField HeaderText="Invoice Date" DataField="InvoiceDate" ItemStyle-HorizontalAlign="Center" SortExpression="InvoiceDate" DataFormatString="{0:d MMM yyyy}" ItemStyle-Width="150">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField HeaderText="Termsr" DataField="Terms" ItemStyle-HorizontalAlign="Center" SortExpression="Terms">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField HeaderText="Number" DataField="Number" ItemStyle-HorizontalAlign="Center" SortExpression="Number">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField HeaderText="Purchase Order" DataField="PurchaseOrder" ItemStyle-HorizontalAlign="Center" SortExpression="PurchaseOrder">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Customer" DataField="Customer" ItemStyle-HorizontalAlign="Center" SortExpression="Customer">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Total Sub Amount" DataField="TotalSubAmount" ItemStyle-HorizontalAlign="Center" SortExpression="TotalSubAmount">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Vat. 7%" DataField="Vat7" ItemStyle-HorizontalAlign="Center" SortExpression="Vat7">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Grand Total Amount Vat" DataField="GrandTotalAmountVat" ItemStyle-HorizontalAlign="Center" SortExpression="GrandTotalAmountVat">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Withholding Tax(3%)" DataField="WithholdingTax3" ItemStyle-HorizontalAlign="Center" SortExpression="WithholdingTax3">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Total Amount" DataField="TotaAmount" ItemStyle-HorizontalAlign="Center" SortExpression="TotaAmount">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Dept. HDD" DataField="DeptBOI" ItemStyle-HorizontalAlign="Center" SortExpression="DeptBOI">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField HeaderText="Customer Type HDD" DataField="CustomerTypeHDD" ItemStyle-HorizontalAlign="Center" SortExpression="CustomerTypeHDD">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>


                            </Columns>
                            <PagerStyle HorizontalAlign="Right" CssClass="pagination-ys" />

                            <EmptyDataTemplate>
                                <div class="data-not-found">
                                    <asp:Literal ID="libDataNotFound" runat="server" Text="Data Not found" />
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>


                        <br />

                    </div>
                </div>
            </div>
        </div>

    </form>


</asp:Content>
