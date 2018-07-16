<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SampleInfo.ascx.cs" Inherits="ALS.ALSI.Web.UserControls.SampleInfo" %>
<div class="note note-info">
    <table border="0">
        <tr>
            <td style="text-align: right"><span style="font-weight: bold;">Specification/Type Of Test</span></td>
            <td>&nbsp&nbsp;
                <asp:Label ID="txtSpec" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td style="text-align: right"><span style="font-weight: bold">CUSTOMER PO NO.:</span></td>
            <td>&nbsp&nbsp;
                <asp:Label ID="lbPoNo" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td style="text-align: right"><span style="font-weight: bold">ALS THAILAND REF NO.:</span></td>
            <td>&nbsp&nbsp;
                <asp:Label ID="lbRefNo" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td style="text-align: right"><span style="font-weight: bold">Supplement to report no.:</span></td>
            <td>&nbsp&nbsp;
                <asp:Label ID="lbSupplementToReportNo" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td style="text-align: right"><span style="font-weight: bold">DATE:</span></td>
            <td>&nbsp&nbsp;
                <asp:Label ID="lbDate" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td style="text-align: right"><span style="font-weight: bold">COMPANY:</span></td>
            <td>&nbsp&nbsp;
                <asp:Label ID="lbCompany" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td style="text-align: right"><span style="font-weight: bold">DATE SAMPLE RECEIVED:</span></td>
            <td>&nbsp&nbsp;
                <asp:Label ID="lbDateSampleReceived" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td style="text-align: right"><span style="font-weight: bold">DATE TEST COMPLETED:</span></td>
            <td>&nbsp&nbsp;
                <asp:Label ID="lbDateTestCompleted" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td style="text-align: right"><span style="font-weight: bold">SAMPLE DESCRIPTION:</span> </td>
            <td>&nbsp&nbsp;One lot of sample was received with references:</td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Label ID="lbSampleDescription" runat="server" Text=""></asp:Label></td>
        </tr>

    </table>
</div>


