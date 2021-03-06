USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[RepartoDetalle_ObtenerPorOrganizacionID]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[RepartoDetalle_ObtenerPorOrganizacionID]
GO
/****** Object:  StoredProcedure [dbo].[RepartoDetalle_ObtenerPorOrganizacionID]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--======================================================
-- Author     : Pedro Delgado
-- Create date: 05/04/2014
-- Description: Obtiene el detalle de movimientos por almacenid
-- SpName     : EXEC RepartoDetalle_ObtenerPorOrganizacionID 5
--001 Jorge Luis Velazquez Araujo 24/08/2015 **Se agrega el parametro de fecha
--======================================================
CREATE PROCEDURE [dbo].[RepartoDetalle_ObtenerPorOrganizacionID]
@OrganizacionID INT
,@Fecha DATE --001
AS
BEGIN
	SELECT 
		RD.RepartoDetalleID,
		RD.RepartoID,
		RD.TipoServicioID,
		RD.FormulaIDProgramada,
		RD.FormulaIDServida,
		RD.CantidadProgramada,
		RD.CantidadServida,
		RD.HoraReparto,
		RD.CostoPromedio,
		RD.Importe,
		RD.Servido,
		RD.Cabezas,
		RD.EstadoComederoID,
		RD.CamionRepartoID,
		RD.Observaciones,
		RD.Activo,
		RD.FechaCreacion,
		RD.UsuarioCreacionID,
		RD.FechaModificacion,
		RD.UsuarioModificacionID,
		RD.Ajuste,
		RD.Prorrateo,
		RD.AlmacenMovimientoID,
		R.Fecha
	 FROM RepartoDetalle (NOLOCK) RD
	INNER JOIN Reparto (NOLOCK) R ON (RD.RepartoID = R.RepartoID)
	WHERE R.OrganizacionID = @OrganizacionID 
	  AND R.Activo = 1
	  AND RD.Importe >= 0 
	  AND Servido = 1 
	  AND Prorrateo = 0 
	  AND rd.CostoPromedio = 0 
	  AND rd.AlmacenMovimientoID is null
	  AND CAST(R.Fecha AS DATE) = @Fecha --001
END

GO
