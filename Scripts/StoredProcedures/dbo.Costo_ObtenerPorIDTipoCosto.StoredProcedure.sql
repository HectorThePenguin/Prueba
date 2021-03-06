USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Costo_ObtenerPorIDTipoCosto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Costo_ObtenerPorIDTipoCosto]
GO
/****** Object:  StoredProcedure [dbo].[Costo_ObtenerPorIDTipoCosto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 2013/11/12
-- Description: Sp para consultar un Costo por ID
-- 
--=============================================
CREATE PROCEDURE [dbo].[Costo_ObtenerPorIDTipoCosto]
@CostoID int
,@TipoCostoID INT
,@ClaveContable char(3)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
		C.CostoID,
		C.ClaveContable,
		C.Descripcion AS [Costo],
		TC.TipoCostoID,
		TC.Descripcion [TipoCosto],
		TP.TipoProrrateoID,
		TP.Descripcion [TipoProrrateo],
		C.Activo,
		C.FechaCreacion,
		C.UsuarioCreacionID,
		C.FechaModificacion,
		C.UsuarioModificacionID	
		, R.RetencionID
	FROM Costo C
	INNER JOIN TipoCosto TC ON C.TipoCostoID = TC.TipoCostoID
	INNER JOIN TipoProrrateo TP ON C.TipoProrrateoID = TP.TipoProrrateoID
	LEFT JOIN Retencion R
		ON (C.RetencionID = R.RetencionID)
	WHERE @CostoID IN (C.ClaveContable, 0)
	AND @ClaveContable IN (C.ClaveContable, '')	
	and C.TipoCostoID = @TipoCostoID
	SET NOCOUNT OFF;
END

GO
