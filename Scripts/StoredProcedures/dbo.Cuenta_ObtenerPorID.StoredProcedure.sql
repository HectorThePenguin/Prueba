USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Cuenta_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Cuenta_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[Cuenta_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jos� Gilberto Quintero L�pez
-- Create date: 2013/11/28
-- Description: 
-- Cuenta_ObtenerPorID 1
--=============================================
CREATE PROCEDURE [dbo].[Cuenta_ObtenerPorID]
@CuentaID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		c.CuentaID,
		c.Descripcion,
		c.TipoCuentaID,
		tc.Descripcion as [TipoCuenta],
		c.ClaveCuenta,
		c.Activo
	FROM Cuenta c
	inner join TipoCuenta tc on tc.TipoCuentaID = c.TipoCuentaID
	WHERE CuentaID = @CuentaID
	SET NOCOUNT OFF;
END

GO
