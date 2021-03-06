USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Costo_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Costo_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[Costo_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 2013/11/12
-- Description: Sp para consultar un Costo por ID
-- Costo_ObtenerPorID 1
--=============================================
CREATE PROCEDURE [dbo].[Costo_ObtenerPorID] @CostoID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT C.CostoID
		,C.ClaveContable
		,C.Descripcion as [Costo]
		,TC.TipoCostoID
		,TC.Descripcion as [TipoCosto]
		,TP.TipoProrrateoID
		,TP.Descripcion as [TipoProrrateo]
		,C.Activo
		,C.FechaCreacion
		,C.UsuarioCreacionID
		,C.FechaModificacion
		,C.UsuarioModificacionID
		,C.RetencionID
		,C.AbonoA
	FROM Costo C
	INNER JOIN TipoCosto TC ON C.TipoCostoID = TC.TipoCostoID
	INNER JOIN TipoProrrateo TP ON C.TipoProrrateoID = TP.TipoProrrateoID
	WHERE CostoID = @CostoID
	SET NOCOUNT OFF;
END

GO
