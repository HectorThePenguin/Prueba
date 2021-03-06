USE [SIAP]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerRepartoFechaCorralServicio]    Script Date: 15/10/2015 09:31:43 a.m. ******/
DROP PROCEDURE [dbo].[Reparto_ObtenerRepartoFechaCorralServicio]
GO
/****** Object:  StoredProcedure [dbo].[Reparto_ObtenerRepartoFechaCorralServicio]    Script Date: 15/10/2015 09:31:45 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		Jorge Luis Velazquez Araujo
-- Create date: 28/08/2015
-- Description: SP para consultar el reparto de la fecha actual
-- EXEC Reparto_ObtenerRepartoFechaCorral 4, 1, '2014-03-27'
-- =============================================
CREATE PROCEDURE [dbo].[Reparto_ObtenerRepartoFechaCorralServicio]
@OrganizacionID INT,
@CorralID INT,
@Fecha DATETIME,
@TipoServicioID INT
AS
BEGIN
SET NOCOUNT ON;
	SELECT re.RepartoID,
		   re.OrganizacionID,
           re.CorralID,
           re.LoteID,
           re.Fecha,
		   re.PesoInicio,
		   re.PesoProyectado,
		   re.DiasEngorda,
		   re.PesoRepeso
	FROM Reparto re
	inner join RepartoDetalle rd on re.RepartoID = rd.RepartoID
	WHERE re.OrganizacionID = @OrganizacionID
	AND CAST(re.Fecha AS DATE) = CAST(@Fecha AS DATE)
	AND re.CorralID = @CorralID
	AND re.Activo= 1
	AND rd.TipoServicioID = @TipoServicioID
	SET NOCOUNT OFF;
END

GO
