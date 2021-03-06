USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorteGanado_ObtenerTotalGanadoSobranteCortado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorteGanado_ObtenerTotalGanadoSobranteCortado]
GO
/****** Object:  StoredProcedure [dbo].[CorteGanado_ObtenerTotalGanadoSobranteCortado]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		César Valdez
-- Create date: 2014/11/28
-- Description: SP para obtener obtener el total de animales sobrantes cor
-- Origen     : APInterfaces
-- EXEC  [dbo].[CorteGanado_ObtenerTotalGanadoSobranteCortado] 
--'<ROOT><PartidasCorte><NoPartida>1109 </NoPartida></PartidasCorte><PartidasCorte><NoPartida> 1145</NoPartida></PartidasCorte></ROOT>', 1
-- =============================================
CREATE PROCEDURE [dbo].[CorteGanado_ObtenerTotalGanadoSobranteCortado]
  @NoPartida XML,
  @OrganizacionID INT
AS
BEGIN

	DECLARE @PartidasCorte AS TABLE ([NoPartida] INT)

	INSERT @PartidasCorte ([NoPartida])
	SELECT [NoPartida] = t.item.value('./NoPartida[1]', 'INT')
	FROM @NoPartida.nodes('ROOT/PartidasCorte') AS T(item)

	/* Se Obtienen los Folios de entrada con sus sobrantes y sus sobrantes cortados */
	SELECT EG.EntradaGanadoID,
           EG.FolioEntrada, 
		   CabezasRecibidas,
		   CabezasOrigen,
		  (CabezasRecibidas-CabezasOrigen) AS [CabezasSobrantes], 
		  (SELECT COUNT(*) 
             FROM EntradaGanadoSobrante EGS 
            WHERE EGS.EntradaGanadoID = EG.EntradaGanadoID) AS [CabezasSobrantesCortadas],
		  (SELECT COUNT(*) 
             FROM Animal A(NOLOCK)
			INNER JOIN AnimalMovimiento AM(NOLOCK) On A.AnimalID = AM.AnimalID
            WHERE A.FolioEntrada = EG.FolioEntrada
			  AND AM.Activo = 1) AS [CabezasCortadas],
		   OO.OrganizacionID, 
		   OO.TipoOrganizacionID, 
		   OO.Descripcion
      FROM EntradaGanado EG 
	 INNER JOIN Organizacion OO ON OO.OrganizacionID = EG.OrganizacionOrigenID
     INNER JOIN TipoOrganizacion Tipo ON Tipo.TipoOrganizacionID = OO.TipoOrganizacionID
     WHERE EG.OrganizacionID = @OrganizacionID
	   AND EG.FolioEntrada IN (SELECT NoPartida FROM @PartidasCorte)
	   AND EG.Activo = 1
	 GROUP BY EG.EntradaGanadoID, 
			  EG.FolioEntrada, 
			  EG.CabezasRecibidas, 
			  EG.CabezasOrigen, 
			  OO.OrganizacionID, 
			  OO.TipoOrganizacionID, 
			  OO.Descripcion;
			  
END

GO
