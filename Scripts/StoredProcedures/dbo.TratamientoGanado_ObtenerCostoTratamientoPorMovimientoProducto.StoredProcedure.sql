USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TratamientoGanado_ObtenerCostoTratamientoPorMovimientoProducto]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TratamientoGanado_ObtenerCostoTratamientoPorMovimientoProducto]
GO
/****** Object:  StoredProcedure [dbo].[TratamientoGanado_ObtenerCostoTratamientoPorMovimientoProducto]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:		Roque.Solis
-- Fecha: 19/02/2014
-- Descripción:	Obtiene el costo de un tratamiento aplicado en un movimiento
-- EXEC TratamientoGanado_ObtenerCostoTratamientoPorMovimientoProducto 4, 1, 1
-- =============================================
CREATE PROCEDURE [dbo].[TratamientoGanado_ObtenerCostoTratamientoPorMovimientoProducto] @OrganizacionID INT
	,@TratamientoID INT
	,@ProductoID INT
	,@AnimalMovimiento BIGINT
AS
BEGIN
	SELECT COALESCE(SUM(ALMD.Importe), 0) [costo]
	FROM Animal A(NOLOCK)
	INNER JOIN AnimalMovimiento AM(NOLOCK) ON A.AnimalID = AM.AnimalID
	INNER JOIN AlmacenMovimiento ALM(NOLOCK) ON AM.AnimalMovimientoID = ALM.AnimalMovimientoID
	INNER JOIN AlmacenMovimientoDetalle ALMD(NOLOCK) ON ALM.AlmacenMovimientoID = ALMD.AlmacenMovimientoID
	WHERE AM.AnimalMovimientoID = @AnimalMovimiento
		AND ALMD.ProductoID = @ProductoID -- IN (SELECT TP.ProductoID FROM TratamientoProducto TP WHERE TP.TratamientoID= @TratamientoID)
		AND ALMD.TratamientoID = @TratamientoID
		AND AM.OrganizacionID = @OrganizacionID
END

GO
