IF EXISTS(SELECT * FROM   sys.objects WHERE  [object_id] = Object_id(N'[dbo].[CuentaGastos_Guardar]'))
BEGIN
 DROP PROCEDURE [dbo].[CuentaGastos_Guardar]
END
GO

--======================================================
-- Author     : Edgar Villarreal
-- Create date: 09/10/2015 12:00:00 a.m.
-- Description: Crea Cuenta de gasto
-- SpName     : EXEC CuentaGastos_Guardar 1,1,4,'primero',1,1
--======================================================
CREATE PROCEDURE [dbo].[CuentaGastos_Guardar]
@OrganizacionID int,
@CuentaSAP VARCHAR(10),
@CostoID int,
@Activo bit,
@UsuarioCreacionID int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT CatCuentaGasto (
		OrganizacionID,
		CuentaSAP,
		CostoID,
		Activo,
		UsuarioCreacionID,
		FechaCreacion
	)
	VALUES(
		@OrganizacionID,
		@CuentaSAP,
		@CostoID,
		@Activo,
		@UsuarioCreacionID,
		GETDATE()
	)
	--SELECT SCOPE_IDENTITY()
	SET NOCOUNT OFF;
END
