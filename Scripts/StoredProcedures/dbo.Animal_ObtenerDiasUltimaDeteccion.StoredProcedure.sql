USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ObtenerDiasUltimaDeteccion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Animal_ObtenerDiasUltimaDeteccion]
GO
/****** Object:  StoredProcedure [dbo].[Animal_ObtenerDiasUltimaDeteccion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:    Pedro Delgado
-- Create date: 17/09/2014
-- Description: Obtiene los dias despues de la ultima deteccion
-- Origen: APInterfaces
-- Animal_ObtenerDiasUltimaDeteccion 103461,'136865',7
-- =============================================
CREATE PROCEDURE [dbo].[Animal_ObtenerDiasUltimaDeteccion]
@AnimalID INT,
@Arete VARCHAR(15),
@TipoMovimiento INT
AS
BEGIN
	DECLARE @Dias INT

	SELECT @Dias = ISNULL(DATEDIFF(DAY,MIN(AM.FechaCreacion),GETDATE()),0)
	FROM Animal A(NOLOCK)
	LEFT JOIN Deteccion D ON (A.Arete = D.Arete)
	LEFT JOIN AnimalMovimiento AM(NOLOCK) ON (A.AnimalID = AM.AnimalID AND AM.FechaCreacion >= D.FechaDeteccion)
	WHERE A.AnimalID = @AnimalID AND A.Arete = @Arete AND AM.TipoMovimientoID = @TipoMovimiento
	GROUP BY A.AnimalID,A.Arete
	ORDER BY A.Arete
	
	SELECT @Dias = ISNULL(@Dias, 0)

	SELECT @Dias AS Dias
END

GO
