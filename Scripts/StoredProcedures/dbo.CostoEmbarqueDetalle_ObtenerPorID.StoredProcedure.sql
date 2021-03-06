USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CostoEmbarqueDetalle_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CostoEmbarqueDetalle_ObtenerPorID]
GO
/****** Object:  StoredProcedure [dbo].[CostoEmbarqueDetalle_ObtenerPorID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 2013/11/12
-- Description: Sp para obtener un Costo Embarque Detalle por ID
-- 
--=============================================
CREATE PROCEDURE [dbo].[CostoEmbarqueDetalle_ObtenerPorID] @CostoEmbarqueDetalleID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT CostoEmbarqueDetalleID
		,EmbarqueDetalleID
		,co.CostoID
		,co.ClaveContable
		,co.Descripcion [Costo]
		,re.RetencionID
		,re.Descripcion [Retencion]
		,Importe
		,ced.Activo
		,Orden
		,ced.FechaCreacion
		,ced.UsuarioCreacionID
		,ced.FechaModificacion
		,ced.UsuarioModificacionID
	FROM CostoEmbarqueDetalle ced
	INNER JOIN Costo co on ced.CostoID = co.CostoID
	LEFT JOIN Retencion re ON co.RetencionID = re.RetencionID
	WHERE CostoEmbarqueDetalleID = @CostoEmbarqueDetalleID
	SET NOCOUNT OFF;
END

GO
