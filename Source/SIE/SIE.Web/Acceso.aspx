<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Acceso.aspx.cs" Inherits="SIE.Web.Acceso" %>

<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SIAP - SuKarne <%: ConfigurationManager.AppSettings["version"] %></title>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <%: Scripts.Render("~/bundles/Scripts_Comunes") %>
        <%: Styles.Render("~/bundles/Estilos_Comunes") %>
        <%: Scripts.Render("~/bundles/jscomunScript") %>
    </asp:PlaceHolder>
    <link href="assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="assets/css/demo.css" rel="stylesheet" />
    <%--<link href="../assets/css/Disponibilidad.css" rel="stylesheet" />--%>
    <link href="assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/style-metro.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/themes/default.css" rel="stylesheet" type="text/css" id="Link1" />
    <link href="assets/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/login.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/Acceso.css" rel="stylesheet" />
    <script src="Scripts/Acceso.js"></script>
    <link rel="shortcut icon" href="/favicon.ico" />
    <script type="text/javascript">
        function changeHashOnLoad() {
            window.location.href += "#";
            setTimeout("changeHashAgain()", "50");
        }

        function changeHashAgain() {
            window.location.href += "1";
        }

        var storedHash = window.location.hash;
        window.setInterval(function () {
            if (window.location.hash != storedHash) {
                window.location.hash = storedHash;
            }
        }, 50);

    </script>
</head>
<body class="login" onload="changeHashOnLoad();" ondragstart="return false;" ondrop="return false;">
    <form id="form1" runat="server">
        <body class="login">
            <div class="logo">
                <img src="Images/skLogo.png" alt="" />
            </div>
            <!-- BEGIN LOGIN -->
            <div class="alineacionCentro ">
                
                <section class="main">
			
				<ul class="ch-grid">
					<li>
						<div class="ch-item ch-img-1">
							<div class="ch-info">
								<h3>SIAP Engorda</h3>
							</div>
						</div>
					</li>
					<li>
						<div class="ch-item ch-img-2">
							<div class="ch-info">
								<h3>SIAP Abastos</h3>
								
							</div>
						</div>
					</li>
				</ul>
				
			</section>

              <%--  <span>
                    <button class="btn blue CajaAcceso" id="btnAccesar">
                        <asp:Label ID="lblSIAP" runat="server" Text="SIAP Engorda"></asp:Label>
                    </button>
                </span>
                <span>
                    <button class="btn blue CajaAcceso" id="Button1">
                        <asp:Label ID="lblCentros" runat="server" Text="SIAP Abastos"></asp:Label>

                    </button>
                </span>--%>
            </div>
            <asp:HiddenField runat="server" ID="hfUsuario"/>
    </form>

    <script src="assets/plugins/jquery-1.10.1.min.js" type="text/javascript"> </script>
    <script src="assets/plugins/jquery-migrate-1.2.1.min.js" type="text/javascript"> </script>
    <script src="assets/plugins/jquery-ui/jquery-ui-1.10.1.custom.min.js" type="text/javascript"> </script>
    <script src="assets/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"> </script>
    <script src="assets/plugins/uniform/jquery.uniform.min.js" type="text/javascript"> </script>
    <script src="assets/plugins/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="assets/plugins/uniform/jquery.uniform.min.js" type="text/javascript"></script>
    <script src="assets/plugins/jquery-validation/dist/jquery.validate.min.js" type="text/javascript"> </script>
    <script src="assets/scripts/app.js" type="text/javascript"> </script>
    <script src="assets/plugins/spin.js" type="text/javascript"> </script>
    <script src="assets/plugins/jquery.spin.js" type="text/javascript"> </script>
    <script src="Scripts/jscomun.js"></script>
    <script src="assets/scripts/modernizr.custom.79639.js"></script>

</body>
</html>
