USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Cuenta_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Cuenta_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[Cuenta_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 17/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Cuenta_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[Cuenta_ObtenerPorDescripcion]
@Descripcion varchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		cu.CuentaID,
		cu.Descripcion,
		tc.TipoCuentaID,
		tc.Descripcion AS TipoCuenta,
		cu.ClaveCuenta,
		cu.Activo
	FROM Cuenta cu
	INNER JOIN TipoCuenta tc on cu.TipoCuentaID = tc.TipoCuentaID
	WHERE cu.Descripcion = @Descripcion
	SET NOCOUNT OFF;
END

GO
