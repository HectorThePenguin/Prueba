USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProductoDetalle_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProductoDetalle_Actualizar]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProductoDetalle_Actualizar]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Octavio Quintero>
-- Create date: <02/06/2014>
-- Description:	Actualiza el detalle de la entrada del producto.
/*EntradaProductoDetalle_Actualizar '
	<ROOT>
		<EntradaProductoDetalle>
				<EntradaProductoDetalleID></EntradaProductoDetalleID>
				<EntradaProductoID></EntradaProductoID>
				<IndicadorID>1</IndicadorID>
				<Activo></Activo>
				<UsuarioModificacionID></UsuarioModificacionID>
			</EntradaProductoDetalle>
	</ROOT>
'*/
-- =============================================
CREATE PROCEDURE [dbo].[EntradaProductoDetalle_Actualizar]
	@XMLDetalle XML
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    UPDATE EntradaProductoDetalle 
	 SET   EntradaProductoID = M.EntradaProductoID,
		   IndicadorID = M.IndicadorID,
		   Activo = M.Activo,
		   FechaModificacion = M.FechaModificacion,
		   UsuarioModificacionID = M.UsuarioModificacionID
	FROM EntradaProductoDetalle D
	INNER JOIN
		 (SELECT X.EntradaProductoID,
				X.IndicadorID,
				X.Activo,
				GETDATE() FechaModificacion,
				X.UsuarioModificacionID,
				X.EntradaProductoDetalleID			
		 FROM 
			(SELECT 
				EntradaProductoID = T.item.value('./EntradaProductoID[1]', 'INT'),
				EntradaProductoDetalleID = T.item.value('./EntradaProductoDetalleID[1]', 'INT'),
				IndicadorID   = T.item.value('./IndicadorID[1]','INT'),
				Activo   = T.item.value('./Activo[1]','INT'),
				UsuarioModificacionID    = T.item.value('./UsuarioModificacionID[1]', 'INT')
			FROM  @XMLDetalle.nodes('ROOT/EntradaProductoDetalle') AS T(item)) X
		 ) M
      ON D.EntradaProductoDetalleID = M.EntradaProductoDetalleID
END

GO
