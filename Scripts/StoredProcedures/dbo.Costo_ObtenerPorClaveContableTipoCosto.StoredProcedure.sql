USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Costo_ObtenerPorClaveContableTipoCosto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[Costo_ObtenerPorClaveContableTipoCosto]
GO
/****** Object:  StoredProcedure [dbo].[Costo_ObtenerPorClaveContableTipoCosto]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Jorge Luis Velazquez
-- Create date: 14/12/2013
-- Description:	Obtener un Costo por Clave Contable y Tipo de Costo
/* Costo_ObtenerPorClaveContableTipoCosto '001', '<ROOT>
<TiposCosto>
<TipoCostoID>3</TipoCostoID>
</TiposCosto>
<TiposCosto>
<TipoCostoID>1</TipoCostoID>
</TiposCosto></ROOT>' */
--=============================================
CREATE PROCEDURE [dbo].[Costo_ObtenerPorClaveContableTipoCosto] 
	@ClaveContable CHAR(3)
	,@XmlTiposCosto XML
AS
BEGIN
	SET NOCOUNT ON;
	SET @ClaveContable  = dbo.RellenaCeros(@ClaveContable, 3)
	DECLARE @TiposCosto AS TABLE ([TipoCostoID] INT)
	INSERT @TiposCosto ([TipoCostoID])
	SELECT [TipoCostoID] = t.item.value('./TipoCostoID[1]', 'INT')
	FROM @XmlTiposCosto.nodes('ROOT/TiposCosto') AS T(item)
	SELECT C.CostoID
		,C.ClaveContable
		,C.Descripcion AS [Costo]
		,TC.TipoCostoID
		,TC.Descripcion [TipoCosto]
		,TP.TipoProrrateoID
		,TP.Descripcion [TipoProrrateo]
		,C.Activo
		,C.FechaCreacion
		,C.UsuarioCreacionID
		,C.FechaModificacion
		,C.UsuarioModificacionID
		,R.RetencionID
	FROM Costo C
	INNER JOIN TipoCosto TC ON C.TipoCostoID = TC.TipoCostoID
	INNER JOIN TipoProrrateo TP ON C.TipoProrrateoID = TP.TipoProrrateoID
	LEFT JOIN Retencion R ON (C.RetencionID = R.RetencionID)
	INNER JOIN @TiposCosto tc1 ON TC.TipoCostoID = tc1.TipoCostoID
	WHERE C.ClaveContable = @ClaveContable
	SET NOCOUNT OFF;
END

GO
