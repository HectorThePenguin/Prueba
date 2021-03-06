USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ContratoHumedad_ObtenerPorContratoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[ContratoHumedad_ObtenerPorContratoID]
GO
/****** Object:  StoredProcedure [dbo].[ContratoHumedad_ObtenerPorContratoID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jesus Alvarez
-- Create date: 22/08/2014
-- Description: Obtiene una lista por ContratoID
-- SpName     : ContratoHumedad_ObtenerPorContratoID 1
--======================================================
CREATE PROCEDURE [dbo].[ContratoHumedad_ObtenerPorContratoID]
@ContratoID INT,
@Activo INT
AS
BEGIN
	SELECT 
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
	WHERE ContratoID = @ContratoID
	AND Activo = @Activo
END

GO
