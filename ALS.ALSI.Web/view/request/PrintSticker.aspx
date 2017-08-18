<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PrintSticker.aspx.cs" Inherits="ALS.ALSI.Web.view.request.PrintSticker" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="<%= ResolveUrl("~/assets/global/plugins/jquery.min.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //App.init(); // init the rest of plugins and elements
        });

        function printDiv(divID) {
            //Get the HTML of div
            var divElements = document.getElementById(divID).innerHTML;
            //Get the HTML of whole page
            //var oldPage = document.body.innerHTML;

            //Reset the page's HTML with div's HTML only
            document.body.innerHTML =
              "<html><head><title></title><style>"+
         "body {" +
            "    width: 4in;" +
            "}" +
        "</style></head><body>" +
              divElements + "</body></html>";

            //Print Page
            window.print();
            location.reload();
            //document.location.href = "PrintSticker.aspx";
            //Restore orignal HTML
            //document.body.innerHTML = oldPage;
            //window.location.assign("CashFlowCalendar.aspx")
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <form id="Form1" method="post" runat="server" class="form-horizontal">

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
                            CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="sn" OnSelectedIndexChanged="gvJob_SelectedIndexChanged">
                            <Columns>
                                <%--<asp:BoundField HeaderText="Date Received." DataField="create_date" ItemStyle-HorizontalAlign="Left" SortExpression="create_date" DataFormatString="{0:dd-MM-yyyy}" />--%>
                                <asp:BoundField HeaderText="Ref No." DataField="job_number" ItemStyle-HorizontalAlign="Left" SortExpression="job_number">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundField>
                                <%--<asp:BoundField HeaderText="Comp" DataField="customer" ItemStyle-HorizontalAlign="Left" SortExpression="customer" />--%>
                                <%--<asp:BoundField HeaderText="Contact" DataField="contract_person" ItemStyle-HorizontalAlign="Left" SortExpression="contract_person" />--%>
                                <%--<asp:BoundField HeaderText="S/N" DataField="sn" ItemStyle-HorizontalAlign="Left" SortExpression="sn" />--%>
                                <asp:BoundField HeaderText="Description" DataField="description" ItemStyle-HorizontalAlign="Left" SortExpression="description">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Model" DataField="model" ItemStyle-HorizontalAlign="Left" SortExpression="model">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Surface Area" DataField="surface_area" ItemStyle-HorizontalAlign="Left" SortExpression="surface_area">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundField>
                                <%--<asp:BoundField HeaderText="Remarks" DataField="remarks" ItemStyle-HorizontalAlign="Left" SortExpression="remarks" />--%>
                                <asp:BoundField HeaderText="Specification" DataField="specification" ItemStyle-HorizontalAlign="Left" SortExpression="specification">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Type of test" DataField="type_of_test" ItemStyle-HorizontalAlign="Left" SortExpression="type_of_test">


                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundField>
                                <asp:CommandField ShowSelectButton="True" />

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
                    <span class="caption-subject font-red-sunglo bold uppercase">Print Label Preview</span>
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
                            <div id="divStrickerPreview">

                                <div id="divStricker" >
                                    <table>
                                         <tr>
                                             <td>
                                                 <img src="/alis/images/als_logo.jpg" alt="" style="width:30px;height:30px;"></image>
                                                 </td>
                                             <td class="text-left">ALS Testing Services (Thailand)</td>
                                             </tr>
                                        <tr>
                                            <td class="text-right"> Job No:&nbsp;&nbsp;&nbsp; </td>
                                            <td class="text-left">
                                                <asp:Label ID="lbJobNo" runat="server" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="text-right">Client:&nbsp;&nbsp;&nbsp;</td>
                                            <td class="text-left">
                                                <asp:Label ID="lbClient" runat="server" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="text-right">Contract:&nbsp;&nbsp;&nbsp;</td>
                                            <td class="text-left">
                                                <asp:Label ID="lbContract" runat="server" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="text-right">Sample:&nbsp;&nbsp;&nbsp;</td>
                                            <td class="text-left">
                                                <asp:Label ID="lbSample" runat="server" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="text-right">Spec:&nbsp;&nbsp;&nbsp;</td>
                                            <td class="text-left">
                                                <asp:Label ID="lbSpec" runat="server" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="text-right">Test:&nbsp;&nbsp;&nbsp;</td>
                                            <td class="text-left">
                                              <asp:Label ID="lbTot" runat="server" Font-Bold="True"></asp:Label>  
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="text-right">Sample:&nbsp;&nbsp;&nbsp;</td>
                                            <td class="text-left">
                                                <asp:Label ID="lbSd" runat="server" Font-Bold="True"></asp:Label>
                                            
                                            </td>
                                        </tr>
                                                                                <tr>
                                            <td class="text-right">Surface Area:&nbsp;&nbsp;&nbsp;</td>
                                            <td class="text-left">
                                                <asp:Label ID="lbSa" runat="server" Font-Bold="True"></asp:Label>
                                            
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>

                        </div>
                    </div>


                    <page size="A4">


                    </page>

                    <div class="form-actions">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:LinkButton ID="lbPrint" runat="server" CssClass="cancel btn blue" OnClick="lbPrint_Click" Visible="False"><i class="icon-print"></i>Print</asp:LinkButton>
                             <button onclick="javascript:printDiv('divStricker'); return false;" class="cancel btn blue">
                                            <i class="icon-print"></i>Print</button>
<%--                                        <asp:Button ID="btnCancel" runat="server" class="cancel btn" Text="Cancel" OnClick="btnCancel_Click" />--%>
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
</asp:Content>
