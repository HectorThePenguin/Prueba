USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TratamientoGanado_ObtenerCostoTratamientoPorMovimiento]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TratamientoGanado_ObtenerCostoTratamientoPorMovimiento]
GO
/****** Object:  StoredProcedure [dbo].[TratamientoGanado_ObtenerCostoTratamientoPorMovimiento]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:		Roque.Solis
-- Fecha: 19/02/2014
-- Descripción:	Obtiene el costo de un tratamiento aplicado en un movimiento
-- EXEC TratamientoGanado_ObtenerCostoTratamientoPorMovimiento 4, 1, 1
-- =============================================
CREATE PROCEDURE [dbo].[TratamientoGanado_ObtenerCostoTratamientoPorMovimiento] @OrganizacionID INT
	,@Tratamiento INT
	,@AnimalMovimiento BIGINT
AS
BEGIN
	SELECT SUM(ALMD.Importe) [costo]
	FROM Animal A(NOLOCK)
	INNER JOIN AnimalMovimiento AM(NOLOCK) ON A.AnimalID = AM.AnimalID
	INNER JOIN AlmacenMovimiento ALM(NOLOCK) ON AM.AnimalMovimientoID = ALM.AnimalMovimientoID
	INNER JOIN AlmacenMovimientoDetalle ALMD(NOLOCK) ON ALM.AlmacenMovimientoID = ALMD.AlmacenMovimientoID
	WHERE AM.AnimalMovimientoID = @AnimalMovimiento
		AND ALMD.ProductoID IN (
			SELECT TP.ProductoID
			FROM TratamientoProducto TP
			WHERE TP.TratamientoID = @Tratamiento
			)
		AND AM.OrganizacionID = @OrganizacionID
END

GO
