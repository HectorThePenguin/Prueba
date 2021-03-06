USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProducto_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProducto_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Octavio Quintero>
-- Create date: <02/06/2014>
-- Description:	Guarda las muestras capturadas en la entrada del producto.
/*EntradaProducto_Actualizar'
	<ROOT>
		<EntradaProducto>
			<EntradaProductoID></EntradaProductoID>
			<ContratoID>12</ContratoID>
			<OrganizacionID>1</OrganizacionID>
			<TipoContratoID><TipoContratoID>
			<ProductoID>111</ProductoID>
			<RegistroVigilanciaID>1</RegistroVigilanciaID>
			<Observaciones><Observaciones>
			<OperadorIDAnalista><OperadorIDAnalista>
			<EstatusID></EstatusID>
			<UsuarioModificacionID></UsuarioModificacionID>
		</EntradaProducto>
	</ROOT>
'*/
-- =============================================
CREATE PROCEDURE [dbo].[EntradaProducto_Actualizar]
	@XMLEntrada XML
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    UPDATE E
	 SET   ContratoID = M.ContratoID,
		   TipoContratoID  = M.TipoContratoID,
		   OrganizacionID = M.OrganizacionID,
		   ProductoID = M.ProductoID,
		   RegistroVigilanciaID = M.RegistroVigilanciaID,
		   EstatusID = M.EstatusID,
		   OperadorIDAnalista = M.OperadorIDAnalista,
		   Observaciones = M.Observaciones,
		   FechaModificacion = M.FechaModificacion,
		   UsuarioModificacionID = M.UsuarioModificacionID
	FROM EntradaProducto E
	INNER JOIN EntradaProductoDetalle D ON (E.EntradaProductoID = D.EntradaProductoID)
	INNER JOIN
		 (SELECT X.EntradaProductoID,
				X.ContratoID,
				X.TipoContratoID,
				X.OrganizacionID,
				X.ProductoID,
				X.RegistroVigilanciaID,
				X.EstatusID,
				X.OperadorIDAnalista,
				X.Observaciones,
				GETDATE() FechaModificacion,
				X.UsuarioModificacionID	
		 FROM 
			(SELECT 
			    EntradaProductoID = T.item.value('./EntradaProductoID[1]', 'INT'),
				ContratoID = T.item.value('./ContratoID[1]', 'INT'),
				TipoContratoID = T.item.value('./TipoContratoID[1]', 'INT'),
				OrganizacionID   = T.item.value('./OrganizacionID[1]','INT'),
				ProductoID = T.item.value('./ProductoID[1]','INT'),
				RegistroVigilanciaID = T.item.value('./RegistroVigilanciaID[1]','INT'),
				EstatusID = T.item.value('./EstatusID[1]','INT'),
				OperadorIDAnalista = T.item.value('./OperadorIDAnalista[1]','INT'),
				Observaciones = T.item.value('./Observaciones[1]','VARCHAR(255)'),
				UsuarioModificacionID    = T.item.value('./UsuarioModificacionID[1]', 'INT')
			FROM  @XMLEntrada.nodes('ROOT/EntradaProducto') AS T(item)) X
		 ) M
      ON D.EntradaProductoID = M.EntradaProductoID
END

GO
