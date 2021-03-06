USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Costo_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Costo_ObtenerTodos]
GO
/****** Object:  StoredProcedure [dbo].[Costo_ObtenerTodos]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 2013/11/12
-- Description: Sp para obtener todos los Costos
-- Costo_ObtenerTodos 1
--=============================================
CREATE PROCEDURE [dbo].[Costo_ObtenerTodos]
@Activo BIT = NULL	
AS
BEGIN
	SET NOCOUNT ON;
	SELECT C.CostoID,
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
	WHERE (C.Activo = @Activo OR @Activo IS NULL)
	SET NOCOUNT OFF;
END

GO
