<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="ALS.ALSI.Web.ForgotPassword" %>

<!DOCTYPE html>

<!-- 
Template Name: Metronic - Responsive Admin Dashboard Template build with Twitter Bootstrap 3.3.4
Version: 3.3.0
Author: KeenThemes
Website: http://www.keenthemes.com/
Contact: support@keenthemes.com
Follow: www.twitter.com/keenthemes
Like: www.facebook.com/keenthemes
Purchase: http://themeforest.net/item/metronic-responsive-admin-dashboard-template/4021469?ref=keenthemes
License: You must have a valid license purchased only from themeforest(the above link) in order to legally use the theme for your project.
-->
<!--[if IE 8]> <html lang="en" class="ie8 no-js"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9 no-js"> <![endif]-->
<!--[if !IE]><!-->
<html lang="en">
<!--<![endif]-->
<!-- BEGIN HEAD -->
<head>
    <meta charset="utf-8" />
<title>	ALIS | Laboratory Information System</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta http-equiv="Content-type" content="text/html; charset=utf-8">
    <meta content="" name="description" />
    <meta content="" name="author" />
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
    <link href="<%= ResolveUrl("~/assets/global/plugins/font-awesome/css/font-awesome.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= ResolveUrl("~/assets/global/plugins/simple-line-icons/simple-line-icons.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= ResolveUrl("~/assets/global/plugins/bootstrap/css/bootstrap.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= ResolveUrl("~/assets/global/plugins/uniform/css/uniform.default.css") %>" rel="stylesheet" type="text/css" />
    <!-- END GLOBAL MANDATORY STYLES -->
    <!-- BEGIN PAGE LEVEL STYLES -->
    <link href="<%= ResolveUrl("~/assets/admin/pages/css/login.css") %>" rel="stylesheet" type="text/css" />
    <!-- END PAGE LEVEL SCRIPTS -->
    <!-- BEGIN THEME STYLES -->
    <link href="<%= ResolveUrl("~/assets/global/css/components.css") %>" id="style_components" rel="stylesheet" type="text/css" />
    <link href="<%= ResolveUrl("~/assets/global/css/plugins.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= ResolveUrl("~/assets/admin/layout/css/layout.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= ResolveUrl("~/assets/admin/layout/css/themes/darkblue.css") %>" rel="stylesheet" type="text/css" id="style_color" />
    <link href="<%= ResolveUrl("~/assets/admin/layout/css/custom.css") %>" rel="stylesheet" type="text/css" />
    <!-- END THEME STYLES -->
    <link rel="shortcut icon" href="favicon.ico" />
</head>
<!-- END HEAD -->
<!-- BEGIN BODY -->
<body class="login">
    <!-- BEGIN SIDEBAR TOGGLER BUTTON -->
    <div class="menu-toggler sidebar-toggler">
    </div>
    <!-- END SIDEBAR TOGGLER BUTTON -->
    <!-- BEGIN LOGO -->
    <div class="logo">
        <a href="index.html">
            <%--<img src="/assets/admin/layout/img/logo-big.png" alt="" width="140" />--%>
        </a>
    </div>
    <!-- END LOGO -->
    <!-- BEGIN LOGIN -->
    <div class="content">
        <!-- BEGIN LOGIN FORM -->
        <form runat="server">
            <!-- BEGIN FORGOT PASSWORD FORM -->
            <h3>Forget Password ?</h3>
            <p>
                Enter your e-mail address below to reset your password.
            </p>
            <div class="form-group">
                <asp:TextBox ID="txtEmail" runat="server" class="form-control placeholder-no-fix"></asp:TextBox>
            </div>
            <div class="form-actions">
                <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-default" OnClick="btnBack_Click">Back <i class="m-icon-swapleft"></i></asp:LinkButton>
                <asp:LinkButton ID="LinkButton2" runat="server" class="btn btn-success uppercase pull-right" OnClick="btnSubmit_Click">Submit <i class="m-icon-swapright m-icon-white"></i></asp:LinkButton>
            </div>
            <!-- END FORGOT PASSWORD FORM -->
        </form>
        <!-- END LOGIN FORM -->


    </div>
    <div class="copyright">
        2015 &copy;  ALS TESTING SERVICES (THAILAND) CO., LTD.
    </div>
    <!-- END LOGIN -->
    <!-- BEGIN JAVASCRIPTS(Load javascripts at bottom, this will reduce page load time) -->
    <!-- BEGIN CORE PLUGINS -->
    <!--[if lt IE 9]>
<script src="../../assets/global/plugins/respond.min.js"></script>
<script src="../../assets/global/plugins/excanvas.min.js"></script> 
<![endif]-->
    <script src="<%= ResolveUrl("~/assets/global/plugins/jquery.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/assets/global/plugins/jquery-migrate.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/assets/global/plugins/bootstrap/js/bootstrap.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/assets/global/plugins/jquery.blockui.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/assets/global/plugins/jquery.cokie.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/assets/global/plugins/uniform/jquery.uniform.min.js") %>" type="text/javascript"></script>
    <!-- END CORE PLUGINS -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <script src="<%= ResolveUrl("~/assets/global/plugins/jquery-validation/js/jquery.validate.min.js") %>" type="text/javascript"></script>
    <!-- END PAGE LEVEL PLUGINS -->
    <!-- BEGIN PAGE LEVEL SCRIPTS -->
    <script src="<%= ResolveUrl("~/assets/global/scripts/metronic.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/assets/admin/layout/scripts/layout.js") %>" type="text/javascript"></script>
    <%--<script src="/assets/admin/layout/scripts/demo.js" type="text/javascript"></script>
<script src="/assets/admin/pages/scripts/login.js" type="text/javascript"></script>--%>
    <!-- END PAGE LEVEL SCRIPTS -->
    <script>
        jQuery(document).ready(function () {
            Metronic.init(); // init metronic core components
            Layout.init(); // init current layout
            //Login.init();
            //Demo.init();
        });
    </script>
    <!-- END JAVASCRIPTS -->
</body>
<!-- END BODY -->
</html>

