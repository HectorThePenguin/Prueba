USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[ReimplanteGanado_ObtenerCabezasMuertas]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[ReimplanteGanado_ObtenerCabezasMuertas]
GO
/****** Object:  StoredProcedure [dbo].[ReimplanteGanado_ObtenerCabezasMuertas]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor: Andres Vejar
-- Fecha: 15/07/2014
-- Origen: APInterfaces
-- Descripción:	Obtener el numero de cabezas muertas de un lote
-- EXEC ReimplanteGanado_ObtenerCabezasMuertas 1,23,6
-- =============================================
CREATE PROCEDURE [dbo].[ReimplanteGanado_ObtenerCabezasMuertas] @OrganizacionID INT
	,@Lote INT
	,@TipoMovimiento INT
AS
BEGIN
	SELECT count(1)
	FROM AnimalMovimiento(NOLOCK)
	WHERE OrganizacionID = @OrganizacionID
		AND LoteID = @Lote
		AND TipoMovimientoID = @TipoMovimiento
		AND Activo = 1
END

GO
