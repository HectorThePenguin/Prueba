USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerDetallePorReparto]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_ObtenerDetallePorReparto]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerDetallePorReparto]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================
-- Author:		Roque.Solis
-- Create date: 2014-02-28
-- Origen: APInterfaces
-- Description:	Obtiene un el detalle de un reparto
-- EXEC Reparto_ObtenerDetallePorReparto 1, 1
--=============================================
CREATE PROCEDURE [dbo].[Reparto_ObtenerDetallePorReparto]
	@RepartoID BIGINT,
	@Activo INT
AS
BEGIN
	SET NOCOUNT ON;
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
		RD.Ajuste,
		F.TipoFormulaID
	FROM RepartoDetalle RD (NOLOCK)
	LEFT JOIN Formula F (NOLOCK) ON RD.FormulaIDServida=F.FormulaID
	WHERE RD.RepartoID = @RepartoID
	AND RD.Activo = @Activo
	SET NOCOUNT OFF;
END

GO
