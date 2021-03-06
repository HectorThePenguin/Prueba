USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CuentaSAP_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CuentaSAP_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[CuentaSAP_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--=============================================
-- Author     : José Gilberto Quintero López
-- Create date: 2013/12/10
-- Description: 
-- 
--=============================================
CREATE PROCEDURE [dbo].[CuentaSAP_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		cs.CuentaSAPID,
		cs.CuentaSAP,
		cs.Descripcion,
		cs.TipoCuentaID,
		tc.Descripcion as [TipoCuenta],
		cs.Activo
	FROM CuentaSAP cs
	INNER JOIN TipoCuenta tc on tc.TipoCuentaID = cs.TipoCuentaID
	WHERE cs.Activo = 1
	SET NOCOUNT OFF;
END

GO
