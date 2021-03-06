USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[LiquidacionDetalle_GuardarDetalleXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[LiquidacionDetalle_GuardarDetalleXML]
GO
/****** Object:  StoredProcedure [dbo].[LiquidacionDetalle_GuardarDetalleXML]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Gilberto Carranza
-- Create date: 19/12/2014
-- Description:  Guarda/Actualiza el detalle
-- LiquidacionDetalle_GuardarDetalleXML
-- =============================================
CREATE PROCEDURE [dbo].[LiquidacionDetalle_GuardarDetalleXML]
@LiquidacionDetalleXML XML	
AS
BEGIN
	SET NOCOUNT ON
		CREATE TABLE #LiquidacionDetalle (
			LiquidacionDetalleID INT,
			LiquidacionID BIGINT,
			EntradaProductoID INT,
			Activo BIT,
			UsuarioCreacionID INT,
			UsuarioModificacionID INT					
		)	
		INSERT #LiquidacionDetalle (
			LiquidacionDetalleID,
			LiquidacionID,
			EntradaProductoID,
			Activo,
			UsuarioCreacionID,
			UsuarioModificacionID)
		SELECT LiquidacionDetalleID = t.item.value('./LiquidacionDetalleID[1]', 'INT'),
			LiquidacionID = t.item.value('./LiquidacionID[1]', 'INT'),
			EntradaProductoID = t.item.value('./EntradaProductoID[1]', 'INT'),
			Activo = t.item.value('./Activo[1]', 'BIT'),
			UsuarioCreacionID = t.item.value('./UsuarioCreacionID[1]', 'INT'),
			UsuarioModificacionID = t.item.value('./UsuarioModificacionID[1]', 'INT')
		FROM @LiquidacionDetalleXML.nodes('ROOT/LiquidacionDetalle') AS T(item)
		DECLARE @Fecha DATETIME
		SET @Fecha = GETDATE()
		INSERT INTO LiquidacionDetalle
		(
			LiquidacionID
			, EntradaProductoID
			, Activo
			, UsuarioCreacionID
			, FechaCreacion
		)
		SELECT LiquidacionID
			,  EntradaProductoID
			,  Activo
			,  UsuarioCreacionID
			,  @Fecha
		FROM #LiquidacionDetalle
		WHERE LiquidacionDetalleID = 0
	SET NOCOUNT OFF
END

GO
