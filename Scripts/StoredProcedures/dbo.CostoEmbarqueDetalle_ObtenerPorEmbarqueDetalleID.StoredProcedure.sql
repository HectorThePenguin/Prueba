USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[CostoEmbarqueDetalle_ObtenerPorEmbarqueDetalleID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
DROP PROCEDURE [dbo].[CostoEmbarqueDetalle_ObtenerPorEmbarqueDetalleID]
GO
/****** Object:  StoredProcedure [dbo].[CostoEmbarqueDetalle_ObtenerPorEmbarqueDetalleID]    Script Date: 15/10/2015 09:31:44 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author     : Jorge Luis Velazquez Araujo
-- Create date: 2013/11/19
-- Description: Sp para obtener los costos de un EmbarqueDetalle
-- CostoEmbarqueDetalle_ObtenerPorEmbarqueDetalleID 7
--=============================================
CREATE PROCEDURE [dbo].[CostoEmbarqueDetalle_ObtenerPorEmbarqueDetalleID] 
@EmbarqueDetalleID INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT CostoEmbarqueDetalleID
		,EmbarqueDetalleID
		,co.CostoID
		,co.Descripcion [Costo]
		,co.ClaveContable
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
	LEFT JOIN Retencion re on co.RetencionID = re.RetencionID
	WHERE ced.EmbarqueDetalleID = @EmbarqueDetalleID
	SET NOCOUNT OFF;
END

GO
