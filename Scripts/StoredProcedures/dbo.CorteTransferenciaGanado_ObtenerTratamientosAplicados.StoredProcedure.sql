USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CorteTransferenciaGanado_ObtenerTratamientosAplicados]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CorteTransferenciaGanado_ObtenerTratamientosAplicados]
GO
/****** Object:  StoredProcedure [dbo].[CorteTransferenciaGanado_ObtenerTratamientosAplicados]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jesus Alvarez
-- Create date: 18-02-2014
-- Description:	Obtiene los movimientos por arete 
-- EXEC CorteTransferenciaGanado_ObtenerTratamientosAplicados '308741', 1, -1
-- =============================================
CREATE PROCEDURE [dbo].[CorteTransferenciaGanado_ObtenerTratamientosAplicados]
@Arete CHAR(15),
@OrganizacionID INT,
@TipoMovimientoID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT DISTINCT T.CodigoTratamiento, AMD.TratamientoID, AM.FechaMovimiento
				,  T.TipoTratamientoID
	FROM Animal A (NOLOCK)
	INNER JOIN AnimalMovimiento AM (NOLOCK) ON AM.AnimalID = A.AnimalID 
	INNER JOIN TipoMovimiento TM (NOLOCK) ON TM.TipoMovimientoID = AM.TipoMovimientoID
	INNER JOIN AlmacenMovimiento AMOV (NOLOCK) ON AMOV.AnimalMovimientoID = AM.AnimalMovimientoID
	INNER JOIN AlmacenMovimientoDetalle AMD (NOLOCK) ON AMD.AlmacenMovimientoID = AMOV.AlmacenMovimientoID
	INNER JOIN Tratamiento T (NOLOCK) ON T.TratamientoID = AMD.TratamientoID
	WHERE A.Arete = @Arete
	AND A.OrganizacionIDEntrada = @OrganizacionID
	AND AMD.TratamientoID IS NOT NULL
	AND TM.TipoMovimientoID = CASE WHEN @TipoMovimientoID=-1 THEN TM.TipoMovimientoID ELSE @TipoMovimientoID END
END

GO
