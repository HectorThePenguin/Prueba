<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SIE.Web.Seguridad.Login" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Import Namespace="System.Web.Optimization" %>

<% var ruta = VirtualPathUtility.ToAbsolute("~"); %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <title></title>                        
        <asp:PlaceHolder ID="PlaceHolder1" runat="server">
            <%: Scripts.Render("~/bundles/Scripts_Comunes") %>
            <%: Styles.Render("~/bundles/Estilos_Comunes") %>            
        </asp:PlaceHolder>        
        <link href="../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css"/>	
        <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>
        <link href="../assets/css/style-metro.css" rel="stylesheet" type="text/css"/>
        <link href="../assets/css/style.css" rel="stylesheet" type="text/css"/>	
        <link href="../assets/css/themes/default.css" rel="stylesheet" type="text/css" id="Link1"/>
        <link href="../assets/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css"/>    		
        <link href="../assets/css/login.css" rel="stylesheet" type="text/css"/>                
        
    </head>
    <body class="login">
        <div class="logo">
            <img src="<%: ruta %>/Images/skLogo.png" alt="" /> 
        </div>
        <!-- BEGIN LOGIN -->
        <div class="content">
            <!-- BEGIN LOGIN FORM -->
            <form class="login-form">
                <div id="skm_LockPane" class="LockOff">                                        
                </div>                        
                <h3 class="form-title"><asp:Label runat="server" meta:resourcekey="LabelResource1"></asp:Label></h3>
                <div class="alert alert-error hide">
                    <button class="close" data-dismiss="alert"></button>
                    <span><asp:Label runat="server" meta:resourcekey="LabelResource2"></asp:Label></span>
                </div>
                <div class="control-group">                    
                    <div class="controls">
                        <div class="input-icon left">
                            <i class="icon-user"></i>
                            <input class="m-wrap" type="text" autocomplete="off" placeholder="Usuario" name="username" id="txtUsuario" maxlength="50"/>
                        </div>
                    </div>
                </div>
                <div class="control-group">                   
                    <div class="controls">
                        <div class="input-icon left">
                            <i class="icon-lock"></i>
                            <input class="m-wrap" type="password" autocomplete="off" placeholder="Contraseña" name="password" id="txtContra" maxlength="50"/>
                        </div>
                    </div>
                </div>
                <div class="form-actions">                    
                    <button class="btn blue pull-right" id="btnAccesar">
                        <asp:Label runat="server" meta:resourcekey="LabelResource3"></asp:Label>
                         <i class="m-icon-swapright m-icon-white"></i>
                    </button>            
                </div>                
            </form>                        
        </div>        

        <script src="../assets/plugins/jquery-1.10.1.min.js" type="text/javascript"> </script>
        <script src="../assets/plugins/jquery-migrate-1.2.1.min.js" type="text/javascript"> </script>
        <script src="../assets/plugins/jquery-ui/jquery-ui-1.10.1.custom.min.js" type="text/javascript"> </script>
        <script src="../assets/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"> </script>                                
        <script src="../assets/plugins/uniform/jquery.uniform.min.js" type="text/javascript" > </script>
        <script src="../>assets/plugins/jquery.blockui.min.js" type="text/javascript"></script>  
        <script src="../assets/plugins/uniform/jquery.uniform.min.js" type="text/javascript" ></script>
        <script src="../assets/plugins/jquery-validation/dist/jquery.validate.min.js" type="text/javascript"> </script>                      
        <script src="../assets/scripts/app.js" type="text/javascript"> </script>
        <script src="../assets/plugins/spin.js" type="text/javascript"> </script>
        <script src="../assets/plugins/jquery.spin.js" type="text/javascript"> </script>
        <script src="../Scripts/jscomun.js"></script>
        <script src="../Scripts/login.js" type="text/javascript"> </script>
        
        <script type="text/javascript">
            var ErrorValidar = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorValidar %>"/>';
            var ErrorUsuario = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorUsuario %>"/>';
            var ErrorTitulo = '<asp:Literal runat="server" Text="<%$ Resources:js.ErrorTitulo %>"/>';
            var InformacionTitulo = '<asp:Literal runat="server" Text="<%$ Resources:js.InformacionTitulo %>"/>';
            var BtnAceptar = '<asp:Literal runat="server" Text="<%$ Resources:js.BtnAceptar %>"/>';
    </script>
    </body>
</html>