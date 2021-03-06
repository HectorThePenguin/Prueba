USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[TipoCambio_ObtenerPorDescripcionFecha]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[TipoCambio_ObtenerPorDescripcionFecha]
GO
/****** Object:  StoredProcedure [dbo].[TipoCambio_ObtenerPorDescripcionFecha]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 16/09/2014 12:00:00 a.m.
-- Description: 
-- SpName     : TipoCambio_ObtenerPorDescripcionFecha
--======================================================
CREATE PROCEDURE [dbo].[TipoCambio_ObtenerPorDescripcionFecha]
@Descripcion varchar(50),
@Fecha DATE
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		tc.TipoCambioID,
		tc.Descripcion,
		mo.MonedaID,
		mo.Descripcion as Moneda,
		tc.Cambio,
		tc.Fecha,
		tc.Activo
	FROM TipoCambio tc
	INNER JOIN Moneda mo on tc.MonedaID = mo.MonedaID
	WHERE tc.Descripcion = @Descripcion
	AND CAST(tc.Fecha AS DATE) = @Fecha
	SET NOCOUNT OFF;
END

GO
