<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Titulo.ascx.cs" Inherits="SIE.Web.Controles.Titulo" %>
   
<div class="portlet-title">
    <div class="caption">
        <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/skLogo.png" meta:resourcekey="imgLogoResource1" />
        <span class="letra">
            <asp:Label ID="lblTitulo" runat="server" ></asp:Label></span>
    </div>
</div>

