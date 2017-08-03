--======================================================
-- Author     : Franco jesus Inzunza Martinez
-- Create date: 19/10/2016 9:45:00 a.m.
-- Description: 
-- SpName     : EnvioAlimento_RegistrarRecepcionProductoDet
--======================================================
IF EXISTS (SELECT  object_id FROM    sys.objects WHERE   object_id = OBJECT_ID(N'EnvioAlimento_RegistrarRecepcionProductoDet') AND type IN ( N'P', N'PC' ) ) 
BEGIN
	DROP PROCEDURE EnvioAlimento_RegistrarRecepcionProductoDet
END
GO
CREATE PROCEDURE [dbo].[EnvioAlimento_RegistrarRecepcionProductoDet]
	@OrganizacionDestinoID INT,
	@TransferenciaID INT,
	@ProductoID INT,
	@Cantidad INT,
	@UsuarioCreacionID INT,
	@OrganizacionOrigenID INT,
	@Importe FLOAT(53),
	@TipoMovimientoID INT,
	@AlmacenOrigenID INT
AS
BEGIN
DECLARE @TipoDestinoID AS INT;

SET @TipoDestinoID = (SELECT TipoOrganizacionID FROM SuKarne.dbo.CatOrganizacion WHERE OrganizacionID=@OrganizacionDestinoID)

	SET NOCOUNT ON;
	INSERT SuKarne.dbo.CacRecepcionProductoDet(
		OrganizacionID,
		TransferenciaID,
		OrganizacionOrigenID,
		DetalleID,
		ProductoID,
		AreteID,	
		Cantidad,
		FechaCreacion,
		UsuarioCreacionID,
		FechaModificacion,
		Importe,
		TipoMovimientoID,
		AlmacenOrigenID
	)
	VALUES(
		@OrganizacionDestinoID, 
		@TransferenciaID,
		@OrganizacionOrigenID,	
		1,
		@ProductoID,
		0,
		@Cantidad,
		GETDATE(),
		@UsuarioCreacionID,
		GETDATE(),
		@Importe,
		@TipoMovimientoID,
		@AlmacenOrigenID)
	SELECT @@ROWCOUNT
	SET NOCOUNT OFF;
END