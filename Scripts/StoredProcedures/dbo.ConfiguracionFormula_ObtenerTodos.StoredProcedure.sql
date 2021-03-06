USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionFormula_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConfiguracionFormula_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionFormula_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : José Gilberto Quintero López
-- Create date: 03/04/2014 12:00:00 a.m.
-- Description: 
-- SpName     : ConfiguracionFormula_ObtenerTodos
--======================================================
CREATE PROCEDURE [dbo].[ConfiguracionFormula_ObtenerTodos]
@Activo BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		ConfiguracionFormulaID,
		OrganizacionID,
		FormulaID,
		PesoInicioMinimo,
		PesoInicioMaximo,
		TipoGanado,
		PesoSalida,
		FormulaSiguienteID,
		DiasEstanciaMinimo,
		DiasEstanciaMaximo,
		DiasTransicionMinimo,
		DiasTransicionMaximo,
		Disponibilidad,
		Activo
	FROM ConfiguracionFormula
	WHERE Activo = @Activo OR @Activo IS NULL
	SET NOCOUNT OFF;
END

GO
