USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CuentaSAP_ObtenerPorFiltroSinTipo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CuentaSAP_ObtenerPorFiltroSinTipo]
GO
/****** Object:  StoredProcedure [dbo].[CuentaSAP_ObtenerPorFiltroSinTipo]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Roque Solis
-- Create date: 2014/27/06
-- Description: 
-- CuentaSAP_ObtenerPorFiltroSinTipo 0,'', '',1
--=============================================
CREATE PROCEDURE [dbo].[CuentaSAP_ObtenerPorFiltroSinTipo]
@CuentaSAPID INT,
@CuentaSAP VARCHAR(10),
@Descripcion VARCHAR(50),
@Activo BIT
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
	WHERE @CuentaSAPID in (0, cs.CuentaSAPID)
	AND (cs.CuentaSAP = @CuentaSAP OR @CuentaSAP = '' OR @CuentaSAP IS NULL)
	AND (cs.Activo = @Activo OR @Activo IS NULL)
	SET NOCOUNT OFF;
END

GO
