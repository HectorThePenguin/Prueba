USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_GuardarAlmacenMovimientoConFecha]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Almacen_GuardarAlmacenMovimientoConFecha]
GO
/****** Object:  StoredProcedure [dbo].[Almacen_GuardarAlmacenMovimientoConFecha]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Jorge Luis Velazquez Araujo
-- Create date: 20/10/2014
-- Description:  Guardar el Almacen Movimiento con la fecha del movimiento como parametro
-- Almacen_GuardarAlmacenMovimientoConFecha 1,null,12,'20141020',1,"Observaciones",1
-- =============================================
CREATE PROCEDURE [dbo].[Almacen_GuardarAlmacenMovimientoConFecha]
	@AlmacenID INT,
	@AnimalMovimientoID BIGINT,
	@TipoMovimientoID INT,
	@FechaMovimiento DATETIME,
	@Status INT,
	@Observaciones VARCHAR(255),
	@UsuarioCreacionID INT,
	@ProveedorID INT
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
			AnimalMovimientoID, 
			FechaCreacion, 
			UsuarioCreacionID,
			ProveedorID) 
	VALUES (@AlmacenID , 
			@TipoMovimientoID, 
			@FolioAlmacen, 
			@FechaMovimiento, 
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
