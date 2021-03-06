USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionFormula_GuardarConfiguracionFormula]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ConfiguracionFormula_GuardarConfiguracionFormula]
GO
/****** Object:  StoredProcedure [dbo].[ConfiguracionFormula_GuardarConfiguracionFormula]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Cesar.Valdez
-- Create date: 2014/03/22
-- Description: SP para Insertar la configuracion de las formulas para cada organizacion
-- Origen     : APInterfaces
-- EXEC ConfiguracionFormula_GuardarConfiguracionFormula 
-- =============================================
CREATE PROCEDURE [dbo].[ConfiguracionFormula_GuardarConfiguracionFormula]
	@OrganizacionID INT,
	@FormulaID INT,
	@PesoInicioMinimo INT,
	@PesoInicioMaximo INT,
	@TipoGanado VARCHAR(100),
	@PesoSalida INT,
	@FormulaSiguienteID INT,
	@DiasEstanciaMinimo INT,
	@DiasEstanciaMaximo INT,
	@DiasTransicionMinimo INT,
	@DiasTransicionMaximo INT,
	@Disponibilidad INT,
    @Activo BIT,
	@UsuarioCreacionID INT
AS
BEGIN
	INSERT INTO ConfiguracionFormula(
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
				UsuarioCreacionID) 
		VALUES (
				@OrganizacionID, 
				@FormulaID, 
				@PesoInicioMinimo, 
				@PesoInicioMaximo, 
				@TipoGanado, 
				@PesoSalida , 
				@FormulaSiguienteID, 
				@DiasEstanciaMinimo, 
				@DiasEstanciaMaximo, 
				@DiasTransicionMinimo, 
				@DiasTransicionMaximo, 
				@Disponibilidad, 
				@Activo, 
				GETDATE(), 
				@UsuarioCreacionID)
END

GO
