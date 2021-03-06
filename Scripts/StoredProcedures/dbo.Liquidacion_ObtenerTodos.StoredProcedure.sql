USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Liquidacion_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Liquidacion_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[Liquidacion_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 16/12/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Liquidacion_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[Liquidacion_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		LiquidacionID,
		ContratoID,
		OrganizacionID,
		TipoCambio,
		Folio,
		Fecha,
		IPRM,
		CuotaEjidal,
		ProEducacion,
		PIEAFES,
		Factura,
		Cosecha,
		FechaInicio,
		FechaFin,
		Activo
	FROM Liquidacion
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO
