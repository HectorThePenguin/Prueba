IF EXISTS(SELECT * FROM   sys.objects WHERE  [object_id] = Object_id(N'[dbo].[CuentaGastos_Actualizar]'))
BEGIN
 DROP PROCEDURE [dbo].[CuentaGastos_Actualizar]
END
GO

--======================================================
-- Author     : Carlos Cazarez Nuñez
-- Create date: 13/10/2015 12:00:00 a.m.
-- Description: 
-- SpName     : ReportePaseaProceso
--======================================================
CREATE PROCEDURE [dbo].[CuentaGastos_Actualizar]
@CuentaGastoID int,
@OrganizacionID int,
@CuentaSAP VARCHAR(10),
@CostoID decimal(10,2),
@Activo bit,
@UsuarioModificacionID int
AS
BEGIN
  -- routine body goes here, e.g.
  -- SELECT 'Navicat for SQL Server'
	SET NOCOUNT ON;
	UPDATE CatCuentaGasto SET
		OrganizacionID = @OrganizacionID,
		CuentaSAP = @CuentaSAP,
		CostoID = @CostoID,
		Activo = @Activo,
		UsuarioModificacionID = @UsuarioModificacionID,
		FechaModificacion = GETDATE()
	WHERE CuentaGastoID = @CuentaGastoID
	SET NOCOUNT OFF;
END