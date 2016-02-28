<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Library.aspx.cs" Inherits="ALS.ALSI.Web.view.library.Library" %>

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
        <div class="portlet light bordered">
            <div class="portlet-title">
                <div class="caption">
                    <i class="icon-equalizer font-red-sunglo"></i>
                    <span class="caption-subject font-red-sunglo bold uppercase">[<asp:Label ID="lbCommandName" runat="server" Text=""></asp:Label>]&nbsp;Library</span>
                    <span class="caption-helper"></span>
                </div>
                <div class="tools">
                    <%--<a href="#" class="collapse"></a>--%>
                </div>
            </div>
            <div class="portlet-body form">
                <div class="form-body">
                    <div class="row fileupload-buttonbar">
                        <div class="col-lg-7">
                            <div class="form-group">
                                <label class="control-label col-md-3">Path Source File::<span class="required">*</span></label>
                                <div class="col-md-6">
                                    <asp:HiddenField ID="hPathSourceFile" runat="server" />
                                    <%--<asp:TextBox ID="txtPathSourceFile" runat="server" class="form-control"></asp:TextBox>--%>
                                    <span class="btn green fileinput-button">
                                        <i class="fa fa-plus"></i>
                                        <span>Add files...</span>
                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                    </span>
                                    <%--<h6>***เลือกไฟล์ที่มี tab component,detail spec,specification</h6>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-actions">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">


                                        <asp:Button ID="btnSave" runat="server" class="btn green" Text="Save" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnCancel" runat="server" class="cancel btn" Text="Cancel" OnClick="btnCancel_Click" />
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

        <div class="portlet-body form">
            <div class="form-body">
                <!-- BEGIN FORM-->
    </form>
</asp:Content>

