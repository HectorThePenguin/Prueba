USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionFormula_ObtenerConfiguracionFormula]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConfiguracionFormula_ObtenerConfiguracionFormula]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionFormula_ObtenerConfiguracionFormula]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Cesar.Valdez
-- Create date: 2014/03/20
-- Description: SP para Obtener las configuraciones de formulas para cada organizacion
-- Origen     : APInterfaces
-- EXEC ConfiguracionFormula_ObtenerConfiguracionFormula 4
-- =============================================
CREATE PROCEDURE [dbo].[ConfiguracionFormula_ObtenerConfiguracionFormula]
	@OrganizacionID INT
AS
BEGIN
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
			Activo,
			FechaCreacion,
			UsuarioCreacionID
		FROM ConfiguracionFormula 
		WHERE Activo = 1
END

GO
