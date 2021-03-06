USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_GuardarAlmacenMovimiento]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Almacen_GuardarAlmacenMovimiento]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_GuardarAlmacenMovimiento]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    C�sar Valdez Figueroa
-- Create date: 18/02/2014
-- Description:  Guardar el Almacen Movimiento
-- Origen: APInterfaces
-- Almacen_GuardarAlmacenMovimiento 6,null,22,1,"Observaciones",1,100
-- =============================================
CREATE PROCEDURE [dbo].[Almacen_GuardarAlmacenMovimiento]
	@AlmacenID INT,
	@AnimalMovimientoID BIGINT,
	@TipoMovimientoID INT,
	@Status INT,
	@Observaciones VARCHAR(255),
	@UsuarioCreacionID INT,
	@ProveedorID INT
AS
	
	--CODIGO PARA VALIDAR VERSION
	DECLARE @TipoAlmacen int = (select top 1 TipoAlmacenID from Almacen where AlmacenID = @AlmacenID)
	if @TipoAlmacen not in (1,4) and @TipoMovimientoID = 22
	BEGIN
	raiserror('Error de versión, SIAP requiere actualizacion', 15, -1)	
	return
	end
	--CODIGO PARA VALIDAR VERSION
	
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
			AnimalMovimientoID, 
			FechaCreacion, 
			UsuarioCreacionID,
			ProveedorID) 
	VALUES (@AlmacenID , 
			@TipoMovimientoID, 
			@FolioAlmacen, 
			GETDATE(), 
			@Observaciones, 
			@Status, 
			CASE WHEN @AnimalMovimientoID = 0 THEN null ELSE @AnimalMovimientoID END, 
			GETDATE(), 
			@UsuarioCreacionID,
			CASE WHEN @ProveedorID = 0 THEN null ELSE @ProveedorID END )
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
		AnimalMovimientoID, 
		FechaCreacion, 
		UsuarioCreacionID,
		FechaModificacion,
		UsuarioModificacionID,
		ProveedorID
	FROM AlmacenMovimiento (nolock)
	WHERE AlmacenMovimientoID = @IdentityID
	SET NOCOUNT OFF
  END

GO
