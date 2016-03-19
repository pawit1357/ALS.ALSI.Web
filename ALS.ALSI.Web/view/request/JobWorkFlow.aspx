<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="JobWorkFlow.aspx.cs" Inherits="ALS.ALSI.Web.view.request.JobWorkFlow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="row-fluid">
        <div class="span12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <h4>
                        <i class="icon-reorder"></i>Work Flow - <span class="step-title">Step
                            <asp:Label ID="lbWorkFlowStep" runat="server" Text="1"></asp:Label>
                            of 6</span>
                    </h4>
                    <div class="tools hidden-phone">
                        <%--<a href="javascript:;" class="collapse"></a>--%>
                        <%--<a href="#portlet-config" data-toggle="modal" class="config"></a>--%>
                        <%--<a href="javascript:;" class="reload"></a>--%>
                        <%--<a href="javascript:;" class="remove"></a>--%>
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-wizard">
                        <div class="form-body">
                            <ul class="nav nav-pills nav-justified steps">
                                <li class="span2<%=Step1Status%>">

                                    <a href="#tab1" data-toggle="tab" class="step">
                                        <span class="number">1</span>
                                        <span class="desc"><i class="fa fa-check"></i>Login
                                            <asp:Label ID="lbStep1UseDate" runat="server" Text=""></asp:Label></span>
                                    </a>
                                </li>
                                <li class="span2<%=Step2Status%>">
                                    <a href="#tab2" data-toggle="tab" class="step">
                                        <span class="number">2</span>
                                        <span class="desc"><i class="fa fa-check"></i>Chemist
                                            <asp:Label ID="lbStep2UseDate" runat="server" Text=""></asp:Label></span>
                                    </a>
                                </li>
                                <li class="span2<%=Step3Status%>">
                                    <a href="#tab3" data-toggle="tab" class="step">
                                        <span class="number">3</span>
                                        <span class="desc"><i class="fa fa-check"></i>Sr.Chemist
                                            <asp:Label ID="lbStep3UseDate" runat="server" Text=""></asp:Label></span>
                                    </a>
                                </li>
                                <li class="span2<%=Step4Status%>">
                                    <a href="#tab4" data-toggle="tab" class="step">
                                        <span class="number">4</span>
                                        <span class="desc"><i class="fa fa-check"></i>Admin
                                            <asp:Label ID="lbStep4UseDate" runat="server" Text=""></asp:Label></span>
                                    </a>
                                </li>
                                <li class="span2<%=Step5Status%>">
                                    <a href="#tab5" data-toggle="tab" class="step">
                                        <span class="number">5</span>
                                        <span class="desc"><i class="fa fa-check"></i>Lab Manager
                                            <asp:Label ID="lbStep5UseDate" runat="server" Text=""></asp:Label></span>
                                    </a>
                                </li>
                                <li class="span2<%=Step6Status%>">
                                    <a href="#tab6" data-toggle="tab" class="step">
                                        <span class="number">6</span>
                                        <span class="desc"><i class="fa fa-check"></i>Admin
                                            <asp:Label ID="lbStep6UseDate" runat="server" Text=""></asp:Label></span>
                                    </a>
                                </li>
                            </ul>
                            <div id="bar" class="progress progress-striped" role="progressbar">
                                <div class="progress-bar progress-bar-success">
                                </div>
                            </div>

                            <div class="tab-content">
                                <asp:PlaceHolder runat="server" ID="plhSampleInfo" />
                                <asp:PlaceHolder runat="server" ID="plhCoverPage" />
                            </div>
                        </div>
                        <div class="form-actions">
                            <div class="row">
                                <div class="col-md-offset-3 col-md-9">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <!-- BEGIN PAGE LEVEL SCRIPTS -->

</asp:Content>

