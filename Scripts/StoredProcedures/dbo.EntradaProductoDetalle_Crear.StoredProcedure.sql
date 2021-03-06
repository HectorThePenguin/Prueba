USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProductoDetalle_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[EntradaProductoDetalle_Crear]
GO
/****** Object:  StoredProcedure [dbo].[EntradaProductoDetalle_Crear]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Octavio Quintero>
-- Create date: <02/06/2014>
-- Description:	Guarda el detalle de la entrada del producto.
/*EntradaProductoDetalle_Crear '
	<ROOT>
		<EntradaProductoDetalle>
				<EntradaProductoID></EntradaProductoID>
				<IndicadorID>1</IndicadorID>
				<UsuarioCreacionID></UsuarioCreacionID>
			</EntradaProductoDetalle>
	</ROOT>
'*/
-- =============================================
CREATE PROCEDURE [dbo].[EntradaProductoDetalle_Crear]
	@XMLDetalle XML
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    INSERT INTO EntradaProductoDetalle
	(EntradaProductoID,IndicadorID,Activo,FechaCreacion,UsuarioCreacionID)
	(SELECT M.EntradaProductoID,
			M.IndicadorID,
			1,
			GETDATE(),
			M.UsuarioCreacionID			
	 FROM 
		(SELECT 
			EntradaProductoID = T.item.value('./EntradaProductoID[1]', 'INT'),
			IndicadorID   = T.item.value('./IndicadorID[1]','INT'),
			UsuarioCreacionID    = T.item.value('./UsuarioCreacionID[1]', 'INT')
		FROM  @XMLDetalle.nodes('ROOT/EntradaProductoDetalle') AS T(item)) M
     )
	 SELECT SCOPE_IDENTITY();
	 SET NOCOUNT OFF;
END

GO
