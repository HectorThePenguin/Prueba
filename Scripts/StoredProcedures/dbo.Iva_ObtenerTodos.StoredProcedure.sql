USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Iva_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Iva_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[Iva_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 14/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Iva_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[Iva_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		IvaID,
		Descripcion,
		TasaIva,
		IndicadorIvaPagar,
		CuentaPagar,
		IndicadorIvaRecuperar,
		CuentaRecuperar,
		Activo
	FROM Iva
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO
