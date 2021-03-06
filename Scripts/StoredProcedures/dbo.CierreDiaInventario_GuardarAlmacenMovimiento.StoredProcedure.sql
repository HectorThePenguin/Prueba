USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CierreDiaInventario_GuardarAlmacenMovimiento]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CierreDiaInventario_GuardarAlmacenMovimiento]
GO
/****** Object:  StoredProcedure [dbo].[CierreDiaInventario_GuardarAlmacenMovimiento]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Edgar Villarreal
-- Create date: 30/03/2014
-- Description:  Guardar el Almacen Movimiento de cierre inventario
-- Origen: APInterfaces
-- EXEC CierreDiaInventario_GuardarAlmacenMovimiento 1,18,1,'Observacion',1
-- =============================================
CREATE PROCEDURE [dbo].[CierreDiaInventario_GuardarAlmacenMovimiento]
@AlmacenID INT,
	@TipoMovimientoID INT,
	@EstatusID INT,
	@Observaciones VARCHAR(255),
	@UsuarioCreacion INT
AS
  BEGIN
      SET NOCOUNT ON
	DECLARE @FolioAlmacen BIGINT ;
	DECLARE @IdentityID BIGINT;
	EXEC FolioAlmacen_Obtener @AlmacenID, @TipoMovimientoID, @Folio = @FolioAlmacen OUTPUT
	INSERT INTO AlmacenMovimiento (
			AlmacenID, 
			TipoMovimientoID, 
			FolioMovimiento, 
			FechaMovimiento, 
			Observaciones, 
			Status, 
			FechaCreacion, 
			UsuarioCreacionID) 
	VALUES (@AlmacenID , 
			@TipoMovimientoID, 
			@FolioAlmacen, 
			GETDATE(), 
			@Observaciones, 
			@EstatusID, 
			GETDATE(), 
			@UsuarioCreacion )
	/* Se obtiene el id Insertado */
	SET @IdentityID = (SELECT @@IDENTITY)
	SELECT 
		AlmacenMovimientoID,
		AlmacenID, 
		TipoMovimientoID, 
		FolioMovimiento, 
		FechaMovimiento, 
		Observaciones, 
		Status, 
		FechaCreacion, 
		UsuarioCreacionID
	FROM AlmacenMovimiento 
	WHERE AlmacenMovimientoID = @IdentityID
	SET NOCOUNT OFF
  END

GO
