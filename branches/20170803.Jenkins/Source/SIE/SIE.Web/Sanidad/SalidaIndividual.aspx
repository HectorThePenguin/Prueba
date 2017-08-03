<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalidaIndividual.aspx.cs" Inherits="SIE.Web.Sanidad.SalidaIndividual" Culture="auto" meta:resourcekey="PageResource1" %>
<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>SIAP - SuKarne <%: ConfigurationManager.AppSettings["version"] %></title>
        <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <%: Scripts.Render("~/bundles/Scripts_Comunes") %>
        <%: Styles.Render("~/bundles/Estilos_Comunes") %>
        <%: Scripts.Render("~/bundles/CalificacionGanadoScript") %>
        
    </asp:PlaceHolder>
</head>
<body>
    <div id="pagewrap">
        <form id="idform" runat="server" class="form-horizontal">
            <div class="container-fluid" />
            <div class="row-fluid">
                <div class="span12">
                    <div class="portlet box SuKarne2">
                        <div class="portlet-title">
                            <div class="caption">
                                <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                                <span class="letra">
                                    <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="lblTituloResource1"></asp:Label></span>
                            </div>
                        </div>
                        <div class="portlet-body form">
							<ul class="breadcrumb">
				                <li>
					                <i class="icon-home"></i>
					                <a href="../Principal.aspx">Home</a> 
					                <i class="icon-angle-right"></i>
				                </li>
                                <li>
					                <a href="SalidaIndividual.aspx">Salida individual.</a> 
				                </li>
			                </ul>
                            <div class="row-fluid">
                                <fieldset class="scheduler-border">
                                    <legend class="scheduler-border">
                                        <asp:Label ID="lblMonitor" runat="server" meta:resourcekey="msgSeleccione"></asp:Label></legend>
                                    <div>
                                        <table class="table">
                                    <tr>
                                        
                                        <td>
                                            <div>
                                                <label class="radio">
                                                    <asp:RadioButton ID="rdbMetafilaxia" class="radio" runat="server" Enabled="False" meta:resourcekey="rdbSalidaRecuperacion" />
                                                </label>
                                            </div>
                                        </td>
                                        <td>
                                            <label class="radio">
                                                <asp:RadioButton ID="rdbNormal" runat="server" class="radio" Enabled="False" meta:resourcekey="rdbSalidaSacrificio" />
                                            </label>

                                        </td>
                                        <td>
                                            <label class="radio">
                                                <asp:RadioButton ID="RadioButton1" runat="server" class="radio" Enabled="False" meta:resourcekey="rdbSalidaVenta" />
                                            </label>

                                        </td>
                                    </tr>

                                    
                                </table>
                                    </div>
                                    
                                </fieldset>
                            </div>
                            <div class="row-fluid">
                                <fieldset class="scheduler-border">
                                    <legend class="scheduler-border">
                                        <asp:Label ID="Label1" runat="server" meta:resourcekey="msgSeleccione"></asp:Label></legend>
                                    <div class="span12">
                                        <div class="span4">
                                           <div class="span6">
                                            <span>
                                                <asp:Label ID="lblSalida" runat="server"  meta:resourcekey="lblSalidaResource1"></asp:Label>
                                            </span>   
                                           </div> 
                                            <div class="span6">
                                                <asp:TextBox ID="txtSalida" runat="server"  ReadOnly="True" meta:resourcekey="txtSalidaResource1" ViewStateMode="Enabled"></asp:TextBox>
                                            </div>
                                            
                                            
                                        </div>
                                        <div class="span4">
                                            <div class="span6">
                                            <span>
                                                <asp:Label ID="Label2" runat="server"  meta:resourcekey="lblAreteResource1"></asp:Label>
                                            </span>   
                                           </div> 
                                            <div class="span6">
                                                <asp:TextBox ID="TextBox1" runat="server"  ReadOnly="True" meta:resourcekey="txtAreteResource1" ViewStateMode="Enabled"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="span4">
                                           <div class="span6">
                                            <span>
                                                <asp:Label ID="Label3" runat="server"  meta:resourcekey="lblFechaResource1"></asp:Label>
                                            </span>   
                                           </div> 
                                            <div class="span6">
                                                <asp:TextBox ID="TextBox2" runat="server"  ReadOnly="True" meta:resourcekey="txtSalidaResource1" ViewStateMode="Enabled"></asp:TextBox>
                                            </div>
                                        </div>
                                    
                                    </div>
                                    
                                </fieldset>
                            </div>
                            
                            
                            
                        </div>
                    </div>
                
                </div>
            </div>
            <br />
           
        </form>
    </div>
     <script type="text/javascript">
        
         

    </script>
</body>

</html>
