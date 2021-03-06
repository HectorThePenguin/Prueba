USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[SalidaPorMuerte_ObtenerMuertePorAreteRecepcion]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[SalidaPorMuerte_ObtenerMuertePorAreteRecepcion]
GO
/****** Object:  StoredProcedure [dbo].[SalidaPorMuerte_ObtenerMuertePorAreteRecepcion]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: C�sar Valdez
-- Create date: 12/05/2013
-- Description: Obtiene la informacion de un arete muerto de recepcion
-- Empresa: Apinterfaces
-- SalidaPorMuerte_ObtenerMuertePorAreteRecepcion 1,'123456'
-- =============================================
CREATE PROCEDURE [dbo].[SalidaPorMuerte_ObtenerMuertePorAreteRecepcion]
@OrganizacionId INT,
@Arete VARCHAR(16)
AS
BEGIN
	DECLARE @peso INT
	SET @peso = 
			  CAST(COALESCE((
						   SELECT (SUM(EN.PesoBruto) - SUM(EN.PesoTara)) / SUM(EN.CabezasRecibidas) AS peso
							 FROM muertes MU
							INNER JOIN Lote L ON L.LoteID = MU.LoteID
							INNER JOIN Corral CR ON L.CorralID = CR.CorralID
							INNER JOIN EntradaGanado EN ON EN.CorralID = CR.CorralID
							WHERE (MU.Arete = @Arete OR MU.AreteMetalico = @Arete)
							  AND CR.OrganizacionID = @OrganizacionId
					    ),0) AS INT)
	SELECT MU.MuerteId, MU.Arete, MU.AreteMetalico, ISNULL(MU.Observaciones, '') AS Observaciones, MU.LoteId, MU.OperadorDeteccion, MU.FechaDeteccion, MU.FotoDeteccion,
		   MU.OperadorRecoleccion, MU.FechaRecoleccion, MU.OperadorNecropsia, MU.FechaNecropsia, MU.EstatusID, MU.ProblemaID, MU.FechaCreacion,
		   LT.Lote, LT.OrganizacionId, CR.Codigo, CR.CorralId, MU.Activo,CAST( 0 AS BIGINT)AS "AnimalId", @peso AS Peso
	FROM muertes MU
	-- INNER JOIN Animal A ON (A.Arete = MU.Arete OR Mu.Arete = '') AND (A.AreteMetalico = MU.AreteMetalico OR MU.AreteMetalico = '' )
	INNER JOIN Lote LT ON MU.LoteId = LT.LoteID
	INNER JOIN Corral CR ON LT.CorralID = CR.CorralID
	WHERE (MU.Arete = @Arete OR MU.AreteMetalico = @Arete)
	  AND LT.OrganizacionID = @OrganizacionId
	  AND MU.Activo = 1
	  AND MU.EstatusID = 8
END

GO
