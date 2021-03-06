USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerDiasEngordaPorLote]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Corral_ObtenerDiasEngordaPorLote]
GO
/****** Object:  StoredProcedure [dbo].[Corral_ObtenerDiasEngordaPorLote]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Cesar Valdez
-- Create date: 2014-09-07
-- Origen: APInterfaces
-- Description:	Obtiene Los dias envorda promedio de un Lote
-- EXEC Corral_ObtenerDiasEngordaPorLote 1
--=============================================
CREATE PROCEDURE [dbo].[Corral_ObtenerDiasEngordaPorLote]
	@LoteID INT
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT COALESCE((SUM(DATEDIFF(DAY, EG.FechaEntrada, GETDATE()))/COUNT(1)),0) AS DiasEngorda
	  FROM Lote L 
	 INNER JOIN AnimalMovimiento AM(NOLOCK) on L.LoteID = AM.LoteID
	 INNER JOIN Animal A(NOLOCK) ON A.AnimalID = AM.AnimalID
	 INNER JOIN EntradaGanado EG ON A.FolioEntrada = EG.FolioEntrada
	 WHERE AM.Activo = 1
	   AND L.LoteID = @LoteID

	SET NOCOUNT OFF;
END

GO
