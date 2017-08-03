USE [SIAP]
GO

IF EXISTS(SELECT ''
FROM SYS.OBJECTS
WHERE [OBJECT_ID] = OBJECT_ID(N'[dbo].[EnvioAlimento_ObtenerPorId]'))
 DROP PROCEDURE [dbo].[EnvioAlimento_ObtenerPorId]; 
GO
--======================================================
-- Author     : Franco jesus Inzunza Martinez
-- Create date: 19/10/2016 9:45:00 a.m.
-- Description: 
-- SpName     : EnvioAlimento_RegistrarEnvioAlimento
--======================================================
CREATE PROCEDURE [dbo].[EnvioAlimento_ObtenerPorId]
@EnvioProductoId BIGINT
AS
BEGIN
	SELECT EnvioProductoId 
		OrganizacionID,
		OrganizacionDestinoID,
		Folio,
		FechaCreacion,	
		AlmacenMovimientoID,
		Activo,
		UsuarioCreacionID,
		FechaEnvio
	FROM EnvioProducto
	WHERE EnvioProductoId = @EnvioProductoId 
END

GO