<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SampleInfo.ascx.cs" Inherits="ALS.ALSI.Web.UserControls.SampleInfo" %>
<div>
    <h3><asp:Label ID="txtSpec" runat="server" Text=""></asp:Label><br /></h3>
    CUSTOMER PO NO.:
                        <asp:Label ID="lbPoNo" runat="server" Text=""></asp:Label><br />
    ALS THAILAND REF NO.:
                        <asp:Label ID="lbRefNo" runat="server" Text=""></asp:Label><br />
    DATE:
                        <asp:Label ID="lbDate" runat="server" Text=""></asp:Label><br />
    COMPANY:
                        <asp:Label ID="lbCompany" runat="server" Text=""></asp:Label><br />
    <br />
    DATE SAMPLE RECEIVED:
                        <asp:Label ID="lbDateSampleReceived" runat="server" Text=""></asp:Label><br />
    DATE TEST COMPLETED:
                        <asp:Label ID="lbDateTestCompleted" runat="server" Text=""></asp:Label><br />
    <br />
    SAMPLE DESCRIPTION: One lot of sample was received with references:<br />
    <asp:Label ID="lbSampleDescription" runat="server" Text=""></asp:Label>
</div>
