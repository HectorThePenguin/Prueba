USE [SIAP]
GO

IF EXISTS(SELECT ''
FROM SYS.OBJECTS
WHERE [OBJECT_ID] = OBJECT_ID(N'[dbo].[EnvioAlimento_RegistrarEnvioAlimento]'))
 DROP PROCEDURE [dbo].[EnvioAlimento_RegistrarEnvioAlimento]; 
GO

--======================================================
-- Author     : Franco jesus Inzunza Martinez
-- Create date: 19/10/2016 9:45:00 a.m.
-- Description: 
-- SpName     : EnvioAlimento_RegistrarEnvioAlimento
--======================================================
CREATE PROCEDURE [dbo].[EnvioAlimento_RegistrarEnvioAlimento]
@OrganizacionID INT,
@OrganizacionDestinoID INT,
@Folio BIGINT,
@AlmacenMovimientoID BIGINT,
@UsuarioCreacionID INT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO EnvioProducto ( 
		OrganizacionOrigenId,
		OrganizacionDestinoID,
		Folio,
		FechaEnvio,
		FechaCreacion,	
		AlmacenMovimientoID,
		Activo,
		UsuarioCreacionID
		)
	VALUES (
			@OrganizacionID,
			@OrganizacionDestinoID,
			@Folio,
			GETDATE(),
			GETDATE(),
			@AlmacenMovimientoID,
			1,
		  @UsuarioCreacionID
		)

	SELECT SCOPE_IDENTITY() as 'EnvioProductoID',GETDATE() AS 'FechaEnvio'
	SET NOCOUNT OFF;
END