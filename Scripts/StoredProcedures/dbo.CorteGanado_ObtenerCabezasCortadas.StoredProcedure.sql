USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorteGanado_ObtenerCabezasCortadas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorteGanado_ObtenerCabezasCortadas]
GO
/****** Object:  StoredProcedure [dbo].[CorteGanado_ObtenerCabezasCortadas]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Cesar.valdez
-- Fecha: 2013-12-19
-- Origen: APInterfaces
-- Descripción:	Obtiene el numero de cabezas que estan cortadas para una partida
-- EXEC CorteGanado_ObtenerCabezasCortadas 123412,1,1,1
-- =============================================
CREATE PROCEDURE [dbo].[CorteGanado_ObtenerCabezasCortadas]
@NoPartida XML,
@OrganizacionID INT,
@TipoCorral INT,
@TipoMovimiento INT
AS
BEGIN

	DECLARE @PartidasCorte AS TABLE ([NoPartida] INT)

	INSERT @PartidasCorte ([NoPartida])
	SELECT [NoPartida] = t.item.value('./NoPartida[1]', 'INT')
	FROM @NoPartida.nodes('ROOT/PartidasCorte') AS T(item)

	
	SELECT COUNT(1) TotalCabezasCortadas
	  FROM Animal A(NOLOCK)
	 INNER JOIN AnimalMovimiento AM(NOLOCK) ON AM.AnimaLID = A.AnimaLID
	 INNER JOIN Corral C ON C.CorralID = AM.CorralID 
	 WHERE A.OrganizacionIDEntrada =  @OrganizacionID 
	   AND A.FolioEntrada IN( SELECT NoPartida FROM @PartidasCorte)
	   AND AM.TipoMovimientoID =  @TipoMovimiento
	   -- AND A.Activo = 1
	   --A ND AM.Activo = 1
	
END

GO
