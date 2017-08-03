--======================================================
-- Author     : Franco jesus Inzunza Martinez
-- Create date: 19/10/2016 9:45:00 a.m.
-- Description: 
-- SpName     : EnvioAlimento_RegistrarRecepcionProductoEnc
--======================================================
IF EXISTS (SELECT  object_id FROM    sys.objects WHERE   object_id = OBJECT_ID(N'EnvioAlimento_RegistrarRecepcionProductoEnc') AND type IN ( N'P', N'PC' ) ) 
BEGIN
	DROP PROCEDURE EnvioAlimento_RegistrarRecepcionProductoEnc
END
GO
CREATE PROCEDURE [dbo].[EnvioAlimento_RegistrarRecepcionProductoEnc]
	@OrganizacionDestinoID INT,
	@Folio INT,
	@OrganizacionOrigenID INT,
	@FechaTransferencia DATETIME,
	@UsuarioCreacionID INT,
	@TipoMovimientoID INT,
	@AlmacenOrigenID INT
AS
BEGIN
DECLARE @TipoDestinoID AS INT;

SET @TipoDestinoID = (SELECT TipoOrganizacionID FROM SuKarne.dbo.CatOrganizacion WHERE OrganizacionID=@OrganizacionDestinoID)

	SET NOCOUNT ON;
	INSERT SuKarne.dbo.CacRecepcionProductoEnc(
		OrganizacionID,
		TransferenciaID,
		OrganizacionOrigenID,
		TipoDestinoID,
		OrganizacionDestinoID,
		EstatusTransferencia,
		FechaTransferencia,
		UsuarioCreacionID,
		UsuarioModificacionID,
		FechaCreacion,
		FechaModificacion,
		TipoMovimientoID,
		Comentarios,
		AlmacenOrigenID
	)
	VALUES(
		@OrganizacionDestinoID, 
		@Folio, 
		@OrganizacionOrigenID,
		@TipoDestinoID,
		@OrganizacionDestinoID,
		0,
		@FechaTransferencia,
		@UsuarioCreacionID, 
		1,
		GETDATE(),
		GETDATE(),
		@TipoMovimientoID,		
		'',
	    @AlmacenOrigenID )
	SELECT @@ROWCOUNT
	SET NOCOUNT OFF;
END
