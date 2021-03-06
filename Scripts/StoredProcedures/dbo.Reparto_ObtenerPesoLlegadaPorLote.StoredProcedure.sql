USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerPesoLlegadaPorLote]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_ObtenerPesoLlegadaPorLote]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerPesoLlegadaPorLote]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Roque.Solis
-- Create date: 2014/03/25
-- Description: SP para el consumo total del dia
-- Origen     : APInterfaces
-- EXEC Reparto_ObtenerPesoLlegadaPorLote 1437, 4
-- =============================================
CREATE PROCEDURE [dbo].[Reparto_ObtenerPesoLlegadaPorLote]
@LoteID INT,
@OrganizacionID INT
AS
BEGIN
	SELECT COUNT(A.AnimalID) Total, SUM(A.PesoLlegada) PesoLlegada, SUM(AM.Peso) Peso
	  FROM Animal A (NOLOCK)
	 INNER JOIN AnimalMovimiento AM (NOLOCK) ON A.AnimalID=AM.AnimalID 
	 INNER JOIN Lote L (NOLOCK) ON AM.LoteId = L.LoteId
	 WHERE L.LoteId = @LoteID
	   AND L.OrganizacionID = @OrganizacionID
	   AND A.Activo = 1
	   AND AM.Activo = 1
 	   AND L.Activo = 1
SET NOCOUNT OFF;
END

GO
