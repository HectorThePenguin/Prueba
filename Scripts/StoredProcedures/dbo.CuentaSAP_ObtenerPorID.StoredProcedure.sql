USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CuentaSAP_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CuentaSAP_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[CuentaSAP_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
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
CREATE PROCEDURE [dbo].[CuentaSAP_ObtenerPorID]
@CuentaSAPID INT
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
	WHERE CuentaSAPID = @CuentaSAPID
	and cs.Activo = 1
	SET NOCOUNT OFF;
END

GO
