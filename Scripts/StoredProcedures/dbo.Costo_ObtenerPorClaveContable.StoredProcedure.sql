USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Costo_ObtenerPorClaveContable]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Costo_ObtenerPorClaveContable]
GO
/****** Object:  StoredProcedure [dbo].[Costo_ObtenerPorClaveContable]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 11/12/2013
-- Description: Sp para consultar un Costo por ID
-- Costo_ObtenerPorClaveContable
--=============================================
CREATE PROCEDURE [dbo].[Costo_ObtenerPorClaveContable]
@ClaveContable CHAR(3)
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
		, C.RetencionID
	FROM Costo C
	INNER JOIN TipoCosto TC ON C.TipoCostoID = TC.TipoCostoID
	INNER JOIN TipoProrrateo TP ON C.TipoProrrateoID = TP.TipoProrrateoID
	WHERE C.ClaveContable = @ClaveContable
	SET NOCOUNT OFF;
END

GO
