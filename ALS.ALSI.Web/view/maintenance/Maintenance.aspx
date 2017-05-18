<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Maintenance.aspx.cs" Inherits="ALS.ALSI.Web.view.initial.Maintenance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <form id="form1" runat="server">
        <br />
        <asp:TextBox ID="txtSql" runat="server" TextMode="MultiLine" Width="500px"></asp:TextBox>
        <br />
        <asp:Button ID="btnExecute" runat="server" Text="Execute" OnClick="btnExecute_Click" />
        <asp:Button ID="btnGetDs" runat="server" Text="Execute(DS)" OnClick="btnGetDs_Click" />
        <asp:Button ID="btnBackup" runat="server" Text="Backup(DB)" OnClick="btnBackup_Click" />

        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">DownloadBackup</asp:LinkButton>
        <asp:HiddenField ID="HiddenField1" runat="server" />

        <br />
        <asp:Label ID="lbResult" runat="server" Text=""></asp:Label>
        <br />
        <div style="width: 100%; overflow-x: scroll; overflow-y: hidden; padding-bottom: 10px;" runat="server">

            <asp:GridView ID="gvResult" runat="server" CssClass="table table-striped table-hover table-bordered" AllowPaging="True" PageSize="100"></asp:GridView>
        </div>
    </form>
</asp:Content>
