USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Costo_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Costo_ObtenerPorDescripcion]
GO
/****** Object:  StoredProcedure [dbo].[Costo_ObtenerPorDescripcion]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Gilberto Julián Carranza Castro
-- Create date: 16/01/2014 12:00:00 a.m.
-- Description: 
-- SpName     : Costo_ObtenerPorDescripcion
--======================================================
CREATE PROCEDURE [dbo].[Costo_ObtenerPorDescripcion]
@Descripcion varchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		C.CostoID,
		C.ClaveContable,
		C.Descripcion,
		C.TipoCostoID,
		C.TipoProrrateoID,
		ISNULL(C.RetencionID, 0) AS RetencionID,
		C.AbonoA,
		C.Activo,
		TC.Descripcion AS TipoCosto,
		TP.Descripcion AS TipoProrrateo,
		ISNULL(R.Descripcion,'')  AS Retencion
	FROM Costo C
	INNER JOIN TipoCosto TC
		ON (C.TipoCostoID = TC.TipoCostoID) 
	INNER JOIN TipoProrrateo TP
		ON (C.TipoProrrateoID = TP.TipoProrrateoID)
	LEFT OUTER JOIN Retencion R
		ON (C.RetencionID = R.RetencionID)
	WHERE C.Descripcion = @Descripcion
	SET NOCOUNT OFF;
END

GO
