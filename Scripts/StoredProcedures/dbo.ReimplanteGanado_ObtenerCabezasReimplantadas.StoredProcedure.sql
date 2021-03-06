USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReimplanteGanado_ObtenerCabezasReimplantadas]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReimplanteGanado_ObtenerCabezasReimplantadas]
GO
/****** Object:  StoredProcedure [dbo].[ReimplanteGanado_ObtenerCabezasReimplantadas]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor: Andres Vejar
-- Fecha: 15/07/2014
-- Origen: APInterfaces
-- Descripción:	Obtener el numero de cabezas reimplantadas de un lote
-- EXEC ReimplanteGanado_ObtenerCabezasReimplantadas 1,23,6
-- =============================================
CREATE PROCEDURE [dbo].[ReimplanteGanado_ObtenerCabezasReimplantadas]
@OrganizacionID INT,
@Lote INT,
@TipoMovimiento INT
AS
BEGIN

	SELECT 
	LoteID
	,COUNT(1) AS Cabezas
	  FROM AnimalMovimiento (NOLOCK)
	 WHERE OrganizacionID = @OrganizacionID 
	   AND LoteIDOrigen = @Lote 
	   AND TipoMovimientoID = @TipoMovimiento 
	   AND Activo = 1
	   AND CONVERT(CHAR(8), FechaMovimiento, 112) = CONVERT(CHAR(8), GETDATE(), 112)
	   group by LoteID
	
END
GO
