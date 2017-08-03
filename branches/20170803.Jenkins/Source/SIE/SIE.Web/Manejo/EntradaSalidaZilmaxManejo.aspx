<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EntradaSalidaZilmaxManejo.aspx.cs" Inherits="SIE.Web.Manejo.EntradaSalidaZilmaxManejo" culture="auto" uiculture="auto" %>
<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>SIAP - SuKarne <%: ConfigurationManager.AppSettings["version"] %></title>

    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <%: Scripts.Render("~/bundles/Scripts_Comunes") %>
        <%: Styles.Render("~/bundles/Estilos_Comunes") %>
        <%: Scripts.Render("~/bundles/jscomunScript") %>
    </asp:PlaceHolder>

    <script src="../assets/plugins/jquery-ui/jquery.ui.datepicker-es.js"></script>
    <link rel="stylesheet" href="../assets/plugins/jquery-ui/jquery-ui-1.10.1.custom.min.css" />
    <script src="../assets/plugins/jquery-ui/jquery.ui.datepicker-es.js"></script>
    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <script src="../Scripts/EntradaSalidaZilmaxManejo.js"></script>
    <script type="text/javascript">
         javascript: window.history.forward(1);
    </script>

</head>
<body class="page-header-fixed">
  <div id="pagewrap">
       <form id="idform" runat="server" class="form-horizontal"> 
            <div class="container-fluid">
            <div class="portlet box SuKarne2">
                <div class="portlet-title">
                    <div class="row-fluid caption">
                        <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
                        <span>
                            <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="lblTitulo"></asp:Label>
                        </span>
                    </div>
                </div>
                <div  class="portlet-body form">
                    <div class="row-fluid">
                        <ul class="breadcrumb">
                            <li>
                                <i class="icon-home"></i>
                                 <a href="../Principal.aspx"><asp:Label ID="LabelHome" runat="server" meta:resourcekey="Configuracion_Home"></asp:Label></a> 
                                <i class="icon-angle-right"></i>
                            </li>
                            <li>
                                <a href="EntradaSalidaZilmaxManejo.aspx"><asp:Label ID="LabelMenu" runat="server" meta:resourcekey="Configuracion_Title"></asp:Label></a> 
                            </li>
                        </ul>
                    </div>

                    <div class="row-fluid"> 
                        <fieldset class="scheduler-border span3"> 
                        <legend class="scheduler-border"><asp:Label ID="EntZimax" runat="server"  meta:resourcekey="EntZimax"></asp:Label></legend>          
                         <div style="height:290px;overflow-y:auto;overflow-x:auto; width:100%;float:left"> 
                          <table id="tbEntradaZilmax" class="table table-striped table-advance table-hover" >
                            <thead>
                               <tr>
                                   <th class="span6 alineacionCentro" scope="col"><asp:Label ID="lblCorralE" runat="server" meta:resourcekey="HeaderECorral"></asp:Label></th>
                                   <th class="span6 alineacionCentro" scope="col"><asp:Label ID="lblFormulaE" runat="server" meta:resourcekey="HeaderEFormula"></asp:Label></th>
                                   <th class="span6 alineacionCentro" scope="col"><asp:Label ID="lblCheckEntradaZilmax" runat="server" ></asp:Label></th>
                               </tr>
                             </thead>
                             <tbody></tbody>                              
                          </table>
                         </div>
                        </fieldset>
                        <div class="span1"></div>
                        <fieldset class="scheduler-border span3"> 
                        <legend class="scheduler-border"><asp:Label ID="SalZilmax" runat="server"  meta:resourcekey="SalZilmax"></asp:Label></legend>  
                        <div style="height:290px;overflow-y:auto;overflow-x:auto; width:100%;float:left;">   
                          <table id="tbSalidaZilmax"  class="table table-striped table-advance table-hover">
                            <thead>
                              <tr>
                                   <th class="span6 alineacionCentro" scope="col"><asp:Label ID="lblCorralS" runat="server" meta:resourcekey="HeaderECorral"></asp:Label></th>
                                   <th class="span6 alineacionCentro" scope="col"><asp:Label ID="lblFormulaS" runat="server" meta:resourcekey="HeaderEFormula"></asp:Label></th>
                                  <th class="span6 alineacionCentro" scope="col"><asp:Label ID="lblCheckSalidaZilmax" runat="server" ></asp:Label></th>
                               </tr>
                             </thead>
                             <tbody></tbody>
                          </table>
                        </div>
                        </fieldset>
                        <div class="span1"></div>
                        <div class="scheduler-border span4">
                            <div style="height:290px;">
                              <table style="width:100%">
                                <tr>
                                    <td><asp:Label ID="lblFecha" runat="server" meta:resourcekey="lblFechaResource1"></asp:Label></td>
                                    <td><asp:TextBox ID="datepicker" class="control span12" runat="server" ReadOnly="True"></asp:TextBox></td>
                                </tr>
                                <tr style="height:35px">
                                    <td>
                                         <asp:Label ID="lblCbzEntrada" runat="server" meta:resourcekey="lblCbzEntradaResource1"></asp:Label>   
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCbzEntrada" runat="server" class="control span12" meta:resourcekey="txtCbzEntradaResource1" ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="height:35px">
                                    <td>
                                        <asp:Label ID="LblCorralesEntrada" runat="server" meta:resourcekey="LblCorralesEntradaResource1"></asp:Label>
                                    </td>
                                    <td>
                                         <asp:TextBox ID="TxtCorralesEntrada" runat="server" class="control span12" meta:resourcekey="TxtCorralesEntradaResource1" ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="height:35px">
                                    <td>
                                        <asp:Label ID="lblCbzSalida" runat="server" meta:resourcekey="lblCbzSalidaResource1"></asp:Label>
                                    </td>
                                    <td>
                                         <asp:TextBox ID="txtCbzSalida" runat="server" class="control span12" meta:resourcekey="txtCbzSalidaResource1" ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="height:35px">
                                    <td>
                                        <asp:Label ID="LblCorralesSalida" runat="server" meta:resourcekey="LblCorralesSalidaResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtCorralesSalida" runat="server" class="control span12" meta:resourcekey="TxtCorralesSalidaResource1" ></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            </div>
                            <div class="textoDerecha">
                                    <span class="espacioCortoDerecha">
                                        <button type="button" id="btnGuardar" class="btn SuKarne">
                                            <asp:Label ID="Label9" runat="server" Text="Guardar"></asp:Label></button>
                                    </span>
                                    <span>
                                        <button id="btnCancelar" type="button" class="btn SuKarne">
                                            <asp:Label ID="Label10" runat="server" Text="Cancelar"></asp:Label>
                                        </button>
                                    </span>
                                </div>
                            <!-- Dialogo De Salir -->
                            <div id="dlgCancelarBuscar" class="modal hide fade"  tabindex="-1" data-backdrop="static" data-keyboard="false">
							 <div class="modal-body">
								<asp:Label ID="Label1" runat="server" meta:resourcekey="msgSalirBuscar"></asp:Label>
							 </div>
							 <div class="modal-footer">
								<asp:Button runat="server" ID="btnSiBuscar" CssClass="btn SuKarne" meta:resourcekey="btnDialogoSi" data-dismiss="modal"/>
                                <asp:Button runat="server" ID="btnNoBuscar" CssClass="btn SuKarne" meta:resourcekey="btnDialogoNo" data-dismiss="modal"/>
							 </div>
						  </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
       </form>
  </div>
</body>
</html>
