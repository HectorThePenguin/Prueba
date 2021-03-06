USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Cuenta_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Cuenta_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[Cuenta_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 2013/11/28
-- Description: 
-- Cuenta_ObtenerTodos 1
--=============================================
CREATE PROCEDURE [dbo].[Cuenta_ObtenerTodos] @Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT c.CuentaID
		,c.Descripcion
		,c.TipoCuentaID
		,tc.Descripcion AS [TipoCuenta]
		,c.ClaveCuenta
		,c.Activo
	FROM Cuenta c
	INNER JOIN TipoCuenta tc ON tc.TipoCuentaID = c.TipoCuentaID
	WHERE c.Activo = @Activo
		OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO
