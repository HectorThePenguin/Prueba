USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimiento_CrearCierreDia]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[AlmacenMovimiento_CrearCierreDia]
GO
/****** Object:  StoredProcedure [dbo].[AlmacenMovimiento_CrearCierreDia]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jorge Luis Vel�zquez Araujo
-- Create date: 02/07/2014
-- Description: Crea un nuevo almacen inventario
-- AlmacenMovimiento_CrearCierreDia
--=============================================
CREATE PROCEDURE [dbo].[AlmacenMovimiento_CrearCierreDia]
	@AlmacenID INT,
	@TipoMovimientoID INT,	
	@FolioMovimiento BIGINT,	
	@Status INT,
	@UsuarioCreacionID INT,
	@Observaciones VARCHAR(255)
AS
BEGIN
	SET NOCOUNT ON;
	INSERT AlmacenMovimiento(
		AlmacenID,
		TipoMovimientoID,
		FolioMovimiento,
		Observaciones,
		FechaMovimiento,
		Status,
		FechaCreacion,
		UsuarioCreacionID
	)
	VALUES(
		@AlmacenID,
		@TipoMovimientoID,
		@FolioMovimiento,
		@Observaciones,
		GETDATE(),
		@Status,
		GETDATE(),
		@UsuarioCreacionID
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO
