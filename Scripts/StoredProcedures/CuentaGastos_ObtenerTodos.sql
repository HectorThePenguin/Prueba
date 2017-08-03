IF EXISTS(SELECT * FROM   sys.objects WHERE  [object_id] = Object_id(N'[dbo].[CuentaGastos_ObtenerTodos]'))
BEGIN
 DROP PROCEDURE [dbo].[CuentaGastos_ObtenerTodos]
END
GO

--=============================================
-- Author     : Pedro Delgado
-- Create date: 2015/11/10
-- Description: 
-- Name : EXEC CuentaGastos_ObtenerTodos 
--=============================================
CREATE PROCEDURE [dbo].[CuentaGastos_ObtenerTodos]
	@Activo BIT = 1
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
				CGG.CuentaGastoID,
				CGG.OrganizacionID,
				O.Descripcion AS DescripcionOrganizacion,
				Cta.CuentaSAPID,
				Cta.CuentaSAP,
				Cta.Descripcion AS DescripcionCuenta,
				CGG.CostoID,
				C.Descripcion AS DescripcionCosto,
				C.ClaveContable,
				CGG.Activo,
				CGG.UsuarioCreacionID,
				CGG.FechaCreacion 
		FROM CatCuentaGasto CGG
		INNER JOIN Organizacion O ON O.OrganizacionID=CGG.OrganizacionID
		INNER JOIN Costo C ON C.CostoID = CGG.CostoID AND C.Activo = 1
		INNER JOIN CuentaSAP Cta ON Cta.CuentaSAP COLLATE Modern_Spanish_CI_AS =CGG.CuentaSAP COLLATE Modern_Spanish_CI_AS AND Cta.Activo = 1
			WHERE CGG.Activo = @Activo
			
	SET NOCOUNT OFF;
END
