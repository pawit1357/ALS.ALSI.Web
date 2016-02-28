<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerPopup.ascx.cs" Inherits="ALS.ALSI.Web.UserControls.CustomerPopup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:TextBox ID="txtFileName" runat="server" Enabled="false" CssClass="input-xlarge" />
<asp:ImageButton ID="ImgSearch" runat="server" ImageUrl="~/Images/search.png" CausesValidation="false" />
<asp:ImageButton ID="ImgCancel" runat="server" ImageUrl="~/Images/cancel.png" CausesValidation="false" />
<asp:ModalPopupExtender ID="ModalPopupForwardContract" runat="server"
    PopupControlID="Modal" TargetControlID="ImgSearch" BackgroundCssClass="modal-backdrop fade in" />

<div class="modal large fade in" runat="server" id="Modal" style="display: none;">
    <div class="modal-header">
        <h3>
            <asp:Literal ID="litHdTitle" runat="server" Text="Select file for rerun" />
        </h3>
    </div>
    <div class="modal-body">
        <div class="well">
            <ul class="nav nav-tabs">
                <li class="active"><a href="#divSearch" data-toggle="tab">Search Condition</a></li>
            </ul>
            <div class="tab-pane active in" id="divSearch">
                <table class="table-search">
                    <tr>
                        <td>
                            <label>FileName :</label>
                            <asp:TextBox ID="txtSourceFileName" runat="server" CssClass="input-xlarge"
                                TabIndex="1" />
                        </td>
                    </tr>
                </table>

                <div class="submit">
                    <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn" TabIndex="2"
                        CausesValidation="false"><i class="icon-search"></i> Search</asp:LinkButton>
                    <asp:LinkButton ID="btnClear" runat="server" CssClass="btn" Text="Clear" TabIndex="3"
                        CausesValidation="false" />
                </div>
            </div>
        </div>

        <div class="well">
            <asp:Label ID="lbTotalRecords" runat="server" Text=""></asp:Label>
            <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CssClass="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="True" DataKeyNames="id" OnRowCommand="gvResult_RowCommand" OnPageIndexChanging="gvResult_PageIndexChanging">
                <Columns>
                    <asp:BoundField HeaderText="Code" DataField="customer_code" ItemStyle-HorizontalAlign="Left" SortExpression="customer_code" />
                    <asp:BoundField HeaderText="Name" DataField="company_name" ItemStyle-HorizontalAlign="Left" SortExpression="company_name" />
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnSelect" runat="server" ToolTip="Select" CommandName="Select" CommandArgument='<%# Eval("id")%>'><i class="icon-check"></i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
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
        </div>
    </div>
    <div class="modal-footer">
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-info" TabIndex="6" />
    </div>
</div>
