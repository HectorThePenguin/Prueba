USE [SIAP]
GO

IF EXISTS(SELECT ''
FROM SYS.OBJECTS
WHERE [OBJECT_ID] = OBJECT_ID(N'[dbo].[EnvioAlimento_RegistrarRecepcionProductoEnc]'))
 DROP PROCEDURE [dbo].[EnvioAlimento_RegistrarRecepcionProductoEnc]; 
GO
--======================================================
-- Author     : Franco jesus Inzunza Martinez
-- Create date: 19/10/2016 9:45:00 a.m.
-- Description: 
-- SpName     : EnvioAlimento_RegistrarRecepcionProductoEnc
--======================================================
CREATE PROCEDURE [dbo].[EnvioAlimento_RegistrarRecepcionProductoEnc]
@OrganizacionDestinoID INT,
@Folio INT,
@OrganizacionOrigenID INT,
@FechaTransferencia DATETIME,
@UsuarioCreacionID INT,
@TipoMovimientoID INT
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
		Comentarios

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
		''
	        )
	SELECT @@ROWCOUNT
	SET NOCOUNT OFF;
END

GO