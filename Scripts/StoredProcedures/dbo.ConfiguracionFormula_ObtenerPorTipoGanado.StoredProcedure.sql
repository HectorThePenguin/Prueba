USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionFormula_ObtenerPorTipoGanado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConfiguracionFormula_ObtenerPorTipoGanado]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionFormula_ObtenerPorTipoGanado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Pedro Delgado
-- Create date: 2014/04/02
-- Description: Sp para obtener las configuraciones de la ganadera por tipo de ganado
-- Origen     : APInterfaces
-- EXEC  [dbo].[ConfiguracionFormula_ObtenerPorTipoGanado] 1,4
--=============================================
CREATE PROCEDURE [dbo].[ConfiguracionFormula_ObtenerPorTipoGanado]
@TipoGanadoID INT,
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
		UsuarioCreacionID,
		FechaModificacion,
		UsuarioModificacionID
	FROM ConfiguracionFormula CF
	WHERE OrganizacionID = @OrganizacionID AND (SELECT Registros FROM dbo.FuncionSplit(CF.TipoGanado,'|') WHERE Registros = @TipoGanadoID) IS NOT NULL
END

GO
