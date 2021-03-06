USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TratamientoGanado_ObtenerTratamientoAplicadoMovimiento]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TratamientoGanado_ObtenerTratamientoAplicadoMovimiento]
GO
/****** Object:  StoredProcedure [dbo].[TratamientoGanado_ObtenerTratamientoAplicadoMovimiento]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Cesar.Valdez
-- Fecha: 05/11/2014
-- Descripci�n:	Obtiene el tratamiento aplicado a un movimiento
-- EXEC TratamientoGanado_ObtenerTratamientoAplicadoMovimiento 1,16,259398
-- =============================================
CREATE PROCEDURE [dbo].[TratamientoGanado_ObtenerTratamientoAplicadoMovimiento]
	@OrganizacionID INT, 
	@TratamientoID INT,
	@AnimalMovimientoID BIGINT
AS
BEGIN	
	SELECT T.CodigoTratamiento, T.TratamientoID, P.Descripcion, COALESCE(ALMD.Importe,0) [Costo]
	  FROM AnimalMovimiento AM (NOLOCK)
	 INNER JOIN AlmacenMovimiento ALM (NOLOCK) ON AM.AnimalMovimientoID= ALM.AnimalMovimientoID
	 INNER JOIN AlmacenMovimientoDetalle ALMD (NOLOCK) ON ALM.AlmacenMovimientoID = ALMD.AlmacenMovimientoID
	 INNER JOIN Tratamiento T ON T.TratamientoID = ALMD.TratamientoID
	 INNER JOIN Producto P ON P.ProductoID = ALMD.ProductoID
	 WHERE AM.AnimalMovimientoID = @AnimalMovimientoID
	   AND ALMD.TratamientoID = @TratamientoID 
	   AND AM.OrganizacionID = @OrganizacionID
	 ORDER BY T.CodigoTratamiento, P.Descripcion;
END

GO
