USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerDetalleDia]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_ObtenerDetalleDia]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerDetalleDia]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Roque.Solis
-- Create date: 2014/03/21
-- Description: SP para consultar el reparto de la fecha actual
-- Origen     : APInterfaces
-- EXEC Reparto_ObtenerDetalleDia 4, '2014-03-27'
-- =============================================
CREATE PROCEDURE [dbo].[Reparto_ObtenerDetalleDia]
@OrganizacionID INT,
@Fecha DATETIME,
@Activo INT
AS
BEGIN
SET NOCOUNT ON;
	SELECT R.RepartoID,
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
			RD.Observaciones
	FROM Reparto R (NOLOCK)
INNER JOIN RepartoDetalle RD (NOLOCK) on R.RepartoID= RD.RepartoID AND RD.Activo= 1
	WHERE R.OrganizacionID = @OrganizacionID
	AND CAST(R.Fecha AS DATE) = CAST(@Fecha AS DATE)
	AND R.Activo= 1 
	SET NOCOUNT OFF;
END

GO
