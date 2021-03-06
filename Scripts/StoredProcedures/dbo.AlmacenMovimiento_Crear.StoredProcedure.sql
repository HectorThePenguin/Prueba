USE [SIAP]
GO

IF EXISTS(SELECT ''
FROM SYS.OBJECTS
WHERE [OBJECT_ID] = OBJECT_ID(N'[dbo].[AlmacenMovimiento_Crear]'))
 DROP PROCEDURE [dbo].[AlmacenMovimiento_Crear]; 
GO

--=============================================
-- Author     : Jesus Alvarez
-- Create date: 23/05/2014
-- Description: Crea un nuevo almacen inventario
-- AlmacenMovimiento_Crear
--=============================================
CREATE PROCEDURE [dbo].[AlmacenMovimiento_Crear]
	@AlmacenID INT,
	@TipoMovimientoID INT,
	@ProveedorID INT,
	@Status INT,
	@UsuarioCreacionID INT,
	@Observaciones VARCHAR(255),
	@EsEnvioAlimento BIT = FALSE,
	@OrganizacionOrigenID INT = 0
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @ValorFolio BIGINT;
	DECLARE @Folio_Text VARCHAR(6);
	DECLARE @OrganizacionOrigenID_Text VARCHAR(2);
	DECLARE @TipoMovimientoID_Text VARCHAR(2);
  DECLARE @PolizaGenerada BIT = 0;

	EXEC FolioAlmacen_Obtener  @AlmacenID, @TipoMovimientoID, @Folio = @ValorFolio output

	IF(@EsEnvioAlimento = 1) 
  BEGIN 
		SET @OrganizacionOrigenID_Text =(SELECT RIGHT('00'+CAST(@OrganizacionOrigenID AS VARCHAR(2)),2));
		SET @TipoMovimientoID_Text =(SELECT RIGHT('00'+CAST(@TipoMovimientoID AS VARCHAR(2)),2));	
		SET @Folio_Text = (SELECT RIGHT('000000' + CAST(@ValorFolio AS VARCHAR(6)),6));
		
		SET @ValorFolio = CAST((@TipoMovimientoID_Text + @OrganizacionOrigenID_Text + @Folio_Text)AS BIGINT);
		SET @PolizaGenerada = 1;
	END

	IF (@ProveedorID = 0) SET @ProveedorID = NULL
	INSERT AlmacenMovimiento(
		AlmacenID,
		TipoMovimientoID,
		ProveedorID,
		FolioMovimiento,
		Observaciones,
		FechaMovimiento,
		Status,
		FechaCreacion,
		UsuarioCreacionID,	
		PolizaGenerada
	)
	VALUES(
		@AlmacenID,
		@TipoMovimientoID,
		@ProveedorID,
		@ValorFolio,
		@Observaciones,
		GETDATE(),
		@Status,
		GETDATE(),
		@UsuarioCreacionID,
		@PolizaGenerada
	)
	SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END

GO