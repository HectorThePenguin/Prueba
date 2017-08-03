USE [SIAP]
GO

IF EXISTS(SELECT ''
FROM SYS.OBJECTS
WHERE [OBJECT_ID] = OBJECT_ID(N'[dbo].[EnvioAlimento_RegistrarRecepcionProductoDet]'))
 DROP PROCEDURE [dbo].[EnvioAlimento_RegistrarRecepcionProductoDet]; 
GO
--======================================================
-- Author     : Franco jesus Inzunza Martinez
-- Create date: 19/10/2016 9:45:00 a.m.
-- Description: 
-- SpName     : EnvioAlimento_RegistrarRecepcionProductoDet
--======================================================
CREATE PROCEDURE [dbo].[EnvioAlimento_RegistrarRecepcionProductoDet]
@OrganizacionDestinoID INT,
@TransferenciaID INT,
@ProductoID INT,
@Cantidad INT,
@UsuarioCreacionID INT,
@OrganizacionOrigenID INT,
@Importe FLOAT(53),
@TipoMovimientoID INT
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
		TipoMovimientoID
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
		@TipoMovimientoID
		)
	SELECT @@ROWCOUNT
	SET NOCOUNT OFF;
END

GO