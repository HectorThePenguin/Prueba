USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ReportarAvanceReparto]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_ReportarAvanceReparto]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ReportarAvanceReparto]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Roque.Solis
-- Create date: 2014-03-28
-- Origen: APInterfaces
-- Description:	Reporta el avance del reparto
-- EXEC Reparto_ReportarAvanceReparto 5,"seccion 1",1,1,1,1,1,1
--=============================================
CREATE PROCEDURE [dbo].[Reparto_ReportarAvanceReparto]
	@UsuarioID INT,
	@Seccion VARCHAR(50),
	@TotalCorrales INT,
	@TotalCorralesSeccion INT,
	@TotalCorralesProcesados INT,
	@TotalCorralesProcesadosSeccion INT,
	@PorcentajeSeccion INT,
	@PorcentajeTotal INT,
	@EstatusError INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @IdentityID INT;
	SET @IdentityID = (SELECT TOP 1 UsuarioID
					     FROM RepartoAvance
					    WHERE UsuarioID = @UsuarioID
					   )
	IF (  @IdentityID > 0 )
		BEGIN
			UPDATE RepartoAvance SET
				Seccion = @Seccion,
				TotalCorrales = @TotalCorrales,
				TotalCorralesSeccion = @TotalCorralesSeccion,
				TotalCorralesProcesados = @TotalCorralesProcesados,
				TotalCorralesProcesadosSeccion = @TotalCorralesProcesadosSeccion,
				PorcentajeSeccion = @PorcentajeSeccion,
				PorcentajeTotal = @PorcentajeTotal,
				EstatusError = @EstatusError
			WHERE UsuarioID = @UsuarioID
		END
	ELSE
		BEGIN
			INSERT INTO RepartoAvance (
				UsuarioID,
				Seccion,
				TotalCorrales,
				TotalCorralesSeccion,
				TotalCorralesProcesados,
				TotalCorralesProcesadosSeccion,
				PorcentajeSeccion,
				PorcentajeTotal)
			VALUES(@UsuarioID,
					@Seccion,
					@TotalCorrales,
					@TotalCorralesSeccion,
					@TotalCorralesProcesados,
					@TotalCorralesProcesadosSeccion,
					@PorcentajeSeccion,
					@PorcentajeTotal)
		END
	SET NOCOUNT OFF;
END

GO
