USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ActualizarLoteEnLote]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Lote_ActualizarLoteEnLote]
GO
/****** Object:  StoredProcedure [dbo].[Lote_ActualizarLoteEnLote]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: cesar.valdez
-- Fecha: 2013-12-29
-- Descripción:	Actualizar el lote de lote
-- EXEC Lote_ActualizarLoteEnLote "10","2014-04-16","2014-04-16",1
-- 001 -- Gilberto Carranza -- Se agrega validacion para no poner fecha de cierre cuando el corral sea de enfermeria
-- =============================================
CREATE PROCEDURE [dbo].[Lote_ActualizarLoteEnLote]
	@Lote VARCHAR(20),
	@FechaDisponibilidad DATETIME,
	@FechaCierre DATETIME,
	@LoteID INT
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @FechaDisponibilidadVal CHAR(8);
	DECLARE @FechaCierreVal CHAR(8);
	
	SET @FechaDisponibilidadVal = CONVERT(CHAR(8), @FechaDisponibilidad, 112);
	IF (@FechaDisponibilidadVal = '18000101') 
		BEGIN
			SET @FechaDisponibilidadVal = null;
		END
	ELSE
		BEGIN
			SET @FechaDisponibilidadVal = @FechaDisponibilidad;
		END
		
	SET @FechaCierreVal = CONVERT(CHAR(8), @FechaCierre, 112);
	IF (@FechaCierreVal = '18000101')
		BEGIN
			SET @FechaCierreVal = null;
		END
	ELSE
		BEGIN
			SET @FechaCierreVal = @FechaCierre;
		END

	DECLARE @TipoCorralEnfermeria INT
	SET @TipoCorralEnfermeria = 3
	
	UPDATE Lote
	SET Lote = @Lote,
		FechaDisponibilidad = CASE WHEN @FechaDisponibilidadVal IS NULL THEN @FechaDisponibilidadVal ELSE @FechaDisponibilidad END ,
		FechaCierre = CASE WHEN @FechaCierreVal = NULL OR TipoCorralID = @TipoCorralEnfermeria THEN NULL ELSE @FechaCierre END , --001
		FechaModificacion = GETDATE()
	WHERE LoteID = @LoteID

	SET NOCOUNT OFF

END

GO
