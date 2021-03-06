USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ContratoHumedad_ObtenerHumedadAlaFechaPorContratoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ContratoHumedad_ObtenerHumedadAlaFechaPorContratoID]
GO
/****** Object:  StoredProcedure [dbo].[ContratoHumedad_ObtenerHumedadAlaFechaPorContratoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Ramses Santos
-- Create date: 17/12/2014
-- Description: Obtiene el la humedad que sea menor o igual a la fecha.
-- SpName     : ContratoHumedad_ObtenerHumedadAlaFechaPorContratoID 1110
--======================================================
CREATE PROCEDURE [dbo].[ContratoHumedad_ObtenerHumedadAlaFechaPorContratoID]
@ContratoID INT,
@Activo INT = 1
AS
BEGIN
	SELECT TOP 1
		ContratoHumedadID,
		ContratoID,
		FechaInicio,
		PorcentajeHumedad,
		Activo,
		FechaCreacion,
		UsuarioCreacionID,
		FechaModificacion,
		UsuarioModificacionID
	FROM ContratoHumedad
	WHERE ContratoID = @ContratoID AND CAST(FechaInicio AS DATE) <= CAST(GETDATE() AS DATE)
	AND Activo = @Activo
	order by FechaInicio desc
END
GO
