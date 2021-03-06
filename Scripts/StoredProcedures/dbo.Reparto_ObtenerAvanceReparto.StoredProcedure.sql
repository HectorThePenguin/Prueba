USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerAvanceReparto]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_ObtenerAvanceReparto]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerAvanceReparto]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Roque.Solis
-- Create date: 2014-03-27
-- Origen: APInterfaces
-- Description:	Obtiene el avance del reparto
-- EXEC Reparto_ObtenerAvanceReparto 5
--=============================================
CREATE PROCEDURE [dbo].[Reparto_ObtenerAvanceReparto]
	@UsuarioID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		UsuarioID,
        Seccion,
        TotalCorrales,
        TotalCorralesSeccion,
        TotalCorralesProcesados,
        TotalCorralesProcesadosSeccion,
        PorcentajeSeccion,
        PorcentajeTotal
	FROM RepartoAvance
	WHERE UsuarioID = @UsuarioID
	SET NOCOUNT OFF;
END

GO
