USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CuentaValor_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CuentaValor_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[CuentaValor_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 16/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : CuentaValor_ObtenerPorID
--======================================================
CREATE PROCEDURE [dbo].[CuentaValor_ObtenerPorID]
@CuentaValorID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		CuentaValorID,
		CuentaID,
		OrganizacionID,
		Valor,
		Activo
	FROM CuentaValor
	WHERE CuentaValorID = @CuentaValorID
	SET NOCOUNT OFF;
END

GO
