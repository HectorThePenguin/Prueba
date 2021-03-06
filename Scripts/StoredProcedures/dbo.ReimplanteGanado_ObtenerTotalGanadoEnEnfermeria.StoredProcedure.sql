USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReimplanteGanado_ObtenerTotalGanadoEnEnfermeria]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReimplanteGanado_ObtenerTotalGanadoEnEnfermeria]
GO
/****** Object:  StoredProcedure [dbo].[ReimplanteGanado_ObtenerTotalGanadoEnEnfermeria]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Andres Vejar
-- Create date: 2014/07/15
-- Description: SP para obtener obtener el total de animales que se encuentran en corral de enfermeria para reimplante por lote
-- Origen     : APInterfaces
-- EXEC  [dbo].[ReimplanteGanado_ObtenerTotalGanadoEnEnfermeria] 1, 2, 3
-- =============================================
CREATE PROCEDURE [dbo].[ReimplanteGanado_ObtenerTotalGanadoEnEnfermeria]
  @OrganizacionID INT,
  @Lote INT,
  @TipoMovimiento INT
AS
BEGIN
	SELECT COUNT(1) 
	  FROM AnimalMovimiento (NOLOCK)
	 WHERE OrganizacionID = @OrganizacionID 
	  AND LoteIDOrigen = @Lote 
	  AND TipoMovimientoID = @TipoMovimiento 
	  AND Activo = 1
END

GO
